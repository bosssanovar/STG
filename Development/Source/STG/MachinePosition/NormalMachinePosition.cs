
using STG.Parameters;

namespace STG.MachinePosition
{
    /// <summary>
    /// <see cref="Frames"/>フレーム後に、全方位正常動作する機体位置クラス
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
        /// フレーム数
        /// </summary>
        internal const int Frames = 5;

        #endregion


        #region プロパティ

        /// <summary>
        /// 移動を行うまでのフレーム数を取得する。
        /// </summary>
        protected override int Frame => Frames;

        #endregion


        #region メソッド

        /// <summary>
        /// 次のX軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetLeftPosition() => new Position(this.Position.X - FieldSizeFactory.GetFieldSizeInstance().UnitMovement, this.Position.Y);

        /// <summary>
        /// 次のX軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetRightPosition() => new Position(this.Position.X + FieldSizeFactory.GetFieldSizeInstance().UnitMovement, this.Position.Y);

        /// <summary>
        /// 次のY軸負方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetUnderPosition() => new Position(this.Position.X, this.Position.Y - FieldSizeFactory.GetFieldSizeInstance().UnitMovement);

        /// <summary>
        /// 次のY軸正方向位置を取得します。
        /// </summary>
        /// <returns></returns>
        protected override Position GetUpperPosition() => new Position(this.Position.X, this.Position.Y + FieldSizeFactory.GetFieldSizeInstance().UnitMovement);
        #endregion
    }
}
