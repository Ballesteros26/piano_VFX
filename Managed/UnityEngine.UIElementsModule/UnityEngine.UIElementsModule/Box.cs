using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BF RID: 191
	public class Box : VisualElement
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x00015765 File Offset: 0x00013965
		public Box()
		{
			base.AddToClassList(Box.ussClassName);
		}

		// Token: 0x04000267 RID: 615
		public static readonly string ussClassName = "unity-box";

		// Token: 0x020000C0 RID: 192
		public new class UxmlFactory : UxmlFactory<Box>
		{
		}
	}
}
