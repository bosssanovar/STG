using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using STG.Parameters;

namespace STGApp.Models
{
    /// <summary>
    /// MainWindowのModel機能を提供するクラス
    /// </summary>
    public class Model : NotificationObject
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model()
        {
        }

        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// 自機の位置を保持します。※VMに対して通知が必要な場合は<see cref="OwnMachinePosition"/>プロパティのSetアクセサを使用してください。
        /// </summary>
        private Position _OwnMachinePosition = new Position(0, 0);

        #endregion


        #region プロパティ

        /// <summary>
        /// 自機の位置を設定または取得します。変更された値はVMに通知されます。
        /// </summary>
        public Position OwnMachinePosition
        {
            get
            {
                return _OwnMachinePosition;
            }
            private set
            {
                if (_OwnMachinePosition.Compare(value) == Position.CompareResult.Same) return;

                _OwnMachinePosition = value;
                RaisePropertyChanged();
            }
        }

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
        /// 自機を左へ移動する
        /// </summary>
        internal void MoveToLeft()
        {
            // TODO K.I : 未実装
        }

        /// <summary>
        /// 自機を右へ移動する
        /// </summary>
        internal void MoveToRight()
        {
            // TODO K.I : 未実装
        }

        /// <summary>
        /// 自機を上へ移動する
        /// </summary>
        internal void MoveToUp()
        {
            // TODO K.I : 未実装
        }

        /// <summary>
        /// 自機を下へ移動する
        /// </summary>
        internal void MoveToDown()
        {
            // TODO K.I : 未実装
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
