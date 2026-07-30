using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;
using System.Web.Hosting;

namespace System.Web.UI
{
	// Token: 0x020001EA RID: 490
	internal sealed class MasterPageParser : UserControlParser
	{
		// Token: 0x060013C0 RID: 5056 RVA: 0x00035888 File Offset: 0x00033A88
		internal MasterPageParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: base(virtualPath, inputFile, context, "System.Web.UI.MasterPage")
		{
			this.cacheEntryName = string.Concat(new object[] { "@@MasterPagePHIDS:", virtualPath, ":", inputFile });
			this.contentPlaceHolderIds = HttpRuntime.InternalCache.Get(this.cacheEntryName) as List<string>;
			this.LoadConfigDefaults();
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000358ED File Offset: 0x00033AED
		internal MasterPageParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000358FC File Offset: 0x00033AFC
		internal MasterPageParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
			: base(virtualPath, inputFile, reader, context)
		{
			this.cacheEntryName = string.Concat(new object[] { "@@MasterPagePHIDS:", virtualPath, ":", base.InputFile });
			this.contentPlaceHolderIds = HttpRuntime.InternalCache.Get(this.cacheEntryName) as List<string>;
			this.LoadConfigDefaults();
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00035963 File Offset: 0x00033B63
		public static MasterPage GetCompiledMasterInstance(string virtualPath, string inputFile, HttpContext context)
		{
			return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(MasterPage)) as MasterPage;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0003597A File Offset: 0x00033B7A
		public static Type GetCompiledMasterType(string virtualPath, string inputFile, HttpContext context)
		{
			return BuildManager.GetCompiledType(virtualPath);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00035982 File Offset: 0x00033B82
		internal override void HandleOptions(object obj)
		{
			base.HandleOptions(obj);
			((MasterPage)obj).MasterPageFile = base.MasterPageFile;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0003599C File Offset: 0x00033B9C
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("MasterType", directive, StringComparison.OrdinalIgnoreCase) == 0)
			{
				PageParserFilter pageParserFilter = base.PageParserFilter;
				if (pageParserFilter != null)
				{
					pageParserFilter.PreprocessDirective(directive.ToLowerInvariant(), atts);
				}
				string @string = BaseParser.GetString(atts, "TypeName", null);
				if (@string != null)
				{
					this.masterType = base.LoadType(@string);
					if (this.masterType == null)
					{
						base.ThrowParseException("Could not load type '" + @string + "'.", Array.Empty<object>());
					}
				}
				else
				{
					string text = BaseParser.GetString(atts, "VirtualPath", null);
					if (!string.IsNullOrEmpty(text))
					{
						VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
						if (!virtualPathProvider.FileExists(text))
						{
							base.ThrowParseFileNotFound(text, Array.Empty<object>());
						}
						text = virtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, VirtualPathUtility.ToAbsolute(text));
						this.masterTypeVirtualPath = text;
						this.AddDependency(text);
					}
					else
					{
						base.ThrowParseException("The MasterType directive must have either a TypeName or a VirtualPath attribute.", Array.Empty<object>());
					}
				}
				if (this.masterType != null)
				{
					this.AddAssembly(this.masterType.Assembly, true);
					return;
				}
			}
			else
			{
				base.AddDirective(directive, atts);
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00035AAB File Offset: 0x00033CAB
		internal void AddContentPlaceHolderId(string id)
		{
			if (this.contentPlaceHolderIds == null)
			{
				this.contentPlaceHolderIds = new List<string>(1);
				HttpRuntime.InternalCache.Insert(this.cacheEntryName, this.contentPlaceHolderIds);
			}
			this.contentPlaceHolderIds.Add(id);
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00035AE3 File Offset: 0x00033CE3
		internal Type MasterType
		{
			get
			{
				if (this.masterType == null && !string.IsNullOrEmpty(this.masterTypeVirtualPath))
				{
					this.masterType = BuildManager.GetCompiledType(this.masterTypeVirtualPath);
				}
				return this.masterType;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00035B17 File Offset: 0x00033D17
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.MasterPage";
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00035B1E File Offset: 0x00033D1E
		internal override string DefaultDirectiveName
		{
			get
			{
				return "master";
			}
		}

		// Token: 0x0400147E RID: 5246
		private Type masterType;

		// Token: 0x0400147F RID: 5247
		private string masterTypeVirtualPath;

		// Token: 0x04001480 RID: 5248
		private List<string> contentPlaceHolderIds;

		// Token: 0x04001481 RID: 5249
		private string cacheEntryName;
	}
}
