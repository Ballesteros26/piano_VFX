using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x0200030B RID: 779
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000694C7 File Offset: 0x000676C7
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or null otherwise.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x000694CF File Offset: 0x000676CF
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed. </param>
		// Token: 0x060018E4 RID: 6372 RVA: 0x000694D7 File Offset: 0x000676D7
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.component = component;
			this.member = member;
		}

		// Token: 0x04001456 RID: 5206
		private object component;

		// Token: 0x04001457 RID: 5207
		private MemberDescriptor member;
	}
}
