using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Manages a collection of user-interface related <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	public sealed class Adorner
	{
		/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window.</summary>
		// Token: 0x06000223 RID: 547 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window within the given <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <param name="rectangle">The area to invalidate.</param>
		// Token: 0x06000224 RID: 548 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate(Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window within the given <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate.</param>
		// Token: 0x06000225 RID: 549 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate(Region region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public BehaviorService BehaviorService
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

		/// <summary>Gets or sets a value indicating if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is enabled.</summary>
		/// <returns>true, if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is enabled; otherwise, false.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool Enabled
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

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> collection.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public GlyphCollection Glyphs
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
