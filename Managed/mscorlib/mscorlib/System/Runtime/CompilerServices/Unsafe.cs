using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200089E RID: 2206
	internal static class Unsafe
	{
		// Token: 0x060054AC RID: 21676 RVA: 0x001280EC File Offset: 0x001262EC
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return (ref source) + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x001280F9 File Offset: 0x001262F9
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return (ref source) + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x001280EC File Offset: 0x001262EC
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00128105 File Offset: 0x00126305
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return (ref source) + byteOffset;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return (ref left) == (ref right);
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00002119 File Offset: 0x00000319
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00002119 File Offset: 0x00000319
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00002119 File Offset: 0x00000319
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0012810A File Offset: 0x0012630A
		public static IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return (ref target) - (ref origin);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0012810F File Offset: 0x0012630F
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x00128116 File Offset: 0x00126316
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x00128116 File Offset: 0x00126316
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00128120 File Offset: 0x00126320
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x00128128 File Offset: 0x00126328
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00128133 File Offset: 0x00126333
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0012813B File Offset: 0x0012633B
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return (ref source) - (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00128148 File Offset: 0x00126348
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}
	}
}
