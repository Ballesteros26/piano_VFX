using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class Readme : ScriptableObject
{
	// Token: 0x040003A7 RID: 935
	public Texture2D icon;

	// Token: 0x040003A8 RID: 936
	public string title;

	// Token: 0x040003A9 RID: 937
	public Readme.Section[] sections;

	// Token: 0x040003AA RID: 938
	public bool loadedLayout;

	// Token: 0x0200006F RID: 111
	[Serializable]
	public class Section
	{
		// Token: 0x0400047E RID: 1150
		public string heading;

		// Token: 0x0400047F RID: 1151
		public string text;

		// Token: 0x04000480 RID: 1152
		public string linkText;

		// Token: 0x04000481 RID: 1153
		public string url;
	}
}
