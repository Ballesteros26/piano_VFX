using System;
using System.Security;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to an encoder when it cannot encode an input character. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000278 RID: 632
	public abstract class EncoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input character. </summary>
		/// <returns>true if the fallback buffer can process <paramref name="charUnknown" />; false if the fallback buffer ignores <paramref name="charUnknown" />.</returns>
		/// <param name="charUnknown">An input character.</param>
		/// <param name="index">The index position of the character in the input buffer.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CCF RID: 7375
		public abstract bool Fallback(char charUnknown, int index);

		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified surrogate pair.</summary>
		/// <returns>true if the fallback buffer can process <paramref name="charUnknownHigh" /> and <paramref name="charUnknownLow" />; false if the fallback buffer ignores the surrogate pair.</returns>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CD0 RID: 7376
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CD1 RID: 7377
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, causes the next call to the <see cref="M:System.Text.EncoderFallbackBuffer.GetNextChar" /> method to access the data buffer character position that is prior to the current character position. </summary>
		/// <returns>true if the <see cref="M:System.Text.EncoderFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CD2 RID: 7378
		public abstract bool MovePrevious();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.EncoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001CD3 RID: 7379
		public abstract int Remaining { get; }

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x0006C438 File Offset: 0x0006A638
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0006C442 File Offset: 0x0006A642
		[SecurityCritical]
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006C460 File Offset: 0x0006A660
		[SecurityCritical]
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0006C494 File Offset: 0x0006A694
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = nextChar > '\0';
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0006C4C0 File Offset: 0x0006A6C0
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int num = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder.charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num2 = this.iRecursionCount;
							this.iRecursionCount = num2 + 1;
							if (num2 > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, num);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num2 = this.iRecursionCount;
				this.iRecursionCount = num2 + 1;
				if (num2 > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, num);
			return this.bFallingBack;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006C5BE File Offset: 0x0006A7BE
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(Environment.GetResourceString("Recursive fallback not allowed for character \\\\u{0:X4}.", new object[] { charRecursive }), "chars");
		}

		// Token: 0x04000FE8 RID: 4072
		[SecurityCritical]
		internal unsafe char* charStart;

		// Token: 0x04000FE9 RID: 4073
		[SecurityCritical]
		internal unsafe char* charEnd;

		// Token: 0x04000FEA RID: 4074
		internal EncoderNLS encoder;

		// Token: 0x04000FEB RID: 4075
		internal bool setEncoder;

		// Token: 0x04000FEC RID: 4076
		internal bool bUsedEncoder;

		// Token: 0x04000FED RID: 4077
		internal bool bFallingBack;

		// Token: 0x04000FEE RID: 4078
		internal int iRecursionCount;

		// Token: 0x04000FEF RID: 4079
		private const int iMaxRecursion = 250;
	}
}
