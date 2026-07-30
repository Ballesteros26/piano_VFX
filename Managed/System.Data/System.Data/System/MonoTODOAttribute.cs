using System;

namespace System
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005E14 File Offset: 0x00004014
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005E1C File Offset: 0x0000401C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005E2B File Offset: 0x0000402B
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000409 RID: 1033
		private string comment;
	}
}
