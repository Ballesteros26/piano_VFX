using System;

namespace System.Drawing
{
	/// <summary>Specifies a range of character positions within a string.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000042 RID: 66
	public struct CharacterRange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.CharacterRange" /> structure, specifying a range of character positions within a string.</summary>
		/// <param name="First">The position of the first character in the range. For example, if <paramref name="First" /> is set to 0, the first position of the range is position 0 in the string. </param>
		/// <param name="Length">The number of positions in the range. </param>
		// Token: 0x060001F3 RID: 499 RVA: 0x00005998 File Offset: 0x00003B98
		public CharacterRange(int First, int Length)
		{
			this.first = First;
			this.length = Length;
		}

		/// <summary>Gets or sets the position in the string of the first character of this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The first position of this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000059A8 File Offset: 0x00003BA8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000059B0 File Offset: 0x00003BB0
		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		/// <summary>Gets or sets the number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000059B9 File Offset: 0x00003BB9
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000059C1 File Offset: 0x00003BC1
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
		/// <returns>true to indicate the specified object is an instance with the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> value as this instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to for equality.</param>
		// Token: 0x060001F8 RID: 504 RVA: 0x000059CC File Offset: 0x00003BCC
		public override bool Equals(object obj)
		{
			if (!(obj is CharacterRange))
			{
				return false;
			}
			CharacterRange characterRange = (CharacterRange)obj;
			return this == characterRange;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000059F6 File Offset: 0x00003BF6
		public override int GetHashCode()
		{
			return this.first ^ this.length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are equal.</summary>
		/// <returns>true to indicate the two <see cref="T:System.Drawing.CharacterRange" /> objects have the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values; otherwise, false. </returns>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		// Token: 0x060001FA RID: 506 RVA: 0x00005A05 File Offset: 0x00003C05
		public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first == cr2.first && cr1.length == cr2.length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are not equal.</summary>
		/// <returns>true to indicate the either the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects differ; otherwise, false. </returns>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		// Token: 0x060001FB RID: 507 RVA: 0x00005A25 File Offset: 0x00003C25
		public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first != cr2.first || cr1.length != cr2.length;
		}

		// Token: 0x0400034F RID: 847
		private int first;

		// Token: 0x04000350 RID: 848
		private int length;
	}
}
