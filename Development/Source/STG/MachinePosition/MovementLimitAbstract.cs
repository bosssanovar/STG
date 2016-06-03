using System;
using STG.Parameters;
using System.Diagnostics.Contracts;

namespace STG.MachinePosition
{
	/// <summary>
	/// 移動を制限する抽象クラス
	/// </summary>
	internal abstract class MovementLimitAbstract : IMachinePosition
	{
		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="postProcess"></param>
		internal MovementLimitAbstract(IMachinePosition postProcess)
		{
			Contract.Requires<ArgumentNullException>(postProcess != null);

			_PostProcess = postProcess;
			postProcess.MachinePositionChanged += PostProcess_MachinePositionChanged;
		}

		#endregion


		#region フィールド

		readonly protected IMachinePosition _PostProcess;

		#endregion


		#region プロパティ

		/// <summary>
		/// 機体位置を取得します。
		/// </summary>
		public Position Position => _PostProcess.Position;

		#endregion


		#region イベント

		/// <summary>
		/// 機体位置が変更されたときに発行されるイベント
		/// </summary>
		public event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

		#endregion


		#region イベントハンドラ

		private void PostProcess_MachinePositionChanged(object sender, MachinePositionChangedEventArgs e)
		{
			MachinePositionChanged?.Invoke(sender, e);
		}

		#endregion


		#region メソッド

		/// <summary>
		/// 機体をX軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		abstract public void MoveToLeft();

		/// <summary>
		/// 機体をX軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		abstract public void MoveToRight();

		/// <summary>
		/// 機体をY軸負方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		abstract public void MoveToUnder();

		/// <summary>
		/// 機体をY軸正方向に移動します。
		/// 移動が完了したタイミングで<see cref="MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		abstract public void MoveToUpper();

		#endregion


		#region Invariant

		/// <summary>
		/// 不変契約を定義します。
		/// </summary>
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_PostProcess != null);
		}

		#endregion
	}
}
