using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200029B RID: 667
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06001ED4 RID: 7892 RVA: 0x00077B74 File Offset: 0x00075D74
		public int GetHashCode(object key)
		{
			return FastResourceComparer.HashFunction((string)key);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00077B81 File Offset: 0x00075D81
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00077B8C File Offset: 0x00075D8C
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)key[i];
			}
			return (int)num;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00077BC0 File Offset: 0x00075DC0
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.CompareOrdinal(text, text2);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00077BE6 File Offset: 0x00075DE6
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00077BEF File Offset: 0x00075DEF
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00077BF8 File Offset: 0x00075DF8
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.Equals(text, text2);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00077C20 File Offset: 0x00075E20
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | ((int)ptr2[1] << 8);
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00077CA6 File Offset: 0x00075EA6
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00077CB4 File Offset: 0x00075EB4
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				num = (int)((char)((int)(*(a++)) | ((int)(*(a++)) << 8)) - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x040010B8 RID: 4280
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
