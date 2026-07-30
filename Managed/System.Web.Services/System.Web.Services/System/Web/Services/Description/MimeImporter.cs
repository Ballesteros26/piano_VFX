using System;
using System.CodeDom;

namespace System.Web.Services.Description
{
	// Token: 0x020000D6 RID: 214
	internal abstract class MimeImporter
	{
		// Token: 0x0600056F RID: 1391
		internal abstract MimeParameterCollection ImportParameters();

		// Token: 0x06000570 RID: 1392
		internal abstract MimeReturn ImportReturn();

		// Token: 0x06000571 RID: 1393 RVA: 0x0000210D File Offset: 0x0000030D
		internal virtual void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
		{
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000210D File Offset: 0x0000030D
		internal virtual void AddClassMetadata(CodeTypeDeclaration codeClass)
		{
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00018CC0 File Offset: 0x00016EC0
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00018CC8 File Offset: 0x00016EC8
		internal HttpProtocolImporter ImportContext
		{
			get
			{
				return this.protocol;
			}
			set
			{
				this.protocol = value;
			}
		}

		// Token: 0x04000394 RID: 916
		private HttpProtocolImporter protocol;
	}
}
