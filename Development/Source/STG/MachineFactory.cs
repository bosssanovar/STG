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
            Machines = new MachineManager(CreateMachineList(position));

            Input = new InputManager(CoreTimer.GetInstance(), Machines.GetOwnMachine());
        }

        #endregion


        #region プロパティ

        /// <summary>
        /// <see cref="MachineManager"/>インスタンスを取得します。
        /// </summary>
        public MachineManager Machines { get; }

        /// <summary>
        /// <see cref="InputManager"/>インスタンスを取得します。
        /// </summary>
        public InputManager Input { get; }

        #endregion


        #region メソッド

        /// <summary>
        /// 機体リストを生成します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private List<MachineAbstract> CreateMachineList(Position position)
        {
            Contract.Ensures(Contract.Result<List<MachineAbstract>>() != null);

            var ret = new List<MachineAbstract>()
                {
                    new OwnMachine(MachinePositionFactory.CreateMachinePositionInstance(position))
                };

            return ret.Where(e => e != null).ToList();
        }

        #endregion
    }
}
