using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ookii.Dialogs
{
	// Token: 0x0200000B RID: 11
	public partial class ExtendedForm : Form
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600004D RID: 77 RVA: 0x00003468 File Offset: 0x00001668
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x000034A0 File Offset: 0x000016A0
		[field: DebuggerBrowsable(0)]
		public event EventHandler DwmCompositionChanged;

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000034E8 File Offset: 0x000016E8
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003500 File Offset: 0x00001700
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Indicates whether or not the form automatically uses the system default font.")]
		public bool UseSystemFont
		{
			get
			{
				return this._useSystemFont;
			}
			set
			{
				this._useSystemFont = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000350C File Offset: 0x0000170C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003524 File Offset: 0x00001724
		[Category("Appearance")]
		[Description("The glass margins of the form.")]
		public Padding GlassMargin
		{
			get
			{
				return this._glassMargin;
			}
			set
			{
				bool flag = this._glassMargin != value;
				if (flag)
				{
					this._glassMargin = value;
					this.EnableGlass();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003554 File Offset: 0x00001754
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000356C File Offset: 0x0000176C
		[Category("Behavior")]
		[Description("Indicates whether the form can be dragged by the glass areas inside the client area.")]
		[DefaultValue(true)]
		public bool AllowGlassDragging
		{
			get
			{
				return this._allowGlassDragging;
			}
			set
			{
				this._allowGlassDragging = value;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003578 File Offset: 0x00001778
		protected virtual void OnDwmCompositionChanged(EventArgs e)
		{
			EventHandler dwmCompositionChanged = this.DwmCompositionChanged;
			bool flag = dwmCompositionChanged != null;
			if (flag)
			{
				dwmCompositionChanged.Invoke(this, e);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000035A0 File Offset: 0x000017A0
		protected override void OnLoad(EventArgs e)
		{
			bool flag = !base.DesignMode && this._useSystemFont;
			if (flag)
			{
				this.Font = SystemFonts.IconTitleFont;
				SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.SystemEvents_UserPreferenceChanged);
			}
			base.OnLoad(e);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035EB File Offset: 0x000017EB
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.SystemEvents_UserPreferenceChanged);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003608 File Offset: 0x00001808
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			bool flag = base.DesignMode || Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				bool designMode = base.DesignMode;
				if (designMode)
				{
					using (HatchBrush hatchBrush = new HatchBrush(51, Color.SkyBlue, this.BackColor))
					{
						this.PaintGlassArea(pevent, hatchBrush);
					}
				}
				else
				{
					this.PaintGlassArea(pevent, Brushes.Black);
				}
			}
			else
			{
				base.OnPaintBackground(pevent);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000368C File Offset: 0x0000188C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			bool flag = this._glassMargin.All != 0;
			if (flag)
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036BB File Offset: 0x000018BB
		protected override void OnHandleCreated(EventArgs e)
		{
			this.EnableGlass();
			base.OnHandleCreated(e);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000036D0 File Offset: 0x000018D0
		[SecurityPermission(6, Flags = 2)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			int msg = m.Msg;
			if (msg != 132)
			{
				if (msg == 798)
				{
					bool flag = this._glassMargin.All != 0;
					if (flag)
					{
						this.EnableGlass();
					}
					this.OnDwmCompositionChanged(EventArgs.Empty);
					m.Result = IntPtr.Zero;
				}
			}
			else
			{
				bool flag2 = this._allowGlassDragging && m.Result == new IntPtr(1) && Glass.IsDwmCompositionEnabled;
				if (flag2)
				{
					bool flag3 = this._glassMargin.Left == -1 && this._glassMargin.Top == -1 && this._glassMargin.Right == -1 && this._glassMargin.Bottom == -1;
					if (flag3)
					{
						m.Result = new IntPtr(2);
					}
					else
					{
						Point point;
						point..ctor((int)m.LParam & 65535, (int)m.LParam >> 16);
						point = base.PointToClient(point);
						bool flag4 = point.X < this._glassMargin.Left || point.X > base.ClientSize.Width - this._glassMargin.Right || point.Y < this._glassMargin.Top || point.Y > base.ClientSize.Height - this._glassMargin.Bottom;
						if (flag4)
						{
							m.Result = new IntPtr(2);
						}
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003878 File Offset: 0x00001A78
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			float width = factor.Width;
			Padding glassMargin = this.GlassMargin;
			bool flag = width != 1f;
			if (flag)
			{
				bool flag2 = glassMargin.Left > 0;
				if (flag2)
				{
					glassMargin.Left = (int)Math.Round((double)((float)glassMargin.Left * width));
				}
				bool flag3 = glassMargin.Right > 0;
				if (flag3)
				{
					glassMargin.Right = (int)Math.Round((double)((float)glassMargin.Right * width));
				}
			}
			float height = factor.Height;
			bool flag4 = height != 1f;
			if (flag4)
			{
				bool flag5 = glassMargin.Top > 0;
				if (flag5)
				{
					glassMargin.Top = (int)Math.Round((double)((float)glassMargin.Top * height));
				}
				bool flag6 = glassMargin.Bottom > 0;
				if (flag6)
				{
					glassMargin.Bottom = (int)Math.Round((double)((float)glassMargin.Bottom * height));
				}
			}
			this.GlassMargin = glassMargin;
			base.ScaleControl(factor, specified);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003978 File Offset: 0x00001B78
		private void EnableGlass()
		{
			bool flag = !base.DesignMode && Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				this.ExtendFrameIntoClientArea(this.GlassMargin);
				base.Invalidate();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000039B0 File Offset: 0x00001BB0
		private void PaintGlassArea(PaintEventArgs pevent, Brush brush)
		{
			bool flag = this._glassMargin.Left == -1 && this._glassMargin.Top == -1 && this._glassMargin.Right == -1 && this._glassMargin.Bottom == -1;
			if (flag)
			{
				pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
			}
			else
			{
				Rectangle rectangle;
				rectangle..ctor(this._glassMargin.Left, this._glassMargin.Top, base.ClientSize.Width - this._glassMargin.Right, base.ClientSize.Height - this._glassMargin.Bottom);
				pevent.Graphics.FillRectangle(new SolidBrush(this.BackColor), rectangle);
				bool flag2 = this._glassMargin.Left != 0;
				if (flag2)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, this._glassMargin.Left, base.ClientSize.Height));
				}
				bool flag3 = this._glassMargin.Right != 0;
				if (flag3)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(base.ClientSize.Width - this._glassMargin.Right, 0, base.ClientSize.Width, base.ClientSize.Height));
				}
				bool flag4 = this._glassMargin.Top != 0;
				if (flag4)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, base.ClientSize.Width, this._glassMargin.Top));
				}
				bool flag5 = this._glassMargin.Bottom != 0;
				if (flag5)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, base.ClientSize.Height - this._glassMargin.Bottom, base.ClientSize.Width, base.ClientSize.Height));
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BBC File Offset: 0x00001DBC
		private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			bool flag = e.Category == 12 && this._useSystemFont;
			if (flag)
			{
				this.Font = SystemFonts.IconTitleFont;
			}
		}

		// Token: 0x04000025 RID: 37
		private bool _useSystemFont;

		// Token: 0x04000026 RID: 38
		private Padding _glassMargin;

		// Token: 0x04000027 RID: 39
		private bool _allowGlassDragging = true;
	}
}
