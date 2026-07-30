using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that can contain text and images. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033F RID: 831
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripButton : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class.</summary>
		// Token: 0x06003A94 RID: 14996 RVA: 0x000F08A8 File Offset: 0x000EEAA8
		public ToolStripButton()
			: this(null, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified image.</summary>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003A95 RID: 14997 RVA: 0x000F08B8 File Offset: 0x000EEAB8
		public ToolStripButton(Image image)
			: this(null, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003A96 RID: 14998 RVA: 0x000F08C8 File Offset: 0x000EEAC8
		public ToolStripButton(string text)
			: this(text, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003A97 RID: 14999 RVA: 0x000F08D8 File Offset: 0x000EEAD8
		public ToolStripButton(string text, Image image)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		// Token: 0x06003A98 RID: 15000 RVA: 0x000F08E8 File Offset: 0x000EEAE8
		public ToolStripButton(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class with the specified name that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003A99 RID: 15001 RVA: 0x000F08F8 File Offset: 0x000EEAF8
		public ToolStripButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.checked_state = CheckState.Unchecked;
			base.ToolTipText = string.Empty;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F0918 File Offset: 0x000EEB18
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripButton()
		{
			ToolStripButton.CheckedChangedEvent = new object();
			ToolStripButton.CheckStateChangedEvent = new object();
			ToolStripButton.UIACheckOnClickChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.Checked" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000365 RID: 869
		// (add) Token: 0x06003A9B RID: 15003 RVA: 0x000F0938 File Offset: 0x000EEB38
		// (remove) Token: 0x06003A9C RID: 15004 RVA: 0x000F094C File Offset: 0x000EEB4C
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.CheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.CheckedChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.CheckState" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000366 RID: 870
		// (add) Token: 0x06003A9D RID: 15005 RVA: 0x000F0960 File Offset: 0x000EEB60
		// (remove) Token: 0x06003A9E RID: 15006 RVA: 0x000F0974 File Offset: 0x000EEB74
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.CheckStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.CheckStateChangedEvent, value);
			}
		}

		// Token: 0x14000367 RID: 871
		// (add) Token: 0x06003A9F RID: 15007 RVA: 0x000F0988 File Offset: 0x000EEB88
		// (remove) Token: 0x06003AA0 RID: 15008 RVA: 0x000F099C File Offset: 0x000EEB9C
		internal event EventHandler UIACheckOnClickChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.UIACheckOnClickChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.UIACheckOnClickChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripButton" />. </summary>
		/// <returns>true if default <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000F09B0 File Offset: 0x000EEBB0
		// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x000F09B8 File Offset: 0x000EEBB8
		[DefaultValue(true)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x000F09C4 File Offset: 0x000EEBC4
		public override bool CanSelect
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed or not pressed.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed in or not pressed in; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000F09C8 File Offset: 0x000EEBC8
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000F09F8 File Offset: 0x000EEBF8
		[DefaultValue(false)]
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
				if (this.checked_state != ((!value) ? CheckState.Unchecked : CheckState.Checked))
				{
					this.checked_state = ((!value) ? CheckState.Unchecked : CheckState.Checked);
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000F0A4C File Offset: 0x000EEC4C
		// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x000F0A54 File Offset: 0x000EEC54
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is in the pressed or not pressed state by default, or is in an indeterminate state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is Unchecked.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000F0A74 File Offset: 0x000EEC74
		// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000F0A7C File Offset: 0x000EEC7C
		[DefaultValue(CheckState.Unchecked)]
		public CheckState CheckState
		{
			get
			{
				return this.checked_state;
			}
			set
			{
				if (this.checked_state != value)
				{
					if (!Enum.IsDefined(typeof(CheckState), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for CheckState", value));
					}
					this.checked_state = value;
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the ToolTip that is defined as the default. </summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000F0AE8 File Offset: 0x000EECE8
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripButton" /> can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The specified area for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003AAB RID: 15019 RVA: 0x000F0AEC File Offset: 0x000EECEC
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			if (preferredSize.Width < 23)
			{
				preferredSize.Width = 23;
			}
			return preferredSize;
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</returns>
		// Token: 0x06003AAC RID: 15020 RVA: 0x000F0B1C File Offset: 0x000EED1C
		[EditorBrowsable(2)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this)
			{
				default_action = "Press",
				role = AccessibleRole.PushButton,
				state = AccessibleStates.Focusable
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003AAD RID: 15021 RVA: 0x000F0B50 File Offset: 0x000EED50
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003AAE RID: 15022 RVA: 0x000F0B84 File Offset: 0x000EED84
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003AAF RID: 15023 RVA: 0x000F0BB8 File Offset: 0x000EEDB8
		protected override void OnClick(EventArgs e)
		{
			if (this.check_on_click)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			ToolStrip topLevelToolStrip = this.GetTopLevelToolStrip();
			if (topLevelToolStrip != null)
			{
				topLevelToolStrip.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000F0BFC File Offset: 0x000EEDFC
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = ((!this.Enabled) ? SystemColors.GrayText : this.ForeColor);
				Image image = ((!this.Enabled) ? ToolStripRenderer.CreateDisabledImage(this.Image) : this.Image);
				base.Owner.Renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (rectangle != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
				}
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
				return;
			}
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000F0CF0 File Offset: 0x000EEEF0
		internal void OnUIACheckOnClickChangedEvent(EventArgs args)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.UIACheckOnClickChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, args);
			}
		}

		// Token: 0x04001A42 RID: 6722
		private CheckState checked_state;

		// Token: 0x04001A43 RID: 6723
		private bool check_on_click;
	}
}
