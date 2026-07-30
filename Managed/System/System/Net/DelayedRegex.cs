using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x0200046E RID: 1134
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x0008235C File Offset: 0x0008055C
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00082379 File Offset: 0x00080579
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x00082396 File Offset: 0x00080596
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000823C8 File Offset: 0x000805C8
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x04001E58 RID: 7768
		private Regex _AsRegex;

		// Token: 0x04001E59 RID: 7769
		private string _AsString;
	}
}
