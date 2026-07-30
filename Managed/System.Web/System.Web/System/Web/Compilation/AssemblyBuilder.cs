using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Provides a container for building an assembly from one or more virtual paths within an ASP.NET project.</summary>
	// Token: 0x02000632 RID: 1586
	public class AssemblyBuilder
	{
		// Token: 0x060043ED RID: 17389 RVA: 0x000B79C4 File Offset: 0x000B5BC4
		internal AssemblyBuilder(CodeDomProvider provider)
			: this(null, provider, "App_Web_")
		{
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000B79D3 File Offset: 0x000B5BD3
		internal AssemblyBuilder(CodeDomProvider provider, string assemblyBaseName)
			: this(null, provider, assemblyBaseName)
		{
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000B79DE File Offset: 0x000B5BDE
		internal AssemblyBuilder(VirtualPath virtualPath, CodeDomProvider provider)
			: this(virtualPath, provider, "App_Web_")
		{
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x000B79F0 File Offset: 0x000B5BF0
		internal AssemblyBuilder(VirtualPath virtualPath, CodeDomProvider provider, string assemblyBaseName)
		{
			this.provider = provider;
			this.outputFilesPrefix = assemblyBaseName ?? "App_Web_";
			this.units = new List<AssemblyBuilder.CodeUnit>();
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			string text = compilationSection.TempDirectory;
			if (string.IsNullOrEmpty(text))
			{
				text = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
			}
			if (!AssemblyBuilder.KeepFiles)
			{
				AssemblyBuilder.KeepFiles = compilationSection.Debug;
			}
			this.temp_files = new TempFileCollection(text, AssemblyBuilder.KeepFiles);
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x000B7A77 File Offset: 0x000B5C77
		// (set) Token: 0x060043F2 RID: 17394 RVA: 0x000B7A92 File Offset: 0x000B5C92
		internal string OutputFilesPrefix
		{
			get
			{
				if (this.outputFilesPrefix == null)
				{
					this.outputFilesPrefix = "App_Web_";
				}
				return this.outputFilesPrefix;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.outputFilesPrefix = "App_Web_";
				}
				else
				{
					this.outputFilesPrefix = value;
				}
				this.outputAssemblyPrefix = null;
				this.outputAssemblyName = null;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x000B7AC0 File Offset: 0x000B5CC0
		internal string OutputAssemblyPrefix
		{
			get
			{
				if (this.outputAssemblyPrefix == null)
				{
					string basePath = this.temp_files.BasePath;
					string fileName = Path.GetFileName(basePath);
					string directoryName = Path.GetDirectoryName(basePath);
					this.outputAssemblyPrefix = Path.Combine(directoryName, this.OutputFilesPrefix + fileName);
				}
				return this.outputAssemblyPrefix;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000B7B0B File Offset: 0x000B5D0B
		internal string OutputAssemblyName
		{
			get
			{
				if (this.outputAssemblyName == null)
				{
					this.outputAssemblyName = this.OutputAssemblyPrefix + ".dll";
				}
				return this.outputAssemblyName;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x000B7B31 File Offset: 0x000B5D31
		internal TempFileCollection TempFiles
		{
			get
			{
				return this.temp_files;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x000B7B39 File Offset: 0x000B5D39
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x000B7B41 File Offset: 0x000B5D41
		internal CompilerParameters CompilerOptions
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000B7B4C File Offset: 0x000B5D4C
		private AssemblyBuilder.CodeUnit[] GetUnitsAsArray()
		{
			AssemblyBuilder.CodeUnit[] array = new AssemblyBuilder.CodeUnit[this.units.Count];
			this.units.CopyTo(array, 0);
			return array;
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x000B7B78 File Offset: 0x000B5D78
		internal Dictionary<string, List<CompileUnitPartialType>> PartialTypes
		{
			get
			{
				if (this.partial_types == null)
				{
					this.partial_types = new Dictionary<string, List<CompileUnitPartialType>>();
				}
				return this.partial_types;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000B7B93 File Offset: 0x000B5D93
		private Dictionary<string, bool> CodeFiles
		{
			get
			{
				if (this.code_files == null)
				{
					this.code_files = new Dictionary<string, bool>();
				}
				return this.code_files;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x000B7BAE File Offset: 0x000B5DAE
		private List<string> SourceFiles
		{
			get
			{
				if (this.source_files == null)
				{
					this.source_files = new List<string>();
				}
				return this.source_files;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x000B7BC9 File Offset: 0x000B5DC9
		private Dictionary<string, string> ResourceFiles
		{
			get
			{
				if (this.resource_files == null)
				{
					this.resource_files = new Dictionary<string, string>();
				}
				return this.resource_files;
			}
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x000B7BE4 File Offset: 0x000B5DE4
		internal BuildProvider GetBuildProviderForPhysicalFilePath(string path)
		{
			if (string.IsNullOrEmpty(path) || this.path_to_buildprovider == null || this.path_to_buildprovider.Count == 0)
			{
				return null;
			}
			BuildProvider buildProvider;
			if (this.path_to_buildprovider.TryGetValue(path, out buildProvider))
			{
				return buildProvider;
			}
			return null;
		}

		/// <summary>Adds an assembly that is referenced by source code generated for a file.</summary>
		/// <param name="a">An assembly referenced by a code compile unit or source file included in the assembly compilation.</param>
		// Token: 0x060043FE RID: 17406 RVA: 0x000B7C24 File Offset: 0x000B5E24
		public void AddAssemblyReference(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			List<Assembly> referencedAssemblies = this.ReferencedAssemblies;
			if (referencedAssemblies.Contains(a))
			{
				return;
			}
			referencedAssemblies.Add(a);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000B7C60 File Offset: 0x000B5E60
		internal void AddAssemblyReference(string assemblyLocation)
		{
			try
			{
				Assembly assembly = Assembly.LoadFrom(assemblyLocation);
				if (!(assembly == null))
				{
					this.AddAssemblyReference(assembly);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000B7C9C File Offset: 0x000B5E9C
		internal void AddAssemblyReference(ICollection asmcoll)
		{
			if (asmcoll == null || asmcoll.Count == 0)
			{
				return;
			}
			foreach (object obj in asmcoll)
			{
				Assembly assembly = obj as Assembly;
				if (!(assembly == null))
				{
					this.AddAssemblyReference(assembly);
				}
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000B7D08 File Offset: 0x000B5F08
		internal void AddAssemblyReference(List<Assembly> asmlist)
		{
			if (asmlist == null)
			{
				return;
			}
			foreach (Assembly assembly in asmlist)
			{
				if (!(assembly == null))
				{
					this.AddAssemblyReference(assembly);
				}
			}
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000B7D64 File Offset: 0x000B5F64
		internal void AddCodeCompileUnit(CodeCompileUnit compileUnit)
		{
			if (compileUnit == null)
			{
				throw new ArgumentNullException("compileUnit");
			}
			this.units.Add(this.CheckForPartialTypes(new AssemblyBuilder.CodeUnit(null, compileUnit)));
		}

		/// <summary>Adds source code for the assembly in the form of a CodeDOM graph.</summary>
		/// <param name="buildProvider">The build provider generating <paramref name="compileUnit" />.</param>
		/// <param name="compileUnit">The code compile unit to include in the assembly compilation.</param>
		// Token: 0x06004403 RID: 17411 RVA: 0x000B7D8C File Offset: 0x000B5F8C
		public void AddCodeCompileUnit(BuildProvider buildProvider, CodeCompileUnit compileUnit)
		{
			if (buildProvider == null)
			{
				throw new ArgumentNullException("buildProvider");
			}
			if (compileUnit == null)
			{
				throw new ArgumentNullException("compileUnit");
			}
			this.units.Add(this.CheckForPartialTypes(new AssemblyBuilder.CodeUnit(buildProvider, compileUnit)));
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000B7DC2 File Offset: 0x000B5FC2
		private void AddPathToBuilderMap(string path, BuildProvider bp)
		{
			if (this.path_to_buildprovider == null)
			{
				this.path_to_buildprovider = new Dictionary<string, BuildProvider>();
			}
			if (this.path_to_buildprovider.ContainsKey(path))
			{
				return;
			}
			this.path_to_buildprovider.Add(path, bp);
		}

		/// <summary>Allows a build provider to create a temporary source file, and include the source file in the assembly compilation.</summary>
		/// <returns>An open <see cref="T:System.IO.TextWriter" /> that can be used to write source code to a temporary file.</returns>
		/// <param name="buildProvider">The build provider generating the code source file.</param>
		// Token: 0x06004405 RID: 17413 RVA: 0x000B7DF4 File Offset: 0x000B5FF4
		public TextWriter CreateCodeFile(BuildProvider buildProvider)
		{
			if (buildProvider == null)
			{
				throw new ArgumentNullException("buildProvider");
			}
			string tempFilePhysicalPath = this.GetTempFilePhysicalPath(this.provider.FileExtension);
			this.SourceFiles.Add(tempFilePhysicalPath);
			this.AddPathToBuilderMap(tempFilePhysicalPath, buildProvider);
			return new StreamWriter(File.OpenWrite(tempFilePhysicalPath));
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000B7E40 File Offset: 0x000B6040
		internal void AddCodeFile(string path)
		{
			this.AddCodeFile(path, null, false);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000B7E4B File Offset: 0x000B604B
		internal void AddCodeFile(string path, BuildProvider bp)
		{
			this.AddCodeFile(path, bp, false);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000B7E58 File Offset: 0x000B6058
		internal void AddCodeFile(string path, BuildProvider bp, bool isVirtual)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			Dictionary<string, bool> codeFiles = this.CodeFiles;
			if (codeFiles.ContainsKey(path))
			{
				return;
			}
			codeFiles.Add(path, true);
			string text = Path.GetExtension(path);
			if (text == null || text.Length == 0)
			{
				return;
			}
			text = text.Substring(1);
			string tempFilePhysicalPath = this.GetTempFilePhysicalPath(text);
			string text2 = text.ToLowerInvariant();
			AssemblyBuilder.ICodePragmaGenerator codePragmaGenerator;
			if (!(text2 == "cs"))
			{
				if (!(text2 == "vb"))
				{
					codePragmaGenerator = null;
				}
				else
				{
					codePragmaGenerator = new AssemblyBuilder.VBCodePragmaGenerator();
				}
			}
			else
			{
				codePragmaGenerator = new AssemblyBuilder.CSharpCodePragmaGenerator();
			}
			if (isVirtual)
			{
				VirtualFile file = HostingEnvironment.VirtualPathProvider.GetFile(path);
				if (file == null)
				{
					throw new HttpException(404, "Virtual file '" + path + "' does not exist.");
				}
				if (file is DefaultVirtualFile)
				{
					path = HostingEnvironment.MapPath(path);
				}
				this.CopyFileWithChecksum(file.Open(), tempFilePhysicalPath, path, codePragmaGenerator);
			}
			else
			{
				this.CopyFileWithChecksum(path, tempFilePhysicalPath, path, codePragmaGenerator);
			}
			if (codePragmaGenerator != null)
			{
				if (bp != null)
				{
					this.AddPathToBuilderMap(tempFilePhysicalPath, bp);
				}
				this.SourceFiles.Add(tempFilePhysicalPath);
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000B7F58 File Offset: 0x000B6158
		private void CopyFileWithChecksum(string input, string to, string from, AssemblyBuilder.ICodePragmaGenerator pragmaGenerator)
		{
			this.CopyFileWithChecksum(new FileStream(input, FileMode.Open, FileAccess.Read), to, from, pragmaGenerator);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000B7F6C File Offset: 0x000B616C
		private void CopyFileWithChecksum(Stream input, string to, string from, AssemblyBuilder.ICodePragmaGenerator pragmaGenerator)
		{
			if (pragmaGenerator == null)
			{
				string text;
				using (StreamReader streamReader = new StreamReader(input, WebEncoding.FileEncoding))
				{
					text = streamReader.ReadToEnd();
				}
				CodeSnippetCompileUnit codeSnippetCompileUnit = new CodeSnippetCompileUnit(text);
				codeSnippetCompileUnit.LinePragma = new CodeLinePragma(from, 1);
				text = null;
				this.AddCodeCompileUnit(codeSnippetCompileUnit);
				return;
			}
			MD5 md = MD5.Create();
			using (FileStream fileStream = new FileStream(to, FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					using (StreamReader streamReader2 = new StreamReader(input, WebEncoding.FileEncoding))
					{
						int num = pragmaGenerator.ReserveSpace(from);
						char[] array;
						if (num > 8192)
						{
							array = new char[num];
						}
						else
						{
							array = new char[8192];
						}
						streamWriter.Write(array, 0, num);
						for (;;)
						{
							num = streamReader2.Read(array, 0, 8192);
							if (num == 0)
							{
								break;
							}
							streamWriter.Write(array, 0, num);
							this.UpdateChecksum(array, num, md, false);
						}
						this.UpdateChecksum(array, 0, md, true);
					}
				}
			}
			pragmaGenerator.DecorateFile(to, from, md, Encoding.UTF8);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000B80C8 File Offset: 0x000B62C8
		private void UpdateChecksum(char[] buf, int count, MD5 checksum, bool final)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(buf, 0, count);
			if (final)
			{
				checksum.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			else
			{
				checksum.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		/// <summary>Allows a build provider to create a resource file to include in the assembly compilation.</summary>
		/// <returns>An open <see cref="T:System.IO.Stream" /> that can be used to write resources, which are included in the assembly compilation.</returns>
		/// <param name="buildProvider">The build provider generating the resource.</param>
		/// <param name="name">The name of the resource file to be created.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid file name.</exception>
		// Token: 0x0600440C RID: 17420 RVA: 0x000B8108 File Offset: 0x000B6308
		public Stream CreateEmbeddedResource(BuildProvider buildProvider, string name)
		{
			if (buildProvider == null)
			{
				throw new ArgumentNullException("buildProvider");
			}
			if (name == null || name == "")
			{
				throw new ArgumentNullException("name");
			}
			string tempFilePhysicalPath = this.GetTempFilePhysicalPath("resource");
			Stream stream = File.OpenWrite(tempFilePhysicalPath);
			this.ResourceFiles[name] = tempFilePhysicalPath;
			return stream;
		}

		/// <summary>Inserts a fast object factory template for a type into the compiled assembly.</summary>
		/// <param name="typeName">The name of the type to generate.</param>
		// Token: 0x0600440D RID: 17421 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Not implemented, does nothing")]
		public void GenerateTypeFactory(string typeName)
		{
		}

		/// <summary>Generates a temporary file path.</summary>
		/// <returns>A path to a temporary file, with the specified file extension.</returns>
		/// <param name="extension">The file extension to use for the temporary file.</param>
		// Token: 0x0600440E RID: 17422 RVA: 0x000B8160 File Offset: 0x000B6360
		public string GetTempFilePhysicalPath(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			string text = string.Concat(new object[]
			{
				this.OutputAssemblyPrefix,
				"_",
				this.temp_files.Count,
				".",
				extension
			});
			this.temp_files.AddFile(text, AssemblyBuilder.KeepFiles);
			return text;
		}

		/// <summary>Gets the compiler used to build source code into an assembly.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation used for compiling source code contributed by each build provider into an assembly.</returns>
		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x000B81C9 File Offset: 0x000B63C9
		public CodeDomProvider CodeDomProvider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x000B81D1 File Offset: 0x000B63D1
		private List<Assembly> ReferencedAssemblies
		{
			get
			{
				if (this.referenced_assemblies == null)
				{
					this.referenced_assemblies = new List<Assembly>();
				}
				return this.referenced_assemblies;
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000B81EC File Offset: 0x000B63EC
		private AssemblyBuilder.CodeUnit CheckForPartialTypes(AssemblyBuilder.CodeUnit codeUnit)
		{
			Dictionary<string, List<CompileUnitPartialType>> partialTypes = this.PartialTypes;
			foreach (object obj in codeUnit.Unit.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				if (codeNamespace != null)
				{
					CodeTypeDeclarationCollection types = codeNamespace.Types;
					if (types != null && types.Count != 0)
					{
						foreach (object obj2 in types)
						{
							CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj2;
							if (codeTypeDeclaration != null && codeTypeDeclaration.IsPartial)
							{
								CompileUnitPartialType compileUnitPartialType = new CompileUnitPartialType(codeUnit.Unit, codeNamespace, codeTypeDeclaration);
								string typeName = compileUnitPartialType.TypeName;
								List<CompileUnitPartialType> list;
								if (!partialTypes.TryGetValue(typeName, out list))
								{
									list = new List<CompileUnitPartialType>(1);
									partialTypes.Add(typeName, list);
								}
								list.Add(compileUnitPartialType);
							}
						}
					}
				}
			}
			return codeUnit;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000B8304 File Offset: 0x000B6504
		private void ProcessPartialTypes()
		{
			Dictionary<string, List<CompileUnitPartialType>> partialTypes = this.PartialTypes;
			if (partialTypes.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, List<CompileUnitPartialType>> keyValuePair in partialTypes)
			{
				this.ProcessType(keyValuePair.Value);
			}
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000B8368 File Offset: 0x000B6568
		private void ProcessType(List<CompileUnitPartialType> typeList)
		{
			CompileUnitPartialType[] array = new CompileUnitPartialType[typeList.Count];
			int num = 0;
			foreach (CompileUnitPartialType compileUnitPartialType in typeList)
			{
				if (num == 0)
				{
					array[0] = compileUnitPartialType;
					num++;
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						this.CompareTypes(array[i], compileUnitPartialType);
					}
					array[num++] = compileUnitPartialType;
				}
			}
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000B83F0 File Offset: 0x000B65F0
		private void CompareTypes(CompileUnitPartialType source, CompileUnitPartialType target)
		{
			CodeTypeDeclaration partialType = source.PartialType;
			CodeTypeMemberCollection members = target.PartialType.Members;
			List<CodeTypeMember> list = new List<CodeTypeMember>();
			foreach (object obj in members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (this.TypeHasMember(partialType, codeTypeMember))
				{
					list.Add(codeTypeMember);
				}
			}
			foreach (CodeTypeMember codeTypeMember2 in list)
			{
				members.Remove(codeTypeMember2);
			}
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x000B84B0 File Offset: 0x000B66B0
		private bool TypeHasMember(CodeTypeDeclaration type, CodeTypeMember member)
		{
			return type != null && member != null && this.FindMemberByName(type, member.Name) != null;
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000B84CC File Offset: 0x000B66CC
		private CodeTypeMember FindMemberByName(CodeTypeDeclaration type, string name)
		{
			foreach (object obj in type.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember != null && !(codeTypeMember.Name != name))
				{
					return codeTypeMember;
				}
			}
			return null;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x000B8538 File Offset: 0x000B6738
		internal CompilerResults BuildAssembly()
		{
			return this.BuildAssembly(null, this.CompilerOptions);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000B8547 File Offset: 0x000B6747
		internal CompilerResults BuildAssembly(VirtualPath virtualPath)
		{
			return this.BuildAssembly(virtualPath, this.CompilerOptions);
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000B8556 File Offset: 0x000B6756
		internal CompilerResults BuildAssembly(CompilerParameters options)
		{
			return this.BuildAssembly(null, options);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000B8560 File Offset: 0x000B6760
		internal CompilerResults BuildAssembly(VirtualPath virtualPath, CompilerParameters options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			options.TempFiles = this.temp_files;
			if (options.OutputAssembly == null)
			{
				options.OutputAssembly = this.OutputAssemblyName;
			}
			this.ProcessPartialTypes();
			AssemblyBuilder.CodeUnit[] unitsAsArray = this.GetUnitsAsArray();
			List<string> sourceFiles = this.SourceFiles;
			Dictionary<string, string> resourceFiles = this.ResourceFiles;
			if (unitsAsArray.Length == 0 && sourceFiles.Count == 0 && resourceFiles.Count == 0 && options.EmbeddedResources.Count == 0)
			{
				return null;
			}
			string text = options.CompilerOptions;
			if (options.IncludeDebugInformation)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = "/d:DEBUG";
				}
				else if (text.IndexOf("d:DEBUG", StringComparison.OrdinalIgnoreCase) == -1)
				{
					text += " /d:DEBUG";
				}
				options.CompilerOptions = text;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "/noconfig";
			}
			else if (text.IndexOf("noconfig", StringComparison.OrdinalIgnoreCase) == -1)
			{
				text += " /noconfig";
			}
			options.CompilerOptions = text;
			StreamWriter streamWriter = null;
			foreach (AssemblyBuilder.CodeUnit codeUnit in unitsAsArray)
			{
				string tempFilePhysicalPath = this.GetTempFilePhysicalPath(this.provider.FileExtension);
				try
				{
					streamWriter = new StreamWriter(File.OpenWrite(tempFilePhysicalPath), Encoding.UTF8);
					this.provider.GenerateCodeFromCompileUnit(codeUnit.Unit, streamWriter, null);
					sourceFiles.Add(tempFilePhysicalPath);
				}
				catch
				{
					throw;
				}
				finally
				{
					if (streamWriter != null)
					{
						streamWriter.Flush();
						streamWriter.Close();
					}
				}
				if (codeUnit.BuildProvider != null)
				{
					this.AddPathToBuilderMap(tempFilePhysicalPath, codeUnit.BuildProvider);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair in resourceFiles)
			{
				options.EmbeddedResources.Add(keyValuePair.Value);
			}
			this.AddAssemblyReference(BuildManager.GetReferencedAssemblies());
			List<Assembly> list = new List<Assembly>();
			Dictionary<Guid, bool> dictionary = new Dictionary<Guid, bool>();
			StringCollection referencedAssemblies = options.ReferencedAssemblies;
			this.ReferenceAssemblies(dictionary, list, this.ReferencedAssemblies);
			this.ReferenceAssemblies(dictionary, list, referencedAssemblies);
			Type appType = HttpApplicationFactory.AppType;
			if (appType != null)
			{
				this.ReferenceAssembly(dictionary, list, appType.Assembly);
			}
			referencedAssemblies.Clear();
			foreach (Assembly assembly in list)
			{
				string localPath = new Uri(assembly.CodeBase).LocalPath;
				string location = assembly.Location;
				if (!referencedAssemblies.Contains(localPath) && !referencedAssemblies.Contains(location))
				{
					referencedAssemblies.Add(localPath);
				}
			}
			CompilerResults compilerResults = this.provider.CompileAssemblyFromFile(options, sourceFiles.ToArray());
			if (compilerResults.NativeCompilerReturnValue != 0)
			{
				string text2 = null;
				CompilerErrorCollection errors = compilerResults.Errors;
				try
				{
					if (errors != null && errors.Count > 0)
					{
						using (StreamReader streamReader = File.OpenText(compilerResults.Errors[0].FileName))
						{
							text2 = streamReader.ReadToEnd();
						}
					}
				}
				catch (Exception)
				{
				}
				throw new CompilationException((virtualPath != null) ? virtualPath.Original : string.Empty, compilerResults, text2);
			}
			if (compilerResults.CompiledAssembly == null)
			{
				if (!File.Exists(options.OutputAssembly))
				{
					compilerResults.TempFiles.Delete();
					throw new CompilationException((virtualPath != null) ? virtualPath.Original : string.Empty, compilerResults.Errors, "No assembly returned after compilation!?");
				}
				try
				{
					compilerResults.CompiledAssembly = Assembly.LoadFrom(options.OutputAssembly);
				}
				catch (Exception ex)
				{
					compilerResults.TempFiles.Delete();
					throw new HttpException("Unable to load compiled assembly", ex);
				}
			}
			if (!AssemblyBuilder.KeepFiles)
			{
				compilerResults.TempFiles.Delete();
			}
			return compilerResults;
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000B8960 File Offset: 0x000B6B60
		private void ReferenceAssembly(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, Assembly asm)
		{
			Guid moduleVersionId = asm.ManifestModule.ModuleVersionId;
			if (moduleGuidCache.ContainsKey(moduleVersionId))
			{
				return;
			}
			moduleGuidCache[moduleVersionId] = true;
			assemblies.Add(asm);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x000B8994 File Offset: 0x000B6B94
		private void ReferenceAssemblies(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, List<Assembly> references)
		{
			if (references == null || references.Count == 0)
			{
				return;
			}
			foreach (Assembly assembly in references)
			{
				this.ReferenceAssembly(moduleGuidCache, assemblies, assembly);
			}
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x000B89F0 File Offset: 0x000B6BF0
		private void ReferenceAssemblies(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, StringCollection references)
		{
			if (references == null || references.Count == 0)
			{
				return;
			}
			foreach (string text in references)
			{
				this.ReferenceAssembly(moduleGuidCache, assemblies, text);
			}
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x000B8A50 File Offset: 0x000B6C50
		private void ReferenceAssembly(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, string asmLocation)
		{
			Assembly assembly = Assembly.LoadFrom(asmLocation);
			if (assembly == null)
			{
				return;
			}
			this.ReferenceAssembly(moduleGuidCache, assemblies, assembly);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal AssemblyBuilder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002461 RID: 9313
		private const string DEFAULT_ASSEMBLY_BASE_NAME = "App_Web_";

		// Token: 0x04002462 RID: 9314
		private const int COPY_BUFFER_SIZE = 8192;

		// Token: 0x04002463 RID: 9315
		private static bool KeepFiles = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;

		// Token: 0x04002464 RID: 9316
		private CodeDomProvider provider;

		// Token: 0x04002465 RID: 9317
		private CompilerParameters parameters;

		// Token: 0x04002466 RID: 9318
		private Dictionary<string, bool> code_files;

		// Token: 0x04002467 RID: 9319
		private Dictionary<string, List<CompileUnitPartialType>> partial_types;

		// Token: 0x04002468 RID: 9320
		private Dictionary<string, BuildProvider> path_to_buildprovider;

		// Token: 0x04002469 RID: 9321
		private List<AssemblyBuilder.CodeUnit> units;

		// Token: 0x0400246A RID: 9322
		private List<string> source_files;

		// Token: 0x0400246B RID: 9323
		private List<Assembly> referenced_assemblies;

		// Token: 0x0400246C RID: 9324
		private Dictionary<string, string> resource_files;

		// Token: 0x0400246D RID: 9325
		private TempFileCollection temp_files;

		// Token: 0x0400246E RID: 9326
		private string outputFilesPrefix;

		// Token: 0x0400246F RID: 9327
		private string outputAssemblyPrefix;

		// Token: 0x04002470 RID: 9328
		private string outputAssemblyName;

		// Token: 0x02000633 RID: 1587
		private struct CodeUnit
		{
			// Token: 0x06004421 RID: 17441 RVA: 0x000B8A8B File Offset: 0x000B6C8B
			public CodeUnit(BuildProvider bp, CodeCompileUnit unit)
			{
				this.BuildProvider = bp;
				this.Unit = unit;
			}

			// Token: 0x04002471 RID: 9329
			public readonly BuildProvider BuildProvider;

			// Token: 0x04002472 RID: 9330
			public readonly CodeCompileUnit Unit;
		}

		// Token: 0x02000634 RID: 1588
		private interface ICodePragmaGenerator
		{
			// Token: 0x06004422 RID: 17442
			int ReserveSpace(string filename);

			// Token: 0x06004423 RID: 17443
			void DecorateFile(string path, string filename, MD5 checksum, Encoding enc);
		}

		// Token: 0x02000635 RID: 1589
		private class CSharpCodePragmaGenerator : AssemblyBuilder.ICodePragmaGenerator
		{
			// Token: 0x06004424 RID: 17444 RVA: 0x000B8A9C File Offset: 0x000B6C9C
			private string QuoteSnippetString(string value)
			{
				string text = value.Replace("\\", "\\\\");
				text = text.Replace("\"", "\\\"");
				text = text.Replace("\t", "\\t");
				text = text.Replace("\r", "\\r");
				text = text.Replace("\n", "\\n");
				return "\"" + text + "\"";
			}

			// Token: 0x06004425 RID: 17445 RVA: 0x000B8B10 File Offset: 0x000B6D10
			private string ChecksumToHex(MD5 checksum)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (byte b in checksum.Hash)
				{
					stringBuilder.Append(b.ToString("X2"));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06004426 RID: 17446 RVA: 0x000B8B55 File Offset: 0x000B6D55
			public int ReserveSpace(string filename)
			{
				return 63 + this.QuoteSnippetString(filename).Length * 2 + Environment.NewLine.Length * 3 + BaseCompiler.HashMD5.ToString("B").Length;
			}

			// Token: 0x06004427 RID: 17447 RVA: 0x000B8B8C File Offset: 0x000B6D8C
			public void DecorateFile(string path, string filename, MD5 checksum, Encoding enc)
			{
				string newLine = Environment.NewLine;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("#pragma checksum {0} \"{1}\" \"{2}\"{3}{3}", new object[]
				{
					this.QuoteSnippetString(filename),
					BaseCompiler.HashMD5.ToString("B"),
					this.ChecksumToHex(checksum),
					newLine
				});
				stringBuilder.AppendFormat("#line 1 {0}{1}", this.QuoteSnippetString(filename), newLine);
				byte[] array = enc.GetBytes(stringBuilder.ToString());
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write))
				{
					fileStream.Seek((long)enc.GetPreamble().Length, SeekOrigin.Begin);
					fileStream.Write(array, 0, array.Length);
					stringBuilder.Length = 0;
					stringBuilder.AppendFormat("{0}#line default{0}#line hidden{0}", newLine);
					array = Encoding.UTF8.GetBytes(stringBuilder.ToString());
					fileStream.Seek(0L, SeekOrigin.End);
					fileStream.Write(array, 0, array.Length);
				}
			}

			// Token: 0x04002473 RID: 9331
			private const int pragmaChecksumStaticCount = 23;

			// Token: 0x04002474 RID: 9332
			private const int pragmaLineStaticCount = 8;

			// Token: 0x04002475 RID: 9333
			private const int md5ChecksumCount = 32;
		}

		// Token: 0x02000636 RID: 1590
		private class VBCodePragmaGenerator : AssemblyBuilder.ICodePragmaGenerator
		{
			// Token: 0x06004429 RID: 17449 RVA: 0x000B8C88 File Offset: 0x000B6E88
			public int ReserveSpace(string filename)
			{
				return 21 + filename.Length + Environment.NewLine.Length;
			}

			// Token: 0x0600442A RID: 17450 RVA: 0x000B8CA0 File Offset: 0x000B6EA0
			public void DecorateFile(string path, string filename, MD5 checksum, Encoding enc)
			{
				string newLine = Environment.NewLine;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("#ExternalSource(\"{0}\",1){1}", filename, newLine);
				byte[] array = enc.GetBytes(stringBuilder.ToString());
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write))
				{
					fileStream.Seek((long)enc.GetPreamble().Length, SeekOrigin.Begin);
					fileStream.Write(array, 0, array.Length);
					stringBuilder.Length = 0;
					stringBuilder.AppendFormat("{0}#End ExternalSource{0}", newLine);
					array = enc.GetBytes(stringBuilder.ToString());
					fileStream.Seek(0L, SeekOrigin.End);
					fileStream.Write(array, 0, array.Length);
				}
			}

			// Token: 0x04002476 RID: 9334
			private const int pragmaExternalSourceCount = 21;
		}
	}
}
