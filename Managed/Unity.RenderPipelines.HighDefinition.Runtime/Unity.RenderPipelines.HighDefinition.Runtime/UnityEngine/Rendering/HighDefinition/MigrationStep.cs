using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001F RID: 31
	internal static class MigrationStep
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000039F5 File Offset: 0x00001BF5
		public static MigrationStep<TVersion, TTarget> New<TVersion, TTarget>(TVersion version, Action<TTarget> action) where TVersion : struct, IConvertible where TTarget : class, IVersionable<TVersion>
		{
			return new MigrationStep<TVersion, TTarget>(version, action);
		}
	}
}
