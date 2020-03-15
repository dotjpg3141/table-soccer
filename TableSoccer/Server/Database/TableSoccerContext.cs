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
		public DbSet<Match> Matches { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<TeamMember> TeamMembers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=" + fileName);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TeamMember>()
				.HasKey(member => new { member.TeamId, member.UserId });
		}
	}
}