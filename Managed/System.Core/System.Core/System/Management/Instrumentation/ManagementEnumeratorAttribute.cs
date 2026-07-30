using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementEnumerator attribute marks a method that returns all the instances of a WMI class.</summary>
	// Token: 0x02000371 RID: 881
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEnumeratorAttribute : ManagementNewInstanceAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the method that is marked with the ManagementEnumerator attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the method marked with the <see cref="ManagementEnumerator" /> attribute will output.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x0000220F File Offset: 0x0000040F
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
