using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000815 RID: 2069
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x0600529D RID: 21149 RVA: 0x001235E0 File Offset: 0x001217E0
		public MCMDictionary(IMethodMessage message)
			: base(message)
		{
			base.MethodKeys = MCMDictionary.InternalKeys;
		}

		// Token: 0x04002B16 RID: 11030
		public static string[] InternalKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext" };
	}
}
