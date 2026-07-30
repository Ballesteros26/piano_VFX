using System;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200005A RID: 90
	public static class NativeLeakDetection
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00002D67 File Offset: 0x00000F67
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			NativeLeakDetection.s_NativeLeakDetectionMode = 1;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002D9C File Offset: 0x00000F9C
		public static NativeLeakDetectionMode Mode
		{
			get
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode == 0;
				if (flag)
				{
					NativeLeakDetection.Initialize();
				}
				return (NativeLeakDetectionMode)NativeLeakDetection.s_NativeLeakDetectionMode;
			}
			set
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode != (int)value;
				if (flag)
				{
					NativeLeakDetection.s_NativeLeakDetectionMode = (int)value;
				}
			}
		}

		// Token: 0x0400010F RID: 271
		private static int s_NativeLeakDetectionMode;

		// Token: 0x04000110 RID: 272
		private const string kNativeLeakDetectionModePrefsString = "Unity.Colletions.NativeLeakDetection.Mode";
	}
}
