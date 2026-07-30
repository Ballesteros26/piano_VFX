using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000CC RID: 204
	internal class MimeFormImporter : MimeImporter
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x000189B0 File Offset: 0x00016BB0
		internal override MimeParameterCollection ImportParameters()
		{
			MimeContentBinding mimeContentBinding = (MimeContentBinding)base.ImportContext.OperationBinding.Input.Extensions.Find(typeof(MimeContentBinding));
			if (mimeContentBinding == null)
			{
				return null;
			}
			if (string.Compare(mimeContentBinding.Type, "application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			MimeParameterCollection mimeParameterCollection = base.ImportContext.ImportStringParametersMessage();
			if (mimeParameterCollection == null)
			{
				return null;
			}
			mimeParameterCollection.WriterType = typeof(HtmlFormParameterWriter);
			return mimeParameterCollection;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override MimeReturn ImportReturn()
		{
			return null;
		}
	}
}
