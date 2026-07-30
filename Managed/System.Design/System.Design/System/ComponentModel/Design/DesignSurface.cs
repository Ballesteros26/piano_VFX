using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Presents a user interface for designing components.</summary>
	// Token: 0x02000104 RID: 260
	public class DesignSurface : IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000775 RID: 1909 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
		public DesignSurface()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="rootComponentType">The type of root component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000776 RID: 1910 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		public DesignSurface(Type rootComponentType)
			: this(null, rootComponentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="parentProvider">The parent service provider, or null if there is no parent used to resolve services.</param>
		/// <param name="rootComponentType">The type of root component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000777 RID: 1911 RVA: 0x0000C504 File Offset: 0x0000A704
		public DesignSurface(IServiceProvider parentProvider, Type rootComponentType)
			: this(parentProvider)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			this.BeginLoad(rootComponentType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="parentProvider">The parent service provider, or null if there is no parent used to resolve services.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000778 RID: 1912 RVA: 0x0000C528 File Offset: 0x0000A728
		public DesignSurface(IServiceProvider parentProvider)
		{
			this._serviceContainer = new DesignSurfaceServiceContainer(parentProvider);
			this._serviceContainer.AddNonReplaceableService(typeof(IServiceContainer), this._serviceContainer);
			this._designerHost = new DesignerHost(this._serviceContainer);
			this._designerHost.DesignerLoaderHostLoaded += this.OnDesignerHost_Loaded;
			this._designerHost.DesignerLoaderHostLoading += this.OnDesignerHost_Loading;
			this._designerHost.DesignerLoaderHostUnloading += this.OnDesignerHost_Unloading;
			this._designerHost.DesignerLoaderHostUnloaded += this.OnDesignerHost_Unloaded;
			this._designerHost.Activated += this.OnDesignerHost_Activated;
			this._serviceContainer.AddNonReplaceableService(typeof(IComponentChangeService), this._designerHost);
			this._serviceContainer.AddNonReplaceableService(typeof(IDesignerHost), this._designerHost);
			this._serviceContainer.AddNonReplaceableService(typeof(IContainer), this._designerHost);
			this._serviceContainer.AddService(typeof(ITypeDescriptorFilterService), new TypeDescriptorFilterService(this._serviceContainer));
			ExtenderService extenderService = new ExtenderService();
			this._serviceContainer.AddService(typeof(IExtenderProviderService), extenderService);
			this._serviceContainer.AddService(typeof(IExtenderListService), extenderService);
			this._serviceContainer.AddService(typeof(DesignSurface), this);
			SelectionService selectionService = new SelectionService(this._serviceContainer);
			this._serviceContainer.AddService(typeof(ISelectionService), selectionService);
		}

		/// <summary>Gets the service container.</summary>
		/// <returns>The service container that provides all services to designers contained within the design surface.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000C6C1 File Offset: 0x0000A8C1
		protected ServiceContainer ServiceContainer
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				return this._serviceContainer;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		public IContainer ComponentContainer
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				return this._designerHost.Container;
			}
		}

		/// <summary>Gets a value indicating whether the design surface is currently loaded.</summary>
		/// <returns>true if the design surface is currently loaded; otherwise, false.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		/// <summary>Returns a collection of loading errors or a void collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of loading errors.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000C704 File Offset: 0x0000A904
		public ICollection LoadErrors
		{
			get
			{
				if (this._loadErrors == null)
				{
					this._loadErrors = new object[0];
				}
				return this._loadErrors;
			}
		}

		/// <summary>Gets the view for the root designer.</summary>
		/// <returns>The view for the root designer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The design surface is not loading, the designer loader has not yet created a root designer, or the design surface finished the load, but failed. More information may available in the <see cref="P:System.Exception.InnerException" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The designer loaded, but it does not offer a view compatible with this design surface.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000C720 File Offset: 0x0000A920
		public object View
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				if (this._designerHost.RootComponent == null || this.LoadErrors.Count > 0)
				{
					throw new InvalidOperationException("The DesignSurface isn't loaded.");
				}
				IRootDesigner rootDesigner = this._designerHost.GetDesigner(this._designerHost.RootComponent) as IRootDesigner;
				if (rootDesigner == null)
				{
					throw new InvalidOperationException("The DesignSurface isn't loaded.");
				}
				ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
				for (int i = 0; i < supportedTechnologies.Length; i++)
				{
					try
					{
						return rootDesigner.GetView(supportedTechnologies[i]);
					}
					catch
					{
					}
				}
				throw new NotSupportedException("No supported View Technology found.");
			}
		}

		/// <summary>Occurs when the design surface is disposed.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600077E RID: 1918 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		// (remove) Token: 0x0600077F RID: 1919 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public event EventHandler Disposed;

		/// <summary>Occurs when a call is made to the <see cref="M:System.ComponentModel.Design.DesignSurface.Flush" /> method of <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000780 RID: 1920 RVA: 0x0000C844 File Offset: 0x0000AA44
		// (remove) Token: 0x06000781 RID: 1921 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public event EventHandler Flushed;

		/// <summary>Occurs when the designer load has completed.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000782 RID: 1922 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		// (remove) Token: 0x06000783 RID: 1923 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public event LoadedEventHandler Loaded;

		/// <summary>Occurs when the designer is about to be loaded.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000784 RID: 1924 RVA: 0x0000C924 File Offset: 0x0000AB24
		// (remove) Token: 0x06000785 RID: 1925 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public event EventHandler Loading;

		/// <summary>Occurs when a designer has finished unloading.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000786 RID: 1926 RVA: 0x0000C994 File Offset: 0x0000AB94
		// (remove) Token: 0x06000787 RID: 1927 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public event EventHandler Unloaded;

		/// <summary>Occurs when a designer is about to unload.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000788 RID: 1928 RVA: 0x0000CA04 File Offset: 0x0000AC04
		// (remove) Token: 0x06000789 RID: 1929 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public event EventHandler Unloading;

		/// <summary>Occurs when the <see cref="M:System.ComponentModel.Design.IDesignerHost.Activate" /> method has been called on <see cref="T:System.ComponentModel.Design.IDesignerHost" />.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600078A RID: 1930 RVA: 0x0000CA74 File Offset: 0x0000AC74
		// (remove) Token: 0x0600078B RID: 1931 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public event EventHandler ViewActivated;

		/// <summary>Begins the loading process.</summary>
		/// <param name="rootComponentType">The type of component to create in design mode.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponentType" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600078C RID: 1932 RVA: 0x0000CAE1 File Offset: 0x0000ACE1
		public void BeginLoad(Type rootComponentType)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			this.BeginLoad(new DesignSurface.DefaultDesignerLoader(rootComponentType));
		}

		/// <summary>Begins the loading process with the given designer loader.</summary>
		/// <param name="loader">The designer loader to use for loading the designer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="loader" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600078D RID: 1933 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public void BeginLoad(DesignerLoader loader)
		{
			if (loader == null)
			{
				throw new ArgumentNullException("loader");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			if (!this._isLoaded)
			{
				this._loadErrors = null;
				this._designerLoader = loader;
				this.OnLoading(EventArgs.Empty);
				this._designerLoader.BeginLoad(this._designerHost);
			}
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		// Token: 0x0600078E RID: 1934 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600078F RID: 1935 RVA: 0x0000CB84 File Offset: 0x0000AD84
		protected virtual void Dispose(bool disposing)
		{
			if (this._designerLoader != null)
			{
				this._designerLoader.Dispose();
				this._designerLoader = null;
			}
			if (this._designerHost != null)
			{
				this._designerHost.Dispose();
				this._designerHost.DesignerLoaderHostLoaded -= this.OnDesignerHost_Loaded;
				this._designerHost.DesignerLoaderHostLoading -= this.OnDesignerHost_Loading;
				this._designerHost.DesignerLoaderHostUnloading -= this.OnDesignerHost_Unloading;
				this._designerHost.DesignerLoaderHostUnloaded -= this.OnDesignerHost_Unloaded;
				this._designerHost.Activated -= this.OnDesignerHost_Activated;
				this._designerHost = null;
			}
			if (this._serviceContainer != null)
			{
				this._serviceContainer.Dispose();
				this._serviceContainer = null;
			}
			if (this.Disposed != null)
			{
				this.Disposed(this, EventArgs.Empty);
			}
		}

		/// <summary>Serializes changes to the design surface.</summary>
		// Token: 0x06000790 RID: 1936 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		public void Flush()
		{
			if (this._designerLoader != null)
			{
				this._designerLoader.Flush();
			}
			if (this.Flushed != null)
			{
				this.Flushed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		private void OnDesignerHost_Loaded(object sender, LoadedEventArgs e)
		{
			this.OnLoaded(e);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
		private void OnDesignerHost_Loading(object sender, EventArgs e)
		{
			this.OnLoading(EventArgs.Empty);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000CCB2 File Offset: 0x0000AEB2
		private void OnDesignerHost_Unloading(object sender, EventArgs e)
		{
			this.OnUnloading(EventArgs.Empty);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000CCBF File Offset: 0x0000AEBF
		private void OnDesignerHost_Unloaded(object sender, EventArgs e)
		{
			this.OnUnloaded(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loaded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.LoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000795 RID: 1941 RVA: 0x0000CCCC File Offset: 0x0000AECC
		protected virtual void OnLoaded(LoadedEventArgs e)
		{
			this._loadErrors = e.Errors;
			this._isLoaded = e.HasSucceeded;
			if (this.Loaded != null)
			{
				this.Loaded(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loading" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000796 RID: 1942 RVA: 0x0000CCFB File Offset: 0x0000AEFB
		protected virtual void OnLoading(EventArgs e)
		{
			if (this.Loading != null)
			{
				this.Loading(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloaded" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000797 RID: 1943 RVA: 0x0000CD12 File Offset: 0x0000AF12
		protected virtual void OnUnloaded(EventArgs e)
		{
			if (this.Unloaded != null)
			{
				this.Unloaded(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloading" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000798 RID: 1944 RVA: 0x0000CD29 File Offset: 0x0000AF29
		protected virtual void OnUnloading(EventArgs e)
		{
			if (this.Unloading != null)
			{
				this.Unloading(this, e);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000CD40 File Offset: 0x0000AF40
		internal void OnDesignerHost_Activated(object sender, EventArgs args)
		{
			this.OnViewActivate(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.ViewActivated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600079A RID: 1946 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		protected virtual void OnViewActivate(EventArgs e)
		{
			if (this.ViewActivated != null)
			{
				this.ViewActivated(this, e);
			}
		}

		/// <summary>Creates an instance of a component.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentType">The type of component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentType" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079B RID: 1947 RVA: 0x0000CD64 File Offset: 0x0000AF64
		[Obsolete("CreateComponent has been replaced by CreateInstance")]
		protected internal virtual IComponent CreateComponent(Type componentType)
		{
			return this.CreateInstance(componentType) as IComponent;
		}

		/// <summary>Creates an instance of the given type.</summary>
		/// <returns>The newly created object.</returns>
		/// <param name="type">The type to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079C RID: 1948 RVA: 0x0000CD72 File Offset: 0x0000AF72
		protected internal virtual object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._designerHost.CreateComponent(type);
		}

		/// <summary>Creates a designer when a component is added to the container.</summary>
		/// <returns>An instance of the requested designer, or null if no matching designer could be found.</returns>
		/// <param name="component">The component for which the designer should be created.</param>
		/// <param name="rootDesigner">true to create a root designer; false to create a normal designer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079D RID: 1949 RVA: 0x0000CD94 File Offset: 0x0000AF94
		protected internal virtual IDesigner CreateDesigner(IComponent component, bool rootDesigner)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignerSurface");
			}
			return this._designerHost.CreateDesigner(component, rootDesigner);
		}

		/// <summary>Creates a container suitable for nesting controls or components.</summary>
		/// <returns>The nested container.</returns>
		/// <param name="owningComponent">The component that manages the nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owningComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079E RID: 1950 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public INestedContainer CreateNestedContainer(IComponent owningComponent)
		{
			return this.CreateNestedContainer(owningComponent, null);
		}

		/// <summary>Creates a container suitable for nesting controls or components.</summary>
		/// <returns>The nested container.</returns>
		/// <param name="owningComponent">The component that manages the nested container.</param>
		/// <param name="containerName">An additional name for the nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owningComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079F RID: 1951 RVA: 0x0000CDCE File Offset: 0x0000AFCE
		public INestedContainer CreateNestedContainer(IComponent owningComponent, string containerName)
		{
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			return new DesignModeNestedContainer(owningComponent, containerName);
		}

		/// <summary>Gets a service from the service container.</summary>
		/// <returns>An object that implements, or is a derived class of, <paramref name="serviceType" />, or null if the service does not exist in the service container.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x060007A0 RID: 1952 RVA: 0x0000CDEA File Offset: 0x0000AFEA
		public object GetService(Type serviceType)
		{
			if (typeof(IServiceContainer) == serviceType)
			{
				return this._serviceContainer;
			}
			return this._serviceContainer.GetService(serviceType);
		}

		/// <summary>Gets a value indicating whether the Design-time Error List is loading. </summary>
		/// <returns>true if the Design-time Error List is loading; otherwise, false. </returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000CE14 File Offset: 0x0000B014
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00009519 File Offset: 0x00007719
		public bool DtelLoading
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000192 RID: 402
		private DesignerHost _designerHost;

		// Token: 0x04000193 RID: 403
		private DesignSurfaceServiceContainer _serviceContainer;

		// Token: 0x04000194 RID: 404
		private ICollection _loadErrors;

		// Token: 0x04000195 RID: 405
		private bool _isLoaded;

		// Token: 0x04000196 RID: 406
		private DesignerLoader _designerLoader;

		// Token: 0x02000105 RID: 261
		internal class DefaultDesignerLoader : DesignerLoader
		{
			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000CE2F File Offset: 0x0000B02F
			public override bool Loading
			{
				get
				{
					return this._loading;
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0000CE37 File Offset: 0x0000B037
			public DefaultDesignerLoader(Type componentType)
			{
				if (componentType == null)
				{
					throw new ArgumentNullException("componentType");
				}
				this._componentType = componentType;
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0000CE5A File Offset: 0x0000B05A
			public override void BeginLoad(IDesignerLoaderHost loaderHost)
			{
				this._loading = true;
				loaderHost.CreateComponent(this._componentType);
				loaderHost.EndLoad(this._componentType.FullName, true, null);
				this._loading = false;
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0000CE8A File Offset: 0x0000B08A
			public override void Dispose()
			{
				this._componentType = null;
			}

			// Token: 0x0400019E RID: 414
			private Type _componentType;

			// Token: 0x0400019F RID: 415
			private bool _loading;
		}
	}
}
