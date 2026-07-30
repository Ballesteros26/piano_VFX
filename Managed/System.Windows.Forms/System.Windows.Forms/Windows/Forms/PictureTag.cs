using System;
using System.Drawing;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	// Token: 0x0200031D RID: 797
	internal class PictureTag : LineTag
	{
		// Token: 0x06003596 RID: 13718 RVA: 0x000D1AA0 File Offset: 0x000CFCA0
		internal PictureTag(Line line, int start, Picture picture)
			: base(line, start)
		{
			this.picture = picture;
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000D1AB4 File Offset: 0x000CFCB4
		public override bool IsTextTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000D1AB8 File Offset: 0x000CFCB8
		public override SizeF SizeOfPosition(Graphics dc, int pos)
		{
			return this.picture.Size;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000D1AC8 File Offset: 0x000CFCC8
		internal override int MaxHeight()
		{
			return (int)(this.picture.Height + 0.5f);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000D1ADC File Offset: 0x000CFCDC
		public override void Draw(Graphics dc, Color color, float xoff, float y, int start, int end)
		{
			this.picture.DrawImage(dc, xoff + base.Line.widths[start], y, false);
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000D1B00 File Offset: 0x000CFD00
		public override void Draw(Graphics dc, Color color, float xoff, float y, int start, int end, string text)
		{
			this.picture.DrawImage(dc, xoff + base.Line.widths[start], y, false);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000D1B24 File Offset: 0x000CFD24
		public override string Text()
		{
			return "I";
		}

		// Token: 0x0400193B RID: 6459
		internal Picture picture;
	}
}
