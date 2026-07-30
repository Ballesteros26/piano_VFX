using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System.Globalization
{
	/// <summary>Enumerates the text elements of a string. </summary>
	// Token: 0x02000427 RID: 1063
	[ComVisible(true)]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06003300 RID: 13056 RVA: 0x000B6835 File Offset: 0x000B4A35
		internal TextElementEnumerator(string str, int startIndex, int strLen)
		{
			this.str = str;
			this.startIndex = startIndex;
			this.strLen = strLen;
			this.Reset();
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000B6858 File Offset: 0x000B4A58
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.charLen = -1;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000B6864 File Offset: 0x000B4A64
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.strLen = this.endIndex + 1;
			this.currTextElementLen = this.nextTextElementLen;
			if (this.charLen == -1)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000B68B1 File Offset: 0x000B4AB1
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.endIndex = this.strLen - 1;
			this.nextTextElementLen = this.currTextElementLen;
		}

		/// <summary>Advances the enumerator to the next text element of the string.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next text element; false if the enumerator has passed the end of the string.</returns>
		// Token: 0x06003304 RID: 13060 RVA: 0x000B68D0 File Offset: 0x000B4AD0
		public bool MoveNext()
		{
			if (this.index >= this.strLen)
			{
				this.index = this.strLen + 1;
				return false;
			}
			this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
			this.index += this.currTextElementLen;
			return true;
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>An object containing the current text element in the string.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000B6938 File Offset: 0x000B4B38
		public object Current
		{
			get
			{
				return this.GetTextElement();
			}
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>A new string containing the current text element in the string being read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x06003306 RID: 13062 RVA: 0x000B6940 File Offset: 0x000B4B40
		public string GetTextElement()
		{
			if (this.index == this.startIndex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
			}
			if (this.index > this.strLen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
			}
			return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
		}

		/// <summary>Gets the index of the text element that the enumerator is currently positioned over.</summary>
		/// <returns>The index of the text element that the enumerator is currently positioned over.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000B69A7 File Offset: 0x000B4BA7
		public int ElementIndex
		{
			get
			{
				if (this.index == this.startIndex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
				}
				return this.index - this.currTextElementLen;
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first text element in the string.</summary>
		// Token: 0x06003308 RID: 13064 RVA: 0x000B69D4 File Offset: 0x000B4BD4
		public void Reset()
		{
			this.index = this.startIndex;
			if (this.index < this.strLen)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal TextElementEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B02 RID: 6914
		private string str;

		// Token: 0x04001B03 RID: 6915
		private int index;

		// Token: 0x04001B04 RID: 6916
		private int startIndex;

		// Token: 0x04001B05 RID: 6917
		[NonSerialized]
		private int strLen;

		// Token: 0x04001B06 RID: 6918
		[NonSerialized]
		private int currTextElementLen;

		// Token: 0x04001B07 RID: 6919
		[OptionalField(VersionAdded = 2)]
		private UnicodeCategory uc;

		// Token: 0x04001B08 RID: 6920
		[OptionalField(VersionAdded = 2)]
		private int charLen;

		// Token: 0x04001B09 RID: 6921
		private int endIndex;

		// Token: 0x04001B0A RID: 6922
		private int nextTextElementLen;
	}
}
