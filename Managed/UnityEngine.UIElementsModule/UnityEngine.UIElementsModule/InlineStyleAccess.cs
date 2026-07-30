using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AA RID: 426
	internal class InlineStyleAccess : StyleValueCollection, IStyle
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0002F351 File Offset: 0x0002D551
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0002F359 File Offset: 0x0002D559
		private VisualElement ve { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0002F362 File Offset: 0x0002D562
		public InlineStyleAccess.InlineRule inlineRule
		{
			get
			{
				return this.m_InlineRule;
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002F36C File Offset: 0x0002D56C
		public InlineStyleAccess(VisualElement ve)
		{
			this.ve = ve;
			bool isShared = ve.computedStyle.isShared;
			if (isShared)
			{
				ComputedStyle computedStyle = ComputedStyle.Create(false);
				computedStyle.CopyShared(ve.m_SharedStyle);
				ve.m_Style = computedStyle;
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002F3B8 File Offset: 0x0002D5B8
		protected override void Finalize()
		{
			try
			{
				StyleValue styleValue = default(StyleValue);
				bool flag = base.TryGetStyleValue(StylePropertyId.BackgroundImage, ref styleValue);
				if (flag)
				{
					bool isAllocated = styleValue.resource.IsAllocated;
					if (isAllocated)
					{
						styleValue.resource.Free();
					}
				}
				bool flag2 = base.TryGetStyleValue(StylePropertyId.UnityFont, ref styleValue);
				if (flag2)
				{
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002F448 File Offset: 0x0002D648
		public void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			this.m_InlineRule.sheet = sheet;
			this.m_InlineRule.properties = rule.properties;
			this.m_InlineRule.propertyIds = StyleSheetCache.GetPropertyIds(rule);
			this.ApplyInlineStyles(this.ve.sharedStyle);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002F498 File Offset: 0x0002D698
		public void ApplyInlineStyles(ComputedStyle sharedStyle)
		{
			Debug.Assert(!this.ve.m_Style.isShared);
			this.ve.m_Style.CopyShared(sharedStyle);
			bool flag = this.m_InlineRule.sheet != null;
			if (flag)
			{
				VisualElement parent = this.ve.hierarchy.parent;
				ComputedStyle computedStyle = ((parent != null) ? parent.computedStyle : null);
				InlineStyleAccess.s_StylePropertyReader.SetInlineContext(this.m_InlineRule.sheet, this.m_InlineRule.properties, this.m_InlineRule.propertyIds, 1f);
				this.ve.m_Style.ApplyProperties(InlineStyleAccess.s_StylePropertyReader, computedStyle);
			}
			foreach (StyleValue styleValue in this.m_Values)
			{
				this.ApplyStyleValue(styleValue);
			}
			bool flag2 = this.ve.style.cursor != StyleKeyword.Null;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleCursor(this.ve.style.cursor);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002F5E4 File Offset: 0x0002D7E4
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0002F614 File Offset: 0x0002D814
		StyleCursor IStyle.cursor
		{
			get
			{
				StyleCursor styleCursor = default(StyleCursor);
				bool flag = this.TryGetInlineCursor(ref styleCursor);
				StyleCursor styleCursor2;
				if (flag)
				{
					styleCursor2 = styleCursor;
				}
				else
				{
					styleCursor2 = StyleKeyword.Null;
				}
				return styleCursor2;
			}
			set
			{
				bool flag = this.SetInlineCursor(value, this.ve.sharedStyle.cursor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002F650 File Offset: 0x0002D850
		private bool SetStyleValue(StylePropertyId id, StyleLength inlineValue, StyleLength sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.length == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.length = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.length = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002F724 File Offset: 0x0002D924
		private bool SetStyleValue(StylePropertyId id, StyleFloat inlineValue, StyleFloat sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002F7F0 File Offset: 0x0002D9F0
		private bool SetStyleValue(StylePropertyId id, StyleInt inlineValue, StyleInt sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = (float)sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
		private bool SetStyleValue(StylePropertyId id, StyleColor inlineValue, StyleColor sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.color == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.color = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.color = sharedValue.value;
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002F998 File Offset: 0x0002DB98
		private bool SetStyleValue<T>(StylePropertyId id, StyleEnum<T> inlineValue, StyleEnum<T> sharedValue) where T : struct, IConvertible
		{
			StyleValue styleValue = default(StyleValue);
			int num = UnsafeUtility.EnumToInt<T>(inlineValue.value);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)num && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)num;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.number = (float)UnsafeUtility.EnumToInt<T>(sharedValue.value);
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002FA74 File Offset: 0x0002DC74
		private bool SetStyleValue(StylePropertyId id, StyleBackground inlineValue, StyleBackground sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				VectorImage vectorImage = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as VectorImage) : null);
				Texture2D texture2D = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null);
				bool flag2 = vectorImage == inlineValue.value.vectorImage && texture2D == inlineValue.value.texture && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.vectorImage != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.vectorImage);
			}
			else
			{
				bool flag5 = inlineValue.value.texture != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.texture);
				}
				else
				{
					styleValue.resource = default(GCHandle);
				}
			}
			base.SetStyleValue(styleValue);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			if (flag6)
			{
				styleValue.keyword = sharedValue.keyword;
				bool flag7 = sharedValue.value.texture != null;
				if (flag7)
				{
					styleValue.resource = GCHandle.Alloc(sharedValue.value.texture);
				}
				else
				{
					bool flag8 = sharedValue.value.vectorImage != null;
					if (flag8)
					{
						styleValue.resource = GCHandle.Alloc(sharedValue.value.vectorImage);
					}
					else
					{
						styleValue.resource = default(GCHandle);
					}
				}
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002FCC0 File Offset: 0x0002DEC0
		private bool SetStyleValue(StylePropertyId id, StyleFont inlineValue, StyleFont sharedValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					Font font = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null);
					bool flag2 = font == inlineValue.value && styleValue.keyword == inlineValue.keyword;
					if (flag2)
					{
						return false;
					}
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.resource = ((inlineValue.value != null) ? GCHandle.Alloc(inlineValue.value) : default(GCHandle));
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleValue.keyword = sharedValue.keyword;
				styleValue.resource = ((sharedValue.value != null) ? GCHandle.Alloc(sharedValue.value) : default(GCHandle));
			}
			this.ApplyStyleValue(styleValue);
			return true;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002FE30 File Offset: 0x0002E030
		private bool SetInlineCursor(StyleCursor inlineValue, StyleCursor sharedValue)
		{
			StyleCursor styleCursor = default(StyleCursor);
			bool flag = this.TryGetInlineCursor(ref styleCursor);
			if (flag)
			{
				bool flag2 = styleCursor.value == inlineValue.value && styleCursor.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleCursor.value = inlineValue.value;
			styleCursor.keyword = inlineValue.keyword;
			this.SetInlineCursor(styleCursor);
			bool flag4 = styleCursor.keyword == StyleKeyword.Null;
			if (flag4)
			{
				styleCursor.keyword = sharedValue.keyword;
				styleCursor.value = sharedValue.value;
			}
			this.ve.computedStyle.ApplyStyleCursor(styleCursor);
			return true;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002FF0C File Offset: 0x0002E10C
		private void ApplyStyleValue(StyleValue value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ComputedStyle computedStyle = ((parent != null) ? parent.computedStyle : null);
			this.ve.computedStyle.ApplyStyleValue(value, computedStyle);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002FF50 File Offset: 0x0002E150
		public bool TryGetInlineCursor(ref StyleCursor value)
		{
			bool hasInlineCursor = this.m_HasInlineCursor;
			bool flag;
			if (hasInlineCursor)
			{
				value = this.m_InlineCursor;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002FF7E File Offset: 0x0002E17E
		public void SetInlineCursor(StyleCursor value)
		{
			this.m_InlineCursor = value;
			this.m_HasInlineCursor = true;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0002FF90 File Offset: 0x0002E190
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		StyleEnum<Align> IStyle.alignContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignContent);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> styleEnum = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignContent, styleEnum, this.ve.sharedStyle.alignContent);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignContent = (YogaAlign)this.ve.computedStyle.alignContent.value;
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00030040 File Offset: 0x0002E240
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00030074 File Offset: 0x0002E274
		StyleEnum<Align> IStyle.alignItems
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignItems);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> styleEnum = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignItems, styleEnum, this.ve.sharedStyle.alignItems);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignItems = (YogaAlign)this.ve.computedStyle.alignItems.value;
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000300F0 File Offset: 0x0002E2F0
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00030124 File Offset: 0x0002E324
		StyleEnum<Align> IStyle.alignSelf
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignSelf);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Align> styleEnum = new StyleEnum<Align>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignSelf, styleEnum, this.ve.sharedStyle.alignSelf);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignSelf = (YogaAlign)this.ve.computedStyle.alignSelf.value;
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000301A0 File Offset: 0x0002E3A0
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000301C0 File Offset: 0x0002E3C0
		StyleColor IStyle.backgroundColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BackgroundColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundColor, value, this.ve.sharedStyle.backgroundColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00030204 File Offset: 0x0002E404
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00030224 File Offset: 0x0002E424
		StyleBackground IStyle.backgroundImage
		{
			get
			{
				return base.GetStyleBackground(StylePropertyId.BackgroundImage);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundImage, value, this.ve.sharedStyle.backgroundImage);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00030268 File Offset: 0x0002E468
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00030288 File Offset: 0x0002E488
		StyleColor IStyle.borderBottomColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderBottomColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomColor, value, this.ve.sharedStyle.borderBottomColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000302CC File Offset: 0x0002E4CC
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x000302EC File Offset: 0x0002E4EC
		StyleLength IStyle.borderBottomLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomLeftRadius, value, this.ve.sharedStyle.borderBottomLeftRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00030330 File Offset: 0x0002E530
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00030350 File Offset: 0x0002E550
		StyleLength IStyle.borderBottomRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomRightRadius, value, this.ve.sharedStyle.borderBottomRightRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00030394 File Offset: 0x0002E594
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x000303B4 File Offset: 0x0002E5B4
		StyleFloat IStyle.borderBottomWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderBottomWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomWidth, value, this.ve.sharedStyle.borderBottomWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderBottomWidth = this.ve.computedStyle.borderBottomWidth.value;
				}
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00030420 File Offset: 0x0002E620
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00030440 File Offset: 0x0002E640
		StyleColor IStyle.borderLeftColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderLeftColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftColor, value, this.ve.sharedStyle.borderLeftColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00030484 File Offset: 0x0002E684
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x000304A4 File Offset: 0x0002E6A4
		StyleFloat IStyle.borderLeftWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderLeftWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftWidth, value, this.ve.sharedStyle.borderLeftWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderLeftWidth = this.ve.computedStyle.borderLeftWidth.value;
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00030510 File Offset: 0x0002E710
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00030530 File Offset: 0x0002E730
		StyleColor IStyle.borderRightColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderRightColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightColor, value, this.ve.sharedStyle.borderRightColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00030574 File Offset: 0x0002E774
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00030594 File Offset: 0x0002E794
		StyleFloat IStyle.borderRightWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderRightWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightWidth, value, this.ve.sharedStyle.borderRightWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderRightWidth = this.ve.computedStyle.borderRightWidth.value;
				}
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00030600 File Offset: 0x0002E800
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00030620 File Offset: 0x0002E820
		StyleColor IStyle.borderTopColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderTopColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopColor, value, this.ve.sharedStyle.borderTopColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00030664 File Offset: 0x0002E864
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00030684 File Offset: 0x0002E884
		StyleLength IStyle.borderTopLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopLeftRadius, value, this.ve.sharedStyle.borderTopLeftRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000306C8 File Offset: 0x0002E8C8
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000306E8 File Offset: 0x0002E8E8
		StyleLength IStyle.borderTopRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopRightRadius, value, this.ve.sharedStyle.borderTopRightRadius);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0003072C File Offset: 0x0002E92C
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0003074C File Offset: 0x0002E94C
		StyleFloat IStyle.borderTopWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderTopWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopWidth, value, this.ve.sharedStyle.borderTopWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderTopWidth = this.ve.computedStyle.borderTopWidth.value;
				}
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000307B8 File Offset: 0x0002E9B8
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x000307D8 File Offset: 0x0002E9D8
		StyleLength IStyle.bottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Bottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Bottom, value, this.ve.sharedStyle.bottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Bottom = this.ve.computedStyle.bottom.ToYogaValue();
				}
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0003083C File Offset: 0x0002EA3C
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00030858 File Offset: 0x0002EA58
		StyleColor IStyle.color
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.Color);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Color, value, this.ve.sharedStyle.color);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00030898 File Offset: 0x0002EA98
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x000308CC File Offset: 0x0002EACC
		StyleEnum<DisplayStyle> IStyle.display
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Display);
				return new StyleEnum<DisplayStyle>((DisplayStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<DisplayStyle> styleEnum = new StyleEnum<DisplayStyle>(value.value, value.keyword);
				bool flag = this.SetStyleValue<DisplayStyle>(StylePropertyId.Display, styleEnum, this.ve.sharedStyle.display);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Display = (YogaDisplay)this.ve.computedStyle.display.value;
				}
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00030948 File Offset: 0x0002EB48
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00030968 File Offset: 0x0002EB68
		StyleLength IStyle.flexBasis
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FlexBasis);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexBasis, value, this.ve.sharedStyle.flexBasis);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexBasis = this.ve.computedStyle.flexBasis.ToYogaValue();
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000309CC File Offset: 0x0002EBCC
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00030A00 File Offset: 0x0002EC00
		StyleEnum<FlexDirection> IStyle.flexDirection
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexDirection);
				return new StyleEnum<FlexDirection>((FlexDirection)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<FlexDirection> styleEnum = new StyleEnum<FlexDirection>(value.value, value.keyword);
				bool flag = this.SetStyleValue<FlexDirection>(StylePropertyId.FlexDirection, styleEnum, this.ve.sharedStyle.flexDirection);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexDirection = (YogaFlexDirection)this.ve.computedStyle.flexDirection.value;
				}
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00030A7C File Offset: 0x0002EC7C
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00030A9C File Offset: 0x0002EC9C
		StyleFloat IStyle.flexGrow
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexGrow);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexGrow, value, this.ve.sharedStyle.flexGrow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexGrow = this.ve.computedStyle.flexGrow.value;
				}
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00030B04 File Offset: 0x0002ED04
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00030B24 File Offset: 0x0002ED24
		StyleFloat IStyle.flexShrink
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexShrink);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexShrink, value, this.ve.sharedStyle.flexShrink);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexShrink = this.ve.computedStyle.flexShrink.value;
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00030B8C File Offset: 0x0002ED8C
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		StyleEnum<Wrap> IStyle.flexWrap
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexWrap);
				return new StyleEnum<Wrap>((Wrap)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Wrap> styleEnum = new StyleEnum<Wrap>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Wrap>(StylePropertyId.FlexWrap, styleEnum, this.ve.sharedStyle.flexWrap);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Wrap = (YogaWrap)this.ve.computedStyle.flexWrap.value;
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00030C3C File Offset: 0x0002EE3C
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00030C58 File Offset: 0x0002EE58
		StyleLength IStyle.fontSize
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FontSize);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FontSize, value, this.ve.sharedStyle.fontSize);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00030C94 File Offset: 0x0002EE94
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00030CB4 File Offset: 0x0002EEB4
		StyleLength IStyle.height
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Height);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Height, value, this.ve.sharedStyle.height);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Height = this.ve.computedStyle.height.ToYogaValue();
				}
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00030D18 File Offset: 0x0002EF18
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00030D4C File Offset: 0x0002EF4C
		StyleEnum<Justify> IStyle.justifyContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.JustifyContent);
				return new StyleEnum<Justify>((Justify)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Justify> styleEnum = new StyleEnum<Justify>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Justify>(StylePropertyId.JustifyContent, styleEnum, this.ve.sharedStyle.justifyContent);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.JustifyContent = (YogaJustify)this.ve.computedStyle.justifyContent.value;
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00030DE8 File Offset: 0x0002EFE8
		StyleLength IStyle.left
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Left);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Left, value, this.ve.sharedStyle.left);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Left = this.ve.computedStyle.left.ToYogaValue();
				}
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00030E4C File Offset: 0x0002F04C
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00030E6C File Offset: 0x0002F06C
		StyleLength IStyle.marginBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginBottom, value, this.ve.sharedStyle.marginBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginBottom = this.ve.computedStyle.marginBottom.ToYogaValue();
				}
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00030ED0 File Offset: 0x0002F0D0
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		StyleLength IStyle.marginLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginLeft, value, this.ve.sharedStyle.marginLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginLeft = this.ve.computedStyle.marginLeft.ToYogaValue();
				}
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00030F54 File Offset: 0x0002F154
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00030F74 File Offset: 0x0002F174
		StyleLength IStyle.marginRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginRight, value, this.ve.sharedStyle.marginRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginRight = this.ve.computedStyle.marginRight.ToYogaValue();
				}
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00030FD8 File Offset: 0x0002F1D8
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		StyleLength IStyle.marginTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginTop, value, this.ve.sharedStyle.marginTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginTop = this.ve.computedStyle.marginTop.ToYogaValue();
				}
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003105C File Offset: 0x0002F25C
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0003107C File Offset: 0x0002F27C
		StyleLength IStyle.maxHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxHeight, value, this.ve.sharedStyle.maxHeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxHeight = this.ve.computedStyle.maxHeight.ToYogaValue();
				}
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000310E0 File Offset: 0x0002F2E0
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00031100 File Offset: 0x0002F300
		StyleLength IStyle.maxWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxWidth, value, this.ve.sharedStyle.maxWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxWidth = this.ve.computedStyle.maxWidth.ToYogaValue();
				}
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00031164 File Offset: 0x0002F364
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00031184 File Offset: 0x0002F384
		StyleLength IStyle.minHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinHeight, value, this.ve.sharedStyle.minHeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinHeight = this.ve.computedStyle.minHeight.ToYogaValue();
				}
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000311E8 File Offset: 0x0002F3E8
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00031208 File Offset: 0x0002F408
		StyleLength IStyle.minWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinWidth, value, this.ve.sharedStyle.minWidth);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinWidth = this.ve.computedStyle.minWidth.ToYogaValue();
				}
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0003126C File Offset: 0x0002F46C
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0003128C File Offset: 0x0002F48C
		StyleFloat IStyle.opacity
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.Opacity);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Opacity, value, this.ve.sharedStyle.opacity);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Opacity);
				}
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000312D0 File Offset: 0x0002F4D0
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00031304 File Offset: 0x0002F504
		StyleEnum<Overflow> IStyle.overflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Overflow);
				return new StyleEnum<Overflow>((Overflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowInternal> styleEnum = new StyleEnum<OverflowInternal>((OverflowInternal)value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowInternal>(StylePropertyId.Overflow, styleEnum, this.ve.sharedStyle.overflow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Overflow);
					this.ve.yogaNode.Overflow = (YogaOverflow)this.ve.computedStyle.overflow.value;
				}
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00031380 File Offset: 0x0002F580
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x000313A0 File Offset: 0x0002F5A0
		StyleLength IStyle.paddingBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingBottom, value, this.ve.sharedStyle.paddingBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingBottom = this.ve.computedStyle.paddingBottom.ToYogaValue();
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00031404 File Offset: 0x0002F604
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00031424 File Offset: 0x0002F624
		StyleLength IStyle.paddingLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingLeft, value, this.ve.sharedStyle.paddingLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingLeft = this.ve.computedStyle.paddingLeft.ToYogaValue();
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00031488 File Offset: 0x0002F688
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x000314A8 File Offset: 0x0002F6A8
		StyleLength IStyle.paddingRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingRight, value, this.ve.sharedStyle.paddingRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingRight = this.ve.computedStyle.paddingRight.ToYogaValue();
				}
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003150C File Offset: 0x0002F70C
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0003152C File Offset: 0x0002F72C
		StyleLength IStyle.paddingTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingTop, value, this.ve.sharedStyle.paddingTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingTop = this.ve.computedStyle.paddingTop.ToYogaValue();
				}
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00031590 File Offset: 0x0002F790
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x000315C4 File Offset: 0x0002F7C4
		StyleEnum<Position> IStyle.position
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Position);
				return new StyleEnum<Position>((Position)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Position> styleEnum = new StyleEnum<Position>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Position>(StylePropertyId.Position, styleEnum, this.ve.sharedStyle.position);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PositionType = (YogaPositionType)this.ve.computedStyle.position.value;
				}
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00031640 File Offset: 0x0002F840
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00031660 File Offset: 0x0002F860
		StyleLength IStyle.right
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Right);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Right, value, this.ve.sharedStyle.right);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Right = this.ve.computedStyle.right.ToYogaValue();
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x000316C4 File Offset: 0x0002F8C4
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x000316F8 File Offset: 0x0002F8F8
		StyleEnum<TextOverflow> IStyle.textOverflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.TextOverflow);
				return new StyleEnum<TextOverflow>((TextOverflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextOverflow> styleEnum = new StyleEnum<TextOverflow>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextOverflow>(StylePropertyId.TextOverflow, styleEnum, this.ve.sharedStyle.textOverflow);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00031750 File Offset: 0x0002F950
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00031770 File Offset: 0x0002F970
		StyleLength IStyle.top
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Top);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Top, value, this.ve.sharedStyle.top);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Top = this.ve.computedStyle.top.ToYogaValue();
				}
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000317D4 File Offset: 0x0002F9D4
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x000317F4 File Offset: 0x0002F9F4
		StyleColor IStyle.unityBackgroundImageTintColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityBackgroundImageTintColor, value, this.ve.sharedStyle.unityBackgroundImageTintColor);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00031838 File Offset: 0x0002FA38
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0003186C File Offset: 0x0002FA6C
		StyleEnum<ScaleMode> IStyle.unityBackgroundScaleMode
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityBackgroundScaleMode);
				return new StyleEnum<ScaleMode>((ScaleMode)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<ScaleMode> styleEnum = new StyleEnum<ScaleMode>(value.value, value.keyword);
				bool flag = this.SetStyleValue<ScaleMode>(StylePropertyId.UnityBackgroundScaleMode, styleEnum, this.ve.sharedStyle.unityBackgroundScaleMode);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000318C4 File Offset: 0x0002FAC4
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x000318E0 File Offset: 0x0002FAE0
		StyleFont IStyle.unityFont
		{
			get
			{
				return base.GetStyleFont(StylePropertyId.UnityFont);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFont, value, this.ve.sharedStyle.unityFont);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00031920 File Offset: 0x0002FB20
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00031950 File Offset: 0x0002FB50
		StyleEnum<FontStyle> IStyle.unityFontStyleAndWeight
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityFontStyleAndWeight);
				return new StyleEnum<FontStyle>((FontStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<FontStyle> styleEnum = new StyleEnum<FontStyle>(value.value, value.keyword);
				bool flag = this.SetStyleValue<FontStyle>(StylePropertyId.UnityFontStyleAndWeight, styleEnum, this.ve.sharedStyle.unityFontStyleAndWeight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x000319A4 File Offset: 0x0002FBA4
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x000319D8 File Offset: 0x0002FBD8
		StyleEnum<OverflowClipBox> IStyle.unityOverflowClipBox
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityOverflowClipBox);
				return new StyleEnum<OverflowClipBox>((OverflowClipBox)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowClipBox> styleEnum = new StyleEnum<OverflowClipBox>(value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowClipBox>(StylePropertyId.UnityOverflowClipBox, styleEnum, this.ve.sharedStyle.unityOverflowClipBox);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00031A30 File Offset: 0x0002FC30
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00031A50 File Offset: 0x0002FC50
		StyleInt IStyle.unitySliceBottom
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceBottom, value, this.ve.sharedStyle.unitySliceBottom);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00031A94 File Offset: 0x0002FC94
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00031AB4 File Offset: 0x0002FCB4
		StyleInt IStyle.unitySliceLeft
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceLeft, value, this.ve.sharedStyle.unitySliceLeft);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00031AF8 File Offset: 0x0002FCF8
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00031B18 File Offset: 0x0002FD18
		StyleInt IStyle.unitySliceRight
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceRight, value, this.ve.sharedStyle.unitySliceRight);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00031B5C File Offset: 0x0002FD5C
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00031B7C File Offset: 0x0002FD7C
		StyleInt IStyle.unitySliceTop
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceTop, value, this.ve.sharedStyle.unitySliceTop);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00031BC0 File Offset: 0x0002FDC0
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		StyleEnum<TextAnchor> IStyle.unityTextAlign
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextAlign);
				return new StyleEnum<TextAnchor>((TextAnchor)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextAnchor> styleEnum = new StyleEnum<TextAnchor>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextAnchor>(StylePropertyId.UnityTextAlign, styleEnum, this.ve.sharedStyle.unityTextAlign);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00031C44 File Offset: 0x0002FE44
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00031C78 File Offset: 0x0002FE78
		StyleEnum<TextOverflowPosition> IStyle.unityTextOverflowPosition
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextOverflowPosition);
				return new StyleEnum<TextOverflowPosition>((TextOverflowPosition)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<TextOverflowPosition> styleEnum = new StyleEnum<TextOverflowPosition>(value.value, value.keyword);
				bool flag = this.SetStyleValue<TextOverflowPosition>(StylePropertyId.UnityTextOverflowPosition, styleEnum, this.ve.sharedStyle.unityTextOverflowPosition);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00031CD0 File Offset: 0x0002FED0
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00031D00 File Offset: 0x0002FF00
		StyleEnum<Visibility> IStyle.visibility
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Visibility);
				return new StyleEnum<Visibility>((Visibility)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<Visibility> styleEnum = new StyleEnum<Visibility>(value.value, value.keyword);
				bool flag = this.SetStyleValue<Visibility>(StylePropertyId.Visibility, styleEnum, this.ve.sharedStyle.visibility);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00031D54 File Offset: 0x0002FF54
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00031D84 File Offset: 0x0002FF84
		StyleEnum<WhiteSpace> IStyle.whiteSpace
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.WhiteSpace);
				return new StyleEnum<WhiteSpace>((WhiteSpace)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<WhiteSpace> styleEnum = new StyleEnum<WhiteSpace>(value.value, value.keyword);
				bool flag = this.SetStyleValue<WhiteSpace>(StylePropertyId.WhiteSpace, styleEnum, this.ve.sharedStyle.whiteSpace);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00031DD4 File Offset: 0x0002FFD4
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00031DF4 File Offset: 0x0002FFF4
		StyleLength IStyle.width
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Width);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Width, value, this.ve.sharedStyle.width);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Width = this.ve.computedStyle.width.ToYogaValue();
				}
			}
		}

		// Token: 0x04000524 RID: 1316
		private static StylePropertyReader s_StylePropertyReader = new StylePropertyReader();

		// Token: 0x04000526 RID: 1318
		private bool m_HasInlineCursor;

		// Token: 0x04000527 RID: 1319
		private StyleCursor m_InlineCursor;

		// Token: 0x04000528 RID: 1320
		private InlineStyleAccess.InlineRule m_InlineRule;

		// Token: 0x020001AB RID: 427
		internal struct InlineRule
		{
			// Token: 0x04000529 RID: 1321
			public StyleSheet sheet;

			// Token: 0x0400052A RID: 1322
			public StyleProperty[] properties;

			// Token: 0x0400052B RID: 1323
			public StylePropertyId[] propertyIds;
		}
	}
}
