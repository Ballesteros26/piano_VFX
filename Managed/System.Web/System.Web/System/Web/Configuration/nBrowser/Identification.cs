using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FC RID: 1532
	internal class Identification
	{
		// Token: 0x0600426B RID: 17003 RVA: 0x000AE0B4 File Offset: 0x000AC2B4
		public Identification(bool matchType, string matchGroup, string matchName, string matchPattern)
		{
			this.MatchType = matchType;
			this.MatchGroup = matchGroup;
			this.MatchName = matchName;
			this.MatchPattern = matchPattern;
			this.RegexPattern = new Regex(matchPattern);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000AE119 File Offset: 0x000AC319
		public Match GetMatch(string Header)
		{
			return this.RegexPattern.Match((Header == null) ? string.Empty : Header);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000AE131 File Offset: 0x000AC331
		public bool IsMatchSuccessful(Match m)
		{
			return this.MatchType == m.Success;
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x000AE141 File Offset: 0x000AC341
		public string Name
		{
			get
			{
				return this.MatchName;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000AE149 File Offset: 0x000AC349
		public string Group
		{
			get
			{
				return this.MatchGroup;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000AE151 File Offset: 0x000AC351
		public string Pattern
		{
			get
			{
				return this.MatchPattern;
			}
		}

		// Token: 0x04002387 RID: 9095
		private bool MatchType = true;

		// Token: 0x04002388 RID: 9096
		private string MatchName = string.Empty;

		// Token: 0x04002389 RID: 9097
		private string MatchGroup = string.Empty;

		// Token: 0x0400238A RID: 9098
		private string MatchPattern = string.Empty;

		// Token: 0x0400238B RID: 9099
		private Regex RegexPattern;
	}
}
