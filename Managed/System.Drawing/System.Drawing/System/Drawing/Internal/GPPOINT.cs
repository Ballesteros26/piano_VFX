using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Sequential)]
	internal class GPPOINT
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00002050 File Offset: 0x00000250
		internal GPPOINT()
		{
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001A555 File Offset: 0x00018755
		internal GPPOINT(Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x04000813 RID: 2067
		internal int X;

		// Token: 0x04000814 RID: 2068
		internal int Y;
	}
}
