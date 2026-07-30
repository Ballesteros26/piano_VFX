using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	/// <summary>Extends the design mode behavior of a <see cref="T:System.Windows.Forms.Control" />.</summary>
	// Token: 0x02000011 RID: 17
	public class ControlDesigner : ComponentDesigner, IMessageReceiver
	{
		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate the designer with. This component must always be an instance of, or derive from, <see cref="T:System.Windows.Forms.Control" />. </param>
		// Token: 0x0600008A RID: 138 RVA: 0x000027F8 File Offset: 0x000009F8
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (!(component is Control))
			{
				throw new ArgumentException("Component is not a Control.");
			}
			this.Control.Text = component.Site.Name;
			this._messageRouter = new WndProcRouter((Control)component, this);
			this.Control.WindowTarget = this._messageRouter;
			this.Visible = true;
			this.Enabled = true;
			this.Locked = false;
			this.AllowDrop = false;
			this.Control.Enabled = true;
			this.Control.Visible = true;
			this.Control.AllowDrop = false;
			this.Control.DragDrop += new DragEventHandler(this.OnDragDrop);
			this.Control.DragEnter += new DragEventHandler(this.OnDragEnter);
			this.Control.DragLeave += this.OnDragLeave;
			this.Control.DragOver += new DragEventHandler(this.OnDragOver);
			if (this.Control.IsHandleCreated)
			{
				this.OnCreateHandle();
			}
		}

		/// <summary>Called when the designer is intialized.</summary>
		// Token: 0x0600008B RID: 139 RVA: 0x00002908 File Offset: 0x00000B08
		public override void OnSetComponentDefaults()
		{
			if (base.Component != null && base.Component.Site != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
				if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly && propertyDescriptor.PropertyType == typeof(string))
				{
					propertyDescriptor.SetValue(base.Component, base.Component.Site.Name);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> from the design environment.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />, or null if the service is unavailable.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000234B File Offset: 0x0000054B
		protected internal BehaviorService BehaviorService
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the control that the designer is designing.</summary>
		/// <returns>The control that the designer is designing.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000297E File Offset: 0x00000B7E
		public virtual Control Control
		{
			get
			{
				return (Control)base.Component;
			}
		}

		/// <summary>Gets a value indicating whether drag rectangles can be drawn on this designer component.</summary>
		/// <returns>true if drag rectangles can be drawn; otherwise, false.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool EnableDragRect
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the selection rules that indicate the movement capabilities of a component.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.Design.SelectionRules" /> values.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000298C File Offset: 0x00000B8C
		public virtual SelectionRules SelectionRules
		{
			get
			{
				if (this.Control == null)
				{
					return SelectionRules.None;
				}
				SelectionRules selectionRules = SelectionRules.Visible;
				if ((bool)this.GetValue(base.Component, "Locked"))
				{
					selectionRules |= SelectionRules.Locked;
				}
				else
				{
					switch ((DockStyle)this.GetValue(base.Component, "Dock", typeof(DockStyle)))
					{
					case 1:
						selectionRules |= SelectionRules.BottomSizeable;
						break;
					case 2:
						selectionRules |= SelectionRules.TopSizeable;
						break;
					case 3:
						selectionRules |= SelectionRules.RightSizeable;
						break;
					case 4:
						selectionRules |= SelectionRules.LeftSizeable;
						break;
					case 5:
						break;
					default:
						selectionRules |= SelectionRules.Moveable;
						selectionRules |= SelectionRules.AllSizeable;
						break;
					}
				}
				return selectionRules;
			}
		}

		/// <summary>Gets the collection of components associated with the component managed by the designer.</summary>
		/// <returns>The components that are associated with the component managed by the designer.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002A30 File Offset: 0x00000C30
		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.Control.Controls)
				{
					Control control = (Control)obj;
					if (control.Site != null)
					{
						arrayList.Add(control);
					}
				}
				return arrayList;
			}
		}

		/// <summary>Gets the parent component for the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
		/// <returns>The parent component for the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />; otherwise, null if there is no parent component.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002AA0 File Offset: 0x00000CA0
		protected override IComponent ParentComponent
		{
			get
			{
				return this.GetValue(this.Control, "Parent") as Control;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public virtual AccessibleObject AccessibilityObject
		{
			get
			{
				if (this.accessibilityObj == null)
				{
					this.accessibilityObj = new AccessibleObject();
				}
				return this.accessibilityObj;
			}
		}

		/// <summary>Provides default processing for Windows messages.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000093 RID: 147 RVA: 0x00002AD3 File Offset: 0x00000CD3
		protected void DefWndProc(ref Message m)
		{
			this._messageRouter.ToControl(ref m);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000094 RID: 148 RVA: 0x00002AE1 File Offset: 0x00000CE1
		protected void BaseWndProc(ref Message m)
		{
			this._messageRouter.ToSystem(ref m);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002AEF File Offset: 0x00000CEF
		void IMessageReceiver.WndProc(ref Message m)
		{
			this.WndProc(ref m);
		}

		/// <summary>Processes Windows messages and optionally routes them to the control.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000096 RID: 150 RVA: 0x00002AF8 File Offset: 0x00000CF8
		protected virtual void WndProc(ref Message m)
		{
			if (m.Msg >= 256 && m.Msg <= 264)
			{
				return;
			}
			if (this.IsMouseMessage((Native.Msg)m.Msg) && this.GetHitTest(new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam))))
			{
				this.DefWndProc(ref m);
				return;
			}
			Native.Msg msg = (Native.Msg)m.Msg;
			if (msg > Native.Msg.WM_SETCURSOR)
			{
				if (msg <= Native.Msg.WM_NCMBUTTONUP)
				{
					if (msg == Native.Msg.WM_CONTEXTMENU)
					{
						this.OnContextMenu(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
						return;
					}
					switch (msg)
					{
					case Native.Msg.WM_NCLBUTTONDOWN:
					case Native.Msg.WM_NCLBUTTONDBLCLK:
					case Native.Msg.WM_NCRBUTTONDOWN:
					case Native.Msg.WM_NCRBUTTONDBLCLK:
					case Native.Msg.WM_NCMBUTTONDOWN:
						return;
					case Native.Msg.WM_NCLBUTTONUP:
					case Native.Msg.WM_NCRBUTTONUP:
					case Native.Msg.WM_NCMBUTTONUP:
						break;
					default:
						goto IL_04F5;
					}
				}
				else
				{
					switch (msg)
					{
					case Native.Msg.WM_MOUSEFIRST:
					{
						if (this._mouseMoveAfterMouseDown)
						{
							this._mouseMoveAfterMouseDown = false;
							this.BaseWndProc(ref m);
							return;
						}
						IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
						ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
						IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (iuiselectionService != null && selectionService != null && designerHost != null)
						{
							Control control = selectionService.PrimarySelection as Control;
							Point point = new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
							if (iuiselectionService.SelectionInProgress && base.Component != designerHost.RootComponent && base.Component != selectionService.PrimarySelection)
							{
								point = control.PointToClient(this.Control.PointToScreen(point));
								Native.SendMessage(control.Handle, (Native.Msg)m.Msg, m.WParam, Native.LParam(point.X, point.Y));
							}
							else if (iuiselectionService.ResizeInProgress && this.Control.Parent == ((Control)selectionService.PrimarySelection).Parent)
							{
								point = this.Control.Parent.PointToClient(this.Control.PointToScreen(point));
								Native.SendMessage(this.Control.Parent.Handle, (Native.Msg)m.Msg, m.WParam, Native.LParam(point.X, point.Y));
							}
							else
							{
								this.OnMouseMove(point.X, point.Y);
							}
						}
						else
						{
							this.OnMouseMove(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
						}
						this.BaseWndProc(ref m);
						return;
					}
					case Native.Msg.WM_LBUTTONDOWN:
					case Native.Msg.WM_RBUTTONDOWN:
					case Native.Msg.WM_MBUTTONDOWN:
						this._mouseMoveAfterMouseDown = true;
						if (m.Msg == 513)
						{
							this._mouseButtonDown = 1048576;
						}
						else if (m.Msg == 516)
						{
							this._mouseButtonDown = 2097152;
						}
						else if (m.Msg == 519)
						{
							this._mouseButtonDown = 4194304;
						}
						if (this._firstMouseMoveInClient)
						{
							this.OnMouseEnter();
							this._firstMouseMoveInClient = false;
						}
						this.OnMouseDown(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
						this.BaseWndProc(ref m);
						return;
					case Native.Msg.WM_LBUTTONUP:
					case Native.Msg.WM_RBUTTONUP:
					case Native.Msg.WM_MBUTTONUP:
						break;
					case Native.Msg.WM_LBUTTONDBLCLK:
					case Native.Msg.WM_RBUTTONDBLCLK:
					case Native.Msg.WM_MBUTTONDBLCLK:
						if (m.Msg == 515)
						{
							this._mouseButtonDown = 1048576;
						}
						else if (m.Msg == 518)
						{
							this._mouseButtonDown = 2097152;
						}
						else if (m.Msg == 521)
						{
							this._mouseButtonDown = 4194304;
						}
						this.OnMouseDoubleClick();
						this.BaseWndProc(ref m);
						return;
					default:
						if (msg == Native.Msg.WM_MOUSEHOVER)
						{
							this.OnMouseHover();
							return;
						}
						if (msg != Native.Msg.WM_MOUSELEAVE)
						{
							goto IL_04F5;
						}
						this._firstMouseMoveInClient = false;
						this.OnMouseLeave();
						this.BaseWndProc(ref m);
						return;
					}
				}
				this._mouseMoveAfterMouseDown = false;
				this.OnMouseUp();
				this.BaseWndProc(ref m);
				return;
			}
			if (msg <= Native.Msg.WM_SETFOCUS)
			{
				if (msg != Native.Msg.WM_CREATE)
				{
					if (msg == Native.Msg.WM_SETFOCUS)
					{
						this.DefWndProc(ref m);
						return;
					}
				}
				else
				{
					this.DefWndProc(ref m);
					if (m.HWnd == this.Control.Handle)
					{
						this.OnCreateHandle();
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == Native.Msg.WM_PAINT)
				{
					this.DefWndProc(ref m);
					Graphics graphics = Graphics.FromHwnd(m.HWnd);
					PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, this.Control.Bounds);
					this.OnPaintAdornments(paintEventArgs);
					graphics.Dispose();
					paintEventArgs.Dispose();
					return;
				}
				if (msg == Native.Msg.WM_CANCELMODE)
				{
					this.OnMouseDragEnd(true);
					this.DefWndProc(ref m);
					return;
				}
				if (msg == Native.Msg.WM_SETCURSOR)
				{
					if (this.GetHitTest(new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam))))
					{
						this.DefWndProc(ref m);
						return;
					}
					this.OnSetCursor();
					return;
				}
			}
			IL_04F5:
			this.DefWndProc(ref m);
		}

		/// <summary>Indicates whether a mouse click at the specified point should be handled by the control.</summary>
		/// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> indicating the position at which the mouse was clicked, in screen coordinates. </param>
		// Token: 0x06000097 RID: 151 RVA: 0x0000241E File Offset: 0x0000061E
		protected virtual bool GetHitTest(Point point)
		{
			return false;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003001 File Offset: 0x00001201
		private bool IsMouseMessage(Native.Msg msg)
		{
			return (msg >= Native.Msg.WM_MOUSEFIRST && msg <= Native.Msg.WM_MOUSEWHEEL) || (msg >= Native.Msg.WM_NCLBUTTONDOWN && msg <= Native.Msg.WM_NCMBUTTONDBLCLK) || (msg == Native.Msg.WM_MOUSEHOVER || msg == Native.Msg.WM_MOUSELEAVE);
		}

		/// <summary>Receives a call each time the cursor needs to be set.</summary>
		// Token: 0x06000099 RID: 153 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnSetCursor()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000303C File Offset: 0x0000123C
		private void OnMouseDoubleClick()
		{
			try
			{
				base.DoDefaultAction();
			}
			catch (Exception ex)
			{
				this.DisplayError(ex);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000306C File Offset: 0x0000126C
		internal virtual void OnMouseDown(int x, int y)
		{
			this._mouseDown = true;
			this._mouseDownFirstMove = true;
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService == null || !iuiselectionService.AdornmentsHitTest(this.Control, x, y))
			{
				ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new IComponent[] { base.Component });
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000030DE File Offset: 0x000012DE
		internal virtual void OnMouseMove(int x, int y)
		{
			if (this._mouseDown)
			{
				if (this._mouseDownFirstMove)
				{
					this.OnMouseDragBegin(x, y);
					this._mouseDownFirstMove = false;
					return;
				}
				this.OnMouseDragMove(x, y);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003108 File Offset: 0x00001308
		internal virtual void OnMouseUp()
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (this._mouseDown)
			{
				this.OnMouseDragEnd(false);
				if (iuiselectionService != null && (iuiselectionService.SelectionInProgress || iuiselectionService.ResizeInProgress))
				{
					iuiselectionService.MouseDragEnd(false);
				}
				this._mouseDown = false;
				return;
			}
			if (iuiselectionService != null && (iuiselectionService.SelectionInProgress || iuiselectionService.ResizeInProgress))
			{
				iuiselectionService.MouseDragEnd(false);
			}
		}

		/// <summary>Shows the context menu and provides an opportunity to perform additional processing when the context menu is about to be displayed.</summary>
		/// <param name="x">The x coordinate at which to display the context menu. </param>
		/// <param name="y">The y coordinate at which to display the context menu. </param>
		// Token: 0x0600009E RID: 158 RVA: 0x00003178 File Offset: 0x00001378
		protected virtual void OnContextMenu(int x, int y)
		{
			IMenuCommandService menuCommandService = this.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
			if (menuCommandService != null)
			{
				menuCommandService.ShowContextMenu(MenuCommands.SelectionMenu, x, y);
			}
		}

		/// <summary>Receives a call when the mouse first enters the control.</summary>
		// Token: 0x0600009F RID: 159 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnMouseEnter()
		{
		}

		/// <summary>Receives a call after the mouse hovers over the control.</summary>
		// Token: 0x060000A0 RID: 160 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnMouseHover()
		{
		}

		/// <summary>Receives a call when the mouse first enters the control.</summary>
		// Token: 0x060000A1 RID: 161 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnMouseLeave()
		{
		}

		/// <summary>Provides an opportunity to perform additional processing immediately after the control handle has been created.</summary>
		// Token: 0x060000A2 RID: 162 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnCreateHandle()
		{
		}

		/// <summary>Receives a call when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> the designer can use to draw on the control. </param>
		// Token: 0x060000A3 RID: 163 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnPaintAdornments(PaintEventArgs pe)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000031AB File Offset: 0x000013AB
		internal MouseButtons MouseButtonDown
		{
			get
			{
				return this._mouseButtonDown;
			}
		}

		/// <summary>Receives a call in response to the left mouse button being pressed and held while over the component.</summary>
		/// <param name="x">The x position of the mouse in screen coordinates. </param>
		/// <param name="y">The y position of the mouse in screen coordinates. </param>
		// Token: 0x060000A5 RID: 165 RVA: 0x000031B4 File Offset: 0x000013B4
		protected virtual void OnMouseDragBegin(int x, int y)
		{
			IUISelectionService iuiselectionService = this.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (iuiselectionService != null && (this.SelectionRules & SelectionRules.Moveable) == SelectionRules.Moveable)
			{
				iuiselectionService.DragBegin();
			}
		}

		/// <summary>Receives a call for each movement of the mouse during a drag-and-drop operation.</summary>
		/// <param name="x">The x position of the mouse in screen coordinates. </param>
		/// <param name="y">The y position of the mouse in screen coordinates. </param>
		// Token: 0x060000A6 RID: 166 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnMouseDragMove(int x, int y)
		{
		}

		/// <summary>Receives a call at the end of a drag-and-drop operation to complete or cancel the operation.</summary>
		/// <param name="cancel">true to cancel the drag; false to commit it. </param>
		// Token: 0x060000A7 RID: 167 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnMouseDragEnd(bool cancel)
		{
		}

		/// <summary>Routes messages from the child controls of the specified control to the designer.</summary>
		/// <param name="firstChild">The first child <see cref="T:System.Windows.Forms.Control" /> to process. This method may recursively call itself for children of the control. </param>
		// Token: 0x060000A8 RID: 168 RVA: 0x000031F4 File Offset: 0x000013F4
		protected void HookChildControls(Control firstChild)
		{
			if (firstChild != null)
			{
				foreach (object obj in firstChild.Controls)
				{
					Control control = (Control)obj;
					control.WindowTarget = new WndProcRouter(control, this);
				}
			}
		}

		/// <summary>Routes messages for the children of the specified control to each control rather than to a parent designer.</summary>
		/// <param name="firstChild">The first child <see cref="T:System.Windows.Forms.Control" /> to process. This method may recursively call itself for children of the control. </param>
		// Token: 0x060000A9 RID: 169 RVA: 0x00003254 File Offset: 0x00001454
		protected void UnhookChildControls(Control firstChild)
		{
			if (firstChild != null)
			{
				foreach (object obj in firstChild.Controls)
				{
					Control control = (Control)obj;
					if (control.WindowTarget is WndProcRouter)
					{
						((WndProcRouter)control.WindowTarget).Dispose();
					}
				}
			}
		}

		/// <summary>Indicates if this designer's control can be parented by the control of the specified designer.</summary>
		/// <returns>true if the control managed by the specified designer can parent the control managed by this designer; otherwise, false.</returns>
		/// <param name="parentDesigner">The <see cref="T:System.ComponentModel.Design.IDesigner" /> that manages the control to check. </param>
		// Token: 0x060000AA RID: 170 RVA: 0x000032C8 File Offset: 0x000014C8
		public virtual bool CanBeParentedTo(IDesigner parentDesigner)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			return parentDesigner is ParentControlDesigner && base.Component != designerHost.RootComponent && !this.Control.Controls.Contains(((ParentControlDesigner)parentDesigner).Control);
		}

		/// <summary>Displays information about the specified exception to the user.</summary>
		/// <param name="e">The <see cref="T:System.Exception" /> to display. </param>
		// Token: 0x060000AB RID: 171 RVA: 0x00003324 File Offset: 0x00001524
		protected void DisplayError(Exception e)
		{
			if (e != null)
			{
				IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					iuiservice.ShowError(e);
					return;
				}
				string text = e.Message;
				if (text == null || text == string.Empty)
				{
					text = e.ToString();
				}
				MessageBox.Show(this.Control, text, "Error", 0, 48);
			}
		}

		/// <summary>Enables or disables drag-and-drop support for the control being designed.</summary>
		/// <param name="value">true to enable drag-and-drop support for the control; false if the control should not have drag-and-drop support. The default is false. </param>
		// Token: 0x060000AC RID: 172 RVA: 0x00003388 File Offset: 0x00001588
		protected void EnableDragDrop(bool value)
		{
			if (this.Control != null)
			{
				if (value)
				{
					this.Control.DragDrop += new DragEventHandler(this.OnDragDrop);
					this.Control.DragOver += new DragEventHandler(this.OnDragOver);
					this.Control.DragEnter += new DragEventHandler(this.OnDragEnter);
					this.Control.DragLeave += this.OnDragLeave;
					this.Control.GiveFeedback += new GiveFeedbackEventHandler(this.OnGiveFeedback);
					this.Control.AllowDrop = true;
					return;
				}
				this.Control.DragDrop -= new DragEventHandler(this.OnDragDrop);
				this.Control.DragOver -= new DragEventHandler(this.OnDragOver);
				this.Control.DragEnter -= new DragEventHandler(this.OnDragEnter);
				this.Control.DragLeave -= this.OnDragLeave;
				this.Control.GiveFeedback -= new GiveFeedbackEventHandler(this.OnGiveFeedback);
				this.Control.AllowDrop = false;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000034A5 File Offset: 0x000016A5
		private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			this.OnGiveFeedback(e);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000034AE File Offset: 0x000016AE
		private void OnDragDrop(object sender, DragEventArgs e)
		{
			this.OnDragDrop(e);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000034B7 File Offset: 0x000016B7
		private void OnDragEnter(object sender, DragEventArgs e)
		{
			this.OnDragEnter(e);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000034C0 File Offset: 0x000016C0
		private void OnDragLeave(object sender, EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000034C9 File Offset: 0x000016C9
		private void OnDragOver(object sender, DragEventArgs e)
		{
			this.OnDragOver(e);
		}

		/// <summary>Receives a call when a drag-and-drop operation is in progress to provide visual cues based on the location of the mouse while a drag operation is in progress.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that provides data for the event. </param>
		// Token: 0x060000B2 RID: 178 RVA: 0x000034D2 File Offset: 0x000016D2
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
			e.UseDefaultCursors = false;
		}

		/// <summary>Receives a call when a drag-and-drop object is dropped onto the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x060000B3 RID: 179 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnDragDrop(DragEventArgs de)
		{
		}

		/// <summary>Receives a call when a drag-and-drop operation enters the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnDragEnter(DragEventArgs de)
		{
		}

		/// <summary>Receives a call when a drag-and-drop operation leaves the control designer view.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event. </param>
		// Token: 0x060000B5 RID: 181 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnDragLeave(EventArgs e)
		{
		}

		/// <summary>Receives a call when a drag-and-drop object is dragged over the control designer view.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event. </param>
		// Token: 0x060000B6 RID: 182 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void OnDragOver(DragEventArgs de)
		{
		}

		/// <summary>Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component. </param>
		// Token: 0x060000B7 RID: 183 RVA: 0x000034DC File Offset: 0x000016DC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[] { "Visible", "Enabled", "ContextMenu", "AllowDrop", "Location", "Name" };
			Attribute[][] array2 = new Attribute[][]
			{
				new Attribute[]
				{
					new DefaultValueAttribute(true)
				},
				new Attribute[]
				{
					new DefaultValueAttribute(true)
				},
				new Attribute[]
				{
					new DefaultValueAttribute(null)
				},
				new Attribute[]
				{
					new DefaultValueAttribute(false)
				},
				new Attribute[]
				{
					new DefaultValueAttribute(typeof(Point), "0, 0")
				},
				new Attribute[0]
			};
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = properties[array[i]] as PropertyDescriptor;
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), propertyDescriptor, array2[i]);
				}
			}
			properties["Locked"] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), "Locked", typeof(bool), new Attribute[]
			{
				DesignOnlyAttribute.Yes,
				BrowsableAttribute.Yes,
				CategoryAttribute.Design,
				new DefaultValueAttribute(false),
				new DescriptionAttribute("The Locked property determines if we can move or resize the control.")
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000363A File Offset: 0x0000183A
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003651 File Offset: 0x00001851
		private bool Visible
		{
			get
			{
				return (bool)base.ShadowProperties["Visible"];
			}
			set
			{
				base.ShadowProperties["Visible"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003669 File Offset: 0x00001869
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003680 File Offset: 0x00001880
		private bool Enabled
		{
			get
			{
				return (bool)base.ShadowProperties["Enabled"];
			}
			set
			{
				base.ShadowProperties["Enabled"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000036A0 File Offset: 0x000018A0
		private bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				this._locked = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000036A9 File Offset: 0x000018A9
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000036C0 File Offset: 0x000018C0
		private bool AllowDrop
		{
			get
			{
				return (bool)base.ShadowProperties["AllowDrop"];
			}
			set
			{
				base.ShadowProperties["AllowDrop"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000036D8 File Offset: 0x000018D8
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000036EA File Offset: 0x000018EA
		private string Name
		{
			get
			{
				return base.Component.Site.Name;
			}
			set
			{
				base.Component.Site.Name = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000036FD File Offset: 0x000018FD
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003714 File Offset: 0x00001914
		private ContextMenu ContextMenu
		{
			get
			{
				return (ContextMenu)base.ShadowProperties["ContextMenu"];
			}
			set
			{
				base.ShadowProperties["ContextMenu"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003727 File Offset: 0x00001927
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003734 File Offset: 0x00001934
		private Point Location
		{
			get
			{
				return this.Control.Location;
			}
			set
			{
				this.Control.Location = value;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003742 File Offset: 0x00001942
		internal object GetValue(object component, string propertyName)
		{
			return this.GetValue(component, propertyName, null);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003750 File Offset: 0x00001950
		internal object GetValue(object component, string propertyName, Type propertyType)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[propertyName];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property \"" + propertyName + "\" is missing on " + component.GetType().AssemblyQualifiedName);
			}
			if (propertyType != null && !propertyType.IsAssignableFrom(propertyDescriptor.PropertyType))
			{
				throw new InvalidOperationException("Types do not match: " + propertyDescriptor.PropertyType.AssemblyQualifiedName + " : " + propertyType.AssemblyQualifiedName);
			}
			return propertyDescriptor.GetValue(component);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000037D4 File Offset: 0x000019D4
		internal void SetValue(object component, string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[propertyName];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property \"" + propertyName + "\" is missing on " + component.GetType().AssemblyQualifiedName);
			}
			if (!propertyDescriptor.PropertyType.IsAssignableFrom(value.GetType()))
			{
				throw new InvalidOperationException("Types do not match: " + value.GetType().AssemblyQualifiedName + " : " + propertyDescriptor.PropertyType.AssemblyQualifiedName);
			}
			if (!propertyDescriptor.IsReadOnly)
			{
				propertyDescriptor.SetValue(component, value);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060000C9 RID: 201 RVA: 0x00003860 File Offset: 0x00001A60
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.Control != null)
			{
				this.UnhookChildControls(this.Control);
				this.OnMouseDragEnd(true);
				this._messageRouter.Dispose();
				this.Control.DragDrop -= new DragEventHandler(this.OnDragDrop);
				this.Control.DragEnter -= new DragEventHandler(this.OnDragEnter);
				this.Control.DragLeave -= this.OnDragLeave;
				this.Control.DragOver -= new DragEventHandler(this.OnDragOver);
			}
			base.Dispose(true);
		}

		/// <summary>Returns the internal control designer with the specified index in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> at the specified index.</returns>
		/// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x0000256A File Offset: 0x0000076A
		public virtual ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			return null;
		}

		/// <summary>Returns the number of internal control designers in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
		/// <returns>The number of internal control designers in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</returns>
		// Token: 0x060000CB RID: 203 RVA: 0x0000241E File Offset: 0x0000061E
		public virtual int NumberOfInternalControlDesigners()
		{
			return 0;
		}

		/// <summary>Enables design time functionality for a child control.</summary>
		/// <returns>true if the child control could be enabled for design time; false if the hosting infrastructure does not support it.</returns>
		/// <param name="child">The child control for which design mode will be enabled.</param>
		/// <param name="name">The name of <paramref name="child" /> as exposed to the end user.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="child" /> or <paramref name="name" /> is null.</exception>
		// Token: 0x060000CC RID: 204 RVA: 0x000038FC File Offset: 0x00001AFC
		protected bool EnableDesignMode(Control child, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			bool flag = false;
			INestedContainer nestedContainer = this.GetService(typeof(INestedContainer)) as INestedContainer;
			if (nestedContainer != null)
			{
				nestedContainer.Add(child, name);
				flag = true;
			}
			return flag;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> representing the bounds of this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> representing the bounds of this control.</returns>
		/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
		// Token: 0x060000CD RID: 205 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects representing the selection borders and grab handles for a standard control.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
		/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Re-initializes an existing component.</summary>
		/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be null if no default values are specified.</param>
		// Token: 0x060000CF RID: 207 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void InitializeExistingComponent(IDictionary defaultValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a newly created component.</summary>
		/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be null if no default values are specified.</param>
		// Token: 0x060000D0 RID: 208 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>Receives a call to clean up a drag-and-drop operation.</summary>
		/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
		// Token: 0x060000D1 RID: 209 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnDragComplete(DragEventArgs de)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the designer.</summary>
		/// <returns>
		///   <see cref="F:System.ComponentModel.InheritanceAttribute.Inherited" /> if the designer is a root designer; otherwise, the value of the parent designer's <see cref="P:System.ComponentModel.Design.ComponentDesigner.InheritanceAttribute" /> property.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</summary>
		/// <returns>A list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual IList SnapLines
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation when the primary drag control is over this designer; otherwise, false.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool ParticipatesWithSnapLines
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether resize handle allocation depends on the value of the <see cref="P:System.Windows.Forms.Control.AutoSize" /> property. </summary>
		/// <returns>true if resize handle allocation depends on the value of the <see cref="P:System.Windows.Forms.Control.AutoSize" /> and AutoSizeMode properties; otherwise, false. The default is false.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool AutoResizeHandles
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000021 RID: 33
		private WndProcRouter _messageRouter;

		// Token: 0x04000022 RID: 34
		private bool _locked;

		// Token: 0x04000023 RID: 35
		private bool _mouseDown;

		// Token: 0x04000024 RID: 36
		private bool _mouseMoveAfterMouseDown;

		// Token: 0x04000025 RID: 37
		private bool _mouseDownFirstMove;

		// Token: 0x04000026 RID: 38
		private bool _firstMouseMoveInClient = true;

		/// <summary>Defines a local <see cref="T:System.Drawing.Point" /> that represents the values of an invalid <see cref="T:System.Drawing.Point" />.</summary>
		// Token: 0x04000027 RID: 39
		protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		/// <summary>Specifies the accessibility object for the designer.</summary>
		// Token: 0x04000028 RID: 40
		protected AccessibleObject accessibilityObj;

		// Token: 0x04000029 RID: 41
		private MouseButtons _mouseButtonDown;

		/// <summary>Provides an <see cref="T:System.Windows.Forms.AccessibleObject" /> for <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
		// Token: 0x02000012 RID: 18
		[ComVisible(true)]
		public class ControlDesignerAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ControlDesigner.ControlDesignerAccessibleObject" /> class using the specified designer and control.</summary>
			/// <param name="designer">The <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> for the accessible object. </param>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> for the accessible object. </param>
			// Token: 0x060000D8 RID: 216 RVA: 0x00003961 File Offset: 0x00001B61
			[MonoTODO]
			public ControlDesignerAccessibleObject(ControlDesigner designer, Control control)
			{
				throw new NotImplementedException();
			}

			/// <summary>Retrieves the accessible child corresponding to the specified index.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the accessible child.</param>
			// Token: 0x060000D9 RID: 217 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleObject GetChild(int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
			/// <returns>The number of children belonging to an accessible object.</returns>
			// Token: 0x060000DA RID: 218 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override int GetChildCount()
			{
				throw new NotImplementedException();
			}

			/// <summary>Retrieves the object that has the keyboard focus.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns null if no object has focus.</returns>
			// Token: 0x060000DB RID: 219 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleObject GetFocused()
			{
				throw new NotImplementedException();
			}

			/// <summary>Retrieves the currently selected child.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns null if is no child is currently selected and the object itself does not have focus.</returns>
			// Token: 0x060000DC RID: 220 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleObject GetSelected()
			{
				throw new NotImplementedException();
			}

			/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.</returns>
			/// <param name="x">The horizontal screen coordinate.</param>
			/// <param name="y">The vertical screen coordinate.</param>
			// Token: 0x060000DD RID: 221 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleObject HitTest(int x, int y)
			{
				throw new NotImplementedException();
			}

			/// <summary>Gets the points that define the boundaries of the accessible object for the designer.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that indicates the boundaries of the accessible object for the designer.</returns>
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000DE RID: 222 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override Rectangle Bounds
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets a string that describes the default action of the specified object.</summary>
			/// <returns>A description of the default action for a specified object.</returns>
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000DF RID: 223 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override string DefaultAction
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets a string that describes the visual appearance of the specified object.</summary>
			/// <returns>A description of the object's visual appearance to the user, or null if the object does not have a description.</returns>
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override string Description
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets or sets the object name.</summary>
			/// <returns>The object name, or null if the property has not been set.</returns>
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override string Name
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the parent of an accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleObject Parent
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None" /> if no role has been specified.</returns>
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleRole Role
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the state of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" />, if no state has been set.</returns>
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override AccessibleStates State
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets or sets the value of an accessible object.</summary>
			/// <returns>The value of an accessible object, or null if the object has no value set.</returns>
			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override string Value
			{
				get
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
