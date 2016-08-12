using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    /// <summary>
    /// 入力値情報を管理し、フレーム制御タイマーからの更新タイミングで各種インスタンスに動作を命令します。
    /// </summary>
    public class InputManager
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="ownMachine"></param>
        internal InputManager(CoreTimer timer, MachineAbstract ownMachine)
        {
            Contract.Requires<ArgumentNullException>(ownMachine != null);
            Contract.Requires<ArgumentNullException>(timer != null);

            _OwnMachine = ownMachine;
            timer.MachineMoveTick += InputManager_MachineMoveTick;
        }

        #endregion


        #region 定数
        #endregion


        #region enum

        /// <summary>
        /// 移動命令の列挙
        /// </summary>
        public enum MoveOrder
        {
            /// <summary>なし</summary>
            None,
            /// <summary>Y軸正方向への移動</summary>
            MoveUp,
            /// <summary>Y軸負方向への移動</summary>
            MoveDown,
            /// <summary>X軸正方向への移動</summary>
            MoveRight,
            /// <summary>X軸負方向への移動</summary>
            MoveLeft,
        }

        #endregion


        #region フィールド

        /// <summary>
        /// 移動命令一覧
        /// </summary>
        private MoveOrder _MoveOrders = MoveOrder.None;

        /// <summary>
        /// 自機インスタンス
        /// </summary>
        private readonly MachineAbstract _OwnMachine;

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

        /// <summary>
        /// 機体移動タイミング通知イベントを受けた際に実行されるハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputManager_MachineMoveTick(object sender, EventArgs e)
        {
            RequestOwnMachineMove();
        }

        #endregion


        #region メソッド

        /// <summary>
        /// 自機に移動要求を送る
        /// </summary>
        private void RequestOwnMachineMove()
        {
            switch (_MoveOrders)
            {
                case MoveOrder.MoveUp:
                    _OwnMachine.MoveToUpper();
                    break;
                case MoveOrder.MoveDown:
                    _OwnMachine.MoveToUnder();
                    break;
                case MoveOrder.MoveRight:
                    _OwnMachine.MoveToRight();
                    break;
                case MoveOrder.MoveLeft:
                    _OwnMachine.MoveToLeft();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 操作命令を設定します。
        /// </summary>
        /// <param name="order"></param>
        public void SetMoveOrder(MoveOrder order)
        {
            _MoveOrders = order;
        }

        #endregion


        #region 内部クラス
        #endregion

        #region Invariant

        /// <summary>
        /// 不変契約を定義します。
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_OwnMachine != null);
        }

        #endregion
    }
}
