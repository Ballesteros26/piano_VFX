using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000CD RID: 205
	internal class MimeFormReflector : MimeReflector
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00018A24 File Offset: 0x00016C24
		internal override bool ReflectParameters()
		{
			if (!ValueCollectionParameterReader.IsSupported(base.ReflectionContext.Method))
			{
				return false;
			}
			base.ReflectionContext.ReflectStringParametersMessage();
			MimeContentBinding mimeContentBinding = new MimeContentBinding();
			mimeContentBinding.Type = "application/x-www-form-urlencoded";
			base.ReflectionContext.OperationBinding.Input.Extensions.Add(mimeContentBinding);
			return true;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00002B51 File Offset: 0x00000D51
		internal override bool ReflectReturn()
		{
			return false;
		}
	}
}
