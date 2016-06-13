using STG.MachinePosition;
using STG.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    /// <summary>
    /// 機体を生成するファクトリクラス
    /// </summary>
    public class MachineFactory
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>]
        /// <param name="position"></param>
        public MachineFactory(Position position)
        {
            _Machines = new MachineManager(CreateOwnMachine(position));

            _Input = new InputManager(CoreTimer.GetInstance(), Machines.GetOwnMachine());
        }

        #endregion


        #region フィールド

        private readonly MachineManager _Machines;

        private readonly InputManager _Input;

        #endregion


        #region プロパティ

        /// <summary>
        /// <see cref="MachineManager"/>インスタンスを取得します。
        /// </summary>
        public MachineManager Machines
        {
            get
            {
                Contract.Ensures(Contract.Result<MachineManager>() != null);
                return _Machines;
            }
        }

        /// <summary>
        /// <see cref="InputManager"/>インスタンスを取得します。
        /// </summary>
        public InputManager Input
        {
            get
            {
                Contract.Ensures(Contract.Result<InputManager>() != null);
                return _Input;
            }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// 自機を生成します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private OwnMachine CreateOwnMachine(Position position)
        {
            Contract.Ensures(Contract.Result<OwnMachine>() != null);

            return new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(position));
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
            Contract.Invariant(_Input != null);
        }

        #endregion
    }
}
