using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using STGApp.ViewModels;
using System.Windows.Input;
using static STG.InputManager;

namespace STG_Test
{
	[TestFixture]
	public class DirectionKeyManagerTest
	{
		[Test]
		public void DirectionKeyManager_初期値()
		{
			var m = new DirectionKeyManager();

			Assert.That(m.CurrentDirection == STG.InputManager.Order.None);
		}

		[TestCase(Key.E, Key.D, Key.F, Key.S)]
		[TestCase(Key.D, Key.F, Key.S, Key.E)]
		[TestCase(Key.F, Key.S, Key.E, Key.D)]
		[TestCase(Key.S, Key.E, Key.D, Key.F)]
		public void DirectionKeyManager_入力結果(Key key1, Key key2, Key key3, Key key4)
		{
			var m = new DirectionKeyManager();

			// Ｋｅｙ追加
			m.SetPressKey(key1);
			Assert.That(m.CurrentDirection == GetOrder(key1));
			m.SetPressKey(key2);
			Assert.That(m.CurrentDirection == GetOrder(key2));
			m.SetPressKey(key3);
			Assert.That(m.CurrentDirection == GetOrder(key3));
			m.SetPressKey(key4);
			Assert.That(m.CurrentDirection == GetOrder(key4));

			// Key削除
			m.SetReleasedKey(key4);
			Assert.That(m.CurrentDirection == GetOrder(key3));
			m.SetReleasedKey(key1);
			Assert.That(m.CurrentDirection == GetOrder(key3));
			m.SetReleasedKey(key3);
			Assert.That(m.CurrentDirection == GetOrder(key2));
			m.SetReleasedKey(key2);
			Assert.That(m.CurrentDirection == Order.None);
		}

		private Order GetOrder(Key key)
		{
			var ret = Order.None;

			switch (key)
			{
				case Key.E:
					ret = Order.MoveUp;
					break;
				case Key.D:
					ret = Order.MoveDown;
					break;
				case Key.F:
					ret = Order.MoveRight;
					break;
				case Key.S:
					ret = Order.MoveLeft;
					break;
				default:
					ret = Order.None;
					break;
			}

			return ret;
		}

		[TestCase(Key.E, Key.D, Key.F, Key.S)]
		public void DirectionKeyManager_変更通知発行(Key key1, Key key2, Key key3, Key key4)
		{
			var m = new DirectionKeyManager();
			Order order = Order.None;
			var handler = new EventHandler<DirectionChangedEventArgs>(
				(sender, e) =>
				{
					Assert.That(order == e.Order);
				});
			m.DirectionChanged += handler;

			// Ｋｅｙ追加
			order = GetOrder(key1);
			m.SetPressKey(key1);
			order = GetOrder(key2);
			m.SetPressKey(key2);
			order = GetOrder(key3);
			m.SetPressKey(key3);
			order = GetOrder(key4);
			m.SetPressKey(key4);

			// Key削除
			order = GetOrder(key3);
			m.SetReleasedKey(key4);
			order = GetOrder(key3);
			m.SetReleasedKey(key1);
			order = GetOrder(key2);
			m.SetReleasedKey(key3);
			order = Order.None;
			m.SetReleasedKey(key2);

		}
	}
}
