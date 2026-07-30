using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000053 RID: 83
	internal class CFData : CFObject
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFData(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x0600015E RID: 350
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataCreate(IntPtr allocator, IntPtr bytes, IntPtr length);

		// Token: 0x0600015F RID: 351 RVA: 0x00004798 File Offset: 0x00002998
		public unsafe static CFData FromData(byte[] buffer)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return CFData.FromData((IntPtr)((void*)ptr), (IntPtr)buffer.Length);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000047D0 File Offset: 0x000029D0
		public static CFData FromData(IntPtr buffer, IntPtr length)
		{
			return new CFData(CFData.CFDataCreate(IntPtr.Zero, buffer, length), true);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000047E4 File Offset: 0x000029E4
		public IntPtr Length
		{
			get
			{
				return CFData.CFDataGetLength(base.Handle);
			}
		}

		// Token: 0x06000162 RID: 354
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataGetLength(IntPtr theData);

		// Token: 0x06000163 RID: 355
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataGetBytePtr(IntPtr theData);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000047F1 File Offset: 0x000029F1
		public IntPtr Bytes
		{
			get
			{
				return CFData.CFDataGetBytePtr(base.Handle);
			}
		}

		// Token: 0x1700002A RID: 42
		public byte this[long idx]
		{
			get
			{
				if (idx < 0L || idx > (long)this.Length)
				{
					throw new ArgumentException("idx");
				}
				return Marshal.ReadByte(new IntPtr(this.Bytes.ToInt64() + idx));
			}
			set
			{
				throw new NotImplementedException("NSData arrays can not be modified, use an NSMutableData instead");
			}
		}
	}
}
