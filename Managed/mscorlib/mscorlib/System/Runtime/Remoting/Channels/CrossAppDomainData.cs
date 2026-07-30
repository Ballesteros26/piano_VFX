using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200079B RID: 1947
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x06004FA7 RID: 20391 RVA: 0x0011EA7A File Offset: 0x0011CC7A
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x0011EAA0 File Offset: 0x0011CCA0
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004FA9 RID: 20393 RVA: 0x0011EAA8 File Offset: 0x0011CCA8
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x04002A53 RID: 10835
		private object _ContextID;

		// Token: 0x04002A54 RID: 10836
		private int _DomainID;

		// Token: 0x04002A55 RID: 10837
		private string _processGuid;
	}
}
