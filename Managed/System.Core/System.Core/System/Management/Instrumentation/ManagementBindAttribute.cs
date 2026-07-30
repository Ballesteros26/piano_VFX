using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementBind attribute indicates that a method is used to return the instance of a WMI class associated with a specific key value.</summary>
	// Token: 0x02000369 RID: 873
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementBindAttribute : ManagementNewInstanceAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementEnumerator attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method marked with the <see cref="ManagementBind" /> attribute will output.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x0000220F File Offset: 0x0000040F
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
