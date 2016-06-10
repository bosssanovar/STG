using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace STG
{
    /// <summary>
    /// 各種動作フレームを通知するsinele-toneクラスです。
    /// </summary>
    internal class CoreTimer
    {
        #region コンストラクタ/デストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CoreTimer()
        {
            if (Interval > 0) InitTimer(Interval);
        }
        #endregion


        #region 定数

        /// <summary>
        /// <see cref="Interval"/>プロパティの初期値[ms]
        /// </summary>
        public const int DefaultInterval = 10;

        /// <summary>
        /// <see cref="MachineMoveTickFrame"/>プロパティ初期値
        /// </summary>
        public const int DefaultMachineMoveTickFrame = 30;

        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// 自インスタンス
        /// </summary>
        private static CoreTimer _Own = new CoreTimer();

        /// <summary>
        /// 単位フレームタイマー
        /// </summary>
        private Timer _Timer;

        /// <summary>
        /// 機体移動のための単位フレームカウンタ
        /// </summary>
        private int _MachineMoveCounter = 0;

        #endregion


        #region プロパティ

        /// <summary>
        /// 単位フレームの間隔[ms]を取得します。
        /// </summary>
        public int Interval { get; private set; } = DefaultInterval;

        /// <summary>
        /// 機体移動の単位フレーム数を取得します。
        /// </summary>
        public int MachineMoveTickFrame { get; private set; } = DefaultMachineMoveTickFrame;

        #endregion


        #region コマンド
        #endregion


        #region デリゲート
        #endregion


        #region イベント

        /// <summary>
        /// 機体移動の単位タイミングを通知するイベント
        /// </summary>
        public event EventHandler MachineMoveTick;

        #endregion


        #region イベントハンドラ
        #endregion


        #region メソッド

        /// <summary>
        /// 単位フレームでの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Timer_Tick(object sender, ElapsedEventArgs e)
        {
            if (_MachineMoveCounter <= MachineMoveTickFrame)
            {
                _MachineMoveCounter++;
            }
            else
            {
                MachineMoveTick?.Invoke(this, e);
                _MachineMoveCounter = 0;
            }
        }

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns></returns>
        public static CoreTimer GetInstance() => _Own;

        /// <summary>
        /// 単位フレームの間隔を変更します。本処理後はタイマーが止まった状態となります。
        /// </summary>
        /// <param name="val"></param>
        public void SetInterval(int val)
        {
            Contract.Requires<ArgumentException>(val > 0);

            Interval = val;
            InitTimer(Interval);
        }

        /// <summary>
        /// 機体移動の単位フレーム数を変更します。
        /// </summary>
        /// <param name="val"></param>
        public void SetMachineMoveTickFrame(int val)
        {
            Contract.Requires<ArgumentException>(val > 0);

            MachineMoveTickFrame = val;
        }

        /// <summary>
        /// タイマーを開始します。
        /// </summary>
        public void StartTimer()
        {
            _Timer?.Start();
        }

        /// <summary>
        /// タイマーを停止します。
        /// </summary>
        public void StopTimer()
        {
            _Timer?.Stop();
        }

        /// <summary>
        /// タイマーを初期化します。本処理後はタイマーが止まった状態となります。
        /// </summary>
        /// <param name="interval"></param>
        private void InitTimer(int interval)
        {
            Contract.Requires<ArgumentException>(interval > 0);

            if (_Timer != null)
            {
                _Timer.Stop();
                _Timer.Elapsed += new ElapsedEventHandler(_Timer_Tick);
            }

            _Timer = new Timer();
            _Timer.Interval = interval;
            _Timer.Elapsed += new ElapsedEventHandler(_Timer_Tick);
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
