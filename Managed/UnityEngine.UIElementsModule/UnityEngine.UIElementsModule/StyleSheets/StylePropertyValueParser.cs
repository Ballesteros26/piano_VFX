using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000274 RID: 628
	internal class StylePropertyValueParser
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x00053060 File Offset: 0x00051260
		public string[] Parse(string propertyValue)
		{
			this.m_PropertyValue = propertyValue;
			this.m_ValueList.Clear();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ParseIndex = 0;
			while (this.m_ParseIndex < this.m_PropertyValue.Length)
			{
				char c = this.m_PropertyValue.get_Chars(this.m_ParseIndex);
				char c2 = c;
				if (c2 != ' ')
				{
					if (c2 != '(')
					{
						if (c2 != ',')
						{
							this.m_StringBuilder.Append(c);
						}
						else
						{
							this.EatSpace();
							this.AddValuePart();
							this.m_ValueList.Add(",");
						}
					}
					else
					{
						this.AppendFunction();
					}
				}
				else
				{
					this.EatSpace();
					this.AddValuePart();
				}
				this.m_ParseIndex++;
			}
			string text = this.m_StringBuilder.ToString();
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				this.m_ValueList.Add(text);
			}
			return this.m_ValueList.ToArray();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0005317C File Offset: 0x0005137C
		private void AddValuePart()
		{
			string text = this.m_StringBuilder.ToString();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ValueList.Add(text);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000531BC File Offset: 0x000513BC
		private void AppendFunction()
		{
			while (this.m_ParseIndex < this.m_PropertyValue.Length && this.m_PropertyValue.get_Chars(this.m_ParseIndex) != ')')
			{
				this.m_StringBuilder.Append(this.m_PropertyValue.get_Chars(this.m_ParseIndex));
				this.m_ParseIndex++;
			}
			this.m_StringBuilder.Append(this.m_PropertyValue.get_Chars(this.m_ParseIndex));
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00053248 File Offset: 0x00051448
		private void EatSpace()
		{
			while (this.m_ParseIndex + 1 < this.m_PropertyValue.Length && this.m_PropertyValue.get_Chars(this.m_ParseIndex + 1) == ' ')
			{
				this.m_ParseIndex++;
			}
		}

		// Token: 0x04000938 RID: 2360
		private string m_PropertyValue;

		// Token: 0x04000939 RID: 2361
		private List<string> m_ValueList = new List<string>();

		// Token: 0x0400093A RID: 2362
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x0400093B RID: 2363
		private int m_ParseIndex = 0;
	}
}
