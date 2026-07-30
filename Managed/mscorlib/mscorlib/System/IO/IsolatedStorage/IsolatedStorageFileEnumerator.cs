using System;
using System.Collections;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020003ED RID: 1005
	internal class IsolatedStorageFileEnumerator : IEnumerator
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x000A987F File Offset: 0x000A7A7F
		public IsolatedStorageFileEnumerator(IsolatedStorageScope scope, string root)
		{
			this._scope = scope;
			if (Directory.Exists(root))
			{
				this._storages = Directory.GetDirectories(root, "d.*");
			}
			this._pos = -1;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000A98AE File Offset: 0x000A7AAE
		public object Current
		{
			get
			{
				if (this._pos < 0 || this._storages == null || this._pos >= this._storages.Length)
				{
					return null;
				}
				return new IsolatedStorageFile(this._scope, this._storages[this._pos]);
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000A98EC File Offset: 0x000A7AEC
		public bool MoveNext()
		{
			if (this._storages == null)
			{
				return false;
			}
			int num = this._pos + 1;
			this._pos = num;
			return num < this._storages.Length;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000A991E File Offset: 0x000A7B1E
		public void Reset()
		{
			this._pos = -1;
		}

		// Token: 0x04001862 RID: 6242
		private IsolatedStorageScope _scope;

		// Token: 0x04001863 RID: 6243
		private string[] _storages;

		// Token: 0x04001864 RID: 6244
		private int _pos;
	}
}
