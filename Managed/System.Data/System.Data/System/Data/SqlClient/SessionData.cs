using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x020001C7 RID: 455
	internal class SessionData
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0006A568 File Offset: 0x00068768
		public SessionData(SessionData recoveryData)
		{
			this._initialDatabase = recoveryData._initialDatabase;
			this._initialCollation = recoveryData._initialCollation;
			this._initialLanguage = recoveryData._initialLanguage;
			this._resolvedAliases = recoveryData._resolvedAliases;
			for (int i = 0; i < 256; i++)
			{
				if (recoveryData._initialState[i] != null)
				{
					this._initialState[i] = (byte[])recoveryData._initialState[i].Clone();
				}
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006A5FF File Offset: 0x000687FF
		public SessionData()
		{
			this._resolvedAliases = new Dictionary<string, Tuple<string, string>>(2);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0006A633 File Offset: 0x00068833
		public void Reset()
		{
			this._database = null;
			this._collation = null;
			this._language = null;
			if (this._deltaDirty)
			{
				this._delta = new SessionStateRecord[256];
				this._deltaDirty = false;
			}
			this._unrecoverableStatesCount = 0;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0006A670 File Offset: 0x00068870
		[Conditional("DEBUG")]
		public void AssertUnrecoverableStateCountIsCorrect()
		{
			byte b = 0;
			foreach (SessionStateRecord sessionStateRecord in this._delta)
			{
				if (sessionStateRecord != null && !sessionStateRecord._recoverable)
				{
					b += 1;
				}
			}
		}

		// Token: 0x04000E41 RID: 3649
		internal const int _maxNumberOfSessionStates = 256;

		// Token: 0x04000E42 RID: 3650
		internal uint _tdsVersion;

		// Token: 0x04000E43 RID: 3651
		internal bool _encrypted;

		// Token: 0x04000E44 RID: 3652
		internal string _database;

		// Token: 0x04000E45 RID: 3653
		internal SqlCollation _collation;

		// Token: 0x04000E46 RID: 3654
		internal string _language;

		// Token: 0x04000E47 RID: 3655
		internal string _initialDatabase;

		// Token: 0x04000E48 RID: 3656
		internal SqlCollation _initialCollation;

		// Token: 0x04000E49 RID: 3657
		internal string _initialLanguage;

		// Token: 0x04000E4A RID: 3658
		internal byte _unrecoverableStatesCount;

		// Token: 0x04000E4B RID: 3659
		internal Dictionary<string, Tuple<string, string>> _resolvedAliases;

		// Token: 0x04000E4C RID: 3660
		internal SessionStateRecord[] _delta = new SessionStateRecord[256];

		// Token: 0x04000E4D RID: 3661
		internal bool _deltaDirty;

		// Token: 0x04000E4E RID: 3662
		internal byte[][] _initialState = new byte[256][];
	}
}
