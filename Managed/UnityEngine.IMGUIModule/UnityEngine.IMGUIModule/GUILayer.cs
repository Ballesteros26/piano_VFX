using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[Obsolete("GUILayer has been removed.", true)]
	[EditorBrowsable(1)]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	public sealed class GUILayer
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000794D File Offset: 0x00005B4D
		[Obsolete("GUILayer has been removed.", true)]
		public GUIElement HitTest(Vector3 screenPosition)
		{
			throw new Exception("GUILayer has been removed from Unity.");
		}
	}
}
