using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002D RID: 45
	public interface ITransform
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		Vector3 position { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FB RID: 251
		// (set) Token: 0x060000FC RID: 252
		Quaternion rotation { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		Vector3 scale { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FF RID: 255
		Matrix4x4 matrix { get; }
	}
}
