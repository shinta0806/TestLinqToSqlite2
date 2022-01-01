// ============================================================================
// 
// Test LINQ to SQLite 2　メインウィンドウ　コードビハインド
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using TestLinqToSqlite2.BasicDatabase;
using TestLinqToSqlite2.GenericDatabase;

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
		// 食べ物データベースを作成（存在しない場合のみ）
		// --------------------------------------------------------------------
		private void CreateFoodDatabaseIfNeeded()
		{
			using FoodContext foodContext = new();

			// データベースが存在しない場合は作成
			foodContext.Database.EnsureCreated();

			if (!foodContext.FruitData.Any())
			{
				// フルーツレコードが存在しない場合は作成
				foodContext.FruitData.Add(new TFruitData { Name = "Apple", Color = "Red" });
				foodContext.FruitData.Add(new TFruitData { Name = "Banana", Color = "Yellow" });
				foodContext.FruitData.Add(new TFruitData { Name = "Strawberry", Color = "Red" });
				foodContext.FruitData.Add(new TFruitData { Name = "Orange", Color = "Orange" });
				foodContext.FruitData.Add(new TFruitData { Name = "Grape" });
				foodContext.SaveChanges();
				MessageBox.Show("食べ物データベースにフルーツレコードを作成しました：" + foodContext.FruitData.Count() + " 件");
			}

			if (!foodContext.MeatData.Any())
			{
				// 肉レコードが存在しない場合は作成
				foodContext.MeatData.Add(new TMeatData { Name = "Pork" });
				foodContext.MeatData.Add(new TMeatData { Name = "Beef", Cooking = "Burger" });
				foodContext.MeatData.Add(new TMeatData { Name = "Chicken", Cooking = "Fried chicken" });
				foodContext.SaveChanges();
				MessageBox.Show("食べ物データベースに肉レコードを作成しました：" + foodContext.MeatData.Count() + " 件");
			}
		}

		// --------------------------------------------------------------------
		// 基本操作用のデータベースを作成（存在しない場合のみ）
		// --------------------------------------------------------------------
		private void CreateTestDatabaseIfNeeded()
		{
			using TestContext testContext = new();

			// データベースが存在しない場合は作成
			testContext.Database.EnsureCreated();

			if (!testContext.TestData.Any())
			{
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
		}

		// --------------------------------------------------------------------
		// 食べ物テーブルを名前で検索（ジェネリック運用）
		// --------------------------------------------------------------------
		private void QueryFoodByName<T>(DbSet<T> table, String keyword) where T : class, IFoodData
		{
			IQueryable<T> queryResult = table.Where(x => EF.Functions.Like(x.Name, $"%{keyword}%"));
			ShowQueryResult(typeof(T).Name + " で名前に「" + keyword + "」を含む結果", queryResult);
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

		// --------------------------------------------------------------------
		// 検索結果の表示（ジェネリック運用用）
		// --------------------------------------------------------------------
		private void ShowQueryResult(String title, IEnumerable<IFoodData> queryResult)
		{
			StringBuilder message = new(title + "：" + queryResult.Count().ToString() + " 件\n");
			foreach (IFoodData data in queryResult)
			{
				message.Append(data.Id.ToString() + ", " + data.Name + "\n");
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

		private void ButtonGenericQuery_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CreateFoodDatabaseIfNeeded();

				using FoodContext foodContext = new();

				// フルーツ検索
				QueryFoodByName(foodContext.FruitData, "P");

				// 肉検索
				QueryFoodByName(foodContext.MeatData, "E");
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
			}
		}
	}
}
