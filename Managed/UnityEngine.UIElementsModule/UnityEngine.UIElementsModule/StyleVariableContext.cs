using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D0 RID: 464
	internal class StyleVariableContext
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00036B50 File Offset: 0x00034D50
		public int count
		{
			get
			{
				return this.m_Variables.Count;
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00036B5D File Offset: 0x00034D5D
		public void Add(StyleVariable sv)
		{
			this.m_DirtyVariableHash = true;
			this.m_Variables.Add(sv);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00036B74 File Offset: 0x00034D74
		public void InsertRange(int index, StyleVariableContext other)
		{
			bool flag = other.m_Variables.Count > 0;
			if (flag)
			{
				this.m_DirtyVariableHash = true;
				this.m_Variables.InsertRange(index, other.m_Variables);
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00036BB0 File Offset: 0x00034DB0
		public void Clear()
		{
			bool flag = this.m_Variables.Count > 0;
			if (flag)
			{
				this.m_DirtyVariableHash = true;
				this.m_Variables.Clear();
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00036BE5 File Offset: 0x00034DE5
		public void RemoveRange(int i, int c)
		{
			this.m_DirtyVariableHash = true;
			this.m_Variables.RemoveRange(i, c);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00036BFD File Offset: 0x00034DFD
		public StyleVariableContext()
		{
			this.m_Variables = new List<StyleVariable>();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00036C19 File Offset: 0x00034E19
		public StyleVariableContext(StyleVariableContext other)
		{
			this.m_Variables = new List<StyleVariable>(other.m_Variables);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00036C3C File Offset: 0x00034E3C
		public bool TryFindVariable(string name, out StyleVariable v)
		{
			for (int i = this.m_Variables.Count - 1; i >= 0; i--)
			{
				bool flag = this.m_Variables[i].name == name;
				if (flag)
				{
					v = this.m_Variables[i];
					return true;
				}
			}
			v = default(StyleVariable);
			return false;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00036CAC File Offset: 0x00034EAC
		public int GetVariableHash()
		{
			bool flag = !this.m_DirtyVariableHash;
			int num;
			if (flag)
			{
				num = this.m_VariableHash;
			}
			else
			{
				this.m_DirtyVariableHash = false;
				bool flag2 = this.m_Variables.Count == 0;
				if (flag2)
				{
					this.m_VariableHash = 0;
					num = this.m_VariableHash;
				}
				else
				{
					this.m_VariableHash = this.m_Variables[0].GetHashCode();
					for (int i = 1; i < this.m_Variables.Count; i++)
					{
						this.m_VariableHash = (this.m_VariableHash * 397) ^ this.m_Variables[i].GetHashCode();
					}
					num = this.m_VariableHash;
				}
			}
			return num;
		}

		// Token: 0x040005E3 RID: 1507
		public static readonly StyleVariableContext none = new StyleVariableContext();

		// Token: 0x040005E4 RID: 1508
		private int m_VariableHash;

		// Token: 0x040005E5 RID: 1509
		private bool m_DirtyVariableHash = true;

		// Token: 0x040005E6 RID: 1510
		private List<StyleVariable> m_Variables;
	}
}
