using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides a user interface for browsing the properties of an object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200029D RID: 669
	[Designer("System.Windows.Forms.Design.PropertyGridDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public class PropertyGrid : ContainerControl, IComPropertyBrowser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyGrid" /> class.</summary>
		// Token: 0x06002C7C RID: 11388 RVA: 0x000ABC58 File Offset: 0x000A9E58
		public PropertyGrid()
		{
			this.selected_objects = new object[0];
			this.property_tabs = new PropertyGrid.PropertyTabCollection(this);
			this.line_color = SystemColors.ScrollBar;
			this.category_fore_color = this.line_color;
			this.commands_visible = false;
			this.commands_visible_if_available = false;
			this.property_sort = PropertySort.CategorizedAlphabetical;
			this.property_grid_view = new PropertyGridView(this);
			this.splitter = new Splitter();
			this.splitter.Dock = DockStyle.Bottom;
			this.help_panel = new Panel();
			this.help_panel.Dock = DockStyle.Bottom;
			this.help_panel.Height = 50;
			this.help_panel.BackColor = SystemColors.Control;
			this.help_title_label = new Label();
			this.help_title_label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.help_title_label.Name = "help_title_label";
			this.help_title_label.Font = new Font(this.Font, 1);
			this.help_title_label.Location = new Point(2, 2);
			this.help_title_label.Height = 17;
			this.help_title_label.Width = this.help_panel.Width - 4;
			this.help_description_label = new Label();
			this.help_description_label.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.help_description_label.AutoEllipsis = true;
			this.help_description_label.AutoSize = false;
			this.help_description_label.Font = this.Font;
			this.help_description_label.Location = new Point(2, this.help_title_label.Top + this.help_title_label.Height);
			this.help_description_label.Width = this.help_panel.Width - 4;
			this.help_description_label.Height = this.help_panel.Height - this.help_description_label.Top - 2;
			this.help_panel.Controls.Add(this.help_description_label);
			this.help_panel.Controls.Add(this.help_title_label);
			this.help_panel.Paint += this.help_panel_Paint;
			this.toolbar = new PropertyGrid.PropertyToolBar();
			this.toolbar.Dock = DockStyle.Top;
			this.categorized_toolbarbutton = new PropertyGrid.PropertyToolBarButton();
			this.categorized_toolbarbutton.Pushed = true;
			this.alphabetic_toolbarbutton = new PropertyGrid.PropertyToolBarButton();
			this.propertypages_toolbarbutton = new PropertyGrid.PropertyToolBarButton();
			this.separator_toolbarbutton = new PropertyGrid.PropertyToolBarSeparator();
			ContextMenu contextMenu = new ContextMenu();
			this.context_menu_default_location = Point.Empty;
			this.categorized_image = new Bitmap(typeof(PropertyGrid), "propertygrid-categorized.png");
			this.alphabetical_image = new Bitmap(typeof(PropertyGrid), "propertygrid-alphabetical.png");
			this.propertypages_image = new Bitmap(typeof(PropertyGrid), "propertygrid-propertypages.png");
			this.toolbar_imagelist = new ImageList();
			this.toolbar_imagelist.ColorDepth = ColorDepth.Depth32Bit;
			this.toolbar_imagelist.ImageSize = new Size(16, 16);
			this.toolbar_imagelist.TransparentColor = Color.Transparent;
			this.toolbar.Appearance = ToolBarAppearance.Flat;
			this.toolbar.AutoSize = false;
			this.toolbar.ImageList = this.toolbar_imagelist;
			this.toolbar.Location = new Point(0, 0);
			this.toolbar.ShowToolTips = true;
			this.toolbar.Size = new Size(256, 27);
			this.toolbar.TabIndex = 0;
			this.toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.categorized_toolbarbutton,
				this.alphabetic_toolbarbutton,
				new PropertyGrid.PropertyToolBarSeparator(),
				this.propertypages_toolbarbutton
			});
			this.categorized_toolbarbutton.Click += new EventHandler(this.toolbarbutton_clicked);
			this.alphabetic_toolbarbutton.Click += new EventHandler(this.toolbarbutton_clicked);
			this.propertypages_toolbarbutton.Click += new EventHandler(this.toolbarbutton_clicked);
			this.categorized_toolbarbutton.Style = ToolBarButtonStyle.ToggleButton;
			this.categorized_toolbarbutton.ToolTipText = Locale.GetText("Categorized");
			this.alphabetic_toolbarbutton.Style = ToolBarButtonStyle.ToggleButton;
			this.alphabetic_toolbarbutton.ToolTipText = Locale.GetText("Alphabetic");
			this.propertypages_toolbarbutton.Enabled = false;
			this.propertypages_toolbarbutton.Style = ToolBarButtonStyle.ToggleButton;
			this.propertypages_toolbarbutton.ToolTipText = "Property Pages";
			this.properties_tab = this.CreatePropertyTab(this.DefaultTabType);
			this.selected_tab = this.properties_tab;
			this.RefreshToolbar(this.property_tabs);
			this.reset_menuitem = contextMenu.MenuItems.Add("Reset");
			this.reset_menuitem.Click += new EventHandler(this.OnResetPropertyClick);
			contextMenu.MenuItems.Add("-");
			this.description_menuitem = contextMenu.MenuItems.Add("Description");
			this.description_menuitem.Click += new EventHandler(this.OnDescriptionClick);
			this.description_menuitem.Checked = this.HelpVisible;
			this.ContextMenu = contextMenu;
			this.toolbar.ContextMenu = contextMenu;
			PropertyGrid.BorderHelperControl borderHelperControl = new PropertyGrid.BorderHelperControl();
			borderHelperControl.Dock = DockStyle.Fill;
			borderHelperControl.Controls.Add(this.property_grid_view);
			this.Controls.Add(borderHelperControl);
			this.Controls.Add(this.toolbar);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.help_panel);
			base.Name = "PropertyGrid";
			base.Size = new Size(256, 400);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000AC1D8 File Offset: 0x000AA3D8
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyGrid()
		{
			PropertyGrid.PropertySortChangedEvent = new object();
			PropertyGrid.PropertyTabChangedEvent = new object();
			PropertyGrid.PropertyValueChangedEvent = new object();
			PropertyGrid.SelectedGridItemChangedEvent = new object();
			PropertyGrid.SelectedObjectsChangedEvent = new object();
			PropertyGrid.ComComponentNameChangedEvent = new object();
		}

		/// <summary>Occurs when the sort mode is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AC RID: 684
		// (add) Token: 0x06002C7E RID: 11390 RVA: 0x000AC224 File Offset: 0x000AA424
		// (remove) Token: 0x06002C7F RID: 11391 RVA: 0x000AC238 File Offset: 0x000AA438
		public event EventHandler PropertySortChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.PropertySortChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.PropertySortChangedEvent, value);
			}
		}

		/// <summary>Occurs when a property tab changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AD RID: 685
		// (add) Token: 0x06002C80 RID: 11392 RVA: 0x000AC24C File Offset: 0x000AA44C
		// (remove) Token: 0x06002C81 RID: 11393 RVA: 0x000AC260 File Offset: 0x000AA460
		public event PropertyTabChangedEventHandler PropertyTabChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.PropertyTabChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.PropertyTabChangedEvent, value);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AE RID: 686
		// (add) Token: 0x06002C82 RID: 11394 RVA: 0x000AC274 File Offset: 0x000AA474
		// (remove) Token: 0x06002C83 RID: 11395 RVA: 0x000AC288 File Offset: 0x000AA488
		public event PropertyValueChangedEventHandler PropertyValueChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.PropertyValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.PropertyValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the selected <see cref="T:System.Windows.Forms.GridItem" /> is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AF RID: 687
		// (add) Token: 0x06002C84 RID: 11396 RVA: 0x000AC29C File Offset: 0x000AA49C
		// (remove) Token: 0x06002C85 RID: 11397 RVA: 0x000AC2B0 File Offset: 0x000AA4B0
		public event SelectedGridItemChangedEventHandler SelectedGridItemChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.SelectedGridItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.SelectedGridItemChangedEvent, value);
			}
		}

		/// <summary>Occurs when the objects selected by the <see cref="P:System.Windows.Forms.PropertyGrid.SelectedObjects" /> property have changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B0 RID: 688
		// (add) Token: 0x06002C86 RID: 11398 RVA: 0x000AC2C4 File Offset: 0x000AA4C4
		// (remove) Token: 0x06002C87 RID: 11399 RVA: 0x000AC2D8 File Offset: 0x000AA4D8
		public event EventHandler SelectedObjectsChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.SelectedObjectsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.SelectedObjectsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B1 RID: 689
		// (add) Token: 0x06002C88 RID: 11400 RVA: 0x000AC2EC File Offset: 0x000AA4EC
		// (remove) Token: 0x06002C89 RID: 11401 RVA: 0x000AC2F8 File Offset: 0x000AA4F8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B2 RID: 690
		// (add) Token: 0x06002C8A RID: 11402 RVA: 0x000AC304 File Offset: 0x000AA504
		// (remove) Token: 0x06002C8B RID: 11403 RVA: 0x000AC310 File Offset: 0x000AA510
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B3 RID: 691
		// (add) Token: 0x06002C8C RID: 11404 RVA: 0x000AC31C File Offset: 0x000AA51C
		// (remove) Token: 0x06002C8D RID: 11405 RVA: 0x000AC328 File Offset: 0x000AA528
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Occurs when a key is first pressed.</summary>
		// Token: 0x140002B4 RID: 692
		// (add) Token: 0x06002C8E RID: 11406 RVA: 0x000AC334 File Offset: 0x000AA534
		// (remove) Token: 0x06002C8F RID: 11407 RVA: 0x000AC340 File Offset: 0x000AA540
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		// Token: 0x140002B5 RID: 693
		// (add) Token: 0x06002C90 RID: 11408 RVA: 0x000AC34C File Offset: 0x000AA54C
		// (remove) Token: 0x06002C91 RID: 11409 RVA: 0x000AC358 File Offset: 0x000AA558
		[EditorBrowsable(2)]
		[Browsable(false)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>Occurs when a key is released while the control has focus.</summary>
		// Token: 0x140002B6 RID: 694
		// (add) Token: 0x06002C92 RID: 11410 RVA: 0x000AC364 File Offset: 0x000AA564
		// (remove) Token: 0x06002C93 RID: 11411 RVA: 0x000AC370 File Offset: 0x000AA570
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.PropertyGrid" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B7 RID: 695
		// (add) Token: 0x06002C94 RID: 11412 RVA: 0x000AC37C File Offset: 0x000AA57C
		// (remove) Token: 0x06002C95 RID: 11413 RVA: 0x000AC388 File Offset: 0x000AA588
		[EditorBrowsable(2)]
		[Browsable(false)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B8 RID: 696
		// (add) Token: 0x06002C96 RID: 11414 RVA: 0x000AC394 File Offset: 0x000AA594
		// (remove) Token: 0x06002C97 RID: 11415 RVA: 0x000AC3A0 File Offset: 0x000AA5A0
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002B9 RID: 697
		// (add) Token: 0x06002C98 RID: 11416 RVA: 0x000AC3AC File Offset: 0x000AA5AC
		// (remove) Token: 0x06002C99 RID: 11417 RVA: 0x000AC3B8 File Offset: 0x000AA5B8
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer moves over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002BA RID: 698
		// (add) Token: 0x06002C9A RID: 11418 RVA: 0x000AC3C4 File Offset: 0x000AA5C4
		// (remove) Token: 0x06002C9B RID: 11419 RVA: 0x000AC3D0 File Offset: 0x000AA5D0
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and the user releases a mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002BB RID: 699
		// (add) Token: 0x06002C9C RID: 11420 RVA: 0x000AC3DC File Offset: 0x000AA5DC
		// (remove) Token: 0x06002C9D RID: 11421 RVA: 0x000AC3E8 File Offset: 0x000AA5E8
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.Padding" /> property changes.</summary>
		// Token: 0x140002BC RID: 700
		// (add) Token: 0x06002C9E RID: 11422 RVA: 0x000AC3F4 File Offset: 0x000AA5F4
		// (remove) Token: 0x06002C9F RID: 11423 RVA: 0x000AC400 File Offset: 0x000AA600
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the text of the <see cref="T:System.Windows.Forms.PropertyGrid" /> changes.</summary>
		// Token: 0x140002BD RID: 701
		// (add) Token: 0x06002CA0 RID: 11424 RVA: 0x000AC40C File Offset: 0x000AA60C
		// (remove) Token: 0x06002CA1 RID: 11425 RVA: 0x000AC418 File Offset: 0x000AA618
		[Browsable(false)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x140002BE RID: 702
		// (add) Token: 0x06002CA2 RID: 11426 RVA: 0x000AC424 File Offset: 0x000AA624
		// (remove) Token: 0x06002CA3 RID: 11427 RVA: 0x000AC438 File Offset: 0x000AA638
		event ComponentRenameEventHandler IComPropertyBrowser.ComComponentNameChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.ComComponentNameChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.ComComponentNameChangedEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.InPropertySet" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object; otherwise, false.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000AC44C File Offset: 0x000AA64C
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		bool IComPropertyBrowser.InPropertySet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Closes any open drop-down controls on the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.DropDownDone" />.</summary>
		// Token: 0x06002CA5 RID: 11429 RVA: 0x000AC454 File Offset: 0x000AA654
		[MonoTODO("Stub, does nothing")]
		void IComPropertyBrowser.DropDownDone()
		{
		}

		/// <summary>Commits all pending changes to the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.EnsurePendingChangesCommitted" />.</summary>
		/// <returns>true if all the <see cref="T:System.Windows.Forms.PropertyGrid" /> successfully commits changes; otherwise, false.</returns>
		// Token: 0x06002CA6 RID: 11430 RVA: 0x000AC458 File Offset: 0x000AA658
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		bool IComPropertyBrowser.EnsurePendingChangesCommitted()
		{
			throw new NotImplementedException();
		}

		/// <summary>Activates the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when the user chooses properties for a control in Design view. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.HandleF4" />.</summary>
		// Token: 0x06002CA7 RID: 11431 RVA: 0x000AC460 File Offset: 0x000AA660
		[MonoTODO("Stub, does nothing")]
		void IComPropertyBrowser.HandleF4()
		{
		}

		/// <summary>Loads user states from the registry into the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.LoadState(Microsoft.Win32.RegistryKey)" />.</summary>
		/// <param name="optRoot">The registry key that contains the user states.</param>
		// Token: 0x06002CA8 RID: 11432 RVA: 0x000AC464 File Offset: 0x000AA664
		[MonoTODO("Stub, does nothing")]
		void IComPropertyBrowser.LoadState(RegistryKey optRoot)
		{
		}

		/// <summary>Saves user states from the <see cref="T:System.Windows.Forms.PropertyGrid" /> control to the registry. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.SaveState(Microsoft.Win32.RegistryKey)" />.</summary>
		/// <param name="optRoot">The registry key that contains the user states.</param>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000AC468 File Offset: 0x000AA668
		[MonoTODO("Stub, does nothing")]
		void IComPropertyBrowser.SaveState(RegistryKey optRoot)
		{
		}

		/// <summary>Gets or sets the browsable attributes associated with the object that the property grid is attached to.</summary>
		/// <returns>The collection of browsable attributes associated with the object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000AC46C File Offset: 0x000AA66C
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000AC4A4 File Offset: 0x000AA6A4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public AttributeCollection BrowsableAttributes
		{
			get
			{
				if (this.browsable_attributes == null)
				{
					this.browsable_attributes = new AttributeCollection(new Attribute[] { BrowsableAttribute.Yes });
				}
				return this.browsable_attributes;
			}
			set
			{
				if (this.browsable_attributes == value)
				{
					return;
				}
				if (this.browsable_attributes == null || this.browsable_attributes.Count == 0)
				{
					this.browsable_attributes = null;
				}
				else
				{
					this.browsable_attributes = value;
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000AC4E4 File Offset: 0x000AA6E4
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x000AC4EC File Offset: 0x000AA6EC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000AC4F8 File Offset: 0x000AA6F8
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x000AC500 File Offset: 0x000AA700
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.toolbar.BackColor = value;
				this.Refresh();
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000AC51C File Offset: 0x000AA71C
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000AC524 File Offset: 0x000AA724
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000AC530 File Offset: 0x000AA730
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x000AC538 File Offset: 0x000AA738
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets a value indicating whether the commands pane can be made visible for the currently selected objects.</summary>
		/// <returns>true if the commands pane can be made visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000AC544 File Offset: 0x000AA744
		[Browsable(false)]
		[EditorBrowsable(2)]
		public virtual bool CanShowCommands
		{
			get
			{
				return this.can_show_commands;
			}
		}

		/// <summary>Gets or sets the text color used for category headings. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the text color.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000AC54C File Offset: 0x000AA74C
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000AC554 File Offset: 0x000AA754
		[DefaultValue(typeof(Color), "ControlText")]
		public Color CategoryForeColor
		{
			get
			{
				return this.category_fore_color;
			}
			set
			{
				if (this.category_fore_color != value)
				{
					this.category_fore_color = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background color of the hot commands region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for controls.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000AC574 File Offset: 0x000AA774
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000AC57C File Offset: 0x000AA77C
		public Color CommandsBackColor
		{
			get
			{
				return this.commands_back_color;
			}
			set
			{
				if (this.commands_back_color == value)
				{
					return;
				}
				this.commands_back_color = value;
			}
		}

		/// <summary>Gets or sets the foreground color for the hot commands region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for control text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000AC598 File Offset: 0x000AA798
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000AC5A0 File Offset: 0x000AA7A0
		public Color CommandsForeColor
		{
			get
			{
				return this.commands_fore_color;
			}
			set
			{
				if (this.commands_fore_color == value)
				{
					return;
				}
				this.commands_fore_color = value;
			}
		}

		/// <summary>Gets or sets the color of active links in the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the active link color.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000AC5BC File Offset: 0x000AA7BC
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x000AC5C4 File Offset: 0x000AA7C4
		public Color CommandsActiveLinkColor
		{
			get
			{
				return this.commands_active_link_color;
			}
			set
			{
				this.commands_active_link_color = value;
			}
		}

		/// <summary>Gets or sets the unavailable link color for the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the unavailable link color.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000AC5D0 File Offset: 0x000AA7D0
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x000AC5D8 File Offset: 0x000AA7D8
		public Color CommandsDisabledLinkColor
		{
			get
			{
				return this.commands_disabled_link_color;
			}
			set
			{
				this.commands_disabled_link_color = value;
			}
		}

		/// <summary>Gets or sets the link color for the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the link color for the executable commands region.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000AC5EC File Offset: 0x000AA7EC
		public Color CommandsLinkColor
		{
			get
			{
				return this.commands_link_color;
			}
			set
			{
				this.commands_link_color = value;
			}
		}

		/// <summary>Gets a value indicating whether the commands pane is visible.</summary>
		/// <returns>true if the commands pane is visible; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000AC5F8 File Offset: 0x000AA7F8
		[MonoTODO("Commands are not implemented yet.")]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public virtual bool CommandsVisible
		{
			get
			{
				return this.commands_visible;
			}
		}

		/// <summary>Gets or sets a value indicating whether the commands pane is visible for objects that expose verbs.</summary>
		/// <returns>true if the commands pane is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000AC600 File Offset: 0x000AA800
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000AC608 File Offset: 0x000AA808
		[DefaultValue(true)]
		public virtual bool CommandsVisibleIfAvailable
		{
			get
			{
				return this.commands_visible_if_available;
			}
			set
			{
				if (this.commands_visible_if_available == value)
				{
					return;
				}
				this.commands_visible_if_available = value;
			}
		}

		/// <summary>Gets the default location for the shortcut menu.</summary>
		/// <returns>The default location for the shortcut menu if the command is invoked. Typically, this is centered over the selected property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000AC620 File Offset: 0x000AA820
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Point ContextMenuDefaultLocation
		{
			get
			{
				return this.context_menu_default_location;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000AC628 File Offset: 0x000AA828
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000AC630 File Offset: 0x000AA830
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x000AC638 File Offset: 0x000AA838
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the background color for the Help region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for controls.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000AC644 File Offset: 0x000AA844
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000AC654 File Offset: 0x000AA854
		[DefaultValue("Color [Control]")]
		public Color HelpBackColor
		{
			get
			{
				return this.help_panel.BackColor;
			}
			set
			{
				this.help_panel.BackColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color for the Help region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for control text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000AC664 File Offset: 0x000AA864
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000AC674 File Offset: 0x000AA874
		[DefaultValue("Color [ControlText]")]
		public Color HelpForeColor
		{
			get
			{
				return this.help_panel.ForeColor;
			}
			set
			{
				this.help_panel.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help text is visible.</summary>
		/// <returns>true if the help text is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000AC684 File Offset: 0x000AA884
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000AC694 File Offset: 0x000AA894
		[Localizable(true)]
		[DefaultValue(true)]
		public virtual bool HelpVisible
		{
			get
			{
				return this.help_panel.Visible;
			}
			set
			{
				this.splitter.Visible = value;
				this.help_panel.Visible = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether buttons appear in standard size or in large size.</summary>
		/// <returns>true if buttons on the control appear large; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000AC6B0 File Offset: 0x000AA8B0
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x000AC6B8 File Offset: 0x000AA8B8
		[DefaultValue(false)]
		public bool LargeButtons
		{
			get
			{
				return this.large_buttons;
			}
			set
			{
				if (this.large_buttons == value)
				{
					return;
				}
				this.large_buttons = value;
			}
		}

		/// <summary>Gets or sets the color of the gridlines and borders.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for scroll bars.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000AC6D0 File Offset: 0x000AA8D0
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x000AC6D8 File Offset: 0x000AA8D8
		[DefaultValue("Color [InactiveBorder]")]
		public Color LineColor
		{
			get
			{
				return this.line_color;
			}
			set
			{
				if (this.line_color == value)
				{
					return;
				}
				this.line_color = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000AC6F4 File Offset: 0x000AA8F4
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x000AC6FC File Offset: 0x000AA8FC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets or sets the type of sorting the <see cref="T:System.Windows.Forms.PropertyGrid" /> uses to display properties.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PropertySort" /> values. The default is <see cref="F:System.Windows.Forms.PropertySort.Categorized" /> or <see cref="F:System.Windows.Forms.PropertySort.Alphabetical" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.PropertySort" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000AC708 File Offset: 0x000AA908
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x000AC710 File Offset: 0x000AA910
		[DefaultValue(PropertySort.CategorizedAlphabetical)]
		public PropertySort PropertySort
		{
			get
			{
				return this.property_sort;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PropertySort), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PropertySort));
				}
				if (this.property_sort == value)
				{
					return;
				}
				bool flag = (this.property_sort & PropertySort.Categorized) == PropertySort.NoSort || (value & PropertySort.Categorized) == PropertySort.NoSort;
				this.property_sort = value;
				if (flag)
				{
					this.UpdateSortLayout(this.root_grid_item);
					if (this.selected_grid_item != null)
					{
						if (this.selected_grid_item.GridItemType == GridItemType.Category && (value == PropertySort.Alphabetical || value == PropertySort.NoSort))
						{
							this.SelectItemCore(null, null);
						}
						else
						{
							this.SelectItemCore(null, this.selected_grid_item);
						}
					}
					this.property_grid_view.UpdateView();
					EventHandler eventHandler = (EventHandler)base.Events[PropertyGrid.PropertySortChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
				this.UpdatePropertySortButtonsState();
			}
		}

		/// <summary>Gets the collection of property tabs that are displayed in the grid.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.PropertyGrid.PropertyTabCollection" /> containing the collection of <see cref="T:System.Windows.Forms.Design.PropertyTab" /> objects being displayed by the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000AC808 File Offset: 0x000AAA08
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public PropertyGrid.PropertyTabCollection PropertyTabs
		{
			get
			{
				return this.property_tabs;
			}
		}

		/// <summary>Gets or sets the selected grid item.</summary>
		/// <returns>The currently selected row in the property grid.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x000AC810 File Offset: 0x000AAA10
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x000AC818 File Offset: 0x000AAA18
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public GridItem SelectedGridItem
		{
			get
			{
				return this.selected_grid_item;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("GridItem specified to PropertyGrid.SelectedGridItem must be a valid GridItem.");
				}
				if (value != this.selected_grid_item)
				{
					GridEntry gridEntry = this.selected_grid_item;
					this.SelectItemCore(gridEntry, (GridEntry)value);
					this.OnSelectedGridItemChanged(new SelectedGridItemChangedEventArgs(gridEntry, value));
				}
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000AC864 File Offset: 0x000AAA64
		internal GridItem RootGridItem
		{
			get
			{
				return this.root_grid_item;
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000AC86C File Offset: 0x000AAA6C
		private void UpdateHelp(GridItem item)
		{
			if (item == null)
			{
				this.help_title_label.Text = string.Empty;
				this.help_description_label.Text = string.Empty;
			}
			else
			{
				this.help_title_label.Text = item.Label;
				if (item.PropertyDescriptor != null)
				{
					this.help_description_label.Text = item.PropertyDescriptor.Description;
				}
			}
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000AC8D8 File Offset: 0x000AAAD8
		private void SelectItemCore(GridEntry oldItem, GridEntry item)
		{
			this.UpdateHelp(item);
			this.selected_grid_item = item;
			this.property_grid_view.SelectItem(oldItem, item);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000AC8F8 File Offset: 0x000AAAF8
		internal void OnPropertyValueChangedInternal(GridItem item, object property_value)
		{
			this.property_grid_view.UpdateView();
			this.OnPropertyValueChanged(new PropertyValueChangedEventArgs(item, property_value));
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000AC914 File Offset: 0x000AAB14
		internal void OnExpandItem(GridEntry item)
		{
			this.property_grid_view.ExpandItem(item);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000AC924 File Offset: 0x000AAB24
		internal void OnCollapseItem(GridEntry item)
		{
			this.property_grid_view.CollapseItem(item);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000AC934 File Offset: 0x000AAB34
		internal DialogResult ShowError(string text)
		{
			return this.ShowError(text, MessageBoxButtons.OK);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000AC940 File Offset: 0x000AAB40
		internal DialogResult ShowError(string text, MessageBoxButtons buttons)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return MessageBox.Show(this, text, "Properties Window", buttons, MessageBoxIcon.Exclamation);
		}

		/// <summary>Gets or sets the object for which the grid displays properties.</summary>
		/// <returns>The first object in the object list. If there is no currently selected object the return is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000AC970 File Offset: 0x000AAB70
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x000AC98C File Offset: 0x000AAB8C
		[TypeConverter("System.Windows.Forms.PropertyGrid+SelectedObjectConverter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[DefaultValue(null)]
		public object SelectedObject
		{
			get
			{
				if (this.selected_objects.Length > 0)
				{
					return this.selected_objects[0];
				}
				return null;
			}
			set
			{
				if (this.selected_objects != null && this.selected_objects.Length == 1 && this.selected_objects[0] == value)
				{
					return;
				}
				if (value == null)
				{
					this.SelectedObjects = new object[0];
				}
				else
				{
					this.SelectedObjects = new object[] { value };
				}
			}
		}

		/// <summary>Gets or sets the currently selected objects.</summary>
		/// <returns>An array of type <see cref="T:System.Object" />. The default is an empty array.</returns>
		/// <exception cref="T:System.ArgumentException">One of the items in the array of objects had a null value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000AC9E8 File Offset: 0x000AABE8
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000AC9F0 File Offset: 0x000AABF0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public object[] SelectedObjects
		{
			get
			{
				return this.selected_objects;
			}
			set
			{
				this.root_grid_item = null;
				this.SelectItemCore(null, null);
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						if (value[i] == null)
						{
							throw new ArgumentException(string.Format("Item {0} in the objs array is null.", i));
						}
					}
					this.selected_objects = value;
				}
				else
				{
					this.selected_objects = new object[0];
				}
				this.ShowEventsButton(false);
				this.PopulateGrid(this.selected_objects);
				this.RefreshTabs(3);
				if (this.root_grid_item != null)
				{
					this.SelectItemCore(null, this.GetDefaultPropertyItem(this.root_grid_item, this.selected_tab));
				}
				this.property_grid_view.UpdateView();
				this.OnSelectedObjectsChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the currently selected property tab.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that is providing the selected view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000ACAB0 File Offset: 0x000AACB0
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public PropertyTab SelectedTab
		{
			get
			{
				return this.selected_tab;
			}
		}

		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000ACAB8 File Offset: 0x000AACB8
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000ACAC0 File Offset: 0x000AACC0
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000ACACC File Offset: 0x000AACCC
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000ACAD4 File Offset: 0x000AACD4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar is visible.</summary>
		/// <returns>true if the toolbar is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000ACAE0 File Offset: 0x000AACE0
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000ACAF0 File Offset: 0x000AACF0
		[DefaultValue(true)]
		public virtual bool ToolbarVisible
		{
			get
			{
				return this.toolbar.Visible;
			}
			set
			{
				if (this.toolbar.Visible == value)
				{
					return;
				}
				this.toolbar.Visible = value;
			}
		}

		/// <summary>Gets or sets the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> for the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000ACB10 File Offset: 0x000AAD10
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000ACB2C File Offset: 0x000AAD2C
		protected ToolStripRenderer ToolStripRenderer
		{
			get
			{
				if (this.toolbar != null)
				{
					return this.toolbar.Renderer;
				}
				return null;
			}
			set
			{
				if (this.toolbar != null)
				{
					this.toolbar.Renderer = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating the background color in the grid.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000ACB48 File Offset: 0x000AAD48
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x000ACB58 File Offset: 0x000AAD58
		[DefaultValue("Color [Window]")]
		public Color ViewBackColor
		{
			get
			{
				return this.property_grid_view.BackColor;
			}
			set
			{
				if (this.property_grid_view.BackColor == value)
				{
					return;
				}
				this.property_grid_view.BackColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating the color of the text in the grid.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is current system color for text in windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000ACB80 File Offset: 0x000AAD80
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x000ACB90 File Offset: 0x000AAD90
		[DefaultValue("Color [WindowText]")]
		public Color ViewForeColor
		{
			get
			{
				return this.property_grid_view.ForeColor;
			}
			set
			{
				if (this.property_grid_view.ForeColor == value)
				{
					return;
				}
				this.property_grid_view.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000ACBB8 File Offset: 0x000AADB8
		// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x000ACBC0 File Offset: 0x000AADC0
		[DefaultValue(false)]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
			set
			{
				if (this.use_compatible_text_rendering != value)
				{
					this.use_compatible_text_rendering = value;
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "UseCompatibleTextRendering");
					}
					base.Invalidate();
				}
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000ACBF8 File Offset: 0x000AADF8
		protected override Size DefaultSize
		{
			get
			{
				return base.DefaultSize;
			}
		}

		/// <summary>Gets the type of the default tab.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the default tab.</returns>
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000ACC00 File Offset: 0x000AAE00
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		protected virtual Type DefaultTabType
		{
			get
			{
				return typeof(PropertiesTab);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PropertyGrid" /> control paints its toolbar with flat buttons.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> paints its toolbar with flat buttons; otherwise false. The default is false.</returns>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000ACC0C File Offset: 0x000AAE0C
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x000ACC1C File Offset: 0x000AAE1C
		protected bool DrawFlatToolbar
		{
			get
			{
				return this.toolbar.Appearance == ToolBarAppearance.Flat;
			}
			set
			{
				if (value)
				{
					this.toolbar.Appearance = ToolBarAppearance.Flat;
				}
				else
				{
					this.toolbar.Appearance = ToolBarAppearance.Normal;
				}
			}
		}

		/// <returns>true if the control should display focus rectangles; otherwise, false.</returns>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000ACC44 File Offset: 0x000AAE44
		protected internal override bool ShowFocusCues
		{
			get
			{
				return base.ShowFocusCues;
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x000ACC4C File Offset: 0x000AAE4C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Collapses all the categories in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002CFA RID: 11514 RVA: 0x000ACC58 File Offset: 0x000AAE58
		public void CollapseAllGridItems()
		{
			GridEntry gridEntry = this.FindCategoryItem(this.selected_grid_item);
			if (gridEntry != null)
			{
				this.SelectedGridItem = gridEntry;
			}
			this.CollapseItemRecursive(this.root_grid_item);
			this.property_grid_view.UpdateView();
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000ACC98 File Offset: 0x000AAE98
		private void CollapseItemRecursive(GridItem item)
		{
			if (item == null)
			{
				return;
			}
			foreach (object obj in item.GridItems)
			{
				GridItem gridItem = (GridItem)obj;
				this.CollapseItemRecursive(gridItem);
				if (gridItem.Expandable)
				{
					gridItem.Expanded = false;
				}
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000ACD20 File Offset: 0x000AAF20
		private GridEntry FindCategoryItem(GridEntry entry)
		{
			if (entry == null || (this.property_sort != PropertySort.Categorized && this.property_sort != PropertySort.CategorizedAlphabetical))
			{
				return null;
			}
			if (entry.GridItemType == GridItemType.Category)
			{
				return entry;
			}
			GridEntry gridEntry = null;
			GridItem gridItem = entry;
			while (gridEntry == null)
			{
				if (gridItem.Parent != null && gridItem.Parent.GridItemType == GridItemType.Category)
				{
					gridEntry = (GridEntry)gridItem.Parent;
				}
				gridItem = gridItem.Parent;
				if (gridItem == null)
				{
					break;
				}
			}
			return gridEntry;
		}

		/// <summary>Expands all the categories in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CFD RID: 11517 RVA: 0x000ACDA8 File Offset: 0x000AAFA8
		public void ExpandAllGridItems()
		{
			this.ExpandItemRecursive(this.root_grid_item);
			this.property_grid_view.UpdateView();
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000ACDC4 File Offset: 0x000AAFC4
		private void ExpandItemRecursive(GridItem item)
		{
			if (item == null)
			{
				return;
			}
			foreach (object obj in item.GridItems)
			{
				GridItem gridItem = (GridItem)obj;
				this.ExpandItemRecursive(gridItem);
				if (gridItem.Expandable)
				{
					gridItem.Expanded = true;
				}
			}
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06002CFF RID: 11519 RVA: 0x000ACE4C File Offset: 0x000AB04C
		public override void Refresh()
		{
			base.Refresh();
			this.SelectedObjects = this.SelectedObjects;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000ACE60 File Offset: 0x000AB060
		private void toolbar_Clicked(PropertyGrid.PropertyToolBarButton button)
		{
			if (button == null)
			{
				return;
			}
			if (button == this.alphabetic_toolbarbutton)
			{
				this.PropertySort = PropertySort.Alphabetical;
				this.alphabetic_toolbarbutton.Pushed = true;
				this.categorized_toolbarbutton.Pushed = false;
			}
			else if (button == this.categorized_toolbarbutton)
			{
				this.PropertySort = PropertySort.CategorizedAlphabetical;
				this.categorized_toolbarbutton.Pushed = true;
				this.alphabetic_toolbarbutton.Pushed = false;
			}
			else if (button.Enabled)
			{
				this.SelectPropertyTab(button.PropertyTab);
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000ACEEC File Offset: 0x000AB0EC
		private void toolbarbutton_clicked(object o, EventArgs args)
		{
			this.toolbar_Clicked(o as PropertyGrid.PropertyToolBarButton);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000ACEFC File Offset: 0x000AB0FC
		private void SelectPropertyTab(PropertyTab propertyTab)
		{
			if (propertyTab != null && this.selected_tab != propertyTab)
			{
				foreach (object obj in this.toolbar.Items)
				{
					PropertyGrid.PropertyToolBarButton propertyToolBarButton = obj as PropertyGrid.PropertyToolBarButton;
					if (propertyToolBarButton != null && propertyToolBarButton.PropertyTab != null)
					{
						if (propertyToolBarButton.PropertyTab == this.selected_tab)
						{
							propertyToolBarButton.Pushed = false;
						}
						else if (propertyToolBarButton.PropertyTab == propertyTab)
						{
							propertyToolBarButton.Pushed = true;
						}
					}
				}
				this.selected_tab = propertyTab;
				this.PopulateGrid(this.selected_objects);
				this.SelectItemCore(null, this.GetDefaultPropertyItem(this.root_grid_item, this.selected_tab));
				this.property_grid_view.UpdateView();
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000ACFF8 File Offset: 0x000AB1F8
		private void UpdatePropertySortButtonsState()
		{
			if (this.property_sort == PropertySort.NoSort)
			{
				this.alphabetic_toolbarbutton.Pushed = false;
				this.categorized_toolbarbutton.Pushed = false;
			}
			else if (this.property_sort == PropertySort.Alphabetical)
			{
				this.alphabetic_toolbarbutton.Pushed = true;
				this.categorized_toolbarbutton.Pushed = false;
			}
			else if (this.property_sort == PropertySort.Categorized || this.property_sort == PropertySort.CategorizedAlphabetical)
			{
				this.alphabetic_toolbarbutton.Pushed = false;
				this.categorized_toolbarbutton.Pushed = true;
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000AD088 File Offset: 0x000AB288
		protected void ShowEventsButton(bool value)
		{
			if (value && this.property_tabs.Contains(typeof(EventsTab)))
			{
				this.events_tab_visible = true;
			}
			else
			{
				this.events_tab_visible = false;
			}
			this.RefreshTabs(3);
		}

		/// <summary>Refreshes the property tabs of the specified scope.</summary>
		/// <param name="tabScope">Either the Component or Document value of <see cref="T:System.ComponentModel.PropertyTabScope" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tabScope" /> parameter is not the Component or Document value of <see cref="T:System.ComponentModel.PropertyTabScope" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D05 RID: 11525 RVA: 0x000AD0D0 File Offset: 0x000AB2D0
		public void RefreshTabs(PropertyTabScope tabScope)
		{
			this.property_tabs.Clear(tabScope);
			if (this.selected_objects != null)
			{
				Type[] array = null;
				PropertyTabScope[] array2 = null;
				if (this.events_tab_visible && this.property_tabs.Contains(typeof(EventsTab)))
				{
					this.property_tabs.InsertTab(0, this.properties_tab, 3);
				}
				this.GetMergedPropertyTabs(this.selected_objects, out array, out array2);
				if (array != null && array2 != null && array.Length > 0)
				{
					bool flag = false;
					for (int i = 0; i < array.Length; i++)
					{
						this.property_tabs.AddTabType(array[i], array2[i]);
						if (array[i] == this.selected_tab.GetType())
						{
							flag = true;
						}
					}
					if (!flag)
					{
						this.SelectPropertyTab(this.properties_tab);
					}
				}
			}
			else
			{
				this.SelectPropertyTab(this.properties_tab);
			}
			this.RefreshToolbar(this.property_tabs);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000AD1C0 File Offset: 0x000AB3C0
		private void RefreshToolbar(PropertyGrid.PropertyTabCollection tabs)
		{
			this.EnsurePropertiesTab();
			this.toolbar.SuspendLayout();
			this.toolbar.Items.Clear();
			this.toolbar_imagelist.Images.Clear();
			int num = 0;
			this.toolbar.Items.Add(this.categorized_toolbarbutton);
			this.toolbar_imagelist.Images.Add(this.categorized_image);
			this.categorized_toolbarbutton.ImageIndex = num;
			num++;
			this.toolbar.Items.Add(this.alphabetic_toolbarbutton);
			this.toolbar_imagelist.Images.Add(this.alphabetical_image);
			this.alphabetic_toolbarbutton.ImageIndex = num;
			num++;
			this.toolbar.Items.Add(this.separator_toolbarbutton);
			if (tabs != null && tabs.Count > 0)
			{
				foreach (object obj in tabs)
				{
					PropertyTab propertyTab = (PropertyTab)obj;
					PropertyGrid.PropertyToolBarButton propertyToolBarButton = new PropertyGrid.PropertyToolBarButton(propertyTab);
					this.toolbar.Items.Add(propertyToolBarButton);
					if (propertyTab.Bitmap != null)
					{
						propertyTab.Bitmap.MakeTransparent();
						this.toolbar_imagelist.Images.Add(propertyTab.Bitmap);
						propertyToolBarButton.ImageIndex = num;
						num++;
					}
					if (propertyTab == this.selected_tab)
					{
						propertyToolBarButton.Pushed = true;
					}
				}
				this.toolbar.Items.Add(new PropertyGrid.PropertyToolBarSeparator());
			}
			this.toolbar.Items.Add(this.propertypages_toolbarbutton);
			this.toolbar_imagelist.Images.Add(this.propertypages_image);
			this.propertypages_toolbarbutton.ImageIndex = num;
			this.toolbar.ResumeLayout();
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000AD3BC File Offset: 0x000AB5BC
		private void EnsurePropertiesTab()
		{
			if (this.property_tabs == null)
			{
				return;
			}
			if (this.property_tabs.Count > 0 && !this.property_tabs.Contains(this.DefaultTabType))
			{
				this.property_tabs.InsertTab(0, this.properties_tab, 3);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000AD410 File Offset: 0x000AB610
		private void GetMergedPropertyTabs(object[] objects, out Type[] tabTypes, out PropertyTabScope[] tabScopes)
		{
			tabTypes = null;
			tabScopes = null;
			if (objects == null || objects.Length == 0)
			{
				return;
			}
			ArrayList arrayList = null;
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] != null)
				{
					PropertyTabAttribute propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(objects[i])[typeof(PropertyTabAttribute)];
					if (propertyTabAttribute == null || propertyTabAttribute.TabClasses == null || propertyTabAttribute.TabClasses.Length == 0)
					{
						return;
					}
					ArrayList arrayList3 = new ArrayList();
					arrayList2.Clear();
					IList list;
					if (i == 0)
					{
						IList tabClasses = propertyTabAttribute.TabClasses;
						list = tabClasses;
					}
					else
					{
						list = arrayList;
					}
					IList list2 = list;
					for (int j = 0; j < list2.Count; j++)
					{
						if ((Type)arrayList[j] == propertyTabAttribute.TabClasses[j])
						{
							arrayList3.Add(propertyTabAttribute.TabClasses[j]);
							arrayList2.Add(propertyTabAttribute.TabScopes[j]);
						}
					}
					arrayList = arrayList3;
				}
			}
			tabTypes = new Type[arrayList.Count];
			arrayList.CopyTo(tabTypes);
			tabScopes = new PropertyTabScope[tabTypes.Length];
			arrayList2.CopyTo(tabScopes);
		}

		/// <summary>Resets the selected property to its default value.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002D09 RID: 11529 RVA: 0x000AD544 File Offset: 0x000AB744
		public void ResetSelectedProperty()
		{
			if (this.selected_grid_item == null)
			{
				return;
			}
			this.selected_grid_item.ResetValue();
		}

		/// <summary>When overridden in a derived class, enables the creation of a <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		/// <returns>The newly created property tab. Returns null in its default implementation.</returns>
		/// <param name="tabType">The type of tab to create. </param>
		// Token: 0x06002D0A RID: 11530 RVA: 0x000AD560 File Offset: 0x000AB760
		protected virtual PropertyTab CreatePropertyTab(Type tabType)
		{
			if (!typeof(PropertyTab).IsAssignableFrom(tabType))
			{
				return null;
			}
			ConstructorInfo constructor = tabType.GetConstructor(new Type[] { typeof(IServiceProvider) });
			PropertyTab propertyTab;
			if (constructor != null)
			{
				propertyTab = (PropertyTab)constructor.Invoke(new object[] { this.Site });
			}
			else
			{
				propertyTab = (PropertyTab)Activator.CreateInstance(tabType);
			}
			return propertyTab;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.ComComponentNameChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D0B RID: 11531 RVA: 0x000AD5D4 File Offset: 0x000AB7D4
		[MonoTODO("Never called")]
		protected void OnComComponentNameChanged(ComponentRenameEventArgs e)
		{
			ComponentRenameEventHandler componentRenameEventHandler = (ComponentRenameEventHandler)base.Events[PropertyGrid.ComComponentNameChangedEvent];
			if (componentRenameEventHandler != null)
			{
				componentRenameEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D0C RID: 11532 RVA: 0x000AD608 File Offset: 0x000AB808
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D0D RID: 11533 RVA: 0x000AD614 File Offset: 0x000AB814
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D0E RID: 11534 RVA: 0x000AD620 File Offset: 0x000AB820
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D0F RID: 11535 RVA: 0x000AD62C File Offset: 0x000AB82C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D10 RID: 11536 RVA: 0x000AD638 File Offset: 0x000AB838
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D11 RID: 11537 RVA: 0x000AD644 File Offset: 0x000AB844
		protected override void OnMouseDown(MouseEventArgs me)
		{
			base.OnMouseDown(me);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D12 RID: 11538 RVA: 0x000AD650 File Offset: 0x000AB850
		protected override void OnMouseMove(MouseEventArgs me)
		{
			base.OnMouseMove(me);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D13 RID: 11539 RVA: 0x000AD65C File Offset: 0x000AB85C
		protected override void OnMouseUp(MouseEventArgs me)
		{
			base.OnMouseUp(me);
		}

		/// <summary>Raises the <see cref="M:System.Drawing.Design.IPropertyValueUIService.NotifyPropertyValueUIItemsChanged" /> event.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D14 RID: 11540 RVA: 0x000AD668 File Offset: 0x000AB868
		protected void OnNotifyPropertyValueUIItemsChanged(object sender, EventArgs e)
		{
			this.property_grid_view.UpdateView();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D15 RID: 11541 RVA: 0x000AD678 File Offset: 0x000AB878
		protected override void OnPaint(PaintEventArgs pevent)
		{
			pevent.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), pevent.ClipRectangle);
			base.OnPaint(pevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertySortChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D16 RID: 11542 RVA: 0x000AD6B4 File Offset: 0x000AB8B4
		protected virtual void OnPropertySortChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGrid.PropertySortChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyTabChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PropertyTabChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D17 RID: 11543 RVA: 0x000AD6E8 File Offset: 0x000AB8E8
		protected virtual void OnPropertyTabChanged(PropertyTabChangedEventArgs e)
		{
			PropertyTabChangedEventHandler propertyTabChangedEventHandler = (PropertyTabChangedEventHandler)base.Events[PropertyGrid.PropertyTabChangedEvent];
			if (propertyTabChangedEventHandler != null)
			{
				propertyTabChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D18 RID: 11544 RVA: 0x000AD71C File Offset: 0x000AB91C
		protected virtual void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
		{
			PropertyValueChangedEventHandler propertyValueChangedEventHandler = (PropertyValueChangedEventHandler)base.Events[PropertyGrid.PropertyValueChangedEvent];
			if (propertyValueChangedEventHandler != null)
			{
				propertyValueChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D19 RID: 11545 RVA: 0x000AD750 File Offset: 0x000AB950
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedGridItemChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SelectedGridItemChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002D1A RID: 11546 RVA: 0x000AD75C File Offset: 0x000AB95C
		protected virtual void OnSelectedGridItemChanged(SelectedGridItemChangedEventArgs e)
		{
			SelectedGridItemChangedEventHandler selectedGridItemChangedEventHandler = (SelectedGridItemChangedEventHandler)base.Events[PropertyGrid.SelectedGridItemChangedEvent];
			if (selectedGridItemChangedEventHandler != null)
			{
				selectedGridItemChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedObjectsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D1B RID: 11547 RVA: 0x000AD790 File Offset: 0x000AB990
		protected virtual void OnSelectedObjectsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGrid.SelectedObjectsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D1C RID: 11548 RVA: 0x000AD7C4 File Offset: 0x000AB9C4
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D1D RID: 11549 RVA: 0x000AD7D0 File Offset: 0x000AB9D0
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000AD7DC File Offset: 0x000AB9DC
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06002D1F RID: 11551 RVA: 0x000AD7E8 File Offset: 0x000AB9E8
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000AD7F4 File Offset: 0x000AB9F4
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000AD800 File Offset: 0x000ABA00
		private GridItem FindFirstPropertyItem(GridItem root)
		{
			if (root.GridItemType == GridItemType.Property)
			{
				return root;
			}
			foreach (object obj in root.GridItems)
			{
				GridItem gridItem = (GridItem)obj;
				GridItem gridItem2 = this.FindFirstPropertyItem(gridItem);
				if (gridItem2 != null)
				{
					return gridItem2;
				}
			}
			return null;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000AD890 File Offset: 0x000ABA90
		private GridEntry GetDefaultPropertyItem(GridEntry rootItem, PropertyTab propertyTab)
		{
			if (rootItem == null || rootItem.GridItems.Count == 0 || propertyTab == null)
			{
				return null;
			}
			object[] values = rootItem.Values;
			if (values == null || values.Length == 0 || values[0] == null)
			{
				return null;
			}
			GridItem gridItem = null;
			if (values.Length > 1)
			{
				gridItem = rootItem.GridItems[0];
			}
			else
			{
				PropertyDescriptor defaultProperty = propertyTab.GetDefaultProperty(values[0]);
				if (defaultProperty != null)
				{
					gridItem = this.FindItem(defaultProperty.Name, rootItem);
				}
				if (gridItem == null)
				{
					gridItem = this.FindFirstPropertyItem(rootItem);
				}
			}
			return gridItem as GridEntry;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000AD92C File Offset: 0x000ABB2C
		private GridEntry FindItem(string name, GridEntry rootItem)
		{
			if (rootItem == null || name == null)
			{
				return null;
			}
			if (this.property_sort == PropertySort.Alphabetical || this.property_sort == PropertySort.NoSort)
			{
				foreach (object obj in rootItem.GridItems)
				{
					GridItem gridItem = (GridItem)obj;
					if (gridItem.Label == name)
					{
						return (GridEntry)gridItem;
					}
				}
			}
			else if (this.property_sort == PropertySort.Categorized || this.property_sort == PropertySort.CategorizedAlphabetical)
			{
				foreach (object obj2 in rootItem.GridItems)
				{
					GridItem gridItem2 = (GridItem)obj2;
					foreach (object obj3 in gridItem2.GridItems)
					{
						GridItem gridItem3 = (GridItem)obj3;
						if (gridItem3.Label == name)
						{
							return (GridEntry)gridItem3;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000ADAD4 File Offset: 0x000ABCD4
		private void OnResetPropertyClick(object sender, EventArgs e)
		{
			this.ResetSelectedProperty();
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000ADADC File Offset: 0x000ABCDC
		private void OnDescriptionClick(object sender, EventArgs e)
		{
			this.HelpVisible = !this.HelpVisible;
			this.description_menuitem.Checked = this.HelpVisible;
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000ADB0C File Offset: 0x000ABD0C
		private void PopulateGrid(object[] objects)
		{
			if (objects.Length > 0)
			{
				this.root_grid_item = new RootGridEntry(this, objects);
				this.root_grid_item.Expanded = true;
				this.UpdateSortLayout(this.root_grid_item);
			}
			else
			{
				this.root_grid_item = null;
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000ADB54 File Offset: 0x000ABD54
		private void UpdateSortLayout(GridEntry rootItem)
		{
			if (rootItem == null)
			{
				return;
			}
			GridItemCollection gridItemCollection = new GridItemCollection();
			if (this.property_sort == PropertySort.Alphabetical || this.property_sort == PropertySort.NoSort)
			{
				this.alphabetic_toolbarbutton.Pushed = true;
				this.categorized_toolbarbutton.Pushed = false;
				foreach (object obj in rootItem.GridItems)
				{
					GridItem gridItem = (GridItem)obj;
					if (gridItem.GridItemType == GridItemType.Category)
					{
						foreach (object obj2 in gridItem.GridItems)
						{
							GridItem gridItem2 = (GridItem)obj2;
							gridItemCollection.Add(gridItem2);
							((GridEntry)gridItem2).SetParent(rootItem);
						}
					}
					else
					{
						gridItemCollection.Add(gridItem);
					}
				}
			}
			else if (this.property_sort == PropertySort.Categorized || this.property_sort == PropertySort.CategorizedAlphabetical)
			{
				this.alphabetic_toolbarbutton.Pushed = false;
				this.categorized_toolbarbutton.Pushed = true;
				GridItemCollection gridItemCollection2 = new GridItemCollection();
				foreach (object obj3 in rootItem.GridItems)
				{
					GridItem gridItem3 = (GridItem)obj3;
					if (gridItem3.GridItemType == GridItemType.Category)
					{
						gridItemCollection2.Add(gridItem3);
					}
					else
					{
						string text = gridItem3.PropertyDescriptor.Category;
						if (text == null)
						{
							text = "Misc";
						}
						GridItem gridItem4 = rootItem.GridItems[text];
						if (gridItem4 == null)
						{
							gridItem4 = gridItemCollection2[text];
						}
						if (gridItem4 == null)
						{
							gridItem4 = new CategoryGridEntry(this, text, rootItem);
							gridItem4.Expanded = true;
							gridItemCollection2.Add(gridItem4);
						}
						gridItem4.GridItems.Add(gridItem3);
						((GridEntry)gridItem3).SetParent(gridItem4);
					}
				}
				gridItemCollection.AddRange(gridItemCollection2);
			}
			rootItem.GridItems.Clear();
			rootItem.GridItems.AddRange(gridItemCollection);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000ADDD8 File Offset: 0x000ABFD8
		private void help_panel_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.help_panel.BackColor), this.help_panel.ClientRectangle);
			e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, this.help_panel.Width - 1, this.help_panel.Height - 1);
		}

		// Token: 0x040015A7 RID: 5543
		private const string UNCATEGORIZED_CATEGORY_LABEL = "Misc";

		// Token: 0x040015A8 RID: 5544
		private AttributeCollection browsable_attributes;

		// Token: 0x040015A9 RID: 5545
		private bool can_show_commands;

		// Token: 0x040015AA RID: 5546
		private Color commands_back_color;

		// Token: 0x040015AB RID: 5547
		private Color commands_fore_color;

		// Token: 0x040015AC RID: 5548
		private bool commands_visible;

		// Token: 0x040015AD RID: 5549
		private bool commands_visible_if_available;

		// Token: 0x040015AE RID: 5550
		private Point context_menu_default_location;

		// Token: 0x040015AF RID: 5551
		private bool large_buttons;

		// Token: 0x040015B0 RID: 5552
		private Color line_color;

		// Token: 0x040015B1 RID: 5553
		private PropertySort property_sort;

		// Token: 0x040015B2 RID: 5554
		private PropertyGrid.PropertyTabCollection property_tabs;

		// Token: 0x040015B3 RID: 5555
		private GridEntry selected_grid_item;

		// Token: 0x040015B4 RID: 5556
		private GridEntry root_grid_item;

		// Token: 0x040015B5 RID: 5557
		private object[] selected_objects;

		// Token: 0x040015B6 RID: 5558
		private PropertyTab properties_tab;

		// Token: 0x040015B7 RID: 5559
		private PropertyTab selected_tab;

		// Token: 0x040015B8 RID: 5560
		private ImageList toolbar_imagelist;

		// Token: 0x040015B9 RID: 5561
		private Image categorized_image;

		// Token: 0x040015BA RID: 5562
		private Image alphabetical_image;

		// Token: 0x040015BB RID: 5563
		private Image propertypages_image;

		// Token: 0x040015BC RID: 5564
		private PropertyGrid.PropertyToolBarButton categorized_toolbarbutton;

		// Token: 0x040015BD RID: 5565
		private PropertyGrid.PropertyToolBarButton alphabetic_toolbarbutton;

		// Token: 0x040015BE RID: 5566
		private PropertyGrid.PropertyToolBarButton propertypages_toolbarbutton;

		// Token: 0x040015BF RID: 5567
		private PropertyGrid.PropertyToolBarSeparator separator_toolbarbutton;

		// Token: 0x040015C0 RID: 5568
		private bool events_tab_visible;

		// Token: 0x040015C1 RID: 5569
		private PropertyGrid.PropertyToolBar toolbar;

		// Token: 0x040015C2 RID: 5570
		private PropertyGridView property_grid_view;

		// Token: 0x040015C3 RID: 5571
		private Splitter splitter;

		// Token: 0x040015C4 RID: 5572
		private Panel help_panel;

		// Token: 0x040015C5 RID: 5573
		private Label help_title_label;

		// Token: 0x040015C6 RID: 5574
		private Label help_description_label;

		// Token: 0x040015C7 RID: 5575
		private MenuItem reset_menuitem;

		// Token: 0x040015C8 RID: 5576
		private MenuItem description_menuitem;

		// Token: 0x040015C9 RID: 5577
		private Color category_fore_color;

		// Token: 0x040015CA RID: 5578
		private Color commands_active_link_color;

		// Token: 0x040015CB RID: 5579
		private Color commands_disabled_link_color;

		// Token: 0x040015CC RID: 5580
		private Color commands_link_color;

		// Token: 0x040015D2 RID: 5586
		private static object ComComponentNameChangedEvent;

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.Design.PropertyTab" /> objects.</summary>
		// Token: 0x0200029E RID: 670
		public class PropertyTabCollection : ICollection, IEnumerable
		{
			// Token: 0x06002D29 RID: 11561 RVA: 0x000ADE44 File Offset: 0x000AC044
			internal PropertyTabCollection(PropertyGrid propertyGrid)
			{
				this.property_grid = propertyGrid;
				this.property_tabs = new ArrayList();
				this.property_tabs_scopes = new ArrayList();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>true to indicate the list is synchronized; otherwise false.</returns>
			// Token: 0x17000B7D RID: 2941
			// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000ADE6C File Offset: 0x000AC06C
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.property_tabs.IsSynchronized;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="dest">A zero-based array that receives the copied items from the collection.</param>
			/// <param name="index">The first position in the specified array to receive copied contents.</param>
			// Token: 0x06002D2B RID: 11563 RVA: 0x000ADE7C File Offset: 0x000AC07C
			void ICollection.CopyTo(Array dest, int index)
			{
				this.property_tabs.CopyTo(dest, index);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
			// Token: 0x17000B7E RID: 2942
			// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000ADE8C File Offset: 0x000AC08C
			object ICollection.SyncRoot
			{
				get
				{
					return this.property_tabs.SyncRoot;
				}
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Design.PropertyTab" /> at the specified index.</returns>
			/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> to return. </param>
			// Token: 0x17000B7F RID: 2943
			public PropertyTab this[int index]
			{
				get
				{
					return (PropertyTab)this.property_tabs[index];
				}
			}

			/// <summary>Returns an enumeration of all the Property tabs in the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.PropertyGrid.PropertyTabCollection" />.</returns>
			// Token: 0x06002D2E RID: 11566 RVA: 0x000ADEB0 File Offset: 0x000AC0B0
			public IEnumerator GetEnumerator()
			{
				return this.property_tabs.GetEnumerator();
			}

			/// <summary>Gets the number of Property tabs in the collection.</summary>
			/// <returns>The number of Property tabs in the collection.</returns>
			// Token: 0x17000B80 RID: 2944
			// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000ADEC0 File Offset: 0x000AC0C0
			public int Count
			{
				get
				{
					return this.property_tabs.Count;
				}
			}

			/// <summary>Adds a Property tab of the specified type to the collection.</summary>
			/// <param name="propertyTabType">The Property tab type to add to the grid. </param>
			// Token: 0x06002D30 RID: 11568 RVA: 0x000ADED0 File Offset: 0x000AC0D0
			public void AddTabType(Type propertyTabType)
			{
				this.AddTabType(propertyTabType, 1);
			}

			/// <summary>Adds a Property tab of the specified type and with the specified scope to the collection.</summary>
			/// <param name="propertyTabType">The Property tab type to add to the grid. </param>
			/// <param name="tabScope">One of the <see cref="T:System.ComponentModel.PropertyTabScope" /> values. </param>
			// Token: 0x06002D31 RID: 11569 RVA: 0x000ADEDC File Offset: 0x000AC0DC
			public void AddTabType(Type propertyTabType, PropertyTabScope tabScope)
			{
				if (propertyTabType == null)
				{
					throw new ArgumentNullException("propertyTabType");
				}
				if (this.Contains(propertyTabType))
				{
					return;
				}
				PropertyTab propertyTab = this.property_grid.CreatePropertyTab(propertyTabType);
				if (propertyTab != null)
				{
					this.property_tabs.Add(propertyTab);
					this.property_tabs_scopes.Add(tabScope);
				}
				this.property_grid.RefreshToolbar(this);
			}

			// Token: 0x06002D32 RID: 11570 RVA: 0x000ADF48 File Offset: 0x000AC148
			internal PropertyTabScope GetTabScope(PropertyTab tab)
			{
				if (tab == null)
				{
					throw new ArgumentNullException("tab");
				}
				int num = this.property_tabs.IndexOf(tab);
				if (num != -1)
				{
					return (int)this.property_tabs_scopes[num];
				}
				return 1;
			}

			// Token: 0x06002D33 RID: 11571 RVA: 0x000ADF90 File Offset: 0x000AC190
			internal void InsertTab(int index, PropertyTab propertyTab, PropertyTabScope tabScope)
			{
				if (propertyTab == null)
				{
					throw new ArgumentNullException("propertyTab");
				}
				if (!this.Contains(propertyTab.GetType()))
				{
					this.property_tabs.Insert(index, propertyTab);
					this.property_tabs_scopes.Insert(index, tabScope);
				}
			}

			// Token: 0x06002D34 RID: 11572 RVA: 0x000ADFE0 File Offset: 0x000AC1E0
			internal bool Contains(Type propertyType)
			{
				if (propertyType == null)
				{
					throw new ArgumentNullException("propertyType");
				}
				foreach (object obj in this.property_tabs)
				{
					PropertyTab propertyTab = (PropertyTab)obj;
					if (propertyTab.GetType() == propertyType)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x17000B81 RID: 2945
			internal PropertyTab this[Type tabType]
			{
				get
				{
					foreach (object obj in this.property_tabs)
					{
						PropertyTab propertyTab = (PropertyTab)obj;
						if (tabType == propertyTab.GetType())
						{
							return propertyTab;
						}
					}
					return null;
				}
			}

			/// <summary>Removes all the Property tabs of the specified scope from the collection.</summary>
			/// <param name="tabScope">The scope of the tabs to clear. </param>
			/// <exception cref="T:System.ArgumentException">The assigned value of the <paramref name="tabScope" /> parameter is less than the Document value of <see cref="T:System.ComponentModel.PropertyTabScope" />. </exception>
			// Token: 0x06002D36 RID: 11574 RVA: 0x000AE0F0 File Offset: 0x000AC2F0
			public void Clear(PropertyTabScope tabScope)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.property_tabs_scopes.Count; i++)
				{
					if ((int)this.property_tabs_scopes[i] == tabScope)
					{
						arrayList.Add(i);
					}
				}
				foreach (object obj in arrayList)
				{
					int num = (int)obj;
					this.property_tabs.RemoveAt(num);
					this.property_tabs_scopes.RemoveAt(num);
				}
				this.property_grid.RefreshToolbar(this);
			}

			/// <summary>Removes the specified tab type from the collection.</summary>
			/// <param name="propertyTabType">The tab type to remove from the collection. </param>
			// Token: 0x06002D37 RID: 11575 RVA: 0x000AE1C0 File Offset: 0x000AC3C0
			public void RemoveTabType(Type propertyTabType)
			{
				if (propertyTabType == null)
				{
					throw new ArgumentNullException("propertyTabType");
				}
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.property_tabs.Count; i++)
				{
					if (this.property_tabs[i].GetType() == propertyTabType)
					{
						arrayList.Add(i);
					}
				}
				foreach (object obj in arrayList)
				{
					int num = (int)obj;
					this.property_tabs.RemoveAt(num);
					this.property_tabs_scopes.RemoveAt(num);
				}
				this.property_grid.RefreshToolbar(this);
			}

			// Token: 0x040015D3 RID: 5587
			private ArrayList property_tabs;

			// Token: 0x040015D4 RID: 5588
			private ArrayList property_tabs_scopes;

			// Token: 0x040015D5 RID: 5589
			private PropertyGrid property_grid;
		}

		// Token: 0x0200029F RID: 671
		internal class BorderHelperControl : Control
		{
			// Token: 0x06002D38 RID: 11576 RVA: 0x000AE2A4 File Offset: 0x000AC4A4
			public BorderHelperControl()
			{
				this.BackColor = ThemeEngine.Current.ColorWindow;
			}

			// Token: 0x06002D39 RID: 11577 RVA: 0x000AE2BC File Offset: 0x000AC4BC
			protected override void OnPaint(PaintEventArgs e)
			{
				e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, base.Width - 1, base.Height - 1);
				base.OnPaint(e);
			}

			// Token: 0x06002D3A RID: 11578 RVA: 0x000AE2F4 File Offset: 0x000AC4F4
			protected override void OnSizeChanged(EventArgs e)
			{
				if (base.Controls.Count == 1)
				{
					Control control = base.Controls[0];
					if (control.Location.X != 1 || control.Location.Y != 1)
					{
						control.Location = new Point(1, 1);
					}
					control.Width = base.ClientRectangle.Width - 2;
					control.Height = base.ClientRectangle.Height - 2;
				}
				base.OnSizeChanged(e);
			}
		}

		// Token: 0x020002A0 RID: 672
		private class PropertyToolBarSeparator : ToolStripSeparator
		{
		}

		// Token: 0x020002A1 RID: 673
		private class PropertyToolBarButton : ToolStripButton
		{
			// Token: 0x06002D3C RID: 11580 RVA: 0x000AE390 File Offset: 0x000AC590
			public PropertyToolBarButton()
			{
			}

			// Token: 0x06002D3D RID: 11581 RVA: 0x000AE398 File Offset: 0x000AC598
			public PropertyToolBarButton(PropertyTab propertyTab)
			{
				if (propertyTab == null)
				{
					throw new ArgumentNullException("propertyTab");
				}
				this.property_tab = propertyTab;
			}

			// Token: 0x17000B82 RID: 2946
			// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000AE3B8 File Offset: 0x000AC5B8
			public PropertyTab PropertyTab
			{
				get
				{
					return this.property_tab;
				}
			}

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000AE3C0 File Offset: 0x000AC5C0
			// (set) Token: 0x06002D40 RID: 11584 RVA: 0x000AE3C8 File Offset: 0x000AC5C8
			public bool Pushed
			{
				get
				{
					return base.Checked;
				}
				set
				{
					base.Checked = value;
				}
			}

			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000AE3D4 File Offset: 0x000AC5D4
			// (set) Token: 0x06002D42 RID: 11586 RVA: 0x000AE3D8 File Offset: 0x000AC5D8
			public ToolBarButtonStyle Style
			{
				get
				{
					return ToolBarButtonStyle.PushButton;
				}
				set
				{
				}
			}

			// Token: 0x040015D6 RID: 5590
			private PropertyTab property_tab;
		}

		// Token: 0x020002A2 RID: 674
		internal class PropertyToolBar : ToolStrip
		{
			// Token: 0x06002D43 RID: 11587 RVA: 0x000AE3DC File Offset: 0x000AC5DC
			public PropertyToolBar()
			{
				base.SetStyle(ControlStyles.ResizeRedraw, true);
				base.GripStyle = ToolStripGripStyle.Hidden;
				this.appearance = ToolBarAppearance.Normal;
			}

			// Token: 0x17000B85 RID: 2949
			// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000AE3FC File Offset: 0x000AC5FC
			// (set) Token: 0x06002D45 RID: 11589 RVA: 0x000AE404 File Offset: 0x000AC604
			public bool ShowToolTips
			{
				get
				{
					return base.ShowItemToolTips;
				}
				set
				{
					base.ShowItemToolTips = value;
				}
			}

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000AE410 File Offset: 0x000AC610
			// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000AE418 File Offset: 0x000AC618
			public ToolBarAppearance Appearance
			{
				get
				{
					return this.appearance;
				}
				set
				{
					if (value == this.Appearance)
					{
						return;
					}
					if (value != ToolBarAppearance.Normal)
					{
						if (value == ToolBarAppearance.Flat)
						{
							base.Renderer = new ToolStripSystemRenderer();
							this.appearance = ToolBarAppearance.Flat;
						}
					}
					else
					{
						base.Renderer = new ToolStripProfessionalRenderer(new ProfessionalColorTable
						{
							UseSystemColors = true
						});
						this.appearance = ToolBarAppearance.Normal;
					}
				}
			}

			// Token: 0x040015D7 RID: 5591
			private ToolBarAppearance appearance;
		}

		// Token: 0x020002A3 RID: 675
		[MonoInternalNote("not sure what this class does, but it's listed as a type converter for a property in this class, and this causes problems if it's not present")]
		private class SelectedObjectConverter : TypeConverter
		{
		}
	}
}
