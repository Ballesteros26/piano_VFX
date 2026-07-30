using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture. </summary>
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class Capture
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00029D50 File Offset: 0x00027F50
		internal Capture(string text, int i, int l)
		{
			this._text = text;
			this._index = i;
			this._length = l;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00029D6D File Offset: 0x00027F6D
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00029D75 File Offset: 0x00027F75
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00029D7D File Offset: 0x00027F7D
		public string Value
		{
			get
			{
				return this._text.Substring(this._index, this._length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property. </summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x060008B4 RID: 2228 RVA: 0x00029D96 File Offset: 0x00027F96
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00029D9E File Offset: 0x00027F9E
		internal string GetOriginalString()
		{
			return this._text;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00029DA6 File Offset: 0x00027FA6
		internal string GetLeftSubstring()
		{
			return this._text.Substring(0, this._index);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00029DBA File Offset: 0x00027FBA
		internal string GetRightSubstring()
		{
			return this._text.Substring(this._index + this._length, this._text.Length - this._index - this._length);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal Capture()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000DD6 RID: 3542
		internal string _text;

		// Token: 0x04000DD7 RID: 3543
		internal int _index;

		// Token: 0x04000DD8 RID: 3544
		internal int _length;
	}
}
