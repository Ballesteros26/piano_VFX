using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x02000652 RID: 1618
	internal sealed class Directive
	{
		// Token: 0x06004578 RID: 17784 RVA: 0x000BE368 File Offset: 0x000BC568
		static Directive()
		{
			Directive.InitHash();
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000BE678 File Offset: 0x000BC878
		private static void InitHash()
		{
			StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
			Directive.directivesHash = new Hashtable(ordinalIgnoreCase);
			Hashtable hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text in Directive.page_atts)
			{
				hashtable.Add(text, null);
			}
			Directive.directivesHash.Add("PAGE", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text2 in Directive.control_atts)
			{
				hashtable.Add(text2, null);
			}
			Directive.directivesHash.Add("CONTROL", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text3 in Directive.import_atts)
			{
				hashtable.Add(text3, null);
			}
			Directive.directivesHash.Add("IMPORT", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text4 in Directive.implements_atts)
			{
				hashtable.Add(text4, null);
			}
			Directive.directivesHash.Add("IMPLEMENTS", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text5 in Directive.register_atts)
			{
				hashtable.Add(text5, null);
			}
			Directive.directivesHash.Add("REGISTER", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text6 in Directive.assembly_atts)
			{
				hashtable.Add(text6, null);
			}
			Directive.directivesHash.Add("ASSEMBLY", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text7 in Directive.outputcache_atts)
			{
				hashtable.Add(text7, null);
			}
			Directive.directivesHash.Add("OUTPUTCACHE", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text8 in Directive.reference_atts)
			{
				hashtable.Add(text8, null);
			}
			Directive.directivesHash.Add("REFERENCE", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text9 in Directive.webservice_atts)
			{
				hashtable.Add(text9, null);
			}
			Directive.directivesHash.Add("WEBSERVICE", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text10 in Directive.webservice_atts)
			{
				hashtable.Add(text10, null);
			}
			Directive.directivesHash.Add("WEBHANDLER", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text11 in Directive.application_atts)
			{
				hashtable.Add(text11, null);
			}
			Directive.directivesHash.Add("APPLICATION", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text12 in Directive.mastertype_atts)
			{
				hashtable.Add(text12, null);
			}
			Directive.directivesHash.Add("MASTERTYPE", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text13 in Directive.control_atts)
			{
				hashtable.Add(text13, null);
			}
			Directive.directivesHash.Add("MASTER", hashtable);
			hashtable = new Hashtable(ordinalIgnoreCase);
			foreach (string text14 in Directive.previouspagetype_atts)
			{
				hashtable.Add(text14, null);
			}
			Directive.directivesHash.Add("PREVIOUSPAGETYPE", hashtable);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00002050 File Offset: 0x00000250
		private Directive()
		{
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x000BE9B4 File Offset: 0x000BCBB4
		public static bool IsDirective(string id)
		{
			return Directive.directivesHash.Contains(id);
		}

		// Token: 0x040024EB RID: 9451
		private static Hashtable directivesHash;

		// Token: 0x040024EC RID: 9452
		private static string[] page_atts = new string[]
		{
			"AspCompat", "AutoEventWireup", "Buffer", "ClassName", "ClientTarget", "CodePage", "CompilerOptions", "ContentType", "Culture", "Debug",
			"Description", "EnableEventValidation", "MaintainScrollPositionOnPostBack", "EnableSessionState", "EnableViewState", "EnableViewStateMac", "ErrorPage", "Explicit", "Inherits", "Language",
			"LCID", "ResponseEncoding", "Src", "SmartNavigation", "Strict", "Trace", "TraceMode", "Transaction", "UICulture", "WarningLevel",
			"CodeBehind", "ValidateRequest"
		};

		// Token: 0x040024ED RID: 9453
		private static string[] control_atts = new string[]
		{
			"AutoEventWireup", "ClassName", "CompilerOptions", "Debug", "Description", "EnableViewState", "Explicit", "Inherits", "Language", "Strict",
			"Src", "WarningLevel", "CodeBehind", "TargetSchema", "LinePragmas"
		};

		// Token: 0x040024EE RID: 9454
		private static string[] import_atts = new string[] { "namespace" };

		// Token: 0x040024EF RID: 9455
		private static string[] implements_atts = new string[] { "interface" };

		// Token: 0x040024F0 RID: 9456
		private static string[] assembly_atts = new string[] { "name", "src" };

		// Token: 0x040024F1 RID: 9457
		private static string[] register_atts = new string[] { "tagprefix", "tagname", "Namespace", "Src", "Assembly" };

		// Token: 0x040024F2 RID: 9458
		private static string[] outputcache_atts = new string[] { "Duration", "Location", "VaryByControl", "VaryByCustom", "VaryByHeader", "VaryByParam" };

		// Token: 0x040024F3 RID: 9459
		private static string[] reference_atts = new string[] { "page", "control" };

		// Token: 0x040024F4 RID: 9460
		private static string[] webservice_atts = new string[] { "class", "codebehind", "debug", "language" };

		// Token: 0x040024F5 RID: 9461
		private static string[] application_atts = new string[] { "description", "inherits", "codebehind" };

		// Token: 0x040024F6 RID: 9462
		private static string[] mastertype_atts = new string[] { "virtualpath", "typename" };

		// Token: 0x040024F7 RID: 9463
		private static string[] previouspagetype_atts = new string[] { "virtualpath", "typename" };
	}
}
