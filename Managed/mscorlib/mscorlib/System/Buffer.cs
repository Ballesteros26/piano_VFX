using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Manipulates arrays of primitive types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000134 RID: 308
	[ComVisible(true)]
	public static class Buffer
	{
		// Token: 0x06000B04 RID: 2820
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalBlockCopy(Array src, int srcOffsetBytes, Array dst, int dstOffsetBytes, int byteCount);

		// Token: 0x06000B05 RID: 2821 RVA: 0x00034548 File Offset: 0x00032748
		[SecurityCritical]
		internal unsafe static int IndexOfByte(byte* src, byte value, int index, int count)
		{
			byte* ptr = src + index;
			while ((ptr & 3) != 0)
			{
				if (count == 0)
				{
					return -1;
				}
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			uint num = (uint)(((int)value << 8) + (int)value);
			num = (num << 16) + num;
			while (count > 3)
			{
				uint num2 = *(uint*)ptr;
				num2 ^= num;
				uint num3 = 2130640639U + num2;
				num2 ^= uint.MaxValue;
				num2 ^= num3;
				num2 &= 2164326656U;
				if (num2 != 0U)
				{
					int num4 = (int)((long)(ptr - src));
					if (*ptr == value)
					{
						return num4;
					}
					if (ptr[1] == value)
					{
						return num4 + 1;
					}
					if (ptr[2] == value)
					{
						return num4 + 2;
					}
					if (ptr[3] == value)
					{
						return num4 + 3;
					}
				}
				count -= 4;
				ptr += 4;
			}
			while (count > 0)
			{
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			return -1;
		}

		// Token: 0x06000B06 RID: 2822
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte _GetByte(Array array, int index);

		// Token: 0x06000B07 RID: 2823
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SetByte(Array array, int index, byte value);

		// Token: 0x06000B08 RID: 2824
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _ByteLength(Array array);

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003460C File Offset: 0x0003280C
		[SecurityCritical]
		internal unsafe static void ZeroMemory(byte* src, long len)
		{
			for (;;)
			{
				long num = len;
				len = num - 1L;
				if (num <= 0L)
				{
					break;
				}
				src[len] = 0;
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00034624 File Offset: 0x00032824
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void Memcpy(byte[] dest, int destIndex, byte* src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = dest)
			{
				byte* ptr;
				if (dest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(ptr + destIndex, src + srcIndex, len);
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00034660 File Offset: 0x00032860
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		internal unsafe static void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = src)
			{
				byte* ptr;
				if (src == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(pDest + destIndex, ptr + srcIndex, len);
			}
		}

		/// <summary>Returns the number of bytes in the specified array.</summary>
		/// <returns>The number of bytes in the array.</returns>
		/// <param name="array">An array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not a primitive. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="array" /> is larger than 2 gigabytes (GB).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00034699 File Offset: 0x00032899
		public static int ByteLength(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Buffer._ByteLength(array);
			if (num < 0)
			{
				throw new ArgumentException(Locale.GetText("Object must be an array of primitives."));
			}
			return num;
		}

		/// <summary>Retrieves the byte at a specified location in a specified array.</summary>
		/// <returns>Returns the <paramref name="index" /> byte in the array.</returns>
		/// <param name="array">An array. </param>
		/// <param name="index">A location in the array. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not a primitive. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative or greater than the length of <paramref name="array" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="array" /> is larger than 2 gigabytes (GB).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B0D RID: 2829 RVA: 0x000346C3 File Offset: 0x000328C3
		public static byte GetByte(Array array, int index)
		{
			if (index < 0 || index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return Buffer._GetByte(array, index);
		}

		/// <summary>Assigns a specified value to a byte at a particular location in a specified array.</summary>
		/// <param name="array">An array. </param>
		/// <param name="index">A location in the array. </param>
		/// <param name="value">A value to assign. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not a primitive. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative or greater than the length of <paramref name="array" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="array" /> is larger than 2 gigabytes (GB).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B0E RID: 2830 RVA: 0x000346E4 File Offset: 0x000328E4
		public static void SetByte(Array array, int index, byte value)
		{
			if (index < 0 || index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			Buffer._SetByte(array, index, value);
		}

		/// <summary>Copies a specified number of bytes from a source array starting at a particular offset to a destination array starting at a particular offset.</summary>
		/// <param name="src">The source buffer. </param>
		/// <param name="srcOffset">The zero-based byte offset into <paramref name="src" />. </param>
		/// <param name="dst">The destination buffer. </param>
		/// <param name="dstOffset">The zero-based byte offset into <paramref name="dst" />. </param>
		/// <param name="count">The number of bytes to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="src" /> or <paramref name="dst" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="src" /> or <paramref name="dst" /> is not an array of primitives.-or- The number of bytes in <paramref name="src" /> is less than <paramref name="srcOffset" /> plus <paramref name="count" />.-or- The number of bytes in <paramref name="dst" /> is less than <paramref name="dstOffset" /> plus <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="srcOffset" />, <paramref name="dstOffset" />, or <paramref name="count" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B0F RID: 2831 RVA: 0x00034708 File Offset: 0x00032908
		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (dst == null)
			{
				throw new ArgumentNullException("dst");
			}
			if (srcOffset < 0)
			{
				throw new ArgumentOutOfRangeException("srcOffset", Locale.GetText("Non-negative number required."));
			}
			if (dstOffset < 0)
			{
				throw new ArgumentOutOfRangeException("dstOffset", Locale.GetText("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Locale.GetText("Non-negative number required."));
			}
			if (!Buffer.InternalBlockCopy(src, srcOffset, dst, dstOffset, count) && (srcOffset > Buffer.ByteLength(src) - count || dstOffset > Buffer.ByteLength(dst) - count))
			{
				throw new ArgumentException(Locale.GetText("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000347B4 File Offset: 0x000329B4
		[CLSCompliant(false)]
		public unsafe static void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			byte* ptr = (byte*)source;
			byte* ptr2 = (byte*)destination;
			while (sourceBytesToCopy > 2147483647L)
			{
				Buffer.Memcpy(ptr2, ptr, int.MaxValue);
				sourceBytesToCopy -= 2147483647L;
				ptr += int.MaxValue;
				ptr2 += int.MaxValue;
			}
			Buffer.memcpy1(ptr2, ptr, (int)sourceBytesToCopy);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003480C File Offset: 0x00032A0C
		[CLSCompliant(false)]
		public unsafe static void MemoryCopy(void* source, void* destination, ulong destinationSizeInBytes, ulong sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			byte* ptr = (byte*)source;
			byte* ptr2 = (byte*)destination;
			while (sourceBytesToCopy > 2147483647UL)
			{
				Buffer.Memcpy(ptr2, ptr, int.MaxValue);
				sourceBytesToCopy -= 2147483647UL;
				ptr += int.MaxValue;
				ptr2 += int.MaxValue;
			}
			Buffer.Memcpy(ptr2, ptr, (int)sourceBytesToCopy);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00034864 File Offset: 0x00032A64
		internal unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + (IntPtr)2 * 4) = *(int*)(src + (IntPtr)2 * 4);
				*(int*)(dest + (IntPtr)3 * 4) = *(int*)(src + (IntPtr)3 * 4);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000348E4 File Offset: 0x00032AE4
		internal unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + (IntPtr)2 * 2) = *(short*)(src + (IntPtr)2 * 2);
				*(short*)(dest + (IntPtr)3 * 2) = *(short*)(src + (IntPtr)3 * 2);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00034950 File Offset: 0x00032B50
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000349D8 File Offset: 0x00032BD8
		internal unsafe static void Memcpy(byte* dest, byte* src, int size)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && size >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					size--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && size >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					size -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					Buffer.memcpy1(dest, src, size);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					Buffer.memcpy2(dest, src, size);
					return;
				}
			}
			Buffer.memcpy4(dest, src, size);
		}
	}
}
