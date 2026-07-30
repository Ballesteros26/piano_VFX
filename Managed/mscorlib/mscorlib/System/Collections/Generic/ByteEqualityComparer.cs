using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000A47 RID: 2631
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x060060B7 RID: 24759 RVA: 0x0013E74A File Offset: 0x0013C94A
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x0013E750 File Offset: 0x0013C950
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x0013E75C File Offset: 0x0013C95C
		[SecuritySafeCritical]
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count == 0)
			{
				return -1;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Buffer.IndexOfByte(ptr, value, startIndex, count);
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x0013E7EC File Offset: 0x0013C9EC
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x0013E815 File Offset: 0x0013CA15
		public override bool Equals(object obj)
		{
			return obj is ByteEqualityComparer;
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
