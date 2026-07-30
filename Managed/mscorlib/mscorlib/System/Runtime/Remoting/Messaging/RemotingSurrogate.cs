using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000821 RID: 2081
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x0600533F RID: 21311 RVA: 0x00125184 File Offset: 0x00123384
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RemotingServices.GetRealProxy(obj).GetObjectData(si, sc);
				return;
			}
			RemotingServices.GetObjectData(obj, si, sc);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException();
		}
	}
}
