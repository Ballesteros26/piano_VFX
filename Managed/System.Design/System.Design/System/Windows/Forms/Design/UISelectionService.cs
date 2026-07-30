using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200003E RID: 62
	internal class UISelectionService : IUISelectionService
	{
		// Token: 0x060001FB RID: 507 RVA: 0x000079A0 File Offset: 0x00005BA0
		public UISelectionService(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
			this._transaction = null;
			this._selectionService = serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
			if (this._selectionService == null)
			{
				IServiceContainer serviceContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
				this._selectionService = new SelectionService(serviceContainer);
				serviceContainer.AddService(typeof(ISelectionService), this._selectionService);
			}
			this._selectionService.SelectionChanged += this.OnSelectionChanged;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00007A4C File Offset: 0x00005C4C
		private ISelectionService SelectionService
		{
			get
			{
				return this._selectionService;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007A54 File Offset: 0x00005C54
		private object GetService(Type service)
		{
			return this._serviceProvider.GetService(service);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007A62 File Offset: 0x00005C62
		public bool SelectionInProgress
		{
			get
			{
				return this._selecting;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007A6A File Offset: 0x00005C6A
		public bool DragDropInProgress
		{
			get
			{
				return this._dragging;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007A72 File Offset: 0x00005C72
		public bool ResizeInProgress
		{
			get
			{
				return this._resizing;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007A7C File Offset: 0x00005C7C
		public bool SetCursor(int x, int y)
		{
			bool flag = false;
			SelectionFrame selectionFrameAt = this.GetSelectionFrameAt(x, y);
			if (selectionFrameAt != null && selectionFrameAt.HitTest(x, y) && selectionFrameAt.SetCursor(x, y))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public void MouseDragBegin(Control container, int x, int y)
		{
			SelectionFrame selectionFrameAt = this.GetSelectionFrameAt(x, y);
			if (selectionFrameAt != null && selectionFrameAt.HitTest(x, y))
			{
				this.SelectionService.SetSelectedComponents(new IComponent[] { selectionFrameAt.Control });
				if (this._transaction == null)
				{
					IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
					this._transaction = designerHost.CreateTransaction("Resize " + ((this.SelectionService.SelectionCount == 1) ? ((IComponent)this.SelectionService.PrimarySelection).Site.Name : "controls"));
				}
				this.ResizeBegin(x, y);
				return;
			}
			this.SelectionBegin(container, x, y);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007B68 File Offset: 0x00005D68
		public void MouseDragMove(int x, int y)
		{
			if (this._selecting)
			{
				this.SelectionContinue(x, y);
				return;
			}
			if (this._resizing)
			{
				this.ResizeContinue(x, y);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007B8C File Offset: 0x00005D8C
		public void MouseDragEnd(bool cancel)
		{
			if (this._selecting)
			{
				this.SelectionEnd(cancel);
			}
			else if (this._resizing)
			{
				this.ResizeEnd(cancel);
				if (this._transaction != null)
				{
					if (cancel)
					{
						this._transaction.Cancel();
					}
					else
					{
						this._transaction.Commit();
					}
					this._transaction = null;
				}
			}
			if (Cursor.Current != Cursors.Default)
			{
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007C00 File Offset: 0x00005E00
		public void DragBegin()
		{
			if (this._transaction == null)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				this._transaction = designerHost.CreateTransaction("Move " + ((this.SelectionService.SelectionCount == 1) ? ((IComponent)this.SelectionService.PrimarySelection).Site.Name : "controls"));
			}
			this._dragging = true;
			this._firstMove = true;
			if (this.SelectionService.PrimarySelection != null)
			{
				((Control)this.SelectionService.PrimarySelection).DoDragDrop(new ControlDataObject((Control)this.SelectionService.PrimarySelection), -2147483645);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007CBC File Offset: 0x00005EBC
		public void DragOver(Control container, int x, int y)
		{
			if (this._dragging)
			{
				if (this._firstMove)
				{
					this._prevMousePosition = new Point(x, y);
					this._firstMove = false;
					return;
				}
				int num = x - this._prevMousePosition.X;
				int num2 = y - this._prevMousePosition.Y;
				this.MoveSelection(container, num, num2);
				this._prevMousePosition = new Point(x, y);
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && designerHost.RootComponent != null)
				{
					((Control)designerHost.RootComponent).Refresh();
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007D54 File Offset: 0x00005F54
		public void DragDrop(bool cancel, Control container, int x, int y)
		{
			if (this._dragging)
			{
				int num = x - this._prevMousePosition.X;
				int num2 = y - this._prevMousePosition.Y;
				this.MoveSelection(container, num, num2);
				this._dragging = false;
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && designerHost.RootComponent != null)
				{
					((Control)designerHost.RootComponent).Refresh();
				}
				Native.SendMessage(((Control)this.SelectionService.PrimarySelection).Handle, Native.Msg.WM_LBUTTONUP, (IntPtr)0, (IntPtr)0);
				if (this._transaction != null)
				{
					if (cancel)
					{
						this._transaction.Cancel();
					}
					else
					{
						this._transaction.Commit();
					}
					this._transaction = null;
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007E20 File Offset: 0x00006020
		private void MoveSelection(Control container, int dx, int dy)
		{
			bool flag = false;
			Control control = null;
			if (((Control)this.SelectionService.PrimarySelection).Parent != container && !this.SelectionService.GetComponentSelected(container))
			{
				flag = true;
				control = ((Control)this.SelectionService.PrimarySelection).Parent;
			}
			foreach (object obj in this.SelectionService.GetSelectedComponents())
			{
				Control control2 = ((Component)obj) as Control;
				if (flag)
				{
					TypeDescriptor.GetProperties(control2)["Parent"].SetValue(control2, container);
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control2)["Location"];
				Point point = (Point)propertyDescriptor.GetValue(control2);
				point.X += dx;
				point.Y += dy;
				propertyDescriptor.SetValue(control2, point);
			}
			if (flag)
			{
				control.Invalidate(false);
				control.Update();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007F34 File Offset: 0x00006134
		public Rectangle SelectionBounds
		{
			get
			{
				return this._selectionRectangle;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007F3C File Offset: 0x0000613C
		private void SelectionBegin(Control container, int x, int y)
		{
			this._selecting = true;
			this._selectionContainer = container;
			this._prevMousePosition = new Point(x, y);
			this._initialMousePosition = this._prevMousePosition;
			this._selectionRectangle = new Rectangle(x, y, 0, 0);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007F74 File Offset: 0x00006174
		private void SelectionContinue(int x, int y)
		{
			if (this._selecting)
			{
				if (x > this._selectionRectangle.Right)
				{
					this._selectionRectangle.Width = x - this._selectionRectangle.X;
				}
				else if (x > this._selectionRectangle.X && x < this._selectionRectangle.Right && x < this._prevMousePosition.X)
				{
					this._selectionRectangle.Width = x - this._selectionRectangle.X;
				}
				else if (x < this._selectionRectangle.X)
				{
					if (this._prevMousePosition.X > this._selectionRectangle.X)
					{
						this._selectionRectangle.X = this._initialMousePosition.X;
						this._selectionRectangle.Width = 0;
					}
					else
					{
						this._selectionRectangle.Width = this._selectionRectangle.Width + (this._selectionRectangle.X - x);
						this._selectionRectangle.X = x;
					}
				}
				else if (x > this._selectionRectangle.X && x < this._selectionRectangle.Right && x > this._prevMousePosition.X)
				{
					if (this._prevMousePosition.X < this._selectionRectangle.X)
					{
						this._selectionRectangle.X = this._initialMousePosition.X;
						this._selectionRectangle.Width = 0;
					}
					else
					{
						this._selectionRectangle.Width = this._selectionRectangle.Width - (x - this._selectionRectangle.X);
						this._selectionRectangle.X = x;
					}
				}
				if (y > this._selectionRectangle.Bottom)
				{
					this._selectionRectangle.Height = y - this._selectionRectangle.Y;
				}
				else if (y > this._selectionRectangle.Y && y < this._selectionRectangle.Bottom && y < this._prevMousePosition.Y)
				{
					this._selectionRectangle.Height = y - this._selectionRectangle.Y;
				}
				else if (y < this._selectionRectangle.Y)
				{
					if (this._prevMousePosition.Y > this._selectionRectangle.Y)
					{
						this._selectionRectangle.Y = this._initialMousePosition.Y;
						this._selectionRectangle.Height = 0;
					}
					else
					{
						this._selectionRectangle.Height = this._selectionRectangle.Height + (this._selectionRectangle.Y - y);
						this._selectionRectangle.Y = y;
					}
				}
				else if (y > this._selectionRectangle.Y && y < this._selectionRectangle.Bottom && y > this._prevMousePosition.Y)
				{
					if (this._prevMousePosition.Y < this._selectionRectangle.Y)
					{
						this._selectionRectangle.Y = this._initialMousePosition.Y;
						this._selectionRectangle.Height = 0;
					}
					else
					{
						this._selectionRectangle.Height = this._selectionRectangle.Height - (y - this._selectionRectangle.Y);
						this._selectionRectangle.Y = y;
					}
				}
				this._prevMousePosition.X = x;
				this._prevMousePosition.Y = y;
				this._selectionContainer.Refresh();
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000082B8 File Offset: 0x000064B8
		private void SelectionEnd(bool cancel)
		{
			this._selecting = false;
			ICollection controlsIn = this.GetControlsIn(this._selectionRectangle);
			if (controlsIn.Count != 0)
			{
				this.SelectionService.SetSelectedComponents(controlsIn, SelectionTypes.Replace);
			}
			this._selectionContainer.Refresh();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000082F9 File Offset: 0x000064F9
		private void ResizeBegin(int x, int y)
		{
			this._resizing = true;
			this._selectionFrame = this.GetSelectionFrameAt(x, y);
			this._selectionFrame.ResizeBegin(x, y);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008320 File Offset: 0x00006520
		private void ResizeContinue(int x, int y)
		{
			Rectangle rectangle = this._selectionFrame.ResizeContinue(x, y);
			foreach (object obj in this.SelectionService.GetSelectedComponents())
			{
				IComponent component = (IComponent)obj;
				if (component is Control)
				{
					SelectionFrame selectionFrameFor = this.GetSelectionFrameFor((Control)component);
					if (selectionFrameFor != this._selectionFrame)
					{
						selectionFrameFor.Resize(rectangle);
					}
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000083B0 File Offset: 0x000065B0
		private void ResizeEnd(bool cancel)
		{
			this._selectionFrame.ResizeEnd(cancel);
			this._resizing = false;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000083C8 File Offset: 0x000065C8
		private SelectionFrame GetSelectionFrameAt(int x, int y)
		{
			SelectionFrame selectionFrame = null;
			foreach (object obj in this._selectionFrames)
			{
				SelectionFrame selectionFrame2 = (SelectionFrame)obj;
				if (selectionFrame2.Bounds.Contains(new Point(x, y)))
				{
					selectionFrame = selectionFrame2;
					break;
				}
			}
			return selectionFrame;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000843C File Offset: 0x0000663C
		private SelectionFrame GetSelectionFrameFor(Control control)
		{
			foreach (object obj in this._selectionFrames)
			{
				SelectionFrame selectionFrame = (SelectionFrame)obj;
				if (control == selectionFrame.Control)
				{
					return selectionFrame;
				}
			}
			return null;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000084A0 File Offset: 0x000066A0
		public bool AdornmentsHitTest(Control control, int x, int y)
		{
			SelectionFrame selectionFrameAt = this.GetSelectionFrameAt(x, y);
			return selectionFrameAt != null && selectionFrameAt.HitTest(x, y);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000084C4 File Offset: 0x000066C4
		public void PaintAdornments(Control container, Graphics gfx)
		{
			if (!(this.GetService(typeof(IDesignerHost)) is IDesignerHost) || !(this.SelectionService.PrimarySelection is Control))
			{
				return;
			}
			if ((Control)this.SelectionService.PrimarySelection == container)
			{
				if (this._selecting)
				{
					Color color = Color.FromArgb((int)(~this._selectionContainer.BackColor.R), (int)(~this._selectionContainer.BackColor.G), (int)(~this._selectionContainer.BackColor.B));
					this.DrawSelectionRectangle(gfx, this._selectionRectangle, color);
					return;
				}
			}
			else if (((Control)this.SelectionService.PrimarySelection).Parent == container)
			{
				foreach (object obj in this._selectionFrames)
				{
					((SelectionFrame)obj).OnPaint(gfx);
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000085D0 File Offset: 0x000067D0
		private void DrawSelectionRectangle(Graphics gfx, Rectangle frame, Color color)
		{
			gfx.DrawRectangle(new Pen(color)
			{
				DashStyle = DashStyle.Dash
			}, frame);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000085F4 File Offset: 0x000067F4
		private void OnSelectionChanged(object sender, EventArgs args)
		{
			ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
			if (this._selectionFrames.Count == 0)
			{
				using (IEnumerator enumerator = selectedComponents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Component component = (Component)obj;
						this._selectionFrames.Add(new SelectionFrame((Control)component));
					}
					goto IL_0114;
				}
			}
			int num = 0;
			foreach (object obj2 in selectedComponents)
			{
				Component component2 = (Component)obj2;
				if (num >= this._selectionFrames.Count)
				{
					this._selectionFrames.Add(new SelectionFrame((Control)component2));
				}
				else
				{
					((SelectionFrame)this._selectionFrames[num]).Control = (Control)component2;
				}
				num++;
			}
			if (num < this._selectionFrames.Count)
			{
				this._selectionFrames.RemoveRange(num, this._selectionFrames.Count - num);
			}
			IL_0114:
			Control control = (this.GetService(typeof(IDesignerHost)) as IDesignerHost).RootComponent as Control;
			if (control != null)
			{
				if (control.Parent != null)
				{
					control.Parent.Refresh();
					return;
				}
				control.Refresh();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008770 File Offset: 0x00006970
		private ICollection GetControlsIn(Rectangle rect)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._selectionContainer.Controls)
			{
				Control control = (Control)obj;
				if (rect.Contains(control.Bounds) || rect.IntersectsWith(control.Bounds))
				{
					arrayList.Add(control);
				}
			}
			return arrayList;
		}

		// Token: 0x040000E6 RID: 230
		private IServiceProvider _serviceProvider;

		// Token: 0x040000E7 RID: 231
		private DesignerTransaction _transaction;

		// Token: 0x040000E8 RID: 232
		private ISelectionService _selectionService;

		// Token: 0x040000E9 RID: 233
		private bool _dragging;

		// Token: 0x040000EA RID: 234
		private Point _prevMousePosition;

		// Token: 0x040000EB RID: 235
		private bool _firstMove;

		// Token: 0x040000EC RID: 236
		private bool _selecting;

		// Token: 0x040000ED RID: 237
		private Control _selectionContainer;

		// Token: 0x040000EE RID: 238
		private Point _initialMousePosition;

		// Token: 0x040000EF RID: 239
		private Rectangle _selectionRectangle;

		// Token: 0x040000F0 RID: 240
		private ArrayList _selectionFrames = new ArrayList();

		// Token: 0x040000F1 RID: 241
		private SelectionFrame _selectionFrame;

		// Token: 0x040000F2 RID: 242
		private bool _resizing;
	}
}
