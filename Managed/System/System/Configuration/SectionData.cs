using System;

namespace System.Configuration
{
	// Token: 0x02000172 RID: 370
	internal class SectionData
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x00039F7B File Offset: 0x0003817B
		public SectionData(string sectionName, string typeName, bool allowLocation, AllowDefinition allowDefinition, bool requirePermission)
		{
			this.SectionName = sectionName;
			this.TypeName = typeName;
			this.AllowLocation = allowLocation;
			this.AllowDefinition = allowDefinition;
			this.RequirePermission = requirePermission;
		}

		// Token: 0x04000F93 RID: 3987
		public readonly string SectionName;

		// Token: 0x04000F94 RID: 3988
		public readonly string TypeName;

		// Token: 0x04000F95 RID: 3989
		public readonly bool AllowLocation;

		// Token: 0x04000F96 RID: 3990
		public readonly AllowDefinition AllowDefinition;

		// Token: 0x04000F97 RID: 3991
		public string FileName;

		// Token: 0x04000F98 RID: 3992
		public readonly bool RequirePermission;
	}
}
