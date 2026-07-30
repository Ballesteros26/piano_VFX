using System;

namespace TMPro
{
	// Token: 0x02000066 RID: 102
	public struct TMP_LinkInfo
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0002385C File Offset: 0x00021A5C
		internal void SetLinkID(char[] text, int startIndex, int length)
		{
			if (this.linkID == null || this.linkID.Length < length)
			{
				this.linkID = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkID[i] = text[startIndex + i];
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000238A4 File Offset: 0x00021AA4
		public string GetLinkText()
		{
			string text = string.Empty;
			TMP_TextInfo textInfo = this.textComponent.textInfo;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000238FE File Offset: 0x00021AFE
		public string GetLinkID()
		{
			if (this.textComponent == null)
			{
				return string.Empty;
			}
			return new string(this.linkID, 0, this.linkIdLength);
		}

		// Token: 0x04000477 RID: 1143
		public TMP_Text textComponent;

		// Token: 0x04000478 RID: 1144
		public int hashCode;

		// Token: 0x04000479 RID: 1145
		public int linkIdFirstCharacterIndex;

		// Token: 0x0400047A RID: 1146
		public int linkIdLength;

		// Token: 0x0400047B RID: 1147
		public int linkTextfirstCharacterIndex;

		// Token: 0x0400047C RID: 1148
		public int linkTextLength;

		// Token: 0x0400047D RID: 1149
		internal char[] linkID;
	}
}
