
using STG.Parameters;

namespace STG.MachinePosition
{
    /// <summary>
    /// 移動タイムラグが<see cref="IntervalTime"/>ミリ秒、全方位正常動作する機体位置クラス
    /// </summary>
    internal class NormalMachinePosition : MachinePositionAbstract
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        internal NormalMachinePosition(Position position) : base(position)
        {
        }

        #endregion


        #region 定数

        /// <summary>
        /// 移動タイムラグ時間(ミリ秒)
        /// </summary>
        internal const int IntervalTime = 50;

        #endregion


        #region プロパティ

        /// <summary>
        /// 移動指示から移動が完了するまでのラグ(ミリ秒)を設定または取得します。
        /// </summary>
        protected override int Intarval => IntervalTime;

        #endregion


        #region メソッド

        /// <summary>
        /// 次のX軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetLeftPosition() => new Position(this.Position.X - 1, this.Position.Y);

        /// <summary>
        /// 次のX軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetRightPosition() => new Position(this.Position.X + 1, this.Position.Y);

        /// <summary>
        /// 次のY軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetUnderPosition() => new Position(this.Position.X, this.Position.Y - 1);

        /// <summary>
        /// 次のY軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetUpperPosition() => new Position(this.Position.X, this.Position.Y + 1);
        #endregion
    }
}
