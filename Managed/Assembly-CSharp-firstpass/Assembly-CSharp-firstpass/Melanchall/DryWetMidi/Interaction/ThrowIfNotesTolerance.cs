using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000DA RID: 218
	internal static class ThrowIfNotesTolerance
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00018015 File Offset: 0x00016215
		internal static void IsNegative(string parameterName, long notesTolerance)
		{
			ThrowIfArgument.IsNegative(parameterName, notesTolerance, "Notes tolerance is negative.");
		}
	}
}
