using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a common dialog box that displays available colors along with controls that enable the user to define custom colors.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007E RID: 126
	[DefaultProperty("Color")]
	public class ColorDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColorDialog" /> class.</summary>
		// Token: 0x060005C6 RID: 1478 RVA: 0x00018560 File Offset: 0x00016760
		public ColorDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.form.SuspendLayout();
			this.form.Text = "Color";
			this.form.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.form.MaximizeBox = false;
			this.satTextBox = new TextBox();
			this.briTextBox = new TextBox();
			this.blueTextBox = new TextBox();
			this.greenTextBox = new TextBox();
			this.redTextBox = new TextBox();
			this.hueTextBox = new TextBox();
			this.redLabel = new Label();
			this.blueLabel = new Label();
			this.greenLabel = new Label();
			this.colorBaseLabel = new Label();
			this.hueLabel = new Label();
			this.satLabel = new Label();
			this.briLabel = new Label();
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.form.CancelButton = this.cancelButton;
			this.helpButton = new Button();
			this.defineColoursButton = new Button();
			this.addColoursButton = new Button();
			this.baseColorControl = new ColorDialog.BaseColorControl(this);
			this.colorMatrixControl = new ColorDialog.ColorMatrixControl(this);
			this.brightnessControl = new ColorDialog.BrightnessControl(this);
			this.triangleControl = new ColorDialog.TriangleControl(this);
			this.selectedColorPanel = new Panel();
			this.hueTextBox.Location = new Point(324, 203);
			this.hueTextBox.Size = new Size(27, 21);
			this.hueTextBox.TabIndex = 11;
			this.hueTextBox.MaxLength = 3;
			this.satTextBox.Location = new Point(324, 225);
			this.satTextBox.Size = new Size(27, 21);
			this.satTextBox.TabIndex = 15;
			this.satTextBox.MaxLength = 3;
			this.greenTextBox.Location = new Point(404, 225);
			this.greenTextBox.Size = new Size(27, 21);
			this.greenTextBox.TabIndex = 18;
			this.greenTextBox.MaxLength = 3;
			this.briTextBox.Location = new Point(324, 247);
			this.briTextBox.Size = new Size(27, 21);
			this.briTextBox.TabIndex = 16;
			this.briTextBox.MaxLength = 3;
			this.blueTextBox.Location = new Point(404, 247);
			this.blueTextBox.Size = new Size(27, 21);
			this.blueTextBox.TabIndex = 19;
			this.blueTextBox.MaxLength = 3;
			this.redTextBox.Location = new Point(404, 203);
			this.redTextBox.Size = new Size(27, 21);
			this.redTextBox.TabIndex = 17;
			this.redTextBox.MaxLength = 3;
			this.redLabel.FlatStyle = FlatStyle.System;
			this.redLabel.Location = new Point(361, 206);
			this.redLabel.Size = new Size(40, 16);
			this.redLabel.TabIndex = 25;
			this.redLabel.Text = Locale.GetText("Red") + ":";
			this.redLabel.TextAlign = 64;
			this.blueLabel.FlatStyle = FlatStyle.System;
			this.blueLabel.Location = new Point(361, 250);
			this.blueLabel.Size = new Size(40, 16);
			this.blueLabel.TabIndex = 26;
			this.blueLabel.Text = Locale.GetText("Blue") + ":";
			this.blueLabel.TextAlign = 64;
			this.greenLabel.FlatStyle = FlatStyle.System;
			this.greenLabel.Location = new Point(361, 228);
			this.greenLabel.Size = new Size(40, 16);
			this.greenLabel.TabIndex = 27;
			this.greenLabel.Text = Locale.GetText("Green") + ":";
			this.greenLabel.TextAlign = 64;
			this.colorBaseLabel.Location = new Point(228, 247);
			this.colorBaseLabel.Size = new Size(60, 25);
			this.colorBaseLabel.TabIndex = 28;
			this.colorBaseLabel.Text = Locale.GetText("Color");
			this.colorBaseLabel.TextAlign = 32;
			this.hueLabel.FlatStyle = FlatStyle.System;
			this.hueLabel.Location = new Point(287, 206);
			this.hueLabel.Size = new Size(36, 16);
			this.hueLabel.TabIndex = 23;
			this.hueLabel.Text = Locale.GetText("Hue") + ":";
			this.hueLabel.TextAlign = 64;
			this.satLabel.FlatStyle = FlatStyle.System;
			this.satLabel.Location = new Point(287, 228);
			this.satLabel.Size = new Size(36, 16);
			this.satLabel.TabIndex = 22;
			this.satLabel.Text = Locale.GetText("Sat") + ":";
			this.satLabel.TextAlign = 64;
			this.briLabel.FlatStyle = FlatStyle.System;
			this.briLabel.Location = new Point(287, 250);
			this.briLabel.Size = new Size(36, 16);
			this.briLabel.TabIndex = 24;
			this.briLabel.Text = Locale.GetText("Bri") + ":";
			this.briLabel.TextAlign = 64;
			this.defineColoursButton.FlatStyle = FlatStyle.System;
			this.defineColoursButton.Location = new Point(5, 244);
			this.defineColoursButton.Size = new Size(210, 22);
			this.defineColoursButton.TabIndex = 6;
			this.defineColoursButton.Text = "Define Custom Colors >>";
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Location = new Point(5, 271);
			this.okButton.Size = new Size(66, 22);
			this.okButton.TabIndex = 0;
			this.okButton.Text = Locale.GetText("OK");
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(78, 271);
			this.cancelButton.Size = new Size(66, 22);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = Locale.GetText("Cancel");
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Location = new Point(149, 271);
			this.helpButton.Size = new Size(66, 22);
			this.helpButton.TabIndex = 5;
			this.helpButton.Text = Locale.GetText("Help");
			this.helpButton.Hide();
			this.addColoursButton.FlatStyle = FlatStyle.System;
			this.addColoursButton.Location = new Point(227, 271);
			this.addColoursButton.Size = new Size(213, 22);
			this.addColoursButton.TabIndex = 7;
			this.addColoursButton.Text = "Add To Custom Colors";
			this.baseColorControl.Location = new Point(3, 6);
			this.baseColorControl.Size = new Size(212, 231);
			this.baseColorControl.TabIndex = 13;
			this.colorMatrixControl.Location = new Point(227, 7);
			this.colorMatrixControl.Size = new Size(179, 190);
			this.colorMatrixControl.TabIndex = 14;
			this.triangleControl.Location = new Point(432, 0);
			this.triangleControl.Size = new Size(16, 204);
			this.triangleControl.TabIndex = 12;
			this.brightnessControl.Location = new Point(415, 7);
			this.brightnessControl.Size = new Size(14, 190);
			this.brightnessControl.TabIndex = 20;
			this.selectedColorPanel.BackColor = SystemColors.Desktop;
			this.selectedColorPanel.BorderStyle = BorderStyle.Fixed3D;
			this.selectedColorPanel.Location = new Point(227, 202);
			this.selectedColorPanel.Size = new Size(60, 42);
			this.selectedColorPanel.TabIndex = 10;
			this.form.Controls.Add(this.hueTextBox);
			this.form.Controls.Add(this.satTextBox);
			this.form.Controls.Add(this.briTextBox);
			this.form.Controls.Add(this.redTextBox);
			this.form.Controls.Add(this.greenTextBox);
			this.form.Controls.Add(this.blueTextBox);
			this.form.Controls.Add(this.defineColoursButton);
			this.form.Controls.Add(this.okButton);
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.addColoursButton);
			this.form.Controls.Add(this.helpButton);
			this.form.Controls.Add(this.baseColorControl);
			this.form.Controls.Add(this.colorMatrixControl);
			this.form.Controls.Add(this.brightnessControl);
			this.form.Controls.Add(this.triangleControl);
			this.form.Controls.Add(this.colorBaseLabel);
			this.form.Controls.Add(this.greenLabel);
			this.form.Controls.Add(this.blueLabel);
			this.form.Controls.Add(this.redLabel);
			this.form.Controls.Add(this.briLabel);
			this.form.Controls.Add(this.hueLabel);
			this.form.Controls.Add(this.satLabel);
			this.form.Controls.Add(this.selectedColorPanel);
			this.form.ResumeLayout(false);
			this.Color = Color.Black;
			this.defineColoursButton.Click += new EventHandler(this.OnClickButtonDefineColours);
			this.addColoursButton.Click += new EventHandler(this.OnClickButtonAddColours);
			this.helpButton.Click += new EventHandler(this.OnClickHelpButton);
			this.cancelButton.Click += new EventHandler(this.OnClickCancelButton);
			this.okButton.Click += new EventHandler(this.OnClickOkButton);
			this.hueTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.satTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.briTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.redTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.greenTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.blueTextBox.KeyPress += this.OnKeyPressTextBoxes;
			this.hueTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.satTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.briTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.redTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.greenTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.blueTextBox.TextChanged += new EventHandler(this.OnTextChangedTextBoxes);
			this.hueTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.satTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.briTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.redTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.greenTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.blueTextBox.GotFocus += new EventHandler(this.OnGotFocusTextBoxes);
			this.hueTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.satTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.briTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.redTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.greenTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.blueTextBox.LostFocus += new EventHandler(this.OnLostFocusTextBoxes);
			this.ResetCustomColors();
		}

		/// <summary>Gets or sets the color selected by the user.</summary>
		/// <returns>The color selected by the user. If a color is not selected, the default value is black.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00019348 File Offset: 0x00017548
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00019358 File Offset: 0x00017558
		public Color Color
		{
			get
			{
				return this.selectedColorPanel.BackColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					this.color = Color.Black;
					this.baseColorControl.SetColor(this.color);
				}
				else if (this.color != value)
				{
					this.color = value;
					this.baseColorControl.SetColor(this.color);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can use the dialog box to define custom colors.</summary>
		/// <returns>true if the user can define custom colors; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000193BC File Offset: 0x000175BC
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x000193C4 File Offset: 0x000175C4
		[DefaultValue(true)]
		public virtual bool AllowFullOpen
		{
			get
			{
				return this.allowFullOpen;
			}
			set
			{
				if (this.allowFullOpen != value)
				{
					this.allowFullOpen = value;
					if (!this.allowFullOpen)
					{
						this.defineColoursButton.Enabled = false;
					}
					else
					{
						this.defineColoursButton.Enabled = true;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays all available colors in the set of basic colors.</summary>
		/// <returns>true if the dialog box displays all available colors in the set of basic colors; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00019404 File Offset: 0x00017604
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001940C File Offset: 0x0001760C
		[DefaultValue(false)]
		public virtual bool AnyColor
		{
			get
			{
				return this.anyColor;
			}
			set
			{
				this.anyColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the controls used to create custom colors are visible when the dialog box is opened </summary>
		/// <returns>true if the custom color controls are available when the dialog box is opened; otherwise, false. The default value is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00019418 File Offset: 0x00017618
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00019420 File Offset: 0x00017620
		[DefaultValue(false)]
		public virtual bool FullOpen
		{
			get
			{
				return this.fullOpen;
			}
			set
			{
				if (this.fullOpen != value)
				{
					this.fullOpen = value;
					if (this.fullOpen && this.allowFullOpen)
					{
						this.defineColoursButton.Enabled = false;
						this.colorMatrixControl.ColorToShow = this.baseColorControl.ColorToShow;
						this.form.Size = this.GetFormSize(true);
					}
					else
					{
						if (this.allowFullOpen)
						{
							this.defineColoursButton.Enabled = true;
						}
						this.form.Size = this.GetFormSize(false);
					}
				}
			}
		}

		/// <summary>Gets or sets the set of custom colors shown in the dialog box.</summary>
		/// <returns>A set of custom colors shown by the dialog box. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000194B8 File Offset: 0x000176B8
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000194C0 File Offset: 0x000176C0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int[] CustomColors
		{
			get
			{
				return this.customColors;
			}
			set
			{
				if (value == null)
				{
					this.ResetCustomColors();
				}
				else
				{
					Array.Copy(value, this.customColors, value.Length);
				}
				this.baseColorControl.SetCustomColors();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button appears in the color dialog box.</summary>
		/// <returns>true if the Help button is shown in the dialog box; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000194FC File Offset: 0x000176FC
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00019504 File Offset: 0x00017704
		[DefaultValue(false)]
		public virtual bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				if (this.showHelp != value)
				{
					this.showHelp = value;
					if (this.showHelp)
					{
						this.helpButton.Show();
					}
					else
					{
						this.helpButton.Hide();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box will restrict users to selecting solid colors only.</summary>
		/// <returns>true if users can select only solid colors; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00019540 File Offset: 0x00017740
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00019548 File Offset: 0x00017748
		[DefaultValue(false)]
		public virtual bool SolidColorOnly
		{
			get
			{
				return this.solidColorOnly;
			}
			set
			{
				this.solidColorOnly = value;
			}
		}

		/// <summary>Resets all options to their default values, the last selected color to black, and the custom colors to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00019554 File Offset: 0x00017754
		public override void Reset()
		{
			this.AllowFullOpen = true;
			this.anyColor = false;
			this.Color = Color.Black;
			this.CustomColors = null;
			this.FullOpen = false;
			this.ShowHelp = false;
			this.solidColorOnly = false;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ColorDialog" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ColorDialog" />. </returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005D6 RID: 1494 RVA: 0x00019598 File Offset: 0x00017798
		public override string ToString()
		{
			return base.ToString() + ",  Color: " + this.Color.ToString();
		}

		/// <summary>Gets the underlying window instance handle (HINSTANCE).</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the HINSTANCE value of the window handle.</returns>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000195C4 File Offset: 0x000177C4
		protected virtual IntPtr Instance
		{
			get
			{
				return (IntPtr)this.GetHashCode();
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.ColorDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.ColorDialog" />.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000195D4 File Offset: 0x000177D4
		protected virtual int Options
		{
			get
			{
				return 0;
			}
		}

		/// <returns>true if the dialog box was successfully run; otherwise, false.</returns>
		/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
		// Token: 0x060005D9 RID: 1497 RVA: 0x000195D8 File Offset: 0x000177D8
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			this.defineColoursButton.Enabled = this.AllowFullOpen && !this.FullOpen;
			this.defineColoursButton.Refresh();
			this.form.Size = this.GetFormSize(this.FullOpen && this.AllowFullOpen);
			this.form.Refresh();
			return true;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019644 File Offset: 0x00017844
		private Size GetFormSize(bool fullOpen)
		{
			if (fullOpen)
			{
				return new Size(448, 332);
			}
			return new Size(221, 332);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001966C File Offset: 0x0001786C
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001967C File Offset: 0x0001787C
		private void OnClickOkButton(object sender, EventArgs e)
		{
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001968C File Offset: 0x0001788C
		private void OnClickButtonAddColours(object sender, EventArgs e)
		{
			this.baseColorControl.SetUserColor(this.selectedColorPanel.BackColor);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000196A4 File Offset: 0x000178A4
		private void OnClickButtonDefineColours(object sender, EventArgs e)
		{
			if (this.allowFullOpen)
			{
				this.defineColoursButton.Enabled = false;
				this.colorMatrixControl.ColorToShow = this.baseColorControl.ColorToShow;
				this.form.Size = this.GetFormSize(true);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000196F0 File Offset: 0x000178F0
		private void OnClickHelpButton(object sender, EventArgs e)
		{
			this.OnHelpRequest(e);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000196FC File Offset: 0x000178FC
		private void OnGotFocusTextBoxes(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			this.textBox_text_old = textBox.Text;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001971C File Offset: 0x0001791C
		private void OnLostFocusTextBoxes(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (textBox.Text.Length == 0)
			{
				textBox.Text = this.textBox_text_old;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001974C File Offset: 0x0001794C
		private void OnKeyPressTextBoxes(object sender, KeyPressEventArgs e)
		{
			if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar) || e.KeyChar == ',')
			{
				e.Handled = true;
				return;
			}
			this.internal_textbox_change = true;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000197A8 File Offset: 0x000179A8
		private void OnTextChangedTextBoxes(object sender, EventArgs e)
		{
			if (!this.internal_textbox_change)
			{
				return;
			}
			this.internal_textbox_change = false;
			TextBox textBox = sender as TextBox;
			if (textBox.Text.Length == 0)
			{
				return;
			}
			string text = textBox.Text;
			int num = 0;
			try
			{
				num = Convert.ToInt32(text);
			}
			catch (Exception)
			{
			}
			if (sender == this.hueTextBox)
			{
				if (num > 239)
				{
					num = 239;
					this.hueTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.hueTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.hueTextBox;
				this.UpdateFromHSBTextBoxes();
				this.UpdateControls(this.selectedColorPanel.BackColor);
			}
			else if (sender == this.satTextBox)
			{
				if (num > 240)
				{
					num = 240;
					this.satTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.satTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.satTextBox;
				this.UpdateFromHSBTextBoxes();
				this.UpdateControls(this.selectedColorPanel.BackColor);
			}
			else if (sender == this.briTextBox)
			{
				if (num > 240)
				{
					num = 240;
					this.briTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.briTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.briTextBox;
				this.UpdateFromHSBTextBoxes();
				this.UpdateControls(this.selectedColorPanel.BackColor);
			}
			else if (sender == this.redTextBox)
			{
				if (num > 255)
				{
					num = 255;
					this.redTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.redTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.redTextBox;
				this.UpdateFromRGBTextBoxes();
			}
			else if (sender == this.greenTextBox)
			{
				if (num > 255)
				{
					num = 255;
					this.greenTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.greenTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.greenTextBox;
				this.UpdateFromRGBTextBoxes();
			}
			else if (sender == this.blueTextBox)
			{
				if (num > 255)
				{
					num = 255;
					this.blueTextBox.Text = num.ToString();
				}
				else if (num < 0)
				{
					num = 0;
					this.blueTextBox.Text = num.ToString();
				}
				this.edit_textbox = this.blueTextBox;
				this.UpdateFromRGBTextBoxes();
			}
			this.textBox_text_old = this.edit_textbox.Text;
			this.edit_textbox = null;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019AC4 File Offset: 0x00017CC4
		internal void UpdateControls(Color acolor)
		{
			this.selectedColorPanel.BackColor = acolor;
			this.colorMatrixControl.ColorToShow = acolor;
			this.brightnessControl.ColorToShow = acolor;
			this.triangleControl.ColorToShow = acolor;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00019B04 File Offset: 0x00017D04
		internal void UpdateRGBTextBoxes(Color acolor)
		{
			if (this.edit_textbox != this.redTextBox)
			{
				this.redTextBox.Text = acolor.R.ToString();
			}
			if (this.edit_textbox != this.greenTextBox)
			{
				this.greenTextBox.Text = acolor.G.ToString();
			}
			if (this.edit_textbox != this.blueTextBox)
			{
				this.blueTextBox.Text = acolor.B.ToString();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00019B94 File Offset: 0x00017D94
		internal void UpdateHSBTextBoxes(Color acolor)
		{
			ColorDialog.HSB hsb = ColorDialog.HSB.RGB2HSB(acolor);
			if (this.edit_textbox != this.hueTextBox)
			{
				this.hueTextBox.Text = hsb.hue.ToString();
			}
			if (this.edit_textbox != this.satTextBox)
			{
				this.satTextBox.Text = hsb.sat.ToString();
			}
			if (this.edit_textbox != this.briTextBox)
			{
				this.briTextBox.Text = hsb.bri.ToString();
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00019C20 File Offset: 0x00017E20
		internal void UpdateFromHSBTextBoxes()
		{
			Color color = ColorDialog.HSB.HSB2RGB(Convert.ToInt32(this.hueTextBox.Text), Convert.ToInt32(this.satTextBox.Text), Convert.ToInt32(this.briTextBox.Text));
			this.selectedColorPanel.BackColor = color;
			this.UpdateRGBTextBoxes(color);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00019C78 File Offset: 0x00017E78
		internal void UpdateFromRGBTextBoxes()
		{
			Color color = Color.FromArgb(Convert.ToInt32(this.redTextBox.Text), Convert.ToInt32(this.greenTextBox.Text), Convert.ToInt32(this.blueTextBox.Text));
			this.selectedColorPanel.BackColor = color;
			this.UpdateHSBTextBoxes(color);
			this.UpdateControls(color);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00019CD8 File Offset: 0x00017ED8
		private void ResetCustomColors()
		{
			if (this.customColors == null)
			{
				this.customColors = new int[16];
			}
			int num = Color.FromArgb(0, 255, 255, 255).ToArgb();
			for (int i = 0; i < this.customColors.Length; i++)
			{
				this.customColors[i] = num;
			}
		}

		// Token: 0x040006E1 RID: 1761
		private bool allowFullOpen = true;

		// Token: 0x040006E2 RID: 1762
		private bool anyColor;

		// Token: 0x040006E3 RID: 1763
		private Color color;

		// Token: 0x040006E4 RID: 1764
		private int[] customColors;

		// Token: 0x040006E5 RID: 1765
		private bool fullOpen;

		// Token: 0x040006E6 RID: 1766
		private bool showHelp;

		// Token: 0x040006E7 RID: 1767
		private bool solidColorOnly;

		// Token: 0x040006E8 RID: 1768
		private Panel selectedColorPanel;

		// Token: 0x040006E9 RID: 1769
		private ColorDialog.BaseColorControl baseColorControl;

		// Token: 0x040006EA RID: 1770
		private ColorDialog.ColorMatrixControl colorMatrixControl;

		// Token: 0x040006EB RID: 1771
		private ColorDialog.BrightnessControl brightnessControl;

		// Token: 0x040006EC RID: 1772
		private ColorDialog.TriangleControl triangleControl;

		// Token: 0x040006ED RID: 1773
		private Button okButton;

		// Token: 0x040006EE RID: 1774
		private Button cancelButton;

		// Token: 0x040006EF RID: 1775
		private Button helpButton;

		// Token: 0x040006F0 RID: 1776
		private Button addColoursButton;

		// Token: 0x040006F1 RID: 1777
		private Button defineColoursButton;

		// Token: 0x040006F2 RID: 1778
		private TextBox hueTextBox;

		// Token: 0x040006F3 RID: 1779
		private TextBox satTextBox;

		// Token: 0x040006F4 RID: 1780
		private TextBox briTextBox;

		// Token: 0x040006F5 RID: 1781
		private TextBox redTextBox;

		// Token: 0x040006F6 RID: 1782
		private TextBox greenTextBox;

		// Token: 0x040006F7 RID: 1783
		private TextBox blueTextBox;

		// Token: 0x040006F8 RID: 1784
		private Label briLabel;

		// Token: 0x040006F9 RID: 1785
		private Label satLabel;

		// Token: 0x040006FA RID: 1786
		private Label hueLabel;

		// Token: 0x040006FB RID: 1787
		private Label colorBaseLabel;

		// Token: 0x040006FC RID: 1788
		private Label greenLabel;

		// Token: 0x040006FD RID: 1789
		private Label blueLabel;

		// Token: 0x040006FE RID: 1790
		private Label redLabel;

		// Token: 0x040006FF RID: 1791
		private string textBox_text_old = string.Empty;

		// Token: 0x04000700 RID: 1792
		internal TextBox edit_textbox;

		// Token: 0x04000701 RID: 1793
		private bool internal_textbox_change;

		// Token: 0x0200007F RID: 127
		internal struct HSB
		{
			// Token: 0x060005EA RID: 1514 RVA: 0x00019D40 File Offset: 0x00017F40
			public static ColorDialog.HSB RGB2HSB(Color color)
			{
				ColorDialog.HSB hsb = default(ColorDialog.HSB);
				hsb.hue = (int)(color.GetHue() / 360f * 240f);
				hsb.sat = (int)(color.GetSaturation() * 241f);
				hsb.bri = (int)(color.GetBrightness() * 241f);
				if (hsb.hue > 239)
				{
					hsb.hue = 239;
				}
				if (hsb.sat > 240)
				{
					hsb.sat = 240;
				}
				if (hsb.bri > 240)
				{
					hsb.bri = 240;
				}
				return hsb;
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x00019DF4 File Offset: 0x00017FF4
			public static Color HSB2RGB(int hue, int saturation, int brightness)
			{
				if (hue > 239)
				{
					hue = 239;
				}
				else if (hue < 0)
				{
					hue = 0;
				}
				if (saturation > 240)
				{
					saturation = 240;
				}
				else if (saturation < 0)
				{
					saturation = 0;
				}
				if (brightness > 240)
				{
					brightness = 240;
				}
				else if (brightness < 0)
				{
					brightness = 0;
				}
				float num = (float)hue / 239f;
				float num2 = (float)saturation / 240f;
				float num3 = (float)brightness / 240f;
				float num6;
				float num5;
				float num4;
				if (num3 == 0f)
				{
					num4 = (num5 = (num6 = 0f));
				}
				else if (num2 == 0f)
				{
					num4 = (num5 = (num6 = num3));
				}
				else
				{
					float num7 = ((num3 > 0.5f) ? (num3 + num2 - num3 * num2) : (num3 * (1f + num2)));
					float num8 = 2f * num3 - num7;
					float[] array = new float[]
					{
						num + 0.33333334f,
						num,
						num - 0.33333334f
					};
					float[] array2 = new float[3];
					for (int i = 0; i < 3; i++)
					{
						if (array[i] < 0f)
						{
							array[i] += 1f;
						}
						if (array[i] > 1f)
						{
							array[i] -= 1f;
						}
						if (6f * array[i] < 1f)
						{
							array2[i] = num8 + (num7 - num8) * array[i] * 6f;
						}
						else if (2f * array[i] < 1f)
						{
							array2[i] = num7;
						}
						else if (3f * array[i] < 2f)
						{
							array2[i] = num8 + (num7 - num8) * (0.6666667f - array[i]) * 6f;
						}
						else
						{
							array2[i] = num8;
						}
					}
					num5 = array2[0];
					num4 = array2[1];
					num6 = array2[2];
				}
				num5 = 255f * num5;
				num4 = 255f * num4;
				num6 = 255f * num6;
				if (num5 < 1f)
				{
					num5 = 0f;
				}
				else if (num5 > 255f)
				{
					num5 = 255f;
				}
				if (num4 < 1f)
				{
					num4 = 0f;
				}
				else if (num4 > 255f)
				{
					num4 = 255f;
				}
				if (num6 < 1f)
				{
					num6 = 0f;
				}
				else if (num6 > 255f)
				{
					num6 = 255f;
				}
				return Color.FromArgb((int)num5, (int)num4, (int)num6);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0001A0C0 File Offset: 0x000182C0
			public static int Brightness(Color color)
			{
				return (int)(color.GetBrightness() * 241f);
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x0001A0D0 File Offset: 0x000182D0
			public static void GetHueSaturation(Color color, out int hue, out int sat)
			{
				hue = (int)(color.GetHue() / 360f * 240f);
				sat = (int)(color.GetSaturation() * 241f);
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x0001A104 File Offset: 0x00018304
			public static void TestColor(Color color)
			{
				Console.WriteLine("Color: " + color);
				ColorDialog.HSB hsb = ColorDialog.HSB.RGB2HSB(color);
				Console.WriteLine(string.Concat(new object[] { "RGB2HSB: ", hsb.hue, ", ", hsb.sat, ", ", hsb.bri }));
				Console.WriteLine("HSB2RGB: " + ColorDialog.HSB.HSB2RGB(hsb.hue, hsb.sat, hsb.bri));
				Console.WriteLine();
			}

			// Token: 0x04000702 RID: 1794
			public int hue;

			// Token: 0x04000703 RID: 1795
			public int sat;

			// Token: 0x04000704 RID: 1796
			public int bri;
		}

		// Token: 0x02000080 RID: 128
		internal class BaseColorControl : Control
		{
			// Token: 0x060005EF RID: 1519 RVA: 0x0001A1B8 File Offset: 0x000183B8
			public BaseColorControl(ColorDialog colorDialog)
			{
				this.colorDialog = colorDialog;
				this.userSmallColorControl = new ColorDialog.BaseColorControl.SmallColorControl[16];
				this.userSmallColorControl[0] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[1] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[2] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[3] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[4] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[5] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[6] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[7] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[8] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[9] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[10] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[11] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[12] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[13] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[14] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.userSmallColorControl[15] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.smallColorControl = new ColorDialog.BaseColorControl.SmallColorControl[48];
				this.smallColorControl[0] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 128, 128));
				this.smallColorControl[1] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 128, 64));
				this.smallColorControl[2] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Gray);
				this.smallColorControl[3] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 0, 255));
				this.smallColorControl[4] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Silver);
				this.smallColorControl[5] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(64, 128, 128));
				this.smallColorControl[6] = new ColorDialog.BaseColorControl.SmallColorControl(Color.White);
				this.smallColorControl[7] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(64, 0, 64));
				this.smallColorControl[8] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 128, 64));
				this.smallColorControl[9] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 64, 64));
				this.smallColorControl[10] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Teal);
				this.smallColorControl[11] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Lime);
				this.smallColorControl[12] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 128, 255));
				this.smallColorControl[13] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 64, 128));
				this.smallColorControl[14] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 0, 128));
				this.smallColorControl[15] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 255, 0));
				this.smallColorControl[16] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 255, 64));
				this.smallColorControl[17] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Red);
				this.smallColorControl[18] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 128, 0));
				this.smallColorControl[19] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 128, 255));
				this.smallColorControl[20] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Fuchsia);
				this.smallColorControl[21] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Aqua);
				this.smallColorControl[22] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 255, 128));
				this.smallColorControl[23] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 255, 255));
				this.smallColorControl[24] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 128, 255));
				this.smallColorControl[25] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 64, 0));
				this.smallColorControl[26] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(64, 0, 0));
				this.smallColorControl[27] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Maroon);
				this.smallColorControl[28] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Purple);
				this.smallColorControl[29] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 0, 160));
				this.smallColorControl[30] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Blue);
				this.smallColorControl[31] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 128, 64));
				this.smallColorControl[32] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Green);
				this.smallColorControl[33] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Yellow);
				this.smallColorControl[34] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 128, 192));
				this.smallColorControl[35] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 128, 192));
				this.smallColorControl[36] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(128, 0, 64));
				this.smallColorControl[37] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 128, 192));
				this.smallColorControl[38] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 255, 128));
				this.smallColorControl[39] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(255, 255, 128));
				this.smallColorControl[40] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 64, 0));
				this.smallColorControl[41] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 64, 64));
				this.smallColorControl[42] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Navy);
				this.smallColorControl[43] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(0, 0, 64));
				this.smallColorControl[44] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(64, 0, 64));
				this.smallColorControl[45] = new ColorDialog.BaseColorControl.SmallColorControl(Color.FromArgb(64, 0, 128));
				this.smallColorControl[46] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Black);
				this.smallColorControl[47] = new ColorDialog.BaseColorControl.SmallColorControl(Color.Olive);
				this.baseColorLabel = new Label();
				this.userColorLabel = new Label();
				base.SuspendLayout();
				this.smallColorControl[0].Location = new Point(0, 15);
				this.smallColorControl[0].TabIndex = 51;
				this.smallColorControl[0].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[1].Location = new Point(50, 130);
				this.smallColorControl[1].TabIndex = 92;
				this.smallColorControl[1].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[2].Location = new Point(75, 130);
				this.smallColorControl[2].TabIndex = 93;
				this.smallColorControl[2].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[3].Location = new Point(175, 84);
				this.smallColorControl[3].TabIndex = 98;
				this.smallColorControl[3].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[4].Location = new Point(125, 130);
				this.smallColorControl[4].TabIndex = 95;
				this.smallColorControl[4].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[5].Location = new Point(100, 130);
				this.smallColorControl[5].TabIndex = 94;
				this.smallColorControl[5].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[6].Location = new Point(175, 130);
				this.smallColorControl[6].TabIndex = 97;
				this.smallColorControl[6].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[7].Location = new Point(150, 130);
				this.smallColorControl[7].TabIndex = 96;
				this.smallColorControl[7].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[8].Location = new Point(25, 61);
				this.smallColorControl[8].TabIndex = 68;
				this.smallColorControl[8].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[9].Location = new Point(0, 61);
				this.smallColorControl[9].TabIndex = 67;
				this.smallColorControl[9].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[10].Location = new Point(75, 61);
				this.smallColorControl[10].TabIndex = 70;
				this.smallColorControl[10].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[11].Location = new Point(50, 61);
				this.smallColorControl[11].TabIndex = 69;
				this.smallColorControl[11].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[12].Location = new Point(125, 61);
				this.smallColorControl[12].TabIndex = 72;
				this.smallColorControl[12].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[13].Location = new Point(100, 61);
				this.smallColorControl[13].TabIndex = 71;
				this.smallColorControl[13].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[14].Location = new Point(175, 61);
				this.smallColorControl[14].TabIndex = 74;
				this.smallColorControl[14].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[15].Location = new Point(50, 38);
				this.smallColorControl[15].TabIndex = 61;
				this.smallColorControl[15].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[16].Location = new Point(75, 38);
				this.smallColorControl[16].TabIndex = 62;
				this.smallColorControl[16].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[17].Location = new Point(0, 38);
				this.smallColorControl[17].TabIndex = 59;
				this.smallColorControl[17].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[18].Location = new Point(25, 84);
				this.smallColorControl[18].TabIndex = 75;
				this.smallColorControl[18].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[19].Location = new Point(175, 15);
				this.smallColorControl[19].TabIndex = 58;
				this.smallColorControl[19].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[20].Location = new Point(175, 38);
				this.smallColorControl[20].TabIndex = 66;
				this.smallColorControl[20].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[21].Location = new Point(100, 38);
				this.smallColorControl[21].TabIndex = 63;
				this.smallColorControl[21].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[22].Location = new Point(50, 15);
				this.smallColorControl[22].TabIndex = 53;
				this.smallColorControl[22].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[23].Location = new Point(100, 15);
				this.smallColorControl[23].TabIndex = 55;
				this.smallColorControl[23].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[24].Location = new Point(125, 15);
				this.smallColorControl[24].TabIndex = 56;
				this.smallColorControl[24].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[25].Location = new Point(25, 107);
				this.smallColorControl[25].TabIndex = 83;
				this.smallColorControl[25].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[26].Location = new Point(0, 107);
				this.smallColorControl[26].TabIndex = 82;
				this.smallColorControl[26].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[27].Location = new Point(0, 84);
				this.smallColorControl[27].TabIndex = 81;
				this.smallColorControl[27].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[28].Location = new Point(150, 84);
				this.smallColorControl[28].TabIndex = 80;
				this.smallColorControl[28].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[29].Location = new Point(125, 84);
				this.smallColorControl[29].TabIndex = 79;
				this.smallColorControl[29].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[30].Location = new Point(100, 84);
				this.smallColorControl[30].TabIndex = 78;
				this.smallColorControl[30].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[31].Location = new Point(75, 84);
				this.smallColorControl[31].TabIndex = 77;
				this.smallColorControl[31].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[32].Location = new Point(50, 84);
				this.smallColorControl[32].TabIndex = 76;
				this.smallColorControl[32].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[33].Location = new Point(25, 38);
				this.smallColorControl[33].TabIndex = 60;
				this.smallColorControl[33].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[34].Location = new Point(150, 38);
				this.smallColorControl[34].TabIndex = 65;
				this.smallColorControl[34].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[35].Location = new Point(125, 38);
				this.smallColorControl[35].TabIndex = 64;
				this.smallColorControl[35].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[36].Location = new Point(150, 61);
				this.smallColorControl[36].TabIndex = 73;
				this.smallColorControl[36].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[37].Location = new Point(150, 15);
				this.smallColorControl[37].TabIndex = 57;
				this.smallColorControl[37].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[38].Location = new Point(75, 15);
				this.smallColorControl[38].TabIndex = 54;
				this.smallColorControl[38].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[39].Location = new Point(25, 15);
				this.smallColorControl[39].TabIndex = 52;
				this.smallColorControl[39].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[40].Location = new Point(50, 107);
				this.smallColorControl[40].TabIndex = 84;
				this.smallColorControl[40].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[41].Location = new Point(75, 107);
				this.smallColorControl[41].TabIndex = 85;
				this.smallColorControl[41].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[42].Location = new Point(100, 107);
				this.smallColorControl[42].TabIndex = 86;
				this.smallColorControl[42].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[43].Location = new Point(125, 107);
				this.smallColorControl[43].TabIndex = 87;
				this.smallColorControl[43].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[44].Location = new Point(150, 107);
				this.smallColorControl[44].TabIndex = 88;
				this.smallColorControl[44].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[45].Location = new Point(175, 107);
				this.smallColorControl[45].TabIndex = 89;
				this.smallColorControl[45].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[46].Location = new Point(0, 130);
				this.smallColorControl[46].TabIndex = 90;
				this.smallColorControl[46].Click += new EventHandler(this.OnSmallColorControlClick);
				this.smallColorControl[47].Location = new Point(25, 130);
				this.smallColorControl[47].TabIndex = 91;
				this.smallColorControl[47].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[0].Location = new Point(0, 180);
				this.userSmallColorControl[0].TabIndex = 99;
				this.userSmallColorControl[0].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[1].Location = new Point(0, 203);
				this.userSmallColorControl[1].TabIndex = 108;
				this.userSmallColorControl[1].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[2].Location = new Point(25, 180);
				this.userSmallColorControl[2].TabIndex = 100;
				this.userSmallColorControl[2].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[3].Location = new Point(25, 203);
				this.userSmallColorControl[3].TabIndex = 109;
				this.userSmallColorControl[3].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[4].Location = new Point(50, 180);
				this.userSmallColorControl[4].TabIndex = 101;
				this.userSmallColorControl[4].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[5].Location = new Point(50, 203);
				this.userSmallColorControl[5].TabIndex = 110;
				this.userSmallColorControl[5].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[6].Location = new Point(75, 180);
				this.userSmallColorControl[6].TabIndex = 102;
				this.userSmallColorControl[6].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[7].Location = new Point(75, 203);
				this.userSmallColorControl[7].TabIndex = 111;
				this.userSmallColorControl[7].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[8].Location = new Point(100, 180);
				this.userSmallColorControl[8].TabIndex = 103;
				this.userSmallColorControl[8].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[9].Location = new Point(100, 203);
				this.userSmallColorControl[9].TabIndex = 112;
				this.userSmallColorControl[9].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[10].Location = new Point(125, 180);
				this.userSmallColorControl[10].TabIndex = 105;
				this.userSmallColorControl[10].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[11].Location = new Point(125, 203);
				this.userSmallColorControl[11].TabIndex = 113;
				this.userSmallColorControl[11].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[12].Location = new Point(150, 180);
				this.userSmallColorControl[12].TabIndex = 106;
				this.userSmallColorControl[12].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[13].Location = new Point(150, 203);
				this.userSmallColorControl[13].TabIndex = 114;
				this.userSmallColorControl[13].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[14].Location = new Point(175, 180);
				this.userSmallColorControl[14].TabIndex = 107;
				this.userSmallColorControl[14].Click += new EventHandler(this.OnSmallColorControlClick);
				this.userSmallColorControl[15].Location = new Point(175, 203);
				this.userSmallColorControl[15].TabIndex = 115;
				this.userSmallColorControl[15].Click += new EventHandler(this.OnSmallColorControlClick);
				this.baseColorLabel.Location = new Point(2, 0);
				this.baseColorLabel.Size = new Size(200, 12);
				this.baseColorLabel.TabIndex = 5;
				this.baseColorLabel.Text = Locale.GetText("Base Colors") + ":";
				this.userColorLabel.FlatStyle = FlatStyle.System;
				this.userColorLabel.Location = new Point(2, 164);
				this.userColorLabel.Size = new Size(200, 14);
				this.userColorLabel.TabIndex = 104;
				this.userColorLabel.Text = Locale.GetText("User Colors") + ":";
				base.Controls.Add(this.userSmallColorControl[7]);
				base.Controls.Add(this.userSmallColorControl[6]);
				base.Controls.Add(this.userSmallColorControl[5]);
				base.Controls.Add(this.userSmallColorControl[4]);
				base.Controls.Add(this.userSmallColorControl[3]);
				base.Controls.Add(this.userSmallColorControl[2]);
				base.Controls.Add(this.userSmallColorControl[1]);
				base.Controls.Add(this.userSmallColorControl[0]);
				base.Controls.Add(this.userSmallColorControl[15]);
				base.Controls.Add(this.userSmallColorControl[14]);
				base.Controls.Add(this.userSmallColorControl[13]);
				base.Controls.Add(this.userSmallColorControl[12]);
				base.Controls.Add(this.userSmallColorControl[11]);
				base.Controls.Add(this.userSmallColorControl[10]);
				base.Controls.Add(this.userSmallColorControl[9]);
				base.Controls.Add(this.userSmallColorControl[8]);
				base.Controls.Add(this.smallColorControl[0]);
				base.Controls.Add(this.smallColorControl[3]);
				base.Controls.Add(this.smallColorControl[6]);
				base.Controls.Add(this.smallColorControl[7]);
				base.Controls.Add(this.smallColorControl[4]);
				base.Controls.Add(this.smallColorControl[5]);
				base.Controls.Add(this.smallColorControl[2]);
				base.Controls.Add(this.smallColorControl[1]);
				base.Controls.Add(this.smallColorControl[47]);
				base.Controls.Add(this.smallColorControl[46]);
				base.Controls.Add(this.smallColorControl[45]);
				base.Controls.Add(this.smallColorControl[44]);
				base.Controls.Add(this.smallColorControl[43]);
				base.Controls.Add(this.smallColorControl[42]);
				base.Controls.Add(this.smallColorControl[41]);
				base.Controls.Add(this.smallColorControl[40]);
				base.Controls.Add(this.smallColorControl[25]);
				base.Controls.Add(this.smallColorControl[26]);
				base.Controls.Add(this.smallColorControl[27]);
				base.Controls.Add(this.smallColorControl[28]);
				base.Controls.Add(this.smallColorControl[29]);
				base.Controls.Add(this.smallColorControl[30]);
				base.Controls.Add(this.smallColorControl[31]);
				base.Controls.Add(this.smallColorControl[32]);
				base.Controls.Add(this.smallColorControl[18]);
				base.Controls.Add(this.smallColorControl[14]);
				base.Controls.Add(this.smallColorControl[36]);
				base.Controls.Add(this.smallColorControl[12]);
				base.Controls.Add(this.smallColorControl[13]);
				base.Controls.Add(this.smallColorControl[10]);
				base.Controls.Add(this.smallColorControl[11]);
				base.Controls.Add(this.smallColorControl[8]);
				base.Controls.Add(this.smallColorControl[9]);
				base.Controls.Add(this.smallColorControl[20]);
				base.Controls.Add(this.smallColorControl[34]);
				base.Controls.Add(this.smallColorControl[35]);
				base.Controls.Add(this.smallColorControl[21]);
				base.Controls.Add(this.smallColorControl[16]);
				base.Controls.Add(this.smallColorControl[15]);
				base.Controls.Add(this.smallColorControl[33]);
				base.Controls.Add(this.smallColorControl[17]);
				base.Controls.Add(this.smallColorControl[19]);
				base.Controls.Add(this.smallColorControl[37]);
				base.Controls.Add(this.smallColorControl[24]);
				base.Controls.Add(this.smallColorControl[23]);
				base.Controls.Add(this.smallColorControl[38]);
				base.Controls.Add(this.smallColorControl[22]);
				base.Controls.Add(this.smallColorControl[39]);
				base.Controls.Add(this.userColorLabel);
				base.Controls.Add(this.baseColorLabel);
				base.Size = new Size(212, 238);
				base.ResumeLayout(false);
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001BE5C File Offset: 0x0001A05C
			public ColorDialog.BaseColorControl.SmallColorControl UIASelectedSmallColorControl
			{
				get
				{
					for (int i = 0; i < this.smallColorControl.Length - 1; i++)
					{
						if (this.smallColorControl[i].IsSelected)
						{
							return this.smallColorControl[i];
						}
					}
					for (int j = 0; j < this.userSmallColorControl.Length - 1; j++)
					{
						if (this.userSmallColorControl[j].IsSelected)
						{
							return this.userSmallColorControl[j];
						}
					}
					return null;
				}
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x0001BED8 File Offset: 0x0001A0D8
			private void CheckIfColorIsInPanel(Color color)
			{
				for (int i = 0; i < this.smallColorControl.Length; i++)
				{
					if (this.smallColorControl[i].InternalColor == color)
					{
						this.selectedSmallColorControl = this.smallColorControl[i];
						this.selectedSmallColorControl.IsSelected = true;
						break;
					}
				}
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0001BF38 File Offset: 0x0001A138
			private void OnSmallColorControlClick(object sender, EventArgs e)
			{
				if (this.selectedSmallColorControl != (ColorDialog.BaseColorControl.SmallColorControl)sender)
				{
					this.selectedSmallColorControl.IsSelected = false;
				}
				this.selectedSmallColorControl = (ColorDialog.BaseColorControl.SmallColorControl)sender;
				ColorDialog.TriangleControl.CurrentBrightness = ColorDialog.HSB.Brightness(this.selectedSmallColorControl.InternalColor);
				this.colorDialog.UpdateControls(this.selectedSmallColorControl.InternalColor);
				this.colorDialog.UpdateRGBTextBoxes(this.selectedSmallColorControl.InternalColor);
				this.colorDialog.UpdateHSBTextBoxes(this.selectedSmallColorControl.InternalColor);
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
			public Color ColorToShow
			{
				get
				{
					return this.selectedSmallColorControl.InternalColor;
				}
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
			public void SetColor(Color acolor)
			{
				if (this.selectedSmallColorControl != null)
				{
					this.selectedSmallColorControl.IsSelected = false;
				}
				this.CheckIfColorIsInPanel(acolor);
				ColorDialog.TriangleControl.CurrentBrightness = ColorDialog.HSB.Brightness(acolor);
				this.colorDialog.UpdateControls(acolor);
				this.colorDialog.UpdateRGBTextBoxes(acolor);
				this.colorDialog.UpdateHSBTextBoxes(acolor);
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0001C034 File Offset: 0x0001A234
			public void SetUserColor(Color col)
			{
				this.userSmallColorControl[this.currentlyUsedUserSmallColorControl].InternalColor = col;
				this.colorDialog.customColors[this.currentlyUsedUserSmallColorControl] = col.ToArgb();
				this.currentlyUsedUserSmallColorControl++;
				if (this.currentlyUsedUserSmallColorControl > 15)
				{
					this.currentlyUsedUserSmallColorControl = 0;
				}
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0001C090 File Offset: 0x0001A290
			public void SetCustomColors()
			{
				for (int i = 0; i < this.colorDialog.customColors.Length; i++)
				{
					this.userSmallColorControl[i].InternalColor = Color.FromArgb(this.colorDialog.customColors[i]);
				}
			}

			// Token: 0x04000705 RID: 1797
			private ColorDialog.BaseColorControl.SmallColorControl[] smallColorControl;

			// Token: 0x04000706 RID: 1798
			private ColorDialog.BaseColorControl.SmallColorControl[] userSmallColorControl;

			// Token: 0x04000707 RID: 1799
			private Label userColorLabel;

			// Token: 0x04000708 RID: 1800
			private Label baseColorLabel;

			// Token: 0x04000709 RID: 1801
			private ColorDialog.BaseColorControl.SmallColorControl selectedSmallColorControl;

			// Token: 0x0400070A RID: 1802
			private int currentlyUsedUserSmallColorControl;

			// Token: 0x0400070B RID: 1803
			private ColorDialog colorDialog;

			// Token: 0x02000081 RID: 129
			internal class SmallColorControl : Control
			{
				// Token: 0x060005F7 RID: 1527 RVA: 0x0001C0DC File Offset: 0x0001A2DC
				public SmallColorControl(Color color)
				{
					base.SuspendLayout();
					this.internalcolor = color;
					base.Size = new Size(25, 23);
					base.ResumeLayout(false);
					base.SetStyle(ControlStyles.DoubleBuffer, true);
					base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
					base.SetStyle(ControlStyles.UserPaint, true);
					base.SetStyle(ControlStyles.Selectable, true);
				}

				// Token: 0x1700015C RID: 348
				// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001C150 File Offset: 0x0001A350
				// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001C140 File Offset: 0x0001A340
				public bool IsSelected
				{
					get
					{
						return this.isSelected;
					}
					set
					{
						this.isSelected = value;
						base.Invalidate();
					}
				}

				// Token: 0x1700015D RID: 349
				// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001C168 File Offset: 0x0001A368
				// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001C158 File Offset: 0x0001A358
				public Color InternalColor
				{
					get
					{
						return this.internalcolor;
					}
					set
					{
						this.internalcolor = value;
						base.Invalidate();
					}
				}

				// Token: 0x060005FC RID: 1532 RVA: 0x0001C170 File Offset: 0x0001A370
				protected override void OnPaint(PaintEventArgs pe)
				{
					base.OnPaint(pe);
					pe.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.internalcolor), new Rectangle(4, 4, 17, 15));
					ControlPaint.DrawBorder3D(pe.Graphics, 3, 3, 19, 17, Border3DStyle.Sunken);
					if (this.isSelected)
					{
						pe.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.Black), new Rectangle(2, 2, 20, 18));
					}
					if (this.Focused)
					{
						ControlPaint.DrawFocusRectangle(pe.Graphics, new Rectangle(0, 0, 25, 23));
					}
				}

				// Token: 0x060005FD RID: 1533 RVA: 0x0001C218 File Offset: 0x0001A418
				protected override void OnClick(EventArgs e)
				{
					base.Focus();
					this.IsSelected = true;
					base.OnClick(e);
				}

				// Token: 0x060005FE RID: 1534 RVA: 0x0001C230 File Offset: 0x0001A430
				protected override void OnLostFocus(EventArgs e)
				{
					base.Invalidate();
					base.OnLostFocus(e);
				}

				// Token: 0x0400070C RID: 1804
				private Color internalcolor;

				// Token: 0x0400070D RID: 1805
				private bool isSelected;
			}
		}

		// Token: 0x02000082 RID: 130
		internal class ColorMatrixControl : Panel
		{
			// Token: 0x060005FF RID: 1535 RVA: 0x0001C240 File Offset: 0x0001A440
			public ColorMatrixControl(ColorDialog colorDialog)
			{
				this.colorDialog = colorDialog;
				base.SuspendLayout();
				base.BorderStyle = BorderStyle.Fixed3D;
				base.Location = new Point(0, 0);
				base.Size = new Size(179, 190);
				base.TabIndex = 0;
				base.TabStop = false;
				base.ResumeLayout(false);
				this.xstep = 240f / (float)(base.ClientSize.Width - 1);
				this.ystep = 241f / (float)(base.ClientSize.Height - 1);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001C30C File Offset: 0x0001A50C
			protected override void OnPaint(PaintEventArgs e)
			{
				if (this.drawingBitmap == null)
				{
					this.drawingBitmap = new ColorDialog.ColorMatrixControl.DrawingBitmap(base.ClientSize);
				}
				this.Draw(e);
				base.OnPaint(e);
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0001C344 File Offset: 0x0001A544
			private void Draw(PaintEventArgs e)
			{
				e.Graphics.DrawImage(this.drawingBitmap.Bitmap, base.ClientRectangle.X, base.ClientRectangle.Y);
				if (this.drawCross)
				{
					e.Graphics.DrawImage(this.crossCursor.Bitmap, this.currentXPos - 11, this.currentYPos - 11);
				}
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.mouseButtonDown = true;
				this.currentXPos = e.X;
				this.currentYPos = e.Y;
				if (this.drawCross)
				{
					this.drawCross = false;
					base.Invalidate();
					base.Update();
				}
				this.UpdateControls();
				XplatUI.GrabWindow(this.Handle, this.Handle);
				base.OnMouseDown(e);
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0001C420 File Offset: 0x0001A620
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.mouseButtonDown && e.X < base.ClientSize.Width && e.X >= 0 && e.Y < base.ClientSize.Height && e.Y >= 0)
				{
					this.currentXPos = e.X;
					this.currentYPos = e.Y;
					this.UpdateControls();
				}
				base.OnMouseMove(e);
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
			protected override void OnMouseUp(MouseEventArgs e)
			{
				XplatUI.UngrabWindow(this.Handle);
				this.mouseButtonDown = false;
				this.drawCross = true;
				base.Invalidate();
				base.Update();
			}

			// Token: 0x1700015E RID: 350
			// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
			public Color ColorToShow
			{
				set
				{
					this.ComputePos(value);
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001C4DC File Offset: 0x0001A6DC
			private void ComputePos(Color acolor)
			{
				if (acolor != this.color)
				{
					this.color = acolor;
					ColorDialog.HSB hsb = ColorDialog.HSB.RGB2HSB(this.color);
					this.currentXPos = (int)((float)hsb.hue / this.xstep);
					this.currentYPos = base.ClientSize.Height - 1 - (int)((float)hsb.sat / this.ystep);
					if (this.currentXPos < 0)
					{
						this.currentXPos = 0;
					}
					if (this.currentYPos < 0)
					{
						this.currentYPos = 0;
					}
					base.Invalidate();
					base.Update();
				}
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0001C57C File Offset: 0x0001A77C
			private Color GetColorFromHSB()
			{
				int num = (int)((float)this.currentXPos * this.xstep);
				int num2 = 240 - (int)((float)this.currentYPos * this.ystep);
				int currentBrightness = ColorDialog.TriangleControl.CurrentBrightness;
				return ColorDialog.HSB.HSB2RGB(num, num2, currentBrightness);
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0001C5C0 File Offset: 0x0001A7C0
			private void UpdateControls()
			{
				Color colorFromHSB = this.GetColorFromHSB();
				this.colorDialog.brightnessControl.ShowColor((int)((float)this.currentXPos * this.xstep), 240 - (int)((float)this.currentYPos * this.ystep));
				int num = 240 - (int)((float)this.currentYPos * this.ystep);
				this.colorDialog.satTextBox.Text = num.ToString();
				int num2 = (int)((float)this.currentXPos * this.xstep);
				if (num2 > 239)
				{
					num2 = 239;
				}
				this.colorDialog.hueTextBox.Text = num2.ToString();
				this.colorDialog.selectedColorPanel.BackColor = colorFromHSB;
				this.colorDialog.UpdateRGBTextBoxes(colorFromHSB);
			}

			// Token: 0x0400070E RID: 1806
			private ColorDialog.ColorMatrixControl.DrawingBitmap drawingBitmap;

			// Token: 0x0400070F RID: 1807
			private ColorDialog.ColorMatrixControl.CrossCursor crossCursor = new ColorDialog.ColorMatrixControl.CrossCursor();

			// Token: 0x04000710 RID: 1808
			private bool mouseButtonDown;

			// Token: 0x04000711 RID: 1809
			private bool drawCross = true;

			// Token: 0x04000712 RID: 1810
			private Color color;

			// Token: 0x04000713 RID: 1811
			private int currentXPos;

			// Token: 0x04000714 RID: 1812
			private int currentYPos;

			// Token: 0x04000715 RID: 1813
			private float xstep;

			// Token: 0x04000716 RID: 1814
			private float ystep;

			// Token: 0x04000717 RID: 1815
			private ColorDialog colorDialog;

			// Token: 0x02000083 RID: 131
			internal class DrawingBitmap
			{
				// Token: 0x06000609 RID: 1545 RVA: 0x0001C68C File Offset: 0x0001A88C
				public DrawingBitmap(Size size)
				{
					this.bitmap = new Bitmap(size.Width, size.Height);
					float num = 240f / (float)(size.Width - 1);
					float num2 = 241f / (float)(size.Height - 1);
					float num3 = 240f;
					for (int i = 0; i < size.Height; i++)
					{
						float num4 = 0f;
						for (int j = 0; j < size.Width; j++)
						{
							ColorDialog.HSB hsb = default(ColorDialog.HSB);
							hsb.hue = (int)num4;
							hsb.sat = (int)num3;
							hsb.bri = 120;
							this.bitmap.SetPixel(j, i, ColorDialog.HSB.HSB2RGB(hsb.hue, hsb.sat, hsb.bri));
							num4 += num;
						}
						num3 -= num2;
					}
				}

				// Token: 0x1700015F RID: 351
				// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001C780 File Offset: 0x0001A980
				// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001C774 File Offset: 0x0001A974
				public Bitmap Bitmap
				{
					get
					{
						return this.bitmap;
					}
					set
					{
						this.bitmap = value;
					}
				}

				// Token: 0x04000718 RID: 1816
				private Bitmap bitmap;
			}

			// Token: 0x02000084 RID: 132
			internal class CrossCursor
			{
				// Token: 0x0600060C RID: 1548 RVA: 0x0001C788 File Offset: 0x0001A988
				public CrossCursor()
				{
					this.bitmap = new Bitmap(22, 22);
					this.cursorColor = Color.Black;
					this.Draw();
				}

				// Token: 0x0600060D RID: 1549 RVA: 0x0001C7BC File Offset: 0x0001A9BC
				public void Draw()
				{
					using (Pen pen = new Pen(ThemeEngine.Current.ResPool.GetSolidBrush(this.cursorColor), 3f))
					{
						using (Graphics graphics = Graphics.FromImage(this.bitmap))
						{
							graphics.DrawLine(pen, 11, 0, 11, 7);
							graphics.DrawLine(pen, 11, 14, 11, 21);
							graphics.DrawLine(pen, 0, 11, 7, 11);
							graphics.DrawLine(pen, 14, 11, 21, 11);
						}
					}
				}

				// Token: 0x17000160 RID: 352
				// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001C894 File Offset: 0x0001AA94
				// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001C888 File Offset: 0x0001AA88
				public Bitmap Bitmap
				{
					get
					{
						return this.bitmap;
					}
					set
					{
						this.bitmap = value;
					}
				}

				// Token: 0x17000161 RID: 353
				// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
				// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001C89C File Offset: 0x0001AA9C
				public Color CursorColor
				{
					get
					{
						return this.cursorColor;
					}
					set
					{
						this.cursorColor = value;
					}
				}

				// Token: 0x04000719 RID: 1817
				private Bitmap bitmap;

				// Token: 0x0400071A RID: 1818
				private Color cursorColor;
			}
		}

		// Token: 0x02000085 RID: 133
		internal class BrightnessControl : Panel
		{
			// Token: 0x06000612 RID: 1554 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
			public BrightnessControl(ColorDialog colorDialog)
			{
				this.colorDialog = colorDialog;
				this.bitmap = new ColorDialog.BrightnessControl.DrawingBitmap();
				base.SuspendLayout();
				base.BorderStyle = BorderStyle.Fixed3D;
				base.Location = new Point(0, 0);
				base.Size = new Size(14, 190);
				base.TabIndex = 0;
				base.TabStop = false;
				base.Size = new Size(14, 190);
				base.ResumeLayout(false);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x0001C948 File Offset: 0x0001AB48
			protected override void OnPaint(PaintEventArgs e)
			{
				e.Graphics.DrawImage(this.bitmap.Bitmap, 0, 0);
				base.OnPaint(e);
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x0001C974 File Offset: 0x0001AB74
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.colorDialog.triangleControl.TrianglePosition = (int)((float)(189 - e.Y) * 1.2751323f);
				base.OnMouseDown(e);
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
			public void ShowColor(int hue, int sat)
			{
				this.bitmap.Draw(hue, sat);
				base.Invalidate();
				base.Update();
			}

			// Token: 0x17000162 RID: 354
			// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
			public Color ColorToShow
			{
				set
				{
					int num;
					int num2;
					ColorDialog.HSB.GetHueSaturation(value, out num, out num2);
					this.bitmap.Draw(num, num2);
					base.Invalidate();
					base.Update();
				}
			}

			// Token: 0x0400071B RID: 1819
			private const float step = 1.2751323f;

			// Token: 0x0400071C RID: 1820
			private ColorDialog.BrightnessControl.DrawingBitmap bitmap;

			// Token: 0x0400071D RID: 1821
			private ColorDialog colorDialog;

			// Token: 0x02000086 RID: 134
			internal class DrawingBitmap
			{
				// Token: 0x06000617 RID: 1559 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
				public DrawingBitmap()
				{
					this.bitmap = new Bitmap(14, 190);
				}

				// Token: 0x17000163 RID: 355
				// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001CA18 File Offset: 0x0001AC18
				// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001CA0C File Offset: 0x0001AC0C
				public Bitmap Bitmap
				{
					get
					{
						return this.bitmap;
					}
					set
					{
						this.bitmap = value;
					}
				}

				// Token: 0x0600061A RID: 1562 RVA: 0x0001CA20 File Offset: 0x0001AC20
				public void Draw(int hue, int sat)
				{
					float num = 1.268421f;
					float num2 = 241f;
					for (int i = 0; i < 190; i++)
					{
						for (int j = 0; j < 14; j++)
						{
							Color color = ColorDialog.HSB.HSB2RGB(hue, sat, (int)num2);
							this.bitmap.SetPixel(j, i, color);
						}
						num2 -= num;
					}
				}

				// Token: 0x0400071E RID: 1822
				private Bitmap bitmap;
			}
		}

		// Token: 0x02000087 RID: 135
		internal class TriangleControl : Panel
		{
			// Token: 0x0600061B RID: 1563 RVA: 0x0001CA80 File Offset: 0x0001AC80
			public TriangleControl(ColorDialog colorDialog)
			{
				this.colorDialog = colorDialog;
				base.SuspendLayout();
				base.Size = new Size(16, 203);
				base.ResumeLayout(false);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
			// (set) Token: 0x0600061D RID: 1565 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
			public static int CurrentBrightness
			{
				get
				{
					return ColorDialog.TriangleControl.currentBrightness;
				}
				set
				{
					ColorDialog.TriangleControl.currentBrightness = value;
				}
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
			protected override void OnPaint(PaintEventArgs e)
			{
				this.Draw(e);
				base.OnPaint(e);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0001CB08 File Offset: 0x0001AD08
			private void Draw(PaintEventArgs e)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), new Rectangle(0, 0, 16, 203));
				Point[] array = new Point[]
				{
					new Point(0, this.currentTrianglePosition),
					new Point(8, this.currentTrianglePosition - 8),
					new Point(8, this.currentTrianglePosition + 8)
				};
				e.Graphics.FillPolygon(ThemeEngine.Current.ResPool.GetSolidBrush(Color.Black), array);
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Y > 195 || e.Y < 9)
				{
					return;
				}
				this.mouseButtonDown = true;
				this.currentTrianglePosition = e.Y;
				this.colorDialog.briTextBox.Text = this.TrianglePosition.ToString();
				this.colorDialog.UpdateFromHSBTextBoxes();
				base.Invalidate();
				base.Update();
				base.OnMouseDown(e);
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0001CC34 File Offset: 0x0001AE34
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.mouseButtonDown && e.Y < 196 && e.Y > 8)
				{
					this.currentTrianglePosition = e.Y;
					this.colorDialog.briTextBox.Text = this.TrianglePosition.ToString();
					this.colorDialog.UpdateFromHSBTextBoxes();
					base.Invalidate();
					base.Update();
				}
				base.OnMouseMove(e);
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
			protected override void OnMouseUp(MouseEventArgs e)
			{
				this.mouseButtonDown = false;
				base.OnMouseUp(e);
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
			// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
			public int TrianglePosition
			{
				get
				{
					float num = (float)(this.currentTrianglePosition - 9);
					num *= 1.2956989f;
					int num2 = 240 - (int)num;
					ColorDialog.TriangleControl.CurrentBrightness = num2;
					return num2;
				}
				set
				{
					float num = (float)value / 1.2956989f;
					this.currentTrianglePosition = 186 - (int)num + 9;
					this.colorDialog.briTextBox.Text = this.TrianglePosition.ToString();
					this.colorDialog.UpdateFromHSBTextBoxes();
					base.Invalidate();
					base.Update();
				}
			}

			// Token: 0x17000166 RID: 358
			// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001CD4C File Offset: 0x0001AF4C
			public Color ColorToShow
			{
				set
				{
					this.SetColor(value);
				}
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001CD58 File Offset: 0x0001AF58
			public void SetColor(Color color)
			{
				int num = ColorDialog.HSB.Brightness(color);
				float num2 = (float)num / 1.2956989f;
				this.currentTrianglePosition = 186 - (int)num2 + 9;
				if (this.colorDialog.edit_textbox == null)
				{
					this.colorDialog.briTextBox.Text = this.TrianglePosition.ToString();
				}
				base.Invalidate();
			}

			// Token: 0x0400071F RID: 1823
			private const float briStep = 1.2956989f;

			// Token: 0x04000720 RID: 1824
			private bool mouseButtonDown;

			// Token: 0x04000721 RID: 1825
			private int currentTrianglePosition = 195;

			// Token: 0x04000722 RID: 1826
			private static int currentBrightness;

			// Token: 0x04000723 RID: 1827
			private ColorDialog colorDialog;
		}
	}
}
