using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000015 RID: 21
	internal static class BC6HExtensions
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000034E9 File Offset: 0x000016E9
		public static void BC6HEncodeFastCubemap(this CommandBuffer cmb, RenderTargetIdentifier source, int sourceSize, RenderTargetIdentifier target, int fromMip, int toMip, int targetArrayIndex = 0)
		{
			EncodeBC6H.DefaultInstance.EncodeFastCubemap(cmb, source, sourceSize, target, fromMip, toMip, targetArrayIndex);
		}
	}
}
