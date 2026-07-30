using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality common to button controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006A RID: 106
	[ClassInterface(1)]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	public abstract class ButtonBase : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase" /> class.</summary>
		// Token: 0x060004A2 RID: 1186 RVA: 0x00015AD4 File Offset: 0x00013CD4
		protected ButtonBase()
		{
			this.flat_style = FlatStyle.Standard;
			this.flat_button_appearance = new FlatButtonAppearance(this);
			this.image_key = string.Empty;
			this.text_image_relation = TextImageRelation.Overlay;
			this.use_mnemonic = true;
			this.use_visual_style_back_color = true;
			this.image_index = -1;
			this.image = null;
			this.image_list = null;
			this.image_alignment = 32;
			this.ImeMode = ImeMode.Disable;
			this.text_alignment = 32;
			this.is_default = false;
			this.is_pressed = false;
			this.text_format = new StringFormat();
			this.text_format.Alignment = 1;
			this.text_format.LineAlignment = 1;
			this.text_format.HotkeyPrefix = 1;
			this.text_format.FormatFlags |= 8192;
			this.text_format_flags = TextFormatFlags.HorizontalCenter;
			this.text_format_flags |= TextFormatFlags.VerticalCenter;
			this.text_format_flags |= TextFormatFlags.TextBoxControl;
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.CacheText | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.StandardClick, false);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ButtonBase.AutoSize" /> property changes.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060004A3 RID: 1187 RVA: 0x00015BDC File Offset: 0x00013DDC
		// (remove) Token: 0x060004A4 RID: 1188 RVA: 0x00015BE8 File Offset: 0x00013DE8
		[Browsable(true)]
		[EditorBrowsable(0)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ButtonBase.ImeMode" /> property is changed. This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060004A5 RID: 1189 RVA: 0x00015BF4 File Offset: 0x00013DF4
		// (remove) Token: 0x060004A6 RID: 1190 RVA: 0x00015C00 File Offset: 0x00013E00
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

		/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the control, denoting that the control text extends beyond the specified length of the control.</summary>
		/// <returns>true if the additional label text is to be indicated by an ellipsis; otherwise, false. The default is true.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00015C0C File Offset: 0x00013E0C
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00015C14 File Offset: 0x00013E14
		[Browsable(true)]
		[EditorBrowsable(0)]
		[MWFCategory("Behavior")]
		[DefaultValue(false)]
		public bool AutoEllipsis
		{
			get
			{
				return this.auto_ellipsis;
			}
			set
			{
				if (this.auto_ellipsis != value)
				{
					this.auto_ellipsis = value;
					if (this.auto_ellipsis)
					{
						this.text_format_flags |= TextFormatFlags.EndEllipsis;
						this.text_format_flags &= ~TextFormatFlags.WordBreak;
					}
					else
					{
						this.text_format_flags &= ~TextFormatFlags.EndEllipsis;
						this.text_format_flags |= TextFormatFlags.WordBreak;
					}
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "AutoEllipsis");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
		/// <returns>true if the control automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00015CA8 File Offset: 0x00013EA8
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00015CB0 File Offset: 0x00013EB0
		[EditorBrowsable(0)]
		[MWFCategory("Layout")]
		[Browsable(true)]
		[DesignerSerializationVisibility(1)]
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

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00015CBC File Offset: 0x00013EBC
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00015CC4 File Offset: 0x00013EC4
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

		/// <summary>Gets the appearance of the border and the colors used to indicate check state and mouse state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatButtonAppearance" /> values.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00015CD0 File Offset: 0x00013ED0
		[DesignerSerializationVisibility(2)]
		[MWFCategory("Appearance")]
		[Browsable(true)]
		public FlatButtonAppearance FlatAppearance
		{
			get
			{
				return this.flat_button_appearance;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the button control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00015CD8 File Offset: 0x00013ED8
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00015CE0 File Offset: 0x00013EE0
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue(FlatStyle.Standard)]
		[MWFDescription("Determines look of button")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
			set
			{
				if (this.flat_style != value)
				{
					this.flat_style = value;
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "FlatStyle");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a button control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed on the button control. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00015D18 File Offset: 0x00013F18
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00015D98 File Offset: 0x00013F98
		[Localizable(true)]
		[MWFDescription("Sets image to be displayed on button face")]
		[MWFCategory("Appearance")]
		public Image Image
		{
			get
			{
				if (this.image != null)
				{
					return this.image;
				}
				if (this.image_index >= 0 && this.image_list != null)
				{
					return this.image_list.Images[this.image_index];
				}
				if (!string.IsNullOrEmpty(this.image_key) && this.image_list != null)
				{
					return this.image_list.Images[this.image_key];
				}
				return null;
			}
			set
			{
				if (this.image != value)
				{
					this.image = value;
					this.image_index = -1;
					this.image_key = string.Empty;
					this.image_list = null;
					if (this.AutoSize && base.Parent != null)
					{
						base.Parent.PerformLayout(this, "Image");
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015E00 File Offset: 0x00014000
		internal bool ShouldSerializeImage()
		{
			return this.Image != null;
		}

		/// <summary>Gets or sets the alignment of the image on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00015E10 File Offset: 0x00014010
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00015E18 File Offset: 0x00014018
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue(32)]
		[MWFDescription("Sets the alignment of the image to be displayed on button face")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.image_alignment;
			}
			set
			{
				if (this.image_alignment != value)
				{
					this.image_alignment = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed on the button control.</summary>
		/// <returns>A zero-based index, which represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the lower bounds of the <see cref="P:System.Windows.Forms.ButtonBase.ImageIndex" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00015E34 File Offset: 0x00014034
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00015E4C File Offset: 0x0001404C
		[TypeConverter(typeof(ImageIndexConverter))]
		[DefaultValue(-1)]
		[Localizable(true)]
		[RefreshProperties(2)]
		[MWFCategory("Appearance")]
		[MWFDescription("Index of image to display, if ImageList is used for button face images")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public int ImageIndex
		{
			get
			{
				if (this.image_list == null)
				{
					return -1;
				}
				return this.image_index;
			}
			set
			{
				if (this.image_index != value)
				{
					this.image_index = value;
					this.image = null;
					this.image_key = string.Empty;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ButtonBase.ImageList" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00015E7C File Offset: 0x0001407C
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00015E84 File Offset: 0x00014084
		[DefaultValue("")]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[RefreshProperties(2)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				if (this.image_key != value)
				{
					this.image = null;
					this.image_index = -1;
					this.image_key = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> displayed on a button control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" />. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00015EC0 File Offset: 0x000140C0
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00015EC8 File Offset: 0x000140C8
		[MWFCategory("Appearance")]
		[RefreshProperties(2)]
		[MWFDescription("ImageList used for ImageIndex")]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				if (this.image_list != value)
				{
					this.image_list = value;
					if (value != null && this.image != null)
					{
						this.image = null;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control. This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00015EFC File Offset: 0x000140FC
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00015F04 File Offset: 0x00014104
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

		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00015F10 File Offset: 0x00014110
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00015F18 File Offset: 0x00014118
		[SettingsBindable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets or sets the alignment of the text on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00015F24 File Offset: 0x00014124
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00015F2C File Offset: 0x0001412C
		[MWFCategory("Appearance")]
		[DefaultValue(32)]
		[Localizable(true)]
		[MWFDescription("Alignment for button text")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				if (this.text_alignment != value)
				{
					this.text_alignment = value;
					this.text_format_flags &= ~TextFormatFlags.Bottom;
					this.text_format_flags &= (TextFormatFlags)(-1);
					this.text_format_flags &= (TextFormatFlags)(-1);
					this.text_format_flags &= ~TextFormatFlags.Right;
					this.text_format_flags &= ~TextFormatFlags.HorizontalCenter;
					this.text_format_flags &= ~TextFormatFlags.VerticalCenter;
					ContentAlignment contentAlignment = this.text_alignment;
					switch (contentAlignment)
					{
					case 1:
						this.text_format.Alignment = 0;
						this.text_format.LineAlignment = 0;
						break;
					case 2:
						this.text_format.Alignment = 1;
						this.text_format.LineAlignment = 0;
						this.text_format_flags |= TextFormatFlags.HorizontalCenter;
						break;
					default:
						if (contentAlignment != 16)
						{
							if (contentAlignment != 32)
							{
								if (contentAlignment != 64)
								{
									if (contentAlignment != 256)
									{
										if (contentAlignment != 512)
										{
											if (contentAlignment == 1024)
											{
												this.text_format.Alignment = 2;
												this.text_format.LineAlignment = 2;
												this.text_format_flags |= TextFormatFlags.Right | TextFormatFlags.Bottom;
											}
										}
										else
										{
											this.text_format.Alignment = 1;
											this.text_format.LineAlignment = 2;
											this.text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
										}
									}
									else
									{
										this.text_format.Alignment = 0;
										this.text_format.LineAlignment = 2;
										this.text_format_flags |= TextFormatFlags.Bottom;
									}
								}
								else
								{
									this.text_format.Alignment = 2;
									this.text_format.LineAlignment = 1;
									this.text_format_flags |= TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
								}
							}
							else
							{
								this.text_format.Alignment = 1;
								this.text_format.LineAlignment = 1;
								this.text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
							}
						}
						else
						{
							this.text_format.Alignment = 0;
							this.text_format.LineAlignment = 1;
							this.text_format_flags |= TextFormatFlags.VerticalCenter;
						}
						break;
					case 4:
						this.text_format.Alignment = 2;
						this.text_format.LineAlignment = 0;
						this.text_format_flags |= TextFormatFlags.Right;
						break;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the position of text and image relative to each other.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.TextImageRelation" />. The default is <see cref="F:System.Windows.Forms.TextImageRelation.Overlay" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00016180 File Offset: 0x00014380
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00016188 File Offset: 0x00014388
		[MWFCategory("Appearance")]
		[DefaultValue(TextImageRelation.Overlay)]
		[Localizable(true)]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.text_image_relation;
			}
			set
			{
				if (!Enum.IsDefined(typeof(TextImageRelation), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextImageRelation", value));
				}
				if (this.text_image_relation != value)
				{
					this.text_image_relation = value;
					if (this.AutoSize && base.Parent != null)
					{
						base.Parent.PerformLayout(this, "TextImageRelation");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016208 File Offset: 0x00014408
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00016210 File Offset: 0x00014410
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
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

		/// <summary>Gets or sets a value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control.</summary>
		/// <returns>true if the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control; otherwise, false. The default is true.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016248 File Offset: 0x00014448
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00016250 File Offset: 0x00014450
		[MWFCategory("Appearance")]
		[DefaultValue(true)]
		public bool UseMnemonic
		{
			get
			{
				return this.use_mnemonic;
			}
			set
			{
				if (this.use_mnemonic != value)
				{
					this.use_mnemonic = value;
					if (this.use_mnemonic)
					{
						this.text_format_flags &= ~TextFormatFlags.NoPrefix;
					}
					else
					{
						this.text_format_flags |= TextFormatFlags.NoPrefix;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that determines if the background is drawn using visual styles, if supported.</summary>
		/// <returns>true if the background is drawn using visual styles; otherwise, false.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000162AC File Offset: 0x000144AC
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000162B4 File Offset: 0x000144B4
		[MWFCategory("Appearance")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return this.use_visual_style_back_color;
			}
			set
			{
				if (this.use_visual_style_back_color != value)
				{
					this.use_visual_style_back_color = value;
					base.Invalidate();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000162D0 File Offset: 0x000144D0
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000162D8 File Offset: 0x000144D8
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000162DC File Offset: 0x000144DC
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ButtonBaseDefaultSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the button control is the default button.</summary>
		/// <returns>true if the button control is the default button; otherwise, false.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000162E8 File Offset: 0x000144E8
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x000162F0 File Offset: 0x000144F0
		protected internal bool IsDefault
		{
			get
			{
				return this.is_default;
			}
			set
			{
				if (this.is_default != value)
				{
					this.is_default = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		// Token: 0x060004CE RID: 1230 RVA: 0x0001630C File Offset: 0x0001450C
		public override Size GetPreferredSize(Size proposedSize)
		{
			return base.GetPreferredSize(proposedSize);
		}

		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060004CF RID: 1231 RVA: 0x00016318 File Offset: 0x00014518
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ButtonBase.ButtonBaseAccessibleObject(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00016320 File Offset: 0x00014520
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001632C File Offset: 0x0001452C
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00016338 File Offset: 0x00014538
		protected override void OnGotFocus(EventArgs e)
		{
			base.Invalidate();
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00016348 File Offset: 0x00014548
		protected override void OnKeyDown(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				this.is_pressed = true;
				base.Invalidate();
				kevent.Handled = true;
			}
			base.OnKeyDown(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00016380 File Offset: 0x00014580
		protected override void OnKeyUp(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				this.is_pressed = false;
				base.Invalidate();
				this.OnClick(EventArgs.Empty);
				kevent.Handled = true;
			}
			base.OnKeyUp(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnLostFocus(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004D5 RID: 1237 RVA: 0x000163C0 File Offset: 0x000145C0
		protected override void OnLostFocus(EventArgs e)
		{
			base.Invalidate();
			base.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060004D6 RID: 1238 RVA: 0x000163D0 File Offset: 0x000145D0
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if ((mevent.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.is_pressed = true;
				base.Invalidate();
			}
			base.OnMouseDown(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseEnter(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000163F8 File Offset: 0x000145F8
		protected override void OnMouseEnter(EventArgs eventargs)
		{
			this.is_entered = true;
			base.Invalidate();
			base.OnMouseEnter(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00016410 File Offset: 0x00014610
		protected override void OnMouseLeave(EventArgs eventargs)
		{
			this.is_entered = false;
			base.Invalidate();
			base.OnMouseLeave(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060004D9 RID: 1241 RVA: 0x00016428 File Offset: 0x00014628
		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			bool flag = false;
			bool flag2 = false;
			if (base.ClientRectangle.Contains(mevent.Location))
			{
				flag = true;
			}
			if ((mevent.Button & MouseButtons.Left) != MouseButtons.None && base.Capture && flag != this.is_pressed)
			{
				this.is_pressed = flag;
				flag2 = true;
			}
			if (this.is_entered != flag)
			{
				this.is_entered = flag;
				flag2 = true;
			}
			if (flag2)
			{
				base.Invalidate();
			}
			base.OnMouseMove(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060004DA RID: 1242 RVA: 0x000164B0 File Offset: 0x000146B0
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (base.Capture && (mevent.Button & MouseButtons.Left) != MouseButtons.None)
			{
				base.Capture = false;
				if (this.is_pressed)
				{
					this.is_pressed = false;
					base.Invalidate();
				}
				else if (this.flat_style == FlatStyle.Flat || this.flat_style == FlatStyle.Popup)
				{
					base.Invalidate();
				}
				if (base.ClientRectangle.Contains(mevent.Location) && !base.ValidationFailed)
				{
					this.OnClick(EventArgs.Empty);
					this.OnMouseClick(mevent);
				}
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060004DB RID: 1243 RVA: 0x00016558 File Offset: 0x00014758
		protected override void OnPaint(PaintEventArgs pevent)
		{
			this.Draw(pevent);
			base.OnPaint(pevent);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004DC RID: 1244 RVA: 0x00016568 File Offset: 0x00014768
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004DD RID: 1245 RVA: 0x00016574 File Offset: 0x00014774
		protected override void OnTextChanged(EventArgs e)
		{
			base.Invalidate();
			base.OnTextChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060004DE RID: 1246 RVA: 0x00016584 File Offset: 0x00014784
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!base.Visible)
			{
				this.is_pressed = false;
				this.is_entered = false;
			}
			base.OnVisibleChanged(e);
		}

		/// <summary>Resets the <see cref="T:System.Windows.Forms.Button" /> control to the state before it is pressed and redraws it.</summary>
		// Token: 0x060004DF RID: 1247 RVA: 0x000165B4 File Offset: 0x000147B4
		protected void ResetFlagsandPaint()
		{
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060004E0 RID: 1248 RVA: 0x000165B8 File Offset: 0x000147B8
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_LBUTTONDBLCLK:
				this.HaveDoubleClick();
				break;
			default:
				if (msg == Msg.WM_MBUTTONDBLCLK)
				{
					this.HaveDoubleClick();
				}
				break;
			case Msg.WM_RBUTTONDBLCLK:
				this.HaveDoubleClick();
				break;
			}
			base.WndProc(ref m);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00016620 File Offset: 0x00014820
		internal ButtonState ButtonState
		{
			get
			{
				ButtonState buttonState = ButtonState.Normal;
				if (base.Enabled)
				{
					if (this.is_entered)
					{
						if (this.flat_style == FlatStyle.Flat)
						{
							buttonState |= ButtonState.Flat;
						}
					}
					else if (this.flat_style == FlatStyle.Flat || this.flat_style == FlatStyle.Popup)
					{
						buttonState |= ButtonState.Flat;
					}
					if (this.is_entered && this.is_pressed)
					{
						buttonState |= ButtonState.Pushed;
					}
				}
				else
				{
					buttonState |= ButtonState.Inactive;
					if (this.flat_style == FlatStyle.Flat || this.flat_style == FlatStyle.Popup)
					{
						buttonState |= ButtonState.Flat;
					}
				}
				return buttonState;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000166C8 File Offset: 0x000148C8
		internal bool Pressed
		{
			get
			{
				return this.is_pressed;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000166D0 File Offset: 0x000148D0
		internal TextFormatFlags TextFormatFlags
		{
			get
			{
				return this.text_format_flags;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000166D8 File Offset: 0x000148D8
		internal virtual void Draw(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawButtonBase(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000166FC File Offset: 0x000148FC
		internal virtual void HaveDoubleClick()
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016700 File Offset: 0x00014900
		internal override void OnPaintBackgroundInternal(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		// Token: 0x0400068A RID: 1674
		private FlatStyle flat_style;

		// Token: 0x0400068B RID: 1675
		private int image_index;

		// Token: 0x0400068C RID: 1676
		internal Image image;

		// Token: 0x0400068D RID: 1677
		internal ImageList image_list;

		// Token: 0x0400068E RID: 1678
		private ContentAlignment image_alignment;

		// Token: 0x0400068F RID: 1679
		internal ContentAlignment text_alignment;

		// Token: 0x04000690 RID: 1680
		private bool is_default;

		// Token: 0x04000691 RID: 1681
		internal bool is_pressed;

		// Token: 0x04000692 RID: 1682
		internal StringFormat text_format;

		// Token: 0x04000693 RID: 1683
		internal bool paint_as_acceptbutton;

		// Token: 0x04000694 RID: 1684
		private bool auto_ellipsis;

		// Token: 0x04000695 RID: 1685
		private FlatButtonAppearance flat_button_appearance;

		// Token: 0x04000696 RID: 1686
		private string image_key;

		// Token: 0x04000697 RID: 1687
		private TextImageRelation text_image_relation;

		// Token: 0x04000698 RID: 1688
		private TextFormatFlags text_format_flags;

		// Token: 0x04000699 RID: 1689
		private bool use_mnemonic;

		// Token: 0x0400069A RID: 1690
		private bool use_visual_style_back_color;

		/// <summary>Provides information that accessibility applications use to adjust an application's user interface for users with disabilities.</summary>
		// Token: 0x0200006B RID: 107
		[ComVisible(true)]
		public class ButtonBaseAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" /> class. </summary>
			/// <param name="owner">The owner of this <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" />.</param>
			// Token: 0x060004E7 RID: 1255 RVA: 0x0001670C File Offset: 0x0001490C
			public ButtonBaseAccessibleObject(Control owner)
				: base(owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
				this.default_action = "Press";
				this.role = AccessibleRole.PushButton;
			}

			/// <summary>Gets the state of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values.</returns>
			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001674C File Offset: 0x0001494C
			public override AccessibleStates State
			{
				get
				{
					return base.State;
				}
			}

			/// <summary>Performs the default action associated with this accessible object.</summary>
			// Token: 0x060004E9 RID: 1257 RVA: 0x00016754 File Offset: 0x00014954
			public override void DoDefaultAction()
			{
				((ButtonBase)this.owner).OnClick(EventArgs.Empty);
			}

			// Token: 0x0400069B RID: 1691
			private new Control owner;
		}
	}
}
