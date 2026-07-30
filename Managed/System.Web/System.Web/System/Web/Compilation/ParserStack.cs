using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x02000622 RID: 1570
	internal class ParserStack
	{
		// Token: 0x0600434C RID: 17228 RVA: 0x000B3BD7 File Offset: 0x000B1DD7
		public ParserStack()
		{
			this.files = new Hashtable();
			this.parsers = new Stack();
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000B3BF8 File Offset: 0x000B1DF8
		public bool Push(AspParser parser)
		{
			if (this.files.Contains(parser.Filename))
			{
				return false;
			}
			this.files[parser.Filename] = true;
			this.parsers.Push(parser);
			this.current = parser;
			return true;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000B3C48 File Offset: 0x000B1E48
		public AspParser Pop()
		{
			if (this.parsers.Count == 0)
			{
				return null;
			}
			this.files.Remove(this.current.Filename);
			AspParser aspParser = (AspParser)this.parsers.Pop();
			if (this.parsers.Count > 0)
			{
				this.current = (AspParser)this.parsers.Peek();
				return aspParser;
			}
			this.current = null;
			return aspParser;
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x000B3CB6 File Offset: 0x000B1EB6
		public int Count
		{
			get
			{
				return this.parsers.Count;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000B3CC3 File Offset: 0x000B1EC3
		public AspParser Parser
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x000B3CCB File Offset: 0x000B1ECB
		public string Filename
		{
			get
			{
				return this.current.Filename;
			}
		}

		// Token: 0x040023FE RID: 9214
		private Hashtable files;

		// Token: 0x040023FF RID: 9215
		private Stack parsers;

		// Token: 0x04002400 RID: 9216
		private AspParser current;
	}
}
