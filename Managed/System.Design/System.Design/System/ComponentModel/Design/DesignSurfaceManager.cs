using System;

namespace System.ComponentModel.Design
{
	/// <summary>Manages a collection of <see cref="T:System.ComponentModel.Design.DesignSurface" /> objects.</summary>
	// Token: 0x0200010A RID: 266
	public class DesignSurfaceManager : IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class.</summary>
		// Token: 0x060007BC RID: 1980 RVA: 0x0000CFDD File Offset: 0x0000B1DD
		public DesignSurfaceManager()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class.</summary>
		/// <param name="parentProvider">A parent service provider. Service requests are forwarded to this provider if they cannot be resolved by the design surface manager.</param>
		// Token: 0x060007BD RID: 1981 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		public DesignSurfaceManager(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
			this.ServiceContainer.AddService(typeof(IDesignerEventService), new DesignerEventService());
		}

		/// <summary>Implementation that creates the design surface.</summary>
		/// <returns>A new design surface instance.</returns>
		/// <param name="parentProvider">A service provider to pass to the design surface. This is either an instance of <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> or an object that implements <see cref="T:System.IServiceProvider" />, and represents a merge between the service provider of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class and an externally passed provider.</param>
		// Token: 0x060007BE RID: 1982 RVA: 0x0000D010 File Offset: 0x0000B210
		protected virtual DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
			DesignSurface designSurface = new DesignSurface(parentProvider);
			this.OnDesignSurfaceCreated(designSurface);
			return designSurface;
		}

		/// <summary>Creates an instance of a design surface.</summary>
		/// <returns>A new design surface instance.</returns>
		// Token: 0x060007BF RID: 1983 RVA: 0x0000D02C File Offset: 0x0000B22C
		public DesignSurface CreateDesignSurface()
		{
			return this.CreateDesignSurfaceCore(this);
		}

		/// <summary>Creates an instance of a design surface.</summary>
		/// <returns>A new design surface instance.</returns>
		/// <param name="parentProvider">A parent service provider. A new merged service provider will be created that will first ask this provider for a service, and then delegate any failures to the design surface manager object. This merged provider will be passed into the <see cref="M:System.ComponentModel.Design.DesignSurfaceManager.CreateDesignSurfaceCore(System.IServiceProvider)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parentProvider" /> is null.</exception>
		// Token: 0x060007C0 RID: 1984 RVA: 0x0000D035 File Offset: 0x0000B235
		public DesignSurface CreateDesignSurface(IServiceProvider parentProvider)
		{
			if (parentProvider == null)
			{
				throw new ArgumentNullException("parentProvider");
			}
			return this.CreateDesignSurfaceCore(new DesignSurfaceManager.MergedServiceProvider(parentProvider, this));
		}

		/// <summary>Gets or sets the active designer.</summary>
		/// <returns>The active designer.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000D054 File Offset: 0x0000B254
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000D09C File Offset: 0x0000B29C
		public virtual DesignSurface ActiveDesignSurface
		{
			get
			{
				DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
				if (designerEventService != null)
				{
					IDesignerHost activeDesigner = designerEventService.ActiveDesigner;
					if (activeDesigner != null)
					{
						return activeDesigner.GetService(typeof(DesignSurface)) as DesignSurface;
					}
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					DesignSurface designSurface = null;
					DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
					if (designerEventService != null)
					{
						IDesignerHost activeDesigner = designerEventService.ActiveDesigner;
						if (activeDesigner != null)
						{
							designSurface = activeDesigner.GetService(typeof(DesignSurface)) as DesignSurface;
						}
					}
					if (designSurface != value)
					{
						ISelectionService selectionService;
						if (designSurface != null)
						{
							selectionService = designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
							if (selectionService != null)
							{
								selectionService.SelectionChanged -= this.OnSelectionChanged;
							}
						}
						selectionService = value.GetService(typeof(ISelectionService)) as ISelectionService;
						if (selectionService != null)
						{
							selectionService.SelectionChanged += this.OnSelectionChanged;
						}
						designerEventService.ActiveDesigner = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (this.ActiveDesignSurfaceChanged != null)
						{
							this.ActiveDesignSurfaceChanged(this, new ActiveDesignSurfaceChangedEventArgs(designSurface, value));
						}
					}
				}
			}
		}

		/// <summary>Gets a collection of design surfaces.</summary>
		/// <returns>A collection of design surfaces that are currently hosted by the design surface manager.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000D184 File Offset: 0x0000B384
		public DesignSurfaceCollection DesignSurfaces
		{
			get
			{
				DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
				if (designerEventService != null)
				{
					return new DesignSurfaceCollection(designerEventService.Designers);
				}
				return new DesignSurfaceCollection(null);
			}
		}

		/// <summary>Gets the design surface manager's <see cref="P:System.ComponentModel.Design.DesignSurfaceManager.ServiceContainer" />.</summary>
		/// <returns>The design surface manager's <see cref="P:System.ComponentModel.Design.DesignSurfaceManager.ServiceContainer" />.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		protected ServiceContainer ServiceContainer
		{
			get
			{
				if (this._serviceContainer == null)
				{
					this._serviceContainer = new ServiceContainer(this._parentProvider);
				}
				return this._serviceContainer;
			}
		}

		/// <summary>Occurs when the global selection changes.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060007C5 RID: 1989 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		// (remove) Token: 0x060007C6 RID: 1990 RVA: 0x0000D218 File Offset: 0x0000B418
		public event EventHandler SelectionChanged;

		/// <summary>Occurs when a designer is disposed.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060007C7 RID: 1991 RVA: 0x0000D250 File Offset: 0x0000B450
		// (remove) Token: 0x060007C8 RID: 1992 RVA: 0x0000D288 File Offset: 0x0000B488
		public event DesignSurfaceEventHandler DesignSurfaceDisposed;

		/// <summary>Occurs when a designer is created.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060007C9 RID: 1993 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		// (remove) Token: 0x060007CA RID: 1994 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		public event DesignSurfaceEventHandler DesignSurfaceCreated;

		/// <summary>Occurs when the currently active designer changes.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060007CB RID: 1995 RVA: 0x0000D330 File Offset: 0x0000B530
		// (remove) Token: 0x060007CC RID: 1996 RVA: 0x0000D368 File Offset: 0x0000B568
		public event ActiveDesignSurfaceChangedEventHandler ActiveDesignSurfaceChanged;

		// Token: 0x060007CD RID: 1997 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		private void OnSelectionChanged(object sender, EventArgs args)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, EventArgs.Empty);
			}
			DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				designerEventService.RaiseSelectionChanged();
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		private void OnDesignSurfaceCreated(DesignSurface surface)
		{
			if (this.DesignSurfaceCreated != null)
			{
				this.DesignSurfaceCreated(this, new DesignSurfaceEventArgs(surface));
			}
			surface.Disposed += this.OnDesignSurfaceDisposed;
			DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				designerEventService.RaiseDesignerCreated(surface.GetService(typeof(IDesignerHost)) as IDesignerHost);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000D458 File Offset: 0x0000B658
		private void OnDesignSurfaceDisposed(object sender, EventArgs args)
		{
			DesignSurface designSurface = (DesignSurface)sender;
			designSurface.Disposed -= this.OnDesignSurfaceDisposed;
			if (this.DesignSurfaceDisposed != null)
			{
				this.DesignSurfaceDisposed(this, new DesignSurfaceEventArgs(designSurface));
			}
			DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				designerEventService.RaiseDesignerDisposed(designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost);
			}
		}

		/// <summary>Gets a service in the design surface manager’s service container.</summary>
		/// <returns>An object that implements, or is a derived class of, the given service type; otherwise, null if the service does not exist in the service container.</returns>
		/// <param name="serviceType">The service type to retrieve.</param>
		// Token: 0x060007D0 RID: 2000 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		public object GetService(Type serviceType)
		{
			if (this._serviceContainer != null)
			{
				return this._serviceContainer.GetService(serviceType);
			}
			return null;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" />.</summary>
		// Token: 0x060007D1 RID: 2001 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060007D2 RID: 2002 RVA: 0x0000D4ED File Offset: 0x0000B6ED
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._serviceContainer != null)
			{
				this._serviceContainer.Dispose();
				this._serviceContainer = null;
			}
		}

		// Token: 0x040001A3 RID: 419
		private IServiceProvider _parentProvider;

		// Token: 0x040001A4 RID: 420
		private ServiceContainer _serviceContainer;

		// Token: 0x0200010B RID: 267
		private class MergedServiceProvider : IServiceProvider
		{
			// Token: 0x060007D3 RID: 2003 RVA: 0x0000D50C File Offset: 0x0000B70C
			public MergedServiceProvider(IServiceProvider primary, IServiceProvider secondary)
			{
				if (primary == null)
				{
					throw new ArgumentNullException("primary");
				}
				if (secondary == null)
				{
					throw new ArgumentNullException("secondary");
				}
				this._primaryProvider = primary;
				this._secondaryProvider = secondary;
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0000D540 File Offset: 0x0000B740
			public object GetService(Type service)
			{
				object obj = this._primaryProvider.GetService(service);
				if (obj == null)
				{
					obj = this._secondaryProvider.GetService(service);
				}
				return obj;
			}

			// Token: 0x040001A9 RID: 425
			private IServiceProvider _primaryProvider;

			// Token: 0x040001AA RID: 426
			private IServiceProvider _secondaryProvider;
		}
	}
}
