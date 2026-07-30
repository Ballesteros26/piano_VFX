using System;

namespace System.Xml
{
	/// <summary>Implements a single-threaded <see cref="T:System.Xml.XmlNameTable" />.</summary>
	// Token: 0x02000248 RID: 584
	public class NameTable : XmlNameTable
	{
		/// <summary>Initializes a new instance of the NameTable class.</summary>
		// Token: 0x06001690 RID: 5776 RVA: 0x0007C2C5 File Offset: 0x0007A4C5
		public NameTable()
		{
			this.mask = 31;
			this.entries = new NameTable.Entry[this.mask + 1];
			this.hashCodeRandomizer = Environment.TickCount;
		}

		/// <summary>Atomizes the specified string and adds it to the NameTable.</summary>
		/// <returns>The atomized string or the existing string if it already exists in the NameTable.</returns>
		/// <param name="key">The string to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06001691 RID: 5777 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
		public override string Add(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int length = key.Length;
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + this.hashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(key))
				{
					return entry.str;
				}
			}
			return this.AddEntry(key, num);
		}

		/// <summary>Atomizes the specified string and adds it to the NameTable.</summary>
		/// <returns>The atomized string or the existing string if one already exists in the NameTable. If <paramref name="len" /> is zero, String.Empty is returned.</returns>
		/// <param name="key">The character array containing the string to add. </param>
		/// <param name="start">The zero-based index into the array specifying the first character of the string. </param>
		/// <param name="len">The number of characters in the string. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="start" />-or- <paramref name="start" /> &gt;= <paramref name="key" />.Length -or- <paramref name="len" /> &gt;= <paramref name="key" />.Length The above conditions do not cause an exception to be thrown if <paramref name="len" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="len" /> &lt; 0. </exception>
		// Token: 0x06001692 RID: 5778 RVA: 0x0007C3A0 File Offset: 0x0007A5A0
		public override string Add(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			int num = len + this.hashCodeRandomizer;
			num += (num << 7) ^ (int)key[start];
			int num2 = start + len;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return this.AddEntry(new string(key, start, len), num);
		}

		/// <summary>Gets the atomized string with the specified value.</summary>
		/// <returns>The atomized string object or null if the string has not already been atomized.</returns>
		/// <param name="value">The name to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		// Token: 0x06001693 RID: 5779 RVA: 0x0007C444 File Offset: 0x0007A644
		public override string Get(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				return string.Empty;
			}
			int num = value.Length + this.hashCodeRandomizer;
			for (int i = 0; i < value.Length; i++)
			{
				num += (num << 7) ^ (int)value[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(value))
				{
					return entry.str;
				}
			}
			return null;
		}

		/// <summary>Gets the atomized string containing the same characters as the specified range of characters in the given array.</summary>
		/// <returns>The atomized string or null if the string has not already been atomized. If <paramref name="len" /> is zero, String.Empty is returned.</returns>
		/// <param name="key">The character array containing the name to find. </param>
		/// <param name="start">The zero-based index into the array specifying the first character of the name. </param>
		/// <param name="len">The number of characters in the name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="start" />-or- <paramref name="start" /> &gt;= <paramref name="key" />.Length -or- <paramref name="len" /> &gt;= <paramref name="key" />.Length The above conditions do not cause an exception to be thrown if <paramref name="len" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="len" /> &lt; 0. </exception>
		// Token: 0x06001694 RID: 5780 RVA: 0x0007C4EC File Offset: 0x0007A6EC
		public override string Get(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			int num = len + this.hashCodeRandomizer;
			num += (num << 7) ^ (int)key[start];
			int num2 = start + len;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return null;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0007C584 File Offset: 0x0007A784
		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & this.mask;
			NameTable.Entry entry = new NameTable.Entry(str, hashCode, this.entries[num]);
			this.entries[num] = entry;
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return entry.str;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0007C5D8 File Offset: 0x0007A7D8
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			NameTable.Entry[] array = this.entries;
			NameTable.Entry[] array2 = new NameTable.Entry[num + 1];
			foreach (NameTable.Entry entry in array)
			{
				while (entry != null)
				{
					int num2 = entry.hashCode & num;
					NameTable.Entry next = entry.next;
					entry.next = array2[num2];
					array2[num2] = entry;
					entry = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0007C64C File Offset: 0x0007A84C
		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1[i] != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000E33 RID: 3635
		private NameTable.Entry[] entries;

		// Token: 0x04000E34 RID: 3636
		private int count;

		// Token: 0x04000E35 RID: 3637
		private int mask;

		// Token: 0x04000E36 RID: 3638
		private int hashCodeRandomizer;

		// Token: 0x02000249 RID: 585
		private class Entry
		{
			// Token: 0x06001698 RID: 5784 RVA: 0x0007C686 File Offset: 0x0007A886
			internal Entry(string str, int hashCode, NameTable.Entry next)
			{
				this.str = str;
				this.hashCode = hashCode;
				this.next = next;
			}

			// Token: 0x04000E37 RID: 3639
			internal string str;

			// Token: 0x04000E38 RID: 3640
			internal int hashCode;

			// Token: 0x04000E39 RID: 3641
			internal NameTable.Entry next;
		}
	}
}
