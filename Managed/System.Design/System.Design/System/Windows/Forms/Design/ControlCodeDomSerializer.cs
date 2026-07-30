using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200000E RID: 14
	internal class ControlCodeDomSerializer : ComponentCodeDomSerializer
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002598 File Offset: 0x00000798
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (!(value is Control))
			{
				throw new InvalidOperationException("value is not a Control");
			}
			object obj = base.Serialize(manager, value);
			CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
			if (codeStatementCollection != null && (TypeDescriptor.GetProperties(value)["Controls"].GetValue(value) as ICollection).Count > 0)
			{
				CodeExpression expression = base.GetExpression(manager, value);
				CodeStatement codeStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(expression, "SuspendLayout", Array.Empty<CodeExpression>()));
				codeStatement.UserData["statement-order"] = "begin";
				codeStatementCollection.Add(codeStatement);
				codeStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(expression, "ResumeLayout", new CodeExpression[]
				{
					new CodePrimitiveExpression(false)
				}));
				codeStatement.UserData["statement-order"] = "end";
				codeStatementCollection.Add(codeStatement);
				obj = codeStatementCollection;
			}
			return obj;
		}
	}
}
