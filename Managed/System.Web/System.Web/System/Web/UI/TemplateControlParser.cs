using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Implements ASP.NET template parsing for template controls.</summary>
	// Token: 0x02000236 RID: 566
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class TemplateControlParser : BaseTemplateParser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateControlParser" /> class.</summary>
		// Token: 0x06001737 RID: 5943 RVA: 0x0003E489 File Offset: 0x0003C689
		protected TemplateControlParser()
		{
			this.LoadConfigDefaults();
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		internal override void LoadConfigDefaults()
		{
			base.LoadConfigDefaults();
			PagesSection pagesConfig = base.PagesConfig;
			this.autoEventWireup = pagesConfig.AutoEventWireup;
			this.enableViewState = pagesConfig.EnableViewState;
			this.compilationMode = pagesConfig.CompilationMode;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		internal override void ProcessMainAttributes(IDictionary atts)
		{
			this.autoEventWireup = base.GetBool(atts, "AutoEventWireup", this.autoEventWireup);
			this.enableViewState = base.GetBool(atts, "EnableViewState", this.enableViewState);
			string text = BaseParser.GetString(atts, "CompilationMode", this.compilationMode.ToString());
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.compilationMode = (CompilationMode)Enum.Parse(typeof(CompilationMode), text, true);
				}
				catch (Exception ex)
				{
					base.ThrowParseException("Invalid value of the CompilationMode attribute.", ex, Array.Empty<object>());
				}
			}
			atts.Remove("TargetSchema");
			text = BaseParser.GetString(atts, "ClientIDMode", null);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.clientIDMode = new ClientIDMode?((ClientIDMode)Enum.Parse(typeof(ClientIDMode), text, true));
				}
				catch (Exception ex2)
				{
					base.ThrowParseException("Invalid value of the ClientIDMode attribute.", ex2, Array.Empty<object>());
				}
			}
			base.ProcessMainAttributes(atts);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0003E5FC File Offset: 0x0003C7FC
		internal object GetCompiledInstance()
		{
			Type type = this.CompileIntoType();
			if (type == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type);
			if (obj == null)
			{
				return null;
			}
			this.HandleOptions(obj);
			return obj;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0003E630 File Offset: 0x0003C830
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("Register", directive, true, Helpers.InvariantCulture) == 0)
			{
				string @string = BaseParser.GetString(atts, "TagPrefix", null);
				if (@string == null || @string.Trim() == "")
				{
					base.ThrowParseException("No TagPrefix attribute found.", Array.Empty<object>());
				}
				string string2 = BaseParser.GetString(atts, "Namespace", null);
				string string3 = BaseParser.GetString(atts, "Assembly", null);
				if (string2 == null && string3 != null)
				{
					base.ThrowParseException("Need a Namespace attribute with Assembly.", Array.Empty<object>());
				}
				if (string2 != null)
				{
					if (atts.Count != 0)
					{
						base.ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts), Array.Empty<object>());
					}
					base.RegisterNamespace(@string, string2, string3);
					return;
				}
				string string4 = BaseParser.GetString(atts, "TagName", null);
				string string5 = BaseParser.GetString(atts, "Src", null);
				if (string4 == null && string5 != null)
				{
					base.ThrowParseException("Need a TagName attribute with Src.", Array.Empty<object>());
				}
				if (string4 != null && string5 == null)
				{
					base.ThrowParseException("Need a Src attribute with TagName.", Array.Empty<object>());
				}
				base.RegisterCustomControl(@string, string4, string5);
				return;
			}
			else
			{
				if (string.Compare("Reference", directive, true, Helpers.InvariantCulture) == 0)
				{
					string text = null;
					string string6 = BaseParser.GetString(atts, "Page", null);
					bool flag = string6 != null;
					if (flag)
					{
						text = string6;
					}
					bool flag2 = false;
					string string7 = BaseParser.GetString(atts, "Control", null);
					if (string7 != null)
					{
						if (flag)
						{
							flag2 = true;
						}
						else
						{
							text = string7;
						}
					}
					string string8 = BaseParser.GetString(atts, "VirtualPath", null);
					if (string8 != null)
					{
						if (text != null)
						{
							flag2 = true;
						}
						else
						{
							text = string8;
						}
					}
					if (text == null)
					{
						base.ThrowParseException("Must provide one of the 'page', 'control' or 'virtualPath' attributes", Array.Empty<object>());
					}
					if (flag2)
					{
						base.ThrowParseException("Only one attribute can be specified.", Array.Empty<object>());
					}
					text = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, text);
					this.AddDependency(text, false);
					Type compiledType = BuildManager.GetCompiledType(text);
					this.AddAssembly(compiledType.Assembly, true);
					if (atts.Count != 0)
					{
						base.ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts), Array.Empty<object>());
					}
					return;
				}
				base.AddDirective(directive, atts);
				return;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0003E83D File Offset: 0x0003CA3D
		internal override void HandleOptions(object obj)
		{
			base.HandleOptions(obj);
			Control control = obj as Control;
			control.AutoEventWireup = this.autoEventWireup;
			control.EnableViewState = this.enableViewState;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0003E863 File Offset: 0x0003CA63
		internal bool AutoEventWireup
		{
			get
			{
				return this.autoEventWireup;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0003E86B File Offset: 0x0003CA6B
		internal bool EnableViewState
		{
			get
			{
				return this.enableViewState;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0003E873 File Offset: 0x0003CA73
		internal CompilationMode CompilationMode
		{
			get
			{
				return this.compilationMode;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0003E87B File Offset: 0x0003CA7B
		internal ClientIDMode? ClientIDMode
		{
			get
			{
				return this.clientIDMode;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0003E883 File Offset: 0x0003CA83
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x0003E88B File Offset: 0x0003CA8B
		internal override TextReader Reader
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

		// Token: 0x040015A6 RID: 5542
		private bool autoEventWireup = true;

		// Token: 0x040015A7 RID: 5543
		private bool enableViewState = true;

		// Token: 0x040015A8 RID: 5544
		private CompilationMode compilationMode = CompilationMode.Always;

		// Token: 0x040015A9 RID: 5545
		private ClientIDMode? clientIDMode;

		// Token: 0x040015AA RID: 5546
		private TextReader reader;
	}
}
