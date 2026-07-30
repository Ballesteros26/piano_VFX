using System;
using System.Collections;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an implementation of the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderService" /> interface.</summary>
	// Token: 0x0200013F RID: 319
	public abstract class BasicDesignerLoader : DesignerLoader, IDesignerLoaderService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader" /> class.</summary>
		// Token: 0x06000971 RID: 2417 RVA: 0x00010EEC File Offset: 0x0000F0EC
		protected BasicDesignerLoader()
		{
			this._loading = (this._loaded = (this._flushing = (this._reloadScheduled = false)));
			this._host = null;
			this._notificationsEnabled = false;
			this._modified = false;
			this._dependenciesCount = 0;
		}

		/// <summary>Initializes services.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
		// Token: 0x06000972 RID: 2418 RVA: 0x00010F40 File Offset: 0x0000F140
		protected virtual void Initialize()
		{
			this._serializationMananger = new DesignerSerializationManager(this._host);
			DesignSurfaceServiceContainer designSurfaceServiceContainer = this._host.GetService(typeof(IServiceContainer)) as DesignSurfaceServiceContainer;
			if (designSurfaceServiceContainer != null)
			{
				designSurfaceServiceContainer.AddService(typeof(IDesignerLoaderService), this);
				designSurfaceServiceContainer.AddNonReplaceableService(typeof(IDesignerSerializationManager), this._serializationMananger);
			}
		}

		/// <summary>Starts the loading process.</summary>
		/// <param name="host">The designer loader host to load.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The designer is already loaded, or <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.BeginLoad(System.ComponentModel.Design.Serialization.IDesignerLoaderHost)" /> has been called with a different designer loader host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <paramref name="host" /> has been disposed.</exception>
		// Token: 0x06000973 RID: 2419 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public override void BeginLoad(IDesignerLoaderHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (this._loaded)
			{
				throw new InvalidOperationException("Already loaded.");
			}
			if (this._host != null && this._host != host)
			{
				throw new InvalidOperationException("Trying to load with a different host");
			}
			if (this._host == null)
			{
				this._host = host;
				this.Initialize();
			}
			IDisposable disposable = this._serializationMananger.CreateSession();
			IDesignerLoaderService designerLoaderService = this._host.GetService(typeof(IDesignerLoaderService)) as IDesignerLoaderService;
			if (designerLoaderService != null)
			{
				this._dependenciesCount = -1;
				designerLoaderService.AddLoadDependency();
			}
			else
			{
				this.OnBeginLoad();
			}
			bool flag = true;
			try
			{
				this.PerformLoad(this._serializationMananger);
			}
			catch (Exception ex)
			{
				flag = false;
				this._serializationMananger.Errors.Add(ex);
			}
			if (designerLoaderService != null)
			{
				designerLoaderService.DependentLoadComplete(flag, this._serializationMananger.Errors);
			}
			else
			{
				this.OnEndLoad(flag, this._serializationMananger.Errors);
			}
			disposable.Dispose();
		}

		/// <summary>Loads a designer from persistence.</summary>
		/// <param name="serializationManager">An <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for loading state for the designers.</param>
		// Token: 0x06000974 RID: 2420
		protected abstract void PerformLoad(IDesignerSerializationManager serializationManager);

		/// <summary>Notifies the designer loader that loading is about to begin.</summary>
		// Token: 0x06000975 RID: 2421 RVA: 0x000110A8 File Offset: 0x0000F2A8
		protected virtual void OnBeginLoad()
		{
			this._loading = true;
		}

		/// <summary>Notifies the designer loader that loading is complete.</summary>
		/// <param name="successful">true if the load completed successfully; otherwise, false.</param>
		/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> containing objects (usually exceptions) that were reported as errors.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
		// Token: 0x06000976 RID: 2422 RVA: 0x000110B4 File Offset: 0x0000F2B4
		protected virtual void OnEndLoad(bool successful, ICollection errors)
		{
			this._host.EndLoad(this._baseComponentClassName, successful, errors);
			if (successful)
			{
				this._loaded = true;
				this.EnableComponentNotification(true);
			}
			else if (this._reloadScheduled && (this._reloadOptions & BasicDesignerLoader.ReloadOptions.ModifyOnError) == BasicDesignerLoader.ReloadOptions.ModifyOnError)
			{
				this.OnModifying();
				this.Modified = true;
			}
			this._loading = false;
		}

		/// <summary>Gets a value indicating whether the designer loader is loading the design surface.</summary>
		/// <returns>true if the designer loader is currently loading the design surface; otherwise, false.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00011111 File Offset: 0x0000F311
		public override bool Loading
		{
			get
			{
				return this._loading;
			}
		}

		/// <summary>Gets the loader host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> that was passed to the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.BeginLoad(System.ComponentModel.Design.Serialization.IDesignerLoaderHost)" /> method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00011119 File Offset: 0x0000F319
		protected IDesignerLoaderHost LoaderHost
		{
			get
			{
				return this._host;
			}
		}

		/// <summary>Gets or sets a value indicating whether the designer has been modified.</summary>
		/// <returns>true if the designer has been modified; otherwise, false,</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00011121 File Offset: 0x0000F321
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00011129 File Offset: 0x0000F329
		protected virtual bool Modified
		{
			get
			{
				return this._modified;
			}
			set
			{
				this._modified = value;
			}
		}

		/// <summary>Gets or sets the property provider for the serialization manager being used by the loader.</summary>
		/// <returns>An object whose properties are to be provided to the serialization manager.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00011132 File Offset: 0x0000F332
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00011152 File Offset: 0x0000F352
		protected object PropertyProvider
		{
			get
			{
				if (!this._loaded)
				{
					throw new InvalidOperationException("host not initialized");
				}
				return this._serializationMananger.PropertyProvider;
			}
			set
			{
				if (!this._loaded)
				{
					throw new InvalidOperationException("host not initialized");
				}
				this._serializationMananger.PropertyProvider = value;
			}
		}

		/// <summary>Gets a value indicating whether a reload has been queued.</summary>
		/// <returns>true, if a call to <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Reload(System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions)" /> has queued a reload request; otherwise, false.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00011173 File Offset: 0x0000F373
		protected bool ReloadPending
		{
			get
			{
				return this._reloadScheduled;
			}
		}

		/// <summary>Enables or disables component notification with the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		/// <returns>true if the component notification was enabled prior to this call; otherwise, false.</returns>
		/// <param name="enable">true to enable component notification by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />; false to disable component notification by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</param>
		// Token: 0x0600097E RID: 2430 RVA: 0x0001117C File Offset: 0x0000F37C
		protected virtual bool EnableComponentNotification(bool enable)
		{
			if (!this._loaded)
			{
				throw new InvalidOperationException("host not initialized");
			}
			IComponentChangeService componentChangeService = this._host.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null && this._notificationsEnabled != enable)
			{
				if (enable)
				{
					componentChangeService.ComponentAdding += this.OnComponentAdding;
					componentChangeService.ComponentAdded += this.OnComponentAdded;
					componentChangeService.ComponentRemoving += this.OnComponentRemoving;
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					componentChangeService.ComponentChanging += this.OnComponentChanging;
					componentChangeService.ComponentChanged += this.OnComponentChanged;
					componentChangeService.ComponentRename += this.OnComponentRename;
				}
				else
				{
					componentChangeService.ComponentAdding -= this.OnComponentAdding;
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					componentChangeService.ComponentChanging -= this.OnComponentChanging;
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
					componentChangeService.ComponentRename -= this.OnComponentRename;
				}
			}
			return this._notificationsEnabled;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000112D8 File Offset: 0x0000F4D8
		private void OnComponentAdded(object sender, ComponentEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.Modified = true;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000112D8 File Offset: 0x0000F4D8
		private void OnComponentRemoved(object sender, ComponentEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.Modified = true;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000112F1 File Offset: 0x0000F4F1
		private void OnComponentAdding(object sender, ComponentEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.OnModifying();
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000112F1 File Offset: 0x0000F4F1
		private void OnComponentRemoving(object sender, ComponentEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.OnModifying();
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000112D8 File Offset: 0x0000F4D8
		private void OnComponentChanged(object sender, ComponentChangedEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.Modified = true;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000112F1 File Offset: 0x0000F4F1
		private void OnComponentChanging(object sender, ComponentChangingEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.OnModifying();
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00011309 File Offset: 0x0000F509
		private void OnComponentRename(object sender, ComponentRenameEventArgs args)
		{
			if (!this._loading && this._loaded)
			{
				this.OnModifying();
				this.Modified = true;
			}
		}

		/// <summary>Flushes pending changes to the designer loader.</summary>
		// Token: 0x06000986 RID: 2438 RVA: 0x00011328 File Offset: 0x0000F528
		public override void Flush()
		{
			if (!this._loaded)
			{
				throw new InvalidOperationException("host not initialized");
			}
			if (!this._flushing && this.Modified)
			{
				this._flushing = true;
				using (this._serializationMananger.CreateSession())
				{
					try
					{
						this.PerformFlush(this._serializationMananger);
					}
					catch (Exception ex)
					{
						this._serializationMananger.Errors.Add(ex);
						this.ReportFlushErrors(this._serializationMananger.Errors);
					}
				}
				this._flushing = false;
			}
		}

		/// <summary>Flushes all changes to the designer.</summary>
		/// <param name="serializationManager">An <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for persisting the state of loaded designers.</param>
		// Token: 0x06000987 RID: 2439
		protected abstract void PerformFlush(IDesignerSerializationManager serializationManager);

		/// <summary>Indicates whether the designer should be reloaded.</summary>
		/// <returns>true if the designer should be reloaded; otherwise, false. The default implementation always returns true.</returns>
		// Token: 0x06000988 RID: 2440 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool IsReloadNeeded()
		{
			return true;
		}

		/// <summary>Notifies the designer loader that unloading is about to begin.</summary>
		// Token: 0x06000989 RID: 2441 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnBeginUnload()
		{
		}

		/// <summary>Notifies the designer loader that the state of the document is about to be modified.</summary>
		// Token: 0x0600098A RID: 2442 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnModifying()
		{
		}

		/// <summary>Queues a reload of the designer.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions" /> values.</param>
		// Token: 0x0600098B RID: 2443 RVA: 0x000113CC File Offset: 0x0000F5CC
		protected void Reload(BasicDesignerLoader.ReloadOptions flags)
		{
			if (!this._reloadScheduled)
			{
				this._reloadScheduled = true;
				this._reloadOptions = flags;
				if ((flags & BasicDesignerLoader.ReloadOptions.Force) == BasicDesignerLoader.ReloadOptions.Force)
				{
					this.ReloadCore();
					return;
				}
				Application.Idle += this.OnIdle;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00011404 File Offset: 0x0000F604
		private void OnIdle(object sender, EventArgs args)
		{
			Application.Idle -= this.OnIdle;
			this.ReloadCore();
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001141D File Offset: 0x0000F61D
		private void ReloadCore()
		{
			if ((this._reloadOptions & BasicDesignerLoader.ReloadOptions.NoFlush) != BasicDesignerLoader.ReloadOptions.NoFlush)
			{
				this.Flush();
			}
			this.Unload();
			this._host.Reload();
			this.BeginLoad(this._host);
			this._reloadScheduled = false;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00011459 File Offset: 0x0000F659
		private void Unload()
		{
			if (this._loaded)
			{
				this.OnBeginUnload();
				this.EnableComponentNotification(false);
				this._loaded = false;
				this._baseComponentClassName = null;
			}
		}

		/// <summary>Reports errors that occurred while flushing changes.</summary>
		/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> containing error objects, usually exceptions.</param>
		/// <exception cref="T:System.InvalidOperationException">One or more errors occurred while flushing changes.</exception>
		// Token: 0x0600098F RID: 2447 RVA: 0x00011480 File Offset: 0x0000F680
		protected virtual void ReportFlushErrors(ICollection errors)
		{
			object obj = null;
			using (IEnumerator enumerator = errors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
			}
			throw (Exception)obj;
		}

		/// <summary>Sets the full class name of the base component.</summary>
		/// <param name="name">A string representing the full name of the component to be designed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000990 RID: 2448 RVA: 0x000114D0 File Offset: 0x0000F6D0
		protected void SetBaseComponentClassName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._baseComponentClassName = name;
		}

		/// <summary>Registers an external component as part of the load process managed by <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderService" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
		// Token: 0x06000991 RID: 2449 RVA: 0x000114E7 File Offset: 0x0000F6E7
		void IDesignerLoaderService.AddLoadDependency()
		{
			this._dependenciesCount++;
			if (this._dependenciesCount == 0)
			{
				this._dependenciesCount = 1;
				this.OnBeginLoad();
			}
		}

		/// <summary>Signals that a dependent load has finished.</summary>
		/// <param name="successful">true to load successfully; otherwise, false.</param>
		/// <param name="errorCollection">An <see cref="T:System.Collections.ICollection" /> containing errors that occurred during the load.</param>
		/// <exception cref="T:System.InvalidOperationException">No load dependencies have been added by <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.System#ComponentModel#Design#Serialization#IDesignerLoaderService#AddLoadDependency" />, or the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
		// Token: 0x06000992 RID: 2450 RVA: 0x0001150C File Offset: 0x0000F70C
		void IDesignerLoaderService.DependentLoadComplete(bool successful, ICollection errorCollection)
		{
			if (this._dependenciesCount == 0)
			{
				throw new InvalidOperationException("dependencies == 0");
			}
			this._dependenciesCount--;
			if (this._dependenciesCount == 0)
			{
				this.OnEndLoad(successful, errorCollection);
			}
		}

		/// <summary>Reloads the design document.</summary>
		/// <returns>true if the reload request is accepted; false if the loader does not allow the reload.</returns>
		// Token: 0x06000993 RID: 2451 RVA: 0x0001153F File Offset: 0x0000F73F
		bool IDesignerLoaderService.Reload()
		{
			if (this._dependenciesCount == 0)
			{
				this.Reload(BasicDesignerLoader.ReloadOptions.Force);
				return true;
			}
			return false;
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>The requested service, or null if the requested service cannot be found.</returns>
		/// <param name="serviceType">The <see cref="T:System.Type" /> of the service.</param>
		// Token: 0x06000994 RID: 2452 RVA: 0x00011553 File Offset: 0x0000F753
		protected object GetService(Type serviceType)
		{
			if (this._host != null)
			{
				return this._host.GetService(serviceType);
			}
			return null;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader" />.</summary>
		// Token: 0x06000995 RID: 2453 RVA: 0x0001156B File Offset: 0x0000F76B
		public override void Dispose()
		{
			this.LoaderHost.RemoveService(typeof(IDesignerLoaderService));
			this.Unload();
		}

		// Token: 0x04000225 RID: 549
		private bool _loaded;

		// Token: 0x04000226 RID: 550
		private bool _loading;

		// Token: 0x04000227 RID: 551
		private IDesignerLoaderHost _host;

		// Token: 0x04000228 RID: 552
		private int _dependenciesCount;

		// Token: 0x04000229 RID: 553
		private bool _notificationsEnabled;

		// Token: 0x0400022A RID: 554
		private bool _modified;

		// Token: 0x0400022B RID: 555
		private string _baseComponentClassName;

		// Token: 0x0400022C RID: 556
		private DesignerSerializationManager _serializationMananger;

		// Token: 0x0400022D RID: 557
		private bool _flushing;

		// Token: 0x0400022E RID: 558
		private bool _reloadScheduled;

		// Token: 0x0400022F RID: 559
		private BasicDesignerLoader.ReloadOptions _reloadOptions;

		/// <summary>Defines the behavior of the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Reload(System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions)" /> method. These flags can be combined using the bitwise OR operator.</summary>
		// Token: 0x02000140 RID: 320
		[Flags]
		protected enum ReloadOptions
		{
			/// <summary>The designer loader flushes changes before reloading, but it does not force a reload, and it also does not set the <see cref="P:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Modified" /> property to true if load errors occur.</summary>
			// Token: 0x04000231 RID: 561
			Default = 0,
			/// <summary>The designer loader forces the reload to occur. Normally, a reload occurs only if the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.IsReloadNeeded" /> method returns true. This flag bypasses calling this method and always performs the reload.</summary>
			// Token: 0x04000232 RID: 562
			Force = 1,
			/// <summary>The designer loader will set the <see cref="P:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Modified" /> property to true if load errors occur. This flag is useful if you want a flush of the loader to overwrite persistent state that had errors.</summary>
			// Token: 0x04000233 RID: 563
			ModifyOnError = 2,
			/// <summary>The designer loader abandons any changes before reloading.</summary>
			// Token: 0x04000234 RID: 564
			NoFlush = 3
		}
	}
}
