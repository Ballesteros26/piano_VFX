using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	/// <summary>Base designer class for extending the design mode behavior of, and providing a root-level design mode view for, a <see cref="T:System.Windows.Forms.Control" /> that supports nested controls and should receive scroll messages.</summary>
	// Token: 0x0200001B RID: 27
	[ToolboxItemFilter("System.Windows.Forms")]
	public class DocumentDesigner : ScrollableControlDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004567 File Offset: 0x00002767
		private DocumentDesigner.DesignerViewFrame View
		{
			get
			{
				return this._designerViewFrame;
			}
		}

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer. </param>
		// Token: 0x0600010C RID: 268 RVA: 0x00004570 File Offset: 0x00002770
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this._designerViewFrame = new DocumentDesigner.DesignerViewFrame(this.Control, new ComponentTray(this, component.Site));
			this._designerViewFrame.DesignedControl.Location = new Point(15, 15);
			base.SetValue(base.Component, "Location", new Point(0, 0));
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			}
			IMenuCommandService menuCommandService = this.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (menuCommandService != null && serviceContainer != null)
			{
				new DefaultMenuCommands(serviceContainer).AddTo(menuCommandService);
			}
			this.InitializeSelectionService();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004658 File Offset: 0x00002858
		private void InitializeSelectionService()
		{
			if (!(this.GetService(typeof(IUISelectionService)) is IUISelectionService))
			{
				IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
				serviceContainer.AddService(typeof(IUISelectionService), new UISelectionService(serviceContainer));
			}
			(this.GetService(typeof(ISelectionService)) as ISelectionService).SetSelectedComponents(new IComponent[] { base.Component });
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.DocumentDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600010E RID: 270 RVA: 0x000046D4 File Offset: 0x000028D4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._designerViewFrame != null)
				{
					this._designerViewFrame.Dispose();
					this._designerViewFrame = null;
				}
				IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> representing the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
		/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" />  value that specifies the selection state.</param>
		// Token: 0x0600010F RID: 271 RVA: 0x00004742 File Offset: 0x00002942
		public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			return base.GetGlyphs(selectionType);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000110 RID: 272 RVA: 0x0000474B File Offset: 0x0000294B
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>Called when the context menu should be displayed.</summary>
		/// <param name="x">The horizontal screen coordinate to display the context menu at. </param>
		/// <param name="y">The vertical screen coordinate to display the context menu at. </param>
		// Token: 0x06000111 RID: 273 RVA: 0x00004754 File Offset: 0x00002954
		protected override void OnContextMenu(int x, int y)
		{
			base.OnContextMenu(x, y);
		}

		/// <summary>Called immediately after the handle for the designer has been created.</summary>
		// Token: 0x06000112 RID: 274 RVA: 0x0000475E File Offset: 0x0000295E
		protected override void OnCreateHandle()
		{
			base.OnCreateHandle();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004768 File Offset: 0x00002968
		private void OnComponentAdded(object sender, ComponentEventArgs args)
		{
			if (!(args.Component is Control))
			{
				this.View.ComponentTray.AddComponent(args.Component);
				if (this.View.ComponentTray.ComponentCount > 0 && !this.View.ComponentTray.Visible)
				{
					this.View.ShowComponentTray();
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000047C8 File Offset: 0x000029C8
		private void OnComponentRemoved(object sender, ComponentEventArgs args)
		{
			if (!(args.Component is Control))
			{
				this.View.ComponentTray.RemoveComponent(args.Component);
				if (this.View.ComponentTray.ComponentCount == 0 && this.View.ComponentTray.Visible)
				{
					this.View.HideComponentTray();
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="T:System.ComponentModel.Design.ViewTechnology" />.</summary>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		// Token: 0x06000115 RID: 277 RVA: 0x00004827 File Offset: 0x00002A27
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			if (technology != ViewTechnology.Default)
			{
				throw new ArgumentException("Only ViewTechnology.WindowsForms is supported.");
			}
			return this._designerViewFrame;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000483E File Offset: 0x00002A3E
		ViewTechnology[] IRootDesigner.SupportedTechnologies
		{
			get
			{
				return new ViewTechnology[] { ViewTechnology.Default };
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.GetToolSupported(System.Drawing.Design.ToolboxItem)" />.</summary>
		/// <returns>true if the tool is supported by the toolbox and can be enabled; false if the document designer does not know how to use the tool.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
		// Token: 0x06000117 RID: 279 RVA: 0x0000484A File Offset: 0x00002A4A
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return this.GetToolSupported(tool);
		}

		/// <summary>Indicates whether the specified tool is supported by the designer.</summary>
		/// <returns>true if the tool should be enabled on the toolbox; false if the document designer doesn't know how to use the tool.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test for toolbox support. </param>
		// Token: 0x06000118 RID: 280 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.ToolPicked(System.Drawing.Design.ToolboxItem)" />.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
		// Token: 0x06000119 RID: 281 RVA: 0x00004853 File Offset: 0x00002A53
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			this.ToolPicked(tool);
		}

		/// <summary>Selects the specified tool.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component for. </param>
		// Token: 0x0600011A RID: 282 RVA: 0x0000485C File Offset: 0x00002A5C
		protected virtual void ToolPicked(ToolboxItem tool)
		{
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (selectionService != null && designerHost != null)
			{
				IDesigner designer = designerHost.GetDesigner((IComponent)selectionService.PrimarySelection);
				if (designer is ParentControlDesigner)
				{
					ParentControlDesigner.InvokeCreateTool((ParentControlDesigner)designer, tool);
				}
				else
				{
					base.CreateTool(tool);
				}
			}
			else
			{
				base.CreateTool(tool);
			}
			(this.GetService(typeof(IToolboxService)) as IToolboxService).SelectedToolboxItemUsed();
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.SelectionRules" /> for the designer.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.Design.SelectionRules" /> values.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000048ED File Offset: 0x00002AED
		public override SelectionRules SelectionRules
		{
			get
			{
				return SelectionRules.BottomSizeable | SelectionRules.RightSizeable | SelectionRules.Visible;
			}
		}

		/// <summary>Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component. </param>
		// Token: 0x0600011C RID: 284 RVA: 0x000048F4 File Offset: 0x00002AF4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = properties["BackColor"] as PropertyDescriptor;
			if (propertyDescriptor != null)
			{
				properties["BackColor"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), propertyDescriptor, new Attribute[]
				{
					new DefaultValueAttribute(SystemColors.Control)
				});
			}
			propertyDescriptor = properties["Location"] as PropertyDescriptor;
			if (propertyDescriptor != null)
			{
				properties["Location"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), propertyDescriptor, new Attribute[]
				{
					new DefaultValueAttribute(typeof(Point), "0, 0")
				});
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000499B File Offset: 0x00002B9B
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000049B2 File Offset: 0x00002BB2
		private Color BackColor
		{
			get
			{
				return (Color)base.ShadowProperties["BackColor"];
			}
			set
			{
				base.ShadowProperties["BackColor"] = value;
				this.Control.BackColor = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000049D6 File Offset: 0x00002BD6
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000049ED File Offset: 0x00002BED
		private Point Location
		{
			get
			{
				return (Point)base.ShadowProperties["Location"];
			}
			set
			{
				base.ShadowProperties["Location"] = value;
			}
		}

		/// <summary>Checks for the existence of a menu editor service and creates one if one does not already exist.</summary>
		/// <param name="c">The <see cref="T:System.ComponentModel.IComponent" /> to ensure has a context menu service. </param>
		// Token: 0x06000121 RID: 289 RVA: 0x00004A05 File Offset: 0x00002C05
		protected virtual void EnsureMenuEditorService(IComponent c)
		{
			if (this.menuEditorService == null && c is ContextMenu)
			{
				this.menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
			}
		}

		// Token: 0x04000037 RID: 55
		private DocumentDesigner.DesignerViewFrame _designerViewFrame;

		/// <summary>Initializes the menuEditorService variable to null.</summary>
		// Token: 0x04000038 RID: 56
		protected IMenuEditorService menuEditorService;

		// Token: 0x0200001C RID: 28
		public class DesignerViewFrame : UserControl
		{
			// Token: 0x06000122 RID: 290 RVA: 0x00004A34 File Offset: 0x00002C34
			public DesignerViewFrame(Control designedControl, ComponentTray tray)
			{
				if (designedControl == null)
				{
					throw new ArgumentNullException("designedControl");
				}
				if (tray == null)
				{
					throw new ArgumentNullException("tray");
				}
				this.InitializeComponent();
				this._designedControl = designedControl;
				base.SuspendLayout();
				this.DesignerPanel.Controls.Add(designedControl);
				base.ResumeLayout();
				this.ComponentTray = tray;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004A94 File Offset: 0x00002C94
			private void InitializeComponent()
			{
				this.ComponentTrayPanel = new Panel();
				this.splitter1 = new Splitter();
				this.DesignerPanel = new Panel();
				base.SuspendLayout();
				this.ComponentTrayPanel.BackColor = Color.LemonChiffon;
				this.ComponentTrayPanel.Dock = 2;
				this.ComponentTrayPanel.Location = new Point(0, 194);
				this.ComponentTrayPanel.Name = "ComponentTrayPanel";
				this.ComponentTrayPanel.Size = new Size(292, 72);
				this.ComponentTrayPanel.TabIndex = 1;
				this.ComponentTrayPanel.Visible = false;
				this.splitter1.Dock = 2;
				this.splitter1.Location = new Point(0, 186);
				this.splitter1.Name = "splitter1";
				this.splitter1.Size = new Size(292, 8);
				this.splitter1.TabIndex = 2;
				this.splitter1.TabStop = false;
				this.splitter1.Visible = false;
				this.DesignerPanel.AutoScroll = true;
				this.DesignerPanel.BackColor = Color.White;
				this.DesignerPanel.Dock = 5;
				this.DesignerPanel.Location = new Point(0, 0);
				this.DesignerPanel.Name = "DesignerPanel";
				this.DesignerPanel.Size = new Size(292, 266);
				this.DesignerPanel.TabIndex = 0;
				this.DesignerPanel.MouseUp += new MouseEventHandler(this.DesignerPanel_MouseUp);
				this.DesignerPanel.MouseMove += new MouseEventHandler(this.DesignerPanel_MouseMove);
				this.DesignerPanel.MouseDown += new MouseEventHandler(this.DesignerPanel_MouseDown);
				this.DesignerPanel.Paint += new PaintEventHandler(this.DesignerPanel_Paint);
				base.Controls.Add(this.splitter1);
				base.Controls.Add(this.ComponentTrayPanel);
				base.Controls.Add(this.DesignerPanel);
				base.Name = "UserControl1";
				base.Size = new Size(292, 266);
				this.Dock = 5;
				base.ResumeLayout(false);
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00004CD4 File Offset: 0x00002ED4
			private void DesignerPanel_Paint(object sender, PaintEventArgs e)
			{
				IUISelectionService iuiselectionService = this.DesignedControl.Site.GetService(typeof(IUISelectionService)) as IUISelectionService;
				if (iuiselectionService != null)
				{
					iuiselectionService.PaintAdornments(this.DesignerPanel, e.Graphics);
				}
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00004D16 File Offset: 0x00002F16
			private void DesignerPanel_MouseDown(object sender, MouseEventArgs e)
			{
				this._mouseDown = true;
				this._firstMove = true;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00004D28 File Offset: 0x00002F28
			private void DesignerPanel_MouseMove(object sender, MouseEventArgs e)
			{
				IUISelectionService iuiselectionService = this.DesignedControl.Site.GetService(typeof(IUISelectionService)) as IUISelectionService;
				if (iuiselectionService == null)
				{
					return;
				}
				iuiselectionService.SetCursor(e.X, e.Y);
				if (!this._mouseDown)
				{
					if (iuiselectionService.SelectionInProgress)
					{
						iuiselectionService.MouseDragMove(e.X, e.Y);
					}
					return;
				}
				if (this._firstMove)
				{
					iuiselectionService.MouseDragBegin(this.DesignerPanel, e.X, e.Y);
					this._firstMove = false;
					return;
				}
				iuiselectionService.MouseDragMove(e.X, e.Y);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00004DCC File Offset: 0x00002FCC
			private void DesignerPanel_MouseUp(object sender, MouseEventArgs e)
			{
				IUISelectionService iuiselectionService = this.DesignedControl.Site.GetService(typeof(IUISelectionService)) as IUISelectionService;
				if (this._mouseDown)
				{
					if (iuiselectionService != null)
					{
						iuiselectionService.MouseDragEnd(false);
					}
					this._mouseDown = false;
					return;
				}
				if (iuiselectionService.SelectionInProgress)
				{
					iuiselectionService.MouseDragEnd(false);
				}
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00004E22 File Offset: 0x00003022
			public void ShowComponentTray()
			{
				if (!this.ComponentTray.Visible)
				{
					this.ComponentTrayPanel.Visible = true;
					this.ComponentTray.Visible = true;
					this.splitter1.Visible = true;
				}
			}

			// Token: 0x06000129 RID: 297 RVA: 0x00004E22 File Offset: 0x00003022
			public void HideComponentTray()
			{
				if (!this.ComponentTray.Visible)
				{
					this.ComponentTrayPanel.Visible = true;
					this.ComponentTray.Visible = true;
					this.splitter1.Visible = true;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600012A RID: 298 RVA: 0x00004E55 File Offset: 0x00003055
			// (set) Token: 0x0600012B RID: 299 RVA: 0x00004E60 File Offset: 0x00003060
			public ComponentTray ComponentTray
			{
				get
				{
					return this._componentTray;
				}
				set
				{
					base.SuspendLayout();
					this.ComponentTrayPanel.Controls.Remove(this._componentTray);
					this.ComponentTrayPanel.Controls.Add(value);
					base.ResumeLayout();
					this._componentTray = value;
					this._componentTray.Visible = false;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600012C RID: 300 RVA: 0x00004EB3 File Offset: 0x000030B3
			// (set) Token: 0x0600012D RID: 301 RVA: 0x00002432 File Offset: 0x00000632
			public Control DesignedControl
			{
				get
				{
					return this._designedControl;
				}
				set
				{
				}
			}

			// Token: 0x0600012E RID: 302 RVA: 0x00004EBC File Offset: 0x000030BC
			protected override void Dispose(bool disposing)
			{
				if (this._designedControl != null)
				{
					this.DesignerPanel.Controls.Remove(this._designedControl);
					this._designedControl = null;
				}
				if (this._componentTray != null)
				{
					this.ComponentTrayPanel.Controls.Remove(this._componentTray);
					this._componentTray.Dispose();
					this._componentTray = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000039 RID: 57
			private Panel DesignerPanel;

			// Token: 0x0400003A RID: 58
			private Splitter splitter1;

			// Token: 0x0400003B RID: 59
			private Panel ComponentTrayPanel;

			// Token: 0x0400003C RID: 60
			private ComponentTray _componentTray;

			// Token: 0x0400003D RID: 61
			private Control _designedControl;

			// Token: 0x0400003E RID: 62
			private bool _mouseDown;

			// Token: 0x0400003F RID: 63
			private bool _firstMove;
		}
	}
}
