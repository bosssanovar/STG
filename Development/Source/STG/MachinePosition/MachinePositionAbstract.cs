using System;
using STG.Parameters;

namespace STG.MachinePosition
{
    /// <summary>
    /// 機体位置を管理・制御する抽象クラス。
    /// </summary>
    abstract internal class MachinePositionAbstract : IMachinePosition
    {
        #region コンストラクタ/デストラクタ
        internal MachinePositionAbstract(Position position)
        {
            Position = position;
        }
        #endregion


        #region フィールド

        int _FrameCount = 0;

        #endregion


        #region プロパティ

        /// <summary>
        /// 機体位置の座標を取得します。
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        /// 移動を行うまでのフレーム数を取得する。
        /// </summary>
        abstract protected int Frame { get; }

        #endregion


        #region イベント

        /// <summary>
        /// 機体位置が変更されたときに発行されるイベント
        /// </summary>
        public event EventHandler<MachinePositionChangedEventArgs> MachinePositionChanged;

        #endregion


        #region メソッド

        /// <summary>
        /// 機体をX軸負方向に移動します。
        /// <see cref="Frame"/>回目の要求で<see cref="MachinePositionChanged"/>イベントが発行されます。
        /// </summary>
        public void MoveToLeft()
        {
            _FrameCount++;
            if (_FrameCount >= Frame)
            {
                Position = GetLeftPosition();
                NotifyMacinePositionChange();
                _FrameCount = 0;
            }
        }

        /// <summary>
        /// 次のX軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        abstract protected Position GetLeftPosition();

        /// <summary>
        /// 機体をX軸正方向に移動します。
        /// <see cref="Frame"/>回目の要求で<see cref="MachinePositionChanged"/>イベントが発行されます。
        /// </summary>
        public void MoveToRight()
        {
            _FrameCount++;
            if (_FrameCount >= Frame)
            {
                Position = GetRightPosition();
                NotifyMacinePositionChange();
                _FrameCount = 0;
            }
        }

        /// <summary>
        /// 次のX軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        abstract protected Position GetRightPosition();

        /// <summary>
        /// 機体をY軸負方向に移動します。
        /// <see cref="Frame"/>回目の要求で<see cref="MachinePositionChanged"/>イベントが発行されます。
        /// </summary>
        public void MoveToUnder()
        {
            _FrameCount++;
            if (_FrameCount >= Frame)
            {
                Position = GetUnderPosition();
                NotifyMacinePositionChange();
                _FrameCount = 0;
            }
        }

        /// <summary>
        /// 次のY軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        abstract protected Position GetUnderPosition();

        /// <summary>
        /// 機体をY軸正方向に移動します。
        /// <see cref="Frame"/>回目の要求で<see cref="MachinePositionChanged"/>イベントが発行されます。
        /// </summary>
        public void MoveToUpper()
        {
            _FrameCount++;
            if (_FrameCount >= Frame)
            {
                Position = GetUpperPosition();
                NotifyMacinePositionChange();
                _FrameCount = 0;
            }
        }

        /// <summary>
        /// 次のY軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        abstract protected Position GetUpperPosition();

        /// <summary>
        /// <see cref="Frame"/>回目の要求で機体移動通知を行います。
        /// </summary>
        private void NotifyMacinePositionChange()
        {
#if TEST
            MachinePositionChanged?.Invoke(this, new MachinePositionChangedEventArgs(Position));
#else
            System.Threading.Tasks.Task.Run(new Action(() =>
            {
                MachinePositionChanged?.Invoke(this, new MachinePositionChangedEventArgs(Position));
            }));
#endif
        }

        #endregion
    }
}
