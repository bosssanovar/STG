using STG.Parameters;
using System;

namespace STG.MachinePosition
{
	/// <summary>
	/// 機体位置の変更通知イベント用データ
	/// </summary>
	public class MachinePositionChangedEventArgs : EventArgs
	{
		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="position"></param>
		public MachinePositionChangedEventArgs(Position position)
		{
			Position = position;
		}

		#endregion


		#region プロパティ
		/// <summary>
		/// 機体の位置を取得します。
		/// </summary>
		public Position Position { get; }
		#endregion
	}
}
