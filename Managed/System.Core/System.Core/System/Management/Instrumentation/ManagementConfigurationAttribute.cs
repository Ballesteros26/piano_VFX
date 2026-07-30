using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementConfiguration attribute indicates that a property or field represents a read-write WMI property.</summary>
	// Token: 0x0200036D RID: 877
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementConfigurationAttribute : ManagementMemberAttribute
	{
		/// <summary>Gets or sets the mode of the property, which specifies whether changes to it are applied as soon as possible or when a commit method is called.</summary>
		/// <returns>Returns a <see cref="T:System.Management.Instrumentation.ManagementConfigurationType" /> that indicates whether the WMI property uses <see cref="F:System.Management.Instrumentation.ManagementConfigurationType.Apply" /> or <see cref="F:System.Management.Instrumentation.ManagementConfigurationType.OnCommit" /> mode.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00056260 File Offset: 0x00054460
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x0000220F File Offset: 0x0000040F
		public ManagementConfigurationType Mode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ManagementConfigurationType.Apply;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that defines the type of output that the property that is marked with the ManagementConfiguration attribute will return.</summary>
		/// <returns>A <see cref="T:System.Type" /> value representing the type of output that the property marked with the ManagementConfiguration attribute will return.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A65 RID: 6757 RVA: 0x0000220F File Offset: 0x0000040F
		public Type Schema
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
