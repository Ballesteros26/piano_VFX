using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the context for the trust manager to consider when making the decision to run an application, and when setting up the security on a new <see cref="T:System.AppDomain" /> in which to run an application.</summary>
	// Token: 0x0200057E RID: 1406
	[ComVisible(true)]
	public class TrustManagerContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class. </summary>
		// Token: 0x06003F03 RID: 16131 RVA: 0x000E1B2A File Offset: 0x000DFD2A
		public TrustManagerContext()
			: this(TrustManagerUIContext.Run)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class using the specified <see cref="T:System.Security.Policy.TrustManagerUIContext" /> object. </summary>
		/// <param name="uiContext">One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values that specifies the type of trust manager user interface to use. </param>
		// Token: 0x06003F04 RID: 16132 RVA: 0x000E1B33 File Offset: 0x000DFD33
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this._ignorePersistedDecision = false;
			this._noPrompt = false;
			this._keepAlive = false;
			this._persist = false;
			this._ui = uiContext;
		}

		/// <summary>Gets or sets a value indicating whether the application security manager should ignore any persisted decisions and call the trust manager.</summary>
		/// <returns>true to call the trust manager; otherwise, false. </returns>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000E1B5E File Offset: 0x000DFD5E
		// (set) Token: 0x06003F06 RID: 16134 RVA: 0x000E1B66 File Offset: 0x000DFD66
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this._ignorePersistedDecision;
			}
			set
			{
				this._ignorePersistedDecision = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should cache state for this application, to facilitate future requests to determine application trust.</summary>
		/// <returns>true to cache state data; otherwise, false. The default is false.</returns>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000E1B6F File Offset: 0x000DFD6F
		// (set) Token: 0x06003F08 RID: 16136 RVA: 0x000E1B77 File Offset: 0x000DFD77
		public virtual bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				this._keepAlive = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should prompt the user for trust decisions.</summary>
		/// <returns>true to not prompt the user; false to prompt the user. The default is false.</returns>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003F09 RID: 16137 RVA: 0x000E1B80 File Offset: 0x000DFD80
		// (set) Token: 0x06003F0A RID: 16138 RVA: 0x000E1B88 File Offset: 0x000DFD88
		public virtual bool NoPrompt
		{
			get
			{
				return this._noPrompt;
			}
			set
			{
				this._noPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user's response to the consent dialog should be persisted. </summary>
		/// <returns>true to cache state data; otherwise, false. The default is true.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x000E1B91 File Offset: 0x000DFD91
		// (set) Token: 0x06003F0C RID: 16140 RVA: 0x000E1B99 File Offset: 0x000DFD99
		public virtual bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		/// <summary>Gets or sets the identity of the previous application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object representing the previous <see cref="T:System.ApplicationIdentity" />.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000E1BA2 File Offset: 0x000DFDA2
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000E1BAA File Offset: 0x000DFDAA
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this._previousId;
			}
			set
			{
				this._previousId = value;
			}
		}

		/// <summary>Gets or sets the type of user interface the trust manager should display.</summary>
		/// <returns>One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values. The default is <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />. </returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000E1BB3 File Offset: 0x000DFDB3
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x000E1BBB File Offset: 0x000DFDBB
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this._ui;
			}
			set
			{
				this._ui = value;
			}
		}

		// Token: 0x04002006 RID: 8198
		private bool _ignorePersistedDecision;

		// Token: 0x04002007 RID: 8199
		private bool _noPrompt;

		// Token: 0x04002008 RID: 8200
		private bool _keepAlive;

		// Token: 0x04002009 RID: 8201
		private bool _persist;

		// Token: 0x0400200A RID: 8202
		private ApplicationIdentity _previousId;

		// Token: 0x0400200B RID: 8203
		private TrustManagerUIContext _ui;
	}
}
