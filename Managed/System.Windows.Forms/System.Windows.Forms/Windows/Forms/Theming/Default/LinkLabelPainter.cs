using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x02000035 RID: 53
	internal class LinkLabelPainter
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000EB60 File Offset: 0x0000CD60
		private Color GetPieceColor(LinkLabel label, LinkLabel.Piece piece, int i)
		{
			if (!label.Enabled)
			{
				return label.DisabledLinkColor;
			}
			if (piece.link == null)
			{
				return label.ForeColor;
			}
			if (!piece.link.Enabled)
			{
				return label.DisabledLinkColor;
			}
			if (piece.link.Active)
			{
				return label.ActiveLinkColor;
			}
			if ((label.LinkVisited && i == 0) || piece.link.Visited)
			{
				return label.VisitedLinkColor;
			}
			return label.LinkColor;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public virtual void Draw(Graphics dc, Rectangle clip_rectangle, LinkLabel label)
		{
			Rectangle paddingClientRectangle = label.PaddingClientRectangle;
			label.DrawImage(dc, label.Image, paddingClientRectangle, label.ImageAlign);
			if (label.pieces == null)
			{
				return;
			}
			if (!label.Enabled)
			{
				dc.SetClip(clip_rectangle);
				ThemeEngine.Current.CPDrawStringDisabled(dc, label.Text, label.Font, label.BackColor, paddingClientRectangle, label.string_format);
				return;
			}
			Font linkFont = ThemeEngine.Current.GetLinkFont(label);
			Region region = new Region(default(Rectangle));
			for (int i = 0; i < label.pieces.Length; i++)
			{
				LinkLabel.Piece piece = label.pieces[i];
				if (piece.link == null)
				{
					region.Union(piece.region);
				}
				else
				{
					Color pieceColor = this.GetPieceColor(label, piece, i);
					Font font;
					if (label.LinkBehavior == LinkBehavior.AlwaysUnderline || label.LinkBehavior == LinkBehavior.SystemDefault || (label.LinkBehavior == LinkBehavior.HoverUnderline && piece.link.Hovered))
					{
						font = linkFont;
					}
					else
					{
						font = label.Font;
					}
					dc.Clip = piece.region;
					dc.Clip.Intersect(clip_rectangle);
					dc.DrawString(label.Text, font, ThemeEngine.Current.ResPool.GetSolidBrush(pieceColor), paddingClientRectangle, label.string_format);
					if (piece.link != null && piece.link.Focused)
					{
						foreach (RectangleF rectangleF in piece.region.GetRegionScans(dc.Transform))
						{
							ControlPaint.DrawFocusRectangle(dc, Rectangle.Round(rectangleF), label.ForeColor, label.BackColor);
						}
					}
				}
			}
			if (!region.IsEmpty(dc))
			{
				dc.Clip = region;
				dc.Clip.Intersect(clip_rectangle);
				if (!dc.Clip.IsEmpty(dc))
				{
					dc.DrawString(label.Text, label.Font, ThemeEngine.Current.ResPool.GetSolidBrush(label.ForeColor), paddingClientRectangle, label.string_format);
				}
			}
		}
	}
}
