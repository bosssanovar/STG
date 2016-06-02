using STG.Parameters;
using System;

namespace STG.MachinePosition
{
	/// <summary>
	/// 機体位置を管理・制御するインターフェース
	/// </summary>
	public interface IMachinePosition
	{
		#region プロパティ
		/// <summary>
		/// 機体位置の座標を取得します。
		/// </summary>
		Position Position { get; }
		#endregion


		#region イベント

		/// <summary>
		/// 機体位置が変更されたときに発行されるイベント
		/// </summary>
		event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

		#endregion


		#region メソッド

		/// <summary>
		/// 機体をX軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		void MoveToRight();

		/// <summary>
		/// 機体をX軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		void MoveToLeft();

		/// <summary>
		/// 機体をY軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		void MoveToUpper();

		/// <summary>
		/// 機体をY軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		void MoveToUnder();

		#endregion
	}
}
