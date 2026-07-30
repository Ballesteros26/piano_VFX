using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementEntity attribute indicates that a class provides management information exposed through a WMI provider.</summary>
	// Token: 0x02000370 RID: 880
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEntityAttribute : Attribute
	{
		/// <summary>Gets or sets a value that specifies whether the class represents a WMI class in a provider implemented external to the current assembly.</summary>
		/// <returns>A boolean value that is true if the class represents an external WMI class and false otherwise.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0005627C File Offset: 0x0005447C
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x0000220F File Offset: 0x0000040F
		public bool External
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

		/// <summary>Gets or sets the name of the WMI class.</summary>
		/// <returns>A string that contains the name of the WMI class.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0000220F File Offset: 0x0000040F
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

		/// <summary>Specifies whether the associated class represents a singleton WMI class.</summary>
		/// <returns>A boolean value that is true if the class represents a singleton WMI class and false otherwise.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00056298 File Offset: 0x00054498
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0000220F File Offset: 0x0000040F
		public bool Singleton
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
	}
}
