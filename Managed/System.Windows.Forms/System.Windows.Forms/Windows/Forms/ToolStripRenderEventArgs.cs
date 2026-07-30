using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs)" />, <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs)" />, and <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs)" /> methods. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000376 RID: 886
	public class ToolStripRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		// Token: 0x06003FF1 RID: 16369 RVA: 0x000FF1E4 File Offset: 0x000FD3E4
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip)
			: this(g, toolStrip, new Rectangle(0, 0, 100, 25), SystemColors.Control)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" />, using the specified <see cref="T:System.Drawing.Graphics" /> to paint the specified bounds with the specified <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		/// <param name="affectedBounds">The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</param>
		// Token: 0x06003FF2 RID: 16370 RVA: 0x000FF200 File Offset: 0x000FD400
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle affectedBounds, Color backColor)
		{
			this.graphics = g;
			this.tool_strip = toolStrip;
			this.affected_bounds = affectedBounds;
			this.back_color = backColor;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted. </summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000FF228 File Offset: 0x000FD428
		public Rectangle AffectedBounds
		{
			get
			{
				return this.affected_bounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000FF230 File Offset: 0x000FD430
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000FF238 File Offset: 0x000FD438
		public Rectangle ConnectedArea
		{
			get
			{
				return this.connected_area;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x000FF240 File Offset: 0x000FD440
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000FF248 File Offset: 0x000FD448
		public ToolStrip ToolStrip
		{
			get
			{
				return this.tool_strip;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (set) Token: 0x06003FF8 RID: 16376 RVA: 0x000FF250 File Offset: 0x000FD450
		internal Rectangle InternalConnectedArea
		{
			set
			{
				this.connected_area = value;
			}
		}

		// Token: 0x04001B59 RID: 7001
		private Rectangle affected_bounds;

		// Token: 0x04001B5A RID: 7002
		private Color back_color;

		// Token: 0x04001B5B RID: 7003
		private Rectangle connected_area;

		// Token: 0x04001B5C RID: 7004
		private Graphics graphics;

		// Token: 0x04001B5D RID: 7005
		private ToolStrip tool_strip;
	}
}
