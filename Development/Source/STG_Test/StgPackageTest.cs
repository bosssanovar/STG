﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using STG;
using STG.MachinePosition;
using STG.Parameters;
using System.Reflection;

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

        [TestCase(Position.Direction.Right, 50, 50, 50 + FieldSize.DefaultUnitMovement, 50)]
        [TestCase(Position.Direction.Left, 50, 50, 50 - FieldSize.DefaultUnitMovement, 50)]
        [TestCase(Position.Direction.Upper, 50, 50, 50, 50 + FieldSize.DefaultUnitMovement)]
        [TestCase(Position.Direction.Under, 50, 50, 50, 50 - FieldSize.DefaultUnitMovement)]
        public void OwnMachineTest_移動(Position.Direction direction, int initX, int initY, int resultX, int resultY)
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
                case Position.Direction.Right:
                    own.MoveToRight();
                    break;
                case Position.Direction.Upper:
                    own.MoveToUpper();
                    break;
                case Position.Direction.Left:
                    own.MoveToLeft();
                    break;
                case Position.Direction.Under:
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
            var manager = new MachineManager(new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 30))));

            Assert.That(manager.GetOwnMachine() is OwnMachine);
            Assert.That(manager.GetOwnMachine().Position.X == 50);
            Assert.That(manager.GetOwnMachine().Position.Y == 30);
        }

        [Test]
        public void MachineManager_コンストラクタ()
        {
            Assert.Throws<ArgumentNullException>(() => new MachineManager(null));

            Assert.DoesNotThrow(() => new MachineManager(new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 30)))));
        }

        [TestCase(Position.Direction.Right, 50, 50, FieldSize.DefaultUnitMovement, 0)]
        [TestCase(Position.Direction.Left, 50, 50, -FieldSize.DefaultUnitMovement, 0)]
        [TestCase(Position.Direction.Upper, 50, 50, 0, FieldSize.DefaultUnitMovement)]
        [TestCase(Position.Direction.Under, 50, 50, 0, -FieldSize.DefaultUnitMovement)]
        public void MachineManager_移動(Position.Direction direction, int initX, int initY, int offsetX, int offsetY)
        {
            var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(initX, initY)));
            var manager = new MachineManager(own);
            int count = 0;
            manager.MachinePositionChanged += (sender, e) =>
            {
                count++;
                Assert.That(sender.Equals(own));
                Assert.That(e.Position.X == 50 + offsetX * count);
                Assert.That(e.Position.Y == 50 + offsetY * count);
            };

            for (int cnt = 0; cnt < 100; cnt++)
            {
                switch (direction)
                {
                    case Position.Direction.Right:
                        manager.GetOwnMachine().MoveToRight();
                        break;
                    case Position.Direction.Upper:
                        manager.GetOwnMachine().MoveToUpper();
                        break;
                    case Position.Direction.Left:
                        manager.GetOwnMachine().MoveToLeft();
                        break;
                    case Position.Direction.Under:
                        manager.GetOwnMachine().MoveToUnder();
                        break;
                    default:
                        break;
                }
            }
        }

        [TestCase(Position.Direction.Right, 50, 50, 50 + FieldSize.DefaultUnitMovement, 50)]
        [TestCase(Position.Direction.Left, 50, 50, 50 - FieldSize.DefaultUnitMovement, 50)]
        [TestCase(Position.Direction.Upper, 50, 50, 50, 50 + FieldSize.DefaultUnitMovement)]
        [TestCase(Position.Direction.Under, 50, 50, 50, 50 - FieldSize.DefaultUnitMovement)]
        public void MachineManager_移動_イベント登録なし(Position.Direction direction, int initX, int initY, int resultX, int resultY)
        {
            var own = new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(new Position(initX, initY)));
            var manager = new MachineManager(own);

            for (int cnt = 0; cnt < 100; cnt++)
            {
                switch (direction)
                {
                    case Position.Direction.Right:
                        manager.GetOwnMachine().MoveToRight();
                        break;
                    case Position.Direction.Upper:
                        manager.GetOwnMachine().MoveToUpper();
                        break;
                    case Position.Direction.Left:
                        manager.GetOwnMachine().MoveToLeft();
                        break;
                    case Position.Direction.Under:
                        manager.GetOwnMachine().MoveToUnder();
                        break;
                    default:
                        break;
                }
            }

            Assert.Pass();
        }

        [Test]
        public void MachineManager_GetMachines()
        {
            var pos = new Position(10, 10);
            var manager = new MachineFactory(pos);

            var machines = manager.Machines.GetMachines();

            Assert.That(machines.Count == 1);
            Assert.That(machines[0].Position.Compare(pos) == Position.Direction.Same);
        }

    }

    [TestFixture]
    class MachineFactoryTest
    {
        [Test]
        public void MachineFactory_CreateMachines()
        {
            var factory = new MachineFactory(new Position(50, 30));
            var manager = factory.Machines;

            Assert.That(manager is MachineManager);
            Assert.That(manager.GetOwnMachine().Position.X == 50);
            Assert.That(manager.GetOwnMachine().Position.Y == 30);

            var input = factory.Input;
            Assert.That(input is InputManager);
        }
    }

    [TestFixture]
    class CoreTimerTest
    {
		[TestCase]
		public void CoreTimer_生成破棄()
		{
			var timer = CoreTimer.GetInstance();
			timer.Dispose();
		}

        [TestCase]
        public void CoreTimerTest_MachineMoveTick()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            bool processing = true;

            var timer = CoreTimer.GetInstance();
            timer.SetInterval(CoreTimer.DefaultInterval);
            timer.SetMachineMoveTickFrame(CoreTimer.DefaultMachineMoveTickFrame);
            timer.MachineMoveTick += (sender, e) =>
                {
                    sw.Stop();
                    Assert.That(sw.ElapsedMilliseconds >= CoreTimer.DefaultInterval * CoreTimer.DefaultMachineMoveTickFrame);
                    processing = false;
                };
            sw.Start();
            timer.StartTimer();

            while (processing)
            {
                if (sw.ElapsedMilliseconds > 3000)
                {
                    timer.StopTimer();
                    sw.Stop();
                    Assert.Fail();
                    break;
                }
            }
            timer.StopTimer();
        }

        [TestCase(10, 3)]
        [TestCase(5, 30)]
        [TestCase(5, 3)]
        public void CoreTimerTest_パラメータ変更(int interval, int frames)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            bool processing = true;

            var timer = CoreTimer.GetInstance();
            timer.MachineMoveTick += (sender, e) =>
            {
                sw.Stop();
                Assert.That(sw.ElapsedMilliseconds >= interval * frames);
                processing = false;
            };
            timer.SetInterval(interval);
            timer.SetMachineMoveTickFrame(frames);
            Assert.That(interval == timer.Interval);
            Assert.That(frames == timer.MachineMoveTickFrame);
            sw.Start();
            timer.StartTimer();

            while (processing)
            {
                if (sw.ElapsedMilliseconds > 3000)
                {
                    timer.StopTimer();
                    sw.Stop();
                    Assert.Fail();
                    break;
                }
            }

            timer.StopTimer();
        }

        [Test]
        public void CoreTimerTest_パラメータ変更時例外()
        {
            var timer = CoreTimer.GetInstance();

            Assert.Throws<ArgumentException>(() => { timer.SetMachineMoveTickFrame(0); });

            Assert.Throws<ArgumentException>(() => { timer.SetInterval(0); });
        }

        [Test]
        public void CoreTimerTest_タイマーのStartとStop()
        {
            var timer = CoreTimer.GetInstance();
            Assert.That(timer.IsTimerEnabled == false);
            timer.StartTimer();
            Assert.That(timer.IsTimerEnabled == true);
            timer.StopTimer();
            Assert.That(timer.IsTimerEnabled == false);
        }
    }

    [TestFixture]
    class CoreTimerControlerTest
    {
        [Test]
        public void CoreTimerControlerTest_タイマーのStartとStop()
        {
            var timer = new CoreTimerControler();
            Assert.That(timer.IsTimerEnabled == false);
            timer.StartTimer();
            Assert.That(timer.IsTimerEnabled == true);
            timer.StopTimer();
            Assert.That(timer.IsTimerEnabled == false);
        }

        [TestCase(10, 3)]
        [TestCase(5, 30)]
        [TestCase(5, 3)]
        public void CoreTimerControlerTest_パラメータ変更(int interval, int frames)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            bool processing = true;

            var timer = new CoreTimerControler();
            timer.Timer.MachineMoveTick += (sender, e) =>
            {
                sw.Stop();
                Assert.That(sw.ElapsedMilliseconds >= interval * frames);
                processing = false;
            };
            timer.SetInterval(interval);
            timer.SetMachineMoveTickFrame(frames);
            Assert.That(interval == timer.Timer.Interval);
            Assert.That(frames == timer.Timer.MachineMoveTickFrame);
            sw.Start();
            timer.StartTimer();

            while (processing)
            {
                if (sw.ElapsedMilliseconds > 3000)
                {
                    timer.StopTimer();
                    sw.Stop();
                    Assert.Fail();
                    break;
                }
            }

            timer.StopTimer();
        }

        [Test]
        public void CoreTimerControlerTest_パラメータ変更時例外()
        {
            var timer = new CoreTimerControler();

            Assert.Throws<ArgumentException>(() => { timer.SetMachineMoveTickFrame(0); });

            Assert.Throws<ArgumentException>(() => { timer.SetInterval(0); });
        }
    }

    [TestFixture]
    class InputManagerTest
    {
        [Test]
        public void InputManagerTest_初期化()
        {
            Assert.Throws<ArgumentNullException>(() => new InputManager(null, null));
            Assert.Throws<ArgumentNullException>(() => new InputManager(null, new OwnMachine(new NormalMachinePosition(new Position(10, 10)))));
            Assert.Throws<ArgumentNullException>(() => new InputManager(CoreTimer.GetInstance(), null));
            Assert.DoesNotThrow(() => new InputManager(CoreTimer.GetInstance(), new OwnMachine(new NormalMachinePosition(new Position(10, 10)))));
        }

        [TestCase(InputManager.MoveOrder.MoveDown, 50, 50 - FieldSize.DefaultUnitMovement)]
        [TestCase(InputManager.MoveOrder.MoveLeft, 50 - FieldSize.DefaultUnitMovement, 50)]
        [TestCase(InputManager.MoveOrder.MoveRight, 50 + FieldSize.DefaultUnitMovement, 50)]
        [TestCase(InputManager.MoveOrder.MoveUp, 50, 50 + FieldSize.DefaultUnitMovement)]
        [TestCase(InputManager.MoveOrder.None, 50, 50)]
        public void InputManagerTest_AddRemoveOrder(InputManager.MoveOrder order, int resultX, int resultY)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetUnitMovement(FieldSize.DefaultUnitMovement);

            var own = new OwnMachine(new NormalMachinePosition(new Position(50, 50)));
            var manager = new InputManager(CoreTimer.GetInstance(), own);

            manager.SetMoveOrder(order);
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                manager.GetType().InvokeMember("RequestOwnMachineMove", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, manager, null);
            }
            Assert.That(own.Position.X == resultX);
            Assert.That(own.Position.Y == resultY);

            manager.SetMoveOrder(InputManager.MoveOrder.None);
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                manager.GetType().InvokeMember("RequestOwnMachineMove", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, manager, null);
            }
            Assert.That(own.Position.X == resultX);
            Assert.That(own.Position.Y == resultY);
        }

        [TestCase(InputManager.MoveOrder.MoveDown, 50, 50 - FieldSize.DefaultUnitMovement)]
        [TestCase(InputManager.MoveOrder.MoveLeft, 50 - FieldSize.DefaultUnitMovement, 50)]
        [TestCase(InputManager.MoveOrder.MoveRight, 50 + FieldSize.DefaultUnitMovement, 50)]
        [TestCase(InputManager.MoveOrder.MoveUp, 50, 50 + FieldSize.DefaultUnitMovement)]
        public void InputManagerTest_AddRemoveOrderTimer(InputManager.MoveOrder order, int resultX, int resultY)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetUnitMovement(FieldSize.DefaultUnitMovement);

            var own = new OwnMachine(new NormalMachinePosition(new Position(50, 50)));
            var timer = CoreTimer.GetInstance();
            var manager = new InputManager(timer, own);

            manager.SetMoveOrder(order);
            timer.StartTimer();
            bool isSuccess = false;
            own.MachinePositionChanged += (sender, e) =>
                {
                    timer.StopTimer();
                    Assert.That(own.Position.X == resultX);
                    Assert.That(own.Position.Y == resultY);
                    isSuccess = true;
                };
            while (!isSuccess) { }

            manager.SetMoveOrder(InputManager.MoveOrder.None);
            timer.StartTimer();
            System.Threading.Thread.Sleep(1000);
            timer.StopTimer();
            Assert.That(own.Position.X == resultX);
            Assert.That(own.Position.Y == resultY);
        }


        [TestCase(InputManager.MoveOrder.None, 50, 50)]
        public void InputManagerTest_AddRemoveOrderTimer_OrderNone(InputManager.MoveOrder order, int resultX, int resultY)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetUnitMovement(FieldSize.DefaultUnitMovement);

            var own = new OwnMachine(new NormalMachinePosition(new Position(50, 50)));
            var timer = CoreTimer.GetInstance();
            var manager = new InputManager(timer, own);

            manager.SetMoveOrder(order);
            timer.StartTimer();
            own.MachinePositionChanged += (sender, e) =>
            {
                timer.StopTimer();
                Assert.Fail();
            };
            System.Threading.Thread.Sleep(1000);

            manager.SetMoveOrder(InputManager.MoveOrder.None);
            timer.StartTimer();
            System.Threading.Thread.Sleep(1000);
            timer.StopTimer();
            Assert.That(own.Position.X == resultX);
            Assert.That(own.Position.Y == resultY);
        }
    }
}