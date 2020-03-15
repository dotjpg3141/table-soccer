using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TableSoccer.Server.Migrations
{
	public static class MigrationExtensions
	{
		public static void AddForeignKeySqlite(
			[NotNull] this MigrationBuilder builder,
			[NotNull] string name,
			[NotNull] string table,
			[NotNull] string column,
			[NotNull] string principalTable,
			 string schema = null,
			 string principalSchema = null,
			 string principalColumn = null,
			 ReferentialAction onUpdate = ReferentialAction.NoAction,
			 ReferentialAction onDelete = ReferentialAction.NoAction)
		{
			// TODO(jpg): add foreign key
		}

		public static void DropForeignKeySqlite(
			[NotNull] this MigrationBuilder builder,
			[NotNull] string name,
			[NotNull] string table,
			string schema = null)
		{
			// TODO(jpg): remove foreign key
		}
	}
}