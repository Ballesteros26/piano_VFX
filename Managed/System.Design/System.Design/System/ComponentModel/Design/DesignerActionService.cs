using System;

namespace System.ComponentModel.Design
{
	/// <summary>Establishes a design-time service that manages the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects for components.</summary>
	// Token: 0x02000117 RID: 279
	public class DesignerActionService : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> class.</summary>
		/// <param name="serviceProvider">The service provider for the current design-time environment.</param>
		// Token: 0x0600081E RID: 2078 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DesignerActionService(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> is removed or added for any component.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600081F RID: 2079 RVA: 0x0000D90C File Offset: 0x0000BB0C
		// (remove) Token: 0x06000820 RID: 2080 RVA: 0x0000D944 File Offset: 0x0000BB44
		public event DesignerActionListsChangedEventHandler DesignerActionListsChanged;

		/// <summary>Adds a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to the current collection of managed smart tags.</summary>
		/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to associate the smart tags with.</param>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the new smart tag items to be added.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null. </exception>
		// Token: 0x06000821 RID: 2081 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Add(IComponent comp, DesignerActionList actionList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to the current collection of managed smart tags.</summary>
		/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to associate the smart tags with.</param>
		/// <param name="designerActionListCollection">The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the new smart tag items to be added.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null. </exception>
		// Token: 0x06000822 RID: 2082 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Add(IComponent comp, DesignerActionListCollection designerActionListCollection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all components from management and clears all push-model smart tag lists.</summary>
		// Token: 0x06000823 RID: 2083 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the current smart tag service manages the action lists for the specified component.</summary>
		/// <returns>true if the component is managed by the current service; otherwise, false.</returns>
		/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comp" /> is null.</exception>
		// Token: 0x06000824 RID: 2084 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool Contains(IComponent comp)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> class.</summary>
		// Token: 0x06000825 RID: 2085 RVA: 0x0000D979 File Offset: 0x0000BB79
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000826 RID: 2086 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Returns the collection of smart tag item lists associated with a component.</summary>
		/// <returns>The collection of smart tags for the specified component.</returns>
		/// <param name="component">The component that the smart tags are associated with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comp" /> is null.</exception>
		// Token: 0x06000827 RID: 2087 RVA: 0x0000D982 File Offset: 0x0000BB82
		[MonoTODO]
		public DesignerActionListCollection GetComponentActions(IComponent component)
		{
			return this.GetComponentActions(component, ComponentActionsType.All);
		}

		/// <summary>Returns the collection of smart tag item lists of the specified type associated with a component.</summary>
		/// <returns>The collection of smart tags of the specified type for the specified component.</returns>
		/// <param name="component">The component that the smart tags are associated with.</param>
		/// <param name="type">The <see cref="T:System.ComponentModel.Design.ComponentActionsType" /> to filter the associated smart tags with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comp" /> is null.</exception>
		// Token: 0x06000828 RID: 2088 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual DesignerActionListCollection GetComponentActions(IComponent component, ComponentActionsType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the pull-model smart tags associated with a component.</summary>
		/// <param name="component">The component that the smart tags are associated with.</param>
		/// <param name="actionLists">The collection to add the associated smart tags to.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000829 RID: 2089 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void GetComponentDesignerActions(IComponent component, DesignerActionListCollection actionLists)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the push-model smart tags associated with a component.</summary>
		/// <param name="component">The component that the smart tags are associated with.</param>
		/// <param name="actionLists">The collection to add the associated smart tags to.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600082A RID: 2090 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void GetComponentServiceActions(IComponent component, DesignerActionListCollection actionLists)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified smart tag list from all components managed by the current service.</summary>
		/// <param name="actionList">The list of smart tags to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="actionList" /> is null.</exception>
		// Token: 0x0600082B RID: 2091 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Remove(DesignerActionList actionList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all the smart tag lists associated with the specified component.</summary>
		/// <param name="comp">The component to disassociate the smart tags from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comp" /> is null.</exception>
		// Token: 0x0600082C RID: 2092 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Remove(IComponent comp)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified smart tag list from the specified component.</summary>
		/// <param name="comp">The component to disassociate the smart tags from.</param>
		/// <param name="actionList">The smart tag list to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600082D RID: 2093 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Remove(IComponent comp, DesignerActionList actionList)
		{
			throw new NotImplementedException();
		}
	}
}
