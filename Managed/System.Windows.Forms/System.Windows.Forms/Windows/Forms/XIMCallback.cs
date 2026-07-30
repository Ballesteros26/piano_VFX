using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200043C RID: 1084
	[Serializable]
	[StructLayout(0)]
	internal class XIMCallback
	{
		// Token: 0x0600462A RID: 17962 RVA: 0x00114730 File Offset: 0x00112930
		public XIMCallback(IntPtr clientData, XIMProc proc)
		{
			this.client_data = clientData;
			this.gch = GCHandle.Alloc(proc);
			this.callback = proc;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00114760 File Offset: 0x00112960
		~XIMCallback()
		{
			this.gch.Free();
		}

		// Token: 0x04002289 RID: 8841
		public IntPtr client_data;

		// Token: 0x0400228A RID: 8842
		public XIMProc callback;

		// Token: 0x0400228B RID: 8843
		[NonSerialized]
		private GCHandle gch;
	}
}
