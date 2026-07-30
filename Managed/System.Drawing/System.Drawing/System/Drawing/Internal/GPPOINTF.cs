using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000EF RID: 239
	[StructLayout(LayoutKind.Sequential)]
	internal class GPPOINTF
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x00002050 File Offset: 0x00000250
		internal GPPOINTF()
		{
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001A577 File Offset: 0x00018777
		internal GPPOINTF(PointF pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001A599 File Offset: 0x00018799
		internal PointF ToPoint()
		{
			return new PointF(this.X, this.Y);
		}

		// Token: 0x04000815 RID: 2069
		internal float X;

		// Token: 0x04000816 RID: 2070
		internal float Y;
	}
}
