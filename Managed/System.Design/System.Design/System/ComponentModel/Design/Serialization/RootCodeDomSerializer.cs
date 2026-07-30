using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200015C RID: 348
	internal class RootCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00015C4C File Offset: 0x00013E4C
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._codeMap == null)
			{
				this._codeMap = new RootCodeDomSerializer.CodeMap(value.GetType(), manager.GetName(value));
			}
			this._codeMap.Clear();
			RootContext rootContext = new RootContext(new CodeThisReferenceExpression(), value);
			manager.Context.Push(rootContext);
			this.SerializeComponents(manager, ((IComponent)value).Site.Container.Components, (IComponent)value);
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
			codeStatementCollection.Add(new CodeCommentStatement(manager.GetName(value)));
			codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
			base.SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
			base.SerializeEvents(manager, codeStatementCollection, value, new Attribute[0]);
			this._codeMap.Add(codeStatementCollection);
			manager.Context.Pop();
			return this._codeMap.GenerateClass();
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00015D58 File Offset: 0x00013F58
		private void SerializeComponents(IDesignerSerializationManager manager, ICollection components, IComponent rootComponent)
		{
			foreach (object obj in components)
			{
				IComponent component = (IComponent)obj;
				if (component != rootComponent)
				{
					this.SerializeComponent(manager, component);
				}
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private void SerializeComponent(IDesignerSerializationManager manager, IComponent component)
		{
			CodeDomSerializer serializer = base.GetSerializer(manager, component);
			if (serializer != null)
			{
				this._codeMap.AddField(new CodeMemberField(component.GetType(), manager.GetName(component)));
				CodeStatementCollection codeStatementCollection = serializer.Serialize(manager, component) as CodeStatementCollection;
				if (codeStatementCollection != null)
				{
					this._codeMap.Add(codeStatementCollection);
				}
				CodeStatement codeStatement = serializer.Serialize(manager, component) as CodeStatement;
				if (codeStatement != null)
				{
					this._codeMap.Add(codeStatement);
				}
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00015E24 File Offset: 0x00014024
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)codeObject;
			Type type = manager.GetType(codeTypeDeclaration.BaseTypes[0].BaseType);
			object obj = manager.CreateInstance(type, null, codeTypeDeclaration.Name, true);
			RootContext rootContext = new RootContext(new CodeThisReferenceExpression(), obj);
			manager.Context.Push(rootContext);
			CodeMemberMethod initializeMethod = this.GetInitializeMethod(codeTypeDeclaration);
			if (initializeMethod == null)
			{
				throw new InvalidOperationException("InitializeComponent method is missing in: " + codeTypeDeclaration.Name);
			}
			foreach (object obj2 in initializeMethod.Statements)
			{
				CodeStatement codeStatement = (CodeStatement)obj2;
				base.DeserializeStatement(manager, codeStatement);
			}
			manager.Context.Pop();
			return obj;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00015EFC File Offset: 0x000140FC
		private CodeMemberMethod GetInitializeMethod(CodeTypeDeclaration declaration)
		{
			CodeMemberMethod codeMemberMethod = null;
			foreach (object obj in declaration.Members)
			{
				codeMemberMethod = ((CodeTypeMember)obj) as CodeMemberMethod;
				if (codeMemberMethod != null && codeMemberMethod.Name == "InitializeComponent")
				{
					break;
				}
			}
			return codeMemberMethod;
		}

		// Token: 0x0400026D RID: 621
		private RootCodeDomSerializer.CodeMap _codeMap;

		// Token: 0x0200015D RID: 349
		internal class CodeMap
		{
			// Token: 0x06000A90 RID: 2704 RVA: 0x00015F70 File Offset: 0x00014170
			public CodeMap(Type classType, string className)
			{
				if (classType == null)
				{
					throw new ArgumentNullException("classType");
				}
				if (className == null)
				{
					throw new ArgumentNullException("className");
				}
				this._classType = classType;
				this._className = className;
				this._fields = new List<CodeMemberField>();
				this._initializers = new CodeStatementCollection();
				this._begin = new CodeStatementCollection();
				this._default = new CodeStatementCollection();
				this._end = new CodeStatementCollection();
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00015FEA File Offset: 0x000141EA
			public void AddField(CodeMemberField field)
			{
				this._fields.Add(field);
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x00015FF8 File Offset: 0x000141F8
			public void Add(CodeStatementCollection statements)
			{
				foreach (object obj in statements)
				{
					CodeStatement codeStatement = (CodeStatement)obj;
					this.Add(codeStatement);
				}
			}

			// Token: 0x06000A93 RID: 2707 RVA: 0x0001604C File Offset: 0x0001424C
			public void Add(CodeStatement statement)
			{
				if (statement.UserData["statement-order"] == null)
				{
					this._default.Add(statement);
					return;
				}
				if ((string)statement.UserData["statement-order"] == "initializer")
				{
					this._initializers.Add(statement);
					return;
				}
				if ((string)statement.UserData["statement-order"] == "begin")
				{
					this._begin.Add(statement);
					return;
				}
				if ((string)statement.UserData["statement-order"] == "end")
				{
					this._end.Add(statement);
				}
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x00016108 File Offset: 0x00014308
			public CodeTypeDeclaration GenerateClass()
			{
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(this._className);
				codeTypeDeclaration.BaseTypes.Add(this._classType);
				codeTypeDeclaration.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Windows Form Designer generated code"));
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "InitializeComponent";
				codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
				codeMemberMethod.Attributes = MemberAttributes.Private;
				codeMemberMethod.Statements.AddRange(this._initializers);
				codeMemberMethod.Statements.AddRange(this._begin);
				codeMemberMethod.Statements.AddRange(this._default);
				codeMemberMethod.Statements.AddRange(this._end);
				codeTypeDeclaration.Members.Add(codeMemberMethod);
				foreach (CodeMemberField codeMemberField in this._fields)
				{
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				codeTypeDeclaration.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, null));
				return codeTypeDeclaration;
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x0001622C File Offset: 0x0001442C
			public void Clear()
			{
				this._fields.Clear();
				this._initializers.Clear();
				this._begin.Clear();
				this._default.Clear();
				this._end.Clear();
			}

			// Token: 0x0400026E RID: 622
			private string _className;

			// Token: 0x0400026F RID: 623
			private Type _classType;

			// Token: 0x04000270 RID: 624
			private List<CodeMemberField> _fields;

			// Token: 0x04000271 RID: 625
			private CodeStatementCollection _initializers;

			// Token: 0x04000272 RID: 626
			private CodeStatementCollection _begin;

			// Token: 0x04000273 RID: 627
			private CodeStatementCollection _default;

			// Token: 0x04000274 RID: 628
			private CodeStatementCollection _end;
		}
	}
}
