using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementName attribute is used to override names exposed through a WMI class.</summary>
	// Token: 0x02000374 RID: 884
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementNameAttribute" /> class that specifies a value for the <see cref="P:System.Management.ManagementNameAttribute.Name" /> property of the class.</summary>
		/// <param name="name">The user-friendly name for the object.</param>
		// Token: 0x06001A72 RID: 6770 RVA: 0x00003C4C File Offset: 0x00001E4C
		public ManagementNameAttribute(string name)
		{
		}

		/// <summary>Gets or sets the user-friendly name for an object. The object can be a method parameter or properties marked with the ManagementProbe, ManagementKey, or ManagementConfiguration attributes.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the user friendly name for an object.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
