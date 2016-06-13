using STG.MachinePosition;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace STG
{
    /// <summary>
    /// 機体管理クラス
    /// </summary>
    public class MachineManager
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ownMachine">※nullな要素は除外されます</param>
        internal MachineManager(OwnMachine ownMachine)
        {
            Contract.Requires<ArgumentNullException>(ownMachine != null);

            _Machines = new List<MachineAbstract>();
            _Machines.Add(ownMachine);

            _OwnMachine = ownMachine;

            foreach (var m in _Machines)
            {
                Contract.Assume(m != null);
                m.MachinePositionChanged += Machine_MachinePositionChanged;
            }
        }

        #endregion


        #region フィールド

        readonly IList<MachineAbstract> _Machines;

        readonly MachineAbstract _OwnMachine;

        #endregion


        #region イベント

        /// <summary>
        /// 機体位置が変更された際に発行されるイベント
        /// </summary>
        public event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

        #endregion


        #region イベントハンドラ

        /// <summary>
        /// 機体位置が変更された際に発行されるイベントのハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_MachinePositionChanged(object sender, MachinePositionChangedEventArgs e)
        {
            MachinePositionChanged?.Invoke(sender, e);
        }

        #endregion


        #region メソッド
        
        /// <summary>
        /// 自機を取得します。
        /// </summary>
        /// <returns></returns>
        public MachineAbstract GetOwnMachine()
        {
            Contract.Ensures(Contract.Result<MachineAbstract>() != null);

            return _OwnMachine;
        }

        /// <summary>
        /// 機体一覧を取得します。
        /// </summary>
        /// <returns></returns>
        public IList<MachineAbstract> GetMachines()
        {
            Contract.Ensures(Contract.Result<IList<MachineAbstract>>() != null);

            return _Machines;
        }

        #endregion

        #region Invariant

        /// <summary>
        /// 不変契約を定義します。
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_Machines != null);
            Contract.Invariant(_OwnMachine != null);
        }

        #endregion
    }
}
