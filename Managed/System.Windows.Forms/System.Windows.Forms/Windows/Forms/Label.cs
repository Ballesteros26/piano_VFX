using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows label. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001FC RID: 508
	[ClassInterface(1)]
	[DefaultBindingProperty("Text")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.LabelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	public class Label : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Label" /> class.</summary>
		// Token: 0x06001F25 RID: 7973 RVA: 0x000753D0 File Offset: 0x000735D0
		public Label()
		{
			this.autosize = false;
			this.TabStop = false;
			this.string_format = new StringFormat();
			this.string_format.FormatFlags = 8192;
			this.TextAlign = 1;
			this.image = null;
			this.UseMnemonic = true;
			this.image_list = null;
			this.image_align = 32;
			this.SetUseMnemonic(this.UseMnemonic);
			this.flat_style = FlatStyle.Standard;
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.HandleCreated += new EventHandler(this.OnHandleCreatedLB);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00075480 File Offset: 0x00073680
		// Note: this type is marked as 'beforefieldinit'.
		static Label()
		{
			Label.AutoSizeChangedEvent = new object();
			Label.TextAlignChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.AutoSize" /> property changes.</summary>
		// Token: 0x140001EC RID: 492
		// (add) Token: 0x06001F27 RID: 7975 RVA: 0x000754B8 File Offset: 0x000736B8
		// (remove) Token: 0x06001F28 RID: 7976 RVA: 0x000754CC File Offset: 0x000736CC
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.Events.AddHandler(Label.AutoSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Label.AutoSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImage" /> property changes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001ED RID: 493
		// (add) Token: 0x06001F29 RID: 7977 RVA: 0x000754E0 File Offset: 0x000736E0
		// (remove) Token: 0x06001F2A RID: 7978 RVA: 0x000754EC File Offset: 0x000736EC
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001EE RID: 494
		// (add) Token: 0x06001F2B RID: 7979 RVA: 0x000754F8 File Offset: 0x000736F8
		// (remove) Token: 0x06001F2C RID: 7980 RVA: 0x00075504 File Offset: 0x00073704
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001EF RID: 495
		// (add) Token: 0x06001F2D RID: 7981 RVA: 0x00075510 File Offset: 0x00073710
		// (remove) Token: 0x06001F2E RID: 7982 RVA: 0x0007551C File Offset: 0x0007371C
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

		/// <summary>Occurs when the user presses a key while the label has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F0 RID: 496
		// (add) Token: 0x06001F2F RID: 7983 RVA: 0x00075528 File Offset: 0x00073728
		// (remove) Token: 0x06001F30 RID: 7984 RVA: 0x00075534 File Offset: 0x00073734
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the user presses a key while the label has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F1 RID: 497
		// (add) Token: 0x06001F31 RID: 7985 RVA: 0x00075540 File Offset: 0x00073740
		// (remove) Token: 0x06001F32 RID: 7986 RVA: 0x0007554C File Offset: 0x0007374C
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user releases a key while the label has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F2 RID: 498
		// (add) Token: 0x06001F33 RID: 7987 RVA: 0x00075558 File Offset: 0x00073758
		// (remove) Token: 0x06001F34 RID: 7988 RVA: 0x00075564 File Offset: 0x00073764
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F3 RID: 499
		// (add) Token: 0x06001F35 RID: 7989 RVA: 0x00075570 File Offset: 0x00073770
		// (remove) Token: 0x06001F36 RID: 7990 RVA: 0x0007557C File Offset: 0x0007377C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TextAlign" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F4 RID: 500
		// (add) Token: 0x06001F37 RID: 7991 RVA: 0x00075588 File Offset: 0x00073788
		// (remove) Token: 0x06001F38 RID: 7992 RVA: 0x0007559C File Offset: 0x0007379C
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(Label.TextAlignChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Label.TextAlignChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the <see cref="T:System.Windows.Forms.Label" />, denoting that the <see cref="T:System.Windows.Forms.Label" /> text extends beyond the specified length of the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>true if the additional label text is to be indicated by an ellipsis; otherwise, false. The default is false.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000755B0 File Offset: 0x000737B0
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x000755B8 File Offset: 0x000737B8
		[EditorBrowsable(0)]
		[Browsable(true)]
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
						this.string_format.Trimming = 3;
					}
					else
					{
						this.string_format.Trimming = 1;
					}
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "AutoEllipsis");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
		/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false.Note:When added to a form using the designer, the default value is true. When instantiated from code, the default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00075624 File Offset: 0x00073824
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x0007562C File Offset: 0x0007382C
		[DesignerSerializationVisibility(1)]
		[DefaultValue(false)]
		[RefreshProperties(1)]
		[Localizable(true)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public override bool AutoSize
		{
			get
			{
				return this.autosize;
			}
			set
			{
				if (this.autosize == value)
				{
					return;
				}
				base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				base.AutoSize = value;
				this.autosize = value;
				this.CalcAutoSize();
				base.Invalidate();
				this.OnAutoSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the image rendered on the background of the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the background image of the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00075674 File Offset: 0x00073874
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x0007567C File Offset: 0x0007387C
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
				base.Invalidate();
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0007568C File Offset: 0x0007388C
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x00075694 File Offset: 0x00073894
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

		/// <summary>Gets or sets the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000756A0 File Offset: 0x000738A0
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x000756A8 File Offset: 0x000738A8
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x000756B4 File Offset: 0x000738B4
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.BorderStyle != BorderStyle.Fixed3D)
				{
					return createParams;
				}
				createParams.ExStyle &= -513;
				createParams.ExStyle |= 131072;
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> supported by this control. The default is <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x000756FC File Offset: 0x000738FC
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value that represents the default space between controls.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00075700 File Offset: 0x00073900
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(3, 0, 3, 0);
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0007570C File Offset: 0x0007390C
		protected override Size DefaultSize
		{
			get
			{
				return ThemeElements.LabelPainter.DefaultSize;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the label control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00075718 File Offset: 0x00073918
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00075720 File Offset: 0x00073920
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for FlatStyle", value));
				}
				if (this.flat_style == value)
				{
					return;
				}
				this.flat_style = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "FlatStyle");
				}
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> displayed on the <see cref="T:System.Windows.Forms.Label" />. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00075794 File Offset: 0x00073994
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00075814 File Offset: 0x00073A14
		[Localizable(true)]
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

		/// <summary>Gets or sets the alignment of an image that is displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is ContentAlignment.MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0007587C File Offset: 0x00073A7C
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00075884 File Offset: 0x00073A84
		[DefaultValue(32)]
		[Localizable(true)]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.image_align;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
				}
				if (this.image_align == value)
				{
					return;
				}
				this.image_align = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the index value of the image displayed on the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>A zero-based index that represents the position in the <see cref="T:System.Windows.Forms.ImageList" /> control (assigned to the <see cref="P:System.Windows.Forms.Label.ImageList" /> property) where the image is located. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the lower bounds of the <see cref="P:System.Windows.Forms.Label.ImageIndex" /> property. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000758DC File Offset: 0x00073ADC
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0007592C File Offset: 0x00073B2C
		[TypeConverter(typeof(ImageIndexConverter))]
		[RefreshProperties(2)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				if (this.ImageList == null)
				{
					return -1;
				}
				if (this.image_index >= this.image_list.Images.Count)
				{
					return this.image_list.Images.Count - 1;
				}
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException();
				}
				if (this.image_index != value)
				{
					this.image_index = value;
					this.image = null;
					this.image_key = string.Empty;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.Label.ImageList" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x00075974 File Offset: 0x00073B74
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0007597C File Offset: 0x00073B7C
		[RefreshProperties(2)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
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

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the images to display in the <see cref="T:System.Windows.Forms.Label" /> control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that stores the collection of <see cref="T:System.Drawing.Image" /> objects. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x000759B8 File Offset: 0x00073BB8
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x000759C0 File Offset: 0x00073BC0
		[DefaultValue(null)]
		[RefreshProperties(2)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				if (this.image_list == value)
				{
					return;
				}
				this.image_list = value;
				if (this.image_list != null && this.image_index != -1)
				{
					this.Image = null;
				}
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not within the range of valid values specified in the enumeration. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x00075A08 File Offset: 0x00073C08
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x00075A10 File Offset: 0x00073C10
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

		// Token: 0x06001F55 RID: 8021 RVA: 0x00075A1C File Offset: 0x00073C1C
		internal virtual Size InternalGetPreferredSize(Size proposed)
		{
			Size size;
			if (this.Text == string.Empty)
			{
				size..ctor(0, this.Font.Height);
			}
			else
			{
				size = Size.Ceiling(TextRenderer.MeasureString(this.Text, this.Font, Label.req_witdthsize, this.string_format));
				size.Width += 3;
			}
			size.Width += base.Padding.Horizontal;
			size.Height += base.Padding.Vertical;
			if (!this.use_compatible_text_rendering)
			{
				return size;
			}
			if (this.border_style == BorderStyle.None)
			{
				size.Height += 3;
			}
			else
			{
				size.Height += 6;
			}
			return size;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted. </summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control. </param>
		// Token: 0x06001F56 RID: 8022 RVA: 0x00075AFC File Offset: 0x00073CFC
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.InternalGetPreferredSize(proposedSize);
		}

		/// <summary>Gets the preferred height of the control.</summary>
		/// <returns>The height of the control (in pixels), assuming a single line of text is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x00075B08 File Offset: 0x00073D08
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual int PreferredHeight
		{
			get
			{
				return this.InternalGetPreferredSize(Size.Empty).Height;
			}
		}

		/// <summary>Gets the preferred width of the control.</summary>
		/// <returns>The width of the control (in pixels), assuming a single line of text is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x00075B28 File Offset: 0x00073D28
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public virtual int PreferredWidth
		{
			get
			{
				return this.InternalGetPreferredSize(Size.Empty).Width;
			}
		}

		/// <summary>Indicates whether the container control background is rendered on the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>true if the background of the <see cref="T:System.Windows.Forms.Label" /> control's container is rendered on the <see cref="T:System.Windows.Forms.Label" />; otherwise, false. The default is false.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x00075B48 File Offset: 0x00073D48
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x00075B50 File Offset: 0x00073D50
		[Obsolete("This property has been deprecated.  Use BackColor instead.")]
		protected virtual bool RenderTransparent
		{
			get
			{
				return this.render_transparent;
			}
			set
			{
				this.render_transparent = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can tab to the <see cref="T:System.Windows.Forms.Label" />. This property is not used by this class.</summary>
		/// <returns>This property is not used by this class. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x00075B5C File Offset: 0x00073D5C
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x00075B64 File Offset: 0x00073D64
		[Browsable(false)]
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the alignment of text in the label.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.TopLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x00075B70 File Offset: 0x00073D70
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x00075B78 File Offset: 0x00073D78
		[Localizable(true)]
		[DefaultValue(1)]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.text_align;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
				}
				if (this.text_align != value)
				{
					this.text_align = value;
					switch (value)
					{
					case 1:
						this.string_format.LineAlignment = 0;
						this.string_format.Alignment = 0;
						break;
					case 2:
						this.string_format.LineAlignment = 0;
						this.string_format.Alignment = 1;
						break;
					default:
						if (value != 16)
						{
							if (value != 32)
							{
								if (value != 64)
								{
									if (value != 256)
									{
										if (value != 512)
										{
											if (value == 1024)
											{
												this.string_format.LineAlignment = 2;
												this.string_format.Alignment = 2;
											}
										}
										else
										{
											this.string_format.LineAlignment = 2;
											this.string_format.Alignment = 1;
										}
									}
									else
									{
										this.string_format.LineAlignment = 2;
										this.string_format.Alignment = 0;
									}
								}
								else
								{
									this.string_format.LineAlignment = 1;
									this.string_format.Alignment = 2;
								}
							}
							else
							{
								this.string_format.LineAlignment = 1;
								this.string_format.Alignment = 1;
							}
						}
						else
						{
							this.string_format.LineAlignment = 1;
							this.string_format.Alignment = 0;
						}
						break;
					case 4:
						this.string_format.LineAlignment = 0;
						this.string_format.Alignment = 2;
						break;
					}
					this.OnTextAlignChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control interprets an ampersand character (&amp;) in the control's <see cref="P:System.Windows.Forms.Control.Text" /> property to be an access key prefix character.</summary>
		/// <returns>true if the label doesn't display the ampersand character and underlines the character after the ampersand in its displayed text and treats the underlined character as an access key; otherwise, false if the ampersand character is displayed in the text of the control. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x00075D3C File Offset: 0x00073F3C
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x00075D44 File Offset: 0x00073F44
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
					this.SetUseMnemonic(this.use_mnemonic);
					base.Invalidate();
				}
			}
		}

		/// <summary>Determines the size and location of an image drawn within the <see cref="T:System.Windows.Forms.Label" /> control based on the alignment of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the specified image within the control.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> used to determine size and location when drawn within the control. </param>
		/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the area to draw the image in. </param>
		/// <param name="align">The alignment of content within the control. </param>
		// Token: 0x06001F61 RID: 8033 RVA: 0x00075D6C File Offset: 0x00073F6C
		protected Rectangle CalcImageRenderBounds(Image image, Rectangle r, ContentAlignment align)
		{
			Rectangle rectangle = r;
			rectangle.Inflate(-2, -2);
			int num = r.X;
			int num2 = r.Y;
			if (align == 2 || align == 32 || align == 512)
			{
				num += (r.Width - image.Width) / 2;
			}
			else if (align == 4 || align == 64 || align == 1024)
			{
				num += r.Width - image.Width;
			}
			if (align == 512 || align == 256 || align == 1024)
			{
				num2 += r.Height - image.Height;
			}
			else if (align == 32 || align == 16 || align == 64)
			{
				num2 += (r.Height - image.Height) / 2;
			}
			rectangle.X = num;
			rectangle.Y = num2;
			rectangle.Width = image.Width;
			rectangle.Height = image.Height;
			return rectangle;
		}

		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06001F62 RID: 8034 RVA: 0x00075E80 File Offset: 0x00074080
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Label" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F63 RID: 8035 RVA: 0x00075E88 File Offset: 0x00074088
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.string_format.Dispose();
			}
		}

		/// <summary>Draws an <see cref="T:System.Drawing.Image" /> within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="align">The alignment of the image to draw within the <see cref="T:System.Windows.Forms.Label" />. </param>
		// Token: 0x06001F64 RID: 8036 RVA: 0x00075EA4 File Offset: 0x000740A4
		protected internal void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
		{
			if (image == null || g == null)
			{
				return;
			}
			Rectangle rectangle = this.CalcImageRenderBounds(image, r, align);
			if (base.Enabled)
			{
				g.DrawImage(image, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			}
			else
			{
				ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, this.BackColor);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F65 RID: 8037 RVA: 0x00075F18 File Offset: 0x00074118
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F66 RID: 8038 RVA: 0x00075F24 File Offset: 0x00074124
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
			base.Invalidate();
		}

		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001F67 RID: 8039 RVA: 0x00075F44 File Offset: 0x00074144
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06001F68 RID: 8040 RVA: 0x00075F50 File Offset: 0x00074150
		protected override void OnPaint(PaintEventArgs e)
		{
			ThemeElements.LabelPainter.Draw(e.Graphics, base.ClientRectangle, this);
			base.OnPaint(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F69 RID: 8041 RVA: 0x00075F7C File Offset: 0x0007417C
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001F6A RID: 8042 RVA: 0x00075F88 File Offset: 0x00074188
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F6B RID: 8043 RVA: 0x00075F94 File Offset: 0x00074194
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Label.TextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F6C RID: 8044 RVA: 0x00075FC8 File Offset: 0x000741C8
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
			base.Invalidate();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F6D RID: 8045 RVA: 0x00075FE8 File Offset: 0x000741E8
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06001F6E RID: 8046 RVA: 0x00075FF4 File Offset: 0x000741F4
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				if (base.Parent != null)
				{
					base.Parent.SelectNextControl(this, true, false, false, false);
				}
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Sets the specified bounds of the label.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
		// Token: 0x06001F6F RID: 8047 RVA: 0x00076038 File Offset: 0x00074238
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F70 RID: 8048 RVA: 0x00076048 File Offset: 0x00074248
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06001F71 RID: 8049 RVA: 0x00076060 File Offset: 0x00074260
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_DRAWITEM)
			{
				base.WndProc(ref m);
			}
			else
			{
				m.Result = (IntPtr)1;
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000760A0 File Offset: 0x000742A0
		private void CalcAutoSize()
		{
			if (!this.AutoSize)
			{
				return;
			}
			Size size = this.InternalGetPreferredSize(Size.Empty);
			base.SetBounds(base.Left, base.Top, size.Width, size.Height, BoundsSpecified.Size);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000760E8 File Offset: 0x000742E8
		private void OnHandleCreatedLB(object o, EventArgs e)
		{
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000760FC File Offset: 0x000742FC
		private void SetUseMnemonic(bool use)
		{
			if (use)
			{
				this.string_format.HotkeyPrefix = 1;
			}
			else
			{
				this.string_format.HotkeyPrefix = 0;
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00076124 File Offset: 0x00074324
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x0007612C File Offset: 0x0007432C
		[DefaultValue(false)]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
			set
			{
				this.use_compatible_text_rendering = value;
			}
		}

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00076138 File Offset: 0x00074338
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x00076140 File Offset: 0x00074340
		[SettingsBindable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F79 RID: 8057 RVA: 0x0007614C File Offset: 0x0007434C
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F7A RID: 8058 RVA: 0x00076158 File Offset: 0x00074358
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001F7B RID: 8059 RVA: 0x00076164 File Offset: 0x00074364
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0400112C RID: 4396
		private bool autosize;

		// Token: 0x0400112D RID: 4397
		private bool auto_ellipsis;

		// Token: 0x0400112E RID: 4398
		private Image image;

		// Token: 0x0400112F RID: 4399
		private bool render_transparent;

		// Token: 0x04001130 RID: 4400
		private FlatStyle flat_style;

		// Token: 0x04001131 RID: 4401
		private bool use_mnemonic;

		// Token: 0x04001132 RID: 4402
		private int image_index = -1;

		// Token: 0x04001133 RID: 4403
		private string image_key = string.Empty;

		// Token: 0x04001134 RID: 4404
		private ImageList image_list;

		// Token: 0x04001135 RID: 4405
		internal ContentAlignment image_align;

		// Token: 0x04001136 RID: 4406
		internal StringFormat string_format;

		// Token: 0x04001137 RID: 4407
		internal ContentAlignment text_align;

		// Token: 0x04001138 RID: 4408
		private static SizeF req_witdthsize = new SizeF(0f, 0f);
	}
}
