using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TableSoccer.Server.Models;
using TableSoccer.Shared.Models;

namespace TableSoccer.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		public UserController(TableSoccerContext context)
		{
			this.Context = context;
		}

		public TableSoccerContext Context { get; }

		[HttpGet]
		public IEnumerable<User> Get()
		{
			return Context.Users.OrderByDescending(user => user.Score);
		}

		[HttpPost]
		public IActionResult Post(User user)
		{
			user.Score = 1000;
			user.CreationDate = DateTime.Now;
			Context.Users.Add(user);
			Context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int userId)
		{
			var user = Context.Users.Find(userId);
			if (user == null)
			{
				return NotFound();
			}

			Context.Users.Remove(user);
			Context.SaveChanges();

			return Ok();
		}
	}
}