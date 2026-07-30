using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000806 RID: 2054
	[Serializable]
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005236 RID: 21046 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005237 RID: 21047 RVA: 0x0000A42E File Offset: 0x0000862E
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06005238 RID: 21048 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x0000A42E File Offset: 0x0000862E
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x00122B09 File Offset: 0x00120D09
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x0000A42E File Offset: 0x0000862E
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x0000A42E File Offset: 0x0000862E
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x00122B09 File Offset: 0x00120D09
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600523E RID: 21054 RVA: 0x00122B10 File Offset: 0x00120D10
		// (set) Token: 0x0600523F RID: 21055 RVA: 0x00122B18 File Offset: 0x00120D18
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0000A42E File Offset: 0x0000862E
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00122B09 File Offset: 0x00120D09
		public string GetArgName(int arg_num)
		{
			return "unknown";
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005242 RID: 21058 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int InArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0000A42E File Offset: 0x0000862E
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0000A42E File Offset: 0x0000862E
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x0000A42E File Offset: 0x0000862E
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06005246 RID: 21062 RVA: 0x0000A42E File Offset: 0x0000862E
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002B04 RID: 11012
		private string _uri = "Exception";
	}
}
