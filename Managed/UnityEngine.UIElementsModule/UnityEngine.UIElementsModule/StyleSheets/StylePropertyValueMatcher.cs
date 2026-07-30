using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000273 RID: 627
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00052C50 File Offset: 0x00050E50
		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[this.m_CurrentIndex] : default(StylePropertyValue);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00052C81 File Offset: 0x00050E81
		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00007992 File Offset: 0x00005B92
		public override bool isVariable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00052C90 File Offset: 0x00050E90
		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle handle = this.m_Values[0].handle;
				bool flag2 = handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1;
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					StyleSheet sheet = this.current.sheet;
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00052DAC File Offset: 0x00050FAC
		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Keyword;
			bool flag2;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)current.handle.valueIndex;
				flag2 = valueIndex.ToUssString() == keyword.ToLower();
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					string text = current.sheet.ReadEnum(current.handle);
					flag2 = text == keyword.ToLower();
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00052E34 File Offset: 0x00051034
		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00052E5C File Offset: 0x0005105C
		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00052E84 File Offset: 0x00051084
		protected override bool MatchLength()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Pixel;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00052F08 File Offset: 0x00051108
		protected override bool MatchPercentage()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Percent;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00052F8C File Offset: 0x0005118C
		protected override bool MatchColor()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Color;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					Color clear = Color.clear;
					string text = current.sheet.ReadAsString(current.handle);
					bool flag4 = StyleSheetColor.TryGetColor(text.ToLower(), out clear);
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00053004 File Offset: 0x00051204
		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0005302C File Offset: 0x0005122C
		protected override bool MatchUrl()
		{
			return this.current.handle.valueType == StyleValueType.AssetReference;
		}

		// Token: 0x04000937 RID: 2359
		private List<StylePropertyValue> m_Values;
	}
}
