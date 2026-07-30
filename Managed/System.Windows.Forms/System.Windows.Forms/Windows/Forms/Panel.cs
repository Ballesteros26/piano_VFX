using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Used to group collections of controls.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000287 RID: 647
	[Designer("System.Windows.Forms.Design.PanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("BorderStyle")]
	[ComVisible(true)]
	[ClassInterface(1)]
	[Docking(DockingBehavior.Ask)]
	[DefaultEvent("Paint")]
	public class Panel : ScrollableControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel" /> class.</summary>
		// Token: 0x060029FF RID: 10751 RVA: 0x000A3034 File Offset: 0x000A1234
		public Panel()
		{
			base.TabStop = false;
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Panel.AutoSize" /> property has changed.</summary>
		// Token: 0x1400026B RID: 619
		// (add) Token: 0x06002A00 RID: 10752 RVA: 0x000A3068 File Offset: 0x000A1268
		// (remove) Token: 0x06002A01 RID: 10753 RVA: 0x000A3074 File Offset: 0x000A1274
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400026C RID: 620
		// (add) Token: 0x06002A02 RID: 10754 RVA: 0x000A3080 File Offset: 0x000A1280
		// (remove) Token: 0x06002A03 RID: 10755 RVA: 0x000A308C File Offset: 0x000A128C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400026D RID: 621
		// (add) Token: 0x06002A04 RID: 10756 RVA: 0x000A3098 File Offset: 0x000A1298
		// (remove) Token: 0x06002A05 RID: 10757 RVA: 0x000A30A4 File Offset: 0x000A12A4
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400026E RID: 622
		// (add) Token: 0x06002A06 RID: 10758 RVA: 0x000A30B0 File Offset: 0x000A12B0
		// (remove) Token: 0x06002A07 RID: 10759 RVA: 0x000A30BC File Offset: 0x000A12BC
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400026F RID: 623
		// (add) Token: 0x06002A08 RID: 10760 RVA: 0x000A30C8 File Offset: 0x000A12C8
		// (remove) Token: 0x06002A09 RID: 10761 RVA: 0x000A30D4 File Offset: 0x000A12D4
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

		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000A30E0 File Offset: 0x000A12E0
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x000A30E8 File Offset: 0x000A12E8
		[DesignerSerializationVisibility(1)]
		[Browsable(true)]
		[EditorBrowsable(0)]
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

		/// <summary>Indicates the automatic sizing behavior of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</exception>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000A30F4 File Offset: 0x000A12F4
		// (set) Token: 0x06002A0D RID: 10765 RVA: 0x000A30FC File Offset: 0x000A12FC
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Browsable(true)]
		[Localizable(true)]
		public virtual AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				base.SetAutoSizeMode(value);
			}
		}

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000A3108 File Offset: 0x000A1308
		// (set) Token: 0x06002A0F RID: 10767 RVA: 0x000A3110 File Offset: 0x000A1310
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

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000A311C File Offset: 0x000A131C
		// (set) Token: 0x06002A11 RID: 10769 RVA: 0x000A3124 File Offset: 0x000A1324
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				if (value == this.TabStop)
				{
					return;
				}
				base.TabStop = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000A313C File Offset: 0x000A133C
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x000A3144 File Offset: 0x000A1344
		[EditorBrowsable(1)]
		[Browsable(false)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == this.Text)
				{
					return;
				}
				base.Text = value;
				this.Refresh();
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x000A3168 File Offset: 0x000A1368
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x000A3170 File Offset: 0x000A1370
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.PanelDefaultSize;
			}
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A16 RID: 10774 RVA: 0x000A317C File Offset: 0x000A137C
		public override string ToString()
		{
			return base.ToString() + ", BorderStyle: " + this.BorderStyle;
		}

		/// <summary>Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A17 RID: 10775 RVA: 0x000A319C File Offset: 0x000A139C
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			base.Invalidate(true);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000A31AC File Offset: 0x000A13AC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size empty = Size.Empty;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Right > empty.Width)
					{
						empty.Width = control.Bounds.Right;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && (control.Anchor & AnchorStyles.Right) == AnchorStyles.None && control.Bounds.Right + control.Margin.Right > empty.Width)
				{
					empty.Width = control.Bounds.Right + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Bottom > empty.Height)
					{
						empty.Height = control.Bounds.Bottom;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && (control.Anchor & AnchorStyles.Bottom) == AnchorStyles.None && control.Bounds.Bottom + control.Margin.Bottom > empty.Height)
				{
					empty.Height = control.Bounds.Bottom + control.Margin.Bottom;
				}
			}
			return empty;
		}
	}
}
