using System;
using System.Globalization;
using System.Security;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to a decoder when it cannot decode an input byte sequence. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026D RID: 621
	public abstract class DecoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input byte sequence.</summary>
		/// <returns>true if the fallback buffer can process <paramref name="bytesUnknown" />; false if the fallback buffer ignores <paramref name="bytesUnknown" />.</returns>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in <paramref name="bytesUnknown" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C6F RID: 7279
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C70 RID: 7280
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, causes the next call to the <see cref="M:System.Text.DecoderFallbackBuffer.GetNextChar" /> method to access the data buffer character position that is prior to the current character position. </summary>
		/// <returns>true if the <see cref="M:System.Text.DecoderFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C71 RID: 7281
		public abstract bool MovePrevious();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.DecoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001C72 RID: 7282
		public abstract int Remaining { get; }

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C73 RID: 7283 RVA: 0x0006B0F0 File Offset: 0x000692F0
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006B0FA File Offset: 0x000692FA
		[SecurityCritical]
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0006B10A File Offset: 0x0006930A
		[SecurityCritical]
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0006B11C File Offset: 0x0006931C
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0006B1BC File Offset: 0x000693BC
		[SecurityCritical]
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points."));
			}
			return num;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0006B24C File Offset: 0x0006944C
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]));
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(Environment.GetResourceString("Recursive fallback not allowed for bytes {0}.", new object[] { stringBuilder.ToString() }), "bytesUnknown");
		}

		// Token: 0x04000FCD RID: 4045
		[SecurityCritical]
		internal unsafe byte* byteStart;

		// Token: 0x04000FCE RID: 4046
		[SecurityCritical]
		internal unsafe char* charEnd;
	}
}
