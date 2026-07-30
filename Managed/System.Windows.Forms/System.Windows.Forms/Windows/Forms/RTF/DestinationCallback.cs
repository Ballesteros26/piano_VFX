using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000024 RID: 36
	internal class DestinationCallback
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005ED8 File Offset: 0x000040D8
		public DestinationCallback()
		{
			this.callbacks = new DestinationDelegate[Enum.GetValues(typeof(Minor)).Length];
		}

		// Token: 0x17000034 RID: 52
		public DestinationDelegate this[Minor c]
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

		// Token: 0x04000070 RID: 112
		private DestinationDelegate[] callbacks;
	}
}
