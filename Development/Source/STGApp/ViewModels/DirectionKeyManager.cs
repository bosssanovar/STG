using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static STG.InputManager;

namespace STGApp.ViewModels
{
	/// <summary>
	/// 方向キーの管理クラス
	/// </summary>
	public class DirectionKeyManager
	{
		#region コンストラクタ/デストラクタ
		#endregion


		#region 定数
		#endregion


		#region enum
		#endregion


		#region フィールド

		private List<Key> _Keys = new List<Key>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在の方向を取得します。
		/// </summary>
		public Order CurrentDirection { get; private set; } = Order.None;

		#endregion


		#region コマンド
		#endregion


		#region デリゲート
		#endregion


		#region イベント

		/// <summary>
		/// 方向の変更されたときに発行されるイベント
		/// </summary>
		public event EventHandler<DirectionChangedEventArgs> DirectionChanged;

		#endregion


		#region イベントハンドラ
		#endregion


		#region メソッド

		/// <summary>
		/// 押下されたキーを指定します。
		/// </summary>
		/// <param name="key"></param>
		public void SetPressKey(Key key)
		{
			_Keys.Add(key);
			UpdateDirection();
		}

		/// <summary>
		/// 押下されなくなったキーを指定します。
		/// </summary>
		/// <param name="key"></param>
		public void SetReleasedKey(Key key)
		{
			_Keys.Remove(key);
			UpdateDirection();
		}

		/// <summary>
		/// 方向を更新します。方向が変更されたら、変更通知を発行します。
		/// </summary>
		/// <returns></returns>
		private void UpdateDirection()
		{
			var direction = GetDirection();
			if (CurrentDirection != direction)
			{
				CurrentDirection = direction;
				DirectionChanged?.Invoke(this, new DirectionChangedEventArgs(direction));
			}
		}

		/// <summary>
		/// 現在の押下されているキー状況から、方向を取得します。
		/// </summary>
		/// <returns></returns>
		private Order GetDirection()
		{
			var ret = Order.None;

			switch (_Keys.LastOrDefault())
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

		#endregion


		#region 内部クラス
		#endregion
	}
}
