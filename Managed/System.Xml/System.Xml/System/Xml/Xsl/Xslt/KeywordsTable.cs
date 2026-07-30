using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200057B RID: 1403
	internal class KeywordsTable
	{
		// Token: 0x060037AD RID: 14253 RVA: 0x00135B0C File Offset: 0x00133D0C
		public KeywordsTable(XmlNameTable nt)
		{
			this.NameTable = nt;
			this.AnalyzeString = nt.Add("analyze-string");
			this.ApplyImports = nt.Add("apply-imports");
			this.ApplyTemplates = nt.Add("apply-templates");
			this.Assembly = nt.Add("assembly");
			this.Attribute = nt.Add("attribute");
			this.AttributeSet = nt.Add("attribute-set");
			this.CallTemplate = nt.Add("call-template");
			this.CaseOrder = nt.Add("case-order");
			this.CDataSectionElements = nt.Add("cdata-section-elements");
			this.Character = nt.Add("character");
			this.CharacterMap = nt.Add("character-map");
			this.Choose = nt.Add("choose");
			this.Comment = nt.Add("comment");
			this.Copy = nt.Add("copy");
			this.CopyOf = nt.Add("copy-of");
			this.Count = nt.Add("count");
			this.DataType = nt.Add("data-type");
			this.DecimalFormat = nt.Add("decimal-format");
			this.DecimalSeparator = nt.Add("decimal-separator");
			this.DefaultCollation = nt.Add("default-collation");
			this.DefaultValidation = nt.Add("default-validation");
			this.Digit = nt.Add("digit");
			this.DisableOutputEscaping = nt.Add("disable-output-escaping");
			this.DocTypePublic = nt.Add("doctype-public");
			this.DocTypeSystem = nt.Add("doctype-system");
			this.Document = nt.Add("document");
			this.Element = nt.Add("element");
			this.Elements = nt.Add("elements");
			this.Encoding = nt.Add("encoding");
			this.ExcludeResultPrefixes = nt.Add("exclude-result-prefixes");
			this.ExtensionElementPrefixes = nt.Add("extension-element-prefixes");
			this.Fallback = nt.Add("fallback");
			this.ForEach = nt.Add("for-each");
			this.ForEachGroup = nt.Add("for-each-group");
			this.Format = nt.Add("format");
			this.From = nt.Add("from");
			this.Function = nt.Add("function");
			this.GroupingSeparator = nt.Add("grouping-separator");
			this.GroupingSize = nt.Add("grouping-size");
			this.Href = nt.Add("href");
			this.Id = nt.Add("id");
			this.If = nt.Add("if");
			this.ImplementsPrefix = nt.Add("implements-prefix");
			this.Import = nt.Add("import");
			this.ImportSchema = nt.Add("import-schema");
			this.Include = nt.Add("include");
			this.Indent = nt.Add("indent");
			this.Infinity = nt.Add("infinity");
			this.Key = nt.Add("key");
			this.Lang = nt.Add("lang");
			this.Language = nt.Add("language");
			this.LetterValue = nt.Add("letter-value");
			this.Level = nt.Add("level");
			this.Match = nt.Add("match");
			this.MatchingSubstring = nt.Add("matching-substring");
			this.MediaType = nt.Add("media-type");
			this.Message = nt.Add("message");
			this.Method = nt.Add("method");
			this.MinusSign = nt.Add("minus-sign");
			this.Mode = nt.Add("mode");
			this.Name = nt.Add("name");
			this.Namespace = nt.Add("namespace");
			this.NamespaceAlias = nt.Add("namespace-alias");
			this.NaN = nt.Add("NaN");
			this.NextMatch = nt.Add("next-match");
			this.NonMatchingSubstring = nt.Add("non-matching-substring");
			this.Number = nt.Add("number");
			this.OmitXmlDeclaration = nt.Add("omit-xml-declaration");
			this.Otherwise = nt.Add("otherwise");
			this.Order = nt.Add("order");
			this.Output = nt.Add("output");
			this.OutputCharacter = nt.Add("output-character");
			this.OutputVersion = nt.Add("output-version");
			this.Param = nt.Add("param");
			this.PatternSeparator = nt.Add("pattern-separator");
			this.Percent = nt.Add("percent");
			this.PerformSort = nt.Add("perform-sort");
			this.PerMille = nt.Add("per-mille");
			this.PreserveSpace = nt.Add("preserve-space");
			this.Priority = nt.Add("priority");
			this.ProcessingInstruction = nt.Add("processing-instruction");
			this.Required = nt.Add("required");
			this.ResultDocument = nt.Add("result-document");
			this.ResultPrefix = nt.Add("result-prefix");
			this.Script = nt.Add("script");
			this.Select = nt.Add("select");
			this.Separator = nt.Add("separator");
			this.Sequence = nt.Add("sequence");
			this.Sort = nt.Add("sort");
			this.Space = nt.Add("space");
			this.Standalone = nt.Add("standalone");
			this.StripSpace = nt.Add("strip-space");
			this.Stylesheet = nt.Add("stylesheet");
			this.StylesheetPrefix = nt.Add("stylesheet-prefix");
			this.Template = nt.Add("template");
			this.Terminate = nt.Add("terminate");
			this.Test = nt.Add("test");
			this.Text = nt.Add("text");
			this.Transform = nt.Add("transform");
			this.UrnMsxsl = nt.Add("urn:schemas-microsoft-com:xslt");
			this.UriXml = nt.Add("http://www.w3.org/XML/1998/namespace");
			this.UriXsl = nt.Add("http://www.w3.org/1999/XSL/Transform");
			this.UriWdXsl = nt.Add("http://www.w3.org/TR/WD-xsl");
			this.Use = nt.Add("use");
			this.UseAttributeSets = nt.Add("use-attribute-sets");
			this.UseWhen = nt.Add("use-when");
			this.Using = nt.Add("using");
			this.Value = nt.Add("value");
			this.ValueOf = nt.Add("value-of");
			this.Variable = nt.Add("variable");
			this.Version = nt.Add("version");
			this.When = nt.Add("when");
			this.WithParam = nt.Add("with-param");
			this.Xml = nt.Add("xml");
			this.Xmlns = nt.Add("xmlns");
			this.XPathDefaultNamespace = nt.Add("xpath-default-namespace");
			this.ZeroDigit = nt.Add("zero-digit");
		}

		// Token: 0x040023BE RID: 9150
		public XmlNameTable NameTable;

		// Token: 0x040023BF RID: 9151
		public string AnalyzeString;

		// Token: 0x040023C0 RID: 9152
		public string ApplyImports;

		// Token: 0x040023C1 RID: 9153
		public string ApplyTemplates;

		// Token: 0x040023C2 RID: 9154
		public string Assembly;

		// Token: 0x040023C3 RID: 9155
		public string Attribute;

		// Token: 0x040023C4 RID: 9156
		public string AttributeSet;

		// Token: 0x040023C5 RID: 9157
		public string CallTemplate;

		// Token: 0x040023C6 RID: 9158
		public string CaseOrder;

		// Token: 0x040023C7 RID: 9159
		public string CDataSectionElements;

		// Token: 0x040023C8 RID: 9160
		public string Character;

		// Token: 0x040023C9 RID: 9161
		public string CharacterMap;

		// Token: 0x040023CA RID: 9162
		public string Choose;

		// Token: 0x040023CB RID: 9163
		public string Comment;

		// Token: 0x040023CC RID: 9164
		public string Copy;

		// Token: 0x040023CD RID: 9165
		public string CopyOf;

		// Token: 0x040023CE RID: 9166
		public string Count;

		// Token: 0x040023CF RID: 9167
		public string DataType;

		// Token: 0x040023D0 RID: 9168
		public string DecimalFormat;

		// Token: 0x040023D1 RID: 9169
		public string DecimalSeparator;

		// Token: 0x040023D2 RID: 9170
		public string DefaultCollation;

		// Token: 0x040023D3 RID: 9171
		public string DefaultValidation;

		// Token: 0x040023D4 RID: 9172
		public string Digit;

		// Token: 0x040023D5 RID: 9173
		public string DisableOutputEscaping;

		// Token: 0x040023D6 RID: 9174
		public string DocTypePublic;

		// Token: 0x040023D7 RID: 9175
		public string DocTypeSystem;

		// Token: 0x040023D8 RID: 9176
		public string Document;

		// Token: 0x040023D9 RID: 9177
		public string Element;

		// Token: 0x040023DA RID: 9178
		public string Elements;

		// Token: 0x040023DB RID: 9179
		public string Encoding;

		// Token: 0x040023DC RID: 9180
		public string ExcludeResultPrefixes;

		// Token: 0x040023DD RID: 9181
		public string ExtensionElementPrefixes;

		// Token: 0x040023DE RID: 9182
		public string Fallback;

		// Token: 0x040023DF RID: 9183
		public string ForEach;

		// Token: 0x040023E0 RID: 9184
		public string ForEachGroup;

		// Token: 0x040023E1 RID: 9185
		public string Format;

		// Token: 0x040023E2 RID: 9186
		public string From;

		// Token: 0x040023E3 RID: 9187
		public string Function;

		// Token: 0x040023E4 RID: 9188
		public string GroupingSeparator;

		// Token: 0x040023E5 RID: 9189
		public string GroupingSize;

		// Token: 0x040023E6 RID: 9190
		public string Href;

		// Token: 0x040023E7 RID: 9191
		public string Id;

		// Token: 0x040023E8 RID: 9192
		public string If;

		// Token: 0x040023E9 RID: 9193
		public string ImplementsPrefix;

		// Token: 0x040023EA RID: 9194
		public string Import;

		// Token: 0x040023EB RID: 9195
		public string ImportSchema;

		// Token: 0x040023EC RID: 9196
		public string Include;

		// Token: 0x040023ED RID: 9197
		public string Indent;

		// Token: 0x040023EE RID: 9198
		public string Infinity;

		// Token: 0x040023EF RID: 9199
		public string Key;

		// Token: 0x040023F0 RID: 9200
		public string Lang;

		// Token: 0x040023F1 RID: 9201
		public string Language;

		// Token: 0x040023F2 RID: 9202
		public string LetterValue;

		// Token: 0x040023F3 RID: 9203
		public string Level;

		// Token: 0x040023F4 RID: 9204
		public string Match;

		// Token: 0x040023F5 RID: 9205
		public string MatchingSubstring;

		// Token: 0x040023F6 RID: 9206
		public string MediaType;

		// Token: 0x040023F7 RID: 9207
		public string Message;

		// Token: 0x040023F8 RID: 9208
		public string Method;

		// Token: 0x040023F9 RID: 9209
		public string MinusSign;

		// Token: 0x040023FA RID: 9210
		public string Mode;

		// Token: 0x040023FB RID: 9211
		public string Name;

		// Token: 0x040023FC RID: 9212
		public string Namespace;

		// Token: 0x040023FD RID: 9213
		public string NamespaceAlias;

		// Token: 0x040023FE RID: 9214
		public string NaN;

		// Token: 0x040023FF RID: 9215
		public string NextMatch;

		// Token: 0x04002400 RID: 9216
		public string NonMatchingSubstring;

		// Token: 0x04002401 RID: 9217
		public string Number;

		// Token: 0x04002402 RID: 9218
		public string OmitXmlDeclaration;

		// Token: 0x04002403 RID: 9219
		public string Order;

		// Token: 0x04002404 RID: 9220
		public string Otherwise;

		// Token: 0x04002405 RID: 9221
		public string Output;

		// Token: 0x04002406 RID: 9222
		public string OutputCharacter;

		// Token: 0x04002407 RID: 9223
		public string OutputVersion;

		// Token: 0x04002408 RID: 9224
		public string Param;

		// Token: 0x04002409 RID: 9225
		public string PatternSeparator;

		// Token: 0x0400240A RID: 9226
		public string Percent;

		// Token: 0x0400240B RID: 9227
		public string PerformSort;

		// Token: 0x0400240C RID: 9228
		public string PerMille;

		// Token: 0x0400240D RID: 9229
		public string PreserveSpace;

		// Token: 0x0400240E RID: 9230
		public string Priority;

		// Token: 0x0400240F RID: 9231
		public string ProcessingInstruction;

		// Token: 0x04002410 RID: 9232
		public string Required;

		// Token: 0x04002411 RID: 9233
		public string ResultDocument;

		// Token: 0x04002412 RID: 9234
		public string ResultPrefix;

		// Token: 0x04002413 RID: 9235
		public string Script;

		// Token: 0x04002414 RID: 9236
		public string Select;

		// Token: 0x04002415 RID: 9237
		public string Separator;

		// Token: 0x04002416 RID: 9238
		public string Sequence;

		// Token: 0x04002417 RID: 9239
		public string Sort;

		// Token: 0x04002418 RID: 9240
		public string Space;

		// Token: 0x04002419 RID: 9241
		public string Standalone;

		// Token: 0x0400241A RID: 9242
		public string StripSpace;

		// Token: 0x0400241B RID: 9243
		public string Stylesheet;

		// Token: 0x0400241C RID: 9244
		public string StylesheetPrefix;

		// Token: 0x0400241D RID: 9245
		public string Template;

		// Token: 0x0400241E RID: 9246
		public string Terminate;

		// Token: 0x0400241F RID: 9247
		public string Test;

		// Token: 0x04002420 RID: 9248
		public string Text;

		// Token: 0x04002421 RID: 9249
		public string Transform;

		// Token: 0x04002422 RID: 9250
		public string UrnMsxsl;

		// Token: 0x04002423 RID: 9251
		public string UriXml;

		// Token: 0x04002424 RID: 9252
		public string UriXsl;

		// Token: 0x04002425 RID: 9253
		public string UriWdXsl;

		// Token: 0x04002426 RID: 9254
		public string Use;

		// Token: 0x04002427 RID: 9255
		public string UseAttributeSets;

		// Token: 0x04002428 RID: 9256
		public string UseWhen;

		// Token: 0x04002429 RID: 9257
		public string Using;

		// Token: 0x0400242A RID: 9258
		public string Value;

		// Token: 0x0400242B RID: 9259
		public string ValueOf;

		// Token: 0x0400242C RID: 9260
		public string Variable;

		// Token: 0x0400242D RID: 9261
		public string Version;

		// Token: 0x0400242E RID: 9262
		public string When;

		// Token: 0x0400242F RID: 9263
		public string WithParam;

		// Token: 0x04002430 RID: 9264
		public string Xml;

		// Token: 0x04002431 RID: 9265
		public string Xmlns;

		// Token: 0x04002432 RID: 9266
		public string XPathDefaultNamespace;

		// Token: 0x04002433 RID: 9267
		public string ZeroDigit;
	}
}
