using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows picture box control for displaying an image.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000288 RID: 648
	[ComVisible(true)]
	[DefaultProperty("Image")]
	[Designer("System.Windows.Forms.Design.PictureBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[Docking(DockingBehavior.Ask)]
	[DefaultBindingProperty("Image")]
	public class PictureBox : Control, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PictureBox" /> class.</summary>
		// Token: 0x06002A19 RID: 10777 RVA: 0x000A3390 File Offset: 0x000A1590
		public PictureBox()
		{
			this.no_update = 0;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Opaque, false);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.HandleCreated += new EventHandler(this.PictureBox_HandleCreated);
			this.initial_image = ResourceImageLoader.Get("image-x-generic.png");
			this.error_image = ResourceImageLoader.Get("image-missing.png");
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000A3408 File Offset: 0x000A1608
		// Note: this type is marked as 'beforefieldinit'.
		static PictureBox()
		{
			PictureBox.LoadCompletedEvent = new object();
			PictureBox.LoadProgressChangedEvent = new object();
			PictureBox.SizeModeChangedEvent = new object();
		}

		/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000270 RID: 624
		// (add) Token: 0x06002A1B RID: 10779 RVA: 0x000A3428 File Offset: 0x000A1628
		// (remove) Token: 0x06002A1C RID: 10780 RVA: 0x000A3434 File Offset: 0x000A1634
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.Enter" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000271 RID: 625
		// (add) Token: 0x06002A1D RID: 10781 RVA: 0x000A3440 File Offset: 0x000A1640
		// (remove) Token: 0x06002A1E RID: 10782 RVA: 0x000A344C File Offset: 0x000A164C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Font" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000272 RID: 626
		// (add) Token: 0x06002A1F RID: 10783 RVA: 0x000A3458 File Offset: 0x000A1658
		// (remove) Token: 0x06002A20 RID: 10784 RVA: 0x000A3464 File Offset: 0x000A1664
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000273 RID: 627
		// (add) Token: 0x06002A21 RID: 10785 RVA: 0x000A3470 File Offset: 0x000A1670
		// (remove) Token: 0x06002A22 RID: 10786 RVA: 0x000A347C File Offset: 0x000A167C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000274 RID: 628
		// (add) Token: 0x06002A23 RID: 10787 RVA: 0x000A3488 File Offset: 0x000A1688
		// (remove) Token: 0x06002A24 RID: 10788 RVA: 0x000A3494 File Offset: 0x000A1694
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when a key is pressed when the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000275 RID: 629
		// (add) Token: 0x06002A25 RID: 10789 RVA: 0x000A34A0 File Offset: 0x000A16A0
		// (remove) Token: 0x06002A26 RID: 10790 RVA: 0x000A34AC File Offset: 0x000A16AC
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

		/// <summary>Occurs when a key is pressed when the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000276 RID: 630
		// (add) Token: 0x06002A27 RID: 10791 RVA: 0x000A34B8 File Offset: 0x000A16B8
		// (remove) Token: 0x06002A28 RID: 10792 RVA: 0x000A34C4 File Offset: 0x000A16C4
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when a key is released when the control has focus. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000277 RID: 631
		// (add) Token: 0x06002A29 RID: 10793 RVA: 0x000A34D0 File Offset: 0x000A16D0
		// (remove) Token: 0x06002A2A RID: 10794 RVA: 0x000A34DC File Offset: 0x000A16DC
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

		/// <summary>Occurs when input focus leaves the <see cref="T:System.Windows.Forms.PictureBox" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000278 RID: 632
		// (add) Token: 0x06002A2B RID: 10795 RVA: 0x000A34E8 File Offset: 0x000A16E8
		// (remove) Token: 0x06002A2C RID: 10796 RVA: 0x000A34F4 File Offset: 0x000A16F4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		/// <summary>Occurs when the asynchronous image-load operation is completed, been canceled, or raised an exception.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000279 RID: 633
		// (add) Token: 0x06002A2D RID: 10797 RVA: 0x000A3500 File Offset: 0x000A1700
		// (remove) Token: 0x06002A2E RID: 10798 RVA: 0x000A3514 File Offset: 0x000A1714
		public event AsyncCompletedEventHandler LoadCompleted
		{
			add
			{
				base.Events.AddHandler(PictureBox.LoadCompletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.LoadCompletedEvent, value);
			}
		}

		/// <summary>Occurs when the progress of an asynchronous image-loading operation has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027A RID: 634
		// (add) Token: 0x06002A2F RID: 10799 RVA: 0x000A3528 File Offset: 0x000A1728
		// (remove) Token: 0x06002A30 RID: 10800 RVA: 0x000A353C File Offset: 0x000A173C
		public event ProgressChangedEventHandler LoadProgressChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.LoadProgressChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.LoadProgressChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.RightToLeft" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027B RID: 635
		// (add) Token: 0x06002A31 RID: 10801 RVA: 0x000A3550 File Offset: 0x000A1750
		// (remove) Token: 0x06002A32 RID: 10802 RVA: 0x000A355C File Offset: 0x000A175C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabIndex" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027C RID: 636
		// (add) Token: 0x06002A33 RID: 10803 RVA: 0x000A3568 File Offset: 0x000A1768
		// (remove) Token: 0x06002A34 RID: 10804 RVA: 0x000A3574 File Offset: 0x000A1774
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027D RID: 637
		// (add) Token: 0x06002A35 RID: 10805 RVA: 0x000A3580 File Offset: 0x000A1780
		// (remove) Token: 0x06002A36 RID: 10806 RVA: 0x000A358C File Offset: 0x000A178C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027E RID: 638
		// (add) Token: 0x06002A37 RID: 10807 RVA: 0x000A3598 File Offset: 0x000A1798
		// (remove) Token: 0x06002A38 RID: 10808 RVA: 0x000A35A4 File Offset: 0x000A17A4
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when <see cref="P:System.Windows.Forms.PictureBox.SizeMode" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400027F RID: 639
		// (add) Token: 0x06002A39 RID: 10809 RVA: 0x000A35B0 File Offset: 0x000A17B0
		// (remove) Token: 0x06002A3A RID: 10810 RVA: 0x000A35C4 File Offset: 0x000A17C4
		public event EventHandler SizeModeChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.SizeModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.SizeModeChangedEvent, value);
			}
		}

		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x06002A3B RID: 10811 RVA: 0x000A35D8 File Offset: 0x000A17D8
		void ISupportInitialize.BeginInit()
		{
			this.no_update++;
		}

		/// <summary>Signals to the object that initialization is complete.</summary>
		// Token: 0x06002A3C RID: 10812 RVA: 0x000A35E8 File Offset: 0x000A17E8
		void ISupportInitialize.EndInit()
		{
			if (this.no_update > 0)
			{
				this.no_update--;
			}
			if (this.no_update == 0)
			{
				base.Invalidate();
			}
		}

		/// <summary>Indicates how the image is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.PictureBoxSizeMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000A3618 File Offset: 0x000A1818
		// (set) Token: 0x06002A3E RID: 10814 RVA: 0x000A3620 File Offset: 0x000A1820
		[DefaultValue(PictureBoxSizeMode.Normal)]
		[RefreshProperties(2)]
		[Localizable(true)]
		public PictureBoxSizeMode SizeMode
		{
			get
			{
				return this.size_mode;
			}
			set
			{
				if (this.size_mode == value)
				{
					return;
				}
				this.size_mode = value;
				if (this.size_mode == PictureBoxSizeMode.AutoSize)
				{
					this.AutoSize = true;
					base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				}
				else
				{
					this.AutoSize = false;
					base.SetAutoSizeMode(AutoSizeMode.GrowOnly);
				}
				this.UpdateSize();
				if (this.no_update == 0)
				{
					base.Invalidate();
				}
				this.OnSizeModeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the image that is displayed by <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000A3690 File Offset: 0x000A1890
		// (set) Token: 0x06002A40 RID: 10816 RVA: 0x000A3698 File Offset: 0x000A1898
		[Localizable(true)]
		[Bindable(true)]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.ChangeImage(value, false);
			}
		}

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000A36A4 File Offset: 0x000A18A4
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x000A36AC File Offset: 0x000A18AC
		[DispId(-504)]
		[DefaultValue(BorderStyle.None)]
		public BorderStyle BorderStyle
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000A36B8 File Offset: 0x000A18B8
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x000A36C0 File Offset: 0x000A18C0
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

		/// <summary>Gets or sets the image to display when an error occurs during the image-loading process or if the image load is canceled.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> to display if an error occurs during the image-loading process or if the image load is canceled.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000A36CC File Offset: 0x000A18CC
		// (set) Token: 0x06002A46 RID: 10822 RVA: 0x000A36D4 File Offset: 0x000A18D4
		[RefreshProperties(1)]
		[Localizable(true)]
		public Image ErrorImage
		{
			get
			{
				return this.error_image;
			}
			set
			{
				this.error_image = value;
			}
		}

		/// <summary>Gets or sets the image displayed in the <see cref="T:System.Windows.Forms.PictureBox" /> control when the main image is loading.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed in the picture box control when the main image is loading.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000A36E0 File Offset: 0x000A18E0
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x000A36E8 File Offset: 0x000A18E8
		[RefreshProperties(1)]
		[Localizable(true)]
		public Image InitialImage
		{
			get
			{
				return this.initial_image;
			}
			set
			{
				this.initial_image = value;
			}
		}

		/// <summary>Gets or sets the path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x000A36F4 File Offset: 0x000A18F4
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x000A36FC File Offset: 0x000A18FC
		[RefreshProperties(1)]
		[DefaultValue(null)]
		[Localizable(true)]
		public string ImageLocation
		{
			get
			{
				return this.image_location;
			}
			set
			{
				this.image_location = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.WaitOnLoad)
					{
						this.Load(value);
					}
					else
					{
						this.LoadAsync(value);
					}
				}
				else if (this.image_from_url)
				{
					this.ChangeImage(null, true);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an image is loaded synchronously.</summary>
		/// <returns>true if an image-loading operation is completed synchronously, otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000A3754 File Offset: 0x000A1954
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x000A375C File Offset: 0x000A195C
		[DefaultValue(false)]
		[Localizable(true)]
		public bool WaitOnLoad
		{
			get
			{
				return this.wait_on_load;
			}
			set
			{
				this.wait_on_load = value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor(IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x000A3768 File Offset: 0x000A1968
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x000A3770 File Offset: 0x000A1970
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

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left languages.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000A377C File Offset: 0x000A197C
		// (set) Token: 0x06002A50 RID: 10832 RVA: 0x000A3784 File Offset: 0x000A1984
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

		/// <summary>Gets or sets the tab index value.</summary>
		/// <returns>The tab index value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000A3790 File Offset: 0x000A1990
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x000A3798 File Offset: 0x000A1998
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000A37A4 File Offset: 0x000A19A4
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x000A37AC File Offset: 0x000A19AC
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The text of the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000A37B8 File Offset: 0x000A19B8
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x000A37C0 File Offset: 0x000A19C0
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000A37CC File Offset: 0x000A19CC
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets a value indicating the mode for Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000A37D4 File Offset: 0x000A19D4
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return base.DefaultImeMode;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.Font" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000A37DC File Offset: 0x000A19DC
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x000A37E4 File Offset: 0x000A19E4
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000A37F0 File Offset: 0x000A19F0
		// (set) Token: 0x06002A5C RID: 10844 RVA: 0x000A37F8 File Offset: 0x000A19F8
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.AllowDrop" /> property.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000A3804 File Offset: 0x000A1A04
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x000A380C File Offset: 0x000A1A0C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000A3818 File Offset: 0x000A1A18
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.PictureBoxDefaultSize;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PictureBox" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release unmanaged resources only.</param>
		// Token: 0x06002A60 RID: 10848 RVA: 0x000A3824 File Offset: 0x000A1A24
		protected override void Dispose(bool disposing)
		{
			if (this.image != null)
			{
				this.StopAnimation();
				this.image = null;
			}
			this.initial_image = null;
			base.Dispose(disposing);
		}

		/// <param name="pe"></param>
		// Token: 0x06002A61 RID: 10849 RVA: 0x000A3858 File Offset: 0x000A1A58
		protected override void OnPaint(PaintEventArgs pe)
		{
			ThemeEngine.Current.DrawPictureBox(pe.Graphics, pe.ClipRectangle, this);
			base.OnPaint(pe);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A62 RID: 10850 RVA: 0x000A3884 File Offset: 0x000A1A84
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.SizeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A63 RID: 10851 RVA: 0x000A3890 File Offset: 0x000A1A90
		protected virtual void OnSizeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PictureBox.SizeModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A64 RID: 10852 RVA: 0x000A38C4 File Offset: 0x000A1AC4
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A65 RID: 10853 RVA: 0x000A38D0 File Offset: 0x000A1AD0
		[EditorBrowsable(2)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002A66 RID: 10854 RVA: 0x000A38DC File Offset: 0x000A1ADC
		[EditorBrowsable(2)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A67 RID: 10855 RVA: 0x000A38E8 File Offset: 0x000A1AE8
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			AsyncCompletedEventHandler asyncCompletedEventHandler = (AsyncCompletedEventHandler)base.Events[PictureBox.LoadCompletedEvent];
			if (asyncCompletedEventHandler != null)
			{
				asyncCompletedEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A68 RID: 10856 RVA: 0x000A391C File Offset: 0x000A1B1C
		protected virtual void OnLoadProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[PictureBox.LoadProgressChangedEvent];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A69 RID: 10857 RVA: 0x000A3950 File Offset: 0x000A1B50
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002A6A RID: 10858 RVA: 0x000A395C File Offset: 0x000A1B5C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Invalidate();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000A396C File Offset: 0x000A1B6C
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.image == null)
			{
				return base.GetPreferredSizeCore(proposedSize);
			}
			return this.image.Size;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x000A398C File Offset: 0x000A1B8C
		private WebClient ImageDownload
		{
			get
			{
				if (this.image_download == null)
				{
					this.image_download = new WebClient();
				}
				return this.image_download;
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000A39AC File Offset: 0x000A1BAC
		private void ChangeImage(Image value, bool from_url)
		{
			this.StopAnimation();
			this.image_from_url = from_url;
			this.image = value;
			if (base.IsHandleCreated)
			{
				this.UpdateSize();
				if (this.image != null && ImageAnimator.CanAnimate(this.image))
				{
					this.frame_handler = new EventHandler(this.OnAnimateImage);
					ImageAnimator.Animate(this.image, this.frame_handler);
				}
				if (this.no_update == 0)
				{
					base.Invalidate();
				}
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000A3A30 File Offset: 0x000A1C30
		private void StopAnimation()
		{
			if (this.frame_handler == null)
			{
				return;
			}
			ImageAnimator.StopAnimate(this.image, this.frame_handler);
			this.frame_handler = null;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000A3A64 File Offset: 0x000A1C64
		private void UpdateSize()
		{
			if (this.image == null)
			{
				return;
			}
			if (base.Parent != null)
			{
				base.Parent.PerformLayout(this, "AutoSize");
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000A3A9C File Offset: 0x000A1C9C
		private void OnAnimateImage(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			this.BeginInvoke(new EventHandler(this.UpdateAnimatedImage), new object[] { this, e });
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000A3ACC File Offset: 0x000A1CCC
		private void UpdateAnimatedImage(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			ImageAnimator.UpdateFrames(this.image);
			this.Refresh();
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000A3AEC File Offset: 0x000A1CEC
		private void PictureBox_HandleCreated(object sender, EventArgs e)
		{
			this.UpdateSize();
			if (this.image != null && ImageAnimator.CanAnimate(this.image))
			{
				this.frame_handler = new EventHandler(this.OnAnimateImage);
				ImageAnimator.Animate(this.image, this.frame_handler);
			}
			if (this.no_update == 0)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000A3B50 File Offset: 0x000A1D50
		private void ImageDownload_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			if (e.Error != null && !e.Cancelled)
			{
				this.Image = this.error_image;
			}
			else if (e.Error == null && !e.Cancelled)
			{
				using (MemoryStream memoryStream = new MemoryStream(e.Result))
				{
					this.Image = Image.FromStream(memoryStream);
				}
			}
			this.ImageDownload.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(this.ImageDownload_DownloadProgressChanged);
			this.ImageDownload.DownloadDataCompleted -= new DownloadDataCompletedEventHandler(this.ImageDownload_DownloadDataCompleted);
			this.image_download = null;
			this.OnLoadCompleted(e);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000A3C1C File Offset: 0x000A1E1C
		private void ImageDownload_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.OnLoadProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, e.UserState));
		}

		/// <summary>Cancels an asynchronous image load.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A75 RID: 10869 RVA: 0x000A3C38 File Offset: 0x000A1E38
		public void CancelAsync()
		{
			if (this.image_download != null)
			{
				this.image_download.CancelAsync();
			}
		}

		/// <summary>Displays the image specified by the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> property of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> is null or an empty string.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A76 RID: 10870 RVA: 0x000A3C50 File Offset: 0x000A1E50
		public void Load()
		{
			this.Load(this.image_location);
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> to the specified URL and displays the image indicated.</summary>
		/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is null or an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <paramref name="url" /> refers to an image on the Web that cannot be accessed.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> refers to a file that is not an image.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="url" /> refers to a file that does not exist.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A77 RID: 10871 RVA: 0x000A3C60 File Offset: 0x000A1E60
		public void Load(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new InvalidOperationException("ImageLocation not specified.");
			}
			this.image_location = url;
			if (url.Contains("://"))
			{
				using (Stream stream = this.ImageDownload.OpenRead(url))
				{
					this.ChangeImage(Image.FromStream(stream), true);
				}
			}
			else
			{
				this.ChangeImage(Image.FromFile(url), true);
			}
		}

		/// <summary>Loads the image asynchronously.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A78 RID: 10872 RVA: 0x000A3CF4 File Offset: 0x000A1EF4
		public void LoadAsync()
		{
			this.LoadAsync(this.image_location);
		}

		/// <summary>Loads the image at the specified location, asynchronously.</summary>
		/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A79 RID: 10873 RVA: 0x000A3D04 File Offset: 0x000A1F04
		public void LoadAsync(string url)
		{
			if (this.wait_on_load)
			{
				this.Load(url);
				return;
			}
			if (string.IsNullOrEmpty(url))
			{
				throw new InvalidOperationException("ImageLocation not specified.");
			}
			this.image_location = url;
			this.ChangeImage(this.InitialImage, true);
			if (this.ImageDownload.IsBusy)
			{
				this.ImageDownload.CancelAsync();
			}
			Uri uri = null;
			try
			{
				uri = new Uri(url);
			}
			catch (UriFormatException)
			{
				uri = new Uri(Path.GetFullPath(url));
			}
			this.ImageDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ImageDownload_DownloadProgressChanged);
			this.ImageDownload.DownloadDataCompleted += new DownloadDataCompletedEventHandler(this.ImageDownload_DownloadDataCompleted);
			this.ImageDownload.DownloadDataAsync(uri);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.PictureBox" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.PictureBox" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A7A RID: 10874 RVA: 0x000A3DE0 File Offset: 0x000A1FE0
		public override string ToString()
		{
			return string.Format("{0}, SizeMode: {1}", base.ToString(), this.SizeMode);
		}

		// Token: 0x040014EE RID: 5358
		private Image image;

		// Token: 0x040014EF RID: 5359
		private PictureBoxSizeMode size_mode;

		// Token: 0x040014F0 RID: 5360
		private Image error_image;

		// Token: 0x040014F1 RID: 5361
		private string image_location;

		// Token: 0x040014F2 RID: 5362
		private Image initial_image;

		// Token: 0x040014F3 RID: 5363
		private bool wait_on_load;

		// Token: 0x040014F4 RID: 5364
		private WebClient image_download;

		// Token: 0x040014F5 RID: 5365
		private bool image_from_url;

		// Token: 0x040014F6 RID: 5366
		private int no_update;

		// Token: 0x040014F7 RID: 5367
		private EventHandler frame_handler;
	}
}
