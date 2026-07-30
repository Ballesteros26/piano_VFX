using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class MaterialDebugSettings
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00009BCC File Offset: 0x00007DCC
		static MaterialDebugSettings()
		{
			MaterialDebugSettings.BuildDebugRepresentation();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009C44 File Offset: 0x00007E44
		private static void FillWithProperties(Type type, ref List<GUIContent> debugViewMaterialStringsList, ref List<int> debugViewMaterialValuesList, string className)
		{
			GenerateHLSL generateHLSL = type.GetCustomAttributes(true)[0] as GenerateHLSL;
			if (!generateHLSL.needParamDebug)
			{
				return;
			}
			FieldInfo[] fields = type.GetFields();
			int num = 0;
			foreach (FieldInfo fieldInfo in fields)
			{
				List<string> list = new List<string>();
				if (Attribute.IsDefined(fieldInfo, typeof(PackingAttribute)))
				{
					foreach (PackingAttribute packingAttribute in (PackingAttribute[])fieldInfo.GetCustomAttributes(typeof(PackingAttribute), false))
					{
						list.AddRange(packingAttribute.displayNames);
					}
				}
				else
				{
					list.Add(fieldInfo.Name);
				}
				if (Attribute.IsDefined(fieldInfo, typeof(SurfaceDataAttributes)))
				{
					SurfaceDataAttributes[] array3 = (SurfaceDataAttributes[])fieldInfo.GetCustomAttributes(typeof(SurfaceDataAttributes), false);
					if (array3[0].displayNames.Length != 0 && array3[0].displayNames[0] != "")
					{
						list.Clear();
						list.AddRange(array3[0].displayNames);
					}
				}
				foreach (string text in list)
				{
					debugViewMaterialStringsList.Add(new GUIContent(className + text));
					debugViewMaterialValuesList.Add(generateHLSL.paramDefinesStart + num);
					num++;
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009DC0 File Offset: 0x00007FC0
		private static void FillWithPropertiesEnum(Type type, ref List<GUIContent> debugViewMaterialStringsList, ref List<int> debugViewMaterialValuesList, string prefix)
		{
			string[] names = Enum.GetNames(type);
			int num = 0;
			foreach (object obj in Enum.GetValues(type))
			{
				string text = prefix + names[num];
				debugViewMaterialStringsList.Add(new GUIContent(text));
				debugViewMaterialValuesList.Add((int)obj);
				num++;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009E44 File Offset: 0x00008044
		private static List<MaterialDebugSettings.MaterialItem> GetAllMaterialDatas()
		{
			List<RenderPipelineMaterial> renderPipelineMaterialList = HDUtils.GetRenderPipelineMaterialList();
			foreach (RenderPipelineMaterial renderPipelineMaterial in renderPipelineMaterialList)
			{
				if (renderPipelineMaterial.IsDefferedMaterial())
				{
					renderPipelineMaterial.GetType().GetNestedType("BSDFData");
				}
			}
			List<MaterialDebugSettings.MaterialItem> list = new List<MaterialDebugSettings.MaterialItem>();
			int num = 0;
			int num2 = 0;
			foreach (RenderPipelineMaterial renderPipelineMaterial2 in renderPipelineMaterialList)
			{
				MaterialDebugSettings.MaterialItem materialItem = new MaterialDebugSettings.MaterialItem();
				materialItem.className = renderPipelineMaterial2.GetType().Name + "/";
				materialItem.surfaceDataType = renderPipelineMaterial2.GetType().GetNestedType("SurfaceData");
				num += materialItem.surfaceDataType.GetFields().Length;
				materialItem.bsdfDataType = renderPipelineMaterial2.GetType().GetNestedType("BSDFData");
				num2 += materialItem.bsdfDataType.GetFields().Length;
				list.Add(materialItem);
			}
			return list;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009F78 File Offset: 0x00008178
		private static void BuildDebugRepresentation()
		{
			if (!MaterialDebugSettings.isDebugViewMaterialInit)
			{
				List<MaterialDebugSettings.MaterialItem> allMaterialDatas = MaterialDebugSettings.GetAllMaterialDatas();
				List<GUIContent> list = new List<GUIContent>();
				List<int> list2 = new List<int>();
				List<GUIContent> list3 = new List<GUIContent>();
				List<int> list4 = new List<int>();
				List<GUIContent> list5 = new List<GUIContent>();
				List<int> list6 = new List<int>();
				List<GUIContent> list7 = new List<GUIContent>();
				List<int> list8 = new List<int>();
				List<GUIContent> list9 = new List<GUIContent>();
				List<int> list10 = new List<int>();
				List<GUIContent> list11 = new List<GUIContent>();
				List<int> list12 = new List<int>();
				list.Add(new GUIContent("None"));
				list2.Add(0);
				foreach (MaterialDebugSettings.MaterialItem materialItem in allMaterialDatas)
				{
					MaterialDebugSettings.FillWithProperties(typeof(Builtin.BuiltinData), ref list, ref list2, materialItem.className);
					MaterialDebugSettings.FillWithProperties(materialItem.surfaceDataType, ref list, ref list2, materialItem.className);
				}
				list3.Add(new GUIContent("None"));
				list4.Add(0);
				foreach (MaterialDebugSettings.MaterialItem materialItem2 in allMaterialDatas)
				{
					MaterialDebugSettings.FillWithProperties(materialItem2.bsdfDataType, ref list3, ref list4, materialItem2.className);
				}
				MaterialDebugSettings.FillWithPropertiesEnum(typeof(DebugViewVarying), ref list5, ref list6, "");
				MaterialDebugSettings.FillWithPropertiesEnum(typeof(DebugViewProperties), ref list7, ref list8, "");
				MaterialDebugSettings.FillWithPropertiesEnum(typeof(DebugViewGbuffer), ref list11, ref list12, "");
				MaterialDebugSettings.FillWithProperties(typeof(Lit.BSDFData), ref list11, ref list12, "");
				MaterialDebugSettings.debugViewMaterialStrings = list.ToArray();
				MaterialDebugSettings.debugViewMaterialValues = list2.ToArray();
				MaterialDebugSettings.debugViewEngineStrings = list3.ToArray();
				MaterialDebugSettings.debugViewEngineValues = list4.ToArray();
				MaterialDebugSettings.debugViewMaterialVaryingStrings = list5.ToArray();
				MaterialDebugSettings.debugViewMaterialVaryingValues = list6.ToArray();
				MaterialDebugSettings.debugViewMaterialPropertiesStrings = list7.ToArray();
				MaterialDebugSettings.debugViewMaterialPropertiesValues = list8.ToArray();
				MaterialDebugSettings.debugViewMaterialTextureStrings = list9.ToArray();
				MaterialDebugSettings.debugViewMaterialTextureValues = list10.ToArray();
				MaterialDebugSettings.debugViewMaterialGBufferStrings = list11.ToArray();
				MaterialDebugSettings.debugViewMaterialGBufferValues = list12.ToArray();
				Dictionary<MaterialSharedProperty, List<int>> dictionary = new Dictionary<MaterialSharedProperty, List<int>>
				{
					{
						MaterialSharedProperty.Albedo,
						new List<int>()
					},
					{
						MaterialSharedProperty.Normal,
						new List<int>()
					},
					{
						MaterialSharedProperty.Smoothness,
						new List<int>()
					},
					{
						MaterialSharedProperty.AmbientOcclusion,
						new List<int>()
					},
					{
						MaterialSharedProperty.Metal,
						new List<int>()
					},
					{
						MaterialSharedProperty.Specular,
						new List<int>()
					},
					{
						MaterialSharedProperty.Alpha,
						new List<int>()
					}
				};
				int num = (typeof(Builtin.BuiltinData).GetCustomAttributes(true)[0] as GenerateHLSL).paramDefinesStart;
				int num2 = 0;
				foreach (FieldInfo fieldInfo in typeof(Builtin.BuiltinData).GetFields())
				{
					if (Attribute.IsDefined(fieldInfo, typeof(MaterialSharedPropertyMappingAttribute)))
					{
						MaterialSharedPropertyMappingAttribute[] array2 = (MaterialSharedPropertyMappingAttribute[])fieldInfo.GetCustomAttributes(typeof(MaterialSharedPropertyMappingAttribute), false);
						dictionary[array2[0].property].Add(num + num2);
					}
					SurfaceDataAttributes[] array3 = (SurfaceDataAttributes[])fieldInfo.GetCustomAttributes(typeof(SurfaceDataAttributes), false);
					if (array3.Length != 0)
					{
						num2 += array3[0].displayNames.Length;
					}
				}
				foreach (MaterialDebugSettings.MaterialItem materialItem3 in allMaterialDatas)
				{
					GenerateHLSL generateHLSL = materialItem3.surfaceDataType.GetCustomAttributes(true)[0] as GenerateHLSL;
					num = generateHLSL.paramDefinesStart;
					if (generateHLSL.needParamDebug)
					{
						FieldInfo[] fields = materialItem3.surfaceDataType.GetFields();
						num2 = 0;
						foreach (FieldInfo fieldInfo2 in fields)
						{
							if (Attribute.IsDefined(fieldInfo2, typeof(MaterialSharedPropertyMappingAttribute)))
							{
								MaterialSharedPropertyMappingAttribute[] array4 = (MaterialSharedPropertyMappingAttribute[])fieldInfo2.GetCustomAttributes(typeof(MaterialSharedPropertyMappingAttribute), false);
								dictionary[array4[0].property].Add(num + num2);
							}
							SurfaceDataAttributes[] array5 = (SurfaceDataAttributes[])fieldInfo2.GetCustomAttributes(typeof(SurfaceDataAttributes), false);
							if (array5.Length != 0)
							{
								num2 += array5[0].displayNames.Length;
							}
						}
						if (!(materialItem3.bsdfDataType == null))
						{
							GenerateHLSL generateHLSL2 = materialItem3.bsdfDataType.GetCustomAttributes(true)[0] as GenerateHLSL;
							num = generateHLSL2.paramDefinesStart;
							if (generateHLSL2.needParamDebug)
							{
								FieldInfo[] fields2 = materialItem3.bsdfDataType.GetFields();
								num2 = 0;
								foreach (FieldInfo fieldInfo3 in fields2)
								{
									if (Attribute.IsDefined(fieldInfo3, typeof(MaterialSharedPropertyMappingAttribute)))
									{
										MaterialSharedPropertyMappingAttribute[] array6 = (MaterialSharedPropertyMappingAttribute[])fieldInfo3.GetCustomAttributes(typeof(MaterialSharedPropertyMappingAttribute), false);
										dictionary[array6[0].property].Add(num + num2++);
									}
									SurfaceDataAttributes[] array7 = (SurfaceDataAttributes[])fieldInfo3.GetCustomAttributes(typeof(SurfaceDataAttributes), false);
									if (array7.Length != 0)
									{
										num2 += array7[0].displayNames.Length;
									}
								}
							}
						}
					}
				}
				foreach (MaterialSharedProperty materialSharedProperty in dictionary.Keys)
				{
					MaterialDebugSettings.s_MaterialPropertyMap[materialSharedProperty] = dictionary[materialSharedProperty].ToArray();
				}
				MaterialDebugSettings.isDebugViewMaterialInit = true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000A558 File Offset: 0x00008758
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000A560 File Offset: 0x00008760
		public int[] debugViewMaterial
		{
			get
			{
				return this.m_DebugViewMaterial;
			}
			internal set
			{
				int num = ((value != null) ? value.Length : 0);
				if (num > 10)
				{
					Debug.LogError(string.Format("DebugViewMaterialBuffer is cannot handle {0} elements. Only first {1} are kept.", num, 10));
				}
				int num2 = Mathf.Min(10, num);
				if (num2 == 0)
				{
					this.m_DebugViewMaterial[0] = 1;
					this.m_DebugViewMaterial[1] = 0;
					return;
				}
				this.m_DebugViewMaterial[0] = num2;
				for (int i = 0; i < num2; i++)
				{
					this.m_DebugViewMaterial[i + 1] = value[i];
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000A5D9 File Offset: 0x000087D9
		public int debugViewEngine
		{
			get
			{
				return this.m_DebugViewEngine;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A5E1 File Offset: 0x000087E1
		public DebugViewVarying debugViewVarying
		{
			get
			{
				return this.m_DebugViewVarying;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000A5E9 File Offset: 0x000087E9
		public DebugViewProperties debugViewProperties
		{
			get
			{
				return this.m_DebugViewProperties;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000A5F1 File Offset: 0x000087F1
		public int debugViewGBuffer
		{
			get
			{
				return this.m_DebugViewGBuffer;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A5FC File Offset: 0x000087FC
		internal float[] GetDebugMaterialIndexes()
		{
			int num = this.m_DebugViewMaterial[0];
			MaterialDebugSettings.s_DebugViewMaterialOffsetedBuffer[0] = (float)num;
			for (int i = 1; i <= num; i++)
			{
				MaterialDebugSettings.s_DebugViewMaterialOffsetedBuffer[i] = (float)(this.m_DebugViewGBuffer + this.m_DebugViewMaterial[i] + this.m_DebugViewEngine + this.m_DebugViewVarying + (int)this.m_DebugViewProperties);
			}
			return MaterialDebugSettings.s_DebugViewMaterialOffsetedBuffer;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A658 File Offset: 0x00008858
		public void DisableMaterialDebug()
		{
			this.debugViewMaterialCommonValue = MaterialSharedProperty.None;
			this.m_DebugViewMaterial[0] = 1;
			this.m_DebugViewMaterial[1] = 0;
			this.m_DebugViewEngine = 0;
			this.m_DebugViewVarying = DebugViewVarying.None;
			this.m_DebugViewProperties = DebugViewProperties.None;
			this.m_DebugViewGBuffer = 0;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A68F File Offset: 0x0000888F
		public void SetDebugViewCommonMaterialProperty(MaterialSharedProperty value)
		{
			if (value != MaterialSharedProperty.None)
			{
				this.DisableMaterialDebug();
				this.materialEnumIndex = 0;
			}
			this.debugViewMaterial = ((value == MaterialSharedProperty.None) ? null : MaterialDebugSettings.s_MaterialPropertyMap[value]);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public void SetDebugViewMaterial(int value)
		{
			this.debugViewMaterialCommonValue = MaterialSharedProperty.None;
			if (value != 0)
			{
				this.DisableMaterialDebug();
				this.m_DebugViewMaterial[0] = 1;
				this.m_DebugViewMaterial[1] = value;
				return;
			}
			this.m_DebugViewMaterial[0] = 1;
			this.m_DebugViewMaterial[1] = 0;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000A6EF File Offset: 0x000088EF
		public void SetDebugViewEngine(int value)
		{
			if (value != 0)
			{
				this.DisableMaterialDebug();
			}
			this.m_DebugViewEngine = value;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000A701 File Offset: 0x00008901
		public void SetDebugViewVarying(DebugViewVarying value)
		{
			if (value != DebugViewVarying.None)
			{
				this.DisableMaterialDebug();
			}
			this.m_DebugViewVarying = value;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000A713 File Offset: 0x00008913
		public void SetDebugViewProperties(DebugViewProperties value)
		{
			if (value != DebugViewProperties.None)
			{
				this.DisableMaterialDebug();
			}
			this.m_DebugViewProperties = value;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000A725 File Offset: 0x00008925
		public void SetDebugViewGBuffer(int value)
		{
			if (value != 0)
			{
				this.DisableMaterialDebug();
			}
			this.m_DebugViewGBuffer = value;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000A737 File Offset: 0x00008937
		public bool IsDebugGBufferEnabled()
		{
			return this.m_DebugViewGBuffer != 0;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000A744 File Offset: 0x00008944
		public bool IsDebugViewMaterialEnabled()
		{
			int[] debugViewMaterial = this.m_DebugViewMaterial;
			int num = ((debugViewMaterial != null) ? debugViewMaterial[0] : 0);
			bool flag = false;
			for (int i = 1; i <= num; i++)
			{
				flag |= this.m_DebugViewMaterial[i] != 0;
			}
			return flag;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000A77E File Offset: 0x0000897E
		public bool IsDebugDisplayEnabled()
		{
			return this.m_DebugViewEngine != 0 || this.IsDebugViewMaterialEnabled() || this.m_DebugViewVarying != DebugViewVarying.None || this.m_DebugViewProperties != DebugViewProperties.None || this.IsDebugGBufferEnabled();
		}

		// Token: 0x0400013B RID: 315
		private static bool isDebugViewMaterialInit = false;

		// Token: 0x0400013C RID: 316
		internal static GUIContent[] debugViewMaterialStrings = null;

		// Token: 0x0400013D RID: 317
		internal static int[] debugViewMaterialValues = null;

		// Token: 0x0400013E RID: 318
		internal static GUIContent[] debugViewEngineStrings = null;

		// Token: 0x0400013F RID: 319
		internal static int[] debugViewEngineValues = null;

		// Token: 0x04000140 RID: 320
		internal static GUIContent[] debugViewMaterialVaryingStrings = null;

		// Token: 0x04000141 RID: 321
		internal static int[] debugViewMaterialVaryingValues = null;

		// Token: 0x04000142 RID: 322
		internal static GUIContent[] debugViewMaterialPropertiesStrings = null;

		// Token: 0x04000143 RID: 323
		internal static int[] debugViewMaterialPropertiesValues = null;

		// Token: 0x04000144 RID: 324
		internal static GUIContent[] debugViewMaterialTextureStrings = null;

		// Token: 0x04000145 RID: 325
		internal static int[] debugViewMaterialTextureValues = null;

		// Token: 0x04000146 RID: 326
		public static GUIContent[] debugViewMaterialGBufferStrings = null;

		// Token: 0x04000147 RID: 327
		public static int[] debugViewMaterialGBufferValues = null;

		// Token: 0x04000148 RID: 328
		private static Dictionary<MaterialSharedProperty, int[]> s_MaterialPropertyMap = new Dictionary<MaterialSharedProperty, int[]>();

		// Token: 0x04000149 RID: 329
		public MaterialSharedProperty debugViewMaterialCommonValue;

		// Token: 0x0400014A RID: 330
		public Color materialValidateLowColor = new Color(1f, 0f, 0f);

		// Token: 0x0400014B RID: 331
		public Color materialValidateHighColor = new Color(0f, 0f, 1f);

		// Token: 0x0400014C RID: 332
		public Color materialValidateTrueMetalColor = new Color(1f, 1f, 0f);

		// Token: 0x0400014D RID: 333
		public bool materialValidateTrueMetal;

		// Token: 0x0400014E RID: 334
		private const int kDebugViewMaterialBufferLength = 10;

		// Token: 0x0400014F RID: 335
		private static float[] s_DebugViewMaterialOffsetedBuffer = new float[11];

		// Token: 0x04000150 RID: 336
		private int[] m_DebugViewMaterial = new int[11];

		// Token: 0x04000151 RID: 337
		private int m_DebugViewEngine;

		// Token: 0x04000152 RID: 338
		private DebugViewVarying m_DebugViewVarying;

		// Token: 0x04000153 RID: 339
		private DebugViewProperties m_DebugViewProperties;

		// Token: 0x04000154 RID: 340
		private int m_DebugViewGBuffer;

		// Token: 0x04000155 RID: 341
		internal int materialEnumIndex;

		// Token: 0x02000197 RID: 407
		internal class MaterialItem
		{
			// Token: 0x0400110B RID: 4363
			public string className;

			// Token: 0x0400110C RID: 4364
			public Type surfaceDataType;

			// Token: 0x0400110D RID: 4365
			public Type bsdfDataType;
		}
	}
}
