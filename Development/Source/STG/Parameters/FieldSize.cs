namespace STG.Parameters
{
	/// <summary>
	/// フィールドサイズの管理、判定を行うクラス
	/// </summary>
	public class FieldSize
	{
		#region コンストラクタ/デストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		internal FieldSize() { }
		#endregion


		#region 定数

		/// <summary>
		/// フィールド最大値Xの初期値
		/// </summary>
		internal const int DefaultMaxX = 100;

		/// <summary>
		/// フィールド最大値Yの初期値
		/// </summary>
		internal const int DefaultMaxY = 100;

		/// <summary>
		/// フィールド最小値Xの初期値
		/// </summary>
		internal const int DefaultMinX = 0;

		/// <summary>
		/// フィールド最小値Yの初期値
		/// </summary>
		internal const int DefaultMinY = 0;

		#endregion


		#region プロパティ

		/// <summary>
		/// フィールドの最大値を取得します。
		/// </summary>
		public Position Max { get; private set; } = new Position() { X = 100, Y = 100 };

		/// <summary>
		/// フィールドの最小値を取得します。
		/// </summary>
		public Position Min { get; private set; } = new Position() { X = 0, Y = 0 };

		#endregion


		#region メソッド

		/// <summary>
		/// フィールドサイズの最大値・最小値を設定します。
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public void SetFieldSize(Position min, Position max)
		{
			Max = max;
			Min = min;
		}

		/// <summary>
		/// 指定した位置がフィールド範囲内かどうかを判定します。
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public bool IsIntoField(Position pos)
		{
			if (Max.Compare(pos) == Position.CompareResult.Same) return true;

			if (Min.Compare(pos) == Position.CompareResult.Same) return true;

			if (Min.Compare(pos) == Position.CompareResult.Upper
				|| Min.Compare(pos) == Position.CompareResult.UpperRight
				|| Min.Compare(pos) == Position.CompareResult.Right)
			{
				if (Max.Compare(pos) == Position.CompareResult.Left
					|| Max.Compare(pos) == Position.CompareResult.Under
					|| Max.Compare(pos) == Position.CompareResult.LowerLeft)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 指定した位置からX軸方向に＋１した位置がフィールド内かを判定します。
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool IsintoFieldNextXUpper(Position position) => IsIntoField(new Position(position.X + 1, position.Y));

		/// <summary>
		/// 指定した位置からX軸方向にー１した位置がフィールド内かを判定します。
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool IsintoFieldNextXLower(Position position) => IsIntoField(new Position(position.X - 1, position.Y));

		/// <summary>
		/// 指定した位置からy軸方向に＋１した位置がフィールド内かを判定します。
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool IsintoFieldNextYUpper(Position position) => IsIntoField(new Position(position.X, position.Y + 1));

		/// <summary>
		/// 指定した位置からY軸方向に-１した位置がフィールド内かを判定します。
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool IsintoFieldNextYLower(Position position) => IsIntoField(new Position(position.X, position.Y - 1));

		#endregion
	}
}
