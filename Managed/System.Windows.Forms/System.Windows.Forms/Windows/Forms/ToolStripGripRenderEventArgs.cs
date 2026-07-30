using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000352 RID: 850
	public class ToolStripGripRenderEventArgs : ToolStripRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> class.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to paint the move handle.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> the move handle is to be drawn on.</param>
		// Token: 0x06003CDE RID: 15582 RVA: 0x000F4978 File Offset: 0x000F2B78
		public ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip)
			: base(g, toolStrip)
		{
			this.grip_bounds = new Rectangle(2, 0, 3, 25);
			this.grip_display_style = ToolStripGripDisplayStyle.Vertical;
			this.grip_style = ToolStripGripStyle.Visible;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000F49AC File Offset: 0x000F2BAC
		internal ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle gripBounds, ToolStripGripDisplayStyle displayStyle, ToolStripGripStyle gripStyle)
			: base(g, toolStrip)
		{
			this.grip_bounds = gripBounds;
			this.grip_display_style = displayStyle;
			this.grip_style = gripStyle;
		}

		/// <summary>Gets the rectangle representing the area in which to paint the move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area in which to paint the move handle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000F49D0 File Offset: 0x000F2BD0
		public Rectangle GripBounds
		{
			get
			{
				return this.grip_bounds;
			}
		}

		/// <summary>Gets the style that indicates whether the move handle is displayed vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000F49D8 File Offset: 0x000F2BD8
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return this.grip_display_style;
			}
		}

		/// <summary>Gets the style that indicates whether or not the move handle is visible.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x000F49E0 File Offset: 0x000F2BE0
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return this.grip_style;
			}
		}

		// Token: 0x04001A8C RID: 6796
		private Rectangle grip_bounds;

		// Token: 0x04001A8D RID: 6797
		private ToolStripGripDisplayStyle grip_display_style;

		// Token: 0x04001A8E RID: 6798
		private ToolStripGripStyle grip_style;
	}
}
