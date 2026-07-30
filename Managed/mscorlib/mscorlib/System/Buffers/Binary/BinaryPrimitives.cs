using System;
using System.Runtime.CompilerServices;

namespace System.Buffers.Binary
{
	// Token: 0x020009A9 RID: 2473
	public static class BinaryPrimitives
	{
		// Token: 0x06005A66 RID: 23142 RVA: 0x00002119 File Offset: 0x00000319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte ReverseEndianness(sbyte value)
		{
			return value;
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0012C176 File Offset: 0x0012A376
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReverseEndianness(short value)
		{
			return (short)(((int)(value & 255) << 8) | (((int)value & 65280) >> 8));
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x0012C18C File Offset: 0x0012A38C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReverseEndianness(int value)
		{
			return (int)BinaryPrimitives.ReverseEndianness((uint)value);
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0012C194 File Offset: 0x0012A394
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReverseEndianness(long value)
		{
			return (long)BinaryPrimitives.ReverseEndianness((ulong)value);
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00002119 File Offset: 0x00000319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ReverseEndianness(byte value)
		{
			return value;
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0012C19C File Offset: 0x0012A39C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReverseEndianness(ushort value)
		{
			return (ushort)(((int)(value & 255) << 8) | (int)((uint)(value & 65280) >> 8));
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x0012C1B2 File Offset: 0x0012A3B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseEndianness(uint value)
		{
			value = (value << 16) | (value >> 16);
			value = ((value & 16711935U) << 8) | ((value & 4278255360U) >> 8);
			return value;
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x0012C1D8 File Offset: 0x0012A3D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReverseEndianness(ulong value)
		{
			value = (value << 32) | (value >> 32);
			value = ((value & 281470681808895UL) << 16) | ((value & 18446462603027742720UL) >> 16);
			value = ((value & 71777214294589695UL) << 8) | ((value & 18374966859414961920UL) >> 8);
			return value;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0012C230 File Offset: 0x0012A430
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T ReadMachineEndian<T>(ReadOnlySpan<byte> buffer) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", typeof(T)));
			}
			if (Unsafe.SizeOf<T>() > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Unsafe.ReadUnaligned<T>(buffer.DangerousGetPinnableReference());
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0012C280 File Offset: 0x0012A480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadMachineEndian<T>(ReadOnlySpan<byte> buffer, out T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", typeof(T)));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)buffer.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(buffer.DangerousGetPinnableReference());
			return true;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x0012C2DC File Offset: 0x0012A4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> buffer)
		{
			short num = BinaryPrimitives.ReadMachineEndian<short>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0012C300 File Offset: 0x0012A500
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32BigEndian(ReadOnlySpan<byte> buffer)
		{
			int num = BinaryPrimitives.ReadMachineEndian<int>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x0012C324 File Offset: 0x0012A524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64BigEndian(ReadOnlySpan<byte> buffer)
		{
			long num = BinaryPrimitives.ReadMachineEndian<long>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x0012C348 File Offset: 0x0012A548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> buffer)
		{
			ushort num = BinaryPrimitives.ReadMachineEndian<ushort>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0012C36C File Offset: 0x0012A56C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> buffer)
		{
			uint num = BinaryPrimitives.ReadMachineEndian<uint>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0012C390 File Offset: 0x0012A590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> buffer)
		{
			ulong num = BinaryPrimitives.ReadMachineEndian<ulong>(buffer);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x0012C3B3 File Offset: 0x0012A5B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16BigEndian(ReadOnlySpan<byte> buffer, out short value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<short>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x0012C3CC File Offset: 0x0012A5CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32BigEndian(ReadOnlySpan<byte> buffer, out int value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<int>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x0012C3E5 File Offset: 0x0012A5E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64BigEndian(ReadOnlySpan<byte> buffer, out long value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<long>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x0012C3FE File Offset: 0x0012A5FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> buffer, out ushort value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<ushort>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x0012C417 File Offset: 0x0012A617
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32BigEndian(ReadOnlySpan<byte> buffer, out uint value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<uint>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0012C430 File Offset: 0x0012A630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64BigEndian(ReadOnlySpan<byte> buffer, out ulong value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<ulong>(buffer, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0012C44C File Offset: 0x0012A64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16LittleEndian(ReadOnlySpan<byte> buffer)
		{
			short num = BinaryPrimitives.ReadMachineEndian<short>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x0012C470 File Offset: 0x0012A670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32LittleEndian(ReadOnlySpan<byte> buffer)
		{
			int num = BinaryPrimitives.ReadMachineEndian<int>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x0012C494 File Offset: 0x0012A694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64LittleEndian(ReadOnlySpan<byte> buffer)
		{
			long num = BinaryPrimitives.ReadMachineEndian<long>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x0012C4B8 File Offset: 0x0012A6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> buffer)
		{
			ushort num = BinaryPrimitives.ReadMachineEndian<ushort>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x0012C4DC File Offset: 0x0012A6DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> buffer)
		{
			uint num = BinaryPrimitives.ReadMachineEndian<uint>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x0012C500 File Offset: 0x0012A700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64LittleEndian(ReadOnlySpan<byte> buffer)
		{
			ulong num = BinaryPrimitives.ReadMachineEndian<ulong>(buffer);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0012C523 File Offset: 0x0012A723
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16LittleEndian(ReadOnlySpan<byte> buffer, out short value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<short>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x0012C53C File Offset: 0x0012A73C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32LittleEndian(ReadOnlySpan<byte> buffer, out int value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<int>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x0012C555 File Offset: 0x0012A755
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64LittleEndian(ReadOnlySpan<byte> buffer, out long value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<long>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x0012C56E File Offset: 0x0012A76E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16LittleEndian(ReadOnlySpan<byte> buffer, out ushort value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<ushort>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x0012C587 File Offset: 0x0012A787
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32LittleEndian(ReadOnlySpan<byte> buffer, out uint value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<uint>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x0012C5A0 File Offset: 0x0012A7A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64LittleEndian(ReadOnlySpan<byte> buffer, out ulong value)
		{
			bool flag = BinaryPrimitives.TryReadMachineEndian<ulong>(buffer, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x0012C5BC File Offset: 0x0012A7BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteMachineEndian<T>(Span<byte> buffer, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", typeof(T)));
			}
			if (Unsafe.SizeOf<T>() > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			Unsafe.WriteUnaligned<T>(buffer.DangerousGetPinnableReference(), value);
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x0012C610 File Offset: 0x0012A810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteMachineEndian<T>(Span<byte> buffer, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", typeof(T)));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)buffer.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(buffer.DangerousGetPinnableReference(), value);
			return true;
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0012C663 File Offset: 0x0012A863
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16BigEndian(Span<byte> buffer, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<short>(buffer, ref value);
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0012C67C File Offset: 0x0012A87C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32BigEndian(Span<byte> buffer, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<int>(buffer, ref value);
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x0012C695 File Offset: 0x0012A895
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64BigEndian(Span<byte> buffer, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<long>(buffer, ref value);
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x0012C6AE File Offset: 0x0012A8AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16BigEndian(Span<byte> buffer, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<ushort>(buffer, ref value);
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x0012C6C7 File Offset: 0x0012A8C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32BigEndian(Span<byte> buffer, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<uint>(buffer, ref value);
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x0012C6E0 File Offset: 0x0012A8E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64BigEndian(Span<byte> buffer, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<ulong>(buffer, ref value);
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x0012C6F9 File Offset: 0x0012A8F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16BigEndian(Span<byte> buffer, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<short>(buffer, ref value);
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x0012C712 File Offset: 0x0012A912
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32BigEndian(Span<byte> buffer, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<int>(buffer, ref value);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0012C72B File Offset: 0x0012A92B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64BigEndian(Span<byte> buffer, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<long>(buffer, ref value);
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x0012C744 File Offset: 0x0012A944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16BigEndian(Span<byte> buffer, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<ushort>(buffer, ref value);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x0012C75D File Offset: 0x0012A95D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32BigEndian(Span<byte> buffer, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<uint>(buffer, ref value);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0012C776 File Offset: 0x0012A976
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64BigEndian(Span<byte> buffer, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<ulong>(buffer, ref value);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0012C78F File Offset: 0x0012A98F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16LittleEndian(Span<byte> buffer, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<short>(buffer, ref value);
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x0012C7A8 File Offset: 0x0012A9A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32LittleEndian(Span<byte> buffer, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<int>(buffer, ref value);
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x0012C7C1 File Offset: 0x0012A9C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64LittleEndian(Span<byte> buffer, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<long>(buffer, ref value);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0012C7DA File Offset: 0x0012A9DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16LittleEndian(Span<byte> buffer, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<ushort>(buffer, ref value);
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0012C7F3 File Offset: 0x0012A9F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32LittleEndian(Span<byte> buffer, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<uint>(buffer, ref value);
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x0012C80C File Offset: 0x0012AA0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64LittleEndian(Span<byte> buffer, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			BinaryPrimitives.WriteMachineEndian<ulong>(buffer, ref value);
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x0012C825 File Offset: 0x0012AA25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16LittleEndian(Span<byte> buffer, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<short>(buffer, ref value);
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x0012C83E File Offset: 0x0012AA3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32LittleEndian(Span<byte> buffer, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<int>(buffer, ref value);
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x0012C857 File Offset: 0x0012AA57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64LittleEndian(Span<byte> buffer, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<long>(buffer, ref value);
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0012C870 File Offset: 0x0012AA70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16LittleEndian(Span<byte> buffer, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<ushort>(buffer, ref value);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x0012C889 File Offset: 0x0012AA89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32LittleEndian(Span<byte> buffer, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<uint>(buffer, ref value);
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0012C8A2 File Offset: 0x0012AAA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64LittleEndian(Span<byte> buffer, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return BinaryPrimitives.TryWriteMachineEndian<ulong>(buffer, ref value);
		}
	}
}
