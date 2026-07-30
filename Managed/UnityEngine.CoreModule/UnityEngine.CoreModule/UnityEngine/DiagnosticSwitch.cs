using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000BF RID: 191
	[UsedByNativeCode]
	internal struct DiagnosticSwitch
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x00006D6C File Offset: 0x00004F6C
		[UsedByNativeCode]
		private static void AppendDiagnosticSwitchToList(List<DiagnosticSwitch> list, string name, string description, DiagnosticSwitchFlags flags, object value, object minValue, object maxValue, object persistentValue, EnumInfo enumInfo)
		{
			list.Add(new DiagnosticSwitch
			{
				name = name,
				description = description,
				flags = flags,
				value = value,
				minValue = minValue,
				maxValue = maxValue,
				persistentValue = persistentValue,
				enumInfo = enumInfo
			});
		}

		// Token: 0x0400022C RID: 556
		public string name;

		// Token: 0x0400022D RID: 557
		public string description;

		// Token: 0x0400022E RID: 558
		public DiagnosticSwitchFlags flags;

		// Token: 0x0400022F RID: 559
		public object value;

		// Token: 0x04000230 RID: 560
		public object minValue;

		// Token: 0x04000231 RID: 561
		public object maxValue;

		// Token: 0x04000232 RID: 562
		public object persistentValue;

		// Token: 0x04000233 RID: 563
		public EnumInfo enumInfo;
	}
}
