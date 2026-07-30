using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class TreePrototype
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002360 File Offset: 0x00000560
		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000236C File Offset: 0x0000056C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002384 File Offset: 0x00000584
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000238E File Offset: 0x0000058E
		public TreePrototype()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002398 File Offset: 0x00000598
		public TreePrototype(TreePrototype other)
		{
			this.prefab = other.prefab;
			this.bendFactor = other.bendFactor;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000023BC File Offset: 0x000005BC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreePrototype);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000023DC File Offset: 0x000005DC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000023F4 File Offset: 0x000005F4
		private bool Equals(TreePrototype other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = other == this;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = base.GetType() != other.GetType();
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = this.prefab == other.prefab && this.bendFactor == other.bendFactor;
						flag2 = flag5;
					}
				}
			}
			return flag2;
		}

		// Token: 0x04000019 RID: 25
		internal GameObject m_Prefab;

		// Token: 0x0400001A RID: 26
		internal float m_BendFactor;
	}
}
