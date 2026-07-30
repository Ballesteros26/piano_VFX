using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class TMP_StyleSheet : ScriptableObject
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000117AA File Offset: 0x0000F9AA
		internal List<TMP_Style> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public TMP_Style GetStyle(int hashCode)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			TMP_Style tmp_Style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out tmp_Style))
			{
				return tmp_Style;
			}
			return null;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public TMP_Style GetStyle(string name)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCode = TMP_TextParsingUtilities.GetHashCode(name);
			TMP_Style tmp_Style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out tmp_Style))
			{
				return tmp_Style;
			}
			return null;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011819 File Offset: 0x0000FA19
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011824 File Offset: 0x0000FA24
		private void LoadStyleDictionaryInternal()
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.m_StyleLookupDictionary = new Dictionary<int, TMP_Style>();
			}
			else
			{
				this.m_StyleLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				if (!this.m_StyleLookupDictionary.ContainsKey(this.m_StyleList[i].hashCode))
				{
					this.m_StyleLookupDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
		}

		// Token: 0x040002B3 RID: 691
		[SerializeField]
		private List<TMP_Style> m_StyleList = new List<TMP_Style>(1);

		// Token: 0x040002B4 RID: 692
		private Dictionary<int, TMP_Style> m_StyleLookupDictionary;
	}
}
