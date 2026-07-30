using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	// Token: 0x02000358 RID: 856
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ResolveNameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> class.</summary>
		/// <param name="name">The name to resolve. </param>
		// Token: 0x06001A97 RID: 6807 RVA: 0x0006B6E0 File Offset: 0x000698E0
		public ResolveNameEventArgs(string name)
		{
			this.name = name;
			this.value = null;
		}

		/// <summary>Gets the name of the object to resolve.</summary>
		/// <returns>The name of the object to resolve.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0006B6F6 File Offset: 0x000698F6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the object that matches the name.</summary>
		/// <returns>The object that the name is associated with.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0006B6FE File Offset: 0x000698FE
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0006B706 File Offset: 0x00069906
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04001843 RID: 6211
		private string name;

		// Token: 0x04001844 RID: 6212
		private object value;
	}
}
