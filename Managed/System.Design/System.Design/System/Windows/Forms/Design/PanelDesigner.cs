using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000032 RID: 50
	internal class PanelDesigner : ParentControlDesigner
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000565C File Offset: 0x0000385C
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005668 File Offset: 0x00003868
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments(pe);
			GraphicsState graphicsState = pe.Graphics.Save();
			pe.Graphics.TranslateTransform((float)this.Control.ClientRectangle.X, (float)this.Control.ClientRectangle.Y);
			ControlPaint.DrawBorder(pe.Graphics, this.Control.ClientRectangle, SystemColors.ControlDarkDark, 2);
			pe.Graphics.Restore(graphicsState);
		}
	}
}
