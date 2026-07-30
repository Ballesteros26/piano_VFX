using System;

namespace System.Collections.Generic
{
	// Token: 0x0200034F RID: 847
	internal sealed class BitHelper
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x000546FA File Offset: 0x000528FA
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00054717 File Offset: 0x00052917
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00054730 File Offset: 0x00052930
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

		// Token: 0x060019BD RID: 6589 RVA: 0x00054784 File Offset: 0x00052984
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

		// Token: 0x060019BE RID: 6590 RVA: 0x000547D6 File Offset: 0x000529D6
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04000B6D RID: 2925
		private const byte MarkedBitFlag = 1;

		// Token: 0x04000B6E RID: 2926
		private const byte IntSize = 32;

		// Token: 0x04000B6F RID: 2927
		private readonly int _length;

		// Token: 0x04000B70 RID: 2928
		private unsafe readonly int* _arrayPtr;

		// Token: 0x04000B71 RID: 2929
		private readonly int[] _array;

		// Token: 0x04000B72 RID: 2930
		private readonly bool _useStackAlloc;
	}
}
