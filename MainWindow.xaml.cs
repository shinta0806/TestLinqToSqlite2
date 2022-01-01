// ============================================================================
// 
// Test LINQ to SQLite 2　メインウィンドウ　コードビハインド
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using TestLinqToSqlite2.BasicDatabase;

namespace TestLinqToSqlite2
{
	public partial class MainWindow : Window
	{
		// ====================================================================
		// コンストラクター
		// ====================================================================

		// --------------------------------------------------------------------
		// デフォルトコンストラクター
		// --------------------------------------------------------------------
		public MainWindow()
		{
			InitializeComponent();
		}

		// ====================================================================
		// private メンバー関数
		// ====================================================================

		// --------------------------------------------------------------------
		// 基本操作用のデータベースを作成（存在しない場合のみ）
		// --------------------------------------------------------------------
		private void CreateTestDatabaseIfNeeded()
		{
			using TestContext testContext = new();

			// データベースが存在しない場合は作成
			testContext.Database.EnsureCreated();

			// レコードが存在していればこれ以上は何もしない
			if (testContext.TestData.Any())
			{
				return;
			}

			// レコードが存在しない場合は作成
			testContext.TestData.Add(new TTestData { Name = "Fukada Kyoko" });
			testContext.TestData.Add(new TTestData { Name = "Eda Ha", Height = 159.0 });
			testContext.TestData.Add(new TTestData { Name = "Dan Gerou", Height = 150.5 });
			testContext.TestData.Add(new TTestData { Name = "Baba Takashi" });
			testContext.TestData.Add(new TTestData { Name = "Aikawa Ai", Height = 145.6 });
			testContext.TestData.Add(new TTestData { Name = "Ccc cc", Height = 120.0 });
			testContext.TestData.Add(new TTestData { Name = "Ggg gg", Height = 165.0 });
			testContext.SaveChanges();
			MessageBox.Show("基本操作用のデータベースにレコードを作成しました：" + testContext.TestData.Count() + " 件");
		}

		// --------------------------------------------------------------------
		// エラーメッセージ表示
		// --------------------------------------------------------------------
		private void ShowErrorMessage(String message)
		{
			MessageBox.Show(message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		// --------------------------------------------------------------------
		// 検索結果の表示（基本操作用）
		// --------------------------------------------------------------------
		private void ShowQueryResult(String title, IEnumerable<TTestData> queryResult)
		{
			StringBuilder message = new(title + "：" + queryResult.Count().ToString() + " 件\n");
			foreach (TTestData data in queryResult)
			{
				message.Append(data.Id.ToString() + ", " + data.Name + ", ");
				if (data.Height.HasValue)
				{
					message.Append(data.Height.Value.ToString() + " cm");
				}
				else
				{
					message.Append("-");
				}
				message.Append("\n");
			}
			MessageBox.Show(message.ToString());
		}

		// ====================================================================
		// private イベントハンドラー
		// ====================================================================

		private void ButtonBasicQuery_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CreateTestDatabaseIfNeeded();

				// 検索
				using TestContext testContext = new();
				IQueryable<TTestData> queryResult = testContext.TestData.Where(x => x.Name == "Eda Ha" || x.Height < 150.0).OrderBy(x => x.Height);
				ShowQueryResult("条件に合致する人（身長順）", queryResult);
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
			}
		}

		private void ButtonBasicDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CreateTestDatabaseIfNeeded();

				// 検索
				using TestContext testContext = new();
				IQueryable<TTestData> queryResult = testContext.TestData.Where(x => x.Height == null || x.Height < 150.0);
				Int32 num = queryResult.Count();

				// ヒットした人を削除
				testContext.TestData.RemoveRange(queryResult);
				testContext.SaveChanges();
				MessageBox.Show(num.ToString() + " 件削除しました。");
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
			}
		}

		private void ButtonBasicUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CreateTestDatabaseIfNeeded();

				// 検索
				using TestContext testContext = new();
				List<TTestData> queryResult = testContext.TestData.Where(x => x.Height >= 150.0).ToList();

				// ヒットした人の身長を下げる
				foreach (TTestData data in queryResult)
				{
					data.Height -= 10.0;
				}
				testContext.SaveChanges();

				ShowQueryResult("身長を下げました", queryResult);
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
			}
		}
	}
}
