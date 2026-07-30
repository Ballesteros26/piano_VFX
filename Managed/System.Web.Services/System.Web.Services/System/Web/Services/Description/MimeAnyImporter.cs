using System;
using System.IO;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000CB RID: 203
	internal class MimeAnyImporter : MimeImporter
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override MimeParameterCollection ImportParameters()
		{
			return null;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018950 File Offset: 0x00016B50
		internal override MimeReturn ImportReturn()
		{
			if (base.ImportContext.OperationBinding.Output.Extensions.Count == 0)
			{
				return null;
			}
			return new MimeReturn
			{
				TypeName = typeof(Stream).FullName,
				ReaderType = typeof(AnyReturnReader)
			};
		}
	}
}
