using System;
using System.Collections;

namespace System.Runtime.Remoting
{
	// Token: 0x02000756 RID: 1878
	internal class ChannelData
	{
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x00118D51 File Offset: 0x00116F51
		internal ArrayList ServerProviders
		{
			get
			{
				if (this._serverProviders == null)
				{
					this._serverProviders = new ArrayList();
				}
				return this._serverProviders;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x00118D6C File Offset: 0x00116F6C
		public ArrayList ClientProviders
		{
			get
			{
				if (this._clientProviders == null)
				{
					this._clientProviders = new ArrayList();
				}
				return this._clientProviders;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x00118D87 File Offset: 0x00116F87
		public Hashtable CustomProperties
		{
			get
			{
				if (this._customProperties == null)
				{
					this._customProperties = new Hashtable();
				}
				return this._customProperties;
			}
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x00118DA4 File Offset: 0x00116FA4
		public void CopyFrom(ChannelData other)
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
			if (this.DelayLoadAsClientChannel == null)
			{
				this.DelayLoadAsClientChannel = other.DelayLoadAsClientChannel;
			}
			if (other._customProperties != null)
			{
				foreach (object obj in other._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
					{
						this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (this._serverProviders == null && other._serverProviders != null)
			{
				foreach (object obj2 in other._serverProviders)
				{
					ProviderData providerData = (ProviderData)obj2;
					ProviderData providerData2 = new ProviderData();
					providerData2.CopyFrom(providerData);
					this.ServerProviders.Add(providerData2);
				}
			}
			if (this._clientProviders == null && other._clientProviders != null)
			{
				foreach (object obj3 in other._clientProviders)
				{
					ProviderData providerData3 = (ProviderData)obj3;
					ProviderData providerData4 = new ProviderData();
					providerData4.CopyFrom(providerData3);
					this.ClientProviders.Add(providerData4);
				}
			}
		}

		// Token: 0x040029B8 RID: 10680
		internal string Ref;

		// Token: 0x040029B9 RID: 10681
		internal string Type;

		// Token: 0x040029BA RID: 10682
		internal string Id;

		// Token: 0x040029BB RID: 10683
		internal string DelayLoadAsClientChannel;

		// Token: 0x040029BC RID: 10684
		private ArrayList _serverProviders = new ArrayList();

		// Token: 0x040029BD RID: 10685
		private ArrayList _clientProviders = new ArrayList();

		// Token: 0x040029BE RID: 10686
		private Hashtable _customProperties = new Hashtable();
	}
}
