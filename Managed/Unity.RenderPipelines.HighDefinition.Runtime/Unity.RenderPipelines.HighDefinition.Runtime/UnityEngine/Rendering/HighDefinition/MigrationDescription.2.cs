using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001E RID: 30
	public struct MigrationDescription<TVersion, TTarget> where TVersion : struct, IConvertible where TTarget : class, IVersionable<TVersion>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000038AF File Offset: 0x00001AAF
		public MigrationDescription(params MigrationStep<TVersion, TTarget>[] steps)
		{
			Array.Sort<MigrationStep<TVersion, TTarget>>(steps, (MigrationStep<TVersion, TTarget> l, MigrationStep<TVersion, TTarget> r) => MigrationDescription<TVersion, TTarget>.Compare(l.Version, r.Version));
			this.Steps = steps;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000038E0 File Offset: 0x00001AE0
		public bool Migrate(TTarget target)
		{
			if (MigrationDescription<TVersion, TTarget>.Equals(target.version, this.Steps[this.Steps.Length - 1].Version))
			{
				return false;
			}
			for (int i = 0; i < this.Steps.Length; i++)
			{
				if (MigrationDescription<TVersion, TTarget>.Compare(target.version, this.Steps[i].Version) < 0)
				{
					this.Steps[i].Migrate(target);
					target.version = this.Steps[i].Version;
				}
			}
			return true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003984 File Offset: 0x00001B84
		public void ExecuteStep(TTarget target, TVersion stepVersion)
		{
			for (int i = 0; i < this.Steps.Length; i++)
			{
				if (MigrationDescription<TVersion, TTarget>.Equals(this.Steps[i].Version, stepVersion))
				{
					this.Steps[i].Migrate(target);
					return;
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000039D0 File Offset: 0x00001BD0
		private static bool Equals(TVersion l, TVersion r)
		{
			return MigrationDescription<TVersion, TTarget>.Compare(l, r) == 0;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000039DC File Offset: 0x00001BDC
		private static int Compare(TVersion l, TVersion r)
		{
			return (int)((object)l) - (int)((object)r);
		}

		// Token: 0x0400007B RID: 123
		private readonly MigrationStep<TVersion, TTarget>[] Steps;
	}
}
