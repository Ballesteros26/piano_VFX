using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000757 RID: 1879
	internal class ProviderData
	{
		// Token: 0x06004DBA RID: 19898 RVA: 0x00118F88 File Offset: 0x00117188
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData sinkProviderData = (SinkProviderData)obj2;
					this.CustomData.Add(sinkProviderData);
				}
			}
		}

		// Token: 0x040029BF RID: 10687
		internal string Ref;

		// Token: 0x040029C0 RID: 10688
		internal string Type;

		// Token: 0x040029C1 RID: 10689
		internal string Id;

		// Token: 0x040029C2 RID: 10690
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x040029C3 RID: 10691
		internal IList CustomData;
	}
}
