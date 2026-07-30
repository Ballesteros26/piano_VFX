using System;

namespace System
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400001E RID: 30
		private string comment;
	}
}
