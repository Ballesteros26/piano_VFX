using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Enables users to change page-related print settings, including margins and paper orientation. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000283 RID: 643
	[DefaultProperty("Document")]
	public sealed class PageSetupDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PageSetupDialog" /> class.</summary>
		// Token: 0x060029C2 RID: 10690 RVA: 0x000A0EE4 File Offset: 0x0009F0E4
		public PageSetupDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.InitializeComponent();
			this.Reset();
		}

		/// <summary>Resets all options to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060029C3 RID: 10691 RVA: 0x000A0F04 File Offset: 0x0009F104
		public override void Reset()
		{
			this.AllowMargins = true;
			this.AllowOrientation = true;
			this.AllowPaper = true;
			this.AllowPrinter = true;
			this.ShowHelp = false;
			this.ShowNetwork = true;
			this.MinMargins = new Margins(0, 0, 0, 0);
			this.PrinterSettings = null;
			this.PageSettings = null;
			this.Document = null;
		}

		/// <summary>Gets or sets a value indicating whether the margins section of the dialog box is enabled.</summary>
		/// <returns>true if the margins section of the dialog box is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000A0F60 File Offset: 0x0009F160
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000A0F68 File Offset: 0x0009F168
		[DefaultValue(true)]
		public bool AllowMargins
		{
			get
			{
				return this.allow_margins;
			}
			set
			{
				this.allow_margins = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the orientation section of the dialog box (landscape versus portrait) is enabled.</summary>
		/// <returns>true if the orientation section of the dialog box is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000A0F74 File Offset: 0x0009F174
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000A0F7C File Offset: 0x0009F17C
		[DefaultValue(true)]
		public bool AllowOrientation
		{
			get
			{
				return this.allow_orientation;
			}
			set
			{
				this.allow_orientation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the paper section of the dialog box (paper size and paper source) is enabled.</summary>
		/// <returns>true if the paper section of the dialog box is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000A0F88 File Offset: 0x0009F188
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000A0F90 File Offset: 0x0009F190
		[DefaultValue(true)]
		public bool AllowPaper
		{
			get
			{
				return this.allow_paper;
			}
			set
			{
				this.allow_paper = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Printer button is enabled.</summary>
		/// <returns>true if the Printer button is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000A0F9C File Offset: 0x0009F19C
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x000A0FA4 File Offset: 0x0009F1A4
		[DefaultValue(true)]
		public bool AllowPrinter
		{
			get
			{
				return this.allow_printer;
			}
			set
			{
				this.allow_printer = value;
			}
		}

		/// <summary>Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument" /> to get page settings from.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> to get page settings from. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000A0FB0 File Offset: 0x0009F1B0
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x000A0FB8 File Offset: 0x0009F1B8
		[DefaultValue(null)]
		public PrintDocument Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
				if (this.document != null)
				{
					this.printer_settings = this.document.PrinterSettings;
					this.page_settings = this.document.DefaultPageSettings;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the margin settings, when displayed in millimeters, should be automatically converted to and from hundredths of an inch.</summary>
		/// <returns>true if the margins should be automatically converted; otherwise, false. The default is false.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000A0FFC File Offset: 0x0009F1FC
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x000A1004 File Offset: 0x0009F204
		[Browsable(true)]
		[DefaultValue(false)]
		[MonoTODO("Stubbed, not implemented")]
		[EditorBrowsable(0)]
		public bool EnableMetric
		{
			get
			{
				return this.enable_metric;
			}
			set
			{
				this.enable_metric = value;
			}
		}

		/// <summary>Gets or sets a value indicating the minimum margins, in hundredths of an inch, the user is allowed to select.</summary>
		/// <returns>The minimum margins, in hundredths of an inch, the user is allowed to select. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x000A1010 File Offset: 0x0009F210
		// (set) Token: 0x060029D1 RID: 10705 RVA: 0x000A1018 File Offset: 0x0009F218
		public Margins MinMargins
		{
			get
			{
				return this.min_margins;
			}
			set
			{
				this.min_margins = value;
			}
		}

		/// <summary>Gets or sets a value indicating the page settings to modify.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PageSettings" /> to modify. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000A1024 File Offset: 0x0009F224
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000A102C File Offset: 0x0009F22C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[DefaultValue(null)]
		public PageSettings PageSettings
		{
			get
			{
				return this.page_settings;
			}
			set
			{
				this.page_settings = value;
				this.document = null;
			}
		}

		/// <summary>Gets or sets the printer settings that are modified when the user clicks the Printer button in the dialog.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrinterSettings" /> to modify when the user clicks the Printer button. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000A103C File Offset: 0x0009F23C
		// (set) Token: 0x060029D5 RID: 10709 RVA: 0x000A1044 File Offset: 0x0009F244
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printer_settings;
			}
			set
			{
				this.printer_settings = value;
				this.document = null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help button is visible.</summary>
		/// <returns>true if the Help button is visible; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000A1054 File Offset: 0x0009F254
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000A105C File Offset: 0x0009F25C
		[DefaultValue(false)]
		public bool ShowHelp
		{
			get
			{
				return this.show_help;
			}
			set
			{
				if (value != this.show_help)
				{
					this.show_help = value;
					this.ShowHelpButton();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the Network button is visible.</summary>
		/// <returns>true if the Network button is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000A1078 File Offset: 0x0009F278
		// (set) Token: 0x060029D9 RID: 10713 RVA: 0x000A1080 File Offset: 0x0009F280
		[DefaultValue(true)]
		public bool ShowNetwork
		{
			get
			{
				return this.show_network;
			}
			set
			{
				this.show_network = value;
			}
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000A108C File Offset: 0x0009F28C
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			bool flag;
			try
			{
				this.SetPrinterDetails();
				flag = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000A10EC File Offset: 0x0009F2EC
		private void InitializeComponent()
		{
			this.groupbox_paper = new GroupBox();
			this.combobox_source = new ComboBox();
			this.combobox_size = new ComboBox();
			this.label_source = new Label();
			this.label_size = new Label();
			this.groupbox_orientation = new GroupBox();
			this.radio_landscape = new RadioButton();
			this.radio_portrait = new RadioButton();
			this.groupbox_margin = new GroupBox();
			this.label_left = new Label();
			this.button_ok = new Button();
			this.button_cancel = new Button();
			this.button_printer = new Button();
			this.label_top = new Label();
			this.label_right = new Label();
			this.label_bottom = new Label();
			this.textbox_left = new NumericTextBox();
			this.textbox_top = new NumericTextBox();
			this.textbox_right = new NumericTextBox();
			this.textbox_bottom = new NumericTextBox();
			this.pagePreview = new PageSetupDialog.PagePreview();
			this.groupbox_paper.SuspendLayout();
			this.groupbox_orientation.SuspendLayout();
			this.groupbox_margin.SuspendLayout();
			this.form.SuspendLayout();
			this.groupbox_paper.Controls.Add(this.combobox_source);
			this.groupbox_paper.Controls.Add(this.combobox_size);
			this.groupbox_paper.Controls.Add(this.label_source);
			this.groupbox_paper.Controls.Add(this.label_size);
			this.groupbox_paper.Location = new Point(12, 157);
			this.groupbox_paper.Name = "groupbox_paper";
			this.groupbox_paper.Size = new Size(336, 90);
			this.groupbox_paper.TabIndex = 0;
			this.groupbox_paper.TabStop = false;
			this.groupbox_paper.Text = "Paper";
			this.combobox_source.Location = new Point(84, 54);
			this.combobox_source.Name = "combobox_source";
			this.combobox_source.Size = new Size(240, 21);
			this.combobox_source.TabIndex = 3;
			this.combobox_size.ItemHeight = 13;
			this.combobox_size.Location = new Point(84, 22);
			this.combobox_size.Name = "combobox_size";
			this.combobox_size.Size = new Size(240, 21);
			this.combobox_size.TabIndex = 2;
			this.combobox_size.SelectedIndexChanged += new EventHandler(this.OnPaperSizeChange);
			this.label_source.Location = new Point(13, 58);
			this.label_source.Name = "label_source";
			this.label_source.Size = new Size(48, 16);
			this.label_source.TabIndex = 1;
			this.label_source.Text = "&Source:";
			this.label_size.Location = new Point(13, 25);
			this.label_size.Name = "label_size";
			this.label_size.Size = new Size(52, 16);
			this.label_size.TabIndex = 0;
			this.label_size.Text = "Si&ze:";
			this.groupbox_orientation.Controls.Add(this.radio_landscape);
			this.groupbox_orientation.Controls.Add(this.radio_portrait);
			this.groupbox_orientation.Location = new Point(12, 255);
			this.groupbox_orientation.Name = "groupbox_orientation";
			this.groupbox_orientation.Size = new Size(96, 90);
			this.groupbox_orientation.TabIndex = 1;
			this.groupbox_orientation.TabStop = false;
			this.groupbox_orientation.Text = "Orientation";
			this.radio_landscape.Location = new Point(13, 52);
			this.radio_landscape.Name = "radio_landscape";
			this.radio_landscape.Size = new Size(80, 24);
			this.radio_landscape.TabIndex = 7;
			this.radio_landscape.Text = "L&andscape";
			this.radio_landscape.CheckedChanged += new EventHandler(this.OnLandscapeChange);
			this.radio_portrait.Location = new Point(13, 19);
			this.radio_portrait.Name = "radio_portrait";
			this.radio_portrait.Size = new Size(72, 24);
			this.radio_portrait.TabIndex = 6;
			this.radio_portrait.Text = "P&ortrait";
			this.groupbox_margin.Controls.Add(this.textbox_bottom);
			this.groupbox_margin.Controls.Add(this.textbox_right);
			this.groupbox_margin.Controls.Add(this.textbox_top);
			this.groupbox_margin.Controls.Add(this.textbox_left);
			this.groupbox_margin.Controls.Add(this.label_bottom);
			this.groupbox_margin.Controls.Add(this.label_right);
			this.groupbox_margin.Controls.Add(this.label_top);
			this.groupbox_margin.Controls.Add(this.label_left);
			this.groupbox_margin.Location = new Point(120, 255);
			this.groupbox_margin.Name = "groupbox_margin";
			this.groupbox_margin.Size = new Size(228, 90);
			this.groupbox_margin.TabIndex = 2;
			this.groupbox_margin.TabStop = false;
			this.groupbox_margin.Text = this.LocalizedLengthUnit();
			this.label_left.Location = new Point(11, 25);
			this.label_left.Name = "label_left";
			this.label_left.Size = new Size(40, 23);
			this.label_left.TabIndex = 0;
			this.label_left.Text = "&Left:";
			this.button_ok.Location = new Point(120, 358);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new Size(72, 23);
			this.button_ok.TabIndex = 3;
			this.button_ok.Text = "OK";
			this.button_ok.Click += new EventHandler(this.OnClickOkButton);
			this.button_cancel.DialogResult = DialogResult.Cancel;
			this.button_cancel.Location = new Point(198, 358);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new Size(72, 23);
			this.button_cancel.TabIndex = 4;
			this.button_cancel.Text = "Cancel";
			this.button_printer.Location = new Point(276, 358);
			this.button_printer.Name = "button_printer";
			this.button_printer.Size = new Size(72, 23);
			this.button_printer.TabIndex = 5;
			this.button_printer.Text = "&Printer...";
			this.button_printer.Click += new EventHandler(this.OnClickPrinterButton);
			this.label_top.Location = new Point(11, 57);
			this.label_top.Name = "label_top";
			this.label_top.Size = new Size(40, 23);
			this.label_top.TabIndex = 1;
			this.label_top.Text = "&Top:";
			this.label_right.Location = new Point(124, 25);
			this.label_right.Name = "label_right";
			this.label_right.Size = new Size(40, 23);
			this.label_right.TabIndex = 2;
			this.label_right.Text = "&Right:";
			this.label_bottom.Location = new Point(124, 57);
			this.label_bottom.Name = "label_bottom";
			this.label_bottom.Size = new Size(40, 23);
			this.label_bottom.TabIndex = 3;
			this.label_bottom.Text = "&Bottom:";
			this.textbox_left.Location = new Point(57, 21);
			this.textbox_left.Name = "textbox_left";
			this.textbox_left.Size = new Size(48, 20);
			this.textbox_left.TabIndex = 4;
			this.textbox_left.TextChanged += new EventHandler(this.OnMarginChange);
			this.textbox_top.Location = new Point(57, 54);
			this.textbox_top.Name = "textbox_top";
			this.textbox_top.Size = new Size(48, 20);
			this.textbox_top.TabIndex = 5;
			this.textbox_top.TextChanged += new EventHandler(this.OnMarginChange);
			this.textbox_right.Location = new Point(171, 21);
			this.textbox_right.Name = "textbox_right";
			this.textbox_right.Size = new Size(48, 20);
			this.textbox_right.TabIndex = 6;
			this.textbox_right.TextChanged += new EventHandler(this.OnMarginChange);
			this.textbox_bottom.Location = new Point(171, 54);
			this.textbox_bottom.Name = "textbox_bottom";
			this.textbox_bottom.Size = new Size(48, 20);
			this.textbox_bottom.TabIndex = 7;
			this.textbox_bottom.TextChanged += new EventHandler(this.OnMarginChange);
			this.pagePreview.Location = new Point(130, 10);
			this.pagePreview.Name = "pagePreview";
			this.pagePreview.Size = new Size(150, 150);
			this.pagePreview.TabIndex = 6;
			this.form.AcceptButton = this.button_ok;
			this.form.AutoScaleBaseSize = new Size(5, 13);
			this.form.CancelButton = this.button_cancel;
			this.form.ClientSize = new Size(360, 390);
			this.form.Controls.Add(this.pagePreview);
			this.form.Controls.Add(this.button_printer);
			this.form.Controls.Add(this.button_cancel);
			this.form.Controls.Add(this.button_ok);
			this.form.Controls.Add(this.groupbox_margin);
			this.form.Controls.Add(this.groupbox_orientation);
			this.form.Controls.Add(this.groupbox_paper);
			this.form.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.form.HelpButton = true;
			this.form.MaximizeBox = false;
			this.form.MinimizeBox = false;
			this.form.Name = "Form3";
			this.form.ShowInTaskbar = false;
			this.form.Text = "Page Setup";
			this.groupbox_paper.ResumeLayout(false);
			this.groupbox_orientation.ResumeLayout(false);
			this.groupbox_margin.ResumeLayout(false);
			this.form.ResumeLayout(false);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000A1C54 File Offset: 0x0009FE54
		private static bool UseYardPound
		{
			get
			{
				return !RegionInfo.CurrentRegion.IsMetric;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000A1C64 File Offset: 0x0009FE64
		private PrinterSettings InternalPrinterSettings
		{
			get
			{
				return (this.printer_settings != null) ? this.printer_settings : this.page_settings.PrinterSettings;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A1C88 File Offset: 0x0009FE88
		private double ToLocalizedLength(int marginsUnit)
		{
			return (double)((!PageSetupDialog.UseYardPound) ? PrinterUnitConvert.Convert(marginsUnit, 1, 3) : PrinterUnitConvert.Convert(marginsUnit, 1, 0));
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000A1CB8 File Offset: 0x0009FEB8
		private int FromLocalizedLength(double marginsUnit)
		{
			return (int)((!PageSetupDialog.UseYardPound) ? PrinterUnitConvert.Convert(marginsUnit, 3, 1) : PrinterUnitConvert.Convert(marginsUnit, 0, 1));
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A1CE8 File Offset: 0x0009FEE8
		private string LocalizedLengthUnit()
		{
			return (!PageSetupDialog.UseYardPound) ? "Margins (millimeters)" : "Margins (inches)";
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A1D04 File Offset: 0x0009FF04
		private void SetPrinterDetails()
		{
			if (this.PageSettings == null)
			{
				throw new ArgumentException("PageSettings");
			}
			this.combobox_size.Items.Clear();
			foreach (object obj in this.InternalPrinterSettings.PaperSizes)
			{
				PaperSize paperSize = (PaperSize)obj;
				this.combobox_size.Items.Add(paperSize.PaperName);
			}
			this.combobox_size.SelectedItem = this.page_settings.PaperSize.PaperName;
			this.combobox_source.Items.Clear();
			foreach (object obj2 in this.InternalPrinterSettings.PaperSources)
			{
				PaperSource paperSource = (PaperSource)obj2;
				this.combobox_source.Items.Add(paperSource.SourceName);
			}
			this.combobox_source.SelectedItem = this.page_settings.PaperSource.SourceName;
			if (this.PageSettings.Landscape)
			{
				this.radio_landscape.Checked = true;
			}
			else
			{
				this.radio_portrait.Checked = true;
			}
			if (this.ShowHelp)
			{
				this.ShowHelpButton();
			}
			Margins margins = this.PageSettings.Margins;
			Margins minMargins = this.MinMargins;
			this.textbox_top.Text = this.ToLocalizedLength(margins.Top).ToString();
			this.textbox_bottom.Text = this.ToLocalizedLength(margins.Bottom).ToString();
			this.textbox_left.Text = this.ToLocalizedLength(margins.Left).ToString();
			this.textbox_right.Text = this.ToLocalizedLength(margins.Right).ToString();
			this.textbox_top.Min = this.ToLocalizedLength(minMargins.Top);
			this.textbox_bottom.Min = this.ToLocalizedLength(minMargins.Bottom);
			this.textbox_left.Min = this.ToLocalizedLength(minMargins.Left);
			this.textbox_right.Min = this.ToLocalizedLength(minMargins.Right);
			this.button_printer.Enabled = this.AllowPrinter && this.PrinterSettings != null;
			this.groupbox_orientation.Enabled = this.AllowOrientation;
			this.groupbox_paper.Enabled = this.AllowPaper;
			this.groupbox_margin.Enabled = this.AllowMargins;
			this.pagePreview.Setup(this.PageSettings);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000A2010 File Offset: 0x000A0210
		private void OnClickOkButton(object sender, EventArgs e)
		{
			if (this.combobox_size.SelectedItem != null)
			{
				foreach (object obj in this.InternalPrinterSettings.PaperSizes)
				{
					PaperSize paperSize = (PaperSize)obj;
					if (paperSize.PaperName == (string)this.combobox_size.SelectedItem)
					{
						this.PageSettings.PaperSize = paperSize;
						break;
					}
				}
			}
			if (this.combobox_source.SelectedItem != null)
			{
				foreach (object obj2 in this.InternalPrinterSettings.PaperSources)
				{
					PaperSource paperSource = (PaperSource)obj2;
					if (paperSource.SourceName == (string)this.combobox_source.SelectedItem)
					{
						this.PageSettings.PaperSource = paperSource;
						break;
					}
				}
			}
			Margins margins = new Margins();
			margins.Top = this.FromLocalizedLength(this.textbox_top.Value);
			margins.Bottom = this.FromLocalizedLength(this.textbox_bottom.Value);
			margins.Left = this.FromLocalizedLength(this.textbox_left.Value);
			margins.Right = this.FromLocalizedLength(this.textbox_right.Value);
			this.PageSettings.Margins = margins;
			this.PageSettings.Landscape = this.radio_landscape.Checked;
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000A21F8 File Offset: 0x000A03F8
		private void ShowHelpButton()
		{
			if (this.button_help == null)
			{
				this.button_help = new Button();
				this.button_help.Location = new Point(12, 358);
				this.button_help.Name = "button_help";
				this.button_help.Size = new Size(72, 23);
				this.button_help.Text = "&Help";
				this.form.Controls.Add(this.button_help);
			}
			this.button_help.Visible = this.show_help;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000A2290 File Offset: 0x000A0490
		private void OnClickPrinterButton(object sender, EventArgs args)
		{
			PageSetupDialog.PrinterForm printerForm = new PageSetupDialog.PrinterForm(this);
			printerForm.UpdateValues();
			if (printerForm.ShowDialog() == DialogResult.OK && printerForm.SelectedPrinter != this.PrinterSettings.PrinterName)
			{
				this.PrinterSettings.PrinterName = printerForm.SelectedPrinter;
			}
			this.PageSettings = this.PrinterSettings.DefaultPageSettings;
			this.SetPrinterDetails();
			this.button_ok.Select();
			printerForm.Dispose();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000A230C File Offset: 0x000A050C
		private void OnPaperSizeChange(object sender, EventArgs e)
		{
			if (this.combobox_size.SelectedItem != null)
			{
				foreach (object obj in this.InternalPrinterSettings.PaperSizes)
				{
					PaperSize paperSize = (PaperSize)obj;
					if (paperSize.PaperName == (string)this.combobox_size.SelectedItem)
					{
						this.pagePreview.SetSize(paperSize.Width, paperSize.Height);
						break;
					}
				}
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000A23C8 File Offset: 0x000A05C8
		private void OnMarginChange(object sender, EventArgs e)
		{
			this.pagePreview.SetMargins(this.FromLocalizedLength(this.textbox_left.Value), this.FromLocalizedLength(this.textbox_right.Value), this.FromLocalizedLength(this.textbox_top.Value), this.FromLocalizedLength(this.textbox_bottom.Value));
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000A2424 File Offset: 0x000A0624
		private void OnLandscapeChange(object sender, EventArgs e)
		{
			this.pagePreview.Landscape = this.radio_landscape.Checked;
		}

		// Token: 0x040014AE RID: 5294
		private PrintDocument document;

		// Token: 0x040014AF RID: 5295
		private PageSettings page_settings;

		// Token: 0x040014B0 RID: 5296
		private PrinterSettings printer_settings;

		// Token: 0x040014B1 RID: 5297
		private Margins min_margins;

		// Token: 0x040014B2 RID: 5298
		private bool allow_margins;

		// Token: 0x040014B3 RID: 5299
		private bool allow_orientation;

		// Token: 0x040014B4 RID: 5300
		private bool allow_paper;

		// Token: 0x040014B5 RID: 5301
		private bool allow_printer;

		// Token: 0x040014B6 RID: 5302
		private bool show_help;

		// Token: 0x040014B7 RID: 5303
		private bool show_network;

		// Token: 0x040014B8 RID: 5304
		private bool enable_metric;

		// Token: 0x040014B9 RID: 5305
		private GroupBox groupbox_paper;

		// Token: 0x040014BA RID: 5306
		private Label label_source;

		// Token: 0x040014BB RID: 5307
		private Label label_size;

		// Token: 0x040014BC RID: 5308
		private GroupBox groupbox_orientation;

		// Token: 0x040014BD RID: 5309
		private RadioButton radio_landscape;

		// Token: 0x040014BE RID: 5310
		private RadioButton radio_portrait;

		// Token: 0x040014BF RID: 5311
		private GroupBox groupbox_margin;

		// Token: 0x040014C0 RID: 5312
		private Label label_left;

		// Token: 0x040014C1 RID: 5313
		private Button button_help;

		// Token: 0x040014C2 RID: 5314
		private Button button_ok;

		// Token: 0x040014C3 RID: 5315
		private Button button_cancel;

		// Token: 0x040014C4 RID: 5316
		private Button button_printer;

		// Token: 0x040014C5 RID: 5317
		private Label label_top;

		// Token: 0x040014C6 RID: 5318
		private Label label_right;

		// Token: 0x040014C7 RID: 5319
		private Label label_bottom;

		// Token: 0x040014C8 RID: 5320
		private NumericTextBox textbox_left;

		// Token: 0x040014C9 RID: 5321
		private NumericTextBox textbox_top;

		// Token: 0x040014CA RID: 5322
		private NumericTextBox textbox_right;

		// Token: 0x040014CB RID: 5323
		private NumericTextBox textbox_bottom;

		// Token: 0x040014CC RID: 5324
		private ComboBox combobox_source;

		// Token: 0x040014CD RID: 5325
		private ComboBox combobox_size;

		// Token: 0x040014CE RID: 5326
		private PageSetupDialog.PagePreview pagePreview;

		// Token: 0x02000284 RID: 644
		private class PrinterForm : Form
		{
			// Token: 0x060029E8 RID: 10728 RVA: 0x000A243C File Offset: 0x000A063C
			public PrinterForm(PageSetupDialog page_setup_dialog)
			{
				this.InitializeComponent();
				this.page_setup_dialog = page_setup_dialog;
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000A2454 File Offset: 0x000A0654
			// (set) Token: 0x060029EA RID: 10730 RVA: 0x000A2468 File Offset: 0x000A0668
			public string SelectedPrinter
			{
				get
				{
					return (string)this.combobox_printers.SelectedItem;
				}
				set
				{
					this.combobox_printers.SelectedItem = value;
					this.label_type_text.Text = value;
				}
			}

			// Token: 0x060029EB RID: 10731 RVA: 0x000A2484 File Offset: 0x000A0684
			public void UpdateValues()
			{
				this.combobox_printers.Items.Clear();
				foreach (object obj in PrinterSettings.InstalledPrinters)
				{
					string text = (string)obj;
					this.combobox_printers.Items.Add(text);
				}
				this.SelectedPrinter = this.page_setup_dialog.PrinterSettings.PrinterName;
				this.button_network.Enabled = this.page_setup_dialog.ShowNetwork;
			}

			// Token: 0x060029EC RID: 10732 RVA: 0x000A253C File Offset: 0x000A073C
			private void InitializeComponent()
			{
				this.groupbox_printer = new GroupBox();
				this.combobox_printers = new ComboBox();
				this.button_network = new Button();
				this.button_cancel = new Button();
				this.button_ok = new Button();
				this.label_name = new Label();
				this.label_status = new Label();
				this.label_status_text = new Label();
				this.label_type = new Label();
				this.label_type_text = new Label();
				this.label_where = new Label();
				this.label_comment = new Label();
				this.label_where_text = new Label();
				this.label_comment_text = new Label();
				this.button_properties = new Button();
				this.groupbox_printer.SuspendLayout();
				base.SuspendLayout();
				this.groupbox_printer.Controls.AddRange(new Control[]
				{
					this.button_properties, this.label_comment_text, this.label_where_text, this.label_comment, this.label_where, this.label_type_text, this.label_type, this.label_status_text, this.label_status, this.label_name,
					this.combobox_printers
				});
				this.groupbox_printer.Location = new Point(12, 8);
				this.groupbox_printer.Name = "groupbox_printer";
				this.groupbox_printer.Size = new Size(438, 136);
				this.groupbox_printer.Text = "Printer";
				this.combobox_printers.Location = new Point(64, 24);
				this.combobox_printers.Name = "combobox_printers";
				this.combobox_printers.SelectedValueChanged += new EventHandler(this.OnSelectedValueChangedPrinters);
				this.combobox_printers.Size = new Size(232, 21);
				this.combobox_printers.TabIndex = 1;
				this.button_network.Location = new Point(16, 160);
				this.button_network.Name = "button_network";
				this.button_network.Size = new Size(68, 22);
				this.button_network.TabIndex = 5;
				this.button_network.Text = "Network...";
				this.button_cancel.DialogResult = DialogResult.Cancel;
				this.button_cancel.Location = new Point(376, 160);
				this.button_cancel.Name = "button_cancel";
				this.button_cancel.Size = new Size(68, 22);
				this.button_cancel.TabIndex = 4;
				this.button_cancel.Text = "Cancel";
				this.button_ok.DialogResult = DialogResult.OK;
				this.button_ok.Location = new Point(300, 160);
				this.button_ok.Name = "button_ok";
				this.button_ok.Size = new Size(68, 22);
				this.button_ok.TabIndex = 3;
				this.button_ok.Text = "OK";
				this.label_name.Location = new Point(12, 28);
				this.label_name.Name = "label_name";
				this.label_name.Size = new Size(48, 20);
				this.label_name.Text = "Name:";
				this.label_status.Location = new Point(6, 52);
				this.label_status.Name = "label_status";
				this.label_status.Size = new Size(58, 14);
				this.label_status.Text = "Status:";
				this.label_status_text.Location = new Point(64, 52);
				this.label_status_text.Name = "label_status_text";
				this.label_status_text.Size = new Size(64, 14);
				this.label_status_text.Text = string.Empty;
				this.label_type.Location = new Point(6, 72);
				this.label_type.Name = "label_type";
				this.label_type.Size = new Size(58, 14);
				this.label_type.Text = "Type:";
				this.label_type_text.Location = new Point(64, 72);
				this.label_type_text.Name = "label_type_text";
				this.label_type_text.Size = new Size(232, 14);
				this.label_type_text.TabIndex = 5;
				this.label_type_text.Text = string.Empty;
				this.label_where.Location = new Point(6, 92);
				this.label_where.Name = "label_where";
				this.label_where.Size = new Size(58, 16);
				this.label_where.TabIndex = 6;
				this.label_where.Text = "Where:";
				this.label_comment.Location = new Point(6, 112);
				this.label_comment.Name = "label_comment";
				this.label_comment.Size = new Size(56, 16);
				this.label_comment.Text = "Comment:";
				this.label_where_text.Location = new Point(64, 92);
				this.label_where_text.Name = "label_where_text";
				this.label_where_text.Size = new Size(232, 16);
				this.label_where_text.Text = string.Empty;
				this.label_comment_text.Location = new Point(64, 112);
				this.label_comment_text.Name = "label_comment_text";
				this.label_comment_text.Size = new Size(232, 16);
				this.label_comment_text.Text = string.Empty;
				this.button_properties.Location = new Point(308, 22);
				this.button_properties.Name = "button_properties";
				this.button_properties.Size = new Size(92, 22);
				this.button_properties.TabIndex = 2;
				this.button_properties.Text = "Properties...";
				this.AllowDrop = true;
				this.AutoScaleBaseSize = new Size(5, 13);
				base.AcceptButton = this.button_ok;
				base.CancelButton = this.button_cancel;
				base.ClientSize = new Size(456, 194);
				base.Controls.AddRange(new Control[] { this.button_ok, this.button_cancel, this.button_network, this.groupbox_printer });
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "PrinterForm";
				base.ShowInTaskbar = false;
				this.Text = "Configure page";
				this.groupbox_printer.ResumeLayout(false);
				base.ResumeLayout(false);
			}

			// Token: 0x060029ED RID: 10733 RVA: 0x000A2C08 File Offset: 0x000A0E08
			private void OnSelectedValueChangedPrinters(object sender, EventArgs args)
			{
				this.SelectedPrinter = (string)this.combobox_printers.SelectedItem;
			}

			// Token: 0x040014CF RID: 5327
			private GroupBox groupbox_printer;

			// Token: 0x040014D0 RID: 5328
			private ComboBox combobox_printers;

			// Token: 0x040014D1 RID: 5329
			private Label label_name;

			// Token: 0x040014D2 RID: 5330
			private Label label_status;

			// Token: 0x040014D3 RID: 5331
			private Button button_properties;

			// Token: 0x040014D4 RID: 5332
			private Button button_network;

			// Token: 0x040014D5 RID: 5333
			private Button button_cancel;

			// Token: 0x040014D6 RID: 5334
			private Button button_ok;

			// Token: 0x040014D7 RID: 5335
			private Label label_status_text;

			// Token: 0x040014D8 RID: 5336
			private Label label_type;

			// Token: 0x040014D9 RID: 5337
			private Label label_where;

			// Token: 0x040014DA RID: 5338
			private Label label_where_text;

			// Token: 0x040014DB RID: 5339
			private Label label_type_text;

			// Token: 0x040014DC RID: 5340
			private Label label_comment;

			// Token: 0x040014DD RID: 5341
			private Label label_comment_text;

			// Token: 0x040014DE RID: 5342
			private PageSetupDialog page_setup_dialog;
		}

		// Token: 0x02000285 RID: 645
		private class PagePreview : UserControl
		{
			// Token: 0x060029EE RID: 10734 RVA: 0x000A2C20 File Offset: 0x000A0E20
			public PagePreview()
			{
				this.sb = new StringBuilder();
				for (int i = 0; i < 4; i++)
				{
					this.sb.Append("blabla piu piublapiu haha lai dlais dhl\ufffdai shd ");
					this.sb.Append("\ufffdoasd \ufffdlaj sd\ufffd\r\n lajsd l\ufffdaisdj l\ufffdillaisd lahs dli");
					this.sb.Append("laksjd liasjdliasdj blabla piu piublapiu haha ");
					this.sb.Append("lai dlais dhl\ufffdai shd \ufffdoasd \ufffdlaj sd\ufffd lajsd l\ufffdaisdj");
					this.sb.Append(" l\ufffdillaisd lahs dli laksjd liasjdliasdj\r\n\r\n");
				}
				this.font = new Font(FontFamily.GenericSansSerif, 4f);
				this.displayHeight = 130f;
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x060029EF RID: 10735 RVA: 0x000A2CC8 File Offset: 0x000A0EC8
			// (set) Token: 0x060029F0 RID: 10736 RVA: 0x000A2CD0 File Offset: 0x000A0ED0
			public bool Landscape
			{
				get
				{
					return this.landscape;
				}
				set
				{
					if (this.landscape != value)
					{
						this.landscape = value;
						base.Invalidate();
					}
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000A2CEC File Offset: 0x000A0EEC
			// (set) Token: 0x060029F2 RID: 10738 RVA: 0x000A2CF4 File Offset: 0x000A0EF4
			public new float Height
			{
				get
				{
					return this.displayHeight;
				}
				set
				{
					if (this.displayHeight != value)
					{
						this.displayHeight = value;
						base.Invalidate();
					}
				}
			}

			// Token: 0x060029F3 RID: 10739 RVA: 0x000A2D10 File Offset: 0x000A0F10
			public void SetSize(int width, int height)
			{
				this.width = width;
				this.height = height;
				base.Invalidate();
			}

			// Token: 0x060029F4 RID: 10740 RVA: 0x000A2D28 File Offset: 0x000A0F28
			public void SetMargins(int left, int right, int top, int bottom)
			{
				this.marginBottom = bottom;
				this.marginTop = top;
				this.marginLeft = left;
				this.marginRight = right;
				base.Invalidate();
			}

			// Token: 0x060029F5 RID: 10741 RVA: 0x000A2D50 File Offset: 0x000A0F50
			public void Setup(PageSettings pageSettings)
			{
				this.width = pageSettings.PaperSize.Width;
				this.height = pageSettings.PaperSize.Height;
				Margins margins = pageSettings.Margins;
				this.marginBottom = margins.Bottom;
				this.marginTop = margins.Top;
				this.marginLeft = margins.Left;
				this.marginRight = margins.Right;
				this.landscape = pageSettings.Landscape;
				this.loaded = true;
			}

			// Token: 0x060029F6 RID: 10742 RVA: 0x000A2DCC File Offset: 0x000A0FCC
			protected override void OnPaint(PaintEventArgs e)
			{
				if (!this.loaded)
				{
					base.OnPaint(e);
					return;
				}
				Graphics graphics = e.Graphics;
				float num = this.displayHeight;
				float num2 = (float)this.width * this.displayHeight / (float)this.height;
				float num3 = (float)this.marginTop * this.displayHeight / (float)this.height;
				float num4 = (float)this.marginLeft * this.displayHeight / (float)this.height;
				float num5 = (float)this.marginBottom * this.displayHeight / (float)this.height;
				float num6 = (float)this.marginRight * this.displayHeight / (float)this.height;
				if (this.landscape)
				{
					float num7 = num2;
					num2 = num;
					num = num7;
					num7 = num6;
					num6 = num3;
					num3 = num4;
					num4 = num5;
					num5 = num7;
				}
				graphics.FillRectangle(SystemBrushes.ControlDark, 4f, 4f, num2 + 4f, num + 4f);
				graphics.FillRectangle(Brushes.White, 0f, 0f, num2, num);
				RectangleF rectangleF;
				rectangleF..ctor(0f, 0f, num2, num);
				RectangleF rectangleF2;
				rectangleF2..ctor(num4, num3, num2 - num4 - num6, num - num3 - num5);
				ControlPaint.DrawBorder(graphics, rectangleF, Color.Black, ButtonBorderStyle.Solid);
				ControlPaint.DrawBorder(graphics, rectangleF2, SystemColors.ControlDark, ButtonBorderStyle.Dashed);
				graphics.DrawString(this.sb.ToString(), this.font, Brushes.Black, new RectangleF(rectangleF2.X + 2f, rectangleF2.Y + 2f, rectangleF2.Width - 4f, rectangleF2.Height - 4f));
				base.OnPaint(e);
			}

			// Token: 0x040014DF RID: 5343
			private int width;

			// Token: 0x040014E0 RID: 5344
			private int height;

			// Token: 0x040014E1 RID: 5345
			private int marginBottom;

			// Token: 0x040014E2 RID: 5346
			private int marginTop;

			// Token: 0x040014E3 RID: 5347
			private int marginLeft;

			// Token: 0x040014E4 RID: 5348
			private int marginRight;

			// Token: 0x040014E5 RID: 5349
			private bool landscape;

			// Token: 0x040014E6 RID: 5350
			private bool loaded;

			// Token: 0x040014E7 RID: 5351
			private StringBuilder sb;

			// Token: 0x040014E8 RID: 5352
			private float displayHeight;

			// Token: 0x040014E9 RID: 5353
			private new Font font;
		}
	}
}
