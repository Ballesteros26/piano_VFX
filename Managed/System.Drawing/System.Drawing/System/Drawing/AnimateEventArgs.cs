using System;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000073 RID: 115
	internal class AnimateEventArgs : EventArgs
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0000EBD7 File Offset: 0x0000CDD7
		public AnimateEventArgs(Image image)
		{
			this.frameCount = image.GetFrameCount(FrameDimension.Time);
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public Thread RunThread
		{
			get
			{
				return this.thread;
			}
			set
			{
				this.thread = value;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000EC01 File Offset: 0x0000CE01
		public int GetNextFrame()
		{
			if (this.activeFrame < this.frameCount - 1)
			{
				this.activeFrame++;
			}
			else
			{
				this.activeFrame = 0;
			}
			return this.activeFrame;
		}

		// Token: 0x040003F3 RID: 1011
		private int frameCount;

		// Token: 0x040003F4 RID: 1012
		private int activeFrame;

		// Token: 0x040003F5 RID: 1013
		private Thread thread;
	}
}
