using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000266 RID: 614
	internal struct MatchResultInfo
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00050BE2 File Offset: 0x0004EDE2
		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}

		// Token: 0x0400090F RID: 2319
		public readonly bool success;

		// Token: 0x04000910 RID: 2320
		public readonly PseudoStates triggerPseudoMask;

		// Token: 0x04000911 RID: 2321
		public readonly PseudoStates dependencyPseudoMask;
	}
}
