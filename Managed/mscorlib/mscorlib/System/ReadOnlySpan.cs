using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CC RID: 204
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	public readonly ref struct ReadOnlySpan<T>
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00022A07 File Offset: 0x00020C07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00022A34 File Offset: 0x00020C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array, int start, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00022A83 File Offset: 0x00020C83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
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

		// Token: 0x06000686 RID: 1670 RVA: 0x00022AC0 File Offset: 0x00020CC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> DangerousCreate(object obj, ref T objectData, int length)
		{
			Pinnable<T> pinnable = Unsafe.As<Pinnable<T>>(obj);
			IntPtr intPtr = Unsafe.ByteOffset<T>(ref pinnable.Data, ref objectData);
			return new ReadOnlySpan<T>(pinnable, intPtr, length);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00022AE7 File Offset: 0x00020CE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00022AFE File Offset: 0x00020CFE
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{{{0}[{1}]}}", typeof(T).Name, this._length);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00022B24 File Offset: 0x00020D24
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00022B2C File Offset: 0x00020D2C
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x17000120 RID: 288
		public unsafe T this[int index]
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
					return *Unsafe.Add<T>(Unsafe.AsRef<T>(this._byteOffset.ToPointer()), index);
				}
				return *Unsafe.Add<T>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset), index);
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00022B9B File Offset: 0x00020D9B
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00022BAC File Offset: 0x00020DAC
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination.Length;
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

		// Token: 0x0600068E RID: 1678 RVA: 0x00022BEA File Offset: 0x00020DEA
		public static bool operator ==(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00022C0F File Offset: 0x00020E0F
		public static bool operator !=(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00022C1B File Offset: 0x00020E1B
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00022C27 File Offset: 0x00020E27
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00022C33 File Offset: 0x00020E33
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00022C3B File Offset: 0x00020E3B
		public static implicit operator ReadOnlySpan<T>(ArraySegment<T> arraySegment)
		{
			return new ReadOnlySpan<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00022C58 File Offset: 0x00020E58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			int num = this._length - start;
			return new ReadOnlySpan<T>(this._pinnable, intPtr, num);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00022C98 File Offset: 0x00020E98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			return new ReadOnlySpan<T>(this._pinnable, intPtr, length);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00022CDC File Offset: 0x00020EDC
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00022D10 File Offset: 0x00020F10
		public static ReadOnlySpan<T> Empty
		{
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00022D28 File Offset: 0x00020F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref T DangerousGetPinnableReference()
		{
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00022D67 File Offset: 0x00020F67
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00022D6F File Offset: 0x00020F6F
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x04000699 RID: 1689
		private readonly Pinnable<T> _pinnable;

		// Token: 0x0400069A RID: 1690
		private readonly IntPtr _byteOffset;

		// Token: 0x0400069B RID: 1691
		private readonly int _length;
	}
}
