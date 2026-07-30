using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies generic undo/redo functionality at design time.</summary>
	// Token: 0x02000139 RID: 313
	public abstract class UndoEngine : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.UndoEngine" /> class.</summary>
		/// <param name="provider">A parenting service provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">A required service cannot be found. See <see cref="T:System.ComponentModel.Design.UndoEngine" /> for required services. If you have removed this service, ensure that you provide a replacement.</exception>
		// Token: 0x06000937 RID: 2359 RVA: 0x0001022E File Offset: 0x0000E42E
		protected UndoEngine(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
			this._currentUnit = null;
			this.Enable();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00010258 File Offset: 0x0000E458
		private void Enable()
		{
			if (!this._enabled)
			{
				IComponentChangeService componentChangeService = this.GetRequiredService(typeof(IComponentChangeService)) as IComponentChangeService;
				componentChangeService.ComponentAdding += this.OnComponentAdding;
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				componentChangeService.ComponentChanging += this.OnComponentChanging;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
				componentChangeService.ComponentRename += this.OnComponentRename;
				IDesignerHost designerHost = this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
				designerHost.TransactionClosed += this.OnTransactionClosed;
				designerHost.TransactionOpened += this.OnTransactionOpened;
				this._enabled = true;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00010344 File Offset: 0x0000E544
		private void Disable()
		{
			if (this._enabled)
			{
				IComponentChangeService componentChangeService = this.GetRequiredService(typeof(IComponentChangeService)) as IComponentChangeService;
				componentChangeService.ComponentAdding -= this.OnComponentAdding;
				componentChangeService.ComponentAdded -= this.OnComponentAdded;
				componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
				componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				componentChangeService.ComponentChanging -= this.OnComponentChanging;
				componentChangeService.ComponentChanged -= this.OnComponentChanged;
				componentChangeService.ComponentRename -= this.OnComponentRename;
				IDesignerHost designerHost = this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
				designerHost.TransactionClosed -= this.OnTransactionClosed;
				designerHost.TransactionOpened -= this.OnTransactionOpened;
				this._enabled = false;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00010430 File Offset: 0x0000E630
		private void OnTransactionOpened(object sender, EventArgs args)
		{
			if (this._currentUnit == null)
			{
				IDesignerHost designerHost = this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
				this._currentUnit = this.CreateUndoUnit(designerHost.TransactionDescription, true);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00010470 File Offset: 0x0000E670
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs args)
		{
			if (!(this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
			{
				this._currentUnit.Close();
				if (args.TransactionCommitted)
				{
					this.AddUndoUnit(this._currentUnit);
				}
				else
				{
					this._currentUnit.Undo();
					this.DiscardUndoUnit(this._currentUnit);
				}
				this._currentUnit = null;
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000104D8 File Offset: 0x0000E6D8
		private void OnComponentAdding(object sender, ComponentEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Add " + args.Component.GetType().Name, true);
			}
			this._currentUnit.ComponentAdding(args);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00010518 File Offset: 0x0000E718
		private void OnComponentAdded(object sender, ComponentEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Add " + args.Component.Site.Name, true);
			}
			this._currentUnit.ComponentAdded(args);
			if (!(this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
			{
				this._currentUnit.Close();
				this.AddUndoUnit(this._currentUnit);
				this._currentUnit = null;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001059A File Offset: 0x0000E79A
		private void OnComponentRemoving(object sender, ComponentEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Remove " + args.Component.Site.Name, true);
			}
			this._currentUnit.ComponentRemoving(args);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000105D8 File Offset: 0x0000E7D8
		private void OnComponentRemoved(object sender, ComponentEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Remove " + args.Component.GetType().Name, true);
			}
			this._currentUnit.ComponentRemoved(args);
			if (!(this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
			{
				this._currentUnit.Close();
				this.AddUndoUnit(this._currentUnit);
				this._currentUnit = null;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001065C File Offset: 0x0000E85C
		private void OnComponentChanging(object sender, ComponentChangingEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Modify " + ((IComponent)args.Component).Site.Name + ((args.Member != null) ? ("." + args.Member.Name) : ""), true);
			}
			this._currentUnit.ComponentChanging(args);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000106D0 File Offset: 0x0000E8D0
		private void OnComponentChanged(object sender, ComponentChangedEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Modify " + ((IComponent)args.Component).Site.Name + "." + ((args.Member != null) ? ("." + args.Member.Name) : ""), true);
			}
			this._currentUnit.ComponentChanged(args);
			if (!(this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
			{
				this._currentUnit.Close();
				this.AddUndoUnit(this._currentUnit);
				this._currentUnit = null;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00010780 File Offset: 0x0000E980
		private void OnComponentRename(object sender, ComponentRenameEventArgs args)
		{
			if (this._currentUnit == null)
			{
				this._currentUnit = this.CreateUndoUnit("Rename " + ((IComponent)args.Component).Site.Name, true);
			}
			this._currentUnit.ComponentRename(args);
			if (!(this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
			{
				this._currentUnit.Close();
				this.AddUndoUnit(this._currentUnit);
				this._currentUnit = null;
			}
		}

		/// <summary>Occurs immediately before an undo action is performed.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000943 RID: 2371 RVA: 0x00010808 File Offset: 0x0000EA08
		// (remove) Token: 0x06000944 RID: 2372 RVA: 0x00010840 File Offset: 0x0000EA40
		public event EventHandler Undoing;

		/// <summary>Occurs immediately after an undo action is performed.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000945 RID: 2373 RVA: 0x00010878 File Offset: 0x0000EA78
		// (remove) Token: 0x06000946 RID: 2374 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public event EventHandler Undone;

		/// <summary>Enables or disables the <see cref="T:System.ComponentModel.Design.UndoEngine" />.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.Design.UndoEngine" /> is enabled; otherwise, false.</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000108E5 File Offset: 0x0000EAE5
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x000108ED File Offset: 0x0000EAED
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (value)
				{
					this.Enable();
					return;
				}
				this.Disable();
			}
		}

		/// <summary>Indicates if an undo action is in progress.</summary>
		/// <returns>true if an undo action is in progress; otherwise, false.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000108FF File Offset: 0x0000EAFF
		public bool UndoInProgress
		{
			get
			{
				return this._undoing;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> with a specified name.</returns>
		/// <param name="name">The name of the unit to create. </param>
		/// <param name="primary">true to create the first of a series of nested units; false to create subsequent nested units.</param>
		// Token: 0x0600094A RID: 2378 RVA: 0x00010907 File Offset: 0x0000EB07
		protected virtual UndoEngine.UndoUnit CreateUndoUnit(string name, bool primary)
		{
			return new UndoEngine.UndoUnit(this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.UndoEngine" />.</summary>
		// Token: 0x0600094B RID: 2379 RVA: 0x00010910 File Offset: 0x0000EB10
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.UndoEngine" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600094C RID: 2380 RVA: 0x00010919 File Offset: 0x0000EB19
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._currentUnit != null)
			{
				this._currentUnit.Close();
				this._currentUnit = null;
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>The requested service, if found.</returns>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="serviceType" /> is required but cannot be found. If you have removed this service, ensure that you provide a replacement.</exception>
		// Token: 0x0600094D RID: 2381 RVA: 0x00010938 File Offset: 0x0000EB38
		protected object GetRequiredService(Type serviceType)
		{
			object service = this.GetService(serviceType);
			if (service == null)
			{
				throw new NotSupportedException("Service '" + serviceType.Name + "' missing");
			}
			return service;
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>The requested service, or null if the requested service is not found.</returns>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x0600094E RID: 2382 RVA: 0x0001095F File Offset: 0x0000EB5F
		protected object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.UndoEngine.Undoing" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600094F RID: 2383 RVA: 0x0001098B File Offset: 0x0000EB8B
		protected virtual void OnUndoing(EventArgs e)
		{
			this.Disable();
			this._undoing = true;
			if (this.Undoing != null)
			{
				this.Undoing(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.UndoEngine.Undone" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000950 RID: 2384 RVA: 0x000109AF File Offset: 0x0000EBAF
		protected virtual void OnUndone(EventArgs e)
		{
			this.Enable();
			this._undoing = false;
			if (this.Undone != null)
			{
				this.Undone(this, e);
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> to the undo stack.</summary>
		/// <param name="unit">The undo unit to add </param>
		// Token: 0x06000951 RID: 2385
		protected abstract void AddUndoUnit(UndoEngine.UndoUnit unit);

		/// <summary>Discards an <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</summary>
		/// <param name="unit">The unit to discard.</param>
		// Token: 0x06000952 RID: 2386 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void DiscardUndoUnit(UndoEngine.UndoUnit unit)
		{
		}

		// Token: 0x04000211 RID: 529
		private bool _undoing;

		// Token: 0x04000212 RID: 530
		private UndoEngine.UndoUnit _currentUnit;

		// Token: 0x04000213 RID: 531
		private IServiceProvider _provider;

		// Token: 0x04000214 RID: 532
		private bool _enabled;

		/// <summary>Encapsulates a unit of work that a user can undo.</summary>
		// Token: 0x0200013A RID: 314
		protected class UndoUnit
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> class.</summary>
			/// <param name="engine">The undo engine that owns this undo unit.</param>
			/// <param name="name">The name for this undo unit.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="engine" /> is null.</exception>
			// Token: 0x06000953 RID: 2387 RVA: 0x000109D3 File Offset: 0x0000EBD3
			public UndoUnit(UndoEngine engine, string name)
			{
				if (engine == null)
				{
					throw new ArgumentNullException("engine");
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				this._engine = engine;
				this._name = name;
				this._actions = new List<UndoEngine.UndoUnit.Action>();
			}

			/// <summary>Performs an undo or redo action.</summary>
			// Token: 0x06000954 RID: 2388 RVA: 0x00010A10 File Offset: 0x0000EC10
			public void Undo()
			{
				this._engine.OnUndoing(EventArgs.Empty);
				this.UndoCore();
				this._engine.OnUndone(EventArgs.Empty);
			}

			/// <summary>Called by <see cref="M:System.ComponentModel.Design.UndoEngine.UndoUnit.Undo" /> to perform an undo action.</summary>
			// Token: 0x06000955 RID: 2389 RVA: 0x00010A38 File Offset: 0x0000EC38
			protected virtual void UndoCore()
			{
				for (int i = this._actions.Count - 1; i >= 0; i--)
				{
					this._actions[i].Undo(this._engine);
				}
				this._actions.Reverse();
			}

			/// <summary>Gets the parent <see cref="P:System.ComponentModel.Design.UndoEngine.UndoUnit.UndoEngine" />.</summary>
			/// <returns>The <see cref="P:System.ComponentModel.Design.UndoEngine.UndoUnit.UndoEngine" /> to which this <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> is attached.</returns>
			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000956 RID: 2390 RVA: 0x00010A7F File Offset: 0x0000EC7F
			protected UndoEngine UndoEngine
			{
				get
				{
					return this._engine;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> contains no events.</summary>
			/// <returns>true if the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> contains no events; otherwise, false.</returns>
			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000957 RID: 2391 RVA: 0x00010A87 File Offset: 0x0000EC87
			public virtual bool IsEmpty
			{
				get
				{
					return this._actions.Count == 0;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />. </summary>
			/// <returns>The name of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</returns>
			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000958 RID: 2392 RVA: 0x00010A97 File Offset: 0x0000EC97
			public virtual string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Receives a call from the undo engine to close this unit.</summary>
			// Token: 0x06000959 RID: 2393 RVA: 0x00010A9F File Offset: 0x0000EC9F
			public virtual void Close()
			{
				this._closed = true;
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095A RID: 2394 RVA: 0x00010AA8 File Offset: 0x0000ECA8
			public virtual void ComponentAdded(ComponentEventArgs e)
			{
				if (!this._closed)
				{
					this._actions.Add(new UndoEngine.UndoUnit.ComponentAddRemoveAction(this._engine, e.Component, true));
				}
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095B RID: 2395 RVA: 0x00002432 File Offset: 0x00000632
			public virtual void ComponentAdding(ComponentEventArgs e)
			{
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095C RID: 2396 RVA: 0x00010AD0 File Offset: 0x0000ECD0
			public virtual void ComponentChanged(ComponentChangedEventArgs e)
			{
				if (this._closed)
				{
					return;
				}
				for (int i = 0; i < this._actions.Count; i++)
				{
					UndoEngine.UndoUnit.ComponentChangeAction componentChangeAction = this._actions[i] as UndoEngine.UndoUnit.ComponentChangeAction;
					if (componentChangeAction != null && !componentChangeAction.IsComplete && componentChangeAction.Component == e.Component && componentChangeAction.Member.Equals(e.Member))
					{
						componentChangeAction.SetModifiedState(this._engine, (IComponent)e.Component, e.Member);
						return;
					}
				}
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095D RID: 2397 RVA: 0x00010B5C File Offset: 0x0000ED5C
			public virtual void ComponentChanging(ComponentChangingEventArgs e)
			{
				if (this._closed)
				{
					return;
				}
				UndoEngine.UndoUnit.ComponentChangeAction componentChangeAction = new UndoEngine.UndoUnit.ComponentChangeAction();
				componentChangeAction.SetOriginalState(this._engine, (IComponent)e.Component, e.Member);
				this._actions.Add(componentChangeAction);
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095E RID: 2398 RVA: 0x00002432 File Offset: 0x00000632
			public virtual void ComponentRemoved(ComponentEventArgs e)
			{
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" />  that contains the event data.</param>
			// Token: 0x0600095F RID: 2399 RVA: 0x00010BA1 File Offset: 0x0000EDA1
			public virtual void ComponentRemoving(ComponentEventArgs e)
			{
				if (!this._closed)
				{
					this._actions.Add(new UndoEngine.UndoUnit.ComponentAddRemoveAction(this._engine, e.Component, false));
				}
			}

			/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
			/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" />  that contains the event data.</param>
			// Token: 0x06000960 RID: 2400 RVA: 0x00010BC8 File Offset: 0x0000EDC8
			public virtual void ComponentRename(ComponentRenameEventArgs e)
			{
				if (!this._closed)
				{
					this._actions.Add(new UndoEngine.UndoUnit.ComponentRenameAction(e.NewName, e.OldName));
				}
			}

			/// <summary>Gets an instance of the requested service.</summary>
			/// <returns>An instance of the given service, or null if the service cannot be resolved.</returns>
			/// <param name="serviceType">The type of service to retrieve.</param>
			// Token: 0x06000961 RID: 2401 RVA: 0x00010BEE File Offset: 0x0000EDEE
			protected object GetService(Type serviceType)
			{
				return this._engine.GetService(serviceType);
			}

			/// <summary>Returns a <see cref="T:System.String" /> that represents the current name of the unit.</summary>
			/// <returns>A <see cref="T:System.String" /> that represents the current name of the unit.</returns>
			// Token: 0x06000962 RID: 2402 RVA: 0x00010A97 File Offset: 0x0000EC97
			public override string ToString()
			{
				return this._name;
			}

			// Token: 0x04000217 RID: 535
			private UndoEngine _engine;

			// Token: 0x04000218 RID: 536
			private string _name;

			// Token: 0x04000219 RID: 537
			private bool _closed;

			// Token: 0x0400021A RID: 538
			private List<UndoEngine.UndoUnit.Action> _actions;

			// Token: 0x0200013B RID: 315
			private class Action
			{
				// Token: 0x06000963 RID: 2403 RVA: 0x00002432 File Offset: 0x00000632
				public virtual void Undo(UndoEngine engine)
				{
				}
			}

			// Token: 0x0200013C RID: 316
			private class ComponentRenameAction : UndoEngine.UndoUnit.Action
			{
				// Token: 0x06000965 RID: 2405 RVA: 0x00010BFC File Offset: 0x0000EDFC
				public ComponentRenameAction(string currentName, string oldName)
				{
					this._currentName = currentName;
					this._oldName = oldName;
				}

				// Token: 0x06000966 RID: 2406 RVA: 0x00010C14 File Offset: 0x0000EE14
				public override void Undo(UndoEngine engine)
				{
					(engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).Container.Components[this._currentName].Site.Name = this._oldName;
					string currentName = this._currentName;
					this._currentName = this._oldName;
					this._oldName = currentName;
				}

				// Token: 0x0400021B RID: 539
				private string _oldName;

				// Token: 0x0400021C RID: 540
				private string _currentName;
			}

			// Token: 0x0200013D RID: 317
			private class ComponentAddRemoveAction : UndoEngine.UndoUnit.Action
			{
				// Token: 0x06000967 RID: 2407 RVA: 0x00010C78 File Offset: 0x0000EE78
				public ComponentAddRemoveAction(UndoEngine engine, IComponent component, bool added)
				{
					if (component == null)
					{
						throw new ArgumentNullException("component");
					}
					ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
					this._serializedComponent = componentSerializationService.CreateStore();
					componentSerializationService.Serialize(this._serializedComponent, component);
					this._serializedComponent.Close();
					this._added = added;
					this._componentName = component.Site.Name;
				}

				// Token: 0x06000968 RID: 2408 RVA: 0x00010CEC File Offset: 0x0000EEEC
				public override void Undo(UndoEngine engine)
				{
					IDesignerHost designerHost = engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
					if (this._added)
					{
						IComponent component = designerHost.Container.Components[this._componentName];
						if (component != null)
						{
							designerHost.DestroyComponent(component);
						}
						this._added = false;
						return;
					}
					(engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService).DeserializeTo(this._serializedComponent, designerHost.Container);
					this._added = true;
				}

				// Token: 0x0400021D RID: 541
				private string _componentName;

				// Token: 0x0400021E RID: 542
				private SerializationStore _serializedComponent;

				// Token: 0x0400021F RID: 543
				private bool _added;
			}

			// Token: 0x0200013E RID: 318
			private class ComponentChangeAction : UndoEngine.UndoUnit.Action
			{
				// Token: 0x0600096A RID: 2410 RVA: 0x00010D78 File Offset: 0x0000EF78
				public void SetOriginalState(UndoEngine engine, IComponent component, MemberDescriptor member)
				{
					this._member = member;
					this._component = component;
					this._componentName = ((component.Site != null) ? component.Site.Name : null);
					ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
					this._beforeChange = componentSerializationService.CreateStore();
					componentSerializationService.SerializeMemberAbsolute(this._beforeChange, component, member);
					this._beforeChange.Close();
				}

				// Token: 0x0600096B RID: 2411 RVA: 0x00010DEC File Offset: 0x0000EFEC
				public void SetModifiedState(UndoEngine engine, IComponent component, MemberDescriptor member)
				{
					ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
					this._afterChange = componentSerializationService.CreateStore();
					componentSerializationService.SerializeMemberAbsolute(this._afterChange, component, member);
					this._afterChange.Close();
				}

				// Token: 0x17000200 RID: 512
				// (get) Token: 0x0600096C RID: 2412 RVA: 0x00010E34 File Offset: 0x0000F034
				public bool IsComplete
				{
					get
					{
						return this._beforeChange != null && this._afterChange != null;
					}
				}

				// Token: 0x17000201 RID: 513
				// (get) Token: 0x0600096D RID: 2413 RVA: 0x00010E49 File Offset: 0x0000F049
				public string ComponentName
				{
					get
					{
						return this._componentName;
					}
				}

				// Token: 0x17000202 RID: 514
				// (get) Token: 0x0600096E RID: 2414 RVA: 0x00010E51 File Offset: 0x0000F051
				public IComponent Component
				{
					get
					{
						return this._component;
					}
				}

				// Token: 0x17000203 RID: 515
				// (get) Token: 0x0600096F RID: 2415 RVA: 0x00010E59 File Offset: 0x0000F059
				public MemberDescriptor Member
				{
					get
					{
						return this._member;
					}
				}

				// Token: 0x06000970 RID: 2416 RVA: 0x00010E64 File Offset: 0x0000F064
				public override void Undo(UndoEngine engine)
				{
					if (this._beforeChange == null)
					{
						return;
					}
					IDesignerHost designerHost = (IDesignerHost)engine.GetRequiredService(typeof(IDesignerHost));
					this._component = designerHost.Container.Components[this._componentName];
					(engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService).DeserializeTo(this._beforeChange, designerHost.Container);
					SerializationStore beforeChange = this._beforeChange;
					this._beforeChange = this._afterChange;
					this._afterChange = beforeChange;
				}

				// Token: 0x04000220 RID: 544
				private string _componentName;

				// Token: 0x04000221 RID: 545
				private MemberDescriptor _member;

				// Token: 0x04000222 RID: 546
				private IComponent _component;

				// Token: 0x04000223 RID: 547
				private SerializationStore _afterChange;

				// Token: 0x04000224 RID: 548
				private SerializationStore _beforeChange;
			}
		}
	}
}
