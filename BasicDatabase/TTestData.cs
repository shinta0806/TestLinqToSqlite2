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
	// 複数個のインデックスを作成する場合は [Index(nameof(Height))] のように注釈を追加する
	// 複合インデックスを作成する場合は [Index(nameof(Name), nameof(Height))] のように 1 つの注釈で複数のカラムを記載する
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
