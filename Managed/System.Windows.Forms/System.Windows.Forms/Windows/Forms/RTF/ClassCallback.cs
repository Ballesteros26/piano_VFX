using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000022 RID: 34
	internal class ClassCallback
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00005D1C File Offset: 0x00003F1C
		public ClassCallback()
		{
			this.callbacks = new ClassDelegate[Enum.GetValues(typeof(Major)).Length];
		}

		// Token: 0x1700002F RID: 47
		public ClassDelegate this[TokenClass c]
		{
			get
			{
				return this.callbacks[(int)c];
			}
			set
			{
				this.callbacks[(int)c] = value;
			}
		}

		// Token: 0x0400006A RID: 106
		private ClassDelegate[] callbacks;
	}
}
