using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.U2D
{
	// Token: 0x0200020F RID: 527
	[StaticAccessor("GetSpriteAtlasManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlasManager.h")]
	public class SpriteAtlasManager
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001768 RID: 5992 RVA: 0x00026028 File Offset: 0x00024228
		// (remove) Token: 0x06001769 RID: 5993 RVA: 0x0002605C File Offset: 0x0002425C
		[field: DebuggerBrowsable(0)]
		public static event Action<string, Action<SpriteAtlas>> atlasRequested;

		// Token: 0x0600176A RID: 5994 RVA: 0x00026090 File Offset: 0x00024290
		[RequiredByNativeCode]
		private static bool RequestAtlas(string tag)
		{
			bool flag = SpriteAtlasManager.atlasRequested != null;
			bool flag2;
			if (flag)
			{
				SpriteAtlasManager.atlasRequested.Invoke(tag, new Action<SpriteAtlas>(SpriteAtlasManager.Register));
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600176B RID: 5995 RVA: 0x000260CC File Offset: 0x000242CC
		// (remove) Token: 0x0600176C RID: 5996 RVA: 0x00026100 File Offset: 0x00024300
		[field: DebuggerBrowsable(0)]
		public static event Action<SpriteAtlas> atlasRegistered;

		// Token: 0x0600176D RID: 5997 RVA: 0x00026133 File Offset: 0x00024333
		[RequiredByNativeCode]
		private static void PostRegisteredAtlas(SpriteAtlas spriteAtlas)
		{
			Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
			if (action != null)
			{
				action.Invoke(spriteAtlas);
			}
		}

		// Token: 0x0600176E RID: 5998
		[MethodImpl(4096)]
		internal static extern void Register(SpriteAtlas spriteAtlas);

		// Token: 0x06001770 RID: 6000 RVA: 0x00026148 File Offset: 0x00024348
		// Note: this type is marked as 'beforefieldinit'.
		static SpriteAtlasManager()
		{
			SpriteAtlasManager.atlasRequested = null;
			SpriteAtlasManager.atlasRegistered = null;
		}
	}
}
