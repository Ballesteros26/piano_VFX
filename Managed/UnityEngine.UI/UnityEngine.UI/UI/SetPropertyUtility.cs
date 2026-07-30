using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000035 RID: 53
	internal static class SetPropertyUtility
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x00012FEC File Offset: 0x000111EC
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001303B File Offset: 0x0001123B
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001305C File Offset: 0x0001125C
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
