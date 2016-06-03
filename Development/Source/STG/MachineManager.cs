using STG.MachinePosition;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace STG
{
	/// <summary>
	/// 機体管理クラス
	/// </summary>
	public class MachineManager
	{
		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="machines">※nullな要素は除外されます</param>
		internal MachineManager(IList<MachineAbstract> machines)
		{
			Contract.Requires<ArgumentNullException>(machines != null);

			_Machines = machines.Where(m => m != null).ToList();

			foreach (var machine in _Machines)
			{
				Contract.Assume(machine != null);
				machine.MachinePositionChanged += Machine_MachinePositionChanged;
			}
		}

		#endregion


		#region フィールド

		readonly IList<MachineAbstract> _Machines;

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
		private void Machine_MachinePositionChanged(object sender, MachinePositionChangedEventArgs e)
		{
			MachinePositionChanged?.Invoke(sender, e);
		}

		#endregion


		#region メソッド

#pragma warning disable CSE0003 // Use expression-bodied members
		/// <summary>
		/// 自機を取得します。
		/// </summary>
		/// <returns></returns>
		public MachineAbstract GetOwnMachine()
		{
			return _Machines.Where(m => m is OwnMachine).FirstOrDefault();
		}
#pragma warning restore CSE0003 // Use expression-bodied members

		#endregion

		#region Invariant

		/// <summary>
		/// 不変契約を定義します。
		/// </summary>
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_Machines != null);
		}

		#endregion
	}
}
