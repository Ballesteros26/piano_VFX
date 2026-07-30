using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000014 RID: 20
	internal static class CsvError
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000477D File Offset: 0x0000297D
		public static void ThrowBadFormat(int lineNumber, string message, Exception innerException = null)
		{
			CsvError.ThrowBadFormat(string.Format("Line {0}: {1}", lineNumber, message), innerException);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004796 File Offset: 0x00002996
		public static void ThrowBadFormat(string message, Exception innerException = null)
		{
			throw new FormatException(message, innerException);
		}
	}
}
