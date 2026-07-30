using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that is properly rendered in a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000340 RID: 832
	[DefaultProperty("Items")]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripComboBox : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class.</summary>
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000F0D24 File Offset: 0x000EEF24
		public ToolStripComboBox()
			: base(new ToolStripComboBox.ToolStripComboBoxControl())
		{
			this.Size = new Size(121, 21);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class derived from a base control.</summary>
		/// <param name="c">The base control. </param>
		/// <exception cref="T:System.NotSupportedException">The operation is not supported. </exception>
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000F0D40 File Offset: 0x000EEF40
		[EditorBrowsable(1)]
		public ToolStripComboBox(Control c)
			: base(c)
		{
			throw new NotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class with the specified name. </summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</param>
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F0D50 File Offset: 0x000EEF50
		public ToolStripComboBox(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000F0D60 File Offset: 0x000EEF60
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripComboBox()
		{
			ToolStripComboBox.DropDownEvent = new object();
			ToolStripComboBox.DropDownClosedEvent = new object();
			ToolStripComboBox.DropDownStyleChangedEvent = new object();
			ToolStripComboBox.SelectedIndexChangedEvent = new object();
			ToolStripComboBox.TextUpdateEvent = new object();
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000368 RID: 872
		// (add) Token: 0x06003AB6 RID: 15030 RVA: 0x000F0DA0 File Offset: 0x000EEFA0
		// (remove) Token: 0x06003AB7 RID: 15031 RVA: 0x000F0DAC File Offset: 0x000EEFAC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> is shown.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000369 RID: 873
		// (add) Token: 0x06003AB8 RID: 15032 RVA: 0x000F0DB8 File Offset: 0x000EEFB8
		// (remove) Token: 0x06003AB9 RID: 15033 RVA: 0x000F0DCC File Offset: 0x000EEFCC
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.DropDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.DropDownEvent, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> has closed.</summary>
		// Token: 0x1400036A RID: 874
		// (add) Token: 0x06003ABA RID: 15034 RVA: 0x000F0DE0 File Offset: 0x000EEFE0
		// (remove) Token: 0x06003ABB RID: 15035 RVA: 0x000F0DF4 File Offset: 0x000EEFF4
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.DropDownClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.DropDownClosedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400036B RID: 875
		// (add) Token: 0x06003ABC RID: 15036 RVA: 0x000F0E08 File Offset: 0x000EF008
		// (remove) Token: 0x06003ABD RID: 15037 RVA: 0x000F0E1C File Offset: 0x000EF01C
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.DropDownStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.DropDownStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripComboBox.SelectedIndex" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400036C RID: 876
		// (add) Token: 0x06003ABE RID: 15038 RVA: 0x000F0E30 File Offset: 0x000EF030
		// (remove) Token: 0x06003ABF RID: 15039 RVA: 0x000F0E44 File Offset: 0x000EF044
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> text has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400036D RID: 877
		// (add) Token: 0x06003AC0 RID: 15040 RVA: 0x000F0E58 File Offset: 0x000EF058
		// (remove) Token: 0x06003AC1 RID: 15041 RVA: 0x000F0E6C File Offset: 0x000EF06C
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.TextUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.TextUpdateEvent, value);
			}
		}

		/// <summary>Gets or sets the custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripComboBox.AutoCompleteSource" /> property is set to <see cref="F:System.Windows.Forms.AutoCompleteSource.CustomSource" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> that contains the strings.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000F0E80 File Offset: 0x000EF080
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000F0E90 File Offset: 0x000EF090
		[Localizable(true)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				return this.ComboBox.AutoCompleteCustomSource;
			}
			set
			{
				this.ComboBox.AutoCompleteCustomSource = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the text completion behavior of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000F0EA0 File Offset: 0x000EF0A0
		// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000F0EB0 File Offset: 0x000EF0B0
		[DefaultValue(AutoCompleteMode.None)]
		[EditorBrowsable(0)]
		[Browsable(true)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.ComboBox.AutoCompleteMode;
			}
			set
			{
				this.ComboBox.AutoCompleteMode = value;
			}
		}

		/// <summary>Gets or sets the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000F0EC0 File Offset: 0x000EF0C0
		// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x000F0ED0 File Offset: 0x000EF0D0
		[EditorBrowsable(0)]
		[Browsable(true)]
		[DefaultValue(AutoCompleteSource.None)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.ComboBox.AutoCompleteSource;
			}
			set
			{
				this.ComboBox.AutoCompleteSource = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000F0EE0 File Offset: 0x000EF0E0
		// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000F0EE8 File Offset: 0x000EF0E8
		[Browsable(false)]
		[EditorBrowsable(1)]
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
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000F0EF4 File Offset: 0x000EF0F4
		// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000F0EFC File Offset: 0x000EF0FC
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ComboBox" /> in which the user can enter text, along with a list from which the user can select.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ComboBox" /> for a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000F0F08 File Offset: 0x000EF108
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ComboBox ComboBox
		{
			get
			{
				return (ComboBox)base.Control;
			}
		}

		/// <summary>Gets or sets the height, in pixels, of the drop-down portion box of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000F0F18 File Offset: 0x000EF118
		// (set) Token: 0x06003ACE RID: 15054 RVA: 0x000F0F28 File Offset: 0x000EF128
		[DefaultValue(106)]
		[EditorBrowsable(0)]
		[Browsable(true)]
		public int DropDownHeight
		{
			get
			{
				return this.ComboBox.DropDownHeight;
			}
			set
			{
				this.ComboBox.DropDownHeight = value;
			}
		}

		/// <summary>Gets or sets a value specifying the style of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is <see cref="F:System.Windows.Forms.ComboBoxStyle.DropDown" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x000F0F38 File Offset: 0x000EF138
		// (set) Token: 0x06003AD0 RID: 15056 RVA: 0x000F0F48 File Offset: 0x000EF148
		[DefaultValue(ComboBoxStyle.DropDown)]
		[RefreshProperties(2)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this.ComboBox.DropDownStyle;
			}
			set
			{
				this.ComboBox.DropDownStyle = value;
			}
		}

		/// <summary>Gets or sets the width, in pixels, of the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000F0F58 File Offset: 0x000EF158
		// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x000F0F68 File Offset: 0x000EF168
		public int DropDownWidth
		{
			get
			{
				return this.ComboBox.DropDownWidth;
			}
			set
			{
				this.ComboBox.DropDownWidth = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000F0F78 File Offset: 0x000EF178
		// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x000F0F88 File Offset: 0x000EF188
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool DroppedDown
		{
			get
			{
				return this.ComboBox.DroppedDown;
			}
			set
			{
				this.ComboBox.DroppedDown = value;
			}
		}

		/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. The options are <see cref="F:System.Windows.Forms.FlatStyle.Flat" />, <see cref="F:System.Windows.Forms.FlatStyle.Popup" />, <see cref="F:System.Windows.Forms.FlatStyle.Standard" />, and <see cref="F:System.Windows.Forms.FlatStyle.System" />. The default is <see cref="F:System.Windows.Forms.FlatStyle.Popup" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000F0F98 File Offset: 0x000EF198
		// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x000F0FA8 File Offset: 0x000EF1A8
		[DefaultValue(FlatStyle.Popup)]
		[Localizable(true)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.ComboBox.FlatStyle;
			}
			set
			{
				this.ComboBox.FlatStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> should resize to avoid showing partial items.</summary>
		/// <returns>true if the list portion can contain only complete items; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000F0FB8 File Offset: 0x000EF1B8
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000F0FC8 File Offset: 0x000EF1C8
		[DefaultValue(true)]
		[Localizable(true)]
		public bool IntegralHeight
		{
			get
			{
				return this.ComboBox.IntegralHeight;
			}
			set
			{
				this.ComboBox.IntegralHeight = value;
			}
		}

		/// <summary>Gets a collection of the items contained in this <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>A collection of items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000F0FD8 File Offset: 0x000EF1D8
		[DesignerSerializationVisibility(2)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this.ComboBox.Items;
			}
		}

		/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The maximum number of items in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000F0FE8 File Offset: 0x000EF1E8
		// (set) Token: 0x06003ADB RID: 15067 RVA: 0x000F0FF8 File Offset: 0x000EF1F8
		[Localizable(true)]
		[DefaultValue(8)]
		public int MaxDropDownItems
		{
			get
			{
				return this.ComboBox.MaxDropDownItems;
			}
			set
			{
				this.ComboBox.MaxDropDownItems = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters allowed in the editable portion of a combo box.</summary>
		/// <returns>The maximum number of characters the user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000F1008 File Offset: 0x000EF208
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x000F1018 File Offset: 0x000EF218
		[Localizable(true)]
		[DefaultValue(0)]
		public int MaxLength
		{
			get
			{
				return this.ComboBox.MaxLength;
			}
			set
			{
				this.ComboBox.MaxLength = value;
			}
		}

		/// <summary>Gets or sets the index specifying the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000F1028 File Offset: 0x000EF228
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x000F1038 File Offset: 0x000EF238
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int SelectedIndex
		{
			get
			{
				return this.ComboBox.SelectedIndex;
			}
			set
			{
				this.ComboBox.SelectedIndex = value;
				if (this.ComboBox.SelectedIndex >= 0)
				{
					this.Text = this.Items[value].ToString();
				}
			}
		}

		/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The object that is the currently selected item or null if there is no currently selected item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000F107C File Offset: 0x000EF27C
		// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x000F108C File Offset: 0x000EF28C
		[Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public object SelectedItem
		{
			get
			{
				return this.ComboBox.SelectedItem;
			}
			set
			{
				this.ComboBox.SelectedItem = value;
			}
		}

		/// <summary>Gets or sets the text that is selected in the editable portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>A string that represents the currently selected text in the combo box. If <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> is set to DropDownList, the return value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000F109C File Offset: 0x000EF29C
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x000F10AC File Offset: 0x000EF2AC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string SelectedText
		{
			get
			{
				return this.ComboBox.SelectedText;
			}
			set
			{
				this.ComboBox.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the number of characters selected in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The number of characters selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000F10BC File Offset: 0x000EF2BC
		// (set) Token: 0x06003AE5 RID: 15077 RVA: 0x000F10CC File Offset: 0x000EF2CC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionLength
		{
			get
			{
				return this.ComboBox.SelectionLength;
			}
			set
			{
				this.ComboBox.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets the starting index of text selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000F10DC File Offset: 0x000EF2DC
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000F10EC File Offset: 0x000EF2EC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionStart
		{
			get
			{
				return this.ComboBox.SelectionStart;
			}
			set
			{
				this.ComboBox.SelectionStart = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> are sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000F10FC File Offset: 0x000EF2FC
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000F110C File Offset: 0x000EF30C
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				return this.ComboBox.Sorted;
			}
			set
			{
				this.ComboBox.Sorted = value;
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000F111C File Offset: 0x000EF31C
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(1, 0, 1, 0);
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 x 20 pixels.</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000F1128 File Offset: 0x000EF328
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> one at a time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000F1134 File Offset: 0x000EF334
		public void BeginUpdate()
		{
			this.ComboBox.BeginUpdate();
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ToolStripComboBox.BeginUpdate" /> method.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AED RID: 15085 RVA: 0x000F1144 File Offset: 0x000EF344
		public void EndUpdate()
		{
			this.ComboBox.EndUpdate();
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that starts with the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003AEE RID: 15086 RVA: 0x000F1154 File Offset: 0x000EF354
		public int FindString(string s)
		{
			return this.ComboBox.FindString(s);
		}

		/// <summary>Finds the first item after the given index which starts with the given string. </summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003AEF RID: 15087 RVA: 0x000F1164 File Offset: 0x000EF364
		public int FindString(string s, int startIndex)
		{
			return this.ComboBox.FindString(s, startIndex);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that exactly matches the specified string.</summary>
		/// <returns>The zero-based index of the first item found; -1 if no match is found.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003AF0 RID: 15088 RVA: 0x000F1174 File Offset: 0x000EF374
		public int FindStringExact(string s)
		{
			return this.ComboBox.FindStringExact(s);
		}

		/// <summary>Finds the first item after the specified index that exactly matches the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003AF1 RID: 15089 RVA: 0x000F1184 File Offset: 0x000EF384
		public int FindStringExact(string s, int startIndex)
		{
			return this.ComboBox.FindStringExact(s, startIndex);
		}

		/// <summary>Returns the height, in pixels, of an item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		/// <param name="index">The index of the item to return the height of.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F1194 File Offset: 0x000EF394
		public int GetItemHeight(int index)
		{
			return this.ComboBox.GetItemHeight(index);
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F11A4 File Offset: 0x000EF3A4
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return base.GetPreferredSize(constrainingSize);
		}

		/// <summary>Selects a range of text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> is less than zero.-or- <paramref name="start" /> minus <paramref name="length" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F11B0 File Offset: 0x000EF3B0
		public void Select(int start, int length)
		{
			this.ComboBox.Select(start, length);
		}

		/// <summary>Selects all the text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F11C0 File Offset: 0x000EF3C0
		public void SelectAll()
		{
			this.ComboBox.SelectAll();
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F11D0 File Offset: 0x000EF3D0
		public override string ToString()
		{
			return this.ComboBox.ToString();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDown" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AF7 RID: 15095 RVA: 0x000F11E0 File Offset: 0x000EF3E0
		protected virtual void OnDropDown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripComboBox.DropDownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AF8 RID: 15096 RVA: 0x000F1214 File Offset: 0x000EF414
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripComboBox.DropDownClosedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AF9 RID: 15097 RVA: 0x000F1248 File Offset: 0x000EF448
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripComboBox.DropDownStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AFA RID: 15098 RVA: 0x000F127C File Offset: 0x000EF47C
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripComboBox.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AFB RID: 15099 RVA: 0x000F12B0 File Offset: 0x000EF4B0
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
		}

		/// <summary>Subscribes events from the specified control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06003AFC RID: 15100 RVA: 0x000F12B4 File Offset: 0x000EF4B4
		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
			this.ComboBox.DropDown += new EventHandler(this.HandleDropDown);
			this.ComboBox.DropDownClosed += new EventHandler(this.HandleDropDownClosed);
			this.ComboBox.DropDownStyleChanged += new EventHandler(this.HandleDropDownStyleChanged);
			this.ComboBox.SelectedIndexChanged += new EventHandler(this.HandleSelectedIndexChanged);
			this.ComboBox.TextChanged += new EventHandler(this.HandleTextChanged);
			this.ComboBox.TextUpdate += new EventHandler(this.HandleTextUpdate);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.TextUpdate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003AFD RID: 15101 RVA: 0x000F1354 File Offset: 0x000EF554
		protected virtual void OnTextUpdate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripComboBox.TextUpdateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Unsubscribes events from the specified control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06003AFE RID: 15102 RVA: 0x000F1388 File Offset: 0x000EF588
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000F1394 File Offset: 0x000EF594
		private void HandleDropDown(object sender, EventArgs e)
		{
			this.OnDropDown(e);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000F13A0 File Offset: 0x000EF5A0
		private void HandleDropDownClosed(object sender, EventArgs e)
		{
			this.OnDropDownClosed(e);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000F13AC File Offset: 0x000EF5AC
		private void HandleDropDownStyleChanged(object sender, EventArgs e)
		{
			this.OnDropDownStyleChanged(e);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000F13B8 File Offset: 0x000EF5B8
		private void HandleSelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000F13C4 File Offset: 0x000EF5C4
		private void HandleTextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(e);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000F13D0 File Offset: 0x000EF5D0
		private void HandleTextUpdate(object sender, EventArgs e)
		{
			this.OnTextUpdate(e);
		}

		// Token: 0x02000341 RID: 833
		private class ToolStripComboBoxControl : ComboBox
		{
			// Token: 0x06003B05 RID: 15109 RVA: 0x000F13DC File Offset: 0x000EF5DC
			public ToolStripComboBoxControl()
			{
				this.border_style = BorderStyle.None;
				base.FlatStyle = FlatStyle.Popup;
			}
		}
	}
}
