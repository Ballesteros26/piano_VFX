using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Represents a single user interface (UI) entity managed by an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</summary>
	// Token: 0x0200004A RID: 74
	public abstract class Glyph
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> class. </summary>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. Can be null.</param>
		// Token: 0x0600027D RID: 637 RVA: 0x00008C5F File Offset: 0x00006E5F
		[MonoTODO]
		protected Glyph(Behavior behavior)
		{
			this.SetBehavior(behavior);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />, or null if there is no behavior.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008C6E File Offset: 0x00006E6E
		[MonoTODO]
		public virtual Behavior Behavior
		{
			get
			{
				return this.behavior;
			}
		}

		/// <summary>Gets the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Provides hit test logic.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, null.</returns>
		/// <param name="p">A point to hit-test.</param>
		// Token: 0x06000280 RID: 640
		public abstract Cursor GetHitTest(Point p);

		/// <summary>Provides paint logic.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000281 RID: 641
		public abstract void Paint(PaintEventArgs pe);

		/// <summary>Changes the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <param name="behavior">A <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to associate with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		// Token: 0x06000282 RID: 642 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected void SetBehavior(Behavior behavior)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000100 RID: 256
		private Behavior behavior;
	}
}
