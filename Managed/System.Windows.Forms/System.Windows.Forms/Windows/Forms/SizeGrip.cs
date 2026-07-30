using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020002DD RID: 733
	internal class SizeGrip : Control
	{
		// Token: 0x06003016 RID: 12310 RVA: 0x000BA868 File Offset: 0x000B8A68
		public SizeGrip(Control CapturedControl)
		{
			this.Cursor = Cursors.SizeNWSE;
			this.enabled = true;
			this.fill_background = true;
			base.Size = SizeGrip.GetDefaultSize();
			this.CapturedControl = CapturedControl;
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000BA8A8 File Offset: 0x000B8AA8
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x000BA8B0 File Offset: 0x000B8AB0
		public bool FillBackground
		{
			get
			{
				return this.fill_background;
			}
			set
			{
				this.fill_background = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000BA8BC File Offset: 0x000B8ABC
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x000BA8C4 File Offset: 0x000B8AC4
		public bool Virtual
		{
			get
			{
				return this.is_virtual;
			}
			set
			{
				if (this.is_virtual == value)
				{
					return;
				}
				this.is_virtual = value;
				if (this.is_virtual)
				{
					this.CapturedControl.MouseMove += this.HandleMouseMove;
					this.CapturedControl.MouseUp += this.HandleMouseUp;
					this.CapturedControl.MouseDown += this.HandleMouseDown;
					this.CapturedControl.EnabledChanged += new EventHandler(this.HandleEnabledChanged);
					this.CapturedControl.Resize += new EventHandler(this.HandleResize);
				}
				else
				{
					this.CapturedControl.MouseMove -= this.HandleMouseMove;
					this.CapturedControl.MouseUp -= this.HandleMouseUp;
					this.CapturedControl.MouseDown -= this.HandleMouseDown;
					this.CapturedControl.EnabledChanged -= new EventHandler(this.HandleEnabledChanged);
					this.CapturedControl.Resize -= new EventHandler(this.HandleResize);
				}
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000BA9DC File Offset: 0x000B8BDC
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x000BA9E4 File Offset: 0x000B8BE4
		public Control CapturedControl
		{
			get
			{
				return this.captured_control;
			}
			set
			{
				this.captured_control = value;
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000BA9F0 File Offset: 0x000B8BF0
		internal static Size GetDefaultSize()
		{
			return new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000BAA04 File Offset: 0x000B8C04
		internal static Rectangle GetDefaultRectangle(Control Parent)
		{
			Size defaultSize = SizeGrip.GetDefaultSize();
			return new Rectangle(Parent.ClientSize.Width - defaultSize.Width, Parent.ClientSize.Height - defaultSize.Height, defaultSize.Width, defaultSize.Height);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000BAA58 File Offset: 0x000B8C58
		private void HandleResize(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			control.Invalidate(this.last_painted_area);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000BAA78 File Offset: 0x000B8C78
		private void HandleEnabledChanged(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			this.enabled = control.Enabled;
			Cursor cursor;
			if (this.enabled)
			{
				cursor = Cursors.SizeNWSE;
			}
			else
			{
				cursor = Cursors.Default;
			}
			if (this.is_virtual)
			{
				if (this.CapturedControl != null)
				{
					this.CapturedControl.Cursor = cursor;
				}
			}
			else
			{
				this.Cursor = cursor;
			}
			control.Invalidate(SizeGrip.GetDefaultRectangle(control));
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000BAAF0 File Offset: 0x000B8CF0
		internal void HandlePaint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				Control control = (Control)sender;
				Graphics graphics = e.Graphics;
				Rectangle defaultRectangle = SizeGrip.GetDefaultRectangle(control);
				if (!this.is_virtual || this.fill_background)
				{
					graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorControl), defaultRectangle);
				}
				if (this.enabled)
				{
					ControlPaint.DrawSizeGrip(graphics, this.BackColor, defaultRectangle);
				}
				this.last_painted_area = defaultRectangle;
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000BAB74 File Offset: 0x000B8D74
		private void HandleMouseCaptureChanged(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			if (this.captured && !control.Capture)
			{
				this.captured = false;
				this.CapturedControl.Size = new Size(this.window_w, this.window_h);
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000BABC4 File Offset: 0x000B8DC4
		internal void HandleMouseDown(object sender, MouseEventArgs e)
		{
			if (this.enabled)
			{
				Control control = (Control)sender;
				if (!SizeGrip.GetDefaultRectangle(control).Contains(e.X, e.Y))
				{
					return;
				}
				control.Capture = true;
				this.captured = true;
				this.capture_point = Control.MousePosition;
				this.window_w = this.CapturedControl.Width;
				this.window_h = this.CapturedControl.Height;
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000BAC40 File Offset: 0x000B8E40
		internal void HandleMouseMove(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			if (SizeGrip.GetDefaultRectangle(control).Contains(e.X, e.Y))
			{
				control.Cursor = Cursors.SizeNWSE;
			}
			else
			{
				control.Cursor = Cursors.Default;
			}
			if (this.captured)
			{
				Point mousePosition = Control.MousePosition;
				int num = mousePosition.X - this.capture_point.X;
				int num2 = mousePosition.Y - this.capture_point.Y;
				Control capturedControl = this.CapturedControl;
				Form form = capturedControl as Form;
				Size size;
				size..ctor(this.window_w + num, this.window_h + num2);
				Size size2 = ((form == null) ? Size.Empty : form.MaximumSize);
				Size size3 = ((form == null) ? Size.Empty : form.MinimumSize);
				if (size.Width > size2.Width && size2.Width > 0)
				{
					size.Width = size2.Width;
				}
				else if (size.Width < size3.Width)
				{
					size.Width = size3.Width;
				}
				if (size.Height > size2.Height && size2.Height > 0)
				{
					size.Height = size2.Height;
				}
				else if (size.Height < size3.Height)
				{
					size.Height = size3.Height;
				}
				if (size != capturedControl.Size)
				{
					capturedControl.Size = size;
				}
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000BADE8 File Offset: 0x000B8FE8
		internal void HandleMouseUp(object sender, MouseEventArgs e)
		{
			if (this.captured)
			{
				Control control = (Control)sender;
				this.captured = false;
				control.Capture = false;
				control.Invalidate(this.last_painted_area);
				if (base.Parent is ScrollableControl)
				{
					((ScrollableControl)base.Parent).UpdateSizeGripVisible();
				}
				if (this.hide_pending)
				{
					base.Hide();
					this.hide_pending = false;
				}
			}
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000BAE5C File Offset: 0x000B905C
		protected override void SetVisibleCore(bool value)
		{
			if (base.Capture)
			{
				if (!value)
				{
					this.hide_pending = true;
				}
				else
				{
					this.hide_pending = false;
				}
				return;
			}
			base.SetVisibleCore(value);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000BAE98 File Offset: 0x000B9098
		protected override void OnPaint(PaintEventArgs pe)
		{
			this.HandlePaint(this, pe);
			base.OnPaint(pe);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000BAEAC File Offset: 0x000B90AC
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			this.HandleMouseCaptureChanged(this, e);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000BAEC0 File Offset: 0x000B90C0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.HandleEnabledChanged(this, e);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000BAED4 File Offset: 0x000B90D4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.HandleMouseDown(this, e);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000BAEE0 File Offset: 0x000B90E0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.HandleMouseMove(this, e);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000BAEEC File Offset: 0x000B90EC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.HandleMouseUp(this, e);
		}

		// Token: 0x040017A4 RID: 6052
		private Point capture_point;

		// Token: 0x040017A5 RID: 6053
		private Control captured_control;

		// Token: 0x040017A6 RID: 6054
		private int window_w;

		// Token: 0x040017A7 RID: 6055
		private int window_h;

		// Token: 0x040017A8 RID: 6056
		private bool hide_pending;

		// Token: 0x040017A9 RID: 6057
		private bool captured;

		// Token: 0x040017AA RID: 6058
		private bool is_virtual;

		// Token: 0x040017AB RID: 6059
		private bool enabled;

		// Token: 0x040017AC RID: 6060
		private bool fill_background;

		// Token: 0x040017AD RID: 6061
		private Rectangle last_painted_area;
	}
}
