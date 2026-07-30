using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000008 RID: 8
	public class ClipperRegistry
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000284A File Offset: 0x00000A4A
		protected ClipperRegistry()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000285D File Offset: 0x00000A5D
		public static ClipperRegistry instance
		{
			get
			{
				if (ClipperRegistry.s_Instance == null)
				{
					ClipperRegistry.s_Instance = new ClipperRegistry();
				}
				return ClipperRegistry.s_Instance;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002878 File Offset: 0x00000A78
		public void Cull()
		{
			for (int i = 0; i < this.m_Clippers.Count; i++)
			{
				this.m_Clippers[i].PerformClipping();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028AC File Offset: 0x00000AAC
		public static void Register(IClipper c)
		{
			if (c == null)
			{
				return;
			}
			ClipperRegistry.instance.m_Clippers.AddUnique(c);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028C3 File Offset: 0x00000AC3
		public static void Unregister(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.Remove(c);
		}

		// Token: 0x04000022 RID: 34
		private static ClipperRegistry s_Instance;

		// Token: 0x04000023 RID: 35
		private readonly IndexedSet<IClipper> m_Clippers = new IndexedSet<IClipper>();
	}
}
