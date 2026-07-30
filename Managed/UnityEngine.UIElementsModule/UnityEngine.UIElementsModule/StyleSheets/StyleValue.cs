using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200026E RID: 622
	[DebuggerDisplay("id = {id}, keyword = {keyword}, number = {number}, boolean = {boolean}, color = {color}, resource = {resource}")]
	[StructLayout(2)]
	internal struct StyleValue
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x00052188 File Offset: 0x00050388
		public static StyleValue Create(StylePropertyId id)
		{
			return new StyleValue
			{
				id = id
			};
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000521AC File Offset: 0x000503AC
		public static StyleValue Create(StylePropertyId id, StyleKeyword keyword)
		{
			return new StyleValue
			{
				id = id,
				keyword = keyword
			};
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000521D8 File Offset: 0x000503D8
		public static StyleValue Create(StylePropertyId id, float number)
		{
			return new StyleValue
			{
				id = id,
				number = number
			};
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00052204 File Offset: 0x00050404
		public static StyleValue Create(StylePropertyId id, int number)
		{
			return new StyleValue
			{
				id = id,
				number = (float)number
			};
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00052230 File Offset: 0x00050430
		public static StyleValue Create(StylePropertyId id, Color color)
		{
			return new StyleValue
			{
				id = id,
				color = color
			};
		}

		// Token: 0x0400091A RID: 2330
		[FieldOffset(0)]
		public StylePropertyId id;

		// Token: 0x0400091B RID: 2331
		[FieldOffset(4)]
		public StyleKeyword keyword;

		// Token: 0x0400091C RID: 2332
		[FieldOffset(8)]
		public float number;

		// Token: 0x0400091D RID: 2333
		[FieldOffset(8)]
		public Length length;

		// Token: 0x0400091E RID: 2334
		[FieldOffset(8)]
		public Color color;

		// Token: 0x0400091F RID: 2335
		[FieldOffset(8)]
		public GCHandle resource;
	}
}
