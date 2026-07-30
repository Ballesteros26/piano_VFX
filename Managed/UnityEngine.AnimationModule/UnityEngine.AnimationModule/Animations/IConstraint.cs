using System;
using System.Collections.Generic;

namespace UnityEngine.Animations
{
	// Token: 0x02000061 RID: 97
	public interface IConstraint
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000568 RID: 1384
		// (set) Token: 0x06000569 RID: 1385
		float weight { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600056A RID: 1386
		// (set) Token: 0x0600056B RID: 1387
		bool constraintActive { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600056C RID: 1388
		// (set) Token: 0x0600056D RID: 1389
		bool locked { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600056E RID: 1390
		int sourceCount { get; }

		// Token: 0x0600056F RID: 1391
		int AddSource(ConstraintSource source);

		// Token: 0x06000570 RID: 1392
		void RemoveSource(int index);

		// Token: 0x06000571 RID: 1393
		ConstraintSource GetSource(int index);

		// Token: 0x06000572 RID: 1394
		void SetSource(int index, ConstraintSource source);

		// Token: 0x06000573 RID: 1395
		void GetSources(List<ConstraintSource> sources);

		// Token: 0x06000574 RID: 1396
		void SetSources(List<ConstraintSource> sources);
	}
}
