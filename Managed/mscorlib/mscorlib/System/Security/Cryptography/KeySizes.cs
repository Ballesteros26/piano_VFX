using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Determines the set of valid key sizes for the symmetric cryptographic algorithms.</summary>
	// Token: 0x0200064D RID: 1613
	[ComVisible(true)]
	public sealed class KeySizes
	{
		/// <summary>Specifies the minimum key size in bits.</summary>
		/// <returns>The minimum key size in bits.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x000F51C0 File Offset: 0x000F33C0
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		/// <summary>Specifies the maximum key size in bits.</summary>
		/// <returns>The maximum key size in bits.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x000F51C8 File Offset: 0x000F33C8
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		/// <summary>Specifies the interval between valid key sizes in bits.</summary>
		/// <returns>The interval between valid key sizes in bits.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000F51D0 File Offset: 0x000F33D0
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.KeySizes" /> class with the specified key values.</summary>
		/// <param name="minSize">The minimum valid key size. </param>
		/// <param name="maxSize">The maximum valid key size. </param>
		/// <param name="skipSize">The interval between valid key sizes. </param>
		// Token: 0x060045D2 RID: 17874 RVA: 0x000F51D8 File Offset: 0x000F33D8
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000F51F8 File Offset: 0x000F33F8
		internal bool IsLegal(int keySize)
		{
			int num = keySize - this.MinSize;
			bool flag = num >= 0 && keySize <= this.MaxSize;
			if (this.SkipSize != 0)
			{
				return flag && num % this.SkipSize == 0;
			}
			return flag;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000F523C File Offset: 0x000F343C
		internal static bool IsLegalKeySize(KeySizes[] legalKeys, int size)
		{
			for (int i = 0; i < legalKeys.Length; i++)
			{
				if (legalKeys[i].IsLegal(size))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040023E2 RID: 9186
		private int m_minSize;

		// Token: 0x040023E3 RID: 9187
		private int m_maxSize;

		// Token: 0x040023E4 RID: 9188
		private int m_skipSize;
	}
}
