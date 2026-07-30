using System;

namespace System.Data
{
	// Token: 0x020000BA RID: 186
	internal sealed class Function
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x000333FA File Offset: 0x000315FA
		internal Function()
		{
			this._name = null;
			this._id = FunctionId.none;
			this._result = null;
			this._isValidateArguments = false;
			this._argumentCount = 0;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00033434 File Offset: 0x00031634
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
			this._name = name;
			this._id = id;
			this._result = result;
			this._isValidateArguments = IsValidateArguments;
			this._isVariantArgumentList = IsVariantArgumentList;
			this._argumentCount = argumentCount;
			if (a1 != null)
			{
				this._parameters[0] = a1;
			}
			if (a2 != null)
			{
				this._parameters[1] = a2;
			}
			if (a3 != null)
			{
				this._parameters[2] = a3;
			}
		}

		// Token: 0x04000764 RID: 1892
		internal readonly string _name;

		// Token: 0x04000765 RID: 1893
		internal readonly FunctionId _id;

		// Token: 0x04000766 RID: 1894
		internal readonly Type _result;

		// Token: 0x04000767 RID: 1895
		internal readonly bool _isValidateArguments;

		// Token: 0x04000768 RID: 1896
		internal readonly bool _isVariantArgumentList;

		// Token: 0x04000769 RID: 1897
		internal readonly int _argumentCount;

		// Token: 0x0400076A RID: 1898
		internal readonly Type[] _parameters = new Type[3];

		// Token: 0x0400076B RID: 1899
		internal static string[] s_functionName = new string[]
		{
			"Unknown", "Ascii", "Char", "CharIndex", "Difference", "Len", "Lower", "LTrim", "Patindex", "Replicate",
			"Reverse", "Right", "RTrim", "Soundex", "Space", "Str", "Stuff", "Substring", "Upper", "IsNull",
			"Iif", "Convert", "cInt", "cBool", "cDate", "cDbl", "cStr", "Abs", "Acos", "In",
			"Trim", "Sum", "Avg", "Min", "Max", "Count", "StDev", "Var", "DateTimeOffset"
		};
	}
}
