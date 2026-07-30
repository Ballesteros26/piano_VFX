using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to choose a font from among those installed on the local computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000192 RID: 402
	[DefaultEvent("Apply")]
	[DefaultProperty("Font")]
	public class FontDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FontDialog" /> class.</summary>
		// Token: 0x06001992 RID: 6546 RVA: 0x0006238C File Offset: 0x0006058C
		public FontDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.example_panel_text = this.char_sets[0];
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.applyButton = new Button();
			this.helpButton = new Button();
			this.fontTextBox = new TextBox();
			this.fontstyleTextBox = new TextBox();
			this.fontsizeTextBox = new TextBox();
			this.fontListBox = new MouseWheelListBox();
			this.fontsizeListBox = new MouseWheelListBox();
			this.fontstyleListBox = new MouseWheelListBox();
			this.fontLabel = new Label();
			this.fontstyleLabel = new Label();
			this.sizeLabel = new Label();
			this.scriptLabel = new Label();
			this.exampleGroupBox = new GroupBox();
			this.effectsGroupBox = new GroupBox();
			this.underlinedCheckBox = new CheckBox();
			this.strikethroughCheckBox = new CheckBox();
			this.scriptComboBox = new ComboBox();
			this.examplePanel = new Panel();
			this.colorComboBox = new FontDialog.ColorComboBox(this);
			this.exampleGroupBox.SuspendLayout();
			this.effectsGroupBox.SuspendLayout();
			this.form.SuspendLayout();
			this.form.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.form.MaximizeBox = false;
			this.fontsizeListBox.Location = new Point(284, 47);
			this.fontsizeListBox.Size = new Size(52, 95);
			this.fontsizeListBox.TabIndex = 10;
			this.fontListBox.Sorted = true;
			this.fontTextBox.Location = new Point(16, 26);
			this.fontTextBox.Size = new Size(140, 21);
			this.fontTextBox.TabIndex = 5;
			this.fontTextBox.Text = string.Empty;
			this.fontstyleLabel.Location = new Point(164, 10);
			this.fontstyleLabel.Size = new Size(100, 16);
			this.fontstyleLabel.TabIndex = 1;
			this.fontstyleLabel.Text = "Font Style:";
			this.fontsizeTextBox.Location = new Point(284, 26);
			this.fontsizeTextBox.Size = new Size(52, 21);
			this.fontsizeTextBox.TabIndex = 7;
			this.fontsizeTextBox.Text = string.Empty;
			this.fontsizeTextBox.MaxLength = 2;
			this.fontListBox.Location = new Point(16, 47);
			this.fontListBox.Size = new Size(140, 95);
			this.fontListBox.TabIndex = 8;
			this.fontListBox.Sorted = true;
			this.exampleGroupBox.Controls.Add(this.examplePanel);
			this.exampleGroupBox.FlatStyle = FlatStyle.System;
			this.exampleGroupBox.Location = new Point(164, 158);
			this.exampleGroupBox.Size = new Size(172, 70);
			this.exampleGroupBox.TabIndex = 12;
			this.exampleGroupBox.TabStop = false;
			this.exampleGroupBox.Text = "Example";
			this.fontstyleListBox.Location = new Point(164, 47);
			this.fontstyleListBox.Size = new Size(112, 95);
			this.fontstyleListBox.TabIndex = 9;
			this.fontLabel.Location = new Point(16, 10);
			this.fontLabel.Size = new Size(88, 16);
			this.fontLabel.TabIndex = 0;
			this.fontLabel.Text = "Font:";
			this.effectsGroupBox.Controls.Add(this.underlinedCheckBox);
			this.effectsGroupBox.Controls.Add(this.strikethroughCheckBox);
			this.effectsGroupBox.Controls.Add(this.colorComboBox);
			this.effectsGroupBox.FlatStyle = FlatStyle.System;
			this.effectsGroupBox.Location = new Point(16, 158);
			this.effectsGroupBox.Size = new Size(140, 116);
			this.effectsGroupBox.TabIndex = 11;
			this.effectsGroupBox.TabStop = false;
			this.effectsGroupBox.Text = "Effects";
			this.strikethroughCheckBox.FlatStyle = FlatStyle.System;
			this.strikethroughCheckBox.Location = new Point(8, 16);
			this.strikethroughCheckBox.TabIndex = 0;
			this.strikethroughCheckBox.Text = "Strikethrough";
			this.colorComboBox.Location = new Point(8, 70);
			this.colorComboBox.Size = new Size(130, 21);
			this.sizeLabel.Location = new Point(284, 10);
			this.sizeLabel.Size = new Size(100, 16);
			this.sizeLabel.TabIndex = 2;
			this.sizeLabel.Text = "Size:";
			this.scriptComboBox.Location = new Point(164, 253);
			this.scriptComboBox.Size = new Size(172, 21);
			this.scriptComboBox.TabIndex = 14;
			this.scriptComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Location = new Point(352, 26);
			this.okButton.Size = new Size(70, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(352, 52);
			this.cancelButton.Size = new Size(70, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.applyButton.FlatStyle = FlatStyle.System;
			this.applyButton.Location = new Point(352, 78);
			this.applyButton.Size = new Size(70, 23);
			this.applyButton.TabIndex = 5;
			this.applyButton.Text = "Apply";
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Location = new Point(352, 104);
			this.helpButton.Size = new Size(70, 23);
			this.helpButton.TabIndex = 6;
			this.helpButton.Text = "Help";
			this.underlinedCheckBox.FlatStyle = FlatStyle.System;
			this.underlinedCheckBox.Location = new Point(8, 36);
			this.underlinedCheckBox.TabIndex = 1;
			this.underlinedCheckBox.Text = "Underlined";
			this.fontstyleTextBox.Location = new Point(164, 26);
			this.fontstyleTextBox.Size = new Size(112, 21);
			this.fontstyleTextBox.TabIndex = 6;
			this.fontstyleTextBox.Text = string.Empty;
			this.scriptLabel.Location = new Point(164, 236);
			this.scriptLabel.Size = new Size(100, 16);
			this.scriptLabel.TabIndex = 13;
			this.scriptLabel.Text = "Script:";
			this.examplePanel.Location = new Point(8, 20);
			this.examplePanel.TabIndex = 0;
			this.examplePanel.Size = new Size(156, 40);
			this.examplePanel.BorderStyle = BorderStyle.Fixed3D;
			this.form.AcceptButton = this.okButton;
			this.form.CancelButton = this.cancelButton;
			this.form.Controls.Add(this.scriptComboBox);
			this.form.Controls.Add(this.scriptLabel);
			this.form.Controls.Add(this.exampleGroupBox);
			this.form.Controls.Add(this.effectsGroupBox);
			this.form.Controls.Add(this.fontsizeListBox);
			this.form.Controls.Add(this.fontstyleListBox);
			this.form.Controls.Add(this.fontListBox);
			this.form.Controls.Add(this.fontsizeTextBox);
			this.form.Controls.Add(this.fontstyleTextBox);
			this.form.Controls.Add(this.fontTextBox);
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.okButton);
			this.form.Controls.Add(this.sizeLabel);
			this.form.Controls.Add(this.fontstyleLabel);
			this.form.Controls.Add(this.fontLabel);
			this.form.Controls.Add(this.applyButton);
			this.form.Controls.Add(this.helpButton);
			this.exampleGroupBox.ResumeLayout(false);
			this.effectsGroupBox.ResumeLayout(false);
			this.form.Size = new Size(430, 318);
			this.form.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.form.MaximizeBox = false;
			this.form.Text = "Font";
			this.form.ResumeLayout(false);
			this.scriptComboBox.BeginUpdate();
			this.scriptComboBox.Items.AddRange(this.char_sets_names);
			this.scriptComboBox.SelectedIndex = 0;
			this.scriptComboBox.EndUpdate();
			this.applyButton.Hide();
			this.helpButton.Hide();
			this.colorComboBox.Hide();
			this.cancelButton.Click += new EventHandler(this.OnClickCancelButton);
			this.okButton.Click += new EventHandler(this.OnClickOkButton);
			this.applyButton.Click += new EventHandler(this.OnApplyButton);
			this.examplePanel.Paint += this.OnPaintExamplePanel;
			this.fontListBox.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChangedFontListBox);
			this.fontsizeListBox.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChangedSizeListBox);
			this.fontstyleListBox.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChangedFontStyleListBox);
			this.underlinedCheckBox.CheckedChanged += new EventHandler(this.OnCheckedChangedUnderlinedCheckBox);
			this.strikethroughCheckBox.CheckedChanged += new EventHandler(this.OnCheckedChangedStrikethroughCheckBox);
			this.scriptComboBox.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChangedScriptComboBox);
			this.fontTextBox.KeyPress += this.OnFontTextBoxKeyPress;
			this.fontstyleTextBox.KeyPress += this.OnFontStyleTextBoxKeyPress;
			this.fontsizeTextBox.KeyPress += this.OnFontSizeTextBoxKeyPress;
			this.fontTextBox.TextChanged += new EventHandler(this.OnFontTextBoxTextChanged);
			this.fontstyleTextBox.TextChanged += new EventHandler(this.OnFontStyleTextTextChanged);
			this.fontsizeTextBox.TextChanged += new EventHandler(this.OnFontSizeTextBoxTextChanged);
			this.fontTextBox.KeyDown += this.OnFontTextBoxKeyDown;
			this.fontstyleTextBox.KeyDown += this.OnFontStyleTextBoxKeyDown;
			this.fontsizeTextBox.KeyDown += this.OnFontSizeTextBoxKeyDown;
			this.fontTextBox.MouseWheel += this.OnFontTextBoxMouseWheel;
			this.fontstyleTextBox.MouseWheel += this.OnFontStyleTextBoxMouseWheel;
			this.fontsizeTextBox.MouseWheel += this.OnFontSizeTextBoxMouseWheel;
			this.PopulateFontList();
		}

		/// <summary>Occurs when the user clicks the Apply button in the font dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000197 RID: 407
		// (add) Token: 0x06001994 RID: 6548 RVA: 0x000634EC File Offset: 0x000616EC
		// (remove) Token: 0x06001995 RID: 6549 RVA: 0x00063500 File Offset: 0x00061700
		public event EventHandler Apply
		{
			add
			{
				base.Events.AddHandler(FontDialog.EventApply, value);
			}
			remove
			{
				base.Events.RemoveHandler(FontDialog.EventApply, value);
			}
		}

		/// <summary>Gets or sets the selected font.</summary>
		/// <returns>The selected font.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00063514 File Offset: 0x00061714
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x0006351C File Offset: 0x0006171C
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				if (value != null)
				{
					this.font = new Font(value, value.Style);
					this.currentFontStyle = this.font.Style;
					this.currentSize = this.font.SizeInPoints;
					this.currentFontName = this.font.Name;
					this.strikethroughCheckBox.Checked = this.font.Strikeout;
					this.underlinedCheckBox.Checked = this.font.Underline;
					int num = this.fontListBox.FindString(this.currentFontName);
					if (num != -1)
					{
						this.fontListBox.SelectedIndex = num;
					}
					else
					{
						this.fontListBox.SelectedIndex = 0;
					}
					this.UpdateFontSizeListBox();
					this.UpdateFontStyleListBox();
					this.fontListBox.TopIndex = this.fontListBox.SelectedIndex;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box specifies an error condition if the user attempts to select a font or style that does not exist.</summary>
		/// <returns>true if the dialog box specifies an error condition when the user tries to select a font or style that does not exist; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000635F8 File Offset: 0x000617F8
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00063600 File Offset: 0x00061800
		[DefaultValue(false)]
		public bool FontMustExist
		{
			get
			{
				return this.fontMustExist;
			}
			set
			{
				this.fontMustExist = value;
			}
		}

		/// <summary>Gets or sets the selected font color.</summary>
		/// <returns>The color of the selected font. The default value is <see cref="P:System.Drawing.Color.Black" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00063620 File Offset: 0x00061820
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x0006360C File Offset: 0x0006180C
		[DefaultValue("Color [Black]")]
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
				this.examplePanel.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows graphics device interface (GDI) font simulations.</summary>
		/// <returns>true if font simulations are allowed; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00063634 File Offset: 0x00061834
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x00063628 File Offset: 0x00061828
		[DefaultValue(true)]
		public bool AllowSimulations
		{
			get
			{
				return this.allowSimulations;
			}
			set
			{
				this.allowSimulations = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows vector font selections.</summary>
		/// <returns>true if vector fonts are allowed; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00063648 File Offset: 0x00061848
		// (set) Token: 0x0600199E RID: 6558 RVA: 0x0006363C File Offset: 0x0006183C
		[DefaultValue(true)]
		public bool AllowVectorFonts
		{
			get
			{
				return this.allowVectorFonts;
			}
			set
			{
				this.allowVectorFonts = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays both vertical and horizontal fonts or only horizontal fonts.</summary>
		/// <returns>true if both vertical and horizontal fonts are allowed; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0006365C File Offset: 0x0006185C
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x00063650 File Offset: 0x00061850
		[DefaultValue(true)]
		public bool AllowVerticalFonts
		{
			get
			{
				return this.allowVerticalFonts;
			}
			set
			{
				this.allowVerticalFonts = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can change the character set specified in the Script combo box to display a character set other than the one currently displayed.</summary>
		/// <returns>true if the user can change the character set specified in the Script combo box; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00063670 File Offset: 0x00061870
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x00063664 File Offset: 0x00061864
		[DefaultValue(true)]
		public bool AllowScriptChange
		{
			get
			{
				return this.allowScriptChange;
			}
			set
			{
				this.allowScriptChange = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows only the selection of fixed-pitch fonts.</summary>
		/// <returns>true if only fixed-pitch fonts can be selected; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00063694 File Offset: 0x00061894
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x00063678 File Offset: 0x00061878
		[DefaultValue(false)]
		public bool FixedPitchOnly
		{
			get
			{
				return this.fixedPitchOnly;
			}
			set
			{
				if (this.fixedPitchOnly != value)
				{
					this.fixedPitchOnly = value;
					this.PopulateFontList();
				}
			}
		}

		/// <summary>Gets or sets the maximum point size a user can select.</summary>
		/// <returns>The maximum point size a user can select. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x000636DC File Offset: 0x000618DC
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x0006369C File Offset: 0x0006189C
		[DefaultValue(0)]
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
				if (this.maxSize < 0)
				{
					this.maxSize = 0;
				}
				if (this.maxSize < this.minSize)
				{
					this.minSize = this.maxSize;
				}
				this.CreateFontSizeListBoxItems();
			}
		}

		/// <summary>Gets or sets the minimum point size a user can select.</summary>
		/// <returns>The minimum point size a user can select. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0006379C File Offset: 0x0006199C
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x000636E4 File Offset: 0x000618E4
		[DefaultValue(0)]
		public int MinSize
		{
			get
			{
				return this.minSize;
			}
			set
			{
				this.minSize = value;
				if (this.minSize < 0)
				{
					this.minSize = 0;
				}
				if (this.minSize > this.maxSize)
				{
					this.maxSize = this.minSize;
				}
				this.CreateFontSizeListBoxItems();
				if ((float)this.minSize > this.currentSize && this.font != null)
				{
					this.font.Dispose();
					this.currentSize = (float)this.minSize;
					this.font = new Font(this.currentFamily, this.currentSize, this.currentFontStyle);
					this.UpdateExamplePanel();
					this.fontsizeTextBox.Text = this.currentSize.ToString();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set.</summary>
		/// <returns>true if selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set, is allowed; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x000637B0 File Offset: 0x000619B0
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x000637A4 File Offset: 0x000619A4
		[DefaultValue(false)]
		public bool ScriptsOnly
		{
			get
			{
				return this.scriptsOnly;
			}
			set
			{
				this.scriptsOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains an Apply button.</summary>
		/// <returns>true if the dialog box contains an Apply button; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0006380C File Offset: 0x00061A0C
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x000637B8 File Offset: 0x000619B8
		[DefaultValue(false)]
		public bool ShowApply
		{
			get
			{
				return this.showApply;
			}
			set
			{
				if (value != this.showApply)
				{
					this.showApply = value;
					if (this.showApply)
					{
						this.applyButton.Show();
					}
					else
					{
						this.applyButton.Hide();
					}
					this.form.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays the color choice.</summary>
		/// <returns>true if the dialog box displays the color choice; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00063868 File Offset: 0x00061A68
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x00063814 File Offset: 0x00061A14
		[DefaultValue(false)]
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}
			set
			{
				if (value != this.showColor)
				{
					this.showColor = value;
					if (this.showColor)
					{
						this.colorComboBox.Show();
					}
					else
					{
						this.colorComboBox.Hide();
					}
					this.form.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains controls that allow the user to specify strikethrough, underline, and text color options.</summary>
		/// <returns>true if the dialog box contains controls to set strikethrough, underline, and text color options; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000638C4 File Offset: 0x00061AC4
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x00063870 File Offset: 0x00061A70
		[DefaultValue(true)]
		public bool ShowEffects
		{
			get
			{
				return this.showEffects;
			}
			set
			{
				if (value != this.showEffects)
				{
					this.showEffects = value;
					if (this.showEffects)
					{
						this.effectsGroupBox.Show();
					}
					else
					{
						this.effectsGroupBox.Hide();
					}
					this.form.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a Help button.</summary>
		/// <returns>true if the dialog box displays a Help button; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00063920 File Offset: 0x00061B20
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x000638CC File Offset: 0x00061ACC
		[DefaultValue(false)]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				if (value != this.showHelp)
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
					this.form.Refresh();
				}
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.FontDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FontDialog" />.</returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00063928 File Offset: 0x00061B28
		protected int Options
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Resets all dialog box options to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060019B5 RID: 6581 RVA: 0x0006392C File Offset: 0x00061B2C
		public override void Reset()
		{
			this.color = Color.Black;
			this.allowSimulations = true;
			this.allowVectorFonts = true;
			this.allowVerticalFonts = true;
			this.allowScriptChange = true;
			this.fixedPitchOnly = false;
			this.maxSize = 0;
			this.minSize = 0;
			this.CreateFontSizeListBoxItems();
			this.scriptsOnly = false;
			this.showApply = false;
			this.applyButton.Hide();
			this.showColor = false;
			this.colorComboBox.Hide();
			this.showEffects = true;
			this.effectsGroupBox.Show();
			this.showHelp = false;
			this.helpButton.Hide();
			this.form.Refresh();
		}

		/// <summary>Retrieves a string that includes the name of the current font selected in the dialog box.</summary>
		/// <returns>A string that includes the name of the currently selected font.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060019B6 RID: 6582 RVA: 0x000639D8 File Offset: 0x00061BD8
		public override string ToString()
		{
			if (this.font == null)
			{
				return base.ToString();
			}
			return base.ToString() + ", Font: " + this.font.ToString();
		}

		/// <summary>Specifies the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <returns>A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.</returns>
		/// <param name="hWnd">The handle to the dialog box window. </param>
		/// <param name="msg">The message being received. </param>
		/// <param name="wparam">Additional information about the message. </param>
		/// <param name="lparam">Additional information about the message. </param>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00063A14 File Offset: 0x00061C14
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return base.HookProc(hWnd, msg, wparam, lparam);
		}

		/// <summary>Specifies a file dialog box.</summary>
		/// <returns>true if the dialog box was successfully run; otherwise, false.</returns>
		/// <param name="hWndOwner">The window handle of the owner window for the common dialog box.</param>
		// Token: 0x060019B8 RID: 6584 RVA: 0x00063A24 File Offset: 0x00061C24
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			this.form.Refresh();
			return true;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00063A34 File Offset: 0x00061C34
		internal void OnApplyButton(object sender, EventArgs e)
		{
			this.OnApply(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.FontDialog.Apply" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the data. </param>
		// Token: 0x060019BA RID: 6586 RVA: 0x00063A40 File Offset: 0x00061C40
		protected virtual void OnApply(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FontDialog.EventApply];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00063A74 File Offset: 0x00061C74
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00063A84 File Offset: 0x00061C84
		private void OnClickOkButton(object sender, EventArgs e)
		{
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00063A94 File Offset: 0x00061C94
		private void OnPaintExamplePanel(object sender, PaintEventArgs e)
		{
			SolidBrush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.color);
			e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(SystemColors.Control), 0, 0, 156, 40);
			SizeF sizeF = e.Graphics.MeasureString(this.example_panel_text, this.font);
			int num = (int)sizeF.Width;
			int num2 = (int)sizeF.Height;
			int num3 = this.examplePanel.Width / 2 - num / 2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = this.examplePanel.Height / 2 - num2 / 2;
			e.Graphics.DrawString(this.example_panel_text, this.font, solidBrush, new Point(num3, num4));
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00063B60 File Offset: 0x00061D60
		private void OnSelectedIndexChangedFontListBox(object sender, EventArgs e)
		{
			if (this.fontListBox.SelectedIndex != -1)
			{
				this.currentFamily = this.FindByName(this.fontListBox.Items[this.fontListBox.SelectedIndex].ToString());
				this.fontTextBox.Text = this.currentFamily.Name;
				this.internal_change = true;
				this.UpdateFontStyleListBox();
				this.UpdateFontSizeListBox();
				this.UpdateExamplePanel();
				this.form.Select(this.fontTextBox);
				this.internal_change = false;
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00063BF4 File Offset: 0x00061DF4
		private void OnSelectedIndexChangedSizeListBox(object sender, EventArgs e)
		{
			if (this.fontsizeListBox.SelectedIndex != -1)
			{
				this.currentSize = (float)Convert.ToDouble(this.fontsizeListBox.Items[this.fontsizeListBox.SelectedIndex]);
				this.fontsizeTextBox.Text = this.currentSize.ToString();
				this.UpdateExamplePanel();
				if (!this.internal_change)
				{
					this.form.Select(this.fontsizeTextBox);
				}
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00063C74 File Offset: 0x00061E74
		private void OnSelectedIndexChangedFontStyleListBox(object sender, EventArgs e)
		{
			if (this.fontstyleListBox.SelectedIndex != -1)
			{
				switch (this.fontstyleListBox.SelectedIndex)
				{
				case 0:
					this.currentFontStyle = 0;
					break;
				case 1:
					this.currentFontStyle = 1;
					break;
				case 2:
					this.currentFontStyle = 2;
					break;
				case 3:
					this.currentFontStyle = 3;
					break;
				default:
					this.currentFontStyle = 0;
					break;
				}
				if (this.underlined)
				{
					this.currentFontStyle |= 4;
				}
				if (this.strikethrough)
				{
					this.currentFontStyle |= 8;
				}
				this.fontstyleTextBox.Text = this.fontstyleListBox.Items[this.fontstyleListBox.SelectedIndex].ToString();
				if (!this.internal_change)
				{
					this.UpdateExamplePanel();
					this.form.Select(this.fontstyleTextBox);
				}
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00063D78 File Offset: 0x00061F78
		private void OnCheckedChangedUnderlinedCheckBox(object sender, EventArgs e)
		{
			if (this.underlinedCheckBox.Checked)
			{
				this.currentFontStyle |= 4;
				this.underlined = true;
			}
			else
			{
				this.currentFontStyle ^= 4;
				this.underlined = false;
			}
			this.UpdateExamplePanel();
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00063DCC File Offset: 0x00061FCC
		private void OnCheckedChangedStrikethroughCheckBox(object sender, EventArgs e)
		{
			if (this.strikethroughCheckBox.Checked)
			{
				this.currentFontStyle |= 8;
				this.strikethrough = true;
			}
			else
			{
				this.currentFontStyle ^= 8;
				this.strikethrough = false;
			}
			this.UpdateExamplePanel();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00063E20 File Offset: 0x00062020
		private void OnFontTextBoxMouseWheel(object sender, MouseEventArgs e)
		{
			this.fontListBox.SendMouseWheelEvent(e);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00063E30 File Offset: 0x00062030
		private void OnFontStyleTextBoxMouseWheel(object sender, MouseEventArgs e)
		{
			this.fontstyleListBox.SendMouseWheelEvent(e);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00063E40 File Offset: 0x00062040
		private void OnFontSizeTextBoxMouseWheel(object sender, MouseEventArgs e)
		{
			this.fontsizeListBox.SendMouseWheelEvent(e);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00063E50 File Offset: 0x00062050
		private void OnFontTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.Up:
			case Keys.Down:
				this.fontListBox.HandleKeyDown(e.KeyCode);
				break;
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00063EA8 File Offset: 0x000620A8
		private void OnFontStyleTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.Up:
			case Keys.Down:
				this.fontstyleListBox.HandleKeyDown(e.KeyCode);
				break;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00063F00 File Offset: 0x00062100
		private void OnFontSizeTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.Up:
			case Keys.Down:
				this.fontsizeListBox.HandleKeyDown(e.KeyCode);
				break;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00063F58 File Offset: 0x00062158
		private void OnFontTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			this.internal_textbox_change = true;
			if (this.fontListBox.SelectedIndex > -1)
			{
				this.fontListBox.SelectedIndex = -1;
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00063F8C File Offset: 0x0006218C
		private void OnFontStyleTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			this.internal_textbox_change = true;
			if (this.fontstyleListBox.SelectedIndex > -1)
			{
				this.fontstyleListBox.SelectedIndex = -1;
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00063FC0 File Offset: 0x000621C0
		private void OnFontSizeTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar) || e.KeyChar == ',')
			{
				e.Handled = true;
				return;
			}
			this.internal_textbox_change = true;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006401C File Offset: 0x0006221C
		private void OnFontTextBoxTextChanged(object sender, EventArgs e)
		{
			if (!this.internal_textbox_change)
			{
				return;
			}
			this.internal_textbox_change = false;
			string text = this.fontTextBox.Text;
			int num = this.fontListBox.FindStringExact(text);
			if (num != -1)
			{
				this.fontListBox.SelectedIndex = num;
				return;
			}
			num = this.fontListBox.FindString(text);
			if (num != -1)
			{
				this.fontListBox.TopIndex = num;
				return;
			}
			if (this.fontListBox.Items.Count > 0)
			{
				this.fontListBox.TopIndex = 0;
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000640AC File Offset: 0x000622AC
		private void OnFontStyleTextTextChanged(object sender, EventArgs e)
		{
			if (!this.internal_textbox_change)
			{
				return;
			}
			this.internal_textbox_change = false;
			int num = this.fontstyleListBox.FindStringExact(this.fontstyleTextBox.Text);
			if (num != -1)
			{
				this.fontstyleListBox.SelectedIndex = num;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000640F8 File Offset: 0x000622F8
		private void OnFontSizeTextBoxTextChanged(object sender, EventArgs e)
		{
			if (!this.internal_textbox_change)
			{
				return;
			}
			this.internal_textbox_change = false;
			if (this.fontsizeTextBox.Text.Length == 0)
			{
				return;
			}
			for (int i = 0; i < this.fontsizeListBox.Items.Count; i++)
			{
				string text = this.fontsizeListBox.Items[i] as string;
				if (text.StartsWith(this.fontsizeTextBox.Text))
				{
					if (text == this.fontsizeTextBox.Text)
					{
						this.fontsizeListBox.SelectedIndex = i;
					}
					else
					{
						this.fontsizeListBox.TopIndex = i;
					}
					break;
				}
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000641B4 File Offset: 0x000623B4
		private void OnSelectedIndexChangedScriptComboBox(object sender, EventArgs e)
		{
			string text = this.char_sets[this.scriptComboBox.SelectedIndex];
			if (text.Length > 0)
			{
				this.example_panel_text = text;
				this.UpdateExamplePanel();
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000641F0 File Offset: 0x000623F0
		private void UpdateExamplePanel()
		{
			if (this.font != null)
			{
				this.font.Dispose();
			}
			this.font = new Font(this.currentFamily, this.currentSize, this.currentFontStyle);
			this.examplePanel.Invalidate();
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0006423C File Offset: 0x0006243C
		private void UpdateFontSizeListBox()
		{
			int num = this.fontsizeListBox.FindString(((int)Math.Round((double)this.currentSize)).ToString());
			if (num != -1)
			{
				this.fontsizeListBox.SelectedIndex = num;
			}
			else
			{
				this.fontsizeListBox.SelectedIndex = 0;
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00064290 File Offset: 0x00062490
		private void UpdateFontStyleListBox()
		{
			this.fontstyleListBox.BeginUpdate();
			this.fontstyleListBox.Items.Clear();
			int num = 0;
			if (this.currentFamily.IsStyleAvailable(0))
			{
				int num2 = this.fontstyleListBox.Items.Add("Regular");
				num = num2;
			}
			if (this.currentFamily.IsStyleAvailable(1))
			{
				int num2 = this.fontstyleListBox.Items.Add("Bold");
				if ((this.currentFontStyle & 1) == 1)
				{
					num = num2;
				}
			}
			if (this.currentFamily.IsStyleAvailable(2))
			{
				int num2 = this.fontstyleListBox.Items.Add("Italic");
				if ((this.currentFontStyle & 2) == 2)
				{
					num = num2;
				}
			}
			if (this.currentFamily.IsStyleAvailable(1) && this.currentFamily.IsStyleAvailable(2))
			{
				int num2 = this.fontstyleListBox.Items.Add("Bold Italic");
				if ((this.currentFontStyle & 3) == 3)
				{
					num = num2;
				}
			}
			if (this.fontstyleListBox.Items.Count > 0)
			{
				this.fontstyleListBox.SelectedIndex = num;
				string text = (string)this.fontstyleListBox.SelectedItem;
				if (text != null)
				{
					if (FontDialog.<>f__switch$map6 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
						dictionary.Add("Regular", 0);
						dictionary.Add("Bold", 1);
						dictionary.Add("Italic", 2);
						dictionary.Add("Bold Italic", 3);
						FontDialog.<>f__switch$map6 = dictionary;
					}
					int num3;
					if (FontDialog.<>f__switch$map6.TryGetValue(text, ref num3))
					{
						switch (num3)
						{
						case 0:
							this.currentFontStyle = 0;
							break;
						case 1:
							this.currentFontStyle = 1;
							break;
						case 2:
							this.currentFontStyle = 2;
							break;
						case 3:
							this.currentFontStyle = 3;
							break;
						}
					}
				}
				if (this.strikethroughCheckBox.Checked)
				{
					this.currentFontStyle |= 8;
				}
				if (this.underlinedCheckBox.Checked)
				{
					this.currentFontStyle |= 4;
				}
			}
			this.fontstyleListBox.EndUpdate();
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000644C0 File Offset: 0x000626C0
		private FontFamily FindByName(string name)
		{
			return this.fontHash[name] as FontFamily;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000644D4 File Offset: 0x000626D4
		private void CreateFontSizeListBoxItems()
		{
			this.fontsizeListBox.BeginUpdate();
			this.fontsizeListBox.Items.Clear();
			if (this.minSize == 0 && this.maxSize == 0)
			{
				foreach (int num in this.a_sizes)
				{
					this.fontsizeListBox.Items.Add(num.ToString());
				}
			}
			else
			{
				foreach (int num2 in this.a_sizes)
				{
					if (num2 >= this.minSize && num2 <= this.maxSize)
					{
						this.fontsizeListBox.Items.Add(num2.ToString());
					}
				}
			}
			this.fontsizeListBox.EndUpdate();
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000645B0 File Offset: 0x000627B0
		private void PopulateFontList()
		{
			this.fontListBox.Items.Clear();
			this.fontHash.Clear();
			this.fontListBox.BeginUpdate();
			foreach (FontFamily fontFamily in FontFamily.Families)
			{
				if (!this.fontHash.ContainsKey(fontFamily.Name) && (!this.fixedPitchOnly || this.IsFontFamilyFixedPitch(fontFamily)))
				{
					this.fontListBox.Items.Add(fontFamily.Name);
					this.fontHash.Add(fontFamily.Name, fontFamily);
				}
			}
			this.fontListBox.EndUpdate();
			this.CreateFontSizeListBoxItems();
			if (this.fixedPitchOnly)
			{
				this.Font = new Font(FontFamily.GenericMonospace, 8.25f);
			}
			else
			{
				this.Font = this.form.Font;
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000646A0 File Offset: 0x000628A0
		private bool IsFontFamilyFixedPitch(FontFamily family)
		{
			FontStyle fontStyle;
			if (family.IsStyleAvailable(0))
			{
				fontStyle = 0;
			}
			else if (family.IsStyleAvailable(1))
			{
				fontStyle = 1;
			}
			else if (family.IsStyleAvailable(2))
			{
				fontStyle = 2;
			}
			else if (family.IsStyleAvailable(8))
			{
				fontStyle = 8;
			}
			else
			{
				if (!family.IsStyleAvailable(4))
				{
					return false;
				}
				fontStyle = 4;
			}
			Font font = new Font(family.Name, 10f, fontStyle);
			return TextRenderer.MeasureString("i", font).Width == TextRenderer.MeasureString("w", font).Width;
		}

		/// <summary>Owns the <see cref="E:System.Windows.Forms.FontDialog.Apply" /> event.</summary>
		// Token: 0x04000E66 RID: 3686
		protected static readonly object EventApply = new object();

		// Token: 0x04000E67 RID: 3687
		private Font font;

		// Token: 0x04000E68 RID: 3688
		private Color color = Color.Black;

		// Token: 0x04000E69 RID: 3689
		private bool allowSimulations = true;

		// Token: 0x04000E6A RID: 3690
		private bool allowVectorFonts = true;

		// Token: 0x04000E6B RID: 3691
		private bool allowVerticalFonts = true;

		// Token: 0x04000E6C RID: 3692
		private bool allowScriptChange = true;

		// Token: 0x04000E6D RID: 3693
		private bool fixedPitchOnly;

		// Token: 0x04000E6E RID: 3694
		private int maxSize;

		// Token: 0x04000E6F RID: 3695
		private int minSize;

		// Token: 0x04000E70 RID: 3696
		private bool scriptsOnly;

		// Token: 0x04000E71 RID: 3697
		private bool showApply;

		// Token: 0x04000E72 RID: 3698
		private bool showColor;

		// Token: 0x04000E73 RID: 3699
		private bool showEffects = true;

		// Token: 0x04000E74 RID: 3700
		private bool showHelp;

		// Token: 0x04000E75 RID: 3701
		private bool fontMustExist;

		// Token: 0x04000E76 RID: 3702
		private Panel examplePanel;

		// Token: 0x04000E77 RID: 3703
		private Button okButton;

		// Token: 0x04000E78 RID: 3704
		private Button cancelButton;

		// Token: 0x04000E79 RID: 3705
		private Button applyButton;

		// Token: 0x04000E7A RID: 3706
		private Button helpButton;

		// Token: 0x04000E7B RID: 3707
		private TextBox fontTextBox;

		// Token: 0x04000E7C RID: 3708
		private TextBox fontstyleTextBox;

		// Token: 0x04000E7D RID: 3709
		private TextBox fontsizeTextBox;

		// Token: 0x04000E7E RID: 3710
		private MouseWheelListBox fontListBox;

		// Token: 0x04000E7F RID: 3711
		private MouseWheelListBox fontstyleListBox;

		// Token: 0x04000E80 RID: 3712
		private MouseWheelListBox fontsizeListBox;

		// Token: 0x04000E81 RID: 3713
		private GroupBox effectsGroupBox;

		// Token: 0x04000E82 RID: 3714
		private CheckBox strikethroughCheckBox;

		// Token: 0x04000E83 RID: 3715
		private CheckBox underlinedCheckBox;

		// Token: 0x04000E84 RID: 3716
		private ComboBox scriptComboBox;

		// Token: 0x04000E85 RID: 3717
		private Label fontLabel;

		// Token: 0x04000E86 RID: 3718
		private Label fontstyleLabel;

		// Token: 0x04000E87 RID: 3719
		private Label sizeLabel;

		// Token: 0x04000E88 RID: 3720
		private Label scriptLabel;

		// Token: 0x04000E89 RID: 3721
		private GroupBox exampleGroupBox;

		// Token: 0x04000E8A RID: 3722
		private FontDialog.ColorComboBox colorComboBox;

		// Token: 0x04000E8B RID: 3723
		private string currentFontName;

		// Token: 0x04000E8C RID: 3724
		private float currentSize;

		// Token: 0x04000E8D RID: 3725
		private FontFamily currentFamily;

		// Token: 0x04000E8E RID: 3726
		private FontStyle currentFontStyle;

		// Token: 0x04000E8F RID: 3727
		private bool underlined;

		// Token: 0x04000E90 RID: 3728
		private bool strikethrough;

		// Token: 0x04000E91 RID: 3729
		private Hashtable fontHash = new Hashtable();

		// Token: 0x04000E92 RID: 3730
		private int[] a_sizes = new int[]
		{
			6, 7, 8, 9, 10, 11, 12, 14, 16, 18,
			20, 22, 24, 26, 28, 36, 48, 72
		};

		// Token: 0x04000E93 RID: 3731
		private string[] char_sets_names = new string[]
		{
			"Western", "Symbol", "Shift Jis", "Hangul", "GB2312", "BIG5", "Greek", "Turkish", "Hebrew", "Arabic",
			"Baltic", "Vietname", "Cyrillic", "East European", "Thai", "Johab", "Mac", "OEM", "VISCII", "TCVN",
			"KOI-8", "ISO-8859-3", "ISO-8859-4", "ISO-8859-10", "Celtic"
		};

		// Token: 0x04000E94 RID: 3732
		private string[] char_sets = new string[]
		{
			"AaBbYyZz",
			"Symbol",
			string.Concat(new object[] { "Aa", 'あ', 'ぁ', 'ア', 'ァ', '亜', '宇' }),
			135036 + "AaBYyZz",
			new string(new char[] { '微', '软', '中', '文', '软', '件' }),
			new string(new char[] { '中', '文', '字', '型', '範', '例' }),
			string.Concat(new object[] { "AaBb", 'Α', 'α', 'Β', 'β' }),
			string.Concat(new object[] { "AaBb", 'Ğ', 'ğ', 'Ş', 'ş' }),
			string.Concat(new object[] { "AaBb", 'נ', 'ס', 'ש', 'ת' }),
			string.Concat(new object[] { "AaBb", 'ا', 'ب', 'ج', 'د', 'ه', 'و', 'ز' }),
			"AaBbYyZz",
			string.Concat(new object[] { "AaBb", 'Ơ', 'ơ', 'Ư', 'ư' }),
			string.Concat(new object[] { "AaBb", 'Б', 'б', 'Ф', 'ф' }),
			string.Concat(new object[] { "AaBb", 'Á', 'á', 'Ô', 'ô' }),
			string.Concat(new object[] { "AaBb", 'อ', '\u0e31', 'ก', 'ษ', 'ร', 'ไ', 'ท', 'ย' }),
			135036 + "AaBYyZz",
			"AaBbYyZz",
			string.Concat(new object[] { "AaBb", 'ø', 'ñ', 'ý' }),
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04000E95 RID: 3733
		private string example_panel_text;

		// Token: 0x04000E96 RID: 3734
		private bool internal_change;

		// Token: 0x04000E97 RID: 3735
		private bool internal_textbox_change;

		// Token: 0x02000193 RID: 403
		internal class ColorComboBox : ComboBox
		{
			// Token: 0x060019D7 RID: 6615 RVA: 0x00064750 File Offset: 0x00062950
			public ColorComboBox(FontDialog fontDialog)
			{
				this.fontDialog = fontDialog;
				base.DropDownStyle = ComboBoxStyle.DropDownList;
				base.DrawMode = DrawMode.OwnerDrawFixed;
				base.Items.AddRange(new object[]
				{
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Black, "Black"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.DarkRed, "Dark-Red"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Green, "Green"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Olive, "Olive-Green"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Aquamarine, "Aquamarine"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Crimson, "Crimson"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Cyan, "Cyan"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Gray, "Gray"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Silver, "Silver"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Red, "Red"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.YellowGreen, "Yellow-Green"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Yellow, "Yellow"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Blue, "Blue"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Purple, "Purple"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.Aquamarine, "Aquamarine"),
					new FontDialog.ColorComboBox.ColorComboBoxItem(Color.White, "White")
				});
				this.SelectedIndex = 0;
			}

			// Token: 0x060019D8 RID: 6616 RVA: 0x000648B8 File Offset: 0x00062AB8
			protected override void OnDrawItem(DrawItemEventArgs e)
			{
				if (e.Index == -1)
				{
					return;
				}
				FontDialog.ColorComboBox.ColorComboBoxItem colorComboBoxItem = base.Items[e.Index] as FontDialog.ColorComboBox.ColorComboBoxItem;
				Rectangle bounds = e.Bounds;
				bounds.X += 24;
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(Color.Blue), e.Bounds);
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(colorComboBoxItem.Color), e.Bounds.X + 3, e.Bounds.Y + 3, e.Bounds.X + 16, e.Bounds.Bottom - 3);
					e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.Black), e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.X + 17, e.Bounds.Bottom - 3);
					e.Graphics.DrawString(colorComboBoxItem.Name, this.Font, ThemeEngine.Current.ResPool.GetSolidBrush(Color.White), bounds);
				}
				else
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(Color.White), e.Bounds);
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(colorComboBoxItem.Color), e.Bounds.X + 3, e.Bounds.Y + 3, e.Bounds.X + 16, e.Bounds.Bottom - 3);
					e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.Black), e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.X + 17, e.Bounds.Bottom - 3);
					e.Graphics.DrawString(colorComboBoxItem.Name, this.Font, ThemeEngine.Current.ResPool.GetSolidBrush(Color.Black), bounds);
				}
			}

			// Token: 0x060019D9 RID: 6617 RVA: 0x00064B50 File Offset: 0x00062D50
			protected override void OnSelectedIndexChanged(EventArgs e)
			{
				FontDialog.ColorComboBox.ColorComboBoxItem colorComboBoxItem = base.Items[this.SelectedIndex] as FontDialog.ColorComboBox.ColorComboBoxItem;
				this.selectedColor = colorComboBoxItem.Color;
				this.fontDialog.Color = this.selectedColor;
			}

			// Token: 0x04000E99 RID: 3737
			private Color selectedColor;

			// Token: 0x04000E9A RID: 3738
			private FontDialog fontDialog;

			// Token: 0x02000194 RID: 404
			internal class ColorComboBoxItem
			{
				// Token: 0x060019DA RID: 6618 RVA: 0x00064B94 File Offset: 0x00062D94
				public ColorComboBoxItem(Color color, string name)
				{
					this.color = color;
					this.name = name;
				}

				// Token: 0x17000625 RID: 1573
				// (get) Token: 0x060019DC RID: 6620 RVA: 0x00064BB8 File Offset: 0x00062DB8
				// (set) Token: 0x060019DB RID: 6619 RVA: 0x00064BAC File Offset: 0x00062DAC
				public Color Color
				{
					get
					{
						return this.color;
					}
					set
					{
						this.color = value;
					}
				}

				// Token: 0x17000626 RID: 1574
				// (get) Token: 0x060019DE RID: 6622 RVA: 0x00064BCC File Offset: 0x00062DCC
				// (set) Token: 0x060019DD RID: 6621 RVA: 0x00064BC0 File Offset: 0x00062DC0
				public string Name
				{
					get
					{
						return this.name;
					}
					set
					{
						this.name = value;
					}
				}

				// Token: 0x060019DF RID: 6623 RVA: 0x00064BD4 File Offset: 0x00062DD4
				public override string ToString()
				{
					return this.Name;
				}

				// Token: 0x04000E9B RID: 3739
				private Color color;

				// Token: 0x04000E9C RID: 3740
				private string name;
			}
		}
	}
}
