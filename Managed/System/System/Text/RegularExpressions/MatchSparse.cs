using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200014E RID: 334
	internal class MatchSparse : Match
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0003360E File Offset: 0x0003180E
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos)
			: base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00033627 File Offset: 0x00031827
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x04000ED6 RID: 3798
		internal new Hashtable _caps;
	}
}
