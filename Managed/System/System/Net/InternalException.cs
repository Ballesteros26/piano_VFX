using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x02000437 RID: 1079
	internal class InternalException : SystemException
	{
		// Token: 0x0600208B RID: 8331 RVA: 0x0007EC10 File Offset: 0x0007CE10
		internal InternalException()
		{
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0006D1C3 File Offset: 0x0006B3C3
		internal InternalException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
