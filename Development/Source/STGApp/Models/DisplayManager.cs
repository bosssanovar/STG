using STG;
using STG.MachinePosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGApp.Models
{
    internal class DisplayManager
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="machines"></param>
        public DisplayManager(MachineManager machines)
        {
            SetMachines(machines);
        }

        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// <see cref="MachineManager"/>インスタンス
        /// </summary>
        private MachineManager _Machines;

        #endregion


        #region プロパティ
        #endregion


        #region コマンド
        #endregion


        #region デリゲート
        #endregion


        #region イベント

        /// <summary>
        /// 機体の位置変更時に発行されるイベント
        /// </summary>
        public event EventHandler<MachinePositionChangedEventArgs> MachinePositinChenged;

        #endregion


        #region イベントハンドラ
        #endregion


        #region メソッド

        /// <summary>
        /// <see cref="MachineManager"/>インスタンスを設定し、各機体に対して変更通知イベントを登録します。
        /// </summary>
        /// <param name="machines"></param>
        private void SetMachines(MachineManager machines)
        {
            _Machines = machines;

            foreach (var machine in _Machines.GetMachines())
            {
                machines.MachinePositionChanged += Machines_MachinePositionChanged;
            }
        }

        private void Machines_MachinePositionChanged(object sender, MachinePositionChangedEventArgs e)
        {
            MachinePositinChenged?.Invoke(sender, e);
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
