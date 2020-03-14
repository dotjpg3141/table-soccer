using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TableSoccer.Shared.Models;

namespace TableSoccer.Server.Database
{
	public class TableSoccerContext : DbContext
	{
		internal static readonly string fileName = "table-soccer.sqlite";

		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=" + fileName);
		}
	}
}