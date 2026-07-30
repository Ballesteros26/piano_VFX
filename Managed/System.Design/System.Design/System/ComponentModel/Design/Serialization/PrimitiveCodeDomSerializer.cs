using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200015A RID: 346
	internal class PrimitiveCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x00015858 File Offset: 0x00013A58
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			return new CodePrimitiveExpression(value);
		}
	}
}
