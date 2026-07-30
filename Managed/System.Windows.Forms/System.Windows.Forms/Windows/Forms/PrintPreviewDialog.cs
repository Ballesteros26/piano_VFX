using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a dialog box form that contains a <see cref="T:System.Windows.Forms.PrintPreviewControl" /> for printing from a Windows Forms application.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000295 RID: 661
	[ComVisible(true)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel", 0)]
	[ClassInterface(1)]
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Document")]
	public partial class PrintPreviewDialog : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class.</summary>
		// Token: 0x06002AEB RID: 10987 RVA: 0x000A6250 File Offset: 0x000A4450
		public PrintPreviewDialog()
		{
			base.ClientSize = new Size(400, 300);
			ToolBar toolBar = this.CreateToolBar();
			toolBar.Location = new Point(0, 0);
			toolBar.Dock = DockStyle.Top;
			base.Controls.Add(toolBar);
			this.print_preview = new PrintPreviewControl();
			this.print_preview.Location = new Point(0, toolBar.Location.Y + toolBar.Size.Height);
			this.print_preview.Size = new Size(base.ClientSize.Width, base.ClientSize.Height - toolBar.Bottom);
			this.print_preview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.print_preview.TabStop = false;
			base.Controls.Add(this.print_preview);
			this.print_preview.Show();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoSize" /> property changes.</summary>
		// Token: 0x14000282 RID: 642
		// (add) Token: 0x06002AEC RID: 10988 RVA: 0x000A6344 File Offset: 0x000A4544
		// (remove) Token: 0x06002AED RID: 10989 RVA: 0x000A6350 File Offset: 0x000A4550
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.AutoValidate" /> property changes.</summary>
		// Token: 0x14000283 RID: 643
		// (add) Token: 0x06002AEE RID: 10990 RVA: 0x000A635C File Offset: 0x000A455C
		// (remove) Token: 0x06002AEF RID: 10991 RVA: 0x000A6368 File Offset: 0x000A4568
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000284 RID: 644
		// (add) Token: 0x06002AF0 RID: 10992 RVA: 0x000A6374 File Offset: 0x000A4574
		// (remove) Token: 0x06002AF1 RID: 10993 RVA: 0x000A6380 File Offset: 0x000A4580
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000285 RID: 645
		// (add) Token: 0x06002AF2 RID: 10994 RVA: 0x000A638C File Offset: 0x000A458C
		// (remove) Token: 0x06002AF3 RID: 10995 RVA: 0x000A6398 File Offset: 0x000A4598
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000286 RID: 646
		// (add) Token: 0x06002AF4 RID: 10996 RVA: 0x000A63A4 File Offset: 0x000A45A4
		// (remove) Token: 0x06002AF5 RID: 10997 RVA: 0x000A63B0 File Offset: 0x000A45B0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.CausesValidation" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000287 RID: 647
		// (add) Token: 0x06002AF6 RID: 10998 RVA: 0x000A63BC File Offset: 0x000A45BC
		// (remove) Token: 0x06002AF7 RID: 10999 RVA: 0x000A63C8 File Offset: 0x000A45C8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenu" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000288 RID: 648
		// (add) Token: 0x06002AF8 RID: 11000 RVA: 0x000A63D4 File Offset: 0x000A45D4
		// (remove) Token: 0x06002AF9 RID: 11001 RVA: 0x000A63E0 File Offset: 0x000A45E0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				base.ContextMenuChanged += value;
			}
			remove
			{
				base.ContextMenuChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x14000289 RID: 649
		// (add) Token: 0x06002AFA RID: 11002 RVA: 0x000A63EC File Offset: 0x000A45EC
		// (remove) Token: 0x06002AFB RID: 11003 RVA: 0x000A63F8 File Offset: 0x000A45F8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Cursor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028A RID: 650
		// (add) Token: 0x06002AFC RID: 11004 RVA: 0x000A6404 File Offset: 0x000A4604
		// (remove) Token: 0x06002AFD RID: 11005 RVA: 0x000A6410 File Offset: 0x000A4610
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Dock" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028B RID: 651
		// (add) Token: 0x06002AFE RID: 11006 RVA: 0x000A641C File Offset: 0x000A461C
		// (remove) Token: 0x06002AFF RID: 11007 RVA: 0x000A6428 File Offset: 0x000A4628
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Enabled" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028C RID: 652
		// (add) Token: 0x06002B00 RID: 11008 RVA: 0x000A6434 File Offset: 0x000A4634
		// (remove) Token: 0x06002B01 RID: 11009 RVA: 0x000A6440 File Offset: 0x000A4640
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Font" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028D RID: 653
		// (add) Token: 0x06002B02 RID: 11010 RVA: 0x000A644C File Offset: 0x000A464C
		// (remove) Token: 0x06002B03 RID: 11011 RVA: 0x000A6458 File Offset: 0x000A4658
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028E RID: 654
		// (add) Token: 0x06002B04 RID: 11012 RVA: 0x000A6464 File Offset: 0x000A4664
		// (remove) Token: 0x06002B05 RID: 11013 RVA: 0x000A6470 File Offset: 0x000A4670
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400028F RID: 655
		// (add) Token: 0x06002B06 RID: 11014 RVA: 0x000A647C File Offset: 0x000A467C
		// (remove) Token: 0x06002B07 RID: 11015 RVA: 0x000A6488 File Offset: 0x000A4688
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Location" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000290 RID: 656
		// (add) Token: 0x06002B08 RID: 11016 RVA: 0x000A6494 File Offset: 0x000A4694
		// (remove) Token: 0x06002B09 RID: 11017 RVA: 0x000A64A0 File Offset: 0x000A46A0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Margin" /> property changes.</summary>
		// Token: 0x14000291 RID: 657
		// (add) Token: 0x06002B0A RID: 11018 RVA: 0x000A64AC File Offset: 0x000A46AC
		// (remove) Token: 0x06002B0B RID: 11019 RVA: 0x000A64B8 File Offset: 0x000A46B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MaximumSize" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000292 RID: 658
		// (add) Token: 0x06002B0C RID: 11020 RVA: 0x000A64C4 File Offset: 0x000A46C4
		// (remove) Token: 0x06002B0D RID: 11021 RVA: 0x000A64D0 File Offset: 0x000A46D0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MaximumSizeChanged
		{
			add
			{
				base.MaximumSizeChanged += value;
			}
			remove
			{
				base.MaximumSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MinimumSize" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000293 RID: 659
		// (add) Token: 0x06002B0E RID: 11022 RVA: 0x000A64DC File Offset: 0x000A46DC
		// (remove) Token: 0x06002B0F RID: 11023 RVA: 0x000A64E8 File Offset: 0x000A46E8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MinimumSizeChanged
		{
			add
			{
				base.MinimumSizeChanged += value;
			}
			remove
			{
				base.MinimumSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Padding" /> property changes.</summary>
		// Token: 0x14000294 RID: 660
		// (add) Token: 0x06002B10 RID: 11024 RVA: 0x000A64F4 File Offset: 0x000A46F4
		// (remove) Token: 0x06002B11 RID: 11025 RVA: 0x000A6500 File Offset: 0x000A4700
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeft" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000295 RID: 661
		// (add) Token: 0x06002B12 RID: 11026 RVA: 0x000A650C File Offset: 0x000A470C
		// (remove) Token: 0x06002B13 RID: 11027 RVA: 0x000A6518 File Offset: 0x000A4718
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000296 RID: 662
		// (add) Token: 0x06002B14 RID: 11028 RVA: 0x000A6524 File Offset: 0x000A4724
		// (remove) Token: 0x06002B15 RID: 11029 RVA: 0x000A6530 File Offset: 0x000A4730
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.RightToLeftLayoutChanged += value;
			}
			remove
			{
				base.RightToLeftLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Size" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000297 RID: 663
		// (add) Token: 0x06002B16 RID: 11030 RVA: 0x000A653C File Offset: 0x000A473C
		// (remove) Token: 0x06002B17 RID: 11031 RVA: 0x000A6548 File Offset: 0x000A4748
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler SizeChanged
		{
			add
			{
				base.SizeChanged += value;
			}
			remove
			{
				base.SizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000298 RID: 664
		// (add) Token: 0x06002B18 RID: 11032 RVA: 0x000A6554 File Offset: 0x000A4754
		// (remove) Token: 0x06002B19 RID: 11033 RVA: 0x000A6560 File Offset: 0x000A4760
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000299 RID: 665
		// (add) Token: 0x06002B1A RID: 11034 RVA: 0x000A656C File Offset: 0x000A476C
		// (remove) Token: 0x06002B1B RID: 11035 RVA: 0x000A6578 File Offset: 0x000A4778
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Visible" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029A RID: 666
		// (add) Token: 0x06002B1C RID: 11036 RVA: 0x000A6584 File Offset: 0x000A4784
		// (remove) Token: 0x06002B1D RID: 11037 RVA: 0x000A6590 File Offset: 0x000A4790
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A659C File Offset: 0x000A479C
		private ToolBar CreateToolBar()
		{
			ImageList imageList = new ImageList();
			imageList.Images.Add(ResourceImageLoader.Get("32_printer.png"));
			imageList.Images.Add(ResourceImageLoader.Get("22_page-magnifier.png"));
			imageList.Images.Add(ResourceImageLoader.Get("1-up.png"));
			imageList.Images.Add(ResourceImageLoader.Get("2-up.png"));
			imageList.Images.Add(ResourceImageLoader.Get("3-up.png"));
			imageList.Images.Add(ResourceImageLoader.Get("4-up.png"));
			imageList.Images.Add(ResourceImageLoader.Get("6-up.png"));
			this.mag_menu = new ContextMenu();
			ToolBar toolBar = new PrintPreviewDialog.PrintToolBar();
			ToolBarButton toolBarButton = new ToolBarButton();
			ToolBarButton toolBarButton2 = new ToolBarButton();
			ToolBarButton toolBarButton3 = new ToolBarButton();
			ToolBarButton toolBarButton4 = new ToolBarButton();
			ToolBarButton toolBarButton5 = new ToolBarButton();
			ToolBarButton toolBarButton6 = new ToolBarButton();
			ToolBarButton toolBarButton7 = new ToolBarButton();
			ToolBarButton toolBarButton8 = new ToolBarButton();
			ToolBarButton toolBarButton9 = new ToolBarButton();
			Button button = new Button();
			Label label = new Label();
			this.pageUpDown = new NumericUpDown();
			toolBar.ImageList = imageList;
			toolBar.Size = new Size(792, 26);
			toolBar.Dock = DockStyle.Top;
			toolBar.Appearance = ToolBarAppearance.Flat;
			toolBar.ShowToolTips = true;
			toolBar.DropDownArrows = true;
			toolBar.TabStop = true;
			toolBar.Buttons.AddRange(new ToolBarButton[] { toolBarButton, toolBarButton2, toolBarButton3, toolBarButton4, toolBarButton5, toolBarButton6, toolBarButton7, toolBarButton8, toolBarButton9 });
			toolBar.ButtonClick += this.OnClickToolBarButton;
			toolBarButton.ImageIndex = 0;
			toolBarButton.Tag = 0;
			toolBarButton.ToolTipText = "Print";
			toolBarButton2.ImageIndex = 1;
			toolBarButton2.Tag = 1;
			toolBarButton2.ToolTipText = "Zoom";
			toolBarButton2.Style = ToolBarButtonStyle.DropDownButton;
			toolBarButton2.DropDownMenu = this.mag_menu;
			MenuItem menuItem = this.mag_menu.MenuItems.Add("Auto", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem.Checked = true;
			this.previous_checked_menu_item = menuItem;
			this.auto_zoom_item = menuItem;
			menuItem = this.mag_menu.MenuItems.Add("500%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("200%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("150%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("100%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("75%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("50%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("25%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			menuItem = this.mag_menu.MenuItems.Add("10%", new EventHandler(this.OnClickPageMagnifierItem));
			menuItem.RadioCheck = true;
			toolBarButton3.Style = ToolBarButtonStyle.Separator;
			toolBarButton4.ImageIndex = 2;
			toolBarButton4.Tag = 2;
			toolBarButton4.ToolTipText = "One page";
			toolBarButton5.ImageIndex = 3;
			toolBarButton5.Tag = 3;
			toolBarButton5.ToolTipText = "Two pages";
			toolBarButton6.ImageIndex = 4;
			toolBarButton6.Tag = 4;
			toolBarButton6.ToolTipText = "Three pages";
			toolBarButton7.ImageIndex = 5;
			toolBarButton7.Tag = 5;
			toolBarButton7.ToolTipText = "Four pages";
			toolBarButton8.ImageIndex = 6;
			toolBarButton8.Tag = 6;
			toolBarButton8.ToolTipText = "Six pages";
			toolBarButton9.Style = ToolBarButtonStyle.Separator;
			label.Text = "Page";
			label.TabStop = false;
			label.Size = new Size(50, 18);
			label.TextAlign = 16;
			label.Dock = DockStyle.Right;
			this.pageUpDown.Dock = DockStyle.Right;
			this.pageUpDown.TextAlign = HorizontalAlignment.Right;
			this.pageUpDown.DecimalPlaces = 0;
			this.pageUpDown.TabIndex = 1;
			this.pageUpDown.Text = "1";
			this.pageUpDown.Minimum = 0m;
			this.pageUpDown.Maximum = 1000m;
			this.pageUpDown.Size = new Size(64, 14);
			this.pageUpDown.Dock = DockStyle.Right;
			this.pageUpDown.ValueChanged += new EventHandler(this.OnPageUpDownValueChanged);
			button.Location = new Point(196, 2);
			button.Size = new Size(50, 20);
			button.TabIndex = 0;
			button.FlatStyle = FlatStyle.Popup;
			button.Text = "Close";
			button.Click += new EventHandler(this.CloseButtonClicked);
			toolBar.Controls.Add(label);
			toolBar.Controls.Add(this.pageUpDown);
			toolBar.Controls.Add(button);
			return toolBar;
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000A6B14 File Offset: 0x000A4D14
		private void CloseButtonClicked(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000A6B1C File Offset: 0x000A4D1C
		private void OnPageUpDownValueChanged(object sender, EventArgs e)
		{
			this.print_preview.StartPage = (int)this.pageUpDown.Value;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		private void OnClickToolBarButton(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Tag == null || !(e.Button.Tag is int))
			{
				return;
			}
			switch ((int)e.Button.Tag)
			{
			case 0:
				Console.WriteLine("do print here");
				break;
			case 1:
				this.OnClickPageMagnifierItem(this.auto_zoom_item, EventArgs.Empty);
				break;
			case 2:
				this.print_preview.Rows = 0;
				this.print_preview.Columns = 1;
				break;
			case 3:
				this.print_preview.Rows = 0;
				this.print_preview.Columns = 2;
				break;
			case 4:
				this.print_preview.Rows = 0;
				this.print_preview.Columns = 3;
				break;
			case 5:
				this.print_preview.Rows = 1;
				this.print_preview.Columns = 2;
				break;
			case 6:
				this.print_preview.Rows = 1;
				this.print_preview.Columns = 3;
				break;
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A6C60 File Offset: 0x000A4E60
		private void OnClickPageMagnifierItem(object sender, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			this.previous_checked_menu_item.Checked = false;
			switch (menuItem.Index)
			{
			case 0:
				this.print_preview.AutoZoom = true;
				break;
			case 1:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 5.0;
				break;
			case 2:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 2.0;
				break;
			case 3:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 1.5;
				break;
			case 4:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 1.0;
				break;
			case 5:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 0.75;
				break;
			case 6:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 0.5;
				break;
			case 7:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 0.25;
				break;
			case 8:
				this.print_preview.AutoZoom = false;
				this.print_preview.Zoom = 0.1;
				break;
			}
			menuItem.Checked = true;
			this.previous_checked_menu_item = menuItem;
		}

		/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000A6E00 File Offset: 0x000A5000
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000A6E08 File Offset: 0x000A5008
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			set
			{
				base.AcceptButton = value;
			}
		}

		/// <summary>Gets or sets the accessible description of the control.</summary>
		/// <returns>The accessible description of the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000A6E14 File Offset: 0x000A5014
		// (set) Token: 0x06002B26 RID: 11046 RVA: 0x000A6E1C File Offset: 0x000A501C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new string AccessibleDescription
		{
			get
			{
				return base.AccessibleDescription;
			}
			set
			{
				base.AccessibleDescription = value;
			}
		}

		/// <summary>Gets or sets the accessible name of the control.</summary>
		/// <returns>The accessible name of the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000A6E28 File Offset: 0x000A5028
		// (set) Token: 0x06002B28 RID: 11048 RVA: 0x000A6E30 File Offset: 0x000A5030
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new string AccessibleName
		{
			get
			{
				return base.AccessibleName;
			}
			set
			{
				base.AccessibleName = value;
			}
		}

		/// <summary>The accessible role of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.Default" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000A6E3C File Offset: 0x000A503C
		// (set) Token: 0x06002B2A RID: 11050 RVA: 0x000A6E44 File Offset: 0x000A5044
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new AccessibleRole AccessibleRole
		{
			get
			{
				return base.AccessibleRole;
			}
			set
			{
				base.AccessibleRole = value;
			}
		}

		/// <summary>Gets or sets whether the control can accept data that the user drags onto it.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000A6E50 File Offset: 0x000A5050
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x000A6E58 File Offset: 0x000A5058
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Gets or sets the anchor style for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000A6E64 File Offset: 0x000A5064
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000A6E6C File Offset: 0x000A506C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
		/// <returns>true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000A6E78 File Offset: 0x000A5078
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x000A6E80 File Offset: 0x000A5080
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool AutoScale
		{
			get
			{
				return base.AutoScale;
			}
			set
			{
				base.AutoScale = value;
			}
		}

		/// <summary>The <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class does not support the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoScaleBaseSize" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000A6E8C File Offset: 0x000A508C
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000A6E94 File Offset: 0x000A5094
		[Browsable(false)]
		[Obsolete("This property has been deprecated.  Use AutoScaleDimensions instead.")]
		[EditorBrowsable(1)]
		public override Size AutoScaleBaseSize
		{
			get
			{
				return base.AutoScaleBaseSize;
			}
			set
			{
				base.AutoScaleBaseSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000A6EA0 File Offset: 0x000A50A0
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000A6EA8 File Offset: 0x000A50A8
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000A6EB4 File Offset: 0x000A50B4
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000A6EBC File Offset: 0x000A50BC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the automatic scroll bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000A6EC8 File Offset: 0x000A50C8
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000A6ED0 File Offset: 0x000A50D0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should automatically resize to fit its contents.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should resize to fit its contents; otherwise, false.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000A6EDC File Offset: 0x000A50DC
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000A6EE4 File Offset: 0x000A50E4
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets how the control performs validation when the user changes focus to another control.</summary>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000A6EF0 File Offset: 0x000A50F0
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000A6EF8 File Offset: 0x000A50F8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <summary>Gets or sets the background color of the form.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000A6F04 File Offset: 0x000A5104
		// (set) Token: 0x06002B3E RID: 11070 RVA: 0x000A6F0C File Offset: 0x000A510C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background image for the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000A6F18 File Offset: 0x000A5118
		// (set) Token: 0x06002B40 RID: 11072 RVA: 0x000A6F20 File Offset: 0x000A5120
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000A6F2C File Offset: 0x000A512C
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x000A6F34 File Offset: 0x000A5134
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the cancel button for the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000A6F40 File Offset: 0x000A5140
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x000A6F48 File Offset: 0x000A5148
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			set
			{
				base.CancelButton = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether entering the control causes validation for all controls that require validation.</summary>
		/// <returns>true if entering the control causes validation to be performed on controls requiring validation; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000A6F54 File Offset: 0x000A5154
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x000A6F5C File Offset: 0x000A515C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000A6F68 File Offset: 0x000A5168
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000A6F70 File Offset: 0x000A5170
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		/// <summary>Gets or sets how the short cut menu for the control.</summary>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000A6F7C File Offset: 0x000A517C
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000A6F84 File Offset: 0x000A5184
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
		/// <returns>true if the form displays a control box in the upper-left corner of the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000A6F90 File Offset: 0x000A5190
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x000A6F98 File Offset: 0x000A5198
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool ControlBox
		{
			get
			{
				return base.ControlBox;
			}
			set
			{
				base.ControlBox = value;
			}
		}

		/// <summary>Gets or sets the cursor for the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000A6FA4 File Offset: 0x000A51A4
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x000A6FAC File Offset: 0x000A51AC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Gets the data bindings for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000A6FB8 File Offset: 0x000A51B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ControlBindingsCollection DataBindings
		{
			get
			{
				return base.DataBindings;
			}
		}

		/// <summary>Gets the default minimum size, in pixels, of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure representing the default minimum size.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000A6FC0 File Offset: 0x000A51C0
		protected override Size DefaultMinimumSize
		{
			get
			{
				return new Size(370, 300);
			}
		}

		/// <summary>Gets or sets how the control should be docked in its parent control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000A6FD4 File Offset: 0x000A51D4
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x000A6FDC File Offset: 0x000A51DC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.ScrollableControl.DockPadding" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000A6FE8 File Offset: 0x000A51E8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Gets or sets the document to preview.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000A6FF0 File Offset: 0x000A51F0
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x000A7000 File Offset: 0x000A5200
		[DefaultValue(null)]
		public PrintDocument Document
		{
			get
			{
				return this.print_preview.Document;
			}
			set
			{
				this.print_preview.Document = value;
			}
		}

		/// <summary>Get or sets a value indicating whether the control is enabled.</summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000A7010 File Offset: 0x000A5210
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x000A7018 File Offset: 0x000A5218
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets or sets the font used for the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000A7024 File Offset: 0x000A5224
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000A702C File Offset: 0x000A522C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000A7038 File Offset: 0x000A5238
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x000A7040 File Offset: 0x000A5240
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the border style of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is <see cref="F:System.Windows.Forms.FormBorderStyle.Sizable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000A704C File Offset: 0x000A524C
		// (set) Token: 0x06002B5D RID: 11101 RVA: 0x000A7054 File Offset: 0x000A5254
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new FormBorderStyle FormBorderStyle
		{
			get
			{
				return base.FormBorderStyle;
			}
			set
			{
				base.FormBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
		/// <returns>true to display a Help button in the form's caption bar; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000A7060 File Offset: 0x000A5260
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000A7068 File Offset: 0x000A5268
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool HelpButton
		{
			get
			{
				return base.HelpButton;
			}
			set
			{
				base.HelpButton = value;
			}
		}

		/// <summary>Gets or sets the icon for the form.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000A7074 File Offset: 0x000A5274
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x000A707C File Offset: 0x000A527C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Icon Icon
		{
			get
			{
				return base.Icon;
			}
			set
			{
				base.Icon = value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000A7088 File Offset: 0x000A5288
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x000A7090 File Offset: 0x000A5290
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is a container for multiple document interface (MDI) child forms.</summary>
		/// <returns>true if the form is a container for MDI child forms; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000A709C File Offset: 0x000A529C
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x000A70A4 File Offset: 0x000A52A4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool IsMdiContainer
		{
			get
			{
				return base.IsMdiContainer;
			}
			set
			{
				base.IsMdiContainer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.</summary>
		/// <returns>true if the form will receive all key events; false if the currently selected control on the form receives key events. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000A70B0 File Offset: 0x000A52B0
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000A70B8 File Offset: 0x000A52B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool KeyPreview
		{
			get
			{
				return base.KeyPreview;
			}
			set
			{
				base.KeyPreview = value;
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000A70C4 File Offset: 0x000A52C4
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x000A70CC File Offset: 0x000A52CC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Gets or sets the margins for the control.</summary>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000A70D8 File Offset: 0x000A52D8
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000A70E0 File Offset: 0x000A52E0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a maximize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000A70EC File Offset: 0x000A52EC
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000A70F4 File Offset: 0x000A52F4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool MaximizeBox
		{
			get
			{
				return base.MaximizeBox;
			}
			set
			{
				base.MaximizeBox = value;
			}
		}

		/// <summary>Gets or sets the maximum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000A7100 File Offset: 0x000A5300
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000A7108 File Offset: 0x000A5308
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000A7114 File Offset: 0x000A5314
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000A711C File Offset: 0x000A531C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new MainMenu Menu
		{
			get
			{
				return base.Menu;
			}
			set
			{
				base.Menu = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a minimize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000A7128 File Offset: 0x000A5328
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x000A7130 File Offset: 0x000A5330
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool MinimizeBox
		{
			get
			{
				return base.MinimizeBox;
			}
			set
			{
				base.MinimizeBox = value;
			}
		}

		/// <summary>Gets the minimum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000A713C File Offset: 0x000A533C
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000A7144 File Offset: 0x000A5344
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>Gets or sets the opacity level of the form.</summary>
		/// <returns>The level of opacity for the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000A7150 File Offset: 0x000A5350
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000A7158 File Offset: 0x000A5358
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new double Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				base.Opacity = value;
			}
		}

		/// <summary>Gets or sets the padding for the control.</summary>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000A7164 File Offset: 0x000A5364
		// (set) Token: 0x06002B79 RID: 11129 RVA: 0x000A716C File Offset: 0x000A536C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000A7178 File Offset: 0x000A5378
		[Browsable(false)]
		public PrintPreviewControl PrintPreviewControl
		{
			get
			{
				return this.print_preview;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000A7180 File Offset: 0x000A5380
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x000A7188 File Offset: 0x000A5388
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should be laid out from right to left.</summary>
		/// <returns>true to indicate the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> contents should be laid out from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000A7194 File Offset: 0x000A5394
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000A719C File Offset: 0x000A539C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override bool RightToLeftLayout
		{
			get
			{
				return base.RightToLeftLayout;
			}
			set
			{
				base.RightToLeftLayout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
		/// <returns>true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000A71A8 File Offset: 0x000A53A8
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x000A71B0 File Offset: 0x000A53B0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DefaultValue(false)]
		public new bool ShowInTaskbar
		{
			get
			{
				return base.ShowInTaskbar;
			}
			set
			{
				base.ShowInTaskbar = value;
			}
		}

		/// <summary>Gets or sets the size of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000A71BC File Offset: 0x000A53BC
		// (set) Token: 0x06002B82 RID: 11138 RVA: 0x000A71C4 File Offset: 0x000A53C4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000A71D0 File Offset: 0x000A53D0
		// (set) Token: 0x06002B84 RID: 11140 RVA: 0x000A71D8 File Offset: 0x000A53D8
		[Browsable(false)]
		[DefaultValue(SizeGripStyle.Hide)]
		[EditorBrowsable(1)]
		public new SizeGripStyle SizeGripStyle
		{
			get
			{
				return base.SizeGripStyle;
			}
			set
			{
				base.SizeGripStyle = value;
			}
		}

		/// <summary>Gets or sets the starting position of the dialog box at run time.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the dialog box.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000A71E4 File Offset: 0x000A53E4
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x000A71EC File Offset: 0x000A53EC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new FormStartPosition StartPosition
		{
			get
			{
				return base.StartPosition;
			}
			set
			{
				base.StartPosition = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give the focus to this control using the TAB key; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000A71F8 File Offset: 0x000A53F8
		// (set) Token: 0x06002B88 RID: 11144 RVA: 0x000A7200 File Offset: 0x000A5400
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the object that contains data about the control.</summary>
		/// <returns>An object that contains data about the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000A720C File Offset: 0x000A540C
		// (set) Token: 0x06002B8A RID: 11146 RVA: 0x000A7214 File Offset: 0x000A5414
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
			}
		}

		/// <summary>Gets or sets the text displayed on the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000A7220 File Offset: 0x000A5420
		// (set) Token: 0x06002B8C RID: 11148 RVA: 0x000A7228 File Offset: 0x000A5428
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether the form should be displayed as the topmost form of your application.</summary>
		/// <returns>true to display the form as a topmost form; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000A7234 File Offset: 0x000A5434
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x000A723C File Offset: 0x000A543C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool TopMost
		{
			get
			{
				return base.TopMost;
			}
			set
			{
				base.TopMost = value;
			}
		}

		/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000A7248 File Offset: 0x000A5448
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000A7250 File Offset: 0x000A5450
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Color TransparencyKey
		{
			get
			{
				return base.TransparencyKey;
			}
			set
			{
				base.TransparencyKey = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
		/// <returns>true if anti-aliasing is used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000A725C File Offset: 0x000A545C
		// (set) Token: 0x06002B92 RID: 11154 RVA: 0x000A726C File Offset: 0x000A546C
		[DefaultValue(false)]
		public bool UseAntiAlias
		{
			get
			{
				return this.print_preview.UseAntiAlias;
			}
			set
			{
				this.print_preview.UseAntiAlias = value;
			}
		}

		/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000A727C File Offset: 0x000A547C
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000A7284 File Offset: 0x000A5484
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				base.UseWaitCursor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visible.</summary>
		/// <returns>This property is not relevant for this class.true if the control is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000A7290 File Offset: 0x000A5490
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x000A7298 File Offset: 0x000A5498
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets the form's window state.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents the window state of the form. The default is <see cref="F:System.Windows.Forms.FormWindowState.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000A72A4 File Offset: 0x000A54A4
		// (set) Token: 0x06002B98 RID: 11160 RVA: 0x000A72AC File Offset: 0x000A54AC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new FormWindowState WindowState
		{
			get
			{
				return base.WindowState;
			}
			set
			{
				base.WindowState = value;
			}
		}

		/// <summary>Creates the handle for the form that encapsulates the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer settings in <see cref="P:System.Windows.Forms.PrintPreviewDialog.Document" /> are not valid. </exception>
		// Token: 0x06002B99 RID: 11161 RVA: 0x000A72B8 File Offset: 0x000A54B8
		[MonoInternalNote("Throw InvalidPrinterException")]
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000A72C0 File Offset: 0x000A54C0
		protected override void OnClosing(CancelEventArgs e)
		{
			this.print_preview.InvalidatePreview();
			base.OnClosing(e);
		}

		/// <summary>Determines whether a key should be processed further.</summary>
		/// <returns>true to indicate the key should be processed; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x06002B9B RID: 11163 RVA: 0x000A72D4 File Offset: 0x000A54D4
		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return false;
			default:
				return base.ProcessDialogKey(keyData);
			}
		}

		/// <summary>Processes the TAB key.</summary>
		/// <returns>true to indicate the TAB key was successfully processed; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the form; otherwise, false.</param>
		// Token: 0x06002B9C RID: 11164 RVA: 0x000A730C File Offset: 0x000A550C
		protected override bool ProcessTabKey(bool forward)
		{
			return base.ProcessTabKey(forward);
		}

		// Token: 0x0400154E RID: 5454
		private PrintPreviewControl print_preview;

		// Token: 0x0400154F RID: 5455
		private MenuItem previous_checked_menu_item;

		// Token: 0x04001550 RID: 5456
		private Menu mag_menu;

		// Token: 0x04001551 RID: 5457
		private MenuItem auto_zoom_item;

		// Token: 0x04001552 RID: 5458
		private NumericUpDown pageUpDown;

		// Token: 0x02000296 RID: 662
		private class PrintToolBar : ToolBar
		{
			// Token: 0x06002B9E RID: 11166 RVA: 0x000A7320 File Offset: 0x000A5520
			public int GetNext(int pos)
			{
				while (++pos < this.items.Length && this.items[pos].Button.Style == ToolBarButtonStyle.Separator)
				{
				}
				return pos;
			}

			// Token: 0x06002B9F RID: 11167 RVA: 0x000A7354 File Offset: 0x000A5554
			public int GetPrev(int pos)
			{
				while (--pos > -1 && this.items[pos].Button.Style == ToolBarButtonStyle.Separator)
				{
				}
				return pos;
			}

			// Token: 0x06002BA0 RID: 11168 RVA: 0x000A738C File Offset: 0x000A558C
			private void SelectNextOnParent(bool forward)
			{
				ContainerControl containerControl = base.Parent as ContainerControl;
				if (containerControl != null && containerControl.ActiveControl != null)
				{
					containerControl.SelectNextControl(containerControl.ActiveControl, forward, true, true, true);
				}
			}

			// Token: 0x06002BA1 RID: 11169 RVA: 0x000A73C8 File Offset: 0x000A55C8
			protected override void OnGotFocus(EventArgs args)
			{
				base.OnGotFocus(args);
				base.CurrentItem = (((Control.ModifierKeys & Keys.Shift) == Keys.None && !this.left_pressed) ? 0 : this.GetPrev(this.items.Length));
				this.left_pressed = false;
			}

			// Token: 0x06002BA2 RID: 11170 RVA: 0x000A7418 File Offset: 0x000A5618
			protected override bool ProcessDialogKey(Keys keyData)
			{
				switch (keyData & Keys.KeyCode)
				{
				case Keys.Left:
					this.left_pressed = true;
					this.SelectNextOnParent(false);
					return true;
				case Keys.Right:
					this.SelectNextOnParent(true);
					return true;
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06002BA3 RID: 11171 RVA: 0x000A7468 File Offset: 0x000A5668
			private void NavigateItems(Keys key)
			{
				bool flag = true;
				Keys keys = key & Keys.KeyCode;
				switch (keys)
				{
				case Keys.Left:
					flag = false;
					break;
				default:
					if (keys == Keys.Tab)
					{
						flag = (Control.ModifierKeys & Keys.Shift) == Keys.None;
					}
					break;
				case Keys.Right:
					flag = true;
					break;
				}
				int num = ((!flag) ? this.GetPrev(base.CurrentItem) : this.GetNext(base.CurrentItem));
				if (num < 0 || num >= this.items.Length)
				{
					base.CurrentItem = -1;
					this.SelectNextOnParent(flag);
					return;
				}
				base.CurrentItem = num;
			}

			// Token: 0x17000ACA RID: 2762
			// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000A7514 File Offset: 0x000A5714
			private bool OnDropDownButton
			{
				get
				{
					return base.CurrentItem != -1 && this.items[base.CurrentItem].Button.Style == ToolBarButtonStyle.DropDownButton;
				}
			}

			// Token: 0x06002BA5 RID: 11173 RVA: 0x000A754C File Offset: 0x000A574C
			internal override bool InternalPreProcessMessage(ref Message msg)
			{
				Keys keys = (Keys)msg.WParam.ToInt32();
				Keys keys2 = keys;
				switch (keys2)
				{
				case Keys.Left:
				case Keys.Right:
					break;
				case Keys.Up:
				case Keys.Down:
					if (this.OnDropDownButton)
					{
						goto IL_007E;
					}
					return true;
				default:
					if (keys2 != Keys.Tab)
					{
						goto IL_007E;
					}
					break;
				}
				if (this.OnDropDownButton)
				{
					((ContextMenu)this.items[base.CurrentItem].Button.DropDownMenu).Hide();
				}
				this.NavigateItems(keys);
				return true;
				IL_007E:
				return base.InternalPreProcessMessage(ref msg);
			}

			// Token: 0x04001553 RID: 5459
			private bool left_pressed;
		}
	}
}
