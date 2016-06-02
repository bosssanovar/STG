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

		[TestCase(Position.CompareResult.Right, 50, 50, 51, 50)]
		[TestCase(Position.CompareResult.Left, 50, 50, 49, 50)]
		[TestCase(Position.CompareResult.Upper, 50, 50, 50, 51)]
		[TestCase(Position.CompareResult.Under, 50, 50, 50, 49)]
		public void OwnMachineTest_移動(Position.CompareResult direction, int initX, int initY, int resultX, int resultY)
		{
			var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(initX, initY)));
			own.MachinePositionChanged += (sender, e) =>
			  {
				  Assert.That(sender.Equals(own));
				  Assert.That(e.Position.X == resultX);
				  Assert.That(e.Position.Y == resultY);
			  };

			switch (direction)
			{
				case Position.CompareResult.Right:
					own.MoveToRight();
					break;
				case Position.CompareResult.Upper:
					own.MoveToUpper();
					break;
				case Position.CompareResult.Left:
					own.MoveToLeft();
					break;
				case Position.CompareResult.Under:
					own.MoveToUnder();
					break;
				default:
					break;
			}
		}

		[Test]
		public void OwnMachineTest_MachinePositionChangedイベント登録なしの場合()
		{
			var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 50)));

			own.MoveToRight();

			Assert.Pass();
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

		[TestCase(Position.CompareResult.Right, 50, 50, 51, 50)]
		[TestCase(Position.CompareResult.Left, 50, 50, 49, 50)]
		[TestCase(Position.CompareResult.Upper, 50, 50, 50, 51)]
		[TestCase(Position.CompareResult.Under, 50, 50, 50, 49)]
		public void MachineManager_移動(Position.CompareResult direction, int initX, int initY, int resultX, int resultY)
		{
			var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(initX, initY)));
			var manager = new MachineManager(new List<MachineAbstract>() { own });
			manager.MachinePositionChanged += (sender, e) =>
			{
				Assert.That(sender.Equals(own));
				Assert.That(e.Position.X == resultX);
				Assert.That(e.Position.Y == resultY);
			};

			switch (direction)
			{
				case Position.CompareResult.Right:
					manager.GetOwnMachine().MoveToRight();
					break;
				case Position.CompareResult.Upper:
					manager.GetOwnMachine().MoveToUpper();
					break;
				case Position.CompareResult.Left:
					manager.GetOwnMachine().MoveToLeft();
					break;
				case Position.CompareResult.Under:
					manager.GetOwnMachine().MoveToUnder();
					break;
				default:
					break;
			}
		}
	}
}