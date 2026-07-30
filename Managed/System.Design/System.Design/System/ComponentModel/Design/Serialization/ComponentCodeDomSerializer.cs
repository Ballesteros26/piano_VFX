using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000151 RID: 337
	internal class ComponentCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00014A50 File Offset: 0x00012C50
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
			RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
			if (rootContext != null && rootContext.Value == value)
			{
				return rootContext.Expression;
			}
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			if (((IComponent)value).Site == null)
			{
				base.ReportError(manager, "Component of type '" + value.GetType().Name + "' not sited");
				return codeStatementCollection;
			}
			string name = manager.GetName(value);
			CodeExpression codeExpression;
			if (rootContext != null)
			{
				codeExpression = new CodeFieldReferenceExpression(rootContext.Expression, name);
			}
			else
			{
				codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), name);
			}
			base.SetExpression(manager, value, codeExpression);
			ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
			if (expressionContext == null || expressionContext.PresetValue != value || (expressionContext.PresetValue == value && (expressionContext.Expression is CodeFieldReferenceExpression || expressionContext.Expression is CodePropertyReferenceExpression)))
			{
				bool flag = true;
				codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
				codeStatementCollection.Add(new CodeCommentStatement(name));
				codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
				if (!(((IComponent)value).Site is INestedSite))
				{
					CodeStatement codeStatement = new CodeAssignStatement(codeExpression, base.SerializeCreationExpression(manager, value, out flag));
					codeStatement.UserData["statement-order"] = "initializer";
					codeStatementCollection.Add(codeStatement);
				}
				base.SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
				base.SerializeEvents(manager, codeStatementCollection, value, Array.Empty<Attribute>());
			}
			return codeStatementCollection;
		}
	}
}
