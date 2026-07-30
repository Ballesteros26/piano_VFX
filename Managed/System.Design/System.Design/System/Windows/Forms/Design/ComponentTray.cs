using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides behavior for the component tray of a designer. </summary>
	// Token: 0x0200000B RID: 11
	[ProvideProperty("Location", typeof(IComponent))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class ComponentTray : ScrollableControl, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentTray" /> class using the specified designer and service provider.</summary>
		/// <param name="mainDesigner">The <see cref="T:System.ComponentModel.Design.IDesigner" /> that is the main or document designer for the current project. </param>
		/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> that can be used to obtain design-time services. </param>
		// Token: 0x06000044 RID: 68 RVA: 0x000023DB File Offset: 0x000005DB
		public ComponentTray(IDesigner mainDesigner, IServiceProvider serviceProvider)
		{
			if (mainDesigner == null)
			{
				throw new ArgumentNullException("mainDesigner");
			}
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._mainDesigner = mainDesigner;
			this._serviceProvider = serviceProvider;
		}

		/// <summary>Gets or sets a value indicating whether the tray items are automatically aligned.</summary>
		/// <returns>true if the tray items are automatically arranged; otherwise, false.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000240D File Offset: 0x0000060D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002415 File Offset: 0x00000615
		public bool AutoArrange
		{
			get
			{
				return this._autoArrange;
			}
			set
			{
				this._autoArrange = value;
			}
		}

		/// <summary>Gets the number of components contained in the tray.</summary>
		/// <returns>The number of components in the tray.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000241E File Offset: 0x0000061E
		[MonoTODO]
		public int ComponentCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the tray displays a large icon to represent each component in the tray.</summary>
		/// <returns>true if large icons are displayed; otherwise, false.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002429 File Offset: 0x00000629
		public bool ShowLargeIcons
		{
			get
			{
				return this._showLargeIcons;
			}
			set
			{
				this._showLargeIcons = value;
			}
		}

		/// <summary>Adds a component to the tray.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add to the tray. </param>
		// Token: 0x0600004A RID: 74 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public virtual void AddComponent(IComponent component)
		{
		}

		/// <summary>Gets a value indicating whether the specified tool can be used to create a new component.</summary>
		/// <returns>true if the specified tool can be used to create a component; otherwise, false.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test. </param>
		// Token: 0x0600004B RID: 75 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool CanCreateComponentFromTool(ToolboxItem tool)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether the specified component can be displayed.</summary>
		/// <returns>true if the component can be displayed; otherwise, false.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to test. </param>
		// Token: 0x0600004C RID: 76 RVA: 0x0000241E File Offset: 0x0000061E
		protected virtual bool CanDisplayComponent(IComponent component)
		{
			return false;
		}

		/// <summary>Creates a component from the specified toolbox item, adds the component to the current document, and displays a representation for the component in the component tray.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from. </param>
		// Token: 0x0600004D RID: 77 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public void CreateComponentFromTool(ToolboxItem tool)
		{
		}

		/// <summary>Displays an error message to the user with information about the specified exception.</summary>
		/// <param name="e">The exception about which to display information. </param>
		// Token: 0x0600004E RID: 78 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected void DisplayError(Exception e)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ComponentTray" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600004F RID: 79 RVA: 0x00002432 File Offset: 0x00000632
		protected override void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the location of the specified component, relative to the upper-left corner of the component tray.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> indicating the coordinates of the specified component, or an empty <see cref="T:System.Drawing.Point" /> if the specified component could not be found in the component tray. An empty <see cref="T:System.Drawing.Point" /> has an <see cref="P:System.Drawing.Point.IsEmpty" /> property equal to true and typically has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties that are each equal to zero.</returns>
		/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the location of. </param>
		// Token: 0x06000050 RID: 80 RVA: 0x00002434 File Offset: 0x00000634
		[Browsable(false)]
		[Category("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DesignOnly(true)]
		[Localizable(false)]
		[MonoTODO]
		public Point GetLocation(IComponent receiver)
		{
			return new Point(0, 0);
		}

		/// <summary>Sets the location of the specified component to the specified location.</summary>
		/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> to set the location of. </param>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> indicating the new location for the specified component. </param>
		// Token: 0x06000051 RID: 81 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public void SetLocation(IComponent receiver, Point location)
		{
		}

		/// <summary>Similar to <see cref="M:System.Windows.Forms.Control.GetNextControl(System.Windows.Forms.Control,System.Boolean)" />, this method returns the next component in the tray, given a starting component.</summary>
		/// <returns>The next component in the component tray, or null, if the end of the list is encountered (or the beginning, if <paramref name="forward" /> is false).</returns>
		/// <param name="component">The component from which to start enumerating.</param>
		/// <param name="forward">true to enumerate forward through the list; otherwise, false to enumerate backward.</param>
		// Token: 0x06000052 RID: 82 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IComponent GetNextComponent(IComponent component, bool forward)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the Location extender property.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> representing the location of <paramref name="receiver" />. </returns>
		/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> that receives the location extender property.</param>
		// Token: 0x06000053 RID: 83 RVA: 0x0000234B File Offset: 0x0000054B
		[Localizable(false)]
		[DesignOnly(true)]
		[Category("Layout")]
		[MonoTODO]
		[Browsable(false)]
		public Point GetTrayLocation(IComponent receiver)
		{
			throw new NotImplementedException();
		}

		/// <summary>Tests a component for presence in the component tray.</summary>
		/// <returns>true if <paramref name="comp" /> is being shown on the tray; otherwise, false.</returns>
		/// <param name="comp">The component to test for presence in the component tray.</param>
		// Token: 0x06000054 RID: 84 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool IsTrayComponent(IComponent comp)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the value of the Location extender property.</summary>
		/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> that receives the location extender property.</param>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> representing the location of <paramref name="receiver" />. </param>
		// Token: 0x06000055 RID: 85 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SetTrayLocation(IComponent receiver, Point location)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the mouse is double clicked over the component tray.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event.</param>
		// Token: 0x06000056 RID: 86 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
		}

		/// <summary>Called when an object that has been dragged is dropped on the component tray.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000057 RID: 87 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnDragDrop(DragEventArgs de)
		{
		}

		/// <summary>Called when an object is dragged over, and has entered the area over, the component tray.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000058 RID: 88 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnDragEnter(DragEventArgs de)
		{
		}

		/// <summary>Called when an object is dragged out of the area over the component tray.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event. </param>
		// Token: 0x06000059 RID: 89 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnDragLeave(EventArgs e)
		{
		}

		/// <summary>Called when an object is dragged over the component tray.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600005A RID: 90 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnDragOver(DragEventArgs de)
		{
		}

		/// <summary>Called during an OLE drag and drop operation to provide an opportunity for the component tray to give feedback to the user about the results of dropping the object at a specific point.</summary>
		/// <param name="gfevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600005B RID: 91 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnGiveFeedback(GiveFeedbackEventArgs gfevent)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x0600005C RID: 92 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnLayout(LayoutEventArgs levent)
		{
		}

		/// <summary>Called when a mouse drag selection operation is canceled.</summary>
		// Token: 0x0600005D RID: 93 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected virtual void OnLostCapture()
		{
		}

		/// <summary>Called when the mouse button is pressed.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600005E RID: 94 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnMouseDown(MouseEventArgs e)
		{
		}

		/// <summary>Called when the mouse cursor position has changed.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600005F RID: 95 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Called when the mouse button has been released.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000060 RID: 96 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnMouseUp(MouseEventArgs e)
		{
		}

		/// <summary>Called when the view for the component tray should be refreshed.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000061 RID: 97 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected override void OnPaint(PaintEventArgs pe)
		{
		}

		/// <summary>Called to set the mouse cursor.</summary>
		// Token: 0x06000062 RID: 98 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		protected virtual void OnSetCursor()
		{
		}

		/// <summary>Removes the specified component from the tray.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove from the tray. </param>
		// Token: 0x06000063 RID: 99 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public virtual void RemoveComponent(IComponent component)
		{
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000064 RID: 100 RVA: 0x0000243D File Offset: 0x0000063D
		[MonoTODO]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		// Token: 0x06000065 RID: 101 RVA: 0x0000241E File Offset: 0x0000061E
		bool IExtenderProvider.CanExtend(object component)
		{
			return false;
		}

		/// <summary>Gets the requested service type.</summary>
		/// <returns>An instance of the requested service, or null if the service could not be found.</returns>
		/// <param name="serviceType">The type of the service to retrieve. </param>
		// Token: 0x06000066 RID: 102 RVA: 0x00002446 File Offset: 0x00000646
		protected override object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0400001A RID: 26
		private IServiceProvider _serviceProvider;

		// Token: 0x0400001B RID: 27
		private IDesigner _mainDesigner;

		// Token: 0x0400001C RID: 28
		private bool _showLargeIcons;

		// Token: 0x0400001D RID: 29
		private bool _autoArrange;
	}
}
