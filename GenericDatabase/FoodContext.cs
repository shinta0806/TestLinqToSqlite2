// ============================================================================
// 
// 【共通カラムジェネリック運用】食べ物データベースのコンテキスト
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace TestLinqToSqlite2.GenericDatabase
{
	internal class FoodContext : DbContext
	{
		// ====================================================================
		// コンストラクター
		// ====================================================================

		// --------------------------------------------------------------------
		// デフォルトコンストラクター
		// --------------------------------------------------------------------
		public FoodContext()
		{
			Debug.Assert(FruitData != null);
			Debug.Assert(MeatData != null);
		}

		// ====================================================================
		// public プロパティー
		// ====================================================================

		// フルーツテーブル
		public DbSet<TFruitData> FruitData { get; set; }

		// 肉テーブル
		public DbSet<TMeatData> MeatData { get; set; }

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
				DataSource = "Food.sqlite3",
			};
			using SqliteConnection sqliteConnection = new(stringBuilder.ToString());
			optionsBuilder.UseSqlite(sqliteConnection);
		}
	}
}
