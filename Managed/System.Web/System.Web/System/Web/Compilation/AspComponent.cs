using System;

namespace System.Web.Compilation
{
	// Token: 0x0200061A RID: 1562
	internal sealed class AspComponent
	{
		// Token: 0x06004325 RID: 17189 RVA: 0x000B31BC File Offset: 0x000B13BC
		public AspComponent(Type type, string ns, string prefix, string source, bool fromConfig)
		{
			this.Type = type;
			this.Namespace = ns;
			this.Prefix = prefix;
			this.Source = source;
			this.FromConfig = fromConfig;
		}

		// Token: 0x040023EA RID: 9194
		public readonly Type Type;

		// Token: 0x040023EB RID: 9195
		public readonly string Prefix;

		// Token: 0x040023EC RID: 9196
		public readonly string Source;

		// Token: 0x040023ED RID: 9197
		public readonly bool FromConfig;

		// Token: 0x040023EE RID: 9198
		public readonly string Namespace;
	}
}
