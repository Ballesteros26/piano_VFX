using System;

namespace System.Windows.Forms
{
	// Token: 0x0200040F RID: 1039
	internal struct MotifWmHints
	{
		// Token: 0x06004627 RID: 17959 RVA: 0x0011461C File Offset: 0x0011281C
		public override string ToString()
		{
			return string.Format("MotifWmHints <flags={0}, functions={1}, decorations={2}, input_mode={3}, status={4}", new object[]
			{
				(MotifFlags)this.flags.ToInt32(),
				(MotifFunctions)this.functions.ToInt32(),
				(MotifDecorations)this.decorations.ToInt32(),
				(MotifInputMode)this.input_mode.ToInt32(),
				this.status.ToInt32()
			});
		}

		// Token: 0x04002094 RID: 8340
		internal IntPtr flags;

		// Token: 0x04002095 RID: 8341
		internal IntPtr functions;

		// Token: 0x04002096 RID: 8342
		internal IntPtr decorations;

		// Token: 0x04002097 RID: 8343
		internal IntPtr input_mode;

		// Token: 0x04002098 RID: 8344
		internal IntPtr status;
	}
}
