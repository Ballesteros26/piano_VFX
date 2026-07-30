using System;

namespace System.Windows.Forms
{
	// Token: 0x020001F7 RID: 503
	[Serializable]
	internal class KeyboardLayout
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x000750A0 File Offset: 0x000732A0
		public KeyboardLayout(int lcid, string name, ScanTableIndex scan_index, VKeyTableIndex vkey_index, uint[][] keys)
		{
			this.Lcid = lcid;
			this.Name = name;
			this.ScanIndex = scan_index;
			this.VKeyIndex = vkey_index;
			this.Keys = keys;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000750D0 File Offset: 0x000732D0
		public KeyboardLayout(int lcid, string name, int scan_index, int vkey_index, uint[][] keys)
			: this(lcid, name, (ScanTableIndex)scan_index, (VKeyTableIndex)vkey_index, keys)
		{
		}

		// Token: 0x04001054 RID: 4180
		public int Lcid;

		// Token: 0x04001055 RID: 4181
		public string Name;

		// Token: 0x04001056 RID: 4182
		public ScanTableIndex ScanIndex;

		// Token: 0x04001057 RID: 4183
		public VKeyTableIndex VKeyIndex;

		// Token: 0x04001058 RID: 4184
		public uint[][] Keys;
	}
}
