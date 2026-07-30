using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001D7 RID: 471
	public sealed class ShaderVariantCollection : Object
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600149D RID: 5277
		public extern int shaderCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600149E RID: 5278
		public extern int variantCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600149F RID: 5279
		public extern bool isWarmedUp
		{
			[NativeName("IsWarmedUp")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060014A0 RID: 5280
		[MethodImpl(4096)]
		private extern bool AddVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060014A1 RID: 5281
		[MethodImpl(4096)]
		private extern bool RemoveVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060014A2 RID: 5282
		[MethodImpl(4096)]
		private extern bool ContainsVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060014A3 RID: 5283
		[NativeName("ClearVariants")]
		[MethodImpl(4096)]
		public extern void Clear();

		// Token: 0x060014A4 RID: 5284
		[NativeName("WarmupShaders")]
		[MethodImpl(4096)]
		public extern void WarmUp();

		// Token: 0x060014A5 RID: 5285
		[NativeName("CreateFromScript")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		// Token: 0x060014A6 RID: 5286 RVA: 0x0002202F File Offset: 0x0002022F
		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00022040 File Offset: 0x00020240
		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0002206C File Offset: 0x0002026C
		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00022098 File Offset: 0x00020298
		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x020001D8 RID: 472
		public struct ShaderVariant
		{
			// Token: 0x060014AA RID: 5290
			[FreeFunction]
			[NativeConditional("UNITY_EDITOR")]
			[MethodImpl(4096)]
			private static extern string CheckShaderVariant(Shader shader, PassType passType, string[] keywords);

			// Token: 0x060014AB RID: 5291 RVA: 0x000220C2 File Offset: 0x000202C2
			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
			}

			// Token: 0x0400068C RID: 1676
			public Shader shader;

			// Token: 0x0400068D RID: 1677
			public PassType passType;

			// Token: 0x0400068E RID: 1678
			public string[] keywords;
		}
	}
}
