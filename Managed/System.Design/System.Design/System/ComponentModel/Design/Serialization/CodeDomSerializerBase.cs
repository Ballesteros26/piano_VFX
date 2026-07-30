using System;
using System.CodeDom;
using System.Collections;
using System.Reflection;
using Unity;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a base class for <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> classes.</summary>
	// Token: 0x0200014C RID: 332
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class CodeDomSerializerBase
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x00002352 File Offset: 0x00000552
		internal CodeDomSerializerBase()
		{
		}

		/// <summary>Serializes the given object into an expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> object if <paramref name="value" /> can be serialized; otherwise, null.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> is null.</exception>
		// Token: 0x06000A04 RID: 2564 RVA: 0x00012A60 File Offset: 0x00010C60
		protected CodeExpression SerializeToExpression(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			CodeExpression codeExpression = null;
			if (value != null)
			{
				codeExpression = this.GetExpression(manager, value);
			}
			if (codeExpression == null)
			{
				CodeDomSerializer serializer = this.GetSerializer(manager, value);
				if (serializer != null)
				{
					object obj = serializer.Serialize(manager, value);
					codeExpression = obj as CodeExpression;
					if (codeExpression == null)
					{
						CodeStatement codeStatement = obj as CodeStatement;
						CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
						if (codeStatement != null || codeStatementCollection != null)
						{
							CodeStatementCollection codeStatementCollection2 = null;
							StatementContext statementContext = manager.Context[typeof(StatementContext)] as StatementContext;
							if (statementContext != null && value != null)
							{
								codeStatementCollection2 = statementContext.StatementCollection[value];
							}
							if (codeStatementCollection2 == null)
							{
								codeStatementCollection2 = manager.Context[typeof(CodeStatementCollection)] as CodeStatementCollection;
							}
							if (codeStatementCollection2 != null)
							{
								if (codeStatementCollection != null)
								{
									codeStatementCollection2.AddRange(codeStatementCollection);
								}
								else
								{
									codeStatementCollection2.Add(codeStatement);
								}
							}
						}
					}
					if (codeExpression == null && value != null)
					{
						codeExpression = this.GetExpression(manager, value);
					}
				}
				else
				{
					this.ReportError(manager, "No serializer found for type '" + value.GetType().Name + "'");
				}
			}
			return codeExpression;
		}

		/// <summary>Locates a serializer for the given object value.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> that is appropriate for <paramref name="value" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object specifying the serializer to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A05 RID: 2565 RVA: 0x00012B70 File Offset: 0x00010D70
		protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, object value)
		{
			DesignerSerializerAttribute designerSerializerAttribute2;
			DesignerSerializerAttribute designerSerializerAttribute = (designerSerializerAttribute2 = null);
			CodeDomSerializer codeDomSerializer;
			if (value == null)
			{
				codeDomSerializer = this.GetSerializer(manager, null);
			}
			else
			{
				foreach (object obj in TypeDescriptor.GetAttributes(value))
				{
					DesignerSerializerAttribute designerSerializerAttribute3 = ((Attribute)obj) as DesignerSerializerAttribute;
					if (designerSerializerAttribute3 != null && manager.GetType(designerSerializerAttribute3.SerializerBaseTypeName) == typeof(CodeDomSerializer))
					{
						designerSerializerAttribute = designerSerializerAttribute3;
						break;
					}
				}
				foreach (object obj2 in TypeDescriptor.GetAttributes(value.GetType()))
				{
					DesignerSerializerAttribute designerSerializerAttribute4 = ((Attribute)obj2) as DesignerSerializerAttribute;
					if (designerSerializerAttribute4 != null && manager.GetType(designerSerializerAttribute4.SerializerBaseTypeName) == typeof(CodeDomSerializer))
					{
						designerSerializerAttribute2 = designerSerializerAttribute4;
						break;
					}
				}
				if (designerSerializerAttribute2 != null && designerSerializerAttribute != null && designerSerializerAttribute2.SerializerTypeName != designerSerializerAttribute.SerializerTypeName)
				{
					codeDomSerializer = Activator.CreateInstance(manager.GetType(designerSerializerAttribute.SerializerTypeName)) as CodeDomSerializer;
				}
				else
				{
					codeDomSerializer = this.GetSerializer(manager, value.GetType());
				}
			}
			return codeDomSerializer;
		}

		/// <summary>Locates a serializer for the given type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> that is appropriate for <paramref name="valueType" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="valueType">The <see cref="T:System.Type" /> specifying the serializer to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="valueType" /> is null.</exception>
		// Token: 0x06000A06 RID: 2566 RVA: 0x00012CC0 File Offset: 0x00010EC0
		protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, Type valueType)
		{
			return manager.GetSerializer(valueType, typeof(CodeDomSerializer)) as CodeDomSerializer;
		}

		/// <summary>Returns an expression for the given object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing v<paramref name="alue" />, or null if there is no existing expression for v<paramref name="alue" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object for which to get an expression.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> is null.</exception>
		// Token: 0x06000A07 RID: 2567 RVA: 0x00012CD8 File Offset: 0x00010ED8
		protected CodeExpression GetExpression(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CodeExpression codeExpression = null;
			CodeDomSerializerBase.ExpressionTable expressionTable = manager.Context[typeof(CodeDomSerializerBase.ExpressionTable)] as CodeDomSerializerBase.ExpressionTable;
			if (expressionTable != null)
			{
				codeExpression = expressionTable[value] as CodeExpression;
			}
			if (codeExpression == null)
			{
				RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
				if (rootContext != null && rootContext.Value == value)
				{
					codeExpression = rootContext.Expression;
				}
			}
			if (codeExpression == null)
			{
				string text = manager.GetName(value);
				if (text == null || text.IndexOf(".") == -1)
				{
					IReferenceService referenceService = manager.GetService(typeof(IReferenceService)) as IReferenceService;
					if (referenceService != null)
					{
						text = referenceService.GetName(value);
						if (text != null && text.IndexOf(".") != -1)
						{
							string[] array = text.Split(new char[] { ',' });
							value = manager.GetInstance(array[0]);
							if (value != null)
							{
								codeExpression = this.SerializeToExpression(manager, value);
								if (codeExpression != null)
								{
									for (int i = 1; i < array.Length; i++)
									{
										codeExpression = new CodePropertyReferenceExpression(codeExpression, array[i]);
									}
								}
							}
						}
					}
				}
			}
			return codeExpression;
		}

		/// <summary>Associates an object with an expression.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> with which to associate <paramref name="value" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="expression" /> is null.</exception>
		// Token: 0x06000A08 RID: 2568 RVA: 0x00012E04 File Offset: 0x00011004
		protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression)
		{
			this.SetExpression(manager, value, expression, false);
		}

		/// <summary>Associates an object with an expression, optionally specifying a preset expression.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> with which to associate <paramref name="value" />.</param>
		/// <param name="isPreset">true to specify a preset expression; otherwise, false.</param>
		// Token: 0x06000A09 RID: 2569 RVA: 0x00012E10 File Offset: 0x00011010
		protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression, bool isPreset)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			CodeDomSerializerBase.ExpressionTable expressionTable = manager.Context[typeof(CodeDomSerializerBase.ExpressionTable)] as CodeDomSerializerBase.ExpressionTable;
			if (expressionTable == null)
			{
				expressionTable = new CodeDomSerializerBase.ExpressionTable();
				manager.Context.Append(expressionTable);
			}
			expressionTable[value] = expression;
		}

		/// <summary>Returns a value indicating whether the given object has been serialized.</summary>
		/// <returns>true if <paramref name="value" /> has been serialized; otherwise, false.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to test for previous serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A0A RID: 2570 RVA: 0x00012E7F File Offset: 0x0001107F
		protected bool IsSerialized(IDesignerSerializationManager manager, object value)
		{
			return this.IsSerialized(manager, value, false);
		}

		/// <summary>Returns a value indicating whether the given object has been serialized, optionally considering preset expressions.</summary>
		/// <returns>true if <paramref name="value" /> has been serialized; otherwise, false.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to test for previous serialization.</param>
		/// <param name="honorPreset">true to include preset expressions; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A0B RID: 2571 RVA: 0x00012E8A File Offset: 0x0001108A
		protected bool IsSerialized(IDesignerSerializationManager manager, object value, bool honorPreset)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return this.GetExpression(manager, value) != null;
		}

		/// <summary>Returns an expression representing the creation of the given object.</summary>
		/// <returns>An expression representing the creation of <paramref name="value" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <param name="isComplete">true if <paramref name="value" /> was fully serialized; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A0C RID: 2572 RVA: 0x00012EB8 File Offset: 0x000110B8
		protected CodeExpression SerializeCreationExpression(IDesignerSerializationManager manager, object value, out bool isComplete)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			CodeExpression codeExpression = null;
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			if (converter != null && converter.CanConvertTo(typeof(InstanceDescriptor)))
			{
				InstanceDescriptor instanceDescriptor = converter.ConvertTo(value, typeof(InstanceDescriptor)) as InstanceDescriptor;
				isComplete = instanceDescriptor.IsComplete;
				if (instanceDescriptor != null && instanceDescriptor.MemberInfo != null)
				{
					codeExpression = this.SerializeInstanceDescriptor(manager, instanceDescriptor);
				}
				else
				{
					this.ReportError(manager, "Unable to serialize to InstanceDescriptor", string.Concat(new string[]
					{
						"Value Type: ",
						value.GetType().Name,
						Environment.NewLine,
						"Value (ToString): ",
						value.ToString()
					}));
				}
			}
			else
			{
				if (value.GetType().GetConstructor(Type.EmptyTypes) != null)
				{
					codeExpression = new CodeObjectCreateExpression(value.GetType().FullName, new CodeExpression[0]);
				}
				isComplete = false;
			}
			return codeExpression;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00012FBC File Offset: 0x000111BC
		private CodeExpression SerializeInstanceDescriptor(IDesignerSerializationManager manager, InstanceDescriptor descriptor)
		{
			CodeExpression codeExpression = null;
			MemberInfo memberInfo = descriptor.MemberInfo;
			CodeExpression codeExpression2 = new CodeTypeReferenceExpression(memberInfo.DeclaringType);
			if (memberInfo is PropertyInfo)
			{
				codeExpression = new CodePropertyReferenceExpression(codeExpression2, memberInfo.Name);
			}
			else if (memberInfo is FieldInfo)
			{
				codeExpression = new CodeFieldReferenceExpression(codeExpression2, memberInfo.Name);
			}
			else if (memberInfo is MethodInfo)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeExpression2, memberInfo.Name, Array.Empty<CodeExpression>());
				if (descriptor.Arguments != null && descriptor.Arguments.Count > 0)
				{
					codeMethodInvokeExpression.Parameters.AddRange(this.SerializeParameters(manager, descriptor.Arguments));
				}
				codeExpression = codeMethodInvokeExpression;
			}
			else if (memberInfo is ConstructorInfo)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(memberInfo.DeclaringType, Array.Empty<CodeExpression>());
				if (descriptor.Arguments != null && descriptor.Arguments.Count > 0)
				{
					codeObjectCreateExpression.Parameters.AddRange(this.SerializeParameters(manager, descriptor.Arguments));
				}
				codeExpression = codeObjectCreateExpression;
			}
			return codeExpression;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000130AC File Offset: 0x000112AC
		private CodeExpression[] SerializeParameters(IDesignerSerializationManager manager, ICollection parameters)
		{
			CodeExpression[] array = null;
			if (parameters != null && parameters.Count > 0)
			{
				array = new CodeExpression[parameters.Count];
				int num = 0;
				foreach (object obj in parameters)
				{
					array[num] = this.SerializeToExpression(manager, obj);
					num++;
				}
			}
			return array;
		}

		/// <summary>Serializes the given event into the given statement collection.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the event will be serialized.</param>
		/// <param name="value">The object to which <paramref name="descriptor" /> is bound.</param>
		/// <param name="descriptor">An <see cref="T:System.ComponentModel.EventDescriptor" /> specifying the event to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, <paramref name="statements" />, or <paramref name="descriptor" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException">
		///   <see cref="T:System.ComponentModel.Design.IEventBindingService" /> is not available.</exception>
		// Token: 0x06000A0F RID: 2575 RVA: 0x00013124 File Offset: 0x00011324
		protected void SerializeEvent(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, EventDescriptor descriptor)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			MemberCodeDomSerializer memberCodeDomSerializer = manager.GetSerializer(descriptor.GetType(), typeof(MemberCodeDomSerializer)) as MemberCodeDomSerializer;
			if (memberCodeDomSerializer != null && memberCodeDomSerializer.ShouldSerialize(manager, value, descriptor))
			{
				memberCodeDomSerializer.Serialize(manager, value, descriptor, statements);
			}
		}

		/// <summary>Serializes the specified events into the given statement collection.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the event will be serialized.</param>
		/// <param name="value">The object on which events will be serialized.</param>
		/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which events will be serialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is null.</exception>
		// Token: 0x06000A10 RID: 2576 RVA: 0x000131A4 File Offset: 0x000113A4
		protected void SerializeEvents(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, params Attribute[] filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			foreach (object obj in TypeDescriptor.GetEvents(value, filter))
			{
				EventDescriptor eventDescriptor = (EventDescriptor)obj;
				this.SerializeEvent(manager, statements, value, eventDescriptor);
			}
		}

		/// <summary>Serializes a property on the given object.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the property will be serialized.</param>
		/// <param name="value">The object on which the property will be serialized.</param>
		/// <param name="propertyToSerialize">The property to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, <paramref name="statements" />, or <paramref name="propertyToSerialize" /> is null.</exception>
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001323C File Offset: 0x0001143C
		protected void SerializeProperty(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, PropertyDescriptor propertyToSerialize)
		{
			if (propertyToSerialize == null)
			{
				throw new ArgumentNullException("propertyToSerialize");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			MemberCodeDomSerializer memberCodeDomSerializer = manager.GetSerializer(propertyToSerialize.GetType(), typeof(MemberCodeDomSerializer)) as MemberCodeDomSerializer;
			if (memberCodeDomSerializer != null && memberCodeDomSerializer.ShouldSerialize(manager, value, propertyToSerialize))
			{
				memberCodeDomSerializer.Serialize(manager, value, propertyToSerialize, statements);
			}
		}

		/// <summary>Serializes the properties on the given object into the given statement collection.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the properties will be serialized.</param>
		/// <param name="value">The object on which the properties will be serialized.</param>
		/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be serialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is null.</exception>
		// Token: 0x06000A12 RID: 2578 RVA: 0x000132BC File Offset: 0x000114BC
		protected void SerializeProperties(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			foreach (object obj in TypeDescriptor.GetProperties(value, filter))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
				{
					this.SerializeProperty(manager, statements, value, propertyDescriptor);
				}
			}
		}

		/// <summary>Returns an instance of the given type.</summary>
		/// <returns>An instance of <paramref name="type" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the instance to return.</param>
		/// <param name="parameters">The parameters to pass to the constructor for <paramref name="type" />.</param>
		/// <param name="name">The name of the deserialized object.</param>
		/// <param name="addToContainer">true to add this object to the design container; otherwise, false. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="type" /> is null.</exception>
		// Token: 0x06000A13 RID: 2579 RVA: 0x00013368 File Offset: 0x00011568
		protected virtual object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return manager.CreateInstance(type, parameters, name, addToContainer);
		}

		/// <summary>Returns a unique name for the given object.</summary>
		/// <returns>A unique name for <paramref name="value" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object for which the name will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x06000A14 RID: 2580 RVA: 0x00013398 File Offset: 0x00011598
		protected string GetUniqueName(IDesignerSerializationManager manager, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			string text = manager.GetName(value);
			if (text == null)
			{
				text = (manager.GetService(typeof(INameCreationService)) as INameCreationService).CreateName(null, value.GetType());
				if (text == null)
				{
					text = value.GetType().Name.ToLower();
				}
				manager.SetName(value, text);
			}
			return text;
		}

		/// <summary>Deserializes the given expression into an in-memory object.</summary>
		/// <returns>An object resulting from interpretation of <paramref name="expression" />.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="name">The name of the object that results from the expression. Can be null if there is no need to name the object.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to interpret.</param>
		// Token: 0x06000A15 RID: 2581 RVA: 0x0001340C File Offset: 0x0001160C
		protected object DeserializeExpression(IDesignerSerializationManager manager, string name, CodeExpression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			bool flag = false;
			object obj = null;
			if (expression is CodeThisReferenceExpression)
			{
				RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
				if (rootContext != null)
				{
					obj = rootContext.Value;
				}
				else
				{
					IDesignerHost designerHost = manager.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						obj = designerHost.RootComponent;
					}
				}
			}
			CodeVariableReferenceExpression codeVariableReferenceExpression = expression as CodeVariableReferenceExpression;
			if (obj == null && codeVariableReferenceExpression != null)
			{
				obj = manager.GetInstance(codeVariableReferenceExpression.VariableName);
				if (obj == null)
				{
					this.ReportError(manager, "Variable '" + codeVariableReferenceExpression.VariableName + "' not initialized prior to reference");
					flag = true;
				}
			}
			CodeFieldReferenceExpression codeFieldReferenceExpression = expression as CodeFieldReferenceExpression;
			if (obj == null && codeFieldReferenceExpression != null)
			{
				obj = manager.GetInstance(codeFieldReferenceExpression.FieldName);
				if (obj == null)
				{
					object obj2 = this.DeserializeExpression(manager, null, codeFieldReferenceExpression.TargetObject);
					FieldInfo fieldInfo;
					if (obj2 is Type)
					{
						fieldInfo = ((Type)obj2).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					}
					else
					{
						fieldInfo = obj2.GetType().GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
					}
					if (fieldInfo != null)
					{
						obj = fieldInfo.GetValue(obj2);
					}
				}
				if (obj == null)
				{
					this.ReportError(manager, "Field '" + codeFieldReferenceExpression.FieldName + "' not initialized prior to reference");
				}
			}
			CodePrimitiveExpression codePrimitiveExpression = expression as CodePrimitiveExpression;
			if (obj == null && codePrimitiveExpression != null)
			{
				obj = codePrimitiveExpression.Value;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression = expression as CodePropertyReferenceExpression;
			if (obj == null && codePropertyReferenceExpression != null)
			{
				object obj3 = this.DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
				if (obj3 != null && obj3 != CodeDomSerializerBase._errorMarker)
				{
					bool flag2 = false;
					if (obj3 is Type)
					{
						PropertyInfo property = ((Type)obj3).GetProperty(codePropertyReferenceExpression.PropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
						if (property != null)
						{
							obj = property.GetValue(null, null);
							flag2 = true;
						}
						FieldInfo field = ((Type)obj3).GetField(codePropertyReferenceExpression.PropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
						if (field != null)
						{
							obj = field.GetValue(null);
							flag2 = true;
						}
					}
					else
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj3)[codePropertyReferenceExpression.PropertyName];
						if (propertyDescriptor != null)
						{
							obj = propertyDescriptor.GetValue(obj3);
							flag2 = true;
						}
						FieldInfo field2 = obj3.GetType().GetField(codePropertyReferenceExpression.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
						if (field2 != null)
						{
							obj = field2.GetValue(null);
							flag2 = true;
						}
					}
					if (!flag2)
					{
						this.ReportError(manager, string.Concat(new string[]
						{
							"Missing field '",
							codePropertyReferenceExpression.PropertyName,
							" 'in type ",
							(obj3 is Type) ? ((Type)obj3).Name : obj3.GetType().Name,
							"'"
						}));
						flag = true;
					}
				}
			}
			CodeObjectCreateExpression codeObjectCreateExpression = expression as CodeObjectCreateExpression;
			if (obj == null && codeObjectCreateExpression != null)
			{
				Type type = manager.GetType(codeObjectCreateExpression.CreateType.BaseType);
				if (type == null)
				{
					this.ReportError(manager, "Type '" + codeObjectCreateExpression.CreateType.BaseType + "' not found.Are you missing a reference?");
					flag = true;
				}
				else
				{
					object[] array = new object[codeObjectCreateExpression.Parameters.Count];
					for (int i = 0; i < codeObjectCreateExpression.Parameters.Count; i++)
					{
						array[i] = this.DeserializeExpression(manager, null, codeObjectCreateExpression.Parameters[i]);
						if (array[i] == CodeDomSerializerBase._errorMarker)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						bool flag3 = false;
						if (typeof(IComponent).IsAssignableFrom(type))
						{
							flag3 = true;
						}
						obj = this.DeserializeInstance(manager, type, array, name, flag3);
						if (obj == CodeDomSerializerBase._errorMarker || obj == null)
						{
							string text = string.Concat(new object[]
							{
								"Type to create: ",
								codeObjectCreateExpression.CreateType.BaseType,
								Environment.NewLine,
								"Name: ",
								name,
								Environment.NewLine,
								"addToContainer: ",
								flag3.ToString(),
								Environment.NewLine,
								"Parameters Count: ",
								codeObjectCreateExpression.Parameters.Count,
								Environment.NewLine
							});
							for (int j = 0; j < array.Length; j++)
							{
								text = string.Concat(new string[]
								{
									text,
									"Parameter Number: ",
									j.ToString(),
									Environment.NewLine,
									"Parameter Type: ",
									(array[j] == null) ? "null" : array[j].GetType().Name,
									Environment.NewLine,
									"Parameter '",
									j.ToString(),
									"' Value: ",
									array[j].ToString(),
									Environment.NewLine
								});
							}
							this.ReportError(manager, "Unable to create an instance of type '" + codeObjectCreateExpression.CreateType.BaseType + "'", text);
							flag = true;
						}
					}
				}
			}
			CodeArrayCreateExpression codeArrayCreateExpression = expression as CodeArrayCreateExpression;
			if (obj == null && codeArrayCreateExpression != null)
			{
				Type type2 = manager.GetType(codeArrayCreateExpression.CreateType.BaseType);
				if (type2 == null)
				{
					this.ReportError(manager, "Type '" + codeArrayCreateExpression.CreateType.BaseType + "' not found.Are you missing a reference?");
					flag = true;
				}
				else
				{
					ArrayList arrayList = new ArrayList();
					Type elementType = type2.GetElementType();
					obj = Array.CreateInstance(type2, codeArrayCreateExpression.Initializers.Count);
					for (int k = 0; k < codeArrayCreateExpression.Initializers.Count; k++)
					{
						object obj4 = this.DeserializeExpression(manager, null, codeArrayCreateExpression.Initializers[k]);
						flag = obj4 == CodeDomSerializerBase._errorMarker;
						if (!flag)
						{
							if (type2.IsInstanceOfType(obj4))
							{
								arrayList.Add(obj4);
							}
							else
							{
								this.ReportError(manager, "Array initializer element type incompatible with array type.", string.Concat(new object[]
								{
									"Array Type: ",
									type2.Name,
									Environment.NewLine,
									"Array Element Type: ",
									elementType,
									Environment.NewLine,
									"Initializer Type: ",
									(obj4 == null) ? "null" : obj4.GetType().Name
								}));
								flag = true;
							}
						}
					}
					if (!flag)
					{
						arrayList.CopyTo((Array)obj, 0);
					}
				}
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = expression as CodeMethodInvokeExpression;
			if (obj == null && codeMethodInvokeExpression != null)
			{
				object obj5 = this.DeserializeExpression(manager, null, codeMethodInvokeExpression.Method.TargetObject);
				object[] array2 = null;
				if (obj5 == CodeDomSerializerBase._errorMarker || obj5 == null)
				{
					flag = true;
				}
				else
				{
					array2 = new object[codeMethodInvokeExpression.Parameters.Count];
					for (int l = 0; l < codeMethodInvokeExpression.Parameters.Count; l++)
					{
						array2[l] = this.DeserializeExpression(manager, null, codeMethodInvokeExpression.Parameters[l]);
						if (array2[l] == CodeDomSerializerBase._errorMarker)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					MethodInfo methodInfo;
					if (obj5 is Type)
					{
						methodInfo = this.GetExactMethod((Type)obj5, codeMethodInvokeExpression.Method.MethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, array2);
					}
					else
					{
						methodInfo = this.GetExactMethod(obj5.GetType(), codeMethodInvokeExpression.Method.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, array2);
					}
					if (methodInfo != null)
					{
						obj = methodInfo.Invoke(obj5, array2);
					}
					else
					{
						string text2 = string.Concat(new object[]
						{
							"Method Name: ",
							codeMethodInvokeExpression.Method.MethodName,
							Environment.NewLine,
							"Method is: ",
							(obj5 is Type) ? "static" : "instance",
							Environment.NewLine,
							"Method Holder Type: ",
							(obj5 is Type) ? ((Type)obj5).Name : obj5.GetType().Name,
							Environment.NewLine,
							"Parameters Count: ",
							codeMethodInvokeExpression.Parameters.Count,
							Environment.NewLine,
							Environment.NewLine
						});
						for (int m = 0; m < array2.Length; m++)
						{
							text2 = string.Concat(new string[]
							{
								text2,
								"Parameter Number: ",
								m.ToString(),
								Environment.NewLine,
								"Parameter Type: ",
								(array2[m] == null) ? "null" : array2[m].GetType().Name,
								Environment.NewLine,
								"Parameter ",
								m.ToString(),
								" Value: ",
								array2[m].ToString(),
								Environment.NewLine
							});
						}
						this.ReportError(manager, "Method '" + codeMethodInvokeExpression.Method.MethodName + "' missing in type '" + ((obj5 is Type) ? ((Type)obj5).Name : (obj5.GetType().Name + "'")), text2);
						flag = true;
					}
				}
			}
			CodeTypeReferenceExpression codeTypeReferenceExpression = expression as CodeTypeReferenceExpression;
			if (obj == null && codeTypeReferenceExpression != null)
			{
				obj = manager.GetType(codeTypeReferenceExpression.Type.BaseType);
				if (obj == null)
				{
					this.ReportError(manager, "Type '" + codeTypeReferenceExpression.Type.BaseType + "' not found.Are you missing a reference?");
					flag = true;
				}
			}
			CodeCastExpression codeCastExpression = expression as CodeCastExpression;
			if (obj == null && codeCastExpression != null)
			{
				Type type3 = manager.GetType(codeCastExpression.TargetType.BaseType);
				object obj6 = this.DeserializeExpression(manager, null, codeCastExpression.Expression);
				if (obj6 != null && obj6 != CodeDomSerializerBase._errorMarker && type3 != null)
				{
					IConvertible convertible = obj6 as IConvertible;
					if (convertible != null)
					{
						try
						{
							obj6 = convertible.ToType(type3, null);
							goto IL_09ED;
						}
						catch
						{
							flag = true;
							goto IL_09ED;
						}
					}
					flag = true;
					IL_09ED:
					if (flag)
					{
						this.ReportError(manager, string.Concat(new string[]
						{
							"Unable to convert type '",
							obj6.GetType().Name,
							"' to type '",
							codeCastExpression.TargetType.BaseType,
							"'"
						}), string.Concat(new string[]
						{
							"Target Type: ",
							codeCastExpression.TargetType.BaseType,
							Environment.NewLine,
							"Instance Type: ",
							(obj6 == null) ? "null" : obj6.GetType().Name,
							Environment.NewLine,
							"Instance Value: ",
							(obj6 == null) ? "null" : obj6.ToString(),
							Environment.NewLine,
							"Instance is IConvertible: ",
							(obj6 is IConvertible).ToString()
						}));
					}
					obj = obj6;
				}
			}
			CodeBinaryOperatorExpression codeBinaryOperatorExpression = expression as CodeBinaryOperatorExpression;
			if (obj == null && codeBinaryOperatorExpression != null)
			{
				string text3 = null;
				IConvertible convertible2 = null;
				IConvertible convertible3 = null;
				CodeBinaryOperatorType @operator = codeBinaryOperatorExpression.Operator;
				if (@operator == CodeBinaryOperatorType.BitwiseOr)
				{
					convertible2 = this.DeserializeExpression(manager, null, codeBinaryOperatorExpression.Left) as IConvertible;
					convertible3 = this.DeserializeExpression(manager, null, codeBinaryOperatorExpression.Right) as IConvertible;
					if (convertible2 is Enum && convertible3 is Enum)
					{
						obj = Enum.ToObject(convertible2.GetType(), Convert.ToInt64(convertible2) | Convert.ToInt64(convertible3));
					}
					else
					{
						text3 = "CodeBinaryOperatorType.BitwiseOr allowed only on Enum types";
						flag = true;
					}
				}
				else
				{
					text3 = "Unsupported CodeBinaryOperatorType: " + codeBinaryOperatorExpression.Operator.ToString();
					flag = true;
				}
				if (flag)
				{
					string text4 = string.Concat(new string[]
					{
						"BinaryOperator Type: ",
						codeBinaryOperatorExpression.Operator.ToString(),
						Environment.NewLine,
						"Left Type: ",
						(convertible2 == null) ? "null" : convertible2.GetType().Name,
						Environment.NewLine,
						"Left Value: ",
						(convertible2 == null) ? "null" : convertible2.ToString(),
						Environment.NewLine,
						"Left Expression Type: ",
						codeBinaryOperatorExpression.Left.GetType().Name,
						Environment.NewLine,
						"Right Type: ",
						(convertible3 == null) ? "null" : convertible3.GetType().Name,
						Environment.NewLine,
						"Right Value: ",
						(convertible3 == null) ? "null" : convertible3.ToString(),
						Environment.NewLine,
						"Right Expression Type: ",
						codeBinaryOperatorExpression.Right.GetType().Name
					});
					this.ReportError(manager, text3, text4);
				}
			}
			if (!flag && obj == null && !(expression is CodePrimitiveExpression) && !(expression is CodeMethodInvokeExpression))
			{
				this.ReportError(manager, "Unsupported Expression Type: " + expression.GetType().Name);
				flag = true;
			}
			if (flag)
			{
				obj = CodeDomSerializerBase._errorMarker;
			}
			return obj;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00014128 File Offset: 0x00012328
		private MethodInfo GetExactMethod(Type type, string methodName, BindingFlags flags, ICollection argsCollection)
		{
			Type[] array = Type.EmptyTypes;
			if (argsCollection != null)
			{
				object[] array2 = new object[argsCollection.Count];
				array = new Type[argsCollection.Count];
				argsCollection.CopyTo(array2, 0);
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] == null)
					{
						array[i] = null;
					}
					else
					{
						array[i] = array2[i].GetType();
					}
				}
			}
			return type.GetMethod(methodName, flags, null, array, null);
		}

		/// <summary>Deserializes a statement by interpreting and executing a CodeDOM statement.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to deserialize.</param>
		// Token: 0x06000A17 RID: 2583 RVA: 0x00014194 File Offset: 0x00012394
		protected void DeserializeStatement(IDesignerSerializationManager manager, CodeStatement statement)
		{
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			CodeAssignStatement codeAssignStatement = statement as CodeAssignStatement;
			if (codeAssignStatement != null)
			{
				this.DeserializeAssignmentStatement(manager, codeAssignStatement);
			}
			CodeExpressionStatement codeExpressionStatement = statement as CodeExpressionStatement;
			if (codeExpressionStatement != null)
			{
				this.DeserializeExpression(manager, null, codeExpressionStatement.Expression);
			}
			CodeAttachEventStatement codeAttachEventStatement = statement as CodeAttachEventStatement;
			if (codeAttachEventStatement != null)
			{
				string text = null;
				CodeObjectCreateExpression codeObjectCreateExpression = codeAttachEventStatement.Listener as CodeObjectCreateExpression;
				if (codeObjectCreateExpression != null && codeObjectCreateExpression.Parameters.Count == 1)
				{
					CodeMethodReferenceExpression codeMethodReferenceExpression = codeObjectCreateExpression.Parameters[0] as CodeMethodReferenceExpression;
					if (codeMethodReferenceExpression != null)
					{
						text = codeMethodReferenceExpression.MethodName;
					}
				}
				CodeDelegateCreateExpression codeDelegateCreateExpression = codeAttachEventStatement.Listener as CodeDelegateCreateExpression;
				if (codeDelegateCreateExpression != null)
				{
					text = codeDelegateCreateExpression.MethodName;
				}
				CodeMethodReferenceExpression codeMethodReferenceExpression2 = codeAttachEventStatement.Listener as CodeMethodReferenceExpression;
				if (codeMethodReferenceExpression2 != null)
				{
					text = codeMethodReferenceExpression2.MethodName;
				}
				object obj = this.DeserializeExpression(manager, null, codeAttachEventStatement.Event.TargetObject);
				if (obj != null && obj != CodeDomSerializerBase._errorMarker && text != null)
				{
					string text2 = null;
					EventDescriptor eventDescriptor = TypeDescriptor.GetEvents(obj)[codeAttachEventStatement.Event.EventName];
					if (eventDescriptor != null)
					{
						IEventBindingService eventBindingService = manager.GetService(typeof(IEventBindingService)) as IEventBindingService;
						if (eventBindingService != null)
						{
							eventBindingService.GetEventProperty(eventDescriptor).SetValue(obj, text);
						}
						else
						{
							text2 = "IEventBindingService missing";
						}
					}
					else
					{
						text2 = string.Concat(new string[]
						{
							"No event '",
							codeAttachEventStatement.Event.EventName,
							"' found in type '",
							obj.GetType().Name,
							"'"
						});
					}
					if (text2 != null)
					{
						this.ReportError(manager, text2, string.Concat(new string[]
						{
							"Method Name: ",
							text,
							Environment.NewLine,
							"Event Name: ",
							codeAttachEventStatement.Event.EventName,
							Environment.NewLine,
							"Listener Expression Type: ",
							codeMethodReferenceExpression2.GetType().Name,
							Environment.NewLine,
							"Event Holder Type: ",
							obj.GetType().Name,
							Environment.NewLine,
							"Event Holder Expression Type: ",
							codeAttachEventStatement.Event.TargetObject.GetType().Name
						}));
					}
				}
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000143E4 File Offset: 0x000125E4
		private void DeserializeAssignmentStatement(IDesignerSerializationManager manager, CodeAssignStatement statement)
		{
			CodeExpression left = statement.Left;
			CodePropertyReferenceExpression codePropertyReferenceExpression = left as CodePropertyReferenceExpression;
			if (codePropertyReferenceExpression != null)
			{
				object obj = this.DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
				object obj2 = null;
				if (obj != null && obj != CodeDomSerializerBase._errorMarker)
				{
					obj2 = this.DeserializeExpression(manager, null, statement.Right);
				}
				if (obj2 != null && obj2 != CodeDomSerializerBase._errorMarker && obj != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)[codePropertyReferenceExpression.PropertyName];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(obj, obj2);
					}
					else
					{
						this.ReportError(manager, string.Concat(new string[]
						{
							"Missing property '",
							codePropertyReferenceExpression.PropertyName,
							"' in type '",
							obj.GetType().Name,
							"'"
						}));
					}
				}
			}
			CodeFieldReferenceExpression codeFieldReferenceExpression = left as CodeFieldReferenceExpression;
			if (codeFieldReferenceExpression != null && codeFieldReferenceExpression.FieldName != null)
			{
				object obj3 = this.DeserializeExpression(manager, null, codeFieldReferenceExpression.TargetObject);
				object obj4 = null;
				if (obj3 != null && obj3 != CodeDomSerializerBase._errorMarker)
				{
					obj4 = this.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, statement.Right);
				}
				RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
				if (obj3 != null && obj3 != CodeDomSerializerBase._errorMarker && obj4 != CodeDomSerializerBase._errorMarker && (!(codeFieldReferenceExpression.TargetObject is CodeThisReferenceExpression) || rootContext == null || rootContext.Value != obj3))
				{
					FieldInfo fieldInfo;
					if (obj3 is Type)
					{
						fieldInfo = ((Type)obj3).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					}
					else
					{
						fieldInfo = obj3.GetType().GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
					}
					if (fieldInfo != null)
					{
						fieldInfo.SetValue(obj3, obj4);
					}
					else
					{
						this.ReportError(manager, string.Concat(new string[]
						{
							"Field '",
							codeFieldReferenceExpression.FieldName,
							"' missing in type '",
							obj3.GetType().Name,
							"'"
						}), string.Concat(new string[]
						{
							"Field Name: ",
							codeFieldReferenceExpression.FieldName,
							Environment.NewLine,
							"Field is: ",
							(obj3 is Type) ? "static" : "instance",
							Environment.NewLine,
							"Field Value: ",
							(obj4 == null) ? "null" : obj4.ToString(),
							Environment.NewLine,
							"Field Holder Type: ",
							obj3.GetType().Name,
							Environment.NewLine,
							"Field Holder Expression Type: ",
							codeFieldReferenceExpression.TargetObject.GetType().Name
						}));
					}
				}
			}
			CodeVariableReferenceExpression codeVariableReferenceExpression = left as CodeVariableReferenceExpression;
			if (codeVariableReferenceExpression != null && codeVariableReferenceExpression.VariableName != null)
			{
				object obj5 = this.DeserializeExpression(manager, codeVariableReferenceExpression.VariableName, statement.Right);
				if (obj5 != CodeDomSerializerBase._errorMarker && manager.GetName(obj5) == null)
				{
					manager.SetName(obj5, codeVariableReferenceExpression.VariableName);
				}
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000146E9 File Offset: 0x000128E9
		internal void ReportError(IDesignerSerializationManager manager, string message)
		{
			this.ReportError(manager, message, string.Empty);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000146F8 File Offset: 0x000128F8
		internal void ReportError(IDesignerSerializationManager manager, string message, string details)
		{
			try
			{
				throw new Exception(message);
			}
			catch (Exception ex)
			{
				ex.Data["Details"] = message + Environment.NewLine + Environment.NewLine + details;
				manager.ReportError(ex);
			}
		}

		/// <summary>Serializes the given object into an expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> containing <paramref name="value" /> as a serialized expression.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> is null.</exception>
		// Token: 0x06000A1B RID: 2587 RVA: 0x0000234B File Offset: 0x0000054B
		protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the given object into an expression appropriate for the invariant culture.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> containing <paramref name="value" /> as a serialized expression.</returns>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object to serialize.</param>
		/// <param name="ensureInvariant">true to serialize into the invariant culture; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> is null.</exception>
		// Token: 0x06000A1C RID: 2588 RVA: 0x0000234B File Offset: 0x0000054B
		protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value, bool ensureInvariant)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the properties on the given object into the invariant culture’s resource bundle.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="statements">Not used.</param>
		/// <param name="value">The object whose properties will be serialized.</param>
		/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be serialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is null.</exception>
		// Token: 0x06000A1D RID: 2589 RVA: 0x0000234B File Offset: 0x0000054B
		protected void SerializePropertiesToResources(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the given object into a resource bundle using the given resource name.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="resourceName">The name of the resource bundle into which <paramref name="value" /> will be serialized.</param>
		/// <param name="value">The object to serialize.</param>
		// Token: 0x06000A1E RID: 2590 RVA: 0x0000234B File Offset: 0x0000054B
		protected void SerializeResource(IDesignerSerializationManager manager, string resourceName, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the given object into a resource bundle using the given resource name.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="resourceName">The name of the resource bundle into which <paramref name="value" /> will be serialized.</param>
		/// <param name="value">The object to serialize.</param>
		// Token: 0x06000A1F RID: 2591 RVA: 0x0000234B File Offset: 0x0000054B
		protected void SerializeResourceInvariant(IDesignerSerializationManager manager, string resourceName, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes properties on the given object from the invariant culture’s resource bundle.</summary>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
		/// <param name="value">The object from which the properties are to be deserialized.</param>
		/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be deserialized.</param>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0000234B File Offset: 0x0000054B
		protected void DeserializePropertiesFromResources(IDesignerSerializationManager manager, object value, Attribute[] filter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of attributes as defined in the project's target version of the .NET Framework.</summary>
		/// <returns>A collection of attributes as defined in the project's target version of the .NET Framework.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="type">The target type.</param>
		// Token: 0x06000A22 RID: 2594 RVA: 0x0000970B File Offset: 0x0000790B
		protected static AttributeCollection GetAttributesFromTypeHelper(IDesignerSerializationManager manager, Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a collection of attributes as defined in the project's target version of the .NET Framework.</summary>
		/// <returns>A collection of attributes as defined in the project's target version of the .NET Framework.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="instance">An object of the target type.</param>
		// Token: 0x06000A23 RID: 2595 RVA: 0x0000970B File Offset: 0x0000790B
		protected static AttributeCollection GetAttributesHelper(IDesignerSerializationManager manager, object instance)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a collection of events as defined in the project's target version of the .NET Framework.</summary>
		/// <returns>A collection of events as defined in the project's target version of the .NET Framework.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="instance">An object of the target type.</param>
		/// <param name="attributes">An array of attributes to pass to the target version of the .NET Framework.</param>
		// Token: 0x06000A24 RID: 2596 RVA: 0x0000970B File Offset: 0x0000790B
		protected static EventDescriptorCollection GetEventsHelper(IDesignerSerializationManager manager, object instance, Attribute[] attributes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a collection of properties as defined in the project's target version of the .NET Framework.</summary>
		/// <returns>A collection of properties as defined in the project's target version of the .NET Framework.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="instance">An object of the target type.</param>
		/// <param name="attributes">An array of attributes to pass to the target version of the .NET Framework.</param>
		// Token: 0x06000A25 RID: 2597 RVA: 0x0000970B File Offset: 0x0000790B
		protected static PropertyDescriptorCollection GetPropertiesHelper(IDesignerSerializationManager manager, object instance, Attribute[] attributes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a reflection type generated from type metadata.</summary>
		/// <returns>A reflection type generated from the metadata of <paramref name="type" />.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="type">The type to use metadata from.</param>
		// Token: 0x06000A26 RID: 2598 RVA: 0x0000970B File Offset: 0x0000790B
		protected static Type GetReflectionTypeFromTypeHelper(IDesignerSerializationManager manager, Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a reflection type generated from object metadata.</summary>
		/// <returns>A reflection type generated from the metadata of <paramref name="object" />.</returns>
		/// <param name="manager">The serialization manager.</param>
		/// <param name="instance">The object to use metadata from.</param>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0000970B File Offset: 0x0000790B
		protected static Type GetReflectionTypeHelper(IDesignerSerializationManager manager, object instance)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that is aware of the target version of the .NET Framework, for use in type filtering.</summary>
		/// <returns>A .NET Framework-aware type description provider.</returns>
		/// <param name="provider">The type description provider service.</param>
		/// <param name="instance">An object from which the type description provider service can be derived, if <paramref name="provider" /> is null.</param>
		// Token: 0x06000A28 RID: 2600 RVA: 0x0000970B File Offset: 0x0000790B
		protected static TypeDescriptionProvider GetTargetFrameworkProvider(IServiceProvider provider, object instance)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04000253 RID: 595
		private static readonly CodeDomSerializerBase.DeserializationErrorMarker _errorMarker = new CodeDomSerializerBase.DeserializationErrorMarker();

		// Token: 0x0200014D RID: 333
		private sealed class DeserializationErrorMarker : CodeExpression
		{
			// Token: 0x06000A29 RID: 2601 RVA: 0x0000241E File Offset: 0x0000061E
			public override bool Equals(object o)
			{
				return false;
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x0000514B File Offset: 0x0000334B
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
		}

		// Token: 0x0200014E RID: 334
		private class ExpressionTable : Hashtable
		{
		}
	}
}
