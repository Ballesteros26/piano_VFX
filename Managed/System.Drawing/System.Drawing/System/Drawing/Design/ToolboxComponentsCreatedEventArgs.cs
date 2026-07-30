using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x02000124 RID: 292
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> class.</summary>
		/// <param name="components">The components to include in the toolbox. </param>
		// Token: 0x06000D79 RID: 3449 RVA: 0x0001D997 File Offset: 0x0001BB97
		public ToolboxComponentsCreatedEventArgs(IComponent[] components)
		{
			this.comps = components;
		}

		/// <summary>Gets or sets an array containing the components to add to the toolbox.</summary>
		/// <returns>An array of type <see cref="T:System.ComponentModel.IComponent" /> indicating the components to add to the toolbox.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		public IComponent[] Components
		{
			get
			{
				return (IComponent[])this.comps.Clone();
			}
		}

		// Token: 0x04000A84 RID: 2692
		private readonly IComponent[] comps;
	}
}
