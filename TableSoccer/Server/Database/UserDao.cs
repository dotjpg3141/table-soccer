using System;
using System.Collections.Generic;
using System.Linq;
using TableSoccer.Shared.Models;

namespace TableSoccer.Server.Database
{
	public class UserDao
	{
		public UserDao(TableSoccerContext context)
		{
			this.Context = context;
		}

		public TableSoccerContext Context { get; }

		public User Get(int userId)
		{
			var user = Context.Users.Find(userId);
			if (user == null)
			{
				throw new KeyNotFoundException();
			}
			return user;
		}

		public IQueryable<User> GetAll()
		{
			return Context.Users.OrderByDescending(user => user.Score);
		}

		public void Add(User user)
		{
			user.Score = 1000;
			user.CreationDate = DateTime.Now;
			Context.Users.Add(user);
		}

		public void Remove(int userId)
		{
			var user = Get(userId);
			Context.Users.Remove(user);
		}
	}
}