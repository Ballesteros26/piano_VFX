using System;

namespace System
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000022C5 File Offset: 0x000004C5
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000022CD File Offset: 0x000004CD
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022DC File Offset: 0x000004DC
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040001CB RID: 459
		private string comment;
	}
}
