using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000309 RID: 777
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00069482 File Offset: 0x00067682
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0006948A File Offset: 0x0006768A
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be null.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00069492 File Offset: 0x00067692
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be null.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0006949A File Offset: 0x0006769A
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed. </param>
		/// <param name="oldValue">The old value of the changed member. </param>
		/// <param name="newValue">The new value of the changed member. </param>
		// Token: 0x060018DD RID: 6365 RVA: 0x000694A2 File Offset: 0x000676A2
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.component = component;
			this.member = member;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		// Token: 0x04001452 RID: 5202
		private object component;

		// Token: 0x04001453 RID: 5203
		private MemberDescriptor member;

		// Token: 0x04001454 RID: 5204
		private object oldValue;

		// Token: 0x04001455 RID: 5205
		private object newValue;
	}
}
