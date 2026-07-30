using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the menu structure of a form. Although <see cref="T:System.Windows.Forms.MenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MainMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.MainMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023C RID: 572
	[ToolboxItemFilter("System.Windows.Forms.MainMenu", 0)]
	public class MainMenu : Menu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class without any specified menu items.</summary>
		// Token: 0x0600253C RID: 9532 RVA: 0x0008CB5C File Offset: 0x0008AD5C
		public MainMenu()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that will be added to the <see cref="T:System.Windows.Forms.MainMenu" />. </param>
		// Token: 0x0600253D RID: 9533 RVA: 0x0008CB6C File Offset: 0x0008AD6C
		public MainMenu(MenuItem[] items)
			: base(items)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class with the specified container. </summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> representing the container of the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
		// Token: 0x0600253E RID: 9534 RVA: 0x0008CB7C File Offset: 0x0008AD7C
		public MainMenu(IContainer container)
			: this()
		{
			container.Add(this);
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x0008CB8C File Offset: 0x0008AD8C
		// Note: this type is marked as 'beforefieldinit'.
		static MainMenu()
		{
			MainMenu.CollapseEvent = new object();
			MainMenu.PaintEvent = new object();
		}

		/// <summary>Occurs when the main menu collapses.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000236 RID: 566
		// (add) Token: 0x06002540 RID: 9536 RVA: 0x0008CBA4 File Offset: 0x0008ADA4
		// (remove) Token: 0x06002541 RID: 9537 RVA: 0x0008CBB8 File Offset: 0x0008ADB8
		public event EventHandler Collapse
		{
			add
			{
				base.Events.AddHandler(MainMenu.CollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MainMenu.CollapseEvent, value);
			}
		}

		// Token: 0x14000237 RID: 567
		// (add) Token: 0x06002542 RID: 9538 RVA: 0x0008CBCC File Offset: 0x0008ADCC
		// (remove) Token: 0x06002543 RID: 9539 RVA: 0x0008CBE0 File Offset: 0x0008ADE0
		internal event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(MainMenu.PaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MainMenu.PaintEvent, value);
			}
		}

		/// <summary>Gets or sets whether the text displayed by the control is displayed from right to left.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x0008CBF4 File Offset: 0x0008ADF4
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x0008CBFC File Offset: 0x0008ADFC
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				return this.right_to_left;
			}
			set
			{
				this.right_to_left = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.MainMenu" /> that is a duplicate of the current <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the cloned menu.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002546 RID: 9542 RVA: 0x0008CC08 File Offset: 0x0008AE08
		public virtual MainMenu CloneMenu()
		{
			MainMenu mainMenu = new MainMenu();
			mainMenu.CloneMenu(this);
			return mainMenu;
		}

		/// <returns>A handle to the menu if the method succeeds; otherwise, null.</returns>
		// Token: 0x06002547 RID: 9543 RVA: 0x0008CC24 File Offset: 0x0008AE24
		protected override IntPtr CreateMenuHandle()
		{
			return IntPtr.Zero;
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002548 RID: 9544 RVA: 0x0008CC2C File Offset: 0x0008AE2C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Form" /> that contains this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that is the container for this control. Returns null if the <see cref="T:System.Windows.Forms.MainMenu" /> is not currently hosted on a form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002549 RID: 9545 RVA: 0x0008CC38 File Offset: 0x0008AE38
		public Form GetForm()
		{
			return this.form;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MainMenu" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600254A RID: 9546 RVA: 0x0008CC40 File Offset: 0x0008AE40
		public override string ToString()
		{
			return base.ToString() + ", GetForm: " + this.form;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MainMenu.Collapse" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600254B RID: 9547 RVA: 0x0008CC58 File Offset: 0x0008AE58
		protected internal virtual void OnCollapse(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MainMenu.CollapseEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0008CC8C File Offset: 0x0008AE8C
		internal void Draw()
		{
			Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
			this.Draw(paintEventArgs, base.Rect);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0008CCE4 File Offset: 0x0008AEE4
		internal void Draw(Rectangle rect)
		{
			if (this.Wnd.IsHandleCreated)
			{
				Point menuOrigin = XplatUI.GetMenuOrigin(this.Wnd.window.Handle);
				Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
				PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
				paintEventArgs.Graphics.SetClip(new Rectangle(rect.X + menuOrigin.X, rect.Y + menuOrigin.Y, rect.Width, rect.Height));
				this.Draw(paintEventArgs, base.Rect);
				XplatUI.PaintEventEnd(ref message, this.Wnd.window.Handle, false);
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0008CDB4 File Offset: 0x0008AFB4
		internal void Draw(PaintEventArgs pe)
		{
			this.Draw(pe, base.Rect);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0008CDC4 File Offset: 0x0008AFC4
		internal void Draw(PaintEventArgs pe, Rectangle rect)
		{
			if (!this.Wnd.IsHandleCreated)
			{
				return;
			}
			base.X = rect.X;
			base.Y = rect.Y;
			base.Height = base.Rect.Height;
			ThemeEngine.Current.DrawMenuBar(pe.Graphics, this, rect);
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[MainMenu.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, pe);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0008CE48 File Offset: 0x0008B048
		internal override void InvalidateItem(MenuItem item)
		{
			this.Draw(item.bounds);
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0008CE58 File Offset: 0x0008B058
		internal void SetForm(Form form)
		{
			this.form = form;
			this.Wnd = form;
			if (this.tracker == null)
			{
				this.tracker = new MenuTracker(this);
				this.tracker.GrabControl = form;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0008CE8C File Offset: 0x0008B08C
		internal override void OnMenuChanged(EventArgs e)
		{
			base.OnMenuChanged(EventArgs.Empty);
			if (this.form == null)
			{
				return;
			}
			Rectangle rect = base.Rect;
			base.Height = 0;
			if (!this.Wnd.IsHandleCreated)
			{
				return;
			}
			Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
			paintEventArgs.Graphics.SetClip(rect);
			this.Draw(paintEventArgs, rect);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0008CF20 File Offset: 0x0008B120
		internal void OnMouseDown(object window, MouseEventArgs args)
		{
			this.tracker.OnMouseDown(args);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0008CF30 File Offset: 0x0008B130
		internal void OnMouseMove(object window, MouseEventArgs e)
		{
			MouseEventArgs mouseEventArgs = new MouseEventArgs(e.Button, e.Clicks, Control.MousePosition.X, Control.MousePosition.Y, e.Delta);
			this.tracker.OnMotion(mouseEventArgs);
		}

		// Token: 0x040012E5 RID: 4837
		private RightToLeft right_to_left = RightToLeft.Inherit;

		// Token: 0x040012E6 RID: 4838
		private Form form;
	}
}
