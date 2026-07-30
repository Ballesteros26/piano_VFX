using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the container for multiple-document interface (MDI) child forms. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000240 RID: 576
	[ClassInterface(1)]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[ComVisible(true)]
	public sealed class MdiClient : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MdiClient" /> class. </summary>
		// Token: 0x060025CB RID: 9675 RVA: 0x0008DE6C File Offset: 0x0008C06C
		public MdiClient()
		{
			this.mdi_child_list = new ArrayList();
			this.BackColor = SystemColors.AppWorkspace;
			this.Dock = DockStyle.Fill;
			base.SetStyle(ControlStyles.Selectable, false);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008DEA8 File Offset: 0x0008C0A8
		internal void SendFocusToActiveChild()
		{
			Form activeMdiChild = this.ActiveMdiChild;
			if (activeMdiChild == null)
			{
				this.ParentForm.SendControlFocus(this);
			}
			else
			{
				activeMdiChild.SendControlFocus(activeMdiChild);
				this.ParentForm.ActiveControl = activeMdiChild;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0008DEE8 File Offset: 0x0008C0E8
		internal bool HorizontalScrollbarVisible
		{
			get
			{
				return this.hbar != null && this.hbar.Visible;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x0008DF04 File Offset: 0x0008C104
		internal bool VerticalScrollbarVisible
		{
			get
			{
				return this.vbar != null && this.vbar.Visible;
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0008DF20 File Offset: 0x0008C120
		internal void SetParentText(bool text_changed)
		{
			if (this.setting_form_text)
			{
				return;
			}
			this.setting_form_text = true;
			if (text_changed)
			{
				this.form_text = this.ParentForm.Text;
			}
			if (this.ParentForm.ActiveMaximizedMdiChild == null)
			{
				this.ParentForm.Text = this.form_text;
			}
			else
			{
				string text = this.ParentForm.ActiveMaximizedMdiChild.form.Text;
				if (text.Length > 0)
				{
					this.ParentForm.Text = this.form_text + " - [" + this.ParentForm.ActiveMaximizedMdiChild.form.Text + "]";
				}
				else
				{
					this.ParentForm.Text = this.form_text;
				}
			}
			this.setting_form_text = false;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0008DFF4 File Offset: 0x0008C1F4
		internal override void OnPaintBackgroundInternal(PaintEventArgs pe)
		{
			if (this.BackgroundImage != null)
			{
				return;
			}
			if (base.Parent == null || base.Parent.BackgroundImage == null)
			{
				return;
			}
			base.Parent.PaintControlBackground(pe);
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0008E038 File Offset: 0x0008C238
		internal Form ParentForm
		{
			get
			{
				return (Form)base.Parent;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0008E048 File Offset: 0x0008C248
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new MdiClient.ControlCollection(this);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0008E050 File Offset: 0x0008C250
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_NCPAINT)
			{
				base.WndProc(ref m);
				return;
			}
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, this.Handle, false);
			Rectangle rectangle;
			rectangle..ctor(0, 0, base.Width, base.Height);
			ControlPaint.DrawBorder3D(paintEventArgs.Graphics, rectangle, Border3DStyle.Sunken);
			XplatUI.PaintEventEnd(ref m, this.Handle, false);
			m.Result = IntPtr.Zero;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0008E0C8 File Offset: 0x0008C2C8
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (base.Parent != null && base.Parent.IsHandleCreated)
			{
				XplatUI.InvalidateNC(base.Parent.Handle);
			}
			this.SizeScrollBars();
			this.ArrangeWindows();
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0008E114 File Offset: 0x0008C314
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			specified &= ~(BoundsSpecified.X | BoundsSpecified.Y);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0008E124 File Offset: 0x0008C324
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0008E130 File Offset: 0x0008C330
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Gets or sets the background image displayed in the <see cref="T:System.Windows.Forms.MdiClient" /> control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x0008E140 File Offset: 0x0008C340
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x0008E148 File Offset: 0x0008C348
		[Localizable(true)]
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
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x0008E154 File Offset: 0x0008C354
		// (set) Token: 0x060025DB RID: 9691 RVA: 0x0008E15C File Offset: 0x0008C35C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets the child multiple-document interface (MDI) forms of the <see cref="T:System.Windows.Forms.MdiClient" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> array that contains the child MDI forms of the <see cref="T:System.Windows.Forms.MdiClient" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x0008E168 File Offset: 0x0008C368
		public Form[] MdiChildren
		{
			get
			{
				if (this.mdi_child_list == null)
				{
					return new Form[0];
				}
				return (Form[])this.mdi_child_list.ToArray(typeof(Form));
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x0008E1A4 File Offset: 0x0008C3A4
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 512;
				return createParams;
			}
		}

		/// <summary>Arranges the multiple-document interface (MDI) child forms within the MDI parent form.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.MdiLayout" /> values that defines the layout of MDI child forms.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060025DE RID: 9694 RVA: 0x0008E1CC File Offset: 0x0008C3CC
		public void LayoutMdi(MdiLayout value)
		{
			this.ArrangeIconicWindows(true);
			switch (value)
			{
			case MdiLayout.Cascade:
			{
				int num = 0;
				for (int i = base.Controls.Count - 1; i >= 0; i--)
				{
					Form form = (Form)base.Controls[i];
					if (form.WindowState != FormWindowState.Minimized)
					{
						if (form.WindowState == FormWindowState.Maximized)
						{
							form.WindowState = FormWindowState.Normal;
						}
						form.Width = Convert.ToInt32((double)base.ClientSize.Width * 0.8);
						form.Height = Math.Max(Convert.ToInt32((double)base.ClientSize.Height * 0.8), SystemInformation.MinimumWindowSize.Height + 2);
						int num2 = 22 * num;
						int num3 = 22 * num;
						if (num != 0 && (num2 + form.Width > base.ClientSize.Width || num3 + form.Height > base.ClientSize.Height))
						{
							num = 0;
							num2 = 22 * num;
							num3 = 22 * num;
						}
						form.Left = num2;
						form.Top = num3;
						num++;
					}
				}
				break;
			}
			case MdiLayout.TileHorizontal:
			case MdiLayout.TileVertical:
			{
				int num4 = 0;
				int num5 = base.ClientSize.Height;
				for (int j = 0; j < base.Controls.Count; j++)
				{
					Form form2 = base.Controls[j] as Form;
					if (form2 != null)
					{
						if (form2.Visible)
						{
							if (form2.WindowState == FormWindowState.Maximized)
							{
								form2.WindowState = FormWindowState.Normal;
							}
							else if (form2.WindowState == FormWindowState.Minimized)
							{
								if (form2.Bounds.Top < num5)
								{
									num5 = form2.Bounds.Top;
								}
								goto IL_01EE;
							}
							num4++;
						}
					}
					IL_01EE:;
				}
				if (num4 <= 0)
				{
					return;
				}
				Size size;
				Size size2;
				if (value == MdiLayout.TileHorizontal)
				{
					size..ctor(base.ClientSize.Width, num5 / num4);
					size2..ctor(0, size.Height);
				}
				else
				{
					size..ctor(base.ClientSize.Width / num4, num5);
					size2..ctor(size.Width, 0);
				}
				Point point = Point.Empty;
				for (int k = 0; k < base.Controls.Count; k++)
				{
					Form form3 = base.Controls[k] as Form;
					if (form3 != null)
					{
						if (form3.Visible)
						{
							if (form3.WindowState != FormWindowState.Minimized)
							{
								form3.Size = size;
								form3.Location = point;
								point += size2;
							}
						}
					}
				}
				break;
			}
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0008E4D4 File Offset: 0x0008C6D4
		internal void SizeScrollBars()
		{
			if (this.lock_sizing)
			{
				return;
			}
			if (!base.IsHandleCreated)
			{
				return;
			}
			if (base.Controls.Count == 0 || ((Form)base.Controls[0]).WindowState == FormWindowState.Maximized)
			{
				if (this.hbar != null)
				{
					this.hbar.Visible = false;
				}
				if (this.vbar != null)
				{
					this.vbar.Visible = false;
				}
				if (this.sizegrip != null)
				{
					this.sizegrip.Visible = false;
				}
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (object obj in base.Controls)
			{
				Form form = (Form)obj;
				if (form.Visible)
				{
					if (form.Right > num)
					{
						num = form.Right;
					}
					if (form.Left < num2)
					{
						num2 = form.Left;
					}
					if (form.Bottom > num4)
					{
						num4 = form.Bottom;
					}
					if (form.Top < 0)
					{
						num3 = form.Top;
					}
				}
			}
			int num5 = base.ClientSize.Width;
			int num6 = base.ClientSize.Height;
			bool flag = false;
			bool flag2 = false;
			if (num - num2 > num5 || num2 < 0)
			{
				flag = true;
				num6 -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (num4 - num3 > num6 || num3 < 0)
			{
				flag2 = true;
				num5 -= SystemInformation.VerticalScrollBarWidth;
				if (!flag && (num - num2 > num5 || num2 < 0))
				{
					flag = true;
					num6 -= SystemInformation.HorizontalScrollBarHeight;
				}
			}
			if (flag)
			{
				if (this.hbar == null)
				{
					this.hbar = new ImplicitHScrollBar();
					base.Controls.AddImplicit(this.hbar);
				}
				this.hbar.Visible = true;
				this.CalcHBar(num2, num, flag2);
			}
			else if (this.hbar != null)
			{
				this.hbar.Visible = false;
			}
			if (flag2)
			{
				if (this.vbar == null)
				{
					this.vbar = new ImplicitVScrollBar();
					base.Controls.AddImplicit(this.vbar);
				}
				this.vbar.Visible = true;
				this.CalcVBar(num3, num4, flag);
			}
			else if (this.vbar != null)
			{
				this.vbar.Visible = false;
			}
			if (flag && flag2)
			{
				if (this.sizegrip == null)
				{
					this.sizegrip = new SizeGrip(this.ParentForm);
					base.Controls.AddImplicit(this.sizegrip);
				}
				this.sizegrip.Location = new Point(this.hbar.Right, this.vbar.Bottom);
				this.sizegrip.Visible = true;
				XplatUI.SetZOrder(this.sizegrip.Handle, this.vbar.Handle, false, false);
			}
			else if (this.sizegrip != null)
			{
				this.sizegrip.Visible = false;
			}
			XplatUI.InvalidateNC(this.Handle);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0008E838 File Offset: 0x0008CA38
		private void CalcHBar(int left, int right, bool vert_vis)
		{
			this.initializing_scrollbars = true;
			this.hbar.Left = 0;
			this.hbar.Top = base.ClientRectangle.Bottom - this.hbar.Height;
			this.hbar.Width = base.ClientRectangle.Width - ((!vert_vis) ? 0 : SystemInformation.VerticalScrollBarWidth);
			this.hbar.LargeChange = 50;
			this.hbar.Minimum = Math.Min(left, 0);
			this.hbar.Maximum = Math.Max(right - base.ClientSize.Width + 51 + ((!vert_vis) ? 0 : SystemInformation.VerticalScrollBarWidth), 0);
			this.hbar.Value = 0;
			this.hbar_value = 0;
			this.hbar.ValueChanged += new EventHandler(this.HBarValueChanged);
			XplatUI.SetZOrder(this.hbar.Handle, IntPtr.Zero, true, false);
			this.initializing_scrollbars = false;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0008E948 File Offset: 0x0008CB48
		private void CalcVBar(int top, int bottom, bool horz_vis)
		{
			this.initializing_scrollbars = true;
			this.vbar.Top = 0;
			this.vbar.Left = base.ClientRectangle.Right - this.vbar.Width;
			this.vbar.Height = base.ClientRectangle.Height - ((!horz_vis) ? 0 : SystemInformation.HorizontalScrollBarHeight);
			this.vbar.LargeChange = 50;
			this.vbar.Minimum = Math.Min(top, 0);
			this.vbar.Maximum = Math.Max(bottom - base.ClientSize.Height + 51 + ((!horz_vis) ? 0 : SystemInformation.HorizontalScrollBarHeight), 0);
			this.vbar.Value = 0;
			this.vbar_value = 0;
			this.vbar.ValueChanged += new EventHandler(this.VBarValueChanged);
			XplatUI.SetZOrder(this.vbar.Handle, IntPtr.Zero, true, false);
			this.initializing_scrollbars = false;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008EA58 File Offset: 0x0008CC58
		private void HBarValueChanged(object sender, EventArgs e)
		{
			if (this.initializing_scrollbars)
			{
				return;
			}
			if (this.hbar.Value == this.hbar_value)
			{
				return;
			}
			this.lock_sizing = true;
			try
			{
				int num = this.hbar_value - this.hbar.Value;
				foreach (object obj in base.Controls)
				{
					Form form = (Form)obj;
					form.Left += num;
				}
			}
			finally
			{
				this.lock_sizing = false;
			}
			this.hbar_value = this.hbar.Value;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0008EB44 File Offset: 0x0008CD44
		private void VBarValueChanged(object sender, EventArgs e)
		{
			if (this.initializing_scrollbars)
			{
				return;
			}
			if (this.vbar.Value == this.vbar_value)
			{
				return;
			}
			this.lock_sizing = true;
			try
			{
				int num = this.vbar_value - this.vbar.Value;
				foreach (object obj in base.Controls)
				{
					Form form = (Form)obj;
					form.Top += num;
				}
			}
			finally
			{
				this.lock_sizing = false;
			}
			this.vbar_value = this.vbar.Value;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0008EC30 File Offset: 0x0008CE30
		private void ArrangeWindows()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			int num = 0;
			if (this.prev_bottom != -1)
			{
				num = base.Bottom - this.prev_bottom;
			}
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				Form form = control as Form;
				if (control != null && form.Visible)
				{
					MdiWindowManager mdiWindowManager = form.WindowManager as MdiWindowManager;
					if (mdiWindowManager.GetWindowState() == FormWindowState.Maximized)
					{
						form.Bounds = mdiWindowManager.MaximizedBounds;
					}
					if (mdiWindowManager.GetWindowState() == FormWindowState.Minimized)
					{
						form.Top += num;
					}
				}
			}
			this.prev_bottom = base.Bottom;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0008ED30 File Offset: 0x0008CF30
		internal void ArrangeIconicWindows(bool rearrange_all)
		{
			Rectangle empty = Rectangle.Empty;
			this.lock_sizing = true;
			foreach (object obj in base.Controls)
			{
				Form form = (Form)obj;
				if (form.WindowState == FormWindowState.Minimized)
				{
					MdiWindowManager mdiWindowManager = (MdiWindowManager)form.WindowManager;
					if (mdiWindowManager.IconicBounds != Rectangle.Empty && !rearrange_all)
					{
						if (form.Bounds != mdiWindowManager.IconicBounds)
						{
							form.Bounds = mdiWindowManager.IconicBounds;
						}
					}
					else
					{
						bool flag = true;
						empty.Size = mdiWindowManager.IconicSize;
						int num = 0;
						int num2 = base.ClientSize.Height - empty.Height;
						int num3 = num;
						int num4 = num2;
						do
						{
							empty.X = num3;
							empty.Y = num4;
							flag = true;
							foreach (object obj2 in base.Controls)
							{
								Form form2 = (Form)obj2;
								if (form2 != form && form2.window_state == FormWindowState.Minimized)
								{
									if (form2.Bounds.IntersectsWith(empty))
									{
										flag = false;
										break;
									}
								}
							}
							if (!flag)
							{
								num3 += empty.Width;
								if (num3 + empty.Width > base.Right)
								{
									num3 = num;
									num4 -= empty.Height;
								}
							}
						}
						while (!flag);
						mdiWindowManager.IconicBounds = empty;
						form.Bounds = mdiWindowManager.IconicBounds;
					}
				}
			}
			this.lock_sizing = false;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0008EF40 File Offset: 0x0008D140
		internal void ChildFormClosed(Form form)
		{
			FormWindowState windowState = form.WindowState;
			form.Visible = false;
			base.Controls.Remove(form);
			if (base.Controls.Count == 0)
			{
				((MdiWindowManager)form.window_manager).RaiseDeactivate();
			}
			else if (windowState == FormWindowState.Maximized)
			{
				Form form2 = (Form)base.Controls[0];
				form2.WindowState = FormWindowState.Maximized;
				this.ActivateChild(form2);
			}
			if (base.Controls.Count == 0)
			{
				XplatUI.RequestNCRecalc(base.Parent.Handle);
				this.ParentForm.PerformLayout();
				MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
				if (mainMenuStrip != null && mainMenuStrip.IsCurrentlyMerged)
				{
					ToolStripManager.RevertMerge(mainMenuStrip);
				}
			}
			this.SizeScrollBars();
			this.SetParentText(false);
			form.Dispose();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0008F018 File Offset: 0x0008D218
		internal void ActivateNextChild()
		{
			if (base.Controls.Count < 1)
			{
				return;
			}
			if (base.Controls.Count == 1 && base.Controls[0] == this.ActiveMdiChild)
			{
				return;
			}
			Form form = (Form)base.Controls[0];
			Form form2 = (Form)base.Controls[1];
			this.ActivateChild(form2);
			form.SendToBack();
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0008F094 File Offset: 0x0008D294
		internal void ActivatePreviousChild()
		{
			if (base.Controls.Count <= 1)
			{
				return;
			}
			Form form = (Form)base.Controls[base.Controls.Count - 1];
			this.ActivateChild(form);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0008F0D8 File Offset: 0x0008D2D8
		internal void ActivateChild(Form form)
		{
			if (base.Controls.Count < 1)
			{
				return;
			}
			if (this.ParentForm.is_changing_visible_state > 0)
			{
				return;
			}
			Form form2 = (Form)base.Controls[0];
			bool flag = this.ParentForm.ActiveControl == form2;
			MdiWindowManager mdiWindowManager = (MdiWindowManager)form.WindowManager;
			if (form2.WindowState == FormWindowState.Maximized && form.WindowState != FormWindowState.Maximized && form.Visible)
			{
				FormWindowState window_state = form.window_state;
				this.SetWindowState(form, window_state, FormWindowState.Maximized, true);
				mdiWindowManager.was_minimized = form.window_state == FormWindowState.Minimized;
				form.window_state = FormWindowState.Maximized;
				this.SetParentText(false);
			}
			form.BringToFront();
			form.SendControlFocus(form);
			this.SetWindowStates(mdiWindowManager);
			if (form2 != form)
			{
				form.has_focus = false;
				if (form2.IsHandleCreated)
				{
					XplatUI.InvalidateNC(form2.Handle);
				}
				if (form.IsHandleCreated)
				{
					XplatUI.InvalidateNC(form.Handle);
				}
				if (flag)
				{
					MdiWindowManager mdiWindowManager2 = (MdiWindowManager)form2.window_manager;
					mdiWindowManager2.RaiseDeactivate();
				}
			}
			this.active_child = (Form)base.Controls[0];
			if (this.active_child.Visible)
			{
				bool flag2 = this.ParentForm.ActiveControl != this.active_child;
				this.ParentForm.ActiveControl = this.active_child;
				if (flag2)
				{
					MdiWindowManager mdiWindowManager3 = (MdiWindowManager)this.active_child.window_manager;
					mdiWindowManager3.RaiseActivated();
				}
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0008F260 File Offset: 0x0008D460
		internal override IntPtr AfterTopMostControl()
		{
			if (this.hbar != null && this.hbar.Visible)
			{
				return this.hbar.Handle;
			}
			if (this.vbar != null && this.vbar.Visible)
			{
				return this.vbar.Handle;
			}
			return base.AfterTopMostControl();
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0008F2C4 File Offset: 0x0008D4C4
		internal bool SetWindowStates(MdiWindowManager wm)
		{
			Form form = wm.form;
			if (this.setting_windowstates)
			{
				return false;
			}
			if (!form.Visible)
			{
				return false;
			}
			bool isActive = wm.IsActive;
			bool flag = false;
			if (!isActive)
			{
				return false;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			this.setting_windowstates = true;
			foreach (object obj in this.mdi_child_list)
			{
				Form form2 = (Form)obj;
				if (form2 != form)
				{
					if (form2.Visible)
					{
						if (form2.WindowState == FormWindowState.Maximized && isActive)
						{
							flag = true;
							if (((MdiWindowManager)form2.window_manager).was_minimized)
							{
								arrayList.Add(form2);
							}
							else
							{
								arrayList2.Add(form2);
							}
						}
					}
				}
			}
			if (flag && form.WindowState != FormWindowState.Maximized)
			{
				wm.was_minimized = form.window_state == FormWindowState.Minimized;
				form.WindowState = FormWindowState.Maximized;
			}
			foreach (object obj2 in arrayList)
			{
				Form form3 = (Form)obj2;
				form3.WindowState = FormWindowState.Minimized;
			}
			foreach (object obj3 in arrayList2)
			{
				Form form4 = (Form)obj3;
				form4.WindowState = FormWindowState.Normal;
			}
			this.SetParentText(false);
			XplatUI.RequestNCRecalc(this.ParentForm.Handle);
			XplatUI.RequestNCRecalc(this.Handle);
			this.SizeScrollBars();
			this.setting_windowstates = false;
			if (form.MdiParent.MainMenuStrip != null)
			{
				form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
			MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
			if (mainMenuStrip != null)
			{
				if (mainMenuStrip.IsCurrentlyMerged)
				{
					ToolStripManager.RevertMerge(mainMenuStrip);
				}
				MenuStrip menuStrip = this.LookForChildMenu(form);
				if (form.WindowState != FormWindowState.Maximized)
				{
					this.RemoveControlMenuItems(wm);
				}
				if (form.WindowState == FormWindowState.Maximized)
				{
					bool flag2 = false;
					foreach (object obj4 in mainMenuStrip.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj4;
						if (toolStripItem is MdiControlStrip.SystemMenuItem)
						{
							(toolStripItem as MdiControlStrip.SystemMenuItem).MdiForm = form;
							flag2 = true;
						}
						else if (toolStripItem is MdiControlStrip.ControlBoxMenuItem)
						{
							(toolStripItem as MdiControlStrip.ControlBoxMenuItem).MdiForm = form;
							flag2 = true;
						}
					}
					if (!flag2)
					{
						mainMenuStrip.SuspendLayout();
						mainMenuStrip.Items.Insert(0, new MdiControlStrip.SystemMenuItem(form));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Close));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Max));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Min));
						mainMenuStrip.ResumeLayout();
					}
				}
				if (menuStrip != null)
				{
					ToolStripManager.Merge(menuStrip, mainMenuStrip);
				}
			}
			return flag;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0008F680 File Offset: 0x0008D880
		private MenuStrip LookForChildMenu(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MenuStrip)
				{
					return (MenuStrip)control;
				}
				if (control is ToolStripContainer || control is ToolStripPanel)
				{
					MenuStrip menuStrip = this.LookForChildMenu(control);
					if (menuStrip != null)
					{
						return menuStrip;
					}
				}
			}
			return null;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0008F730 File Offset: 0x0008D930
		internal void RemoveControlMenuItems(MdiWindowManager wm)
		{
			Form form = wm.form;
			MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
			if (mainMenuStrip != null)
			{
				mainMenuStrip.SuspendLayout();
				for (int i = mainMenuStrip.Items.Count - 1; i >= 0; i--)
				{
					if (mainMenuStrip.Items[i] is MdiControlStrip.SystemMenuItem)
					{
						if ((mainMenuStrip.Items[i] as MdiControlStrip.SystemMenuItem).MdiForm == form)
						{
							mainMenuStrip.Items.RemoveAt(i);
						}
					}
					else if (mainMenuStrip.Items[i] is MdiControlStrip.ControlBoxMenuItem && (mainMenuStrip.Items[i] as MdiControlStrip.ControlBoxMenuItem).MdiForm == form)
					{
						mainMenuStrip.Items.RemoveAt(i);
					}
				}
				mainMenuStrip.ResumeLayout();
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0008F804 File Offset: 0x0008DA04
		internal void SetWindowState(Form form, FormWindowState old_window_state, FormWindowState new_window_state, bool is_activating_child)
		{
			MdiWindowManager mdiWindowManager = (MdiWindowManager)form.window_manager;
			if (!is_activating_child && new_window_state == FormWindowState.Maximized && !mdiWindowManager.IsActive)
			{
				this.ActivateChild(form);
				return;
			}
			if (old_window_state == FormWindowState.Normal)
			{
				mdiWindowManager.NormalBounds = form.Bounds;
			}
			if (this.SetWindowStates(mdiWindowManager))
			{
				return;
			}
			if (old_window_state == new_window_state)
			{
				return;
			}
			bool flag = old_window_state == FormWindowState.Maximized || new_window_state == FormWindowState.Maximized;
			switch (new_window_state)
			{
			case FormWindowState.Normal:
				form.Bounds = mdiWindowManager.NormalBounds;
				break;
			case FormWindowState.Minimized:
				this.ArrangeIconicWindows(false);
				break;
			case FormWindowState.Maximized:
				form.Bounds = mdiWindowManager.MaximizedBounds;
				break;
			}
			mdiWindowManager.UpdateWindowDecorations(new_window_state);
			form.ResetCursor();
			if (flag)
			{
				base.Parent.PerformLayout();
			}
			XplatUI.RequestNCRecalc(base.Parent.Handle);
			XplatUI.RequestNCRecalc(form.Handle);
			if (!this.setting_windowstates)
			{
				this.SizeScrollBars();
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x0008F908 File Offset: 0x0008DB08
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x0008F910 File Offset: 0x0008DB10
		internal int ChildrenCreated
		{
			get
			{
				return this.mdi_created;
			}
			set
			{
				this.mdi_created = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0008F91C File Offset: 0x0008DB1C
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x0008F9F0 File Offset: 0x0008DBF0
		internal Form ActiveMdiChild
		{
			get
			{
				if (this.ParentForm != null && !this.ParentForm.Visible)
				{
					return null;
				}
				if (base.Controls.Count < 1)
				{
					return null;
				}
				if (!this.ParentForm.IsHandleCreated)
				{
					return null;
				}
				if (!this.ParentForm.has_been_visible)
				{
					return null;
				}
				if (!this.ParentForm.Visible)
				{
					return this.active_child;
				}
				this.active_child = null;
				for (int i = 0; i < base.Controls.Count; i++)
				{
					if (base.Controls[i].Visible)
					{
						this.active_child = (Form)base.Controls[i];
						break;
					}
				}
				return this.active_child;
			}
			set
			{
				this.ActivateChild(value);
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0008F9FC File Offset: 0x0008DBFC
		internal void ActivateActiveMdiChild()
		{
			if (this.ParentForm.is_changing_visible_state > 0)
			{
				return;
			}
			for (int i = 0; i < base.Controls.Count; i++)
			{
				if (base.Controls[i].Visible)
				{
					this.ActivateChild((Form)base.Controls[i]);
					return;
				}
			}
		}

		// Token: 0x04001304 RID: 4868
		private int mdi_created;

		// Token: 0x04001305 RID: 4869
		private ImplicitHScrollBar hbar;

		// Token: 0x04001306 RID: 4870
		private ImplicitVScrollBar vbar;

		// Token: 0x04001307 RID: 4871
		private SizeGrip sizegrip;

		// Token: 0x04001308 RID: 4872
		private int hbar_value;

		// Token: 0x04001309 RID: 4873
		private int vbar_value;

		// Token: 0x0400130A RID: 4874
		private bool lock_sizing;

		// Token: 0x0400130B RID: 4875
		private bool initializing_scrollbars;

		// Token: 0x0400130C RID: 4876
		private int prev_bottom;

		// Token: 0x0400130D RID: 4877
		private bool setting_windowstates;

		// Token: 0x0400130E RID: 4878
		internal ArrayList mdi_child_list;

		// Token: 0x0400130F RID: 4879
		private string form_text;

		// Token: 0x04001310 RID: 4880
		private bool setting_form_text;

		// Token: 0x04001311 RID: 4881
		private Form active_child;

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.MdiClient" /> controls.</summary>
		// Token: 0x02000241 RID: 577
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MdiClient.ControlCollection" /> class, specifying the owner of the collection. </summary>
			/// <param name="owner">The owner of the collection.</param>
			// Token: 0x060025F4 RID: 9716 RVA: 0x0008FA68 File Offset: 0x0008DC68
			public ControlCollection(MdiClient owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds a control to the multiple-document interface (MDI) Container.</summary>
			/// <param name="value">MDI Child Form to add. </param>
			// Token: 0x060025F5 RID: 9717 RVA: 0x0008FA78 File Offset: 0x0008DC78
			public override void Add(Control value)
			{
				if (!(value is Form) || !((Form)value).IsMdiChild)
				{
					throw new ArgumentException("Form must be MdiChild");
				}
				this.owner.mdi_child_list.Add(value);
				base.Add(value);
				Form form = (Form)value;
				this.owner.ActiveMdiChild = form;
			}

			/// <summary>Removes a child control.</summary>
			/// <param name="value">MDI Child Form to remove. </param>
			// Token: 0x060025F6 RID: 9718 RVA: 0x0008FAD8 File Offset: 0x0008DCD8
			public override void Remove(Control value)
			{
				Form form = value as Form;
				if (form != null)
				{
					MdiWindowManager mdiWindowManager = form.WindowManager as MdiWindowManager;
					if (mdiWindowManager != null)
					{
						form.Closed -= mdiWindowManager.form_closed_handler;
					}
				}
				this.owner.mdi_child_list.Remove(value);
				base.Remove(value);
			}

			// Token: 0x04001312 RID: 4882
			private MdiClient owner;
		}
	}
}
