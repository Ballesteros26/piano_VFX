using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200033F RID: 831
	public static class AssemblyExtensions
	{
		// Token: 0x060024AB RID: 9387 RVA: 0x0002126B File Offset: 0x0001F46B
		[CLSCompliant(false)]
		public unsafe static bool TryGetRawMetadata(this Assembly assembly, out byte* blob, out int length)
		{
			throw new NotImplementedException();
		}
	}
}
