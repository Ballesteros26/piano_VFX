using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000707 RID: 1799
	internal interface IStreamable
	{
		// Token: 0x06004B63 RID: 19299
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x06004B64 RID: 19300
		void Write(__BinaryWriter sout);
	}
}
