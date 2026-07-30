using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200006E RID: 110
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[AddComponentMenu("Mesh/TextMeshPro - Text")]
	[ExecuteAlways]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00023B24 File Offset: 0x00021D24
		protected override void Awake()
		{
			this.m_renderer = base.GetComponent<Renderer>();
			if (this.m_renderer == null)
			{
				this.m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			CanvasRenderer component = base.GetComponent<CanvasRenderer>();
			if (component != null)
			{
				Debug.Log("Removing unnecessary CanvasRenderer component from text object.", this);
				global::UnityEngine.Object.DestroyImmediate(component);
			}
			this.m_rectTransform = base.rectTransform;
			this.m_transform = this.transform;
			this.m_meshFilter = base.GetComponent<MeshFilter>();
			if (this.m_meshFilter == null)
			{
				this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.m_mesh == null)
			{
				this.m_mesh = new Mesh();
				this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				this.m_meshFilter.mesh = this.m_mesh;
				this.m_textInfo = new TMP_TextInfo(this);
			}
			this.m_meshFilter.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
			base.LoadDefaultSettings();
			this.LoadFontAsset();
			if (this.m_TextParsingBuffer == null)
			{
				this.m_TextParsingBuffer = new TMP_Text.UnicodeChar[this.m_max_characters];
			}
			this.m_cached_TextElement = new TMP_Character();
			this.m_isFirstAllocation = true;
			TMP_SubMesh[] componentsInChildren = base.GetComponentsInChildren<TMP_SubMesh>();
			if (componentsInChildren.Length != 0)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					this.m_subTextObjects[i + 1] = componentsInChildren[i];
				}
			}
			this.m_isInputParsingRequired = true;
			this.m_havePropertiesChanged = true;
			this.m_isCalculateSizeRequired = true;
			this.m_isAwake = true;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00023C8C File Offset: 0x00021E8C
		protected override void OnEnable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (!this.m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			this.meshFilter.sharedMesh = this.mesh;
			this.SetActiveSubMeshes(true);
			this.ComputeMarginSize();
			this.m_verticesAlreadyDirty = false;
			this.m_layoutAlreadyDirty = false;
			this.m_isInputParsingRequired = true;
			this.SetAllDirty();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00023CF8 File Offset: 0x00021EF8
		protected override void OnDisable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			this.m_meshFilter.sharedMesh = null;
			this.SetActiveSubMeshes(false);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00023D22 File Offset: 0x00021F22
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			this.m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00023D50 File Offset: 0x00021F50
		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_fontAsset == null)
			{
				if (TMP_Settings.defaultFontAsset != null)
				{
					this.m_fontAsset = TMP_Settings.defaultFontAsset;
				}
				else
				{
					this.m_fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
				}
				if (this.m_fontAsset == null)
				{
					Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (this.m_fontAsset.characterLookupTable == null)
				{
					Debug.Log("Dictionary is Null!");
				}
				this.m_renderer.sharedMaterial = this.m_fontAsset.material;
				this.m_sharedMaterial = this.m_fontAsset.material;
				this.m_sharedMaterial.SetFloat("_CullMode", 0f);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				this.m_renderer.receiveShadows = false;
				this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (this.m_fontAsset.characterLookupTable == null)
				{
					this.m_fontAsset.ReadFontAssetDefinition();
				}
				if (this.m_renderer.sharedMaterial == null || this.m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || this.m_fontAsset.atlasTexture.GetInstanceID() != this.m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					this.m_renderer.sharedMaterial = this.m_fontAsset.material;
					this.m_sharedMaterial = this.m_fontAsset.material;
				}
				else
				{
					this.m_sharedMaterial = this.m_renderer.sharedMaterial;
				}
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				if (this.m_sharedMaterial.passCount == 1)
				{
					this.m_renderer.receiveShadows = false;
					this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			this.m_padding = this.GetPaddingForMaterial();
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			base.GetSpecialCharacters(this.m_fontAsset);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00023F64 File Offset: 0x00022164
		private void UpdateEnvMapMatrix()
		{
			if (!this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) || this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) == null)
			{
				return;
			}
			Vector3 vector = this.m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
			this.m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(vector), Vector3.one);
			this.m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, this.m_EnvMapMatrix);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00023FE4 File Offset: 0x000221E4
		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskHard:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskSoft:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			default:
				return;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00024096 File Offset: 0x00022296
		private void SetMaskCoordinates(Vector4 coords)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000240A9 File Offset: 0x000222A9
		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000240E0 File Offset: 0x000222E0
		private void EnableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = true;
				this.UpdateMask();
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002413C File Offset: 0x0002233C
		private void DisableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = false;
				this.UpdateMask();
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00024198 File Offset: 0x00022398
		private void UpdateMask()
		{
			if (!this.m_isMaskingEnabled)
			{
				return;
			}
			if (this.m_isMaskingEnabled && this.m_fontMaterial == null)
			{
				this.CreateMaterialInstance();
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000241C0 File Offset: 0x000223C0
		protected override Material GetMaterial(Material mat)
		{
			if (this.m_fontMaterial == null || this.m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_fontMaterial = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00024228 File Offset: 0x00022428
		protected override Material[] GetMaterials(Material[] mats)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontMaterials == null)
			{
				this.m_fontMaterials = new Material[materialCount];
			}
			else if (this.m_fontMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontMaterials[i] = base.fontMaterial;
				}
				else
				{
					this.m_fontMaterials[i] = this.m_subTextObjects[i].material;
				}
			}
			this.m_fontSharedMaterials = this.m_fontMaterials;
			return this.m_fontMaterials;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000242B5 File Offset: 0x000224B5
		protected override void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000242D0 File Offset: 0x000224D0
		protected override Material[] GetSharedMaterials()
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontSharedMaterials[i] = this.m_sharedMaterial;
				}
				else
				{
					this.m_fontSharedMaterials[i] = this.m_subTextObjects[i].sharedMaterial;
				}
			}
			return this.m_fontSharedMaterials;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00024354 File Offset: 0x00022554
		protected override void SetSharedMaterials(Material[] materials)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				Texture texture = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
				if (i == 0)
				{
					if (!(texture == null) && texture.GetInstanceID() == this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						this.m_sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
						this.m_padding = this.GetPaddingForMaterial(this.m_sharedMaterial);
					}
				}
				else if (!(texture == null) && texture.GetInstanceID() == this.m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && this.m_subTextObjects[i].isDefaultMaterial)
				{
					this.m_subTextObjects[i].sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
				}
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00024468 File Offset: 0x00022668
		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			this.m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_fontMaterial = this.m_renderer.material;
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000244DC File Offset: 0x000226DC
		protected override void SetFaceColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00024530 File Offset: 0x00022730
		protected override void SetOutlineColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00024584 File Offset: 0x00022784
		private void CreateMaterialInstance()
		{
			Material material = new Material(this.m_sharedMaterial);
			material.shaderKeywords = this.m_sharedMaterial.shaderKeywords;
			Material material2 = material;
			material2.name += " Instance";
			this.m_fontMaterial = material;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000245CC File Offset: 0x000227CC
		protected override void SetShaderDepth()
		{
			if (this.m_isOverlay)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
				this.m_renderer.material.renderQueue = 4000;
				this.m_sharedMaterial = this.m_renderer.material;
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			this.m_renderer.material.renderQueue = -1;
			this.m_sharedMaterial = this.m_renderer.material;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00024654 File Offset: 0x00022854
		protected override void SetCulling()
		{
			if (this.m_isCullingEnabled)
			{
				this.m_renderer.material.SetFloat("_CullMode", 2f);
				for (int i = 1; i < this.m_subTextObjects.Length; i++)
				{
					if (!(this.m_subTextObjects[i] != null))
					{
						return;
					}
					Renderer renderer = this.m_subTextObjects[i].renderer;
					if (renderer != null)
					{
						renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
					}
				}
			}
			else
			{
				this.m_renderer.material.SetFloat("_CullMode", 0f);
				int num = 1;
				while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
				{
					Renderer renderer2 = this.m_subTextObjects[num].renderer;
					if (renderer2 != null)
					{
						renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
					}
					num++;
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002473C File Offset: 0x0002293C
		private void SetPerspectiveCorrection()
		{
			if (this.m_isOrthographic)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00024774 File Offset: 0x00022974
		protected override int SetArraySizes(TMP_Text.UnicodeChar[] chars)
		{
			int num = 0;
			this.m_totalCharacterCount = 0;
			this.m_isUsingBold = false;
			this.m_isParsingText = false;
			this.tag_NoParsing = false;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_fontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			this.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			this.m_materialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
			if (this.m_textInfo == null)
			{
				this.m_textInfo = new TMP_TextInfo();
			}
			this.m_textElementType = TMP_TextElementType.Character;
			if (this.m_linkedTextComponent != null && !this.m_isCalculatingPreferredValues)
			{
				this.m_linkedTextComponent.text = string.Empty;
			}
			int num2 = 0;
			while (num2 < chars.Length && chars[num2].unicode != 0)
			{
				if (this.m_textInfo.characterInfo == null || this.m_totalCharacterCount >= this.m_textInfo.characterInfo.Length)
				{
					TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_totalCharacterCount + 1, true);
				}
				int num3 = chars[num2].unicode;
				if (!this.m_isRichText || num3 != 60)
				{
					goto IL_0347;
				}
				int currentMaterialIndex = this.m_currentMaterialIndex;
				int num4;
				if (!base.ValidateHtmlTag(chars, num2 + 1, out num4))
				{
					goto IL_0347;
				}
				int stringIndex = chars[num2].stringIndex;
				num2 = num4;
				if ((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					this.m_isUsingBold = true;
				}
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					MaterialReference[] materialReferences = this.m_materialReferences;
					int currentMaterialIndex2 = this.m_currentMaterialIndex;
					materialReferences[currentMaterialIndex2].referenceCount = materialReferences[currentMaterialIndex2].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)(57344 + this.m_spriteIndex);
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteIndex = this.m_spriteIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteAsset = this.m_currentSpriteAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = stringIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = chars[num2].stringIndex - stringIndex + 1;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = currentMaterialIndex;
					num++;
					this.m_totalCharacterCount++;
				}
				IL_0C2C:
				num2++;
				continue;
				IL_0347:
				bool flag = false;
				bool flag2 = false;
				TMP_FontAsset currentFontAsset = this.m_currentFontAsset;
				Material currentMaterial = this.m_currentMaterial;
				int currentMaterialIndex3 = this.m_currentMaterialIndex;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num3))
						{
							num3 = (int)char.ToUpper((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num3))
						{
							num3 = (int)char.ToLower((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num3))
					{
						num3 = (int)char.ToUpper((char)num3);
					}
				}
				TMP_FontAsset tmp_FontAsset;
				TMP_Character tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				if (tmp_Character == null && this.m_currentFontAsset.m_FallbackFontAssetTable != null && this.m_currentFontAsset.m_FallbackFontAssetTable.Count > 0)
				{
					tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num3, this.m_currentFontAsset.m_FallbackFontAssetTable, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				}
				if (tmp_Character == null)
				{
					TMP_SpriteAsset tmp_SpriteAsset = base.spriteAsset;
					if (tmp_SpriteAsset != null)
					{
						int num5 = -1;
						tmp_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicode(tmp_SpriteAsset, (uint)num3, true, out num5);
						if (num5 != -1)
						{
							this.m_textElementType = TMP_TextElementType.Sprite;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(tmp_SpriteAsset.material, tmp_SpriteAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
							MaterialReference[] materialReferences2 = this.m_materialReferences;
							int currentMaterialIndex4 = this.m_currentMaterialIndex;
							materialReferences2[currentMaterialIndex4].referenceCount = materialReferences2[currentMaterialIndex4].referenceCount + 1;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)num3;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteIndex = num5;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteAsset = tmp_SpriteAsset;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = tmp_SpriteAsset.spriteCharacterTable[num5];
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = chars[num2].stringIndex;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = chars[num2].length;
							this.m_textElementType = TMP_TextElementType.Character;
							this.m_currentMaterialIndex = currentMaterialIndex3;
							num++;
							this.m_totalCharacterCount++;
							goto IL_0C2C;
						}
					}
				}
				if (tmp_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num3, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				}
				if (tmp_Character == null && TMP_Settings.defaultFontAsset != null)
				{
					tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				}
				if (tmp_Character == null)
				{
					TMP_SpriteAsset tmp_SpriteAsset2 = TMP_Settings.defaultSpriteAsset;
					if (tmp_SpriteAsset2 != null)
					{
						int num6 = -1;
						tmp_SpriteAsset2 = TMP_SpriteAsset.SearchForSpriteByUnicode(tmp_SpriteAsset2, (uint)num3, true, out num6);
						if (num6 != -1)
						{
							this.m_textElementType = TMP_TextElementType.Sprite;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(tmp_SpriteAsset2.material, tmp_SpriteAsset2, this.m_materialReferences, this.m_materialReferenceIndexLookup);
							MaterialReference[] materialReferences3 = this.m_materialReferences;
							int currentMaterialIndex5 = this.m_currentMaterialIndex;
							materialReferences3[currentMaterialIndex5].referenceCount = materialReferences3[currentMaterialIndex5].referenceCount + 1;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)num3;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteIndex = num6;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteAsset = tmp_SpriteAsset2;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = tmp_SpriteAsset2.spriteCharacterTable[num6];
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = chars[num2].stringIndex;
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = chars[num2].length;
							this.m_textElementType = TMP_TextElementType.Character;
							this.m_currentMaterialIndex = currentMaterialIndex3;
							num++;
							this.m_totalCharacterCount++;
							goto IL_0C2C;
						}
					}
				}
				if (tmp_Character == null)
				{
					int num7 = num3;
					num3 = (chars[num2].unicode = ((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
					tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
					if (tmp_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
					{
						tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num3, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
					}
					if (tmp_Character == null && TMP_Settings.defaultFontAsset != null)
					{
						tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
					}
					if (tmp_Character == null)
					{
						num3 = (chars[num2].unicode = 32);
						tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
						if (!TMP_Settings.warningsDisabled)
						{
							Debug.LogWarning("Character with ASCII value of " + num7 + " was not found in the Font Asset Glyph Table. It was replaced by a space.", this);
						}
					}
				}
				if (tmp_FontAsset != null && tmp_FontAsset.instanceID != this.m_currentFontAsset.instanceID)
				{
					flag2 = true;
					this.m_currentFontAsset = tmp_FontAsset;
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = tmp_Character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].isUsingAlternateTypeface = flag;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)num3;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = chars[num2].stringIndex;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = chars[num2].length;
				if (flag2)
				{
					if (TMP_Settings.matchMaterialPreset)
					{
						this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_currentFontAsset.material);
					}
					else
					{
						this.m_currentMaterial = this.m_currentFontAsset.material;
					}
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
				}
				if (tmp_Character != null && tmp_Character.glyph.atlasIndex > 0)
				{
					this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentFontAsset, this.m_currentMaterial, tmp_Character.glyph.atlasIndex);
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
					flag2 = true;
				}
				if (!char.IsWhiteSpace((char)num3) && num3 != 8203)
				{
					if (this.m_materialReferences[this.m_currentMaterialIndex].referenceCount < 16383)
					{
						MaterialReference[] materialReferences4 = this.m_materialReferences;
						int currentMaterialIndex6 = this.m_currentMaterialIndex;
						materialReferences4[currentMaterialIndex6].referenceCount = materialReferences4[currentMaterialIndex6].referenceCount + 1;
					}
					else
					{
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_currentMaterial), this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
						MaterialReference[] materialReferences5 = this.m_materialReferences;
						int currentMaterialIndex7 = this.m_currentMaterialIndex;
						materialReferences5[currentMaterialIndex7].referenceCount = materialReferences5[currentMaterialIndex7].referenceCount + 1;
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].material = this.m_currentMaterial;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
				this.m_materialReferences[this.m_currentMaterialIndex].isFallbackMaterial = flag2;
				if (flag2)
				{
					this.m_materialReferences[this.m_currentMaterialIndex].fallbackMaterial = currentMaterial;
					this.m_currentFontAsset = currentFontAsset;
					this.m_currentMaterial = currentMaterial;
					this.m_currentMaterialIndex = currentMaterialIndex3;
				}
				this.m_totalCharacterCount++;
				goto IL_0C2C;
			}
			if (this.m_isCalculatingPreferredValues)
			{
				this.m_isCalculatingPreferredValues = false;
				this.m_isInputParsingRequired = true;
				return this.m_totalCharacterCount;
			}
			this.m_textInfo.spriteCount = num;
			int num8 = (this.m_textInfo.materialCount = this.m_materialReferenceIndexLookup.Count);
			if (num8 > this.m_textInfo.meshInfo.Length)
			{
				TMP_TextInfo.Resize<TMP_MeshInfo>(ref this.m_textInfo.meshInfo, num8, false);
			}
			if (num8 > this.m_subTextObjects.Length)
			{
				TMP_TextInfo.Resize<TMP_SubMesh>(ref this.m_subTextObjects, Mathf.NextPowerOfTwo(num8 + 1));
			}
			if (this.m_textInfo.characterInfo.Length - this.m_totalCharacterCount > 256)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, Mathf.Max(this.m_totalCharacterCount + 1, 256), true);
			}
			for (int i = 0; i < num8; i++)
			{
				if (i > 0)
				{
					if (this.m_subTextObjects[i] == null)
					{
						this.m_subTextObjects[i] = TMP_SubMesh.AddSubTextObject(this, this.m_materialReferences[i]);
						this.m_textInfo.meshInfo[i].vertices = null;
					}
					if (this.m_subTextObjects[i].sharedMaterial == null || this.m_subTextObjects[i].sharedMaterial.GetInstanceID() != this.m_materialReferences[i].material.GetInstanceID())
					{
						bool isDefaultMaterial = this.m_materialReferences[i].isDefaultMaterial;
						this.m_subTextObjects[i].isDefaultMaterial = isDefaultMaterial;
						if (!isDefaultMaterial || this.m_subTextObjects[i].sharedMaterial == null || this.m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() != this.m_materialReferences[i].material.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
						{
							this.m_subTextObjects[i].sharedMaterial = this.m_materialReferences[i].material;
							this.m_subTextObjects[i].fontAsset = this.m_materialReferences[i].fontAsset;
							this.m_subTextObjects[i].spriteAsset = this.m_materialReferences[i].spriteAsset;
						}
					}
					if (this.m_materialReferences[i].isFallbackMaterial)
					{
						this.m_subTextObjects[i].fallbackMaterial = this.m_materialReferences[i].material;
						this.m_subTextObjects[i].fallbackSourceMaterial = this.m_materialReferences[i].fallbackMaterial;
					}
				}
				int referenceCount = this.m_materialReferences[i].referenceCount;
				if (this.m_textInfo.meshInfo[i].vertices == null || this.m_textInfo.meshInfo[i].vertices.Length < referenceCount * ((!this.m_isVolumetricText) ? 4 : 8))
				{
					if (this.m_textInfo.meshInfo[i].vertices == null)
					{
						if (i == 0)
						{
							this.m_textInfo.meshInfo[i] = new TMP_MeshInfo(this.m_mesh, referenceCount + 1, this.m_isVolumetricText);
						}
						else
						{
							this.m_textInfo.meshInfo[i] = new TMP_MeshInfo(this.m_subTextObjects[i].mesh, referenceCount + 1, this.m_isVolumetricText);
						}
					}
					else
					{
						this.m_textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), this.m_isVolumetricText);
					}
				}
				else if (this.m_VertexBufferAutoSizeReduction && referenceCount > 0 && this.m_textInfo.meshInfo[i].vertices.Length - referenceCount * ((!this.m_isVolumetricText) ? 4 : 8) > 1024)
				{
					this.m_textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), this.m_isVolumetricText);
				}
				this.m_textInfo.meshInfo[i].material = this.m_materialReferences[i].material;
			}
			int num9 = num8;
			while (num9 < this.m_subTextObjects.Length && this.m_subTextObjects[num9] != null)
			{
				if (num9 < this.m_textInfo.meshInfo.Length)
				{
					this.m_textInfo.meshInfo[num9].ClearUnusedVertices(0, true);
				}
				num9++;
			}
			return this.m_totalCharacterCount;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00025870 File Offset: 0x00023A70
		public override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				this.m_marginWidth = this.m_rectTransform.rect.width - this.m_margin.x - this.m_margin.z;
				this.m_marginHeight = this.m_rectTransform.rect.height - this.m_margin.y - this.m_margin.w;
				this.m_RectTransformCorners = this.GetTextContainerLocalCorners();
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000258F9 File Offset: 0x00023AF9
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_havePropertiesChanged = true;
			this.isMaskUpdateRequired = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002590F File Offset: 0x00023B0F
		protected override void OnTransformParentChanged()
		{
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002591D File Offset: 0x00023B1D
		protected override void OnRectTransformDimensionsChange()
		{
			this.ComputeMarginSize();
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00025934 File Offset: 0x00023B34
		internal override void InternalUpdate()
		{
			if (!this.m_havePropertiesChanged)
			{
				float y = this.m_rectTransform.lossyScale.y;
				if (y != this.m_previousLossyScaleY && this.m_text != string.Empty && this.m_text != null)
				{
					float num = y / this.m_previousLossyScaleY;
					this.UpdateSDFScale(num);
					this.m_previousLossyScaleY = y;
				}
			}
			if (this.m_isUsingLegacyAnimationComponent)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000259AC File Offset: 0x00023BAC
		private void OnPreRenderObject()
		{
			if (!this.m_isAwake || (!this.IsActive() && !this.m_ignoreActiveState))
			{
				return;
			}
			if (this.m_fontAsset == null)
			{
				Debug.LogWarning("Please assign a Font Asset to this " + this.transform.name + " gameobject.", this);
				return;
			}
			if (this.m_havePropertiesChanged || this.m_isLayoutDirty)
			{
				if (this.isMaskUpdateRequired)
				{
					this.UpdateMask();
					this.isMaskUpdateRequired = false;
				}
				if (this.checkPaddingRequired)
				{
					this.UpdateMeshPadding();
				}
				if (this.m_isInputParsingRequired || this.m_isTextTruncated)
				{
					base.ParseInputText();
					TMP_FontAsset.UpdateFontAssets();
				}
				if (this.m_enableAutoSizing)
				{
					this.m_fontSize = Mathf.Clamp(this.m_fontSizeBase, this.m_fontSizeMin, this.m_fontSizeMax);
				}
				this.m_maxFontSize = this.m_fontSizeMax;
				this.m_minFontSize = this.m_fontSizeMin;
				this.m_lineSpacingDelta = 0f;
				this.m_charWidthAdjDelta = 0f;
				this.m_isTextTruncated = false;
				this.m_havePropertiesChanged = false;
				this.m_isLayoutDirty = false;
				this.m_ignoreActiveState = false;
				this.m_IsAutoSizePointSizeSet = false;
				this.m_AutoSizeIterationCount = 0;
				while (!this.m_IsAutoSizePointSizeSet)
				{
					this.GenerateTextMesh();
					this.m_AutoSizeIterationCount++;
				}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00025AF0 File Offset: 0x00023CF0
		protected override void GenerateTextMesh()
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID());
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			if (this.m_textInfo != null)
			{
				this.m_textInfo.Clear();
			}
			if (this.m_TextParsingBuffer == null || this.m_TextParsingBuffer.Length == 0 || this.m_TextParsingBuffer[0].unicode == 0)
			{
				this.ClearMesh(true);
				this.m_preferredWidth = 0f;
				this.m_preferredHeight = 0f;
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			this.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			this.m_currentSpriteAsset = this.m_spriteAsset;
			if (this.m_spriteAnimator != null)
			{
				this.m_spriteAnimator.StopAllAnimations();
			}
			int totalCharacterCount = this.m_totalCharacterCount;
			float num = (this.m_fontScale = this.m_fontSize / (float)this.m_fontAsset.m_FaceInfo.pointSize * this.m_fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f));
			float num2 = num;
			float num3 = this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = this.m_fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_fontStyleStack.Clear();
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			float num4 = 0f;
			float num5 = 0f;
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			bool flag = false;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			bool flag2 = false;
			Vector3 zero3 = Vector3.zero;
			Vector3 zero4 = Vector3.zero;
			bool flag3 = false;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			this.m_fontColor32 = this.m_fontColor;
			this.m_htmlColor = this.m_fontColor32;
			this.m_underlineColor = this.m_htmlColor;
			this.m_strikethroughColor = this.m_htmlColor;
			this.m_colorStack.SetDefault(this.m_htmlColor);
			this.m_underlineColorStack.SetDefault(this.m_htmlColor);
			this.m_strikethroughColorStack.SetDefault(this.m_htmlColor);
			this.m_HighlightStateStack.SetDefault(new HighlightState(this.m_htmlColor, TMP_Offset.zero));
			this.m_colorGradientPreset = null;
			this.m_colorGradientStack.SetDefault(null);
			this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
			this.m_ItalicAngleStack.SetDefault(this.m_ItalicAngle);
			this.m_actionStack.Clear();
			this.m_isFXMatrixSet = false;
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float num6 = this.m_currentFontAsset.m_FaceInfo.lineHeight - (this.m_currentFontAsset.m_FaceInfo.ascentLine - this.m_currentFontAsset.m_FaceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = this.m_firstVisibleCharacter;
			this.m_lastCharacterOfLine = 0;
			this.m_firstVisibleCharacterOfLine = 0;
			this.m_lastVisibleCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_lineVisibleCharacterCount = 0;
			bool flag4 = true;
			bool flag5 = false;
			this.m_firstOverflowCharacterIndex = -1;
			this.m_pageNumber = 0;
			int num7 = Mathf.Clamp(this.m_pageToDisplay - 1, 0, this.m_textInfo.pageInfo.Length - 1);
			this.m_textInfo.ClearPageInfo();
			Vector4 margin = this.m_margin;
			float num8 = ((this.m_marginWidth > 0f) ? this.m_marginWidth : 0f);
			float num9 = ((this.m_marginHeight > 0f) ? this.m_marginHeight : 0f);
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			this.m_width = -1f;
			float num10 = num8 + 0.0001f - this.m_marginLeft - this.m_marginRight;
			this.m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
			this.m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
			this.m_textInfo.ClearLineInfo();
			this.m_maxCapHeight = 0f;
			this.m_maxAscender = 0f;
			this.m_maxDescender = 0f;
			float num11 = 0f;
			float num12 = 0f;
			bool flag6 = false;
			this.m_isNewPage = false;
			bool flag7 = true;
			this.m_isNonBreakingSpace = false;
			bool flag8 = false;
			bool flag9 = false;
			TMP_Text.CharacterSubstitution characterSubstitution = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool flag10 = false;
			base.SaveWordWrappingState(ref this.m_SavedWordWrapState, -1, -1);
			base.SaveWordWrappingState(ref this.m_SavedLineState, -1, -1);
			base.SaveWordWrappingState(ref this.m_SavedEllipsisState, -1, -1);
			base.SaveWordWrappingState(ref this.m_SavedLastValidState, -1, -1);
			int num13 = 0;
			while (num13 < this.m_TextParsingBuffer.Length && this.m_TextParsingBuffer[num13].unicode != 0)
			{
				int num14 = this.m_TextParsingBuffer[num13].unicode;
				if (!this.m_isRichText || num14 != 60)
				{
					this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					goto IL_065F;
				}
				this.m_isParsingText = true;
				this.m_textElementType = TMP_TextElementType.Character;
				int num15;
				if (!base.ValidateHtmlTag(this.m_TextParsingBuffer, num13 + 1, out num15))
				{
					goto IL_065F;
				}
				num13 = num15;
				if (this.m_textElementType != TMP_TextElementType.Character)
				{
					goto IL_065F;
				}
				IL_3608:
				num13++;
				continue;
				IL_065F:
				int currentMaterialIndex = this.m_currentMaterialIndex;
				bool isUsingAlternateTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
				this.m_isParsingText = false;
				bool flag11 = false;
				if (characterSubstitution.index == this.m_characterCount)
				{
					num14 = (int)characterSubstitution.unicode;
					this.m_textElementType = TMP_TextElementType.Character;
					flag11 = true;
					if (num14 != 3 && num14 != 45 && num14 == 8230)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_cached_Ellipsis_Character;
						this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
						this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_materialReferences[0].fontAsset;
						this.m_textInfo.characterInfo[this.m_characterCount].material = this.m_materialReferences[0].material;
						this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = 0;
						this.m_isTextTruncated = true;
						characterSubstitution.index = this.m_characterCount + 1;
						characterSubstitution.unicode = 3U;
					}
				}
				if (this.m_characterCount < this.m_firstVisibleCharacter && num14 != 3)
				{
					this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
					this.m_textInfo.characterInfo[this.m_characterCount].character = '\u200b';
					this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = 0;
					this.m_characterCount++;
					goto IL_3608;
				}
				float num16 = 1f;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num14))
						{
							num14 = (int)char.ToUpper((char)num14);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num14))
						{
							num14 = (int)char.ToLower((char)num14);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num14))
					{
						num16 = 0.8f;
						num14 = (int)char.ToUpper((char)num14);
					}
				}
				float num17 = 0f;
				float num18 = 1f;
				float num19 = 0f;
				float num20 = 0f;
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_currentSpriteAsset = this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset;
					this.m_spriteIndex = this.m_textInfo.characterInfo[this.m_characterCount].spriteIndex;
					TMP_SpriteCharacter tmp_SpriteCharacter = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					if (tmp_SpriteCharacter == null)
					{
						goto IL_3608;
					}
					if (num14 == 60)
					{
						num14 = 57344 + this.m_spriteIndex;
					}
					else
					{
						this.m_spriteColor = TMP_Text.s_colorWhite;
					}
					if (this.m_currentSpriteAsset.m_FaceInfo.pointSize > 0)
					{
						num18 = this.m_currentFontSize / (float)this.m_currentSpriteAsset.m_FaceInfo.pointSize * this.m_currentSpriteAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num18;
						num19 = this.m_currentSpriteAsset.m_FaceInfo.ascentLine;
						num17 = this.m_currentSpriteAsset.m_FaceInfo.baseline * this.m_fontScale * this.m_fontScaleMultiplier * this.m_currentSpriteAsset.m_FaceInfo.scale;
						num20 = this.m_currentSpriteAsset.m_FaceInfo.descentLine;
					}
					else
					{
						num18 = this.m_currentFontSize / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = this.m_currentFontAsset.m_FaceInfo.ascentLine / tmp_SpriteCharacter.m_Glyph.metrics.height * tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num18;
						num19 = this.m_currentFontAsset.m_FaceInfo.ascentLine;
						num17 = this.m_currentFontAsset.m_FaceInfo.baseline * this.m_fontScale * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
						num20 = this.m_currentFontAsset.m_FaceInfo.descentLine;
					}
					this.m_cached_TextElement = tmp_SpriteCharacter;
					this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
					this.m_textInfo.characterInfo[this.m_characterCount].scale = num18;
					this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset = this.m_currentSpriteAsset;
					this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_currentMaterialIndex = currentMaterialIndex;
					num4 = 0f;
				}
				else if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if (flag11)
					{
						this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset.characterLookupTable[(uint)num14];
					}
					else
					{
						this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
					}
					if (this.m_cached_TextElement == null)
					{
						goto IL_3608;
					}
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					this.m_currentMaterial = this.m_textInfo.characterInfo[this.m_characterCount].material;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					if (flag11 && this.m_TextParsingBuffer[num13].unicode == 10 && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						this.m_fontScale = this.m_textInfo.characterInfo[this.m_characterCount - 1].scale;
					}
					else
					{
						this.m_fontScale = this.m_currentFontSize * num16 / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					}
					num2 = this.m_fontScale * this.m_fontScaleMultiplier * this.m_cached_TextElement.m_Scale * this.m_cached_TextElement.m_Glyph.scale;
					num17 = this.m_currentFontAsset.m_FaceInfo.baseline * this.m_fontScale * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
					this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
					this.m_textInfo.characterInfo[this.m_characterCount].scale = num2;
					num4 = ((this.m_currentMaterialIndex == 0) ? this.m_padding : this.m_subTextObjects[this.m_currentMaterialIndex].padding);
				}
				float num21 = num2;
				if (num14 == 173 || num14 == 3)
				{
					num2 = 0f;
				}
				this.m_textInfo.characterInfo[this.m_characterCount].character = (char)num14;
				this.m_textInfo.characterInfo[this.m_characterCount].pointSize = this.m_currentFontSize;
				this.m_textInfo.characterInfo[this.m_characterCount].color = this.m_htmlColor;
				this.m_textInfo.characterInfo[this.m_characterCount].underlineColor = this.m_underlineColor;
				this.m_textInfo.characterInfo[this.m_characterCount].strikethroughColor = this.m_strikethroughColor;
				this.m_textInfo.characterInfo[this.m_characterCount].highlightState = this.m_HighlightStateStack.current;
				this.m_textInfo.characterInfo[this.m_characterCount].style = this.m_FontStyleInternal;
				GlyphMetrics metrics = this.m_cached_TextElement.m_Glyph.metrics;
				bool flag12 = char.IsWhiteSpace((char)num14);
				TMP_GlyphValueRecord tmp_GlyphValueRecord = default(TMP_GlyphValueRecord);
				float num22 = this.m_characterSpacing;
				if (this.m_enableKerning)
				{
					uint glyphIndex = this.m_cached_TextElement.m_GlyphIndex;
					if (this.m_characterCount < totalCharacterCount - 1)
					{
						uint num23 = (this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16) | glyphIndex;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(num23, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord = tmp_GlyphPairAdjustmentRecord.m_FirstAdjustmentRecord.m_GlyphValueRecord;
							num22 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num22);
						}
					}
					if (this.m_characterCount >= 1)
					{
						uint glyphIndex2 = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
						uint num24 = (glyphIndex << 16) | glyphIndex2;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(num24, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord += tmp_GlyphPairAdjustmentRecord.m_SecondAdjustmentRecord.m_GlyphValueRecord;
							num22 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num22);
						}
					}
				}
				if (this.m_isRightToLeft)
				{
					this.m_xAdvance -= (metrics.horizontalAdvance * num2 + (this.m_currentFontAsset.normalSpacingOffset + num22 + this.m_wordSpacing + num5) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag12 || num14 == 8203)
					{
						this.m_xAdvance -= this.m_wordSpacing * num3;
					}
				}
				float num25 = 0f;
				if (this.m_monoSpacing != 0f)
				{
					num25 = (this.m_monoSpacing / 2f - (metrics.width / 2f + metrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
					this.m_xAdvance += num25;
				}
				float num26;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
					{
						float @float = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num26 = this.m_currentFontAsset.boldStyle / 4f * @float * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num26 + num4 > @float)
						{
							num4 = @float - num26;
						}
					}
					else
					{
						num26 = 0f;
					}
					num5 = this.m_currentFontAsset.boldSpacing;
				}
				else
				{
					if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
					{
						float float2 = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num26 = this.m_currentFontAsset.normalStyle / 4f * float2 * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num26 + num4 > float2)
						{
							num4 = float2 - num26;
						}
					}
					else
					{
						num26 = 0f;
					}
					num5 = 0f;
				}
				Vector3 vector3;
				vector3.x = this.m_xAdvance + (metrics.horizontalBearingX - num4 - num26 + tmp_GlyphValueRecord.m_XPlacement) * num2 * (1f - this.m_charWidthAdjDelta);
				vector3.y = num17 + (metrics.horizontalBearingY + num4 + tmp_GlyphValueRecord.m_YPlacement) * num2 - this.m_lineOffset + this.m_baselineOffset;
				vector3.z = 0f;
				Vector3 vector4;
				vector4.x = vector3.x;
				vector4.y = vector3.y - (metrics.height + num4 * 2f) * num2;
				vector4.z = 0f;
				Vector3 vector5;
				vector5.x = vector4.x + (metrics.width + num4 * 2f + num26 * 2f) * num2 * (1f - this.m_charWidthAdjDelta);
				vector5.y = vector3.y;
				vector5.z = 0f;
				Vector3 vector6;
				vector6.x = vector5.x;
				vector6.y = vector4.y;
				vector6.z = 0f;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
				{
					float num27 = (float)this.m_ItalicAngle * 0.01f;
					Vector3 vector7 = new Vector3(num27 * ((metrics.horizontalBearingY + num4 + num26) * num2), 0f, 0f);
					Vector3 vector8 = new Vector3(num27 * ((metrics.horizontalBearingY - metrics.height - num4 - num26) * num2), 0f, 0f);
					Vector3 vector9 = new Vector3((vector7.x - vector8.x) / 2f, 0f, 0f);
					vector3 = vector3 + vector7 - vector9;
					vector4 = vector4 + vector8 - vector9;
					vector5 = vector5 + vector7 - vector9;
					vector6 = vector6 + vector8 - vector9;
				}
				if (this.m_isFXMatrixSet)
				{
					float x = this.m_FXMatrix.lossyScale.x;
					Vector3 vector10 = (vector5 + vector4) / 2f;
					vector3 = this.m_FXMatrix.MultiplyPoint3x4(vector3 - vector10) + vector10;
					vector4 = this.m_FXMatrix.MultiplyPoint3x4(vector4 - vector10) + vector10;
					vector5 = this.m_FXMatrix.MultiplyPoint3x4(vector5 - vector10) + vector10;
					vector6 = this.m_FXMatrix.MultiplyPoint3x4(vector6 - vector10) + vector10;
				}
				this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft = vector4;
				this.m_textInfo.characterInfo[this.m_characterCount].topLeft = vector3;
				this.m_textInfo.characterInfo[this.m_characterCount].topRight = vector5;
				this.m_textInfo.characterInfo[this.m_characterCount].bottomRight = vector6;
				this.m_textInfo.characterInfo[this.m_characterCount].origin = this.m_xAdvance;
				this.m_textInfo.characterInfo[this.m_characterCount].baseLine = num17 - this.m_lineOffset + this.m_baselineOffset;
				this.m_textInfo.characterInfo[this.m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
				float num28 = ((this.m_textElementType == TMP_TextElementType.Character) ? (this.m_currentFontAsset.m_FaceInfo.ascentLine * num2 / num16 + this.m_baselineOffset) : (num19 * num18 + this.m_baselineOffset));
				this.m_textInfo.characterInfo[this.m_characterCount].ascender = num28 - this.m_lineOffset;
				float num29 = ((this.m_textElementType == TMP_TextElementType.Character) ? (this.m_currentFontAsset.m_FaceInfo.descentLine * num2 / num16 + this.m_baselineOffset) : (num20 * num18 + this.m_baselineOffset));
				float num30 = (this.m_textInfo.characterInfo[this.m_characterCount].descender = num29 - this.m_lineOffset);
				if (num14 != 10 || this.m_characterCount == this.m_firstCharacterOfLine)
				{
					this.m_maxLineAscender = ((num28 > this.m_maxLineAscender) ? num28 : this.m_maxLineAscender);
					this.m_maxLineDescender = ((num29 < this.m_maxLineDescender) ? num29 : this.m_maxLineDescender);
				}
				if ((this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
				{
					float num31 = (num28 - this.m_baselineOffset) / this.m_currentFontAsset.m_FaceInfo.subscriptSize;
					num28 = this.m_maxLineAscender;
					this.m_maxLineAscender = ((num31 > this.m_maxLineAscender) ? num31 : this.m_maxLineAscender);
					float num32 = (num29 - this.m_baselineOffset) / this.m_currentFontAsset.m_FaceInfo.subscriptSize;
					num29 = this.m_maxLineDescender;
					this.m_maxLineDescender = ((num32 < this.m_maxLineDescender) ? num32 : this.m_maxLineDescender);
				}
				if ((this.m_lineNumber == 0 || this.m_isNewPage) && (num14 != 10 || this.m_characterCount == this.m_firstCharacterOfLine))
				{
					this.m_maxAscender = ((this.m_maxAscender > num28) ? this.m_maxAscender : num28);
					this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * num2 / num16);
				}
				if (this.m_lineOffset == 0f)
				{
					num11 = ((num11 > num28) ? num11 : num28);
				}
				this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
				bool flag13 = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
				if (num14 == 9 || num14 == 160 || num14 == 8199 || (!flag12 && num14 != 8203 && num14 != 173 && num14 != 3) || (num14 == 173 && !flag10) || this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_textInfo.characterInfo[this.m_characterCount].isVisible = true;
					float num33 = this.m_marginLeft;
					float num34 = this.m_marginRight;
					if (flag11)
					{
						num33 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
						num34 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
					}
					num10 = ((this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - num33 - num34, this.m_width) : (num8 + 0.0001f - num33 - num34));
					float num35 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * ((num14 != 173) ? num2 : num21);
					float num36 = this.m_maxAscender - num30 + ((this.m_lineNumber > 0 && !flag5) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
					int characterCount = this.m_characterCount;
					if (num36 > num9 + 0.0001f)
					{
						if (this.m_firstOverflowCharacterIndex == -1)
						{
							this.m_firstOverflowCharacterIndex = this.m_characterCount;
						}
						if (this.m_enableAutoSizing)
						{
							if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_lineNumber > 0 && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								float num37 = (num9 - num36) / (float)this.m_lineNumber;
								this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num37 / num, this.m_lineSpacingMax);
								return;
							}
							if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								this.m_maxFontSize = this.m_fontSize;
								float num38 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
								this.m_fontSize -= num38;
								this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
								return;
							}
						}
						switch (this.m_overflowMode)
						{
						case TextOverflowModes.Ellipsis:
							num13 = base.RestoreWordWrappingState(ref this.m_SavedEllipsisState);
							if (num13 < 0 || characterCount == 0)
							{
								num13 = -1;
								this.m_characterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							}
							num13--;
							this.m_characterCount--;
							characterSubstitution.index = this.m_characterCount;
							characterSubstitution.unicode = 8230U;
							goto IL_3608;
						case TextOverflowModes.Truncate:
							num13 = base.RestoreWordWrappingState(ref this.m_SavedLastValidState);
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3U;
							goto IL_3608;
						case TextOverflowModes.Page:
							if (num13 < 0 || characterCount == 0)
							{
								num13 = -1;
								this.m_characterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							}
							num13 = base.RestoreWordWrappingState(ref this.m_SavedLineState);
							this.m_isNewPage = true;
							this.m_xAdvance = 0f + this.tag_Indent;
							this.m_lineOffset = 0f;
							this.m_maxAscender = 0f;
							num11 = 0f;
							this.m_lineNumber++;
							this.m_pageNumber++;
							goto IL_3608;
						case TextOverflowModes.Linked:
							num13 = base.RestoreWordWrappingState(ref this.m_SavedLastValidState);
							if (this.m_linkedTextComponent != null)
							{
								this.m_linkedTextComponent.text = this.text;
								this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
								this.m_linkedTextComponent.ForceMeshUpdate(false, false);
								this.m_isTextTruncated = true;
							}
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3U;
							goto IL_3608;
						}
					}
					if (num35 > num10 * (flag13 ? 1.05f : 1f))
					{
						if (this.m_enableWordWrapping && this.m_characterCount != this.m_firstCharacterOfLine)
						{
							num13 = base.RestoreWordWrappingState(ref this.m_SavedWordWrapState);
							float num40;
							if (this.m_lineHeight == -32767f)
							{
								float num39 = this.m_textInfo.characterInfo[this.m_characterCount].ascender - this.m_textInfo.characterInfo[this.m_characterCount].baseLine;
								num40 = 0f - this.m_maxLineDescender + num39 + (num6 + this.m_lineSpacingDelta) * num + this.m_lineSpacing * num3;
							}
							else
							{
								num40 = this.m_lineHeight + this.m_lineSpacing * num3;
								flag5 = true;
							}
							float num41 = this.m_maxAscender - this.m_textInfo.characterInfo[this.m_characterCount].descender + num40;
							if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '\u00ad' && !flag10 && (this.m_overflowMode == TextOverflowModes.Overflow || num41 < num9 + 0.0001f))
							{
								characterSubstitution.index = this.m_characterCount - 1;
								characterSubstitution.unicode = 45U;
								num13--;
								this.m_characterCount--;
								goto IL_3608;
							}
							flag10 = false;
							if (this.m_textInfo.characterInfo[this.m_characterCount].character == '\u00ad')
							{
								flag10 = true;
								goto IL_3608;
							}
							if (this.m_enableAutoSizing && flag7)
							{
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num42 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num42 /= 1f - this.m_charWidthAdjDelta;
									}
									float num43 = num35 - (num10 - 0.0001f) * (flag13 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num43 / num42;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num44 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num44;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							if (num41 <= num9 + 0.0001f)
							{
								base.InsertNewLine(num13, num, num3, num22, num10, num6, ref flag6, ref num12);
								flag4 = true;
								flag7 = true;
								goto IL_3608;
							}
							if (this.m_firstOverflowCharacterIndex == -1)
							{
								this.m_firstOverflowCharacterIndex = this.m_characterCount;
							}
							if (this.m_enableAutoSizing)
							{
								if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num45 = (num9 - num41) / (float)(this.m_lineNumber + 1);
									this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num45 / num, this.m_lineSpacingMax);
									return;
								}
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num46 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num46 /= 1f - this.m_charWidthAdjDelta;
									}
									float num47 = num35 - (num10 - 0.0001f) * (flag13 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num47 / num46;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num48 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num48;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							switch (this.m_overflowMode)
							{
							case TextOverflowModes.Overflow:
							case TextOverflowModes.Masking:
							case TextOverflowModes.ScrollRect:
								base.InsertNewLine(num13, num, num3, num22, num10, num6, ref flag6, ref num12);
								flag4 = true;
								flag7 = true;
								goto IL_3608;
							case TextOverflowModes.Ellipsis:
								num13 = base.RestoreWordWrappingState(ref this.m_SavedEllipsisState);
								if (num13 < 0)
								{
									this.m_characterCount = 0;
									characterSubstitution.index = 0;
									characterSubstitution.unicode = 3U;
									goto IL_3608;
								}
								num13--;
								this.m_characterCount--;
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 8230U;
								goto IL_3608;
							case TextOverflowModes.Truncate:
								num13 = base.RestoreWordWrappingState(ref this.m_SavedLastValidState);
								characterSubstitution.index = characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							case TextOverflowModes.Page:
								this.m_isNewPage = true;
								base.InsertNewLine(num13, num, num3, num22, num10, num6, ref flag6, ref num12);
								this.m_lineOffset = 0f;
								this.m_maxAscender = 0f;
								num11 = 0f;
								this.m_pageNumber++;
								flag4 = true;
								flag7 = true;
								goto IL_3608;
							case TextOverflowModes.Linked:
								if (this.m_linkedTextComponent != null)
								{
									this.m_linkedTextComponent.text = this.text;
									this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
									this.m_linkedTextComponent.ForceMeshUpdate(false, false);
									this.m_isTextTruncated = true;
								}
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							}
						}
						else
						{
							if (this.m_enableAutoSizing && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
								{
									float num49 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num49 /= 1f - this.m_charWidthAdjDelta;
									}
									float num50 = num35 - (num10 - 0.0001f) * (flag13 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num50 / num49;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num51 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num51;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							switch (this.m_overflowMode)
							{
							case TextOverflowModes.Ellipsis:
								num13 = base.RestoreWordWrappingState(ref this.m_SavedEllipsisState);
								if (num13 < 0)
								{
									this.m_characterCount = 0;
									characterSubstitution.index = 0;
									characterSubstitution.unicode = 3U;
									goto IL_3608;
								}
								num13--;
								this.m_characterCount--;
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 8230U;
								goto IL_3608;
							case TextOverflowModes.Truncate:
								num13 = base.RestoreWordWrappingState(ref this.m_SavedWordWrapState);
								characterSubstitution.index = characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							case TextOverflowModes.Linked:
								num13 = base.RestoreWordWrappingState(ref this.m_SavedWordWrapState);
								if (this.m_linkedTextComponent != null)
								{
									this.m_linkedTextComponent.text = this.text;
									this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
									this.m_linkedTextComponent.ForceMeshUpdate(false, false);
									this.m_isTextTruncated = true;
								}
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3608;
							}
						}
					}
					if (num14 == 9 || num14 == 160 || num14 == 8199)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						this.m_lastVisibleCharacterOfLine = this.m_characterCount;
						TMP_LineInfo[] lineInfo = this.m_textInfo.lineInfo;
						int lineNumber = this.m_lineNumber;
						lineInfo[lineNumber].spaceCount = lineInfo[lineNumber].spaceCount + 1;
						this.m_textInfo.spaceCount++;
						if (num14 == 160)
						{
							TMP_LineInfo[] lineInfo2 = this.m_textInfo.lineInfo;
							int lineNumber2 = this.m_lineNumber;
							lineInfo2[lineNumber2].controlCharacterCount = lineInfo2[lineNumber2].controlCharacterCount + 1;
						}
					}
					else if (num14 == 173)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
					}
					else
					{
						Color32 color;
						if (this.m_overrideHtmlColors)
						{
							color = this.m_fontColor32;
						}
						else
						{
							color = this.m_htmlColor;
						}
						if (this.m_textElementType == TMP_TextElementType.Character)
						{
							this.SaveGlyphVertexInfo(num4, num26, color);
						}
						else if (this.m_textElementType == TMP_TextElementType.Sprite)
						{
							this.SaveSpriteVertexInfo(color);
						}
						if (flag4)
						{
							flag4 = false;
							this.m_firstVisibleCharacterOfLine = this.m_characterCount;
						}
						this.m_lineVisibleCharacterCount++;
						this.m_lastVisibleCharacterOfLine = this.m_characterCount;
						this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = num33;
						this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = num34;
					}
				}
				else if ((num14 == 10 || num14 == 11 || char.IsSeparator((char)num14)) && num14 != 173 && num14 != 8203 && num14 != 8288)
				{
					TMP_LineInfo[] lineInfo3 = this.m_textInfo.lineInfo;
					int lineNumber3 = this.m_lineNumber;
					lineInfo3[lineNumber3].spaceCount = lineInfo3[lineNumber3].spaceCount + 1;
					this.m_textInfo.spaceCount++;
				}
				if (this.m_lineNumber > 0 && !TMP_Math.Approximately(this.m_maxLineAscender, this.m_startOfLineAscender) && !flag5 && !this.m_isNewPage && !flag11)
				{
					float num52 = this.m_maxLineAscender - this.m_startOfLineAscender;
					base.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, num52);
					num30 -= num52;
					this.m_lineOffset += num52;
					this.m_startOfLineAscender += num52;
					this.m_SavedWordWrapState.lineOffset = this.m_lineOffset;
					this.m_SavedWordWrapState.previousLineAscender = this.m_startOfLineAscender;
				}
				if (this.m_overflowMode == TextOverflowModes.Ellipsis && !flag11)
				{
					float num53 = this.m_currentFontSize * num16 / (float)this.m_fontAsset.m_FaceInfo.pointSize * this.m_fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_cached_Ellipsis_Character.m_Scale * this.m_cached_Ellipsis_Character.m_Glyph.scale;
					float num54 = this.m_marginLeft;
					float num55 = this.m_marginRight;
					if (num14 == 10 && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num53 = this.m_textInfo.characterInfo[this.m_characterCount - 1].scale;
						num54 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
						num55 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
					}
					float num56 = this.m_fontAsset.m_FaceInfo.descentLine * num53 / num16 + this.m_baselineOffset;
					float num57 = this.m_maxAscender - (num56 - this.m_lineOffset);
					float num58 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? this.m_cached_Ellipsis_Character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * num53;
					float num59 = ((this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - num54 - num55, this.m_width) : (num8 + 0.0001f - num54 - num55));
					if (num58 < num59 * (flag13 ? 1.05f : 1f) && num57 < num9 + 0.0001f)
					{
						base.SaveWordWrappingState(ref this.m_SavedEllipsisState, num13, this.m_characterCount);
					}
				}
				this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = this.m_lineNumber;
				this.m_textInfo.characterInfo[this.m_characterCount].pageNumber = this.m_pageNumber;
				if ((num14 != 10 && num14 != 11 && num14 != 13 && !flag11) || this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
				{
					this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
				}
				if (num14 == 9)
				{
					float num60 = this.m_currentFontAsset.m_FaceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * num2;
					float num61 = Mathf.Ceil(this.m_xAdvance / num60) * num60;
					this.m_xAdvance = ((num61 > this.m_xAdvance) ? num61 : (this.m_xAdvance + num60));
				}
				else if (this.m_monoSpacing != 0f)
				{
					this.m_xAdvance += (this.m_monoSpacing - num25 + (this.m_currentFontAsset.normalSpacingOffset + num22) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag12 || num14 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				else if (!this.m_isRightToLeft)
				{
					float num62 = 1f;
					if (this.m_isFXMatrixSet)
					{
						num62 = this.m_FXMatrix.lossyScale.x;
					}
					this.m_xAdvance += ((metrics.horizontalAdvance * num62 + tmp_GlyphValueRecord.m_XAdvance) * num2 + (this.m_currentFontAsset.normalSpacingOffset + num22 + num5) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag12 || num14 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				else
				{
					this.m_xAdvance -= tmp_GlyphValueRecord.m_XAdvance * num2;
				}
				this.m_textInfo.characterInfo[this.m_characterCount].xAdvance = this.m_xAdvance;
				if (num14 == 13)
				{
					this.m_xAdvance = 0f + this.tag_Indent;
				}
				if (num14 == 10 || num14 == 11 || num14 == 3 || (num14 == 45 && flag11) || this.m_characterCount == totalCharacterCount - 1)
				{
					if (this.m_lineNumber > 0 && !TMP_Math.Approximately(this.m_maxLineAscender, this.m_startOfLineAscender) && !flag5 && !this.m_isNewPage && !flag11)
					{
						float num63 = this.m_maxLineAscender - this.m_startOfLineAscender;
						base.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, num63);
						num30 -= num63;
						this.m_lineOffset += num63;
					}
					this.m_isNewPage = false;
					float num64 = this.m_maxLineAscender - this.m_lineOffset;
					float num65 = this.m_maxLineDescender - this.m_lineOffset;
					this.m_maxDescender = ((this.m_maxDescender < num65) ? this.m_maxDescender : num65);
					if (!flag6)
					{
						num12 = this.m_maxDescender;
					}
					if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
					{
						flag6 = true;
					}
					this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
					this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
					this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = this.m_characterCount);
					this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
					this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
					this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
					this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, num65);
					this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, num64);
					this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x - num4 * num2;
					this.m_textInfo.lineInfo[this.m_lineNumber].width = num10;
					if (this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
					}
					if (this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].isVisible)
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance - (this.m_currentFontAsset.normalSpacingOffset + num22) * num3 - this.m_cSpacing;
					}
					else
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastCharacterOfLine].xAdvance - (this.m_currentFontAsset.normalSpacingOffset + num22) * num3 - this.m_cSpacing;
					}
					this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
					this.m_textInfo.lineInfo[this.m_lineNumber].ascender = num64;
					this.m_textInfo.lineInfo[this.m_lineNumber].descender = num65;
					this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = num64 - num65 + num6 * num;
					if (num14 == 10 || num14 == 11 || num14 == 45)
					{
						base.SaveWordWrappingState(ref this.m_SavedLineState, num13, this.m_characterCount);
						this.m_lineNumber++;
						flag4 = true;
						flag8 = false;
						flag7 = true;
						this.m_firstCharacterOfLine = this.m_characterCount + 1;
						this.m_lineVisibleCharacterCount = 0;
						if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
						{
							base.ResizeLineExtents(this.m_lineNumber);
						}
						float num66 = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].ascender + this.m_lineOffset;
						if (this.m_lineHeight == -32767f)
						{
							float num67 = 0f - this.m_maxLineDescender + num66 + (num6 + this.m_lineSpacingDelta) * num + (this.m_lineSpacing + ((num14 == 10) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_lineOffset += num67;
							flag5 = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((num14 == 10) ? this.m_paragraphSpacing : 0f)) * num3;
							flag5 = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = num66;
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
						base.SaveWordWrappingState(ref this.m_SavedLastValidState, num13, this.m_characterCount);
						this.m_characterCount++;
						goto IL_3608;
					}
					if (num14 == 3)
					{
						num13 = this.m_TextParsingBuffer.Length;
					}
				}
				if (this.m_textInfo.characterInfo[this.m_characterCount].isVisible)
				{
					this.m_meshExtents.min.x = Mathf.Min(this.m_meshExtents.min.x, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.x);
					this.m_meshExtents.min.y = Mathf.Min(this.m_meshExtents.min.y, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.y);
					this.m_meshExtents.max.x = Mathf.Max(this.m_meshExtents.max.x, this.m_textInfo.characterInfo[this.m_characterCount].topRight.x);
					this.m_meshExtents.max.y = Mathf.Max(this.m_meshExtents.max.y, this.m_textInfo.characterInfo[this.m_characterCount].topRight.y);
				}
				if (this.m_overflowMode == TextOverflowModes.Page && num14 != 10 && num14 != 11 && num14 != 13)
				{
					if (this.m_pageNumber + 1 > this.m_textInfo.pageInfo.Length)
					{
						TMP_TextInfo.Resize<TMP_PageInfo>(ref this.m_textInfo.pageInfo, this.m_pageNumber + 1, true);
					}
					this.m_textInfo.pageInfo[this.m_pageNumber].ascender = num11;
					this.m_textInfo.pageInfo[this.m_pageNumber].descender = ((num30 < this.m_textInfo.pageInfo[this.m_pageNumber].descender) ? num30 : this.m_textInfo.pageInfo[this.m_pageNumber].descender);
					if (this.m_pageNumber == 0 && this.m_characterCount == 0)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
					}
					else if (this.m_characterCount > 0 && this.m_pageNumber != this.m_textInfo.characterInfo[this.m_characterCount - 1].pageNumber)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber - 1].lastCharacterIndex = this.m_characterCount - 1;
						this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
					}
					else if (this.m_characterCount == totalCharacterCount - 1)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber].lastCharacterIndex = this.m_characterCount;
					}
				}
				if (this.m_enableWordWrapping || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis || this.m_overflowMode == TextOverflowModes.Linked)
				{
					if ((flag12 || num14 == 8203 || num14 == 45 || num14 == 173) && (!this.m_isNonBreakingSpace || flag8) && num14 != 160 && num14 != 8199 && num14 != 8209 && num14 != 8239 && num14 != 8288)
					{
						base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
						flag7 = false;
						flag9 = false;
					}
					else if ((!this.m_isNonBreakingSpace && ((num14 > 4352 && num14 < 4607) || (num14 > 43360 && num14 < 43391) || (num14 > 44032 && num14 < 55295)) && !TMP_Settings.useModernHangulLineBreakingRules) || (num14 > 11904 && num14 < 40959) || (num14 > 63744 && num14 < 64255) || (num14 > 65072 && num14 < 65103) || (num14 > 65280 && num14 < 65519))
					{
						bool flag14 = TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num14);
						bool flag15 = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.ContainsKey((int)this.m_textInfo.characterInfo[this.m_characterCount + 1].character);
						if (flag7 || !flag14)
						{
							if (!flag15)
							{
								base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
								flag7 = false;
							}
							if (flag7)
							{
								base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
							}
						}
						flag9 = true;
					}
					else if (flag9)
					{
						if (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num14))
						{
							base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
						}
						flag9 = false;
					}
					else if (flag7)
					{
						base.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_characterCount);
						flag9 = false;
					}
				}
				base.SaveWordWrappingState(ref this.m_SavedLastValidState, num13, this.m_characterCount);
				this.m_characterCount++;
				goto IL_3608;
			}
			float num68 = this.m_maxFontSize - this.m_minFontSize;
			if (this.m_enableAutoSizing && num68 > 0.051f && this.m_fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = this.m_fontSize;
				float num69 = Mathf.Max((this.m_maxFontSize - this.m_fontSize) / 2f, 0.05f);
				this.m_fontSize += num69;
				this.m_fontSize = Mathf.Min((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return;
			}
			this.m_IsAutoSizePointSizeSet = true;
			if (this.m_AutoSizeIterationCount >= this.m_AutoSizeMaxIterationCount)
			{
				Debug.Log(string.Concat(new object[] { "Auto Size Iteration Count: ", this.m_AutoSizeIterationCount, ". Final Point Size: ", this.m_fontSize }));
			}
			if (this.m_characterCount == 0)
			{
				this.ClearMesh(true);
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			int num70 = this.m_materialReferences[0].referenceCount * ((!this.m_isVolumetricText) ? 4 : 8);
			this.m_textInfo.meshInfo[0].Clear(false);
			Vector3 vector11 = Vector3.zero;
			Vector3[] rectTransformCorners = this.m_RectTransformCorners;
			VerticalAlignmentOptions verticalAlignment = this.m_VerticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							if (this.m_overflowMode != TextOverflowModes.Page)
							{
								vector11 = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - num12 + margin.w, 0f);
							}
							else
							{
								vector11 = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].descender + margin.w, 0f);
							}
						}
					}
					else if (this.m_overflowMode != TextOverflowModes.Page)
					{
						vector11 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxAscender + margin.y + num12 - margin.w) / 2f, 0f);
					}
					else
					{
						vector11 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_textInfo.pageInfo[num7].ascender + margin.y + this.m_textInfo.pageInfo[num7].descender - margin.w) / 2f, 0f);
					}
				}
				else if (this.m_overflowMode != TextOverflowModes.Page)
				{
					vector11 = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_maxAscender - margin.y, 0f);
				}
				else
				{
					vector11 = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].ascender - margin.y, 0f);
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						vector11 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxCapHeight - margin.y - margin.w) / 2f, 0f);
					}
				}
				else
				{
					vector11 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_meshExtents.max.y + margin.y + this.m_meshExtents.min.y - margin.w) / 2f, 0f);
				}
			}
			else
			{
				vector11 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f, 0f);
			}
			Vector3 vector12 = Vector3.zero;
			Vector3 vector13 = Vector3.zero;
			int num71 = 0;
			int num72 = 0;
			int num73 = 0;
			int num74 = 0;
			int num75 = 0;
			bool flag16 = false;
			bool flag17 = false;
			int num76 = 0;
			float num77 = (this.m_previousLossyScaleY = this.transform.lossyScale.y);
			Color32 color2 = Color.white;
			Color32 color3 = Color.white;
			HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), TMP_Offset.zero);
			float num78 = 0f;
			float num79 = 0f;
			float num80 = 0f;
			float num81 = 0f;
			float num82 = TMP_Text.k_LargePositiveFloat;
			int num83 = 0;
			float num84 = 0f;
			float num85 = 0f;
			float num86 = 0f;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			int i = 0;
			while (i < this.m_characterCount)
			{
				TMP_FontAsset fontAsset = characterInfo[i].fontAsset;
				char character = characterInfo[i].character;
				int lineNumber4 = characterInfo[i].lineNumber;
				TMP_LineInfo tmp_LineInfo = this.m_textInfo.lineInfo[lineNumber4];
				num74 = lineNumber4 + 1;
				HorizontalAlignmentOptions alignment = tmp_LineInfo.alignment;
				if (alignment <= HorizontalAlignmentOptions.Justified)
				{
					switch (alignment)
					{
					case HorizontalAlignmentOptions.Left:
						if (!this.m_isRightToLeft)
						{
							vector12 = new Vector3(0f + tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector12 = new Vector3(0f - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						break;
					case HorizontalAlignmentOptions.Center:
						vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - tmp_LineInfo.maxAdvance / 2f, 0f, 0f);
						break;
					case (HorizontalAlignmentOptions)3:
						break;
					case HorizontalAlignmentOptions.Right:
						if (!this.m_isRightToLeft)
						{
							vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						else
						{
							vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						break;
					default:
						if (alignment == HorizontalAlignmentOptions.Justified)
						{
							goto IL_3DF4;
						}
						break;
					}
				}
				else
				{
					if (alignment == HorizontalAlignmentOptions.Flush)
					{
						goto IL_3DF4;
					}
					if (alignment == HorizontalAlignmentOptions.Geometry)
					{
						vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - (tmp_LineInfo.lineExtents.min.x + tmp_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
					}
				}
				IL_4084:
				vector13 = vector11 + vector12;
				bool isVisible = characterInfo[i].isVisible;
				if (isVisible)
				{
					TMP_TextElementType elementType = characterInfo[i].elementType;
					if (elementType != TMP_TextElementType.Character)
					{
						if (elementType != TMP_TextElementType.Sprite)
						{
						}
					}
					else
					{
						Extents lineExtents = tmp_LineInfo.lineExtents;
						float num87 = this.m_uvLineOffset * (float)lineNumber4 % 1f;
						switch (this.m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.x = 0f;
							characterInfo[i].vertex_TL.uv2.x = 0f;
							characterInfo[i].vertex_TR.uv2.x = 1f;
							characterInfo[i].vertex_BR.uv2.x = 1f;
							break;
						case TextureMappingOptions.Line:
							if (this.m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num87;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num87;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num87;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num87;
							}
							else
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
							characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
							characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
							characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector12.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num87;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (this.m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfo[i].vertex_BL.uv2.y = 0f;
								characterInfo[i].vertex_TL.uv2.y = 1f;
								characterInfo[i].vertex_TR.uv2.y = 0f;
								characterInfo[i].vertex_BR.uv2.y = 1f;
								break;
							case TextureMappingOptions.Line:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num87;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num87;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num87;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num87;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num88 = (1f - (characterInfo[i].vertex_BL.uv2.y + characterInfo[i].vertex_TL.uv2.y) * characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.x = characterInfo[i].vertex_BL.uv2.y * characterInfo[i].aspectRatio + num88 + num87;
							characterInfo[i].vertex_TL.uv2.x = characterInfo[i].vertex_BL.uv2.x;
							characterInfo[i].vertex_TR.uv2.x = characterInfo[i].vertex_TL.uv2.y * characterInfo[i].aspectRatio + num88 + num87;
							characterInfo[i].vertex_BR.uv2.x = characterInfo[i].vertex_TR.uv2.x;
							break;
						}
						}
						switch (this.m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.y = 0f;
							characterInfo[i].vertex_TL.uv2.y = 1f;
							characterInfo[i].vertex_TR.uv2.y = 1f;
							characterInfo[i].vertex_BR.uv2.y = 0f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num89 = (1f - (characterInfo[i].vertex_BL.uv2.x + characterInfo[i].vertex_TR.uv2.x) / characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.y = num89 + characterInfo[i].vertex_BL.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_TL.uv2.y = num89 + characterInfo[i].vertex_TR.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							break;
						}
						}
						num78 = characterInfo[i].scale * Mathf.Abs(num77) * (1f - this.m_charWidthAdjDelta);
						if (!characterInfo[i].isUsingAlternateTypeface && (characterInfo[i].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num78 *= -1f;
						}
						float num90 = characterInfo[i].vertex_BL.uv2.x;
						float num91 = characterInfo[i].vertex_BL.uv2.y;
						float num92 = characterInfo[i].vertex_TR.uv2.x;
						float num93 = characterInfo[i].vertex_TR.uv2.y;
						float num94 = (float)((int)num90);
						float num95 = (float)((int)num91);
						num90 -= num94;
						num92 -= num94;
						num91 -= num95;
						num93 -= num95;
						characterInfo[i].vertex_BL.uv2.x = base.PackUV(num90, num91);
						characterInfo[i].vertex_BL.uv2.y = num78;
						characterInfo[i].vertex_TL.uv2.x = base.PackUV(num90, num93);
						characterInfo[i].vertex_TL.uv2.y = num78;
						characterInfo[i].vertex_TR.uv2.x = base.PackUV(num92, num93);
						characterInfo[i].vertex_TR.uv2.y = num78;
						characterInfo[i].vertex_BR.uv2.x = base.PackUV(num92, num91);
						characterInfo[i].vertex_BR.uv2.y = num78;
					}
					if (i < this.m_maxVisibleCharacters && num73 < this.m_maxVisibleWords && lineNumber4 < this.m_maxVisibleLines && this.m_overflowMode != TextOverflowModes.Page)
					{
						TMP_CharacterInfo[] array = characterInfo;
						int num96 = i;
						array[num96].vertex_BL.position = array[num96].vertex_BL.position + vector13;
						TMP_CharacterInfo[] array2 = characterInfo;
						int num97 = i;
						array2[num97].vertex_TL.position = array2[num97].vertex_TL.position + vector13;
						TMP_CharacterInfo[] array3 = characterInfo;
						int num98 = i;
						array3[num98].vertex_TR.position = array3[num98].vertex_TR.position + vector13;
						TMP_CharacterInfo[] array4 = characterInfo;
						int num99 = i;
						array4[num99].vertex_BR.position = array4[num99].vertex_BR.position + vector13;
					}
					else if (i < this.m_maxVisibleCharacters && num73 < this.m_maxVisibleWords && lineNumber4 < this.m_maxVisibleLines && this.m_overflowMode == TextOverflowModes.Page && characterInfo[i].pageNumber == num7)
					{
						TMP_CharacterInfo[] array5 = characterInfo;
						int num100 = i;
						array5[num100].vertex_BL.position = array5[num100].vertex_BL.position + vector13;
						TMP_CharacterInfo[] array6 = characterInfo;
						int num101 = i;
						array6[num101].vertex_TL.position = array6[num101].vertex_TL.position + vector13;
						TMP_CharacterInfo[] array7 = characterInfo;
						int num102 = i;
						array7[num102].vertex_TR.position = array7[num102].vertex_TR.position + vector13;
						TMP_CharacterInfo[] array8 = characterInfo;
						int num103 = i;
						array8[num103].vertex_BR.position = array8[num103].vertex_BR.position + vector13;
					}
					else
					{
						characterInfo[i].vertex_BL.position = Vector3.zero;
						characterInfo[i].vertex_TL.position = Vector3.zero;
						characterInfo[i].vertex_TR.position = Vector3.zero;
						characterInfo[i].vertex_BR.position = Vector3.zero;
						characterInfo[i].isVisible = false;
					}
					if (elementType == TMP_TextElementType.Character)
					{
						this.FillCharacterVertexBuffers(i, num71, this.m_isVolumetricText);
					}
					else if (elementType == TMP_TextElementType.Sprite)
					{
						this.FillSpriteVertexBuffers(i, num72);
					}
				}
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num104 = i;
				characterInfo2[num104].bottomLeft = characterInfo2[num104].bottomLeft + vector13;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num105 = i;
				characterInfo3[num105].topLeft = characterInfo3[num105].topLeft + vector13;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num106 = i;
				characterInfo4[num106].topRight = characterInfo4[num106].topRight + vector13;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num107 = i;
				characterInfo5[num107].bottomRight = characterInfo5[num107].bottomRight + vector13;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num108 = i;
				characterInfo6[num108].origin = characterInfo6[num108].origin + vector13.x;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num109 = i;
				characterInfo7[num109].xAdvance = characterInfo7[num109].xAdvance + vector13.x;
				TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
				int num110 = i;
				characterInfo8[num110].ascender = characterInfo8[num110].ascender + vector13.y;
				TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
				int num111 = i;
				characterInfo9[num111].descender = characterInfo9[num111].descender + vector13.y;
				TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
				int num112 = i;
				characterInfo10[num112].baseLine = characterInfo10[num112].baseLine + vector13.y;
				if (lineNumber4 != num75 || i == this.m_characterCount - 1)
				{
					if (lineNumber4 != num75)
					{
						TMP_LineInfo[] lineInfo4 = this.m_textInfo.lineInfo;
						int num113 = num75;
						lineInfo4[num113].baseline = lineInfo4[num113].baseline + vector13.y;
						TMP_LineInfo[] lineInfo5 = this.m_textInfo.lineInfo;
						int num114 = num75;
						lineInfo5[num114].ascender = lineInfo5[num114].ascender + vector13.y;
						TMP_LineInfo[] lineInfo6 = this.m_textInfo.lineInfo;
						int num115 = num75;
						lineInfo6[num115].descender = lineInfo6[num115].descender + vector13.y;
						TMP_LineInfo[] lineInfo7 = this.m_textInfo.lineInfo;
						int num116 = num75;
						lineInfo7[num116].maxAdvance = lineInfo7[num116].maxAdvance + vector13.x;
						this.m_textInfo.lineInfo[num75].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num75].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[num75].descender);
						this.m_textInfo.lineInfo[num75].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num75].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[num75].ascender);
					}
					if (i == this.m_characterCount - 1)
					{
						TMP_LineInfo[] lineInfo8 = this.m_textInfo.lineInfo;
						int num117 = lineNumber4;
						lineInfo8[num117].baseline = lineInfo8[num117].baseline + vector13.y;
						TMP_LineInfo[] lineInfo9 = this.m_textInfo.lineInfo;
						int num118 = lineNumber4;
						lineInfo9[num118].ascender = lineInfo9[num118].ascender + vector13.y;
						TMP_LineInfo[] lineInfo10 = this.m_textInfo.lineInfo;
						int num119 = lineNumber4;
						lineInfo10[num119].descender = lineInfo10[num119].descender + vector13.y;
						TMP_LineInfo[] lineInfo11 = this.m_textInfo.lineInfo;
						int num120 = lineNumber4;
						lineInfo11[num120].maxAdvance = lineInfo11[num120].maxAdvance + vector13.x;
						this.m_textInfo.lineInfo[lineNumber4].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber4].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[lineNumber4].descender);
						this.m_textInfo.lineInfo[lineNumber4].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber4].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[lineNumber4].ascender);
					}
				}
				if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
				{
					if (!flag17)
					{
						flag17 = true;
						num76 = i;
					}
					if (flag17 && i == this.m_characterCount - 1)
					{
						int num121 = this.m_textInfo.wordInfo.Length;
						int wordCount = this.m_textInfo.wordCount;
						if (this.m_textInfo.wordCount + 1 > num121)
						{
							TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num121 + 1);
						}
						int num122 = i;
						this.m_textInfo.wordInfo[wordCount].firstCharacterIndex = num76;
						this.m_textInfo.wordInfo[wordCount].lastCharacterIndex = num122;
						this.m_textInfo.wordInfo[wordCount].characterCount = num122 - num76 + 1;
						this.m_textInfo.wordInfo[wordCount].textComponent = this;
						num73++;
						this.m_textInfo.wordCount++;
						TMP_LineInfo[] lineInfo12 = this.m_textInfo.lineInfo;
						int num123 = lineNumber4;
						lineInfo12[num123].wordCount = lineInfo12[num123].wordCount + 1;
					}
				}
				else if ((flag17 || (i == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '\u200b' || i == this.m_characterCount - 1))) && (i <= 0 || i >= characterInfo.Length - 1 || i >= this.m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[i - 1].character) || !char.IsLetterOrDigit(characterInfo[i + 1].character)))
				{
					int num122 = ((i == this.m_characterCount - 1 && char.IsLetterOrDigit(character)) ? i : (i - 1));
					flag17 = false;
					int num124 = this.m_textInfo.wordInfo.Length;
					int wordCount2 = this.m_textInfo.wordCount;
					if (this.m_textInfo.wordCount + 1 > num124)
					{
						TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num124 + 1);
					}
					this.m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num76;
					this.m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num122;
					this.m_textInfo.wordInfo[wordCount2].characterCount = num122 - num76 + 1;
					this.m_textInfo.wordInfo[wordCount2].textComponent = this;
					num73++;
					this.m_textInfo.wordCount++;
					TMP_LineInfo[] lineInfo13 = this.m_textInfo.lineInfo;
					int num125 = lineNumber4;
					lineInfo13[num125].wordCount = lineInfo13[num125].wordCount + 1;
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag18 = true;
					int pageNumber = this.m_textInfo.characterInfo[i].pageNumber;
					this.m_textInfo.characterInfo[i].underlineVertexIndex = num70;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != this.m_pageToDisplay))
					{
						flag18 = false;
					}
					if (!char.IsWhiteSpace(character) && character != '\u200b')
					{
						num81 = Mathf.Max(num81, this.m_textInfo.characterInfo[i].scale);
						num79 = Mathf.Max(num79, Mathf.Abs(num78));
						num82 = Mathf.Min((pageNumber == num83) ? num82 : TMP_Text.k_LargePositiveFloat, this.m_textInfo.characterInfo[i].baseLine + base.font.m_FaceInfo.underlineOffset * num81);
						num83 = pageNumber;
					}
					if (!flag && flag18 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag = true;
						num80 = this.m_textInfo.characterInfo[i].scale;
						if (num81 == 0f)
						{
							num81 = num80;
							num79 = num78;
						}
						zero = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, num82, 0f);
						color2 = this.m_textInfo.characterInfo[i].underlineColor;
					}
					if (flag && this.m_characterCount == 1)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num82, 0f);
						float num126 = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num70, num80, num126, num81, num79, color2);
						num81 = 0f;
						num79 = 0f;
						num82 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						float num126;
						if (char.IsWhiteSpace(character) || character == '\u200b')
						{
							int lastVisibleCharacterIndex = tmp_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num82, 0f);
							num126 = this.m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num82, 0f);
							num126 = this.m_textInfo.characterInfo[i].scale;
						}
						flag = false;
						this.DrawUnderlineMesh(zero, zero2, ref num70, num80, num126, num81, num79, color2);
						num81 = 0f;
						num79 = 0f;
						num82 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && !flag18)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num82, 0f);
						float num126 = this.m_textInfo.characterInfo[i - 1].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num70, num80, num126, num81, num79, color2);
						num81 = 0f;
						num79 = 0f;
						num82 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && i < this.m_characterCount - 1 && !color2.Compare(this.m_textInfo.characterInfo[i + 1].underlineColor))
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num82, 0f);
						float num126 = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num70, num80, num126, num81, num79, color2);
						num81 = 0f;
						num79 = 0f;
						num82 = TMP_Text.k_LargePositiveFloat;
					}
				}
				else if (flag)
				{
					flag = false;
					zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num82, 0f);
					float num126 = this.m_textInfo.characterInfo[i - 1].scale;
					this.DrawUnderlineMesh(zero, zero2, ref num70, num80, num126, num81, num79, color2);
					num81 = 0f;
					num79 = 0f;
					num82 = TMP_Text.k_LargePositiveFloat;
				}
				bool flag19 = (this.m_textInfo.characterInfo[i].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = fontAsset.m_FaceInfo.strikethroughOffset;
				if (flag19)
				{
					bool flag20 = true;
					this.m_textInfo.characterInfo[i].strikethroughVertexIndex = num70;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && this.m_textInfo.characterInfo[i].pageNumber + 1 != this.m_pageToDisplay))
					{
						flag20 = false;
					}
					if (!flag2 && flag20 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag2 = true;
						num84 = this.m_textInfo.characterInfo[i].pointSize;
						num85 = this.m_textInfo.characterInfo[i].scale;
						zero3 = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num85, 0f);
						color3 = this.m_textInfo.characterInfo[i].strikethroughColor;
						num86 = this.m_textInfo.characterInfo[i].baseLine;
					}
					if (flag2 && this.m_characterCount == 1)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num85, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
					}
					else if (flag2 && i == tmp_LineInfo.lastCharacterIndex)
					{
						if (char.IsWhiteSpace(character) || character == '\u200b')
						{
							int lastVisibleCharacterIndex2 = tmp_LineInfo.lastVisibleCharacterIndex;
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num85, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num85, 0f);
						}
						flag2 = false;
						this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
					}
					else if (flag2 && i < this.m_characterCount && (this.m_textInfo.characterInfo[i + 1].pointSize != num84 || !TMP_Math.Approximately(this.m_textInfo.characterInfo[i + 1].baseLine + vector13.y, num86)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tmp_LineInfo.lastVisibleCharacterIndex;
						if (i > lastVisibleCharacterIndex3)
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num85, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num85, 0f);
						}
						this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
					}
					else if (flag2 && i < this.m_characterCount && fontAsset.GetInstanceID() != characterInfo[i + 1].fontAsset.GetInstanceID())
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num85, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
					}
					else if (flag2 && !flag20)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num85, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num85, 0f);
					this.DrawUnderlineMesh(zero3, zero4, ref num70, num85, num85, num85, num78, color3);
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Highlight) == FontStyles.Highlight)
				{
					bool flag21 = true;
					int pageNumber2 = this.m_textInfo.characterInfo[i].pageNumber;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != this.m_pageToDisplay))
					{
						flag21 = false;
					}
					if (!flag3 && flag21 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag3 = true;
						vector = TMP_Text.k_LargePositiveVector2;
						vector2 = TMP_Text.k_LargeNegativeVector2;
						highlightState = this.m_textInfo.characterInfo[i].highlightState;
					}
					if (flag3)
					{
						TMP_CharacterInfo tmp_CharacterInfo = this.m_textInfo.characterInfo[i];
						HighlightState highlightState2 = tmp_CharacterInfo.highlightState;
						bool flag22 = false;
						if (highlightState != tmp_CharacterInfo.highlightState)
						{
							vector2.x = (vector2.x - highlightState.padding.right + tmp_CharacterInfo.bottomLeft.x) / 2f;
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender);
							this.DrawTextHighlight(vector, vector2, ref num70, highlightState.color);
							flag3 = true;
							vector = new Vector2(vector2.x, tmp_CharacterInfo.descender - highlightState2.padding.bottom);
							vector2 = new Vector2(tmp_CharacterInfo.topRight.x + highlightState2.padding.right, tmp_CharacterInfo.ascender + highlightState2.padding.top);
							highlightState = tmp_CharacterInfo.highlightState;
							flag22 = true;
						}
						if (!flag22)
						{
							vector.x = Mathf.Min(vector.x, tmp_CharacterInfo.bottomLeft.x - highlightState.padding.left);
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender - highlightState.padding.bottom);
							vector2.x = Mathf.Max(vector2.x, tmp_CharacterInfo.topRight.x + highlightState.padding.right);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender + highlightState.padding.top);
						}
					}
					if (flag3 && this.m_characterCount == 1)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num70, highlightState.color);
					}
					else if (flag3 && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num70, highlightState.color);
					}
					else if (flag3 && !flag21)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num70, highlightState.color);
					}
				}
				else if (flag3)
				{
					flag3 = false;
					this.DrawTextHighlight(vector, vector2, ref num70, highlightState.color);
				}
				num75 = lineNumber4;
				i++;
				continue;
				IL_3DF4:
				if (character == '\n' || character == '\u00ad' || character == '\u200b' || character == '\u2060' || character == '\u0003')
				{
					goto IL_4084;
				}
				char character2 = characterInfo[tmp_LineInfo.lastCharacterIndex].character;
				bool flag23 = (alignment & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush;
				if ((!char.IsControl(character2) && lineNumber4 < this.m_lineNumber) || flag23 || tmp_LineInfo.maxAdvance > tmp_LineInfo.width)
				{
					if (lineNumber4 != num75 || i == 0 || i == this.m_firstVisibleCharacter)
					{
						if (!this.m_isRightToLeft)
						{
							vector12 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						flag16 = char.IsSeparator(character);
						goto IL_4084;
					}
					float num127 = ((!this.m_isRightToLeft) ? (tmp_LineInfo.width - tmp_LineInfo.maxAdvance) : (tmp_LineInfo.width + tmp_LineInfo.maxAdvance));
					int num128 = tmp_LineInfo.visibleCharacterCount - 1 + tmp_LineInfo.controlCharacterCount;
					int num129 = (characterInfo[tmp_LineInfo.lastCharacterIndex].isVisible ? tmp_LineInfo.spaceCount : (tmp_LineInfo.spaceCount - 1)) - tmp_LineInfo.controlCharacterCount;
					if (flag16)
					{
						num129--;
						num128++;
					}
					float num130 = ((num129 > 0) ? this.m_wordWrappingRatios : 1f);
					if (num129 < 1)
					{
						num129 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!this.m_isRightToLeft)
						{
							vector12 += new Vector3(num127 * (1f - num130) / (float)num129, 0f, 0f);
							goto IL_4084;
						}
						vector12 -= new Vector3(num127 * (1f - num130) / (float)num129, 0f, 0f);
						goto IL_4084;
					}
					else
					{
						if (!this.m_isRightToLeft)
						{
							vector12 += new Vector3(num127 * num130 / (float)num128, 0f, 0f);
							goto IL_4084;
						}
						vector12 -= new Vector3(num127 * num130 / (float)num128, 0f, 0f);
						goto IL_4084;
					}
				}
				else
				{
					if (!this.m_isRightToLeft)
					{
						vector12 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						goto IL_4084;
					}
					vector12 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
					goto IL_4084;
				}
			}
			this.m_textInfo.characterCount = this.m_characterCount;
			this.m_textInfo.spriteCount = this.m_spriteCount;
			this.m_textInfo.lineCount = num74;
			this.m_textInfo.wordCount = ((num73 != 0 && this.m_characterCount > 0) ? num73 : 1);
			this.m_textInfo.pageCount = this.m_pageNumber + 1;
			if (this.m_renderMode == TextRenderFlags.Render && this.IsActive())
			{
				if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
				{
					this.m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
				}
				this.m_mesh.MarkDynamic();
				this.m_mesh.vertices = this.m_textInfo.meshInfo[0].vertices;
				this.m_mesh.uv = this.m_textInfo.meshInfo[0].uvs0;
				this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				this.m_mesh.colors32 = this.m_textInfo.meshInfo[0].colors32;
				this.m_mesh.RecalculateBounds();
				for (int j = 1; j < this.m_textInfo.materialCount; j++)
				{
					this.m_textInfo.meshInfo[j].ClearUnusedVertices();
					if (!(this.m_subTextObjects[j] == null))
					{
						if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
						{
							this.m_textInfo.meshInfo[j].SortGeometry(VertexSortingOrder.Reverse);
						}
						this.m_subTextObjects[j].mesh.vertices = this.m_textInfo.meshInfo[j].vertices;
						this.m_subTextObjects[j].mesh.uv = this.m_textInfo.meshInfo[j].uvs0;
						this.m_subTextObjects[j].mesh.uv2 = this.m_textInfo.meshInfo[j].uvs2;
						this.m_subTextObjects[j].mesh.colors32 = this.m_textInfo.meshInfo[j].colors32;
						this.m_subTextObjects[j].mesh.RecalculateBounds();
					}
				}
			}
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0002C478 File Offset: 0x0002A678
		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (this.m_rectTransform == null)
			{
				this.m_rectTransform = base.rectTransform;
			}
			this.m_rectTransform.GetLocalCorners(this.m_RectTransformCorners);
			return this.m_RectTransformCorners;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		private void SetMeshFilters(bool state)
		{
			if (this.m_meshFilter != null)
			{
				if (state)
				{
					this.m_meshFilter.sharedMesh = this.m_mesh;
				}
				else
				{
					this.m_meshFilter.sharedMesh = null;
				}
			}
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].meshFilter != null)
				{
					if (state)
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = this.m_subTextObjects[num].mesh;
					}
					else
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = null;
					}
				}
				num++;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0002C558 File Offset: 0x0002A758
		protected override void SetActiveSubMeshes(bool state)
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].enabled != state)
				{
					this.m_subTextObjects[num].enabled = state;
				}
				num++;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002C5A8 File Offset: 0x0002A7A8
		protected override void ClearSubMeshObjects()
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Debug.Log("Destroying Sub Text object[" + num + "].");
				global::UnityEngine.Object.DestroyImmediate(this.m_subTextObjects[num]);
				num++;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002C600 File Offset: 0x0002A800
		protected override Bounds GetCompoundBounds()
		{
			Bounds bounds = this.m_mesh.bounds;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Bounds bounds2 = this.m_subTextObjects[num].mesh.bounds;
				min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
				min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
				max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
				max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
				num++;
			}
			Vector3 vector = (min + max) / 2f;
			Vector2 vector2 = max - min;
			return new Bounds(vector, vector2);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002C754 File Offset: 0x0002A954
		private void UpdateSDFScale(float scaleDelta)
		{
			if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity || scaleDelta == float.NegativeInfinity)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
				return;
			}
			for (int i = 0; i < this.m_textInfo.materialCount; i++)
			{
				TMP_MeshInfo tmp_MeshInfo = this.m_textInfo.meshInfo[i];
				for (int j = 0; j < tmp_MeshInfo.uvs2.Length; j++)
				{
					Vector2[] uvs = tmp_MeshInfo.uvs2;
					int num = j;
					uvs[num].y = uvs[num].y * Mathf.Abs(scaleDelta);
				}
			}
			for (int k = 0; k < this.m_textInfo.meshInfo.Length; k++)
			{
				if (k == 0)
				{
					this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				}
				else
				{
					this.m_subTextObjects[k].mesh.uv2 = this.m_textInfo.meshInfo[k].uvs2;
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0002C843 File Offset: 0x0002AA43
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0002C850 File Offset: 0x0002AA50
		public int sortingLayerID
		{
			get
			{
				return this.m_renderer.sortingLayerID;
			}
			set
			{
				this.m_renderer.sortingLayerID = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0002C85E File Offset: 0x0002AA5E
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0002C86B File Offset: 0x0002AA6B
		public int sortingOrder
		{
			get
			{
				return this.m_renderer.sortingOrder;
			}
			set
			{
				this.m_renderer.sortingOrder = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0002C879 File Offset: 0x0002AA79
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0002C881 File Offset: 0x0002AA81
		public override bool autoSizeTextContainer
		{
			get
			{
				return this.m_autoSizeTextContainer;
			}
			set
			{
				if (this.m_autoSizeTextContainer == value)
				{
					return;
				}
				this.m_autoSizeTextContainer = value;
				if (this.m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00013544 File Offset: 0x00011744
		[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
		public TextContainer textContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001328C File Offset: 0x0001148C
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		public Renderer renderer
		{
			get
			{
				if (this.m_renderer == null)
				{
					this.m_renderer = base.GetComponent<Renderer>();
				}
				return this.m_renderer;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0002C8CA File Offset: 0x0002AACA
		public override Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
					this.meshFilter.mesh = this.m_mesh;
				}
				return this.m_mesh;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0002C909 File Offset: 0x0002AB09
		public MeshFilter meshFilter
		{
			get
			{
				if (this.m_meshFilter == null)
				{
					this.m_meshFilter = base.GetComponent<MeshFilter>();
				}
				return this.m_meshFilter;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0002C92B File Offset: 0x0002AB2B
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0002C933 File Offset: 0x0002AB33
		public MaskingTypes maskType
		{
			get
			{
				return this.m_maskType;
			}
			set
			{
				this.m_maskType = value;
				this.SetMask(this.m_maskType);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002C948 File Offset: 0x0002AB48
		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002C958 File Offset: 0x0002AB58
		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002C96B File Offset: 0x0002AB6B
		public override void SetVerticesDirty()
		{
			if (this.m_verticesAlreadyDirty || this == null || !this.IsActive())
			{
				return;
			}
			TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
			this.m_verticesAlreadyDirty = true;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002C994 File Offset: 0x0002AB94
		public override void SetLayoutDirty()
		{
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			if (this.m_layoutAlreadyDirty || this == null || !this.IsActive())
			{
				return;
			}
			this.m_layoutAlreadyDirty = true;
			this.m_isLayoutDirty = true;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000123A0 File Offset: 0x000105A0
		public override void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002C9CC File Offset: 0x0002ABCC
		public override void SetAllDirty()
		{
			this.m_isInputParsingRequired = true;
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		public override void Rebuild(CanvasUpdate update)
		{
			if (this == null)
			{
				return;
			}
			if (update == CanvasUpdate.Prelayout)
			{
				if (this.m_autoSizeTextContainer)
				{
					this.m_rectTransform.sizeDelta = base.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
					return;
				}
			}
			else if (update == CanvasUpdate.PreRender)
			{
				this.OnPreRenderObject();
				this.m_verticesAlreadyDirty = false;
				this.m_layoutAlreadyDirty = false;
				if (!this.m_isMaterialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_isMaterialDirty = false;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002CA54 File Offset: 0x0002AC54
		protected override void UpdateMaterial()
		{
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_renderer == null)
			{
				this.m_renderer = this.renderer;
			}
			if (this.m_renderer.sharedMaterial.GetInstanceID() != this.m_sharedMaterial.GetInstanceID())
			{
				this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		public override void UpdateMeshPadding()
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_havePropertiesChanged = true;
			this.checkPaddingRequired = false;
			if (this.m_textInfo == null)
			{
				return;
			}
			for (int i = 1; i < this.m_textInfo.materialCount; i++)
			{
				this.m_subTextObjects[i].UpdateMeshPadding(this.m_enableExtraPadding, this.m_isUsingBold);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002CB39 File Offset: 0x0002AD39
		public override void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
			this.m_havePropertiesChanged = true;
			this.m_ignoreActiveState = ignoreActiveState;
			this.m_isInputParsingRequired = this.m_isInputParsingRequired || forceTextReparsing;
			this.OnPreRenderObject();
			this.m_verticesAlreadyDirty = false;
			this.m_layoutAlreadyDirty = false;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002CB6F File Offset: 0x0002AD6F
		public override TMP_TextInfo GetTextInfo(string text)
		{
			base.StringToCharArray(text, ref this.m_TextParsingBuffer);
			this.SetArraySizes(this.m_TextParsingBuffer);
			this.m_renderMode = TextRenderFlags.DontRender;
			this.ComputeMarginSize();
			this.GenerateTextMesh();
			this.m_renderMode = TextRenderFlags.Render;
			return base.textInfo;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		public override void ClearMesh(bool updateMesh)
		{
			if (this.m_textInfo.meshInfo[0].mesh == null)
			{
				this.m_textInfo.meshInfo[0].mesh = this.m_mesh;
			}
			this.m_textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002CC03 File Offset: 0x0002AE03
		public override void UpdateGeometry(Mesh mesh, int index)
		{
			mesh.RecalculateBounds();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					mesh = this.m_subTextObjects[i].mesh;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
				{
					mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
				{
					mesh.uv = this.m_textInfo.meshInfo[i].uvs0;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
				{
					mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
				{
					mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				}
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002CCE4 File Offset: 0x0002AEE4
		public override void UpdateVertexData()
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					this.m_textInfo.meshInfo[i].ClearUnusedVertices();
					mesh = this.m_subTextObjects[i].mesh;
				}
				mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				mesh.uv = this.m_textInfo.meshInfo[i].uvs0;
				mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002CDB5 File Offset: 0x0002AFB5
		public void UpdateFontAsset()
		{
			this.LoadFontAsset();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
		public void CalculateLayoutInputHorizontal()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_currentAutoSizeMode = this.m_enableAutoSizing;
			if (this.m_isCalculateSizeRequired || this.m_rectTransform.hasChanged)
			{
				this.m_minWidth = 0f;
				this.m_flexibleWidth = 0f;
				if (this.m_enableAutoSizing)
				{
					this.m_fontSize = this.m_fontSizeMax;
				}
				this.m_marginWidth = TMP_Text.k_LargePositiveFloat;
				this.m_marginHeight = TMP_Text.k_LargePositiveFloat;
				if (this.m_isInputParsingRequired || this.m_isTextTruncated)
				{
					base.ParseInputText();
				}
				this.GenerateTextMesh();
				this.m_renderMode = TextRenderFlags.Render;
				this.ComputeMarginSize();
				this.m_isLayoutDirty = true;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002CE70 File Offset: 0x0002B070
		public void CalculateLayoutInputVertical()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_isCalculateSizeRequired || this.m_rectTransform.hasChanged)
			{
				this.m_minHeight = 0f;
				this.m_flexibleHeight = 0f;
				if (this.m_enableAutoSizing)
				{
					this.m_currentAutoSizeMode = true;
					this.m_enableAutoSizing = false;
				}
				this.m_marginHeight = TMP_Text.k_LargePositiveFloat;
				this.GenerateTextMesh();
				this.m_enableAutoSizing = this.m_currentAutoSizeMode;
				this.m_renderMode = TextRenderFlags.Render;
				this.ComputeMarginSize();
				this.m_isLayoutDirty = true;
			}
			this.m_isCalculateSizeRequired = false;
		}

		// Token: 0x040004D3 RID: 1235
		[SerializeField]
		private bool m_hasFontAssetChanged;

		// Token: 0x040004D4 RID: 1236
		private float m_previousLossyScaleY = -1f;

		// Token: 0x040004D5 RID: 1237
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x040004D6 RID: 1238
		private MeshFilter m_meshFilter;

		// Token: 0x040004D7 RID: 1239
		private bool m_isFirstAllocation;

		// Token: 0x040004D8 RID: 1240
		private int m_max_characters = 8;

		// Token: 0x040004D9 RID: 1241
		private int m_max_numberOfLines = 4;

		// Token: 0x040004DA RID: 1242
		protected TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

		// Token: 0x040004DB RID: 1243
		[SerializeField]
		private MaskingTypes m_maskType;

		// Token: 0x040004DC RID: 1244
		private Matrix4x4 m_EnvMapMatrix;

		// Token: 0x040004DD RID: 1245
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x040004DE RID: 1246
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x040004DF RID: 1247
		private bool m_currentAutoSizeMode;
	}
}
