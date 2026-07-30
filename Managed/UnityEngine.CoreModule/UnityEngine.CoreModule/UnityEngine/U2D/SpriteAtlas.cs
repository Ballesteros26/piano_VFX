using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000210 RID: 528
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	public class SpriteAtlas : Object
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001771 RID: 6001
		public extern bool isVariant
		{
			[NativeMethod("IsVariant")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001772 RID: 6002
		public extern string tag
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001773 RID: 6003
		public extern int spriteCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001774 RID: 6004
		[MethodImpl(4096)]
		public extern bool CanBindTo(Sprite sprite);

		// Token: 0x06001775 RID: 6005
		[MethodImpl(4096)]
		public extern Sprite GetSprite(string name);

		// Token: 0x06001776 RID: 6006 RVA: 0x00026158 File Offset: 0x00024358
		public int GetSprites(Sprite[] sprites)
		{
			return this.GetSpritesScripting(sprites);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00026174 File Offset: 0x00024374
		public int GetSprites(Sprite[] sprites, string name)
		{
			return this.GetSpritesWithNameScripting(sprites, name);
		}

		// Token: 0x06001778 RID: 6008
		[MethodImpl(4096)]
		private extern int GetSpritesScripting(Sprite[] sprites);

		// Token: 0x06001779 RID: 6009
		[MethodImpl(4096)]
		private extern int GetSpritesWithNameScripting(Sprite[] sprites, string name);
	}
}
