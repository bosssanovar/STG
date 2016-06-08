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
        #region コンストラクタ/デストラクタ
        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド
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
        /// フィールドサイズを設定します。
        /// </summary>
        /// <param name="max"></param>
        public void SetFieldSize(Position max)
        {
            FieldSizeFactory.GetFieldSizeInstance().SetFieldSize(new Position(0, 0), max);
        }

        /// <summary>
        /// <see cref="MachineManager"/>インスタンスを取得します。
        /// </summary>
        /// <param name="ownMachinePosition"></param>
        /// <returns></returns>
        public MachineManager CreateMachineManager(Position ownMachinePosition) => MachineFactory.CreateMachines(ownMachinePosition);

        #endregion

        #region 内部クラス
        #endregion
    }
}
