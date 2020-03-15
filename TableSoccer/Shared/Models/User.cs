using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableSoccer.Shared.Models
{
	public class User
	{
		[Key]
		public long UserId { get; set; }

		public string Name { get; set; }
		public int Score { get; set; }
		public DateTime CreationDate { get; set; }
	}
}