using System;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007B0 RID: 1968
	internal sealed class Indentation
	{
		// Token: 0x06003F7C RID: 16252 RVA: 0x000DFCB1 File Offset: 0x000DDEB1
		internal Indentation(ExposedTabStringIndentedTextWriter writer, int indent)
		{
			this._writer = writer;
			this._indent = indent;
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000DFCC8 File Offset: 0x000DDEC8
		internal string IndentationString
		{
			get
			{
				if (this._s == null)
				{
					string tabString = this._writer.TabString;
					switch (this._indent)
					{
					case 0:
						this._s = string.Empty;
						break;
					case 1:
						this._s = tabString;
						break;
					case 2:
						this._s = tabString + tabString;
						break;
					case 3:
						this._s = tabString + tabString + tabString;
						break;
					case 4:
						this._s = tabString + tabString + tabString + tabString;
						break;
					default:
					{
						string[] array = new string[this._indent];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = tabString;
						}
						return string.Concat(array);
					}
					}
				}
				return this._s;
			}
		}

		// Token: 0x04002E56 RID: 11862
		private readonly ExposedTabStringIndentedTextWriter _writer;

		// Token: 0x04002E57 RID: 11863
		private readonly int _indent;

		// Token: 0x04002E58 RID: 11864
		private string _s;
	}
}
