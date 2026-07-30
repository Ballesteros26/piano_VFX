using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000615 RID: 1557
	internal class AppResourcesCompiler
	{
		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x000B1940 File Offset: 0x000AFB40
		private string TempDirectory
		{
			get
			{
				if (this.tempDirectory != null)
				{
					return this.tempDirectory;
				}
				return this.tempDirectory = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x000B1974 File Offset: 0x000AFB74
		public Dictionary<string, List<string>> CultureFiles
		{
			get
			{
				return this.cultureFiles;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000B197C File Offset: 0x000AFB7C
		public List<string> DefaultCultureFiles
		{
			get
			{
				return this.defaultCultureFiles;
			}
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000B1984 File Offset: 0x000AFB84
		static AppResourcesCompiler()
		{
			if (!BuildManager.IsPrecompiled)
			{
				return;
			}
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			if (binDirectoryAssemblies == null || binDirectoryAssemblies.Length == 0)
			{
				return;
			}
			foreach (string text in binDirectoryAssemblies)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string fileName = Path.GetFileName(text);
					if (fileName.StartsWith("App_LocalResources.", StringComparison.OrdinalIgnoreCase))
					{
						string precompiledVirtualPath = AppResourcesCompiler.GetPrecompiledVirtualPath(text);
						if (!string.IsNullOrEmpty(precompiledVirtualPath))
						{
							Assembly assembly = AppResourcesCompiler.LoadAssembly(text);
							if (!(assembly == null))
							{
								AppResourcesCompiler.AddAssemblyToCache(precompiledVirtualPath, assembly);
							}
						}
					}
					else if (string.Compare(fileName, "App_GlobalResources.dll", StringComparison.OrdinalIgnoreCase) == 0)
					{
						Assembly assembly = AppResourcesCompiler.LoadAssembly(text);
						if (!(assembly == null))
						{
							HttpContext.AppGlobalResourcesAssembly = assembly;
						}
					}
				}
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000B1A35 File Offset: 0x000AFC35
		public AppResourcesCompiler(HttpContext context)
		{
			this.isGlobal = true;
			this.files = new AppResourceFilesCollection(context);
			this.cultureFiles = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000B1A60 File Offset: 0x000AFC60
		public AppResourcesCompiler(string virtualPath)
		{
			this.virtualPath = virtualPath;
			this.isGlobal = false;
			this.files = new AppResourceFilesCollection(HttpContext.Current.Request.MapPath(virtualPath));
			this.cultureFiles = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000B1AAC File Offset: 0x000AFCAC
		private static Assembly LoadAssembly(string asmPath)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFrom(asmPath);
			}
			catch (BadImageFormatException)
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000B1AD8 File Offset: 0x000AFCD8
		private static string GetPrecompiledVirtualPath(string asmPath)
		{
			string text = Path.ChangeExtension(asmPath, ".compiled");
			if (!File.Exists(text))
			{
				return null;
			}
			string text2 = new PreservationFile(text).VirtualPath;
			if (string.IsNullOrEmpty(text2))
			{
				return "/";
			}
			if (text2.EndsWith("/App_LocalResources/", StringComparison.OrdinalIgnoreCase))
			{
				text2 = text2.Substring(0, text2.Length - 19);
			}
			return text2;
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000B1B35 File Offset: 0x000AFD35
		public Assembly Compile()
		{
			this.files.Collect();
			if (!this.files.HasFiles)
			{
				return null;
			}
			if (this.isGlobal)
			{
				return this.CompileGlobal();
			}
			return this.CompileLocal();
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000B1B68 File Offset: 0x000AFD68
		private Assembly CompileGlobal()
		{
			string text = FileUtils.CreateTemporaryFile(this.TempDirectory, "App_GlobalResources", "dll", new FileUtils.CreateTempFile(this.OnCreateRandomFile)) as string;
			if (text == null)
			{
				throw new ApplicationException("Failed to create global resources assembly");
			}
			List<string>[] array = this.GroupGlobalFiles();
			if (array == null || array.Length == 0)
			{
				return null;
			}
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(null);
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Globalization"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Resources"));
			codeCompileUnit.Namespaces.Add(codeNamespace);
			AppResourcesAssemblyBuilder appResourcesAssemblyBuilder = new AppResourcesAssemblyBuilder("App_GlobalResources", text, this);
			CodeDomProvider provider = appResourcesAssemblyBuilder.Provider;
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (List<string> list in array)
			{
				this.DomFromResource(list[0], codeCompileUnit, dictionary, provider);
			}
			foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
			{
				codeCompileUnit.ReferencedAssemblies.Add(keyValuePair.Key);
			}
			appResourcesAssemblyBuilder.Build(codeCompileUnit);
			HttpContext.AppGlobalResourcesAssembly = appResourcesAssemblyBuilder.MainAssembly;
			return appResourcesAssemblyBuilder.MainAssembly;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000B1CE4 File Offset: 0x000AFEE4
		private Assembly CompileLocal()
		{
			if (string.IsNullOrEmpty(this.virtualPath))
			{
				return null;
			}
			Assembly cachedLocalResourcesAssembly = AppResourcesCompiler.GetCachedLocalResourcesAssembly(this.virtualPath);
			if (cachedLocalResourcesAssembly != null)
			{
				return cachedLocalResourcesAssembly;
			}
			string text;
			if (this.virtualPath == "/")
			{
				text = "App_LocalResources.root";
			}
			else
			{
				text = "App_LocalResources" + this.virtualPath.Replace('/', '.');
			}
			string text2 = FileUtils.CreateTemporaryFile(this.TempDirectory, text, "dll", new FileUtils.CreateTempFile(this.OnCreateRandomFile)) as string;
			if (text2 == null)
			{
				throw new ApplicationException("Failed to create local resources assembly");
			}
			foreach (AppResourceFileInfo appResourceFileInfo in this.files.Files)
			{
				this.GetResourceFile(appResourceFileInfo, true);
			}
			AppResourcesAssemblyBuilder appResourcesAssemblyBuilder = new AppResourcesAssemblyBuilder("App_LocalResources", text2, this);
			appResourcesAssemblyBuilder.Build();
			Assembly mainAssembly = appResourcesAssemblyBuilder.MainAssembly;
			if (mainAssembly != null)
			{
				AppResourcesCompiler.AddAssemblyToCache(this.virtualPath, mainAssembly);
			}
			return mainAssembly;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000B1DFC File Offset: 0x000AFFFC
		internal static Assembly GetCachedLocalResourcesAssembly(string path)
		{
			Dictionary<string, Assembly> dictionary = HttpRuntime.InternalCache["@@LocalResourcesAssemblies"] as Dictionary<string, Assembly>;
			if (dictionary == null || !dictionary.ContainsKey(path))
			{
				return null;
			}
			return dictionary[path];
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000B1E34 File Offset: 0x000B0034
		private static void AddAssemblyToCache(string path, Assembly asm)
		{
			Cache internalCache = HttpRuntime.InternalCache;
			Dictionary<string, Assembly> dictionary = internalCache["@@LocalResourcesAssemblies"] as Dictionary<string, Assembly>;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, Assembly>();
			}
			dictionary[path] = asm;
			internalCache.Insert("@@LocalResourcesAssemblies", dictionary);
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000B1E74 File Offset: 0x000B0074
		private uint CountChars(char c, string s)
		{
			uint num = 0U;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == c)
				{
					num += 1U;
				}
			}
			return num;
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000B1EA8 File Offset: 0x000B00A8
		private string IsFileCultureValid(string fileName)
		{
			string text = Path.GetFileNameWithoutExtension(fileName);
			text = Path.GetExtension(text);
			if (text != null && text.Length > 0)
			{
				text = text.Substring(1);
				try
				{
					CultureInfo.GetCultureInfo(text);
					return text;
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000B1EFC File Offset: 0x000B00FC
		private string GetResourceFile(AppResourceFileInfo arfi, bool local)
		{
			string text;
			if (arfi.Kind == AppResourceFileKind.ResX)
			{
				text = this.CompileResource(arfi, local);
			}
			else
			{
				text = arfi.Info.FullName;
			}
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = this.IsFileCultureValid(text);
				List<string> list;
				if (text2 != null)
				{
					if (this.cultureFiles.ContainsKey(text2))
					{
						list = this.cultureFiles[text2];
					}
					else
					{
						list = new List<string>(1);
						this.cultureFiles[text2] = list;
					}
				}
				else
				{
					if (this.defaultCultureFiles == null)
					{
						this.defaultCultureFiles = new List<string>();
					}
					list = this.defaultCultureFiles;
				}
				list.Add(text);
			}
			return text;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000B1F94 File Offset: 0x000B0194
		private List<string>[] GroupGlobalFiles()
		{
			List<AppResourceFileInfo> list = this.files.Files;
			List<List<string>> list2 = new List<List<string>>();
			AppResourcesLengthComparer<List<string>> appResourcesLengthComparer = new AppResourcesLengthComparer<List<string>>();
			foreach (AppResourceFileInfo appResourceFileInfo in list)
			{
				if (appResourceFileInfo.Kind == AppResourceFileKind.ResX || appResourceFileInfo.Kind == AppResourceFileKind.Resource)
				{
					string text = appResourceFileInfo.Info.FullName;
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					uint num = this.CountChars('.', fileNameWithoutExtension);
					AppResourceFileInfo appResourceFileInfo2 = null;
					foreach (AppResourceFileInfo appResourceFileInfo3 in list)
					{
						if (!appResourceFileInfo3.Seen)
						{
							string fullName = appResourceFileInfo3.Info.FullName;
							if (fullName != null && !(text == fullName))
							{
								string text2 = Path.GetFileNameWithoutExtension(fullName);
								if (this.CountChars('.', text2) == num + 1U && text2.StartsWith(fileNameWithoutExtension))
								{
									if (this.IsFileCultureValid(fullName) != null)
									{
										appResourceFileInfo2 = appResourceFileInfo;
										break;
									}
									appResourceFileInfo3.Seen = true;
								}
							}
						}
					}
					if (appResourceFileInfo2 != null)
					{
						List<string> list3 = new List<string>();
						list3.Add(this.GetResourceFile(appResourceFileInfo, false));
						appResourceFileInfo.Seen = true;
						list2.Add(list3);
					}
				}
			}
			list2.Sort(appResourcesLengthComparer);
			foreach (List<string> list4 in list2)
			{
				string text = list4[0];
				string text2 = Path.GetFileNameWithoutExtension(text);
				if (text2.StartsWith("Resources."))
				{
					text2 = text2.Substring(10);
				}
				foreach (AppResourceFileInfo appResourceFileInfo4 in list)
				{
					if (!appResourceFileInfo4.Seen)
					{
						text = appResourceFileInfo4.Info.FullName;
						if (text != null && appResourceFileInfo4.Info.Name.StartsWith(text2))
						{
							list4.Add(this.GetResourceFile(appResourceFileInfo4, false));
							appResourceFileInfo4.Seen = true;
						}
					}
				}
			}
			foreach (AppResourceFileInfo appResourceFileInfo5 in list)
			{
				if (!appResourceFileInfo5.Seen && this.IsFileCultureValid(appResourceFileInfo5.Info.FullName) == null)
				{
					list2.Add(new List<string> { this.GetResourceFile(appResourceFileInfo5, false) });
				}
			}
			list2.Sort(appResourcesLengthComparer);
			return list2.ToArray();
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000B22A4 File Offset: 0x000B04A4
		private void DomFromResource(string resfile, CodeCompileUnit unit, Dictionary<string, bool> assemblies, CodeDomProvider provider)
		{
			if (string.IsNullOrEmpty(resfile))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resfile);
			string text = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
			string text2 = Path.GetExtension(fileNameWithoutExtension);
			if (text2 == null || text2.Length == 0)
			{
				text2 = text;
				text = "Resources";
			}
			else
			{
				if (!text.StartsWith("Resources", StringComparison.InvariantCulture))
				{
					text = "Resources." + text;
				}
				text2 = text2.Substring(1);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = text2.Replace('.', '_');
			}
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace('.', '_');
			}
			if (!provider.IsValidIdentifier(text) || !provider.IsValidIdentifier(text2))
			{
				throw new ApplicationException("Invalid resource file name.");
			}
			ResourceReader resourceReader;
			try
			{
				resourceReader = new ResourceReader(resfile);
			}
			catch (ArgumentException)
			{
				return;
			}
			CodeNamespace codeNamespace = new CodeNamespace(text);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text2);
			codeTypeDeclaration.IsClass = true;
			codeTypeDeclaration.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
			CodeMemberField codeMemberField = new CodeMemberField(typeof(CultureInfo), "_culture");
			codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeTypeDeclaration.Members.Add(codeMemberField);
			codeMemberField = new CodeMemberField(typeof(ResourceManager), "_resourceManager");
			codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeTypeDeclaration.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes = (MemberAttributes)24579;
			codeMemberProperty.Name = "ResourceManager";
			codeMemberProperty.HasGet = true;
			codeMemberProperty.Type = new CodeTypeReference(typeof(ResourceManager));
			this.CodePropertyResourceManagerGet(codeMemberProperty.GetStatements, resfile, text2);
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes = (MemberAttributes)24578;
			codeMemberProperty.Attributes = (MemberAttributes)24579;
			codeMemberProperty.Name = "Culture";
			codeMemberProperty.HasGet = true;
			codeMemberProperty.HasSet = true;
			codeMemberProperty.Type = new CodeTypeReference(typeof(CultureInfo));
			this.CodePropertyGenericGet(codeMemberProperty.GetStatements, "_culture", text2);
			this.CodePropertyGenericSet(codeMemberProperty.SetStatements, "_culture", text2);
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			try
			{
				foreach (object obj in resourceReader)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Type type = dictionaryEntry.Value.GetType();
					if (!dictionary.ContainsKey(type.Namespace))
					{
						dictionary[type.Namespace] = true;
					}
					string name = new AssemblyName(type.Assembly.FullName).Name;
					if (!assemblies.ContainsKey(name))
					{
						assemblies[name] = true;
					}
					codeMemberProperty = new CodeMemberProperty();
					codeMemberProperty.Attributes = (MemberAttributes)24579;
					codeMemberProperty.Name = this.SanitizeResourceName(provider, (string)dictionaryEntry.Key);
					codeMemberProperty.HasGet = true;
					this.CodePropertyResourceGet(codeMemberProperty.GetStatements, (string)dictionaryEntry.Key, type, text2);
					codeMemberProperty.Type = new CodeTypeReference(type);
					codeTypeDeclaration.Members.Add(codeMemberProperty);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to compile global resources.", ex);
			}
			foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport(keyValuePair.Key));
			}
			codeNamespace.Types.Add(codeTypeDeclaration);
			unit.Namespaces.Add(codeNamespace);
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x000B26BC File Offset: 0x000B08BC
		private static bool is_identifier_start_character(int c)
		{
			return (c >= 97 && c <= 122) || (c >= 65 && c <= 90) || c == 95 || char.IsLetter((char)c);
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000B26E0 File Offset: 0x000B08E0
		private static bool is_identifier_part_character(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == '_' || (c >= '0' && c <= '9')) || (c >= '\u0080' && (char.IsLetter(c) || char.GetUnicodeCategory(c) == UnicodeCategory.ConnectorPunctuation));
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000B2734 File Offset: 0x000B0934
		private string SanitizeResourceName(CodeDomProvider provider, string name)
		{
			if (provider.IsValidIdentifier(name))
			{
				return provider.CreateEscapedIdentifier(name);
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = name[0];
			if (AppResourcesCompiler.is_identifier_start_character((int)c))
			{
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append('_');
				if (c >= '0' && c <= '9')
				{
					stringBuilder.Append(c);
				}
			}
			for (int i = 1; i < name.Length; i++)
			{
				c = name[i];
				if (AppResourcesCompiler.is_identifier_part_character(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			return provider.CreateEscapedIdentifier(stringBuilder.ToString());
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000B27D0 File Offset: 0x000B09D0
		private CodeObjectCreateExpression NewResourceManager(string name, string typename)
		{
			CodeExpression codeExpression = new CodePrimitiveExpression(name);
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeTypeOfExpression(new CodeTypeReference(typename)), "Assembly");
			return new CodeObjectCreateExpression("System.Resources.ResourceManager", new CodeExpression[] { codeExpression, codePropertyReferenceExpression });
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000B2814 File Offset: 0x000B0A14
		private void CodePropertyResourceManagerGet(CodeStatementCollection csc, string resfile, string typename)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resfile);
			CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), "_resourceManager");
			CodeStatement codeStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[]
			{
				new CodeMethodReturnStatement(codeExpression)
			});
			csc.Add(codeStatement);
			codeStatement = new CodeAssignStatement(codeExpression, this.NewResourceManager(fileNameWithoutExtension, typename));
			csc.Add(codeStatement);
			csc.Add(new CodeMethodReturnStatement(codeExpression));
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000B2888 File Offset: 0x000B0A88
		private void CodePropertyResourceGet(CodeStatementCollection csc, string resname, Type restype, string typename)
		{
			CodeStatement codeStatement = new CodeVariableDeclarationStatement(typeof(ResourceManager), "rm", new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typename), "ResourceManager"));
			csc.Add(codeStatement);
			codeStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("rm"), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)), new CodeStatement[]
			{
				new CodeMethodReturnStatement(new CodePrimitiveExpression(null))
			});
			csc.Add(codeStatement);
			bool flag = restype == typeof(string);
			CodeExpression codeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("rm"), flag ? "GetString" : "GetObject", new CodeExpression[]
			{
				new CodePrimitiveExpression(resname),
				new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), "_culture")
			});
			codeStatement = new CodeVariableDeclarationStatement(restype, "obj", flag ? codeExpression : new CodeCastExpression(restype, codeExpression));
			csc.Add(codeStatement);
			csc.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("obj")));
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000B2985 File Offset: 0x000B0B85
		private void CodePropertyGenericGet(CodeStatementCollection csc, string field, string typename)
		{
			csc.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), field)));
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000B299F File Offset: 0x000B0B9F
		private void CodePropertyGenericSet(CodeStatementCollection csc, string field, string typename)
		{
			csc.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), field), new CodeVariableReferenceExpression("value")));
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000B29C4 File Offset: 0x000B0BC4
		private string CompileResource(AppResourceFileInfo arfi, bool local)
		{
			string fullName = arfi.Info.FullName;
			string text = Path.GetFileNameWithoutExtension(fullName) + ".resources";
			if (!local)
			{
				text = "Resources." + text;
			}
			string text2 = Path.Combine(this.TempDirectory, text);
			FileStream fileStream = null;
			FileStream fileStream2 = null;
			IResourceReader resourceReader = null;
			ResourceWriter resourceWriter = null;
			try
			{
				fileStream = new FileStream(fullName, FileMode.Open, FileAccess.Read);
				fileStream2 = new FileStream(text2, FileMode.Create, FileAccess.Write);
				resourceReader = this.GetReaderForKind(arfi.Kind, fileStream, fullName);
				resourceWriter = new ResourceWriter(fileStream2);
				foreach (object obj in resourceReader)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					if (value is string)
					{
						resourceWriter.AddResource((string)dictionaryEntry.Key, (string)value);
					}
					else
					{
						resourceWriter.AddResource((string)dictionaryEntry.Key, value);
					}
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to compile resource file", ex);
			}
			finally
			{
				if (resourceReader != null)
				{
					resourceReader.Dispose();
				}
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
				if (resourceWriter != null)
				{
					resourceWriter.Dispose();
				}
				if (fileStream2 != null)
				{
					fileStream2.Dispose();
				}
			}
			return text2;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000B2B24 File Offset: 0x000B0D24
		private IResourceReader GetReaderForKind(AppResourceFileKind kind, Stream stream, string path)
		{
			if (kind == AppResourceFileKind.ResX)
			{
				ResXResourceReader resXResourceReader = new ResXResourceReader(stream, new AppResourcesCompiler.TypeResolutionService());
				if (!string.IsNullOrEmpty(path))
				{
					resXResourceReader.BasePath = Path.GetDirectoryName(path);
				}
				return resXResourceReader;
			}
			if (kind != AppResourceFileKind.Resource)
			{
				return null;
			}
			return new ResourceReader(stream);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000AFF2B File Offset: 0x000AE12B
		private object OnCreateRandomFile(string path)
		{
			new FileStream(path, FileMode.CreateNew).Close();
			return path;
		}

		// Token: 0x040023E0 RID: 9184
		private const string cachePrefix = "@@LocalResourcesAssemblies";

		// Token: 0x040023E1 RID: 9185
		private bool isGlobal;

		// Token: 0x040023E2 RID: 9186
		private AppResourceFilesCollection files;

		// Token: 0x040023E3 RID: 9187
		private string tempDirectory;

		// Token: 0x040023E4 RID: 9188
		private string virtualPath;

		// Token: 0x040023E5 RID: 9189
		private Dictionary<string, List<string>> cultureFiles;

		// Token: 0x040023E6 RID: 9190
		private List<string> defaultCultureFiles;

		// Token: 0x02000616 RID: 1558
		private class TypeResolutionService : ITypeResolutionService
		{
			// Token: 0x0600430E RID: 17166 RVA: 0x000B2B65 File Offset: 0x000B0D65
			public Assembly GetAssembly(AssemblyName name)
			{
				return this.GetAssembly(name, false);
			}

			// Token: 0x0600430F RID: 17167 RVA: 0x000B2B70 File Offset: 0x000B0D70
			public Assembly GetAssembly(AssemblyName name, bool throwOnError)
			{
				try
				{
					return Assembly.Load(name);
				}
				catch
				{
					if (throwOnError)
					{
						throw;
					}
				}
				return null;
			}

			// Token: 0x06004310 RID: 17168 RVA: 0x000B2BA4 File Offset: 0x000B0DA4
			public void ReferenceAssembly(AssemblyName name)
			{
				if (this.referencedAssemblies == null)
				{
					this.referencedAssemblies = new List<Assembly>();
				}
				Assembly assembly = this.GetAssembly(name, false);
				if (assembly == null)
				{
					return;
				}
				if (this.referencedAssemblies.Contains(assembly))
				{
					return;
				}
				this.referencedAssemblies.Add(assembly);
			}

			// Token: 0x06004311 RID: 17169 RVA: 0x000B2BF4 File Offset: 0x000B0DF4
			public string GetPathOfAssembly(AssemblyName name)
			{
				if (name == null)
				{
					return null;
				}
				Assembly assembly = this.GetAssembly(name, false);
				if (assembly == null)
				{
					return null;
				}
				return assembly.Location;
			}

			// Token: 0x06004312 RID: 17170 RVA: 0x000B2C20 File Offset: 0x000B0E20
			public Type GetType(string name)
			{
				return this.GetType(name, false, false);
			}

			// Token: 0x06004313 RID: 17171 RVA: 0x000B2C2B File Offset: 0x000B0E2B
			public Type GetType(string name, bool throwOnError)
			{
				return this.GetType(name, throwOnError, false);
			}

			// Token: 0x06004314 RID: 17172 RVA: 0x000B2C38 File Offset: 0x000B0E38
			public Type GetType(string name, bool throwOnError, bool ignoreCase)
			{
				if (string.IsNullOrEmpty(name))
				{
					if (throwOnError)
					{
						throw new ArgumentNullException("name");
					}
					return null;
				}
				else if (name.IndexOf(',') == -1)
				{
					Type type = this.MapType(name, false);
					if (type != null)
					{
						return type;
					}
					type = this.FindInAssemblies(name, ignoreCase);
					if (!(type == null))
					{
						return type;
					}
					if (throwOnError)
					{
						throw new InvalidOperationException("Type '" + name + "' is not fully qualified and there are no referenced assemblies.");
					}
					return null;
				}
				else
				{
					Type type = this.MapType(name, true);
					if (type != null)
					{
						return type;
					}
					return Type.GetType(name, throwOnError, ignoreCase);
				}
			}

			// Token: 0x06004315 RID: 17173 RVA: 0x000B2CCC File Offset: 0x000B0ECC
			private Type MapType(string name, bool full)
			{
				if (this.mappedTypes == null)
				{
					this.mappedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
				}
				Type type;
				if (this.mappedTypes.TryGetValue(name, out type))
				{
					return type;
				}
				if (!full)
				{
					if (string.Compare(name, "ResXDataNode", StringComparison.Ordinal) == 0)
					{
						return this.AddMappedType(name, typeof(ResXDataNode));
					}
					if (string.Compare(name, "ResXFileRef", StringComparison.Ordinal) == 0)
					{
						return this.AddMappedType(name, typeof(ResXFileRef));
					}
					if (string.Compare(name, "ResXNullRef", StringComparison.Ordinal) == 0)
					{
						return this.AddMappedType(name, typeof(ResXNullRef));
					}
					if (string.Compare(name, "ResXResourceReader", StringComparison.Ordinal) == 0)
					{
						return this.AddMappedType(name, typeof(ResXResourceReader));
					}
					if (string.Compare(name, "ResXResourceWriter", StringComparison.Ordinal) == 0)
					{
						return this.AddMappedType(name, typeof(ResXResourceWriter));
					}
					return null;
				}
				else
				{
					if (name.IndexOf("System.Windows.Forms") == -1)
					{
						return null;
					}
					if (name.IndexOf("ResXDataNode", StringComparison.Ordinal) != -1)
					{
						return this.AddMappedType(name, typeof(ResXDataNode));
					}
					if (name.IndexOf("ResXFileRef", StringComparison.Ordinal) != -1)
					{
						return this.AddMappedType(name, typeof(ResXFileRef));
					}
					if (name.IndexOf("ResXNullRef", StringComparison.Ordinal) != -1)
					{
						return this.AddMappedType(name, typeof(ResXNullRef));
					}
					if (name.IndexOf("ResXResourceReader", StringComparison.Ordinal) != -1)
					{
						return this.AddMappedType(name, typeof(ResXResourceReader));
					}
					if (name.IndexOf("ResXResourceWriter", StringComparison.Ordinal) != -1)
					{
						return this.AddMappedType(name, typeof(ResXResourceWriter));
					}
					return null;
				}
			}

			// Token: 0x06004316 RID: 17174 RVA: 0x000B2E61 File Offset: 0x000B1061
			private Type AddMappedType(string name, Type type)
			{
				this.mappedTypes.Add(name, type);
				return type;
			}

			// Token: 0x06004317 RID: 17175 RVA: 0x000B2E74 File Offset: 0x000B1074
			private Type FindInAssemblies(string name, bool ignoreCase)
			{
				Type type = Type.GetType(name, false);
				if (type != null)
				{
					return type;
				}
				if (this.referencedAssemblies == null || this.referencedAssemblies.Count == 0)
				{
					return null;
				}
				foreach (Assembly assembly in this.referencedAssemblies)
				{
					type = assembly.GetType(name, false, ignoreCase);
					if (type != null)
					{
						return type;
					}
				}
				return null;
			}

			// Token: 0x040023E7 RID: 9191
			private List<Assembly> referencedAssemblies;

			// Token: 0x040023E8 RID: 9192
			private Dictionary<string, Type> mappedTypes;
		}
	}
}
