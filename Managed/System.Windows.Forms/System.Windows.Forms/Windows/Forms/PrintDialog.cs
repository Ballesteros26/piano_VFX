using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Lets users select a printer and choose which sections of the document to print from a Windows Forms application.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000292 RID: 658
	[DefaultProperty("Document")]
	[Designer("System.Windows.Forms.Design.PrintDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public sealed class PrintDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintDialog" /> class.</summary>
		// Token: 0x06002A9C RID: 10908 RVA: 0x000A4290 File Offset: 0x000A2490
		public PrintDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.help_button = null;
			this.installed_printers = PrinterSettings.InstalledPrinters;
			this.form.Text = "Print";
			this.CreateFormControls();
			this.Reset();
		}

		/// <summary>Resets all options, the last selected printer, and the page settings to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002A9D RID: 10909 RVA: 0x000A42E0 File Offset: 0x000A24E0
		public override void Reset()
		{
			this.current_settings = null;
			this.AllowPrintToFile = true;
			this.AllowSelection = false;
			this.AllowSomePages = false;
			this.PrintToFile = false;
			this.ShowHelp = false;
			this.ShowNetwork = true;
		}

		/// <summary>Gets or sets a value indicating whether the Current Page option button is displayed.</summary>
		/// <returns>true if the Current Page option button is displayed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000A4320 File Offset: 0x000A2520
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x000A4328 File Offset: 0x000A2528
		[DefaultValue(false)]
		public bool AllowCurrentPage
		{
			get
			{
				return this.allow_current_page;
			}
			set
			{
				this.allow_current_page = value;
				this.radio_pages.Enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Print to file check box is enabled.</summary>
		/// <returns>true if the Print to file check box is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000A4340 File Offset: 0x000A2540
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x000A4348 File Offset: 0x000A2548
		[DefaultValue(true)]
		public bool AllowPrintToFile
		{
			get
			{
				return this.allow_print_to_file;
			}
			set
			{
				this.allow_print_to_file = value;
				this.chkbox_print.Enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Selection option button is enabled.</summary>
		/// <returns>true if the Selection option button is enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000A4360 File Offset: 0x000A2560
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x000A4368 File Offset: 0x000A2568
		[DefaultValue(false)]
		public bool AllowSelection
		{
			get
			{
				return this.allow_selection;
			}
			set
			{
				this.allow_selection = value;
				this.radio_sel.Enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Pages option button is enabled.</summary>
		/// <returns>true if the Pages option button is enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000A4380 File Offset: 0x000A2580
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x000A4388 File Offset: 0x000A2588
		[DefaultValue(false)]
		public bool AllowSomePages
		{
			get
			{
				return this.allow_some_pages;
			}
			set
			{
				this.allow_some_pages = value;
				this.radio_pages.Enabled = value;
				this.txtFrom.Enabled = value;
				this.txtTo.Enabled = value;
				this.labelTo.Enabled = value;
				this.labelFrom.Enabled = value;
				if (this.PrinterSettings != null)
				{
					this.txtFrom.Text = this.PrinterSettings.FromPage.ToString();
					this.txtTo.Text = this.PrinterSettings.ToPage.ToString();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument" /> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings" />. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000A4420 File Offset: 0x000A2620
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000A4428 File Offset: 0x000A2628
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
				this.current_settings = ((value != null) ? value.PrinterSettings : new PrinterSettings());
			}
		}

		/// <summary>Gets or sets the printer settings the dialog box modifies.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrinterSettings" /> the dialog box modifies.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000A4450 File Offset: 0x000A2650
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000A4470 File Offset: 0x000A2670
		[DefaultValue(null)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public PrinterSettings PrinterSettings
		{
			get
			{
				if (this.current_settings == null)
				{
					this.current_settings = new PrinterSettings();
				}
				return this.current_settings;
			}
			set
			{
				if (value != null && value == this.current_settings)
				{
					return;
				}
				this.current_settings = ((value != null) ? value : new PrinterSettings());
				this.document = null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Print to file check box is selected.</summary>
		/// <returns>true if the Print to file check box is selected; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000A44A4 File Offset: 0x000A26A4
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x000A44AC File Offset: 0x000A26AC
		[DefaultValue(false)]
		public bool PrintToFile
		{
			get
			{
				return this.print_to_file;
			}
			set
			{
				this.print_to_file = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Network button is displayed.</summary>
		/// <returns>true if the Network button is displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000A44B8 File Offset: 0x000A26B8
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000A44C0 File Offset: 0x000A26C0
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

		/// <summary>Gets or sets a value indicating whether the Help button is displayed.</summary>
		/// <returns>true if the Help button is displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000A44CC File Offset: 0x000A26CC
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000A44D4 File Offset: 0x000A26D4
		[DefaultValue(false)]
		public bool ShowHelp
		{
			get
			{
				return this.show_help;
			}
			set
			{
				this.show_help = value;
				this.ShowHelpButton();
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog should be shown in the Windows XP style for systems running Windows XP Home Edition, Windows XP Professional, Windows Server 2003 or later.</summary>
		/// <returns>true to indicate the dialog should be shown with the Windows XP style, otherwise false. The default is false.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000A44E4 File Offset: 0x000A26E4
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000A44EC File Offset: 0x000A26EC
		[DefaultValue(false)]
		[MonoTODO("Stub, not implemented, will always use default dialog")]
		public bool UseEXDialog
		{
			get
			{
				return this.use_ex_dialog;
			}
			set
			{
				this.use_ex_dialog = value;
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000A44F8 File Offset: 0x000A26F8
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			if (this.allow_some_pages && this.PrinterSettings.FromPage > this.PrinterSettings.ToPage)
			{
				throw new ArgumentException("FromPage out of range");
			}
			if (this.allow_some_pages)
			{
				this.txtFrom.Text = this.PrinterSettings.FromPage.ToString();
				this.txtTo.Text = this.PrinterSettings.ToPage.ToString();
			}
			if (this.PrinterSettings.PrintRange == 2 && this.allow_some_pages)
			{
				this.radio_pages.Checked = true;
			}
			else if (this.PrinterSettings.PrintRange == 1 && this.allow_selection)
			{
				this.radio_sel.Checked = true;
			}
			else
			{
				this.radio_all.Checked = true;
			}
			this.updown_copies.Value = (int)((this.PrinterSettings.Copies != 0) ? this.PrinterSettings.Copies : 1);
			this.chkbox_collate.Checked = this.PrinterSettings.Collate;
			this.chkbox_collate.Enabled = this.updown_copies.Value > 1m;
			if (this.show_help)
			{
				this.ShowHelpButton();
			}
			this.SetPrinterDetails();
			return true;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000A4670 File Offset: 0x000A2870
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000A4680 File Offset: 0x000A2880
		private void ShowErrorMessage(string message, Control control_to_focus)
		{
			MessageBox.Show(message, "Print", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			if (control_to_focus != null)
			{
				control_to_focus.Focus();
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000A46A0 File Offset: 0x000A28A0
		private void OnClickOkButton(object sender, EventArgs e)
		{
			if (this.updown_copies.Text.Length < 1)
			{
				this.ShowErrorMessage("The 'Copies' value cannot be empty and must be a positive value.", this.updown_copies);
				return;
			}
			int num = -1;
			int num2 = -1;
			if (this.allow_some_pages && this.radio_pages.Checked)
			{
				if (this.txtFrom.Text.Length < 1)
				{
					this.ShowErrorMessage("The 'From' value cannot be empty and must be a positive value.", this.txtFrom);
					return;
				}
				try
				{
					num = int.Parse(this.txtFrom.Text);
					num2 = int.Parse(this.txtTo.Text);
				}
				catch
				{
					this.ShowErrorMessage("From/To values should be numeric", this.txtFrom);
					return;
				}
				if (num > num2)
				{
					this.ShowErrorMessage("'From' value cannot be greater than 'To' value.", this.txtFrom);
					return;
				}
				if (num2 < this.PrinterSettings.MinimumPage || num2 > this.PrinterSettings.MaximumPage)
				{
					this.ShowErrorMessage(string.Concat(new object[]
					{
						"'To' value is not within the page range\nEnter a number between ",
						this.PrinterSettings.MinimumPage,
						" and ",
						this.PrinterSettings.MaximumPage,
						"."
					}), this.txtTo);
					return;
				}
				if (num < this.PrinterSettings.MinimumPage || num > this.PrinterSettings.MaximumPage)
				{
					this.ShowErrorMessage(string.Concat(new object[]
					{
						"'From' value is not within the page range\nEnter a number between ",
						this.PrinterSettings.MinimumPage,
						" and ",
						this.PrinterSettings.MaximumPage,
						"."
					}), this.txtFrom);
					return;
				}
			}
			if (this.radio_all.Checked)
			{
				this.PrinterSettings.PrintRange = 0;
			}
			else if (this.radio_pages.Checked)
			{
				this.PrinterSettings.PrintRange = 2;
			}
			else
			{
				this.PrinterSettings.PrintRange = 1;
			}
			this.PrinterSettings.Copies = (short)this.updown_copies.Value;
			if (this.PrinterSettings.PrintRange == 2)
			{
				this.PrinterSettings.FromPage = num;
				this.PrinterSettings.ToPage = num2;
			}
			this.PrinterSettings.Collate = this.chkbox_collate.Checked;
			if (this.allow_print_to_file)
			{
				this.PrinterSettings.PrintToFile = this.chkbox_print.Checked;
			}
			this.form.DialogResult = DialogResult.OK;
			if (this.printer_combo.SelectedItem != null)
			{
				this.PrinterSettings.PrinterName = (string)this.printer_combo.SelectedItem;
			}
			if (this.document != null)
			{
				this.document.PrintController = new PrintControllerWithStatusDialog(this.document.PrintController);
				this.document.PrinterSettings = this.PrinterSettings;
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000A49BC File Offset: 0x000A2BBC
		private void ShowHelpButton()
		{
			if (this.help_button == null)
			{
				this.help_button = new Button();
				this.help_button.TabIndex = 60;
				this.help_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
				this.help_button.FlatStyle = FlatStyle.System;
				this.help_button.Location = new Point(20, 270);
				this.help_button.Text = "&Help";
				this.help_button.FlatStyle = FlatStyle.System;
				this.form.Controls.Add(this.help_button);
			}
			this.help_button.Visible = this.show_help;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000A4A60 File Offset: 0x000A2C60
		private void OnUpDownValueChanged(object sender, EventArgs e)
		{
			this.chkbox_collate.Enabled = this.updown_copies.Value > 1m;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000A4A90 File Offset: 0x000A2C90
		private void OnPagesCheckedChanged(object obj, EventArgs args)
		{
			if (this.radio_pages.Checked && !this.txtTo.Focused)
			{
				this.txtFrom.Focus();
			}
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000A4ACC File Offset: 0x000A2CCC
		private void CreateFormControls()
		{
			this.form.SuspendLayout();
			GroupBox groupBox = new GroupBox();
			groupBox.Location = new Point(10, 8);
			groupBox.Text = "Printer";
			groupBox.Size = new Size(420, 145);
			GroupBox groupBox2 = new GroupBox();
			groupBox2.Location = new Point(10, 155);
			groupBox2.Text = "Print range";
			groupBox2.Size = new Size(240, 100);
			GroupBox groupBox3 = new GroupBox();
			groupBox3.Location = new Point(265, 155);
			groupBox3.Text = "Copies";
			groupBox3.Size = new Size(165, 100);
			this.accept_button = new Button();
			this.form.AcceptButton = this.accept_button;
			this.accept_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.accept_button.FlatStyle = FlatStyle.System;
			this.accept_button.Location = new Point(265, 270);
			this.accept_button.Text = "OK";
			this.accept_button.FlatStyle = FlatStyle.System;
			this.accept_button.Click += new EventHandler(this.OnClickOkButton);
			this.cancel_button = new Button();
			this.form.CancelButton = this.cancel_button;
			this.cancel_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cancel_button.FlatStyle = FlatStyle.System;
			this.cancel_button.Location = new Point(350, 270);
			this.cancel_button.Text = "Cancel";
			this.cancel_button.FlatStyle = FlatStyle.System;
			this.cancel_button.Click += new EventHandler(this.OnClickCancelButton);
			Label label = new Label();
			label.AutoSize = true;
			label.Text = "&Name:";
			label.Location = new Point(20, 33);
			groupBox.Controls.Add(label);
			label = new Label();
			label.Text = "Status:";
			label.AutoSize = true;
			label.Location = new Point(20, 60);
			groupBox.Controls.Add(label);
			this.label_status = new Label();
			this.label_status.AutoSize = true;
			this.label_status.Location = new Point(80, 60);
			groupBox.Controls.Add(this.label_status);
			label = new Label();
			label.Text = "Type:";
			label.AutoSize = true;
			label.Location = new Point(20, 80);
			groupBox.Controls.Add(label);
			this.label_type = new Label();
			this.label_type.AutoSize = true;
			this.label_type.Location = new Point(80, 80);
			groupBox.Controls.Add(this.label_type);
			label = new Label();
			label.Text = "Where:";
			label.AutoSize = true;
			label.Location = new Point(20, 100);
			groupBox.Controls.Add(label);
			this.label_where = new Label();
			this.label_where.AutoSize = true;
			this.label_where.Location = new Point(80, 100);
			groupBox.Controls.Add(this.label_where);
			label = new Label();
			label.Text = "Comment:";
			label.AutoSize = true;
			label.Location = new Point(20, 120);
			groupBox.Controls.Add(label);
			this.label_comment = new Label();
			this.label_comment.AutoSize = true;
			this.label_comment.Location = new Point(80, 120);
			groupBox.Controls.Add(this.label_comment);
			this.radio_all = new RadioButton();
			this.radio_all.TabIndex = 21;
			this.radio_all.Location = new Point(20, 20);
			this.radio_all.Text = "&All";
			this.radio_all.Checked = true;
			groupBox2.Controls.Add(this.radio_all);
			this.radio_pages = new RadioButton();
			this.radio_pages.TabIndex = 22;
			this.radio_pages.Location = new Point(20, 46);
			this.radio_pages.Text = "Pa&ges";
			this.radio_pages.Width = 60;
			this.radio_pages.CheckedChanged += new EventHandler(this.OnPagesCheckedChanged);
			groupBox2.Controls.Add(this.radio_pages);
			this.radio_sel = new RadioButton();
			this.radio_sel.TabIndex = 23;
			this.radio_sel.Location = new Point(20, 72);
			this.radio_sel.Text = "&Selection";
			groupBox2.Controls.Add(this.radio_sel);
			this.labelFrom = new Label();
			this.labelFrom.Text = "&from:";
			this.labelFrom.TabIndex = 24;
			this.labelFrom.AutoSize = true;
			this.labelFrom.Location = new Point(80, 50);
			groupBox2.Controls.Add(this.labelFrom);
			this.txtFrom = new TextBox();
			this.txtFrom.TabIndex = 25;
			this.txtFrom.Location = new Point(120, 50);
			this.txtFrom.Width = 40;
			this.txtFrom.TextChanged += new EventHandler(this.OnPagesTextChanged);
			groupBox2.Controls.Add(this.txtFrom);
			this.labelTo = new Label();
			this.labelTo.Text = "&to:";
			this.labelTo.TabIndex = 26;
			this.labelTo.AutoSize = true;
			this.labelTo.Location = new Point(170, 50);
			groupBox2.Controls.Add(this.labelTo);
			this.txtTo = new TextBox();
			this.txtTo.TabIndex = 27;
			this.txtTo.Location = new Point(190, 50);
			this.txtTo.Width = 40;
			this.txtTo.TextChanged += new EventHandler(this.OnPagesTextChanged);
			groupBox2.Controls.Add(this.txtTo);
			this.chkbox_print = new CheckBox();
			this.chkbox_print.Location = new Point(305, 115);
			this.chkbox_print.Text = "Print to fil&e";
			this.updown_copies = new NumericUpDown();
			this.updown_copies.TabIndex = 31;
			this.updown_copies.Location = new Point(105, 18);
			this.updown_copies.Minimum = 1m;
			groupBox3.Controls.Add(this.updown_copies);
			this.updown_copies.ValueChanged += new EventHandler(this.OnUpDownValueChanged);
			this.updown_copies.Size = new Size(40, 20);
			label = new Label();
			label.Text = "Number of &copies:";
			label.AutoSize = true;
			label.Location = new Point(10, 20);
			groupBox3.Controls.Add(label);
			this.chkbox_collate = new CheckBox();
			this.chkbox_collate.TabIndex = 32;
			this.chkbox_collate.Location = new Point(105, 55);
			this.chkbox_collate.Text = "C&ollate";
			this.chkbox_collate.Width = 58;
			this.chkbox_collate.CheckedChanged += new EventHandler(this.chkbox_collate_CheckedChanged);
			groupBox3.Controls.Add(this.chkbox_collate);
			this.collate = new PrintDialog.CollatePreview();
			this.collate.Location = new Point(6, 50);
			this.collate.Size = new Size(100, 45);
			groupBox3.Controls.Add(this.collate);
			this.printer_combo = new ComboBox();
			this.printer_combo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.printer_combo.Location = new Point(80, 32);
			this.printer_combo.Width = 220;
			this.printer_combo.SelectedIndexChanged += new EventHandler(this.OnPrinterSelectedIndexChanged);
			this.default_printer_settings = new PrinterSettings();
			for (int i = 0; i < this.installed_printers.Count; i++)
			{
				this.printer_combo.Items.Add(this.installed_printers[i]);
				if (this.installed_printers[i] == this.default_printer_settings.PrinterName)
				{
					this.printer_combo.SelectedItem = this.installed_printers[i];
				}
			}
			this.printer_combo.TabIndex = 11;
			this.chkbox_print.TabIndex = 12;
			groupBox.Controls.Add(this.printer_combo);
			groupBox.Controls.Add(this.chkbox_print);
			this.form.Size = new Size(450, 327);
			this.form.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.form.MaximizeBox = false;
			groupBox.TabIndex = 10;
			groupBox2.TabIndex = 20;
			groupBox3.TabIndex = 30;
			this.accept_button.TabIndex = 40;
			this.cancel_button.TabIndex = 50;
			this.form.Controls.Add(groupBox);
			this.form.Controls.Add(groupBox2);
			this.form.Controls.Add(groupBox3);
			this.form.Controls.Add(this.accept_button);
			this.form.Controls.Add(this.cancel_button);
			this.form.ResumeLayout(false);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000A547C File Offset: 0x000A367C
		private void OnPagesTextChanged(object sender, EventArgs args)
		{
			this.radio_pages.Checked = true;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000A548C File Offset: 0x000A368C
		private void OnPrinterSelectedIndexChanged(object sender, EventArgs e)
		{
			this.SetPrinterDetails();
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000A5494 File Offset: 0x000A3694
		private void SetPrinterDetails()
		{
			try
			{
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				string text4 = string.Empty;
				Type type = Type.GetType("System.Drawing.Printing.SysPrn, System.Drawing");
				MethodInfo method = type.GetMethod("GetPrintDialogInfo", 40);
				string text5 = (string)this.printer_combo.SelectedItem;
				if (text5 != null)
				{
					object[] array = new object[] { text5, text, text2, text3, text4 };
					method.Invoke(null, array);
					text = (string)array[1];
					text2 = (string)array[2];
					text3 = (string)array[3];
					text4 = (string)array[4];
				}
				this.label_status.Text = text3;
				this.label_type.Text = text2;
				this.label_where.Text = text;
				this.label_comment.Text = text4;
				this.accept_button.Enabled = true;
			}
			catch
			{
				this.accept_button.Enabled = false;
			}
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000A55B4 File Offset: 0x000A37B4
		private void chkbox_collate_CheckedChanged(object sender, EventArgs e)
		{
			this.collate.Collate = this.chkbox_collate.Checked;
		}

		// Token: 0x0400151C RID: 5404
		private PrintDocument document;

		// Token: 0x0400151D RID: 5405
		private bool allow_current_page;

		// Token: 0x0400151E RID: 5406
		private bool allow_print_to_file;

		// Token: 0x0400151F RID: 5407
		private bool allow_selection;

		// Token: 0x04001520 RID: 5408
		private bool allow_some_pages;

		// Token: 0x04001521 RID: 5409
		private bool show_help;

		// Token: 0x04001522 RID: 5410
		private bool show_network;

		// Token: 0x04001523 RID: 5411
		private bool print_to_file;

		// Token: 0x04001524 RID: 5412
		private PrinterSettings current_settings;

		// Token: 0x04001525 RID: 5413
		private Button cancel_button;

		// Token: 0x04001526 RID: 5414
		private Button accept_button;

		// Token: 0x04001527 RID: 5415
		private Button help_button;

		// Token: 0x04001528 RID: 5416
		private ComboBox printer_combo;

		// Token: 0x04001529 RID: 5417
		private RadioButton radio_all;

		// Token: 0x0400152A RID: 5418
		private RadioButton radio_pages;

		// Token: 0x0400152B RID: 5419
		private RadioButton radio_sel;

		// Token: 0x0400152C RID: 5420
		private PrinterSettings.StringCollection installed_printers;

		// Token: 0x0400152D RID: 5421
		private PrinterSettings default_printer_settings;

		// Token: 0x0400152E RID: 5422
		private TextBox txtFrom;

		// Token: 0x0400152F RID: 5423
		private TextBox txtTo;

		// Token: 0x04001530 RID: 5424
		private Label labelTo;

		// Token: 0x04001531 RID: 5425
		private Label labelFrom;

		// Token: 0x04001532 RID: 5426
		private CheckBox chkbox_print;

		// Token: 0x04001533 RID: 5427
		private NumericUpDown updown_copies;

		// Token: 0x04001534 RID: 5428
		private CheckBox chkbox_collate;

		// Token: 0x04001535 RID: 5429
		private Label label_status;

		// Token: 0x04001536 RID: 5430
		private Label label_type;

		// Token: 0x04001537 RID: 5431
		private Label label_where;

		// Token: 0x04001538 RID: 5432
		private Label label_comment;

		// Token: 0x04001539 RID: 5433
		private PrintDialog.CollatePreview collate;

		// Token: 0x0400153A RID: 5434
		private bool use_ex_dialog;

		// Token: 0x02000293 RID: 659
		private class CollatePreview : UserControl
		{
			// Token: 0x06002ABE RID: 10942 RVA: 0x000A55CC File Offset: 0x000A37CC
			public CollatePreview()
			{
				this.font = new Font(FontFamily.GenericSansSerif, 10f);
			}

			// Token: 0x17000A80 RID: 2688
			// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000A55EC File Offset: 0x000A37EC
			// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x000A55F4 File Offset: 0x000A37F4
			public bool Collate
			{
				get
				{
					return this.collate;
				}
				set
				{
					if (this.collate != value)
					{
						this.collate = value;
						base.Invalidate();
					}
				}
			}

			// Token: 0x06002AC1 RID: 10945 RVA: 0x000A5610 File Offset: 0x000A3810
			protected override void OnPaint(PaintEventArgs e)
			{
				if (this.collate)
				{
					this.DrawCollate(e.Graphics);
				}
				else
				{
					this.DrawNoCollate(e.Graphics);
				}
				base.OnPaint(e);
			}

			// Token: 0x06002AC2 RID: 10946 RVA: 0x000A564C File Offset: 0x000A384C
			private void DrawCollate(Graphics g)
			{
				int num = 0;
				int num2 = 12;
				int num3 = 14;
				int num4 = 6;
				int num5 = 26;
				int num6 = 0;
				for (int i = 0; i < 2; i++)
				{
					g.FillRectangle(Brushes.White, num5 + i * 18, num6, 18, 24);
					ControlPaint.DrawBorder(g, new Rectangle(num5 + i * 18, num6, 18, 24), SystemColors.ControlDark, ButtonBorderStyle.Solid);
					g.DrawString((i + 1).ToString(), this.font, SystemBrushes.ControlDarkDark, (float)(num5 + i * 18 + 5), (float)(num6 + 5), StringFormat.GenericTypographic);
					g.FillRectangle(Brushes.White, num3 + i * 18, num4, 18, 24);
					ControlPaint.DrawBorder(g, new Rectangle(num3 + i * 18, num4, 18, 24), SystemColors.ControlDark, ButtonBorderStyle.Solid);
					g.DrawString((i + 1).ToString(), this.font, SystemBrushes.ControlDarkDark, (float)(num3 + i * 18 + 5), (float)(num4 + 5), StringFormat.GenericTypographic);
					g.FillRectangle(Brushes.White, num + i * 18, num2, 18, 24);
					ControlPaint.DrawBorder(g, new Rectangle(num + i * 18, num2, 18, 24), SystemColors.ControlDark, ButtonBorderStyle.Solid);
					g.DrawString((i + 1).ToString(), this.font, SystemBrushes.ControlDarkDark, (float)(num + i * 18 + 5), (float)(num2 + 5), StringFormat.GenericTypographic);
					num += 28;
					num3 += 28;
					num5 += 28;
				}
			}

			// Token: 0x06002AC3 RID: 10947 RVA: 0x000A57C8 File Offset: 0x000A39C8
			private void DrawNoCollate(Graphics g)
			{
				int num = 0;
				int num2 = 12;
				int num3 = 13;
				int num4 = 4;
				for (int i = 0; i < 3; i++)
				{
					g.FillRectangle(Brushes.White, num3 + i * 18, num4, 18, 24);
					ControlPaint.DrawBorder(g, new Rectangle(num3 + i * 18, num4, 18, 24), SystemColors.ControlDark, ButtonBorderStyle.Solid);
					g.DrawString((i + 1).ToString(), this.font, SystemBrushes.ControlDarkDark, (float)(num3 + i * 18 + 5), (float)(num4 + 5), StringFormat.GenericTypographic);
					g.FillRectangle(Brushes.White, num + i * 18, num2, 18, 24);
					ControlPaint.DrawBorder(g, new Rectangle(num + i * 18, num2, 18, 24), SystemColors.ControlDark, ButtonBorderStyle.Solid);
					g.DrawString((i + 1).ToString(), this.font, SystemBrushes.ControlDarkDark, (float)(num + i * 18 + 5), (float)(num2 + 5), StringFormat.GenericTypographic);
					num += 15;
					num3 += 15;
				}
			}

			// Token: 0x0400153B RID: 5435
			private bool collate;

			// Token: 0x0400153C RID: 5436
			private new Font font;
		}
	}
}
