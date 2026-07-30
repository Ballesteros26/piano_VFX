using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web.Hosting;
using System.Web.Util;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Provides compilation services for ASP.NET applications.</summary>
	// Token: 0x02000649 RID: 1609
	public sealed class ClientBuildManager : MarshalByRefObject, IDisposable
	{
		/// <summary>Occurs when an application domain is shut down. </summary>
		// Token: 0x14000112 RID: 274
		// (add) Token: 0x0600451E RID: 17694 RVA: 0x000BD862 File Offset: 0x000BBA62
		// (remove) Token: 0x0600451F RID: 17695 RVA: 0x000BD875 File Offset: 0x000BBA75
		public event BuildManagerHostUnloadEventHandler AppDomainShutdown
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainShutdownEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainShutdownEvent, value);
			}
		}

		/// <summary>Occurs when an application domain is started. </summary>
		// Token: 0x14000113 RID: 275
		// (add) Token: 0x06004520 RID: 17696 RVA: 0x000BD888 File Offset: 0x000BBA88
		// (remove) Token: 0x06004521 RID: 17697 RVA: 0x000BD89B File Offset: 0x000BBA9B
		public event EventHandler AppDomainStarted
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainStartedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainStartedEvent, value);
			}
		}

		/// <summary>Occurs when an application domain is unloaded. </summary>
		// Token: 0x14000114 RID: 276
		// (add) Token: 0x06004522 RID: 17698 RVA: 0x000BD8AE File Offset: 0x000BBAAE
		// (remove) Token: 0x06004523 RID: 17699 RVA: 0x000BD8C1 File Offset: 0x000BBAC1
		public event BuildManagerHostUnloadEventHandler AppDomainUnloaded
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainUnloadedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainUnloadedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class without a specified target directory or precompilation flags. </summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		// Token: 0x06004524 RID: 17700 RVA: 0x000BD8D4 File Offset: 0x000BBAD4
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir)
		{
			this.events = new EventHandlerList();
			base..ctor();
			if (appVirtualDir == null || appVirtualDir == "")
			{
				throw new ArgumentNullException("appVirtualDir");
			}
			if (appPhysicalSourceDir == null || appPhysicalSourceDir == "")
			{
				throw new ArgumentNullException("appPhysicalSourceDir");
			}
			this.virt_dir = appVirtualDir;
			this.phys_src_dir = appPhysicalSourceDir;
			this.manager = ApplicationManager.GetApplicationManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory. </summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		// Token: 0x06004525 RID: 17701 RVA: 0x000BD941 File Offset: 0x000BBB41
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir)
			: this(appVirtualDir, appPhysicalSourceDir)
		{
			if (appPhysicalTargetDir == null || appPhysicalTargetDir == "")
			{
				throw new ArgumentNullException("appPhysicalTargetDir");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory and compilation parameter.</summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		/// <param name="parameter">Values that determine the precompilation behavior.</param>
		// Token: 0x06004526 RID: 17702 RVA: 0x000BD966 File Offset: 0x000BBB66
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter)
			: this(appVirtualDir, appPhysicalSourceDir, appPhysicalTargetDir)
		{
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000BD974 File Offset: 0x000BBB74
		private BareApplicationHost Host
		{
			get
			{
				if (this.host != null)
				{
					return this.host;
				}
				int num = this.virt_dir.GetHashCode();
				if (this.app_id != null)
				{
					num ^= int.Parse(this.app_id);
				}
				this.app_id = num.ToString(Helpers.InvariantCulture);
				this.host = this.manager.CreateHostWithCheck(this.app_id, this.virt_dir, this.phys_src_dir);
				this.cache_path = "";
				int num2 = this.virt_dir.GetHashCode() << 5 + this.phys_src_dir.GetHashCode();
				this.cache_path = Path.Combine(this.cache_path, num2.ToString(Helpers.InvariantCulture));
				Directory.CreateDirectory(this.cache_path);
				this.OnAppDomainStarted();
				return this.host;
			}
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x000BDA44 File Offset: 0x000BBC44
		private void OnAppDomainStarted()
		{
			EventHandler eventHandler = this.events[ClientBuildManager.appDomainStartedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000BDA78 File Offset: 0x000BBC78
		private void OnAppDomainShutdown(ApplicationShutdownReason reason)
		{
			BuildManagerHostUnloadEventHandler buildManagerHostUnloadEventHandler = this.events[ClientBuildManager.appDomainShutdownEvent] as BuildManagerHostUnloadEventHandler;
			if (buildManagerHostUnloadEventHandler != null)
			{
				BuildManagerHostUnloadEventArgs buildManagerHostUnloadEventArgs = new BuildManagerHostUnloadEventArgs(reason);
				buildManagerHostUnloadEventHandler(this, buildManagerHostUnloadEventArgs);
			}
		}

		/// <summary>Compiles application-dependent files, such as files in the App_Code directory, the Global.asax file, resource files, and Web references.</summary>
		// Token: 0x0600452A RID: 17706 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void CompileApplicationDependencies()
		{
			throw new NotImplementedException();
		}

		/// <summary>Compiles the file represented by the virtual path.</summary>
		/// <param name="virtualPath">The path to the file to be compiled.</param>
		// Token: 0x0600452B RID: 17707 RVA: 0x000BDAAD File Offset: 0x000BBCAD
		public void CompileFile(string virtualPath)
		{
			this.CompileFile(virtualPath, null);
		}

		/// <summary>Compiles the file represented by the virtual path and provides a callback class to receive status information about the build.</summary>
		/// <param name="virtualPath">The path to the file to be compiled.</param>
		/// <param name="callback">The object to receive status information from compilation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x0600452C RID: 17708 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void CompileFile(string virtualPath, ClientBuildManagerCallback callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object in the application domain of the ASP.NET runtime.</summary>
		/// <returns>An object in the application domain of the ASP.NET runtime.</returns>
		/// <param name="type">The type of object to be created.</param>
		/// <param name="failIfExists">true to throw an exception if the object has already been created in the application domain of the ASP.NET runtime; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The object already exists in the application domain and <paramref name="failIfExists" /> is true.</exception>
		// Token: 0x0600452D RID: 17709 RVA: 0x000BDAB7 File Offset: 0x000BBCB7
		public IRegisteredObject CreateObject(Type type, bool failIfExists)
		{
			return this.manager.CreateObject(this.app_id, type, this.virt_dir, this.phys_src_dir, failIfExists);
		}

		/// <summary>Generates code from the contents of a file.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the generated code.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="virtualFileString">The contents of the file.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x0600452E RID: 17710 RVA: 0x000BDAD8 File Offset: 0x000BBCD8
		[global::System.MonoTODO("Currently does not return the GeneratedCode")]
		public string GenerateCode(string virtualPath, string virtualFileString, out IDictionary linePragmasTable)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			Type type;
			CompilerParameters compilerParameters;
			this.GenerateCodeCompileUnit(virtualPath, virtualFileString, out type, out compilerParameters, out linePragmasTable);
			return null;
		}

		/// <summary>Returns the contents, codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="virtualFileString">The contents of the file represented by the <paramref name="virtualPath" /> parameter.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file represented by the <paramref name="virtualPath" /> parameter will be compiled.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x0600452F RID: 17711 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, string virtualFileString, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		// Token: 0x06004530 RID: 17712 RVA: 0x000BDB07 File Offset: 0x000BBD07
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			return this.GenerateCodeCompileUnit(virtualPath, out codeDomProviderType, out compilerParameters, out linePragmasTable);
		}

		/// <summary>Gets the directories with files that, when changed, cause the application domain to shut down.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing the top-level directory names.</returns>
		// Token: 0x06004531 RID: 17713 RVA: 0x000BDB14 File Offset: 0x000BBD14
		public string[] GetAppDomainShutdownDirectories()
		{
			return ClientBuildManager.shutdown_directories;
		}

		/// <summary>Gets a collection of browser elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing browser elements.</returns>
		// Token: 0x06004532 RID: 17714 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public IDictionary GetBrowserDefinitions()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets information about the compiler type, compiler parameters, and the directory in which to store code files generated from non-code files such as .wsdl files.</summary>
		/// <param name="virtualCodeDir">The directory about which to retrieve information.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <param name="generatedFilesDir">When this method returns, contains the directory for files generated from non-code files.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualCodeDir" /> is null.</exception>
		// Token: 0x06004533 RID: 17715 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void GetCodeDirectoryInformation(string virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			throw new NotImplementedException();
		}

		/// <summary>Compiles the file represented by the virtual path and returns its compiled type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the compiled file.</returns>
		/// <param name="virtualPath">The virtual path of the file to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06004534 RID: 17716 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public Type GetCompiledType(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the compiler type and parameters that are used to build a file represented by a virtual path.</summary>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06004535 RID: 17717 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void GetCompilerParameters(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the virtual path of a generated file.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the virtual path for <paramref name="filePath" />.</returns>
		/// <param name="filePath">The full physical path to a generated file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePath" /> is null.</exception>
		// Token: 0x06004536 RID: 17718 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string GetGeneratedFileVirtualPath(string filePath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the physical path to the generated file for a virtual path. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the physical path to the generated file.</returns>
		/// <param name="virtualPath">The virtual path of the file to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06004537 RID: 17719 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string GetGeneratedSourceFile(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an array of the assemblies defined in the Bin directory and the &lt;assembly&gt; section of the Web configuration file.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing paths to code bases in the Bin directory and the &lt;assembly&gt; section of the Web configuration file. </returns>
		/// <param name="virtualPath">The configuration name and path.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06004538 RID: 17720 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string[] GetTopLevelAssemblyReferences(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the virtual paths to the App_Code directory and its subdirectories in an ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing all the virtual paths to code directories in an application.</returns>
		// Token: 0x06004539 RID: 17721 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string[] GetVirtualCodeDirectories()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gives the application domain an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x0600453A RID: 17722 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Indicates whether an assembly is a code assembly.</summary>
		/// <returns>true if the <paramref name="assemblyName" /> parameter matches one of the generated code assemblies; otherwise, false.</returns>
		/// <param name="assemblyName">The name of the assembly to be identified as a code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.</exception>
		// Token: 0x0600453B RID: 17723 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool IsCodeAssembly(string assemblyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application.</summary>
		// Token: 0x0600453C RID: 17724 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication()
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application and provides a callback method to receive status information about the build.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
		// Token: 0x0600453D RID: 17725 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication(ClientBuildManagerCallback callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application, provides a callback method to receive status information about the build, and indicates whether to create a clean build.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
		/// <param name="forceCleanBuild">true to perform a clean build, which will first delete all object and intermediate files; false to rebuild only those files that have changed. Set to true if there is a chance that a dependency might not be picked up by the build environment.</param>
		// Token: 0x0600453E RID: 17726 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication(ClientBuildManagerCallback callback, bool forceCleanBuild)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unloads the application domain for compiling ASP.NET Web applications.</summary>
		/// <returns>true if the application domain is unloaded; otherwise, false.</returns>
		// Token: 0x0600453F RID: 17727 RVA: 0x000BDB1B File Offset: 0x000BBD1B
		public bool Unload()
		{
			if (this.host != null)
			{
				this.host.Shutdown();
				this.OnAppDomainShutdown(ApplicationShutdownReason.None);
				this.host = null;
			}
			return true;
		}

		/// <summary>Gets the physical path to the directory used for code generation.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the directory path used for code generation.</returns>
		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x000BDB3F File Offset: 0x000BBD3F
		public string CodeGenDir
		{
			get
			{
				return this.Host.GetCodeGenDir();
			}
		}

		/// <summary>Gets a value that indicates whether an application domain for compiling ASP.NET Web applications has been created.</summary>
		/// <returns>true if the application domain for compiling ASP.NET Web applications has been created; otherwise, false.</returns>
		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x000BDB4C File Offset: 0x000BBD4C
		public bool IsHostCreated
		{
			get
			{
				return this.host != null;
			}
		}

		/// <summary>Terminates the current ASP.NET application.</summary>
		// Token: 0x06004542 RID: 17730 RVA: 0x000BDB57 File Offset: 0x000BBD57
		void IDisposable.Dispose()
		{
			this.Unload();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class by using the specified virtual directory, source directory, target directory, compilation parameter, and type-description provider.</summary>
		/// <param name="appVirtualDir">The virtual path of the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path of the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		/// <param name="parameter">Values that determine the precompilation behavior.</param>
		/// <param name="typeDescriptionProvider">The type-description provider to use. This parameter is primarily used to support the multi-targeting infrastructure in Visual Studio. It is used to retrieve metadata about types that is filtered for specific versions of the .NET Framework.</param>
		// Token: 0x06004544 RID: 17732 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter, TypeDescriptionProvider typeDescriptionProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024D0 RID: 9424
		private static readonly object appDomainShutdownEvent = new object();

		// Token: 0x040024D1 RID: 9425
		private static readonly object appDomainStartedEvent = new object();

		// Token: 0x040024D2 RID: 9426
		private static readonly object appDomainUnloadedEvent = new object();

		// Token: 0x040024D3 RID: 9427
		private string virt_dir;

		// Token: 0x040024D4 RID: 9428
		private string phys_src_dir;

		// Token: 0x040024D5 RID: 9429
		private BareApplicationHost host;

		// Token: 0x040024D6 RID: 9430
		private ApplicationManager manager;

		// Token: 0x040024D7 RID: 9431
		private string app_id;

		// Token: 0x040024D8 RID: 9432
		private string cache_path;

		// Token: 0x040024D9 RID: 9433
		private EventHandlerList events;

		// Token: 0x040024DA RID: 9434
		private static string[] shutdown_directories = new string[] { "bin", "App_GlobalResources", "App_Code", "App_WebReferences", "App_Browsers" };
	}
}
