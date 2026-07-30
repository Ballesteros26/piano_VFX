using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006C8 RID: 1736
	public static class CompatibilitySwitch
	{
		// Token: 0x06004991 RID: 18833 RVA: 0x00015ED5 File Offset: 0x000140D5
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return false;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x0000A42E File Offset: 0x0000862E
		public static string GetValue(string compatibilitySwitchName)
		{
			return null;
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x0000A42E File Offset: 0x0000862E
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return null;
		}
	}
}
