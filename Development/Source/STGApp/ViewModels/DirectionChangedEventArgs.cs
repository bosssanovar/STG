using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGApp.ViewModels
{
	/// <summary>
	/// DirectionChangedイベント用データクラス
	/// </summary>
	public class DirectionChangedEventArgs : EventArgs
	{
		#region コンストラクタ/デストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="order"></param>
		public DirectionChangedEventArgs(STG.InputManager.MoveOrder order)
		{
			Order = order;
		}

		#endregion


		#region プロパティ

		/// <summary>
		/// 方向を取得します。
		/// </summary>
		public STG.InputManager.MoveOrder Order { get; }

		#endregion
	}
}
