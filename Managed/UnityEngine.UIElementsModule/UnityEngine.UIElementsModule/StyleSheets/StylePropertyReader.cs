using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000264 RID: 612
	internal class StylePropertyReader
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0004FFA8 File Offset: 0x0004E1A8
		public StyleProperty property { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004FFB1 File Offset: 0x0004E1B1
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0004FFB9 File Offset: 0x0004E1B9
		public StylePropertyId propertyId { get; private set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004FFC2 File Offset: 0x0004E1C2
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0004FFCA File Offset: 0x0004E1CA
		public int valueCount { get; private set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004FFD3 File Offset: 0x0004E1D3
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0004FFDB File Offset: 0x0004E1DB
		public float dpiScaling { get; private set; }

		// Token: 0x06001221 RID: 4641 RVA: 0x0004FFE4 File Offset: 0x0004E1E4
		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00050039 File Offset: 0x0004E239
		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00050064 File Offset: 0x0004E264
		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000500A4 File Offset: 0x0004E2A4
		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000500CC File Offset: 0x0004E2CC
		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00050100 File Offset: 0x0004E300
		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00050138 File Offset: 0x0004E338
		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.handle.valueType == StyleValueType.Keyword && stylePropertyValue.handle.valueIndex == (int)keyword;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00050180 File Offset: 0x0004E380
		public string ReadAsString(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000501B8 File Offset: 0x0004E3B8
		public StyleLength ReadStyleLength(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			StyleLength styleLength;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
				styleLength = new StyleLength(valueIndex.ToStyleKeyword());
			}
			else
			{
				styleLength = new StyleLength(stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToLength());
			}
			return styleLength;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0005022C File Offset: 0x0004E42C
		public StyleFloat ReadStyleFloat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return new StyleFloat(stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle));
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00050268 File Offset: 0x0004E468
		public StyleInt ReadStyleInt(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return new StyleInt((int)stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle));
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000502A8 File Offset: 0x0004E4A8
		public StyleColor ReadStyleColor(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			Color color = Color.clear;
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				StyleSheetColor.TryGetColor(text.ToLower(), out color);
			}
			else
			{
				color = stylePropertyValue.sheet.ReadColor(stylePropertyValue.handle);
			}
			return new StyleColor(color);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00050328 File Offset: 0x0004E528
		public StyleInt ReadStyleEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = stylePropertyValue.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = stylePropertyValue.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = stylePropertyValue.sheet.ReadEnum(handle);
			}
			int enumIntValue = StylePropertyUtil.GetEnumIntValue(enumType, text);
			return new StyleInt(enumIntValue);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000503A0 File Offset: 0x0004E5A0
		public StyleFont ReadStyleFont(int index)
		{
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			if (valueType != StyleValueType.ResourcePath)
			{
				if (valueType != StyleValueType.AssetReference)
				{
					Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType);
				}
				else
				{
					font = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font;
					bool flag = font == null;
					if (flag)
					{
						Debug.LogWarning("Invalid font reference");
					}
				}
			}
			else
			{
				string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					font = Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font;
				}
				bool flag3 = font == null;
				if (flag3)
				{
					Debug.LogWarning(string.Format("Font not found for path: {0}", text));
				}
			}
			return new StyleFont(font);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000504A8 File Offset: 0x0004E6A8
		public StyleBackground ReadStyleBackground(int index)
		{
			ImageSource imageSource = default(ImageSource);
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = stylePropertyValue.handle.valueIndex != 6;
				if (flag2)
				{
					Debug.LogWarning("Invalid keyword for image source " + (StyleValueKeyword)stylePropertyValue.handle.valueIndex);
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource);
				if (flag3)
				{
				}
			}
			bool flag4 = imageSource.texture != null;
			StyleBackground styleBackground;
			if (flag4)
			{
				styleBackground = new StyleBackground(imageSource.texture);
			}
			else
			{
				bool flag5 = imageSource.vectorImage != null;
				if (flag5)
				{
					styleBackground = new StyleBackground(imageSource.vectorImage);
				}
				else
				{
					styleBackground = default(StyleBackground);
				}
			}
			return styleBackground;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00050590 File Offset: 0x0004E790
		public StyleCursor ReadStyleCursor(int index)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			Texture2D texture2D = null;
			StyleValueType valueType = this.GetValueType(index);
			bool flag = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.valueCount < 1;
				if (flag3)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource imageSource = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag4 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out imageSource);
					if (flag4)
					{
						texture2D = imageSource.texture;
						bool flag5 = this.valueCount >= 3;
						if (flag5)
						{
							StylePropertyValue value2 = this.GetValue(index + 1);
							StylePropertyValue value3 = this.GetValue(index + 2);
							bool flag6 = value2.handle.valueType != StyleValueType.Float || value3.handle.valueType != StyleValueType.Float;
							if (flag6)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								num = value2.sheet.ReadFloat(value2.handle);
								num2 = value3.sheet.ReadFloat(value3.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = StylePropertyReader.getCursorIdFunc != null;
				if (flag7)
				{
					StylePropertyValue value4 = this.GetValue(index);
					num3 = StylePropertyReader.getCursorIdFunc(value4.sheet, value4.handle);
				}
			}
			Cursor cursor = new Cursor
			{
				texture = texture2D,
				hotspot = new Vector2(num, num2),
				defaultCursorId = num3
			};
			return new StyleCursor(cursor);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00050738 File Offset: 0x0004E938
		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			foreach (StyleProperty styleProperty in this.m_Properties)
			{
				int num = 0;
				bool flag = true;
				bool requireVariableResolve = styleProperty.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(styleProperty, this.m_Sheet, styleProperty.values);
					int num2 = 0;
					while (num2 < styleProperty.values.Length && flag)
					{
						StyleValueHandle styleValueHandle = styleProperty.values[num2];
						bool flag2 = styleValueHandle.IsVarFunction();
						if (flag2)
						{
							StyleVariableResolver.Result result = this.m_Resolver.ResolveVarFunction(ref num2);
							bool flag3 = result > StyleVariableResolver.Result.Valid;
							if (flag3)
							{
								StyleValueHandle styleValueHandle2 = new StyleValueHandle
								{
									valueType = StyleValueType.Keyword,
									valueIndex = 3
								};
								this.m_Values.Add(new StylePropertyValue
								{
									sheet = this.m_Sheet,
									handle = styleValueHandle2
								});
								num++;
								flag = false;
							}
						}
						else
						{
							this.m_Resolver.AddValue(styleValueHandle);
						}
						num2++;
					}
					bool flag4 = flag;
					if (flag4)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						num += this.m_Resolver.resolvedValues.Count;
					}
				}
				else
				{
					num = styleProperty.values.Length;
					for (int j = 0; j < num; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleProperty.values[j]
						});
					}
				}
				this.m_ValueCount.Add(num);
			}
			this.SetCurrentProperty();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00050928 File Offset: 0x0004EB28
		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000509A8 File Offset: 0x0004EBA8
		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			if (valueType != StyleValueType.ResourcePath)
			{
				if (valueType != StyleValueType.AssetReference)
				{
					if (valueType != StyleValueType.ScalableImage)
					{
						Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType);
						return false;
					}
					ScalableImage scalableImage = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
					bool flag = scalableImage.normalImage == null && scalableImage.highResolutionImage == null;
					if (flag)
					{
						Debug.LogWarning("Invalid scalable image specified");
						return false;
					}
					source.texture = scalableImage.normalImage;
					bool flag2 = !Mathf.Approximately(dpiScaling % 1f, 0f);
					if (flag2)
					{
						source.texture.filterMode = FilterMode.Bilinear;
					}
				}
				else
				{
					Object @object = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
					source.texture = @object as Texture2D;
					source.vectorImage = @object as VectorImage;
					bool flag3 = source.texture == null && source.vectorImage == null;
					if (flag3)
					{
						Debug.LogWarning("Invalid image specified");
						return false;
					}
				}
			}
			else
			{
				string text = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
				bool flag4 = !string.IsNullOrEmpty(text);
				if (flag4)
				{
					source.texture = Panel.LoadResource(text, typeof(Texture2D), dpiScaling) as Texture2D;
					bool flag5 = source.texture == null;
					if (flag5)
					{
						source.vectorImage = Panel.LoadResource(text, typeof(VectorImage), dpiScaling) as VectorImage;
					}
				}
				bool flag6 = source.texture == null && source.vectorImage == null;
				if (flag6)
				{
					Debug.LogWarning(string.Format("Image not found for path: {0}", text));
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000902 RID: 2306
		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc = null;

		// Token: 0x04000903 RID: 2307
		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		// Token: 0x04000904 RID: 2308
		private List<int> m_ValueCount = new List<int>();

		// Token: 0x04000905 RID: 2309
		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		// Token: 0x04000906 RID: 2310
		private StyleSheet m_Sheet;

		// Token: 0x04000907 RID: 2311
		private StyleProperty[] m_Properties;

		// Token: 0x04000908 RID: 2312
		private StylePropertyId[] m_PropertyIds;

		// Token: 0x04000909 RID: 2313
		private int m_CurrentValueIndex;

		// Token: 0x0400090A RID: 2314
		private int m_CurrentPropertyIndex;

		// Token: 0x02000265 RID: 613
		// (Invoke) Token: 0x06001237 RID: 4663
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);
	}
}
