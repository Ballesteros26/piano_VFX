using System;

namespace System
{
	// Token: 0x020001AF RID: 431
	internal struct ParamsArray
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x0004990E File Offset: 0x00047B0E
		public ParamsArray(object arg0)
		{
			this.arg0 = arg0;
			this.arg1 = null;
			this.arg2 = null;
			this.args = ParamsArray.oneArgArray;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00049930 File Offset: 0x00047B30
		public ParamsArray(object arg0, object arg1)
		{
			this.arg0 = arg0;
			this.arg1 = arg1;
			this.arg2 = null;
			this.args = ParamsArray.twoArgArray;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00049952 File Offset: 0x00047B52
		public ParamsArray(object arg0, object arg1, object arg2)
		{
			this.arg0 = arg0;
			this.arg1 = arg1;
			this.arg2 = arg2;
			this.args = ParamsArray.threeArgArray;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00049974 File Offset: 0x00047B74
		public ParamsArray(object[] args)
		{
			int num = args.Length;
			this.arg0 = ((num > 0) ? args[0] : null);
			this.arg1 = ((num > 1) ? args[1] : null);
			this.arg2 = ((num > 2) ? args[2] : null);
			this.args = args;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000499BC File Offset: 0x00047BBC
		public int Length
		{
			get
			{
				return this.args.Length;
			}
		}

		// Token: 0x17000216 RID: 534
		public object this[int index]
		{
			get
			{
				if (index != 0)
				{
					return this.GetAtSlow(index);
				}
				return this.arg0;
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000499D9 File Offset: 0x00047BD9
		private object GetAtSlow(int index)
		{
			if (index == 1)
			{
				return this.arg1;
			}
			if (index == 2)
			{
				return this.arg2;
			}
			return this.args[index];
		}

		// Token: 0x04000A4D RID: 2637
		private static readonly object[] oneArgArray = new object[1];

		// Token: 0x04000A4E RID: 2638
		private static readonly object[] twoArgArray = new object[2];

		// Token: 0x04000A4F RID: 2639
		private static readonly object[] threeArgArray = new object[3];

		// Token: 0x04000A50 RID: 2640
		private readonly object arg0;

		// Token: 0x04000A51 RID: 2641
		private readonly object arg1;

		// Token: 0x04000A52 RID: 2642
		private readonly object arg2;

		// Token: 0x04000A53 RID: 2643
		private readonly object[] args;
	}
}
