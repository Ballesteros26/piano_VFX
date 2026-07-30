using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Associates a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with its control.</summary>
	// Token: 0x02000049 RID: 73
	public class ControlBodyGlyph : ComponentGlyph
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> class.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		/// <param name="cursor">A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		// Token: 0x06000279 RID: 633 RVA: 0x00008C29 File Offset: 0x00006E29
		[MonoTODO]
		public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, Behavior behavior)
			: base(relatedComponent, behavior)
		{
			this.bounds = bounds;
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> class.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		/// <param name="cursor">A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		/// <param name="designer">A <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		// Token: 0x0600027A RID: 634 RVA: 0x00008C40 File Offset: 0x00006E40
		[MonoTODO]
		public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, ControlDesigner designer)
			: this(bounds, cursor, relatedComponent, designer.BehaviorService.CurrentBehavior)
		{
		}

		/// <summary>Gets the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008C57 File Offset: 0x00006E57
		[MonoTODO]
		public override Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Indicates whether a mouse click at the specified point should be handled by the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, null.</returns>
		/// <param name="p">A point to hit test.</param>
		// Token: 0x0600027C RID: 636 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override Cursor GetHitTest(Point p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000FF RID: 255
		private Rectangle bounds;
	}
}
