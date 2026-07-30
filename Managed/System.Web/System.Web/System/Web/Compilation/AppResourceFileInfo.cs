using System;
using System.IO;

namespace System.Web.Compilation
{
	// Token: 0x0200060F RID: 1551
	internal class AppResourceFileInfo
	{
		// Token: 0x060042DA RID: 17114 RVA: 0x000B0F4C File Offset: 0x000AF14C
		public AppResourceFileInfo(FileInfo info, AppResourceFileKind kind)
		{
			this.Embeddable = kind == AppResourceFileKind.Resource || kind == AppResourceFileKind.Binary;
			this.Compilable = kind == AppResourceFileKind.ResX;
			this.Info = info;
			this.Kind = kind;
			this.Seen = false;
		}

		// Token: 0x040023C9 RID: 9161
		public readonly bool Embeddable;

		// Token: 0x040023CA RID: 9162
		public readonly bool Compilable;

		// Token: 0x040023CB RID: 9163
		public readonly FileInfo Info;

		// Token: 0x040023CC RID: 9164
		public readonly AppResourceFileKind Kind;

		// Token: 0x040023CD RID: 9165
		public bool Seen;
	}
}
