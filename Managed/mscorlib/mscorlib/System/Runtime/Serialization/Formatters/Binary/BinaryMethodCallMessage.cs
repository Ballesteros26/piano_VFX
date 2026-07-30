using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000731 RID: 1841
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x06004C3C RID: 19516 RVA: 0x00110498 File Offset: 0x0010E698
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
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

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x00110506 File Offset: 0x0010E706
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x0011050E File Offset: 0x0010E70E
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x00110516 File Offset: 0x0010E716
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x0011051E File Offset: 0x0010E71E
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004C41 RID: 19521 RVA: 0x00110526 File Offset: 0x0010E726
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x0011052E File Offset: 0x0010E72E
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x00110536 File Offset: 0x0010E736
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x00110544 File Offset: 0x0010E744
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x0400289F RID: 10399
		private object[] _inargs;

		// Token: 0x040028A0 RID: 10400
		private string _methodName;

		// Token: 0x040028A1 RID: 10401
		private string _typeName;

		// Token: 0x040028A2 RID: 10402
		private object _methodSignature;

		// Token: 0x040028A3 RID: 10403
		private Type[] _instArgs;

		// Token: 0x040028A4 RID: 10404
		private object[] _args;

		// Token: 0x040028A5 RID: 10405
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040028A6 RID: 10406
		private object[] _properties;
	}
}
