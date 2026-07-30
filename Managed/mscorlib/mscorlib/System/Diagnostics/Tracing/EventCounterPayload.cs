using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A92 RID: 2706
	[EventData]
	internal class EventCounterPayload : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06006294 RID: 25236 RVA: 0x001418D1 File Offset: 0x0013FAD1
		// (set) Token: 0x06006295 RID: 25237 RVA: 0x001418D9 File Offset: 0x0013FAD9
		public string Name { get; set; }

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06006296 RID: 25238 RVA: 0x001418E2 File Offset: 0x0013FAE2
		// (set) Token: 0x06006297 RID: 25239 RVA: 0x001418EA File Offset: 0x0013FAEA
		public float Mean { get; set; }

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x001418F3 File Offset: 0x0013FAF3
		// (set) Token: 0x06006299 RID: 25241 RVA: 0x001418FB File Offset: 0x0013FAFB
		public float StandardDeviation { get; set; }

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x00141904 File Offset: 0x0013FB04
		// (set) Token: 0x0600629B RID: 25243 RVA: 0x0014190C File Offset: 0x0013FB0C
		public int Count { get; set; }

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x00141915 File Offset: 0x0013FB15
		// (set) Token: 0x0600629D RID: 25245 RVA: 0x0014191D File Offset: 0x0013FB1D
		public float Min { get; set; }

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x0600629E RID: 25246 RVA: 0x00141926 File Offset: 0x0013FB26
		// (set) Token: 0x0600629F RID: 25247 RVA: 0x0014192E File Offset: 0x0013FB2E
		public float Max { get; set; }

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060062A0 RID: 25248 RVA: 0x00141937 File Offset: 0x0013FB37
		// (set) Token: 0x060062A1 RID: 25249 RVA: 0x0014193F File Offset: 0x0013FB3F
		public float IntervalSec { get; internal set; }

		// Token: 0x060062A2 RID: 25250 RVA: 0x00141948 File Offset: 0x0013FB48
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x00141948 File Offset: 0x0013FB48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x00141955 File Offset: 0x0013FB55
		private IEnumerable<KeyValuePair<string, object>> ForEnumeration
		{
			get
			{
				yield return new KeyValuePair<string, object>("Name", this.Name);
				yield return new KeyValuePair<string, object>("Mean", this.Mean);
				yield return new KeyValuePair<string, object>("StandardDeviation", this.StandardDeviation);
				yield return new KeyValuePair<string, object>("Count", this.Count);
				yield return new KeyValuePair<string, object>("Min", this.Min);
				yield return new KeyValuePair<string, object>("Max", this.Max);
				yield break;
			}
		}
	}
}
