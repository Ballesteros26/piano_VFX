using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>This class is used by the WMI.NET Provider Extensions framework. It is the base class for all the management attributes that can be applied to members.</summary>
	// Token: 0x0200036B RID: 875
	[AttributeUsage(AttributeTargets.All)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementMemberAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the management attribute.</summary>
		/// <returns>Returns a string which is the name of the management attribute.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x0000220F File Offset: 0x0000040F
		public string Name
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
