namespace STG.Parameters
{
    /// <summary>
    /// フィールドサイズの管理、判定を行うクラス
    /// </summary>
    public class FieldSize
    {
        #region コンストラクタ/デストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal FieldSize() { }
        #endregion


        #region 定数

        /// <summary>
        /// フィールド最大値Xの初期値
        /// </summary>
        internal const int DefaultMaxX = 100;

        /// <summary>
        /// フィールド最大値Yの初期値
        /// </summary>
        internal const int DefaultMaxY = 100;

        /// <summary>
        /// フィールド最小値Xの初期値
        /// </summary>
        internal const int DefaultMinX = 0;

        /// <summary>
        /// フィールド最小値Yの初期値
        /// </summary>
        internal const int DefaultMinY = 0;

        /// <summary>
        /// 機体の単位移動量移動量の初期値
        /// </summary>
        internal const int DefaultUnitMovement = 10;

        #endregion


        #region プロパティ

        /// <summary>
        /// フィールドの最大値を取得します。
        /// </summary>
        public Position Max { get; private set; } = new Position() { X = 100, Y = 100 };

        /// <summary>
        /// フィールドの最小値を取得します。
        /// </summary>
        public Position Min { get; private set; } = new Position() { X = 0, Y = 0 };

        /// <summary>
        /// 機体の単位移動量を取得します。
        /// </summary>
        public int UnitMovement { get; private set; } = DefaultUnitMovement;

        #endregion


        #region メソッド

        /// <summary>
        /// 機体の単位移動量を変更します。
        /// </summary>
        /// <param name="val"></param>
        public void SetUnitMovement(int val)
        {
            UnitMovement = val;
        }

        /// <summary>
        /// フィールドサイズの最大値・最小値を設定します。
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void SetFieldSize(Position min, Position max)
        {
            Max = max;
            Min = min;
        }

        /// <summary>
        /// 指定した位置がフィールド範囲内かどうかを判定します。
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsIntoField(Position pos)
        {
            if (Max.Compare(pos) == Position.Direction.Same) return true;

            if (Min.Compare(pos) == Position.Direction.Same) return true;

            if (Min.Compare(pos) == Position.Direction.Upper
                || Min.Compare(pos) == Position.Direction.UpperRight
                || Min.Compare(pos) == Position.Direction.Right)
            {
                if (Max.Compare(pos) == Position.Direction.Left
                    || Max.Compare(pos) == Position.Direction.Under
                    || Max.Compare(pos) == Position.Direction.LowerLeft)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定した位置からX軸正方向に<see cref="UnitMovement"/>で指定ピクセル移動した位置がフィールド内かを判定します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsintoFieldNextXUpper(Position position) => IsIntoField(new Position(position.X + UnitMovement, position.Y));

        /// <summary>
        /// 指定した位置からX軸負方向に<see cref="UnitMovement"/>で指定ピクセル移動した位置がフィールド内かを判定します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsintoFieldNextXLower(Position position) => IsIntoField(new Position(position.X - UnitMovement, position.Y));

        /// <summary>
        /// 指定した位置からy軸正方向に<see cref="UnitMovement"/>で指定ピクセル移動した位置がフィールド内かを判定します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsintoFieldNextYUpper(Position position) => IsIntoField(new Position(position.X, position.Y + UnitMovement));

        /// <summary>
        /// 指定した位置からY軸負方向に<see cref="UnitMovement"/>で指定ピクセル移動した位置がフィールド内かを判定します。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsintoFieldNextYLower(Position position) => IsIntoField(new Position(position.X, position.Y - UnitMovement));

        #endregion
    }
}
