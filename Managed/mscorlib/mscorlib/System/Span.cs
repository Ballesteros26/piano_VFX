using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CD RID: 205
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly ref struct Span<T>
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00022D78 File Offset: 0x00020F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException_ArrayTypeMustBeExactMatch(typeof(T));
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00022DE4 File Offset: 0x00020FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException_ArrayTypeMustBeExactMatch(typeof(T));
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00022E69 File Offset: 0x00021069
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = null;
			this._byteOffset = new IntPtr(pointer);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00022EA8 File Offset: 0x000210A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> DangerousCreate(object obj, ref T objectData, int length)
		{
			Pinnable<T> pinnable = Unsafe.As<Pinnable<T>>(obj);
			IntPtr intPtr = Unsafe.ByteOffset<T>(ref pinnable.Data, ref objectData);
			return new Span<T>(pinnable, intPtr, length);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00022ECF File Offset: 0x000210CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00022EE6 File Offset: 0x000210E6
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{{{0}[{1}]}}", typeof(T).Name, this._length);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00022F0C File Offset: 0x0002110C
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00022F14 File Offset: 0x00021114
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x17000127 RID: 295
		public ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				if (this._pinnable == null)
				{
					return Unsafe.Add<T>(Unsafe.AsRef<T>(this._byteOffset.ToPointer()), index);
				}
				return Unsafe.Add<T>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset), index);
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00022F7C File Offset: 0x0002117C
		public unsafe void Clear()
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			UIntPtr uintPtr = (UIntPtr)((ulong)length * (ulong)((long)Unsafe.SizeOf<T>()));
			if ((Unsafe.SizeOf<T>() & (sizeof(IntPtr) - 1)) != 0)
			{
				if (this._pinnable == null)
				{
					byte* ptr = (byte*)this._byteOffset.ToPointer();
					SpanHelpers.ClearLessThanPointerSized(ptr, uintPtr);
					return;
				}
				SpanHelpers.ClearLessThanPointerSized(Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset)), uintPtr);
				return;
			}
			else
			{
				if (SpanHelpers.IsReferenceOrContainsReferences<T>())
				{
					UIntPtr uintPtr2 = (UIntPtr)((ulong)((long)(length * Unsafe.SizeOf<T>() / sizeof(IntPtr))));
					SpanHelpers.ClearPointerSizedWithReferences(Unsafe.As<T, IntPtr>(this.DangerousGetPinnableReference()), uintPtr2);
					return;
				}
				SpanHelpers.ClearPointerSizedWithoutReferences(Unsafe.As<T, byte>(this.DangerousGetPinnableReference()), uintPtr);
				return;
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00023038 File Offset: 0x00021238
		public unsafe void Fill(T value)
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			if (Unsafe.SizeOf<T>() != 1)
			{
				ref T ptr = ref this.DangerousGetPinnableReference();
				int i;
				for (i = 0; i < (length & -8); i += 8)
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					*Unsafe.Add<T>(ref ptr, i + 1) = value;
					*Unsafe.Add<T>(ref ptr, i + 2) = value;
					*Unsafe.Add<T>(ref ptr, i + 3) = value;
					*Unsafe.Add<T>(ref ptr, i + 4) = value;
					*Unsafe.Add<T>(ref ptr, i + 5) = value;
					*Unsafe.Add<T>(ref ptr, i + 6) = value;
					*Unsafe.Add<T>(ref ptr, i + 7) = value;
				}
				if (i < (length & -4))
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					*Unsafe.Add<T>(ref ptr, i + 1) = value;
					*Unsafe.Add<T>(ref ptr, i + 2) = value;
					*Unsafe.Add<T>(ref ptr, i + 3) = value;
					i += 4;
				}
				while (i < length)
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					i++;
				}
				return;
			}
			byte b = *Unsafe.As<T, byte>(ref value);
			if (this._pinnable == null)
			{
				Unsafe.InitBlockUnaligned(this._byteOffset.ToPointer(), b, (uint)length);
				return;
			}
			Unsafe.InitBlockUnaligned(Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset)), b, (uint)length);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000231A7 File Offset: 0x000213A7
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000231B8 File Offset: 0x000213B8
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination._length;
			if (length == 0)
			{
				return true;
			}
			if (length > length2)
			{
				return false;
			}
			ref T ptr = ref this.DangerousGetPinnableReference();
			SpanHelpers.CopyTo<T>(destination.DangerousGetPinnableReference(), length2, ref ptr, length);
			return true;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000231F5 File Offset: 0x000213F5
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002321A File Offset: 0x0002141A
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00022C1B File Offset: 0x00020E1B
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00022C27 File Offset: 0x00020E27
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00023226 File Offset: 0x00021426
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0002322E File Offset: 0x0002142E
		public static implicit operator Span<T>(ArraySegment<T> arraySegment)
		{
			return new Span<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0002324A File Offset: 0x0002144A
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pinnable, span._byteOffset, span._length);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00023264 File Offset: 0x00021464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			int num = this._length - start;
			return new Span<T>(this._pinnable, intPtr, num);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000232A4 File Offset: 0x000214A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			return new Span<T>(this._pinnable, intPtr, length);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000232E8 File Offset: 0x000214E8
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return SpanHelpers.PerTypeValues<T>.EmptyArray;
			}
			T[] array = new T[this._length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002331C File Offset: 0x0002151C
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00023334 File Offset: 0x00021534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref T DangerousGetPinnableReference()
		{
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00023373 File Offset: 0x00021573
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0002337B File Offset: 0x0002157B
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x0400069C RID: 1692
		private readonly Pinnable<T> _pinnable;

		// Token: 0x0400069D RID: 1693
		private readonly IntPtr _byteOffset;

		// Token: 0x0400069E RID: 1694
		private readonly int _length;
	}
}
