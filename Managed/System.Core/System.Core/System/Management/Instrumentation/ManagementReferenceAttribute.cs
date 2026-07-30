using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementReferenceAttribute marks a class member, property or method parameter as a reference to another management object or class.</summary>
	// Token: 0x02000376 RID: 886
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementReferenceAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the referenced type.</summary>
		/// <returns>A string containing the name of the referenced type.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x0000220F File Offset: 0x0000040F
		public string Type
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
