using System;

namespace System.Numerics
{
	// Token: 0x0200001A RID: 26
	internal static class Vector
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00010BD5 File Offset: 0x0000EDD5
		[JitIntrinsic]
		public static bool IsHardwareAccelerated
		{
			get
			{
				return false;
			}
		}
	}
}
