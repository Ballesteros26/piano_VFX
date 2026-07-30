using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x02000564 RID: 1380
	internal class DelayedRegex
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x0009F083 File Offset: 0x0009D283
		internal DelayedRegex(string s)
		{
			this._regex = null;
			this._regstring = s;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x0009F099 File Offset: 0x0009D299
		internal Match Match(string s)
		{
			this.EnsureRegex();
			return this._regex.Match(s);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x0009F0AD File Offset: 0x0009D2AD
		internal int GroupNumberFromName(string name)
		{
			this.EnsureRegex();
			return this._regex.GroupNumberFromName(name);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
		internal void EnsureRegex()
		{
			string regstring = this._regstring;
			if (this._regex == null)
			{
				this._regex = new Regex(regstring);
				this._regstring = null;
			}
		}

		// Token: 0x04002016 RID: 8214
		private string _regstring;

		// Token: 0x04002017 RID: 8215
		private Regex _regex;
	}
}
