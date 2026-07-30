using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A5 RID: 421
	public struct Background : IEquatable<Background>
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0002C5B8 File Offset: 0x0002A7B8
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = value != null && this.vectorImage != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both texture and vectorImage on Background object");
				}
				this.m_Texture = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002C5F4 File Offset: 0x0002A7F4
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0002C60C File Offset: 0x0002A80C
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && this.texture != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both texture and vectorImage on Background object");
				}
				this.m_VectorImage = value;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002C648 File Offset: 0x0002A848
		[Obsolete("Use Background.FromTexture2D instead")]
		public Background(Texture2D t)
		{
			this.m_Texture = t;
			this.m_VectorImage = null;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002C65C File Offset: 0x0002A85C
		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002C680 File Offset: 0x0002A880
		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002C6A4 File Offset: 0x0002A8A4
		internal static Background FromObject(object obj)
		{
			Texture2D texture2D = obj as Texture2D;
			bool flag = texture2D != null;
			Background background;
			if (flag)
			{
				background = Background.FromTexture2D(texture2D);
			}
			else
			{
				VectorImage vectorImage = obj as VectorImage;
				bool flag2 = vectorImage != null;
				if (flag2)
				{
					background = Background.FromVectorImage(vectorImage);
				}
				else
				{
					background = default(Background);
				}
			}
			return background;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
		public static bool operator ==(Background lhs, Background rhs)
		{
			return EqualityComparer<Texture2D>.Default.Equals(lhs.texture, rhs.texture);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002C724 File Offset: 0x0002A924
		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002C740 File Offset: 0x0002A940
		public bool Equals(Background other)
		{
			return other == this;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002C760 File Offset: 0x0002A960
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Background background = (Background)obj;
				flag2 = background == this;
			}
			return flag2;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002C79C File Offset: 0x0002A99C
		public override int GetHashCode()
		{
			int num = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			}
			bool flag2 = this.vectorImage != null;
			if (flag2)
			{
				num = num * -1521134295 + EqualityComparer<VectorImage>.Default.GetHashCode(this.vectorImage);
			}
			return num;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002C808 File Offset: 0x0002AA08
		public override string ToString()
		{
			return string.Format("{0}", this.texture);
		}

		// Token: 0x0400051A RID: 1306
		private Texture2D m_Texture;

		// Token: 0x0400051B RID: 1307
		private VectorImage m_VectorImage;
	}
}
