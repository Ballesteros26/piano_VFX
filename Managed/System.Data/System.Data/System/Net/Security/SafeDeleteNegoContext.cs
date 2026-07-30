using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200004A RID: 74
	internal sealed class SafeDeleteNegoContext : SafeDeleteContext
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000EA7F File Offset: 0x0000CC7F
		public SafeGssNameHandle TargetName
		{
			get
			{
				return this._targetName;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000EA87 File Offset: 0x0000CC87
		public bool IsNtlmUsed
		{
			get
			{
				return this._isNtlmUsed;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		public SafeGssContextHandle GssContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000EA98 File Offset: 0x0000CC98
		public SafeDeleteNegoContext(SafeFreeNegoCredentials credential, string targetName)
			: base(credential)
		{
			try
			{
				this._targetName = SafeGssNameHandle.CreatePrincipal(targetName);
			}
			catch
			{
				base.Dispose();
				throw;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		public void SetGssContext(SafeGssContextHandle context)
		{
			this._context = context;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EADD File Offset: 0x0000CCDD
		public void SetAuthenticationPackage(bool isNtlmUsed)
		{
			this._isNtlmUsed = isNtlmUsed;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EAE6 File Offset: 0x0000CCE6
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._context != null)
				{
					this._context.Dispose();
					this._context = null;
				}
				if (this._targetName != null)
				{
					this._targetName.Dispose();
					this._targetName = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x040004D1 RID: 1233
		private SafeGssNameHandle _targetName;

		// Token: 0x040004D2 RID: 1234
		private SafeGssContextHandle _context;

		// Token: 0x040004D3 RID: 1235
		private bool _isNtlmUsed;
	}
}
