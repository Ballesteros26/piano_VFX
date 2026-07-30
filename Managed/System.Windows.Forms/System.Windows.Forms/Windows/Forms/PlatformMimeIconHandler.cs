using System;

namespace System.Windows.Forms
{
	// Token: 0x02000265 RID: 613
	internal abstract class PlatformMimeIconHandler
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00099138 File Offset: 0x00097338
		public MimeExtensionHandlerStatus MimeExtensionHandlerStatus
		{
			get
			{
				return this.mimeExtensionHandlerStatus;
			}
		}

		// Token: 0x060027D7 RID: 10199
		public abstract MimeExtensionHandlerStatus Start();

		// Token: 0x060027D8 RID: 10200 RVA: 0x00099140 File Offset: 0x00097340
		public virtual object AddAndGetIconIndex(string filename, string mime_type)
		{
			return null;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x00099144 File Offset: 0x00097344
		public virtual object AddAndGetIconIndex(string mime_type)
		{
			return null;
		}

		// Token: 0x040013F2 RID: 5106
		protected MimeExtensionHandlerStatus mimeExtensionHandlerStatus;
	}
}
