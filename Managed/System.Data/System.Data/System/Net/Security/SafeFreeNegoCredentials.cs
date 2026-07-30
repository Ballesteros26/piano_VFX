using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200004D RID: 77
	internal sealed class SafeFreeNegoCredentials : SafeFreeCredentials
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000EBA5 File Offset: 0x0000CDA5
		public SafeGssCredHandle GssCredential
		{
			get
			{
				return this._credential;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public bool IsNtlmOnly
		{
			get
			{
				return this._isNtlmOnly;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000EBB5 File Offset: 0x0000CDB5
		public string UserName
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000EBBD File Offset: 0x0000CDBD
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public SafeFreeNegoCredentials(bool isNtlmOnly, string username, string password, string domain)
			: base(IntPtr.Zero, true)
		{
			int num = username.IndexOf('\\');
			if (num > 0 && username.IndexOf('\\', num + 1) < 0 && string.IsNullOrEmpty(domain))
			{
				domain = username.Substring(0, num);
				username = username.Substring(num + 1);
			}
			if (domain != null)
			{
				domain = domain.Trim();
			}
			username = username.Trim();
			if (username.IndexOf('@') < 0 && !string.IsNullOrEmpty(domain))
			{
				username = username + "@" + domain;
			}
			bool flag = false;
			this._isNtlmOnly = isNtlmOnly;
			this._userName = username;
			this._isDefault = string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password);
			this._credential = SafeGssCredHandle.Create(username, password, isNtlmOnly);
			this._credential.DangerousAddRef(ref flag);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public override bool IsInvalid
		{
			get
			{
				return this._credential == null;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000EC9F File Offset: 0x0000CE9F
		protected override bool ReleaseHandle()
		{
			this._credential.DangerousRelease();
			this._credential = null;
			return true;
		}

		// Token: 0x040004D5 RID: 1237
		private SafeGssCredHandle _credential;

		// Token: 0x040004D6 RID: 1238
		private readonly bool _isNtlmOnly;

		// Token: 0x040004D7 RID: 1239
		private readonly string _userName;

		// Token: 0x040004D8 RID: 1240
		private readonly bool _isDefault;
	}
}
