using System;
using System.Collections;

namespace System.Net.Configuration
{
	// Token: 0x02000698 RID: 1688
	internal class ConnectionManagementData
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x000C3DC8 File Offset: 0x000C1FC8
		public ConnectionManagementData(object parent)
		{
			this.data = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			if (parent != null && parent is ConnectionManagementData)
			{
				ConnectionManagementData connectionManagementData = (ConnectionManagementData)parent;
				foreach (object obj in connectionManagementData.data.Keys)
				{
					string text = (string)obj;
					this.data[text] = connectionManagementData.data[text];
				}
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000C3E64 File Offset: 0x000C2064
		public void Add(string address, string nconns)
		{
			if (nconns == null || nconns == "")
			{
				nconns = "2";
			}
			this.data[address] = uint.Parse(nconns);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000C3E94 File Offset: 0x000C2094
		public void Add(string address, int nconns)
		{
			this.data[address] = (uint)nconns;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000C3EA8 File Offset: 0x000C20A8
		public void Remove(string address)
		{
			this.data.Remove(address);
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000C3EB6 File Offset: 0x000C20B6
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000C3EC4 File Offset: 0x000C20C4
		public uint GetMaxConnections(string hostOrIP)
		{
			object obj = this.data[hostOrIP];
			if (obj == null)
			{
				obj = this.data["*"];
			}
			if (obj == null)
			{
				return 2U;
			}
			return (uint)obj;
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000C3EFD File Offset: 0x000C20FD
		public Hashtable Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x04002A5B RID: 10843
		private Hashtable data;

		// Token: 0x04002A5C RID: 10844
		private const int defaultMaxConnections = 2;
	}
}
