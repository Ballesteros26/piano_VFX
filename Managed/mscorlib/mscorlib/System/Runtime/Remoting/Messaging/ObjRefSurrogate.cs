using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000822 RID: 2082
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005342 RID: 21314 RVA: 0x001251B0 File Offset: 0x001233B0
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x001251D7 File Offset: 0x001233D7
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}
	}
}
