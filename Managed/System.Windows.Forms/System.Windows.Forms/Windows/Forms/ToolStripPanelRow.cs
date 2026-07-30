using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a row of a <see cref="T:System.Windows.Forms.ToolStripPanel" /> that can contain controls.</summary>
	// Token: 0x02000372 RID: 882
	[ToolboxItem(false)]
	public class ToolStripPanelRow : Component, IDisposable, IComponent, IBounds
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> class, specifying the containing <see cref="T:System.Windows.Forms.ToolStripPanel" />. </summary>
		/// <param name="parent">The containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		// Token: 0x06003F34 RID: 16180 RVA: 0x000FB82C File Offset: 0x000F9A2C
		public ToolStripPanelRow(ToolStripPanel parent)
		{
			this.bounds = Rectangle.Empty;
			this.controls = new List<Control>();
			this.layout_engine = new DefaultLayout();
			this.parent = parent;
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />, including its nonclient elements, in pixels, relative to the parent control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x000FB868 File Offset: 0x000F9A68
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the controls in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <returns>An array of controls.</returns>
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000FB870 File Offset: 0x000F9A70
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Control[] Controls
		{
			get
			{
				return this.controls.ToArray();
			}
		}

		/// <summary>Gets the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x000FB880 File Offset: 0x000F9A80
		public Rectangle DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		/// <summary>Gets an instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x000FB888 File Offset: 0x000F9A88
		public LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new DefaultLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000FB8A8 File Offset: 0x000F9AA8
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x000FB8B0 File Offset: 0x000F9AB0
		public Padding Margin
		{
			get
			{
				return this.margin;
			}
			set
			{
				this.margin = value;
			}
		}

		/// <summary>Gets the layout direction of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> relative to its containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000FB8BC File Offset: 0x000F9ABC
		public Orientation Orientation
		{
			get
			{
				return this.parent.Orientation;
			}
		}

		/// <summary>Gets or sets padding within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x000FB8CC File Offset: 0x000F9ACC
		// (set) Token: 0x06003F3D RID: 16189 RVA: 0x000FB8D4 File Offset: 0x000F9AD4
		public virtual Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x000FB8E0 File Offset: 0x000F9AE0
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x000FB8E8 File Offset: 0x000F9AE8
		protected virtual Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000FB8F0 File Offset: 0x000F9AF0
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> can be dragged and dropped into a <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <returns>true if there is enough space in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to receive the <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, false. </returns>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be dragged and dropped into the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000FB8F8 File Offset: 0x000F9AF8
		public bool CanMove(ToolStrip toolStripToDrag)
		{
			if (this.controls.Count > 0 && (toolStripToDrag.Stretch || (this.controls[0] as ToolStrip).Stretch))
			{
				return false;
			}
			int num = 0;
			foreach (Control control in this.controls)
			{
				ToolStrip toolStrip = (ToolStrip)control;
				num += toolStrip.Width + toolStrip.Margin.Horizontal;
			}
			return num + toolStripToDrag.Width + toolStripToDrag.Margin.Horizontal <= this.bounds.Width;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000FB9DC File Offset: 0x000F9BDC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property changes.</summary>
		/// <param name="oldBounds">The original value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
		/// <param name="newBounds">The new value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
		// Token: 0x06003F43 RID: 16195 RVA: 0x000FB9E8 File Offset: 0x000F9BE8
		protected void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
		/// <param name="control">The control that was added to the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <param name="index">The zero-based index representing the position of the added control.</param>
		// Token: 0x06003F44 RID: 16196 RVA: 0x000FB9EC File Offset: 0x000F9BEC
		protected internal virtual void OnControlAdded(Control control, int index)
		{
			control.SizeChanged += new EventHandler(this.control_SizeChanged);
			this.controls.Add(control);
			this.OnLayout(new LayoutEventArgs(control, string.Empty));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
		/// <param name="control">The control that was removed from the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <param name="index">The zero-based index representing the position of the removed control.</param>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000FBA20 File Offset: 0x000F9C20
		protected internal virtual void OnControlRemoved(Control control, int index)
		{
			control.SizeChanged -= new EventHandler(this.control_SizeChanged);
			this.controls.Remove(control);
			this.OnLayout(new LayoutEventArgs(control, string.Empty));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F46 RID: 16198 RVA: 0x000FBA60 File Offset: 0x000F9C60
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			int num = 0;
			if (this.Orientation == Orientation.Horizontal)
			{
				foreach (Control control in this.controls)
				{
					ToolStrip toolStrip = (ToolStrip)control;
					if (toolStrip.Height > num)
					{
						num = toolStrip.Height;
					}
				}
				if (num != this.bounds.Height)
				{
					this.bounds.Height = num;
				}
			}
			else
			{
				foreach (Control control2 in this.controls)
				{
					ToolStrip toolStrip2 = (ToolStrip)control2;
					if (toolStrip2.GetPreferredSize(Size.Empty).Width > num)
					{
						num = toolStrip2.GetPreferredSize(Size.Empty).Width;
					}
				}
				if (num != this.bounds.Width)
				{
					this.bounds.Width = num;
				}
			}
			this.Layout(this, e);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Orientation" /> property changes.</summary>
		// Token: 0x06003F47 RID: 16199 RVA: 0x000FBBB0 File Offset: 0x000F9DB0
		protected internal virtual void OnOrientationChanged()
		{
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000FBBB4 File Offset: 0x000F9DB4
		internal void SetBounds(Rectangle bounds)
		{
			if (this.bounds != bounds)
			{
				Rectangle rectangle = this.bounds;
				this.bounds = bounds;
				this.OnBoundsChanged(rectangle, bounds);
				this.OnLayout(new LayoutEventArgs(null, "Bounds"));
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000FBBFC File Offset: 0x000F9DFC
		private bool Layout(object container, LayoutEventArgs args)
		{
			ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)container;
			Point location = toolStripPanelRow.DisplayRectangle.Location;
			foreach (ToolStrip toolStrip in toolStripPanelRow.Controls)
			{
				if (this.Orientation == Orientation.Horizontal)
				{
					if (toolStrip.Stretch)
					{
						toolStrip.Width = this.bounds.Width - toolStrip.Margin.Horizontal - this.Padding.Horizontal;
					}
					else
					{
						toolStrip.Width = toolStrip.GetToolStripPreferredSize(Size.Empty).Width;
					}
					location.X += toolStrip.Margin.Left;
					toolStrip.Location = location;
					location.X += toolStrip.Width + toolStrip.Margin.Left;
				}
				else
				{
					if (toolStrip.Stretch)
					{
						toolStrip.Size = new Size(toolStrip.GetToolStripPreferredSize(Size.Empty).Width, this.bounds.Height - toolStrip.Margin.Vertical - this.Padding.Vertical);
					}
					else
					{
						toolStrip.Size = toolStrip.GetToolStripPreferredSize(Size.Empty);
					}
					location.Y += toolStrip.Margin.Top;
					toolStrip.Location = location;
					location.Y += toolStrip.Height + toolStrip.Margin.Top;
				}
			}
			return false;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000FBDB0 File Offset: 0x000F9FB0
		private void control_SizeChanged(object sender, EventArgs e)
		{
			this.OnLayout(new LayoutEventArgs((Control)sender, string.Empty));
		}

		// Token: 0x04001B3B RID: 6971
		private Rectangle bounds;

		// Token: 0x04001B3C RID: 6972
		internal List<Control> controls;

		// Token: 0x04001B3D RID: 6973
		private LayoutEngine layout_engine;

		// Token: 0x04001B3E RID: 6974
		private Padding margin;

		// Token: 0x04001B3F RID: 6975
		private Padding padding;

		// Token: 0x04001B40 RID: 6976
		private ToolStripPanel parent;
	}
}
