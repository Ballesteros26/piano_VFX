using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class TrackClipTypeAttribute : Attribute
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000092FB File Offset: 0x000074FB
		public TrackClipTypeAttribute(Type clipClass)
		{
			this.inspectedType = clipClass;
			this.allowAutoCreate = true;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009311 File Offset: 0x00007511
		public TrackClipTypeAttribute(Type clipClass, bool allowAutoCreate)
		{
			this.inspectedType = clipClass;
		}

		// Token: 0x040000E5 RID: 229
		public readonly Type inspectedType;

		// Token: 0x040000E6 RID: 230
		public readonly bool allowAutoCreate;
	}
}
