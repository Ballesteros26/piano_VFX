using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CA RID: 458
	internal static class StyleValueFunctionExtension
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00036998 File Offset: 0x00034B98
		public static StyleValueFunction FromUssString(string ussValue)
		{
			ussValue = ussValue.ToLower();
			string text = ussValue;
			StyleValueFunction styleValueFunction;
			if (!(text == "var"))
			{
				if (!(text == "env"))
				{
					if (!(text == "linear-gradient"))
					{
						throw new ArgumentOutOfRangeException("ussValue", ussValue, "Unknown function name");
					}
					styleValueFunction = StyleValueFunction.LinearGradient;
				}
				else
				{
					styleValueFunction = StyleValueFunction.Env;
				}
			}
			else
			{
				styleValueFunction = StyleValueFunction.Var;
			}
			return styleValueFunction;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000369F8 File Offset: 0x00034BF8
		public static string ToUssString(this StyleValueFunction svf)
		{
			string text;
			switch (svf)
			{
			case StyleValueFunction.Var:
				text = "var";
				break;
			case StyleValueFunction.Env:
				text = "env";
				break;
			case StyleValueFunction.LinearGradient:
				text = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return text;
		}

		// Token: 0x040005C5 RID: 1477
		public const string k_Var = "var";

		// Token: 0x040005C6 RID: 1478
		public const string k_Env = "env";

		// Token: 0x040005C7 RID: 1479
		public const string k_LinearGradient = "linear-gradient";
	}
}
