using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A5 RID: 165
	public struct VisualElementStyleSheetSet : IEquatable<VisualElementStyleSheetSet>
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00012C74 File Offset: 0x00010E74
		internal VisualElementStyleSheetSet(VisualElement element)
		{
			this.m_Element = element;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00012C80 File Offset: 0x00010E80
		public void Add(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList == null;
			if (flag2)
			{
				this.m_Element.styleSheetList = new List<StyleSheet>();
			}
			else
			{
				bool flag3 = this.m_Element.styleSheetList.Contains(styleSheet);
				if (flag3)
				{
					return;
				}
			}
			this.m_Element.styleSheetList.Add(styleSheet);
			this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00012D04 File Offset: 0x00010F04
		public void Clear()
		{
			bool flag = this.m_Element.styleSheetList == null;
			if (!flag)
			{
				this.m_Element.styleSheetList = null;
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00012D40 File Offset: 0x00010F40
		public bool Remove(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null && this.m_Element.styleSheetList.Remove(styleSheet);
			bool flag4;
			if (flag2)
			{
				bool flag3 = this.m_Element.styleSheetList.Count == 0;
				if (flag3)
				{
					this.m_Element.styleSheetList = null;
				}
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00012DC8 File Offset: 0x00010FC8
		internal void Swap(StyleSheet old, StyleSheet @new)
		{
			bool flag = old == null;
			if (flag)
			{
				throw new ArgumentNullException("old");
			}
			bool flag2 = @new == null;
			if (flag2)
			{
				throw new ArgumentNullException("new");
			}
			bool flag3 = this.m_Element.styleSheetList == null;
			if (!flag3)
			{
				int num = this.m_Element.styleSheetList.IndexOf(old);
				bool flag4 = num >= 0;
				if (flag4)
				{
					this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
					this.m_Element.styleSheetList[num] = @new;
				}
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012E5C File Offset: 0x0001105C
		public bool Contains(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null;
			return flag2 && this.m_Element.styleSheetList.Contains(styleSheet);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00012EAC File Offset: 0x000110AC
		public int count
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					num = this.m_Element.styleSheetList.Count;
				}
				return num;
			}
		}

		// Token: 0x17000127 RID: 295
		public StyleSheet this[int index]
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Element.styleSheetList[index];
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012F24 File Offset: 0x00011124
		public bool Equals(VisualElementStyleSheetSet other)
		{
			return object.Equals(this.m_Element, other.m_Element);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012F48 File Offset: 0x00011148
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualElementStyleSheetSet && this.Equals((VisualElementStyleSheetSet)obj);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012F80 File Offset: 0x00011180
		public override int GetHashCode()
		{
			return (this.m_Element != null) ? this.m_Element.GetHashCode() : 0;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012FA8 File Offset: 0x000111A8
		public static bool operator ==(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return left.Equals(right);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00012FC4 File Offset: 0x000111C4
		public static bool operator !=(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000205 RID: 517
		private readonly VisualElement m_Element;
	}
}
