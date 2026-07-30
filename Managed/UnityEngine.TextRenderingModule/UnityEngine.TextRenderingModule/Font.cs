using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/TextRendering/Public/FontImpl.h")]
	[NativeHeader("Modules/TextRendering/Public/Font.h")]
	[StaticAccessor("TextRenderingPrivate", StaticAccessorType.DoubleColon)]
	[NativeClass("TextRendering::Font")]
	public sealed class Font : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00003014 File Offset: 0x00001214
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00003048 File Offset: 0x00001248
		[field: DebuggerBrowsable(0)]
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000080 RID: 128 RVA: 0x0000307C File Offset: 0x0000127C
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x000030B4 File Offset: 0x000012B4
		[field: DebuggerBrowsable(0)]
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public extern string[] fontNames
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000086 RID: 134
		public extern bool dynamic
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000087 RID: 135
		public extern int ascent
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000088 RID: 136
		public extern int fontSize
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		public extern CharacterInfo[] characterInfo
		{
			[FreeFunction("TextRenderingPrivate::GetFontCharacterInfo", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("TextRenderingPrivate::SetFontCharacterInfo", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008B RID: 139
		[NativeProperty("LineSpacing", false, TargetType.Function)]
		public extern int lineHeight
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000030EC File Offset: 0x000012EC
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003104 File Offset: 0x00001304
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000310E File Offset: 0x0000130E
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003120 File Offset: 0x00001320
		public Font(string name)
		{
			bool flag = Path.GetDirectoryName(name) == string.Empty;
			bool flag2 = flag;
			if (flag2)
			{
				Font.Internal_CreateFont(this, name);
			}
			else
			{
				Font.Internal_CreateFontFromPath(this, name);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000315D File Offset: 0x0000135D
		private Font(string[] names, int size)
		{
			Font.Internal_CreateDynamicFont(this, names, size);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003170 File Offset: 0x00001370
		public static Font CreateDynamicFontFromOSFont(string fontname, int size)
		{
			return new Font(new string[] { fontname }, size);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003194 File Offset: 0x00001394
		public static Font CreateDynamicFontFromOSFont(string[] fontnames, int size)
		{
			return new Font(fontnames, size);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000031AD File Offset: 0x000013AD
		[RequiredByNativeCode]
		internal static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action.Invoke(font);
			}
			Font.FontTextureRebuildCallback fontTextureRebuildCallback = font.m_FontTextureRebuildCallback;
			if (fontTextureRebuildCallback != null)
			{
				fontTextureRebuildCallback();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000031D4 File Offset: 0x000013D4
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06000095 RID: 149
		[MethodImpl(4096)]
		internal static extern Font GetDefault();

		// Token: 0x06000096 RID: 150 RVA: 0x000031F0 File Offset: 0x000013F0
		public bool HasCharacter(char c)
		{
			return this.HasCharacter((int)c);
		}

		// Token: 0x06000097 RID: 151
		[MethodImpl(4096)]
		private extern bool HasCharacter(int c);

		// Token: 0x06000098 RID: 152
		[MethodImpl(4096)]
		public static extern string[] GetOSInstalledFontNames();

		// Token: 0x06000099 RID: 153
		[MethodImpl(4096)]
		public static extern string[] GetPathsToOSFonts();

		// Token: 0x0600009A RID: 154
		[MethodImpl(4096)]
		private static extern void Internal_CreateFont([Writable] Font self, string name);

		// Token: 0x0600009B RID: 155
		[MethodImpl(4096)]
		private static extern void Internal_CreateFontFromPath([Writable] Font self, string fontPath);

		// Token: 0x0600009C RID: 156
		[MethodImpl(4096)]
		private static extern void Internal_CreateDynamicFont([Writable] Font self, string[] _names, int size);

		// Token: 0x0600009D RID: 157
		[FreeFunction("TextRenderingPrivate::GetCharacterInfo", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x0600009E RID: 158 RVA: 0x0000320C File Offset: 0x0000140C
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			return this.GetCharacterInfo(ch, out info, size, FontStyle.Normal);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003228 File Offset: 0x00001428
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			return this.GetCharacterInfo(ch, out info, 0, FontStyle.Normal);
		}

		// Token: 0x060000A0 RID: 160
		[MethodImpl(4096)]
		public extern void RequestCharactersInTexture(string characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x060000A1 RID: 161 RVA: 0x00003244 File Offset: 0x00001444
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			this.RequestCharactersInTexture(characters, size, FontStyle.Normal);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003251 File Offset: 0x00001451
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			this.RequestCharactersInTexture(characters, 0, FontStyle.Normal);
		}

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000A4 RID: 164
		public delegate void FontTextureRebuildCallback();
	}
}
