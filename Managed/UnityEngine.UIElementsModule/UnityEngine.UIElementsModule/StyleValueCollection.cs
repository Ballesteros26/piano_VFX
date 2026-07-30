using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A9 RID: 425
	internal class StyleValueCollection
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0002F06C File Offset: 0x0002D26C
		public StyleLength GetStyleLength(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleLength styleLength;
			if (flag)
			{
				styleLength = new StyleLength(styleValue.number, styleValue.keyword);
			}
			else
			{
				styleLength = StyleKeyword.Null;
			}
			return styleLength;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
		public StyleFloat GetStyleFloat(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = new StyleFloat(styleValue.number, styleValue.keyword);
			}
			else
			{
				styleFloat = StyleKeyword.Null;
			}
			return styleFloat;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		public StyleInt GetStyleInt(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleInt styleInt;
			if (flag)
			{
				styleInt = new StyleInt((int)styleValue.number, styleValue.keyword);
			}
			else
			{
				styleInt = StyleKeyword.Null;
			}
			return styleInt;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002F13C File Offset: 0x0002D33C
		public StyleColor GetStyleColor(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleColor styleColor;
			if (flag)
			{
				styleColor = new StyleColor(styleValue.color, styleValue.keyword);
			}
			else
			{
				styleColor = StyleKeyword.Null;
			}
			return styleColor;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002F180 File Offset: 0x0002D380
		public StyleBackground GetStyleBackground(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleBackground styleBackground;
			if (flag)
			{
				Texture2D texture2D = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null);
				styleBackground = new StyleBackground(texture2D, styleValue.keyword);
			}
			else
			{
				styleBackground = StyleKeyword.Null;
			}
			return styleBackground;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		public StyleFont GetStyleFont(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFont styleFont;
			if (flag)
			{
				Font font = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null);
				styleFont = new StyleFont(font, styleValue.keyword);
			}
			else
			{
				styleFont = StyleKeyword.Null;
			}
			return styleFont;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002F240 File Offset: 0x0002D440
		public bool TryGetStyleValue(StylePropertyId id, ref StyleValue value)
		{
			value.id = StylePropertyId.Unknown;
			foreach (StyleValue styleValue in this.m_Values)
			{
				bool flag = styleValue.id == id;
				if (flag)
				{
					value = styleValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		public void SetStyleValue(StyleValue value)
		{
			for (int i = 0; i < this.m_Values.Count; i++)
			{
				bool flag = this.m_Values[i].id == value.id;
				if (flag)
				{
					bool flag2 = value.keyword == StyleKeyword.Null;
					if (flag2)
					{
						this.m_Values.RemoveAt(i);
					}
					else
					{
						this.m_Values[i] = value;
					}
					return;
				}
			}
			this.m_Values.Add(value);
		}

		// Token: 0x04000523 RID: 1315
		internal List<StyleValue> m_Values = new List<StyleValue>();
	}
}
