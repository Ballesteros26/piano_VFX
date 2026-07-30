using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides ambient property values to top-level controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003C RID: 60
	public sealed class AmbientProperties
	{
		/// <summary>Gets or sets the ambient background color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the background color of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000F784 File Offset: 0x0000D984
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000F78C File Offset: 0x0000D98C
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
			set
			{
				this.back_color = value;
			}
		}

		/// <summary>Gets or sets the ambient cursor of an object.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000F798 File Offset: 0x0000D998
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		/// <summary>Gets or sets the ambient font of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font used when displaying text within an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		/// <summary>Gets or sets the ambient foreground color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the foreground color of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
			set
			{
				this.fore_color = value;
			}
		}

		// Token: 0x0400059F RID: 1439
		private Color fore_color;

		// Token: 0x040005A0 RID: 1440
		private Color back_color;

		// Token: 0x040005A1 RID: 1441
		private Font font;

		// Token: 0x040005A2 RID: 1442
		private Cursor cursor;
	}
}
