using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x0200030F RID: 783
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00069504 File Offset: 0x00067704
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0006950C File Offset: 0x0006770C
		public virtual string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00069514 File Offset: 0x00067714
		public virtual string NewName
		{
			get
			{
				return this.newName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed. </param>
		/// <param name="oldName">The old name of the component. </param>
		/// <param name="newName">The new name of the component. </param>
		// Token: 0x060018F2 RID: 6386 RVA: 0x0006951C File Offset: 0x0006771C
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.oldName = oldName;
			this.newName = newName;
			this.component = component;
		}

		// Token: 0x04001459 RID: 5209
		private object component;

		// Token: 0x0400145A RID: 5210
		private string oldName;

		// Token: 0x0400145B RID: 5211
		private string newName;
	}
}
