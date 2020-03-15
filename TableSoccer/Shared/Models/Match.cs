using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TableSoccer.Shared.Validations;

namespace TableSoccer.Shared.Models
{
	public class Match : IValidatableObject
	{
		[Key]
		public long MatchId { get; set; }

		public DateTime Time { get; set; }

		[InverseProperty("Match")]
		[CollectionCount(2)]
		[ValidateComplexType]
		public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var allUsers = from team in Teams ?? Enumerable.Empty<Team>()
						   from member in team.TeamMembers ?? Enumerable.Empty<TeamMember>()
						   select member.User?.UserId ?? member.UserId;

			if (allUsers.Count() != allUsers.Distinct().Count())
			{
				yield return new ValidationResult("Duplicate users in match");
			}
		}
	}
}