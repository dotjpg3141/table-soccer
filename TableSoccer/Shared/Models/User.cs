using System;
using System.Collections.Generic;

namespace TableSoccer.Shared.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Score { get; set; }
		public DateTime CreationDate { get; set; }
	}
}