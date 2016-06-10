using STG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGApp.Models
{
    internal class InputManager
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager"></param>
        public InputManager(STG.InputManager manager)
        {
            _InputManager = manager;
        }

        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// 入力管理用の<see cref="STG.InputManager"/>インスタンス
        /// </summary>
        private STG.InputManager _InputManager;

        #endregion


        #region プロパティ
        #endregion


        #region コマンド
        #endregion


        #region デリゲート
        #endregion


        #region イベント
        #endregion


        #region イベントハンドラ
        #endregion


        #region メソッド

        /// <summary>
        /// 自機を左に移動します。
        /// </summary>
        public void MoveToLeft()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveLeft);
        }

        /// <summary>
        /// 自機を右に移動します。
        /// </summary>
        public void MoveToRight()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveRight);
        }

        /// <summary>
        /// 自機を上に移動します。
        /// </summary>
        public void MoveToUpper()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveUp);
        }

        /// <summary>
        /// 自機を下に移動します。
        /// </summary>
        public void MoveToUnder()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveDown);
        }


        #endregion


        #region 内部クラス
        #endregion
    }
}
