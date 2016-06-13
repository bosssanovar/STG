﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using STG.MachinePosition;
using STG.Parameters;
using System.Diagnostics;
using System.Windows.Threading;

namespace STG_Test
{
    [TestFixture]
    public class MachinePositionChangedEventArgsTest
    {
        [Test]
        public void MachinePositionChangedEventArgs_初期化()
        {
            var a = new MachinePositionChangedEventArgs(new Position(5, 5));
            Assert.That(a.Position.X == 5);
            Assert.That(a.Position.Y == 5);
        }
    }

    [TestFixture]
    public class NormalMachinePositionTest
    {
        private Stopwatch _Sw = new Stopwatch();

        [Test]
        public void NormalMachinePosition_MoveToLeft()
        {
            var machinePosition = new NormalMachinePosition(new Position(10, 10));
            bool isSuccess = false;
            machinePosition.MachinePositionChanged += (sender, e) =>
            {
                Assert.That(e.Position.X == 10 - FieldSize.DefaultUnitMovement);
                Assert.That(e.Position.Y == 10);
                isSuccess = true;
            };
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                machinePosition.MoveToLeft();
            }
            Assert.That(isSuccess);
        }

        [Test]
        public void NormalMachinePosition_MoveToLeft_イベント登録なし()
        {
            var machinePosition = new NormalMachinePosition(new Position(10, 10));

            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                machinePosition.MoveToLeft();
            }

