// ============================================================================
// 
// 【基本操作用】テスト用データベースのコンテキスト
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace TestLinqToSqlite2.BasicDatabase
{
	internal class TestContext : DbContext
	{
		// ====================================================================
		// コンストラクター
		// ====================================================================

		// --------------------------------------------------------------------
		// デフォルトコンストラクター
		// --------------------------------------------------------------------
		public TestContext()
		{
			Debug.Assert(TestData != null);
		}

		// ====================================================================
		// public プロパティー
		// ====================================================================

		// テストテーブル
		public DbSet<TTestData> TestData { get; set; }

		// ====================================================================
		// protected メンバー関数
		// ====================================================================

		// --------------------------------------------------------------------
		// データベース設定
		// --------------------------------------------------------------------
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			SqliteConnectionStringBuilder stringBuilder = new()
			{
				DataSource = "Test.sqlite3",
			};
			SqliteConnection sqliteConnection = new(stringBuilder.ToString());

#if false
			// インメモリデータベースの場合は接続を Open() しておく必要があります。（データベースの寿命に注意）
			SqliteConnectionStringBuilder stringBuilder = new()
			{
				DataSource = "Test.sqlite3",
				Mode = SqliteOpenMode.Memory,
				Cache = SqliteCacheMode.Shared,
			};
			SqliteConnection sqliteConnection = new(stringBuilder.ToString());
			sqliteConnection.Open();
#endif

#if false
			// EF Core ではデフォルトでジャーナルモードが WAL になっています。DELETE にしたい場合は以下を実行します。
			sqliteConnection.Open();
			using SqliteCommand command = sqliteConnection.CreateCommand();
			command.CommandText = @"PRAGMA journal_mode = 'delete'";
			command.ExecuteNonQuery();
#endif

			optionsBuilder.UseSqlite(sqliteConnection);
		}
	}
}
