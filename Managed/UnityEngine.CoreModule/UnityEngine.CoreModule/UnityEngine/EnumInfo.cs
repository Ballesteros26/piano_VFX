using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A6 RID: 422
	internal class EnumInfo
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x0001F918 File Offset: 0x0001DB18
		[UsedByNativeCode]
		internal static EnumInfo CreateEnumInfoFromNativeEnum(string[] names, int[] values, string[] annotations, bool isFlags)
		{
			return new EnumInfo
			{
				names = names,
				values = values,
				annotations = annotations,
				isFlags = isFlags
			};
		}

		// Token: 0x04000648 RID: 1608
		public string[] names;

		// Token: 0x04000649 RID: 1609
		public int[] values;

		// Token: 0x0400064A RID: 1610
		public string[] annotations;

		// Token: 0x0400064B RID: 1611
		public bool isFlags;
	}
}
