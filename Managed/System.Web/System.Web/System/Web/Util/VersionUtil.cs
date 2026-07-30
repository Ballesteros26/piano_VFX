using System;

namespace System.Web.Util
{
	// Token: 0x02000136 RID: 310
	internal static class VersionUtil
	{
		// Token: 0x040011DF RID: 4575
		public static readonly Version Framework00 = new Version(0, 0);

		// Token: 0x040011E0 RID: 4576
		public static readonly Version Framework20 = new Version(2, 0);

		// Token: 0x040011E1 RID: 4577
		public static readonly Version Framework35 = new Version(3, 5);

		// Token: 0x040011E2 RID: 4578
		public static readonly Version Framework40 = new Version(4, 0);

		// Token: 0x040011E3 RID: 4579
		public static readonly Version Framework45 = new Version(4, 5);

		// Token: 0x040011E4 RID: 4580
		public static readonly Version Framework451 = new Version(4, 5, 1);

		// Token: 0x040011E5 RID: 4581
		public static readonly Version Framework452 = new Version(4, 5, 2);

		// Token: 0x040011E6 RID: 4582
		public static readonly Version Framework46 = new Version(4, 6);

		// Token: 0x040011E7 RID: 4583
		public static readonly Version Framework461 = new Version(4, 6, 1);

		// Token: 0x040011E8 RID: 4584
		public static readonly Version Framework463 = new Version(4, 6, 3);

		// Token: 0x040011E9 RID: 4585
		public static readonly Version FrameworkDefault = VersionUtil.Framework40;

		// Token: 0x040011EA RID: 4586
		public const string FrameworkDefaultString = "4.0";
	}
}
