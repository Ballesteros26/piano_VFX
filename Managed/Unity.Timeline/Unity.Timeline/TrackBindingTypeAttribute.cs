using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackBindingTypeAttribute : Attribute
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000932B File Offset: 0x0000752B
		public TrackBindingTypeAttribute(Type type)
		{
			this.type = type;
			this.flags = TrackBindingFlags.AllowCreateComponent;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009341 File Offset: 0x00007541
		public TrackBindingTypeAttribute(Type type, TrackBindingFlags flags)
		{
			this.type = type;
			this.flags = flags;
		}

		// Token: 0x040000EB RID: 235
		public readonly Type type;

		// Token: 0x040000EC RID: 236
		public readonly TrackBindingFlags flags;
	}
}
