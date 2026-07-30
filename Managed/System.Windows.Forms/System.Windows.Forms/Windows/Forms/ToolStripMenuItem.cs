using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable option displayed on a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000367 RID: 871
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripMenuItemCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ToolStripMenuItem : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class.</summary>
		// Token: 0x06003E83 RID: 16003 RVA: 0x000F9C64 File Offset: 0x000F7E64
		public ToolStripMenuItem()
			: this(null, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		// Token: 0x06003E84 RID: 16004 RVA: 0x000F9C74 File Offset: 0x000F7E74
		public ToolStripMenuItem(Image image)
			: this(null, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		// Token: 0x06003E85 RID: 16005 RVA: 0x000F9C84 File Offset: 0x000F7E84
		public ToolStripMenuItem(string text)
			: this(text, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		// Token: 0x06003E86 RID: 16006 RVA: 0x000F9C94 File Offset: 0x000F7E94
		public ToolStripMenuItem(string text, Image image)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		// Token: 0x06003E87 RID: 16007 RVA: 0x000F9CA4 File Offset: 0x000F7EA4
		public ToolStripMenuItem(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that contains the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> collection.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="dropDownItems">The menu items to display when the control is clicked.</param>
		// Token: 0x06003E88 RID: 16008 RVA: 0x000F9CB4 File Offset: 0x000F7EB4
		public ToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems)
			: this(text, image, null, string.Empty)
		{
			if (dropDownItems != null)
			{
				foreach (ToolStripItem toolStripItem in dropDownItems)
				{
					base.DropDownItems.Add(toolStripItem);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image, does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked, and displays the specified shortcut keys.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		/// <param name="shortcutKeys">One of the values of <see cref="T:System.Windows.Forms.Keys" /> that represents the shortcut key for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		// Token: 0x06003E89 RID: 16009 RVA: 0x000F9CFC File Offset: 0x000F7EFC
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class with the specified name that displays the specified text and image that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		/// <param name="name">The name of the menu item.</param>
		// Token: 0x06003E8A RID: 16010 RVA: 0x000F9D0C File Offset: 0x000F7F0C
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			base.Overflow = ToolStripItemOverflow.Never;
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000F9D28 File Offset: 0x000F7F28
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripMenuItem()
		{
			ToolStripMenuItem.CheckedChangedEvent = new object();
			ToolStripMenuItem.CheckStateChangedEvent = new object();
			ToolStripMenuItem.UIACheckOnClickChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.Checked" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003C7 RID: 967
		// (add) Token: 0x06003E8C RID: 16012 RVA: 0x000F9D48 File Offset: 0x000F7F48
		// (remove) Token: 0x06003E8D RID: 16013 RVA: 0x000F9D5C File Offset: 0x000F7F5C
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.CheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.CheckedChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003C8 RID: 968
		// (add) Token: 0x06003E8E RID: 16014 RVA: 0x000F9D70 File Offset: 0x000F7F70
		// (remove) Token: 0x06003E8F RID: 16015 RVA: 0x000F9D84 File Offset: 0x000F7F84
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.CheckStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.CheckStateChangedEvent, value);
			}
		}

		// Token: 0x140003C9 RID: 969
		// (add) Token: 0x06003E90 RID: 16016 RVA: 0x000F9D98 File Offset: 0x000F7F98
		// (remove) Token: 0x06003E91 RID: 16017 RVA: 0x000F9DAC File Offset: 0x000F7FAC
		internal event EventHandler UIACheckOnClickChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.UIACheckOnClickChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.UIACheckOnClickChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked or is in an indeterminate state; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000F9DC0 File Offset: 0x000F7FC0
		// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000F9DF0 File Offset: 0x000F7FF0
		[Bindable(true)]
		[DefaultValue(false)]
		[RefreshProperties(1)]
		public bool Checked
		{
			get
			{
				switch (this.checked_state)
				{
				default:
					return false;
				case CheckState.Checked:
				case CheckState.Indeterminate:
					return true;
				}
			}
			set
			{
				this.CheckState = ((!value) ? CheckState.Unchecked : CheckState.Checked);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked and unchecked when clicked.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked when clicked; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000F9E08 File Offset: 0x000F8008
		// (set) Token: 0x06003E95 RID: 16021 RVA: 0x000F9E10 File Offset: 0x000F8010
		[DefaultValue(false)]
		public bool CheckOnClick
		{
			get
			{
				return this.check_on_click;
			}
			set
			{
				if (this.check_on_click != value)
				{
					this.check_on_click = value;
					this.OnUIACheckOnClickChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is in the checked, unchecked, or indeterminate state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is Unchecked.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property is not set to one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x000F9E30 File Offset: 0x000F8030
		// (set) Token: 0x06003E97 RID: 16023 RVA: 0x000F9E38 File Offset: 0x000F8038
		[RefreshProperties(1)]
		[Bindable(true)]
		[DefaultValue(CheckState.Unchecked)]
		public CheckState CheckState
		{
			get
			{
				return this.checked_state;
			}
			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for CheckState", value));
				}
				if (value == this.checked_state)
				{
					return;
				}
				this.checked_state = value;
				base.Invalidate();
				this.OnCheckedChanged(EventArgs.Empty);
				this.OnCheckStateChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is enabled. </summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x000F9EA8 File Offset: 0x000F80A8
		// (set) Token: 0x06003E99 RID: 16025 RVA: 0x000F9EB0 File Offset: 0x000F80B0
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a multiple document interface (MDI) window list.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a MDI window list; otherwise, false.</returns>
		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06003E9A RID: 16026 RVA: 0x000F9EBC File Offset: 0x000F80BC
		[Browsable(false)]
		public bool IsMdiWindowListEntry
		{
			get
			{
				return this.mdi_client_form != null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or whether it can float between the two.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is Never.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000F9ECC File Offset: 0x000F80CC
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000F9ED4 File Offset: 0x000F80D4
		[DefaultValue(ToolStripItemOverflow.Never)]
		public new ToolStripItemOverflow Overflow
		{
			get
			{
				return base.Overflow;
			}
			set
			{
				base.Overflow = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the shortcut keys that are associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> are displayed next to the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. </summary>
		/// <returns>true if the shortcut keys are shown; otherwise, false. The default is true.</returns>
		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000F9EE0 File Offset: 0x000F80E0
		// (set) Token: 0x06003E9E RID: 16030 RVA: 0x000F9EE8 File Offset: 0x000F80E8
		[DefaultValue(true)]
		[Localizable(true)]
		public bool ShowShortcutKeys
		{
			get
			{
				return this.show_shortcut_keys;
			}
			set
			{
				this.show_shortcut_keys = value;
			}
		}

		/// <summary>Gets or sets the shortcut key text.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the shortcut key.</returns>
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000F9EF4 File Offset: 0x000F80F4
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x000F9EFC File Offset: 0x000F80FC
		[Localizable(true)]
		[DefaultValue(null)]
		public string ShortcutKeyDisplayString
		{
			get
			{
				return this.shortcut_display_string;
			}
			set
			{
				this.shortcut_display_string = value;
			}
		}

		/// <summary>Gets or sets the shortcut keys associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property was not set to one of the <see cref="T:System.Windows.Forms.Keys" /> values.</exception>
		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x000F9F08 File Offset: 0x000F8108
		// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x000F9F10 File Offset: 0x000F8110
		[DefaultValue(Keys.None)]
		[Localizable(true)]
		public Keys ShortcutKeys
		{
			get
			{
				return this.shortcut_keys;
			}
			set
			{
				if (this.shortcut_keys != value)
				{
					this.shortcut_keys = value;
					if (base.Parent != null)
					{
						ToolStripManager.AddToolStripMenuItem(this);
					}
				}
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000F9F44 File Offset: 0x000F8144
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		/// <summary>Gets the internal spacing within the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000F9F4C File Offset: 0x000F814C
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(4, 0, 4, 0);
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000F9F58 File Offset: 0x000F8158
		protected override Size DefaultSize
		{
			get
			{
				return new Size(32, 19);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x06003EA6 RID: 16038 RVA: 0x000F9F64 File Offset: 0x000F8164
		[EditorBrowsable(2)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripMenuItem.ToolStripMenuItemAccessibleObject();
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x06003EA7 RID: 16039 RVA: 0x000F9F6C File Offset: 0x000F816C
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu
			{
				OwnerItem = this
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003EA8 RID: 16040 RVA: 0x000F9F88 File Offset: 0x000F8188
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003EA9 RID: 16041 RVA: 0x000F9F94 File Offset: 0x000F8194
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003EAA RID: 16042 RVA: 0x000F9FC8 File Offset: 0x000F81C8
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003EAB RID: 16043 RVA: 0x000F9FFC File Offset: 0x000F81FC
		protected override void OnClick(EventArgs e)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (this.HasDropDownItems)
			{
				base.OnClick(e);
				return;
			}
			if (base.OwnerItem is ToolStripDropDownItem)
			{
				(base.OwnerItem as ToolStripDropDownItem).OnDropDownItemClicked(new ToolStripItemClickedEventArgs(this));
			}
			if (base.IsOnDropDown)
			{
				ToolStrip topLevelToolStrip = this.GetTopLevelToolStrip();
				if (topLevelToolStrip != null)
				{
					topLevelToolStrip.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				}
			}
			if (this.IsMdiWindowListEntry)
			{
				this.mdi_client_form.MdiParent.MdiContainer.ActivateChild(this.mdi_client_form);
				return;
			}
			if (this.check_on_click)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			if (!base.IsOnDropDown && !this.HasDropDownItems)
			{
				ToolStrip topLevelToolStrip2 = this.GetTopLevelToolStrip();
				if (topLevelToolStrip2 != null)
				{
					topLevelToolStrip2.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				}
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003EAC RID: 16044 RVA: 0x000FA0E0 File Offset: 0x000F82E0
		protected override void OnDropDownHide(EventArgs e)
		{
			base.OnDropDownHide(e);
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003EAD RID: 16045 RVA: 0x000FA0EC File Offset: 0x000F82EC
		protected override void OnDropDownShow(EventArgs e)
		{
			base.OnDropDownShow(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003EAE RID: 16046 RVA: 0x000FA0F8 File Offset: 0x000F82F8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003EAF RID: 16047 RVA: 0x000FA104 File Offset: 0x000F8304
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!base.IsOnDropDown && this.HasDropDownItems && base.DropDown.Visible)
			{
				this.close_on_mouse_release = true;
			}
			if (this.Enabled && !base.DropDown.Visible)
			{
				base.ShowDropDown();
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000FA168 File Offset: 0x000F8368
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.IsOnDropDown && this.HasDropDownItems && this.Enabled)
			{
				base.ShowDropDown();
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000FA1A4 File Offset: 0x000F83A4
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06003EB2 RID: 16050 RVA: 0x000FA1B0 File Offset: 0x000F83B0
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.close_on_mouse_release)
			{
				base.DropDown.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				base.Invalidate();
				this.close_on_mouse_release = false;
				if (!base.IsOnDropDown && base.Parent is MenuStrip)
				{
					(base.Parent as MenuStrip).MenuDroppedDown = false;
				}
			}
			if (!this.HasDropDownItems && this.Enabled)
			{
				base.OnMouseUp(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003EB3 RID: 16051 RVA: 0x000FA22C File Offset: 0x000F842C
		protected override void OnOwnerChanged(EventArgs e)
		{
			base.OnOwnerChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003EB4 RID: 16052 RVA: 0x000FA238 File Offset: 0x000F8438
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner == null)
			{
				return;
			}
			Image image = ((!base.UseImageMargin) ? null : this.Image);
			Color color = this.ForeColor;
			if ((this.Selected || this.Pressed) && base.IsOnDropDown && color == SystemColors.MenuText)
			{
				color = SystemColors.HighlightText;
			}
			if (!this.Enabled && this.ForeColor == SystemColors.ControlText)
			{
				color = SystemColors.GrayText;
			}
			image = ((!this.Enabled) ? ToolStripRenderer.CreateDisabledImage(image) : image);
			base.Owner.Renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			Rectangle rectangle;
			Rectangle empty;
			base.CalculateTextAndImageRectangles(out rectangle, out empty);
			if (base.IsOnDropDown)
			{
				if (!base.UseImageMargin)
				{
					empty = Rectangle.Empty;
					rectangle..ctor(8, rectangle.Top, rectangle.Width, rectangle.Height);
				}
				else
				{
					rectangle..ctor(35, rectangle.Top, rectangle.Width, rectangle.Height);
					if (empty != Rectangle.Empty)
					{
						empty..ctor(new Point(4, 3), base.GetImageSize());
					}
				}
				if (this.Checked && base.ShowMargin)
				{
					base.Owner.Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, new Rectangle(2, 1, 19, 19)));
				}
			}
			if (rectangle != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
			}
			string shortcutDisplayString = this.GetShortcutDisplayString();
			if (!string.IsNullOrEmpty(shortcutDisplayString) && !this.HasDropDownItems)
			{
				int num = 15;
				Size size = TextRenderer.MeasureText(shortcutDisplayString, this.Font);
				Rectangle rectangle2;
				rectangle2..ctor(base.ContentRectangle.Right - size.Width - num, rectangle.Top, size.Width, rectangle.Height);
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, shortcutDisplayString, rectangle2, color, this.Font, this.TextAlign));
			}
			if (empty != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, empty));
			}
			if (base.IsOnDropDown && this.HasDropDownItems && base.Parent is ToolStripDropDownMenu)
			{
				base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(this.Bounds.Width - 17, 2, 10, 20), Color.Black, ArrowDirection.Right));
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, which represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003EB5 RID: 16053 RVA: 0x000FA538 File Offset: 0x000F8738
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			Control control = Control.FromHandle(m.HWnd);
			Form form = ((control != null) ? ((Form)control.TopLevelControl) : null);
			if (this.Enabled && keyData == this.shortcut_keys && this.GetTopLevelControl() == form)
			{
				base.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000FA5A4 File Offset: 0x000F87A4
		private Control GetTopLevelControl()
		{
			ToolStripItem toolStripItem = this;
			while (toolStripItem.OwnerItem != null)
			{
				toolStripItem = toolStripItem.OwnerItem;
			}
			if (toolStripItem.Owner == null)
			{
				return null;
			}
			if (toolStripItem.Owner is ContextMenuStrip)
			{
				Control container = ((ContextMenuStrip)toolStripItem.Owner).container;
				return (container != null) ? container.TopLevelControl : null;
			}
			return toolStripItem.Owner.TopLevelControl;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06003EB7 RID: 16055 RVA: 0x000FA618 File Offset: 0x000F8818
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.Selected)
			{
				base.Parent.ChangeSelection(this);
			}
			if (this.HasDropDownItems)
			{
				ToolStripManager.SetActiveToolStrip(base.Parent, true);
				base.ShowDropDown();
				base.DropDown.SelectNextToolStripItem(null, true);
			}
			else
			{
				base.PerformClick();
			}
			return true;
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		// Token: 0x06003EB8 RID: 16056 RVA: 0x000FA674 File Offset: 0x000F8874
		protected internal override void SetBounds(Rectangle rect)
		{
			base.SetBounds(rect);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000FA680 File Offset: 0x000F8880
		internal void OnUIACheckOnClickChangedEvent(EventArgs args)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.UIACheckOnClickChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, args);
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000FA6B4 File Offset: 0x000F88B4
		// (set) Token: 0x06003EBB RID: 16059 RVA: 0x000FA6BC File Offset: 0x000F88BC
		internal Form MdiClientForm
		{
			get
			{
				return this.mdi_client_form;
			}
			set
			{
				this.mdi_client_form = value;
			}
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x000FA6C8 File Offset: 0x000F88C8
		internal override Size CalculatePreferredSize(Size constrainingSize)
		{
			Size size = base.CalculatePreferredSize(constrainingSize);
			string shortcutDisplayString = this.GetShortcutDisplayString();
			if (string.IsNullOrEmpty(shortcutDisplayString))
			{
				return size;
			}
			Size size2 = TextRenderer.MeasureText(shortcutDisplayString, this.Font);
			return new Size(size.Width + size2.Width - 25, size.Height);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000FA71C File Offset: 0x000F891C
		internal string GetShortcutDisplayString()
		{
			if (!this.show_shortcut_keys)
			{
				return string.Empty;
			}
			if (base.Parent == null || !(base.Parent is ToolStripDropDownMenu))
			{
				return string.Empty;
			}
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.shortcut_display_string))
			{
				text = this.shortcut_display_string;
			}
			else if (this.shortcut_keys != Keys.None)
			{
				KeysConverter keysConverter = new KeysConverter();
				text = keysConverter.ConvertToString(this.shortcut_keys);
			}
			return text;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x000FA7A4 File Offset: 0x000F89A4
		internal void HandleAutoExpansion()
		{
			if (this.HasDropDownItems)
			{
				base.ShowDropDown();
				base.DropDown.SelectNextToolStripItem(null, true);
			}
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000FA7D0 File Offset: 0x000F89D0
		internal override void HandleClick(EventArgs e)
		{
			this.OnClick(e);
			if (base.Parent != null)
			{
				base.Parent.Invalidate();
			}
		}

		// Token: 0x04001B24 RID: 6948
		private CheckState checked_state;

		// Token: 0x04001B25 RID: 6949
		private bool check_on_click;

		// Token: 0x04001B26 RID: 6950
		private bool close_on_mouse_release;

		// Token: 0x04001B27 RID: 6951
		private string shortcut_display_string;

		// Token: 0x04001B28 RID: 6952
		private Keys shortcut_keys;

		// Token: 0x04001B29 RID: 6953
		private bool show_shortcut_keys = true;

		// Token: 0x04001B2A RID: 6954
		private Form mdi_client_form;

		// Token: 0x02000368 RID: 872
		private class ToolStripMenuItemAccessibleObject : AccessibleObject
		{
		}
	}
}
