using System;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Web.Services.Protocols
{
	/// <summary>The .NET Framework uses the <see cref="T:System.Web.Services.Protocols.ServerType" /> class to process XML Web service requests.</summary>
	// Token: 0x02000057 RID: 87
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ServerType
	{
		/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.ServerType" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that exposes the XML Web service.</param>
		// Token: 0x060001F3 RID: 499 RVA: 0x00009320 File Offset: 0x00007520
		public ServerType(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000932F File Offset: 0x0000752F
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009337 File Offset: 0x00007537
		internal Evidence Evidence
		{
			get
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
				return this.Type.Assembly.Evidence;
			}
		}

		// Token: 0x04000238 RID: 568
		private Type type;
	}
}
