using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates optional configuration parameters for the user interface (UI) that Cryptography Next Generation (CNG) displays when you access a protected key.</summary>
	// Token: 0x02000069 RID: 105
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngUIPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.</param>
		// Token: 0x06000263 RID: 611 RVA: 0x00005DC6 File Offset: 0x00003FC6
		public CngUIPolicy(CngUIProtectionLevels protectionLevel)
			: this(protectionLevel, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level and friendly name.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		// Token: 0x06000264 RID: 612 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName)
			: this(protectionLevel, friendlyName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, and description.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		// Token: 0x06000265 RID: 613 RVA: 0x00005DDB File Offset: 0x00003FDB
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description)
			: this(protectionLevel, friendlyName, description, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, description string, and use context.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		/// <param name="useContext">A description of how the key will be used. Specify a null string to use the default description.</param>
		// Token: 0x06000266 RID: 614 RVA: 0x00005DE7 File Offset: 0x00003FE7
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext)
			: this(protectionLevel, friendlyName, description, useContext, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, description string, use context, and title.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		/// <param name="useContext">A description of how the key will be used. Specify a null string to use the default description.</param>
		/// <param name="creationTitle">The title for the dialog box that provides the UI prompt. Specify a null string to use the default title.</param>
		// Token: 0x06000267 RID: 615 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext, string creationTitle)
		{
			this.m_creationTitle = creationTitle;
			this.m_description = description;
			this.m_friendlyName = friendlyName;
			this.m_protectionLevel = protectionLevel;
			this.m_useContext = useContext;
		}

		/// <summary>Gets the title that is displayed by the UI prompt.</summary>
		/// <returns>The title of the dialog box that appears when the key is accessed.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00005E22 File Offset: 0x00004022
		public string CreationTitle
		{
			get
			{
				return this.m_creationTitle;
			}
		}

		/// <summary>Gets the description string that is displayed by the UI prompt.</summary>
		/// <returns>The description text for the dialog box that appears when the key is accessed.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00005E2A File Offset: 0x0000402A
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		/// <summary>Gets the friendly name that is displayed by the UI prompt.</summary>
		/// <returns>The friendly name that is used to describe the key in the dialog box that appears when the key is accessed.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00005E32 File Offset: 0x00004032
		public string FriendlyName
		{
			get
			{
				return this.m_friendlyName;
			}
		}

		/// <summary>Gets the UI protection level for the key.</summary>
		/// <returns>An object that describes the level of UI protection to apply to the key.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00005E3A File Offset: 0x0000403A
		public CngUIProtectionLevels ProtectionLevel
		{
			get
			{
				return this.m_protectionLevel;
			}
		}

		/// <summary>Gets the description of how the key will be used.</summary>
		/// <returns>The description of how the key will be used.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00005E42 File Offset: 0x00004042
		public string UseContext
		{
			get
			{
				return this.m_useContext;
			}
		}

		// Token: 0x040002B6 RID: 694
		private string m_creationTitle;

		// Token: 0x040002B7 RID: 695
		private string m_description;

		// Token: 0x040002B8 RID: 696
		private string m_friendlyName;

		// Token: 0x040002B9 RID: 697
		private CngUIProtectionLevels m_protectionLevel;

		// Token: 0x040002BA RID: 698
		private string m_useContext;
	}
}
