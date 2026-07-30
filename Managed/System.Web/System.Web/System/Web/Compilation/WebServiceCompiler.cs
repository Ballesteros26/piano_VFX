using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000676 RID: 1654
	internal class WebServiceCompiler : BaseCompiler
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x000C69CC File Offset: 0x000C4BCC
		public WebServiceCompiler(SimpleWebHandlerParser wService)
			: base(null)
		{
			this.parser = wService;
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000C69DC File Offset: 0x000C4BDC
		public static Type CompileIntoType(SimpleWebHandlerParser wService)
		{
			return new WebServiceCompiler(wService).GetCompiledType();
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x000C69EC File Offset: 0x000C4BEC
		public override Type GetCompiledType()
		{
			Type type = CachingCompiler.GetTypeFromCache(this.parser.PhysicalPath);
			if (type != null)
			{
				return type;
			}
			if (this.parser.Program.Trim() == "")
			{
				type = Type.GetType(this.parser.ClassName, false);
				if (type == null)
				{
					type = this.parser.GetTypeFromBin(this.parser.ClassName);
				}
				CachingCompiler.InsertTypeFileDep(type, this.parser.PhysicalPath);
				return type;
			}
			string language = this.parser.Language;
			string text;
			int num;
			string text2;
			CodeDomProvider codeDomProvider = (base.Provider = BaseCompiler.CreateProvider(this.parser.Context, language, out text, out num, out text2));
			if (base.Provider == null)
			{
				throw new HttpException("Configuration error. Language not supported: " + language, 500);
			}
			CompilerParameters compilerParameters = (base.CompilerParameters = CachingCompiler.GetOptions(this.parser.Assemblies));
			compilerParameters.IncludeDebugInformation = this.parser.Debug;
			compilerParameters.CompilerOptions = text;
			compilerParameters.WarningLevel = num;
			bool flag = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;
			TempFileCollection tempFileCollection = new TempFileCollection(text2, flag);
			compilerParameters.TempFiles = tempFileCollection;
			this.inputFile = tempFileCollection.AddExtension(codeDomProvider.FileExtension);
			StreamWriter streamWriter = new StreamWriter(File.OpenWrite(this.inputFile));
			streamWriter.WriteLine(this.parser.Program);
			streamWriter.Close();
			string fileName = Path.GetFileName(tempFileCollection.AddExtension("dll", true));
			compilerParameters.OutputAssembly = Path.Combine(base.DynamicDir(), fileName);
			CompilerResults compilerResults = CachingCompiler.Compile(this);
			this.CheckCompilerErrors(compilerResults);
			Assembly assembly = compilerResults.CompiledAssembly;
			if (assembly == null)
			{
				if (!File.Exists(compilerParameters.OutputAssembly))
				{
					throw new CompilationException(this.inputFile, compilerResults.Errors, "No assembly returned after compilation!?");
				}
				assembly = Assembly.LoadFrom(compilerParameters.OutputAssembly);
			}
			compilerResults.TempFiles.Delete();
			type = assembly.GetType(this.parser.ClassName, true);
			CachingCompiler.InsertTypeFileDep(type, this.parser.PhysicalPath);
			return type;
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x000C6C09 File Offset: 0x000C4E09
		private void CheckCompilerErrors(CompilerResults results)
		{
			if (results.NativeCompilerReturnValue == 0)
			{
				return;
			}
			throw new CompilationException(this.parser.PhysicalPath, results.Errors, this.parser.Program);
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x000C6C35 File Offset: 0x000C4E35
		internal new SimpleWebHandlerParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x000C6C3D File Offset: 0x000C4E3D
		internal string InputFile
		{
			get
			{
				return this.inputFile;
			}
		}

		// Token: 0x04002553 RID: 9555
		private SimpleWebHandlerParser parser;

		// Token: 0x04002554 RID: 9556
		private string inputFile;
	}
}
