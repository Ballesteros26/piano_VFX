using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200081E RID: 2078
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoMethodMessage : IMethodCallMessage, IMethodMessage, IMessage, IMethodReturnMessage, IInternalMessage
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x00124BCC File Offset: 0x00122DCC
		internal void InitMessage(MonoMethod method, object[] out_args)
		{
			this.method = method;
			ParameterInfo[] parametersInternal = method.GetParametersInternal();
			int num = parametersInternal.Length;
			this.args = new object[num];
			this.arg_types = new byte[num];
			this.asyncResult = null;
			this.call_type = CallType.Sync;
			this.names = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.names[i] = parametersInternal[i].Name;
			}
			bool flag = out_args != null;
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				bool isOut = parametersInternal[j].IsOut;
				byte b;
				if (parametersInternal[j].ParameterType.IsByRef)
				{
					if (flag)
					{
						this.args[j] = out_args[num2++];
					}
					b = 2;
					if (!isOut)
					{
						b |= 1;
					}
				}
				else
				{
					b = 1;
					if (isOut)
					{
						b |= 4;
					}
				}
				this.arg_types[j] = b;
			}
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00124CAD File Offset: 0x00122EAD
		public MonoMethodMessage(MethodBase method, object[] out_args)
		{
			if (method != null)
			{
				this.InitMessage((MonoMethod)method, out_args);
				return;
			}
			this.args = null;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x00124CD4 File Offset: 0x00122ED4
		internal MonoMethodMessage(MethodInfo minfo, object[] in_args, object[] out_args)
		{
			this.InitMessage((MonoMethod)minfo, out_args);
			int num = in_args.Length;
			for (int i = 0; i < num; i++)
			{
				this.args[i] = in_args[i];
			}
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x00124D0F File Offset: 0x00122F0F
		private static MethodInfo GetMethodInfo(Type type, string methodName)
		{
			MethodInfo methodInfo = type.GetMethod(methodName);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("Could not find '{0}' in {1}", methodName, type), "methodName");
			}
			return methodInfo;
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x00124D38 File Offset: 0x00122F38
		public MonoMethodMessage(Type type, string methodName, object[] in_args)
			: this(MonoMethodMessage.GetMethodInfo(type, methodName), in_args, null)
		{
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x00124D49 File Offset: 0x00122F49
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MCMDictionary(this);
				}
				return this.properties;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x00124D65 File Offset: 0x00122F65
		public int ArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				return this.args.Length;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x00124D84 File Offset: 0x00122F84
		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x00124D8C File Offset: 0x00122F8C
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x00124D94 File Offset: 0x00122F94
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.ctx;
			}
			set
			{
				this.ctx = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x00124D9D File Offset: 0x00122F9D
		public MethodBase MethodBase
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x00124DA5 File Offset: 0x00122FA5
		public string MethodName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.Name;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x00124DC8 File Offset: 0x00122FC8
		public object MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					ParameterInfo[] parameters = this.method.GetParameters();
					this.methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this.methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this.methodSignature;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x00124E1B File Offset: 0x0012301B
		public string TypeName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.DeclaringType.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x00124E41 File Offset: 0x00123041
		// (set) Token: 0x06005329 RID: 21289 RVA: 0x00124E49 File Offset: 0x00123049
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00124E52 File Offset: 0x00123052
		public object GetArg(int arg_num)
		{
			if (this.args == null)
			{
				return null;
			}
			return this.args[arg_num];
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x00124E66 File Offset: 0x00123066
		public string GetArgName(int arg_num)
		{
			if (this.args == null)
			{
				return string.Empty;
			}
			return this.names[arg_num];
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00124E80 File Offset: 0x00123080
		public int InArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 1) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x00124EC8 File Offset: 0x001230C8
		public object[] InArgs
		{
			get
			{
				object[] array = new object[this.InArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 1) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00124F1C File Offset: 0x0012311C
		public object GetInArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00124F60 File Offset: 0x00123160
		public string GetInArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00124FA3 File Offset: 0x001231A3
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x00124FAC File Offset: 0x001231AC
		public int OutArgCount
		{
			get
			{
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 2) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x00124FE8 File Offset: 0x001231E8
		public object[] OutArgs
		{
			get
			{
				if (this.args == null)
				{
					return null;
				}
				object[] array = new object[this.OutArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 2) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x00125044 File Offset: 0x00123244
		public object ReturnValue
		{
			get
			{
				return this.rval;
			}
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0012504C File Offset: 0x0012324C
		public object GetOutArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00125090 File Offset: 0x00123290
		public string GetOutArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x001250D3 File Offset: 0x001232D3
		// (set) Token: 0x06005337 RID: 21303 RVA: 0x001250DB File Offset: 0x001232DB
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x001250E4 File Offset: 0x001232E4
		bool IInternalMessage.HasProperties()
		{
			return this.properties != null;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x001250EF File Offset: 0x001232EF
		public bool IsAsync
		{
			get
			{
				return this.asyncResult != null;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x001250FA File Offset: 0x001232FA
		public AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x00125102 File Offset: 0x00123302
		internal CallType CallType
		{
			get
			{
				if (this.call_type == CallType.Sync && RemotingServices.IsOneWay(this.method))
				{
					this.call_type = CallType.OneWay;
				}
				return this.call_type;
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00125128 File Offset: 0x00123328
		public bool NeedsOutProcessing(out int outCount)
		{
			bool flag = false;
			outCount = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0)
				{
					outCount++;
				}
				else if ((b & 4) != 0)
				{
					flag = true;
				}
			}
			return outCount > 0 || flag;
		}

		// Token: 0x04002B3B RID: 11067
		private MonoMethod method;

		// Token: 0x04002B3C RID: 11068
		private object[] args;

		// Token: 0x04002B3D RID: 11069
		private string[] names;

		// Token: 0x04002B3E RID: 11070
		private byte[] arg_types;

		// Token: 0x04002B3F RID: 11071
		public LogicalCallContext ctx;

		// Token: 0x04002B40 RID: 11072
		public object rval;

		// Token: 0x04002B41 RID: 11073
		public Exception exc;

		// Token: 0x04002B42 RID: 11074
		private AsyncResult asyncResult;

		// Token: 0x04002B43 RID: 11075
		private CallType call_type;

		// Token: 0x04002B44 RID: 11076
		private string uri;

		// Token: 0x04002B45 RID: 11077
		private MCMDictionary properties;

		// Token: 0x04002B46 RID: 11078
		private Type[] methodSignature;

		// Token: 0x04002B47 RID: 11079
		private Identity identity;

		// Token: 0x04002B48 RID: 11080
		internal static string CallContextKey = "__CallContext";

		// Token: 0x04002B49 RID: 11081
		internal static string UriKey = "__Uri";
	}
}
