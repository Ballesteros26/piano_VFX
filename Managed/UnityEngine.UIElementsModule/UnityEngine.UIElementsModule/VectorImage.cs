using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class VectorImage : ScriptableObject
	{
		// Token: 0x0400067D RID: 1661
		[SerializeField]
		internal Texture2D atlas = null;

		// Token: 0x0400067E RID: 1662
		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		// Token: 0x0400067F RID: 1663
		[SerializeField]
		internal ushort[] indices = null;

		// Token: 0x04000680 RID: 1664
		[SerializeField]
		internal GradientSettings[] settings = null;

		// Token: 0x04000681 RID: 1665
		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
