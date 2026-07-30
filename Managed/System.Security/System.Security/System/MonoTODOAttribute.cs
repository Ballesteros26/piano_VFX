using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000028D1 File Offset: 0x00000AD1
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028D9 File Offset: 0x00000AD9
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000028E8 File Offset: 0x00000AE8
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400008D RID: 141
		private string comment;
	}
}
