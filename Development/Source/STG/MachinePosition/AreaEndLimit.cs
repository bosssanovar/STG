using STG.Parameters;
using System;
using System.Diagnostics.Contracts;

namespace STG.MachinePosition
{
	internal class AreaEndLimit : MovementLimitAbstract
	{

		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="postProcess"></param>
		internal AreaEndLimit(IMachinePosition postProcess) : base(postProcess)
		{
			Contract.Requires<ArgumentNullException>(postProcess != null);
		}

		#endregion


		#region メソッド

		/// <summary>
		/// 機体をX軸負方向に移動します。フィールドサイズ外には移動しません。
		/// 移動が完了したタイミングで<see cref="MovementLimitAbstract.MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public override void MoveToLeft()
		{
			if (!FieldSizeFactory.GetFieldSizeInstance().IsintoFieldNextXLower(this.Position)) return;

			_PostProcess.MoveToLeft();
		}

		/// <summary>
		/// 機体をX軸正方向に移動します。フィールドサイズ外には移動しません。
		/// 移動が完了したタイミングで<see cref="MovementLimitAbstract.MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public override void MoveToRight()
		{
			if (!FieldSizeFactory.GetFieldSizeInstance().IsintoFieldNextXUpper(this.Position)) return;

			_PostProcess.MoveToRight();
		}

		/// <summary>
		/// 機体をY軸負方向に移動します。フィールドサイズ外には移動しません。
		/// 移動が完了したタイミングで<see cref="MovementLimitAbstract.MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public override void MoveToUnder()
		{
			if (!FieldSizeFactory.GetFieldSizeInstance().IsintoFieldNextYLower(this.Position)) return;

			_PostProcess.MoveToUnder();
		}

		/// <summary>
		/// 機体をY軸正方向に移動します。フィールドサイズ外には移動しません。
		/// 移動が完了したタイミングで<see cref="MovementLimitAbstract.MachinePositionChanged"/>イベントが発行されます。
		/// </summary>
		public override void MoveToUpper()
		{
			if (!FieldSizeFactory.GetFieldSizeInstance().IsintoFieldNextYUpper(this.Position)) return;

			_PostProcess.MoveToUpper();
		}

		#endregion		
	}
}
