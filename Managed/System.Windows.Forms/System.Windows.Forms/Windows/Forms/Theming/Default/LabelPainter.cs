using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x02000034 RID: 52
	internal class LabelPainter
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000EABC File Offset: 0x0000CCBC
		public virtual void Draw(Graphics dc, Rectangle client_rectangle, Label label)
		{
			Rectangle paddingClientRectangle = label.PaddingClientRectangle;
			label.DrawImage(dc, label.Image, paddingClientRectangle, label.ImageAlign);
			if (label.Enabled)
			{
				dc.DrawString(label.Text, label.Font, ThemeEngine.Current.ResPool.GetSolidBrush(label.ForeColor), paddingClientRectangle, label.string_format);
			}
			else
			{
				ControlPaint.DrawStringDisabled(dc, label.Text, label.Font, label.BackColor, paddingClientRectangle, label.string_format);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public virtual Size DefaultSize
		{
			get
			{
				return new Size(100, 23);
			}
		}
	}
}
