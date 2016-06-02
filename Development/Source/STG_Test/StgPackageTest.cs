using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using STG;
using STG.MachinePosition;
using STG.Parameters;

namespace STG_Test
{
	[TestFixture]
	class OwnMachineTest
	{
		[TestCase(0, 0)]
		[TestCase(100, 100)]
		[TestCase(50, 30)]
		public void OwnMachineTest_生成(int x, int y)
		{
			var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(x, y)));

			Assert.That(own.Position.X == x);
			Assert.That(own.Position.Y == y);
		}

		[TestCase()]
		public  void OwnMachineTest_移動()
		{
			// TODO K.I : 対応
		}
	}

	[TestFixture]
	class MachineManagerTest
	{
		[Test]
		public void MachineManager_GetOwnMachine()
		{
			var manager = new MachineManager(new List<MachineAbstract>() { new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 30))) });

			Assert.That(manager.GetOwnMachine() is OwnMachine);
			Assert.That(manager.GetOwnMachine().Position.X == 50);
			Assert.That(manager.GetOwnMachine().Position.Y == 30);
		}

		[Test]
		public void MachineManager_コンストラクタ()
		{
			Assert.Throws<ArgumentNullException>(() => new MachineManager(null));
			Assert.DoesNotThrow(() => new MachineManager(new List<MachineAbstract>() { new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 30))) }));
		}
	}
}