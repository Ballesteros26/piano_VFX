using System;

namespace System.Web.UI
{
	// Token: 0x0200022F RID: 559
	internal class TemplateBinding
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x0003DA75 File Offset: 0x0003BC75
		public TemplateBinding(Type controlType, string controlProperty, string controlId, string fieldName)
		{
			this.ControlType = controlType;
			this.ControlProperty = controlProperty;
			this.ControlId = controlId;
			this.FieldName = fieldName;
		}

		// Token: 0x0400158C RID: 5516
		public Type ControlType;

		// Token: 0x0400158D RID: 5517
		public string ControlProperty;

		// Token: 0x0400158E RID: 5518
		public string ControlId;

		// Token: 0x0400158F RID: 5519
		public string FieldName;
	}
}
