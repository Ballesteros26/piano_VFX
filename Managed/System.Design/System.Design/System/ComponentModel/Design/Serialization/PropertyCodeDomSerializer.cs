using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200015B RID: 347
	internal class PropertyCodeDomSerializer : MemberCodeDomSerializer
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00015868 File Offset: 0x00013A68
		public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)descriptor;
			if (propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
			{
				this.SerializeContentProperty(manager, value, propertyDescriptor, statements);
				return;
			}
			this.SerializeNormalProperty(manager, value, propertyDescriptor, statements);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000158E0 File Offset: 0x00013AE0
		private void SerializeNormalProperty(IDesignerSerializationManager manager, object instance, PropertyDescriptor descriptor, CodeStatementCollection statements)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
			RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
			CodeExpression codeExpression;
			if (expressionContext != null && expressionContext.PresetValue == instance && expressionContext.Expression != null)
			{
				codeExpression = new CodePropertyReferenceExpression(expressionContext.Expression, descriptor.Name);
			}
			else if (rootContext != null && rootContext.Value == instance)
			{
				codeExpression = new CodePropertyReferenceExpression(rootContext.Expression, descriptor.Name);
			}
			else
			{
				codeExpression = new CodePropertyReferenceExpression
				{
					PropertyName = descriptor.Name,
					TargetObject = base.SerializeToExpression(manager, instance)
				};
			}
			MemberRelationship relationship = this.GetRelationship(manager, instance, descriptor);
			CodeExpression codeExpression2;
			if (!relationship.IsEmpty)
			{
				codeExpression2 = new CodePropertyReferenceExpression
				{
					PropertyName = relationship.Member.Name,
					TargetObject = base.SerializeToExpression(manager, relationship.Owner)
				};
			}
			else
			{
				codeExpression2 = base.SerializeToExpression(manager, descriptor.GetValue(instance));
			}
			if (codeExpression2 == null || codeExpression == null)
			{
				base.ReportError(manager, "Cannot serialize " + ((IComponent)instance).Site.Name + "." + descriptor.Name, string.Concat(new string[]
				{
					"Property Name: ",
					descriptor.Name,
					Environment.NewLine,
					"Property Type: ",
					descriptor.PropertyType.Name,
					Environment.NewLine
				}));
				return;
			}
			codeAssignStatement.Left = codeExpression;
			codeAssignStatement.Right = codeExpression2;
			statements.Add(codeAssignStatement);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00015A78 File Offset: 0x00013C78
		private void SerializeContentProperty(IDesignerSerializationManager manager, object instance, PropertyDescriptor descriptor, CodeStatementCollection statements)
		{
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.PropertyName = descriptor.Name;
			object value = descriptor.GetValue(instance);
			ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
			if (expressionContext != null && expressionContext.PresetValue == instance)
			{
				codePropertyReferenceExpression.TargetObject = expressionContext.Expression;
			}
			else
			{
				codePropertyReferenceExpression.TargetObject = base.SerializeToExpression(manager, instance);
			}
			CodeDomSerializer codeDomSerializer = manager.GetSerializer(value.GetType(), typeof(CodeDomSerializer)) as CodeDomSerializer;
			if (codePropertyReferenceExpression.TargetObject != null && codeDomSerializer != null)
			{
				manager.Context.Push(new ExpressionContext(codePropertyReferenceExpression, codePropertyReferenceExpression.GetType(), null, value));
				object obj = codeDomSerializer.Serialize(manager, value);
				manager.Context.Pop();
				CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
				if (codeStatementCollection != null)
				{
					statements.AddRange(codeStatementCollection);
				}
				CodeStatement codeStatement = obj as CodeStatement;
				if (codeStatement != null)
				{
					statements.Add(codeStatement);
				}
				CodeExpression codeExpression = obj as CodeExpression;
				if (codeExpression != null)
				{
					statements.Add(new CodeAssignStatement(codePropertyReferenceExpression, codeExpression));
				}
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00015B7C File Offset: 0x00013D7C
		public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)descriptor;
			if (propertyDescriptor.Attributes.Contains(DesignOnlyAttribute.Yes))
			{
				return false;
			}
			SerializeAbsoluteContext serializeAbsoluteContext = manager.Context[typeof(SerializeAbsoluteContext)] as SerializeAbsoluteContext;
			if (serializeAbsoluteContext != null && serializeAbsoluteContext.ShouldSerialize(descriptor))
			{
				return true;
			}
			bool flag = propertyDescriptor.ShouldSerializeValue(value);
			if (!flag && !this.GetRelationship(manager, value, descriptor).IsEmpty)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00015C18 File Offset: 0x00013E18
		private MemberRelationship GetRelationship(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			MemberRelationshipService memberRelationshipService = manager.GetService(typeof(MemberRelationshipService)) as MemberRelationshipService;
			if (memberRelationshipService != null)
			{
				return memberRelationshipService[value, descriptor];
			}
			return MemberRelationship.Empty;
		}
	}
}
