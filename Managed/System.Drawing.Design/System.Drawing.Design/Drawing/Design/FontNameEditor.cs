using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> that paints a glyph for the font name.</summary>
	// Token: 0x02000015 RID: 21
	public class FontNameEditor : UITypeEditor
	{
		/// <summary>Determines if this editor supports the painting of a representation of an object's value.</summary>
		/// <returns>true if <see cref="Overload:System.Drawing.Design.FontNameEditor.PaintValue" /> is implemented; otherwise, false.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		// Token: 0x06000038 RID: 56 RVA: 0x0000245B File Offset: 0x0000065B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Paints a representative value of the given object to the provided canvas. Painting should be done within the boundaries of the provided rectangle.</summary>
		/// <param name="e">What to paint and where to paint it. </param>
		// Token: 0x06000039 RID: 57 RVA: 0x0000318C File Offset: 0x0000138C
		public override void PaintValue(PaintValueEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.FillRectangle(SystemBrushes.ActiveCaption, e.Bounds);
			string text = e.Value as string;
			if (text != null && text.Length > 0)
			{
				using (Font font = new Font(text, (float)e.Bounds.Height, FontStyle.Regular, GraphicsUnit.Pixel))
				{
					graphics.DrawString("Ab", font, SystemBrushes.ActiveCaptionText, e.Bounds);
				}
			}
			graphics.DrawRectangle(Pens.Black, e.Bounds);
		}

		// Token: 0x04000039 RID: 57
		private const string PreviewString = "Ab";
	}
}
