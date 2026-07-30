using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Serializes an object graph to a series of CodeDOM statements. This class provides an abstract base class for a serializer.</summary>
	// Token: 0x0200014B RID: 331
	public class CodeDomSerializer : CodeDomSerializerBase
	{
		/// <summary>Serializes the given object, accounting for default values.</summary>
		/// <returns>A CodeDom object representing <paramref name="value" />.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060009FB RID: 2555 RVA: 0x00012784 File Offset: 0x00010984
		public virtual object SerializeAbsolute(IDesignerSerializationManager manager, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			SerializeAbsoluteContext serializeAbsoluteContext = new SerializeAbsoluteContext();
			manager.Context.Push(serializeAbsoluteContext);
			object obj = this.Serialize(manager, value);
			manager.Context.Pop();
			return obj;
		}

		/// <summary>Serializes the specified object into a CodeDOM object.</summary>
		/// <returns>A CodeDOM object representing the object that has been serialized.</returns>
		/// <param name="manager">The serialization manager to use during serialization. </param>
		/// <param name="value">The object to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060009FC RID: 2556 RVA: 0x000127D4 File Offset: 0x000109D4
		public virtual object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			object obj = null;
			bool flag = false;
			CodeExpression codeExpression = base.SerializeCreationExpression(manager, value, out flag);
			if (codeExpression != null)
			{
				if (flag)
				{
					obj = codeExpression;
				}
				else
				{
					CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
					base.SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
					base.SerializeEvents(manager, codeStatementCollection, value, new Attribute[0]);
					obj = codeStatementCollection;
				}
				base.SetExpression(manager, value, codeExpression);
			}
			return obj;
		}

		/// <summary>Serializes the specified value to a CodeDOM expression.</summary>
		/// <returns>The serialized value. This returns null if no reference expression can be obtained for the specified value, or the value cannot be serialized.</returns>
		/// <param name="manager">The serialization manager to use during serialization. </param>
		/// <param name="value">The object to serialize. </param>
		// Token: 0x060009FD RID: 2557 RVA: 0x00012846 File Offset: 0x00010A46
		[Obsolete("This method has been deprecated. Use SerializeToExpression or GetExpression instead.")]
		protected CodeExpression SerializeToReferenceExpression(IDesignerSerializationManager manager, object value)
		{
			return base.SerializeToExpression(manager, value);
		}

		/// <summary>Determines which statement group the given statement should belong to.</summary>
		/// <returns>The name of the component with which <paramref name="statement" /> should be grouped.</returns>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> for which to determine the group.</param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that <paramref name="statement" /> has been reduced to.</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> of <paramref name="statement" />.</param>
		// Token: 0x060009FE RID: 2558 RVA: 0x00012850 File Offset: 0x00010A50
		public virtual string GetTargetComponentName(CodeStatement statement, CodeExpression expression, Type targetType)
		{
			if (expression is CodeFieldReferenceExpression)
			{
				return ((CodeFieldReferenceExpression)expression).FieldName;
			}
			if (expression is CodeVariableReferenceExpression)
			{
				return ((CodeVariableReferenceExpression)expression).VariableName;
			}
			return null;
		}

		/// <summary>Serializes the given member on the given object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> representing the serialized state of <paramref name="member" />.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="owningObject">The object to which is <paramref name="member" /> attached.</param>
		/// <param name="member">The member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="owningObject" />, or <paramref name="member" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="member" /> is not a serializable type.</exception>
		// Token: 0x060009FF RID: 2559 RVA: 0x0001287C File Offset: 0x00010A7C
		public virtual CodeStatementCollection SerializeMember(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			if (base.GetExpression(manager, owningObject) == null)
			{
				string text = manager.GetName(owningObject);
				if (text == null)
				{
					text = base.GetUniqueName(manager, owningObject);
				}
				CodeExpression codeExpression = new CodeVariableReferenceExpression(text);
				base.SetExpression(manager, owningObject, codeExpression);
			}
			if (member is PropertyDescriptor)
			{
				base.SerializeProperty(manager, codeStatementCollection, owningObject, (PropertyDescriptor)member);
			}
			if (member is EventDescriptor)
			{
				base.SerializeEvent(manager, codeStatementCollection, owningObject, (EventDescriptor)member);
			}
			return codeStatementCollection;
		}

		/// <summary>Serializes the given member, accounting for default values.</summary>
		/// <returns>A CodeDom object representing <paramref name="member" />.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="owningObject">The object to which is <paramref name="member" /> attached.</param>
		/// <param name="member">The member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="owningObject" />, or <paramref name="member" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="member" /> is not a serializable type.</exception>
		// Token: 0x06000A00 RID: 2560 RVA: 0x00012918 File Offset: 0x00010B18
		public virtual CodeStatementCollection SerializeMemberAbsolute(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			SerializeAbsoluteContext serializeAbsoluteContext = new SerializeAbsoluteContext(member);
			manager.Context.Push(serializeAbsoluteContext);
			CodeStatementCollection codeStatementCollection = this.SerializeMember(manager, owningObject, member);
			manager.Context.Pop();
			return codeStatementCollection;
		}

		/// <summary>Deserializes the specified serialized CodeDOM object into an object.</summary>
		/// <returns>The deserialized CodeDOM object.</returns>
		/// <param name="manager">A serialization manager interface that is used during the deserialization process. </param>
		/// <param name="codeObject">A serialized CodeDOM object to deserialize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="codeObject" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeObject" /> is an unsupported code element.</exception>
		// Token: 0x06000A01 RID: 2561 RVA: 0x00012978 File Offset: 0x00010B78
		public virtual object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			object obj = null;
			CodeExpression codeExpression = codeObject as CodeExpression;
			if (codeExpression != null)
			{
				obj = base.DeserializeExpression(manager, null, codeExpression);
			}
			CodeStatement codeStatement = codeObject as CodeStatement;
			if (codeStatement != null)
			{
				obj = this.DeserializeStatementToInstance(manager, codeStatement);
			}
			CodeStatementCollection codeStatementCollection = codeObject as CodeStatementCollection;
			if (codeStatementCollection != null)
			{
				foreach (object obj2 in codeStatementCollection)
				{
					CodeStatement codeStatement2 = (CodeStatement)obj2;
					if (obj == null)
					{
						obj = this.DeserializeStatementToInstance(manager, codeStatement2);
					}
					else
					{
						base.DeserializeStatement(manager, codeStatement2);
					}
				}
			}
			return obj;
		}

		/// <summary>Deserializes a single statement.</summary>
		/// <returns>An object instance resulting from deserializing <paramref name="statement" />.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="statement">The statement to deserialize.</param>
		// Token: 0x06000A02 RID: 2562 RVA: 0x00012A1C File Offset: 0x00010C1C
		protected object DeserializeStatementToInstance(IDesignerSerializationManager manager, CodeStatement statement)
		{
			CodeAssignStatement codeAssignStatement = statement as CodeAssignStatement;
			if (codeAssignStatement != null)
			{
				CodeFieldReferenceExpression codeFieldReferenceExpression = codeAssignStatement.Left as CodeFieldReferenceExpression;
				if (codeFieldReferenceExpression != null)
				{
					return base.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, codeAssignStatement.Right);
				}
			}
			base.DeserializeStatement(manager, statement);
			return null;
		}
	}
}
