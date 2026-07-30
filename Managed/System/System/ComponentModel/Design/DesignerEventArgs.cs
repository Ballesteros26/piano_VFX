using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x0200031F RID: 799
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document. </param>
		// Token: 0x06001973 RID: 6515 RVA: 0x0006A2FA File Offset: 0x000684FA
		public DesignerEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0006A309 File Offset: 0x00068509
		public IDesignerHost Designer
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04001471 RID: 5233
		private readonly IDesignerHost host;
	}
}
