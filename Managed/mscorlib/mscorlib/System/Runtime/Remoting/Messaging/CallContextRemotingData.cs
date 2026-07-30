using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007F6 RID: 2038
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x00121683 File Offset: 0x0011F883
		// (set) Token: 0x060051CB RID: 20939 RVA: 0x0012168B File Offset: 0x0011F88B
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x00121694 File Offset: 0x0011F894
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0012169F File Offset: 0x0011F89F
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x04002ACE RID: 10958
		private string _logicalCallID;
	}
}
