using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CB RID: 203
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct ReadOnlyMemory<T>
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00022572 File Offset: 0x00020772
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._arrayOrOwnedMemory = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00022594 File Offset: 0x00020794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array, int start, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._arrayOrOwnedMemory = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000225C9 File Offset: 0x000207C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlyMemory(OwnedMemory<T> owner, int index, int length)
		{
			if (owner == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.ownedMemory);
			}
			if (index < 0 || length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._arrayOrOwnedMemory = owner;
			this._index = index | int.MinValue;
			this._length = length;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000225FF File Offset: 0x000207FF
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{{{0}[{1}]}}", typeof(T).Name, this._length);
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00022625 File Offset: 0x00020825
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002262D File Offset: 0x0002082D
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> arraySegment)
		{
			return new ReadOnlyMemory<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00022649 File Offset: 0x00020849
		public static ReadOnlyMemory<T> Empty { get; } = SpanHelpers.PerTypeValues<T>.EmptyArray;

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00022650 File Offset: 0x00020850
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00022658 File Offset: 0x00020858
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00022664 File Offset: 0x00020864
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (this._index < 0)
			{
				return new ReadOnlyMemory<T>((OwnedMemory<T>)this._arrayOrOwnedMemory, (this._index & int.MaxValue) + start, this._length - start);
			}
			return new ReadOnlyMemory<T>((T[])this._arrayOrOwnedMemory, this._index + start, this._length - start);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000226D4 File Offset: 0x000208D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (this._index < 0)
			{
				return new ReadOnlyMemory<T>((OwnedMemory<T>)this._arrayOrOwnedMemory, (this._index & int.MaxValue) + start, length);
			}
			return new ReadOnlyMemory<T>((T[])this._arrayOrOwnedMemory, this._index + start, length);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00022740 File Offset: 0x00020940
		public ReadOnlySpan<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((OwnedMemory<T>)this._arrayOrOwnedMemory).Span.Slice(this._index & int.MaxValue, this._length);
				}
				return new ReadOnlySpan<T>((T[])this._arrayOrOwnedMemory, this._index, this._length);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000227A4 File Offset: 0x000209A4
		public unsafe MemoryHandle Retain(bool pin = false)
		{
			MemoryHandle memoryHandle;
			if (pin)
			{
				if (this._index < 0)
				{
					memoryHandle = ((OwnedMemory<T>)this._arrayOrOwnedMemory).Pin();
					memoryHandle.AddOffset((this._index & int.MaxValue) * Unsafe.SizeOf<T>());
				}
				else
				{
					GCHandle gchandle = GCHandle.Alloc((T[])this._arrayOrOwnedMemory, GCHandleType.Pinned);
					void* ptr = Unsafe.Add<T>((void*)gchandle.AddrOfPinnedObject(), this._index);
					memoryHandle = new MemoryHandle(null, ptr, gchandle);
				}
			}
			else if (this._index < 0)
			{
				((OwnedMemory<T>)this._arrayOrOwnedMemory).Retain();
				memoryHandle = new MemoryHandle((OwnedMemory<T>)this._arrayOrOwnedMemory, null, default(GCHandle));
			}
			else
			{
				memoryHandle = new MemoryHandle(null, null, default(GCHandle));
			}
			return memoryHandle;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022870 File Offset: 0x00020A70
		public bool DangerousTryGetArray(out ArraySegment<T> arraySegment)
		{
			if (this._index >= 0)
			{
				arraySegment = new ArraySegment<T>((T[])this._arrayOrOwnedMemory, this._index, this._length);
				return true;
			}
			ArraySegment<T> arraySegment2;
			if (((OwnedMemory<T>)this._arrayOrOwnedMemory).TryGetArray(out arraySegment2))
			{
				arraySegment = new ArraySegment<T>(arraySegment2.Array, arraySegment2.Offset + (this._index & int.MaxValue), this._length);
				return true;
			}
			arraySegment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000228F4 File Offset: 0x00020AF4
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022910 File Offset: 0x00020B10
		public override bool Equals(object obj)
		{
			bool flag = obj is ReadOnlyMemory<T>;
			ReadOnlyMemory<T> readOnlyMemory = (flag ? ((ReadOnlyMemory<T>)obj) : default(ReadOnlyMemory<T>));
			if (flag)
			{
				return this.Equals(readOnlyMemory);
			}
			bool flag2 = obj is Memory<T>;
			Memory<T> memory = (flag2 ? ((Memory<T>)obj) : default(Memory<T>));
			return flag2 && this.Equals(memory);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00022978 File Offset: 0x00020B78
		public bool Equals(ReadOnlyMemory<T> other)
		{
			return this._arrayOrOwnedMemory == other._arrayOrOwnedMemory && this._index == other._index && this._length == other._length;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000229A8 File Offset: 0x00020BA8
		public override int GetHashCode()
		{
			return ReadOnlyMemory<T>.CombineHashCodes(this._arrayOrOwnedMemory.GetHashCode(), (this._index & int.MaxValue).GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000224D3 File Offset: 0x000206D3
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000229E7 File Offset: 0x00020BE7
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return ReadOnlyMemory<T>.CombineHashCodes(ReadOnlyMemory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x04000694 RID: 1684
		private readonly object _arrayOrOwnedMemory;

		// Token: 0x04000695 RID: 1685
		private readonly int _index;

		// Token: 0x04000696 RID: 1686
		private readonly int _length;

		// Token: 0x04000697 RID: 1687
		private const int RemoveOwnedFlagBitMask = 2147483647;
	}
}
