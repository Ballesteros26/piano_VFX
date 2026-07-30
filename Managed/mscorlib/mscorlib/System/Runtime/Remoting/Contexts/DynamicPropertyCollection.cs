using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200077B RID: 1915
	internal class DynamicPropertyCollection
	{
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0011CFE0 File Offset: 0x0011B1E0
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0011CFF0 File Offset: 0x0011B1F0
		public bool RegisterDynamicProperty(IDynamicProperty prop)
		{
			bool flag2;
			lock (this)
			{
				if (this.FindProperty(prop.Name) != -1)
				{
					throw new InvalidOperationException("Another property by this name already exists");
				}
				ArrayList arrayList = new ArrayList(this._properties);
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = new DynamicPropertyCollection.DynamicPropertyReg();
				dynamicPropertyReg.Property = prop;
				IContributeDynamicSink contributeDynamicSink = prop as IContributeDynamicSink;
				if (contributeDynamicSink != null)
				{
					dynamicPropertyReg.Sink = contributeDynamicSink.GetDynamicSink();
				}
				arrayList.Add(dynamicPropertyReg);
				this._properties = arrayList;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0011D088 File Offset: 0x0011B288
		public bool UnregisterDynamicProperty(string name)
		{
			bool flag2;
			lock (this)
			{
				int num = this.FindProperty(name);
				if (num == -1)
				{
					throw new RemotingException("A property with the name " + name + " was not found");
				}
				this._properties.RemoveAt(num);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0011D0F0 File Offset: 0x0011B2F0
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				using (IEnumerator enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
						if (dynamicPropertyReg.Sink != null)
						{
							dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
						}
					}
					return;
				}
			}
			foreach (object obj2 in properties)
			{
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
				if (dynamicPropertyReg2.Sink != null)
				{
					dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
				}
			}
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0011D1B4 File Offset: 0x0011B3B4
		private int FindProperty(string name)
		{
			for (int i = 0; i < this._properties.Count; i++)
			{
				if (((DynamicPropertyCollection.DynamicPropertyReg)this._properties[i]).Property.Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04002A24 RID: 10788
		private ArrayList _properties = new ArrayList();

		// Token: 0x0200077C RID: 1916
		private class DynamicPropertyReg
		{
			// Token: 0x04002A25 RID: 10789
			public IDynamicProperty Property;

			// Token: 0x04002A26 RID: 10790
			public IDynamicMessageSink Sink;
		}
	}
}
