using STG.MachinePosition;
using STG.Parameters;
using System;
using System.Diagnostics.Contracts;

namespace STG
{


	/// <summary>
	/// 機体の抽象クラス
	/// </summary>
	public abstract class MachineAbstract
	{
		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="machinePosition"></param>
		public MachineAbstract(IMachinePosition machinePosition)
		{
			Contract.Requires<ArgumentNullException>(machinePosition != null);

			_MachinePosition = machinePosition;
			_MachinePosition.MachinePositionChanged += _MachinePosition_MachinePositionChanged;
		}

		#endregion


		#region フィールド

		/// <summary>
		/// 機体位置管理インスタンス
		/// </summary>
		protected IMachinePosition _MachinePosition;

		#endregion


		#region プロパティ

		/// <summary>
		/// 機体位置を取得します。
		/// </summary>
		public Position Position => _MachinePosition.Position;

		#endregion


		#region イベント

		/// <summary>
		/// 機体位置が変更された際に発行されるイベント
		/// </summary>
		public event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

		#endregion


		#region イベントハンドラ

		/// <summary>
		/// 機体位置が変更された際に発行されるイベントのハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _MachinePosition_MachinePositionChanged(object sender, MachinePositionChangedEventArgs e)
		{
			MachinePositionChanged?.Invoke(this, e);
		}
		#endregion


		#region メソッド

		/// <summary>
		/// 機体をX軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToRight()
		{
			_MachinePosition.MoveToRight();
		}

		/// <summary>
		/// 機体をX軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToLeft()
		{
			_MachinePosition.MoveToLeft();
		}

		/// <summary>
		/// 機体をY軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToUpper()
		{
			_MachinePosition.MoveToUpper();
		}

		/// <summary>
		/// 機体をY軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public void MoveToUnder()
		{
			_MachinePosition.MoveToUnder();
		}

		#endregion

		#region Invariant

		/// <summary>
		/// 不変契約を定義します。
		/// </summary>
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_MachinePosition != null);
		}

		#endregion
	}
}
