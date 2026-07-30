using System;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	internal class GUILayoutEntry
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000C3EE File Offset: 0x0000A5EE
		public virtual int marginLeft
		{
			get
			{
				return this.style.margin.left;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000C400 File Offset: 0x0000A600
		public virtual int marginRight
		{
			get
			{
				return this.style.margin.right;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000C412 File Offset: 0x0000A612
		public virtual int marginTop
		{
			get
			{
				return this.style.margin.top;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000C424 File Offset: 0x0000A624
		public virtual int marginBottom
		{
			get
			{
				return this.style.margin.bottom;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C436 File Offset: 0x0000A636
		public int marginHorizontal
		{
			get
			{
				return this.marginLeft + this.marginRight;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000C445 File Offset: 0x0000A645
		public int marginVertical
		{
			get
			{
				return this.marginBottom + this.marginTop;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C454 File Offset: 0x0000A654
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			bool flag = _style == null;
			if (flag)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00002201 File Offset: 0x00000401
		public virtual void CalcWidth()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00002201 File Offset: 0x00000401
		public virtual void CalcHeight()
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000C545 File Offset: 0x0000A745
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C562 File Offset: 0x0000A762
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C580 File Offset: 0x0000A780
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
			this.stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
			this.m_Style = style;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				foreach (GUILayoutOption guilayoutOption in options)
				{
					switch (guilayoutOption.type)
					{
					case GUILayoutOption.Type.fixedWidth:
						this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
						this.stretchWidth = 0;
						break;
					case GUILayoutOption.Type.fixedHeight:
						this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
						this.stretchHeight = 0;
						break;
					case GUILayoutOption.Type.minWidth:
					{
						this.minWidth = (float)guilayoutOption.value;
						bool flag2 = this.maxWidth < this.minWidth;
						if (flag2)
						{
							this.maxWidth = this.minWidth;
						}
						break;
					}
					case GUILayoutOption.Type.maxWidth:
					{
						this.maxWidth = (float)guilayoutOption.value;
						bool flag3 = this.minWidth > this.maxWidth;
						if (flag3)
						{
							this.minWidth = this.maxWidth;
						}
						this.stretchWidth = 0;
						break;
					}
					case GUILayoutOption.Type.minHeight:
					{
						this.minHeight = (float)guilayoutOption.value;
						bool flag4 = this.maxHeight < this.minHeight;
						if (flag4)
						{
							this.maxHeight = this.minHeight;
						}
						break;
					}
					case GUILayoutOption.Type.maxHeight:
					{
						this.maxHeight = (float)guilayoutOption.value;
						bool flag5 = this.minHeight > this.maxHeight;
						if (flag5)
						{
							this.minHeight = this.maxHeight;
						}
						this.stretchHeight = 0;
						break;
					}
					case GUILayoutOption.Type.stretchWidth:
						this.stretchWidth = (int)guilayoutOption.value;
						break;
					case GUILayoutOption.Type.stretchHeight:
						this.stretchHeight = (int)guilayoutOption.value;
						break;
					}
				}
				bool flag6 = this.maxWidth != 0f && this.maxWidth < this.minWidth;
				if (flag6)
				{
					this.maxWidth = this.minWidth;
				}
				bool flag7 = this.maxHeight != 0f && this.maxHeight < this.minHeight;
				if (flag7)
				{
					this.maxHeight = this.minHeight;
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new object[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style != null) ? this.style.name : "NULL",
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth,
				"-",
				this.maxWidth,
				(this.stretchWidth != 0) ? "+" : "",
				", H: ",
				this.minHeight,
				"-",
				this.maxHeight,
				(this.stretchHeight != 0) ? "+" : ""
			});
		}

		// Token: 0x040000E8 RID: 232
		public float minWidth;

		// Token: 0x040000E9 RID: 233
		public float maxWidth;

		// Token: 0x040000EA RID: 234
		public float minHeight;

		// Token: 0x040000EB RID: 235
		public float maxHeight;

		// Token: 0x040000EC RID: 236
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040000ED RID: 237
		public int stretchWidth;

		// Token: 0x040000EE RID: 238
		public int stretchHeight;

		// Token: 0x040000EF RID: 239
		public bool consideredForMargin = true;

		// Token: 0x040000F0 RID: 240
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x040000F1 RID: 241
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040000F2 RID: 242
		protected static int indent = 0;
	}
}
