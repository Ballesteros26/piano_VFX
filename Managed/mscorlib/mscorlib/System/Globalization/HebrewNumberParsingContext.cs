using System;

namespace System.Globalization
{
	// Token: 0x02000415 RID: 1045
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x060031C0 RID: 12736 RVA: 0x000B3638 File Offset: 0x000B1838
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x04001A4B RID: 6731
		internal HebrewNumber.HS state;

		// Token: 0x04001A4C RID: 6732
		internal int result;
	}
}
