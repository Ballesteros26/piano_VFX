using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000C8 RID: 200
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	public readonly struct Memory<T>
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00021F98 File Offset: 0x00020198
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException_ArrayTypeMustBeExactMatch(typeof(T));
			}
			this._arrayOrOwnedMemory = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00021FFC File Offset: 0x000201FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array, int start, int length)
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
			this._arrayOrOwnedMemory = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00022072 File Offset: 0x00020272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(OwnedMemory<T> owner, int index, int length)
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000220A8 File Offset: 0x000202A8
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{{{0}[{1}]}}", typeof(T).Name, this._length);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000220CE File Offset: 0x000202CE
		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000220D6 File Offset: 0x000202D6
		public static implicit operator Memory<T>(ArraySegment<T> arraySegment)
		{
			return new Memory<T>(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000220F4 File Offset: 0x000202F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			if (memory._index < 0)
			{
				return new ReadOnlyMemory<T>((OwnedMemory<T>)memory._arrayOrOwnedMemory, memory._index & int.MaxValue, memory._length);
			}
			return new ReadOnlyMemory<T>((T[])memory._arrayOrOwnedMemory, memory._index, memory._length);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00022149 File Offset: 0x00020349
		public static Memory<T> Empty { get; } = SpanHelpers.PerTypeValues<T>.EmptyArray;

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00022150 File Offset: 0x00020350
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00022158 File Offset: 0x00020358
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00022164 File Offset: 0x00020364
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (this._index < 0)
			{
				return new Memory<T>((OwnedMemory<T>)this._arrayOrOwnedMemory, (this._index & int.MaxValue) + start, this._length - start);
			}
			return new Memory<T>((T[])this._arrayOrOwnedMemory, this._index + start, this._length - start);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000221D4 File Offset: 0x000203D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (this._index < 0)
			{
				return new Memory<T>((OwnedMemory<T>)this._arrayOrOwnedMemory, (this._index & int.MaxValue) + start, length);
			}
			return new Memory<T>((T[])this._arrayOrOwnedMemory, this._index + start, length);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00022240 File Offset: 0x00020440
		public Span<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((OwnedMemory<T>)this._arrayOrOwnedMemory).Span.Slice(this._index & int.MaxValue, this._length);
				}
				return new Span<T>((T[])this._arrayOrOwnedMemory, this._index, this._length);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000222A0 File Offset: 0x000204A0
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

		// Token: 0x06000662 RID: 1634 RVA: 0x0002236C File Offset: 0x0002056C
		public bool TryGetArray(out ArraySegment<T> arraySegment)
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

		// Token: 0x06000663 RID: 1635 RVA: 0x000223F0 File Offset: 0x000205F0
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0002240C File Offset: 0x0002060C
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			bool flag = obj is Memory<T>;
			Memory<T> memory = (flag ? ((Memory<T>)obj) : default(Memory<T>));
			return flag && this.Equals(memory);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00022466 File Offset: 0x00020666
		public bool Equals(Memory<T> other)
		{
			return this._arrayOrOwnedMemory == other._arrayOrOwnedMemory && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00022494 File Offset: 0x00020694
		public override int GetHashCode()
		{
			return Memory<T>.CombineHashCodes(this._arrayOrOwnedMemory.GetHashCode(), (this._index & int.MaxValue).GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000224D3 File Offset: 0x000206D3
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000224DC File Offset: 0x000206DC
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0400068D RID: 1677
		private readonly object _arrayOrOwnedMemory;

		// Token: 0x0400068E RID: 1678
		private readonly int _index;

		// Token: 0x0400068F RID: 1679
		private readonly int _length;

		// Token: 0x04000690 RID: 1680
		private const int RemoveOwnedFlagBitMask = 2147483647;
	}
}
