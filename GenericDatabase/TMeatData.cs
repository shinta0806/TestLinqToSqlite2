// ============================================================================
// 
// 【共通カラムジェネリック運用】肉テーブル
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
	[Table("t_meat")]
	internal class TMeatData : IFoodData
	{
		// --------------------------------------------------------------------
		// IFoodData 実装
		// --------------------------------------------------------------------

		// ID
		[Key]
		[Column("meat_id")]
		public Int32 Id { get; set; }

		// 名前
		[Column("meat_name")]
		public String Name { get; set; } = String.Empty;

		// --------------------------------------------------------------------
		// TMeatData 独自項目
		// --------------------------------------------------------------------

		// 料理名
		[Column("meat_cooking")]
		public String? Cooking { get; set; }
	}
}
