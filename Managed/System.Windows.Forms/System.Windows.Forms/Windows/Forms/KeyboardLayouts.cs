using System;
using System.Reflection;
using System.Resources;

namespace System.Windows.Forms
{
	// Token: 0x020001F6 RID: 502
	internal class KeyboardLayouts
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x00075024 File Offset: 0x00073224
		public void LoadLayouts()
		{
			ResourceManager resourceManager = new ResourceManager("keyboards", Assembly.GetExecutingAssembly());
			this.keyboard_layouts = (KeyboardLayout[])resourceManager.GetObject("keyboard_table");
			this.vkey_table = (int[][])resourceManager.GetObject("vkey_table");
			this.scan_table = (short[][])resourceManager.GetObject("scan_table");
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x00075084 File Offset: 0x00073284
		public KeyboardLayout[] Layouts
		{
			get
			{
				if (this.keyboard_layouts == null)
				{
					this.LoadLayouts();
				}
				return this.keyboard_layouts;
			}
		}

		// Token: 0x04001051 RID: 4177
		private KeyboardLayout[] keyboard_layouts;

		// Token: 0x04001052 RID: 4178
		public int[][] vkey_table;

		// Token: 0x04001053 RID: 4179
		public short[][] scan_table;
	}
}
