using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct GdipImageCodecInfo
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x00015BE4 File Offset: 0x00013DE4
		internal static void MarshalTo(GdipImageCodecInfo gdipcodec, ImageCodecInfo codec)
		{
			codec.CodecName = Marshal.PtrToStringUni(gdipcodec.CodecName);
			codec.DllName = Marshal.PtrToStringUni(gdipcodec.DllName);
			codec.FormatDescription = Marshal.PtrToStringUni(gdipcodec.FormatDescription);
			codec.FilenameExtension = Marshal.PtrToStringUni(gdipcodec.FilenameExtension);
			codec.MimeType = Marshal.PtrToStringUni(gdipcodec.MimeType);
			codec.Clsid = gdipcodec.Clsid;
			codec.FormatID = gdipcodec.FormatID;
			codec.Flags = gdipcodec.Flags;
			codec.Version = gdipcodec.Version;
			codec.SignatureMasks = new byte[gdipcodec.SigCount][];
			codec.SignaturePatterns = new byte[gdipcodec.SigCount][];
			IntPtr sigPattern = gdipcodec.SigPattern;
			IntPtr sigMask = gdipcodec.SigMask;
			for (int i = 0; i < gdipcodec.SigCount; i++)
			{
				codec.SignatureMasks[i] = new byte[gdipcodec.SigSize];
				Marshal.Copy(sigMask, codec.SignatureMasks[i], 0, gdipcodec.SigSize);
				sigMask = new IntPtr(sigMask.ToInt64() + (long)gdipcodec.SigSize);
				codec.SignaturePatterns[i] = new byte[gdipcodec.SigSize];
				Marshal.Copy(sigPattern, codec.SignaturePatterns[i], 0, gdipcodec.SigSize);
				sigPattern = new IntPtr(sigPattern.ToInt64() + (long)gdipcodec.SigSize);
			}
		}

		// Token: 0x040005F4 RID: 1524
		internal Guid Clsid;

		// Token: 0x040005F5 RID: 1525
		internal Guid FormatID;

		// Token: 0x040005F6 RID: 1526
		internal IntPtr CodecName;

		// Token: 0x040005F7 RID: 1527
		internal IntPtr DllName;

		// Token: 0x040005F8 RID: 1528
		internal IntPtr FormatDescription;

		// Token: 0x040005F9 RID: 1529
		internal IntPtr FilenameExtension;

		// Token: 0x040005FA RID: 1530
		internal IntPtr MimeType;

		// Token: 0x040005FB RID: 1531
		internal ImageCodecFlags Flags;

		// Token: 0x040005FC RID: 1532
		internal int Version;

		// Token: 0x040005FD RID: 1533
		internal int SigCount;

		// Token: 0x040005FE RID: 1534
		internal int SigSize;

		// Token: 0x040005FF RID: 1535
		private IntPtr SigPattern;

		// Token: 0x04000600 RID: 1536
		private IntPtr SigMask;
	}
}
