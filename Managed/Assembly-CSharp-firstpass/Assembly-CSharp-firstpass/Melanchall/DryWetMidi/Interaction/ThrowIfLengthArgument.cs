using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D9 RID: 217
	internal static class ThrowIfLengthArgument
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00018007 File Offset: 0x00016207
		internal static void IsNegative(string parameterName, long length)
		{
			ThrowIfArgument.IsNegative(parameterName, length, "Length is negative.");
		}
	}
}
