using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A1 RID: 161
	public class ContentsResizedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> class.</summary>
		/// <param name="newRectangle">A <see cref="T:System.Drawing.Rectangle" /> that specifies the requested dimensions of the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
		// Token: 0x060007D1 RID: 2001 RVA: 0x00022B40 File Offset: 0x00020D40
		public ContentsResizedEventArgs(Rectangle newRectangle)
		{
			this.rect = newRectangle;
		}

		/// <summary>Represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00022B50 File Offset: 0x00020D50
		public Rectangle NewRectangle
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x04000792 RID: 1938
		private Rectangle rect;
	}
}
