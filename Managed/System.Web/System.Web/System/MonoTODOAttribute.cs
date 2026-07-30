using System;

namespace System
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002C1E File Offset: 0x00000E1E
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C26 File Offset: 0x00000E26
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C35 File Offset: 0x00000E35
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000D43 RID: 3395
		private string comment;
	}
}
