using System;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[EditorBrowsable(1)]
	[Obsolete("GUIElement has been removed.", true)]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	public sealed class GUIElement
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000078A3 File Offset: 0x00005AA3
		private static void FeatureRemoved()
		{
			throw new Exception("GUIElement has been removed from Unity.");
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000078B0 File Offset: 0x00005AB0
		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000078CC File Offset: 0x00005ACC
		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition, [DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000078E8 File Offset: 0x00005AE8
		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect([DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000791C File Offset: 0x00005B1C
		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect()
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}
	}
}
