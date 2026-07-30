using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00036174 File Offset: 0x00034374
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0003618C File Offset: 0x0003438C
		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000361A0 File Offset: 0x000343A0
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x000361B8 File Offset: 0x000343B8
		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x000361CC File Offset: 0x000343CC
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x000361E4 File Offset: 0x000343E4
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000361F0 File Offset: 0x000343F0
		private static bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool flag = false;
			value = default(T);
			bool flag2 = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag2)
			{
				value = list[handle.valueIndex];
				flag = true;
			}
			else
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			return flag;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00036274 File Offset: 0x00034474
		private static T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T t = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property");
				}
				else
				{
					t = list[handle.valueIndex];
				}
			}
			return t;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003630C File Offset: 0x0003450C
		private void OnEnable()
		{
			this.SetupReferences();
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00036318 File Offset: 0x00034518
		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				foreach (StyleRule styleRule in this.rules)
				{
					foreach (StyleProperty styleProperty in styleRule.properties)
					{
						bool flag2 = StyleSheet.CustomStartsWith(styleProperty.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							styleRule.customPropertiesCount++;
							styleProperty.isCustomProperty = true;
						}
						foreach (StyleValueHandle styleValueHandle in styleProperty.values)
						{
							bool flag3 = styleValueHandle.IsVarFunction();
							if (flag3)
							{
								styleProperty.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int l = 0;
				int num = this.complexSelectors.Length;
				while (l < num)
				{
					this.complexSelectors[l].CachePseudoStateMasks();
					l++;
				}
				this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				int m = 0;
				while (m < this.complexSelectors.Length)
				{
					StyleComplexSelector styleComplexSelector = this.complexSelectors[m];
					bool flag4 = styleComplexSelector.ruleIndex < this.rules.Length;
					if (flag4)
					{
						styleComplexSelector.rule = this.rules[styleComplexSelector.ruleIndex];
					}
					styleComplexSelector.orderInStyleSheet = m;
					StyleSelector styleSelector = styleComplexSelector.selectors[styleComplexSelector.selectors.Length - 1];
					StyleSelectorPart styleSelectorPart = styleSelector.parts[0];
					string text = styleSelectorPart.value;
					Dictionary<string, StyleComplexSelector> dictionary = null;
					switch (styleSelectorPart.type)
					{
					case StyleSelectorType.Wildcard:
					case StyleSelectorType.Type:
						text = styleSelectorPart.value ?? "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.Class:
						dictionary = this.orderedClassSelectors;
						break;
					case StyleSelectorType.PseudoClass:
						text = "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_0227;
					case StyleSelectorType.ID:
						dictionary = this.orderedNameSelectors;
						break;
					default:
						goto IL_0227;
					}
					IL_0245:
					bool flag5 = dictionary != null;
					if (flag5)
					{
						StyleComplexSelector styleComplexSelector2;
						bool flag6 = dictionary.TryGetValue(text, ref styleComplexSelector2);
						if (flag6)
						{
							styleComplexSelector.nextInTable = styleComplexSelector2;
						}
						dictionary[text] = styleComplexSelector;
					}
					m++;
					continue;
					IL_0227:
					Debug.LogError(string.Format("Invalid first part type {0}", styleSelectorPart.type));
					goto IL_0245;
				}
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000365BC File Offset: 0x000347BC
		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000365D4 File Offset: 0x000347D4
		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float num;
			if (flag)
			{
				Dimension dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				num = dimension.value;
			}
			else
			{
				num = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return num;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003661C File Offset: 0x0003481C
		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Dimension dimension;
				bool flag3 = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
				value = dimension.value;
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00036660 File Offset: 0x00034860
		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension dimension;
			if (flag)
			{
				float num = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				dimension = new Dimension(num, Dimension.Unit.Unitless);
			}
			else
			{
				dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return dimension;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000366A8 File Offset: 0x000348A8
		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = 0f;
				bool flag3 = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out num);
				value = new Dimension(num, Dimension.Unit.Unitless);
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000366F4 File Offset: 0x000348F4
		internal Color ReadColor(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00036714 File Offset: 0x00034914
		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return StyleSheet.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00036734 File Offset: 0x00034934
		internal string ReadString(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00036754 File Offset: 0x00034954
		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00036778 File Offset: 0x00034978
		internal string ReadEnum(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00036798 File Offset: 0x00034998
		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000367B8 File Offset: 0x000349B8
		internal string ReadVariable(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000367D8 File Offset: 0x000349D8
		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000367F8 File Offset: 0x000349F8
		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00036818 File Offset: 0x00034A18
		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00036838 File Offset: 0x00034A38
		internal Object ReadAssetReference(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00036858 File Offset: 0x00034A58
		internal bool TryReadAssetReference(StyleValueHandle handle, out Object value)
		{
			return StyleSheet.TryCheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00036878 File Offset: 0x00034A78
		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00036890 File Offset: 0x00034A90
		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string text;
			if (flag)
			{
				Debug.LogErrorFormat(string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), new object[0]);
				text = string.Empty;
			}
			else
			{
				StyleValueFunction valueIndex = (StyleValueFunction)handle.valueIndex;
				text = valueIndex.ToUssString();
			}
			return text;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000368F4 File Offset: 0x00034AF4
		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00036914 File Offset: 0x00034B14
		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int length = originalString.Length;
			int length2 = pattern.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && originalString.get_Chars(num) == pattern.get_Chars(num2))
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}

		// Token: 0x040005B2 RID: 1458
		[SerializeField]
		private StyleRule[] m_Rules;

		// Token: 0x040005B3 RID: 1459
		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		// Token: 0x040005B4 RID: 1460
		[SerializeField]
		internal float[] floats;

		// Token: 0x040005B5 RID: 1461
		[SerializeField]
		internal Dimension[] dimensions;

		// Token: 0x040005B6 RID: 1462
		[SerializeField]
		internal Color[] colors;

		// Token: 0x040005B7 RID: 1463
		[SerializeField]
		internal string[] strings;

		// Token: 0x040005B8 RID: 1464
		[SerializeField]
		internal Object[] assets;

		// Token: 0x040005B9 RID: 1465
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x040005BA RID: 1466
		[SerializeField]
		internal ScalableImage[] scalableImages;

		// Token: 0x040005BB RID: 1467
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;

		// Token: 0x040005BC RID: 1468
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;

		// Token: 0x040005BD RID: 1469
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;

		// Token: 0x040005BE RID: 1470
		[NonSerialized]
		internal bool isUnityStyleSheet;

		// Token: 0x040005BF RID: 1471
		private static string kCustomPropertyMarker = "--";
	}
}
