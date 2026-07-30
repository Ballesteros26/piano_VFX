using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200066E RID: 1646
	internal abstract class TemplateBuildProvider : GenericBuildProvider<TemplateParser>
	{
		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x000C1BC9 File Offset: 0x000BFDC9
		internal override string LanguageName
		{
			get
			{
				if (string.IsNullOrEmpty(this.compilationLanguage))
				{
					this.ExtractDependencies();
					if (string.IsNullOrEmpty(this.compilationLanguage))
					{
						this.compilationLanguage = base.LanguageName;
					}
				}
				return this.compilationLanguage;
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000C1C00 File Offset: 0x000BFE00
		static TemplateBuildProvider()
		{
			TemplateBuildProvider.directiveAttributes.Add("Control", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractControlDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Master", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPageOrMasterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("MasterType", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPreviousPageTypeOrMasterTypeDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Page", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPageOrMasterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("PreviousPageType", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractPreviousPageTypeOrMasterTypeDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Reference", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractReferenceDependencies));
			TemplateBuildProvider.directiveAttributes.Add("Register", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractRegisterDependencies));
			TemplateBuildProvider.directiveAttributes.Add("WebHandler", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractLanguage));
			TemplateBuildProvider.directiveAttributes.Add("WebService", new TemplateBuildProvider.ExtractDirectiveDependencies(TemplateBuildProvider.ExtractLanguage));
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000C1D25 File Offset: 0x000BFF25
		private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values)
		{
			return TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, name, names, values, true);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000C1D34 File Offset: 0x000BFF34
		private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values, bool isPath)
		{
			if (names.Count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			int count = values.Count;
			using (IEnumerator enumerator = names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(((Capture)enumerator.Current).Value, name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						num++;
					}
					else
					{
						if (num > count)
						{
							return string.Empty;
						}
						if (!isPath)
						{
							return values[num].Value.Trim();
						}
						string text = values[num].Value.Trim(TemplateBuildProvider.directiveValueTrimChars);
						if (string.IsNullOrEmpty(text))
						{
							return string.Empty;
						}
						return new VirtualPath(text, baseDirectory).Absolute;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000C1E18 File Offset: 0x000C0018
		private static void ExtractControlDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			TemplateBuildProvider.ExtractLanguage(baseDirectory, names, values, bp);
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000C1E2C File Offset: 0x000C002C
		private static void ExtractLanguage(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Language", names, values, false);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			bp.compilationLanguage = text;
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000C1E64 File Offset: 0x000C0064
		private static void ExtractPageOrMasterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			TemplateBuildProvider.ExtractLanguage(baseDirectory, names, values, bp);
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "MasterPageFile", names, values);
			if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
			{
				bp.dependencies.Add(text, true);
			}
			TemplateBuildProvider.ExtractCodeBehind(baseDirectory, names, values, bp);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000C1EB4 File Offset: 0x000C00B4
		private static void ExtractCodeBehind(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			foreach (string text in new string[]
			{
				TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "CodeFile", names, values),
				TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Src", names, values)
			})
			{
				if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
				{
					bp.dependencies.Add(text, true);
				}
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000C1F20 File Offset: 0x000C0120
		private static void ExtractRegisterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Src", names, values);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (string.IsNullOrEmpty(TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "TagName", names, values)))
			{
				return;
			}
			if (string.IsNullOrEmpty(TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "TagPrefix", names, values)))
			{
				return;
			}
			if (bp.dependencies.ContainsKey(text))
			{
				return;
			}
			bp.dependencies.Add(text, true);
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x000C1F8C File Offset: 0x000C018C
		private static void ExtractPreviousPageTypeOrMasterTypeDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (bp.dependencies.ContainsKey(text))
			{
				return;
			}
			bp.dependencies.Add(text, true);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000C1FCC File Offset: 0x000C01CC
		private static void ExtractReferenceDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
		{
			string text = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Control", names, values);
			string text2 = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
			string text3 = TemplateBuildProvider.ExtractDirectiveAttribute(baseDirectory, "Page", names, values);
			bool flag = string.IsNullOrEmpty(text);
			bool flag2 = string.IsNullOrEmpty(text2);
			bool flag3 = string.IsNullOrEmpty(text3);
			if (flag && flag2 && flag3)
			{
				return;
			}
			if ((flag ? 1 : 0) + (flag2 ? 1 : 0) + (flag3 ? 1 : 0) != 2)
			{
				return;
			}
			string text4;
			if (!flag)
			{
				text4 = text;
			}
			else if (!flag2)
			{
				text4 = text2;
			}
			else
			{
				text4 = text3;
			}
			if (bp.dependencies.ContainsKey(text4))
			{
				return;
			}
			bp.dependencies.Add(text4, true);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000C2074 File Offset: 0x000C0274
		private IDictionary<string, bool> AddParsedDependencies(IDictionary<string, bool> dict)
		{
			if (base.Parsed)
			{
				List<string> list = base.Parser.Dependencies;
				if (list == null || list.Count > 0)
				{
					return dict;
				}
				if (dict == null)
				{
					dict = this.dependencies;
					if (dict == null)
					{
						dict = (this.dependencies = new SortedDictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
					}
				}
				foreach (string text in list)
				{
					string text2 = text as string;
					if (text2 != null && !dict.ContainsKey(text2))
					{
						dict.Add(text2, true);
					}
				}
			}
			if (dict == null || dict.Count == 0)
			{
				return null;
			}
			return dict;
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000C212C File Offset: 0x000C032C
		internal override IDictionary<string, bool> ExtractDependencies()
		{
			if (this.dependencies != null)
			{
				return this.AddParsedDependencies(this.dependencies);
			}
			string virtualPath = base.VirtualPath;
			if (string.IsNullOrEmpty(virtualPath))
			{
				return this.AddParsedDependencies(null);
			}
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			if (!virtualPathProvider.FileExists(virtualPath))
			{
				return this.AddParsedDependencies(null);
			}
			VirtualFile file = virtualPathProvider.GetFile(virtualPath);
			if (file == null)
			{
				return this.AddParsedDependencies(null);
			}
			string text;
			using (Stream stream = file.Open())
			{
				if (stream == null || !stream.CanRead)
				{
					return this.AddParsedDependencies(null);
				}
				using (StreamReader streamReader = new StreamReader(stream, WebEncoding.FileEncoding))
				{
					text = streamReader.ReadToEnd();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return this.AddParsedDependencies(null);
			}
			MatchCollection matchCollection = AspGenerator.DirectiveRegex.Matches(text);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return this.AddParsedDependencies(null);
			}
			this.dependencies = new SortedDictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			string directory = VirtualPathUtility.GetDirectory(virtualPath);
			foreach (object obj in matchCollection)
			{
				GroupCollection groups = ((Match)obj).Groups;
				if (groups.Count >= 6)
				{
					CaptureCollection captures = groups[3].Captures;
					string value = captures[0].Value;
					TemplateBuildProvider.ExtractDirectiveDependencies extractDirectiveDependencies;
					if (TemplateBuildProvider.directiveAttributes.TryGetValue(value, out extractDirectiveDependencies))
					{
						extractDirectiveDependencies(directory, captures, groups[5].Captures, this);
					}
				}
			}
			return this.AddParsedDependencies(this.dependencies);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000C22F4 File Offset: 0x000C04F4
		protected override string GetClassType(BaseCompiler compiler, TemplateParser parser)
		{
			if (compiler != null)
			{
				return compiler.MainClassType;
			}
			return null;
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000C2301 File Offset: 0x000C0501
		protected override ICollection GetParserDependencies(TemplateParser parser)
		{
			if (parser != null)
			{
				return parser.Dependencies;
			}
			return null;
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x000C230E File Offset: 0x000C050E
		protected override string GetParserLanguage(TemplateParser parser)
		{
			if (parser != null)
			{
				return parser.Language;
			}
			return null;
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x000C231B File Offset: 0x000C051B
		protected override string GetCodeBehindSource(TemplateParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(parser.CodeBehindSource))
			{
				return null;
			}
			return parser.CodeBehindSource;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000C2337 File Offset: 0x000C0537
		protected override AspGenerator CreateAspGenerator(TemplateParser parser)
		{
			if (parser != null)
			{
				return new AspGenerator(parser);
			}
			return null;
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x000C2344 File Offset: 0x000C0544
		protected override List<string> GetReferencedAssemblies(TemplateParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			List<string> assemblies = parser.Assemblies;
			if (assemblies == null || assemblies.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (string text in assemblies)
			{
				if (!string.IsNullOrEmpty(text) && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x04002542 RID: 9538
		private static SortedDictionary<string, TemplateBuildProvider.ExtractDirectiveDependencies> directiveAttributes = new SortedDictionary<string, TemplateBuildProvider.ExtractDirectiveDependencies>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04002543 RID: 9539
		private static char[] directiveValueTrimChars = new char[] { ' ', '\t', '\r', '\n', '"', '\'' };

		// Token: 0x04002544 RID: 9540
		private SortedDictionary<string, bool> dependencies;

		// Token: 0x04002545 RID: 9541
		private string compilationLanguage;

		// Token: 0x0200066F RID: 1647
		// (Invoke) Token: 0x06004669 RID: 18025
		private delegate void ExtractDirectiveDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp);
	}
}
