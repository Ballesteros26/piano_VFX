using System;
using System.Collections.Generic;

namespace System.Security.AccessControl
{
	// Token: 0x02000610 RID: 1552
	internal class SddlAccessRight
	{
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x000EDD8C File Offset: 0x000EBF8C
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x000EDD94 File Offset: 0x000EBF94
		public string Name { get; set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x000EDD9D File Offset: 0x000EBF9D
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x000EDDA5 File Offset: 0x000EBFA5
		public int Value { get; set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x000EDDAE File Offset: 0x000EBFAE
		// (set) Token: 0x060043E2 RID: 17378 RVA: 0x000EDDB6 File Offset: 0x000EBFB6
		public int ObjectType { get; set; }

		// Token: 0x060043E3 RID: 17379 RVA: 0x000EDDC0 File Offset: 0x000EBFC0
		public static SddlAccessRight LookupByName(string s)
		{
			foreach (SddlAccessRight sddlAccessRight in SddlAccessRight.rights)
			{
				if (sddlAccessRight.Name == s)
				{
					return sddlAccessRight;
				}
			}
			return null;
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x000EDDF8 File Offset: 0x000EBFF8
		public static SddlAccessRight[] Decompose(int mask)
		{
			foreach (SddlAccessRight sddlAccessRight in SddlAccessRight.rights)
			{
				if (mask == sddlAccessRight.Value)
				{
					return new SddlAccessRight[] { sddlAccessRight };
				}
			}
			int num = 0;
			List<SddlAccessRight> list = new List<SddlAccessRight>();
			int num2 = 0;
			foreach (SddlAccessRight sddlAccessRight2 in SddlAccessRight.rights)
			{
				if ((mask & sddlAccessRight2.Value) == sddlAccessRight2.Value && (num2 | sddlAccessRight2.Value) != num2)
				{
					if (num == 0)
					{
						num = sddlAccessRight2.ObjectType;
					}
					if (sddlAccessRight2.ObjectType != 0 && num != sddlAccessRight2.ObjectType)
					{
						return null;
					}
					list.Add(sddlAccessRight2);
					num2 |= sddlAccessRight2.Value;
				}
				if (num2 == mask)
				{
					return list.ToArray();
				}
			}
			return null;
		}

		// Token: 0x04002211 RID: 8721
		private static readonly SddlAccessRight[] rights = new SddlAccessRight[]
		{
			new SddlAccessRight
			{
				Name = "CC",
				Value = 1,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "DC",
				Value = 2,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "LC",
				Value = 4,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "SW",
				Value = 8,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "RP",
				Value = 16,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "WP",
				Value = 32,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "DT",
				Value = 64,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "LO",
				Value = 128,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "CR",
				Value = 256,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "SD",
				Value = 65536
			},
			new SddlAccessRight
			{
				Name = "RC",
				Value = 131072
			},
			new SddlAccessRight
			{
				Name = "WD",
				Value = 262144
			},
			new SddlAccessRight
			{
				Name = "WO",
				Value = 524288
			},
			new SddlAccessRight
			{
				Name = "GA",
				Value = 268435456
			},
			new SddlAccessRight
			{
				Name = "GX",
				Value = 536870912
			},
			new SddlAccessRight
			{
				Name = "GW",
				Value = 1073741824
			},
			new SddlAccessRight
			{
				Name = "GR",
				Value = int.MinValue
			},
			new SddlAccessRight
			{
				Name = "FA",
				Value = 2032127,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FR",
				Value = 1179785,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FW",
				Value = 1179926,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FX",
				Value = 1179808,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "KA",
				Value = 983103,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KR",
				Value = 131097,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KW",
				Value = 131078,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KX",
				Value = 131097,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "NW",
				Value = 1
			},
			new SddlAccessRight
			{
				Name = "NR",
				Value = 2
			},
			new SddlAccessRight
			{
				Name = "NX",
				Value = 4
			}
		};
	}
}
