using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006E RID: 110
	internal static class RaycasterManager
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x000191DE File Offset: 0x000173DE
		public static void AddRaycaster(BaseRaycaster baseRaycaster)
		{
			if (RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Add(baseRaycaster);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000191F9 File Offset: 0x000173F9
		public static List<BaseRaycaster> GetRaycasters()
		{
			return RaycasterManager.s_Raycasters;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019200 File Offset: 0x00017400
		public static void RemoveRaycasters(BaseRaycaster baseRaycaster)
		{
			if (!RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Remove(baseRaycaster);
		}

		// Token: 0x04000218 RID: 536
		private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();
	}
}
