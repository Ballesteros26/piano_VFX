using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public sealed class ScalableSettingLevelParameter : IntParameter
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00041DEA File Offset: 0x0003FFEA
		public ScalableSettingLevelParameter(int level, bool useOverride, bool overrideState = false)
			: base(useOverride ? 3 : level, overrideState)
		{
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00041DFA File Offset: 0x0003FFFA
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00041E1C File Offset: 0x0004001C
		[TupleElementNames(new string[] { "level", "useOverride" })]
		public ValueTuple<int, bool> levelAndOverride
		{
			[return: TupleElementNames(new string[] { "level", "useOverride" })]
			get
			{
				if (this.value != 3)
				{
					return new ValueTuple<int, bool>(this.value, false);
				}
				return new ValueTuple<int, bool>(0, true);
			}
			[param: TupleElementNames(new string[] { "level", "useOverride" })]
			set
			{
				int item = value.Item1;
				this.value = (value.Item2 ? 3 : item);
			}
		}

		// Token: 0x040008EC RID: 2284
		public const int LevelCount = 3;

		// Token: 0x02000268 RID: 616
		public enum Level
		{
			// Token: 0x040015D3 RID: 5587
			Low,
			// Token: 0x040015D4 RID: 5588
			Medium,
			// Token: 0x040015D5 RID: 5589
			High
		}
	}
}
