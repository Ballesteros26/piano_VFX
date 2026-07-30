using System;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents WsdlHelpGenerator element in the configuration file that specifies the XML Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX XML Web services page.</summary>
	// Token: 0x0200014B RID: 331
	public sealed class WsdlHelpGeneratorElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.WsdlHelpGeneratorElement" /> class.</summary>
		// Token: 0x06000A3A RID: 2618 RVA: 0x000450AD File Offset: 0x000432AD
		public WsdlHelpGeneratorElement()
		{
			this.properties.Add(this.href);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000450ED File Offset: 0x000432ED
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private string GetConfigurationDirectory()
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			return HttpRuntime.MachineConfigurationDirectory;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000450F9 File Offset: 0x000432F9
		internal string HelpGeneratorVirtualPath
		{
			get
			{
				return this.virtualPath + this.Href;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0004510C File Offset: 0x0004330C
		internal string HelpGeneratorPath
		{
			get
			{
				return Path.Combine(this.actualPath, this.Href);
			}
		}

		/// <summary>Gets or sets the file path to the Help page.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the file path to the Help page.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0004511F File Offset: 0x0004331F
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00045132 File Offset: 0x00043332
		[ConfigurationProperty("href", IsRequired = true)]
		public string Href
		{
			get
			{
				return (string)base[this.href];
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.needToValidateHref && value.Length > 0)
				{
					WsdlHelpGeneratorElement.CheckIOReadPermission(this.actualPath, value);
				}
				base[this.href] = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00045168 File Offset: 0x00043368
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00045170 File Offset: 0x00043370
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			base.DeserializeElement(reader, serializeCollectionKey);
			try
			{
				ContextInformation evaluationContext = base.EvaluationContext;
			}
			catch (ConfigurationErrorsException)
			{
				this.actualPath = this.GetConfigurationDirectory();
				return;
			}
			WebContext webContext = base.EvaluationContext.HostingContext as WebContext;
			if (webContext == null)
			{
				return;
			}
			if (this.Href.Length == 0)
			{
				return;
			}
			string text = webContext.Path;
			string text2;
			if (text == null)
			{
				text = HostingEnvironment.ApplicationVirtualPath;
				if (text == null)
				{
					text = "";
				}
				text2 = this.GetConfigurationDirectory();
			}
			else
			{
				text2 = HostingEnvironment.MapPath(text);
			}
			if (!text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/";
			}
			WsdlHelpGeneratorElement.CheckIOReadPermission(text2, this.Href);
			this.actualPath = text2;
			this.virtualPath = text;
			this.needToValidateHref = true;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00045240 File Offset: 0x00043440
		protected override void Reset(ConfigurationElement parentElement)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			WsdlHelpGeneratorElement wsdlHelpGeneratorElement = (WsdlHelpGeneratorElement)parentElement;
			try
			{
				ContextInformation evaluationContext = base.EvaluationContext;
			}
			catch (ConfigurationErrorsException)
			{
				base.Reset(parentElement);
				this.actualPath = this.GetConfigurationDirectory();
				return;
			}
			WebContext webContext = base.EvaluationContext.HostingContext as WebContext;
			if (webContext != null)
			{
				string text = webContext.Path;
				bool flag = text == null;
				this.actualPath = wsdlHelpGeneratorElement.actualPath;
				if (flag)
				{
					text = HostingEnvironment.ApplicationVirtualPath;
				}
				if (text != null && !text.EndsWith("/", StringComparison.Ordinal))
				{
					text += "/";
				}
				if (text == null && parentElement != null)
				{
					this.virtualPath = wsdlHelpGeneratorElement.virtualPath;
				}
				else if (text != null)
				{
					this.virtualPath = text;
				}
			}
			base.Reset(parentElement);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00045300 File Offset: 0x00043500
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void SetDefaults()
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			if (HttpContext.Current != null)
			{
				this.virtualPath = HostingEnvironment.ApplicationVirtualPath;
			}
			this.actualPath = this.GetConfigurationDirectory();
			if (this.virtualPath != null && !this.virtualPath.EndsWith("/", StringComparison.Ordinal))
			{
				this.virtualPath += "/";
			}
			if (this.actualPath != null && !this.actualPath.EndsWith("\\", StringComparison.Ordinal))
			{
				this.actualPath += "\\";
			}
			this.Href = "DefaultWsdlHelpGenerator.aspx";
			WsdlHelpGeneratorElement.CheckIOReadPermission(this.actualPath, this.Href);
			this.needToValidateHref = true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000453B8 File Offset: 0x000435B8
		private static void CheckIOReadPermission(string path, string file)
		{
			if (path == null)
			{
				return;
			}
			string fullPath = Path.GetFullPath(Path.Combine(path, file));
			new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
		}

		// Token: 0x040005CD RID: 1485
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005CE RID: 1486
		private readonly ConfigurationProperty href = new ConfigurationProperty("href", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040005CF RID: 1487
		private string virtualPath;

		// Token: 0x040005D0 RID: 1488
		private string actualPath;

		// Token: 0x040005D1 RID: 1489
		private bool needToValidateHref;
	}
}
