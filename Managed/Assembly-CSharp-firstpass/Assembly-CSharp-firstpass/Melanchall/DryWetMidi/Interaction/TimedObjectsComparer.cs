using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D5 RID: 213
	internal sealed class TimedObjectsComparer<TObject> : IComparer<TObject> where TObject : ITimedObject
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00017D2C File Offset: 0x00015F2C
		public int Compare(TObject x, TObject y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return Math.Sign(x.Time - y.Time);
		}
	}
}
