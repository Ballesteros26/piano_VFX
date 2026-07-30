using System;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x020000B5 RID: 181
	internal class ExpandString
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		public ExpandString(string s)
		{
			this.value = s;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001FB4B File Offset: 0x0001DD4B
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public string Expand()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i] == '%')
				{
					int j;
					for (j = i + 1; j < this.value.Length; j++)
					{
						if (this.value[j] == '%')
						{
							string text = this.value.Substring(i + 1, j - i - 1);
							stringBuilder.Append(Environment.GetEnvironmentVariable(text));
							i += j;
							break;
						}
					}
					if (j == this.value.Length)
					{
						stringBuilder.Append('%');
					}
				}
				else
				{
					stringBuilder.Append(this.value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400063B RID: 1595
		private string value;
	}
}
