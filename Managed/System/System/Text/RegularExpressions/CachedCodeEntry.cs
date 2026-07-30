using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000135 RID: 309
	internal sealed class CachedCodeEntry
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x000296C4 File Offset: 0x000278C4
		internal CachedCodeEntry(string key, Hashtable capnames, string[] capslist, RegexCode code, Hashtable caps, int capsize, ExclusiveReference runner, SharedReference repl)
		{
			this._key = key;
			this._capnames = capnames;
			this._capslist = capslist;
			this._code = code;
			this._caps = caps;
			this._capsize = capsize;
			this._runnerref = runner;
			this._replref = repl;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00029714 File Offset: 0x00027914
		internal void AddCompiled(RegexRunnerFactory factory)
		{
			this._factory = factory;
			this._code = null;
		}

		// Token: 0x04000DBE RID: 3518
		internal string _key;

		// Token: 0x04000DBF RID: 3519
		internal RegexCode _code;

		// Token: 0x04000DC0 RID: 3520
		internal Hashtable _caps;

		// Token: 0x04000DC1 RID: 3521
		internal Hashtable _capnames;

		// Token: 0x04000DC2 RID: 3522
		internal string[] _capslist;

		// Token: 0x04000DC3 RID: 3523
		internal int _capsize;

		// Token: 0x04000DC4 RID: 3524
		internal RegexRunnerFactory _factory;

		// Token: 0x04000DC5 RID: 3525
		internal ExclusiveReference _runnerref;

		// Token: 0x04000DC6 RID: 3526
		internal SharedReference _replref;
	}
}
