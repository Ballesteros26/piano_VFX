using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x02000063 RID: 99
	internal abstract class BaseParamsCollection : WebROCollection
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00007576 File Offset: 0x00005776
		public BaseParamsCollection(HttpRequest request)
		{
			this._request = request;
			base.IsReadOnly = true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000758C File Offset: 0x0000578C
		private void LoadInfo()
		{
			if (this._loaded)
			{
				return;
			}
			base.IsReadOnly = false;
			this.InsertInfo();
			base.IsReadOnly = true;
			this._loaded = true;
		}

		// Token: 0x06000411 RID: 1041
		protected abstract void InsertInfo();

		// Token: 0x06000412 RID: 1042 RVA: 0x000075B2 File Offset: 0x000057B2
		public override string Get(int index)
		{
			this.LoadInfo();
			return base.Get(index);
		}

		// Token: 0x06000413 RID: 1043
		protected abstract string InternalGet(string name);

		// Token: 0x06000414 RID: 1044 RVA: 0x000075C4 File Offset: 0x000057C4
		public override string Get(string name)
		{
			if (!this._loaded)
			{
				string text = this.InternalGet(name);
				if (text != null && text.Length > 0)
				{
					return text;
				}
				this.LoadInfo();
			}
			return base.Get(name);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000075FC File Offset: 0x000057FC
		public override string GetKey(int index)
		{
			this.LoadInfo();
			return base.GetKey(index);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000760C File Offset: 0x0000580C
		public override string[] GetValues(int index)
		{
			string text = this.Get(index);
			if (text == null)
			{
				return null;
			}
			return new string[] { text };
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00007630 File Offset: 0x00005830
		public override string[] GetValues(string name)
		{
			string text = this.Get(name);
			if (text == null)
			{
				return null;
			}
			return new string[] { text };
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00007654 File Offset: 0x00005854
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000765B File Offset: 0x0000585B
		public override string[] AllKeys
		{
			get
			{
				this.LoadInfo();
				return base.AllKeys;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00007669 File Offset: 0x00005869
		public override int Count
		{
			get
			{
				this.LoadInfo();
				return base.Count;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00007677 File Offset: 0x00005877
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.LoadInfo();
				return base.Keys;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00007685 File Offset: 0x00005885
		public override IEnumerator GetEnumerator()
		{
			this.LoadInfo();
			return base.GetEnumerator();
		}

		// Token: 0x04000E4A RID: 3658
		protected HttpRequest _request;

		// Token: 0x04000E4B RID: 3659
		protected bool _loaded;
	}
}
