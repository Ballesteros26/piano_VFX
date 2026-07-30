using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Associates a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with its component.</summary>
	// Token: 0x02000048 RID: 72
	public class ComponentGlyph : Glyph
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" /> class.</summary>
		/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		// Token: 0x06000274 RID: 628 RVA: 0x00008C07 File Offset: 0x00006E07
		public ComponentGlyph(IComponent relatedComponent)
			: this(relatedComponent, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" /> class. </summary>
		/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
		// Token: 0x06000275 RID: 629 RVA: 0x00008C11 File Offset: 0x00006E11
		public ComponentGlyph(IComponent relatedComponent, Behavior behavior)
			: base(behavior)
		{
			this.component = relatedComponent;
		}

		/// <summary>Gets the component that is associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> .</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008C21 File Offset: 0x00006E21
		[MonoTODO]
		public IComponent RelatedComponent
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Indicates whether a mouse click at the specified point should be handled by the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, null.</returns>
		/// <param name="p">A point to hit-test.</param>
		// Token: 0x06000277 RID: 631 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override Cursor GetHitTest(Point p)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides paint logic.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> containing the <see cref="P:System.Windows.Forms.Design.Behavior.BehaviorService.AdornerWindowGraphics" />  of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</param>
		// Token: 0x06000278 RID: 632 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void Paint(PaintEventArgs pe)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000FE RID: 254
		private IComponent component;
	}
}
