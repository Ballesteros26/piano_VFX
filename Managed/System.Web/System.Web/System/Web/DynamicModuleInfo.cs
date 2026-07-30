using System;

namespace System.Web
{
	// Token: 0x0200006B RID: 107
	internal struct DynamicModuleInfo
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x00008C9E File Offset: 0x00006E9E
		public DynamicModuleInfo(Type type, string name)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x04000E61 RID: 3681
		public readonly string Name;

		// Token: 0x04000E62 RID: 3682
		public readonly Type Type;
	}
}
