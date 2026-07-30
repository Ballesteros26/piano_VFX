using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A8 RID: 424
	public interface ICustomStyle
	{
		// Token: 0x06000C2E RID: 3118
		bool TryGetValue(CustomStyleProperty<float> property, out float value);

		// Token: 0x06000C2F RID: 3119
		bool TryGetValue(CustomStyleProperty<int> property, out int value);

		// Token: 0x06000C30 RID: 3120
		bool TryGetValue(CustomStyleProperty<bool> property, out bool value);

		// Token: 0x06000C31 RID: 3121
		bool TryGetValue(CustomStyleProperty<Color> property, out Color value);

		// Token: 0x06000C32 RID: 3122
		bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value);

		// Token: 0x06000C33 RID: 3123
		bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value);

		// Token: 0x06000C34 RID: 3124
		bool TryGetValue(CustomStyleProperty<string> property, out string value);
	}
}
