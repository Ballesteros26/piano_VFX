using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ReloadAttribute : Attribute
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000BC0E File Offset: 0x00009E0E
		public ReloadAttribute(string[] paths, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BC16 File Offset: 0x00009E16
		public ReloadAttribute(string path, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
			: this(new string[] { path }, package)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BC0E File Offset: 0x00009E0E
		public ReloadAttribute(string pathFormat, int rangeMin, int rangeMax, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x020000DF RID: 223
		public enum Package
		{
			// Token: 0x040002CF RID: 719
			Builtin,
			// Token: 0x040002D0 RID: 720
			Root
		}
	}
}
