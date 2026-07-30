using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x02000205 RID: 517
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003948C File Offset: 0x0003768C
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x000394A4 File Offset: 0x000376A4
		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = Enumerable.ToList<string>(value);
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000394B4 File Offset: 0x000376B4
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration uxmlEnumeration = other as UxmlEnumeration;
			bool flag = uxmlEnumeration == null;
			return !flag && Enumerable.All<string>(this.values, new Func<string, bool>(uxmlEnumeration.values.Contains<string>)) && Enumerable.Count<string>(this.values) == Enumerable.Count<string>(uxmlEnumeration.values);
		}

		// Token: 0x0400066D RID: 1645
		private List<string> m_Values = new List<string>();
	}
}
