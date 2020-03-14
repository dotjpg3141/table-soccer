using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TableSoccer.Server.Models;

namespace TableSoccer.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var forceMigration = args.Contains("--force-migration", StringComparer.InvariantCultureIgnoreCase);
			MigrateDatabase(forceMigration);

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		private static void MigrateDatabase(bool force)
		{
			if (!force && File.Exists(TableSoccerContext.fileName))
			{
				return;
			}

			Console.WriteLine("Migrating database");

			using (var context = new TableSoccerContext())
			{
				context.Database.Migrate();
			}
		}
	}
}