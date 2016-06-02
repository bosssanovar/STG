using STG.MachinePosition;
using System;
using System.Diagnostics.Contracts;

namespace STG
{
	internal class OwnMachine : MachineAbstract
	{
		public OwnMachine(IMachinePosition machinePosition) : base(machinePosition)
		{
			Contract.Requires<ArgumentNullException>(machinePosition != null);
		}
	}
}
