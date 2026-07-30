using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007FF RID: 2047
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06005205 RID: 20997 RVA: 0x00122438 File Offset: 0x00120638
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00122458 File Offset: 0x00120658
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
			: base(retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			this._sig = CADMessageBase.GetSignature(base.GetMethod(), true);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00122510 File Offset: 0x00120710
		internal ArrayList GetArguments()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				arrayList = new ArrayList((object[])CADSerializer.DeserializeObject(new MemoryStream(this._serializedArgs)));
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0012241E File Offset: 0x0012061E
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0012254A File Offset: 0x0012074A
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00122559 File Offset: 0x00120759
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x0012242D File Offset: 0x0012062D
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x04002AF5 RID: 10997
		private object _returnValue;

		// Token: 0x04002AF6 RID: 10998
		private CADArgHolder _exception;

		// Token: 0x04002AF7 RID: 10999
		private Type[] _sig;
	}
}
