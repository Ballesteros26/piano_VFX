using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200004E RID: 78
	[NativeHeader("Modules/TextCore/Native/FontEngine/FontEngine.h")]
	public sealed class FontEngine
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0001A967 File Offset: 0x00018B67
		internal FontEngine()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001A974 File Offset: 0x00018B74
		internal static FontEngine GetInstance()
		{
			return FontEngine.s_Instance;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001A98C File Offset: 0x00018B8C
		public static FontEngineError InitializeFontEngine()
		{
			return (FontEngineError)FontEngine.InitializeFontEngine_Internal();
		}

		// Token: 0x060001DB RID: 475
		[NativeMethod(Name = "TextCore::FontEngine::InitFontEngine", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int InitializeFontEngine_Internal();

		// Token: 0x060001DC RID: 476 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		public static FontEngineError DestroyFontEngine()
		{
			return (FontEngineError)FontEngine.DestroyFontEngine_Internal();
		}

		// Token: 0x060001DD RID: 477
		[NativeMethod(Name = "TextCore::FontEngine::DestroyFontEngine", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int DestroyFontEngine_Internal();

		// Token: 0x060001DE RID: 478 RVA: 0x0001A9BB File Offset: 0x00018BBB
		internal static void SendCancellationRequest()
		{
			FontEngine.SendCancellationRequest_Internal();
		}

		// Token: 0x060001DF RID: 479
		[NativeMethod(Name = "TextCore::FontEngine::SendCancellationRequest", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void SendCancellationRequest_Internal();

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E0 RID: 480
		internal static extern bool isProcessingDone
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetIsProcessingDone", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001E1 RID: 481
		internal static extern float generationProgress
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetGenerationProgress", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)FontEngine.LoadFontFace_Internal(filePath);
		}

		// Token: 0x060001E3 RID: 483
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_Internal(string filePath);

		// Token: 0x060001E4 RID: 484 RVA: 0x0001A9DC File Offset: 0x00018BDC
		public static FontEngineError LoadFontFace(string filePath, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_Internal(filePath, pointSize);
		}

		// Token: 0x060001E5 RID: 485
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_With_Size_Internal(string filePath, int pointSize);

		// Token: 0x060001E6 RID: 486 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public static FontEngineError LoadFontFace(byte[] sourceFontFile)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError fontEngineError;
			if (flag)
			{
				fontEngineError = FontEngineError.Invalid_File;
			}
			else
			{
				fontEngineError = (FontEngineError)FontEngine.LoadFontFace_FromSourceFontFile_Internal(sourceFontFile);
			}
			return fontEngineError;
		}

		// Token: 0x060001E7 RID: 487
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_FromSourceFontFile_Internal(byte[] sourceFontFile);

		// Token: 0x060001E8 RID: 488 RVA: 0x0001AA20 File Offset: 0x00018C20
		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError fontEngineError;
			if (flag)
			{
				fontEngineError = FontEngineError.Invalid_File;
			}
			else
			{
				fontEngineError = (FontEngineError)FontEngine.LoadFontFace_With_Size_FromSourceFontFile_Internal(sourceFontFile, pointSize);
			}
			return fontEngineError;
		}

		// Token: 0x060001E9 RID: 489
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_With_Size_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize);

		// Token: 0x060001EA RID: 490 RVA: 0x0001AA48 File Offset: 0x00018C48
		public static FontEngineError LoadFontFace(Font font)
		{
			return (FontEngineError)FontEngine.LoadFontFace_FromFont_Internal(font);
		}

		// Token: 0x060001EB RID: 491
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_FromFont_Internal(Font font);

		// Token: 0x060001EC RID: 492 RVA: 0x0001AA60 File Offset: 0x00018C60
		public static FontEngineError LoadFontFace(Font font, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_FromFont_Internal(font, pointSize);
		}

		// Token: 0x060001ED RID: 493
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadFontFace_With_Size_FromFont_Internal(Font font, int pointSize);

		// Token: 0x060001EE RID: 494 RVA: 0x0001AA7C File Offset: 0x00018C7C
		public static FontEngineError SetFaceSize(int pointSize)
		{
			return (FontEngineError)FontEngine.SetFaceSize_Internal(pointSize);
		}

		// Token: 0x060001EF RID: 495
		[NativeMethod(Name = "TextCore::FontEngine::SetFaceSize", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int SetFaceSize_Internal(int pointSize);

		// Token: 0x060001F0 RID: 496 RVA: 0x0001AA94 File Offset: 0x00018C94
		public static FaceInfo GetFaceInfo()
		{
			FaceInfo faceInfo = default(FaceInfo);
			FontEngine.GetFaceInfo_Internal(ref faceInfo);
			return faceInfo;
		}

		// Token: 0x060001F1 RID: 497
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		// Token: 0x060001F2 RID: 498
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern uint GetGlyphIndex(uint unicode);

		// Token: 0x060001F3 RID: 499
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern bool TryGetGlyphIndex(uint unicode, out uint glyphIndex);

		// Token: 0x060001F4 RID: 500 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		internal static FontEngineError LoadGlyph(uint unicode, GlyphLoadFlags flags)
		{
			return (FontEngineError)FontEngine.LoadGlyph_Internal(unicode, flags);
		}

		// Token: 0x060001F5 RID: 501
		[NativeMethod(Name = "TextCore::FontEngine::LoadGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int LoadGlyph_Internal(uint unicode, GlyphLoadFlags loadFlags);

		// Token: 0x060001F6 RID: 502 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphMarshallingStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001F7 RID: 503
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x060001F8 RID: 504 RVA: 0x0001AB0C File Offset: 0x00018D0C
		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphMarshallingStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001F9 RID: 505
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x060001FA RID: 506 RVA: 0x0001AB44 File Offset: 0x00018D44
		internal static bool TryPackGlyphInAtlas(Glyph glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag4 = FontEngine.TryPackGlyphInAtlas_Internal(ref glyphMarshallingStruct, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2);
			bool flag7;
			if (flag4)
			{
				glyph.glyphRect = glyphMarshallingStruct.glyphRect;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				flag7 = true;
			}
			else
			{
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x060001FB RID: 507
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryPackGlyphInAtlas_Internal(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x060001FC RID: 508 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		internal static bool TryPackGlyphsInAtlas(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			int count = glyphsToAdd.Count;
			int count2 = glyphsAdded.Count;
			int count3 = freeGlyphRects.Count;
			int count4 = usedGlyphRects.Count;
			int num = count + count2 + count3 + count4;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < num || FontEngine.s_GlyphMarshallingStruct_OUT.Length < num || FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num2];
				FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num2];
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			for (int i = 0; i < num; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyphsToAdd[i]);
					FontEngine.s_GlyphMarshallingStruct_IN[i] = glyphMarshallingStruct;
					bool flag3 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct.index);
					if (flag3)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct.index, glyphsToAdd[i]);
					}
				}
				bool flag4 = i < count2;
				if (flag4)
				{
					GlyphMarshallingStruct glyphMarshallingStruct2 = new GlyphMarshallingStruct(glyphsAdded[i]);
					FontEngine.s_GlyphMarshallingStruct_OUT[i] = glyphMarshallingStruct2;
					bool flag5 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct2.index);
					if (flag5)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct2.index, glyphsAdded[i]);
					}
				}
				bool flag6 = i < count3;
				if (flag6)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag7 = i < count4;
				if (flag7)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag8 = FontEngine.TryPackGlyphsInAtlas_Internal(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref count2, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count3, FontEngine.s_UsedGlyphRects, ref count4);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			for (int j = 0; j < num; j++)
			{
				bool flag9 = j < count;
				if (flag9)
				{
					GlyphMarshallingStruct glyphMarshallingStruct3 = FontEngine.s_GlyphMarshallingStruct_IN[j];
					Glyph glyph = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct3.index];
					glyph.metrics = glyphMarshallingStruct3.metrics;
					glyph.glyphRect = glyphMarshallingStruct3.glyphRect;
					glyph.scale = glyphMarshallingStruct3.scale;
					glyph.atlasIndex = glyphMarshallingStruct3.atlasIndex;
					glyphsToAdd.Add(glyph);
				}
				bool flag10 = j < count2;
				if (flag10)
				{
					GlyphMarshallingStruct glyphMarshallingStruct4 = FontEngine.s_GlyphMarshallingStruct_OUT[j];
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct4.index];
					glyph2.metrics = glyphMarshallingStruct4.metrics;
					glyph2.glyphRect = glyphMarshallingStruct4.glyphRect;
					glyph2.scale = glyphMarshallingStruct4.scale;
					glyph2.atlasIndex = glyphMarshallingStruct4.atlasIndex;
					glyphsAdded.Add(glyph2);
				}
				bool flag11 = j < count3;
				if (flag11)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
				}
				bool flag12 = j < count4;
				if (flag12)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
				}
			}
			return flag8;
		}

		// Token: 0x060001FD RID: 509
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyphs", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryPackGlyphsInAtlas_Internal([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x060001FE RID: 510 RVA: 0x0001B028 File Offset: 0x00019228
		internal static FontEngineError RenderGlyphToTexture(Glyph glyph, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			return (FontEngineError)FontEngine.RenderGlyphToTexture_Internal(glyphMarshallingStruct, padding, renderMode, texture);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001B04B File Offset: 0x0001924B
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphToTexture", IsFreeFunction = true)]
		private static int RenderGlyphToTexture_Internal(GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			return FontEngine.RenderGlyphToTexture_Internal_Injected(ref glyphStruct, padding, renderMode, texture);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001B058 File Offset: 0x00019258
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texture);
		}

		// Token: 0x06000201 RID: 513
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTexture", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int RenderGlyphsToTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, Texture2D texture);

		// Token: 0x06000202 RID: 514 RVA: 0x0001B0D8 File Offset: 0x000192D8
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, byte[] texBuffer, int texWidth, int texHeight)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTextureBuffer_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texBuffer, texWidth, texHeight);
		}

		// Token: 0x06000203 RID: 515
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTextureBuffer", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int RenderGlyphsToTextureBuffer_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, [Out] byte[] texBuffer, int texWidth, int texHeight);

		// Token: 0x06000204 RID: 516 RVA: 0x0001B15C File Offset: 0x0001935C
		internal static FontEngineError RenderGlyphsToSharedTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToSharedTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode);
		}

		// Token: 0x06000205 RID: 517
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToSharedTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int RenderGlyphsToSharedTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode);

		// Token: 0x06000206 RID: 518
		[NativeMethod(Name = "TextCore::FontEngine::SetSharedTextureData", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void SetSharedTexture(Texture2D texture);

		// Token: 0x06000207 RID: 519
		[NativeMethod(Name = "TextCore::FontEngine::ReleaseSharedTextureData", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void ReleaseSharedTexture();

		// Token: 0x06000208 RID: 520 RVA: 0x0001B1DC File Offset: 0x000193DC
		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			GlyphMarshallingStruct glyphMarshallingStruct;
			bool flag4 = FontEngine.TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2, renderMode, texture, out glyphMarshallingStruct);
			bool flag7;
			if (flag4)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				flag7 = true;
			}
			else
			{
				glyph = null;
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x06000209 RID: 521
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph);

		// Token: 0x0600020A RID: 522 RVA: 0x0001B348 File Offset: 0x00019548
		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			glyphs = null;
			bool flag = glyphIndexes == null || glyphIndexes.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int count = glyphIndexes.Count;
				bool flag3 = FontEngine.s_GlyphIndexes_MarshallingArray == null || FontEngine.s_GlyphIndexes_MarshallingArray.Length < count;
				if (flag3)
				{
					bool flag4 = FontEngine.s_GlyphIndexes_MarshallingArray == null;
					if (flag4)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray = new uint[count];
					}
					else
					{
						int num = Mathf.NextPowerOfTwo(count + 1);
						FontEngine.s_GlyphIndexes_MarshallingArray = new uint[num];
					}
				}
				int count2 = freeGlyphRects.Count;
				int count3 = usedGlyphRects.Count;
				int num2 = count2 + count3 + count;
				bool flag5 = FontEngine.s_FreeGlyphRects.Length < num2 || FontEngine.s_UsedGlyphRects.Length < num2;
				if (flag5)
				{
					int num3 = Mathf.NextPowerOfTwo(num2 + 1);
					FontEngine.s_FreeGlyphRects = new GlyphRect[num3];
					FontEngine.s_UsedGlyphRects = new GlyphRect[num3];
				}
				bool flag6 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag6)
				{
					int num4 = Mathf.NextPowerOfTwo(count + 1);
					FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num4];
				}
				int num5 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int i = 0; i < num5; i++)
				{
					bool flag7 = i < count;
					if (flag7)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray[i] = glyphIndexes[i];
					}
					bool flag8 = i < count2;
					if (flag8)
					{
						FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
					}
					bool flag9 = i < count3;
					if (flag9)
					{
						FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
					}
				}
				bool flag10 = FontEngine.TryAddGlyphsToTexture_Internal(FontEngine.s_GlyphIndexes_MarshallingArray, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture, FontEngine.s_GlyphMarshallingStruct_OUT, ref count);
				bool flag11 = FontEngine.s_Glyphs == null || FontEngine.s_Glyphs.Length <= count;
				if (flag11)
				{
					FontEngine.s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(count + 1)];
				}
				FontEngine.s_Glyphs[count] = null;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num5 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int j = 0; j < num5; j++)
				{
					bool flag12 = j < count;
					if (flag12)
					{
						FontEngine.s_Glyphs[j] = new Glyph(FontEngine.s_GlyphMarshallingStruct_OUT[j]);
					}
					bool flag13 = j < count2;
					if (flag13)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag14 = j < count3;
					if (flag14)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				glyphs = FontEngine.s_Glyphs;
				flag2 = flag10;
			}
			return flag2;
		}

		// Token: 0x0600020B RID: 523
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount);

		// Token: 0x0600020C RID: 524
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeFontFeatures", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern int GetOpenTypeFontFeatureTable();

		// Token: 0x0600020D RID: 525 RVA: 0x0001B5CC File Offset: 0x000197CC
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			int num = glyphIndexes.Length * glyphIndexes.Length;
			bool flag = FontEngine.s_PairAdjustmentRecords_MarshallingArray == null || FontEngine.s_PairAdjustmentRecords_MarshallingArray.Length < num;
			if (flag)
			{
				FontEngine.s_PairAdjustmentRecords_MarshallingArray = new GlyphPairAdjustmentRecord[num];
			}
			int num2;
			bool flag2 = FontEngine.GetGlyphPairAdjustmentTable_Internal(glyphIndexes, FontEngine.s_PairAdjustmentRecords_MarshallingArray, out num2) != 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag2)
			{
				array = null;
			}
			else
			{
				Array.Clear(FontEngine.s_PairAdjustmentRecords_MarshallingArray, num2, FontEngine.s_PairAdjustmentRecords_MarshallingArray.Length - num2);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x0600020E RID: 526
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentTable", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int GetGlyphPairAdjustmentTable_Internal(uint[] glyphIndexes, [Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords, out int adjustmentRecordCount);

		// Token: 0x0600020F RID: 527 RVA: 0x0001B644 File Offset: 0x00019844
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecord", IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetGlyphPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
			FontEngine.GetGlyphPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out glyphPairAdjustmentRecord);
			return glyphPairAdjustmentRecord;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001B65C File Offset: 0x0001985C
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> glyphIndexes, out int recordCount)
		{
			int count = glyphIndexes.Count;
			bool flag = FontEngine.s_GlyphIndexes_MarshallingArray == null || FontEngine.s_GlyphIndexes_MarshallingArray.Length < count;
			if (flag)
			{
				FontEngine.s_GlyphIndexes_MarshallingArray = new uint[Mathf.NextPowerOfTwo(count + 1)];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphIndexes_MarshallingArray[i] = glyphIndexes[i];
			}
			FontEngine.s_GlyphIndexes_MarshallingArray[count] = 0U;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndexes(FontEngine.s_GlyphIndexes_MarshallingArray, out recordCount);
			bool flag2 = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag2)
			{
				array = null;
			}
			else
			{
				bool flag3 = FontEngine.s_PairAdjustmentRecords_MarshallingArray == null || FontEngine.s_PairAdjustmentRecords_MarshallingArray.Length < recordCount;
				if (flag3)
				{
					FontEngine.s_PairAdjustmentRecords_MarshallingArray = new GlyphPairAdjustmentRecord[Mathf.NextPowerOfTwo(recordCount + 1)];
				}
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001B724 File Offset: 0x00019924
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(uint glyphIndex, out int recordCount)
		{
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(glyphIndex, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				bool flag2 = FontEngine.s_PairAdjustmentRecords_MarshallingArray == null || FontEngine.s_PairAdjustmentRecords_MarshallingArray.Length < recordCount;
				if (flag2)
				{
					FontEngine.s_PairAdjustmentRecords_MarshallingArray = new GlyphPairAdjustmentRecord[Mathf.NextPowerOfTwo(recordCount + 1)];
				}
				FontEngine.GetGlyphPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000212 RID: 530
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, out int recordCount);

		// Token: 0x06000213 RID: 531
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(uint glyphIndex, out int recordCount);

		// Token: 0x06000214 RID: 532
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int GetGlyphPairAdjustmentRecordsFromMarshallingArray([Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords);

		// Token: 0x06000215 RID: 533
		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void ResetAtlasTexture(Texture2D texture);

		// Token: 0x06000216 RID: 534
		[NativeMethod(Name = "TextCore::FontEngine::RenderToTexture", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void RenderBufferToTexture(Texture2D srcTexture, int padding, GlyphRenderMode renderMode, Texture2D dstTexture);

		// Token: 0x06000218 RID: 536
		[MethodImpl(4096)]
		private static extern int RenderGlyphToTexture_Internal_Injected(ref GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture);

		// Token: 0x06000219 RID: 537
		[MethodImpl(4096)]
		private static extern void GetGlyphPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		// Token: 0x040003C2 RID: 962
		private static readonly FontEngine s_Instance = new FontEngine();

		// Token: 0x040003C3 RID: 963
		private static Glyph[] s_Glyphs = new Glyph[16];

		// Token: 0x040003C4 RID: 964
		private static uint[] s_GlyphIndexes_MarshallingArray = new uint[16];

		// Token: 0x040003C5 RID: 965
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		// Token: 0x040003C6 RID: 966
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		// Token: 0x040003C7 RID: 967
		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		// Token: 0x040003C8 RID: 968
		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		// Token: 0x040003C9 RID: 969
		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		// Token: 0x040003CA RID: 970
		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
	}
}
