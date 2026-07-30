using System;
using System.Runtime.Versioning;

namespace System.Web.Util
{
	// Token: 0x0200010E RID: 270
	internal sealed class BinaryCompatibility
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00025E10 File Offset: 0x00024010
		static BinaryCompatibility()
		{
			TelemetryLogger.LogTargetFramework(BinaryCompatibility.Current.TargetFramework);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00025E40 File Offset: 0x00024040
		public BinaryCompatibility(FrameworkName frameworkName)
		{
			Version version = VersionUtil.FrameworkDefault;
			if (frameworkName != null && frameworkName.Identifier == ".NETFramework")
			{
				version = frameworkName.Version;
			}
			this.TargetFramework = version;
			this.TargetsAtLeastFramework45 = version >= VersionUtil.Framework45;
			this.TargetsAtLeastFramework451 = version >= VersionUtil.Framework451;
			this.TargetsAtLeastFramework452 = version >= VersionUtil.Framework452;
			this.TargetsAtLeastFramework46 = version >= VersionUtil.Framework46;
			this.TargetsAtLeastFramework461 = version >= VersionUtil.Framework461;
			this.TargetsAtLeastFramework463 = version >= VersionUtil.Framework463;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00025EE8 File Offset: 0x000240E8
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00025EF0 File Offset: 0x000240F0
		public bool TargetsAtLeastFramework45 { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00025EF9 File Offset: 0x000240F9
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00025F01 File Offset: 0x00024101
		public bool TargetsAtLeastFramework451 { get; private set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00025F0A File Offset: 0x0002410A
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x00025F12 File Offset: 0x00024112
		public bool TargetsAtLeastFramework452 { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00025F1B File Offset: 0x0002411B
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00025F23 File Offset: 0x00024123
		public bool TargetsAtLeastFramework46 { get; private set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00025F2C File Offset: 0x0002412C
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00025F34 File Offset: 0x00024134
		public bool TargetsAtLeastFramework461 { get; private set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00025F3D File Offset: 0x0002413D
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x00025F45 File Offset: 0x00024145
		public bool TargetsAtLeastFramework463 { get; private set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00025F4E File Offset: 0x0002414E
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x00025F56 File Offset: 0x00024156
		public Version TargetFramework { get; private set; }

		// Token: 0x0400118F RID: 4495
		internal const string TargetFrameworkKey = "ASPNET_TARGETFRAMEWORK";

		// Token: 0x04001190 RID: 4496
		public static readonly BinaryCompatibility Current = new BinaryCompatibility(AppDomain.CurrentDomain.GetData("ASPNET_TARGETFRAMEWORK") as FrameworkName);
	}
}
