using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class TMP_Style
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0001165C File Offset: 0x0000F85C
		public static TMP_Style NormalStyle
		{
			get
			{
				if (TMP_Style.k_NormalStyle == null)
				{
					TMP_Style.k_NormalStyle = new TMP_Style("Normal", string.Empty, string.Empty);
				}
				return TMP_Style.k_NormalStyle;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00011683 File Offset: 0x0000F883
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001168B File Offset: 0x0000F88B
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value != this.m_Name)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000116A2 File Offset: 0x0000F8A2
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x000116AA File Offset: 0x0000F8AA
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				if (value != this.m_HashCode)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000116BC File Offset: 0x0000F8BC
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000116CC File Offset: 0x0000F8CC
		public int[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000116D4 File Offset: 0x0000F8D4
		public int[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000116DC File Offset: 0x0000F8DC
		internal TMP_Style(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001170C File Offset: 0x0000F90C
		public void RefreshStyle()
		{
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(this.m_Name);
			this.m_OpeningTagArray = new int[this.m_OpeningDefinition.Length];
			for (int i = 0; i < this.m_OpeningDefinition.Length; i++)
			{
				this.m_OpeningTagArray[i] = (int)this.m_OpeningDefinition[i];
			}
			this.m_ClosingTagArray = new int[this.m_ClosingDefinition.Length];
			for (int j = 0; j < this.m_ClosingDefinition.Length; j++)
			{
				this.m_ClosingTagArray[j] = (int)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x040002AC RID: 684
		internal static TMP_Style k_NormalStyle;

		// Token: 0x040002AD RID: 685
		[SerializeField]
		private string m_Name;

		// Token: 0x040002AE RID: 686
		[SerializeField]
		private int m_HashCode;

		// Token: 0x040002AF RID: 687
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x040002B0 RID: 688
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x040002B1 RID: 689
		[SerializeField]
		private int[] m_OpeningTagArray;

		// Token: 0x040002B2 RID: 690
		[SerializeField]
		private int[] m_ClosingTagArray;
	}
}
