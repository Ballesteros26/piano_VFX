using System;

namespace System.IO
{
	// Token: 0x020003C7 RID: 967
	internal static class PathInternal
	{
		// Token: 0x06002D78 RID: 11640 RVA: 0x00015ED5 File Offset: 0x000140D5
		public static bool IsPartiallyQualified(string path)
		{
			return false;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000A2986 File Offset: 0x000A0B86
		public static bool HasIllegalCharacters(string path, bool checkAdditional)
		{
			return path.IndexOfAny(Path.InvalidPathChars) != -1;
		}
	}
}
