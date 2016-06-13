using STG;
using STG.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGApp.Models
{
    /// <summary>
    /// STGゲーム開始機能を提供するクラスです。
    /// </summary>
    internal class GameManager
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ownMachinePosition"></param>
        public GameManager(Position ownMachinePosition)
        {
            _Factory = new MachineFactory(ownMachinePosition);

            _Timer = new CoreTimerControler();
        }

        #endregion


        #region フィールド

        MachineFactory _Factory;

        CoreTimerControler _Timer;

        #endregion


        #region プロパティ

        /// <summary>
        /// <see cref="MachineManager"/>インスタンスを取得します。
        /// </summary>
        public MachineManager MachineManager => _Factory.Machines;

        /// <summary>
        /// <see cref="STG.InputManager"/>インスタンスを取得します。
        /// </summary>
        public STG.InputManager InputManager => _Factory.Input;

        #endregion


        #region メソッド

        /// <summary>
        /// フィールドサイズを設定します。
        /// </summary>
        /// <param name="max"></param>
        public void SetFieldSize(Position max)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(0, 0), max);
        }

        /// <summary>
        /// ゲームを開始します。
        /// </summary>
        internal void StartGame()
        {
            _Timer.StartTimer();
        }

        #endregion
    }
}
