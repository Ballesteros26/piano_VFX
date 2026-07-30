using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000020 RID: 32
	public struct MigrationStep<TVersion, TTarget> : IEquatable<MigrationStep<TVersion, TTarget>> where TVersion : struct, IConvertible where TTarget : class, IVersionable<TVersion>
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000039FE File Offset: 0x00001BFE
		public MigrationStep(TVersion version, Action<TTarget> action)
		{
			this.Version = version;
			this.m_MigrationAction = action;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003A10 File Offset: 0x00001C10
		public void Migrate(TTarget target)
		{
			if ((int)((object)target.version) >= (int)((object)this.Version))
			{
				return;
			}
			this.m_MigrationAction(target);
			target.version = this.Version;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003A64 File Offset: 0x00001C64
		public bool Equals(MigrationStep<TVersion, TTarget> other)
		{
			TVersion version = this.Version;
			return version.Equals(other.Version);
		}

		// Token: 0x0400007C RID: 124
		private readonly Action<TTarget> m_MigrationAction;

		// Token: 0x0400007D RID: 125
		public readonly TVersion Version;
	}
}
