using System;

namespace UnityEngine.TextCore
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal class TextStyle
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000198B0 File Offset: 0x00017AB0
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000198C8 File Offset: 0x00017AC8
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value != this.m_Name;
				if (flag)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000198F0 File Offset: 0x00017AF0
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00019908 File Offset: 0x00017B08
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				bool flag = value != this.m_HashCode;
				if (flag)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00019930 File Offset: 0x00017B30
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00019948 File Offset: 0x00017B48
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00019960 File Offset: 0x00017B60
		public int[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00019978 File Offset: 0x00017B78
		public int[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00019990 File Offset: 0x00017B90
		public void RefreshStyle()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
			this.m_OpeningTagArray = new int[this.m_OpeningDefinition.Length];
			for (int i = 0; i < this.m_OpeningDefinition.Length; i++)
			{
				this.m_OpeningTagArray[i] = (int)this.m_OpeningDefinition.get_Chars(i);
			}
			this.m_ClosingTagArray = new int[this.m_ClosingDefinition.Length];
			for (int j = 0; j < this.m_ClosingDefinition.Length; j++)
			{
				this.m_ClosingTagArray[j] = (int)this.m_ClosingDefinition.get_Chars(j);
			}
		}

		// Token: 0x0400034B RID: 843
		[SerializeField]
		private string m_Name;

		// Token: 0x0400034C RID: 844
		[SerializeField]
		private int m_HashCode;

		// Token: 0x0400034D RID: 845
		[SerializeField]
		private string m_OpeningDefinition = string.Empty;

		// Token: 0x0400034E RID: 846
		[SerializeField]
		private string m_ClosingDefinition = string.Empty;

		// Token: 0x0400034F RID: 847
		[SerializeField]
		private int[] m_OpeningTagArray;

		// Token: 0x04000350 RID: 848
		[SerializeField]
		private int[] m_ClosingTagArray;
	}
}
