using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Globalization
{
	/// <summary>Represents the result of mapping a string to its sort key.</summary>
	// Token: 0x0200043B RID: 1083
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SortKey
	{
		/// <summary>Compares two sort keys.</summary>
		/// <returns>A signed integer that indicates the relationship between <paramref name="sortkey1" /> and <paramref name="sortkey2" />.Value Condition Less than zero <paramref name="sortkey1" /> is less than <paramref name="sortkey2" />. Zero <paramref name="sortkey1" /> is equal to <paramref name="sortkey2" />. Greater than zero <paramref name="sortkey1" /> is greater than <paramref name="sortkey2" />. </returns>
		/// <param name="sortkey1">The first sort key to compare. </param>
		/// <param name="sortkey2">The second sort key to compare. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sortkey1" /> or <paramref name="sortkey2" /> is null.</exception>
		// Token: 0x060033CA RID: 13258 RVA: 0x000BB050 File Offset: 0x000B9250
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			if (sortkey1 == sortkey2 || sortkey1.OriginalString == sortkey2.OriginalString)
			{
				return 0;
			}
			byte[] keyData = sortkey1.KeyData;
			byte[] keyData2 = sortkey2.KeyData;
			int num = ((keyData.Length > keyData2.Length) ? keyData2.Length : keyData.Length);
			int i = 0;
			while (i < num)
			{
				if (keyData[i] != keyData2[i])
				{
					if (keyData[i] >= keyData2[i])
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			if (keyData.Length == keyData2.Length)
			{
				return 0;
			}
			if (keyData.Length >= keyData2.Length)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000BB0E1 File Offset: 0x000B92E1
		internal SortKey(int lcid, string source, CompareOptions opt)
		{
			this.lcid = lcid;
			this.source = source;
			this.options = opt;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000BB0FE File Offset: 0x000B92FE
		internal SortKey(int lcid, string source, byte[] buffer, CompareOptions opt, int lv1Length, int lv2Length, int lv3Length, int kanaSmallLength, int markTypeLength, int katakanaLength, int kanaWidthLength, int identLength)
		{
			this.lcid = lcid;
			this.source = source;
			this.key = buffer;
			this.options = opt;
		}

		/// <summary>Gets the original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>The original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000BB123 File Offset: 0x000B9323
		public virtual string OriginalString
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000BB12B File Offset: 0x000B932B
		public virtual byte[] KeyData
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is equal to the current <see cref="T:System.Globalization.SortKey" /> object; otherwise, false. </returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.SortKey" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060033CF RID: 13263 RVA: 0x000BB134 File Offset: 0x000B9334
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && this.lcid == sortKey.lcid && this.options == sortKey.options && SortKey.Compare(this, sortKey) == 0;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.SortKey" /> object that is suitable for hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060033D0 RID: 13264 RVA: 0x000BB174 File Offset: 0x000B9374
		public override int GetHashCode()
		{
			if (this.key.Length == 0)
			{
				return 0;
			}
			int num = (int)this.key[0];
			for (int i = 1; i < this.key.Length; i++)
			{
				num ^= (int)this.key[i] << (i & 3);
			}
			return num;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060033D1 RID: 13265 RVA: 0x000BB1BC File Offset: 0x000B93BC
		public override string ToString()
		{
			return string.Concat(new object[] { "SortKey - ", this.lcid, ", ", this.options, ", ", this.source });
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal SortKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B94 RID: 7060
		private readonly string source;

		// Token: 0x04001B95 RID: 7061
		private readonly byte[] key;

		// Token: 0x04001B96 RID: 7062
		private readonly CompareOptions options;

		// Token: 0x04001B97 RID: 7063
		private readonly int lcid;
	}
}
