using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000035 RID: 53
	internal class SelectionFrame
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000663B File Offset: 0x0000483B
		public SelectionFrame(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this._control = control;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000666C File Offset: 0x0000486C
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000066F1 File Offset: 0x000048F1
		public Rectangle Bounds
		{
			get
			{
				this._bounds.X = this._control.Location.X - 7;
				this._bounds.Y = this._control.Location.Y - 7;
				this._bounds.Width = this._control.Width + 14;
				this._bounds.Height = this._control.Height + 14;
				return this._bounds;
			}
			set
			{
				this._bounds = value;
				this._control.Bounds = this._bounds;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000670C File Offset: 0x0000490C
		private SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = SelectionRules.AllSizeable;
				if (this._control.Site != null)
				{
					IDesignerHost designerHost = this._control.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						ControlDesigner controlDesigner = designerHost.GetDesigner(this._control) as ControlDesigner;
						if (controlDesigner != null)
						{
							selectionRules = controlDesigner.SelectionRules;
						}
					}
				}
				return selectionRules;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006769 File Offset: 0x00004969
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00006771 File Offset: 0x00004971
		public Control Control
		{
			get
			{
				return this._control;
			}
			set
			{
				if (value != null)
				{
					this._control = value;
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000677D File Offset: 0x0000497D
		public Control Parent
		{
			get
			{
				if (this._control.Parent == null)
				{
					return this._control;
				}
				return this._control.Parent;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000679E File Offset: 0x0000499E
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000067A6 File Offset: 0x000049A6
		private SelectionFrame.GrabHandle GrabHandleSelected
		{
			get
			{
				return this._handle;
			}
			set
			{
				this._handle = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000067B0 File Offset: 0x000049B0
		private bool PrimarySelection
		{
			get
			{
				bool flag = false;
				if (this.Control != null && this.Control.Site != null)
				{
					ISelectionService selectionService = this.Control.Site.GetService(typeof(ISelectionService)) as ISelectionService;
					if (selectionService != null && selectionService.PrimarySelection == this.Control)
					{
						flag = true;
					}
				}
				return flag;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006808 File Offset: 0x00004A08
		public void OnPaint(Graphics gfx)
		{
			this.DrawFrame(gfx);
			this.DrawGrabHandles(gfx);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006818 File Offset: 0x00004A18
		private void DrawGrabHandles(Graphics gfx)
		{
			GraphicsState graphicsState = gfx.Save();
			gfx.TranslateTransform((float)this.Bounds.X, (float)this.Bounds.Y);
			for (int i = 0; i < this._handles.Length; i++)
			{
				this._handles[i].Width = 7;
				this._handles[i].Height = 7;
			}
			SelectionRules selectionRules = this.SelectionRules;
			bool primarySelection = this.PrimarySelection;
			bool flag = false;
			this._handles[0].Location = new Point(0, 0);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable | SelectionRules.TopSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[0], primarySelection, flag);
			flag = false;
			this._handles[1].Location = new Point((this.Bounds.Width - 7) / 2, 0);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[1], primarySelection, flag);
			flag = false;
			this._handles[2].Location = new Point(this.Bounds.Width - 7, 0);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.RightSizeable | SelectionRules.TopSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[2], primarySelection, flag);
			flag = false;
			this._handles[3].Location = new Point(this.Bounds.Width - 7, (this.Bounds.Height - 7) / 2);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.RightSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[3], primarySelection, flag);
			flag = false;
			this._handles[4].Location = new Point(this.Bounds.Width - 7, this.Bounds.Height - 7);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.RightSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[4], primarySelection, flag);
			flag = false;
			this._handles[5].Location = new Point((this.Bounds.Width - 7) / 2, this.Bounds.Height - 7);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[5], primarySelection, flag);
			flag = false;
			this._handles[6].Location = new Point(0, this.Bounds.Height - 7);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.LeftSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[6], primarySelection, flag);
			flag = false;
			this._handles[7].Location = new Point(0, (this.Bounds.Height - 7) / 2);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
			{
				flag = true;
			}
			ControlPaint.DrawGrabHandle(gfx, this._handles[7], primarySelection, flag);
			gfx.Restore(graphicsState);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006B20 File Offset: 0x00004D20
		protected void DrawFrame(Graphics gfx)
		{
			Color color = Color.FromArgb((int)(~this._control.Parent.BackColor.R), (int)(~this._control.Parent.BackColor.G), (int)(~this._control.Parent.BackColor.B));
			Pen pen = new Pen(new HatchBrush(HatchStyle.Percent30, color, Color.FromArgb(0)), 7f);
			gfx.DrawRectangle(pen, this.Control.Bounds);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006BAC File Offset: 0x00004DAC
		public bool SetCursor(int x, int y)
		{
			bool flag = false;
			if (!this._resizing)
			{
				SelectionFrame.GrabHandle grabHandle = this.PointToGrabHandle(this.PointToClient(Control.MousePosition));
				if (grabHandle != SelectionFrame.GrabHandle.None)
				{
					flag = true;
				}
				if (grabHandle == SelectionFrame.GrabHandle.TopLeft)
				{
					Cursor.Current = Cursors.SizeNWSE;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.TopMiddle)
				{
					Cursor.Current = Cursors.SizeNS;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.TopRight)
				{
					Cursor.Current = Cursors.SizeNESW;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.Right)
				{
					Cursor.Current = Cursors.SizeWE;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.BottomRight)
				{
					Cursor.Current = Cursors.SizeNWSE;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.BottomMiddle)
				{
					Cursor.Current = Cursors.SizeNS;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.BottomLeft)
				{
					Cursor.Current = Cursors.SizeNESW;
				}
				else if (grabHandle == SelectionFrame.GrabHandle.Left)
				{
					Cursor.Current = Cursors.SizeWE;
				}
				else
				{
					Cursor.Current = Cursors.Default;
				}
			}
			return flag;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006C68 File Offset: 0x00004E68
		public void ResizeBegin(int x, int y)
		{
			this.GrabHandleSelected = this.PointToGrabHandle(this.PointToClient(this.Parent.PointToScreen(new Point(x, y))));
			if (this.GrabHandleSelected != SelectionFrame.GrabHandle.None)
			{
				this._resizing = true;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006C9E File Offset: 0x00004E9E
		private bool CheckSelectionRules(SelectionRules rules, SelectionRules toCheck)
		{
			return (rules & toCheck) == toCheck;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public Rectangle ResizeContinue(int x, int y)
		{
			Rectangle rectangle = (Rectangle)TypeDescriptor.GetProperties(this._control)["Bounds"].GetValue(this._control);
			Rectangle rectangle2 = rectangle;
			Point point = new Point(x, y);
			SelectionRules selectionRules = this.SelectionRules;
			if (this._resizing && this.GrabHandleSelected != SelectionFrame.GrabHandle.None && selectionRules != SelectionRules.Locked)
			{
				if (this.GrabHandleSelected == SelectionFrame.GrabHandle.TopLeft && this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable | SelectionRules.TopSizeable))
				{
					int num = this._control.Top;
					int num2 = this._control.Height;
					int num3 = this._control.Left;
					int num4 = this._control.Width;
					if (point.Y < this._control.Bottom)
					{
						num = point.Y;
						num2 = this._control.Bottom - point.Y;
					}
					if (point.X < this._control.Right)
					{
						num3 = point.X;
						num4 = this._control.Right - point.X;
						rectangle = new Rectangle(num3, num, num4, num2);
					}
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.TopRight && this.CheckSelectionRules(selectionRules, SelectionRules.RightSizeable | SelectionRules.TopSizeable))
				{
					int num = this._control.Top;
					int num2 = this._control.Height;
					int num4 = this._control.Width;
					if (point.Y < this._control.Bottom)
					{
						num = point.Y;
						num2 = this._control.Bottom - point.Y;
					}
					num4 = point.X - this._control.Left;
					rectangle = new Rectangle(this._control.Left, num, num4, num2);
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.TopMiddle && this.CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
				{
					if (point.Y < this._control.Bottom)
					{
						int num = point.Y;
						int num2 = this._control.Bottom - point.Y;
						rectangle = new Rectangle(this._control.Left, num, this._control.Width, num2);
					}
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.Right && this.CheckSelectionRules(selectionRules, SelectionRules.RightSizeable))
				{
					int num4 = point.X - this._control.Left;
					rectangle = new Rectangle(this._control.Left, this._control.Top, num4, this._control.Height);
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.BottomRight && this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.RightSizeable))
				{
					int num4 = point.X - this._control.Left;
					int num2 = point.Y - this._control.Top;
					rectangle = new Rectangle(this._control.Left, this._control.Top, num4, num2);
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.BottomMiddle && this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable))
				{
					int num2 = point.Y - this._control.Top;
					rectangle = new Rectangle(this._control.Left, this._control.Top, this._control.Width, num2);
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.BottomLeft && this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.LeftSizeable))
				{
					int num2 = this._control.Height;
					int num3 = this._control.Left;
					int num4 = this._control.Width;
					if (point.X < this._control.Right)
					{
						num3 = point.X;
						num4 = this._control.Right - point.X;
					}
					num2 = point.Y - this._control.Top;
					rectangle = new Rectangle(num3, this._control.Top, num4, num2);
				}
				else if (this.GrabHandleSelected == SelectionFrame.GrabHandle.Left && this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable) && point.X < this._control.Right)
				{
					int num3 = point.X;
					int num4 = this._control.Right - point.X;
					rectangle = new Rectangle(num3, this._control.Top, num4, this._control.Height);
				}
				TypeDescriptor.GetProperties(this._control)["Bounds"].SetValue(this._control, rectangle);
			}
			this.Parent.Refresh();
			rectangle2.X = rectangle.X - rectangle2.X;
			rectangle2.Y = rectangle.Y - rectangle2.Y;
			rectangle2.Height = rectangle.Height - rectangle2.Height;
			rectangle2.Width = rectangle.Width - rectangle2.Width;
			return rectangle2;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000718F File Offset: 0x0000538F
		public void ResizeEnd(bool cancel)
		{
			this.GrabHandleSelected = SelectionFrame.GrabHandle.None;
			this._resizing = false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000071A0 File Offset: 0x000053A0
		public void Resize(Rectangle deltaBounds)
		{
			SelectionRules selectionRules = this.SelectionRules;
			if (this.CheckSelectionRules(selectionRules, SelectionRules.Locked) || !this.CheckSelectionRules(selectionRules, SelectionRules.Moveable))
			{
				return;
			}
			Rectangle rectangle = (Rectangle)TypeDescriptor.GetProperties(this._control)["Bounds"].GetValue(this._control);
			if (this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
			{
				rectangle.X += deltaBounds.X;
				rectangle.Width += deltaBounds.Width;
			}
			if (this.CheckSelectionRules(selectionRules, SelectionRules.RightSizeable) && !this.CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
			{
				rectangle.Y += deltaBounds.Y;
				rectangle.Width += deltaBounds.Width;
			}
			if (this.CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				rectangle.Y += deltaBounds.Y;
				rectangle.Height += deltaBounds.Height;
			}
			if (this.CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable) && !this.CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				rectangle.Height += deltaBounds.Height;
			}
			TypeDescriptor.GetProperties(this._control)["Bounds"].SetValue(this._control, rectangle);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000072EC File Offset: 0x000054EC
		public bool HitTest(int x, int y)
		{
			return this.PointToGrabHandle(this.PointToClient(this.Parent.PointToScreen(new Point(x, y)))) != SelectionFrame.GrabHandle.None;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007314 File Offset: 0x00005514
		private SelectionFrame.GrabHandle PointToGrabHandle(Point pointerLocation)
		{
			SelectionFrame.GrabHandle grabHandle;
			if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[0]))
			{
				grabHandle = SelectionFrame.GrabHandle.TopLeft;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[1]))
			{
				grabHandle = SelectionFrame.GrabHandle.TopMiddle;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[2]))
			{
				grabHandle = SelectionFrame.GrabHandle.TopRight;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[3]))
			{
				grabHandle = SelectionFrame.GrabHandle.Right;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[4]))
			{
				grabHandle = SelectionFrame.GrabHandle.BottomRight;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[5]))
			{
				grabHandle = SelectionFrame.GrabHandle.BottomMiddle;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[6]))
			{
				grabHandle = SelectionFrame.GrabHandle.BottomLeft;
			}
			else if (this.IsCursorOnGrabHandle(pointerLocation, this._handles[7]))
			{
				grabHandle = SelectionFrame.GrabHandle.Left;
			}
			else
			{
				grabHandle = SelectionFrame.GrabHandle.None;
			}
			return grabHandle;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000073F4 File Offset: 0x000055F4
		private bool IsCursorOnGrabHandle(Point pointerLocation, Rectangle handleRectangle)
		{
			return pointerLocation.X >= handleRectangle.X && pointerLocation.X <= handleRectangle.X + handleRectangle.Width && pointerLocation.Y >= handleRectangle.Y && pointerLocation.Y <= handleRectangle.Y + handleRectangle.Height;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007454 File Offset: 0x00005654
		private Point PointToClient(Point screenPoint)
		{
			Point point = this.Parent.PointToClient(screenPoint);
			point.X -= this.Bounds.X;
			point.Y -= this.Bounds.Y;
			return point;
		}

		// Token: 0x040000C7 RID: 199
		private Rectangle _bounds;

		// Token: 0x040000C8 RID: 200
		private Control _control;

		// Token: 0x040000C9 RID: 201
		private Rectangle[] _handles = new Rectangle[8];

		// Token: 0x040000CA RID: 202
		private SelectionFrame.GrabHandle _handle = SelectionFrame.GrabHandle.None;

		// Token: 0x040000CB RID: 203
		private const int BORDER_SIZE = 7;

		// Token: 0x040000CC RID: 204
		private bool _resizing;

		// Token: 0x02000036 RID: 54
		private enum GrabHandle
		{
			// Token: 0x040000CE RID: 206
			None = -1,
			// Token: 0x040000CF RID: 207
			TopLeft,
			// Token: 0x040000D0 RID: 208
			TopMiddle,
			// Token: 0x040000D1 RID: 209
			TopRight,
			// Token: 0x040000D2 RID: 210
			Right,
			// Token: 0x040000D3 RID: 211
			BottomRight,
			// Token: 0x040000D4 RID: 212
			BottomMiddle,
			// Token: 0x040000D5 RID: 213
			BottomLeft,
			// Token: 0x040000D6 RID: 214
			Left,
			// Token: 0x040000D7 RID: 215
			Border
		}
	}
}
