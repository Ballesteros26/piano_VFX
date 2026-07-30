using System;

namespace System.IO
{
	// Token: 0x020003E8 RID: 1000
	internal class SearchPattern2
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x00079096 File Offset: 0x00077296
		public SearchPattern2(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000790A0 File Offset: 0x000772A0
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000790C0 File Offset: 0x000772C0
		public bool IsMatch(string text, bool ignorecase)
		{
			if (!this.hasWildcard && string.Compare(this.pattern, text, ignorecase) == 0)
			{
				return true;
			}
			string fileName = Path.GetFileName(text);
			if (!this.hasWildcard)
			{
				return string.Compare(this.pattern, fileName, ignorecase) == 0;
			}
			return this.Match(this.ops, fileName, 0);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00079117 File Offset: 0x00077317
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00079126 File Offset: 0x00077326
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00079130 File Offset: 0x00077330
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
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
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
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
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
				return;
			}
			op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0007925C File Offset: 0x0007745C
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
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
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04001AD3 RID: 6867
		private SearchPattern2.Op ops;

		// Token: 0x04001AD4 RID: 6868
		private bool ignore;

		// Token: 0x04001AD5 RID: 6869
		private bool hasWildcard;

		// Token: 0x04001AD6 RID: 6870
		private string pattern;

		// Token: 0x04001AD7 RID: 6871
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x04001AD8 RID: 6872
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x020003E9 RID: 1001
		private class Op
		{
			// Token: 0x06001E66 RID: 7782 RVA: 0x0007935E File Offset: 0x0007755E
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04001AD9 RID: 6873
			public SearchPattern2.OpCode Code;

			// Token: 0x04001ADA RID: 6874
			public string Argument;

			// Token: 0x04001ADB RID: 6875
			public SearchPattern2.Op Next;
		}

		// Token: 0x020003EA RID: 1002
		private enum OpCode
		{
			// Token: 0x04001ADD RID: 6877
			ExactString,
			// Token: 0x04001ADE RID: 6878
			AnyChar,
			// Token: 0x04001ADF RID: 6879
			AnyString,
			// Token: 0x04001AE0 RID: 6880
			End,
			// Token: 0x04001AE1 RID: 6881
			True
		}
	}
}
