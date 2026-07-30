using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200002B RID: 43
	internal static class SetPropertyUtility
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000D54C File Offset: 0x0000B74C
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D59B File Offset: 0x0000B79B
		public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D5B6 File Offset: 0x0000B7B6
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
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
