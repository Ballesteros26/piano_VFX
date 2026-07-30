using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001D RID: 29
	internal static class MigrationDescription
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000038A0 File Offset: 0x00001AA0
		public static T LastVersion<T>() where T : struct, IConvertible
		{
			return TypeInfo.GetEnumLastValue<T>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000038A7 File Offset: 0x00001AA7
		public static MigrationDescription<TVersion, TTarget> New<TVersion, TTarget>(params MigrationStep<TVersion, TTarget>[] steps) where TVersion : struct, IConvertible where TTarget : class, IVersionable<TVersion>
		{
			return new MigrationDescription<TVersion, TTarget>(steps);
		}
	}
}
