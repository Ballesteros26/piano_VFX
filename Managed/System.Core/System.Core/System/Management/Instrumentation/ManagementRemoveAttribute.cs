using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementRemoveAttribute is used to indicate that a method cleans up an instance of a managed entity.</summary>
	// Token: 0x02000377 RID: 887
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementRemoveAttribute : ManagementMemberAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the object that is marked with the ManagementRemove attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the object marked with the Remove attribute will output.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0000220F File Offset: 0x0000040F
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
