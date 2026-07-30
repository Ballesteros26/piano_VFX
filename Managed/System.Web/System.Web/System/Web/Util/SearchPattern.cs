using System;

namespace System.Web.Util
{
	// Token: 0x02000145 RID: 325
	internal class SearchPattern
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x0002A1C8 File Offset: 0x000283C8
		public SearchPattern(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002A1D2 File Offset: 0x000283D2
		public SearchPattern(string pattern, bool ignore)
		{
			this.SetPattern(pattern, ignore);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002A1E2 File Offset: 0x000283E2
		public void SetPattern(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.Compile(pattern);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002A1F2 File Offset: 0x000283F2
		public bool IsMatch(string text)
		{
			return this.Match(this.ops, text, 0);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002A204 File Offset: 0x00028404
		private void Compile(string pattern)
		{
			if (pattern == null)
			{
				throw new ArgumentException("Invalid search pattern.");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.True);
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyChar);
						i++;
					}
					else
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
				}
				else
				{
					op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyString);
					i++;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.End);
				return;
			}
			op.Next = new SearchPattern.Op(SearchPattern.OpCode.End);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002A304 File Offset: 0x00028504
		private bool Match(SearchPattern.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern.OpCode.End:
					return ptr == text.Length;
				case SearchPattern.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x0400120F RID: 4623
		private SearchPattern.Op ops;

		// Token: 0x04001210 RID: 4624
		private bool ignore;

		// Token: 0x04001211 RID: 4625
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x02000146 RID: 326
		private class Op
		{
			// Token: 0x06000ED5 RID: 3797 RVA: 0x0002A3EB File Offset: 0x000285EB
			public Op(SearchPattern.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04001212 RID: 4626
			public SearchPattern.OpCode Code;

			// Token: 0x04001213 RID: 4627
			public string Argument;

			// Token: 0x04001214 RID: 4628
			public SearchPattern.Op Next;
		}

		// Token: 0x02000147 RID: 327
		private enum OpCode
		{
			// Token: 0x04001216 RID: 4630
			ExactString,
			// Token: 0x04001217 RID: 4631
			AnyChar,
			// Token: 0x04001218 RID: 4632
			AnyString,
			// Token: 0x04001219 RID: 4633
			End,
			// Token: 0x0400121A RID: 4634
			True
		}
	}
}
