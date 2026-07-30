using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200025A RID: 602
	internal static class StylePropertyCache
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x0004EE04 File Offset: 0x0004D004
		public static bool TryGetSyntax(string name, out string syntax)
		{
			return StylePropertyCache.s_PropertySyntaxCache.TryGetValue(name, ref syntax);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004EE24 File Offset: 0x0004D024
		public static string FindClosestPropertyName(string name)
		{
			float num = float.MaxValue;
			string text = null;
			foreach (string text2 in StylePropertyCache.s_PropertySyntaxCache.Keys)
			{
				float num2 = 1f;
				bool flag = text2.Contains(name);
				if (flag)
				{
					num2 = 0.1f;
				}
				float num3 = (float)StringUtils.LevenshteinDistance(name, text2) * num2;
				bool flag2 = num3 < num;
				if (flag2)
				{
					num = num3;
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		// Note: this type is marked as 'beforefieldinit'.
		static StylePropertyCache()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("align-content", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("align-items", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("align-self", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("background-color", "<color>");
			dictionary.Add("background-image", "<resource> | <url> | none");
			dictionary.Add("border-bottom-color", "<color>");
			dictionary.Add("border-bottom-left-radius", "<length> | <percentage>");
			dictionary.Add("border-bottom-right-radius", "<length> | <percentage>");
			dictionary.Add("border-bottom-width", "<length>");
			dictionary.Add("border-color", "<color>{1,4}");
			dictionary.Add("border-left-color", "<color>");
			dictionary.Add("border-left-width", "<length>");
			dictionary.Add("border-radius", "[ <length> | <percentage> ]{1,4}");
			dictionary.Add("border-right-color", "<color>");
			dictionary.Add("border-right-width", "<length>");
			dictionary.Add("border-top-color", "<color>");
			dictionary.Add("border-top-left-radius", "<length> | <percentage>");
			dictionary.Add("border-top-right-radius", "<length> | <percentage>");
			dictionary.Add("border-top-width", "<length>");
			dictionary.Add("border-width", "<length>{1,4}");
			dictionary.Add("bottom", "<length> | <percentage> | auto");
			dictionary.Add("color", "<color>");
			dictionary.Add("cursor", "[ [ <resource> | <url> ] [ <integer> <integer> ]? ] | [ arrow | text | resize-vertical | resize-horizontal | link | slide-arrow | resize-up-right | resize-up-left | move-arrow | rotate-arrow | scale-arrow | arrow-plus | arrow-minus | pan | orbit | zoom | fps | split-resize-up-down | split-resize-left-right ]");
			dictionary.Add("display", "flex | none");
			dictionary.Add("flex", "none | [ <'flex-grow'> <'flex-shrink'>? || <'flex-basis'> ]");
			dictionary.Add("flex-basis", "<'width'>");
			dictionary.Add("flex-direction", "column | row | column-reverse | row-reverse");
			dictionary.Add("flex-grow", "<number>");
			dictionary.Add("flex-shrink", "<number>");
			dictionary.Add("flex-wrap", "nowrap | wrap | wrap-reverse");
			dictionary.Add("font-size", "<length> | <percentage>");
			dictionary.Add("height", "<length> | <percentage> | auto");
			dictionary.Add("justify-content", "flex-start | flex-end | center | space-between | space-around");
			dictionary.Add("left", "<length> | <percentage> | auto");
			dictionary.Add("margin", "[ <length> | <percentage> | auto ]{1,4}");
			dictionary.Add("margin-bottom", "<length> | <percentage> | auto");
			dictionary.Add("margin-left", "<length> | <percentage> | auto");
			dictionary.Add("margin-right", "<length> | <percentage> | auto");
			dictionary.Add("margin-top", "<length> | <percentage> | auto");
			dictionary.Add("max-height", "<length> | <percentage> | none");
			dictionary.Add("max-width", "<length> | <percentage> | none");
			dictionary.Add("min-height", "<length> | <percentage> | auto");
			dictionary.Add("min-width", "<length> | <percentage> | auto");
			dictionary.Add("opacity", "<number>");
			dictionary.Add("overflow", "visible | hidden | scroll");
			dictionary.Add("padding", "[ <length> | <percentage> ]{1,4}");
			dictionary.Add("padding-bottom", "<length> | <percentage>");
			dictionary.Add("padding-left", "<length> | <percentage>");
			dictionary.Add("padding-right", "<length> | <percentage>");
			dictionary.Add("padding-top", "<length> | <percentage>");
			dictionary.Add("position", "relative | absolute");
			dictionary.Add("right", "<length> | <percentage> | auto");
			dictionary.Add("text-overflow", "clip | ellipsis");
			dictionary.Add("top", "<length> | <percentage> | auto");
			dictionary.Add("-unity-background-image-tint-color", "<color>");
			dictionary.Add("-unity-background-scale-mode", "stretch-to-fill | scale-and-crop | scale-to-fit");
			dictionary.Add("-unity-font", "<resource> | <url>");
			dictionary.Add("-unity-font-style", "normal | italic | bold | bold-and-italic");
			dictionary.Add("-unity-overflow-clip-box", "padding-box | content-box");
			dictionary.Add("-unity-slice-bottom", "<integer>");
			dictionary.Add("-unity-slice-left", "<integer>");
			dictionary.Add("-unity-slice-right", "<integer>");
			dictionary.Add("-unity-slice-top", "<integer>");
			dictionary.Add("-unity-text-align", "upper-left | middle-left | lower-left | upper-center | middle-center | lower-center | upper-right | middle-right | lower-right");
			dictionary.Add("-unity-text-overflow-position", "start | middle | end");
			dictionary.Add("visibility", "visible | hidden");
			dictionary.Add("white-space", "normal | nowrap");
			dictionary.Add("width", "<length> | <percentage> | auto");
			StylePropertyCache.s_PropertySyntaxCache = dictionary;
		}

		// Token: 0x04000897 RID: 2199
		internal static readonly Dictionary<string, string> s_PropertySyntaxCache;
	}
}
