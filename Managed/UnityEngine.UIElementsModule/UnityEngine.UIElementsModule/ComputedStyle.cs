using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A6 RID: 422
	internal class ComputedStyle : ICustomStyle
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002C82C File Offset: 0x0002AA2C
		public int customPropertiesCount
		{
			get
			{
				return (this.m_CustomProperties != null) ? this.m_CustomProperties.Count : 0;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002C854 File Offset: 0x0002AA54
		public static ComputedStyle Create(bool isShared = true)
		{
			ComputedStyle computedStyle = new ComputedStyle(isShared);
			computedStyle.CopyFrom(InitialStyle.Get());
			return computedStyle;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002C87C File Offset: 0x0002AA7C
		public static ComputedStyle Create(ComputedStyle parentStyle, bool isShared = true)
		{
			ComputedStyle computedStyle = ComputedStyle.Create(isShared);
			bool flag = parentStyle != null;
			if (flag)
			{
				computedStyle.inheritedData = parentStyle.inheritedData;
			}
			return computedStyle;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		public static ComputedStyle CreateUninitialized(bool isShared = true)
		{
			return new ComputedStyle(isShared);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002C8C4 File Offset: 0x0002AAC4
		private ComputedStyle(bool isShared)
		{
			this.isShared = isShared;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		public void CopyShared(ComputedStyle sharedStyle)
		{
			this.m_CustomProperties = sharedStyle.m_CustomProperties;
			this.CopyFrom(sharedStyle);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002C910 File Offset: 0x0002AB10
		public void FinalizeApply(ComputedStyle parentStyle)
		{
			bool flag = this.yogaNode == null;
			if (flag)
			{
				this.yogaNode = new YogaNode(null);
			}
			bool flag2 = parentStyle != null;
			if (flag2)
			{
				bool flag3 = this.fontSize.value.unit == LengthUnit.Percent;
				if (flag3)
				{
					float value = parentStyle.fontSize.value.value;
					float num = value * this.fontSize.value.value / 100f;
					this.inheritedData.fontSize = new Length(num);
				}
			}
			this.SyncWithLayout(this.yogaNode);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
		public void SyncWithLayout(YogaNode targetNode)
		{
			targetNode.Flex = float.NaN;
			targetNode.FlexGrow = this.flexGrow.value;
			targetNode.FlexShrink = this.flexShrink.value;
			targetNode.FlexBasis = this.flexBasis.ToYogaValue();
			targetNode.Left = this.left.ToYogaValue();
			targetNode.Top = this.top.ToYogaValue();
			targetNode.Right = this.right.ToYogaValue();
			targetNode.Bottom = this.bottom.ToYogaValue();
			targetNode.MarginLeft = this.marginLeft.ToYogaValue();
			targetNode.MarginTop = this.marginTop.ToYogaValue();
			targetNode.MarginRight = this.marginRight.ToYogaValue();
			targetNode.MarginBottom = this.marginBottom.ToYogaValue();
			targetNode.PaddingLeft = this.paddingLeft.ToYogaValue();
			targetNode.PaddingTop = this.paddingTop.ToYogaValue();
			targetNode.PaddingRight = this.paddingRight.ToYogaValue();
			targetNode.PaddingBottom = this.paddingBottom.ToYogaValue();
			targetNode.BorderLeftWidth = this.borderLeftWidth.value;
			targetNode.BorderTopWidth = this.borderTopWidth.value;
			targetNode.BorderRightWidth = this.borderRightWidth.value;
			targetNode.BorderBottomWidth = this.borderBottomWidth.value;
			targetNode.Width = this.width.ToYogaValue();
			targetNode.Height = this.height.ToYogaValue();
			targetNode.PositionType = (YogaPositionType)this.position.value;
			targetNode.Overflow = (YogaOverflow)this.overflow.value;
			targetNode.AlignSelf = (YogaAlign)this.alignSelf.value;
			targetNode.MaxWidth = this.maxWidth.ToYogaValue();
			targetNode.MaxHeight = this.maxHeight.ToYogaValue();
			targetNode.MinWidth = this.minWidth.ToYogaValue();
			targetNode.MinHeight = this.minHeight.ToYogaValue();
			targetNode.FlexDirection = (YogaFlexDirection)this.flexDirection.value;
			targetNode.AlignContent = (YogaAlign)this.alignContent.value;
			targetNode.AlignItems = (YogaAlign)this.alignItems.value;
			targetNode.JustifyContent = (YogaJustify)this.justifyContent.value;
			targetNode.Wrap = (YogaWrap)this.flexWrap.value;
			targetNode.Display = (YogaDisplay)this.display.value;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002CC70 File Offset: 0x0002AE70
		private bool ApplyGlobalKeyword(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			StyleValueHandle handle = reader.GetValue(0).handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = handle.valueIndex == 1;
				if (flag2)
				{
					this.ApplyInitialValue(reader);
					return true;
				}
				bool flag3 = handle.valueIndex == 3;
				if (flag3)
				{
					this.ApplyUnsetValue(reader, parentStyle);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		private bool ApplyGlobalKeyword(StyleValue sv, ComputedStyle parentStyle)
		{
			bool flag = sv.keyword == StyleKeyword.Initial;
			bool flag2;
			if (flag)
			{
				this.ApplyInitialValue(sv.id);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		private void RemoveCustomStyleProperty(StylePropertyReader reader)
		{
			string name = reader.property.name;
			bool flag = this.m_CustomProperties == null || !this.m_CustomProperties.ContainsKey(name);
			if (!flag)
			{
				this.m_CustomProperties.Remove(name);
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002CD54 File Offset: 0x0002AF54
		private void ApplyCustomStyleProperty(StylePropertyReader reader)
		{
			this.dpiScaling = reader.dpiScaling;
			bool flag = this.m_CustomProperties == null;
			if (flag)
			{
				this.m_CustomProperties = new Dictionary<string, StylePropertyValue>();
			}
			StyleProperty property = reader.property;
			StylePropertyValue value = reader.GetValue(0);
			this.m_CustomProperties[property.name] = value;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		public bool TryGetValue(CustomStyleProperty<float> property, out float value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
			if (flag)
			{
				bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out value);
				if (flag2)
				{
					return true;
				}
			}
			value = 0f;
			return false;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002CDF8 File Offset: 0x0002AFF8
		public bool TryGetValue(CustomStyleProperty<int> property, out int value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
			if (flag)
			{
				float num = 0f;
				bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out num);
				if (flag2)
				{
					value = (int)num;
					return true;
				}
			}
			value = 0;
			return false;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002CE50 File Offset: 0x0002B050
		public bool TryGetValue(CustomStyleProperty<bool> property, out bool value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
			bool flag2;
			if (flag)
			{
				value = stylePropertyValue.sheet.ReadKeyword(stylePropertyValue.handle) == StyleValueKeyword.True;
				flag2 = true;
			}
			else
			{
				value = false;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
		public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.TryGetValue(property.name, StyleValueType.Color, out stylePropertyValue);
			if (flag)
			{
				bool flag2 = stylePropertyValue.sheet.TryReadColor(stylePropertyValue.handle, out value);
				if (flag2)
				{
					return true;
				}
			}
			value = Color.clear;
			return false;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
			if (flag)
			{
				ImageSource imageSource = default(ImageSource);
				bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource) && imageSource.texture != null;
				if (flag2)
				{
					value = imageSource.texture;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002CF6C File Offset: 0x0002B16C
		public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
			if (flag)
			{
				ImageSource imageSource = default(ImageSource);
				bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource) && imageSource.vectorImage != null;
				if (flag2)
				{
					value = imageSource.vectorImage;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
		public bool TryGetValue(CustomStyleProperty<string> property, out string value)
		{
			StylePropertyValue stylePropertyValue;
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
			bool flag2;
			if (flag)
			{
				value = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				flag2 = true;
			}
			else
			{
				value = string.Empty;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002D03C File Offset: 0x0002B23C
		private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
		{
			customProp = default(StylePropertyValue);
			bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, ref customProp);
			bool flag3;
			if (flag)
			{
				StyleValueHandle handle = customProp.handle;
				bool flag2 = handle.valueType != valueType;
				if (flag2)
				{
					Debug.LogWarning(string.Format("Trying to read value as {0} while parsed type is {1}", valueType, handle.valueType));
					flag3 = false;
				}
				else
				{
					flag3 = true;
				}
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002D0B6 File Offset: 0x0002B2B6
		public StyleEnum<Align> alignContent
		{
			get
			{
				return this.nonInheritedData.alignContent;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002D0C3 File Offset: 0x0002B2C3
		public StyleEnum<Align> alignItems
		{
			get
			{
				return this.nonInheritedData.alignItems;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002D0D0 File Offset: 0x0002B2D0
		public StyleEnum<Align> alignSelf
		{
			get
			{
				return this.nonInheritedData.alignSelf;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002D0DD File Offset: 0x0002B2DD
		public StyleColor backgroundColor
		{
			get
			{
				return this.nonInheritedData.backgroundColor;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002D0EA File Offset: 0x0002B2EA
		public StyleBackground backgroundImage
		{
			get
			{
				return this.nonInheritedData.backgroundImage;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002D0F7 File Offset: 0x0002B2F7
		public StyleColor borderBottomColor
		{
			get
			{
				return this.nonInheritedData.borderBottomColor;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002D104 File Offset: 0x0002B304
		public StyleLength borderBottomLeftRadius
		{
			get
			{
				return this.nonInheritedData.borderBottomLeftRadius;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002D111 File Offset: 0x0002B311
		public StyleLength borderBottomRightRadius
		{
			get
			{
				return this.nonInheritedData.borderBottomRightRadius;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002D11E File Offset: 0x0002B31E
		public StyleFloat borderBottomWidth
		{
			get
			{
				return this.nonInheritedData.borderBottomWidth;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002D12B File Offset: 0x0002B32B
		public StyleColor borderLeftColor
		{
			get
			{
				return this.nonInheritedData.borderLeftColor;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0002D138 File Offset: 0x0002B338
		public StyleFloat borderLeftWidth
		{
			get
			{
				return this.nonInheritedData.borderLeftWidth;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002D145 File Offset: 0x0002B345
		public StyleColor borderRightColor
		{
			get
			{
				return this.nonInheritedData.borderRightColor;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002D152 File Offset: 0x0002B352
		public StyleFloat borderRightWidth
		{
			get
			{
				return this.nonInheritedData.borderRightWidth;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002D15F File Offset: 0x0002B35F
		public StyleColor borderTopColor
		{
			get
			{
				return this.nonInheritedData.borderTopColor;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002D16C File Offset: 0x0002B36C
		public StyleLength borderTopLeftRadius
		{
			get
			{
				return this.nonInheritedData.borderTopLeftRadius;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002D179 File Offset: 0x0002B379
		public StyleLength borderTopRightRadius
		{
			get
			{
				return this.nonInheritedData.borderTopRightRadius;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002D186 File Offset: 0x0002B386
		public StyleFloat borderTopWidth
		{
			get
			{
				return this.nonInheritedData.borderTopWidth;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002D193 File Offset: 0x0002B393
		public StyleLength bottom
		{
			get
			{
				return this.nonInheritedData.bottom;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
		public StyleColor color
		{
			get
			{
				return this.inheritedData.color;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002D1AD File Offset: 0x0002B3AD
		public StyleCursor cursor
		{
			get
			{
				return this.nonInheritedData.cursor;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002D1BA File Offset: 0x0002B3BA
		public StyleEnum<DisplayStyle> display
		{
			get
			{
				return this.nonInheritedData.display;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002D1C7 File Offset: 0x0002B3C7
		public StyleLength flexBasis
		{
			get
			{
				return this.nonInheritedData.flexBasis;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public StyleEnum<FlexDirection> flexDirection
		{
			get
			{
				return this.nonInheritedData.flexDirection;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002D1E1 File Offset: 0x0002B3E1
		public StyleFloat flexGrow
		{
			get
			{
				return this.nonInheritedData.flexGrow;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0002D1EE File Offset: 0x0002B3EE
		public StyleFloat flexShrink
		{
			get
			{
				return this.nonInheritedData.flexShrink;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002D1FB File Offset: 0x0002B3FB
		public StyleEnum<Wrap> flexWrap
		{
			get
			{
				return this.nonInheritedData.flexWrap;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0002D208 File Offset: 0x0002B408
		public StyleLength fontSize
		{
			get
			{
				return this.inheritedData.fontSize;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002D215 File Offset: 0x0002B415
		public StyleLength height
		{
			get
			{
				return this.nonInheritedData.height;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002D222 File Offset: 0x0002B422
		public StyleEnum<Justify> justifyContent
		{
			get
			{
				return this.nonInheritedData.justifyContent;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002D22F File Offset: 0x0002B42F
		public StyleLength left
		{
			get
			{
				return this.nonInheritedData.left;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002D23C File Offset: 0x0002B43C
		public StyleLength marginBottom
		{
			get
			{
				return this.nonInheritedData.marginBottom;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002D249 File Offset: 0x0002B449
		public StyleLength marginLeft
		{
			get
			{
				return this.nonInheritedData.marginLeft;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002D256 File Offset: 0x0002B456
		public StyleLength marginRight
		{
			get
			{
				return this.nonInheritedData.marginRight;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002D263 File Offset: 0x0002B463
		public StyleLength marginTop
		{
			get
			{
				return this.nonInheritedData.marginTop;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002D270 File Offset: 0x0002B470
		public StyleLength maxHeight
		{
			get
			{
				return this.nonInheritedData.maxHeight;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002D27D File Offset: 0x0002B47D
		public StyleLength maxWidth
		{
			get
			{
				return this.nonInheritedData.maxWidth;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002D28A File Offset: 0x0002B48A
		public StyleLength minHeight
		{
			get
			{
				return this.nonInheritedData.minHeight;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002D297 File Offset: 0x0002B497
		public StyleLength minWidth
		{
			get
			{
				return this.nonInheritedData.minWidth;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
		public StyleFloat opacity
		{
			get
			{
				return this.nonInheritedData.opacity;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002D2B1 File Offset: 0x0002B4B1
		public StyleEnum<OverflowInternal> overflow
		{
			get
			{
				return this.nonInheritedData.overflow;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002D2BE File Offset: 0x0002B4BE
		public StyleLength paddingBottom
		{
			get
			{
				return this.nonInheritedData.paddingBottom;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002D2CB File Offset: 0x0002B4CB
		public StyleLength paddingLeft
		{
			get
			{
				return this.nonInheritedData.paddingLeft;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
		public StyleLength paddingRight
		{
			get
			{
				return this.nonInheritedData.paddingRight;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002D2E5 File Offset: 0x0002B4E5
		public StyleLength paddingTop
		{
			get
			{
				return this.nonInheritedData.paddingTop;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002D2F2 File Offset: 0x0002B4F2
		public StyleEnum<Position> position
		{
			get
			{
				return this.nonInheritedData.position;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002D2FF File Offset: 0x0002B4FF
		public StyleLength right
		{
			get
			{
				return this.nonInheritedData.right;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002D30C File Offset: 0x0002B50C
		public StyleEnum<TextOverflow> textOverflow
		{
			get
			{
				return this.nonInheritedData.textOverflow;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002D319 File Offset: 0x0002B519
		public StyleLength top
		{
			get
			{
				return this.nonInheritedData.top;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002D326 File Offset: 0x0002B526
		public StyleColor unityBackgroundImageTintColor
		{
			get
			{
				return this.nonInheritedData.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002D333 File Offset: 0x0002B533
		public StyleEnum<ScaleMode> unityBackgroundScaleMode
		{
			get
			{
				return this.nonInheritedData.unityBackgroundScaleMode;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002D340 File Offset: 0x0002B540
		public StyleFont unityFont
		{
			get
			{
				return this.inheritedData.unityFont;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0002D34D File Offset: 0x0002B54D
		public StyleEnum<FontStyle> unityFontStyleAndWeight
		{
			get
			{
				return this.inheritedData.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002D35A File Offset: 0x0002B55A
		public StyleEnum<OverflowClipBox> unityOverflowClipBox
		{
			get
			{
				return this.nonInheritedData.unityOverflowClipBox;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002D367 File Offset: 0x0002B567
		public StyleInt unitySliceBottom
		{
			get
			{
				return this.nonInheritedData.unitySliceBottom;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002D374 File Offset: 0x0002B574
		public StyleInt unitySliceLeft
		{
			get
			{
				return this.nonInheritedData.unitySliceLeft;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002D381 File Offset: 0x0002B581
		public StyleInt unitySliceRight
		{
			get
			{
				return this.nonInheritedData.unitySliceRight;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0002D38E File Offset: 0x0002B58E
		public StyleInt unitySliceTop
		{
			get
			{
				return this.nonInheritedData.unitySliceTop;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002D39B File Offset: 0x0002B59B
		public StyleEnum<TextAnchor> unityTextAlign
		{
			get
			{
				return this.inheritedData.unityTextAlign;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		public StyleEnum<TextOverflowPosition> unityTextOverflowPosition
		{
			get
			{
				return this.nonInheritedData.unityTextOverflowPosition;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002D3B5 File Offset: 0x0002B5B5
		public StyleEnum<Visibility> visibility
		{
			get
			{
				return this.inheritedData.visibility;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002D3C2 File Offset: 0x0002B5C2
		public StyleEnum<WhiteSpace> whiteSpace
		{
			get
			{
				return this.inheritedData.whiteSpace;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002D3CF File Offset: 0x0002B5CF
		public StyleLength width
		{
			get
			{
				return this.nonInheritedData.width;
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D3DC File Offset: 0x0002B5DC
		public void CopyFrom(ComputedStyle other)
		{
			this.inheritedData = other.inheritedData;
			this.nonInheritedData = other.nonInheritedData;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002D3F8 File Offset: 0x0002B5F8
		public void ApplyProperties(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			for (StylePropertyId stylePropertyId = reader.propertyId; stylePropertyId != StylePropertyId.Unknown; stylePropertyId = reader.MoveNextProperty())
			{
				bool flag = this.ApplyGlobalKeyword(reader, parentStyle);
				if (!flag)
				{
					StylePropertyId stylePropertyId2 = stylePropertyId;
					switch (stylePropertyId2)
					{
					case StylePropertyId.Custom:
						this.ApplyCustomStyleProperty(reader);
						break;
					case StylePropertyId.Unknown:
						goto IL_0836;
					case StylePropertyId.Color:
						this.inheritedData.color = reader.ReadStyleColor(0);
						break;
					case StylePropertyId.FontSize:
						this.inheritedData.fontSize = reader.ReadStyleLength(0);
						break;
					case StylePropertyId.UnityFont:
						this.inheritedData.unityFont = reader.ReadStyleFont(0);
						break;
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.unityFontStyleAndWeight = (FontStyle)reader.ReadStyleEnum(StyleEnumType.FontStyle, 0).value;
						break;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.unityTextAlign = (TextAnchor)reader.ReadStyleEnum(StyleEnumType.TextAnchor, 0).value;
						break;
					case StylePropertyId.Visibility:
						this.inheritedData.visibility = (Visibility)reader.ReadStyleEnum(StyleEnumType.Visibility, 0).value;
						break;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.whiteSpace = (WhiteSpace)reader.ReadStyleEnum(StyleEnumType.WhiteSpace, 0).value;
						break;
					default:
						switch (stylePropertyId2)
						{
						case StylePropertyId.AlignContent:
							this.nonInheritedData.alignContent = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.AlignItems:
							this.nonInheritedData.alignItems = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.AlignSelf:
							this.nonInheritedData.alignSelf = (Align)reader.ReadStyleEnum(StyleEnumType.Align, 0).value;
							break;
						case StylePropertyId.BackgroundColor:
							this.nonInheritedData.backgroundColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BackgroundImage:
							this.nonInheritedData.backgroundImage = reader.ReadStyleBackground(0);
							break;
						case StylePropertyId.BorderBottomColor:
							this.nonInheritedData.borderBottomColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderBottomLeftRadius:
							this.nonInheritedData.borderBottomLeftRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderBottomRightRadius:
							this.nonInheritedData.borderBottomRightRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderBottomWidth:
							this.nonInheritedData.borderBottomWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderLeftColor:
							this.nonInheritedData.borderLeftColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderLeftWidth:
							this.nonInheritedData.borderLeftWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderRightColor:
							this.nonInheritedData.borderRightColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderRightWidth:
							this.nonInheritedData.borderRightWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.BorderTopColor:
							this.nonInheritedData.borderTopColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.BorderTopLeftRadius:
							this.nonInheritedData.borderTopLeftRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderTopRightRadius:
							this.nonInheritedData.borderTopRightRadius = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.BorderTopWidth:
							this.nonInheritedData.borderTopWidth = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.Bottom:
							this.nonInheritedData.bottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Cursor:
							this.nonInheritedData.cursor = reader.ReadStyleCursor(0);
							break;
						case StylePropertyId.Display:
							this.nonInheritedData.display = (DisplayStyle)reader.ReadStyleEnum(StyleEnumType.DisplayStyle, 0).value;
							break;
						case StylePropertyId.FlexBasis:
							this.nonInheritedData.flexBasis = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.FlexDirection:
							this.nonInheritedData.flexDirection = (FlexDirection)reader.ReadStyleEnum(StyleEnumType.FlexDirection, 0).value;
							break;
						case StylePropertyId.FlexGrow:
							this.nonInheritedData.flexGrow = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.FlexShrink:
							this.nonInheritedData.flexShrink = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.FlexWrap:
							this.nonInheritedData.flexWrap = (Wrap)reader.ReadStyleEnum(StyleEnumType.Wrap, 0).value;
							break;
						case StylePropertyId.Height:
							this.nonInheritedData.height = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.JustifyContent:
							this.nonInheritedData.justifyContent = (Justify)reader.ReadStyleEnum(StyleEnumType.Justify, 0).value;
							break;
						case StylePropertyId.Left:
							this.nonInheritedData.left = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginBottom:
							this.nonInheritedData.marginBottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginLeft:
							this.nonInheritedData.marginLeft = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginRight:
							this.nonInheritedData.marginRight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MarginTop:
							this.nonInheritedData.marginTop = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MaxHeight:
							this.nonInheritedData.maxHeight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MaxWidth:
							this.nonInheritedData.maxWidth = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MinHeight:
							this.nonInheritedData.minHeight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.MinWidth:
							this.nonInheritedData.minWidth = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Opacity:
							this.nonInheritedData.opacity = reader.ReadStyleFloat(0);
							break;
						case StylePropertyId.Overflow:
							this.nonInheritedData.overflow = (OverflowInternal)reader.ReadStyleEnum(StyleEnumType.OverflowInternal, 0).value;
							break;
						case StylePropertyId.PaddingBottom:
							this.nonInheritedData.paddingBottom = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingLeft:
							this.nonInheritedData.paddingLeft = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingRight:
							this.nonInheritedData.paddingRight = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.PaddingTop:
							this.nonInheritedData.paddingTop = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.Position:
							this.nonInheritedData.position = (Position)reader.ReadStyleEnum(StyleEnumType.Position, 0).value;
							break;
						case StylePropertyId.Right:
							this.nonInheritedData.right = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.TextOverflow:
							this.nonInheritedData.textOverflow = (TextOverflow)reader.ReadStyleEnum(StyleEnumType.TextOverflow, 0).value;
							break;
						case StylePropertyId.Top:
							this.nonInheritedData.top = reader.ReadStyleLength(0);
							break;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.nonInheritedData.unityBackgroundImageTintColor = reader.ReadStyleColor(0);
							break;
						case StylePropertyId.UnityBackgroundScaleMode:
							this.nonInheritedData.unityBackgroundScaleMode = (ScaleMode)reader.ReadStyleEnum(StyleEnumType.ScaleMode, 0).value;
							break;
						case StylePropertyId.UnityOverflowClipBox:
							this.nonInheritedData.unityOverflowClipBox = (OverflowClipBox)reader.ReadStyleEnum(StyleEnumType.OverflowClipBox, 0).value;
							break;
						case StylePropertyId.UnitySliceBottom:
							this.nonInheritedData.unitySliceBottom = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceLeft:
							this.nonInheritedData.unitySliceLeft = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceRight:
							this.nonInheritedData.unitySliceRight = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnitySliceTop:
							this.nonInheritedData.unitySliceTop = reader.ReadStyleInt(0);
							break;
						case StylePropertyId.UnityTextOverflowPosition:
							this.nonInheritedData.unityTextOverflowPosition = (TextOverflowPosition)reader.ReadStyleEnum(StyleEnumType.TextOverflowPosition, 0).value;
							break;
						case StylePropertyId.Width:
							this.nonInheritedData.width = reader.ReadStyleLength(0);
							break;
						default:
							switch (stylePropertyId2)
							{
							case StylePropertyId.BorderColor:
								ShorthandApplicator.ApplyBorderColor(reader, this);
								break;
							case StylePropertyId.BorderRadius:
								ShorthandApplicator.ApplyBorderRadius(reader, this);
								break;
							case StylePropertyId.BorderWidth:
								ShorthandApplicator.ApplyBorderWidth(reader, this);
								break;
							case StylePropertyId.Flex:
								ShorthandApplicator.ApplyFlex(reader, this);
								break;
							case StylePropertyId.Margin:
								ShorthandApplicator.ApplyMargin(reader, this);
								break;
							case StylePropertyId.Padding:
								ShorthandApplicator.ApplyPadding(reader, this);
								break;
							default:
								goto IL_0836;
							}
							break;
						}
						break;
					}
					goto IL_084F;
					IL_0836:
					Debug.LogAssertion(string.Format("Unknown property id {0}", stylePropertyId));
				}
				IL_084F:;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		public void ApplyStyleValue(StyleValue sv, ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv, parentStyle);
			if (!flag)
			{
				StylePropertyId id = sv.id;
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.color = new StyleColor(sv.color, sv.keyword);
					break;
				case StylePropertyId.FontSize:
					this.inheritedData.fontSize = new StyleLength(sv.length, sv.keyword);
					break;
				case StylePropertyId.UnityFont:
					this.inheritedData.unityFont = new StyleFont(sv.resource, sv.keyword);
					break;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)sv.number, sv.keyword);
					break;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)sv.number, sv.keyword);
					break;
				case StylePropertyId.Visibility:
					this.inheritedData.visibility = new StyleEnum<Visibility>((Visibility)sv.number, sv.keyword);
					break;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)sv.number, sv.keyword);
					break;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
					{
						this.nonInheritedData.alignContent = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag2 = sv.keyword == StyleKeyword.Auto;
						if (flag2)
						{
							this.nonInheritedData.alignContent = Align.Auto;
						}
						return;
					}
					case StylePropertyId.AlignItems:
					{
						this.nonInheritedData.alignItems = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag3 = sv.keyword == StyleKeyword.Auto;
						if (flag3)
						{
							this.nonInheritedData.alignItems = Align.Auto;
						}
						return;
					}
					case StylePropertyId.AlignSelf:
					{
						this.nonInheritedData.alignSelf = new StyleEnum<Align>((Align)sv.number, sv.keyword);
						bool flag4 = sv.keyword == StyleKeyword.Auto;
						if (flag4)
						{
							this.nonInheritedData.alignSelf = Align.Auto;
						}
						return;
					}
					case StylePropertyId.BackgroundColor:
						this.nonInheritedData.backgroundColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BackgroundImage:
						this.nonInheritedData.backgroundImage = new StyleBackground(sv.resource, sv.keyword);
						return;
					case StylePropertyId.BorderBottomColor:
						this.nonInheritedData.borderBottomColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderBottomLeftRadius:
						this.nonInheritedData.borderBottomLeftRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.nonInheritedData.borderBottomRightRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderBottomWidth:
						this.nonInheritedData.borderBottomWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderLeftColor:
						this.nonInheritedData.borderLeftColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderLeftWidth:
						this.nonInheritedData.borderLeftWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderRightColor:
						this.nonInheritedData.borderRightColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderRightWidth:
						this.nonInheritedData.borderRightWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.BorderTopColor:
						this.nonInheritedData.borderTopColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.nonInheritedData.borderTopLeftRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.nonInheritedData.borderTopRightRadius = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.BorderTopWidth:
						this.nonInheritedData.borderTopWidth = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.Bottom:
						this.nonInheritedData.bottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Display:
					{
						this.nonInheritedData.display = new StyleEnum<DisplayStyle>((DisplayStyle)sv.number, sv.keyword);
						bool flag5 = sv.keyword == StyleKeyword.None;
						if (flag5)
						{
							this.nonInheritedData.display = DisplayStyle.None;
						}
						return;
					}
					case StylePropertyId.FlexBasis:
						this.nonInheritedData.flexBasis = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.FlexDirection:
						this.nonInheritedData.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexGrow:
						this.nonInheritedData.flexGrow = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexShrink:
						this.nonInheritedData.flexShrink = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.FlexWrap:
						this.nonInheritedData.flexWrap = new StyleEnum<Wrap>((Wrap)sv.number, sv.keyword);
						return;
					case StylePropertyId.Height:
						this.nonInheritedData.height = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.JustifyContent:
						this.nonInheritedData.justifyContent = new StyleEnum<Justify>((Justify)sv.number, sv.keyword);
						return;
					case StylePropertyId.Left:
						this.nonInheritedData.left = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginBottom:
						this.nonInheritedData.marginBottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginLeft:
						this.nonInheritedData.marginLeft = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginRight:
						this.nonInheritedData.marginRight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MarginTop:
						this.nonInheritedData.marginTop = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MaxHeight:
						this.nonInheritedData.maxHeight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MaxWidth:
						this.nonInheritedData.maxWidth = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MinHeight:
						this.nonInheritedData.minHeight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.MinWidth:
						this.nonInheritedData.minWidth = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Opacity:
						this.nonInheritedData.opacity = new StyleFloat(sv.number, sv.keyword);
						return;
					case StylePropertyId.Overflow:
						this.nonInheritedData.overflow = new StyleEnum<OverflowInternal>((OverflowInternal)sv.number, sv.keyword);
						return;
					case StylePropertyId.PaddingBottom:
						this.nonInheritedData.paddingBottom = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingLeft:
						this.nonInheritedData.paddingLeft = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingRight:
						this.nonInheritedData.paddingRight = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.PaddingTop:
						this.nonInheritedData.paddingTop = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.Position:
						this.nonInheritedData.position = new StyleEnum<Position>((Position)sv.number, sv.keyword);
						return;
					case StylePropertyId.Right:
						this.nonInheritedData.right = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.TextOverflow:
						this.nonInheritedData.textOverflow = new StyleEnum<TextOverflow>((TextOverflow)sv.number, sv.keyword);
						return;
					case StylePropertyId.Top:
						this.nonInheritedData.top = new StyleLength(sv.length, sv.keyword);
						return;
					case StylePropertyId.UnityBackgroundImageTintColor:
						this.nonInheritedData.unityBackgroundImageTintColor = new StyleColor(sv.color, sv.keyword);
						return;
					case StylePropertyId.UnityBackgroundScaleMode:
						this.nonInheritedData.unityBackgroundScaleMode = new StyleEnum<ScaleMode>((ScaleMode)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnityOverflowClipBox:
						this.nonInheritedData.unityOverflowClipBox = new StyleEnum<OverflowClipBox>((OverflowClipBox)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceBottom:
						this.nonInheritedData.unitySliceBottom = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceLeft:
						this.nonInheritedData.unitySliceLeft = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceRight:
						this.nonInheritedData.unitySliceRight = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnitySliceTop:
						this.nonInheritedData.unitySliceTop = new StyleInt((int)sv.number, sv.keyword);
						return;
					case StylePropertyId.UnityTextOverflowPosition:
						this.nonInheritedData.unityTextOverflowPosition = new StyleEnum<TextOverflowPosition>((TextOverflowPosition)sv.number, sv.keyword);
						return;
					case StylePropertyId.Width:
						this.nonInheritedData.width = new StyleLength(sv.length, sv.keyword);
						return;
					}
					Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
					break;
				}
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002E622 File Offset: 0x0002C822
		public void ApplyStyleCursor(StyleCursor sc)
		{
			this.nonInheritedData.cursor = sc;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002E634 File Offset: 0x0002C834
		public void ApplyInitialValue(StylePropertyReader reader)
		{
			StylePropertyId propertyId = reader.propertyId;
			if (propertyId != StylePropertyId.Custom)
			{
				this.ApplyInitialValue(reader.propertyId);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002E66C File Offset: 0x0002C86C
		public void ApplyInitialValue(StylePropertyId id)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.color = InitialStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.fontSize = InitialStyle.fontSize;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.unityFont = InitialStyle.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.unityFontStyleAndWeight = InitialStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.unityTextAlign = InitialStyle.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.visibility = InitialStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.whiteSpace = InitialStyle.whiteSpace;
				break;
			default:
				switch (id)
				{
				case StylePropertyId.AlignContent:
					this.nonInheritedData.alignContent = InitialStyle.alignContent;
					break;
				case StylePropertyId.AlignItems:
					this.nonInheritedData.alignItems = InitialStyle.alignItems;
					break;
				case StylePropertyId.AlignSelf:
					this.nonInheritedData.alignSelf = InitialStyle.alignSelf;
					break;
				case StylePropertyId.BackgroundColor:
					this.nonInheritedData.backgroundColor = InitialStyle.backgroundColor;
					break;
				case StylePropertyId.BackgroundImage:
					this.nonInheritedData.backgroundImage = InitialStyle.backgroundImage;
					break;
				case StylePropertyId.BorderBottomColor:
					this.nonInheritedData.borderBottomColor = InitialStyle.borderBottomColor;
					break;
				case StylePropertyId.BorderBottomLeftRadius:
					this.nonInheritedData.borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
					break;
				case StylePropertyId.BorderBottomRightRadius:
					this.nonInheritedData.borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
					break;
				case StylePropertyId.BorderBottomWidth:
					this.nonInheritedData.borderBottomWidth = InitialStyle.borderBottomWidth;
					break;
				case StylePropertyId.BorderLeftColor:
					this.nonInheritedData.borderLeftColor = InitialStyle.borderLeftColor;
					break;
				case StylePropertyId.BorderLeftWidth:
					this.nonInheritedData.borderLeftWidth = InitialStyle.borderLeftWidth;
					break;
				case StylePropertyId.BorderRightColor:
					this.nonInheritedData.borderRightColor = InitialStyle.borderRightColor;
					break;
				case StylePropertyId.BorderRightWidth:
					this.nonInheritedData.borderRightWidth = InitialStyle.borderRightWidth;
					break;
				case StylePropertyId.BorderTopColor:
					this.nonInheritedData.borderTopColor = InitialStyle.borderTopColor;
					break;
				case StylePropertyId.BorderTopLeftRadius:
					this.nonInheritedData.borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
					break;
				case StylePropertyId.BorderTopRightRadius:
					this.nonInheritedData.borderTopRightRadius = InitialStyle.borderTopRightRadius;
					break;
				case StylePropertyId.BorderTopWidth:
					this.nonInheritedData.borderTopWidth = InitialStyle.borderTopWidth;
					break;
				case StylePropertyId.Bottom:
					this.nonInheritedData.bottom = InitialStyle.bottom;
					break;
				case StylePropertyId.Cursor:
					this.nonInheritedData.cursor = InitialStyle.cursor;
					break;
				case StylePropertyId.Display:
					this.nonInheritedData.display = InitialStyle.display;
					break;
				case StylePropertyId.FlexBasis:
					this.nonInheritedData.flexBasis = InitialStyle.flexBasis;
					break;
				case StylePropertyId.FlexDirection:
					this.nonInheritedData.flexDirection = InitialStyle.flexDirection;
					break;
				case StylePropertyId.FlexGrow:
					this.nonInheritedData.flexGrow = InitialStyle.flexGrow;
					break;
				case StylePropertyId.FlexShrink:
					this.nonInheritedData.flexShrink = InitialStyle.flexShrink;
					break;
				case StylePropertyId.FlexWrap:
					this.nonInheritedData.flexWrap = InitialStyle.flexWrap;
					break;
				case StylePropertyId.Height:
					this.nonInheritedData.height = InitialStyle.height;
					break;
				case StylePropertyId.JustifyContent:
					this.nonInheritedData.justifyContent = InitialStyle.justifyContent;
					break;
				case StylePropertyId.Left:
					this.nonInheritedData.left = InitialStyle.left;
					break;
				case StylePropertyId.MarginBottom:
					this.nonInheritedData.marginBottom = InitialStyle.marginBottom;
					break;
				case StylePropertyId.MarginLeft:
					this.nonInheritedData.marginLeft = InitialStyle.marginLeft;
					break;
				case StylePropertyId.MarginRight:
					this.nonInheritedData.marginRight = InitialStyle.marginRight;
					break;
				case StylePropertyId.MarginTop:
					this.nonInheritedData.marginTop = InitialStyle.marginTop;
					break;
				case StylePropertyId.MaxHeight:
					this.nonInheritedData.maxHeight = InitialStyle.maxHeight;
					break;
				case StylePropertyId.MaxWidth:
					this.nonInheritedData.maxWidth = InitialStyle.maxWidth;
					break;
				case StylePropertyId.MinHeight:
					this.nonInheritedData.minHeight = InitialStyle.minHeight;
					break;
				case StylePropertyId.MinWidth:
					this.nonInheritedData.minWidth = InitialStyle.minWidth;
					break;
				case StylePropertyId.Opacity:
					this.nonInheritedData.opacity = InitialStyle.opacity;
					break;
				case StylePropertyId.Overflow:
					this.nonInheritedData.overflow = InitialStyle.overflow;
					break;
				case StylePropertyId.PaddingBottom:
					this.nonInheritedData.paddingBottom = InitialStyle.paddingBottom;
					break;
				case StylePropertyId.PaddingLeft:
					this.nonInheritedData.paddingLeft = InitialStyle.paddingLeft;
					break;
				case StylePropertyId.PaddingRight:
					this.nonInheritedData.paddingRight = InitialStyle.paddingRight;
					break;
				case StylePropertyId.PaddingTop:
					this.nonInheritedData.paddingTop = InitialStyle.paddingTop;
					break;
				case StylePropertyId.Position:
					this.nonInheritedData.position = InitialStyle.position;
					break;
				case StylePropertyId.Right:
					this.nonInheritedData.right = InitialStyle.right;
					break;
				case StylePropertyId.TextOverflow:
					this.nonInheritedData.textOverflow = InitialStyle.textOverflow;
					break;
				case StylePropertyId.Top:
					this.nonInheritedData.top = InitialStyle.top;
					break;
				case StylePropertyId.UnityBackgroundImageTintColor:
					this.nonInheritedData.unityBackgroundImageTintColor = InitialStyle.unityBackgroundImageTintColor;
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					this.nonInheritedData.unityBackgroundScaleMode = InitialStyle.unityBackgroundScaleMode;
					break;
				case StylePropertyId.UnityOverflowClipBox:
					this.nonInheritedData.unityOverflowClipBox = InitialStyle.unityOverflowClipBox;
					break;
				case StylePropertyId.UnitySliceBottom:
					this.nonInheritedData.unitySliceBottom = InitialStyle.unitySliceBottom;
					break;
				case StylePropertyId.UnitySliceLeft:
					this.nonInheritedData.unitySliceLeft = InitialStyle.unitySliceLeft;
					break;
				case StylePropertyId.UnitySliceRight:
					this.nonInheritedData.unitySliceRight = InitialStyle.unitySliceRight;
					break;
				case StylePropertyId.UnitySliceTop:
					this.nonInheritedData.unitySliceTop = InitialStyle.unitySliceTop;
					break;
				case StylePropertyId.UnityTextOverflowPosition:
					this.nonInheritedData.unityTextOverflowPosition = InitialStyle.unityTextOverflowPosition;
					break;
				case StylePropertyId.Width:
					this.nonInheritedData.width = InitialStyle.width;
					break;
				default:
					switch (id)
					{
					case StylePropertyId.BorderColor:
						this.nonInheritedData.borderTopColor = InitialStyle.borderTopColor;
						this.nonInheritedData.borderRightColor = InitialStyle.borderRightColor;
						this.nonInheritedData.borderBottomColor = InitialStyle.borderBottomColor;
						this.nonInheritedData.borderLeftColor = InitialStyle.borderLeftColor;
						break;
					case StylePropertyId.BorderRadius:
						this.nonInheritedData.borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
						this.nonInheritedData.borderTopRightRadius = InitialStyle.borderTopRightRadius;
						this.nonInheritedData.borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
						this.nonInheritedData.borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
						break;
					case StylePropertyId.BorderWidth:
						this.nonInheritedData.borderTopWidth = InitialStyle.borderTopWidth;
						this.nonInheritedData.borderRightWidth = InitialStyle.borderRightWidth;
						this.nonInheritedData.borderBottomWidth = InitialStyle.borderBottomWidth;
						this.nonInheritedData.borderLeftWidth = InitialStyle.borderLeftWidth;
						break;
					case StylePropertyId.Flex:
						this.nonInheritedData.flexGrow = InitialStyle.flexGrow;
						this.nonInheritedData.flexShrink = InitialStyle.flexShrink;
						this.nonInheritedData.flexBasis = InitialStyle.flexBasis;
						break;
					case StylePropertyId.Margin:
						this.nonInheritedData.marginTop = InitialStyle.marginTop;
						this.nonInheritedData.marginRight = InitialStyle.marginRight;
						this.nonInheritedData.marginBottom = InitialStyle.marginBottom;
						this.nonInheritedData.marginLeft = InitialStyle.marginLeft;
						break;
					case StylePropertyId.Padding:
						this.nonInheritedData.paddingTop = InitialStyle.paddingTop;
						this.nonInheritedData.paddingRight = InitialStyle.paddingRight;
						this.nonInheritedData.paddingBottom = InitialStyle.paddingBottom;
						this.nonInheritedData.paddingLeft = InitialStyle.paddingLeft;
						break;
					default:
						Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
						break;
					}
					break;
				}
				break;
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002EE60 File Offset: 0x0002D060
		public void ApplyUnsetValue(StylePropertyReader reader, ComputedStyle parentStyle)
		{
			StylePropertyId propertyId = reader.propertyId;
			if (propertyId != StylePropertyId.Custom)
			{
				this.ApplyUnsetValue(reader.propertyId, parentStyle);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002EE98 File Offset: 0x0002D098
		public void ApplyUnsetValue(StylePropertyId id, ComputedStyle parentStyle)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.color = parentStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.fontSize = parentStyle.fontSize;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.unityFont = parentStyle.unityFont;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.unityFontStyleAndWeight = parentStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.unityTextAlign = parentStyle.unityTextAlign;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.visibility = parentStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.whiteSpace = parentStyle.whiteSpace;
				break;
			default:
				this.ApplyInitialValue(id);
				break;
			}
		}

		// Token: 0x0400051C RID: 1308
		internal readonly bool isShared;

		// Token: 0x0400051D RID: 1309
		internal YogaNode yogaNode;

		// Token: 0x0400051E RID: 1310
		internal Dictionary<string, StylePropertyValue> m_CustomProperties;

		// Token: 0x0400051F RID: 1311
		private float dpiScaling = 1f;

		// Token: 0x04000520 RID: 1312
		public InheritedData inheritedData = default(InheritedData);

		// Token: 0x04000521 RID: 1313
		public NonInheritedData nonInheritedData = default(NonInheritedData);
	}
}
