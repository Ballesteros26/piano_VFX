using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000047 RID: 71
	public interface ITextElement
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000339 RID: 825
		Material sharedMaterial { get; }

		// Token: 0x0600033A RID: 826
		void Rebuild(CanvasUpdate update);

		// Token: 0x0600033B RID: 827
		int GetInstanceID();
	}
}
