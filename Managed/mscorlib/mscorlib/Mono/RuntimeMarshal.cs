using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono
{
	// Token: 0x02000019 RID: 25
	internal static class RuntimeMarshal
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal unsafe static string PtrToUtf8String(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return string.Empty;
			}
			byte* ptr2 = (byte*)(void*)ptr;
			int num = 0;
			try
			{
				while (*(ptr2++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return new string((sbyte*)(void*)ptr, 0, num, Encoding.UTF8);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F40 File Offset: 0x00002140
		internal static SafeStringMarshal MarshalString(string str)
		{
			return new SafeStringMarshal(str);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F48 File Offset: 0x00002148
		private unsafe static int DecodeBlobSize(IntPtr in_ptr, out IntPtr out_ptr)
		{
			byte* ptr = (byte*)(void*)in_ptr;
			uint num;
			if ((*ptr & 128) == 0)
			{
				num = (uint)(*ptr & 127);
				ptr++;
			}
			else if ((*ptr & 64) == 0)
			{
				num = (uint)(((int)(*ptr & 63) << 8) + (int)ptr[1]);
				ptr += 2;
			}
			else
			{
				num = (uint)(((int)(*ptr & 31) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3]);
				ptr += 4;
			}
			out_ptr = (IntPtr)((void*)ptr);
			return (int)num;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal static byte[] DecodeBlobArray(IntPtr ptr)
		{
			IntPtr intPtr;
			int num = RuntimeMarshal.DecodeBlobSize(ptr, out intPtr);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FDF File Offset: 0x000021DF
		internal static int AsciHexDigitValue(int c)
		{
			if (c >= 48 && c <= 57)
			{
				return c - 48;
			}
			if (c >= 97 && c <= 102)
			{
				return c - 97 + 10;
			}
			return c - 65 + 10;
		}

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeAssemblyName(ref MonoAssemblyName name, bool freeStruct);
	}
}
