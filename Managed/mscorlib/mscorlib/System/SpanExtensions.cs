using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CF RID: 207
	public static class SpanExtensions
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x000234A4 File Offset: 0x000216A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(this Span<T> source) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			int num = checked(source.Length * Unsafe.SizeOf<T>());
			return new Span<byte>(Unsafe.As<Pinnable<byte>>(source.Pinnable), source.ByteOffset, num);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000234F0 File Offset: 0x000216F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(this ReadOnlySpan<T> source) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			int num = checked(source.Length * Unsafe.SizeOf<T>());
			return new ReadOnlySpan<byte>(Unsafe.As<Pinnable<byte>>(source.Pinnable), source.ByteOffset, num);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0002353A File Offset: 0x0002173A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsReadOnlySpan(this string text)
		{
			if (text == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
			}
			return new ReadOnlySpan<char>(Unsafe.As<Pinnable<char>>(text), SpanExtensions.StringAdjustment, text.Length);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002355C File Offset: 0x0002175C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<TTo> NonPortableCast<TFrom, TTo>(this Span<TFrom> source) where TFrom : struct where TTo : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (SpanHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			checked
			{
				int num = (int)(unchecked((long)source.Length) * unchecked((long)Unsafe.SizeOf<TFrom>()) / unchecked((long)Unsafe.SizeOf<TTo>()));
				return new Span<TTo>(Unsafe.As<Pinnable<TTo>>(source.Pinnable), source.ByteOffset, num);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000235C8 File Offset: 0x000217C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<TTo> NonPortableCast<TFrom, TTo>(this ReadOnlySpan<TFrom> source) where TFrom : struct where TTo : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (SpanHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			checked
			{
				int num = (int)(unchecked((long)source.Length) * unchecked((long)Unsafe.SizeOf<TFrom>()) / unchecked((long)Unsafe.SizeOf<TTo>()));
				return new ReadOnlySpan<TTo>(Unsafe.As<Pinnable<TTo>>(source.Pinnable), source.ByteOffset, num);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00023634 File Offset: 0x00021834
		private unsafe static IntPtr MeasureStringAdjustment()
		{
			string text;
			object obj = (text = "a");
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Unsafe.ByteOffset<char>(ref Unsafe.As<Pinnable<char>>(obj).Data, Unsafe.AsRef<char>((void*)ptr));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002366B File Offset: 0x0002186B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, T value) where T : struct, IEquatable<T>
		{
			return SpanHelpers.IndexOf<T>(span.DangerousGetPinnableReference(), value, span.Length);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00023681 File Offset: 0x00021881
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf(this Span<byte> span, byte value)
		{
			return SpanHelpers.IndexOf(span.DangerousGetPinnableReference(), value, span.Length);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00023697 File Offset: 0x00021897
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : struct, IEquatable<T>
		{
			return SpanHelpers.IndexOf<T>(span.DangerousGetPinnableReference(), span.Length, value.DangerousGetPinnableReference(), value.Length);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000236BA File Offset: 0x000218BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf(this Span<byte> span, ReadOnlySpan<byte> value)
		{
			return SpanHelpers.IndexOf(span.DangerousGetPinnableReference(), span.Length, value.DangerousGetPinnableReference(), value.Length);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000236E0 File Offset: 0x000218E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this Span<T> first, ReadOnlySpan<T> second) where T : struct, IEquatable<T>
		{
			int length = first.Length;
			return length == second.Length && SpanHelpers.SequenceEqual<T>(first.DangerousGetPinnableReference(), second.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00023718 File Offset: 0x00021918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual(this Span<byte> first, ReadOnlySpan<byte> second)
		{
			int length = first.Length;
			return length == second.Length && SpanHelpers.SequenceEqual(first.DangerousGetPinnableReference(), second.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002374D File Offset: 0x0002194D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : struct, IEquatable<T>
		{
			return SpanHelpers.IndexOf<T>(span.DangerousGetPinnableReference(), value, span.Length);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00023763 File Offset: 0x00021963
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf(this ReadOnlySpan<byte> span, byte value)
		{
			return SpanHelpers.IndexOf(span.DangerousGetPinnableReference(), value, span.Length);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00023779 File Offset: 0x00021979
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : struct, IEquatable<T>
		{
			return SpanHelpers.IndexOf<T>(span.DangerousGetPinnableReference(), span.Length, value.DangerousGetPinnableReference(), value.Length);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002379C File Offset: 0x0002199C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf(this ReadOnlySpan<byte> span, ReadOnlySpan<byte> value)
		{
			return SpanHelpers.IndexOf(span.DangerousGetPinnableReference(), span.Length, value.DangerousGetPinnableReference(), value.Length);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000237BF File Offset: 0x000219BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this Span<byte> span, byte value0, byte value1)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), value0, value1, span.Length);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000237D6 File Offset: 0x000219D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this Span<byte> span, byte value0, byte value1, byte value2)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), value0, value1, value2, span.Length);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000237EE File Offset: 0x000219EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this Span<byte> span, ReadOnlySpan<byte> values)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), span.Length, values.DangerousGetPinnableReference(), values.Length);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00023811 File Offset: 0x00021A11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this ReadOnlySpan<byte> span, byte value0, byte value1)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), value0, value1, span.Length);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00023828 File Offset: 0x00021A28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this ReadOnlySpan<byte> span, byte value0, byte value1, byte value2)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), value0, value1, value2, span.Length);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00023840 File Offset: 0x00021A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny(this ReadOnlySpan<byte> span, ReadOnlySpan<byte> values)
		{
			return SpanHelpers.IndexOfAny(span.DangerousGetPinnableReference(), span.Length, values.DangerousGetPinnableReference(), values.Length);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00023864 File Offset: 0x00021A64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this ReadOnlySpan<T> first, ReadOnlySpan<T> second) where T : struct, IEquatable<T>
		{
			int length = first.Length;
			return length == second.Length && SpanHelpers.SequenceEqual<T>(first.DangerousGetPinnableReference(), second.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0002389C File Offset: 0x00021A9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual(this ReadOnlySpan<byte> first, ReadOnlySpan<byte> second)
		{
			int length = first.Length;
			return length == second.Length && SpanHelpers.SequenceEqual(first.DangerousGetPinnableReference(), second.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000238D4 File Offset: 0x00021AD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith(this Span<byte> span, ReadOnlySpan<byte> value)
		{
			int length = value.Length;
			return length <= span.Length && SpanHelpers.SequenceEqual(span.DangerousGetPinnableReference(), value.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0002390C File Offset: 0x00021B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : struct, IEquatable<T>
		{
			int length = value.Length;
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(span.DangerousGetPinnableReference(), value.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00023944 File Offset: 0x00021B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith(this ReadOnlySpan<byte> span, ReadOnlySpan<byte> value)
		{
			int length = value.Length;
			return length <= span.Length && SpanHelpers.SequenceEqual(span.DangerousGetPinnableReference(), value.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0002397C File Offset: 0x00021B7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : struct, IEquatable<T>
		{
			int length = value.Length;
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(span.DangerousGetPinnableReference(), value.DangerousGetPinnableReference(), length);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000239B1 File Offset: 0x00021BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000239B9 File Offset: 0x00021BB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> arraySegment)
		{
			return new Span<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000239D5 File Offset: 0x00021BD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000239DD File Offset: 0x00021BDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> AsReadOnlySpan<T>(this ArraySegment<T> arraySegment)
		{
			return new ReadOnlySpan<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000239FC File Offset: 0x00021BFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] array, Span<T> destination)
		{
			new ReadOnlySpan<T>(array).CopyTo(destination);
		}

		// Token: 0x040006A2 RID: 1698
		private static readonly IntPtr StringAdjustment = SpanExtensions.MeasureStringAdjustment();
	}
}
