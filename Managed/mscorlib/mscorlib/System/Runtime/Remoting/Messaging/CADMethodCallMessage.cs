using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007FE RID: 2046
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x0012234B File Offset: 0x0012054B
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00122354 File Offset: 0x00120554
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00122374 File Offset: 0x00120574
		internal CADMethodCallMessage(IMethodCallMessage callMsg)
			: base(callMsg)
		{
			this._uri = callMsg.Uri;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001223E4 File Offset: 0x001205E4
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

		// Token: 0x06005203 RID: 20995 RVA: 0x0012241E File Offset: 0x0012061E
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0012242D File Offset: 0x0012062D
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x04002AF4 RID: 10996
		private string _uri;
	}
}
