using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00003D4C File Offset: 0x00001F4C
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarInternal(go, humanDescription);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00003D76 File Offset: 0x00001F76
		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return AvatarBuilder.BuildHumanAvatarInternal_Injected(go, ref humanDescription);
		}

		// Token: 0x0600023A RID: 570
		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		[MethodImpl(4096)]
		public static extern Avatar BuildGenericAvatar([NotNull] GameObject go, [NotNull] string rootMotionTransformName);

		// Token: 0x0600023C RID: 572
		[MethodImpl(4096)]
		private static extern Avatar BuildHumanAvatarInternal_Injected(GameObject go, ref HumanDescription humanDescription);
	}
}
