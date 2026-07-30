using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000626 RID: 1574
	internal class AspGenerator
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x000B3DC4 File Offset: 0x000B1FC4
		public AspGenerator(TemplateParser tparser, AspComponentFoundry componentFoundry)
			: this(tparser)
		{
			this.componentFoundry = componentFoundry;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x000B3DD4 File Offset: 0x000B1FD4
		public AspGenerator(TemplateParser tparser)
		{
			this.tparser = tparser;
			this.text = new StringBuilder();
			this.stack = new BuilderLocationStack();
			this.pstack = new ParserStack();
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x000B3E0F File Offset: 0x000B200F
		public RootBuilder RootBuilder
		{
			get
			{
				return this.rootBuilder;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000B3E17 File Offset: 0x000B2017
		public AspParser Parser
		{
			get
			{
				return this.pstack.Parser;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000B3E24 File Offset: 0x000B2024
		public string Filename
		{
			get
			{
				return this.pstack.Filename;
			}
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000B3E31 File Offset: 0x000B2031
		private PageParserFilter PageParserFilter
		{
			get
			{
				if (this.tparser == null)
				{
					return null;
				}
				return this.tparser.PageParserFilter;
			}
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x000B3E48 File Offset: 0x000B2048
		private IDictionary GetDirectiveAttributesDictionary(string skipKeyName, CaptureCollection names, CaptureCollection values)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			int num = 0;
			foreach (object obj in names)
			{
				Capture capture = (Capture)obj;
				string value = capture.Value;
				if (string.Compare(skipKeyName, value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					num++;
				}
				else
				{
					dictionary.Add(capture.Value, values[num++].Value);
				}
			}
			return dictionary;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000B3EDC File Offset: 0x000B20DC
		private string GetDirectiveName(CaptureCollection names)
		{
			foreach (object obj in names)
			{
				string value = ((Capture)obj).Value;
				if (Directive.IsDirective(value))
				{
					return value;
				}
			}
			return this.tparser.DefaultDirectiveName;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000B3F48 File Offset: 0x000B2148
		private int GetLineNumberForIndex(string fileContents, int index)
		{
			int num = 1;
			bool flag = false;
			for (int i = 0; i < index; i++)
			{
				char c = fileContents[i];
				if (c == '\n' || flag)
				{
					num++;
				}
				flag = c == '\r';
			}
			return num;
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000B3F84 File Offset: 0x000B2184
		private int GetNumberOfLinesForRange(string fileContents, int index, int length)
		{
			int num = 0;
			int num2 = index + length;
			bool flag = false;
			for (int i = index; i < num2; i++)
			{
				char c = fileContents[i];
				if (c == '\n' || flag)
				{
					num++;
				}
				flag = c == '\r';
			}
			return num;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x000B3FC4 File Offset: 0x000B21C4
		private Type GetInheritedType(string fileContents, string filename)
		{
			MatchCollection matchCollection = AspGenerator.DirectiveRegex.Matches(fileContents);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return null;
			}
			string text = this.tparser.DefaultDirectiveName.ToLower(Helpers.InvariantCulture);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				GroupCollection groups = match.Groups;
				if (groups.Count >= 6)
				{
					CaptureCollection captures = groups[3].Captures;
					string directiveName = this.GetDirectiveName(captures);
					if (!string.IsNullOrEmpty(directiveName) && string.Compare(directiveName.ToLower(Helpers.InvariantCulture), text, StringComparison.Ordinal) == 0)
					{
						Location location = new Location(null);
						int index = match.Index;
						location.Filename = filename;
						location.BeginLine = this.GetLineNumberForIndex(fileContents, index);
						location.EndLine = location.BeginLine + this.GetNumberOfLinesForRange(fileContents, index, match.Length);
						this.tparser.Location = location;
						this.tparser.allowedMainDirectives = 2;
						this.tparser.AddDirective(text, this.GetDirectiveAttributesDictionary(text, captures, groups[5].Captures));
						return this.tparser.BaseType;
					}
				}
			}
			return null;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000B4130 File Offset: 0x000B2330
		private string ReadFileContents(Stream inputStream, string filename)
		{
			string text = null;
			if (inputStream != null)
			{
				if (inputStream.CanSeek)
				{
					long position = inputStream.Position;
					inputStream.Seek(0L, SeekOrigin.Begin);
					Encoding fileEncoding = WebEncoding.FileEncoding;
					StringBuilder stringBuilder = new StringBuilder();
					byte[] array = new byte[8192];
					int num;
					while ((num = inputStream.Read(array, 0, 8192)) > 0)
					{
						stringBuilder.Append(fileEncoding.GetString(array, 0, num));
					}
					inputStream.Seek(position, SeekOrigin.Begin);
					text = stringBuilder.ToString();
					stringBuilder.Length = 0;
					stringBuilder.Capacity = 0;
				}
				else
				{
					FileStream fileStream = inputStream as FileStream;
					if (fileStream != null)
					{
						string name = fileStream.Name;
						try
						{
							if (File.Exists(name))
							{
								text = File.ReadAllText(name);
							}
						}
						catch
						{
						}
					}
				}
			}
			if (text == null && !string.IsNullOrEmpty(filename) && string.Compare(filename, "@@inner_string@@", StringComparison.Ordinal) != 0)
			{
				try
				{
					if (File.Exists(filename))
					{
						text = File.ReadAllText(filename);
					}
				}
				catch
				{
				}
			}
			return text;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000B4234 File Offset: 0x000B2434
		private Type GetRootBuilderType(Stream inputStream, string filename)
		{
			Type type = null;
			string text;
			if (this.tparser != null)
			{
				text = this.ReadFileContents(inputStream, filename);
			}
			else
			{
				text = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				Type inheritedType = this.GetInheritedType(text, filename);
				if (inheritedType != null)
				{
					FileLevelControlBuilderAttribute fileLevelControlBuilderAttribute;
					try
					{
						object[] customAttributes = inheritedType.GetCustomAttributes(typeof(FileLevelControlBuilderAttribute), true);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							fileLevelControlBuilderAttribute = customAttributes[0] as FileLevelControlBuilderAttribute;
						}
						else
						{
							fileLevelControlBuilderAttribute = null;
						}
					}
					catch
					{
						fileLevelControlBuilderAttribute = null;
					}
					type = ((fileLevelControlBuilderAttribute != null) ? fileLevelControlBuilderAttribute.BuilderType : null);
				}
			}
			if (!(type == null))
			{
				return type;
			}
			if (this.tparser is PageParser)
			{
				return typeof(FileLevelPageControlBuilder);
			}
			if (this.tparser is UserControlParser)
			{
				return typeof(FileLevelUserControlBuilder);
			}
			return typeof(RootBuilder);
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000B4308 File Offset: 0x000B2508
		private void CreateRootBuilder(Stream inputStream, string filename)
		{
			if (this.rootBuilder != null)
			{
				return;
			}
			Type rootBuilderType = this.GetRootBuilderType(inputStream, filename);
			this.rootBuilder = Activator.CreateInstance(rootBuilderType) as RootBuilder;
			if (this.rootBuilder == null)
			{
				throw new HttpException("Cannot create an instance of file-level control builder.");
			}
			this.rootBuilder.Init(this.tparser, null, null, null, null, null);
			if (this.componentFoundry != null)
			{
				this.rootBuilder.Foundry = this.componentFoundry;
			}
			this.stack.Push(this.rootBuilder, null);
			this.tparser.RootBuilder = this.rootBuilder;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000B43A0 File Offset: 0x000B25A0
		private BaseCompiler GetCompilerFromType()
		{
			Type type = this.tparser.GetType();
			if (type == typeof(PageParser))
			{
				return new PageCompiler((PageParser)this.tparser);
			}
			if (type == typeof(ApplicationFileParser))
			{
				return new GlobalAsaxCompiler((ApplicationFileParser)this.tparser);
			}
			if (type == typeof(UserControlParser))
			{
				return new UserControlCompiler((UserControlParser)this.tparser);
			}
			if (type == typeof(MasterPageParser))
			{
				return new MasterPageCompiler((MasterPageParser)this.tparser);
			}
			throw new Exception("Got type: " + type);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000B4458 File Offset: 0x000B2658
		private void InitParser(TextReader reader, string filename)
		{
			AspParser aspParser = new AspParser(filename, reader);
			aspParser.Error += this.ParseError;
			aspParser.TagParsed += this.TagParsed;
			aspParser.TextParsed += this.TextParsed;
			aspParser.ParsingComplete += this.ParsingCompleted;
			this.tparser.AspGenerator = this;
			this.CreateRootBuilder(this.inputStream, filename);
			if (!this.pstack.Push(aspParser))
			{
				throw new ParseException(this.Location, "Infinite recursion detected including file: " + filename);
			}
			if (filename != "@@inner_string@@")
			{
				string text = Path.Combine(this.tparser.BaseVirtualDir, Path.GetFileName(filename));
				if (VirtualPathUtility.IsAbsolute(text))
				{
					text = VirtualPathUtility.ToAppRelative(text);
				}
				this.tparser.AddDependency(text);
			}
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x000B4534 File Offset: 0x000B2734
		private void InitParser(string filename)
		{
			StreamReader streamReader = new StreamReader(filename, WebEncoding.FileEncoding);
			this.InitParser(streamReader, filename);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000B4558 File Offset: 0x000B2758
		private void CheckForDuplicateIds(ControlBuilder root, Stack scopes)
		{
			if (root == null)
			{
				return;
			}
			if (scopes == null)
			{
				scopes = new Stack();
			}
			Dictionary<string, bool> dictionary;
			if (scopes.Count == 0 || root.IsNamingContainer)
			{
				dictionary = new Dictionary<string, bool>(StringComparer.Ordinal);
				scopes.Push(dictionary);
			}
			else
			{
				dictionary = scopes.Peek() as Dictionary<string, bool>;
			}
			if (dictionary == null)
			{
				return;
			}
			ArrayList children = root.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					ControlBuilder controlBuilder = obj as ControlBuilder;
					if (controlBuilder != null)
					{
						string id = controlBuilder.ID;
						if (id != null && id.Length != 0)
						{
							if (dictionary.ContainsKey(id))
							{
								throw new ParseException(controlBuilder.Location, "Id '" + id + "' is already used by another control.");
							}
							dictionary.Add(id, true);
							this.CheckForDuplicateIds(controlBuilder, scopes);
						}
					}
				}
			}
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x000B4648 File Offset: 0x000B2848
		public void Parse(string file)
		{
			this.Parse(file, false);
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000B4654 File Offset: 0x000B2854
		public void Parse(TextReader reader, string filename, bool doInitParser)
		{
			try
			{
				this.isApplication = this.tparser.DefaultDirectiveName == "application";
				if (doInitParser)
				{
					this.InitParser(reader, filename);
				}
				this.pstack.Parser.Parse();
				if (this.text.Length > 0)
				{
					this.FlushText();
				}
				this.tparser.MD5Checksum = this.pstack.Parser.MD5Checksum;
				this.pstack.Pop();
				if (this.stack.Count > 1 && this.pstack.Count == 0)
				{
					throw new ParseException(this.stack.Builder.Location, string.Concat(new object[]
					{
						"Expecting </",
						this.stack.Builder.TagName,
						"> ",
						this.stack.Builder
					}));
				}
				this.CheckForDuplicateIds(this.RootBuilder, null);
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000B4768 File Offset: 0x000B2968
		public void Parse(Stream stream, string filename, bool doInitParser)
		{
			this.inputStream = stream;
			this.Parse(new StreamReader(stream, WebEncoding.FileEncoding), filename, doInitParser);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000B4784 File Offset: 0x000B2984
		public void Parse(string filename, bool doInitParser)
		{
			StreamReader streamReader = new StreamReader(filename, WebEncoding.FileEncoding);
			this.Parse(streamReader, filename, doInitParser);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000B47A8 File Offset: 0x000B29A8
		public void Parse()
		{
			string text = this.tparser.InputFile;
			TextReader reader = this.tparser.Reader;
			try
			{
				if (string.IsNullOrEmpty(text))
				{
					StreamReader streamReader = reader as StreamReader;
					if (streamReader != null)
					{
						FileStream fileStream = streamReader.BaseStream as FileStream;
						if (fileStream != null)
						{
							text = fileStream.Name;
						}
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "@@inner_string@@";
					}
				}
				if (reader != null)
				{
					this.Parse(reader, text, true);
				}
				else
				{
					if (string.IsNullOrEmpty(text))
					{
						throw new HttpException("Parser input file is empty, cannot continue.");
					}
					text = Path.GetFullPath(text);
					this.InitParser(text);
					this.Parse(text);
				}
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000B4858 File Offset: 0x000B2A58
		internal static void AddTypeToCache(List<string> dependencies, string inputFile, Type type)
		{
			if (type == null || inputFile == null || inputFile.Length == 0)
			{
				return;
			}
			if (dependencies == null || dependencies.Count <= 0)
			{
				HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type);
				return;
			}
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
			HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type, new CacheDependency(array));
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000B48FC File Offset: 0x000B2AFC
		public Type GetCompiledType()
		{
			Type type = (Type)HttpRuntime.InternalCache.Get("@@Type" + this.tparser.InputFile);
			if (type != null)
			{
				return type;
			}
			this.Parse();
			type = this.GetCompilerFromType().GetCompiledType();
			AspGenerator.AddTypeToCache(this.tparser.Dependencies, this.tparser.InputFile, type);
			return type;
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x0003C550 File Offset: 0x0003A750
		private void ParseError(ILocation location, string message)
		{
			throw new ParseException(location, message);
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000B4968 File Offset: 0x000B2B68
		private bool ProcessTagsInAttributes(ILocation location, string tagid, TagAttributes attributes, TagType type)
		{
			if (attributes == null || attributes.Count == 0)
			{
				return false;
			}
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\t<{0}", tagid);
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				string text2 = attributes[text] as string;
				if (text2 == null || text2.Length < 16)
				{
					stringBuilder.AppendFormat(" {0}=\"{1}\"", text, text2);
				}
				else
				{
					Match match = AspGenerator.runatServer.Match(attributes[text] as string);
					if (!match.Success)
					{
						stringBuilder.AppendFormat(" {0}=\"{1}\"", text, text2);
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							this.TextParsed(location, stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
						flag = true;
						Group group = match.Groups[0];
						int index = group.Index;
						int length = group.Length;
						this.TextParsed(location, string.Format(" {0}=\"{1}", text, (index > 0) ? text2.Substring(0, index) : string.Empty));
						this.FlushText();
						this.ParseAttributeTag(group.Value, location);
						if (index + length < text2.Length)
						{
							this.TextParsed(location, text2.Substring(index + length) + "\"");
						}
						else
						{
							this.TextParsed(location, "\"");
						}
					}
				}
			}
			if (type == TagType.SelfClosing)
			{
				stringBuilder.Append("/>");
			}
			else
			{
				stringBuilder.Append(">");
			}
			if (flag && stringBuilder.Length > 0)
			{
				this.TextParsed(location, stringBuilder.ToString());
			}
			return flag;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x000B4B4C File Offset: 0x000B2D4C
		private void ParseAttributeTag(string code, ILocation location)
		{
			AspParser aspParser = location as AspParser;
			int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
			AspParser aspParser2 = new AspParser("@@attribute_tag@@", new StringReader(code), location.BeginLine - 1, num, aspParser);
			aspParser2.Error += this.ParseError;
			aspParser2.TagParsed += this.TagParsed;
			aspParser2.TextParsed += this.TextParsed;
			aspParser2.Parse();
			if (this.text.Length > 0)
			{
				this.FlushText();
			}
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000B4BD8 File Offset: 0x000B2DD8
		private void ParsingCompleted()
		{
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter == null)
			{
				return;
			}
			pageParserFilter.ParseComplete(this.RootBuilder);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000B4BFC File Offset: 0x000B2DFC
		private void CheckIfIncludeFileIsSecure(string filePath)
		{
			if (filePath == null || filePath.Length == 0)
			{
				return;
			}
			string text = null;
			Exception ex = null;
			try
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(Path.GetDirectoryName(filePath));
				text = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(currentDirectory);
				if (text[text.Length - 1] != '/')
				{
					text += "/";
				}
			}
			catch (DirectoryNotFoundException)
			{
				return;
			}
			catch (FileNotFoundException)
			{
				return;
			}
			catch (Exception ex)
			{
			}
			if (ex != null || !StrUtils.StartsWith(text, HttpRuntime.AppDomainAppPath))
			{
				throw new ParseException(this.Location, "Files above the application's root directory cannot be included.");
			}
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000B4CA8 File Offset: 0x000B2EA8
		private string ChopOffTagStart(ILocation location, string content, string tagid)
		{
			string text = "<" + tagid;
			if (content.StartsWith(text))
			{
				this.TextParsed(location, text);
				content = content.Substring(text.Length);
			}
			return content;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x000B4CE4 File Offset: 0x000B2EE4
		private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
		{
			this.location = new Location(location);
			if (this.tparser != null)
			{
				this.tparser.Location = location;
			}
			if (this.text.Length != 0)
			{
				bool flag = this.lastTag == TagType.CodeRender;
				this.FlushText(flag);
			}
			if (string.Compare(tagid, "script", true, Helpers.InvariantCulture) == 0)
			{
				if (this.inScript || this.ignore_text)
				{
					if (this.ProcessScript(tagtype, attributes))
					{
						return;
					}
				}
				else if (this.ProcessScript(tagtype, attributes))
				{
					return;
				}
			}
			this.lastTag = tagtype;
			switch (tagtype)
			{
			case TagType.Tag:
			{
				bool flag2;
				if (this.ProcessTag(location, tagid, attributes, tagtype, out flag2))
				{
					if (!flag2)
					{
						this.useOtherTags = true;
						return;
					}
				}
				else
				{
					if (this.useOtherTags)
					{
						this.stack.Builder.EnsureOtherTags();
						this.stack.Builder.OtherTags.Add(tagid);
					}
					string plainText = location.PlainText;
					if (!this.ProcessTagsInAttributes(location, tagid, attributes, TagType.Tag))
					{
						this.TextParsed(location, this.ChopOffTagStart(location, plainText, tagid));
						return;
					}
				}
				break;
			}
			case TagType.Close:
				if ((this.useOtherTags && AspGenerator.TryRemoveTag(tagid, this.stack.Builder.OtherTags)) || !this.CloseControl(tagid))
				{
					this.TextParsed(location, location.PlainText);
					return;
				}
				break;
			case TagType.SelfClosing:
			{
				int count = this.stack.Count;
				bool flag2;
				if (!this.ProcessTag(location, tagid, attributes, tagtype, out flag2) && !flag2)
				{
					string plainText2 = location.PlainText;
					if (!this.ProcessTagsInAttributes(location, tagid, attributes, TagType.SelfClosing))
					{
						this.TextParsed(location, this.ChopOffTagStart(location, plainText2, tagid));
						return;
					}
				}
				else if (this.stack.Count != count)
				{
					this.CloseControl(tagid);
					return;
				}
				break;
			}
			case TagType.Directive:
				if (tagid.Length == 0)
				{
					tagid = this.tparser.DefaultDirectiveName;
				}
				this.tparser.AddDirective(tagid, attributes.GetDictionary(null));
				return;
			case TagType.ServerComment:
				break;
			case TagType.DataBinding:
			case TagType.CodeRender:
			case TagType.CodeRenderExpression:
			case TagType.CodeRenderEncode:
				if (this.isApplication)
				{
					throw new ParseException(location, "Invalid content for application file.");
				}
				this.ProcessCode(tagtype, tagid, location);
				return;
			case TagType.Include:
			{
				if (this.isApplication)
				{
					throw new ParseException(location, "Invalid content for application file.");
				}
				string text = attributes["virtual"] as string;
				bool flag3 = text != null;
				if (!flag3)
				{
					text = attributes["file"] as string;
				}
				if (flag3)
				{
					bool flag4 = false;
					VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
					if (virtualPathProvider.FileExists(text))
					{
						VirtualFile file = virtualPathProvider.GetFile(text);
						if (file != null)
						{
							this.Parse(file.Open(), text, true);
							flag4 = true;
						}
					}
					if (!flag4)
					{
						this.Parse(this.tparser.MapPath(text), true);
						return;
					}
				}
				else
				{
					string includeFilePath = AspGenerator.GetIncludeFilePath(this.tparser.ParserDir, text);
					this.CheckIfIncludeFileIsSecure(includeFilePath);
					this.tparser.PushIncludeDir(Path.GetDirectoryName(includeFilePath));
					try
					{
						this.Parse(includeFilePath, true);
					}
					finally
					{
						this.tparser.PopIncludeDir();
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000B4FE8 File Offset: 0x000B31E8
		private static bool TryRemoveTag(string tagid, ArrayList otags)
		{
			if (otags == null || otags.Count == 0)
			{
				return false;
			}
			for (int i = otags.Count - 1; i >= 0; i--)
			{
				string text = (string)otags[i];
				if (string.Compare(tagid, text, true, Helpers.InvariantCulture) == 0)
				{
					do
					{
						otags.RemoveAt(i);
					}
					while (otags.Count - 1 >= i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000B5046 File Offset: 0x000B3246
		private static string GetIncludeFilePath(string basedir, string filename)
		{
			if (Path.DirectorySeparatorChar == '/')
			{
				filename = filename.Replace("\\", "/");
			}
			return Path.GetFullPath(Path.Combine(basedir, filename));
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000B506F File Offset: 0x000B326F
		private bool CheckTagEndNeeded(string text)
		{
			return !text.EndsWith("/>");
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000B5080 File Offset: 0x000B3280
		private List<TextBlock> FindRegexBlocks(Regex rxStart, Regex rxEnd, AspGenerator.CheckBlockEnd checkEnd, IList blocks, TextBlockType typeForMatches, bool discardBlocks)
		{
			List<TextBlock> list = new List<TextBlock>();
			foreach (object obj in blocks)
			{
				TextBlock textBlock = (TextBlock)obj;
				if (textBlock.Type != TextBlockType.Verbatim)
				{
					list.Add(textBlock);
				}
				else
				{
					int num = 0;
					MatchCollection matchCollection = rxStart.Matches(textBlock.Content);
					bool flag = matchCollection.Count > 0;
					foreach (object obj2 in matchCollection)
					{
						Match match = (Match)obj2;
						flag = true;
						int index = match.Index;
						if (num < index)
						{
							list.Add(new TextBlock(TextBlockType.Verbatim, textBlock.Content.Substring(num, index - num)));
						}
						string text = match.Value;
						if (rxEnd != null && checkEnd(text))
						{
							int num2 = index + text.Length;
							Match match2 = rxEnd.Match(textBlock.Content, num2);
							if (match2.Success)
							{
								text = text + textBlock.Content.Substring(num2, match2.Index - num2) + match2.Value;
							}
						}
						if (!discardBlocks)
						{
							list.Add(new TextBlock(typeForMatches, text));
						}
						num = index + text.Length;
					}
					if (num > 0 && num < textBlock.Content.Length)
					{
						list.Add(new TextBlock(TextBlockType.Verbatim, textBlock.Content.Substring(num)));
					}
					if (!flag)
					{
						list.Add(textBlock);
					}
				}
			}
			return list;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000B524C File Offset: 0x000B344C
		private IList SplitTextIntoBlocks(string text)
		{
			List<TextBlock> list = new List<TextBlock>();
			list.Add(new TextBlock(TextBlockType.Verbatim, text));
			list = this.FindRegexBlocks(AspGenerator.clientCommentRegex, null, null, list, TextBlockType.Comment, false);
			list = this.FindRegexBlocks(AspGenerator.runatServer, AspGenerator.endOfTag, new AspGenerator.CheckBlockEnd(this.CheckTagEndNeeded), list, TextBlockType.Tag, false);
			return this.FindRegexBlocks(AspGenerator.expressionRegex, null, null, list, TextBlockType.Expression, false);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000B52B0 File Offset: 0x000B34B0
		private void TextParsed(ILocation location, string text)
		{
			if (this.ignore_text)
			{
				return;
			}
			if (this.inScript)
			{
				this.text.Append(text);
				this.FlushText(true);
				return;
			}
			foreach (object obj in this.SplitTextIntoBlocks(text))
			{
				TextBlock textBlock = (TextBlock)obj;
				switch (textBlock.Type)
				{
				case TextBlockType.Verbatim:
					this.text.Append(textBlock.Content);
					break;
				case TextBlockType.Expression:
					if (this.text.Length > 0)
					{
						this.FlushText(true);
					}
					new AspGenerator.CodeRenderParser(textBlock.Content, this.stack.Builder, location).AddChildren(this);
					break;
				case TextBlockType.Tag:
					this.ParseAttributeTag(textBlock.Content, location);
					break;
				case TextBlockType.Comment:
					if (this.javascript)
					{
						this.text.Append(textBlock.Content);
					}
					else
					{
						this.text.Append("<!--");
						this.FlushText(true);
						string text2 = textBlock.Content.Substring(4, textBlock.Length - 7);
						bool flag;
						if (text2.EndsWith("<![endif]"))
						{
							text2 = text2.Substring(0, text2.Length - 9);
							flag = true;
						}
						else
						{
							flag = false;
						}
						AspParser aspParser = location as AspParser;
						int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
						AspParser aspParser2 = new AspParser("@@comment_code@@", new StringReader(text2), location.BeginLine - 1, num, aspParser);
						aspParser2.Error += this.ParseError;
						aspParser2.TagParsed += this.TagParsed;
						aspParser2.TextParsed += this.TextParsed;
						aspParser2.Parse();
						if (flag)
						{
							this.text.Append("<![endif]");
						}
						this.text.Append("-->");
						this.FlushText(true);
					}
					break;
				}
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000B54D0 File Offset: 0x000B36D0
		private void FlushText()
		{
			this.FlushText(false);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000B54DC File Offset: 0x000B36DC
		private void FlushText(bool ignoreEmptyString)
		{
			string text = this.text.ToString();
			this.text.Length = 0;
			if (ignoreEmptyString && text.Trim().Length == 0)
			{
				return;
			}
			if (this.inScript)
			{
				PageParserFilter pageParserFilter = this.PageParserFilter;
				if (pageParserFilter != null && !pageParserFilter.ProcessCodeConstruct(CodeConstructType.ScriptTag, text))
				{
					return;
				}
				this.tparser.Scripts.Add(new ServerSideScript(text, new Location(this.tparser.Location)));
				return;
			}
			else
			{
				if (this.tparser.DefaultDirectiveName == "application" && text.Trim() != "")
				{
					throw new ParseException(this.location, "Content not valid for application file.");
				}
				ControlBuilder builder = this.stack.Builder;
				builder.AppendLiteralString(text);
				if (builder.NeedsTagInnerText())
				{
					this.tagInnerText.Append(text);
				}
				return;
			}
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000B55B8 File Offset: 0x000B37B8
		private bool BuilderHasOtherThan(Type type, ControlBuilder cb)
		{
			ArrayList arrayList = cb.OtherTags;
			if (arrayList != null && arrayList.Count > 0)
			{
				return true;
			}
			arrayList = cb.Children;
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					if (obj != null)
					{
						ControlBuilder controlBuilder = obj as ControlBuilder;
						if (controlBuilder == null)
						{
							string text = obj as string;
							if (text == null || !string.IsNullOrEmpty(text.Trim()))
							{
								return true;
							}
						}
						else if (!(controlBuilder is ContentBuilderInternal) && controlBuilder.ControlType != typeof(Content))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000B5678 File Offset: 0x000B3878
		private bool OtherControlsAllowed(ControlBuilder cb)
		{
			return cb == null || !typeof(Content).IsAssignableFrom(cb.ControlType) || !this.BuilderHasOtherThan(typeof(Content), this.RootBuilder);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000B56B4 File Offset: 0x000B38B4
		public void AddControl(Type type, IDictionary attributes)
		{
			ControlBuilder builder = this.stack.Builder;
			ControlBuilder controlBuilder = ControlBuilder.CreateBuilderFromType(this.tparser, builder, type, null, null, attributes, this.location.BeginLine, this.location.Filename);
			if (controlBuilder != null)
			{
				builder.AppendSubBuilder(controlBuilder);
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000B5700 File Offset: 0x000B3900
		private bool ProcessTag(ILocation location, string tagid, TagAttributes atts, TagType tagtype, out bool ignored)
		{
			ignored = false;
			if (this.isApplication && string.Compare(tagid, "object", true, Helpers.InvariantCulture) != 0)
			{
				throw new ParseException(location, "Invalid tag for application file.");
			}
			ControlBuilder builder = this.stack.Builder;
			ControlBuilder controlBuilder = null;
			if (builder != null && builder.ControlType == typeof(HtmlTable) && (string.Compare(tagid, "thead", true, Helpers.InvariantCulture) == 0 || string.Compare(tagid, "tbody", true, Helpers.InvariantCulture) == 0))
			{
				ignored = true;
				return true;
			}
			IDictionary dictionary = ((atts != null) ? atts.GetDictionary(null) : AspGenerator.emptyHash);
			if (this.stack.Count > 1)
			{
				try
				{
					controlBuilder = builder.CreateSubBuilder(tagid, dictionary, null, this.tparser, location);
				}
				catch (TypeLoadException ex)
				{
					throw new ParseException(this.Location, "Type not found.", ex);
				}
				catch (Exception ex2)
				{
					throw new ParseException(this.Location, ex2.Message, ex2);
				}
			}
			bool flag = atts != null && atts.IsRunAtServer();
			if (controlBuilder == null && flag)
			{
				string text = dictionary["id"] as string;
				if (text != null && !CodeGenerator.IsValidLanguageIndependentIdentifier(text))
				{
					throw new ParseException(this.Location, "'" + text + "' is not a valid identifier");
				}
				try
				{
					controlBuilder = this.RootBuilder.CreateSubBuilder(tagid, dictionary, null, this.tparser, location);
				}
				catch (TypeLoadException ex3)
				{
					throw new ParseException(this.Location, "Type not found.", ex3);
				}
				catch (HttpException ex4)
				{
					CompilationException ex5 = ex4.InnerException as CompilationException;
					if (ex5 != null)
					{
						throw ex5;
					}
					throw new ParseException(this.Location, ex4.Message, ex4);
				}
				catch (Exception ex6)
				{
					throw new ParseException(this.Location, ex6.Message, ex6);
				}
			}
			if (controlBuilder == null)
			{
				return false;
			}
			string plainText = location.PlainText;
			if (!flag && plainText.IndexOf("<%$") == -1 && plainText.IndexOf("<%") > -1)
			{
				return false;
			}
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter != null && !pageParserFilter.AllowControl(controlBuilder.ControlType, controlBuilder))
			{
				throw new ParseException(this.Location, "Control type '" + controlBuilder.ControlType + "' not allowed.");
			}
			if (!this.OtherControlsAllowed(controlBuilder))
			{
				throw new ParseException(this.Location, "Only Content controls are allowed directly in a content page that contains Content controls.");
			}
			controlBuilder.Location = location;
			controlBuilder.ID = dictionary["id"] as string;
			if (typeof(HtmlForm).IsAssignableFrom(controlBuilder.ControlType))
			{
				if (this.inForm)
				{
					throw new ParseException(location, "Only one <form> allowed.");
				}
				this.inForm = true;
			}
			if (controlBuilder.HasBody() && !(controlBuilder is ObjectTagBuilder))
			{
				TemplateBuilder templateBuilder = controlBuilder as TemplateBuilder;
				this.stack.Push(controlBuilder, location);
			}
			else
			{
				if (!this.isApplication && controlBuilder is ObjectTagBuilder)
				{
					ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)controlBuilder;
					if (objectTagBuilder.Scope != null && objectTagBuilder.Scope.Length > 0)
					{
						throw new ParseException(location, "Scope not allowed here");
					}
					if (tagtype == TagType.Tag)
					{
						this.stack.Push(controlBuilder, location);
						return true;
					}
				}
				builder.AppendSubBuilder(controlBuilder);
				controlBuilder.CloseControl();
			}
			return true;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000B5A4C File Offset: 0x000B3C4C
		private string ReadFile(string filename)
		{
			string text;
			using (StreamReader streamReader = new StreamReader(this.tparser.MapPath(filename), WebEncoding.FileEncoding))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000B5A94 File Offset: 0x000B3C94
		private bool ProcessScript(TagType tagtype, TagAttributes attributes)
		{
			if (tagtype == TagType.Close)
			{
				bool flag;
				if (this.inScript)
				{
					flag = this.inScript;
					this.inScript = false;
				}
				else if (!this.ignore_text)
				{
					flag = this.javascript;
					this.javascript = false;
					this.TextParsed(this.location, this.location.PlainText);
				}
				else
				{
					this.ignore_text = false;
					flag = true;
				}
				return flag;
			}
			if (attributes != null && attributes.IsRunAtServer())
			{
				string text = (string)attributes["language"];
				if (text != null && text.Length > 0 && this.tparser.ImplicitLanguage)
				{
					this.tparser.SetLanguage(text);
				}
				this.CheckLanguage(text);
				string text2 = (string)attributes["src"];
				if (text2 != null)
				{
					if (text2.Length == 0)
					{
						throw new ParseException(this.Parser, "src cannot be an empty string");
					}
					string text3 = this.ReadFile(text2);
					this.inScript = true;
					this.TextParsed(this.Parser, text3);
					this.FlushText();
					this.inScript = false;
					if (tagtype != TagType.SelfClosing)
					{
						this.ignore_text = true;
						this.Parser.VerbatimID = "script";
					}
				}
				else if (tagtype == TagType.Tag)
				{
					this.Parser.VerbatimID = "script";
					this.inScript = true;
				}
				return true;
			}
			if (tagtype != TagType.SelfClosing)
			{
				this.Parser.VerbatimID = "script";
				this.javascript = true;
			}
			string text4 = this.location.PlainText;
			if (text4.StartsWith("<script"))
			{
				this.TextParsed(this.location, "<script");
				text4 = text4.Substring(7);
			}
			this.TextParsed(this.location, text4);
			return true;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000B5C38 File Offset: 0x000B3E38
		private bool CloseControl(string tagid)
		{
			ControlBuilder builder = this.stack.Builder;
			string originalTagName = builder.OriginalTagName;
			if (string.Compare(originalTagName, "tbody", true, Helpers.InvariantCulture) != 0 && string.Compare(tagid, "tbody", true, Helpers.InvariantCulture) == 0)
			{
				if (!builder.ChildrenAsProperties)
				{
					try
					{
						this.TextParsed(this.location, this.location.PlainText);
						this.FlushText();
					}
					catch
					{
					}
				}
				return true;
			}
			if (builder.ControlType == typeof(HtmlTable) && string.Compare(tagid, "thead", true, Helpers.InvariantCulture) == 0)
			{
				return true;
			}
			if (string.Compare(tagid, originalTagName, true, Helpers.InvariantCulture) != 0)
			{
				return false;
			}
			if (builder.NeedsTagInnerText())
			{
				try
				{
					builder.SetTagInnerText(this.tagInnerText.ToString());
				}
				catch (Exception ex)
				{
					throw new ParseException(builder.Location, ex.Message, ex);
				}
				this.tagInnerText.Length = 0;
			}
			if (typeof(HtmlForm).IsAssignableFrom(builder.ControlType))
			{
				this.inForm = false;
			}
			builder.CloseControl();
			this.stack.Pop();
			this.stack.Builder.AppendSubBuilder(builder);
			return true;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000B5D80 File Offset: 0x000B3F80
		private CodeConstructType MapTagTypeToConstructType(TagType tagtype)
		{
			switch (tagtype)
			{
			case TagType.DataBinding:
				return CodeConstructType.DataBindingSnippet;
			case TagType.CodeRender:
			case TagType.CodeRenderEncode:
				return CodeConstructType.CodeSnippet;
			case TagType.CodeRenderExpression:
				return CodeConstructType.ExpressionSnippet;
			}
			throw new InvalidOperationException("Unexpected tag type.");
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000B5DB0 File Offset: 0x000B3FB0
		private bool ProcessCode(TagType tagtype, string code, ILocation location)
		{
			PageParserFilter pageParserFilter = this.PageParserFilter;
			if (pageParserFilter != null && (!pageParserFilter.AllowCode || pageParserFilter.ProcessCodeConstruct(this.MapTagTypeToConstructType(tagtype), code)))
			{
				return true;
			}
			ControlBuilder controlBuilder;
			if (tagtype == TagType.CodeRender)
			{
				controlBuilder = new CodeRenderBuilder(code, false, location);
			}
			else if (tagtype == TagType.CodeRenderExpression)
			{
				controlBuilder = new CodeRenderBuilder(code, true, location);
			}
			else if (tagtype == TagType.DataBinding)
			{
				controlBuilder = new DataBindingBuilder(code, location);
			}
			else
			{
				if (tagtype != TagType.CodeRenderEncode)
				{
					throw new HttpException("Should never happen");
				}
				controlBuilder = new CodeRenderBuilder(code, true, location, true);
			}
			this.stack.Builder.AppendSubBuilder(controlBuilder);
			return true;
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x000B5E3D File Offset: 0x000B403D
		public ILocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000B5E48 File Offset: 0x000B4048
		private void CheckLanguage(string lang)
		{
			if (lang == null || lang == "")
			{
				return;
			}
			if (string.Compare(lang, this.tparser.Language, true, Helpers.InvariantCulture) == 0)
			{
				return;
			}
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			if (compilationSection.Compilers[this.tparser.Language] != compilationSection.Compilers[lang])
			{
				throw new ParseException(this.Location, string.Format("Trying to mix language '{0}' and '{1}'.", this.tparser.Language, lang));
			}
		}

		// Token: 0x0400240A RID: 9226
		private const int READ_BUFFER_SIZE = 8192;

		// Token: 0x0400240B RID: 9227
		internal static Regex DirectiveRegex = new Regex("<%\\s*@(\\s*(?<attrname>\\w[\\w:]*(?=\\W))(\\s*(?<equal>=)\\s*\"(?<attrval>[^\"]*)\"|\\s*(?<equal>=)\\s*'(?<attrval>[^']*)'|\\s*(?<equal>=)\\s*(?<attrval>[^\\s%>]*)|(?<equal>)(?<attrval>\\s*?)))*\\s*?%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400240C RID: 9228
		private static readonly Regex runatServer = new Regex("<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*(?:/>|>)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400240D RID: 9229
		private static readonly Regex endOfTag = new Regex("</[\\w:\\.]+\\s*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400240E RID: 9230
		private static readonly Regex expressionRegex = new Regex("<%.*?%>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400240F RID: 9231
		private static readonly Regex clientCommentRegex = new Regex("<!--(.|\\s)*?-->", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04002410 RID: 9232
		private ParserStack pstack;

		// Token: 0x04002411 RID: 9233
		private BuilderLocationStack stack;

		// Token: 0x04002412 RID: 9234
		private TemplateParser tparser;

		// Token: 0x04002413 RID: 9235
		private StringBuilder text;

		// Token: 0x04002414 RID: 9236
		private RootBuilder rootBuilder;

		// Token: 0x04002415 RID: 9237
		private bool inScript;

		// Token: 0x04002416 RID: 9238
		private bool javascript;

		// Token: 0x04002417 RID: 9239
		private bool ignore_text;

		// Token: 0x04002418 RID: 9240
		private ILocation location;

		// Token: 0x04002419 RID: 9241
		private bool isApplication;

		// Token: 0x0400241A RID: 9242
		private StringBuilder tagInnerText = new StringBuilder();

		// Token: 0x0400241B RID: 9243
		private static IDictionary emptyHash = new Dictionary<string, object>();

		// Token: 0x0400241C RID: 9244
		private bool inForm;

		// Token: 0x0400241D RID: 9245
		private bool useOtherTags;

		// Token: 0x0400241E RID: 9246
		private TagType lastTag;

		// Token: 0x0400241F RID: 9247
		private AspComponentFoundry componentFoundry;

		// Token: 0x04002420 RID: 9248
		private Stream inputStream;

		// Token: 0x02000627 RID: 1575
		// (Invoke) Token: 0x0600438F RID: 17295
		private delegate bool CheckBlockEnd(string text);

		// Token: 0x02000628 RID: 1576
		private class CodeRenderParser
		{
			// Token: 0x06004392 RID: 17298 RVA: 0x000B5F50 File Offset: 0x000B4150
			public CodeRenderParser(string str, ControlBuilder builder, ILocation location)
			{
				this.str = str;
				this.builder = builder;
				this.location = location;
			}

			// Token: 0x06004393 RID: 17299 RVA: 0x000B5F6D File Offset: 0x000B416D
			public void AddChildren(AspGenerator generator)
			{
				this.generator = generator;
				if (this.str.IndexOf("<%") > 0)
				{
					this.DoParseExpressions(this.str);
					return;
				}
				this.DoParse(this.str);
			}

			// Token: 0x06004394 RID: 17300 RVA: 0x000B5FA4 File Offset: 0x000B41A4
			private void DoParseExpressions(string str)
			{
				int num = 0;
				int num2 = 0;
				Regex regex = new Regex("(<%(?!@)(?<code>(.|\\s)*?)%>)|(<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*?/>)", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				int length = str.Length;
				while (num2 > -1 && num < length)
				{
					Match match = regex.Match(str, num2);
					if (!match.Success)
					{
						break;
					}
					string value = match.Value;
					num2 = match.Index;
					if (num2 > num)
					{
						this.TextParsed(null, str.Substring(num, num2 - num));
					}
					this.DoParse(value);
					num2 += value.Length;
					num = num2;
					if (num2 >= length)
					{
						break;
					}
					num2 = str.IndexOf('<', num2);
				}
				if (num < length)
				{
					this.TextParsed(null, str.Substring(num));
				}
			}

			// Token: 0x06004395 RID: 17301 RVA: 0x000B6044 File Offset: 0x000B4244
			private void DoParse(string str)
			{
				AspParser aspParser = this.location as AspParser;
				int num = ((aspParser != null) ? aspParser.BeginPosition : 0);
				AspParser aspParser2 = new AspParser("@@code_render@@", new StringReader(str), this.location.BeginLine - 1, num, aspParser);
				aspParser2.Error += this.ParseError;
				aspParser2.TagParsed += this.TagParsed;
				aspParser2.TextParsed += this.TextParsed;
				aspParser2.Parse();
			}

			// Token: 0x06004396 RID: 17302 RVA: 0x000B60C4 File Offset: 0x000B42C4
			private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
			{
				switch (tagtype)
				{
				case TagType.Tag:
				case TagType.Close:
				case TagType.SelfClosing:
					if (this.generator != null)
					{
						this.generator.TagParsed(location, tagtype, tagid, attributes);
						return;
					}
					break;
				case TagType.DataBinding:
					this.builder.AppendSubBuilder(new DataBindingBuilder(tagid, location));
					return;
				case TagType.CodeRender:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, false, location));
					return;
				case TagType.CodeRenderExpression:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, true, location));
					return;
				case TagType.CodeRenderEncode:
					this.builder.AppendSubBuilder(new CodeRenderBuilder(tagid, true, location, true));
					return;
				}
				string plainText = location.PlainText;
				if (plainText != null && plainText.Trim().Length > 0)
				{
					this.builder.AppendLiteralString(plainText);
				}
			}

			// Token: 0x06004397 RID: 17303 RVA: 0x000B6190 File Offset: 0x000B4390
			private void TextParsed(ILocation location, string text)
			{
				this.builder.AppendLiteralString(text);
			}

			// Token: 0x06004398 RID: 17304 RVA: 0x0003C550 File Offset: 0x0003A750
			private void ParseError(ILocation location, string message)
			{
				throw new ParseException(location, message);
			}

			// Token: 0x04002421 RID: 9249
			private string str;

			// Token: 0x04002422 RID: 9250
			private ControlBuilder builder;

			// Token: 0x04002423 RID: 9251
			private AspGenerator generator;

			// Token: 0x04002424 RID: 9252
			private ILocation location;
		}
	}
}
