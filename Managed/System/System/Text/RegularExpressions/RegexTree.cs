using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000158 RID: 344
	internal sealed class RegexTree
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x00037C3E File Offset: 0x00035E3E
		internal RegexTree(RegexNode root, Hashtable caps, int[] capnumlist, int captop, Hashtable capnames, string[] capslist, RegexOptions opts)
		{
			this._root = root;
			this._caps = caps;
			this._capnumlist = capnumlist;
			this._capnames = capnames;
			this._capslist = capslist;
			this._captop = captop;
			this._options = opts;
		}

		// Token: 0x04000F52 RID: 3922
		internal RegexNode _root;

		// Token: 0x04000F53 RID: 3923
		internal Hashtable _caps;

		// Token: 0x04000F54 RID: 3924
		internal int[] _capnumlist;

		// Token: 0x04000F55 RID: 3925
		internal Hashtable _capnames;

		// Token: 0x04000F56 RID: 3926
		internal string[] _capslist;

		// Token: 0x04000F57 RID: 3927
		internal RegexOptions _options;

		// Token: 0x04000F58 RID: 3928
		internal int _captop;
	}
}
