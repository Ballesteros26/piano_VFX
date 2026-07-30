using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.Compilation
{
	// Token: 0x02000618 RID: 1560
	internal class AppWebReferencesCompiler
	{
		// Token: 0x0600431F RID: 17183 RVA: 0x000B3048 File Offset: 0x000B1248
		public void Compile()
		{
			string text = Path.Combine(HttpRuntime.AppDomainAppPath, "App_WebReferences");
			if (!Directory.Exists(text))
			{
				return;
			}
			string[] files = Directory.GetFiles(text, "*.wsdl", SearchOption.AllDirectories);
			if (files == null || files.Length == 0)
			{
				return;
			}
			CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			if (compilationSection == null)
			{
				throw new HttpException("Unable to determine default compilation language.");
			}
			CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(compilationSection.DefaultLanguage, compilationSection);
			CodeDomProvider codeDomProvider = null;
			Exception ex = null;
			try
			{
				codeDomProvider = Activator.CreateInstance(defaultCompilerTypeForLanguage.CodeDomProviderType) as CodeDomProvider;
			}
			catch (Exception ex)
			{
			}
			if (codeDomProvider == null)
			{
				throw new HttpException("Unable to instantiate default compilation language provider.", ex);
			}
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(codeDomProvider, "App_WebReferences_");
			assemblyBuilder.CompilerOptions = defaultCompilerTypeForLanguage.CompilerParameters;
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				VirtualPath virtualPath = VirtualPath.PhysicalToVirtual(array[i]);
				if (virtualPath != null)
				{
					WsdlBuildProvider wsdlBuildProvider = new WsdlBuildProvider();
					wsdlBuildProvider.SetVirtualPath(virtualPath);
					wsdlBuildProvider.GenerateCode(assemblyBuilder);
				}
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = assemblyBuilder.BuildAssembly();
			}
			catch (CompilationException ex2)
			{
				throw new HttpException("Failed to compile web references.", ex2);
			}
			if (compilerResults == null)
			{
				return;
			}
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			BuildManager.TopLevelAssemblies.Add(compiledAssembly);
		}

		// Token: 0x040023E9 RID: 9193
		private const string ResourcesDirName = "App_WebReferences";
	}
}
