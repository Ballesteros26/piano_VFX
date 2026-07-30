using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	internal class TextSettings : ScriptableObject
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00018CEC File Offset: 0x00016EEC
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00018D08 File Offset: 0x00016F08
		public static int missingGlyphCharacter
		{
			get
			{
				return TextSettings.instance.m_missingGlyphCharacter;
			}
			set
			{
				TextSettings.instance.m_missingGlyphCharacter = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00018D18 File Offset: 0x00016F18
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00018D34 File Offset: 0x00016F34
		public static bool warningsDisabled
		{
			get
			{
				return TextSettings.instance.m_warningsDisabled;
			}
			set
			{
				TextSettings.instance.m_warningsDisabled = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00018D44 File Offset: 0x00016F44
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00018D60 File Offset: 0x00016F60
		public static FontAsset defaultFontAsset
		{
			get
			{
				return TextSettings.instance.m_defaultFontAsset;
			}
			set
			{
				TextSettings.instance.m_defaultFontAsset = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00018D70 File Offset: 0x00016F70
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00018D8C File Offset: 0x00016F8C
		public static string defaultFontAssetPath
		{
			get
			{
				return TextSettings.instance.m_defaultFontAssetPath;
			}
			set
			{
				TextSettings.instance.m_defaultFontAssetPath = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00018D9C File Offset: 0x00016F9C
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00018DB8 File Offset: 0x00016FB8
		public static List<FontAsset> fallbackFontAssets
		{
			get
			{
				return TextSettings.instance.m_fallbackFontAssets;
			}
			set
			{
				TextSettings.instance.m_fallbackFontAssets = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00018DC8 File Offset: 0x00016FC8
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public static bool matchMaterialPreset
		{
			get
			{
				return TextSettings.instance.m_matchMaterialPreset;
			}
			set
			{
				TextSettings.instance.m_matchMaterialPreset = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00018DF4 File Offset: 0x00016FF4
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00018E10 File Offset: 0x00017010
		public static TextSpriteAsset defaultSpriteAsset
		{
			get
			{
				return TextSettings.instance.m_defaultSpriteAsset;
			}
			set
			{
				TextSettings.instance.m_defaultSpriteAsset = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00018E20 File Offset: 0x00017020
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00018E3C File Offset: 0x0001703C
		public static string defaultSpriteAssetPath
		{
			get
			{
				return TextSettings.instance.m_defaultSpriteAssetPath;
			}
			set
			{
				TextSettings.instance.m_defaultSpriteAssetPath = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00018E4C File Offset: 0x0001704C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00018E68 File Offset: 0x00017068
		public static string defaultColorGradientPresetsPath
		{
			get
			{
				return TextSettings.instance.m_defaultColorGradientPresetsPath;
			}
			set
			{
				TextSettings.instance.m_defaultColorGradientPresetsPath = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00018E78 File Offset: 0x00017078
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00018E94 File Offset: 0x00017094
		public static TextStyleSheet defaultStyleSheet
		{
			get
			{
				return TextSettings.instance.m_defaultStyleSheet;
			}
			set
			{
				TextSettings.instance.m_defaultStyleSheet = value;
				TextStyleSheet.LoadDefaultStyleSheet();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00018EA8 File Offset: 0x000170A8
		public static TextSettings.LineBreakingTable linebreakingRules
		{
			get
			{
				bool flag = TextSettings.instance.m_linebreakingRules == null;
				if (flag)
				{
					TextSettings.LoadLinebreakingRules();
				}
				return TextSettings.instance.m_linebreakingRules;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00018EDC File Offset: 0x000170DC
		public static TextSettings instance
		{
			get
			{
				bool flag = TextSettings.s_Instance == null;
				if (flag)
				{
					TextSettings.s_Instance = Resources.Load<TextSettings>("TextSettings") ?? ScriptableObject.CreateInstance<TextSettings>();
				}
				return TextSettings.s_Instance;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00018F1C File Offset: 0x0001711C
		public static void LoadLinebreakingRules()
		{
			bool flag = TextSettings.instance == null;
			if (!flag)
			{
				bool flag2 = TextSettings.s_Instance.m_linebreakingRules == null;
				if (flag2)
				{
					TextSettings.s_Instance.m_linebreakingRules = new TextSettings.LineBreakingTable();
				}
				TextSettings.s_Instance.m_linebreakingRules.leadingCharacters = ((TextSettings.s_Instance.m_leadingCharacters != null) ? TextSettings.GetCharacters(TextSettings.s_Instance.m_leadingCharacters.text) : TextSettings.GetCharacters("([｛〔〈《「『【〘〖〝‘“｟«$—…‥〳〴〵\\［（{£¥\"々〇〉》」＄｠￥￦ #"));
				TextSettings.s_Instance.m_linebreakingRules.followingCharacters = ((TextSettings.s_Instance.m_followingCharacters != null) ? TextSettings.GetCharacters(TextSettings.s_Instance.m_followingCharacters.text) : TextSettings.GetCharacters(")]｝〕〉》」』】〙〗〟’”｠»ヽヾーァィゥェォッャュョヮヵヶぁぃぅぇぉっゃゅょゎゕゖㇰㇱㇲㇳㇴㇵㇶㇷㇸㇹㇺㇻㇼㇽㇾㇿ々〻‐゠–〜?!‼⁇⁈⁉・、%,.:;。！？］）：；＝}¢°\"†‡℃〆％，．"));
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00018FE0 File Offset: 0x000171E0
		private static Dictionary<int, char> GetCharacters(string text)
		{
			Dictionary<int, char> dictionary = new Dictionary<int, char>();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				bool flag = !dictionary.ContainsKey((int)c);
				if (flag)
				{
					dictionary.Add((int)c, c);
				}
			}
			return dictionary;
		}

		// Token: 0x04000329 RID: 809
		private const string k_DefaultLeadingCharacters = "([｛〔〈《「『【〘〖〝‘“｟«$—…‥〳〴〵\\［（{£¥\"々〇〉》」＄｠￥￦ #";

		// Token: 0x0400032A RID: 810
		private const string k_DefaultFollowingCharacters = ")]｝〕〉》」』】〙〗〟’”｠»ヽヾーァィゥェォッャュョヮヵヶぁぃぅぇぉっゃゅょゎゕゖㇰㇱㇲㇳㇴㇵㇶㇷㇸㇹㇺㇻㇼㇽㇾㇿ々〻‐゠–〜?!‼⁇⁈⁉・、%,.:;。！？］）：；＝}¢°\"†‡℃〆％，．";

		// Token: 0x0400032B RID: 811
		private static TextSettings s_Instance;

		// Token: 0x0400032C RID: 812
		[SerializeField]
		private int m_missingGlyphCharacter;

		// Token: 0x0400032D RID: 813
		[SerializeField]
		private bool m_warningsDisabled = true;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		private FontAsset m_defaultFontAsset;

		// Token: 0x0400032F RID: 815
		[SerializeField]
		private string m_defaultFontAssetPath;

		// Token: 0x04000330 RID: 816
		[SerializeField]
		private List<FontAsset> m_fallbackFontAssets;

		// Token: 0x04000331 RID: 817
		[SerializeField]
		private bool m_matchMaterialPreset;

		// Token: 0x04000332 RID: 818
		[SerializeField]
		private TextSpriteAsset m_defaultSpriteAsset;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		private string m_defaultSpriteAssetPath;

		// Token: 0x04000334 RID: 820
		[SerializeField]
		private string m_defaultColorGradientPresetsPath;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		private TextStyleSheet m_defaultStyleSheet;

		// Token: 0x04000336 RID: 822
		[SerializeField]
		private TextAsset m_leadingCharacters = null;

		// Token: 0x04000337 RID: 823
		[SerializeField]
		private TextAsset m_followingCharacters = null;

		// Token: 0x04000338 RID: 824
		[SerializeField]
		private TextSettings.LineBreakingTable m_linebreakingRules;

		// Token: 0x0200003B RID: 59
		public class LineBreakingTable
		{
			// Token: 0x04000339 RID: 825
			public Dictionary<int, char> leadingCharacters;

			// Token: 0x0400033A RID: 826
			public Dictionary<int, char> followingCharacters;
		}
	}
}
