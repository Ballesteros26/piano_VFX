using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000106 RID: 262
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal class ImageCodecInfoPrivate
	{
		// Token: 0x040009A5 RID: 2469
		[MarshalAs(UnmanagedType.Struct)]
		public Guid Clsid;

		// Token: 0x040009A6 RID: 2470
		[MarshalAs(UnmanagedType.Struct)]
		public Guid FormatID;

		// Token: 0x040009A7 RID: 2471
		public IntPtr CodecName = IntPtr.Zero;

		// Token: 0x040009A8 RID: 2472
		public IntPtr DllName = IntPtr.Zero;

		// Token: 0x040009A9 RID: 2473
		public IntPtr FormatDescription = IntPtr.Zero;

		// Token: 0x040009AA RID: 2474
		public IntPtr FilenameExtension = IntPtr.Zero;

		// Token: 0x040009AB RID: 2475
		public IntPtr MimeType = IntPtr.Zero;

		// Token: 0x040009AC RID: 2476
		public int Flags;

		// Token: 0x040009AD RID: 2477
		public int Version;

		// Token: 0x040009AE RID: 2478
		public int SigCount;

		// Token: 0x040009AF RID: 2479
		public int SigSize;

		// Token: 0x040009B0 RID: 2480
		public IntPtr SigPattern = IntPtr.Zero;

		// Token: 0x040009B1 RID: 2481
		public IntPtr SigMask = IntPtr.Zero;
	}
}
