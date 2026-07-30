using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	// Token: 0x02000327 RID: 807
	internal class SystemResPool
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x000D333C File Offset: 0x000D153C
		public Pen GetPen(Color color)
		{
			int num = color.ToArgb();
			Hashtable hashtable = this.pens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.pens[num] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color);
					this.pens.Add(num, pen3);
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000D33D0 File Offset: 0x000D15D0
		public Pen GetDashPen(Color color, DashStyle dashStyle)
		{
			string text = color.ToString() + dashStyle;
			Hashtable hashtable = this.dashpens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.dashpens[text] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color);
					pen3.DashStyle = dashStyle;
					this.dashpens[text] = pen3;
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000D346C File Offset: 0x000D166C
		public Pen GetSizedPen(Color color, int size)
		{
			string text = color.ToString() + size;
			Hashtable hashtable = this.sizedpens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.sizedpens[text] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color, (float)size);
					this.sizedpens[text] = pen3;
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000D3504 File Offset: 0x000D1704
		public SolidBrush GetSolidBrush(Color color)
		{
			int num = color.ToArgb();
			Hashtable hashtable = this.solidbrushes;
			SolidBrush solidBrush2;
			lock (hashtable)
			{
				SolidBrush solidBrush = this.solidbrushes[num] as SolidBrush;
				if (solidBrush != null)
				{
					solidBrush2 = solidBrush;
				}
				else
				{
					SolidBrush solidBrush3 = new SolidBrush(color);
					this.solidbrushes.Add(num, solidBrush3);
					solidBrush2 = solidBrush3;
				}
			}
			return solidBrush2;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000D3598 File Offset: 0x000D1798
		public HatchBrush GetHatchBrush(HatchStyle hatchStyle, Color foreColor, Color backColor)
		{
			int num = hatchStyle;
			string text = num.ToString() + foreColor.ToString() + backColor.ToString();
			Hashtable hashtable = this.hatchbrushes;
			HatchBrush hatchBrush2;
			lock (hashtable)
			{
				HatchBrush hatchBrush = (HatchBrush)this.hatchbrushes[text];
				if (hatchBrush == null)
				{
					hatchBrush = new HatchBrush(hatchStyle, foreColor, backColor);
					this.hatchbrushes.Add(text, hatchBrush);
				}
				hatchBrush2 = hatchBrush;
			}
			return hatchBrush2;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000D3630 File Offset: 0x000D1830
		public void AddUIImage(Image image, string name, int size)
		{
			string text = name + size.ToString();
			Hashtable hashtable = this.uiImages;
			lock (hashtable)
			{
				if (!this.uiImages.Contains(text))
				{
					this.uiImages.Add(text, image);
				}
			}
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000D36A4 File Offset: 0x000D18A4
		public Image GetUIImage(string name, int size)
		{
			string text = name + size.ToString();
			return this.uiImages[text] as Image;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000D36D4 File Offset: 0x000D18D4
		public CPColor GetCPColor(Color color)
		{
			Hashtable hashtable = this.cpcolors;
			CPColor cpcolor2;
			lock (hashtable)
			{
				object obj = this.cpcolors[color];
				if (obj == null)
				{
					CPColor cpcolor = default(CPColor);
					cpcolor.Dark = ControlPaint.Dark(color);
					cpcolor.DarkDark = ControlPaint.DarkDark(color);
					cpcolor.Light = ControlPaint.Light(color);
					cpcolor.LightLight = ControlPaint.LightLight(color);
					this.cpcolors.Add(color, cpcolor);
					cpcolor2 = cpcolor;
				}
				else
				{
					cpcolor2 = (CPColor)obj;
				}
			}
			return cpcolor2;
		}

		// Token: 0x04001983 RID: 6531
		private Hashtable pens = new Hashtable();

		// Token: 0x04001984 RID: 6532
		private Hashtable dashpens = new Hashtable();

		// Token: 0x04001985 RID: 6533
		private Hashtable sizedpens = new Hashtable();

		// Token: 0x04001986 RID: 6534
		private Hashtable solidbrushes = new Hashtable();

		// Token: 0x04001987 RID: 6535
		private Hashtable hatchbrushes = new Hashtable();

		// Token: 0x04001988 RID: 6536
		private Hashtable uiImages = new Hashtable();

		// Token: 0x04001989 RID: 6537
		private Hashtable cpcolors = new Hashtable();
	}
}
