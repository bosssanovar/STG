﻿using STG.Parameters;
using System.Diagnostics.Contracts;

namespace STG.MachinePosition
{
	/// <summary>
	/// <see cref="IMachinePosition"/>を実装した実態を生成するFactoryクラス。
	/// </summary>
	public class MachinePositionFactory
	{
		/// <summary>
		/// <see cref="IMachinePosition"/>を実装したインスタンスを取得します。
		/// </summary>
		/// <returns></returns>
		public static IMachinePosition CreateMachinePositionInstance(Position initialPosition)
		{
			Contract.Ensures(Contract.Result<IMachinePosition>() != null);

			return new AreaEndLimit(new NormalMachinePosition(initialPosition));
		}
	}
}
