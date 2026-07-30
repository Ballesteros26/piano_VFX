using System;

namespace System
{
	// Token: 0x020001EE RID: 494
	internal static class AppContextDefaultValues
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x00002194 File Offset: 0x00000394
		public static void PopulateDefaultValues()
		{
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005A158 File Offset: 0x00058358
		public static bool TryGetSwitchOverride(string switchName, out bool overrideValue)
		{
			overrideValue = false;
			return false;
		}

		// Token: 0x04000BEB RID: 3051
		internal const string SwitchNoAsyncCurrentCulture = "Switch.System.Globalization.NoAsyncCurrentCulture";

		// Token: 0x04000BEC RID: 3052
		internal const string SwitchThrowExceptionIfDisposedCancellationTokenSource = "Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource";

		// Token: 0x04000BED RID: 3053
		internal const string SwitchPreserveEventListnerObjectIdentity = "Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity";

		// Token: 0x04000BEE RID: 3054
		internal const string SwitchUseLegacyPathHandling = "Switch.System.IO.UseLegacyPathHandling";

		// Token: 0x04000BEF RID: 3055
		internal const string SwitchBlockLongPaths = "Switch.System.IO.BlockLongPaths";

		// Token: 0x04000BF0 RID: 3056
		internal const string SwitchDoNotAddrOfCspParentWindowHandle = "Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle";

		// Token: 0x04000BF1 RID: 3057
		internal const string SwitchSetActorAsReferenceWhenCopyingClaimsIdentity = "Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity";
	}
}
