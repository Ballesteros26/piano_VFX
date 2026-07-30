using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000732 RID: 1842
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x06004C45 RID: 19525 RVA: 0x00110584 File Offset: 0x0010E784
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x001105DF File Offset: 0x0010E7DF
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x001105E7 File Offset: 0x0010E7E7
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x001105EF File Offset: 0x0010E7EF
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004C49 RID: 19529 RVA: 0x001105F7 File Offset: 0x0010E7F7
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004C4A RID: 19530 RVA: 0x001105FF File Offset: 0x0010E7FF
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0011060C File Offset: 0x0010E80C
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040028A7 RID: 10407
		private object[] _outargs;

		// Token: 0x040028A8 RID: 10408
		private Exception _exception;

		// Token: 0x040028A9 RID: 10409
		private object _returnValue;

		// Token: 0x040028AA RID: 10410
		private object[] _args;

		// Token: 0x040028AB RID: 10411
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040028AC RID: 10412
		private object[] _properties;
	}
}
