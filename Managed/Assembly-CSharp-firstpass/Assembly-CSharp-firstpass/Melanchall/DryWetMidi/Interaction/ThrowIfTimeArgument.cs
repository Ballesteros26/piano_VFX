using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000DB RID: 219
	internal static class ThrowIfTimeArgument
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00018023 File Offset: 0x00016223
		internal static void IsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "Time is negative.");
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00018031 File Offset: 0x00016231
		internal static void StartIsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "Start time is negative.");
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001803F File Offset: 0x0001623F
		internal static void EndIsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "End time is negative.");
		}
	}
}
