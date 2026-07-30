using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x0200030D RID: 781
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000694ED File Offset: 0x000676ED
		public virtual IComponent Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event. </param>
		// Token: 0x060018EA RID: 6378 RVA: 0x000694F5 File Offset: 0x000676F5
		public ComponentEventArgs(IComponent component)
		{
			this.component = component;
		}

		// Token: 0x04001458 RID: 5208
		private IComponent component;
	}
}
