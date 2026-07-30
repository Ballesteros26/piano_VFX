using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusBar" /> control. Although the <see cref="T:System.Windows.Forms.StatusStrip" /> control replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EA RID: 746
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class StatusBarPanel : Component, ISupportInitialize
	{
		// Token: 0x06003194 RID: 12692 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
		// Note: this type is marked as 'beforefieldinit'.
		static StatusBarPanel()
		{
			StatusBarPanel.UIATextChangedEvent = new object();
		}

		// Token: 0x14000313 RID: 787
		// (add) Token: 0x06003195 RID: 12693 RVA: 0x000BE4D4 File Offset: 0x000BC6D4
		// (remove) Token: 0x06003196 RID: 12694 RVA: 0x000BE4E8 File Offset: 0x000BC6E8
		internal event EventHandler UIATextChanged
		{
			add
			{
				base.Events.AddHandler(StatusBarPanel.UIATextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBarPanel.UIATextChangedEvent, value);
			}
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		internal void OnUIATextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[StatusBarPanel.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets or sets the alignment of text and icons within the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000BE530 File Offset: 0x000BC730
		// (set) Token: 0x06003199 RID: 12697 RVA: 0x000BE538 File Offset: 0x000BC738
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
				this.InvalidateContents();
			}
		}

		/// <summary>Gets or sets a value indicating whether the status bar panel is automatically resized.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000BE548 File Offset: 0x000BC748
		// (set) Token: 0x0600319B RID: 12699 RVA: 0x000BE550 File Offset: 0x000BC750
		[RefreshProperties(1)]
		[DefaultValue(StatusBarPanelAutoSize.None)]
		public StatusBarPanelAutoSize AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				this.auto_size = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the border style of the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelBorderStyle.Sunken" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000BE560 File Offset: 0x000BC760
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000BE568 File Offset: 0x000BC768
		[DefaultValue(StatusBarPanelBorderStyle.Sunken)]
		[DispId(-504)]
		public StatusBarPanelBorderStyle BorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				this.border_style = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the icon to display within the status bar panel.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon to display in the panel.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000BE578 File Offset: 0x000BC778
		// (set) Token: 0x0600319F RID: 12703 RVA: 0x000BE580 File Offset: 0x000BC780
		[Localizable(true)]
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
				this.InvalidateContents();
			}
		}

		/// <summary>Gets or sets the minimum allowed width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The minimum width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <exception cref="T:System.ArgumentException">A value less than 0 is assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000BE590 File Offset: 0x000BC790
		// (set) Token: 0x060031A1 RID: 12705 RVA: 0x000BE598 File Offset: 0x000BC798
		[RefreshProperties(1)]
		[Localizable(true)]
		[DefaultValue(10)]
		public int MinWidth
		{
			get
			{
				return this.min_width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.min_width = value;
				if (this.min_width > this.width)
				{
					this.width = this.min_width;
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the name to apply to the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000BE5E4 File Offset: 0x000BC7E4
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x000BE600 File Offset: 0x000BC800
		[Localizable(true)]
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <exception cref="T:System.ArgumentException">The width specified is less than the value of the <see cref="P:System.Windows.Forms.StatusBarPanel.MinWidth" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000BE60C File Offset: 0x000BC80C
		// (set) Token: 0x060031A5 RID: 12709 RVA: 0x000BE614 File Offset: 0x000BC814
		[DefaultValue(100)]
		[Localizable(true)]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				if (this.initializing)
				{
					this.width = value;
				}
				else
				{
					this.SetWidth(value);
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the style of the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelStyle.Text" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000BE658 File Offset: 0x000BC858
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x000BE660 File Offset: 0x000BC860
		[DefaultValue(StatusBarPanelStyle.Text)]
		public StatusBarPanelStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000BE670 File Offset: 0x000BC870
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x000BE678 File Offset: 0x000BC878
		[Localizable(false)]
		[Bindable(true)]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text of the status bar panel.</summary>
		/// <returns>The text displayed in the panel.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000BE684 File Offset: 0x000BC884
		// (set) Token: 0x060031AB RID: 12715 RVA: 0x000BE68C File Offset: 0x000BC88C
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.InvalidateContents();
				this.OnUIATextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets ToolTip text associated with the status bar panel.</summary>
		/// <returns>The ToolTip text for the panel.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000BE6A8 File Offset: 0x000BC8A8
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x000BE6B0 File Offset: 0x000BC8B0
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tool_tip_text;
			}
			set
			{
				this.tool_tip_text = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBar" /> control that hosts the status bar panel.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBar" /> that contains the panel.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000BE6BC File Offset: 0x000BC8BC
		[Browsable(false)]
		public StatusBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000BE6C4 File Offset: 0x000BC8C4
		private void Invalidate()
		{
			if (this.parent == null)
			{
				return;
			}
			this.parent.UpdatePanel(this);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000BE6E0 File Offset: 0x000BC8E0
		private void InvalidateContents()
		{
			if (this.parent == null)
			{
				return;
			}
			this.parent.UpdatePanelContents(this);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000BE6FC File Offset: 0x000BC8FC
		internal void SetParent(StatusBar parent)
		{
			this.parent = parent;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000BE708 File Offset: 0x000BC908
		internal void SetWidth(int width)
		{
			this.width = width;
			if (this.min_width > this.width)
			{
				this.width = this.min_width;
			}
		}

		/// <summary>Retrieves a string that contains information about the panel.</summary>
		/// <returns>Returns a string that contains the class name for the control and the text it contains.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060031B3 RID: 12723 RVA: 0x000BE73C File Offset: 0x000BC93C
		public override string ToString()
		{
			return "StatusBarPanel: {" + this.Text + "}";
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusBarPanel" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060031B4 RID: 12724 RVA: 0x000BE754 File Offset: 0x000BC954
		protected override void Dispose(bool disposing)
		{
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060031B5 RID: 12725 RVA: 0x000BE758 File Offset: 0x000BC958
		public void BeginInit()
		{
			this.initializing = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060031B6 RID: 12726 RVA: 0x000BE764 File Offset: 0x000BC964
		public void EndInit()
		{
			if (!this.initializing)
			{
				return;
			}
			if (this.min_width > this.width)
			{
				this.width = this.min_width;
			}
			this.initializing = false;
		}

		// Token: 0x040017F2 RID: 6130
		private StatusBar parent;

		// Token: 0x040017F3 RID: 6131
		private bool initializing;

		// Token: 0x040017F4 RID: 6132
		private string text = string.Empty;

		// Token: 0x040017F5 RID: 6133
		private string tool_tip_text = string.Empty;

		// Token: 0x040017F6 RID: 6134
		private Icon icon;

		// Token: 0x040017F7 RID: 6135
		private HorizontalAlignment alignment;

		// Token: 0x040017F8 RID: 6136
		private StatusBarPanelAutoSize auto_size = StatusBarPanelAutoSize.None;

		// Token: 0x040017F9 RID: 6137
		private StatusBarPanelBorderStyle border_style = StatusBarPanelBorderStyle.Sunken;

		// Token: 0x040017FA RID: 6138
		private StatusBarPanelStyle style = StatusBarPanelStyle.Text;

		// Token: 0x040017FB RID: 6139
		private int width = 100;

		// Token: 0x040017FC RID: 6140
		private int min_width = 10;

		// Token: 0x040017FD RID: 6141
		internal int X;

		// Token: 0x040017FE RID: 6142
		private string name;

		// Token: 0x040017FF RID: 6143
		private object tag;
	}
}
