using STG.MachinePosition;
using STG.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	/// <summary>
	/// 機体を生成するファクトリクラス
	/// </summary>
	public static class MachineFactory
	{
		#region メソッド

		/// <summary>
		/// 必要な機体を保持した機体管理インスタンスを生成します。
		/// </summary>
		/// <returns></returns>
		public static MachineManager CreateMachines(Position position)
		{
			Contract.Ensures(Contract.Result<MachineManager>() != null);

			return new MachineManager(CreateMachineList(position));
		}

		/// <summary>
		/// 機体リストを生成します。
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private static List<MachineAbstract> CreateMachineList(Position position)
		{
			Contract.Ensures(Contract.Result<List<MachineAbstract>>() != null);

			var ret = new List<MachineAbstract>()
				{
					new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(position))
				};

			return ret.Where(e => e != null).ToList();
		}

		#endregion
	}
}
