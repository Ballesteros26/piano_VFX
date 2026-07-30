using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Provides data for the <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event.</summary>
	// Token: 0x02000705 RID: 1797
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" /> change.</summary>
		/// <param name="action">The action that caused the event. This must be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />.</param>
		// Token: 0x06003870 RID: 14448 RVA: 0x000CF62C File Offset: 0x000CD82C
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			if (action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Reset }), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItem">The item that is affected by the change.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, or if <paramref name="action" /> is Reset and <paramref name="changedItem" /> is not null.</exception>
		// Token: 0x06003871 RID: 14449 RVA: 0x000CF680 File Offset: 0x000CD880
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(global::SR.GetString("Constructor only supports either a Reset, Add, or Remove action."), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[] { changedItem }, -1);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(global::SR.GetString("Reset action must be initialized with no changed items."), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItem">The item that is affected by the change.</param>
		/// <param name="index">The index where the change occurred.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, or if <paramref name="action" /> is Reset and either <paramref name="changedItems" /> is not null or <paramref name="index" /> is not -1.</exception>
		// Token: 0x06003872 RID: 14450 RVA: 0x000CF6FC File Offset: 0x000CD8FC
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(global::SR.GetString("Constructor only supports either a Reset, Add, or Remove action."), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[] { changedItem }, index);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(global::SR.GetString("Reset action must be initialized with no changed items."), "action");
			}
			if (index != -1)
			{
				throw new ArgumentException(global::SR.GetString("Reset action must be initialized with index -1."), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItems">The items that are affected by the change.</param>
		// Token: 0x06003873 RID: 14451 RVA: 0x000CF790 File Offset: 0x000CD990
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(global::SR.GetString("Constructor only supports either a Reset, Add, or Remove action."), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(global::SR.GetString("Reset action must be initialized with no changed items."), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				this.InitializeAddOrRemove(action, changedItems, -1);
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item change or a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" /> change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItems">The items affected by the change.</param>
		/// <param name="startingIndex">The index where the change occurred.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, if <paramref name="action" /> is Reset and either <paramref name="changedItems" /> is not null or <paramref name="startingIndex" /> is not -1, or if action is Add or Remove and <paramref name="startingIndex" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="action" /> is Add or Remove and <paramref name="changedItems" /> is null.</exception>
		// Token: 0x06003874 RID: 14452 RVA: 0x000CF810 File Offset: 0x000CDA10
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(global::SR.GetString("Constructor only supports either a Reset, Add, or Remove action."), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(global::SR.GetString("Reset action must be initialized with no changed items."), "action");
				}
				if (startingIndex != -1)
				{
					throw new ArgumentException(global::SR.GetString("Reset action must be initialized with index -1."), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				if (startingIndex < -1)
				{
					throw new ArgumentException(global::SR.GetString("Index cannot be negative."), "startingIndex");
				}
				this.InitializeAddOrRemove(action, changedItems, startingIndex);
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItem">The new item that is replacing the original item.</param>
		/// <param name="oldItem">The original item that is replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		// Token: 0x06003875 RID: 14453 RVA: 0x000CF8C0 File Offset: 0x000CDAC0
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, -1, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItem">The new item that is replacing the original item.</param>
		/// <param name="oldItem">The original item that is replaced.</param>
		/// <param name="index">The index of the item being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		// Token: 0x06003876 RID: 14454 RVA: 0x000CF928 File Offset: 0x000CDB28
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, index, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItems">The new items that are replacing the original items.</param>
		/// <param name="oldItems">The original items that are replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="oldItems" /> or <paramref name="newItems" /> is null.</exception>
		// Token: 0x06003877 RID: 14455 RVA: 0x000CF994 File Offset: 0x000CDB94
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItems">The new items that are replacing the original items.</param>
		/// <param name="oldItems">The original items that are replaced.</param>
		/// <param name="startingIndex">The index of the first item of the items that are being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="oldItems" /> or <paramref name="newItems" /> is null.</exception>
		// Token: 0x06003878 RID: 14456 RVA: 0x000CFA04 File Offset: 0x000CDC04
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />.</param>
		/// <param name="changedItem">The item affected by the change.</param>
		/// <param name="index">The new index for the changed item.</param>
		/// <param name="oldIndex">The old index for the changed item.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Move or <paramref name="index" /> is less than 0.</exception>
		// Token: 0x06003879 RID: 14457 RVA: 0x000CFA78 File Offset: 0x000CDC78
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Move }), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(global::SR.GetString("Index cannot be negative."), "index");
			}
			object[] array = new object[] { changedItem };
			this.InitializeMoveOrReplace(action, array, array, index, oldIndex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />.</param>
		/// <param name="changedItems">The items affected by the change.</param>
		/// <param name="index">The new index for the changed items.</param>
		/// <param name="oldIndex">The old index for the changed items.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Move or <paramref name="index" /> is less than 0.</exception>
		// Token: 0x0600387A RID: 14458 RVA: 0x000CFAF4 File Offset: 0x000CDCF4
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(global::SR.GetString("Constructor supports only the '{0}' action.", new object[] { NotifyCollectionChangedAction.Move }), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(global::SR.GetString("Index cannot be negative."), "index");
			}
			this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000CFB64 File Offset: 0x000CDD64
		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int newIndex, int oldIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._newStartingIndex = newIndex;
			this._oldStartingIndex = oldIndex;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000CFBC0 File Offset: 0x000CDDC0
		private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action == NotifyCollectionChangedAction.Add)
			{
				this.InitializeAdd(action, changedItems, startingIndex);
				return;
			}
			if (action == NotifyCollectionChangedAction.Remove)
			{
				this.InitializeRemove(action, changedItems, startingIndex);
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000CFBDC File Offset: 0x000CDDDC
		private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._newStartingIndex = newStartingIndex;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000CFBFE File Offset: 0x000CDDFE
		private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
		{
			this._action = action;
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._oldStartingIndex = oldStartingIndex;
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000CFC20 File Offset: 0x000CDE20
		private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
		{
			this.InitializeAdd(action, newItems, startingIndex);
			this.InitializeRemove(action, oldItems, oldStartingIndex);
		}

		/// <summary>Gets the action that caused the event. </summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NotifyCollectionChangedAction" /> value that describes the action that caused the event.</returns>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000CFC36 File Offset: 0x000CDE36
		public NotifyCollectionChangedAction Action
		{
			get
			{
				return this._action;
			}
		}

		/// <summary>Gets the list of new items involved in the change.</summary>
		/// <returns>The list of new items involved in the change.</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x000CFC3E File Offset: 0x000CDE3E
		public IList NewItems
		{
			get
			{
				return this._newItems;
			}
		}

		/// <summary>Gets the list of items affected by a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />, Remove, or Move action.</summary>
		/// <returns>The list of items affected by a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />, Remove, or Move action.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000CFC46 File Offset: 0x000CDE46
		public IList OldItems
		{
			get
			{
				return this._oldItems;
			}
		}

		/// <summary>Gets the index at which the change occurred.</summary>
		/// <returns>The zero-based index at which the change occurred.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000CFC4E File Offset: 0x000CDE4E
		public int NewStartingIndex
		{
			get
			{
				return this._newStartingIndex;
			}
		}

		/// <summary>Gets the index at which a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />, Remove, or Replace action occurred.</summary>
		/// <returns>The zero-based index at which a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />, Remove, or Replace action occurred.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000CFC56 File Offset: 0x000CDE56
		public int OldStartingIndex
		{
			get
			{
				return this._oldStartingIndex;
			}
		}

		// Token: 0x04002C5F RID: 11359
		private NotifyCollectionChangedAction _action;

		// Token: 0x04002C60 RID: 11360
		private IList _newItems;

		// Token: 0x04002C61 RID: 11361
		private IList _oldItems;

		// Token: 0x04002C62 RID: 11362
		private int _newStartingIndex = -1;

		// Token: 0x04002C63 RID: 11363
		private int _oldStartingIndex = -1;
	}
}
