using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TableSoccer.Shared.Models
{
	public class TeamMember
	{
		// [Key]
		public long TeamId { get; set; }

		// [Key]
		[Range(1.0, double.PositiveInfinity, ErrorMessage = "User is required")]
		public long UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		[ForeignKey("TeamId")]
		[JsonIgnore]
		public virtual Team Team { get; set; }
	}
}