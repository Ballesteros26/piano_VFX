using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Represents a class that can generate proxy code from an XML representation of a data structure.</summary>
	// Token: 0x020002C6 RID: 710
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeExporter
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x00093DE4 File Offset: 0x00091FE4
		internal CodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable exportedMappings)
		{
			this.includeMetadata = new CodeAttributeDeclarationCollection();
			base..ctor();
			if (codeNamespace != null)
			{
				CodeGenerator.ValidateIdentifiers(codeNamespace);
			}
			this.codeNamespace = codeNamespace;
			if (codeCompileUnit != null)
			{
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				}
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.Xml.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				}
			}
			this.codeCompileUnit = codeCompileUnit;
			this.options = options;
			this.exportedMappings = exportedMappings;
			this.codeProvider = codeProvider;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00093E79 File Offset: 0x00092079
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00093E81 File Offset: 0x00092081
		internal CodeNamespace CodeNamespace
		{
			get
			{
				if (this.codeNamespace == null)
				{
					this.codeNamespace = new CodeNamespace();
				}
				return this.codeNamespace;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00093E9C File Offset: 0x0009209C
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00093EB7 File Offset: 0x000920B7
		internal Hashtable ExportedClasses
		{
			get
			{
				if (this.exportedClasses == null)
				{
					this.exportedClasses = new Hashtable();
				}
				return this.exportedClasses;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00093ED2 File Offset: 0x000920D2
		internal Hashtable ExportedMappings
		{
			get
			{
				if (this.exportedMappings == null)
				{
					this.exportedMappings = new Hashtable();
				}
				return this.exportedMappings;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00093EED File Offset: 0x000920ED
		internal bool GenerateProperties
		{
			get
			{
				return (this.options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00093EFC File Offset: 0x000920FC
		internal CodeAttributeDeclaration GeneratedCodeAttribute
		{
			get
			{
				if (this.generatedCodeAttribute == null)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetExecutingAssembly();
						if (assembly == null)
						{
							assembly = typeof(CodeExporter).Assembly;
						}
					}
					AssemblyName name = assembly.GetName();
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
					string productVersion = CodeExporter.GetProductVersion(assembly);
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
					this.generatedCodeAttribute = codeAttributeDeclaration;
				}
				return this.generatedCodeAttribute;
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00093FB8 File Offset: 0x000921B8
		internal static CodeAttributeDeclaration FindAttributeDeclaration(Type type, CodeAttributeDeclarationCollection metadata)
		{
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == type.FullName || codeAttributeDeclaration.Name == type.Name)
				{
					return codeAttributeDeclaration;
				}
			}
			return null;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00094034 File Offset: 0x00092234
		private static string GetProductVersion(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is AssemblyInformationalVersionAttribute)
				{
					return ((AssemblyInformationalVersionAttribute)customAttributes[i]).InformationalVersion;
				}
			}
			return null;
		}

		/// <summary>Gets a collection of code attribute metadata that is included when the code is exported.</summary>
		/// <returns>A collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects that represent metadata that is included when the code is exported.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00094070 File Offset: 0x00092270
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.includeMetadata;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00094078 File Offset: 0x00092278
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00094080 File Offset: 0x00092280
		internal void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("Exported mappings must come from the same importer."));
			}
		}

		// Token: 0x06001A75 RID: 6773
		internal abstract void ExportDerivedStructs(StructMapping mapping);

		// Token: 0x06001A76 RID: 6774
		internal abstract void EnsureTypesExported(Accessor[] accessors, string ns);

		// Token: 0x06001A77 RID: 6775 RVA: 0x000940AB File Offset: 0x000922AB
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("CODEGEN Warning: {0}", new object[] { text }), false));
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000940D0 File Offset: 0x000922D0
		internal void ExportRoot(StructMapping mapping, Type includeType)
		{
			if (!this.rootExported)
			{
				this.rootExported = true;
				this.ExportDerivedStructs(mapping);
				for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
				{
					if (!structMapping.ReferencedByElement && structMapping.IncludeInSchema && !structMapping.IsAnonymousType)
					{
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(includeType.FullName);
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName)));
						this.includeMetadata.Add(codeAttributeDeclaration);
					}
				}
				Hashtable hashtable = new Hashtable();
				foreach (object obj in this.Scope.TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping is ArrayMapping)
					{
						ArrayMapping arrayMapping = (ArrayMapping)typeMapping;
						if (CodeExporter.ShouldInclude(arrayMapping) && !hashtable.Contains(arrayMapping.TypeDesc.FullName))
						{
							CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(includeType.FullName);
							codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(arrayMapping.TypeDesc.FullName)));
							this.includeMetadata.Add(codeAttributeDeclaration2);
							hashtable.Add(arrayMapping.TypeDesc.FullName, string.Empty);
							this.EnsureTypesExported(arrayMapping.Elements, arrayMapping.Namespace);
						}
					}
				}
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00094250 File Offset: 0x00092450
		private static bool ShouldInclude(ArrayMapping arrayMapping)
		{
			if (arrayMapping.ReferencedByElement)
			{
				return false;
			}
			if (arrayMapping.Next != null)
			{
				return false;
			}
			if (arrayMapping.Elements.Length == 1 && arrayMapping.Elements[0].Mapping.TypeDesc.Kind == TypeKind.Node)
			{
				return false;
			}
			for (int i = 0; i < arrayMapping.Elements.Length; i++)
			{
				if (arrayMapping.Elements[i].Name != arrayMapping.Elements[i].Mapping.DefaultElementName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000942D8 File Offset: 0x000924D8
		internal CodeTypeDeclaration ExportEnum(EnumMapping mapping, Type type)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(mapping.TypeDesc.Name);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			codeTypeDeclaration.IsEnum = true;
			if (mapping.IsFlags && mapping.Constants.Length > 31)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(long)));
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			this.CodeNamespace.Types.Add(codeTypeDeclaration);
			for (int i = 0; i < mapping.Constants.Length; i++)
			{
				CodeExporter.ExportConstant(codeTypeDeclaration, mapping.Constants[i], type, mapping.IsFlags, 1L << i);
			}
			if (mapping.IsFlags)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(FlagsAttribute).FullName);
				codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			}
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000943C8 File Offset: 0x000925C8
		internal void AddTypeMetadata(CodeAttributeDeclarationCollection metadata, Type type, string defaultName, string name, string ns, bool includeInSchema)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
			if (name == null || name.Length == 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("AnonymousType", new CodePrimitiveExpression(true)));
			}
			else if (defaultName != name)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("TypeName", new CodePrimitiveExpression(name)));
			}
			if (ns != null && ns.Length != 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (!includeInSchema)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IncludeInSchema", new CodePrimitiveExpression(false)));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				metadata.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000944A0 File Offset: 0x000926A0
		internal static void AddIncludeMetadata(CodeAttributeDeclarationCollection metadata, StructMapping mapping, Type type)
		{
			if (mapping.IsAnonymousType)
			{
				return;
			}
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				metadata.Add(new CodeAttributeDeclaration(type.FullName)
				{
					Arguments = 
					{
						new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName))
					}
				});
				CodeExporter.AddIncludeMetadata(metadata, structMapping, type);
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00094508 File Offset: 0x00092708
		internal static void ExportConstant(CodeTypeDeclaration codeClass, ConstantMapping constant, Type type, bool init, long enumValue)
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(int).FullName, constant.Name);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			if (init)
			{
				codeMemberField.InitExpression = new CodePrimitiveExpression(enumValue);
			}
			codeClass.Members.Add(codeMemberField);
			if (constant.XmlName != constant.Name)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(constant.XmlName)));
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000945B8 File Offset: 0x000927B8
		internal static object PromoteType(Type type, object value)
		{
			if (type == typeof(sbyte))
			{
				return ((IConvertible)value).ToInt16(null);
			}
			if (type == typeof(ushort))
			{
				return ((IConvertible)value).ToInt32(null);
			}
			if (type == typeof(uint))
			{
				return ((IConvertible)value).ToInt64(null);
			}
			if (type == typeof(ulong))
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return value;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00094658 File Offset: 0x00092858
		internal CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = (codeMemberProperty.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			CodeExpression codeExpression2 = new CodePropertySetValueReferenceExpression();
			codeAssignStatement.Left = codeExpression;
			codeAssignStatement.Right = codeExpression2;
			if (this.EnableDataBinding)
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
				codeMemberProperty.SetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), CodeExporter.RaisePropertyChangedEventMethod.Name, new CodeExpression[]
				{
					new CodePrimitiveExpression(name)
				}));
			}
			else
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
			}
			return codeMemberProperty;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00094740 File Offset: 0x00092940
		internal static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00094754 File Offset: 0x00092954
		internal void AddPropertyChangedNotifier(CodeTypeDeclaration codeClass)
		{
			if (this.EnableDataBinding && codeClass != null)
			{
				if (codeClass.BaseTypes.Count == 0)
				{
					codeClass.BaseTypes.Add(typeof(object));
				}
				codeClass.BaseTypes.Add(new CodeTypeReference(typeof(INotifyPropertyChanged)));
				codeClass.Members.Add(CodeExporter.PropertyChangedEvent);
				codeClass.Members.Add(CodeExporter.RaisePropertyChangedEventMethod);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x000947CB File Offset: 0x000929CB
		private bool EnableDataBinding
		{
			get
			{
				return (this.options & CodeGenerationOptions.EnableDataBinding) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000947DC File Offset: 0x000929DC
		internal static CodeMemberMethod RaisePropertyChangedEventMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "RaisePropertyChanged";
				codeMemberMethod.Attributes = (MemberAttributes)12290;
				CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("propertyName");
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), codeArgumentReferenceExpression.ParameterName));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("propertyChanged");
				codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(PropertyChangedEventHandler), codeVariableReferenceExpression.VariableName, new CodeEventReferenceExpression(new CodeThisReferenceExpression(), CodeExporter.PropertyChangedEvent.Name)));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), Array.Empty<CodeStatement>());
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeConditionStatement.TrueStatements.Add(new CodeDelegateInvokeExpression(codeVariableReferenceExpression, new CodeExpression[]
				{
					new CodeThisReferenceExpression(),
					new CodeObjectCreateExpression(typeof(PropertyChangedEventArgs), new CodeExpression[] { codeArgumentReferenceExpression })
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x000948D4 File Offset: 0x00092AD4
		internal static CodeMemberEvent PropertyChangedEvent
		{
			get
			{
				return new CodeMemberEvent
				{
					Attributes = MemberAttributes.Public,
					Name = "PropertyChanged",
					Type = new CodeTypeReference(typeof(PropertyChangedEventHandler)),
					ImplementationTypes = { typeof(INotifyPropertyChanged) }
				};
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal CodeExporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001580 RID: 5504
		private Hashtable exportedMappings;

		// Token: 0x04001581 RID: 5505
		private Hashtable exportedClasses;

		// Token: 0x04001582 RID: 5506
		private CodeNamespace codeNamespace;

		// Token: 0x04001583 RID: 5507
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x04001584 RID: 5508
		private bool rootExported;

		// Token: 0x04001585 RID: 5509
		private TypeScope scope;

		// Token: 0x04001586 RID: 5510
		private CodeAttributeDeclarationCollection includeMetadata;

		// Token: 0x04001587 RID: 5511
		private CodeGenerationOptions options;

		// Token: 0x04001588 RID: 5512
		private CodeDomProvider codeProvider;

		// Token: 0x04001589 RID: 5513
		private CodeAttributeDeclaration generatedCodeAttribute;
	}
}
