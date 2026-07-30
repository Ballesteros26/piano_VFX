using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class TMP_Settings : ScriptableObject
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		public static string version
		{
			get
			{
				return "1.4.0";
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
		public static bool enableWordWrapping
		{
			get
			{
				return TMP_Settings.instance.m_enableWordWrapping;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000FAE5 File Offset: 0x0000DCE5
		public static bool enableKerning
		{
			get
			{
				return TMP_Settings.instance.m_enableKerning;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000FAF1 File Offset: 0x0000DCF1
		public static bool enableExtraPadding
		{
			get
			{
				return TMP_Settings.instance.m_enableExtraPadding;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000FAFD File Offset: 0x0000DCFD
		public static bool enableTintAllSprites
		{
			get
			{
				return TMP_Settings.instance.m_enableTintAllSprites;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000FB09 File Offset: 0x0000DD09
		public static bool enableParseEscapeCharacters
		{
			get
			{
				return TMP_Settings.instance.m_enableParseEscapeCharacters;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000FB15 File Offset: 0x0000DD15
		public static bool enableRaycastTarget
		{
			get
			{
				return TMP_Settings.instance.m_EnableRaycastTarget;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000FB21 File Offset: 0x0000DD21
		public static bool getFontFeaturesAtRuntime
		{
			get
			{
				return TMP_Settings.instance.m_GetFontFeaturesAtRuntime;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000FB2D File Offset: 0x0000DD2D
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000FB39 File Offset: 0x0000DD39
		public static int missingGlyphCharacter
		{
			get
			{
				return TMP_Settings.instance.m_missingGlyphCharacter;
			}
			set
			{
				TMP_Settings.instance.m_missingGlyphCharacter = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000FB46 File Offset: 0x0000DD46
		public static bool warningsDisabled
		{
			get
			{
				return TMP_Settings.instance.m_warningsDisabled;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000FB52 File Offset: 0x0000DD52
		public static TMP_FontAsset defaultFontAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAsset;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000FB5E File Offset: 0x0000DD5E
		public static string defaultFontAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAssetPath;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000FB6A File Offset: 0x0000DD6A
		public static float defaultFontSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontSize;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000FB76 File Offset: 0x0000DD76
		public static float defaultTextAutoSizingMinRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMinRatio;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000FB82 File Offset: 0x0000DD82
		public static float defaultTextAutoSizingMaxRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMaxRatio;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000FB8E File Offset: 0x0000DD8E
		public static Vector2 defaultTextMeshProTextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProTextContainerSize;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000FB9A File Offset: 0x0000DD9A
		public static Vector2 defaultTextMeshProUITextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProUITextContainerSize;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000FBA6 File Offset: 0x0000DDA6
		public static bool autoSizeTextContainer
		{
			get
			{
				return TMP_Settings.instance.m_autoSizeTextContainer;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		public static List<TMP_FontAsset> fallbackFontAssets
		{
			get
			{
				return TMP_Settings.instance.m_fallbackFontAssets;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		public static bool matchMaterialPreset
		{
			get
			{
				return TMP_Settings.instance.m_matchMaterialPreset;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000FBCA File Offset: 0x0000DDCA
		public static TMP_SpriteAsset defaultSpriteAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAsset;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000FBD6 File Offset: 0x0000DDD6
		public static string defaultSpriteAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAssetPath;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000FBE2 File Offset: 0x0000DDE2
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000FBEE File Offset: 0x0000DDEE
		public static bool enableEmojiSupport
		{
			get
			{
				return TMP_Settings.instance.m_enableEmojiSupport;
			}
			set
			{
				TMP_Settings.instance.m_enableEmojiSupport = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000FBFB File Offset: 0x0000DDFB
		public static string defaultColorGradientPresetsPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultColorGradientPresetsPath;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000FC07 File Offset: 0x0000DE07
		public static TMP_StyleSheet defaultStyleSheet
		{
			get
			{
				return TMP_Settings.instance.m_defaultStyleSheet;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000FC13 File Offset: 0x0000DE13
		public static string styleSheetsResourcePath
		{
			get
			{
				return TMP_Settings.instance.m_StyleSheetsResourcePath;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000FC1F File Offset: 0x0000DE1F
		public static TextAsset leadingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_leadingCharacters;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000FC2B File Offset: 0x0000DE2B
		public static TextAsset followingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_followingCharacters;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000FC37 File Offset: 0x0000DE37
		public static TMP_Settings.LineBreakingTable linebreakingRules
		{
			get
			{
				if (TMP_Settings.instance.m_linebreakingRules == null)
				{
					TMP_Settings.LoadLinebreakingRules();
				}
				return TMP_Settings.instance.m_linebreakingRules;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000FC54 File Offset: 0x0000DE54
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public static bool useModernHangulLineBreakingRules
		{
			get
			{
				return TMP_Settings.instance.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				TMP_Settings.instance.m_UseModernHangulLineBreakingRules = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000FC6D File Offset: 0x0000DE6D
		public static TMP_Settings instance
		{
			get
			{
				if (TMP_Settings.s_Instance == null)
				{
					TMP_Settings.s_Instance = Resources.Load<TMP_Settings>("TMP Settings");
				}
				return TMP_Settings.s_Instance;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public static TMP_Settings LoadDefaultSettings()
		{
			if (TMP_Settings.s_Instance == null)
			{
				TMP_Settings tmp_Settings = Resources.Load<TMP_Settings>("TMP Settings");
				if (tmp_Settings != null)
				{
					TMP_Settings.s_Instance = tmp_Settings;
				}
			}
			return TMP_Settings.s_Instance;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000FCC9 File Offset: 0x0000DEC9
		public static TMP_Settings GetSettings()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000FCDF File Offset: 0x0000DEDF
		public static TMP_FontAsset GetFontAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultFontAsset;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000FCFA File Offset: 0x0000DEFA
		public static TMP_SpriteAsset GetSpriteAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultSpriteAsset;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000FD15 File Offset: 0x0000DF15
		public static TMP_StyleSheet GetStyleSheet()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultStyleSheet;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public static void LoadLinebreakingRules()
		{
			if (TMP_Settings.instance == null)
			{
				return;
			}
			if (TMP_Settings.s_Instance.m_linebreakingRules == null)
			{
				TMP_Settings.s_Instance.m_linebreakingRules = new TMP_Settings.LineBreakingTable();
			}
			TMP_Settings.s_Instance.m_linebreakingRules.leadingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_leadingCharacters);
			TMP_Settings.s_Instance.m_linebreakingRules.followingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_followingCharacters);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		private static Dictionary<int, char> GetCharacters(TextAsset file)
		{
			Dictionary<int, char> dictionary = new Dictionary<int, char>();
			foreach (char c in file.text)
			{
				if (!dictionary.ContainsKey((int)c))
				{
					dictionary.Add((int)c, c);
				}
			}
			return dictionary;
		}

		// Token: 0x04000236 RID: 566
		private static TMP_Settings s_Instance;

		// Token: 0x04000237 RID: 567
		[SerializeField]
		private bool m_enableWordWrapping;

		// Token: 0x04000238 RID: 568
		[SerializeField]
		private bool m_enableKerning;

		// Token: 0x04000239 RID: 569
		[SerializeField]
		private bool m_enableExtraPadding;

		// Token: 0x0400023A RID: 570
		[SerializeField]
		private bool m_enableTintAllSprites;

		// Token: 0x0400023B RID: 571
		[SerializeField]
		private bool m_enableParseEscapeCharacters;

		// Token: 0x0400023C RID: 572
		[SerializeField]
		private bool m_EnableRaycastTarget = true;

		// Token: 0x0400023D RID: 573
		[SerializeField]
		private bool m_GetFontFeaturesAtRuntime = true;

		// Token: 0x0400023E RID: 574
		[SerializeField]
		private int m_missingGlyphCharacter;

		// Token: 0x0400023F RID: 575
		[SerializeField]
		private bool m_warningsDisabled;

		// Token: 0x04000240 RID: 576
		[SerializeField]
		private TMP_FontAsset m_defaultFontAsset;

		// Token: 0x04000241 RID: 577
		[SerializeField]
		private string m_defaultFontAssetPath;

		// Token: 0x04000242 RID: 578
		[SerializeField]
		private float m_defaultFontSize;

		// Token: 0x04000243 RID: 579
		[SerializeField]
		private float m_defaultAutoSizeMinRatio;

		// Token: 0x04000244 RID: 580
		[SerializeField]
		private float m_defaultAutoSizeMaxRatio;

		// Token: 0x04000245 RID: 581
		[SerializeField]
		private Vector2 m_defaultTextMeshProTextContainerSize;

		// Token: 0x04000246 RID: 582
		[SerializeField]
		private Vector2 m_defaultTextMeshProUITextContainerSize;

		// Token: 0x04000247 RID: 583
		[SerializeField]
		private bool m_autoSizeTextContainer;

		// Token: 0x04000248 RID: 584
		[SerializeField]
		private List<TMP_FontAsset> m_fallbackFontAssets;

		// Token: 0x04000249 RID: 585
		[SerializeField]
		private bool m_matchMaterialPreset;

		// Token: 0x0400024A RID: 586
		[SerializeField]
		private TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x0400024B RID: 587
		[SerializeField]
		private string m_defaultSpriteAssetPath;

		// Token: 0x0400024C RID: 588
		[SerializeField]
		private bool m_enableEmojiSupport;

		// Token: 0x0400024D RID: 589
		[SerializeField]
		private string m_defaultColorGradientPresetsPath;

		// Token: 0x0400024E RID: 590
		[SerializeField]
		private TMP_StyleSheet m_defaultStyleSheet;

		// Token: 0x0400024F RID: 591
		[SerializeField]
		private string m_StyleSheetsResourcePath;

		// Token: 0x04000250 RID: 592
		[SerializeField]
		private TextAsset m_leadingCharacters;

		// Token: 0x04000251 RID: 593
		[SerializeField]
		private TextAsset m_followingCharacters;

		// Token: 0x04000252 RID: 594
		[SerializeField]
		private TMP_Settings.LineBreakingTable m_linebreakingRules;

		// Token: 0x04000253 RID: 595
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x0200009B RID: 155
		public class LineBreakingTable
		{
			// Token: 0x04000578 RID: 1400
			public Dictionary<int, char> leadingCharacters;

			// Token: 0x04000579 RID: 1401
			public Dictionary<int, char> followingCharacters;
		}
	}
}