            Assert.Pass();
        }

        [Test]
        public void NormalMachinePosition_MoveToRight()
        {
            var machinePosition = new NormalMachinePosition(new Position(10, 10));
            bool isSuccess = false;
            machinePosition.MachinePositionChanged += (sender, e) =>
            {
                Assert.That(e.Position.X == 10 + FieldSize.DefaultUnitMovement);
                Assert.That(e.Position.Y == 10);
                isSuccess = true;
            };
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                machinePosition.MoveToRight();
            }
            Assert.That(isSuccess);
        }

        [Test]
        public void NormalMachinePosition_MoveToUnder()
        {
            var machinePosition = new NormalMachinePosition(new Position(10, 10));
            bool isSuccess = false;
            machinePosition.MachinePositionChanged += (sender, e) =>
            {
                Assert.That(e.Position.X == 10);
                Assert.That(e.Position.Y == 10 - FieldSize.DefaultUnitMovement);
                isSuccess = true;
            };
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                machinePosition.MoveToUnder();
            }
            Assert.That(isSuccess);
        }

        [Test]
        public void NormalMachinePosition_MoveToUpper()
        {
            var machinePosition = new NormalMachinePosition(new Position(10, 10));
            bool isSuccess = false;
            machinePosition.MachinePositionChanged += (sender, e) =>
            {
                Assert.That(e.Position.X == 10);
                Assert.That(e.Position.Y == 10 + FieldSize.DefaultUnitMovement);
                isSuccess = true;
            };
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                machinePosition.MoveToUpper();
            }
            Assert.That(isSuccess);
        }
    }

    [TestFixture]
    public class AreaEndLimitTest
    {
        private Position _Result;
        private bool _IsPositionChanged;

        [Test]
        public void AreaEndLimitTest_コンストラクタ()
        {
            Assert.Throws<ArgumentNullException>(() => new AreaEndLimit(null));
            Assert.DoesNotThrow(() => new AreaEndLimit(new NormalMachinePosition(new Position(0, 0))));
        }

        [Test]
        public void AreaEndLimit_MoveLeft_イベント登録なし()
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(0, 0), new Position(100, 100));

            var limit = new AreaEndLimit(new NormalMachinePosition(new Position(50, 50)));
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                limit.MoveToLeft();
            }

            Assert.Pass();
        }

        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, false)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - FieldSize.DefaultUnitMovement, false)]
        public void AreaEndLimit_MoveLeft(int x, int y, bool isPositionChanged)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(FieldSize.DefaultMinX, FieldSize.DefaultMinY), new Position(FieldSize.DefaultMaxX, FieldSize.DefaultMaxY));

            var limit = new AreaEndLimit(new NormalMachinePosition(new Position(x, y)));
            _Result = new Position(x - FieldSize.DefaultUnitMovement, y);
            _IsPositionChanged = false;
            limit.MachinePositionChanged += Limit_MachinePositionChanged_MoveLeft;
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                limit.MoveToLeft();
            }
            limit.MachinePositionChanged -= Limit_MachinePositionChanged_MoveLeft;
            Assert.That(isPositionChanged == _IsPositionChanged);
        }

        private void Limit_MachinePositionChanged_MoveLeft(object sender, MachinePositionChangedEventArgs e)
        {
            Assert.That(e.Position.X == _Result.X);
            Assert.That(e.Position.Y == _Result.Y);

            _IsPositionChanged = true;
        }

        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - FieldSize.DefaultUnitMovement, false)]
        public void AreaEndLimit_MoveRight(int x, int y, bool isPositionChanged)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(FieldSize.DefaultMinX, FieldSize.DefaultMinY), new Position(FieldSize.DefaultMaxX, FieldSize.DefaultMaxY));

            var limit = new AreaEndLimit(new NormalMachinePosition(new Position(x, y)));
            _Result = new Position(x + FieldSize.DefaultUnitMovement, y);
            _IsPositionChanged = false;
            limit.MachinePositionChanged += Limit_MachinePositionChanged_MoveLeft;
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                limit.MoveToRight();
            }
            limit.MachinePositionChanged -= Limit_MachinePositionChanged_MoveLeft;
            Assert.That(isPositionChanged == _IsPositionChanged);
        }

        private void Limit_MachinePositionChanged_MoveRight(object sender, MachinePositionChangedEventArgs e)
        {
            Assert.That(e.Position.X == _Result.X);
            Assert.That(e.Position.Y == _Result.Y);

            _IsPositionChanged = true;
        }

        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, true)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - 1, true)]
        [TestCase(FieldSize.DefaultMinX - FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - FieldSize.DefaultUnitMovement, true)]
        public void AreaEndLimit_MoveUpper(int x, int y, bool isPositionChanged)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(FieldSize.DefaultMinX, FieldSize.DefaultMinY), new Position(FieldSize.DefaultMaxX, FieldSize.DefaultMaxY));

            var limit = new AreaEndLimit(new NormalMachinePosition(new Position(x, y)));
            _Result = new Position(x, y + FieldSize.DefaultUnitMovement);
            _IsPositionChanged = false;
            limit.MachinePositionChanged += Limit_MachinePositionChanged_MoveLeft;
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                limit.MoveToUpper();
            }
            limit.MachinePositionChanged -= Limit_MachinePositionChanged_MoveLeft;
            Assert.That(isPositionChanged == _IsPositionChanged);
        }

        private void Limit_MachinePositionChanged_MoveUpper(object sender, MachinePositionChangedEventArgs e)
        {
            Assert.That(e.Position.X == _Result.X);
            Assert.That(e.Position.Y == _Result.Y);

            _IsPositionChanged = true;
        }

        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX + FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY + FieldSize.DefaultUnitMovement, true)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - 1, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - 1, false)]
        [TestCase(FieldSize.DefaultMinX - FieldSize.DefaultUnitMovement, FieldSize.DefaultMinY, false)]
        [TestCase(FieldSize.DefaultMinX, FieldSize.DefaultMinY - FieldSize.DefaultUnitMovement, false)]
        public void AreaEndLimit_MoveUnder(int x, int y, bool isPositionChanged)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(FieldSize.DefaultMinX, FieldSize.DefaultMinY), new Position(FieldSize.DefaultMaxX, FieldSize.DefaultMaxY));

            var limit = new AreaEndLimit(new NormalMachinePosition(new Position(x, y)));
            _Result = new Position(x, y - FieldSize.DefaultUnitMovement);
            _IsPositionChanged = false;
            limit.MachinePositionChanged += Limit_MachinePositionChanged_MoveLeft;
            for (int cnt = 0; cnt < NormalMachinePosition.Frames; cnt++)
            {
                limit.MoveToUnder();
            }
            limit.MachinePositionChanged -= Limit_MachinePositionChanged_MoveLeft;
            Assert.That(isPositionChanged == _IsPositionChanged);
        }

        private void Limit_MachinePositionChanged_MoveUnder(object sender, MachinePositionChangedEventArgs e)
        {
            Assert.That(e.Position.X == _Result.X);
            Assert.That(e.Position.Y == _Result.Y);

            _IsPositionChanged = true;
        }
    }

    [TestFixture]
    public class MachinePositionFactoryTest
    {
        [Test]
        public void MachinePositionFactory_インスタンス生成()
        {
            var machine = MachinePositionFactory.CreateMachinePositionInstance(new Position(50, 30));

            Assert.That(machine is AreaEndLimit);
            Assert.That(machine.Position.X == 50);
            Assert.That(machine.Position.Y == 30);
        }
    }
}
