using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000283 RID: 643
	public struct StyleValues
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00054D68 File Offset: 0x00052F68
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00054D92 File Offset: 0x00052F92
		public float top
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Top).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Top, value);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00054DA4 File Offset: 0x00052FA4
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x00054DCE File Offset: 0x00052FCE
		public float left
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Left).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Left, value);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00054DE0 File Offset: 0x00052FE0
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00054E0A File Offset: 0x0005300A
		public float width
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Width).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Width, value);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00054E1C File Offset: 0x0005301C
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00054E46 File Offset: 0x00053046
		public float height
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Height).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Height, value);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00054E58 File Offset: 0x00053058
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x00054E82 File Offset: 0x00053082
		public float right
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Right).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Right, value);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00054E94 File Offset: 0x00053094
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x00054EBE File Offset: 0x000530BE
		public float bottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Bottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Bottom, value);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00054ED0 File Offset: 0x000530D0
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x00054EF6 File Offset: 0x000530F6
		public Color color
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.Color).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Color, value);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00054F04 File Offset: 0x00053104
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x00054F2E File Offset: 0x0005312E
		public Color backgroundColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BackgroundColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BackgroundColor, value);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00054F40 File Offset: 0x00053140
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x00054F6A File Offset: 0x0005316A
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.UnityBackgroundImageTintColor, value);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00054F7C File Offset: 0x0005317C
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x00054FA6 File Offset: 0x000531A6
		public Color borderColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BorderColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderColor, value);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00054FB8 File Offset: 0x000531B8
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x00054FE2 File Offset: 0x000531E2
		public float marginLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginLeft, value);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00054FF4 File Offset: 0x000531F4
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0005501E File Offset: 0x0005321E
		public float marginTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginTop, value);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00055030 File Offset: 0x00053230
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x0005505A File Offset: 0x0005325A
		public float marginRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginRight, value);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0005506C File Offset: 0x0005326C
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x00055096 File Offset: 0x00053296
		public float marginBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginBottom, value);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000550A8 File Offset: 0x000532A8
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x000550D2 File Offset: 0x000532D2
		public float paddingLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingLeft, value);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000550E4 File Offset: 0x000532E4
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0005510E File Offset: 0x0005330E
		public float paddingTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingTop, value);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00055120 File Offset: 0x00053320
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x0005514A File Offset: 0x0005334A
		public float paddingRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingRight, value);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0005515C File Offset: 0x0005335C
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00055186 File Offset: 0x00053386
		public float paddingBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingBottom, value);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00055198 File Offset: 0x00053398
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x000551C2 File Offset: 0x000533C2
		public float borderLeftWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderLeftWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderLeftWidth, value);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x000551D4 File Offset: 0x000533D4
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x000551FE File Offset: 0x000533FE
		public float borderRightWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderRightWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderRightWidth, value);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00055210 File Offset: 0x00053410
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x0005523A File Offset: 0x0005343A
		public float borderTopWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopWidth, value);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0005524C File Offset: 0x0005344C
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00055276 File Offset: 0x00053476
		public float borderBottomWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomWidth, value);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00055288 File Offset: 0x00053488
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x000552B2 File Offset: 0x000534B2
		public float borderTopLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopLeftRadius, value);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x000552C4 File Offset: 0x000534C4
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x000552EE File Offset: 0x000534EE
		public float borderTopRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopRightRadius, value);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00055300 File Offset: 0x00053500
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x0005532A File Offset: 0x0005352A
		public float borderBottomLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomLeftRadius, value);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0005533C File Offset: 0x0005353C
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x00055366 File Offset: 0x00053566
		public float borderBottomRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomRightRadius, value);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00055378 File Offset: 0x00053578
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x000553A2 File Offset: 0x000535A2
		public float opacity
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Opacity).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Opacity, value);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000553B4 File Offset: 0x000535B4
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x000553DE File Offset: 0x000535DE
		public float flexGrow
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexGrow).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x000553F0 File Offset: 0x000535F0
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x000553DE File Offset: 0x000535DE
		public float flexShrink
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexShrink).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0005541C File Offset: 0x0005361C
		internal void SetValue(StylePropertyId id, float value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.number = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00055450 File Offset: 0x00053650
		internal void SetValue(StylePropertyId id, Color value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.color = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00055484 File Offset: 0x00053684
		internal StyleValueCollection Values()
		{
			bool flag = this.m_StyleValues == null;
			if (flag)
			{
				this.m_StyleValues = new StyleValueCollection();
			}
			return this.m_StyleValues;
		}

		// Token: 0x0400098D RID: 2445
		internal StyleValueCollection m_StyleValues;
	}
}
