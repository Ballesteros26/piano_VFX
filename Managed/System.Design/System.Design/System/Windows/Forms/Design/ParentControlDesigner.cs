using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design.Behavior;
using Unity;

namespace System.Windows.Forms.Design
{
	/// <summary>Extends the design mode behavior of a <see cref="T:System.Windows.Forms.Control" /> that supports nested controls.</summary>
	// Token: 0x02000033 RID: 51
	public class ParentControlDesigner : ControlDesigner
	{
		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer. </param>
		// Token: 0x06000195 RID: 405 RVA: 0x000056F8 File Offset: 0x000038F8
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.Control.AllowDrop = true;
			this._defaultDrawGrid = true;
			this._defaultSnapToGrid = true;
			this._defaultGridSize = new Size(8, 8);
			if (this.Control.Parent != null)
			{
				ParentControlDesigner parentControlDesignerOf = this.GetParentControlDesignerOf(this.Control.Parent);
				if (parentControlDesignerOf != null)
				{
					this._defaultDrawGrid = (bool)base.GetValue(parentControlDesignerOf.Component, "DrawGrid");
					this._defaultSnapToGrid = (bool)base.GetValue(parentControlDesignerOf.Component, "SnapToGrid");
					this._defaultGridSize = (Size)base.GetValue(parentControlDesignerOf.Component, "GridSize");
				}
			}
			else
			{
				IDesignerOptionService designerOptionService = this.GetService(typeof(IDesignerOptionService)) as IDesignerOptionService;
				if (designerOptionService != null)
				{
					object obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "DrawGrid");
					if (obj is bool)
					{
						this._defaultDrawGrid = (bool)obj;
					}
					obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "SnapToGrid");
					if (obj is bool)
					{
						this._defaultSnapToGrid = (bool)obj;
					}
					obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "GridSize");
					if (obj is Size)
					{
						this._defaultGridSize = (Size)obj;
					}
				}
			}
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			}
			this._drawGrid = this._defaultDrawGrid;
			this._snapToGrid = this._defaultSnapToGrid;
			this._gridSize = this._defaultGridSize;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000196 RID: 406 RVA: 0x0000589C File Offset: 0x00003A9C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.EnableDragDrop(false);
				this.OnMouseDragEnd(true);
			}
			base.Dispose(disposing);
		}

		/// <summary>Creates a tool from the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" /> that the tool is to be used with. </param>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a tool from. </param>
		// Token: 0x06000197 RID: 407 RVA: 0x000058B6 File Offset: 0x00003AB6
		protected static void InvokeCreateTool(ParentControlDesigner toInvoke, ToolboxItem tool)
		{
			if (toInvoke != null)
			{
				toInvoke.CreateTool(tool);
			}
		}

		/// <summary>Creates a component or control from the specified tool and adds it to the current design document.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from. </param>
		// Token: 0x06000198 RID: 408 RVA: 0x000058C4 File Offset: 0x00003AC4
		protected void CreateTool(ToolboxItem tool)
		{
			this.CreateToolCore(tool, this.DefaultControlLocation.X, this.DefaultControlLocation.Y, 0, 0, true, false);
		}

		/// <summary>Creates a component or control from the specified tool and adds it to the current design document at the specified location.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from. </param>
		/// <param name="location">The <see cref="T:System.Drawing.Point" />, in design-time view screen coordinates, at which to center the component. </param>
		// Token: 0x06000199 RID: 409 RVA: 0x000058F9 File Offset: 0x00003AF9
		protected void CreateTool(ToolboxItem tool, Point location)
		{
			this.CreateToolCore(tool, location.X, location.Y, 0, 0, true, false);
		}

		/// <summary>Creates a component or control from the specified tool and adds it to the current design document within the bounds of the specified rectangle.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from. </param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> indicating the location and size for the component created from the tool. The <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Y" /> values of the <see cref="T:System.Drawing.Rectangle" /> indicate the design-time view screen coordinates of the upper-left corner of the component. </param>
		// Token: 0x0600019A RID: 410 RVA: 0x00005915 File Offset: 0x00003B15
		protected void CreateTool(ToolboxItem tool, Rectangle bounds)
		{
			this.CreateToolCore(tool, bounds.X, bounds.Y, bounds.Width, bounds.Width, true, true);
		}

		/// <summary>Provides core functionality for all the <see cref="M:System.Windows.Forms.Design.ParentControlDesigner.CreateTool(System.Drawing.Design.ToolboxItem)" /> methods.</summary>
		/// <returns>An array of components created from the tool.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from. </param>
		/// <param name="x">The horizontal position, in design-time view coordinates, of the location of the left edge of the tool, if a size is specified; the horizontal position of the center of the tool, if no size is specified. </param>
		/// <param name="y">The vertical position, in design-time view coordinates, of the location of the top edge of the tool, if a size is specified; the vertical position of the center of the tool, if no size is specified. </param>
		/// <param name="width">The width of the tool. This parameter is ignored if the <paramref name="hasSize" /> parameter is set to false. </param>
		/// <param name="height">The height of the tool. This parameter is ignored if the <paramref name="hasSize" /> parameter is set to false. </param>
		/// <param name="hasLocation">true if a location for the component is specified; false if the component is to be positioned in the center of the currently selected control. </param>
		/// <param name="hasSize">true if a size for the component is specified; false if the default height and width values for the component are to be used. </param>
		// Token: 0x0600019B RID: 411 RVA: 0x00005940 File Offset: 0x00003B40
		protected virtual IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			if (tool == null)
			{
				throw new ArgumentNullException("tool");
			}
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DesignerTransaction designerTransaction = designerHost.CreateTransaction("Create components in tool '" + tool.DisplayName + "'");
			IComponent[] array = tool.CreateComponents(designerHost);
			foreach (IComponent component in array)
			{
				ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
				if (controlDesigner != null)
				{
					if (!this.CanParent(controlDesigner))
					{
						designerHost.DestroyComponent(component);
					}
					else
					{
						Control control = component as Control;
						if (control != null)
						{
							this.Control.SuspendLayout();
							TypeDescriptor.GetProperties(control)["Parent"].SetValue(control, this.Control);
							this.Control.SuspendLayout();
							if (hasLocation)
							{
								base.SetValue(component, "Location", this.SnapPointToGrid(new Point(x, y)));
							}
							else
							{
								base.SetValue(component, "Location", this.SnapPointToGrid(this.DefaultControlLocation));
							}
							if (hasSize)
							{
								base.SetValue(component, "Size", new Size(width, height));
							}
							this.Control.Refresh();
						}
					}
				}
			}
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(array, SelectionTypes.Replace);
			}
			designerTransaction.Commit();
			return array;
		}

		/// <summary>Indicates whether the specified control can be a child of the control managed by this designer.</summary>
		/// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to test. </param>
		// Token: 0x0600019C RID: 412 RVA: 0x00005ABE File Offset: 0x00003CBE
		public virtual bool CanParent(Control control)
		{
			return control != null && !control.Contains(this.Control);
		}

		/// <summary>Indicates whether the control managed by the specified designer can be a child of the control managed by this designer.</summary>
		/// <returns>true if the control managed by the specified designer can be a child of the control managed by this designer; otherwise, false.</returns>
		/// <param name="controlDesigner">The designer for the control to test. </param>
		// Token: 0x0600019D RID: 413 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public virtual bool CanParent(ControlDesigner controlDesigner)
		{
			return this.CanParent(controlDesigner.Control);
		}

		/// <summary>Called when a drag-and-drop object is dropped onto the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600019E RID: 414 RVA: 0x00005AE4 File Offset: 0x00003CE4
		protected override void OnDragDrop(DragEventArgs de)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				Point point = this.SnapPointToGrid(this.Control.PointToClient(new Point(de.X, de.Y)));
				iuiselectionService.DragDrop(false, this.Control, point.X, point.Y);
			}
		}

		/// <summary>Called when a drag-and-drop operation enters the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x0600019F RID: 415 RVA: 0x00005B48 File Offset: 0x00003D48
		protected override void OnDragEnter(DragEventArgs de)
		{
			this.Control.Refresh();
		}

		/// <summary>Called when a drag-and-drop operation leaves the control designer view.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event. </param>
		// Token: 0x060001A0 RID: 416 RVA: 0x00005B48 File Offset: 0x00003D48
		protected override void OnDragLeave(EventArgs e)
		{
			this.Control.Refresh();
		}

		/// <summary>Called when a drag-and-drop object is dragged over the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x060001A1 RID: 417 RVA: 0x00005B58 File Offset: 0x00003D58
		protected override void OnDragOver(DragEventArgs de)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				Point point = this.SnapPointToGrid(this.Control.PointToClient(new Point(de.X, de.Y)));
				iuiselectionService.DragOver(this.Control, point.X, point.Y);
			}
			de.Effect = 2;
		}

		/// <summary>Gets the default location for a control added to the designer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that indicates the default location for a control added to the designer.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00002434 File Offset: 0x00000634
		protected virtual Point DefaultControlLocation
		{
			get
			{
				return new Point(0, 0);
			}
		}

		/// <summary>Gets a value indicating whether drag rectangles are drawn by the designer.</summary>
		/// <returns>true if drag rectangles are drawn; otherwise, false. The default is true.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000023D8 File Offset: 0x000005D8
		protected override bool EnableDragRect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005BC4 File Offset: 0x00003DC4
		private void OnComponentRemoving(object sender, ComponentEventArgs args)
		{
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			Control control = args.Component as Control;
			if (control != null && control.Parent == this.Control && componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(args.Component, TypeDescriptor.GetProperties(args.Component)["Parent"]);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005C28 File Offset: 0x00003E28
		private void OnComponentRemoved(object sender, ComponentEventArgs args)
		{
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			Control control = args.Component as Control;
			if (control != null && control.Parent == this.Control && componentChangeService != null)
			{
				control.Parent = null;
				componentChangeService.OnComponentChanged(args.Component, TypeDescriptor.GetProperties(args.Component)["Parent"], this.Control, null);
			}
		}

		/// <summary>Adjusts the set of properties the component will expose through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component. </param>
		// Token: 0x060001A6 RID: 422 RVA: 0x00005C9C File Offset: 0x00003E9C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties["DrawGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "DrawGrid", typeof(bool), new Attribute[]
			{
				BrowsableAttribute.Yes,
				DesignOnlyAttribute.Yes,
				new DescriptionAttribute("Indicates whether or not to draw the positioning grid."),
				CategoryAttribute.Design
			});
			properties["SnapToGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "SnapToGrid", typeof(bool), new Attribute[]
			{
				BrowsableAttribute.Yes,
				DesignOnlyAttribute.Yes,
				new DescriptionAttribute("Determines if controls should snap to the positioning grid."),
				CategoryAttribute.Design
			});
			properties["GridSize"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "GridSize", typeof(Size), new Attribute[]
			{
				BrowsableAttribute.Yes,
				DesignOnlyAttribute.Yes,
				new DescriptionAttribute("Determines the size of the positioning grid."),
				CategoryAttribute.Design
			});
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005DAC File Offset: 0x00003FAC
		private void PopulateGridProperties()
		{
			this.Control.Invalidate(false);
			if (this.Control != null)
			{
				foreach (object obj in this.Control.Controls)
				{
					Control control = (Control)obj;
					ParentControlDesigner parentControlDesignerOf = this.GetParentControlDesignerOf(control);
					if (parentControlDesignerOf != null)
					{
						parentControlDesignerOf.OnParentGridPropertiesChanged(this);
					}
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005E2C File Offset: 0x0000402C
		private void OnParentGridPropertiesChanged(ParentControlDesigner parentDesigner)
		{
			base.SetValue(base.Component, "DrawGrid", (bool)base.GetValue(parentDesigner.Component, "DrawGrid"));
			base.SetValue(base.Component, "SnapToGrid", (bool)base.GetValue(parentDesigner.Component, "SnapToGrid"));
			base.SetValue(base.Component, "GridSize", (Size)base.GetValue(parentDesigner.Component, "GridSize"));
			this._defaultDrawGrid = (bool)base.GetValue(parentDesigner.Component, "DrawGrid");
			this._defaultSnapToGrid = (bool)base.GetValue(parentDesigner.Component, "SnapToGrid");
			this._defaultGridSize = (Size)base.GetValue(parentDesigner.Component, "GridSize");
			this.PopulateGridProperties();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005F18 File Offset: 0x00004118
		private ParentControlDesigner GetParentControlDesignerOf(Control control)
		{
			if (control != null)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(this.Control.Parent) as ParentControlDesigner;
					if (parentControlDesigner != null)
					{
						return parentControlDesigner;
					}
				}
			}
			return null;
		}

		/// <summary>Gets or sets a value indicating whether a grid should be drawn on the control for this designer.</summary>
		/// <returns>true if a grid should be drawn on the control in the designer; otherwise, false.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005F60 File Offset: 0x00004160
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005F68 File Offset: 0x00004168
		protected virtual bool DrawGrid
		{
			get
			{
				return this._drawGrid;
			}
			set
			{
				this._drawGrid = value;
				if (!value)
				{
					base.SetValue(base.Component, "SnapToGrid", false);
				}
				this.PopulateGridProperties();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005F91 File Offset: 0x00004191
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005F99 File Offset: 0x00004199
		private bool SnapToGrid
		{
			get
			{
				return this._snapToGrid;
			}
			set
			{
				this._snapToGrid = value;
				this.PopulateGridProperties();
			}
		}

		/// <summary>Gets or sets the size of each square of the grid that is drawn when the designer is in grid draw mode.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of each square of the grid drawn on a form or user control.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="T:System.Drawing.Size" /> is outside the allowed range for <see cref="P:System.Windows.Forms.Design.ParentControlDesigner.GridSize" />. The default minimum value is 2, and the default maximum value is 200. </exception>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005FA8 File Offset: 0x000041A8
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005FB0 File Offset: 0x000041B0
		protected Size GridSize
		{
			get
			{
				return this._gridSize;
			}
			set
			{
				this._gridSize = value;
				this.PopulateGridProperties();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005FBF File Offset: 0x000041BF
		private bool ShouldSerializeDrawGrid()
		{
			return this.DrawGrid != this._defaultDrawGrid;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005FD2 File Offset: 0x000041D2
		private void ResetDrawGrid()
		{
			this.DrawGrid = this._defaultDrawGrid;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005FE0 File Offset: 0x000041E0
		private bool ShouldSerializeSnapToGrid()
		{
			return this._drawGrid != this._defaultDrawGrid;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005FF3 File Offset: 0x000041F3
		private void ResetSnapToGrid()
		{
			this.SnapToGrid = this._defaultSnapToGrid;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006001 File Offset: 0x00004201
		private bool ShouldSerializeGridSize()
		{
			return this.GridSize != this._defaultGridSize;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006014 File Offset: 0x00004214
		private void ResetGridSize()
		{
			this.GridSize = this._defaultGridSize;
		}

		/// <summary>Called in response to the left mouse button being pressed and held while over the component.</summary>
		/// <param name="x">The x-coordinate of the mouse in screen coordinates. </param>
		/// <param name="y">The y-coordinate of the mouse in screen coordinates. </param>
		// Token: 0x060001B6 RID: 438 RVA: 0x00006024 File Offset: 0x00004224
		protected override void OnMouseDragBegin(int x, int y)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				Point point = new Point(x, y);
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (base.MouseButtonDown == 4194304 && designerHost != null && designerHost.RootComponent != this.Control)
				{
					point = this.Control.Parent.PointToClient(this.Control.PointToScreen(new Point(x, y)));
					this.Control.AllowDrop = false;
					iuiselectionService.DragBegin();
					return;
				}
				iuiselectionService.MouseDragBegin(this.Control, point.X, point.Y);
			}
		}

		/// <summary>Called for each movement of the mouse during a drag-and-drop operation.</summary>
		/// <param name="x">The x-coordinate of the mouse in screen coordinates. </param>
		/// <param name="y">The y-coordinate of the mouse in screen coordinates. </param>
		// Token: 0x060001B7 RID: 439 RVA: 0x000060DC File Offset: 0x000042DC
		protected override void OnMouseDragMove(int x, int y)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				Point point = new Point(x, y);
				if (!iuiselectionService.SelectionInProgress)
				{
					point = this.SnapPointToGrid(new Point(x, y));
				}
				iuiselectionService.MouseDragMove(point.X, point.Y);
			}
		}

		/// <summary>Called at the end of a drag-and-drop operation to complete or cancel the operation.</summary>
		/// <param name="cancel">true to cancel the drag operation; false to commit it. </param>
		// Token: 0x060001B8 RID: 440 RVA: 0x00006138 File Offset: 0x00004338
		protected override void OnMouseDragEnd(bool cancel)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				IToolboxService toolboxService = this.GetService(typeof(IToolboxService)) as IToolboxService;
				if (!cancel && toolboxService != null && toolboxService.GetSelectedToolboxItem() != null)
				{
					if (iuiselectionService.SelectionInProgress)
					{
						bool flag = iuiselectionService.SelectionBounds.Width > 0 && iuiselectionService.SelectionBounds.Height > 0;
						this.CreateToolCore(toolboxService.GetSelectedToolboxItem(), iuiselectionService.SelectionBounds.X, iuiselectionService.SelectionBounds.Y, iuiselectionService.SelectionBounds.Width, iuiselectionService.SelectionBounds.Height, true, flag);
						toolboxService.SelectedToolboxItemUsed();
						cancel = true;
					}
					else if (!iuiselectionService.SelectionInProgress && !iuiselectionService.ResizeInProgress && !iuiselectionService.DragDropInProgress)
					{
						this.CreateTool(toolboxService.GetSelectedToolboxItem(), this._mouseDownPoint);
						toolboxService.SelectedToolboxItemUsed();
						cancel = true;
					}
				}
				if (iuiselectionService.SelectionInProgress || iuiselectionService.ResizeInProgress)
				{
					iuiselectionService.MouseDragEnd(cancel);
				}
			}
		}

		/// <summary>Called in order to clean up a drag-and-drop operation.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
		// Token: 0x060001B9 RID: 441 RVA: 0x00006258 File Offset: 0x00004458
		protected override void OnDragComplete(DragEventArgs de)
		{
			base.OnDragComplete(de);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006261 File Offset: 0x00004461
		internal override void OnMouseDown(int x, int y)
		{
			this._mouseDownPoint.X = x;
			this._mouseDownPoint.Y = y;
			base.OnMouseDown(x, y);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006283 File Offset: 0x00004483
		internal override void OnMouseUp()
		{
			base.OnMouseUp();
			if (!this.Control.AllowDrop)
			{
				this.Control.AllowDrop = true;
			}
			this._mouseDownPoint = Point.Empty;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000062B0 File Offset: 0x000044B0
		internal override void OnMouseMove(int x, int y)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				iuiselectionService.SetCursor(x, y);
			}
			base.OnMouseMove(x, y);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000062E8 File Offset: 0x000044E8
		private Point SnapPointToGrid(Point location)
		{
			Rectangle bounds = this.Control.Bounds;
			Size size = (Size)base.GetValue(base.Component, "GridSize");
			if ((bool)base.GetValue(base.Component, "SnapToGrid"))
			{
				int num = location.X + (size.Width - location.X % size.Width);
				if (num > bounds.Width)
				{
					num = bounds.Width - size.Width;
				}
				location.X = num;
				int num2 = location.Y + (size.Height - location.Y % size.Height);
				if (num2 > bounds.Height)
				{
					num2 = bounds.Height - size.Height;
				}
				location.Y = num2;
			}
			return location;
		}

		/// <summary>Provides an opportunity to change the current mouse cursor.</summary>
		// Token: 0x060001BE RID: 446 RVA: 0x000063B8 File Offset: 0x000045B8
		protected override void OnSetCursor()
		{
			if (this.Control != null)
			{
				IToolboxService toolboxService = this.GetService(typeof(IToolboxService)) as IToolboxService;
				if (toolboxService != null)
				{
					toolboxService.SetCursor();
					return;
				}
				base.OnSetCursor();
			}
		}

		/// <summary>Called when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that provides data for the event. </param>
		// Token: 0x060001BF RID: 447 RVA: 0x000063F4 File Offset: 0x000045F4
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments(pe);
			bool flag;
			try
			{
				flag = (bool)base.GetValue(base.Component, "DrawGrid");
			}
			catch
			{
				flag = this.DrawGrid;
			}
			Size size;
			try
			{
				size = (Size)base.GetValue(base.Component, "GridSize");
			}
			catch
			{
				size = this.GridSize;
			}
			if (flag)
			{
				GraphicsState graphicsState = pe.Graphics.Save();
				pe.Graphics.TranslateTransform((float)this.Control.ClientRectangle.X, (float)this.Control.ClientRectangle.Y);
				ControlPaint.DrawGrid(pe.Graphics, this.Control.ClientRectangle, size, this.Control.BackColor);
				pe.Graphics.Restore(graphicsState);
			}
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null)
			{
				iuiselectionService.PaintAdornments(this.Control, pe.Graphics);
			}
		}

		/// <summary>Gets the control from the designer of the specified component.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the specified component belongs to.</returns>
		/// <param name="component">The component to retrieve the control for. </param>
		// Token: 0x060001C0 RID: 448 RVA: 0x00006508 File Offset: 0x00004708
		protected Control GetControl(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				IDesignerHost designerHost = component2.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(component2) as ControlDesigner;
					if (controlDesigner != null)
					{
						return controlDesigner.Control;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a value indicating whether selected controls will be re-parented.</summary>
		/// <returns>true if the controls that were selected by lassoing on the designer's surface will be re-parented to this designer's control.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000241E File Offset: 0x0000061E
		[MonoTODO]
		protected virtual bool AllowControlLasso
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a generic drag box should be drawn when dragging a toolbox item over the designer's surface.</summary>
		/// <returns>true if a generic drag box should be drawn when dragging a toolbox item over the designer's surface; otherwise, false. The default is true.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000241E File Offset: 0x0000061E
		[MonoTODO]
		protected virtual bool AllowGenericDragBox
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the z-order of dragged controls should be maintained when dropped on a <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />.</summary>
		/// <returns>true if the z-order of dragged controls should be maintained when dropped on a <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />; otherwise, false.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000241E File Offset: 0x0000061E
		[MonoTODO]
		protected internal virtual bool AllowSetChildIndexOnDrop
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control. </summary>
		/// <returns>A list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000655D File Offset: 0x0000475D
		[MonoTODO]
		public override IList SnapLines
		{
			get
			{
				return new object[0];
			}
		}

		/// <summary>Gets a value indicating whether the designer has a valid tool during a drag operation. </summary>
		/// <returns>The tool being dragged, if creating a component, or null if there is no tool.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000256A File Offset: 0x0000076A
		[MonoTODO]
		protected ToolboxItem MouseDragTool
		{
			get
			{
				return null;
			}
		}

		/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be null if no default values are specified.</param>
		// Token: 0x060001C6 RID: 454 RVA: 0x00006565 File Offset: 0x00004765
		[MonoTODO]
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
		}

		/// <summary>Adds padding snaplines.</summary>
		/// <param name="snapLines">An <see cref="T:System.Collections.ArrayList" /> that contains <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects.</param>
		// Token: 0x060001C7 RID: 455 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected void AddPaddingSnapLines(ref ArrayList snapLines)
		{
			throw new NotImplementedException();
		}

		/// <summary>Used by deriving classes to determine if it returns the control being designed or some other <see cref="T:System.ComponentModel.Container" /> while adding a component to it.</summary>
		/// <returns>The parent <see cref="T:System.Windows.Forms.Control" /> for the component.</returns>
		/// <param name="component">The component for which to retrieve the parent <see cref="T:System.Windows.Forms.Control" />.</param>
		// Token: 0x060001C8 RID: 456 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual Control GetParentForComponent(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a body glyph that represents the bounds of the control. </summary>
		/// <returns>A body glyph that represents the bounds of the control.</returns>
		/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" />  value that specifies the selection state.</param>
		// Token: 0x060001C9 RID: 457 RVA: 0x0000656E File Offset: 0x0000476E
		[MonoTODO]
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			return base.GetControlGlyph(selectionType);
		}

		/// <summary>Gets a collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects representing the selection borders and grab handles for a standard control.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
		/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" />  value that specifies the selection state.</param>
		// Token: 0x060001CA RID: 458 RVA: 0x00006577 File Offset: 0x00004777
		[MonoTODO]
		public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			return base.GetGlyphs(selectionType);
		}

		/// <summary>Updates the position of the specified rectangle, adjusting it for grid alignment if grid alignment mode is enabled.</summary>
		/// <returns>A rectangle indicating the position of the component in design-time view screen coordinates. If no changes have been made, this method returns the original rectangle.</returns>
		/// <param name="originalRect">A <see cref="T:System.Drawing.Rectangle" /> indicating the initial position of the component being updated. </param>
		/// <param name="dragRect">A <see cref="T:System.Drawing.Rectangle" /> indicating the new position of the component. </param>
		/// <param name="updateSize">true to update the size of the rectangle, if there has been any change; otherwise, false. </param>
		// Token: 0x060001CB RID: 459 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when a component is added to the parent container.</summary>
		/// <returns>true if <paramref name="component" /> can be added; otherwise, false. </returns>
		/// <param name="component">The component to test for errors. </param>
		// Token: 0x060001CC RID: 460 RVA: 0x00006580 File Offset: 0x00004780
		protected internal virtual bool CanAddComponent(IComponent component)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x040000BE RID: 190
		private bool _defaultDrawGrid;

		// Token: 0x040000BF RID: 191
		private bool _defaultSnapToGrid;

		// Token: 0x040000C0 RID: 192
		private Size _defaultGridSize;

		// Token: 0x040000C1 RID: 193
		private bool _drawGrid;

		// Token: 0x040000C2 RID: 194
		private bool _snapToGrid;

		// Token: 0x040000C3 RID: 195
		private Size _gridSize;

		// Token: 0x040000C4 RID: 196
		private Point _mouseDownPoint = Point.Empty;
	}
}
