using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000060 RID: 96
	public class Compute_DT_EventArgs
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x000232A3 File Offset: 0x000214A3
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, float progress)
		{
			this.EventType = type;
			this.ProgressPercentage = progress;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000232B9 File Offset: 0x000214B9
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, Color[] colors)
		{
			this.EventType = type;
			this.Colors = colors;
		}

		// Token: 0x0400045B RID: 1115
		public Compute_DistanceTransform_EventTypes EventType;

		// Token: 0x0400045C RID: 1116
		public float ProgressPercentage;

		// Token: 0x0400045D RID: 1117
		public Color[] Colors;
	}
}
