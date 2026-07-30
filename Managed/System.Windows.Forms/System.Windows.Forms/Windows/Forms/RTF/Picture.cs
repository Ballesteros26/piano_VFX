using System;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200002A RID: 42
	internal class Picture
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public Minor ImageType
		{
			get
			{
				return this.image_type;
			}
			set
			{
				this.image_type = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public MemoryStream Data
		{
			get
			{
				if (this.data == null)
				{
					this.data = new MemoryStream();
				}
				return this.data;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public float Width
		{
			get
			{
				float num = this.width;
				if (num == -1f)
				{
					if (this.image == null)
					{
						this.image = this.ToImage();
					}
					num = (float)this.image.Width;
				}
				return num;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000B718 File Offset: 0x00009918
		public float Height
		{
			get
			{
				float num = this.height;
				if (num == -1f)
				{
					if (this.image == null)
					{
						this.image = this.ToImage();
					}
					num = (float)this.image.Height;
				}
				return num;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000B75C File Offset: 0x0000995C
		public SizeF Size
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000B770 File Offset: 0x00009970
		public void SetWidthFromTwips(int twips)
		{
			this.width = (float)((int)((float)twips / 1440f * Picture.dpix + 0.5f));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B790 File Offset: 0x00009990
		public void SetHeightFromTwips(int twips)
		{
			this.height = (float)((int)((float)twips / 1440f * Picture.dpix + 0.5f));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public bool IsValid()
		{
			if (this.data == null)
			{
				return false;
			}
			switch (this.image_type)
			{
			case Minor.WinMetafile:
			case Minor.PngBlip:
				return true;
			}
			return false;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B7FC File Offset: 0x000099FC
		public void DrawImage(Graphics dc, float x, float y, bool selected)
		{
			if (this.image == null)
			{
				this.image = this.ToImage();
			}
			float num = this.height;
			float num2 = this.width;
			if (num == -1f)
			{
				num = (float)this.image.Height;
			}
			if (num2 == -1f)
			{
				num2 = (float)this.image.Width;
			}
			dc.DrawImage(this.image, x, y, num2, num);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B870 File Offset: 0x00009A70
		public Image ToImage()
		{
			this.data.Position = 0L;
			return Image.FromStream(this.data);
		}

		// Token: 0x04000346 RID: 838
		private Minor image_type;

		// Token: 0x04000347 RID: 839
		private Image image;

		// Token: 0x04000348 RID: 840
		private MemoryStream data;

		// Token: 0x04000349 RID: 841
		private float width = -1f;

		// Token: 0x0400034A RID: 842
		private float height = -1f;

		// Token: 0x0400034B RID: 843
		private static readonly float dpix = TextRenderer.GetDpi().Width;
	}
}
