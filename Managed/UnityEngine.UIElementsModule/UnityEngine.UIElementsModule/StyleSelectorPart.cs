using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	internal struct StyleSelectorPart
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00035FC0 File Offset: 0x000341C0
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x00035FD8 File Offset: 0x000341D8
		public string value
		{
			get
			{
				return this.m_Value;
			}
			internal set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00035FE4 File Offset: 0x000341E4
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x00035FFC File Offset: 0x000341FC
		public StyleSelectorType type
		{
			get
			{
				return this.m_Type;
			}
			internal set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00036008 File Offset: 0x00034208
		public override string ToString()
		{
			return UnityString.Format("[StyleSelectorPart: value={0}, type={1}]", new object[] { this.value, this.type });
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00036044 File Offset: 0x00034244
		public static StyleSelectorPart CreateClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Class,
				m_Value = className
			};
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00036070 File Offset: 0x00034270
		public static StyleSelectorPart CreatePseudoClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.PseudoClass,
				m_Value = className
			};
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003609C File Offset: 0x0003429C
		public static StyleSelectorPart CreateId(string Id)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.ID,
				m_Value = Id
			};
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000360C8 File Offset: 0x000342C8
		public static StyleSelectorPart CreateType(Type t)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = t.Name
			};
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000360F8 File Offset: 0x000342F8
		public static StyleSelectorPart CreateType(string typeName)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = typeName
			};
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00036124 File Offset: 0x00034324
		public static StyleSelectorPart CreatePredicate(object predicate)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Predicate,
				tempData = predicate
			};
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00036150 File Offset: 0x00034350
		public static StyleSelectorPart CreateWildCard()
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Wildcard
			};
		}

		// Token: 0x040005A2 RID: 1442
		[SerializeField]
		private string m_Value;

		// Token: 0x040005A3 RID: 1443
		[SerializeField]
		private StyleSelectorType m_Type;

		// Token: 0x040005A4 RID: 1444
		internal object tempData;
	}
}
