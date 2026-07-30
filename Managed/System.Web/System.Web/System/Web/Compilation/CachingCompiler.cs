using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;

namespace System.Web.Compilation
{
	// Token: 0x02000648 RID: 1608
	internal class CachingCompiler
	{
		// Token: 0x0600450F RID: 17679 RVA: 0x000BCF7C File Offset: 0x000BB17C
		public static void InsertTypeFileDep(Type type, string filename)
		{
			CacheDependency cacheDependency = new CacheDependency(filename);
			HttpRuntime.InternalCache.Insert("@@@Type" + filename, type, cacheDependency);
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000BCFA8 File Offset: 0x000BB1A8
		public static void InsertType(Type type, string filename)
		{
			string[] array = new string[] { "@@Assembly" + filename };
			CacheDependency cacheDependency = new CacheDependency(null, array);
			HttpRuntime.InternalCache.Insert("@@@Type" + filename, type, cacheDependency);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000BCFE9 File Offset: 0x000BB1E9
		public static Type GetTypeFromCache(string filename)
		{
			return (Type)HttpRuntime.InternalCache["@@@Type" + filename];
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x000BD008 File Offset: 0x000BB208
		public static CompilerResults Compile(BaseCompiler compiler)
		{
			Cache internalCache = HttpRuntime.InternalCache;
			string text = "@@Assembly" + compiler.Parser.InputFile;
			CompilerResults compilerResults = (CompilerResults)internalCache[text];
			if (!compiler.IsRebuildingPartial && compilerResults != null)
			{
				return compilerResults;
			}
			object obj;
			bool flag = CachingCompiler.AcquireCompilationTicket(text, out obj);
			try
			{
				Monitor.Enter(obj);
				compilerResults = (CompilerResults)internalCache[text];
				if (!compiler.IsRebuildingPartial && compilerResults != null)
				{
					return compilerResults;
				}
				CodeDomProvider provider = compiler.Provider;
				CompilerParameters compilerParameters = compiler.CompilerParameters;
				CachingCompiler.GetExtraAssemblies(compilerParameters);
				compilerResults = provider.CompileAssemblyFromDom(compilerParameters, new CodeCompileUnit[] { compiler.CompileUnit });
				List<string> dependencies = compiler.Parser.Dependencies;
				if (dependencies != null && dependencies.Count > 0)
				{
					string[] array = dependencies.ToArray();
					HttpContext httpContext = HttpContext.Current;
					HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
					if (httpRequest == null)
					{
						throw new HttpException("No current context, cannot compile.");
					}
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = httpRequest.MapPath(array[i]);
					}
					internalCache.Insert(text, compilerResults, new CacheDependency(array));
				}
			}
			finally
			{
				Monitor.Exit(obj);
				if (flag)
				{
					CachingCompiler.ReleaseCompilationTicket(text);
				}
			}
			return compilerResults;
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x000BD14C File Offset: 0x000BB34C
		public static CompilerResults Compile(WebServiceCompiler compiler)
		{
			string text = "@@Assembly" + compiler.Parser.PhysicalPath;
			Cache internalCache = HttpRuntime.InternalCache;
			CompilerResults compilerResults = (CompilerResults)internalCache[text];
			if (compilerResults != null)
			{
				return compilerResults;
			}
			object obj;
			bool flag = CachingCompiler.AcquireCompilationTicket(text, out obj);
			try
			{
				Monitor.Enter(obj);
				compilerResults = (CompilerResults)internalCache[text];
				if (compilerResults != null)
				{
					return compilerResults;
				}
				CodeDomProvider provider = compiler.Provider;
				CompilerParameters compilerParameters = compiler.CompilerParameters;
				CachingCompiler.GetExtraAssemblies(compilerParameters);
				compilerResults = provider.CompileAssemblyFromFile(compilerParameters, new string[] { compiler.InputFile });
				string[] array = (string[])compiler.Parser.Dependencies.ToArray(typeof(string));
				internalCache.Insert(text, compilerResults, new CacheDependency(array));
			}
			finally
			{
				Monitor.Exit(obj);
				if (flag)
				{
					CachingCompiler.ReleaseCompilationTicket(text);
				}
			}
			return compilerResults;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000BD230 File Offset: 0x000BB430
		internal static CompilerParameters GetOptions(ICollection assemblies)
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			if (assemblies != null)
			{
				StringCollection referencedAssemblies = compilerParameters.ReferencedAssemblies;
				foreach (object obj in assemblies)
				{
					string text = (string)obj;
					referencedAssemblies.Add(text);
				}
			}
			CachingCompiler.GetExtraAssemblies(compilerParameters);
			return compilerParameters;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000BD2A0 File Offset: 0x000BB4A0
		public static CompilerResults Compile(string language, string key, string file, ArrayList assemblies)
		{
			return CachingCompiler.Compile(language, key, file, assemblies, false);
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000BD2AC File Offset: 0x000BB4AC
		public static CompilerResults Compile(string language, string key, string file, ArrayList assemblies, bool debug)
		{
			Cache internalCache = HttpRuntime.InternalCache;
			CompilerResults compilerResults = (CompilerResults)internalCache["@@Assembly" + key];
			if (compilerResults != null)
			{
				return compilerResults;
			}
			if (!Directory.Exists(CachingCompiler.dynamicBase))
			{
				Directory.CreateDirectory(CachingCompiler.dynamicBase);
			}
			object obj;
			bool flag = CachingCompiler.AcquireCompilationTicket("@@Assembly" + key, out obj);
			try
			{
				Monitor.Enter(obj);
				compilerResults = (CompilerResults)internalCache["@@Assembly" + key];
				if (compilerResults != null)
				{
					return compilerResults;
				}
				string text;
				int num;
				string text2;
				CodeDomProvider codeDomProvider = BaseCompiler.CreateProvider(language, out text, out num, out text2);
				if (codeDomProvider == null)
				{
					throw new HttpException("Configuration error. Language not supported: " + language, 500);
				}
				CompilerParameters options = CachingCompiler.GetOptions(assemblies);
				options.IncludeDebugInformation = debug;
				options.WarningLevel = num;
				options.CompilerOptions = text;
				string fileName = Path.GetFileName(new TempFileCollection(text2, true).AddExtension("dll", true));
				options.OutputAssembly = Path.Combine(CachingCompiler.dynamicBase, fileName);
				compilerResults = codeDomProvider.CompileAssemblyFromFile(options, new string[] { file });
				ArrayList arrayList = new ArrayList(assemblies.Count + 1);
				arrayList.Add(file);
				for (int i = assemblies.Count - 1; i >= 0; i--)
				{
					string text3 = (string)assemblies[i];
					if (Path.IsPathRooted(text3))
					{
						arrayList.Add(text3);
					}
				}
				string[] array = (string[])arrayList.ToArray(typeof(string));
				internalCache.Insert("@@Assembly" + key, compilerResults, new CacheDependency(array));
			}
			finally
			{
				Monitor.Exit(obj);
				if (flag)
				{
					CachingCompiler.ReleaseCompilationTicket("@@Assembly" + key);
				}
			}
			return compilerResults;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000BD474 File Offset: 0x000BB674
		public static Type CompileAndGetType(string typename, string language, string key, string file, ArrayList assemblies)
		{
			CompilerResults compilerResults = CachingCompiler.Compile(language, key, file, assemblies);
			if (compilerResults.NativeCompilerReturnValue != 0)
			{
				using (StreamReader streamReader = new StreamReader(file))
				{
					throw new CompilationException(file, compilerResults.Errors, streamReader.ReadToEnd());
				}
			}
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			if (compiledAssembly == null)
			{
				using (StreamReader streamReader2 = new StreamReader(file))
				{
					throw new CompilationException(file, compilerResults.Errors, streamReader2.ReadToEnd());
				}
			}
			Type type = compiledAssembly.GetType(typename, true);
			CachingCompiler.InsertType(type, file);
			return type;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000BD518 File Offset: 0x000BB718
		private static void GetExtraAssemblies(CompilerParameters options)
		{
			StringCollection referencedAssemblies = options.ReferencedAssemblies;
			ArrayList extraAssemblies = WebConfigurationManager.ExtraAssemblies;
			if (extraAssemblies != null && extraAssemblies.Count > 0)
			{
				foreach (object obj in extraAssemblies)
				{
					string text = obj as string;
					if (text != null && !referencedAssemblies.Contains(text))
					{
						referencedAssemblies.Add(text);
					}
				}
			}
			IList list = BuildManager.CodeAssemblies;
			if (list != null && list.Count > 0)
			{
				foreach (object obj2 in list)
				{
					Assembly assembly = obj2 as Assembly;
					if (!(assembly == null))
					{
						string text = assembly.Location;
						if (text != null && !referencedAssemblies.Contains(text))
						{
							referencedAssemblies.Add(text);
						}
					}
				}
			}
			list = BuildManager.TopLevelAssemblies;
			if (list != null && list.Count > 0)
			{
				foreach (object obj3 in list)
				{
					Assembly assembly = obj3 as Assembly;
					if (obj3 != null)
					{
						string text = assembly.Location;
						if (!referencedAssemblies.Contains(text))
						{
							referencedAssemblies.Add(text);
						}
					}
				}
			}
			CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			AssemblyCollection assemblyCollection = ((compilationSection != null) ? compilationSection.Assemblies : null);
			if (assemblyCollection == null)
			{
				return;
			}
			foreach (object obj4 in assemblyCollection)
			{
				string assemblyLocationFromName = CachingCompiler.GetAssemblyLocationFromName(((AssemblyInfo)obj4).Assembly);
				if (assemblyLocationFromName != null && !referencedAssemblies.Contains(assemblyLocationFromName))
				{
					referencedAssemblies.Add(assemblyLocationFromName);
				}
			}
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000BD718 File Offset: 0x000BB918
		private static string GetAssemblyLocationFromName(string name)
		{
			Assembly assembly = CachingCompiler.assemblyCache[name] as Assembly;
			if (assembly != null)
			{
				return assembly.Location;
			}
			try
			{
				assembly = Assembly.Load(name);
			}
			catch
			{
			}
			if (assembly == null)
			{
				return null;
			}
			CachingCompiler.assemblyCache[name] = assembly;
			return assembly.Location;
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000BD780 File Offset: 0x000BB980
		private static bool AcquireCompilationTicket(string key, out object ticket)
		{
			object syncRoot = CachingCompiler.compilationTickets.SyncRoot;
			lock (syncRoot)
			{
				ticket = CachingCompiler.compilationTickets[key];
				if (ticket == null)
				{
					ticket = new object();
					CachingCompiler.compilationTickets[key] = ticket;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x000BD7EC File Offset: 0x000BB9EC
		private static void ReleaseCompilationTicket(string key)
		{
			object syncRoot = CachingCompiler.compilationTickets.SyncRoot;
			lock (syncRoot)
			{
				CachingCompiler.compilationTickets.Remove(key);
			}
		}

		// Token: 0x040024CB RID: 9419
		private static string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;

		// Token: 0x040024CC RID: 9420
		private static Hashtable compilationTickets = new Hashtable();

		// Token: 0x040024CD RID: 9421
		private const string cachePrefix = "@@Assembly";

		// Token: 0x040024CE RID: 9422
		private const string cacheTypePrefix = "@@@Type";

		// Token: 0x040024CF RID: 9423
		private static Hashtable assemblyCache = new Hashtable();
	}
}
