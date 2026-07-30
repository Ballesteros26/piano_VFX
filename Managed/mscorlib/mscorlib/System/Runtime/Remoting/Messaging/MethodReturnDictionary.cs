using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200081B RID: 2075
	internal class MethodReturnDictionary : MessageDictionary
	{
		// Token: 0x060052FB RID: 21243 RVA: 0x00124878 File Offset: 0x00122A78
		public MethodReturnDictionary(IMethodReturnMessage message)
			: base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
				return;
			}
			base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
		}

		// Token: 0x04002B32 RID: 11058
		public static string[] InternalReturnKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__OutArgs", "__Return", "__CallContext" };

		// Token: 0x04002B33 RID: 11059
		public static string[] InternalExceptionKeys = new string[] { "__CallContext" };
	}
}
