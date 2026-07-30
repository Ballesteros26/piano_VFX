using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The WmiConfiguration attribute indicates that an assembly contains code that implements a WMI provider by using the WMI.NET Provider Extensions model. The attribute accepts parameters that establish the high-level configuration of the implemented WMI provider. </summary>
	// Token: 0x02000379 RID: 889
	[AttributeUsage(AttributeTargets.Assembly)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class WmiConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.WmiConfigurationAttribute" /> class that specifies the WMI namespace in which the WMI provider will expose classes.</summary>
		/// <param name="scope">The WMI namespace in which the provider will expose classes. For example, "root\MyProviderNamespace".</param>
		// Token: 0x06001A80 RID: 6784 RVA: 0x00003C4C File Offset: 0x00001E4C
		public WmiConfigurationAttribute(string scope)
		{
		}

		/// <summary>Gets or sets the hosting group for the WMI provider.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the hosting group for the WMI provider.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0000220F File Offset: 0x0000040F
		public string HostingGroup
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the hosting model for the WMI provider.</summary>
		/// <returns>A <see cref="T:System.Management.Instrumentation.ManagementHostingModel" /> value that indicates the hosting model of the WMI provider.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000562B4 File Offset: 0x000544B4
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0000220F File Offset: 0x0000040F
		public ManagementHostingModel HostingModel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ManagementHostingModel.Decoupled;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the WMI provider can impersonate its callers. If the value is false, the provider cannot impersonate, and if the value is true, the provider can impersonate.</summary>
		/// <returns>A Boolean value that indicates whether a provider can or cannot impersonate its callers. If the value is false, the provider cannot impersonate, and if the value is true, the provider can impersonate.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000562D0 File Offset: 0x000544D0
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0000220F File Offset: 0x0000040F
		public bool IdentifyLevel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a Security Descriptor Definition Language (SDDL) string that specifies the security descriptor on the namespace in which the provider exposes management objects.</summary>
		/// <returns>An SDDL string that represents the security descriptor on the namespace in which the provider exposes management objects.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0000220F File Offset: 0x0000040F
		public string NamespaceSecurity
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the WMI namespace in which the WMI provider exposes classes.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the namespace in which the WMI provider exposes classes.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Scope
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a security descriptor for the WMI provider. For more information, see the SecurityDescriptor property information in the "__Win32Provider" topic in the MSDN online library at http://www.msdn.com. </summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the security descriptor for the WMI provider.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0000220F File Offset: 0x0000040F
		public string SecurityRestriction
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
