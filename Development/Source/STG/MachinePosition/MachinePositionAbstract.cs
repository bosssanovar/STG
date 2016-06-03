using System;
using STG.Parameters;

namespace STG.MachinePosition
{
	/// <summary>
	/// 機体位置を管理・制御する抽象クラス。
	/// </summary>
	abstract internal class MachinePositionAbstract : IMachinePosition
	{
		#region コンストラクタ/デストラクタ
		internal MachinePositionAbstract(Position position)
		{
			Position = position;
		}
		#endregion


		#region プロパティ

		/// <summary>
		/// 機体位置の座標を取得します。
		/// </summary>
		public Position Position { get; private set; }

		/// <summary>
		/// 移動指示から移動が完了するまでのラグ(ミリ秒)を設定または取得します。
		/// </summary>
		abstract protected int Intarval { get; }

		#endregion


		#region イベント

		/// <summary>
		/// 機体位置が変更されたときに発行されるイベント
		/// </summary>
		public event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

		#endregion


		#region メソッド

		/// <summary>
		/// 機体をX軸負方向に移動します。
		/// <see cref="Intarval"/>ミリ秒後に<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToLeft()
		{
			Position = GetLeftPosition();
			NotifyMacinePositionChange();
		}

		/// <summary>
		/// 次のX軸負方向位置を取得します。
		/// </summary>
		/// <returns></returns>
		abstract protected Position GetLeftPosition();

		/// <summary>
		/// 機体をX軸正方向に移動します。
		/// <see cref="Intarval"/>ミリ秒後に<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToRight()
		{
			Position = GetRightPosition();
			NotifyMacinePositionChange();
		}

		/// <summary>
		/// 次のX軸正方向位置を取得します。
		/// </summary>
		/// <returns></returns>
		abstract protected Position GetRightPosition();

		/// <summary>
		/// 機体をY軸負方向に移動します。
		/// <see cref="Intarval"/>ミリ秒後に<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToUnder()
		{
			Position = GetUnderPosition();
			NotifyMacinePositionChange();
		}

		/// <summary>
		/// 次のY軸負方向位置を取得します。
		/// </summary>
		/// <returns></returns>
		abstract protected Position GetUnderPosition();

		/// <summary>
		/// 機体をY軸正方向に移動します。
		/// <see cref="Intarval"/>ミリ秒後に<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToUpper()
		{
			Position = GetUpperPosition();
			NotifyMacinePositionChange();
		}

		/// <summary>
		/// 次のY軸正方向位置を取得します。
		/// </summary>
		/// <returns></returns>
		abstract protected Position GetUpperPosition();

		/// <summary>
		/// <see cref="Intarval"/>ミリ秒後に機体移動通知を行います。
		/// </summary>
		private void NotifyMacinePositionChange()
		{
#if TEST
			System.Threading.Thread.Sleep(Intarval);
			MachinePositionChanged?.Invoke(this, new MachinePositionChangedEventArgs(Position));
#else
			Task.Run(new Action(() =>
			{
				System.Threading.Thread.Sleep(Intarval);
				MachinePositionChanged?.Invoke(this, new MachinePositionChangedEventArgs(Position));
			}));
#endif
		}

		#endregion
	}
}
