using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200060D RID: 1549
	internal class AppCodeAssembly
	{
		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x000AFEB4 File Offset: 0x000AE0B4
		public string OutputAssemblyName
		{
			get
			{
				return this.outputAssemblyName;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x000AFEBC File Offset: 0x000AE0BC
		public bool IsValid
		{
			get
			{
				return this.validAssembly;
			}
		}

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000AFEC4 File Offset: 0x000AE0C4
		public string SourcePath
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x000AFECC File Offset: 0x000AE0CC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000AFED4 File Offset: 0x000AE0D4
		public List<string> Files
		{
			get
			{
				return this.files;
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x000AFEDC File Offset: 0x000AE0DC
		public AppCodeAssembly(string name, string path)
		{
			this.files = new List<string>();
			this.units = new List<CodeCompileUnit>();
			this.validAssembly = true;
			this.name = name;
			this.path = path;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000AFF0F File Offset: 0x000AE10F
		public void AddFile(string path)
		{
			this.files.Add(path);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000AFF1D File Offset: 0x000AE11D
		public void AddUnit(CodeCompileUnit unit)
		{
			this.units.Add(unit);
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x000AFF2B File Offset: 0x000AE12B
		private object OnCreateTemporaryAssemblyFile(string path)
		{
			new FileStream(path, FileMode.CreateNew).Close();
			return path;
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000AFF3C File Offset: 0x000AE13C
		public void Build(string[] binAssemblies)
		{
			Type type = null;
			CompilerInfo compilerInfo = null;
			string text = null;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			foreach (string text2 in this.files)
			{
				bool flag = true;
				string text3 = null;
				string extension = Path.GetExtension(text2);
				if (string.IsNullOrEmpty(extension) || !CodeDomProvider.IsDefinedExtension(extension))
				{
					flag = false;
				}
				if (flag)
				{
					text3 = CodeDomProvider.GetLanguageFromExtension(extension);
					if (!CodeDomProvider.IsDefinedLanguage(text3))
					{
						flag = false;
					}
				}
				if (!flag || text3 == null)
				{
					list2.Add(text2);
				}
				else
				{
					CompilerInfo compilerInfo2 = CodeDomProvider.GetCompilerInfo(text3);
					if (compilerInfo2 != null && compilerInfo2.IsCodeDomProviderTypeValid)
					{
						if (type == null)
						{
							text = text2;
							type = compilerInfo2.CodeDomProviderType;
							compilerInfo = compilerInfo2;
						}
						else if (type != compilerInfo2.CodeDomProviderType)
						{
							throw new HttpException(string.Format("Files {0} and {1} are in different languages - they cannot be compiled into the same assembly", Path.GetFileName(text), Path.GetFileName(text2)));
						}
						list.Add(text2);
					}
				}
			}
			CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			if (compilerInfo == null)
			{
				if (!CodeDomProvider.IsDefinedLanguage(compilationSection.DefaultLanguage))
				{
					throw new HttpException("Failed to retrieve default source language");
				}
				compilerInfo = CodeDomProvider.GetCompilerInfo(compilationSection.DefaultLanguage);
				if (compilerInfo == null || !compilerInfo.IsCodeDomProviderTypeValid)
				{
					throw new HttpException("Internal error while initializing application");
				}
			}
			CodeDomProvider codeDomProvider = compilerInfo.CreateProvider();
			if (codeDomProvider == null)
			{
				throw new HttpException("A code provider error occurred while initializing application.");
			}
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(codeDomProvider);
			foreach (string text4 in list)
			{
				assemblyBuilder.AddCodeFile(text4);
			}
			foreach (CodeCompileUnit codeCompileUnit in this.units)
			{
				assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
			}
			CompilerParameters compilerParameters = compilerInfo.CreateDefaultCompilerParameters();
			compilerParameters.IncludeDebugInformation = compilationSection.Debug;
			if (binAssemblies != null && binAssemblies.Length != 0)
			{
				StringCollection referencedAssemblies = compilerParameters.ReferencedAssemblies;
				foreach (string text5 in binAssemblies)
				{
					if (!referencedAssemblies.Contains(text5))
					{
						referencedAssemblies.Add(text5);
					}
				}
			}
			if (compilationSection != null)
			{
				foreach (object obj in compilationSection.Assemblies)
				{
					AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
					if (assemblyInfo.Assembly != "*")
					{
						try
						{
							compilerParameters.ReferencedAssemblies.Add(AssemblyPathResolver.GetAssemblyPath(assemblyInfo.Assembly));
						}
						catch (Exception ex)
						{
							throw new HttpException(string.Format("Could not find assembly {0}.", assemblyInfo.Assembly), ex);
						}
					}
				}
				BuildProviderCollection buildProviders = compilationSection.BuildProviders;
				foreach (string text6 in list2)
				{
					BuildProvider buildProviderFor = this.GetBuildProviderFor(text6, buildProviders);
					if (buildProviderFor != null)
					{
						buildProviderFor.GenerateCode(assemblyBuilder);
					}
				}
			}
			if (list.Count == 0 && list2.Count == 0 && this.units.Count == 0)
			{
				return;
			}
			this.outputAssemblyName = (string)FileUtils.CreateTemporaryFile(AppDomain.CurrentDomain.SetupInformation.DynamicBase, this.name, "dll", new FileUtils.CreateTempFile(this.OnCreateTemporaryAssemblyFile));
			compilerParameters.OutputAssembly = this.outputAssemblyName;
			foreach (object obj2 in BuildManager.TopLevelAssemblies)
			{
				Assembly assembly = (Assembly)obj2;
				compilerParameters.ReferencedAssemblies.Add(assembly.Location);
			}
			CompilerResults compilerResults = assemblyBuilder.BuildAssembly(compilerParameters);
			if (compilerResults == null)
			{
				return;
			}
			if (compilerResults.NativeCompilerReturnValue == 0)
			{
				BuildManager.CodeAssemblies.Add(compilerResults.CompiledAssembly);
				BuildManager.TopLevelAssemblies.Add(compilerResults.CompiledAssembly);
				HttpRuntime.WritePreservationFile(compilerResults.CompiledAssembly, this.name);
				return;
			}
			if (HttpContext.Current.IsCustomErrorEnabled)
			{
				throw new HttpException("An error occurred while initializing application.");
			}
			throw new CompilationException(null, compilerResults.Errors, null);
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000B03D4 File Offset: 0x000AE5D4
		private VirtualPath PhysicalToVirtual(string file)
		{
			return new VirtualPath(file.Replace(HttpRuntime.AppDomainAppPath, "~/").Replace(Path.DirectorySeparatorChar, '/'));
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000B03F8 File Offset: 0x000AE5F8
		private BuildProvider GetBuildProviderFor(string file, BuildProviderCollection buildProviders)
		{
			if (file == null || file.Length == 0 || buildProviders == null || buildProviders.Count == 0)
			{
				return null;
			}
			BuildProvider providerInstanceForExtension = buildProviders.GetProviderInstanceForExtension(Path.GetExtension(file));
			if (providerInstanceForExtension != null && this.IsCorrectBuilderType(providerInstanceForExtension))
			{
				providerInstanceForExtension.SetVirtualPath(this.PhysicalToVirtual(file));
				return providerInstanceForExtension;
			}
			return null;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x000B0448 File Offset: 0x000AE648
		private bool IsCorrectBuilderType(BuildProvider bp)
		{
			if (bp == null)
			{
				return false;
			}
			object[] customAttributes = bp.GetType().GetCustomAttributes(true);
			if (customAttributes == null)
			{
				return false;
			}
			bool flag = false;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				BuildProviderAppliesToAttribute buildProviderAppliesToAttribute = array[i] as BuildProviderAppliesToAttribute;
				if (buildProviderAppliesToAttribute != null)
				{
					flag = true;
					if ((buildProviderAppliesToAttribute.AppliesTo & BuildProviderAppliesTo.All) == BuildProviderAppliesTo.All || (buildProviderAppliesToAttribute.AppliesTo & BuildProviderAppliesTo.Code) == BuildProviderAppliesTo.Code)
					{
						return true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x040023BF RID: 9151
		private List<string> files;

		// Token: 0x040023C0 RID: 9152
		private List<CodeCompileUnit> units;

		// Token: 0x040023C1 RID: 9153
		private string name;

		// Token: 0x040023C2 RID: 9154
		private string path;

		// Token: 0x040023C3 RID: 9155
		private bool validAssembly;

		// Token: 0x040023C4 RID: 9156
		private string outputAssemblyName;
	}
}
