using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the raw preview part of print previewing from a Windows Forms application, without any dialog boxes or buttons. Most <see cref="T:System.Windows.Forms.PrintPreviewControl" /> objects are found on <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> objects, but they do not have to be.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	[DefaultProperty("Document")]
	[ClassInterface(1)]
	public class PrintPreviewControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> class.</summary>
		// Token: 0x06002AC4 RID: 10948 RVA: 0x000A58CC File Offset: 0x000A3ACC
		public PrintPreviewControl()
		{
			this.autozoom = true;
			this.columns = 1;
			this.rows = 0;
			this.startPage = 1;
			this.BackColor = SystemColors.AppWorkspace;
			this.controller = new PreviewPrintController();
			this.vbar = new ImplicitVScrollBar();
			this.hbar = new ImplicitHScrollBar();
			this.vbar.Visible = false;
			this.hbar.Visible = false;
			this.vbar.ValueChanged += new EventHandler(this.VScrollBarValueChanged);
			this.hbar.ValueChanged += new EventHandler(this.HScrollBarValueChanged);
			base.SuspendLayout();
			base.Controls.AddImplicit(this.vbar);
			base.Controls.AddImplicit(this.hbar);
			base.ResumeLayout();
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000A59AC File Offset: 0x000A3BAC
		// Note: this type is marked as 'beforefieldinit'.
		static PrintPreviewControl()
		{
			PrintPreviewControl.StartPageChangedEvent = new object();
		}

		/// <summary>Occurs when the start page changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000280 RID: 640
		// (add) Token: 0x06002AC6 RID: 10950 RVA: 0x000A59B8 File Offset: 0x000A3BB8
		// (remove) Token: 0x06002AC7 RID: 10951 RVA: 0x000A59CC File Offset: 0x000A3BCC
		public event EventHandler StartPageChanged
		{
			add
			{
				base.Events.AddHandler(PrintPreviewControl.StartPageChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PrintPreviewControl.StartPageChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewControl.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000281 RID: 641
		// (add) Token: 0x06002AC8 RID: 10952 RVA: 0x000A59E0 File Offset: 0x000A3BE0
		// (remove) Token: 0x06002AC9 RID: 10953 RVA: 0x000A59EC File Offset: 0x000A3BEC
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

		/// <summary>Gets or sets a value indicating whether resizing the control or changing the number of pages shown automatically adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property.</summary>
		/// <returns>true if the changing the control size or number of pages adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000A59F8 File Offset: 0x000A3BF8
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x000A5A00 File Offset: 0x000A3C00
		[DefaultValue(true)]
		public bool AutoZoom
		{
			get
			{
				return this.autozoom;
			}
			set
			{
				this.autozoom = value;
				this.InvalidateLayout();
			}
		}

		/// <summary>Gets or sets the number of pages displayed horizontally across the screen.</summary>
		/// <returns>The number of pages displayed horizontally across the screen. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000A5A10 File Offset: 0x000A3C10
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x000A5A18 File Offset: 0x000A3C18
		[DefaultValue(1)]
		public int Columns
		{
			get
			{
				return this.columns;
			}
			set
			{
				this.columns = value;
				this.InvalidateLayout();
			}
		}

		/// <summary>Gets or sets a value indicating the document to preview.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x000A5A28 File Offset: 0x000A3C28
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x000A5A30 File Offset: 0x000A3C30
		[DefaultValue(null)]
		public PrintDocument Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000A5A3C File Offset: 0x000A3C3C
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x000A5A44 File Offset: 0x000A3C44
		[AmbientValue(RightToLeft.Inherit)]
		[Localizable(true)]
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

		/// <summary>Gets or sets the number of pages displayed vertically down the screen.</summary>
		/// <returns>The number of pages displayed vertically down the screen. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000A5A50 File Offset: 0x000A3C50
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x000A5A58 File Offset: 0x000A3C58
		[DefaultValue(1)]
		public int Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				this.rows = value;
				this.InvalidateLayout();
			}
		}

		/// <summary>Gets or sets the page number of the upper left page.</summary>
		/// <returns>The page number of the upper left page. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000A5A68 File Offset: 0x000A3C68
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x000A5A70 File Offset: 0x000A3C70
		[DefaultValue(0)]
		public int StartPage
		{
			get
			{
				return this.startPage;
			}
			set
			{
				if (value < 1)
				{
					return;
				}
				if (this.document != null && value + (this.Rows + 1) * this.Columns > this.page_infos.Length + 1)
				{
					value = this.page_infos.Length + 1 - (this.Rows + 1) * this.Columns;
					if (value < 1)
					{
						value = 1;
					}
				}
				int num = this.StartPage;
				this.startPage = value;
				if (num != this.startPage)
				{
					this.InvalidateLayout();
					this.OnStartPageChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000A5B04 File Offset: 0x000A3D04
		// (set) Token: 0x06002AD7 RID: 10967 RVA: 0x000A5B0C File Offset: 0x000A3D0C
		[Bindable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
		/// <returns>true if anti-aliasing is used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x000A5B18 File Offset: 0x000A3D18
		// (set) Token: 0x06002AD9 RID: 10969 RVA: 0x000A5B28 File Offset: 0x000A3D28
		[DefaultValue(false)]
		public bool UseAntiAlias
		{
			get
			{
				return this.controller.UseAntiAlias;
			}
			set
			{
				this.controller.UseAntiAlias = value;
			}
		}

		/// <summary>Gets or sets a value indicating how large the pages will appear.</summary>
		/// <returns>A value indicating how large the pages will appear. A value of 1.0 indicates full size.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than or equal to 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000A5B38 File Offset: 0x000A3D38
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x000A5B40 File Offset: 0x000A3D40
		[DefaultValue(0.3)]
		public double Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException("zoom");
				}
				this.autozoom = false;
				this.zoom = value;
				this.InvalidateLayout();
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000A5B7C File Offset: 0x000A3D7C
		internal void GeneratePreview()
		{
			if (this.document == null)
			{
				return;
			}
			try
			{
				if (this.page_infos == null)
				{
					if (this.document.PrintController == null || !(this.document.PrintController is PrintControllerWithStatusDialog))
					{
						this.document.PrintController = new PrintControllerWithStatusDialog(this.controller);
					}
					this.document.Print();
					this.page_infos = this.controller.GetPreviewPageInfo();
				}
				if (this.image_cache == null)
				{
					this.image_cache = new Image[this.page_infos.Length];
					if (this.page_infos.Length > 0)
					{
						this.image_size = ThemeEngine.Current.PrintPreviewControlGetPageSize(this);
						if (this.image_size.Width >= 0 && this.image_size.Width < this.page_infos[0].Image.Width && this.image_size.Height >= 0 && this.image_size.Height < this.page_infos[0].Image.Height)
						{
							for (int i = 0; i < this.page_infos.Length; i++)
							{
								this.image_cache[i] = new Bitmap(this.image_size.Width, this.image_size.Height);
								Graphics graphics = Graphics.FromImage(this.image_cache[i]);
								graphics.DrawImage(this.page_infos[i].Image, new Rectangle(new Point(0, 0), this.image_size), 0, 0, this.page_infos[i].Image.Width, this.page_infos[i].Image.Height, 2);
								graphics.Dispose();
							}
						}
					}
				}
				this.UpdateScrollBars();
			}
			catch (Exception ex)
			{
				this.page_infos = new PreviewPageInfo[0];
				this.image_cache = new Image[0];
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>Refreshes the preview of the document.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ADD RID: 10973 RVA: 0x000A5D90 File Offset: 0x000A3F90
		public void InvalidatePreview()
		{
			if (this.page_infos != null)
			{
				for (int i = 0; i < this.page_infos.Length; i++)
				{
					if (this.page_infos[i].Image != null)
					{
						this.page_infos[i].Image.Dispose();
					}
				}
				this.page_infos = null;
			}
			this.InvalidateLayout();
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to <see cref="P:System.Drawing.SystemColors.AppWorkspace" />, which is the default color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ADE RID: 10974 RVA: 0x000A5DF4 File Offset: 0x000A3FF4
		[EditorBrowsable(1)]
		public override void ResetBackColor()
		{
			base.ResetBackColor();
		}

		/// <summary>Resets the foreground color of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> to <see cref="P:System.Drawing.Color.White" />, which is the default color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ADF RID: 10975 RVA: 0x000A5DFC File Offset: 0x000A3FFC
		[EditorBrowsable(1)]
		public override void ResetForeColor()
		{
			base.ResetForeColor();
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000A5E04 File Offset: 0x000A4004
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> method.</summary>
		// Token: 0x06002AE1 RID: 10977 RVA: 0x000A5E0C File Offset: 0x000A400C
		protected override void OnPaint(PaintEventArgs pevent)
		{
			if (this.page_infos == null || this.image_cache == null)
			{
				this.GeneratePreview();
			}
			ThemeEngine.Current.PrintPreviewControlPaint(pevent, this, this.image_size);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AE2 RID: 10978 RVA: 0x000A5E48 File Offset: 0x000A4048
		protected override void OnResize(EventArgs eventargs)
		{
			this.InvalidateLayout();
			base.OnResize(eventargs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PrintPreviewControl.StartPageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002AE3 RID: 10979 RVA: 0x000A5E58 File Offset: 0x000A4058
		protected virtual void OnStartPageChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PrintPreviewControl.StartPageChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		// Token: 0x06002AE4 RID: 10980 RVA: 0x000A5E8C File Offset: 0x000A408C
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000A5E98 File Offset: 0x000A4098
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.vbar;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000A5EA0 File Offset: 0x000A40A0
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.hbar;
			}
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000A5EA8 File Offset: 0x000A40A8
		private void VScrollBarValueChanged(object sender, EventArgs e)
		{
			int num;
			if (this.vbar.Value > this.vbar_value)
			{
				num = -1 * (this.vbar.Value - this.vbar_value);
			}
			else
			{
				num = this.vbar_value - this.vbar.Value;
			}
			this.vbar_value = this.vbar.Value;
			XplatUI.ScrollWindow(this.Handle, this.ViewPort, 0, num, false);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A5F20 File Offset: 0x000A4120
		private void HScrollBarValueChanged(object sender, EventArgs e)
		{
			int num;
			if (this.hbar.Value > this.hbar_value)
			{
				num = -1 * (this.hbar.Value - this.hbar_value);
			}
			else
			{
				num = this.hbar_value - this.hbar.Value;
			}
			this.hbar_value = this.hbar.Value;
			XplatUI.ScrollWindow(this.Handle, this.ViewPort, num, 0, false);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000A5F98 File Offset: 0x000A4198
		private void UpdateScrollBars()
		{
			this.ViewPort = base.ClientRectangle;
			if (this.AutoZoom)
			{
				return;
			}
			int num = this.image_size.Width * this.Columns + (this.Columns + 1) * this.padding;
			int num2 = this.image_size.Height * (this.Rows + 1) + (this.Rows + 2) * this.padding;
			bool flag = false;
			bool flag2 = false;
			if (num > base.ClientRectangle.Width)
			{
				flag2 = true;
				this.ViewPort.Height = this.ViewPort.Height - this.hbar.Height;
			}
			if (num2 > this.ViewPort.Height)
			{
				flag = true;
				this.ViewPort.Width = this.ViewPort.Width - this.vbar.Width;
			}
			if (!flag2 && num > this.ViewPort.Width)
			{
				flag2 = true;
				this.ViewPort.Height = this.ViewPort.Height - this.hbar.Height;
			}
			base.SuspendLayout();
			if (flag)
			{
				this.vbar.SetValues(num2, this.ViewPort.Height);
				this.vbar.Bounds = new Rectangle(base.ClientRectangle.Width - this.vbar.Width, 0, this.vbar.Width, base.ClientRectangle.Height - ((!flag2) ? 0 : SystemInformation.VerticalScrollBarWidth));
				this.vbar.Visible = true;
				this.vbar_value = this.vbar.Value;
			}
			else
			{
				this.vbar.Visible = false;
			}
			if (flag2)
			{
				this.hbar.SetValues(num, this.ViewPort.Width);
				this.hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - this.hbar.Height, base.ClientRectangle.Width - ((!flag) ? 0 : SystemInformation.HorizontalScrollBarHeight), this.hbar.Height);
				this.hbar.Visible = true;
				this.hbar_value = this.hbar.Value;
			}
			else
			{
				this.hbar.Visible = false;
			}
			base.ResumeLayout(false);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000A61F8 File Offset: 0x000A43F8
		private void InvalidateLayout()
		{
			if (this.image_cache != null)
			{
				for (int i = 0; i < this.image_cache.Length; i++)
				{
					if (this.image_cache[i] != null)
					{
						this.image_cache[i].Dispose();
					}
				}
				this.image_cache = null;
			}
			base.Invalidate();
		}

		// Token: 0x0400153D RID: 5437
		private bool autozoom;

		// Token: 0x0400153E RID: 5438
		private int columns;

		// Token: 0x0400153F RID: 5439
		private int rows;

		// Token: 0x04001540 RID: 5440
		private int startPage;

		// Token: 0x04001541 RID: 5441
		private double zoom;

		// Token: 0x04001542 RID: 5442
		private int padding = ThemeEngine.Current.PrintPreviewControlPadding;

		// Token: 0x04001543 RID: 5443
		private PrintDocument document;

		// Token: 0x04001544 RID: 5444
		internal PreviewPrintController controller;

		// Token: 0x04001545 RID: 5445
		internal PreviewPageInfo[] page_infos;

		// Token: 0x04001546 RID: 5446
		private VScrollBar vbar;

		// Token: 0x04001547 RID: 5447
		private HScrollBar hbar;

		// Token: 0x04001548 RID: 5448
		internal Rectangle ViewPort;

		// Token: 0x04001549 RID: 5449
		internal Image[] image_cache;

		// Token: 0x0400154A RID: 5450
		private Size image_size;

		// Token: 0x0400154C RID: 5452
		internal int vbar_value;

		// Token: 0x0400154D RID: 5453
		internal int hbar_value;
	}
}
