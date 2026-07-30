using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/IMGUI/GUIContent.h")]
	[Serializable]
	[StructLayout(0)]
	public class GUIContent
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000074DC File Offset: 0x000056DC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000074F4 File Offset: 0x000056F4
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007500 File Offset: 0x00005700
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00007518 File Offset: 0x00005718
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00007524 File Offset: 0x00005724
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000753C File Offset: 0x0000573C
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007546 File Offset: 0x00005746
		public GUIContent()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007566 File Offset: 0x00005766
		public GUIContent(string text)
			: this(text, null, string.Empty)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007577 File Offset: 0x00005777
		public GUIContent(Texture image)
			: this(string.Empty, image, string.Empty)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000758C File Offset: 0x0000578C
		public GUIContent(string text, Texture image)
			: this(text, image, string.Empty)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000759D File Offset: 0x0000579D
		public GUIContent(string text, string tooltip)
			: this(text, null, tooltip)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000075AA File Offset: 0x000057AA
		public GUIContent(Texture image, string tooltip)
			: this(string.Empty, image, tooltip)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000075BB File Offset: 0x000057BB
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.text = text;
			this.image = image;
			this.tooltip = tooltip;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000075F4 File Offset: 0x000057F4
		public GUIContent(GUIContent src)
		{
			this.text = src.m_Text;
			this.image = src.m_Image;
			this.tooltip = src.m_Tooltip;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007648 File Offset: 0x00005848
		internal int hash
		{
			get
			{
				int num = 0;
				bool flag = !string.IsNullOrEmpty(this.m_Text);
				if (flag)
				{
					num = this.m_Text.GetHashCode() * 37;
				}
				return num;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007680 File Offset: 0x00005880
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			return GUIContent.s_Text;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000076B4 File Offset: 0x000058B4
		internal static GUIContent Temp(string t, string tooltip)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = tooltip;
			return GUIContent.s_Text;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000076E4 File Offset: 0x000058E4
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			return GUIContent.s_Image;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007718 File Offset: 0x00005918
		internal static GUIContent Temp(Texture i, string tooltip)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = tooltip;
			return GUIContent.s_Image;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007748 File Offset: 0x00005948
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007778 File Offset: 0x00005978
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000077D0 File Offset: 0x000059D0
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000780C File Offset: 0x00005A0C
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x0400006D RID: 109
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private Texture m_Image;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x04000070 RID: 112
		private static readonly GUIContent s_Text = new GUIContent();

		// Token: 0x04000071 RID: 113
		private static readonly GUIContent s_Image = new GUIContent();

		// Token: 0x04000072 RID: 114
		private static readonly GUIContent s_TextImage = new GUIContent();

		// Token: 0x04000073 RID: 115
		public static GUIContent none = new GUIContent("");
	}
}
