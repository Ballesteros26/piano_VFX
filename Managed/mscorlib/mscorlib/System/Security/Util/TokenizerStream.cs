using System;

namespace System.Security.Util
{
	// Token: 0x0200061D RID: 1565
	internal sealed class TokenizerStream
	{
		// Token: 0x0600441F RID: 17439 RVA: 0x000EF9A2 File Offset: 0x000EDBA2
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000EF9D0 File Offset: 0x000EDBD0
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000EFA48 File Offset: 0x000EDC48
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000EFAB0 File Offset: 0x000EDCB0
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000EFB43 File Offset: 0x000EDD43
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000EFB54 File Offset: 0x000EDD54
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000EFBAB File Offset: 0x000EDDAB
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000EFBB4 File Offset: 0x000EDDB4
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000EFC32 File Offset: 0x000EDE32
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x000EFC3C File Offset: 0x000EDE3C
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x0400225D RID: 8797
		private int m_countTokens;

		// Token: 0x0400225E RID: 8798
		private TokenizerShortBlock m_headTokens;

		// Token: 0x0400225F RID: 8799
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04002260 RID: 8800
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x04002261 RID: 8801
		private int m_indexTokens;

		// Token: 0x04002262 RID: 8802
		private TokenizerStringBlock m_headStrings;

		// Token: 0x04002263 RID: 8803
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04002264 RID: 8804
		private int m_indexStrings;
	}
}
