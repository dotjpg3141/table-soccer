using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TableSoccer.Server.Database;
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
			var dao = new UserDao(Context);
			return dao.GetAll();
		}

		[HttpGet("lookup")]
		public IEnumerable<KeyValuePair<long, string>> Lookup()
		{
			// TODO(jpg): proper lookup type
			var dao = new UserDao(Context);
			return dao.GetAll().Select(user => KeyValuePair.Create(user.UserId, user.Name)).ToList();
		}

		[HttpPost]
		public IActionResult Post(User user)
		{
			var dao = new UserDao(Context);
			dao.Add(user);
			Context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int userId)
		{
			var dao = new UserDao(Context);
			dao.Remove(userId);
			Context.SaveChanges();
			return Ok();
		}
	}
}