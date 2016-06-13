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
        /// 自機の左移動命令を登録します。
        /// </summary>
        public void AddLeftOrder()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveLeft);
        }

        /// <summary>
        /// 自機の左移動命令を削除します。
        /// </summary>
        public void RemoveLeftOrder()
        {
            _InputManager.RemoveOrder(STG.InputManager.Order.MoveLeft);
        }

        /// <summary>
        /// 自機の右移動命令を登録します。
        /// </summary>
        public void AddRightOrder()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveRight);
        }

        /// <summary>
        /// 自機の右移動命令を削除します。
        /// </summary>
        public void RemoveRightOrder()
        {
            _InputManager.RemoveOrder(STG.InputManager.Order.MoveRight);
        }

        /// <summary>
        /// 自機の上移動命令を登録します。
        /// </summary>
        public void AddUpOrder()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveUp);
        }

        /// <summary>
        /// 自機の上移動命令を削除します。
        /// </summary>
        public void RemoveUpOrder()
        {
            _InputManager.RemoveOrder(STG.InputManager.Order.MoveUp);
        }

        /// <summary>
        /// 自機の下移動命令を登録します。
        /// </summary>
        public void AddDownOrder()
        {
            _InputManager.AddOrder(STG.InputManager.Order.MoveDown);
        }

        /// <summary>
        /// 自機の下移動命令を削除します。
        /// </summary>
        public void RemoveDownOrder()
        {
            _InputManager.RemoveOrder(STG.InputManager.Order.MoveDown);
        }


        #endregion


        #region 内部クラス
        #endregion
    }
}
