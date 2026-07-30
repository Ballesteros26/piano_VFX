using System;
using System.Configuration;
using System.Net.Sockets;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020006B2 RID: 1714
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class. </summary>
		// Token: 0x060035B7 RID: 13751 RVA: 0x000C5914 File Offset: 0x000C3B14
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000C598D File Offset: 0x000C3B8D
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x000C599F File Offset: 0x000C3B9F
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForAcceptProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForAcceptProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000C59B2 File Offset: 0x000C3BB2
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000C59C4 File Offset: 0x000C3BC4
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForConnectProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForConnectProp] = value;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060035BC RID: 13756 RVA: 0x000C59D7 File Offset: 0x000C3BD7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets a value that specifies the default <see cref="T:System.Net.Sockets.IPProtectionLevel" /> to use for a socket.</summary>
		/// <returns>The value of the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> for the current instance.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000C59E0 File Offset: 0x000C3BE0
		// (set) Token: 0x060035BF RID: 13759 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return (IPProtectionLevel)0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002AA0 RID: 10912
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04002AA1 RID: 10913
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x04002AA2 RID: 10914
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
