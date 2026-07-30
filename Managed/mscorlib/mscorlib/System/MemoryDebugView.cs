using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x020000C9 RID: 201
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x000224FC File Offset: 0x000206FC
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00022510 File Offset: 0x00020710
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00022520 File Offset: 0x00020720
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				ArraySegment<T> arraySegment;
				if (this._memory.DangerousTryGetArray(out arraySegment))
				{
					T[] array = new T[this._memory.Length];
					Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, array.Length);
					return array;
				}
				return SpanHelpers.PerTypeValues<T>.EmptyArray;
			}
		}

		// Token: 0x04000692 RID: 1682
		private readonly ReadOnlyMemory<T> _memory;
	}
}
