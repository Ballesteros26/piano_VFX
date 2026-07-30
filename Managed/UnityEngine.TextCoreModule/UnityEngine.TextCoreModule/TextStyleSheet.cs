using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal class TextStyleSheet : ScriptableObject
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00019A58 File Offset: 0x00017C58
		public static TextStyleSheet instance
		{
			get
			{
				bool flag = TextStyleSheet.s_Instance == null;
				if (flag)
				{
					TextStyleSheet.s_Instance = TextSettings.defaultStyleSheet;
					bool flag2 = TextStyleSheet.s_Instance == null;
					if (flag2)
					{
						return null;
					}
					TextStyleSheet.s_Instance.LoadStyleDictionaryInternal();
				}
				return TextStyleSheet.s_Instance;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00019AA8 File Offset: 0x00017CA8
		public static TextStyleSheet LoadDefaultStyleSheet()
		{
			TextStyleSheet.s_Instance = null;
			return TextStyleSheet.instance;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public static TextStyle GetStyle(int hashCode)
		{
			bool flag = TextStyleSheet.instance == null;
			TextStyle textStyle;
			if (flag)
			{
				textStyle = null;
			}
			else
			{
				textStyle = TextStyleSheet.instance.GetStyleInternal(hashCode);
			}
			return textStyle;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00019AFC File Offset: 0x00017CFC
		private TextStyle GetStyleInternal(int hashCode)
		{
			TextStyle textStyle;
			bool flag = this.m_StyleDictionary.TryGetValue(hashCode, ref textStyle);
			TextStyle textStyle2;
			if (flag)
			{
				textStyle2 = textStyle;
			}
			else
			{
				textStyle2 = null;
			}
			return textStyle2;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00019B28 File Offset: 0x00017D28
		public void UpdateStyleDictionaryKey(int old_key, int new_key)
		{
			bool flag = this.m_StyleDictionary.ContainsKey(old_key);
			if (flag)
			{
				TextStyle textStyle = this.m_StyleDictionary[old_key];
				this.m_StyleDictionary.Add(new_key, textStyle);
				this.m_StyleDictionary.Remove(old_key);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00019B70 File Offset: 0x00017D70
		public static void RefreshStyles()
		{
			TextStyleSheet.instance.LoadStyleDictionaryInternal();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00019B80 File Offset: 0x00017D80
		private void LoadStyleDictionaryInternal()
		{
			this.m_StyleDictionary.Clear();
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				bool flag = !this.m_StyleDictionary.ContainsKey(this.m_StyleList[i].hashCode);
				if (flag)
				{
					this.m_StyleDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
		}

		// Token: 0x04000351 RID: 849
		private static TextStyleSheet s_Instance;

		// Token: 0x04000352 RID: 850
		[SerializeField]
		private List<TextStyle> m_StyleList = new List<TextStyle>(1);

		// Token: 0x04000353 RID: 851
		private Dictionary<int, TextStyle> m_StyleDictionary = new Dictionary<int, TextStyle>();
	}
}
