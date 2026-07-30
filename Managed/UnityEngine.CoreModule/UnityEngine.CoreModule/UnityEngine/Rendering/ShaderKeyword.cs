using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200038A RID: 906
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ShaderKeywords.h")]
	public struct ShaderKeyword
	{
		// Token: 0x06001FA3 RID: 8099
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(4096)]
		internal static extern int GetGlobalKeywordIndex(string keyword);

		// Token: 0x06001FA4 RID: 8100
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(4096)]
		internal static extern int GetKeywordIndex(Shader shader, string keyword);

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00036066 File Offset: 0x00034266
		[FreeFunction("ShaderScripting::GetGlobalKeywordName")]
		public static string GetGlobalKeywordName(ShaderKeyword index)
		{
			return ShaderKeyword.GetGlobalKeywordName_Injected(ref index);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0003606F File Offset: 0x0003426F
		[FreeFunction("ShaderScripting::GetGlobalKeywordType")]
		public static ShaderKeywordType GetGlobalKeywordType(ShaderKeyword index)
		{
			return ShaderKeyword.GetGlobalKeywordType_Injected(ref index);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00036078 File Offset: 0x00034278
		[FreeFunction("ShaderScripting::IsKeywordLocal")]
		public static bool IsKeywordLocal(ShaderKeyword index)
		{
			return ShaderKeyword.IsKeywordLocal_Injected(ref index);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00036081 File Offset: 0x00034281
		[FreeFunction("ShaderScripting::GetKeywordName")]
		public static string GetKeywordName(Shader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetKeywordName_Injected(shader, ref index);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0003608B File Offset: 0x0003428B
		[FreeFunction("ShaderScripting::GetKeywordType")]
		public static ShaderKeywordType GetKeywordType(Shader shader, ShaderKeyword index)
		{
			return ShaderKeyword.GetKeywordType_Injected(shader, ref index);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00036095 File Offset: 0x00034295
		internal ShaderKeyword(int keywordIndex)
		{
			this.m_KeywordIndex = keywordIndex;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0003609F File Offset: 0x0003429F
		public ShaderKeyword(string keywordName)
		{
			this.m_KeywordIndex = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000360AE File Offset: 0x000342AE
		public ShaderKeyword(Shader shader, string keywordName)
		{
			this.m_KeywordIndex = ShaderKeyword.GetKeywordIndex(shader, keywordName);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000360C0 File Offset: 0x000342C0
		public bool IsValid()
		{
			return this.m_KeywordIndex >= 0 && this.m_KeywordIndex < 320 && this.m_KeywordIndex != -1;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000360F8 File Offset: 0x000342F8
		public int index
		{
			get
			{
				return this.m_KeywordIndex;
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00036110 File Offset: 0x00034310
		[Obsolete("GetKeywordType is deprecated. Use ShaderKeyword.GetGlobalKeywordType instead.")]
		public ShaderKeywordType GetKeywordType()
		{
			return ShaderKeyword.GetGlobalKeywordType(this);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00036130 File Offset: 0x00034330
		[Obsolete("GetKeywordName is deprecated. Use ShaderKeyword.GetGlobalKeywordName instead.")]
		public string GetKeywordName()
		{
			return ShaderKeyword.GetGlobalKeywordName(this);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00036150 File Offset: 0x00034350
		[Obsolete("GetName() has been deprecated. Use ShaderKeyword.GetGlobalKeywordName instead.")]
		public string GetName()
		{
			return this.GetKeywordName();
		}

		// Token: 0x06001FB2 RID: 8114
		[MethodImpl(4096)]
		private static extern string GetGlobalKeywordName_Injected(ref ShaderKeyword index);

		// Token: 0x06001FB3 RID: 8115
		[MethodImpl(4096)]
		private static extern ShaderKeywordType GetGlobalKeywordType_Injected(ref ShaderKeyword index);

		// Token: 0x06001FB4 RID: 8116
		[MethodImpl(4096)]
		private static extern bool IsKeywordLocal_Injected(ref ShaderKeyword index);

		// Token: 0x06001FB5 RID: 8117
		[MethodImpl(4096)]
		private static extern string GetKeywordName_Injected(Shader shader, ref ShaderKeyword index);

		// Token: 0x06001FB6 RID: 8118
		[MethodImpl(4096)]
		private static extern ShaderKeywordType GetKeywordType_Injected(Shader shader, ref ShaderKeyword index);

		// Token: 0x04000B5E RID: 2910
		internal const int k_MaxShaderKeywords = 320;

		// Token: 0x04000B5F RID: 2911
		private const int k_InvalidKeyword = -1;

		// Token: 0x04000B60 RID: 2912
		internal int m_KeywordIndex;
	}
}
