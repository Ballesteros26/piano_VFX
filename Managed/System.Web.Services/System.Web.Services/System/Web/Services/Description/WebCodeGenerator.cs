using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000132 RID: 306
	internal class WebCodeGenerator
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0000210F File Offset: 0x0000030F
		private WebCodeGenerator()
		{
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000400A0 File Offset: 0x0003E2A0
		internal static CodeAttributeDeclaration GeneratedCodeAttribute
		{
			get
			{
				if (WebCodeGenerator.generatedCodeAttribute == null)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetExecutingAssembly();
						if (assembly == null)
						{
							assembly = typeof(WebCodeGenerator).Assembly;
						}
					}
					AssemblyName name = assembly.GetName();
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
					string productVersion = WebCodeGenerator.GetProductVersion(assembly);
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
					WebCodeGenerator.generatedCodeAttribute = codeAttributeDeclaration;
				}
				return WebCodeGenerator.generatedCodeAttribute;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00040158 File Offset: 0x0003E358
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

		// Token: 0x0600093B RID: 2363 RVA: 0x00040194 File Offset: 0x0003E394
		internal static string[] GetNamespacesForTypes(Type[] types)
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < types.Length; i++)
			{
				string fullName = types[i].FullName;
				int num = fullName.LastIndexOf('.');
				if (num > 0)
				{
					hashtable[fullName.Substring(0, num)] = types[i];
				}
			}
			string[] array = new string[hashtable.Keys.Count];
			hashtable.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00040200 File Offset: 0x0003E400
		internal static void AddImports(CodeNamespace codeNamespace, string[] namespaces)
		{
			foreach (string text in namespaces)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport(text));
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00040234 File Offset: 0x0003E434
		private static CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			CodeExpression codeExpression2 = new CodeArgumentReferenceExpression("value");
			codeMemberProperty.SetStatements.Add(new CodeAssignStatement(codeExpression, codeExpression2));
			return codeMemberProperty;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000402B4 File Offset: 0x0003E4B4
		internal static CodeTypeMember AddMember(CodeTypeDeclaration codeClass, string typeName, string memberName, CodeExpression initializer, CodeAttributeDeclarationCollection metadata, CodeFlags flags, CodeGenerationOptions options)
		{
			bool flag = (options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None;
			string text = (flag ? WebCodeGenerator.MakeFieldName(memberName) : memberName);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, text);
			codeMemberField.InitExpression = initializer;
			CodeTypeMember codeTypeMember;
			if (flag)
			{
				codeClass.Members.Add(codeMemberField);
				codeTypeMember = WebCodeGenerator.CreatePropertyDeclaration(codeMemberField, memberName, typeName);
			}
			else
			{
				codeTypeMember = codeMemberField;
			}
			codeTypeMember.CustomAttributes = metadata;
			if ((flags & CodeFlags.IsPublic) != (CodeFlags)0)
			{
				codeTypeMember.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			}
			codeClass.Members.Add(codeTypeMember);
			return codeTypeMember;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00040334 File Offset: 0x0003E534
		internal static string FullTypeName(XmlMemberMapping mapping, CodeDomProvider codeProvider)
		{
			return mapping.GenerateTypeName(codeProvider);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0004033D File Offset: 0x0003E53D
		private static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00040350 File Offset: 0x0003E550
		internal static CodeConstructor AddConstructor(CodeTypeDeclaration codeClass, string[] parameterTypeNames, string[] parameterNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
		{
			CodeConstructor codeConstructor = new CodeConstructor();
			if ((flags & CodeFlags.IsPublic) != (CodeFlags)0)
			{
				codeConstructor.Attributes = (codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			}
			if ((flags & CodeFlags.IsAbstract) != (CodeFlags)0)
			{
				codeConstructor.Attributes |= MemberAttributes.Abstract;
			}
			codeConstructor.CustomAttributes = metadata;
			for (int i = 0; i < parameterTypeNames.Length; i++)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(parameterTypeNames[i], parameterNames[i]);
				codeConstructor.Parameters.Add(codeParameterDeclarationExpression);
			}
			codeClass.Members.Add(codeConstructor);
			return codeConstructor;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000403D4 File Offset: 0x0003E5D4
		internal static CodeMemberMethod AddMethod(CodeTypeDeclaration codeClass, string methodName, CodeFlags[] parameterFlags, string[] parameterTypeNames, string[] parameterNames, string returnTypeName, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
		{
			return WebCodeGenerator.AddMethod(codeClass, methodName, parameterFlags, parameterTypeNames, parameterNames, new CodeAttributeDeclarationCollection[0], returnTypeName, metadata, flags);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000403F8 File Offset: 0x0003E5F8
		internal static CodeMemberMethod AddMethod(CodeTypeDeclaration codeClass, string methodName, CodeFlags[] parameterFlags, string[] parameterTypeNames, string[] parameterNames, CodeAttributeDeclarationCollection[] parameterAttributes, string returnTypeName, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = methodName;
			codeMemberMethod.ReturnType = new CodeTypeReference(returnTypeName);
			codeMemberMethod.CustomAttributes = metadata;
			if ((flags & CodeFlags.IsPublic) != (CodeFlags)0)
			{
				codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			}
			if ((flags & CodeFlags.IsAbstract) != (CodeFlags)0)
			{
				codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-16)) | MemberAttributes.Abstract;
			}
			if ((flags & CodeFlags.IsNew) != (CodeFlags)0)
			{
				codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-241)) | MemberAttributes.New;
			}
			for (int i = 0; i < parameterNames.Length; i++)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(parameterTypeNames[i], parameterNames[i]);
				if ((parameterFlags[i] & CodeFlags.IsByRef) != (CodeFlags)0)
				{
					codeParameterDeclarationExpression.Direction = FieldDirection.Ref;
				}
				else if ((parameterFlags[i] & CodeFlags.IsOut) != (CodeFlags)0)
				{
					codeParameterDeclarationExpression.Direction = FieldDirection.Out;
				}
				if (i < parameterAttributes.Length)
				{
					codeParameterDeclarationExpression.CustomAttributes = parameterAttributes[i];
				}
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			}
			codeClass.Members.Add(codeMemberMethod);
			return codeMemberMethod;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000404E0 File Offset: 0x0003E6E0
		internal static CodeTypeDeclaration AddClass(CodeNamespace codeNamespace, string className, string baseClassName, string[] implementedInterfaceNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags, bool isPartial)
		{
			CodeTypeDeclaration codeTypeDeclaration = WebCodeGenerator.CreateClass(className, baseClassName, implementedInterfaceNames, metadata, flags, isPartial);
			codeNamespace.Types.Add(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0004050C File Offset: 0x0003E70C
		internal static CodeTypeDeclaration CreateClass(string className, string baseClassName, string[] implementedInterfaceNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags, bool isPartial)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(className);
			if (baseClassName != null && baseClassName.Length > 0)
			{
				codeTypeDeclaration.BaseTypes.Add(baseClassName);
			}
			foreach (string text in implementedInterfaceNames)
			{
				codeTypeDeclaration.BaseTypes.Add(text);
			}
			codeTypeDeclaration.IsStruct = (flags & CodeFlags.IsStruct) > (CodeFlags)0;
			if ((flags & CodeFlags.IsPublic) != (CodeFlags)0)
			{
				codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			}
			else
			{
				codeTypeDeclaration.TypeAttributes &= ~TypeAttributes.Public;
			}
			if ((flags & CodeFlags.IsAbstract) != (CodeFlags)0)
			{
				codeTypeDeclaration.TypeAttributes |= TypeAttributes.Abstract;
			}
			else
			{
				codeTypeDeclaration.TypeAttributes &= ~TypeAttributes.Abstract;
			}
			if ((flags & CodeFlags.IsInterface) != (CodeFlags)0)
			{
				codeTypeDeclaration.IsInterface = true;
			}
			else
			{
				codeTypeDeclaration.IsPartial = isPartial;
			}
			codeTypeDeclaration.CustomAttributes = metadata;
			codeTypeDeclaration.CustomAttributes.Add(WebCodeGenerator.GeneratedCodeAttribute);
			return codeTypeDeclaration;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000405E8 File Offset: 0x0003E7E8
		internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeAttributeArgument[] arguments)
		{
			if (metadata == null)
			{
				metadata = new CodeAttributeDeclarationCollection();
			}
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName, arguments);
			metadata.Add(codeAttributeDeclaration);
			return metadata;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00040615 File Offset: 0x0003E815
		internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeExpression[] arguments)
		{
			return WebCodeGenerator.AddCustomAttribute(metadata, type, arguments, new string[0], new CodeExpression[0]);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0004062C File Offset: 0x0003E82C
		internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeExpression[] parameters, string[] propNames, CodeExpression[] propValues)
		{
			CodeAttributeArgument[] array = new CodeAttributeArgument[((parameters == null) ? 0 : parameters.Length) + ((propNames == null) ? 0 : propNames.Length)];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = new CodeAttributeArgument(null, parameters[i]);
			}
			for (int j = 0; j < propNames.Length; j++)
			{
				array[parameters.Length + j] = new CodeAttributeArgument(propNames[j], propValues[j]);
			}
			return WebCodeGenerator.AddCustomAttribute(metadata, type, array);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00040698 File Offset: 0x0003E898
		internal static void AddEvent(CodeTypeMemberCollection members, string handlerType, string handlerName)
		{
			CodeMemberEvent codeMemberEvent = new CodeMemberEvent();
			codeMemberEvent.Type = new CodeTypeReference(handlerType);
			codeMemberEvent.Name = handlerName;
			codeMemberEvent.Attributes = (codeMemberEvent.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			codeMemberEvent.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			members.Add(codeMemberEvent);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000406FC File Offset: 0x0003E8FC
		internal static void AddDelegate(CodeTypeDeclarationCollection codeClasses, string handlerType, string handlerArgs)
		{
			codeClasses.Add(new CodeTypeDelegate(handlerType)
			{
				CustomAttributes = { WebCodeGenerator.GeneratedCodeAttribute },
				Parameters = 
				{
					new CodeParameterDeclarationExpression(typeof(object), "sender"),
					new CodeParameterDeclarationExpression(handlerArgs, "e")
				},
				Comments = 
				{
					new CodeCommentStatement(Res.GetString("CodeRemarks"), true)
				}
			});
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0004077C File Offset: 0x0003E97C
		internal static void AddCallbackDeclaration(CodeTypeMemberCollection members, string callbackMember)
		{
			members.Add(new CodeMemberField
			{
				Type = new CodeTypeReference(typeof(SendOrPostCallback)),
				Name = callbackMember
			});
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000407B4 File Offset: 0x0003E9B4
		internal static void AddCallbackImplementation(CodeTypeDeclaration codeClass, string callbackName, string handlerName, string handlerArgs, bool methodHasOutParameters)
		{
			CodeMemberMethod codeMemberMethod = WebCodeGenerator.AddMethod(codeClass, callbackName, new CodeFlags[1], new string[] { typeof(object).FullName }, new string[] { "arg" }, typeof(void).FullName, null, (CodeFlags)0);
			CodeBinaryOperatorExpression codeBinaryOperatorExpression = new CodeBinaryOperatorExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), handlerName), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
			CodeStatement[] array = new CodeStatement[2];
			array[0] = new CodeVariableDeclarationStatement(typeof(InvokeCompletedEventArgs), "invokeArgs", new CodeCastExpression(typeof(InvokeCompletedEventArgs), new CodeArgumentReferenceExpression("arg")));
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("invokeArgs");
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
			if (methodHasOutParameters)
			{
				codeObjectCreateExpression.CreateType = new CodeTypeReference(handlerArgs);
				codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Results"));
			}
			else
			{
				codeObjectCreateExpression.CreateType = new CodeTypeReference(typeof(AsyncCompletedEventArgs));
			}
			codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Error"));
			codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Cancelled"));
			codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "UserState"));
			array[1] = new CodeExpressionStatement(new CodeDelegateInvokeExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), handlerName), new CodeExpression[]
			{
				new CodeThisReferenceExpression(),
				codeObjectCreateExpression
			}));
			codeMemberMethod.Statements.Add(new CodeConditionStatement(codeBinaryOperatorExpression, array, new CodeStatement[0]));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00040934 File Offset: 0x0003EB34
		internal static CodeMemberMethod AddAsyncMethod(CodeTypeDeclaration codeClass, string methodName, string[] parameterTypeNames, string[] parameterNames, string callbackMember, string callbackName, string userState)
		{
			CodeMemberMethod codeMemberMethod = WebCodeGenerator.AddMethod(codeClass, methodName, new CodeFlags[parameterNames.Length], parameterTypeNames, parameterNames, typeof(void).FullName, null, CodeFlags.IsPublic);
			codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), methodName, Array.Empty<CodeExpression>());
			for (int i = 0; i < parameterNames.Length; i++)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression(parameterNames[i]));
			}
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(null));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			codeMemberMethod = WebCodeGenerator.AddMethod(codeClass, methodName, new CodeFlags[parameterNames.Length], parameterTypeNames, parameterNames, typeof(void).FullName, null, CodeFlags.IsPublic);
			codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), userState));
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), callbackMember);
			CodeBinaryOperatorExpression codeBinaryOperatorExpression = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			CodeStatement[] array = new CodeStatement[]
			{
				new CodeAssignStatement(codeFieldReferenceExpression, new CodeDelegateCreateExpression
				{
					DelegateType = new CodeTypeReference(typeof(SendOrPostCallback)),
					TargetObject = new CodeThisReferenceExpression(),
					MethodName = callbackName
				})
			};
			codeMemberMethod.Statements.Add(new CodeConditionStatement(codeBinaryOperatorExpression, array, new CodeStatement[0]));
			return codeMemberMethod;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		internal static CodeTypeDeclaration CreateArgsClass(string name, string[] paramTypes, string[] paramNames, bool isPartial)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.CustomAttributes.Add(WebCodeGenerator.GeneratedCodeAttribute);
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression("code"))
			}));
			codeTypeDeclaration.IsPartial = isPartial;
			codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(AsyncCompletedEventArgs)));
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.AddUnique("Error", "Error");
			codeIdentifiers.AddUnique("Cancelled", "Cancelled");
			codeIdentifiers.AddUnique("UserState", "UserState");
			for (int i = 0; i < paramNames.Length; i++)
			{
				if (paramNames[i] != null)
				{
					codeIdentifiers.AddUnique(paramNames[i], paramNames[i]);
				}
			}
			string text = codeIdentifiers.AddUnique("results", "results");
			CodeMemberField codeMemberField = new CodeMemberField(typeof(object[]), text);
			codeTypeDeclaration.Members.Add(codeMemberField);
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Assembly;
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(typeof(object[]), text);
			codeConstructor.Parameters.Add(codeParameterDeclarationExpression);
			codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Exception), "exception"));
			codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "cancelled"));
			codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "userState"));
			codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("exception"));
			codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("cancelled"));
			codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("userState"));
			codeConstructor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name), new CodeArgumentReferenceExpression(text)));
			codeTypeDeclaration.Members.Add(codeConstructor);
			int num = 0;
			for (int j = 0; j < paramNames.Length; j++)
			{
				if (paramNames[j] != null)
				{
					codeTypeDeclaration.Members.Add(WebCodeGenerator.CreatePropertyDeclaration(codeMemberField, paramNames[j], paramTypes[j], num++));
				}
			}
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			return codeTypeDeclaration;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00040D58 File Offset: 0x0003EF58
		private static CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName, int index)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = (codeMemberProperty.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			codeMemberProperty.GetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "RaiseExceptionIfNecessary", new CodeExpression[0]));
			CodeArrayIndexerExpression codeArrayIndexerExpression = new CodeArrayIndexerExpression();
			codeArrayIndexerExpression.TargetObject = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeArrayIndexerExpression.Indices.Add(new CodePrimitiveExpression(index));
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeCastExpression(typeName, codeArrayIndexerExpression);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			return codeMemberProperty;
		}

		// Token: 0x04000579 RID: 1401
		private static CodeAttributeDeclaration generatedCodeAttribute;
	}
}
