using System.Diagnostics.Contracts;
using System;

namespace STG.Parameters
{
	/// <summary>
	/// <see cref="FieldSize"/>インスタンスのSingle-ton工場機能を提供する静的クラス。
	/// </summary>
	public static class FieldSizeFactory
	{

		#region フィールド
		static private FieldSize _Instance = new FieldSize();
		#endregion


		#region メソッド

		/// <summary>
		/// 常に同一の<see cref="FieldSize"/>インスタンスを取得します。
		/// </summary>
		/// <returns></returns>
		static public FieldSize GetFieldSizeInstance()
		{
			Contract.Ensures(Contract.Result<FieldSize>() != null);

			return _Instance;
		}

		#endregion
	}
}
