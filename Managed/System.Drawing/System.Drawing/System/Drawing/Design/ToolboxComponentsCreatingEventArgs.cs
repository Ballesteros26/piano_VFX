using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x02000126 RID: 294
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> class.</summary>
		/// <param name="host">The designer host that is making the request. </param>
		// Token: 0x06000D7F RID: 3455 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public ToolboxComponentsCreatingEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components, or null if no designer host was provided to the toolbox item.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0001D9C7 File Offset: 0x0001BBC7
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04000A85 RID: 2693
		private readonly IDesignerHost host;
	}
}
