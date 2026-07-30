using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar button. Although <see cref="T:System.Windows.Forms.ToolStripButton" /> replaces and extends the <see cref="T:System.Windows.Forms.ToolBarButton" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBarButton" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000338 RID: 824
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.ToolBarButtonDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolBarButton : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class.</summary>
		// Token: 0x06003989 RID: 14729 RVA: 0x000ECE6C File Offset: 0x000EB06C
		public ToolBarButton()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class and displays the assigned text on the button.</summary>
		/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />. </param>
		// Token: 0x0600398A RID: 14730 RVA: 0x000ECEBC File Offset: 0x000EB0BC
		public ToolBarButton(string text)
		{
			this.text = text;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000ECF14 File Offset: 0x000EB114
		// Note: this type is marked as 'beforefieldinit'.
		static ToolBarButton()
		{
			ToolBarButton.UIAGotFocusEvent = new object();
			ToolBarButton.UIALostFocusEvent = new object();
			ToolBarButton.UIATextChangedEvent = new object();
			ToolBarButton.UIAEnabledChangedEvent = new object();
			ToolBarButton.UIADropDownMenuChangedEvent = new object();
			ToolBarButton.UIAStyleChangedEvent = new object();
		}

		// Token: 0x14000350 RID: 848
		// (add) Token: 0x0600398C RID: 14732 RVA: 0x000ECF60 File Offset: 0x000EB160
		// (remove) Token: 0x0600398D RID: 14733 RVA: 0x000ECF74 File Offset: 0x000EB174
		internal event EventHandler UIAGotFocus
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIAGotFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIAGotFocusEvent, value);
			}
		}

		// Token: 0x14000351 RID: 849
		// (add) Token: 0x0600398E RID: 14734 RVA: 0x000ECF88 File Offset: 0x000EB188
		// (remove) Token: 0x0600398F RID: 14735 RVA: 0x000ECF9C File Offset: 0x000EB19C
		internal event EventHandler UIALostFocus
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIALostFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIALostFocusEvent, value);
			}
		}

		// Token: 0x14000352 RID: 850
		// (add) Token: 0x06003990 RID: 14736 RVA: 0x000ECFB0 File Offset: 0x000EB1B0
		// (remove) Token: 0x06003991 RID: 14737 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
		internal event EventHandler UIATextChanged
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIATextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIATextChangedEvent, value);
			}
		}

		// Token: 0x14000353 RID: 851
		// (add) Token: 0x06003992 RID: 14738 RVA: 0x000ECFD8 File Offset: 0x000EB1D8
		// (remove) Token: 0x06003993 RID: 14739 RVA: 0x000ECFEC File Offset: 0x000EB1EC
		internal event EventHandler UIAEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIAEnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIAEnabledChangedEvent, value);
			}
		}

		// Token: 0x14000354 RID: 852
		// (add) Token: 0x06003994 RID: 14740 RVA: 0x000ED000 File Offset: 0x000EB200
		// (remove) Token: 0x06003995 RID: 14741 RVA: 0x000ED014 File Offset: 0x000EB214
		internal event EventHandler UIADropDownMenuChanged
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIADropDownMenuChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIADropDownMenuChangedEvent, value);
			}
		}

		// Token: 0x14000355 RID: 853
		// (add) Token: 0x06003996 RID: 14742 RVA: 0x000ED028 File Offset: 0x000EB228
		// (remove) Token: 0x06003997 RID: 14743 RVA: 0x000ED03C File Offset: 0x000EB23C
		internal event EventHandler UIAStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolBarButton.UIAStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBarButton.UIAStyleChangedEvent, value);
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000ED050 File Offset: 0x000EB250
		internal Image Image
		{
			get
			{
				if (this.Parent == null || this.Parent.ImageList == null)
				{
					return null;
				}
				ImageList imageList = this.Parent.ImageList;
				if (this.ImageIndex > -1 && this.ImageIndex < imageList.Images.Count)
				{
					return imageList.Images[this.ImageIndex];
				}
				if (!string.IsNullOrEmpty(this.image_key))
				{
					return imageList.Images[this.image_key];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the menu to be displayed in the drop-down toolbar button.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> to be displayed in the drop-down toolbar button. The default is null.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned object is not a <see cref="T:System.Windows.Forms.ContextMenu" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000ED0E0 File Offset: 0x000EB2E0
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000ED0E8 File Offset: 0x000EB2E8
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		public Menu DropDownMenu
		{
			get
			{
				return this.menu;
			}
			set
			{
				if (value is ContextMenu)
				{
					this.menu = (ContextMenu)value;
					this.OnUIADropDownMenuChanged(EventArgs.Empty);
					return;
				}
				throw new ArgumentException("DropDownMenu must be of type ContextMenu.");
			}
		}

		/// <summary>Gets or sets a value indicating whether the button is enabled.</summary>
		/// <returns>true if the button is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000ED128 File Offset: 0x000EB328
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000ED130 File Offset: 0x000EB330
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (value == this.enabled)
				{
					return;
				}
				this.enabled = value;
				this.Invalidate();
				this.OnUIAEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the index value of the image assigned to the button.</summary>
		/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000ED158 File Offset: 0x000EB358
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x000ED160 File Offset: 0x000EB360
		[RefreshProperties(2)]
		[DefaultValue(-1)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("ImageIndex value must be above or equal to -1.");
				}
				if (value == this.image_index)
				{
					return;
				}
				bool flag = this.Parent != null && (value == -1 || this.image_index == -1);
				this.image_index = value;
				this.image_key = string.Empty;
				if (flag)
				{
					this.Parent.Redraw(true);
				}
				else
				{
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the name of the image assigned to the button.</summary>
		/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000ED1E0 File Offset: 0x000EB3E0
		// (set) Token: 0x060039A0 RID: 14752 RVA: 0x000ED1E8 File Offset: 0x000EB3E8
		[TypeConverter(typeof(ImageKeyConverter))]
		[Localizable(true)]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				if (this.image_key == value)
				{
					return;
				}
				bool flag = this.Parent != null && (value == string.Empty || this.image_key == string.Empty);
				this.image_index = -1;
				this.image_key = value;
				if (flag)
				{
					this.Parent.Redraw(true);
				}
				else
				{
					this.Invalidate();
				}
			}
		}

		/// <summary>The name of the button.</summary>
		/// <returns>The name of the button.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000ED264 File Offset: 0x000EB464
		// (set) Token: 0x060039A2 RID: 14754 RVA: 0x000ED280 File Offset: 0x000EB480
		[Browsable(false)]
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

		/// <summary>Gets the toolbar control that the toolbar button is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBar" /> control that the <see cref="T:System.Windows.Forms.ToolBarButton" /> is assigned to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000ED28C File Offset: 0x000EB48C
		[Browsable(false)]
		public ToolBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is partially pushed.</summary>
		/// <returns>true if a toggle-style toolbar button is partially pushed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000ED294 File Offset: 0x000EB494
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000ED29C File Offset: 0x000EB49C
		[DefaultValue(false)]
		public bool PartialPush
		{
			get
			{
				return this.partial_push;
			}
			set
			{
				if (value == this.partial_push)
				{
					return;
				}
				this.partial_push = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is currently in the pushed state.</summary>
		/// <returns>true if a toggle-style toolbar button is currently in the pushed state; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000ED2B8 File Offset: 0x000EB4B8
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000ED2C0 File Offset: 0x000EB4C0
		[DefaultValue(false)]
		public bool Pushed
		{
			get
			{
				return this.pushed;
			}
			set
			{
				if (value == this.pushed)
				{
					return;
				}
				this.pushed = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets the bounding rectangle for a toolbar button.</summary>
		/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> for a toolbar button.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000ED2DC File Offset: 0x000EB4DC
		public Rectangle Rectangle
		{
			get
			{
				if (this.Visible && this.Parent != null && this.Parent.items != null)
				{
					foreach (ToolBarItem toolBarItem in this.Parent.items)
					{
						if (toolBarItem.Button == this)
						{
							return toolBarItem.Rectangle;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		/// <summary>Gets or sets the style of the toolbar button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. The default is ToolBarButtonStyle.PushButton.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x000ED34C File Offset: 0x000EB54C
		// (set) Token: 0x060039AA RID: 14762 RVA: 0x000ED354 File Offset: 0x000EB554
		[RefreshProperties(2)]
		[DefaultValue(ToolBarButtonStyle.PushButton)]
		public ToolBarButtonStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (value == this.style)
				{
					return;
				}
				this.style = value;
				if (this.parent != null)
				{
					this.parent.Redraw(true);
				}
				this.OnUIAStyleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the object that contains data about the toolbar button.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the toolbar button. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x000ED398 File Offset: 0x000EB598
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x000ED3A0 File Offset: 0x000EB5A0
		[Bindable(true)]
		[DefaultValue(null)]
		[Localizable(false)]
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

		/// <summary>Gets or sets the text displayed on the toolbar button.</summary>
		/// <returns>The text displayed on the toolbar button. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000ED3AC File Offset: 0x000EB5AC
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000ED3B4 File Offset: 0x000EB5B4
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value == this.text)
				{
					return;
				}
				this.text = value;
				this.OnUIATextChanged(EventArgs.Empty);
				if (this.Parent != null)
				{
					this.Parent.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the text that appears as a ToolTip for the button.</summary>
		/// <returns>The text that is displayed when the mouse pointer moves over the toolbar button. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000ED40C File Offset: 0x000EB60C
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000ED414 File Offset: 0x000EB614
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tooltip;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.tooltip = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar button is visible.</summary>
		/// <returns>true if the toolbar button is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000ED42C File Offset: 0x000EB62C
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000ED434 File Offset: 0x000EB634
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (value == this.visible)
				{
					return;
				}
				this.visible = value;
				if (this.Parent != null)
				{
					this.Parent.Redraw(true);
				}
			}
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000ED464 File Offset: 0x000EB664
		internal void SetParent(ToolBar parent)
		{
			if (this.Parent == parent)
			{
				return;
			}
			if (this.Parent != null)
			{
				this.Parent.Buttons.Remove(this);
			}
			this.parent = parent;
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000ED4A4 File Offset: 0x000EB6A4
		internal void Invalidate()
		{
			if (this.Parent != null)
			{
				this.Parent.Invalidate(this.Rectangle);
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000ED4D0 File Offset: 0x000EB6D0
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000ED4D8 File Offset: 0x000EB6D8
		internal bool UIAHasFocus
		{
			get
			{
				return this.uiaHasFocus;
			}
			set
			{
				this.uiaHasFocus = value;
				EventHandler eventHandler = (EventHandler)((!value) ? base.Events[ToolBarButton.UIALostFocusEvent] : base.Events[ToolBarButton.UIAGotFocusEvent]);
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000ED530 File Offset: 0x000EB730
		private void OnUIATextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000ED564 File Offset: 0x000EB764
		private void OnUIAEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIAEnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000ED598 File Offset: 0x000EB798
		private void OnUIADropDownMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIADropDownMenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000ED5CC File Offset: 0x000EB7CC
		private void OnUIAStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIAStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBarButton" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060039BB RID: 14779 RVA: 0x000ED600 File Offset: 0x000EB800
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBarButton" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ToolBarButton" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039BC RID: 14780 RVA: 0x000ED60C File Offset: 0x000EB80C
		public override string ToString()
		{
			return string.Format("ToolBarButton: {0}, Style: {1}", this.text, this.style);
		}

		// Token: 0x040019F7 RID: 6647
		private bool enabled = true;

		// Token: 0x040019F8 RID: 6648
		private int image_index = -1;

		// Token: 0x040019F9 RID: 6649
		private ContextMenu menu;

		// Token: 0x040019FA RID: 6650
		private ToolBar parent;

		// Token: 0x040019FB RID: 6651
		private bool partial_push;

		// Token: 0x040019FC RID: 6652
		private bool pushed;

		// Token: 0x040019FD RID: 6653
		private ToolBarButtonStyle style = ToolBarButtonStyle.PushButton;

		// Token: 0x040019FE RID: 6654
		private object tag;

		// Token: 0x040019FF RID: 6655
		private string text = string.Empty;

		// Token: 0x04001A00 RID: 6656
		private string tooltip = string.Empty;

		// Token: 0x04001A01 RID: 6657
		private bool visible = true;

		// Token: 0x04001A02 RID: 6658
		private string image_key = string.Empty;

		// Token: 0x04001A03 RID: 6659
		private string name;

		// Token: 0x04001A04 RID: 6660
		private bool uiaHasFocus;
	}
}
