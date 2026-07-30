using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x02000305 RID: 773
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation. </param>
		/// <param name="newDesigner">The document that is gaining activation. </param>
		// Token: 0x060018C6 RID: 6342 RVA: 0x0006933D File Offset: 0x0006753D
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.oldDesigner = oldDesigner;
			this.newDesigner = newDesigner;
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00069353 File Offset: 0x00067553
		public IDesignerHost OldDesigner
		{
			get
			{
				return this.oldDesigner;
			}
		}

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0006935B File Offset: 0x0006755B
		public IDesignerHost NewDesigner
		{
			get
			{
				return this.newDesigner;
			}
		}

		// Token: 0x0400144D RID: 5197
		private readonly IDesignerHost oldDesigner;

		// Token: 0x0400144E RID: 5198
		private readonly IDesignerHost newDesigner;
	}
}
