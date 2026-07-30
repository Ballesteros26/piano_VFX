using System;

namespace System.Web.UI
{
	// Token: 0x020001DF RID: 479
	internal interface IScriptManager
	{
		// Token: 0x06001386 RID: 4998
		void RegisterOnSubmitStatementExternal(Control control, Type type, string key, string script);

		// Token: 0x06001387 RID: 4999
		void RegisterExpandoAttributeExternal(Control control, string controlId, string attributeName, string attributeValue, bool encode);

		// Token: 0x06001388 RID: 5000
		void RegisterHiddenFieldExternal(Control control, string hiddenFieldName, string hiddenFieldInitialValue);

		// Token: 0x06001389 RID: 5001
		void RegisterStartupScriptExternal(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x0600138A RID: 5002
		void RegisterArrayDeclarationExternal(Control control, string arrayName, string arrayValue);

		// Token: 0x0600138B RID: 5003
		void RegisterClientScriptBlockExternal(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x0600138C RID: 5004
		void RegisterClientScriptIncludeExternal(Control control, Type type, string key, string url);

		// Token: 0x0600138D RID: 5005
		void RegisterClientScriptResourceExternal(Control control, Type type, string resourceName);
	}
}
