using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    /// <summary>
    /// フレーム制御タイマーを制御するクラス
    /// </summary>
    public class CoreTimerControler
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

        /// <summary>
        /// <see cref="CoreTimer"/>インスタンス
        /// </summary>
        internal CoreTimer Timer => CoreTimer.GetInstance();

        /// <summary>
        /// フレーム制御用のタイマーが動作しているかを取得します。
        /// </summary>
        public bool IsTimerEnabled => Timer.IsTimerEnabled;

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
        /// フレーム制御用のタイマーをスタートします。
        /// </summary>
        public void StartTimer()
        {
            Timer.StartTimer();
        }

        /// <summary>
        /// フレーム制御用のタイマーをストップします。
        /// </summary>
        public void StopTimer()
        {
            Timer.StopTimer();
        }

        /// <summary>
        /// フレーム制御用のタイマーのInterval時間を設定します。
        /// </summary>
        /// <param name="interval"></param>
        public void SetInterval(int interval)
        {
            Contract.Requires<ArgumentException>(interval > 0);

            Timer.SetInterval(interval);
            
        }

        /// <summary>
        /// フレーム制御用のタイマーの機体動作用フレーム数を設定します。
        /// </summary>
        /// <param name="frames"></param>
        public void SetMachineMoveTickFrame(int frames)
        {
            Contract.Requires<ArgumentException>(frames > 0);

            Timer.SetMachineMoveTickFrame(frames);
            
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
