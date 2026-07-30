using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001C RID: 28
	public interface IVersionable<TVersion> where TVersion : struct, IConvertible
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		TVersion version { get; set; }
	}
}
