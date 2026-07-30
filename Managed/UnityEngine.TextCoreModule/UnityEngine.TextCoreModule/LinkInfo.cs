using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000016 RID: 22
	internal struct LinkInfo
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000054C4 File Offset: 0x000036C4
		internal void SetLinkId(char[] text, int startIndex, int length)
		{
			bool flag = this.linkId == null || this.linkId.Length < length;
			if (flag)
			{
				this.linkId = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkId[i] = text[startIndex + i];
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005518 File Offset: 0x00003718
		public string GetLinkText(TextInfo textInfo)
		{
			string text = string.Empty;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.textElementInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005570 File Offset: 0x00003770
		public string GetLinkId()
		{
			return new string(this.linkId, 0, this.linkIdLength);
		}

		// Token: 0x04000089 RID: 137
		public int hashCode;

		// Token: 0x0400008A RID: 138
		public int linkIdFirstCharacterIndex;

		// Token: 0x0400008B RID: 139
		public int linkIdLength;

		// Token: 0x0400008C RID: 140
		public int linkTextfirstCharacterIndex;

		// Token: 0x0400008D RID: 141
		public int linkTextLength;

		// Token: 0x0400008E RID: 142
		internal char[] linkId;
	}
}
