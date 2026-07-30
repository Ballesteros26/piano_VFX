using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Serves as the abstract base class for ASP.NET file parsers. </summary>
	// Token: 0x02000238 RID: 568
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class TemplateParser : BaseParser
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x0003E8AC File Offset: 0x0003CAAC
		internal TemplateParser()
		{
			this.imports = new Dictionary<string, bool>(StringComparer.Ordinal);
			this.LoadConfigDefaults();
			this.assemblies = new List<string>();
			CompilationSection compilationConfig = base.CompilationConfig;
			foreach (object obj in compilationConfig.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				if (assemblyInfo.Assembly != "*")
				{
					this.AddAssemblyByName(assemblyInfo.Assembly);
				}
			}
			this.language = compilationConfig.DefaultLanguage;
			this.implicitLanguage = true;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0003E974 File Offset: 0x0003CB74
		internal virtual void LoadConfigDefaults()
		{
			this.AddNamespaces(this.imports);
			this.debug = base.CompilationConfig.Debug;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0003E994 File Offset: 0x0003CB94
		internal void AddApplicationAssembly()
		{
			if (base.Context.ApplicationInstance == null)
			{
				return;
			}
			string assemblyLocation = base.Context.ApplicationInstance.AssemblyLocation;
			if (assemblyLocation != typeof(TemplateParser).Assembly.Location)
			{
				this.assemblies.Add(assemblyLocation);
				this.appAssemblyIndex = this.assemblies.Count - 1;
			}
		}

		// Token: 0x06001747 RID: 5959
		internal abstract Type CompileIntoType();

		// Token: 0x06001748 RID: 5960 RVA: 0x0003E9FC File Offset: 0x0003CBFC
		internal void AddControl(Type type, IDictionary attributes)
		{
			AspGenerator aspGenerator = this.AspGenerator;
			if (aspGenerator == null)
			{
				return;
			}
			aspGenerator.AddControl(type, attributes);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0003EA1C File Offset: 0x0003CC1C
		private void AddNamespaces(Dictionary<string, bool> imports)
		{
			if (BuildManager.HaveResources)
			{
				imports.Add("System.Resources", true);
			}
			PagesSection pagesConfig = this.PagesConfig;
			if (pagesConfig == null)
			{
				return;
			}
			NamespaceCollection namespaces = pagesConfig.Namespaces;
			if (namespaces == null || namespaces.Count == 0)
			{
				return;
			}
			foreach (object obj in namespaces)
			{
				string @namespace = ((NamespaceInfo)obj).Namespace;
				if (!imports.ContainsKey(@namespace))
				{
					imports.Add(@namespace, true);
				}
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		internal void RegisterCustomControl(string tagPrefix, string tagName, string src)
		{
			string text = null;
			bool flag = false;
			VirtualFile virtualFile = null;
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			VirtualPath virtualPath = new VirtualPath(src, this.BaseVirtualDir);
			string text2 = virtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, virtualPath.Absolute);
			if (virtualPathProvider.FileExists(text2))
			{
				flag = true;
				virtualFile = virtualPathProvider.GetFile(text2);
				if (virtualFile != null)
				{
					text = base.MapPath(virtualFile.VirtualPath);
				}
			}
			if (!flag)
			{
				base.ThrowParseFileNotFound(src, Array.Empty<object>());
			}
			if (string.Compare(text, this.inputFile, StringComparison.Ordinal) == 0)
			{
				return;
			}
			string virtualPath2 = virtualFile.VirtualPath;
			try
			{
				this.RegisterTagName(tagPrefix + ":" + tagName);
				this.RootBuilder.Foundry.RegisterFoundry(tagPrefix, tagName, virtualPath2);
				this.AddDependency(virtualPath2);
			}
			catch (ParseException ex)
			{
				if (this is UserControlParser)
				{
					throw new ParseException(base.Location, ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
		internal void RegisterNamespace(string tagPrefix, string ns, string assembly)
		{
			this.AddImport(ns);
			Assembly assembly2 = null;
			if (assembly != null && assembly.Length > 0)
			{
				assembly2 = this.AddAssemblyByName(assembly);
			}
			this.RootBuilder.Foundry.RegisterFoundry(tagPrefix, assembly2, ns);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0000393A File Offset: 0x00001B3A
		internal virtual void HandleOptions(object obj)
		{
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0003EBE4 File Offset: 0x0003CDE4
		internal static string GetOneKey(IDictionary tbl)
		{
			using (IEnumerator enumerator = tbl.Keys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.ToString();
				}
			}
			return null;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		internal virtual void AddDirective(string directive, IDictionary atts)
		{
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (string.Compare(directive, this.DefaultDirectiveName, true, Helpers.InvariantCulture) == 0)
			{
				bool flag = this.allowedMainDirectives > 0;
				if (this.mainAttributes != null && !flag)
				{
					base.ThrowParseException("Only 1 " + this.DefaultDirectiveName + " is allowed", Array.Empty<object>());
				}
				this.allowedMainDirectives--;
				if (this.mainAttributes != null)
				{
					return;
				}
				if (pageParserFilter != null)
				{
					pageParserFilter.PreprocessDirective(directive.ToLower(Helpers.InvariantCulture), atts);
				}
				this.mainAttributes = atts;
				this.ProcessMainAttributes(this.mainAttributes);
				return;
			}
			else
			{
				if (pageParserFilter != null)
				{
					pageParserFilter.PreprocessDirective(directive.ToLower(Helpers.InvariantCulture), atts);
				}
				if (string.Compare("Assembly", directive, true, Helpers.InvariantCulture) == 0)
				{
					string @string = BaseParser.GetString(atts, "Name", null);
					string string2 = BaseParser.GetString(atts, "Src", null);
					if (atts.Count > 0)
					{
						base.ThrowParseException("Attribute " + TemplateParser.GetOneKey(atts) + " unknown.", Array.Empty<object>());
					}
					if (@string == null && string2 == null)
					{
						base.ThrowParseException("You gotta specify Src or Name", Array.Empty<object>());
					}
					if (@string != null && string2 != null)
					{
						base.ThrowParseException("Src and Name cannot be used together", Array.Empty<object>());
					}
					if (@string != null)
					{
						this.AddAssemblyByName(@string);
						return;
					}
					this.GetAssemblyFromSource(string2);
					return;
				}
				else
				{
					if (string.Compare("Import", directive, true, Helpers.InvariantCulture) == 0)
					{
						string string3 = BaseParser.GetString(atts, "Namespace", null);
						if (atts.Count > 0)
						{
							base.ThrowParseException("Attribute " + TemplateParser.GetOneKey(atts) + " unknown.", Array.Empty<object>());
						}
						this.AddImport(string3);
						return;
					}
					if (string.Compare("Implements", directive, true, Helpers.InvariantCulture) == 0)
					{
						string string4 = BaseParser.GetString(atts, "Interface", "");
						if (atts.Count > 0)
						{
							base.ThrowParseException("Attribute " + TemplateParser.GetOneKey(atts) + " unknown.", Array.Empty<object>());
						}
						Type type = this.LoadType(string4);
						if (type == null)
						{
							base.ThrowParseException("Cannot find type " + string4, Array.Empty<object>());
						}
						if (!type.IsInterface)
						{
							base.ThrowParseException(type + " is not an interface", Array.Empty<object>());
						}
						this.AddInterface(type.FullName);
						return;
					}
					if (string.Compare("OutputCache", directive, true, Helpers.InvariantCulture) == 0)
					{
						HttpResponse response = HttpContext.Current.Response;
						if (response != null)
						{
							response.Cache.SetValidUntilExpires(true);
						}
						this.output_cache = true;
						this.ProcessOutputCacheAttributes(atts);
						return;
					}
					base.ThrowParseException("Unknown directive: " + directive, Array.Empty<object>());
					return;
				}
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		internal virtual void ProcessOutputCacheAttributes(IDictionary atts)
		{
			if (atts["Duration"] == null)
			{
				base.ThrowParseException("The directive is missing a 'duration' attribute.", Array.Empty<object>());
			}
			if (atts["VaryByParam"] == null && atts["VaryByControl"] == null)
			{
				base.ThrowParseException("This directive is missing 'VaryByParam' or 'VaryByControl' attribute, which should be set to \"none\", \"*\", or a list of name/value pairs.", Array.Empty<object>());
			}
			foreach (object obj in atts)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (text != null)
				{
					string text2 = text.ToLower(Helpers.InvariantCulture);
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text2);
					if (num <= 1999241327U)
					{
						if (num <= 799079693U)
						{
							if (num != 200649126U)
							{
								if (num != 799079693U)
								{
									goto IL_0425;
								}
								if (!(text2 == "duration"))
								{
									goto IL_0425;
								}
								this.oc_duration = int.Parse((string)dictionaryEntry.Value);
								if (this.oc_duration < 1)
								{
									base.ThrowParseException("The 'duration' attribute must be set to a positive integer value", Array.Empty<object>());
									continue;
								}
								continue;
							}
							else
							{
								if (!(text2 == "location"))
								{
									goto IL_0425;
								}
								if (!(this is PageParser))
								{
									goto IL_0425;
								}
								try
								{
									this.oc_location = (OutputCacheLocation)Enum.Parse(typeof(OutputCacheLocation), (string)dictionaryEntry.Value, true);
									this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.Location;
									continue;
								}
								catch
								{
									base.ThrowParseException("The 'location' attribute is case sensitive and must be one of the following values: Any, Client, Downstream, Server, None, ServerAndClient.", Array.Empty<object>());
									continue;
								}
							}
						}
						else if (num != 811325582U)
						{
							if (num != 1534393705U)
							{
								if (num != 1999241327U)
								{
									goto IL_0425;
								}
								if (!(text2 == "varybycustom"))
								{
									goto IL_0425;
								}
								this.oc_custom = (string)dictionaryEntry.Value;
								this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.VaryByCustom;
								continue;
							}
							else if (!(text2 == "varybycontrol"))
							{
								goto IL_0425;
							}
						}
						else
						{
							if (!(text2 == "cacheprofile"))
							{
								goto IL_0425;
							}
							goto IL_02A1;
						}
						this.oc_controls = (string)dictionaryEntry.Value;
						this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.VaryByControl;
						continue;
					}
					if (num <= 2767733972U)
					{
						if (num != 2100197691U)
						{
							if (num != 2512901937U)
							{
								if (num != 2767733972U)
								{
									goto IL_0425;
								}
								if (!(text2 == "shared"))
								{
									goto IL_0425;
								}
								if (!(this is PageParser))
								{
									try
									{
										this.oc_shared = bool.Parse((string)dictionaryEntry.Value);
										continue;
									}
									catch
									{
										base.ThrowParseException("The 'shared' attribute is case sensitive and must be set to 'true' or 'false'.", Array.Empty<object>());
										continue;
									}
									goto IL_0425;
								}
								goto IL_0425;
							}
							else
							{
								if (!(text2 == "varybyheader"))
								{
									goto IL_0425;
								}
								this.oc_header = (string)dictionaryEntry.Value;
								this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.VaryByHeader;
								continue;
							}
						}
						else
						{
							if (!(text2 == "nostore"))
							{
								goto IL_0425;
							}
							try
							{
								this.oc_nostore = bool.Parse((string)dictionaryEntry.Value);
								this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.NoStore;
								continue;
							}
							catch
							{
								base.ThrowParseException("The 'NoStore' attribute is case sensitive and must be set to 'true' or 'false'.", Array.Empty<object>());
								continue;
							}
						}
					}
					else if (num != 2774370877U)
					{
						if (num != 2929071957U)
						{
							if (num != 3171214010U)
							{
								goto IL_0425;
							}
							if (!(text2 == "sqldependency"))
							{
								goto IL_0425;
							}
							this.oc_sqldependency = (string)dictionaryEntry.Value;
							continue;
						}
						else
						{
							if (!(text2 == "varybyparam"))
							{
								goto IL_0425;
							}
							this.oc_param = (string)dictionaryEntry.Value;
							if (string.Compare(this.oc_param, "none", true, Helpers.InvariantCulture) == 0)
							{
								this.oc_param = null;
								continue;
							}
							continue;
						}
					}
					else
					{
						if (!(text2 == "varybycontentencodings"))
						{
							goto IL_0425;
						}
						this.oc_content_encodings = (string)dictionaryEntry.Value;
						this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.VaryByContentEncodings;
						continue;
					}
					IL_02A1:
					this.oc_cacheprofile = (string)dictionaryEntry.Value;
					this.oc_parsed_params |= TemplateParser.OutputCacheParsedParams.CacheProfile;
					continue;
					IL_0425:
					base.ThrowParseException("The '" + text + "' attribute is not supported by the 'Outputcache' directive.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0003F3B0 File Offset: 0x0003D5B0
		internal Type LoadType(string typeName)
		{
			Type type = HttpApplication.LoadType(typeName);
			if (type == null)
			{
				return null;
			}
			Assembly assembly = type.Assembly;
			string directoryName = Path.GetDirectoryName(assembly.Location);
			bool flag = true;
			if (directoryName == HttpApplication.BinDirectory)
			{
				flag = false;
			}
			if (flag)
			{
				this.AddAssembly(assembly, true);
			}
			return type;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003F3FD File Offset: 0x0003D5FD
		internal virtual void AddInterface(string iface)
		{
			if (this.interfaces == null)
			{
				this.interfaces = new List<string>();
			}
			if (!this.interfaces.Contains(iface))
			{
				this.interfaces.Add(iface);
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0003F42C File Offset: 0x0003D62C
		internal virtual void AddImport(string namesp)
		{
			if (namesp == null || namesp.Length == 0)
			{
				return;
			}
			if (this.imports == null)
			{
				this.imports = new Dictionary<string, bool>(StringComparer.Ordinal);
			}
			if (this.imports.ContainsKey(namesp))
			{
				return;
			}
			this.imports.Add(namesp, true);
			this.AddAssemblyForNamespace(namesp);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003F480 File Offset: 0x0003D680
		private void AddAssemblyForNamespace(string namesp)
		{
			if (this.binDirAssemblies == null)
			{
				this.binDirAssemblies = HttpApplication.BinDirectoryAssemblies;
			}
			if (this.binDirAssemblies.Length == 0)
			{
				return;
			}
			if (this.namespacesCache == null)
			{
				this.namespacesCache = new Dictionary<string, bool>();
			}
			else if (this.namespacesCache.ContainsKey(namesp))
			{
				return;
			}
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (this.FindNamespaceInAssembly(assembly, namesp))
				{
					return;
				}
			}
			IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
			if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
			{
				foreach (object obj in topLevelAssemblies)
				{
					Assembly assembly2 = (Assembly)obj;
					if (this.FindNamespaceInAssembly(assembly2, namesp))
					{
						return;
					}
				}
			}
			string[] array2 = this.binDirAssemblies;
			for (int i = 0; i < array2.Length; i++)
			{
				Assembly assembly3 = Assembly.LoadFrom(array2[i]);
				if (this.FindNamespaceInAssembly(assembly3, namesp))
				{
					return;
				}
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0003F58C File Offset: 0x0003D78C
		private bool FindNamespaceInAssembly(Assembly asm, string namesp)
		{
			Type[] types;
			try
			{
				types = asm.GetTypes();
			}
			catch (ReflectionTypeLoadException)
			{
				return false;
			}
			Type[] array = types;
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(array[i].Namespace, namesp, StringComparison.Ordinal) == 0)
				{
					this.namespacesCache.Add(namesp, true);
					this.AddAssembly(asm, true);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		internal virtual void AddSourceDependency(string filename)
		{
			if (this.dependencies != null && this.dependencies.Contains(filename))
			{
				base.ThrowParseException("Circular file references are not allowed. File: " + filename, Array.Empty<object>());
			}
			this.AddDependency(filename);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0003F629 File Offset: 0x0003D829
		internal virtual void AddDependency(string filename)
		{
			this.AddDependency(filename, true);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0003F634 File Offset: 0x0003D834
		internal virtual void AddDependency(string filename, bool combinePaths)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return;
			}
			if (this.dependencies == null)
			{
				this.dependencies = new List<string>();
			}
			if (combinePaths)
			{
				filename = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, filename);
			}
			if (!this.dependencies.Contains(filename))
			{
				this.dependencies.Add(filename);
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0003F694 File Offset: 0x0003D894
		internal virtual void AddAssembly(Assembly assembly, bool fullPath)
		{
			if (assembly == null || assembly.Location == string.Empty)
			{
				return;
			}
			if (this.anames == null)
			{
				this.anames = new Dictionary<string, object>();
			}
			string name = assembly.GetName().Name;
			string location = assembly.Location;
			if (fullPath)
			{
				if (!this.assemblies.Contains(location))
				{
					this.assemblies.Add(location);
				}
				this.anames[name] = location;
				this.anames[location] = assembly;
				return;
			}
			if (!this.assemblies.Contains(name))
			{
				this.assemblies.Add(name);
			}
			this.anames[name] = assembly;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0003F744 File Offset: 0x0003D944
		internal virtual Assembly AddAssemblyByFileName(string filename)
		{
			Assembly assembly = null;
			Exception ex = null;
			try
			{
				assembly = Assembly.LoadFrom(filename);
			}
			catch (Exception ex)
			{
			}
			if (assembly == null)
			{
				base.ThrowParseException("Assembly " + filename + " not found", ex, Array.Empty<object>());
			}
			this.AddAssembly(assembly, true);
			return assembly;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		internal virtual Assembly AddAssemblyByName(string name)
		{
			if (this.anames == null)
			{
				this.anames = new Dictionary<string, object>();
			}
			if (this.anames.Contains(name))
			{
				object obj = this.anames[name];
				if (obj is string)
				{
					obj = this.anames[obj];
				}
				return (Assembly)obj;
			}
			Assembly assembly = null;
			Exception ex = null;
			try
			{
				assembly = Assembly.Load(name);
			}
			catch (Exception ex)
			{
			}
			if (assembly == null)
			{
				try
				{
					assembly = Assembly.LoadWithPartialName(name);
				}
				catch (Exception ex)
				{
				}
			}
			if (assembly == null)
			{
				base.ThrowParseException("Assembly " + name + " not found", ex, Array.Empty<object>());
			}
			this.AddAssembly(assembly, true);
			return assembly;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0003F868 File Offset: 0x0003DA68
		internal virtual void ProcessMainAttributes(IDictionary atts)
		{
			this.directiveLocation = new Location(base.Location);
			CompilationSection compilationConfig = base.CompilationConfig;
			atts.Remove("Description");
			atts.Remove("CodeBehind");
			atts.Remove("AspCompat");
			this.debug = base.GetBool(atts, "Debug", compilationConfig.Debug);
			this.compilerOptions = BaseParser.GetString(atts, "CompilerOptions", string.Empty);
			this.language = BaseParser.GetString(atts, "Language", "");
			if (this.language.Length != 0)
			{
				this.implicitLanguage = false;
			}
			else
			{
				this.language = compilationConfig.DefaultLanguage;
			}
			this.strictOn = base.GetBool(atts, "Strict", compilationConfig.Strict);
			this.explicitOn = base.GetBool(atts, "Explicit", compilationConfig.Explicit);
			if (atts.Contains("LinePragmas"))
			{
				this.linePragmasOn = base.GetBool(atts, "LinePragmas", true);
			}
			string @string = BaseParser.GetString(atts, "Inherits", null);
			this.src = BaseParser.GetString(atts, "CodeFile", null);
			this.codeFileBaseClass = BaseParser.GetString(atts, "CodeFileBaseClass", null);
			if (this.src == null && this.codeFileBaseClass != null)
			{
				base.ThrowParseException("The 'CodeFileBaseClass' attribute cannot be used without a 'CodeFile' attribute", Array.Empty<object>());
			}
			string text = BaseParser.GetString(atts, "Src", null);
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			if (text != null)
			{
				text = virtualPathProvider.CombineVirtualPaths(this.BaseVirtualDir, text);
				this.GetAssemblyFromSource(text);
				if (this.src == null)
				{
					this.src = text;
					text = base.MapPath(text, false);
					string text2 = text;
					if (!File.Exists(text2))
					{
						base.ThrowParseException("File " + this.src + " not found", Array.Empty<object>());
					}
					this.srcIsLegacy = true;
				}
				else
				{
					text = base.MapPath(text, false);
				}
				this.AddDependency(text, false);
			}
			if (!this.srcIsLegacy && this.src != null && @string != null)
			{
				this.src = virtualPathProvider.CombineVirtualPaths(this.BaseVirtualDir, this.src);
				string text2 = base.MapPath(this.src, false);
				if (!virtualPathProvider.FileExists(this.src))
				{
					base.ThrowParseException("File " + this.src + " not found", Array.Empty<object>());
				}
				this.partialClassName = @string;
				this.compilerOptions = this.compilerOptions + " \"" + text2 + "\"";
				if (this.codeFileBaseClass != null)
				{
					try
					{
						this.codeFileBaseClassType = this.LoadType(this.codeFileBaseClass);
					}
					catch (Exception)
					{
					}
					if (this.codeFileBaseClassType == null)
					{
						base.ThrowParseException("Could not load type '{0}'", new object[] { this.codeFileBaseClass });
					}
				}
			}
			else if (@string != null)
			{
				this.SetBaseType(@string);
			}
			if (this.src != null)
			{
				if (VirtualPathUtility.IsAbsolute(this.src))
				{
					this.src = VirtualPathUtility.ToAppRelative(this.src);
				}
				this.AddDependency(this.src, false);
			}
			this.className = BaseParser.GetString(atts, "ClassName", null);
			if (this.className != null)
			{
				string[] array = this.className.Split(new char[] { '.' });
				for (int i = 0; i < array.Length; i++)
				{
					if (!CodeGenerator.IsValidLanguageIndependentIdentifier(array[i]))
					{
						base.ThrowParseException(string.Format("'{0}' is not a valid value for attribute 'classname'.", this.className), Array.Empty<object>());
					}
				}
			}
			if (this is TemplateControlParser)
			{
				this.metaResourceKey = BaseParser.GetString(atts, "meta:resourcekey", null);
			}
			if (@string != null && (this is PageParser || this is UserControlParser) && atts.Count > 0)
			{
				if (this.unknownMainAttributes == null)
				{
					this.unknownMainAttributes = new List<UnknownAttributeDescriptor>();
				}
				foreach (object obj in atts)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text3 = dictionaryEntry.Key as string;
					string text4 = dictionaryEntry.Value as string;
					if (!string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
					{
						this.CheckUnknownAttribute(text3, text4, @string);
					}
				}
				return;
			}
			if (atts.Count > 0)
			{
				base.ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts), Array.Empty<object>());
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0003FCCC File Offset: 0x0003DECC
		private void RegisterTagName(string tagName)
		{
			if (this.registeredTagNames == null)
			{
				this.registeredTagNames = new List<string>();
			}
			if (this.registeredTagNames.Contains(tagName))
			{
				return;
			}
			this.registeredTagNames.Add(tagName);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0003FCFC File Offset: 0x0003DEFC
		private void CheckUnknownAttribute(string name, string val, string inherits)
		{
			MemberInfo memberInfo = null;
			bool flag = false;
			string text = name.Trim().ToLower(Helpers.InvariantCulture);
			Type type = this.codeFileBaseClassType;
			if (type == null)
			{
				type = this.baseType;
			}
			try
			{
				MemberInfo[] member = type.GetMember(text, MemberTypes.Field | MemberTypes.Property, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				if (member.Length != 0)
				{
					foreach (MemberInfo memberInfo2 in member)
					{
						if (memberInfo2 is PropertyInfo)
						{
							memberInfo = memberInfo2;
							break;
						}
					}
					if (memberInfo == null)
					{
						memberInfo = member[0];
					}
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception)
			{
				flag = true;
			}
			if (flag)
			{
				base.ThrowParseException("Error parsing attribute '{0}': Type '{1}' does not have a public property named '{0}'", new object[] { text, inherits });
			}
			Type type2 = null;
			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				if (!propertyInfo.CanWrite)
				{
					base.ThrowParseException("Error parsing attribute '{0}': The '{0}' property is read-only and cannot be set.", new object[] { text });
				}
				type2 = propertyInfo.PropertyType;
			}
			else if (memberInfo is FieldInfo)
			{
				type2 = ((FieldInfo)memberInfo).FieldType;
			}
			else
			{
				base.ThrowParseException("Could not determine member the kind of '{0}' in base type '{1}", new object[] { text, inherits });
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type2);
			bool flag2 = true;
			object obj = null;
			if (converter == null || !converter.CanConvertFrom(typeof(string)))
			{
				flag2 = false;
			}
			if (flag2)
			{
				try
				{
					obj = converter.ConvertFromInvariantString(val);
				}
				catch (Exception)
				{
					flag2 = false;
				}
			}
			if (!flag2)
			{
				base.ThrowParseException("Error parsing attribute '{0}': Cannot create an object of type '{1}' from its string representation '{2}' for the '{3}' property.", new object[] { text, type2, val, memberInfo.Name });
			}
			UnknownAttributeDescriptor unknownAttributeDescriptor = new UnknownAttributeDescriptor(memberInfo, obj);
			this.unknownMainAttributes.Add(unknownAttributeDescriptor);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
		internal void SetBaseType(string type)
		{
			Type type2;
			if (type == null || type == this.DefaultBaseTypeName)
			{
				type2 = this.DefaultBaseType;
			}
			else
			{
				type2 = null;
			}
			if (type2 == null)
			{
				type2 = this.LoadType(type);
				if (type2 == null)
				{
					base.ThrowParseException("Cannot find type " + type, Array.Empty<object>());
				}
				if (!this.DefaultBaseType.IsAssignableFrom(type2))
				{
					base.ThrowParseException(string.Concat(new object[] { "The parent type '", type, "' does not derive from ", this.DefaultBaseType }), Array.Empty<object>());
				}
			}
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter != null && !pageParserFilter.AllowBaseType(type2))
			{
				throw new HttpException("Base type '" + type2 + "' is not allowed.");
			}
			this.baseType = type2;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0003FF7E File Offset: 0x0003E17E
		internal void SetLanguage(string language)
		{
			this.language = language;
			this.implicitLanguage = false;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0003FF8E File Offset: 0x0003E18E
		internal void PushIncludeDir(string dir)
		{
			if (this.includeDirs == null)
			{
				this.includeDirs = new Stack<string>(1);
			}
			this.includeDirs.Push(dir);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0003FFB0 File Offset: 0x0003E1B0
		internal string PopIncludeDir()
		{
			if (this.includeDirs == null || this.includeDirs.Count == 0)
			{
				return null;
			}
			return this.includeDirs.Pop();
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0003FFD4 File Offset: 0x0003E1D4
		private Assembly GetAssemblyFromSource(string vpath)
		{
			vpath = UrlUtils.Combine(this.BaseVirtualDir, vpath);
			string text = base.MapPath(vpath, false);
			if (!File.Exists(text))
			{
				base.ThrowParseException("File " + vpath + " not found", Array.Empty<object>());
			}
			this.AddSourceDependency(vpath);
			CompilerParameters compilerParameters;
			string text2;
			CodeDomProvider codeDomProvider = BaseCompiler.CreateProvider(HttpContext.Current, this.language, out compilerParameters, out text2);
			if (codeDomProvider == null)
			{
				throw new HttpException("Cannot find provider for language '" + this.language + "'.");
			}
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(codeDomProvider);
			assemblyBuilder.CompilerOptions = compilerParameters;
			assemblyBuilder.AddAssemblyReference(BuildManager.GetReferencedAssemblies() as List<Assembly>);
			assemblyBuilder.AddCodeFile(text);
			CompilerResults compilerResults = assemblyBuilder.BuildAssembly(new VirtualPath(vpath));
			if (compilerResults.NativeCompilerReturnValue != 0)
			{
				using (StreamReader streamReader = new StreamReader(text))
				{
					throw new CompilationException(text, compilerResults.Errors, streamReader.ReadToEnd());
				}
			}
			this.AddAssembly(compilerResults.CompiledAssembly, true);
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001763 RID: 5987
		internal abstract string DefaultBaseTypeName { get; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001764 RID: 5988
		internal abstract string DefaultDirectiveName { get; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x000400D8 File Offset: 0x0003E2D8
		internal bool LinePragmasOn
		{
			get
			{
				return this.linePragmasOn;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x000400E0 File Offset: 0x0003E2E0
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x000400E8 File Offset: 0x0003E2E8
		internal byte[] MD5Checksum
		{
			get
			{
				return this.md5checksum;
			}
			set
			{
				this.md5checksum = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000400F4 File Offset: 0x0003E2F4
		internal PageParserFilter PageParserFilter
		{
			get
			{
				if (this.pageParserFilter != null)
				{
					return this.pageParserFilter;
				}
				Type type = this.PageParserFilterType;
				if (type == null)
				{
					return null;
				}
				this.pageParserFilter = Activator.CreateInstance(type) as PageParserFilter;
				this.pageParserFilter.Initialize(this);
				return this.pageParserFilter;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00040148 File Offset: 0x0003E348
		internal Type PageParserFilterType
		{
			get
			{
				if (this.pageParserFilterType == null)
				{
					this.pageParserFilterType = PageParser.DefaultPageParserFilterType;
					if (this.pageParserFilterType != null)
					{
						return this.pageParserFilterType;
					}
					string text = this.PagesConfig.PageParserFilterType;
					if (string.IsNullOrEmpty(text))
					{
						return null;
					}
					this.pageParserFilterType = HttpApplication.LoadType(text, true);
				}
				return this.pageParserFilterType;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000401AC File Offset: 0x0003E3AC
		internal virtual Type DefaultBaseType
		{
			get
			{
				return Type.GetType(this.DefaultBaseTypeName, true);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000401BA File Offset: 0x0003E3BA
		internal ILocation DirectiveLocation
		{
			get
			{
				return this.directiveLocation;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000401C2 File Offset: 0x0003E3C2
		internal string ParserDir
		{
			get
			{
				if (this.includeDirs == null || this.includeDirs.Count == 0)
				{
					return base.BaseDir;
				}
				return this.includeDirs.Peek();
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x000401EB File Offset: 0x0003E3EB
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x000401F3 File Offset: 0x0003E3F3
		internal string InputFile
		{
			get
			{
				return this.inputFile;
			}
			set
			{
				this.inputFile = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x000401FC File Offset: 0x0003E3FC
		internal bool IsPartial
		{
			get
			{
				return !this.srcIsLegacy && this.src != null;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00040211 File Offset: 0x0003E411
		internal string CodeBehindSource
		{
			get
			{
				if (this.srcIsLegacy)
				{
					return null;
				}
				return this.src;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00040223 File Offset: 0x0003E423
		internal string PartialClassName
		{
			get
			{
				return this.partialClassName;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0004022B File Offset: 0x0003E42B
		internal string CodeFileBaseClass
		{
			get
			{
				return this.codeFileBaseClass;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00040233 File Offset: 0x0003E433
		internal string MetaResourceKey
		{
			get
			{
				return this.metaResourceKey;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0004023B File Offset: 0x0003E43B
		internal Type CodeFileBaseClassType
		{
			get
			{
				return this.codeFileBaseClassType;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00040243 File Offset: 0x0003E443
		internal List<UnknownAttributeDescriptor> UnknownMainAttributes
		{
			get
			{
				return this.unknownMainAttributes;
			}
		}

		/// <summary>Gets the string that contains the data to be parsed.</summary>
		/// <returns>The data to be parsed.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0004024B File Offset: 0x0003E44B
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x00040253 File Offset: 0x0003E453
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0004025C File Offset: 0x0003E45C
		internal Type BaseType
		{
			get
			{
				if (this.baseType == null)
				{
					this.SetBaseType(this.DefaultBaseTypeName);
				}
				return this.baseType;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0004027E File Offset: 0x0003E47E
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x00040286 File Offset: 0x0003E486
		internal bool BaseTypeIsGlobal
		{
			get
			{
				return this.baseTypeIsGlobal;
			}
			set
			{
				this.baseTypeIsGlobal = value;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00040290 File Offset: 0x0003E490
		internal string EncodeIdentifier(string value)
		{
			if (value == null || value.Length == 0 || CodeGenerator.IsValidLanguageIndependentIdentifier(value))
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = value[0];
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			switch (unicodeCategory)
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				break;
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
				goto IL_007A;
			case UnicodeCategory.DecimalDigitNumber:
				stringBuilder.Append('_');
				stringBuilder.Append(c);
				goto IL_0083;
			default:
				if (unicodeCategory != UnicodeCategory.ConnectorPunctuation)
				{
					goto IL_007A;
				}
				break;
			}
			stringBuilder.Append(c);
			goto IL_0083;
			IL_007A:
			stringBuilder.Append('_');
			IL_0083:
			int i = 1;
			while (i < value.Length)
			{
				c = value[i];
				switch (char.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.Format:
				case UnicodeCategory.ConnectorPunctuation:
					stringBuilder.Append(c);
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_00F4;
				default:
					goto IL_00F4;
				}
				IL_00FD:
				i++;
				continue;
				IL_00F4:
				stringBuilder.Append('_');
				goto IL_00FD;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x000403B0 File Offset: 0x0003E5B0
		internal string ClassName
		{
			get
			{
				if (this.className != null)
				{
					return this.className;
				}
				string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
				string text;
				if (string.IsNullOrEmpty(this.inputFile))
				{
					text = null;
					using (StreamReader streamReader = this.Reader as StreamReader)
					{
						if (streamReader != null)
						{
							FileStream fileStream = streamReader.BaseStream as FileStream;
							if (fileStream != null)
							{
								text = fileStream.Name;
							}
						}
						goto IL_0066;
					}
				}
				text = this.inputFile;
				IL_0066:
				if (string.IsNullOrEmpty(text))
				{
					long num = Interlocked.Increment(ref TemplateParser.autoClassCounter);
					this.className = string.Format("autoclass_nosource_{0:x}", num);
					return this.className;
				}
				if (StrUtils.StartsWith(text, physicalApplicationPath))
				{
					this.className = this.inputFile.Substring(physicalApplicationPath.Length).ToLower(Helpers.InvariantCulture);
				}
				else
				{
					this.className = Path.GetFileName(this.inputFile);
				}
				this.className = this.EncodeIdentifier(this.className);
				return this.className;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x000404BC File Offset: 0x0003E6BC
		internal List<ServerSideScript> Scripts
		{
			get
			{
				if (this.scripts == null)
				{
					this.scripts = new List<ServerSideScript>();
				}
				return this.scripts;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x000404D7 File Offset: 0x0003E6D7
		internal Dictionary<string, bool> Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x000404DF File Offset: 0x0003E6DF
		internal List<string> Interfaces
		{
			get
			{
				return this.interfaces;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x000404E8 File Offset: 0x0003E6E8
		internal List<string> Assemblies
		{
			get
			{
				if (this.appAssemblyIndex != -1)
				{
					string text = this.assemblies[this.appAssemblyIndex];
					this.assemblies.RemoveAt(this.appAssemblyIndex);
					this.assemblies.Add(text);
					this.appAssemblyIndex = -1;
				}
				return this.assemblies;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0004053C File Offset: 0x0003E73C
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x00040574 File Offset: 0x0003E774
		internal RootBuilder RootBuilder
		{
			get
			{
				if (this.rootBuilder != null)
				{
					return this.rootBuilder;
				}
				AspGenerator aspGenerator = this.AspGenerator;
				if (aspGenerator != null)
				{
					this.rootBuilder = aspGenerator.RootBuilder;
				}
				return this.rootBuilder;
			}
			set
			{
				this.rootBuilder = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0004057D File Offset: 0x0003E77D
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x00040585 File Offset: 0x0003E785
		internal List<string> Dependencies
		{
			get
			{
				return this.dependencies;
			}
			set
			{
				this.dependencies = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0004058E File Offset: 0x0003E78E
		internal string CompilerOptions
		{
			get
			{
				return this.compilerOptions;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00040596 File Offset: 0x0003E796
		internal string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0004059E File Offset: 0x0003E79E
		internal bool ImplicitLanguage
		{
			get
			{
				return this.implicitLanguage;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000405A6 File Offset: 0x0003E7A6
		internal bool StrictOn
		{
			get
			{
				return this.strictOn;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000405AE File Offset: 0x0003E7AE
		internal bool ExplicitOn
		{
			get
			{
				return this.explicitOn;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000405B6 File Offset: 0x0003E7B6
		internal bool Debug
		{
			get
			{
				return this.debug;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000405BE File Offset: 0x0003E7BE
		internal bool OutputCache
		{
			get
			{
				return this.output_cache;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x000405C6 File Offset: 0x0003E7C6
		internal int OutputCacheDuration
		{
			get
			{
				return this.oc_duration;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000405CE File Offset: 0x0003E7CE
		internal TemplateParser.OutputCacheParsedParams OutputCacheParsedParameters
		{
			get
			{
				return this.oc_parsed_params;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x000405D6 File Offset: 0x0003E7D6
		internal string OutputCacheSqlDependency
		{
			get
			{
				return this.oc_sqldependency;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x000405DE File Offset: 0x0003E7DE
		internal string OutputCacheCacheProfile
		{
			get
			{
				return this.oc_cacheprofile;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x000405E6 File Offset: 0x0003E7E6
		internal string OutputCacheVaryByContentEncodings
		{
			get
			{
				return this.oc_content_encodings;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000405EE File Offset: 0x0003E7EE
		internal bool OutputCacheNoStore
		{
			get
			{
				return this.oc_nostore;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0000393A File Offset: 0x00001B3A
		internal virtual TextReader Reader
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000405F6 File Offset: 0x0003E7F6
		internal string OutputCacheVaryByHeader
		{
			get
			{
				return this.oc_header;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000405FE File Offset: 0x0003E7FE
		internal string OutputCacheVaryByCustom
		{
			get
			{
				return this.oc_custom;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00040606 File Offset: 0x0003E806
		internal string OutputCacheVaryByControls
		{
			get
			{
				return this.oc_controls;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0004060E File Offset: 0x0003E80E
		internal bool OutputCacheShared
		{
			get
			{
				return this.oc_shared;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00040616 File Offset: 0x0003E816
		internal OutputCacheLocation OutputCacheLocation
		{
			get
			{
				return this.oc_location;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x0004061E File Offset: 0x0003E81E
		internal string OutputCacheVaryByParam
		{
			get
			{
				return this.oc_param;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00040626 File Offset: 0x0003E826
		internal List<string> RegisteredTagNames
		{
			get
			{
				return this.registeredTagNames;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0004062E File Offset: 0x0003E82E
		internal PagesSection PagesConfig
		{
			get
			{
				return base.GetConfigSection<PagesSection>("system.web/pages");
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0004063B File Offset: 0x0003E83B
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00040643 File Offset: 0x0003E843
		internal AspGenerator AspGenerator { get; set; }

		/// <summary>Parses the content of the file that is specified by either its virtual or physical path.</summary>
		/// <param name="physicalPath">The physical path of the file to parse. <paramref name="physicalPath" /> has precedence over <paramref name="virtualPath" />.</param>
		/// <param name="virtualPath">The virtual path of the file to parse. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="physicalPath" /> or <paramref name="virtualPath" /> refers to the file currently being parsed. </exception>
		// Token: 0x0600179F RID: 6047 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void ParseFile(string physicalPath, string virtualPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Parses the template.</summary>
		/// <returns>Returns the template.</returns>
		/// <param name="content">The content.</param>
		/// <param name="virtualPath">The virtual path.</param>
		/// <param name="ignoreFilter">true to ignore the filter; otherwise, false.</param>
		// Token: 0x060017A0 RID: 6048 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static ITemplate ParseTemplate(string content, string virtualPath, bool ignoreFilter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Reports a process error by creating a new <see cref="T:System.Web.ParserError" /> object with the specified message and adding it to a <see cref="T:System.Web.ParserErrorCollection" /> collection.</summary>
		/// <param name="message">The error message text used to create a new <see cref="T:System.Web.ParserError" />. </param>
		// Token: 0x060017A1 RID: 6049 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void ProcessError(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Reports a parser exception by creating a new <see cref="T:System.Web.ParserError" /> object as the specified <see cref="T:System.Exception" /> exception and adding it to a <see cref="T:System.Web.ParserErrorCollection" /> collection.</summary>
		/// <param name="ex">The <see cref="T:System.Exception" /> used to create a new <see cref="T:System.Web.ParserError" />. </param>
		/// <exception cref="T:System.Web.HttpCompileException">The <see cref="T:System.Exception" /> to process is a compiler error. </exception>
		// Token: 0x060017A2 RID: 6050 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void ProcessException(Exception ex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015AD RID: 5549
		private string inputFile;

		// Token: 0x040015AE RID: 5550
		private string text;

		// Token: 0x040015AF RID: 5551
		private IDictionary mainAttributes;

		// Token: 0x040015B0 RID: 5552
		private List<string> dependencies;

		// Token: 0x040015B1 RID: 5553
		private List<string> assemblies;

		// Token: 0x040015B2 RID: 5554
		private IDictionary anames;

		// Token: 0x040015B3 RID: 5555
		private string[] binDirAssemblies;

		// Token: 0x040015B4 RID: 5556
		private Dictionary<string, bool> namespacesCache;

		// Token: 0x040015B5 RID: 5557
		private Dictionary<string, bool> imports;

		// Token: 0x040015B6 RID: 5558
		private List<string> interfaces;

		// Token: 0x040015B7 RID: 5559
		private List<ServerSideScript> scripts;

		// Token: 0x040015B8 RID: 5560
		private Type baseType;

		// Token: 0x040015B9 RID: 5561
		private bool baseTypeIsGlobal = true;

		// Token: 0x040015BA RID: 5562
		private string className;

		// Token: 0x040015BB RID: 5563
		private RootBuilder rootBuilder;

		// Token: 0x040015BC RID: 5564
		private bool debug;

		// Token: 0x040015BD RID: 5565
		private string compilerOptions;

		// Token: 0x040015BE RID: 5566
		private string language;

		// Token: 0x040015BF RID: 5567
		private bool implicitLanguage;

		// Token: 0x040015C0 RID: 5568
		private bool strictOn;

		// Token: 0x040015C1 RID: 5569
		private bool explicitOn;

		// Token: 0x040015C2 RID: 5570
		private bool linePragmasOn = true;

		// Token: 0x040015C3 RID: 5571
		private bool output_cache;

		// Token: 0x040015C4 RID: 5572
		private int oc_duration;

		// Token: 0x040015C5 RID: 5573
		private string oc_header;

		// Token: 0x040015C6 RID: 5574
		private string oc_custom;

		// Token: 0x040015C7 RID: 5575
		private string oc_param;

		// Token: 0x040015C8 RID: 5576
		private string oc_controls;

		// Token: 0x040015C9 RID: 5577
		private string oc_content_encodings;

		// Token: 0x040015CA RID: 5578
		private string oc_cacheprofile;

		// Token: 0x040015CB RID: 5579
		private string oc_sqldependency;

		// Token: 0x040015CC RID: 5580
		private bool oc_nostore;

		// Token: 0x040015CD RID: 5581
		private TemplateParser.OutputCacheParsedParams oc_parsed_params;

		// Token: 0x040015CE RID: 5582
		private bool oc_shared;

		// Token: 0x040015CF RID: 5583
		private OutputCacheLocation oc_location;

		// Token: 0x040015D0 RID: 5584
		internal int allowedMainDirectives;

		// Token: 0x040015D1 RID: 5585
		private byte[] md5checksum;

		// Token: 0x040015D2 RID: 5586
		private string src;

		// Token: 0x040015D3 RID: 5587
		private bool srcIsLegacy;

		// Token: 0x040015D4 RID: 5588
		private string partialClassName;

		// Token: 0x040015D5 RID: 5589
		private string codeFileBaseClass;

		// Token: 0x040015D6 RID: 5590
		private string metaResourceKey;

		// Token: 0x040015D7 RID: 5591
		private Type codeFileBaseClassType;

		// Token: 0x040015D8 RID: 5592
		private Type pageParserFilterType;

		// Token: 0x040015D9 RID: 5593
		private PageParserFilter pageParserFilter;

		// Token: 0x040015DA RID: 5594
		private List<UnknownAttributeDescriptor> unknownMainAttributes;

		// Token: 0x040015DB RID: 5595
		private Stack<string> includeDirs;

		// Token: 0x040015DC RID: 5596
		private List<string> registeredTagNames;

		// Token: 0x040015DD RID: 5597
		private ILocation directiveLocation;

		// Token: 0x040015DE RID: 5598
		private int appAssemblyIndex = -1;

		// Token: 0x040015DF RID: 5599
		private static long autoClassCounter;

		// Token: 0x02000239 RID: 569
		[Flags]
		internal enum OutputCacheParsedParams
		{
			// Token: 0x040015E2 RID: 5602
			Location = 1,
			// Token: 0x040015E3 RID: 5603
			CacheProfile = 2,
			// Token: 0x040015E4 RID: 5604
			NoStore = 4,
			// Token: 0x040015E5 RID: 5605
			SqlDependency = 8,
			// Token: 0x040015E6 RID: 5606
			VaryByCustom = 16,
			// Token: 0x040015E7 RID: 5607
			VaryByHeader = 32,
			// Token: 0x040015E8 RID: 5608
			VaryByControl = 64,
			// Token: 0x040015E9 RID: 5609
			VaryByContentEncodings = 128
		}
	}
}
