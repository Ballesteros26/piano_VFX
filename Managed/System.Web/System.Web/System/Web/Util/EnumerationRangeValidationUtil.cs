using System;
using System.Web.UI.WebControls;

namespace System.Web.Util
{
	// Token: 0x02000117 RID: 279
	internal static class EnumerationRangeValidationUtil
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x0002623F File Offset: 0x0002443F
		public static void ValidateRepeatLayout(RepeatLayout value)
		{
			if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
