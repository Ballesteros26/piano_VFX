using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x02000324 RID: 804
	[ComVisible(true)]
	public interface IComponentChangeService
	{
		/// <summary>Occurs when a component has been added.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001981 RID: 6529
		// (remove) Token: 0x06001982 RID: 6530
		event ComponentEventHandler ComponentAdded;

		/// <summary>Occurs when a component is in the process of being added.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001983 RID: 6531
		// (remove) Token: 0x06001984 RID: 6532
		event ComponentEventHandler ComponentAdding;

		/// <summary>Occurs when a component has been changed.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001985 RID: 6533
		// (remove) Token: 0x06001986 RID: 6534
		event ComponentChangedEventHandler ComponentChanged;

		/// <summary>Occurs when a component is in the process of being changed.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001987 RID: 6535
		// (remove) Token: 0x06001988 RID: 6536
		event ComponentChangingEventHandler ComponentChanging;

		/// <summary>Occurs when a component has been removed.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001989 RID: 6537
		// (remove) Token: 0x0600198A RID: 6538
		event ComponentEventHandler ComponentRemoved;

		/// <summary>Occurs when a component is in the process of being removed.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600198B RID: 6539
		// (remove) Token: 0x0600198C RID: 6540
		event ComponentEventHandler ComponentRemoving;

		/// <summary>Occurs when a component is renamed.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600198D RID: 6541
		// (remove) Token: 0x0600198E RID: 6542
		event ComponentRenameEventHandler ComponentRename;

		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed. </param>
		/// <param name="member">The member that has changed. This is null if this change is not related to a single member. </param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not null. </param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not null. </param>
		// Token: 0x0600198F RID: 6543
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change. </param>
		/// <param name="member">The member that is changing. This is null if this change is not related to a single member. </param>
		// Token: 0x06001990 RID: 6544
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
