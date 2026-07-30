using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Defines a base class for controls that support auto-scrolling behavior.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D0 RID: 720
	[Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[ComVisible(true)]
	public class ScrollableControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl" /> class.</summary>
		// Token: 0x06002FA3 RID: 12195 RVA: 0x000B7D80 File Offset: 0x000B5F80
		public ScrollableControl()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			this.auto_scroll = false;
			this.force_hscroll_visible = false;
			this.force_vscroll_visible = false;
			this.auto_scroll_margin = new Size(0, 0);
			this.auto_scroll_min_size = new Size(0, 0);
			this.scroll_position = new Point(0, 0);
			this.dock_padding = new ScrollableControl.DockPaddingEdges(this);
			base.SizeChanged += new EventHandler(this.Recalculate);
			base.VisibleChanged += new EventHandler(this.VisibleChangedHandler);
			base.LocationChanged += new EventHandler(this.LocationChangedHandler);
			base.ParentChanged += new EventHandler(this.ParentChangedHandler);
			base.HandleCreated += new EventHandler(this.AddScrollbars);
			this.CreateScrollbars();
			this.horizontalScroll = new HScrollProperties(this);
			this.verticalScroll = new VScrollProperties(this);
		}

		/// <summary>Occurs when the user or code scrolls through the client area.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002EB RID: 747
		// (add) Token: 0x06002FA5 RID: 12197 RVA: 0x000B7E74 File Offset: 0x000B6074
		// (remove) Token: 0x06002FA6 RID: 12198 RVA: 0x000B7E88 File Offset: 0x000B6088
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollableControl.OnScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollableControl.OnScrollEvent, value);
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000B7E9C File Offset: 0x000B609C
		private void VisibleChangedHandler(object sender, EventArgs e)
		{
			this.Recalculate(false);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B7EA8 File Offset: 0x000B60A8
		private void LocationChangedHandler(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000B7EB0 File Offset: 0x000B60B0
		private void ParentChangedHandler(object sender, EventArgs e)
		{
			if (this.old_parent == base.Parent)
			{
				return;
			}
			if (this.old_parent != null)
			{
				this.old_parent.SizeChanged -= new EventHandler(this.Parent_SizeChanged);
				this.old_parent.PaddingChanged -= new EventHandler(this.Parent_PaddingChanged);
			}
			if (base.Parent != null)
			{
				base.Parent.SizeChanged += new EventHandler(this.Parent_SizeChanged);
				base.Parent.PaddingChanged += new EventHandler(this.Parent_PaddingChanged);
			}
			this.old_parent = base.Parent;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000B7F50 File Offset: 0x000B6150
		private void Parent_PaddingChanged(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000B7F58 File Offset: 0x000B6158
		private void Parent_SizeChanged(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		/// <summary>Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.</summary>
		/// <returns>true if the container enables auto-scrolling; otherwise, false. The default value is false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000B7F60 File Offset: 0x000B6160
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x000B7F68 File Offset: 0x000B6168
		[MWFCategory("Layout")]
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool AutoScroll
		{
			get
			{
				return this.auto_scroll;
			}
			set
			{
				if (this.auto_scroll != value)
				{
					this.auto_scroll = value;
					base.PerformLayout(this, "AutoScroll");
				}
			}
		}

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width of the auto-scroll margin in pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> value assigned is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000B7F8C File Offset: 0x000B618C
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x000B7F94 File Offset: 0x000B6194
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Size AutoScrollMargin
		{
			get
			{
				return this.auto_scroll_margin;
			}
			set
			{
				if (value.Width < 0)
				{
					throw new ArgumentException("Width is assigned less than 0", "value.Width");
				}
				if (value.Height < 0)
				{
					throw new ArgumentException("Height is assigned less than 0", "value.Height");
				}
				this.auto_scroll_margin = value;
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B7FE4 File Offset: 0x000B61E4
		internal bool ShouldSerializeAutoScrollMargin()
		{
			return this.AutoScrollMargin != new Size(0, 0);
		}

		/// <summary>Gets or sets the minimum size of the auto-scroll.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that determines the minimum size of the virtual area through which the user can scroll.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000B7FF8 File Offset: 0x000B61F8
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000B8000 File Offset: 0x000B6200
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Size AutoScrollMinSize
		{
			get
			{
				return this.auto_scroll_min_size;
			}
			set
			{
				if (value != this.auto_scroll_min_size)
				{
					this.auto_scroll_min_size = value;
					this.AutoScroll = true;
					base.PerformLayout(this, "AutoScrollMinSize");
				}
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B8030 File Offset: 0x000B6230
		internal bool ShouldSerializeAutoScrollMinSize()
		{
			return this.AutoScrollMinSize != new Size(0, 0);
		}

		/// <summary>Gets or sets the location of the auto-scroll position.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the auto-scroll position in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000B8044 File Offset: 0x000B6244
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x000B8060 File Offset: 0x000B6260
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Point AutoScrollPosition
		{
			get
			{
				return this.DisplayRectangle.Location;
			}
			set
			{
				if (value != this.AutoScrollPosition)
				{
					int num = 0;
					int num2 = 0;
					if (this.hscrollbar.VisibleInternal)
					{
						int num3 = this.hscrollbar.Maximum - this.hscrollbar.LargeChange + 1;
						value.X = ((value.X >= this.hscrollbar.Minimum) ? value.X : this.hscrollbar.Minimum);
						value.X = ((value.X <= num3) ? value.X : num3);
						num = value.X - this.scroll_position.X;
					}
					if (this.vscrollbar.VisibleInternal)
					{
						int num4 = this.vscrollbar.Maximum - this.vscrollbar.LargeChange + 1;
						value.Y = ((value.Y >= this.vscrollbar.Minimum) ? value.Y : this.vscrollbar.Minimum);
						value.Y = ((value.Y <= num4) ? value.Y : num4);
						num2 = value.Y - this.scroll_position.Y;
					}
					this.ScrollWindow(num, num2);
					if (this.hscrollbar.VisibleInternal && this.scroll_position.X >= this.hscrollbar.Minimum && this.scroll_position.X <= this.hscrollbar.Maximum)
					{
						this.hscrollbar.Value = this.scroll_position.X;
					}
					if (this.vscrollbar.VisibleInternal && this.scroll_position.Y >= this.vscrollbar.Minimum && this.scroll_position.Y <= this.vscrollbar.Maximum)
					{
						this.vscrollbar.Value = this.scroll_position.Y;
					}
				}
			}
		}

		/// <summary>Gets the rectangle that represents the virtual display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000B826C File Offset: 0x000B646C
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (this.auto_scroll)
				{
					int num;
					if (this.canvas_size.Width <= base.DisplayRectangle.Width)
					{
						num = base.DisplayRectangle.Width;
						if (this.vscrollbar.VisibleInternal)
						{
							num -= this.vscrollbar.Width;
						}
					}
					else
					{
						num = this.canvas_size.Width;
					}
					int num2;
					if (this.canvas_size.Height <= base.DisplayRectangle.Height)
					{
						num2 = base.DisplayRectangle.Height;
						if (this.hscrollbar.VisibleInternal)
						{
							num2 -= this.hscrollbar.Height;
						}
					}
					else
					{
						num2 = this.canvas_size.Height;
					}
					this.display_rectangle.X = -this.scroll_position.X;
					this.display_rectangle.Y = -this.scroll_position.Y;
					this.display_rectangle.Width = Math.Max(this.auto_scroll_min_size.Width, num);
					this.display_rectangle.Height = Math.Max(this.auto_scroll_min_size.Height, num2);
				}
				else
				{
					this.display_rectangle = base.DisplayRectangle;
				}
				this.display_rectangle.X = this.display_rectangle.X + this.dock_padding.Left;
				this.display_rectangle.Y = this.display_rectangle.Y + this.dock_padding.Top;
				this.display_rectangle.Width = this.display_rectangle.Width - (this.dock_padding.Left + this.dock_padding.Right);
				this.display_rectangle.Height = this.display_rectangle.Height - (this.dock_padding.Top + this.dock_padding.Bottom);
				return this.display_rectangle;
			}
		}

		/// <summary>Gets the dock padding settings for all edges of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000B8448 File Offset: 0x000B6648
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[MWFCategory("Layout")]
		[Browsable(false)]
		public ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return this.dock_padding;
			}
		}

		/// <summary>Gets the characteristics associated with the horizontal scroll bar.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.HScrollProperties" /> that contains information about the horizontal scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000B8450 File Offset: 0x000B6650
		[EditorBrowsable(0)]
		[Browsable(false)]
		public HScrollProperties HorizontalScroll
		{
			get
			{
				return this.horizontalScroll;
			}
		}

		/// <summary>Gets the characteristics associated with the vertical scroll bar.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VScrollProperties" /> that contains information about the vertical scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000B8458 File Offset: 0x000B6658
		[Browsable(false)]
		[EditorBrowsable(0)]
		public VScrollProperties VerticalScroll
		{
			get
			{
				return this.verticalScroll;
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000B8460 File Offset: 0x000B6660
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets a value indicating whether the horizontal scroll bar is visible.</summary>
		/// <returns>true if the horizontal scroll bar is visible; otherwise, false.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000B8468 File Offset: 0x000B6668
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x000B8478 File Offset: 0x000B6678
		protected bool HScroll
		{
			get
			{
				return this.hscrollbar.VisibleInternal;
			}
			set
			{
				if (!this.AutoScroll && this.hscrollbar.VisibleInternal != value)
				{
					this.force_hscroll_visible = value;
					this.Recalculate(false);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the vertical scroll bar is visible.</summary>
		/// <returns>true if the vertical scroll bar is visible; otherwise, false.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000B84B0 File Offset: 0x000B66B0
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x000B84C0 File Offset: 0x000B66C0
		protected bool VScroll
		{
			get
			{
				return this.vscrollbar.VisibleInternal;
			}
			set
			{
				if (!this.AutoScroll && this.vscrollbar.VisibleInternal != value)
				{
					this.force_vscroll_visible = value;
					this.Recalculate(false);
				}
			}
		}

		/// <summary>Scrolls the specified child control into view on an auto-scroll enabled control.</summary>
		/// <param name="activeControl">The child control to scroll into view. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002FBF RID: 12223 RVA: 0x000B84F8 File Offset: 0x000B66F8
		public void ScrollControlIntoView(Control activeControl)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.Size = base.ClientSize;
			if (!this.AutoScroll || (!this.hscrollbar.VisibleInternal && !this.vscrollbar.VisibleInternal))
			{
				return;
			}
			if (!base.Contains(activeControl))
			{
				return;
			}
			if (this.vscrollbar.Visible)
			{
				rectangle.Width -= this.vscrollbar.Width;
			}
			if (this.hscrollbar.Visible)
			{
				rectangle.Height -= this.hscrollbar.Height;
			}
			if (rectangle.Contains(activeControl.Location) && rectangle.Contains(activeControl.Right, activeControl.Bottom))
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			if (activeControl.Top <= 0 || activeControl.Height >= rectangle.Height)
			{
				num2 = -activeControl.Top;
			}
			else if (activeControl.Bottom > rectangle.Height)
			{
				num2 = rectangle.Height - activeControl.Bottom;
			}
			if (activeControl.Left <= 0 || activeControl.Width >= rectangle.Width)
			{
				num = -activeControl.Left;
			}
			else if (activeControl.Right > rectangle.Width)
			{
				num = rectangle.Width - activeControl.Right;
			}
			int num3 = this.hscrollbar.Value - num;
			int num4 = this.vscrollbar.Value - num2;
			if (this.hscrollbar.VisibleInternal)
			{
				if (num3 > this.hscrollbar.Maximum)
				{
					num3 = this.hscrollbar.Maximum;
				}
				else if (num3 < this.hscrollbar.Minimum)
				{
					num3 = this.hscrollbar.Minimum;
				}
				if (num3 != this.hscrollbar.Value)
				{
					this.hscrollbar.Value = num3;
				}
			}
			if (this.vscrollbar.VisibleInternal)
			{
				if (num4 > this.vscrollbar.Maximum)
				{
					num4 = this.vscrollbar.Maximum;
				}
				else if (num4 < this.vscrollbar.Minimum)
				{
					num4 = this.vscrollbar.Minimum;
				}
				if (num4 != this.vscrollbar.Value)
				{
					this.vscrollbar.Value = num4;
				}
			}
		}

		/// <summary>Sets the size of the auto-scroll margins.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Size.Width" /> value. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Size.Height" /> value. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002FC0 RID: 12224 RVA: 0x000B8764 File Offset: 0x000B6964
		public void SetAutoScrollMargin(int x, int y)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (y < 0)
			{
				y = 0;
			}
			this.auto_scroll_margin = new Size(x, y);
			this.Recalculate(false);
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected. </summary>
		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		// Token: 0x06002FC1 RID: 12225 RVA: 0x000B879C File Offset: 0x000B699C
		[EditorBrowsable(2)]
		protected virtual void AdjustFormScrollbars(bool displayScrollbars)
		{
			this.Recalculate(false);
		}

		/// <summary>Determines whether the specified flag has been set.</summary>
		/// <returns>true if the specified flag has been set; otherwise, false.</returns>
		/// <param name="bit">The flag to check.</param>
		// Token: 0x06002FC2 RID: 12226 RVA: 0x000B87A8 File Offset: 0x000B69A8
		[EditorBrowsable(2)]
		protected bool GetScrollState(int bit)
		{
			return false;
		}

		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000B87AC File Offset: 0x000B69AC
		[EditorBrowsable(2)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.CalculateCanvasSize(true);
			this.AdjustFormScrollbars(this.AutoScroll);
			base.OnLayout(levent);
			if (this is FlowLayoutPanel)
			{
				this.CalculateCanvasSize(false);
				this.AdjustFormScrollbars(this.AutoScroll);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000B87F4 File Offset: 0x000B69F4
		[EditorBrowsable(2)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.vscrollbar.VisibleInternal)
			{
				if (e.Delta > 0)
				{
					if (this.vscrollbar.Minimum < this.vscrollbar.Value - this.vscrollbar.LargeChange)
					{
						this.vscrollbar.Value -= this.vscrollbar.LargeChange;
					}
					else
					{
						this.vscrollbar.Value = this.vscrollbar.Minimum;
					}
				}
				else
				{
					int num = this.vscrollbar.Maximum - this.vscrollbar.LargeChange + 1;
					if (num > this.vscrollbar.Value + this.vscrollbar.LargeChange)
					{
						this.vscrollbar.Value += this.vscrollbar.LargeChange;
					}
					else
					{
						this.vscrollbar.Value = num;
					}
				}
			}
			base.OnMouseWheel(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000B88F0 File Offset: 0x000B6AF0
		[EditorBrowsable(2)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				base.UpdateChildrenZOrder();
				base.PerformLayout(this, "Visible");
			}
			base.OnVisibleChanged(e);
		}

		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000B8924 File Offset: 0x000B6B24
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.dock_padding.Scale(dx, dy);
			base.ScaleCore(dx, dy);
		}

		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000B893C File Offset: 0x000B6B3C
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>Calculates the scroll offset to the specified child control. </summary>
		/// <returns>The upper-left hand <see cref="T:System.Drawing.Point" /> of the display area relative to the client area required to scroll the control into view.</returns>
		/// <param name="activeControl">The child control to scroll into view. </param>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B8948 File Offset: 0x000B6B48
		protected virtual Point ScrollToControl(Control activeControl)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.Size = base.ClientSize;
			if (this.vscrollbar.Visible)
			{
				rectangle.Width -= this.vscrollbar.Width;
			}
			if (this.hscrollbar.Visible)
			{
				rectangle.Height -= this.hscrollbar.Height;
			}
			int num = 0;
			int num2 = 0;
			if (activeControl.Top <= 0 || activeControl.Height >= rectangle.Height)
			{
				num2 = -activeControl.Top;
			}
			else if (activeControl.Bottom > rectangle.Height)
			{
				num2 = rectangle.Height - activeControl.Bottom;
			}
			if (activeControl.Left <= 0 || activeControl.Width >= rectangle.Width)
			{
				num = -activeControl.Left;
			}
			else if (activeControl.Right > rectangle.Width)
			{
				num = rectangle.Width - activeControl.Right;
			}
			int num3 = this.AutoScrollPosition.X + num;
			int num4 = this.AutoScrollPosition.Y + num2;
			return new Point(num3, num4);
		}

		/// <summary>Positions the display window to the specified value.</summary>
		/// <param name="x">The horizontal offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
		/// <param name="y">The vertical offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
		// Token: 0x06002FC9 RID: 12233 RVA: 0x000B8A88 File Offset: 0x000B6C88
		protected void SetDisplayRectLocation(int x, int y)
		{
			if (x > 0)
			{
				x = 0;
			}
			if (y > 0)
			{
				y = 0;
			}
			this.ScrollWindow(this.scroll_position.X - x, this.scroll_position.Y - y);
		}

		/// <summary>Sets the specified scroll state flag.</summary>
		/// <param name="bit">The scroll state flag to set. </param>
		/// <param name="value">The value to set the flag. </param>
		// Token: 0x06002FCA RID: 12234 RVA: 0x000B8ACC File Offset: 0x000B6CCC
		protected void SetScrollState(int bit, bool value)
		{
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06002FCB RID: 12235 RVA: 0x000B8AD0 File Offset: 0x000B6CD0
		[EditorBrowsable(2)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000B8ADC File Offset: 0x000B6CDC
		internal override IntPtr AfterTopMostControl()
		{
			if (this.hscrollbar != null && this.hscrollbar.Visible)
			{
				return this.hscrollbar.Handle;
			}
			if (this.vscrollbar != null && this.vscrollbar.Visible)
			{
				return this.hscrollbar.Handle;
			}
			return base.AfterTopMostControl();
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000B8B40 File Offset: 0x000B6D40
		internal virtual void CalculateCanvasSize(bool canOverride)
		{
			int count = base.Controls.Count;
			int num = 0;
			int num2 = 0;
			int num3 = this.dock_padding.Right + this.hscrollbar.Value;
			int num4 = this.dock_padding.Bottom + this.vscrollbar.Value;
			for (int i = 0; i < count; i++)
			{
				Control control = base.Controls[i];
				if (control.Dock == DockStyle.Right)
				{
					num3 += control.Width;
				}
				else if (control.Dock == DockStyle.Bottom)
				{
					num4 += control.Height;
				}
			}
			if (!this.auto_scroll_min_size.IsEmpty)
			{
				num = this.auto_scroll_min_size.Width;
				num2 = this.auto_scroll_min_size.Height;
			}
			for (int j = 0; j < count; j++)
			{
				Control control = base.Controls[j];
				switch (control.Dock)
				{
				case DockStyle.Top:
					if (control.Bottom + num4 > num2)
					{
						num2 = control.Bottom + num4;
					}
					break;
				case DockStyle.Bottom:
				case DockStyle.Right:
				case DockStyle.Fill:
					break;
				case DockStyle.Left:
					if (control.Right + num3 > num)
					{
						num = control.Right + num3;
					}
					break;
				default:
				{
					AnchorStyles anchor = control.Anchor;
					if ((anchor & AnchorStyles.Left) != AnchorStyles.None && (anchor & AnchorStyles.Right) == AnchorStyles.None && control.Right + num3 > num)
					{
						num = control.Right + num3;
					}
					if (((anchor & AnchorStyles.Top) != AnchorStyles.None || (anchor & AnchorStyles.Bottom) == AnchorStyles.None) && control.Bottom + num4 > num2)
					{
						num2 = control.Bottom + num4;
					}
					break;
				}
				}
			}
			this.canvas_size.Width = num;
			this.canvas_size.Height = num2;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000B8D18 File Offset: 0x000B6F18
		private void Recalculate(object sender, EventArgs e)
		{
			this.Recalculate(true);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000B8D24 File Offset: 0x000B6F24
		private void Recalculate(bool doLayout)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			Size size = this.canvas_size;
			Size clientSize = base.ClientSize;
			size.Width += this.auto_scroll_margin.Width;
			size.Height += this.auto_scroll_margin.Height;
			int num = clientSize.Width;
			int num2 = clientSize.Height;
			int num3;
			int num4;
			bool flag;
			bool flag2;
			do
			{
				num3 = num;
				num4 = num2;
				if ((this.force_hscroll_visible || (size.Width > num && this.auto_scroll)) && clientSize.Width > 0)
				{
					flag = true;
					num2 = clientSize.Height - SystemInformation.HorizontalScrollBarHeight;
				}
				else
				{
					flag = false;
					num2 = clientSize.Height;
				}
				if ((this.force_vscroll_visible || (size.Height > num2 && this.auto_scroll)) && clientSize.Height > 0)
				{
					flag2 = true;
					num = clientSize.Width - SystemInformation.VerticalScrollBarWidth;
				}
				else
				{
					flag2 = false;
					num = clientSize.Width;
				}
			}
			while (num != num3 || num2 != num4);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			Rectangle rectangle;
			rectangle..ctor(0, clientSize.Height - SystemInformation.HorizontalScrollBarHeight, base.ClientRectangle.Width, SystemInformation.HorizontalScrollBarHeight);
			Rectangle rectangle2;
			rectangle2..ctor(clientSize.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, base.ClientRectangle.Height);
			if (!this.vscrollbar.Visible)
			{
				this.vscrollbar.Value = 0;
			}
			if (!this.hscrollbar.Visible)
			{
				this.hscrollbar.Value = 0;
			}
			if (flag)
			{
				this.hscrollbar.manual_thumb_size = num;
				this.hscrollbar.LargeChange = num;
				this.hscrollbar.SmallChange = 5;
				this.hscrollbar.Maximum = size.Width - 1;
			}
			else
			{
				if (this.hscrollbar != null && this.hscrollbar.VisibleInternal)
				{
					this.ScrollWindow(-this.scroll_position.X, 0);
				}
				this.scroll_position.X = 0;
			}
			if (flag2)
			{
				this.vscrollbar.manual_thumb_size = num2;
				this.vscrollbar.LargeChange = num2;
				this.vscrollbar.SmallChange = 5;
				this.vscrollbar.Maximum = size.Height - 1;
			}
			else
			{
				if (this.vscrollbar != null && this.vscrollbar.VisibleInternal)
				{
					this.ScrollWindow(0, -this.scroll_position.Y);
				}
				this.scroll_position.Y = 0;
			}
			if (flag && flag2)
			{
				rectangle.Width -= SystemInformation.VerticalScrollBarWidth;
				rectangle2.Height -= SystemInformation.HorizontalScrollBarHeight;
				this.sizegrip.Bounds = new Rectangle(rectangle.Right, rectangle2.Bottom, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
			}
			base.SuspendLayout();
			this.hscrollbar.SetBoundsInternal(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.None);
			this.hscrollbar.Visible = flag;
			if (this.hscrollbar.Visible)
			{
				XplatUI.SetZOrder(this.hscrollbar.Handle, IntPtr.Zero, true, false);
			}
			this.vscrollbar.SetBoundsInternal(rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, BoundsSpecified.None);
			this.vscrollbar.Visible = flag2;
			if (this.vscrollbar.Visible)
			{
				XplatUI.SetZOrder(this.vscrollbar.Handle, IntPtr.Zero, true, false);
			}
			this.UpdateSizeGripVisible();
			base.ResumeLayout(doLayout);
			ContainerControl containerControl = this as ContainerControl;
			if (containerControl != null && containerControl.ActiveControl != null)
			{
				this.ScrollControlIntoView(containerControl.ActiveControl);
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000B9130 File Offset: 0x000B7330
		internal void UpdateSizeGripVisible()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			this.sizegrip.CapturedControl = base.Parent;
			bool flag = this.hscrollbar.VisibleInternal && this.vscrollbar.VisibleInternal;
			bool flag2 = false;
			if (flag && base.Parent != null)
			{
				Point point;
				point..ctor(base.Parent.ClientRectangle.Bottom - base.Bottom, base.Parent.ClientRectangle.Right - base.Right);
				flag2 = point.X <= 2 && point.X >= 0 && point.Y <= 2 && point.Y >= 0;
			}
			this.sizegrip.Visible = flag;
			this.sizegrip.Enabled = flag2 || this.sizegrip.Capture;
			if (this.sizegrip.Visible)
			{
				XplatUI.SetZOrder(this.sizegrip.Handle, this.vscrollbar.Handle, false, false);
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000B925C File Offset: 0x000B745C
		private void HandleScrollBar(object sender, EventArgs e)
		{
			if (sender == this.vscrollbar)
			{
				if (!this.vscrollbar.Visible)
				{
					return;
				}
				this.ScrollWindow(0, this.vscrollbar.Value - this.scroll_position.Y);
			}
			else
			{
				if (!this.hscrollbar.Visible)
				{
					return;
				}
				this.ScrollWindow(this.hscrollbar.Value - this.scroll_position.X, 0);
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000B92D8 File Offset: 0x000B74D8
		private void HandleScrollEvent(object sender, ScrollEventArgs args)
		{
			this.OnScroll(args);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B92E4 File Offset: 0x000B74E4
		private void AddScrollbars(object o, EventArgs e)
		{
			base.Controls.AddRangeImplicit(new Control[] { this.hscrollbar, this.vscrollbar, this.sizegrip });
			base.HandleCreated -= new EventHandler(this.AddScrollbars);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000B9330 File Offset: 0x000B7530
		private void CreateScrollbars()
		{
			this.hscrollbar = new ImplicitHScrollBar();
			this.hscrollbar.Visible = false;
			this.hscrollbar.ValueChanged += new EventHandler(this.HandleScrollBar);
			this.hscrollbar.Height = SystemInformation.HorizontalScrollBarHeight;
			this.hscrollbar.use_manual_thumb_size = true;
			this.hscrollbar.Scroll += this.HandleScrollEvent;
			this.vscrollbar = new ImplicitVScrollBar();
			this.vscrollbar.Visible = false;
			this.vscrollbar.ValueChanged += new EventHandler(this.HandleScrollBar);
			this.vscrollbar.Width = SystemInformation.VerticalScrollBarWidth;
			this.vscrollbar.use_manual_thumb_size = true;
			this.vscrollbar.Scroll += this.HandleScrollEvent;
			this.sizegrip = new SizeGrip(this);
			this.sizegrip.Visible = false;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B9418 File Offset: 0x000B7618
		private void ScrollWindow(int XOffset, int YOffset)
		{
			if (XOffset == 0 && YOffset == 0)
			{
				return;
			}
			base.SuspendLayout();
			int count = base.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				base.Controls[i].Location = new Point(base.Controls[i].Left - XOffset, base.Controls[i].Top - YOffset);
			}
			this.scroll_position.X = this.scroll_position.X + XOffset;
			this.scroll_position.Y = this.scroll_position.Y + YOffset;
			XplatUI.ScrollWindow(this.Handle, base.ClientRectangle, -XOffset, -YOffset, false);
			base.ResumeLayout(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B94D8 File Offset: 0x000B76D8
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollableControl.OnScrollEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B950C File Offset: 0x000B770C
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000B9518 File Offset: 0x000B7718
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000B9524 File Offset: 0x000B7724
		[EditorBrowsable(2)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Determines the value of the <see cref="P:System.Windows.Forms.ScrollableControl.AutoScroll" /> property.</summary>
		// Token: 0x040016CE RID: 5838
		protected const int ScrollStateAutoScrolling = 1;

		/// <summary>Determines whether the user has enabled full window drag.</summary>
		// Token: 0x040016CF RID: 5839
		protected const int ScrollStateFullDrag = 16;

		/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.HScroll" /> property is set to true.</summary>
		// Token: 0x040016D0 RID: 5840
		protected const int ScrollStateHScrollVisible = 2;

		/// <summary>Determines whether the user had scrolled through the <see cref="T:System.Windows.Forms.ScrollableControl" /> control.</summary>
		// Token: 0x040016D1 RID: 5841
		protected const int ScrollStateUserHasScrolled = 8;

		/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.VScroll" /> property is set to true.</summary>
		// Token: 0x040016D2 RID: 5842
		protected const int ScrollStateVScrollVisible = 4;

		// Token: 0x040016D3 RID: 5843
		private bool force_hscroll_visible;

		// Token: 0x040016D4 RID: 5844
		private bool force_vscroll_visible;

		// Token: 0x040016D5 RID: 5845
		private bool auto_scroll;

		// Token: 0x040016D6 RID: 5846
		private Size auto_scroll_margin;

		// Token: 0x040016D7 RID: 5847
		private Size auto_scroll_min_size;

		// Token: 0x040016D8 RID: 5848
		private Point scroll_position;

		// Token: 0x040016D9 RID: 5849
		private ScrollableControl.DockPaddingEdges dock_padding;

		// Token: 0x040016DA RID: 5850
		private SizeGrip sizegrip;

		// Token: 0x040016DB RID: 5851
		internal ImplicitHScrollBar hscrollbar;

		// Token: 0x040016DC RID: 5852
		internal ImplicitVScrollBar vscrollbar;

		// Token: 0x040016DD RID: 5853
		internal Size canvas_size;

		// Token: 0x040016DE RID: 5854
		private Rectangle display_rectangle;

		// Token: 0x040016DF RID: 5855
		private Control old_parent;

		// Token: 0x040016E0 RID: 5856
		private HScrollProperties horizontalScroll;

		// Token: 0x040016E1 RID: 5857
		private VScrollProperties verticalScroll;

		// Token: 0x040016E2 RID: 5858
		private static object OnScrollEvent = new object();

		/// <summary>Determines the border padding for docked controls.</summary>
		// Token: 0x020002D1 RID: 721
		[TypeConverter(typeof(ScrollableControl.DockPaddingEdgesConverter))]
		public class DockPaddingEdges : ICloneable
		{
			// Token: 0x06002FDA RID: 12250 RVA: 0x000B9530 File Offset: 0x000B7730
			internal DockPaddingEdges(Control owner)
			{
				this.owner = owner;
			}

			/// <summary>Creates a new object that is a copy of the current instance.</summary>
			/// <returns>A new object that is a copy of the current instance.</returns>
			// Token: 0x06002FDB RID: 12251 RVA: 0x000B9540 File Offset: 0x000B7740
			object ICloneable.Clone()
			{
				return new ScrollableControl.DockPaddingEdges(this.owner);
			}

			/// <summary>Gets or sets the padding width for all edges of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000B9550 File Offset: 0x000B7750
			// (set) Token: 0x06002FDD RID: 12253 RVA: 0x000B9570 File Offset: 0x000B7770
			[RefreshProperties(1)]
			public int All
			{
				get
				{
					return this.owner.Padding.All;
				}
				set
				{
					this.owner.Padding = new Padding(value);
				}
			}

			/// <summary>Gets or sets the padding width for the bottom edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000B9584 File Offset: 0x000B7784
			// (set) Token: 0x06002FDF RID: 12255 RVA: 0x000B95A4 File Offset: 0x000B77A4
			[RefreshProperties(1)]
			public int Bottom
			{
				get
				{
					return this.owner.Padding.Bottom;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, this.Top, this.Right, value);
				}
			}

			/// <summary>Gets or sets the padding width for the left edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000B95D4 File Offset: 0x000B77D4
			// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x000B95F4 File Offset: 0x000B77F4
			[RefreshProperties(1)]
			public int Left
			{
				get
				{
					return this.owner.Padding.Left;
				}
				set
				{
					this.owner.Padding = new Padding(value, this.Top, this.Right, this.Bottom);
				}
			}

			/// <summary>Gets or sets the padding width for the right edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17000C24 RID: 3108
			// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000B9624 File Offset: 0x000B7824
			// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x000B9644 File Offset: 0x000B7844
			[RefreshProperties(1)]
			public int Right
			{
				get
				{
					return this.owner.Padding.Right;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, this.Top, value, this.Bottom);
				}
			}

			/// <summary>Gets or sets the padding width for the top edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17000C25 RID: 3109
			// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000B9674 File Offset: 0x000B7874
			// (set) Token: 0x06002FE5 RID: 12261 RVA: 0x000B9694 File Offset: 0x000B7894
			[RefreshProperties(1)]
			public int Top
			{
				get
				{
					return this.owner.Padding.Top;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, value, this.Right, this.Bottom);
				}
			}

			/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
			/// <param name="other"></param>
			// Token: 0x06002FE6 RID: 12262 RVA: 0x000B96C4 File Offset: 0x000B78C4
			public override bool Equals(object other)
			{
				return other is ScrollableControl.DockPaddingEdges && (this.All == ((ScrollableControl.DockPaddingEdges)other).All && this.Left == ((ScrollableControl.DockPaddingEdges)other).Left && this.Right == ((ScrollableControl.DockPaddingEdges)other).Right && this.Top == ((ScrollableControl.DockPaddingEdges)other).Top && this.Bottom == ((ScrollableControl.DockPaddingEdges)other).Bottom);
			}

			/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
			// Token: 0x06002FE7 RID: 12263 RVA: 0x000B9750 File Offset: 0x000B7950
			public override int GetHashCode()
			{
				return this.All * this.Top * this.Bottom * this.Right * this.Left;
			}

			/// <summary>Returns an empty string.</summary>
			/// <returns>An empty string.</returns>
			// Token: 0x06002FE8 RID: 12264 RVA: 0x000B9780 File Offset: 0x000B7980
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"All = ",
					this.All.ToString(),
					" Top = ",
					this.Top.ToString(),
					" Left = ",
					this.Left.ToString(),
					" Bottom = ",
					this.Bottom.ToString(),
					" Right = ",
					this.Right.ToString()
				});
			}

			// Token: 0x06002FE9 RID: 12265 RVA: 0x000B9818 File Offset: 0x000B7A18
			internal void Scale(float dx, float dy)
			{
				this.Left = (int)((float)this.Left * dx);
				this.Right = (int)((float)this.Right * dx);
				this.Top = (int)((float)this.Top * dy);
				this.Bottom = (int)((float)this.Bottom * dy);
			}

			// Token: 0x040016E3 RID: 5859
			private Control owner;
		}

		/// <summary>A <see cref="T:System.ComponentModel.TypeConverter" /> for the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> class.</summary>
		// Token: 0x020002D2 RID: 722
		public class DockPaddingEdgesConverter : TypeConverter
		{
			/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
			/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="value">An object that specifies the type of array for which to get properties.</param>
			/// <param name="attributes">An array of type attribute that is used as a filter.</param>
			// Token: 0x06002FEB RID: 12267 RVA: 0x000B9870 File Offset: 0x000B7A70
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				return TypeDescriptor.GetProperties(typeof(ScrollableControl.DockPaddingEdges), attributes);
			}

			/// <summary>Returns whether the current object supports properties, using the specified context.</summary>
			/// <returns>true in all cases.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			// Token: 0x06002FEC RID: 12268 RVA: 0x000B9884 File Offset: 0x000B7A84
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
