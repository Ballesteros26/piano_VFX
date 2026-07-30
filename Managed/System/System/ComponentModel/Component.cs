using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.IComponent" /> interface and enables object sharing between applications.</summary>
	// Token: 0x02000245 RID: 581
	[DesignerCategory("Component")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		~Component()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value indicating whether the component can raise an event.</summary>
		/// <returns>true if the component can raise events; otherwise, false. The default is true.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000027E2 File Offset: 0x000009E2
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004E720 File Offset: 0x0004C920
		internal bool CanRaiseEventsInternal
		{
			get
			{
				return this.CanRaiseEvents;
			}
		}

		/// <summary>Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose" /> method. </summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060012C5 RID: 4805 RVA: 0x0004E728 File Offset: 0x0004C928
		// (remove) Token: 0x060012C6 RID: 4806 RVA: 0x0004E73B File Offset: 0x0004C93B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(Component.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(Component.EventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004E74E File Offset: 0x0004C94E
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList(this);
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004E76A File Offset: 0x0004C96A
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0004E772 File Offset: 0x0004C972
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
		// Token: 0x060012CA RID: 4810 RVA: 0x0004E77B File Offset: 0x0004C97B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060012CB RID: 4811 RVA: 0x0004E78C File Offset: 0x0004C98C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.site != null && this.site.Container != null)
					{
						this.site.Container.Remove(this);
					}
					if (this.events != null)
					{
						EventHandler eventHandler = (EventHandler)this.events[Component.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0004E818 File Offset: 0x0004CA18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IContainer Container
		{
			get
			{
				ISite site = this.site;
				if (site != null)
				{
					return site.Container;
				}
				return null;
			}
		}

		/// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		/// <param name="service">A service provided by the <see cref="T:System.ComponentModel.Component" />. </param>
		// Token: 0x060012CD RID: 4813 RVA: 0x0004E838 File Offset: 0x0004CA38
		protected virtual object GetService(Type service)
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.ComponentModel.Component" /> is currently in design mode.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.Component" /> is in design mode; otherwise, false.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004E858 File Offset: 0x0004CA58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x060012CF RID: 4815 RVA: 0x0004E878 File Offset: 0x0004CA78
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x04001283 RID: 4739
		private static readonly object EventDisposed = new object();

		// Token: 0x04001284 RID: 4740
		private ISite site;

		// Token: 0x04001285 RID: 4741
		private EventHandlerList events;
	}
}
