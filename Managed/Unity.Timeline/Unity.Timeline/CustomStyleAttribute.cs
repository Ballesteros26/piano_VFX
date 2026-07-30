using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomStyleAttribute : Attribute
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00009384 File Offset: 0x00007584
		public CustomStyleAttribute(string ussStyle)
		{
			this.ussStyle = ussStyle;
		}

		// Token: 0x040000F0 RID: 240
		public readonly string ussStyle;
	}
}
