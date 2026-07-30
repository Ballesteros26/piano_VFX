using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200001F RID: 31
	internal class Charset
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public Charset()
		{
			this.flags = CharsetFlags.Read | CharsetFlags.Switch;
			this.id = CharsetType.General;
			this.file = string.Empty;
			this.ReadMap();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005C0C File Offset: 0x00003E0C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005C14 File Offset: 0x00003E14
		public Charcode Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005C20 File Offset: 0x00003E20
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005C28 File Offset: 0x00003E28
		public CharsetFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005C34 File Offset: 0x00003E34
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005C3C File Offset: 0x00003E3C
		public CharsetType ID
		{
			get
			{
				return this.id;
			}
			set
			{
				if (value != CharsetType.General)
				{
					if (value == CharsetType.Symbol)
					{
						this.id = CharsetType.Symbol;
						return;
					}
				}
				this.id = CharsetType.General;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005C6C File Offset: 0x00003E6C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005C74 File Offset: 0x00003E74
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				if (this.file != value)
				{
					this.file = value;
				}
			}
		}

		// Token: 0x1700002E RID: 46
		public StandardCharCode this[int c]
		{
			get
			{
				return this.code[c];
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public bool ReadMap()
		{
			CharsetType charsetType = this.id;
			if (charsetType != CharsetType.General)
			{
				if (charsetType != CharsetType.Symbol)
				{
					return false;
				}
				if (this.file == string.Empty)
				{
					this.code = Charcode.AnsiSymbol;
					return true;
				}
				return true;
			}
			else
			{
				if (this.file == string.Empty)
				{
					this.code = Charcode.AnsiGeneric;
					return true;
				}
				return true;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D10 File Offset: 0x00003F10
		public char StdCharCode(string name)
		{
			return ' ';
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005D14 File Offset: 0x00003F14
		public string StdCharName(char code)
		{
			return string.Empty;
		}

		// Token: 0x0400005F RID: 95
		private CharsetType id;

		// Token: 0x04000060 RID: 96
		private CharsetFlags flags;

		// Token: 0x04000061 RID: 97
		private Charcode code;

		// Token: 0x04000062 RID: 98
		private string file;
	}
}
