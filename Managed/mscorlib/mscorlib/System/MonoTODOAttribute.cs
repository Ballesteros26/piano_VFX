using System;

namespace System
{
	// Token: 0x020000F2 RID: 242
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x00002180 File Offset: 0x00000380
		public MonoTODOAttribute()
		{
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00030A35 File Offset: 0x0002EC35
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00030A44 File Offset: 0x0002EC44
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040006FD RID: 1789
		private string comment;
	}
}
