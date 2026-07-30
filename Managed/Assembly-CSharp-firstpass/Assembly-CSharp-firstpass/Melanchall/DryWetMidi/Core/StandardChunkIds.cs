using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000115 RID: 277
	internal static class StandardChunkIds
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001CA68 File Offset: 0x0001AC68
		public static string[] GetIds()
		{
			string[] array;
			if ((array = StandardChunkIds._ids) == null)
			{
				array = (StandardChunkIds._ids = new string[] { "MThd", "MTrk" });
			}
			return array;
		}

		// Token: 0x0400083D RID: 2109
		private static string[] _ids;
	}
}
