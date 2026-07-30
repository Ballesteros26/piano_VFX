using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000637 RID: 1591
	internal abstract class BaseCompiler
	{
		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x000B8D54 File Offset: 0x000B6F54
		public VirtualPath InputVirtualPath
		{
			get
			{
				if (this.inputVirtualPath == null)
				{
					this.inputVirtualPath = new VirtualPath(VirtualPathUtility.Combine(this.parser.BaseVirtualDir, Path.GetFileName(this.parser.InputFile)));
				}
				return this.inputVirtualPath;
			}
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000B8D8F File Offset: 0x000B6F8F
		protected BaseCompiler(TemplateParser parser)
		{
			this.parser = parser;
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000B8DAC File Offset: 0x000B6FAC
		protected void AddReferencedAssembly(Assembly asm)
		{
			if (this.unit == null || asm == null)
			{
				return;
			}
			StringCollection referencedAssemblies = this.unit.ReferencedAssemblies;
			string location = asm.Location;
			if (!referencedAssemblies.Contains(location))
			{
				referencedAssemblies.Add(location);
			}
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000B8DEF File Offset: 0x000B6FEF
		internal CodeStatement AddLinePragma(CodeExpression expression, ControlBuilder builder)
		{
			return this.AddLinePragma(new CodeExpressionStatement(expression), builder);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x000B8E00 File Offset: 0x000B7000
		internal CodeStatement AddLinePragma(CodeStatement statement, ControlBuilder builder)
		{
			if (builder == null || statement == null)
			{
				return statement;
			}
			ILocation location = null;
			if (!(builder is CodeRenderBuilder))
			{
				location = builder.Location;
			}
			if (location != null)
			{
				return this.AddLinePragma(statement, location);
			}
			return this.AddLinePragma(statement, builder.Line, builder.FileName);
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x000B8E45 File Offset: 0x000B7045
		internal CodeStatement AddLinePragma(CodeStatement statement, ILocation location)
		{
			if (location == null || statement == null)
			{
				return statement;
			}
			return this.AddLinePragma(statement, location.BeginLine, location.Filename);
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x000B8E62 File Offset: 0x000B7062
		private bool IgnoreFile(string fileName)
		{
			return (this.parser != null && !this.parser.LinePragmasOn) || string.Compare(fileName, "@@inner_string@@", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000B8E8A File Offset: 0x000B708A
		internal CodeStatement AddLinePragma(CodeStatement statement, int line, string fileName)
		{
			if (statement == null || this.IgnoreFile(fileName))
			{
				return statement;
			}
			statement.LinePragma = new CodeLinePragma(fileName, line);
			return statement;
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000B8EA8 File Offset: 0x000B70A8
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, ControlBuilder builder)
		{
			if (builder == null || member == null)
			{
				return member;
			}
			ILocation location = builder.Location;
			if (location != null)
			{
				return this.AddLinePragma(member, location);
			}
			return this.AddLinePragma(member, builder.Line, builder.FileName);
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x000B8EE3 File Offset: 0x000B70E3
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, ILocation location)
		{
			if (location == null || member == null)
			{
				return member;
			}
			return this.AddLinePragma(member, location.BeginLine, location.Filename);
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x000B8F00 File Offset: 0x000B7100
		internal CodeTypeMember AddLinePragma(CodeTypeMember member, int line, string fileName)
		{
			if (member == null || this.IgnoreFile(fileName))
			{
				return member;
			}
			member.LinePragma = new CodeLinePragma(fileName, line);
			return member;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x000B8F20 File Offset: 0x000B7120
		internal void ConstructType()
		{
			this.unit = new CodeCompileUnit();
			byte[] md5Checksum = this.parser.MD5Checksum;
			if (md5Checksum != null)
			{
				CodeChecksumPragma codeChecksumPragma = new CodeChecksumPragma();
				codeChecksumPragma.FileName = this.parser.InputFile;
				codeChecksumPragma.ChecksumAlgorithmId = BaseCompiler.HashMD5;
				codeChecksumPragma.ChecksumData = md5Checksum;
				this.unit.StartDirectives.Add(codeChecksumPragma);
			}
			if (this.parser.IsPartial)
			{
				string text = null;
				string text2 = this.parser.PartialClassName;
				int num = text2.LastIndexOf('.');
				if (num != -1)
				{
					text = text2.Substring(0, num);
					text2 = text2.Substring(num + 1);
				}
				CodeNamespace codeNamespace = new CodeNamespace(text);
				this.partialClass = new CodeTypeDeclaration(text2);
				this.partialClass.IsPartial = true;
				this.partialClassExpr = new CodeTypeReferenceExpression(this.parser.PartialClassName);
				this.unit.Namespaces.Add(codeNamespace);
				this.partialClass.TypeAttributes = TypeAttributes.Public;
				codeNamespace.Types.Add(this.partialClass);
			}
			string text3 = this.parser.ClassName;
			string text4 = "ASP";
			int num2 = text3.LastIndexOf('.');
			if (num2 != -1)
			{
				text4 = text3.Substring(0, num2);
				text3 = text3.Substring(num2 + 1);
			}
			this.mainNS = new CodeNamespace(text4);
			this.mainClass = new CodeTypeDeclaration(text3);
			CodeTypeReference codeTypeReference;
			if (this.partialClass != null)
			{
				codeTypeReference = new CodeTypeReference(this.parser.PartialClassName);
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			else
			{
				codeTypeReference = new CodeTypeReference(this.parser.BaseType.FullName);
				if (this.parser.BaseTypeIsGlobal)
				{
					codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
				}
			}
			this.mainClass.BaseTypes.Add(codeTypeReference);
			this.mainClassExpr = new CodeTypeReferenceExpression(text4 + "." + text3);
			this.unit.Namespaces.Add(this.mainNS);
			this.mainClass.TypeAttributes = TypeAttributes.Public;
			this.mainNS.Types.Add(this.mainClass);
			foreach (object obj in this.parser.Imports.Keys)
			{
				if (obj is string)
				{
					this.mainNS.Imports.Add(new CodeNamespaceImport((string)obj));
				}
			}
			StringCollection referencedAssemblies = this.unit.ReferencedAssemblies;
			if (this.parser.Assemblies != null)
			{
				foreach (string text5 in this.parser.Assemblies)
				{
					string text6 = text5 as string;
					if (text6 != null && !referencedAssemblies.Contains(text6))
					{
						referencedAssemblies.Add(text6);
					}
				}
			}
			ArrayList extraAssemblies = WebConfigurationManager.ExtraAssemblies;
			if (extraAssemblies != null && extraAssemblies.Count > 0)
			{
				foreach (object obj2 in extraAssemblies)
				{
					string text6 = obj2 as string;
					if (text6 != null && !referencedAssemblies.Contains(text6))
					{
						referencedAssemblies.Add(text6);
					}
				}
			}
			IList codeAssemblies = BuildManager.CodeAssemblies;
			if (codeAssemblies != null && codeAssemblies.Count > 0)
			{
				foreach (object obj3 in codeAssemblies)
				{
					Assembly assembly = obj3 as Assembly;
					if (obj3 != null)
					{
						string text6 = assembly.Location;
						if (text6 != null && !referencedAssemblies.Contains(text6))
						{
							referencedAssemblies.Add(text6);
						}
					}
				}
			}
			this.unit.UserData["RequireVariableDeclaration"] = this.parser.ExplicitOn;
			this.unit.UserData["AllowLateBound"] = !this.parser.StrictOn;
			this.InitializeType();
			this.AddInterfaces();
			this.AddClassAttributes();
			this.CreateStaticFields();
			this.AddApplicationAndSessionObjects();
			this.AddScripts();
			this.CreateMethods();
			this.CreateConstructor(null, null);
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x000B93A4 File Offset: 0x000B75A4
		internal CodeFieldReferenceExpression GetMainClassFieldReferenceExpression(string fieldName)
		{
			CodeTypeReference codeTypeReference = new CodeTypeReference(this.mainNS.Name + "." + this.mainClass.Name);
			codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(codeTypeReference), fieldName);
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void InitializeType()
		{
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x000B93E4 File Offset: 0x000B75E4
		protected virtual void CreateStaticFields()
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(bool), "__initialized");
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodePrimitiveExpression(false);
			this.mainClass.Members.Add(codeMemberField);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000B9434 File Offset: 0x000B7634
		private void AssignAppRelativeVirtualPath(CodeConstructor ctor)
		{
			if (string.IsNullOrEmpty(this.parser.InputFile))
			{
				return;
			}
			Type type = this.parser.CodeFileBaseClassType;
			if (type == null)
			{
				type = this.parser.BaseType;
			}
			if (type == null)
			{
				return;
			}
			if (!type.IsSubclassOf(typeof(TemplateControl)))
			{
				return;
			}
			CodeTypeReference codeTypeReference = new CodeTypeReference(type.FullName);
			if (this.parser.BaseTypeIsGlobal)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeCastExpression(codeTypeReference, new CodeThisReferenceExpression()), "AppRelativeVirtualPath");
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codePropertyReferenceExpression;
			codeAssignStatement.Right = new CodePrimitiveExpression(VirtualPathUtility.RemoveTrailingSlash(this.InputVirtualPath.AppRelative));
			ctor.Statements.Add(codeAssignStatement);
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x000B9504 File Offset: 0x000B7704
		protected virtual void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
		{
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			this.mainClass.Members.Add(codeConstructor);
			if (localVars != null)
			{
				codeConstructor.Statements.AddRange(localVars);
			}
			this.AssignAppRelativeVirtualPath(codeConstructor);
			CodeFieldReferenceExpression mainClassFieldReferenceExpression = this.GetMainClassFieldReferenceExpression("__initialized");
			CodeBinaryOperatorExpression codeBinaryOperatorExpression = new CodeBinaryOperatorExpression(mainClassFieldReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(mainClassFieldReferenceExpression, new CodePrimitiveExpression(true));
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = codeBinaryOperatorExpression;
			if (trueStmt != null)
			{
				codeConditionStatement.TrueStatements.AddRange(trueStmt);
			}
			codeConditionStatement.TrueStatements.Add(codeAssignStatement);
			codeConstructor.Statements.Add(codeConditionStatement);
			this.AddStatementsToConstructor(codeConstructor);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void AddStatementsToConstructor(CodeConstructor ctor)
		{
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000B95B8 File Offset: 0x000B77B8
		private void AddScripts()
		{
			if (this.parser.Scripts == null || this.parser.Scripts.Count == 0)
			{
				return;
			}
			foreach (ServerSideScript serverSideScript in this.parser.Scripts)
			{
				ServerSideScript serverSideScript2 = serverSideScript as ServerSideScript;
				if (serverSideScript2 != null)
				{
					this.mainClass.Members.Add(this.AddLinePragma(new CodeSnippetTypeMember(serverSideScript2.Script), serverSideScript2.Location));
				}
			}
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void CreateMethods()
		{
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000B965C File Offset: 0x000B785C
		private void InternalCreatePageProperty(string retType, string name, string contextProperty)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = new CodeTypeReference(retType);
			codeMemberProperty.Attributes = (MemberAttributes)12290;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeCastExpression codeCastExpression = new CodeCastExpression();
			codeMethodReturnStatement.Expression = codeCastExpression;
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Context");
			codePropertyReferenceExpression.PropertyName = contextProperty;
			codeCastExpression.TargetType = new CodeTypeReference(retType);
			codeCastExpression.Expression = codePropertyReferenceExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			if (this.partialClass == null)
			{
				this.mainClass.Members.Add(codeMemberProperty);
				return;
			}
			this.partialClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000B9710 File Offset: 0x000B7910
		protected void CreateProfileProperty()
		{
			string text;
			if (AppCodeCompiler.HaveCustomProfile(WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection))
			{
				text = "ProfileCommon";
			}
			else
			{
				text = "System.Web.Profile.DefaultProfile";
			}
			this.InternalCreatePageProperty(text, "Profile", "Profile");
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000B9754 File Offset: 0x000B7954
		protected virtual void AddInterfaces()
		{
			if (this.parser.Interfaces == null)
			{
				return;
			}
			foreach (object obj in this.parser.Interfaces)
			{
				if (obj is string)
				{
					this.mainClass.BaseTypes.Add(new CodeTypeReference((string)obj));
				}
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void AddClassAttributes()
		{
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void AddApplicationAndSessionObjects()
		{
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x000B97D8 File Offset: 0x000B79D8
		protected void CreateApplicationOrSessionPropertyForObject(Type type, string propName, bool isApplication, bool isPublic)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Name = propName;
			if (isPublic)
			{
				codeMemberProperty.Attributes = (MemberAttributes)24578;
			}
			else
			{
				codeMemberProperty.Attributes = (MemberAttributes)20482;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression;
			if (isApplication)
			{
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Application");
			}
			else
			{
				codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Session");
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(codePropertyReferenceExpression, "StaticObjects"), "GetObject"), new CodeExpression[]
			{
				new CodePrimitiveExpression(propName)
			});
			CodeCastExpression codeCastExpression = new CodeCastExpression(codeMemberProperty.Type, codeMethodInvokeExpression);
			CodeExpression codeExpression;
			if (isApplication)
			{
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, "cached" + propName);
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = codeFieldReferenceExpression;
				codeAssignStatement.Right = codeCastExpression;
				codeConditionStatement.TrueStatements.Add(codeAssignStatement);
				codeMemberProperty.GetStatements.Add(codeConditionStatement);
				codeExpression = codeFieldReferenceExpression;
			}
			else
			{
				codeExpression = codeCastExpression;
			}
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x000B9914 File Offset: 0x000B7B14
		protected string CreateFieldForObject(Type type, string name)
		{
			string text = "cached" + name;
			CodeMemberField codeMemberField = new CodeMemberField(type, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			this.mainClass.Members.Add(codeMemberField);
			return text;
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000B9954 File Offset: 0x000B7B54
		protected void CreatePropertyForObject(Type type, string propName, string fieldName, bool isPublic)
		{
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, fieldName);
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Name = propName;
			if (isPublic)
			{
				codeMemberProperty.Attributes = (MemberAttributes)24578;
			}
			else
			{
				codeMemberProperty.Attributes = (MemberAttributes)20482;
			}
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(codeMemberProperty.Type, Array.Empty<CodeExpression>());
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression, codeObjectCreateExpression));
			codeMemberProperty.GetStatements.Add(codeConditionStatement);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeFieldReferenceExpression));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x000B9A10 File Offset: 0x000B7C10
		private void CheckCompilerErrors(CompilerResults results)
		{
			if (results.NativeCompilerReturnValue == 0)
			{
				return;
			}
			string text = null;
			CompilerErrorCollection errors = results.Errors;
			CompilerError compilerError = ((errors != null && errors.Count > 0) ? errors[0] : null);
			string text2 = ((compilerError != null) ? compilerError.FileName : null);
			if (text2 != null && File.Exists(text2))
			{
				using (StreamReader streamReader = File.OpenText(text2))
				{
					text = streamReader.ReadToEnd();
					goto IL_0087;
				}
			}
			StringWriter stringWriter = new StringWriter();
			this.provider.CreateGenerator().GenerateCodeFromCompileUnit(this.unit, stringWriter, null);
			text = stringWriter.ToString();
			IL_0087:
			throw new CompilationException(this.parser.InputFile, errors, text);
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x0009E532 File Offset: 0x0009C732
		protected string DynamicDir()
		{
			return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x000B9AC8 File Offset: 0x000B7CC8
		internal static CodeDomProvider CreateProvider(string lang)
		{
			CompilerParameters compilerParameters;
			string text;
			return BaseCompiler.CreateProvider(HttpContext.Current, lang, out compilerParameters, out text);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
		internal static CodeDomProvider CreateProvider(string lang, out string compilerOptions, out int warningLevel, out string tempdir)
		{
			return BaseCompiler.CreateProvider(HttpContext.Current, lang, out compilerOptions, out warningLevel, out tempdir);
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000B9AF4 File Offset: 0x000B7CF4
		internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out string compilerOptions, out int warningLevel, out string tempdir)
		{
			CompilerParameters compilerParameters;
			CodeDomProvider codeDomProvider = BaseCompiler.CreateProvider(context, lang, out compilerParameters, out tempdir);
			if (compilerParameters != null)
			{
				warningLevel = compilerParameters.WarningLevel;
				compilerOptions = compilerParameters.CompilerOptions;
				return codeDomProvider;
			}
			warningLevel = 2;
			compilerOptions = string.Empty;
			return codeDomProvider;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x000B9B2C File Offset: 0x000B7D2C
		internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out CompilerParameters par, out string tempdir)
		{
			CodeDomProvider codeDomProvider = null;
			par = null;
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			Compiler compiler = compilationSection.Compilers[lang];
			if (compiler == null)
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(lang);
				if (compilerInfo != null && compilerInfo.IsCodeDomProviderTypeValid)
				{
					codeDomProvider = compilerInfo.CreateProvider();
					par = compilerInfo.CreateDefaultCompilerParameters();
				}
			}
			else
			{
				codeDomProvider = Activator.CreateInstance(HttpApplication.LoadType(compiler.Type, true)) as CodeDomProvider;
				par = new CompilerParameters();
				par.CompilerOptions = compiler.CompilerOptions;
				par.WarningLevel = compiler.WarningLevel;
			}
			tempdir = compilationSection.TempDirectory;
			return codeDomProvider;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000B9BC4 File Offset: 0x000B7DC4
		[global::System.MonoTODO("find out how to extract the warningLevel and compilerOptions in the <system.codedom> case")]
		public virtual Type GetCompiledType()
		{
			Type typeFromCache = CachingCompiler.GetTypeFromCache(this.parser.InputFile);
			if (typeFromCache != null)
			{
				return typeFromCache;
			}
			this.ConstructType();
			string language = this.parser.Language;
			string text;
			int num;
			string text2;
			this.Provider = BaseCompiler.CreateProvider(this.parser.Context, language, out text, out num, out text2);
			if (this.Provider == null)
			{
				throw new HttpException("Configuration error. Language not supported: " + language, 500);
			}
			CompilerParameters compilerParameters = this.CompilerParameters;
			compilerParameters.IncludeDebugInformation = this.parser.Debug;
			compilerParameters.CompilerOptions = text + " " + this.parser.CompilerOptions;
			compilerParameters.WarningLevel = num;
			bool flag = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;
			if (text2 == null || text2 == "")
			{
				text2 = this.DynamicDir();
			}
			TempFileCollection tempFileCollection = new TempFileCollection(text2, flag);
			compilerParameters.TempFiles = tempFileCollection;
			string fileName = Path.GetFileName(tempFileCollection.AddExtension("dll", true));
			compilerParameters.OutputAssembly = Path.Combine(this.DynamicDir(), fileName);
			CompilerResults compilerResults = CachingCompiler.Compile(this);
			this.CheckCompilerErrors(compilerResults);
			Assembly assembly = compilerResults.CompiledAssembly;
			if (assembly == null)
			{
				if (!File.Exists(compilerParameters.OutputAssembly))
				{
					compilerResults.TempFiles.Delete();
					throw new CompilationException(this.parser.InputFile, compilerResults.Errors, "No assembly returned after compilation!?");
				}
				assembly = Assembly.LoadFrom(compilerParameters.OutputAssembly);
			}
			compilerResults.TempFiles.Delete();
			Type type = assembly.GetType(this.MainClassType, true);
			if (this.parser.IsPartial && !this.isRebuilding && this.CheckPartialBaseType(type))
			{
				this.isRebuilding = true;
				this.parser.RootBuilder.ResetState();
				return this.GetCompiledType();
			}
			return type;
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x000B9D9E File Offset: 0x000B7F9E
		internal string MainClassType
		{
			get
			{
				if (this.mainClassExpr == null)
				{
					return null;
				}
				return this.mainClassExpr.Type.BaseType;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x000B9DBA File Offset: 0x000B7FBA
		internal bool IsRebuildingPartial
		{
			get
			{
				return this.isRebuilding;
			}
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x000B9DC4 File Offset: 0x000B7FC4
		internal bool CheckPartialBaseType(Type type)
		{
			Type baseType = type.BaseType;
			if (baseType == null || baseType == typeof(Page))
			{
				return false;
			}
			bool flag = false;
			if (this.CheckPartialBaseFields(type, baseType))
			{
				flag = true;
			}
			if (this.CheckPartialBaseProperties(type, baseType))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x000B9E10 File Offset: 0x000B8010
		internal bool CheckPartialBaseFields(Type type, Type baseType)
		{
			bool flag = false;
			foreach (FieldInfo fieldInfo in baseType.GetFields(BaseCompiler.replaceableFlags))
			{
				if (!fieldInfo.IsPrivate)
				{
					FieldInfo field = type.GetField(fieldInfo.Name, BaseCompiler.replaceableFlags);
					if (field != null && field.DeclaringType == type)
					{
						this.partialNameOverride[field.Name] = true;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x000B9E8C File Offset: 0x000B808C
		internal bool CheckPartialBaseProperties(Type type, Type baseType)
		{
			bool flag = false;
			foreach (PropertyInfo propertyInfo in baseType.GetProperties())
			{
				PropertyInfo property = type.GetProperty(propertyInfo.Name);
				if (property != null && property.DeclaringType == type)
				{
					this.partialNameOverride[property.Name] = true;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000B9EF6 File Offset: 0x000B80F6
		// (set) Token: 0x06004455 RID: 17493 RVA: 0x000B9EFE File Offset: 0x000B80FE
		internal CodeDomProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x000B9F07 File Offset: 0x000B8107
		// (set) Token: 0x06004457 RID: 17495 RVA: 0x000B9F0F File Offset: 0x000B810F
		internal ICodeCompiler Compiler
		{
			get
			{
				return this.compiler;
			}
			set
			{
				this.compiler = value;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x000B9F18 File Offset: 0x000B8118
		// (set) Token: 0x06004459 RID: 17497 RVA: 0x000B9F33 File Offset: 0x000B8133
		internal CompilerParameters CompilerParameters
		{
			get
			{
				if (this.compilerParameters == null)
				{
					this.compilerParameters = new CompilerParameters();
				}
				return this.compilerParameters;
			}
			set
			{
				this.compilerParameters = value;
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x000B9F3C File Offset: 0x000B813C
		internal CodeCompileUnit CompileUnit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x000B9F44 File Offset: 0x000B8144
		internal CodeTypeDeclaration DerivedType
		{
			get
			{
				return this.mainClass;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x000B9F4C File Offset: 0x000B814C
		internal CodeTypeDeclaration BaseType
		{
			get
			{
				if (this.partialClass == null)
				{
					return this.DerivedType;
				}
				return this.partialClass;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000B9F63 File Offset: 0x000B8163
		internal TemplateParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x04002477 RID: 9335
		private const string DEFAULT_NAMESPACE = "ASP";

		// Token: 0x04002478 RID: 9336
		internal static Guid HashMD5 = new Guid(1080993376, 25807, 19586, 182, 240, 66, 212, 129, 114, 167, 153);

		// Token: 0x04002479 RID: 9337
		private static BindingFlags replaceableFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400247A RID: 9338
		private TemplateParser parser;

		// Token: 0x0400247B RID: 9339
		private CodeDomProvider provider;

		// Token: 0x0400247C RID: 9340
		private ICodeCompiler compiler;

		// Token: 0x0400247D RID: 9341
		private CodeCompileUnit unit;

		// Token: 0x0400247E RID: 9342
		private CodeNamespace mainNS;

		// Token: 0x0400247F RID: 9343
		private CompilerParameters compilerParameters;

		// Token: 0x04002480 RID: 9344
		private bool isRebuilding;

		// Token: 0x04002481 RID: 9345
		protected Hashtable partialNameOverride = new Hashtable();

		// Token: 0x04002482 RID: 9346
		protected CodeTypeDeclaration partialClass;

		// Token: 0x04002483 RID: 9347
		protected CodeTypeReferenceExpression partialClassExpr;

		// Token: 0x04002484 RID: 9348
		protected CodeTypeDeclaration mainClass;

		// Token: 0x04002485 RID: 9349
		protected CodeTypeReferenceExpression mainClassExpr;

		// Token: 0x04002486 RID: 9350
		protected static CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();

		// Token: 0x04002487 RID: 9351
		private VirtualPath inputVirtualPath;
	}
}
