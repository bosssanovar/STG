using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using STG.Parameters;

namespace STG_Test
{
    [TestFixture]
    public class FiledSizeTest
    {
        [Test]
        public void FiledSize_初期化()
        {
            var f = new FieldSize();
            Assert.That(f.Max.X == 100);
            Assert.That(f.Max.Y == 100);
            Assert.That(f.Min.X == 0);
            Assert.That(f.Min.Y == 0);
        }

        [TestCase(int.MinValue, int.MinValue, int.MinValue, int.MinValue)]
        [TestCase(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1)]
        [TestCase(-1, -1, -1, -1)]
        public void FieldSize_フィールド情報を設定する(int xMax, int yMax, int xMin, int yMin)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(xMin, yMin), new Position(xMax, yMax));
            Assert.That(p.Max.X == xMax);
            Assert.That(p.Max.Y == yMax);
            Assert.That(p.Min.X == xMin);
            Assert.That(p.Min.Y == yMin);
        }

        [TestCase(int.MinValue, int.MinValue, false)]
        [TestCase(int.MaxValue, int.MaxValue, false)]
        [TestCase(0, 0, true)]
        [TestCase(1, 1, true)]
        [TestCase(-1, -1, false)]
        [TestCase(100, 100, true)]
        [TestCase(101, 101, false)]
        public void FieldSize_フィールド内か(int x, int y, bool isIn)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(0, 0), new Position(100, 100));
            Assert.That(p.IsIntoField(new Position(x, y)) == isIn);
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
        public void FieldSize_1つy座標正方向はフィールド内か(int x, int y, bool isIn)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(0, 0), new Position(100, 100));
            Assert.That(p.IsintoFieldNextYUpper(new Position(x, y)) == isIn);
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
        public void FieldSize_1つy座標負方向はフィールド内か(int x, int y, bool isIn)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(0, 0), new Position(100, 100));
            Assert.That(p.IsintoFieldNextYLower(new Position(x, y)) == isIn);
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
        public void FieldSize_1つx座標正方向はフィールド内か(int x, int y, bool isIn)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(0, 0), new Position(100, 100));
            Assert.That(p.IsintoFieldNextXUpper(new Position(x, y)) == isIn);
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
        public void FieldSize_1つx座標負方向はフィールド内か(int x, int y, bool isIn)
        {
            var p = new FieldSize();
            p.SetFieldSize(new Position(0, 0), new Position(100, 100));
            Assert.That(p.IsintoFieldNextXLower(new Position(x, y)) == isIn);
        }

        [Test]
        public void FieldSize_SetUnitMovement()
        {
            var p = new FieldSize();
            Assert.That(p.UnitMovement == FieldSize.DefaultUnitMovement);

            p.SetUnitMovement(100);
            Assert.That(p.UnitMovement == 100);
        }
    }

    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void Position_初期化()
        {
            var p = new Position();
            Assert.That(p.X == 0);
            Assert.That(p.Y == 0);
        }

        [TestCase(int.MinValue, int.MinValue)]
        [TestCase(int.MaxValue, int.MaxValue)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        public void Position_初期化(int x, int y)
        {
            var p = new Position(x, y);
            Assert.That(p.X == x);
            Assert.That(p.Y == y);

        }

        [TestCase(0, 0, Position.Direction.Same)]
        [TestCase(1, 0, Position.Direction.Right)]
        [TestCase(1, 1, Position.Direction.UpperRight)]
        [TestCase(0, 1, Position.Direction.Upper)]
        [TestCase(-1, 1, Position.Direction.UpperLeft)]
        [TestCase(-1, 0, Position.Direction.Left)]
        [TestCase(-1, -1, Position.Direction.LowerLeft)]
        [TestCase(0, -1, Position.Direction.Under)]
        [TestCase(1, -1, Position.Direction.LowerRight)]
        public void Position_Compare結果(int x, int y, Position.Direction result)
        {
            var p = new Position(0, 0);
            var t = new Position(x, y);
            Assert.That(p.Compare(t) == result);
        }
    }

    [TestFixture]
    public class FieldSizeFactoryTest
    {
        [Test]
        public void FieldSizeFactory_SingleTon()
        {
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Max.X == 100);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Max.Y == 100);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Min.X == 0);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Min.Y == 0);
        }

        [TestCase(10, 10, 20, 20)]
        [TestCase(0, 0, 100, 100)]
        public void FieldSizeFactory_SingleTonサイズ指定(int minX, int minY, int maxX, int maxY)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(minX, minY), new Position(maxX, maxY));
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Max.X == maxX);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Max.Y == maxY);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Min.X == minX);
            Assert.That(FieldSizeFactory.GetFieldSizeInstance().Min.Y == minY);
        }
    }
}
