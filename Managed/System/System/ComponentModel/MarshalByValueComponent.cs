using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x020002AE RID: 686
	[TypeConverter(typeof(ComponentConverter))]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[ComVisible(true)]
	[DesignerCategory("Component")]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x00053D64 File Offset: 0x00051F64
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Adds an event handler to listen to the <see cref="E:System.ComponentModel.MarshalByValueComponent.Disposed" /> event on the component.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001535 RID: 5429 RVA: 0x00053D94 File Offset: 0x00051F94
		// (remove) Token: 0x06001536 RID: 5430 RVA: 0x00053DA7 File Offset: 0x00051FA7
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(MarshalByValueComponent.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(MarshalByValueComponent.EventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00053DBA File Offset: 0x00051FBA
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00053DD5 File Offset: 0x00051FD5
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x00053DDD File Offset: 0x00051FDD
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

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x0600153A RID: 5434 RVA: 0x00053DE6 File Offset: 0x00051FE6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600153B RID: 5435 RVA: 0x00053DF8 File Offset: 0x00051FF8
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
						EventHandler eventHandler = (EventHandler)this.events[MarshalByValueComponent.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets the container for the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.IContainer" /> interface that represents the component's container, or null if the component does not have a site.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00053E84 File Offset: 0x00052084
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
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

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want. </param>
		// Token: 0x0600153D RID: 5437 RVA: 0x00053EA3 File Offset: 0x000520A3
		public virtual object GetService(Type service)
		{
			if (this.site != null)
			{
				return this.site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value indicating whether the component is currently in design mode.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00053EBC File Offset: 0x000520BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x0600153F RID: 5439 RVA: 0x00053EDC File Offset: 0x000520DC
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x0400132B RID: 4907
		private static readonly object EventDisposed = new object();

		// Token: 0x0400132C RID: 4908
		private ISite site;

		// Token: 0x0400132D RID: 4909
		private EventHandlerList events;
	}
}
