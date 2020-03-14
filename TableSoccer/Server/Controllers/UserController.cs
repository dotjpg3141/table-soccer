using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TableSoccer.Shared;

namespace TableSoccer.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private static readonly List<User> users = new List<User> {
			new User() { Name = "A", Score = 1337, Id = 1 },
			new User() { Name = "Q", Score = 1337 , Id = 2 },
			new User() { Name = "S", Score = 1337 , Id = 3 },
			new User() { Name = "S", Score = 1337 , Id = 4 },
			new User() { Name = "D", Score = 1337 , Id = 5 },
			new User() { Name = "DF", Score = 1337 , Id = 6 },
			new User() { Name = "D", Score = 1337 , Id = 7 },
			new User() { Name = "Aasd", Score = 1337 , Id = 8 },
			new User() { Name = "Aqq", Score = 1337 , Id = 9 },
			new User() { Name = "Aw", Score = 1337 , Id = 10 },
			new User() { Name = "Aqew", Score = 1337 , Id = 11 },
			new User() { Name = "Aqq", Score = 42 , Id = 12 },
		};

		[HttpGet]
		public IEnumerable<User> Get()
		{
			return users.OrderByDescending(user => user.Score);
		}

		[HttpPost]
		public IActionResult Post(User user)
		{
			user.Id = users.Select(users => users.Id).DefaultIfEmpty().Max() + 1;
			user.Score = 1000;
			users.Add(user);

			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(long userId)
		{
			var removed = users.RemoveAll(user => user.Id == userId) != 0;
			if (!removed)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}