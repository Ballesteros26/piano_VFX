using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000424 RID: 1060
	internal sealed class ValidationState
	{
		// Token: 0x04001C64 RID: 7268
		public bool IsNill;

		// Token: 0x04001C65 RID: 7269
		public bool IsDefault;

		// Token: 0x04001C66 RID: 7270
		public bool NeedValidateChildren;

		// Token: 0x04001C67 RID: 7271
		public bool CheckRequiredAttribute;

		// Token: 0x04001C68 RID: 7272
		public bool ValidationSkipped;

		// Token: 0x04001C69 RID: 7273
		public int Depth;

		// Token: 0x04001C6A RID: 7274
		public XmlSchemaContentProcessing ProcessContents;

		// Token: 0x04001C6B RID: 7275
		public XmlSchemaValidity Validity;

		// Token: 0x04001C6C RID: 7276
		public SchemaElementDecl ElementDecl;

		// Token: 0x04001C6D RID: 7277
		public SchemaElementDecl ElementDeclBeforeXsi;

		// Token: 0x04001C6E RID: 7278
		public string LocalName;

		// Token: 0x04001C6F RID: 7279
		public string Namespace;

		// Token: 0x04001C70 RID: 7280
		public ConstraintStruct[] Constr;

		// Token: 0x04001C71 RID: 7281
		public StateUnion CurrentState;

		// Token: 0x04001C72 RID: 7282
		public bool HasMatched;

		// Token: 0x04001C73 RID: 7283
		public BitSet[] CurPos = new BitSet[2];

		// Token: 0x04001C74 RID: 7284
		public BitSet AllElementsSet;

		// Token: 0x04001C75 RID: 7285
		public List<RangePositionInfo> RunningPositions;

		// Token: 0x04001C76 RID: 7286
		public bool TooComplex;
	}
}
