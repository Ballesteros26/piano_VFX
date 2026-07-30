using System;
using System.Collections.Generic;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006A RID: 106
	internal struct TextHandle
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00009260 File Offset: 0x00007460
		public static TextHandle New()
		{
			return new TextHandle
			{
				m_TextInfo = new TextInfo(),
				useLegacy = false,
				m_CurrentGenerationSettings = new TextGenerationSettings(),
				m_CurrentLayoutSettings = new TextGenerationSettings()
			};
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000092A8 File Offset: 0x000074A8
		private static FontAsset GetFontAsset(Font font)
		{
			FontAsset fontAsset = null;
			bool flag = TextHandle.fontAssetCache.TryGetValue(font, ref fontAsset) && fontAsset != null;
			FontAsset fontAsset2;
			if (flag)
			{
				fontAsset2 = fontAsset;
			}
			else
			{
				fontAsset = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, FontAsset.AtlasPopulationMode.Dynamic);
				fontAsset2 = (TextHandle.fontAssetCache[font] = fontAsset);
			}
			return fontAsset2;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009308 File Offset: 0x00007508
		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			bool flag = this.useLegacy;
			Vector2 vector;
			if (flag)
			{
				vector = TextNative.GetCursorPosition(parms.GetTextNativeSettings(scaling), parms.rect, parms.cursorIndex);
			}
			else
			{
				vector = TextGenerator.GetCursorPosition(this.m_TextInfo, parms.rect, parms.cursorIndex);
			}
			return vector;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009358 File Offset: 0x00007558
		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			bool flag = this.useLegacy;
			float num;
			if (flag)
			{
				num = TextNative.ComputeTextWidth(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			}
			else
			{
				this.UpdatePreferredValues(parms);
				num = this.m_PreferredSize.x;
			}
			return num;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009398 File Offset: 0x00007598
		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			bool flag = this.useLegacy;
			float num;
			if (flag)
			{
				num = TextNative.ComputeTextHeight(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			}
			else
			{
				this.UpdatePreferredValues(parms);
				num = this.m_PreferredSize.y;
			}
			return num;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000093D8 File Offset: 0x000075D8
		internal TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			parms.rect = new Rect(Vector2.zero, parms.rect.size);
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode;
			TextInfo textInfo;
			if (flag)
			{
				textInfo = this.m_TextInfo;
			}
			else
			{
				TextHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentGenerationSettings);
				this.m_CurrentGenerationSettings.color = parms.fontColor;
				this.m_CurrentGenerationSettings.inverseYAxis = true;
				this.m_CurrentGenerationSettings.scale = pixelsPerPoint;
				this.m_CurrentGenerationSettings.overflowMode = parms.textOverflowMode;
				this.m_TextInfo.isDirty = true;
				TextGenerator.GenerateText(this.m_CurrentGenerationSettings, this.m_TextInfo);
				this.m_PreviousGenerationSettingsHash = hashCode;
				textInfo = this.m_TextInfo;
			}
			return textInfo;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000094A0 File Offset: 0x000076A0
		private void UpdatePreferredValues(MeshGenerationContextUtils.TextParams parms)
		{
			parms.rect = new Rect(Vector2.zero, parms.rect.size);
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousLayoutSettingsHash == hashCode;
			if (!flag)
			{
				TextHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentLayoutSettings);
				this.m_PreferredSize = TextGenerator.GetPreferredValues(this.m_CurrentLayoutSettings, this.m_TextInfo);
				this.m_PreviousLayoutSettingsHash = hashCode;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009514 File Offset: 0x00007714
		private static void UpdateGenerationSettingsCommon(MeshGenerationContextUtils.TextParams painterParams, TextGenerationSettings settings)
		{
			settings.fontAsset = TextHandle.GetFontAsset(painterParams.font);
			settings.material = settings.fontAsset.material;
			Rect rect = painterParams.rect;
			bool flag = float.IsNaN(rect.width);
			if (flag)
			{
				rect.width = painterParams.wordWrapWidth;
			}
			settings.screenRect = rect;
			settings.text = (string.IsNullOrEmpty(painterParams.text) ? " " : painterParams.text);
			settings.fontSize = (float)((painterParams.fontSize > 0) ? painterParams.fontSize : painterParams.font.fontSize);
			settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(painterParams.fontStyle);
			settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(painterParams.anchor);
			settings.wordWrap = painterParams.wordWrap;
			settings.richText = false;
			settings.overflowMode = TextOverflowMode.Overflow;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000095F0 File Offset: 0x000077F0
		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		// Token: 0x0400013F RID: 319
		public bool useLegacy;

		// Token: 0x04000140 RID: 320
		private static Dictionary<Font, FontAsset> fontAssetCache = new Dictionary<Font, FontAsset>();

		// Token: 0x04000141 RID: 321
		private Vector2 m_PreferredSize;

		// Token: 0x04000142 RID: 322
		private TextInfo m_TextInfo;

		// Token: 0x04000143 RID: 323
		private int m_PreviousGenerationSettingsHash;

		// Token: 0x04000144 RID: 324
		private TextGenerationSettings m_CurrentGenerationSettings;

		// Token: 0x04000145 RID: 325
		private int m_PreviousLayoutSettingsHash;

		// Token: 0x04000146 RID: 326
		private TextGenerationSettings m_CurrentLayoutSettings;
	}
}
