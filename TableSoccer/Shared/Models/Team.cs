using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TableSoccer.Shared.Validations;

namespace TableSoccer.Shared.Models
{
	public class Team
	{
		[Key]
		public long TeamId { get; set; }

		public long MatchId { get; set; }

		public int Position { get; set; }

		[Range(0.0, double.PositiveInfinity, ErrorMessage = "Non-negative scores required")]
		public int Result { get; set; }

		[ForeignKey("MatchId")]
		[JsonIgnore]
		public virtual Match Match { get; set; }

		[InverseProperty("Team")]
		[CollectionCount(MinCount = 1)]
		[ValidateComplexType]
		public virtual ICollection<TeamMember> TeamMembers { get; set; } = new HashSet<TeamMember>();

		[NotMapped]
		public string Name => ((char)('A' + Position)).ToString();
	}
}