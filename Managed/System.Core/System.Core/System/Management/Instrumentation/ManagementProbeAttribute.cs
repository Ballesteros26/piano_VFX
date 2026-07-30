using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementProbe attribute indicates that a property or field represents a read-only WMI property.</summary>
	// Token: 0x02000375 RID: 885
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementProbeAttribute : ManagementMemberAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the property that is marked with the ManagementProbe attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the property that is marked with the ManagementProbe attribute will output.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x0000220F File Offset: 0x0000040F
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
