using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementTask attribute indicates that the target method implements a WMI method.</summary>
	// Token: 0x02000378 RID: 888
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementTaskAttribute : ManagementMemberAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementTask attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method that is marked with the ManagementTask attribute will output.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x0000220F File Offset: 0x0000040F
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
