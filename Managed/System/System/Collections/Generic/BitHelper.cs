using System;

namespace System.Collections.Generic
{
	// Token: 0x02000718 RID: 1816
	internal sealed class BitHelper
	{
		// Token: 0x0600393E RID: 14654 RVA: 0x000D14A3 File Offset: 0x000CF6A3
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000D14C0 File Offset: 0x000CF6C0
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000D14D8 File Offset: 0x000CF6D8
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._length && num >= 0)
			{
				int num2 = 1 << bitPosition % 32;
				if (this._useStackAlloc)
				{
					this._arrayPtr[num] |= num2;
					return;
				}
				this._array[num] |= num2;
			}
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000D152C File Offset: 0x000CF72C
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num >= this._length || num < 0)
			{
				return false;
			}
			int num2 = 1 << bitPosition % 32;
			if (this._useStackAlloc)
			{
				return (this._arrayPtr[num] & num2) != 0;
			}
			return (this._array[num] & num2) != 0;
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000D157E File Offset: 0x000CF77E
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04002C9B RID: 11419
		private const byte MarkedBitFlag = 1;

		// Token: 0x04002C9C RID: 11420
		private const byte IntSize = 32;

		// Token: 0x04002C9D RID: 11421
		private readonly int _length;

		// Token: 0x04002C9E RID: 11422
		private unsafe readonly int* _arrayPtr;

		// Token: 0x04002C9F RID: 11423
		private readonly int[] _array;

		// Token: 0x04002CA0 RID: 11424
		private readonly bool _useStackAlloc;
	}
}
