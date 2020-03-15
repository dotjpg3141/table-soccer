using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TableSoccer.Server.Database;
using TableSoccer.Shared.Models;

namespace TableSoccer.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MatchController : ControllerBase
	{
		public MatchController(TableSoccerContext context)
		{
			this.Context = context;
		}

		public TableSoccerContext Context { get; }

		[HttpGet]
		public IEnumerable<Match> Get()
		{
			var dao = new MatchDao(Context);
			return dao.GetAll();
		}

		[HttpPost]
		public IActionResult Post(Match match)
		{
			var dao = new MatchDao(Context);
			dao.Add(match);
			Context.SaveChanges();
			return Ok();
		}
	}
}