using System;

namespace TMPro
{
	// Token: 0x02000067 RID: 103
	public struct TMP_WordInfo
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00023928 File Offset: 0x00021B28
		public string GetWord()
		{
			string text = string.Empty;
			TMP_CharacterInfo[] characterInfo = this.textComponent.textInfo.characterInfo;
			for (int i = this.firstCharacterIndex; i < this.lastCharacterIndex + 1; i++)
			{
				text += characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x0400047E RID: 1150
		public TMP_Text textComponent;

		// Token: 0x0400047F RID: 1151
		public int firstCharacterIndex;

		// Token: 0x04000480 RID: 1152
		public int lastCharacterIndex;

		// Token: 0x04000481 RID: 1153
		public int characterCount;
	}
}
