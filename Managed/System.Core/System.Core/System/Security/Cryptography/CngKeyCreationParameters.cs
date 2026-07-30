using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Contains advanced properties for key creation.</summary>
	// Token: 0x02000065 RID: 101
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngKeyCreationParameters
	{
		/// <summary>Gets or sets the key export policy.</summary>
		/// <returns>An object that specifies a key export policy. The default value is null, which indicates that the key storage provider's default export policy is set.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005A33 File Offset: 0x00003C33
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00005A3B File Offset: 0x00003C3B
		public CngExportPolicies? ExportPolicy
		{
			get
			{
				return this.m_exportPolicy;
			}
			set
			{
				this.m_exportPolicy = value;
			}
		}

		/// <summary>Gets or sets the key creation options.</summary>
		/// <returns>An object that specifies options for creating keys. The default value is null, which indicates that the key storage provider's default key creation options are set.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005A44 File Offset: 0x00003C44
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00005A4C File Offset: 0x00003C4C
		public CngKeyCreationOptions KeyCreationOptions
		{
			get
			{
				return this.m_keyCreationOptions;
			}
			set
			{
				this.m_keyCreationOptions = value;
			}
		}

		/// <summary>Gets or sets the cryptographic operations that apply to the current key. </summary>
		/// <returns>A bitwise combination of one or more enumeration values that specify key usage. The default value is null, which indicates that the key storage provider's default key usage is set.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00005A55 File Offset: 0x00003C55
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00005A5D File Offset: 0x00003C5D
		public CngKeyUsages? KeyUsage
		{
			get
			{
				return this.m_keyUsage;
			}
			set
			{
				this.m_keyUsage = value;
			}
		}

		/// <summary>Gets or sets the window handle that should be used as the parent window for dialog boxes that are created by Cryptography Next Generation (CNG) classes.</summary>
		/// <returns>The HWND of the parent window that is used for CNG dialog boxes.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00005A66 File Offset: 0x00003C66
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00005A6E File Offset: 0x00003C6E
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			set
			{
				this.m_parentWindowHandle = value;
			}
		}

		/// <summary>Enables a <see cref="T:System.Security.Cryptography.CngKey" /> object to be created with additional properties that are set before the key is finalized.</summary>
		/// <returns>A collection object that contains any additional parameters that you must set on a <see cref="T:System.Security.Cryptography.CngKey" /> object during key creation.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00005A77 File Offset: 0x00003C77
		public CngPropertyCollection Parameters
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005A77 File Offset: 0x00003C77
		internal CngPropertyCollection ParametersNoDemand
		{
			get
			{
				return this.m_parameters;
			}
		}

		/// <summary>Gets or sets the key storage provider (KSP) to create a key in.</summary>
		/// <returns>An object that specifies the KSP that a new key will be created in.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.CngKeyCreationParameters.Provider" /> property is set to a null value.</exception>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00005A7F File Offset: 0x00003C7F
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00005A87 File Offset: 0x00003C87
		public CngProvider Provider
		{
			get
			{
				return this.m_provider;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_provider = value;
			}
		}

		/// <summary>Gets or sets information about the user interface to display when a key is created or accessed.</summary>
		/// <returns>An object that contains details about the user interface shown by Cryptography Next Generation (CNG) classes when a key is created or accessed. A null value indicates that the key storage provider's default user interface policy is set.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00005AA4 File Offset: 0x00003CA4
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00005AAC File Offset: 0x00003CAC
		public CngUIPolicy UIPolicy
		{
			get
			{
				return this.m_uiPolicy;
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeSubWindows)]
			set
			{
				this.m_uiPolicy = value;
			}
		}

		// Token: 0x040002A8 RID: 680
		private CngExportPolicies? m_exportPolicy;

		// Token: 0x040002A9 RID: 681
		private CngKeyCreationOptions m_keyCreationOptions;

		// Token: 0x040002AA RID: 682
		private CngKeyUsages? m_keyUsage;

		// Token: 0x040002AB RID: 683
		private CngPropertyCollection m_parameters = new CngPropertyCollection();

		// Token: 0x040002AC RID: 684
		private IntPtr m_parentWindowHandle;

		// Token: 0x040002AD RID: 685
		private CngProvider m_provider = CngProvider.MicrosoftSoftwareKeyStorageProvider;

		// Token: 0x040002AE RID: 686
		private CngUIPolicy m_uiPolicy;
	}
}
