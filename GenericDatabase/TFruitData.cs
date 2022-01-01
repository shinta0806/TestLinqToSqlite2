// ============================================================================
// 
// 【共通カラムジェネリック運用】フルーツテーブル
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestLinqToSqlite2.GenericDatabase
{
	[Table("t_fruit")]
	internal class TFruitData : IFoodData
	{
		// --------------------------------------------------------------------
		// IFoodData 実装
		// --------------------------------------------------------------------

		// ID
		[Key]
		[Column("fruit_id")]
		public Int32 Id { get; set; }

		// 名前
		[Column("fruit_name")]
		public String Name { get; set; } = String.Empty;

		// --------------------------------------------------------------------
		// TFruitData 独自項目
		// --------------------------------------------------------------------

		// 色
		[Column("fruit_color")]
		public String? Color { get; set; }
	}
}
