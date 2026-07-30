using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x02000593 RID: 1427
	public sealed class CompilationSection : ConfigurationSection
	{
		// Token: 0x06003C46 RID: 15430 RVA: 0x000A0B74 File Offset: 0x0009ED74
		static CompilationSection()
		{
			CompilationSection.properties.Add(CompilationSection.assembliesProp);
			CompilationSection.properties.Add(CompilationSection.assemblyPostProcessorTypeProp);
			CompilationSection.properties.Add(CompilationSection.batchProp);
			CompilationSection.properties.Add(CompilationSection.buildProvidersProp);
			CompilationSection.properties.Add(CompilationSection.batchTimeoutProp);
			CompilationSection.properties.Add(CompilationSection.codeSubDirectoriesProp);
			CompilationSection.properties.Add(CompilationSection.compilersProp);
			CompilationSection.properties.Add(CompilationSection.debugProp);
			CompilationSection.properties.Add(CompilationSection.defaultLanguageProp);
			CompilationSection.properties.Add(CompilationSection.expressionBuildersProp);
			CompilationSection.properties.Add(CompilationSection.explicitProp);
			CompilationSection.properties.Add(CompilationSection.maxBatchSizeProp);
			CompilationSection.properties.Add(CompilationSection.maxBatchGeneratedFileSizeProp);
			CompilationSection.properties.Add(CompilationSection.numRecompilesBeforeAppRestartProp);
			CompilationSection.properties.Add(CompilationSection.strictProp);
			CompilationSection.properties.Add(CompilationSection.tempDirectoryProp);
			CompilationSection.properties.Add(CompilationSection.urlLinePragmasProp);
			CompilationSection.properties.Add(CompilationSection.optimizeCompilationsProp);
			CompilationSection.properties.Add(CompilationSection.targetFrameworkProp);
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x0009FE7D File Offset: 0x0009E07D
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AssemblyCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.AssemblyCollection" /> that contains the assembly objects used during compilation of an ASP.NET resource.</returns>
		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000A0F13 File Offset: 0x0009F113
		[ConfigurationProperty("assemblies")]
		public AssemblyCollection Assemblies
		{
			get
			{
				return (AssemblyCollection)base[CompilationSection.assembliesProp];
			}
		}

		/// <summary>Gets or sets a value specifying a post-process compilation step for an assembly.</summary>
		/// <returns>A string value specifying the post-process compilation step for an assembly.</returns>
		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000A0F25 File Offset: 0x0009F125
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000A0F37 File Offset: 0x0009F137
		[ConfigurationProperty("assemblyPostProcessorType", DefaultValue = "")]
		public string AssemblyPostProcessorType
		{
			get
			{
				return (string)base[CompilationSection.assemblyPostProcessorTypeProp];
			}
			set
			{
				base[CompilationSection.assemblyPostProcessorTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether batch compilation is attempted.</summary>
		/// <returns>true if batch compilation is attempted; otherwise, false. The default is true.</returns>
		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000A0F45 File Offset: 0x0009F145
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000A0F57 File Offset: 0x0009F157
		[ConfigurationProperty("batch", DefaultValue = "True")]
		public bool Batch
		{
			get
			{
				return (bool)base[CompilationSection.batchProp];
			}
			set
			{
				base[CompilationSection.batchProp] = value;
			}
		}

		/// <summary>Gets or sets the time-out period, in seconds, for batch compilation.</summary>
		/// <returns>A value indicating the amount of time in seconds granted for batch compilation to occur.  </returns>
		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000A0F6A File Offset: 0x0009F16A
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000A0F7C File Offset: 0x0009F17C
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00")]
		[ConfigurationProperty("batchTimeout", DefaultValue = "00:15:00")]
		public TimeSpan BatchTimeout
		{
			get
			{
				return (TimeSpan)base[CompilationSection.batchTimeoutProp];
			}
			set
			{
				base[CompilationSection.batchTimeoutProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProviderCollection" />  collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProviderCollection" /> that contains the build providers used during a compilation.</returns>
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000A0F8F File Offset: 0x0009F18F
		[ConfigurationProperty("buildProviders")]
		public BuildProviderCollection BuildProviders
		{
			get
			{
				return (BuildProviderCollection)base[CompilationSection.buildProvidersProp];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection that contains an ordered collection of subdirectories containing files compiled at run time.</returns>
		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x000A0FA1 File Offset: 0x0009F1A1
		[ConfigurationProperty("codeSubDirectories")]
		public CodeSubDirectoriesCollection CodeSubDirectories
		{
			get
			{
				return (CodeSubDirectoriesCollection)base[CompilationSection.codeSubDirectoriesProp];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CompilerCollection" /> collection that contains a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects.</returns>
		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000A0FB3 File Offset: 0x0009F1B3
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CompilationSection.compilersProp];
			}
		}

		/// <summary>Gets or sets a value specifying whether to compile release binaries or debug binaries. </summary>
		/// <returns>true if debug binaries will be used for compilation; otherwise, false. false specifies that release binaries will be used for compilation. The default is false.</returns>
		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x000A0FC5 File Offset: 0x0009F1C5
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x000A0FD7 File Offset: 0x0009F1D7
		[ConfigurationProperty("debug", DefaultValue = "False")]
		public bool Debug
		{
			get
			{
				return (bool)base[CompilationSection.debugProp];
			}
			set
			{
				base[CompilationSection.debugProp] = value;
			}
		}

		/// <summary>Gets or sets the default programming language to use in dynamic-compilation files.</summary>
		/// <returns>A value specifying the default programming language to use in dynamic-compilation files.</returns>
		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000A0FEA File Offset: 0x0009F1EA
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x000A0FFC File Offset: 0x0009F1FC
		[ConfigurationProperty("defaultLanguage", DefaultValue = "vb")]
		public string DefaultLanguage
		{
			get
			{
				return (string)base[CompilationSection.defaultLanguageProp];
			}
			set
			{
				base[CompilationSection.defaultLanguageProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the Microsoft Visual Basic explicit compile option.</summary>
		/// <returns>true if the Visual Basic explicit compile option is enabled; otherwise, false. false specifies that the Visual Basic explicit compile option is disabled. The default is true.</returns>
		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000A100A File Offset: 0x0009F20A
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x000A101C File Offset: 0x0009F21C
		[ConfigurationProperty("explicit", DefaultValue = "True")]
		public bool Explicit
		{
			get
			{
				return (bool)base[CompilationSection.explicitProp];
			}
			set
			{
				base[CompilationSection.explicitProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> that contains <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects.</returns>
		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000A102F File Offset: 0x0009F22F
		[ConfigurationProperty("expressionBuilders")]
		public ExpressionBuilderCollection ExpressionBuilders
		{
			get
			{
				return (ExpressionBuilderCollection)base[CompilationSection.expressionBuildersProp];
			}
		}

		/// <summary>Gets or sets the maximum combined size of the generated source files per batched compilation.</summary>
		/// <returns>An integer value indicating the maximum combined size of the generated source files per batched compilation.</returns>
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000A1041 File Offset: 0x0009F241
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000A1053 File Offset: 0x0009F253
		[ConfigurationProperty("maxBatchGeneratedFileSize", DefaultValue = "1000")]
		public int MaxBatchGeneratedFileSize
		{
			get
			{
				return (int)base[CompilationSection.maxBatchGeneratedFileSizeProp];
			}
			set
			{
				base[CompilationSection.maxBatchGeneratedFileSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of pages per batched compilation.</summary>
		/// <returns>An integer value indicating the maximum number of pages that will be compiled into a single batch. The default number of pages is 1000.</returns>
		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000A1066 File Offset: 0x0009F266
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000A1078 File Offset: 0x0009F278
		[ConfigurationProperty("maxBatchSize", DefaultValue = "1000")]
		public int MaxBatchSize
		{
			get
			{
				return (int)base[CompilationSection.maxBatchSizeProp];
			}
			set
			{
				base[CompilationSection.maxBatchSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the number of dynamic recompiles of resources that can occur before the application restarts.</summary>
		/// <returns>A value indicating the number of dynamic recompiles of resources that can occur before the application restarts. The default is 15 recompilations.</returns>
		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000A108B File Offset: 0x0009F28B
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000A109D File Offset: 0x0009F29D
		[ConfigurationProperty("numRecompilesBeforeAppRestart", DefaultValue = "15")]
		public int NumRecompilesBeforeAppRestart
		{
			get
			{
				return (int)base[CompilationSection.numRecompilesBeforeAppRestartProp];
			}
			set
			{
				base[CompilationSection.numRecompilesBeforeAppRestartProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the compilation must be optimized.</summary>
		/// <returns>true if the compilation must be optimized; otherwise, false. The default is false. </returns>
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000A10B0 File Offset: 0x0009F2B0
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x000A10C2 File Offset: 0x0009F2C2
		[ConfigurationProperty("optimizeCompilations", DefaultValue = "False")]
		public bool OptimizeCompilations
		{
			get
			{
				return (bool)base[CompilationSection.optimizeCompilationsProp];
			}
			set
			{
				base[CompilationSection.optimizeCompilationsProp] = value;
			}
		}

		/// <summary>Gets or sets the Visual Basic strict compile option.</summary>
		/// <returns>true if the Visual Basic strict compile option is used; otherwise, false. The default is true. </returns>
		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000A10D5 File Offset: 0x0009F2D5
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000A10E7 File Offset: 0x0009F2E7
		[ConfigurationProperty("strict", DefaultValue = "False")]
		public bool Strict
		{
			get
			{
				return (bool)base[CompilationSection.strictProp];
			}
			set
			{
				base[CompilationSection.strictProp] = value;
			}
		}

		/// <summary>Gets or sets the version of the .NET Framework that the Web site targets. </summary>
		/// <returns>The version of the .NET Framework that the Web site targets. The default value is null.</returns>
		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000A10FA File Offset: 0x0009F2FA
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x000A110C File Offset: 0x0009F30C
		[ConfigurationProperty("targetFramework", DefaultValue = null)]
		public string TargetFramework
		{
			get
			{
				return (string)base[CompilationSection.targetFrameworkProp];
			}
			set
			{
				base[CompilationSection.targetFrameworkProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the directory to use for temporary file storage during compilation.</summary>
		/// <returns>A value specifying the directory to use for temporary file storage during compilation.</returns>
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000A111A File Offset: 0x0009F31A
		// (set) Token: 0x06003C68 RID: 15464 RVA: 0x000A112C File Offset: 0x0009F32C
		[ConfigurationProperty("tempDirectory", DefaultValue = "")]
		public string TempDirectory
		{
			get
			{
				return (string)base[CompilationSection.tempDirectoryProp];
			}
			set
			{
				base[CompilationSection.tempDirectoryProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether instructions to the compiler use physical paths or URLs.</summary>
		/// <returns>true if instructions to the compiler use URLs rather than physical paths; otherwise, false. The default is false. </returns>
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000A113A File Offset: 0x0009F33A
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000A114C File Offset: 0x0009F34C
		[ConfigurationProperty("urlLinePragmas", DefaultValue = "False")]
		public bool UrlLinePragmas
		{
			get
			{
				return (bool)base[CompilationSection.urlLinePragmasProp];
			}
			set
			{
				base[CompilationSection.urlLinePragmasProp] = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000A115F File Offset: 0x0009F35F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilationSection.properties;
			}
		}

		/// <summary>Gets or sets a string representing the object type used to intercept a <see cref="T:System.Web.UI.ControlBuilder" /> object and configure a container.</summary>
		/// <returns>A string representing the object type used to intercept a <see cref="T:System.Web.UI.ControlBuilder" /> object.</returns>
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003C6D RID: 15469 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ControlBuilderInterceptorType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets whether the "disableObsoleteWarnings" configuration value in the Compilation section is set.</summary>
		/// <returns>true if the "disableObsoleteWarnings" configuration value in the Compilation section is set; otherwise, false.</returns>
		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x000A1168 File Offset: 0x0009F368
		// (set) Token: 0x06003C6F RID: 15471 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool DisableObsoleteWarnings
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether an ASP.NET application can take advantage of Windows 8 prefetch functionality.</summary>
		/// <returns>true if an ASP.NET application can take advantage of Windows 8 prefetch functionality; otherwise, false. The default is false.</returns>
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000A1184 File Offset: 0x0009F384
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool EnablePrefetchOptimization
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class, which represents the build providers that are used during compilation.</summary>
		/// <returns>The build providers that are used during compilation.</returns>
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public FolderLevelBuildProviderCollection FolderLevelBuildProviders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets whether the " maxConcurrentCompilations" configuration value in the Compilation section is set.</summary>
		/// <returns>true if the " maxConcurrentCompilations" configuration value in the Compilation section is set; otherwise, false.</returns>
		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000A11A0 File Offset: 0x0009F3A0
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int MaxConcurrentCompilations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the application is optimized for the deployed environment.</summary>
		/// <returns>A value that indicates whether the application is optimized for the deployed environment.</returns>
		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000A11BC File Offset: 0x0009F3BC
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProfileGuidedOptimizationsFlags ProfileGuidedOptimizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ProfileGuidedOptimizationsFlags.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040020B5 RID: 8373
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020B6 RID: 8374
		private static ConfigurationProperty compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020B7 RID: 8375
		private static ConfigurationProperty tempDirectoryProp = new ConfigurationProperty("tempDirectory", typeof(string), "");

		// Token: 0x040020B8 RID: 8376
		private static ConfigurationProperty debugProp = new ConfigurationProperty("debug", typeof(bool), false);

		// Token: 0x040020B9 RID: 8377
		private static ConfigurationProperty strictProp = new ConfigurationProperty("strict", typeof(bool), false);

		// Token: 0x040020BA RID: 8378
		private static ConfigurationProperty explicitProp = new ConfigurationProperty("explicit", typeof(bool), true);

		// Token: 0x040020BB RID: 8379
		private static ConfigurationProperty batchProp = new ConfigurationProperty("batch", typeof(bool), true);

		// Token: 0x040020BC RID: 8380
		private static ConfigurationProperty batchTimeoutProp = new ConfigurationProperty("batchTimeout", typeof(TimeSpan), new TimeSpan(0, 15, 0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020BD RID: 8381
		private static ConfigurationProperty maxBatchSizeProp = new ConfigurationProperty("maxBatchSize", typeof(int), 1000);

		// Token: 0x040020BE RID: 8382
		private static ConfigurationProperty maxBatchGeneratedFileSizeProp = new ConfigurationProperty("maxBatchGeneratedFileSize", typeof(int), 3000);

		// Token: 0x040020BF RID: 8383
		private static ConfigurationProperty numRecompilesBeforeAppRestartProp = new ConfigurationProperty("numRecompilesBeforeAppRestart", typeof(int), 15);

		// Token: 0x040020C0 RID: 8384
		private static ConfigurationProperty defaultLanguageProp = new ConfigurationProperty("defaultLanguage", typeof(string), "vb");

		// Token: 0x040020C1 RID: 8385
		private static ConfigurationProperty assembliesProp = new ConfigurationProperty("assemblies", typeof(AssemblyCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020C2 RID: 8386
		private static ConfigurationProperty assemblyPostProcessorTypeProp = new ConfigurationProperty("assemblyPostProcessorType", typeof(string), "");

		// Token: 0x040020C3 RID: 8387
		private static ConfigurationProperty buildProvidersProp = new ConfigurationProperty("buildProviders", typeof(BuildProviderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020C4 RID: 8388
		private static ConfigurationProperty expressionBuildersProp = new ConfigurationProperty("expressionBuilders", typeof(ExpressionBuilderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020C5 RID: 8389
		private static ConfigurationProperty urlLinePragmasProp = new ConfigurationProperty("urlLinePragmas", typeof(bool), false);

		// Token: 0x040020C6 RID: 8390
		private static ConfigurationProperty codeSubDirectoriesProp = new ConfigurationProperty("codeSubDirectories", typeof(CodeSubDirectoriesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020C7 RID: 8391
		private static ConfigurationProperty optimizeCompilationsProp = new ConfigurationProperty("optimizeCompilations", typeof(bool), false);

		// Token: 0x040020C8 RID: 8392
		private static ConfigurationProperty targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(string), null);
	}
}
