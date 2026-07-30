using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AB RID: 939
	internal class SmiXetterAccessMap
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x000C0AC6 File Offset: 0x000BECC6
		internal static bool IsSetterAccessValid(SmiMetaData metaData, SmiXetterTypeCode xetterType)
		{
			return SmiXetterAccessMap.s_isSetterAccessValid[(int)metaData.SqlDbType, (int)xetterType];
		}

		// Token: 0x04001ABF RID: 6847
		private const bool X = true;

		// Token: 0x04001AC0 RID: 6848
		private const bool _ = false;

		// Token: 0x04001AC1 RID: 6849
		private static bool[,] s_isSetterAccessValid = new bool[,]
		{
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, false, true, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true
			}
		};
	}
}
