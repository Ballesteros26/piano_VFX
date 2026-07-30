using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CE RID: 206
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00023383 File Offset: 0x00021583
		public SpanDebugView(Span<T> collection)
		{
			this._pinnable = (T[])collection.Pinnable;
			this._byteOffset = collection.ByteOffset;
			this._length = collection.Length;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000233B7 File Offset: 0x000215B7
		public SpanDebugView(ReadOnlySpan<T> collection)
		{
			this._pinnable = (T[])collection.Pinnable;
			this._byteOffset = collection.ByteOffset;
			this._length = collection.Length;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000233EC File Offset: 0x000215EC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public unsafe T[] Items
		{
			get
			{
				int num = (typeof(T).GetTypeInfo().IsValueType ? Unsafe.SizeOf<T>() : IntPtr.Size);
				T[] array = new T[this._length];
				if (this._pinnable == null)
				{
					byte* ptr = (byte*)this._byteOffset.ToPointer();
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Unsafe.Read<T>((void*)ptr);
						ptr += num;
					}
				}
				else
				{
					long num2 = this._byteOffset.ToInt64();
					long num3 = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.ToInt64();
					int num4 = (int)((num2 - num3) / (long)num);
					Array.Copy(this._pinnable, num4, array, 0, this._length);
				}
				return array;
			}
		}

		// Token: 0x0400069F RID: 1695
		private readonly T[] _pinnable;

		// Token: 0x040006A0 RID: 1696
		private readonly IntPtr _byteOffset;

		// Token: 0x040006A1 RID: 1697
		private readonly int _length;
	}
}
