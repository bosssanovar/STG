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
        /// <param name="ownMachine"></param>
        public InputManager(MachineAbstract ownMachine)
        {
            _OwnMachine = ownMachine;
        }

        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// 自機インスタンス
        /// </summary>
        private MachineAbstract _OwnMachine;

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
            _OwnMachine.MoveToLeft();
        }

        /// <summary>
        /// 自機を右に移動します。
        /// </summary>
        public void MoveToRight()
        {
            _OwnMachine.MoveToRight();
        }

        /// <summary>
        /// 自機を上に移動します。
        /// </summary>
        public void MoveToUpper()
        {
            _OwnMachine.MoveToUpper();
        }

        /// <summary>
        /// 自機を下に移動します。
        /// </summary>
        public void MoveToUnder()
        {
            _OwnMachine.MoveToUnder();
        }


        #endregion


        #region 内部クラス
        #endregion
    }
}
