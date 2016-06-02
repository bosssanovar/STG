using System.Windows;

namespace STG.Parameters
{
	/// <summary>
	/// 座標を定義する構造体
	/// </summary>
	public struct Position
	{

		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		#endregion


		#region enum


		/// <summary>
		/// Positionの比較結果列挙
		/// </summary>
		public enum CompareResult
		{
			/// <summary> 同位置 </summary>
			Same,
			/// <summary> 右 </summary>
			Right,
			/// <summary> 右上 </summary>
			UpperRight,
			/// <summary> 上 </summary>
			Upper,
			/// <summary> 左上 </summary>
			UpperLeft,
			/// <summary> 左 </summary>
			Left,
			/// <summary> 左下 </summary>
			LowerLeft,
			/// <summary> 下 </summary>
			Under,
			/// <summary> 右下 </summary>
			LowerRight
		}

		#endregion


		#region プロパティ
		/// <summary>
		/// X座標を設定、または取得します。
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Y座標を設定、または取得します。
		/// </summary>
		public int Y { get; set; }
		#endregion


		#region メソッド

		/// <summary>
		/// 引数の<see cref="Position"/>インスタンスがどちら方向に位置するかを判定します。
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public CompareResult Compare(Position target)
		{
			if (X == target.X && Y == target.Y) return CompareResult.Same;

			var angle = System.Math.Atan2(target.Y - Y, target.X - X) * (180 / System.Math.PI);

			if (angle == 0) return CompareResult.Right;
			else if (angle > 0 && angle < 90) return CompareResult.UpperRight;
			else if (angle == 90) return CompareResult.Upper;
			else if (angle > 90 && angle < 180) return CompareResult.UpperLeft;
			else if (angle == 180) return CompareResult.Left;
			else if (angle < 0 && angle > -90) return CompareResult.LowerRight;
			else if (angle == -90) return CompareResult.Under;
			else return CompareResult.LowerLeft; //(angle < -90 && angle > -180)
		}

		#endregion
	}
}
