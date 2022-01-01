// ============================================================================
// 
// 【基本操作用】テスト用テーブル
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestLinqToSqlite2.BasicDatabase
{
	[Table("t_test")]
	[Index(nameof(Name), IsUnique = true)]
	internal class TTestData
	{
		// ID
		[Key]
		[Column("test_id")]
		public Int32 Id { get; set; }

		// 氏名
		[Column("test_name")]
		public String Name { get; set; } = String.Empty;

		// 身長
		[Column("test_height")]
		public Double? Height { get; set; }
	}
}
