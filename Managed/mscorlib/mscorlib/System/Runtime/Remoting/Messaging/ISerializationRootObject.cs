using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000811 RID: 2065
	internal interface ISerializationRootObject
	{
		// Token: 0x0600526F RID: 21103
		void RootSetObjectData(SerializationInfo info, StreamingContext context);
	}
}
