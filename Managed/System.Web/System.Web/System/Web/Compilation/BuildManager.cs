using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Provides a set of methods to help manage the compilation of an ASP.NET application.</summary>
	// Token: 0x02000639 RID: 1593
	public sealed class BuildManager
	{
		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x000B9FC5 File Offset: 0x000B81C5
		// (set) Token: 0x06004463 RID: 17507 RVA: 0x000B9FCC File Offset: 0x000B81CC
		internal static bool AllowReferencedAssembliesCaching
		{
			get
			{
				return BuildManager.allowReferencedAssembliesCaching;
			}
			set
			{
				BuildManager.allowReferencedAssembliesCaching = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x000B9FD4 File Offset: 0x000B81D4
		internal static bool IsPrecompiled
		{
			get
			{
				return BuildManager.is_precompiled;
			}
		}

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x06004465 RID: 17509 RVA: 0x000B9FDB File Offset: 0x000B81DB
		// (remove) Token: 0x06004466 RID: 17510 RVA: 0x000B9FED File Offset: 0x000B81ED
		internal static event BuildManagerRemoveEntryEventHandler RemoveEntry
		{
			add
			{
				BuildManager.events.AddHandler(BuildManager.buildManagerRemoveEntryEvent, value);
			}
			remove
			{
				BuildManager.events.RemoveHandler(BuildManager.buildManagerRemoveEntryEvent, value);
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000B9FFF File Offset: 0x000B81FF
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x000BA006 File Offset: 0x000B8206
		internal static bool CompilingTopLevelAssemblies { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x000BA00E File Offset: 0x000B820E
		internal static bool PreStartMethodsRunning
		{
			get
			{
				return BuildManager.preStartMethodsRunning;
			}
		}

		/// <summary>Gets or sets a value that indicates whether batch compilation is enabled.</summary>
		/// <returns>true if batch compilation is always enabled, false if batch compilation is never enabled, or null if the compilation setting is determined from the configuration file. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property was not set in the PreApplicationStart method.</exception>
		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x000BA015 File Offset: 0x000B8215
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x000BA01C File Offset: 0x000B821C
		public static bool? BatchCompilationEnabled
		{
			get
			{
				return BuildManager.batchCompilationEnabled;
			}
			set
			{
				if (BuildManager.preStartMethodsDone)
				{
					throw new InvalidOperationException("This method cannot be called after the application's pre-start initialization stage.");
				}
				BuildManager.batchCompilationEnabled = value;
			}
		}

		/// <summary>Gets the target version of the .NET Framework for the current Web site.</summary>
		/// <returns>The target version of the .NET Framework for the current Web site.</returns>
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x000BA038 File Offset: 0x000B8238
		public static FrameworkName TargetFramework
		{
			get
			{
				if (BuildManager.targetFramework == null)
				{
					CompilationSection compilationConfig = BuildManager.CompilationConfig;
					string text;
					if (compilationConfig == null)
					{
						text = null;
					}
					else
					{
						text = compilationConfig.TargetFramework;
					}
					if (string.IsNullOrEmpty(text))
					{
						BuildManager.targetFramework = new FrameworkName(".NETFramework,Version=v4.0");
					}
					else
					{
						BuildManager.targetFramework = new FrameworkName(text);
					}
				}
				return BuildManager.targetFramework;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x000BA090 File Offset: 0x000B8290
		internal static bool BatchMode
		{
			get
			{
				if (BuildManager.batchCompilationEnabled != null)
				{
					return BuildManager.batchCompilationEnabled.Value;
				}
				if (!BuildManager.hosted)
				{
					return false;
				}
				CompilationSection compilationConfig = BuildManager.CompilationConfig;
				return compilationConfig == null || compilationConfig.Batch;
			}
		}

		/// <summary>Gets a list of assemblies built from the App_Code directory.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> collection that contains the assemblies built from the App_Code directory.</returns>
		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x000BA0CE File Offset: 0x000B82CE
		public static IList CodeAssemblies
		{
			get
			{
				return BuildManager.AppCode_Assemblies;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000BA0D5 File Offset: 0x000B82D5
		internal static CompilationSection CompilationConfig
		{
			get
			{
				return WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x000BA0E6 File Offset: 0x000B82E6
		// (set) Token: 0x06004471 RID: 17521 RVA: 0x000BA0ED File Offset: 0x000B82ED
		internal static bool HaveResources { get; set; }

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x000BA0F5 File Offset: 0x000B82F5
		internal static IList TopLevelAssemblies
		{
			get
			{
				return BuildManager.TopLevel_Assemblies;
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000BA0FC File Offset: 0x000B82FC
		static BuildManager()
		{
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			string text = null;
			BuildManager.is_precompiled = !string.IsNullOrEmpty(appDomainAppPath) && File.Exists(text = Path.Combine(appDomainAppPath, "PrecompiledApp.config"));
			if (BuildManager.is_precompiled)
			{
				BuildManager.is_precompiled = BuildManager.LoadPrecompilationInfo(text);
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000BA1ED File Offset: 0x000B83ED
		internal static void AssertPreStartMethodsRunning()
		{
			if (!BuildManager.PreStartMethodsRunning)
			{
				throw new InvalidOperationException("This method must be called during the application's pre-start initialization stage.");
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000BA204 File Offset: 0x000B8404
		private static void FixVirtualPaths()
		{
			if (BuildManager.precompiled == null)
			{
				return;
			}
			int num = -1;
			string text = VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath);
			foreach (string text2 in BuildManager.precompiled.Keys)
			{
				string[] array = text2.Split(new char[] { '/' });
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						VirtualPath absoluteVirtualPath = BuildManager.GetAbsoluteVirtualPath(text + string.Join("/", array, i, array.Length - i));
						if (absoluteVirtualPath != null && File.Exists(absoluteVirtualPath.PhysicalPath))
						{
							num = i - 1;
							break;
						}
					}
				}
			}
			string text3 = HttpRuntime.AppDomainAppVirtualPath;
			if (num == -1 || (num == 0 && text3 == "/"))
			{
				return;
			}
			if (!text3.EndsWith("/"))
			{
				text3 += "/";
			}
			Dictionary<string, BuildManager.PreCompilationData> dictionary = new Dictionary<string, BuildManager.PreCompilationData>(BuildManager.precompiled);
			BuildManager.precompiled.Clear();
			foreach (KeyValuePair<string, BuildManager.PreCompilationData> keyValuePair in dictionary)
			{
				string[] array = keyValuePair.Key.Split(new char[] { '/' });
				string text4;
				if (string.IsNullOrEmpty(array[0]))
				{
					text4 = text3 + string.Join("/", array, num + 1, array.Length - num - 1);
				}
				else
				{
					text4 = text3 + string.Join("/", array, num, array.Length - num);
				}
				keyValuePair.Value.VirtualPath = text4;
				BuildManager.precompiled.Add(text4, keyValuePair.Value);
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x000BA3D0 File Offset: 0x000B85D0
		private static bool LoadPrecompilationInfo(string precomp_config)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(precomp_config))
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.Name != "precompiledApp")
				{
					return false;
				}
			}
			string[] files = Directory.GetFiles(HttpRuntime.BinDirectory, "*.compiled");
			for (int i = 0; i < files.Length; i++)
			{
				BuildManager.LoadCompiled(files[i]);
			}
			BuildManager.FixVirtualPaths();
			return true;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000BA44C File Offset: 0x000B864C
		private static void LoadCompiled(string filename)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(filename))
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.Name == "preserve" && xmlTextReader.HasAttributes)
				{
					xmlTextReader.MoveToNextAttribute();
					string value = xmlTextReader.Value;
					if (xmlTextReader.Name == "resultType" && (value == "2" || value == "3" || value == "8"))
					{
						BuildManager.LoadPageData(xmlTextReader, true);
					}
					else if (value == "1" || value == "6")
					{
						BuildManager.PreCompilationData preCompilationData = BuildManager.LoadPageData(xmlTextReader, false);
						BuildManager.CodeAssemblies.Add(Assembly.Load(preCompilationData.AssemblyFileName));
					}
					else if (value == "9")
					{
						HttpContext.AppGlobalResourcesAssembly = Assembly.Load(BuildManager.LoadPageData(xmlTextReader, false).AssemblyFileName);
					}
				}
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000BA554 File Offset: 0x000B8754
		private static BuildManager.PreCompilationData LoadPageData(XmlTextReader reader, bool store)
		{
			BuildManager.PreCompilationData preCompilationData = new BuildManager.PreCompilationData();
			while (reader.MoveToNextAttribute())
			{
				string name = reader.Name;
				if (name == "virtualPath")
				{
					preCompilationData.VirtualPath = VirtualPathUtility.RemoveTrailingSlash(reader.Value);
				}
				else if (name == "assembly")
				{
					preCompilationData.AssemblyFileName = reader.Value;
				}
				else if (name == "type")
				{
					preCompilationData.TypeName = reader.Value;
				}
			}
			if (store)
			{
				if (BuildManager.precompiled == null)
				{
					BuildManager.precompiled = new Dictionary<string, BuildManager.PreCompilationData>(RuntimeHelpers.StringEqualityComparerCulture);
				}
				BuildManager.precompiled.Add(preCompilationData.VirtualPath, preCompilationData);
			}
			return preCompilationData;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x000BA5F7 File Offset: 0x000B87F7
		private static void AddAssembly(Assembly asm, List<Assembly> al)
		{
			if (al.Contains(asm))
			{
				return;
			}
			al.Add(asm);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000BA60C File Offset: 0x000B880C
		private static void AddPathToIgnore(string vp)
		{
			if (BuildManager.virtualPathsToIgnore == null)
			{
				BuildManager.virtualPathsToIgnore = new Dictionary<string, bool>(RuntimeHelpers.StringEqualityComparerCulture);
			}
			VirtualPath absoluteVirtualPath = BuildManager.GetAbsoluteVirtualPath(vp);
			string absolute = absoluteVirtualPath.Absolute;
			if (!BuildManager.virtualPathsToIgnore.ContainsKey(absolute))
			{
				BuildManager.virtualPathsToIgnore.Add(absolute, true);
				BuildManager.haveVirtualPathsToIgnore = true;
			}
			string appRelative = absoluteVirtualPath.AppRelative;
			if (!BuildManager.virtualPathsToIgnore.ContainsKey(appRelative))
			{
				BuildManager.virtualPathsToIgnore.Add(appRelative, true);
				BuildManager.haveVirtualPathsToIgnore = true;
			}
			if (!BuildManager.virtualPathsToIgnore.ContainsKey(vp))
			{
				BuildManager.virtualPathsToIgnore.Add(vp, true);
				BuildManager.haveVirtualPathsToIgnore = true;
			}
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x0000393A File Offset: 0x00001B3A
		internal static void AddToReferencedAssemblies(Assembly asm)
		{
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000BA6A0 File Offset: 0x000B88A0
		private static void AssertVirtualPathExists(VirtualPath virtualPath)
		{
			bool flag = false;
			if (virtualPath.IsFake)
			{
				string physicalPath = virtualPath.PhysicalPath;
				if (!File.Exists(physicalPath) && !Directory.Exists(physicalPath))
				{
					flag = true;
				}
			}
			else
			{
				VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
				string absolute = virtualPath.Absolute;
				if (!virtualPathProvider.FileExists(absolute) && !virtualPathProvider.DirectoryExists(absolute))
				{
					flag = true;
				}
			}
			if (flag)
			{
				throw new HttpException(404, "The file '" + virtualPath + "' does not exist.", virtualPath.Absolute);
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000BA718 File Offset: 0x000B8918
		private static void Build(VirtualPath vp)
		{
			BuildManager.AssertVirtualPathExists(vp);
			CompilationSection compilationConfig = BuildManager.CompilationConfig;
			object obj = BuildManager.bigCompilationLock;
			lock (obj)
			{
				bool flag2;
				if (!BuildManager.HasCachedItemNoLock(vp.Absolute, out flag2))
				{
					if (BuildManager.recursionDepth == 0UL)
					{
						BuildManager.referencedAssemblies.Clear();
					}
					BuildManager.recursionDepth += 1UL;
					try
					{
						BuildManager.BuildInner(vp, compilationConfig != null && compilationConfig.Debug);
						if (flag2 && BuildManager.recursionDepth <= 1UL)
						{
							BuildManager.buildCount++;
						}
					}
					finally
					{
						if (BuildManager.buildCount > compilationConfig.NumRecompilesBeforeAppRestart)
						{
							HttpRuntime.UnloadAppDomain();
						}
						BuildManager.recursionDepth -= 1UL;
					}
				}
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000BA7E4 File Offset: 0x000B89E4
		private static void BuildInner(VirtualPath vp, bool debug)
		{
			BuildManagerDirectoryBuilder buildManagerDirectoryBuilder = new BuildManagerDirectoryBuilder(vp);
			bool flag = BuildManager.recursionDepth > 1UL;
			List<BuildProviderGroup> list = buildManagerDirectoryBuilder.Build(BuildManager.IsSingleBuild(vp, flag));
			if (list == null)
			{
				return;
			}
			string absolute = vp.Absolute;
			int num = (absolute.GetHashCode() | (int)DateTime.Now.Ticks) + (int)BuildManager.recursionDepth;
			foreach (BuildProviderGroup buildProviderGroup in list)
			{
				bool flag2 = false;
				CompilationException ex = null;
				string text = null;
				bool flag3;
				if (buildProviderGroup.Count == 1)
				{
					if (flag || !buildProviderGroup.Master)
					{
						text = string.Format("{0}_{1}.{2:x}.", buildProviderGroup.NamePrefix, VirtualPathUtility.GetFileName(buildProviderGroup[0].VirtualPath), num);
					}
					flag3 = true;
				}
				else
				{
					flag3 = false;
				}
				if (text == null)
				{
					text = buildProviderGroup.NamePrefix + "_";
				}
				CompilerType compilerType = buildProviderGroup.CompilerType;
				int i = 3;
				while (i > 0)
				{
					AssemblyBuilder assemblyBuilder = new AssemblyBuilder(vp, BuildManager.CreateDomProvider(compilerType), text);
					assemblyBuilder.CompilerOptions = compilerType.CompilerParameters;
					assemblyBuilder.AddAssemblyReference(BuildManager.GetReferencedAssemblies() as List<Assembly>);
					try
					{
						BuildManager.GenerateAssembly(assemblyBuilder, buildProviderGroup, vp, debug);
						i = 0;
					}
					catch (CompilationException ex2)
					{
						i--;
						if (flag3)
						{
							throw new HttpException("Single file build failed.", ex2);
						}
						if (i == 0)
						{
							flag2 = true;
							ex = ex2;
							break;
						}
						CompilerResults results = ex2.Results;
						if (results == null)
						{
							throw new HttpException("No results returned from failed compilation.", ex2);
						}
						BuildManager.RemoveFailedAssemblies(absolute, ex2, assemblyBuilder, buildProviderGroup, results, debug);
					}
				}
				if (flag2)
				{
					if (BuildManager.HasCachedItemNoLock(absolute))
					{
						if (debug)
						{
							BuildManager.DescribeCompilationError("Path '{0}' built successfully, but a compilation exception has been thrown for other files:", ex, new object[] { absolute });
						}
						break;
					}
					BuildManager.Build(vp);
					if (BuildManager.HasCachedItemNoLock(absolute))
					{
						if (debug)
						{
							BuildManager.DescribeCompilationError("Path '{0}' built successfully, but a compilation exception has been thrown for other files:", ex, new object[] { absolute });
						}
						break;
					}
					throw new HttpException("Requested virtual path build failed.", ex);
				}
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000BAA10 File Offset: 0x000B8C10
		private static CodeDomProvider CreateDomProvider(CompilerType ct)
		{
			if (BuildManager.codeDomProviders == null)
			{
				BuildManager.codeDomProviders = new Dictionary<Type, CodeDomProvider>();
			}
			Type type = ct.CodeDomProviderType;
			if (type == null)
			{
				CompilationSection compilationConfig = BuildManager.CompilationConfig;
				CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(compilationConfig.DefaultLanguage, compilationConfig);
				if (defaultCompilerTypeForLanguage != null)
				{
					type = defaultCompilerTypeForLanguage.CodeDomProviderType;
				}
			}
			if (type == null)
			{
				return null;
			}
			CodeDomProvider codeDomProvider;
			if (BuildManager.codeDomProviders.TryGetValue(type, out codeDomProvider))
			{
				return codeDomProvider;
			}
			codeDomProvider = Activator.CreateInstance(type) as CodeDomProvider;
			if (codeDomProvider == null)
			{
				return null;
			}
			BuildManager.codeDomProviders.Add(type, codeDomProvider);
			return codeDomProvider;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000BAA98 File Offset: 0x000B8C98
		internal static void CallPreStartMethods()
		{
			if (BuildManager.preStartMethodsDone)
			{
				return;
			}
			BuildManager.preStartMethodsRunning = true;
			MethodInfo methodInfo = null;
			try
			{
				List<MethodInfo> list = BuildManager.LoadPreStartMethodsFromAssemblies(BuildManager.GetReferencedAssemblies() as List<Assembly>);
				if (list != null && list.Count != 0)
				{
					foreach (MethodInfo methodInfo2 in list)
					{
						(methodInfo = methodInfo2).Invoke(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				throw new HttpException(string.Format("The pre-application start initialization method {0} on type {1} threw an exception with the following error message: {2}", (methodInfo != null) ? methodInfo.Name : "UNKNOWN", (methodInfo != null) ? methodInfo.DeclaringType.FullName : "UNKNOWN", ex.Message), ex);
			}
			finally
			{
				BuildManager.preStartMethodsRunning = false;
				BuildManager.preStartMethodsDone = true;
			}
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000BAB88 File Offset: 0x000B8D88
		private static List<MethodInfo> LoadPreStartMethodsFromAssemblies(List<Assembly> assemblies)
		{
			if (assemblies == null || assemblies.Count == 0)
			{
				return null;
			}
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (Assembly assembly in assemblies)
			{
				PreApplicationStartMethodAttribute preApplicationStartMethodAttribute;
				Type type;
				try
				{
					object[] customAttributes = assembly.GetCustomAttributes(typeof(PreApplicationStartMethodAttribute), false);
					if (customAttributes == null || customAttributes.Length == 0)
					{
						continue;
					}
					preApplicationStartMethodAttribute = customAttributes[0] as PreApplicationStartMethodAttribute;
					type = preApplicationStartMethodAttribute.Type;
					if (type == null)
					{
						continue;
					}
				}
				catch
				{
					continue;
				}
				Exception ex = null;
				MethodInfo methodInfo;
				try
				{
					if (type.IsPublic)
					{
						methodInfo = type.GetMethod(preApplicationStartMethodAttribute.MethodName, BindingFlags.Static | BindingFlags.Public, null, new Type[0], null);
					}
					else
					{
						methodInfo = null;
					}
				}
				catch (Exception ex)
				{
					methodInfo = null;
				}
				if (methodInfo == null)
				{
					throw new HttpException(string.Format("The method specified by the PreApplicationStartMethodAttribute on assembly '{0}' cannot be resolved. Type: '{1}', MethodName: '{2}'. Verify that the type is public and the method is public and static (Shared in Visual Basic).", assembly.FullName, type.FullName, preApplicationStartMethodAttribute.MethodName), ex);
				}
				list.Add(methodInfo);
			}
			return list;
		}

		/// <summary>Gets an object that represents the compiled type for the Global.asax file.</summary>
		/// <returns>An object that represents the compiled type for the Global.asax file.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to call this method before the Global.asax page was compiled.</exception>
		// Token: 0x06004482 RID: 17538 RVA: 0x000BACAC File Offset: 0x000B8EAC
		public static Type GetGlobalAsaxType()
		{
			Type appType = HttpApplicationFactory.AppType;
			if (appType == null)
			{
				return typeof(HttpApplication);
			}
			return appType;
		}

		/// <summary>Creates a cached file.</summary>
		/// <returns>The <see cref="T:System.IO.Stream" /> object for the new file.</returns>
		/// <param name="fileName">The name of the file to create.</param>
		// Token: 0x06004483 RID: 17539 RVA: 0x000BACD4 File Offset: 0x000B8ED4
		public static Stream CreateCachedFile(string fileName)
		{
			if (fileName != null && (fileName == string.Empty || fileName.IndexOf(Path.DirectorySeparatorChar) != -1))
			{
				throw new ArgumentException("Value does not fall within the expected range.");
			}
			return new FileStream(Path.Combine(HttpRuntime.CodegenDir, fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>Reads a cached file.</summary>
		/// <returns>The <see cref="T:System.IO.Stream" /> object for the file, or null if the file does not exist.</returns>
		/// <param name="fileName">The name of the file to read.</param>
		// Token: 0x06004484 RID: 17540 RVA: 0x000BAD14 File Offset: 0x000B8F14
		public static Stream ReadCachedFile(string fileName)
		{
			if (fileName != null && (fileName == string.Empty || fileName.IndexOf(Path.DirectorySeparatorChar) != -1))
			{
				throw new ArgumentException("Value does not fall within the expected range.");
			}
			string text = Path.Combine(HttpRuntime.CodegenDir, fileName);
			if (!File.Exists(text))
			{
				return null;
			}
			return new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.None);
		}

		/// <summary>Adds an assembly to the application's set of referenced assemblies.</summary>
		/// <param name="assembly">The assembly to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is null or empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was not called before the Application_Start event in the Global.asax file occurred.</exception>
		// Token: 0x06004485 RID: 17541 RVA: 0x000BAD6C File Offset: 0x000B8F6C
		[global::System.MonoDocumentationNote("Fully implemented but no info on application pre-init stage is available yet.")]
		public static void AddReferencedAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (BuildManager.preStartMethodsDone)
			{
				throw new InvalidOperationException("This method cannot be called after the application's pre-start initialization stage.");
			}
			if (BuildManager.dynamicallyRegisteredAssemblies == null)
			{
				BuildManager.dynamicallyRegisteredAssemblies = new List<Assembly>();
			}
			if (!BuildManager.dynamicallyRegisteredAssemblies.Contains(assembly))
			{
				BuildManager.dynamicallyRegisteredAssemblies.Add(assembly);
			}
		}

		/// <summary>Gets an object factory for the specified virtual path.</summary>
		/// <returns>The object factory.</returns>
		/// <param name="virtualPath">The virtual path.</param>
		/// <param name="throwIfNotFound">true to throw an error if the virtual path does not exist; otherwise, false. If the virtual path does not exist and <paramref name="throwIfNotFound" /> is false, this method returns null.</param>
		/// <exception cref="T:System.Web.HttpException">The virtual path does not exist.-or-A higher-level exception already existed when this method was called.-or-This method was called while the compilation process was building top-level files.-or-This is a precompiled application and the virtual path was not found in the cache.-or-A circular reference was detected.</exception>
		// Token: 0x06004486 RID: 17542 RVA: 0x000BADC8 File Offset: 0x000B8FC8
		[global::System.MonoDocumentationNote("Not used by Mono internally. Needed for MVC3")]
		public static IWebObjectFactory GetObjectFactory(string virtualPath, bool throwIfNotFound)
		{
			if (BuildManager.CompilingTopLevelAssemblies)
			{
				throw new HttpException("Method must not be called while compiling the top level assemblies.");
			}
			if (BuildManager.is_precompiled)
			{
				Type type = BuildManager.GetPrecompiledType(virtualPath);
				if (!(type == null))
				{
					return new SimpleWebObjectFactory(type);
				}
				if (throwIfNotFound)
				{
					throw new HttpException(string.Format("Virtual path '{0}' not found in precompiled application type cache.", virtualPath));
				}
				return null;
			}
			else
			{
				Exception ex = null;
				Type type;
				try
				{
					type = BuildManager.GetCompiledType(virtualPath);
				}
				catch (Exception ex)
				{
					type = null;
				}
				if (!(type == null))
				{
					return new SimpleWebObjectFactory(type);
				}
				if (throwIfNotFound)
				{
					throw new HttpException(string.Format("Virtual path '{0}' does not exist.", virtualPath), ex);
				}
				return null;
			}
		}

		/// <summary>Processes a file, given its virtual path, and creates an instance of the result.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents the instance of the processed file.</returns>
		/// <param name="virtualPath">The virtual path of the file to create an instance of.</param>
		/// <param name="requiredBaseType">The base type that defines the object to be created.</param>
		// Token: 0x06004487 RID: 17543 RVA: 0x000BAE64 File Offset: 0x000B9064
		public static object CreateInstanceFromVirtualPath(string virtualPath, Type requiredBaseType)
		{
			return BuildManager.CreateInstanceFromVirtualPath(BuildManager.GetAbsoluteVirtualPath(virtualPath), requiredBaseType);
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000BAE74 File Offset: 0x000B9074
		internal static object CreateInstanceFromVirtualPath(VirtualPath virtualPath, Type requiredBaseType)
		{
			if (requiredBaseType == null)
			{
				throw new NullReferenceException();
			}
			Type compiledType = BuildManager.GetCompiledType(virtualPath);
			if (compiledType == null)
			{
				return null;
			}
			if (!requiredBaseType.IsAssignableFrom(compiledType))
			{
				throw new HttpException(500, string.Format("Type '{0}' does not inherit from '{1}'.", compiledType.FullName, requiredBaseType.FullName));
			}
			return Activator.CreateInstance(compiledType, null);
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000BAED4 File Offset: 0x000B90D4
		private static void DescribeCompilationError(string format, CompilationException ex, params object[] parms)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			if (parms != null)
			{
				stringBuilder.AppendFormat(format + newLine, parms);
			}
			else
			{
				stringBuilder.Append(format + newLine);
			}
			CompilerResults compilerResults = ((ex != null) ? ex.Results : null);
			if (compilerResults == null)
			{
				stringBuilder.Append("No compiler error information present." + newLine);
			}
			else
			{
				stringBuilder.Append("Compiler errors:" + newLine);
				foreach (object obj in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					stringBuilder.Append("  " + compilerError.ToString() + newLine);
				}
			}
			if (ex != null)
			{
				stringBuilder.Append(newLine + "Exception thrown:" + newLine);
				stringBuilder.Append(ex.ToString());
			}
			BuildManager.ShowDebugModeMessage(stringBuilder.ToString());
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000BAFD8 File Offset: 0x000B91D8
		private static BuildProvider FindBuildProviderForPhysicalPath(string path, BuildProviderGroup group, HttpRequest req)
		{
			if (req == null || string.IsNullOrEmpty(path))
			{
				return null;
			}
			foreach (BuildProvider buildProvider in group)
			{
				if (string.Compare(path, req.MapPath(buildProvider.VirtualPath), RuntimeHelpers.StringComparison) == 0)
				{
					return buildProvider;
				}
			}
			return null;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000BB04C File Offset: 0x000B924C
		private static void GenerateAssembly(AssemblyBuilder abuilder, BuildProviderGroup group, VirtualPath vp, bool debug)
		{
			string absolute = vp.Absolute;
			int num = 0;
			string text;
			StringBuilder stringBuilder;
			if (debug)
			{
				text = Environment.NewLine;
				stringBuilder = new StringBuilder("Code generation for certain virtual paths in a batch failed. Those files have been removed from the batch." + text);
				stringBuilder.Append("Since you're running in debug mode, here's some more information about the error:" + text);
			}
			else
			{
				text = null;
				stringBuilder = null;
			}
			List<BuildProvider> list = null;
			StringComparison stringComparison = RuntimeHelpers.StringComparison;
			foreach (BuildProvider buildProvider in group)
			{
				string virtualPath = buildProvider.VirtualPath;
				if (!BuildManager.HasCachedItemNoLock(virtualPath))
				{
					try
					{
						buildProvider.GenerateCode(abuilder);
					}
					catch (Exception ex)
					{
						if (string.Compare(virtualPath, absolute, stringComparison) != 0)
						{
							if (list == null)
							{
								list = new List<BuildProvider>();
							}
							list.Add(buildProvider);
							num++;
							if (stringBuilder != null)
							{
								if (num > 1)
								{
									stringBuilder.Append(text);
								}
								stringBuilder.AppendFormat("Failed file virtual path: {0}; Exception: {1}{2}{1}", buildProvider.VirtualPath, text, ex);
							}
							continue;
						}
						if (ex is CompilationException || ex is ParseException)
						{
							throw;
						}
						throw new HttpException("Code generation failed.", ex);
					}
					IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
					if (dictionary != null)
					{
						foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
						{
							BuildManagerCacheItem cachedItemNoLock = BuildManager.GetCachedItemNoLock(keyValuePair.Key);
							if (cachedItemNoLock != null && !(cachedItemNoLock.BuiltAssembly == null))
							{
								abuilder.AddAssemblyReference(cachedItemNoLock.BuiltAssembly);
							}
						}
					}
				}
			}
			if (stringBuilder != null && num > 0)
			{
				BuildManager.ShowDebugModeMessage(stringBuilder.ToString());
			}
			if (list != null)
			{
				foreach (BuildProvider buildProvider2 in list)
				{
					group.Remove(buildProvider2);
				}
			}
			foreach (Assembly assembly in BuildManager.referencedAssemblies)
			{
				if (!(assembly == null))
				{
					abuilder.AddAssemblyReference(assembly);
				}
			}
			CompilerResults compilerResults = abuilder.BuildAssembly(vp);
			Assembly assembly2 = ((compilerResults != null) ? compilerResults.CompiledAssembly : null);
			try
			{
				BuildManager.buildCacheLock.EnterWriteLock();
				if (assembly2 != null)
				{
					BuildManager.referencedAssemblies.Add(assembly2);
				}
				foreach (BuildProvider buildProvider3 in group)
				{
					if (!BuildManager.HasCachedItemNoLock(buildProvider3.VirtualPath))
					{
						BuildManager.StoreInCache(buildProvider3, assembly2, compilerResults);
					}
				}
			}
			finally
			{
				BuildManager.buildCacheLock.ExitWriteLock();
			}
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000BB390 File Offset: 0x000B9590
		private static VirtualPath GetAbsoluteVirtualPath(string virtualPath)
		{
			string text2;
			if (!VirtualPathUtility.IsRooted(virtualPath))
			{
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest == null)
				{
					throw new HttpException("No context, cannot map paths.");
				}
				string text = httpRequest.FilePath;
				if (!string.IsNullOrEmpty(text) && string.Compare(text, "/", StringComparison.Ordinal) != 0)
				{
					text = VirtualPathUtility.GetDirectory(text);
				}
				else
				{
					text = "/";
				}
				text2 = VirtualPathUtility.Combine(text, virtualPath);
			}
			else
			{
				text2 = virtualPath;
			}
			return new VirtualPath(text2);
		}

		/// <summary>Returns a build dependency set for a virtual path if the path is located in the ASP.NET cache.</summary>
		/// <returns>A <see cref="T:System.Web.Compilation.BuildDependencySet" /> object that is stored in the cache, or null if the <see cref="T:System.Web.Compilation.BuildDependencySet" /> object cannot be retrieved from the cache.</returns>
		/// <param name="context">The context of the request.</param>
		/// <param name="virtualPath">The virtual path from which to determine the build dependency set.</param>
		// Token: 0x0600448D RID: 17549 RVA: 0x00003BEA File Offset: 0x00001DEA
		[global::System.MonoTODO("Not implemented, always returns null")]
		public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath)
		{
			return null;
		}

		/// <summary>Returns a build dependency set for a virtual path if the path is located in the ASP.NET cache, even if the content is not current. </summary>
		/// <returns>A <see cref="T:System.Web.Compilation.BuildDependencySet" /> object that is stored in the cache, or null if the <see cref="T:System.Web.Compilation.BuildDependencySet" /> object cannot be retrieved from the cache.</returns>
		/// <param name="context">The context of the request.</param>
		/// <param name="virtualPath">The virtual path from which to determine the build dependency set.</param>
		/// <param name="ensureIsUpToDate">true to specify that only a current build dependency set should be returned, or false to indicate that any available build dependency set should be returned, even if it is not current. The default is true.</param>
		// Token: 0x0600448E RID: 17550 RVA: 0x00003BEA File Offset: 0x00001DEA
		[global::System.MonoTODO("Not implemented, always returns null")]
		public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath, bool ensureIsUpToDate)
		{
			return null;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000BB404 File Offset: 0x000B9604
		private static BuildManagerCacheItem GetCachedItem(string vp)
		{
			BuildManagerCacheItem cachedItemNoLock;
			try
			{
				BuildManager.buildCacheLock.EnterReadLock();
				cachedItemNoLock = BuildManager.GetCachedItemNoLock(vp);
			}
			finally
			{
				BuildManager.buildCacheLock.ExitReadLock();
			}
			return cachedItemNoLock;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000BB440 File Offset: 0x000B9640
		private static BuildManagerCacheItem GetCachedItemNoLock(string vp)
		{
			BuildManagerCacheItem buildManagerCacheItem;
			if (BuildManager.buildCache.TryGetValue(vp, out buildManagerCacheItem))
			{
				return buildManagerCacheItem;
			}
			return null;
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000BB460 File Offset: 0x000B9660
		internal static Type GetCodeDomProviderType(BuildProvider provider)
		{
			Type type = null;
			CompilerType codeCompilerType = provider.CodeCompilerType;
			if (codeCompilerType != null)
			{
				type = codeCompilerType.CodeDomProviderType;
			}
			if (type == null)
			{
				throw new HttpException("Provider '" + provider + " 'fails to specify the compiler type.");
			}
			return type;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000BB4A0 File Offset: 0x000B96A0
		private static Type GetPrecompiledType(string virtualPath)
		{
			if (BuildManager.precompiled == null || BuildManager.precompiled.Count == 0)
			{
				return null;
			}
			VirtualPath virtualPath2 = new VirtualPath(virtualPath);
			BuildManager.PreCompilationData preCompilationData;
			if (!BuildManager.precompiled.TryGetValue(virtualPath2.Absolute, out preCompilationData) && !BuildManager.precompiled.TryGetValue(virtualPath, out preCompilationData))
			{
				return null;
			}
			if (preCompilationData.Type == null)
			{
				preCompilationData.Type = Type.GetType(preCompilationData.TypeName + ", " + preCompilationData.AssemblyFileName, true);
			}
			return preCompilationData.Type;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x000BB524 File Offset: 0x000B9724
		internal static Type GetPrecompiledApplicationType()
		{
			if (!BuildManager.is_precompiled)
			{
				return null;
			}
			string text = VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath);
			Type type = BuildManager.GetPrecompiledType(VirtualPathUtility.Combine(text, "global.asax"));
			if (type == null)
			{
				type = BuildManager.GetPrecompiledType(VirtualPathUtility.Combine(text, "Global.asax"));
			}
			return type;
		}

		/// <summary>Compiles a file into an assembly using the specified virtual path.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that is compiled from the specified virtual path, which is cached to either memory or to disk.</returns>
		/// <param name="virtualPath">The virtual path to build into an assembly.</param>
		// Token: 0x06004494 RID: 17556 RVA: 0x000BB571 File Offset: 0x000B9771
		public static Assembly GetCompiledAssembly(string virtualPath)
		{
			return BuildManager.GetCompiledAssembly(BuildManager.GetAbsoluteVirtualPath(virtualPath));
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000BB580 File Offset: 0x000B9780
		internal static Assembly GetCompiledAssembly(VirtualPath virtualPath)
		{
			string absolute = virtualPath.Absolute;
			if (BuildManager.is_precompiled)
			{
				Type precompiledType = BuildManager.GetPrecompiledType(absolute);
				if (precompiledType != null)
				{
					return precompiledType.Assembly;
				}
			}
			BuildManagerCacheItem buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				return buildManagerCacheItem.BuiltAssembly;
			}
			BuildManager.Build(virtualPath);
			buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				return buildManagerCacheItem.BuiltAssembly;
			}
			return null;
		}

		/// <summary>Compiles a file, given its virtual path, and returns the compiled type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type generated from compiling the virtual path.</returns>
		/// <param name="virtualPath">The virtual path to build into a type.</param>
		/// <exception cref="T:System.Web.HttpException">An error occurred when compiling the virtual path.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06004496 RID: 17558 RVA: 0x000BB5DB File Offset: 0x000B97DB
		public static Type GetCompiledType(string virtualPath)
		{
			return BuildManager.GetCompiledType(BuildManager.GetAbsoluteVirtualPath(virtualPath));
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000BB5E8 File Offset: 0x000B97E8
		internal static Type GetCompiledType(VirtualPath virtualPath)
		{
			string absolute = virtualPath.Absolute;
			if (BuildManager.is_precompiled)
			{
				Type precompiledType = BuildManager.GetPrecompiledType(absolute);
				if (precompiledType != null)
				{
					return precompiledType;
				}
			}
			BuildManagerCacheItem buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				BuildManager.ReferenceAssemblyInCompilation(buildManagerCacheItem);
				return buildManagerCacheItem.Type;
			}
			BuildManager.Build(virtualPath);
			buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				BuildManager.ReferenceAssemblyInCompilation(buildManagerCacheItem);
				return buildManagerCacheItem.Type;
			}
			return null;
		}

		/// <summary>Compiles a file, given its virtual path, and returns a custom string that the build provider persists in cache.</summary>
		/// <returns>A string, as returned by the <see cref="M:System.Web.Compilation.BuildProvider.GetCustomString(System.CodeDom.Compiler.CompilerResults)" /> method, that is cached to disk or memory.</returns>
		/// <param name="virtualPath">The virtual path of the file to build.</param>
		// Token: 0x06004498 RID: 17560 RVA: 0x000BB64A File Offset: 0x000B984A
		public static string GetCompiledCustomString(string virtualPath)
		{
			return BuildManager.GetCompiledCustomString(BuildManager.GetAbsoluteVirtualPath(virtualPath));
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000BB658 File Offset: 0x000B9858
		internal static string GetCompiledCustomString(VirtualPath virtualPath)
		{
			string absolute = virtualPath.Absolute;
			BuildManagerCacheItem buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				return buildManagerCacheItem.CompiledCustomString;
			}
			BuildManager.Build(virtualPath);
			buildManagerCacheItem = BuildManager.GetCachedItem(absolute);
			if (buildManagerCacheItem != null)
			{
				return buildManagerCacheItem.CompiledCustomString;
			}
			return null;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000BB695 File Offset: 0x000B9895
		internal static CompilerType GetDefaultCompilerTypeForLanguage(string language, CompilationSection configSection)
		{
			return BuildManager.GetDefaultCompilerTypeForLanguage(language, configSection, true);
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000BB6A0 File Offset: 0x000B98A0
		internal static CompilerType GetDefaultCompilerTypeForLanguage(string language, CompilationSection configSection, bool throwOnMissing)
		{
			if (language == null || language.Length == 0)
			{
				throw new ArgumentNullException("language");
			}
			CompilationSection compilationSection;
			if (configSection == null)
			{
				compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			}
			else
			{
				compilationSection = configSection;
			}
			Compiler compiler = compilationSection.Compilers.Get(language);
			if (compiler != null)
			{
				Type type = HttpApplication.LoadType(compiler.Type, true);
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.CompilerOptions = compiler.CompilerOptions;
				compilerParameters.WarningLevel = compiler.WarningLevel;
				BuildManager.SetCommonParameters(compilationSection, compilerParameters, type, language);
				return new CompilerType(type, compilerParameters);
			}
			if (CodeDomProvider.IsDefinedLanguage(language))
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
				CompilerParameters compilerParameters = compilerInfo.CreateDefaultCompilerParameters();
				Type type = compilerInfo.CodeDomProviderType;
				BuildManager.SetCommonParameters(compilationSection, compilerParameters, type, language);
				return new CompilerType(type, compilerParameters);
			}
			if (throwOnMissing)
			{
				throw new HttpException("No compiler for language '" + language + "'.");
			}
			return null;
		}

		/// <summary>Returns a list of assembly references that all page compilations must reference.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> collection of assembly references.</returns>
		// Token: 0x0600449C RID: 17564 RVA: 0x000BB76C File Offset: 0x000B996C
		public static ICollection GetReferencedAssemblies()
		{
			if (BuildManager.getReferencedAssembliesInvoked)
			{
				return BuildManager.configReferencedAssemblies;
			}
			if (BuildManager.allowReferencedAssembliesCaching)
			{
				BuildManager.getReferencedAssembliesInvoked = true;
			}
			if (BuildManager.configReferencedAssemblies == null)
			{
				BuildManager.configReferencedAssemblies = new List<Assembly>();
			}
			else if (BuildManager.getReferencedAssembliesInvoked)
			{
				BuildManager.configReferencedAssemblies.Clear();
			}
			CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			if (compilationSection == null)
			{
				return BuildManager.configReferencedAssemblies;
			}
			bool flag = false;
			foreach (object obj in compilationSection.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				if (assemblyInfo.Assembly == "*")
				{
					flag = !BuildManager.is_precompiled;
				}
				else
				{
					BuildManager.LoadAssembly(assemblyInfo, BuildManager.configReferencedAssemblies);
				}
			}
			foreach (object obj2 in BuildManager.TopLevelAssemblies)
			{
				Assembly assembly = (Assembly)obj2;
				BuildManager.configReferencedAssemblies.Add(assembly);
			}
			foreach (object obj3 in WebConfigurationManager.ExtraAssemblies)
			{
				BuildManager.LoadAssembly((string)obj3, BuildManager.configReferencedAssemblies);
			}
			if (BuildManager.dynamicallyRegisteredAssemblies != null)
			{
				foreach (Assembly assembly2 in BuildManager.dynamicallyRegisteredAssemblies)
				{
					BuildManager.configReferencedAssemblies.Add(assembly2);
				}
			}
			if (BuildManager.is_precompiled || flag)
			{
				foreach (string text in HttpApplication.BinDirectoryAssemblies)
				{
					try
					{
						BuildManager.LoadAssembly(text, BuildManager.configReferencedAssemblies);
					}
					catch (BadImageFormatException)
					{
					}
				}
			}
			return BuildManager.configReferencedAssemblies;
		}

		/// <summary>Finds a type in the top-level assemblies or in assemblies that are defined in configuration, and optionally throws an exception on failure.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the requested <paramref name="typeName" /> parameter.</returns>
		/// <param name="typeName">The name of the type.</param>
		/// <param name="throwOnError">true to throw an exception if a <see cref="T:System.Type" /> object cannot be generated for the type name; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="typeName" /> is invalid.- or -<paramref name="typeName" /> is ambiguous.- or -<paramref name="typeName" /> could not be found, and <paramref name="throwOnError" /> is true.</exception>
		// Token: 0x0600449D RID: 17565 RVA: 0x000BB97C File Offset: 0x000B9B7C
		public static Type GetType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError, false);
		}

		/// <summary>Finds a type in the top-level assemblies, or in assemblies that are defined in configuration, by using a case-insensitive search and optionally throwing an exception on failure.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the requested <paramref name="typeName" /> parameter.</returns>
		/// <param name="typeName">The name of the type.</param>
		/// <param name="throwOnError">true to throw an exception if a <see cref="T:System.Type" /> cannot be generated for the type name; otherwise, false.</param>
		/// <param name="ignoreCase">true if <paramref name="typeName" /> is case-sensitive; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="typeName" /> is invalid.- or -<paramref name="typeName" /> is ambiguous.- or -<paramref name="typeName" /> could not be found, and <paramref name="throwOnError" /> is true.</exception>
		// Token: 0x0600449E RID: 17566 RVA: 0x000BB988 File Offset: 0x000B9B88
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new HttpException("Type name must not be empty.");
			}
			Exception ex = null;
			try
			{
				int num = typeName.IndexOf(',');
				string text;
				string text2;
				if (num > 0 && num < typeName.Length - 1)
				{
					text = new AssemblyName(typeName.Substring(num + 1)).ToString();
					text2 = typeName.Substring(0, num);
				}
				else
				{
					text = null;
					text2 = typeName;
				}
				List<Assembly> list = new List<Assembly>();
				list.AddRange(BuildManager.GetReferencedAssemblies() as List<Assembly>);
				list.AddRange(BuildManager.TopLevel_Assemblies);
				Type appType = HttpApplicationFactory.AppType;
				if (appType != null)
				{
					list.Add(appType.Assembly);
				}
				foreach (Assembly assembly in list)
				{
					if (!(assembly == null))
					{
						if (text != null)
						{
							if (string.Compare(text, assembly.GetName().ToString(), StringComparison.Ordinal) == 0)
							{
								Type type = assembly.GetType(text2, throwOnError, ignoreCase);
								if (type != null)
								{
									return type;
								}
							}
						}
						else
						{
							Type type = assembly.GetType(text2, false, ignoreCase);
							if (type != null)
							{
								return type;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
			if (throwOnError)
			{
				throw new HttpException("Failed to find the specified type.", ex);
			}
			return null;
		}

		/// <summary>Provides a collection of virtual-path dependencies for a specified virtual path.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> collection of files represented by virtual paths that are caching dependencies for the virtual path.</returns>
		/// <param name="virtualPath">The virtual path used to determine the dependencies.</param>
		// Token: 0x0600449F RID: 17567 RVA: 0x000BBAFC File Offset: 0x000B9CFC
		public static ICollection GetVirtualPathDependencies(string virtualPath)
		{
			return BuildManager.GetVirtualPathDependencies(virtualPath, null);
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000BBB08 File Offset: 0x000B9D08
		internal static ICollection GetVirtualPathDependencies(string virtualPath, BuildProvider bprovider)
		{
			BuildProvider buildProvider = bprovider;
			if (buildProvider == null)
			{
				CompilationSection compilationConfig = BuildManager.CompilationConfig;
				if (compilationConfig == null)
				{
					return null;
				}
				buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(virtualPath, compilationConfig.BuildProviders);
			}
			if (buildProvider == null)
			{
				return null;
			}
			IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
			if (dictionary == null)
			{
				return null;
			}
			return (ICollection)dictionary.Keys;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000BBB50 File Offset: 0x000B9D50
		internal static bool HasCachedItemNoLock(string vp, out bool entryExists)
		{
			BuildManagerCacheItem buildManagerCacheItem;
			if (BuildManager.buildCache.TryGetValue(vp, out buildManagerCacheItem))
			{
				entryExists = true;
				return buildManagerCacheItem != null;
			}
			entryExists = false;
			return false;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000BBB78 File Offset: 0x000B9D78
		internal static bool HasCachedItemNoLock(string vp)
		{
			bool flag;
			return BuildManager.HasCachedItemNoLock(vp, out flag);
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000BBB90 File Offset: 0x000B9D90
		internal static bool IgnoreVirtualPath(string virtualPath)
		{
			if (!BuildManager.virtualPathsToIgnoreChecked)
			{
				object obj = BuildManager.virtualPathsToIgnoreLock;
				lock (obj)
				{
					if (!BuildManager.virtualPathsToIgnoreChecked)
					{
						BuildManager.LoadVirtualPathsToIgnore();
					}
					BuildManager.virtualPathsToIgnoreChecked = true;
				}
			}
			return BuildManager.haveVirtualPathsToIgnore && BuildManager.virtualPathsToIgnore.ContainsKey(virtualPath);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000BBBFC File Offset: 0x000B9DFC
		private static bool IsSingleBuild(VirtualPath vp, bool recursive)
		{
			return string.Compare(vp.AppRelative, "~/global.asax", StringComparison.OrdinalIgnoreCase) == 0 || !BuildManager.BatchMode || recursive;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000BBC1D File Offset: 0x000B9E1D
		private static void LoadAssembly(string path, List<Assembly> al)
		{
			BuildManager.AddAssembly(Assembly.LoadFrom(path), al);
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000BBC2B File Offset: 0x000B9E2B
		private static void LoadAssembly(AssemblyInfo info, List<Assembly> al)
		{
			BuildManager.AddAssembly(Assembly.Load(info.Assembly), al);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x000BBC40 File Offset: 0x000B9E40
		private static void LoadVirtualPathsToIgnore()
		{
			NameValueCollection appSettings = WebConfigurationManager.AppSettings;
			if (appSettings == null)
			{
				return;
			}
			string text = appSettings["MonoAspnetBatchCompileIgnorePaths"];
			string text2 = appSettings["MonoAspnetBatchCompileIgnoreFromFile"];
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(BuildManager.virtualPathsToIgnoreSplitChars);
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = array[i].Trim();
					if (text3.Length != 0)
					{
						BuildManager.AddPathToIgnore(text3);
					}
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest == null)
				{
					throw new HttpException("Missing context, cannot continue.");
				}
				string text4 = httpRequest.MapPath(text2);
				if (!File.Exists(text4))
				{
					return;
				}
				string[] array2 = File.ReadAllLines(text4);
				if (array2 == null || array2.Length == 0)
				{
					return;
				}
				string[] array = array2;
				for (int i = 0; i < array.Length; i++)
				{
					string text5 = array[i].Trim();
					if (text5.Length != 0)
					{
						BuildManager.AddPathToIgnore(text5);
					}
				}
			}
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000BBD34 File Offset: 0x000B9F34
		private static void OnEntryRemoved(string vp)
		{
			BuildManagerRemoveEntryEventHandler buildManagerRemoveEntryEventHandler = BuildManager.events[BuildManager.buildManagerRemoveEntryEvent] as BuildManagerRemoveEntryEventHandler;
			if (buildManagerRemoveEntryEventHandler != null)
			{
				buildManagerRemoveEntryEventHandler(new BuildManagerRemoveEntryEventArgs(vp, HttpContext.Current));
			}
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x000BBD6C File Offset: 0x000B9F6C
		private static void OnVirtualPathChanged(string key, object value, CacheItemRemovedReason removedReason)
		{
			if (StrUtils.StartsWith(key, "@@Build_Manager@@"))
			{
				string text = key.Substring(BuildManager.BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX_LENGTH);
				try
				{
					BuildManager.buildCacheLock.EnterWriteLock();
					if (BuildManager.HasCachedItemNoLock(text))
					{
						BuildManager.buildCache[text] = null;
						BuildManager.OnEntryRemoved(text);
					}
				}
				finally
				{
					BuildManager.buildCacheLock.ExitWriteLock();
				}
				return;
			}
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x000BBDD8 File Offset: 0x000B9FD8
		private static void ReferenceAssemblyInCompilation(BuildManagerCacheItem bmci)
		{
			if (BuildManager.recursionDepth == 0UL || BuildManager.referencedAssemblies.Contains(bmci.BuiltAssembly))
			{
				return;
			}
			BuildManager.referencedAssemblies.Add(bmci.BuiltAssembly);
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x000BBE04 File Offset: 0x000BA004
		private static void RemoveFailedAssemblies(string requestedVirtualPath, CompilationException ex, AssemblyBuilder abuilder, BuildProviderGroup group, CompilerResults results, bool debug)
		{
			string text;
			StringBuilder stringBuilder;
			if (debug)
			{
				text = Environment.NewLine;
				stringBuilder = new StringBuilder("Compilation of certain files in a batch failed. Another attempt to compile the batch will be made." + text);
				stringBuilder.Append("Since you're running in debug mode, here's some more information about the error:" + text);
			}
			else
			{
				text = null;
				stringBuilder = null;
			}
			List<BuildProvider> list = new List<BuildProvider>();
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			bool flag = false;
			foreach (object obj in results.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					BuildProvider buildProvider = abuilder.GetBuildProviderForPhysicalFilePath(compilerError.FileName);
					if (buildProvider == null)
					{
						buildProvider = BuildManager.FindBuildProviderForPhysicalPath(compilerError.FileName, group, httpRequest);
						if (buildProvider == null)
						{
							continue;
						}
					}
					if (string.Compare(buildProvider.VirtualPath, requestedVirtualPath, StringComparison.Ordinal) == 0)
					{
						flag = true;
					}
					if (!list.Contains(buildProvider))
					{
						list.Add(buildProvider);
						if (stringBuilder != null)
						{
							stringBuilder.AppendFormat("\t{0}{1}", buildProvider.VirtualPath, text);
						}
					}
					if (stringBuilder != null)
					{
						stringBuilder.AppendFormat("\t\t{0}{1}", compilerError, text);
					}
				}
			}
			foreach (BuildProvider buildProvider2 in list)
			{
				group.Remove(buildProvider2);
			}
			if (stringBuilder != null)
			{
				stringBuilder.AppendFormat("{0}The following exception has been thrown for the file(s) listed above:{0}{1}", text, ex.ToString());
				BuildManager.ShowDebugModeMessage(stringBuilder.ToString());
				stringBuilder = null;
			}
			if (flag)
			{
				throw new HttpException("Compilation failed.", ex);
			}
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x000BBFA4 File Offset: 0x000BA1A4
		private static void SetCommonParameters(CompilationSection config, CompilerParameters p, Type compilerType, string language)
		{
			p.IncludeDebugInformation = config.Debug;
			MonoSettingsSection monoSettingsSection = WebConfigurationManager.GetSection("system.web/monoSettings") as MonoSettingsSection;
			if (monoSettingsSection == null || !monoSettingsSection.UseCompilersCompatibility)
			{
				return;
			}
			Compiler compiler = monoSettingsSection.CompilersCompatibility.Get(language);
			if (compiler == null)
			{
				return;
			}
			if (HttpApplication.LoadType(compiler.Type, false) != compilerType)
			{
				return;
			}
			p.CompilerOptions = p.CompilerOptions + " " + compiler.CompilerOptions;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x000BC01C File Offset: 0x000BA21C
		private static void ShowDebugModeMessage(string msg)
		{
			if (BuildManager.suppressDebugModeMessages)
			{
				return;
			}
			Console.Error.WriteLine();
			Console.Error.WriteLine("******* DEBUG MODE MESSAGE *******");
			Console.Error.WriteLine(msg);
			Console.Error.WriteLine("******* DEBUG MODE MESSAGE *******");
			Console.Error.WriteLine();
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000BC070 File Offset: 0x000BA270
		private static void StoreInCache(BuildProvider bp, Assembly compiledAssembly, CompilerResults results)
		{
			string virtualPath = bp.VirtualPath;
			BuildManagerCacheItem buildManagerCacheItem = new BuildManagerCacheItem(compiledAssembly, bp, results);
			if (BuildManager.buildCache.ContainsKey(virtualPath))
			{
				BuildManager.buildCache[virtualPath] = buildManagerCacheItem;
			}
			else
			{
				BuildManager.buildCache.Add(virtualPath, buildManagerCacheItem);
			}
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			CacheDependency cacheDependency;
			if (httpRequest != null)
			{
				IDictionary<string, bool> dictionary = bp.ExtractDependencies();
				List<string> list = new List<string>();
				string text = httpRequest.MapPath(virtualPath);
				if (File.Exists(text))
				{
					list.Add(text);
				}
				if (dictionary != null && dictionary.Count > 0)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
					{
						text = httpRequest.MapPath(keyValuePair.Key);
						if (File.Exists(text) && !list.Contains(text))
						{
							list.Add(text);
						}
					}
				}
				cacheDependency = new CacheDependency(list.ToArray());
			}
			else
			{
				cacheDependency = null;
			}
			HttpRuntime.InternalCache.Add("@@Build_Manager@@" + virtualPath, true, cacheDependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(BuildManager.OnVirtualPathChanged));
		}

		/// <summary>Gets a value that specifies whether the application is precompiled.</summary>
		/// <returns>true if the application is precompiled; otherwise, false.</returns>
		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x000BC1B4 File Offset: 0x000BA3B4
		public static bool IsPrecompiledApp
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that specifies whether the application is precompiled as updatable.</summary>
		/// <returns>true if the application is precompiled as updatable; otherwise, false.</returns>
		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x000BC1D0 File Offset: 0x000BA3D0
		public static bool IsUpdatablePrecompiledApp
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Specifies a string that represents a dependency that the build manager uses to help determine if a clean build is required.</summary>
		/// <param name="dependency">A string that represents a dependency.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="dependency" /> parameter is null or an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called after the Application_PreStartInit stage of the application.</exception>
		// Token: 0x060044B2 RID: 17586 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void AddCompilationDependency(string dependency)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002488 RID: 9352
		internal const string FAKE_VIRTUAL_PATH_PREFIX = "/@@MonoFakeVirtualPath@@";

		// Token: 0x04002489 RID: 9353
		private const string BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX = "@@Build_Manager@@";

		// Token: 0x0400248A RID: 9354
		private static int BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX_LENGTH = "@@Build_Manager@@".Length;

		// Token: 0x0400248B RID: 9355
		private static readonly object bigCompilationLock = new object();

		// Token: 0x0400248C RID: 9356
		private static readonly object virtualPathsToIgnoreLock = new object();

		// Token: 0x0400248D RID: 9357
		private static readonly char[] virtualPathsToIgnoreSplitChars = new char[] { ',' };

		// Token: 0x0400248E RID: 9358
		private static EventHandlerList events = new EventHandlerList();

		// Token: 0x0400248F RID: 9359
		private static object buildManagerRemoveEntryEvent = new object();

		// Token: 0x04002490 RID: 9360
		private static bool hosted = AppDomain.CurrentDomain.GetData(".:!MonoAspNetHostedApp!:.") as string == "yes";

		// Token: 0x04002491 RID: 9361
		private static Dictionary<string, bool> virtualPathsToIgnore;

		// Token: 0x04002492 RID: 9362
		private static bool virtualPathsToIgnoreChecked;

		// Token: 0x04002493 RID: 9363
		private static bool haveVirtualPathsToIgnore;

		// Token: 0x04002494 RID: 9364
		private static List<Assembly> AppCode_Assemblies = new List<Assembly>();

		// Token: 0x04002495 RID: 9365
		private static List<Assembly> TopLevel_Assemblies = new List<Assembly>();

		// Token: 0x04002496 RID: 9366
		private static Dictionary<Type, CodeDomProvider> codeDomProviders;

		// Token: 0x04002497 RID: 9367
		private static Dictionary<string, BuildManagerCacheItem> buildCache = new Dictionary<string, BuildManagerCacheItem>(RuntimeHelpers.StringEqualityComparer);

		// Token: 0x04002498 RID: 9368
		private static List<Assembly> referencedAssemblies = new List<Assembly>();

		// Token: 0x04002499 RID: 9369
		private static List<Assembly> configReferencedAssemblies;

		// Token: 0x0400249A RID: 9370
		private static bool getReferencedAssembliesInvoked;

		// Token: 0x0400249B RID: 9371
		private static int buildCount;

		// Token: 0x0400249C RID: 9372
		private static bool is_precompiled;

		// Token: 0x0400249D RID: 9373
		private static bool allowReferencedAssembliesCaching;

		// Token: 0x0400249E RID: 9374
		private static List<Assembly> dynamicallyRegisteredAssemblies;

		// Token: 0x0400249F RID: 9375
		private static bool? batchCompilationEnabled;

		// Token: 0x040024A0 RID: 9376
		private static FrameworkName targetFramework;

		// Token: 0x040024A1 RID: 9377
		private static bool preStartMethodsDone;

		// Token: 0x040024A2 RID: 9378
		private static bool preStartMethodsRunning;

		// Token: 0x040024A3 RID: 9379
		private static Dictionary<string, BuildManager.PreCompilationData> precompiled;

		// Token: 0x040024A4 RID: 9380
		internal static bool suppressDebugModeMessages;

		// Token: 0x040024A5 RID: 9381
		private static ReaderWriterLockSlim buildCacheLock = new ReaderWriterLockSlim();

		// Token: 0x040024A6 RID: 9382
		private static ulong recursionDepth = 0UL;

		// Token: 0x0200063A RID: 1594
		private class PreCompilationData
		{
			// Token: 0x040024A9 RID: 9385
			public string VirtualPath;

			// Token: 0x040024AA RID: 9386
			public string AssemblyFileName;

			// Token: 0x040024AB RID: 9387
			public string TypeName;

			// Token: 0x040024AC RID: 9388
			public Type Type;
		}
	}
}
