using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TableSoccer.Shared.Models;

namespace TableSoccer.Server.Database
{
	public class MatchDao
	{
		public MatchDao(TableSoccerContext context)
		{
			this.Context = context;
		}

		public TableSoccerContext Context { get; }

		public Match Get(int matchId)
		{
			var match = Context.Matches.Find(matchId);
			if (match == null)
			{
				throw new KeyNotFoundException();
			}

			return match;
		}

		public IQueryable<Match> GetAll()
		{
			var query = Context.Matches
				.Include(match => match.Teams)
					.ThenInclude(team => team.TeamMembers)
					.ThenInclude(teamMember => teamMember.User);
			return query.OrderByDescending(match => match.Time);
		}

		public void Add(Match match)
		{
			match.Time = DateTime.Now;

			foreach (var (index, team) in match.Teams.OrderBy(team => team.Position).Enumerate())
			{
				team.Position = index;
			}

			Context.Matches.Add(match);
		}
	}
}