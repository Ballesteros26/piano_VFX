using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides base functionality for parsing Web handler files.</summary>
	// Token: 0x02000226 RID: 550
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class SimpleWebHandlerParser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SimpleWebHandlerParser" /> class. </summary>
		/// <param name="context">Pass null. Parameter is now obsolete.</param>
		/// <param name="virtualPath">The path of the current virtual directory.</param>
		/// <param name="physicalPath">Pass null. Parameter is now obsolete.</param>
		// Token: 0x0600166F RID: 5743 RVA: 0x0003C0EC File Offset: 0x0003A2EC
		protected SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
			: this(context, virtualPath, physicalPath, null)
		{
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0003C0F8 File Offset: 0x0003A2F8
		internal SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath, TextReader reader)
		{
			this.reader = reader;
			this.cachedType = CachingCompiler.GetTypeFromCache(physicalPath);
			if (this.cachedType != null)
			{
				return;
			}
			if (context != null)
			{
				this.context = context;
			}
			else
			{
				this.context = HttpContext.Current;
			}
			this.vPath = virtualPath;
			this.AddDependency(virtualPath);
			if (physicalPath != null && physicalPath.Length > 0)
			{
				this.physPath = physicalPath;
			}
			else
			{
				HttpRequest httpRequest = ((this.context != null) ? context.Request : null);
				if (httpRequest != null)
				{
					this.physPath = httpRequest.MapPath(virtualPath);
				}
			}
			this.assemblies = new ArrayList();
			string assemblyLocation = this.Context.ApplicationInstance.AssemblyLocation;
			if (assemblyLocation != typeof(TemplateParser).Assembly.Location)
			{
				this.appAssemblyIndex = this.assemblies.Add(assemblyLocation);
			}
			bool flag = false;
			foreach (object obj in this.CompilationConfig.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				if (assemblyInfo.Assembly == "*")
				{
					flag = true;
				}
				else
				{
					this.AddAssemblyByName(assemblyInfo.Assembly, null);
				}
			}
			if (flag)
			{
				this.AddAssembliesInBin();
			}
			this.language = this.CompilationConfig.DefaultLanguage;
			this.GetDirectivesAndContent();
		}

		/// <summary>Returns the type for the compiled object from the virtual path.</summary>
		/// <returns>The <see cref="T:System.Type" /> assigned to the virtual path.</returns>
		// Token: 0x06001671 RID: 5745 RVA: 0x0003C274 File Offset: 0x0003A474
		protected Type GetCompiledTypeFromCache()
		{
			return this.cachedType;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0003C27C File Offset: 0x0003A47C
		private void GetDirectivesAndContent()
		{
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = null;
			StringBuilder stringBuilder2 = new StringBuilder();
			StreamReader streamReader;
			if (this.reader != null)
			{
				streamReader = this.reader as StreamReader;
			}
			else
			{
				streamReader = new StreamReader(File.OpenRead(this.physPath), WebEncoding.FileEncoding);
			}
			using (streamReader)
			{
				string text;
				while ((text = streamReader.ReadLine()) != null && this.cachedType == null)
				{
					int length = text.Length;
					if (length == 0)
					{
						stringBuilder2.Append("\n");
					}
					else
					{
						int num = text.IndexOf("<%");
						if (num > -1)
						{
							int num2 = text.IndexOf("%>");
							if (num > 0)
							{
								stringBuilder2.Append(text.Substring(0, num));
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							else
							{
								stringBuilder.Length = 0;
							}
							if (num2 <= -1)
							{
								flag2 = true;
								flag = false;
								stringBuilder.Append(text.Substring(num));
								continue;
							}
							flag = true;
							flag2 = false;
							stringBuilder.Append(text.Substring(num, num2 - num + 2));
							if (num2 < length - 2)
							{
								stringBuilder2.Append(text.Substring(num2 + 2, length - num2 - 2));
							}
						}
						if (flag2)
						{
							int num3 = text.IndexOf("%>");
							if (num3 <= -1)
							{
								stringBuilder.Append(text);
								continue;
							}
							stringBuilder.Append(text.Substring(0, num3 + 2));
							if (num3 < length)
							{
								stringBuilder2.Append(text.Substring(num3 + 2) + "\n");
							}
							flag2 = false;
							flag = true;
						}
						if (flag)
						{
							this.ParseDirective(stringBuilder.ToString());
							flag = false;
							if (this.gotDefault)
							{
								this.cachedType = CachingCompiler.GetTypeFromCache(this.physPath);
								if (this.cachedType != null)
								{
									break;
								}
							}
						}
						else
						{
							stringBuilder2.Append(text + "\n");
						}
					}
				}
			}
			if (!this.gotDefault)
			{
				throw new ParseException(null, "No @" + this.DefaultDirectiveName + " directive found");
			}
			if (this.cachedType == null)
			{
				this.program = stringBuilder2.ToString();
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0003C4BC File Offset: 0x0003A6BC
		private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
		{
			if (tagtype != TagType.Directive)
			{
				throw new ParseException(location, "Unexpected tag");
			}
			if (tagid == null || tagid.Length == 0 || string.Compare(tagid, this.DefaultDirectiveName, true, Helpers.InvariantCulture) == 0)
			{
				this.AddDefaultDirective(location, attributes);
				return;
			}
			if (string.Compare(tagid, "Assembly", true, Helpers.InvariantCulture) == 0)
			{
				this.AddAssemblyDirective(location, attributes);
				return;
			}
			throw new ParseException(location, "Unexpected directive: " + tagid);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0003C530 File Offset: 0x0003A730
		private void TextParsed(ILocation location, string text)
		{
			if (text.Trim() != "")
			{
				throw new ParseException(location, "Text not allowed here");
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0003C550 File Offset: 0x0003A750
		private void ParseError(ILocation location, string message)
		{
			throw new ParseException(location, message);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003C559 File Offset: 0x0003A759
		private static string GetAndRemove(IDictionary table, string key)
		{
			string text = table[key] as string;
			table.Remove(key);
			return text;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0003C570 File Offset: 0x0003A770
		private void ParseDirective(string line)
		{
			AspParser aspParser;
			using (StringReader stringReader = new StringReader(line))
			{
				aspParser = new AspParser(this.physPath, stringReader);
			}
			aspParser.Error += this.ParseError;
			aspParser.TagParsed += this.TagParsed;
			aspParser.TextParsed += this.TextParsed;
			aspParser.Parse();
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0003C5EC File Offset: 0x0003A7EC
		internal virtual void AddDefaultDirective(ILocation location, TagAttributes attrs)
		{
			CompilationSection compilationConfig = this.CompilationConfig;
			if (this.gotDefault)
			{
				throw new ParseException(location, "duplicate " + this.DefaultDirectiveName + " directive");
			}
			this.gotDefault = true;
			IDictionary dictionary = attrs.GetDictionary(null);
			this.className = SimpleWebHandlerParser.GetAndRemove(dictionary, "class");
			if (this.className == null)
			{
				throw new ParseException(null, "No Class attribute found.");
			}
			string andRemove = SimpleWebHandlerParser.GetAndRemove(dictionary, "debug");
			if (andRemove != null)
			{
				this.debug = string.Compare(andRemove, "true", true, Helpers.InvariantCulture) == 0;
				if (!this.debug && string.Compare(andRemove, "false", true, Helpers.InvariantCulture) != 0)
				{
					throw new ParseException(null, "Invalid value for Debug attribute");
				}
			}
			else
			{
				this.debug = compilationConfig.Debug;
			}
			this.language = SimpleWebHandlerParser.GetAndRemove(dictionary, "language");
			if (this.language == null)
			{
				this.language = compilationConfig.DefaultLanguage;
			}
			SimpleWebHandlerParser.GetAndRemove(dictionary, "codebehind");
			if (dictionary.Count > 0)
			{
				throw new ParseException(location, "Unrecognized attribute in " + this.DefaultDirectiveName + " directive");
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0003C70C File Offset: 0x0003A90C
		internal virtual void AddAssemblyDirective(ILocation location, TagAttributes attrs)
		{
			IDictionary dictionary = attrs.GetDictionary(null);
			string andRemove = SimpleWebHandlerParser.GetAndRemove(dictionary, "Name");
			string andRemove2 = SimpleWebHandlerParser.GetAndRemove(dictionary, "Src");
			if (andRemove == null && andRemove2 == null)
			{
				throw new ParseException(location, "You gotta specify Src or Name");
			}
			if (andRemove != null && andRemove2 != null)
			{
				throw new ParseException(location, "Src and Name cannot be used together");
			}
			if (andRemove != null)
			{
				this.AddAssemblyByName(andRemove, location);
			}
			else
			{
				this.GetAssemblyFromSource(andRemove2, location);
			}
			if (dictionary.Count > 0)
			{
				throw new ParseException(location, "Unrecognized attribute in Assembly directive");
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0003C788 File Offset: 0x0003A988
		internal virtual void AddAssembly(Assembly assembly, bool fullPath)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (this.anames == null)
			{
				this.anames = new Hashtable();
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

		// Token: 0x0600167B RID: 5755 RVA: 0x0003C830 File Offset: 0x0003AA30
		internal virtual Assembly AddAssemblyByName(string name, ILocation location)
		{
			if (this.anames == null)
			{
				this.anames = new Hashtable();
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
			Assembly assembly = this.LoadAssemblyFromBin(name);
			if (assembly != null)
			{
				this.AddAssembly(assembly, true);
				return assembly;
			}
			Exception ex = null;
			try
			{
				assembly = Assembly.LoadWithPartialName(name);
			}
			catch (Exception ex)
			{
				assembly = null;
			}
			if (assembly == null)
			{
				throw new ParseException(location, string.Format("Assembly '{0}' not found", name), ex);
			}
			this.AddAssembly(assembly, true);
			return assembly;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0003C8E8 File Offset: 0x0003AAE8
		private void AddAssembliesInBin()
		{
			foreach (string text in HttpApplication.BinDirectoryAssemblies)
			{
				Exception ex = null;
				try
				{
					Assembly assembly = Assembly.LoadFrom(text);
					this.AddAssembly(assembly, true);
				}
				catch (FileLoadException ex)
				{
				}
				catch (BadImageFormatException ex)
				{
				}
				catch (Exception ex2)
				{
					throw new Exception("Error while loading " + text, ex2);
				}
				if (ex != null && RuntimeHelpers.DebuggingEnabled)
				{
					Console.WriteLine("**** DEBUG MODE *****");
					Console.WriteLine("Bad assembly found in bin/. Exception (ignored):");
					Console.WriteLine(ex);
				}
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0003C98C File Offset: 0x0003AB8C
		private Assembly LoadAssemblyFromBin(string name)
		{
			foreach (string text in HttpApplication.BinDirectoryAssemblies)
			{
				if (!(Path.ChangeExtension(Path.GetFileName(text), null) != name))
				{
					return Assembly.LoadFrom(text);
				}
			}
			return null;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		private Assembly GetAssemblyFromSource(string vpath, ILocation location)
		{
			vpath = UrlUtils.Combine(this.BaseVirtualDir, vpath);
			string text = this.context.Request.MapPath(vpath);
			if (!File.Exists(text))
			{
				throw new ParseException(location, "File " + vpath + " not found");
			}
			this.AddDependency(vpath);
			CompilerResults compilerResults = CachingCompiler.Compile(this.language, text, text, this.assemblies);
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

		// Token: 0x0600167F RID: 5759 RVA: 0x0003CA88 File Offset: 0x0003AC88
		internal Type GetTypeFromBin(string tname)
		{
			if (tname == null || tname.Length == 0)
			{
				throw new ArgumentNullException("tname");
			}
			Type type = null;
			int num = tname.IndexOf(',');
			string text;
			string text2;
			if (num != -1)
			{
				text = tname.Substring(0, num).Trim();
				text2 = tname.Substring(num + 1).Trim();
			}
			else
			{
				text = tname;
				text2 = null;
			}
			Type type2 = null;
			Assembly assembly = null;
			if (text2 != null)
			{
				assembly = Assembly.Load(text2);
				if (assembly != null)
				{
					type2 = assembly.GetType(text, false);
				}
				if (type2 != null)
				{
					return type2;
				}
			}
			IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
			if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
			{
				foreach (object obj in topLevelAssemblies)
				{
					type2 = ((Assembly)obj).GetType(text, false);
					if (type2 != null)
					{
						if (type != null)
						{
							throw new HttpException(string.Format("Type {0} is not unique.", text));
						}
						type = type2;
					}
				}
			}
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			int i = 0;
			while (i < binDirectoryAssemblies.Length)
			{
				string text3 = binDirectoryAssemblies[i];
				try
				{
					assembly = Assembly.LoadFrom(text3);
				}
				catch (FileLoadException)
				{
					goto IL_014D;
				}
				catch (BadImageFormatException)
				{
					goto IL_014D;
				}
				goto IL_011B;
				IL_014D:
				i++;
				continue;
				IL_011B:
				type2 = assembly.GetType(text, false);
				if (!(type2 != null))
				{
					goto IL_014D;
				}
				if (type != null)
				{
					throw new HttpException(string.Format("Type {0} is not unique.", text));
				}
				type = type2;
				goto IL_014D;
			}
			if (type == null)
			{
				throw new HttpException(string.Format("Type {0} not found.", text));
			}
			return type;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0003CC34 File Offset: 0x0003AE34
		internal virtual void AddDependency(string filename)
		{
			if (this.dependencies == null)
			{
				this.dependencies = new ArrayList();
			}
			if (!this.dependencies.Contains(filename))
			{
				this.dependencies.Add(filename);
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the main directive from a &lt;%@ %&gt; block.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the default directive name.</returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001681 RID: 5761
		protected abstract string DefaultDirectiveName { get; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0003CC64 File Offset: 0x0003AE64
		internal HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0003CC6C File Offset: 0x0003AE6C
		internal string VirtualPath
		{
			get
			{
				return this.vPath;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0003CC74 File Offset: 0x0003AE74
		internal string PhysicalPath
		{
			get
			{
				return this.physPath;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0003CC7C File Offset: 0x0003AE7C
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0003CC84 File Offset: 0x0003AE84
		internal bool Debug
		{
			get
			{
				return this.debug;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		internal string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0003CC94 File Offset: 0x0003AE94
		internal string Program
		{
			get
			{
				if (this.program != null)
				{
					return this.program;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		internal ArrayList Assemblies
		{
			get
			{
				if (this.appAssemblyIndex != -1)
				{
					object obj = this.assemblies[this.appAssemblyIndex];
					this.assemblies.RemoveAt(this.appAssemblyIndex);
					this.assemblies.Add(obj);
					this.appAssemblyIndex = -1;
				}
				return this.assemblies;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0003CCFF File Offset: 0x0003AEFF
		internal ArrayList Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0003CD07 File Offset: 0x0003AF07
		internal string BaseDir
		{
			get
			{
				if (this.baseDir == null)
				{
					this.baseDir = this.context.Request.MapPath(this.BaseVirtualDir);
				}
				return this.baseDir;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0003CD33 File Offset: 0x0003AF33
		internal virtual string BaseVirtualDir
		{
			get
			{
				if (this.baseVDir == null)
				{
					this.baseVDir = UrlUtils.GetDirectory(this.context.Request.FilePath);
				}
				return this.baseVDir;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0003CD60 File Offset: 0x0003AF60
		private CompilationSection CompilationConfig
		{
			get
			{
				string virtualPath = this.VirtualPath;
				if (string.IsNullOrEmpty(virtualPath))
				{
					return WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
				}
				return WebConfigurationManager.GetSection("system.web/compilation", virtualPath) as CompilationSection;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0003CD9C File Offset: 0x0003AF9C
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		internal TextReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		// Token: 0x0400156C RID: 5484
		private HttpContext context;

		// Token: 0x0400156D RID: 5485
		private string vPath;

		// Token: 0x0400156E RID: 5486
		private string physPath;

		// Token: 0x0400156F RID: 5487
		private string className;

		// Token: 0x04001570 RID: 5488
		private bool debug;

		// Token: 0x04001571 RID: 5489
		private string language;

		// Token: 0x04001572 RID: 5490
		private string program;

		// Token: 0x04001573 RID: 5491
		private bool gotDefault;

		// Token: 0x04001574 RID: 5492
		private ArrayList assemblies;

		// Token: 0x04001575 RID: 5493
		private ArrayList dependencies;

		// Token: 0x04001576 RID: 5494
		private Hashtable anames;

		// Token: 0x04001577 RID: 5495
		private string baseDir;

		// Token: 0x04001578 RID: 5496
		private string baseVDir;

		// Token: 0x04001579 RID: 5497
		private TextReader reader;

		// Token: 0x0400157A RID: 5498
		private int appAssemblyIndex = -1;

		// Token: 0x0400157B RID: 5499
		private Type cachedType;
	}
}
