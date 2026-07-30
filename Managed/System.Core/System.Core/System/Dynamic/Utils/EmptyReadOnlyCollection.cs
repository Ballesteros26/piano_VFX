using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x0200033D RID: 829
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x04000B4A RID: 2890
		public static readonly ReadOnlyCollection<T> Instance = new TrueReadOnlyCollection<T>(Array.Empty<T>());
	}
}
