using System;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Manages the path context for the current Web application. This class cannot be inherited.</summary>
	// Token: 0x020005F3 RID: 1523
	public sealed class WebContext
	{
		// Token: 0x06004230 RID: 16944 RVA: 0x000AD2D1 File Offset: 0x000AB4D1
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath)
		{
			this.pathLevel = pathLevel;
			this.site = site;
			this.applicationPath = applicationPath;
			this.path = path;
			this.locationSubPath = locationSubPath;
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object that represents the path level of the current Web application.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object.</returns>
		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x000AD2FE File Offset: 0x000AB4FE
		public WebApplicationLevel ApplicationLevel
		{
			get
			{
				return this.pathLevel;
			}
		}

		/// <summary>Gets the application path of the current Web application.</summary>
		/// <returns>The application path of the current Web application.</returns>
		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000AD306 File Offset: 0x000AB506
		public string ApplicationPath
		{
			get
			{
				return this.applicationPath;
			}
		}

		/// <summary>Gets the location subpath of the Web application.</summary>
		/// <returns>The location subpath of the current Web application.</returns>
		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x000AD30E File Offset: 0x000AB50E
		public string LocationSubPath
		{
			get
			{
				return this.locationSubPath;
			}
		}

		/// <summary>Gets the current virtual path of the Web application.</summary>
		/// <returns>The current virtual path of the Web application.</returns>
		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000AD316 File Offset: 0x000AB516
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the name of the current Web application.</summary>
		/// <returns>The name of the current Web application.</returns>
		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000AD31E File Offset: 0x000AB51E
		public string Site
		{
			get
			{
				return this.site;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebContext" /> class.</summary>
		/// <param name="pathLevel">A <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object.</param>
		/// <param name="site">The name of the Web site.</param>
		/// <param name="applicationPath">The virtual path of the root level of the current Web application.</param>
		/// <param name="path">The virtual path of the Web.config file that the current configuration object represents.</param>
		/// <param name="locationSubPath">The path value of the location element that is currently being edited.</param>
		/// <param name="appConfigPath">The current Web application's configuration path.</param>
		// Token: 0x06004236 RID: 16950 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath, string appConfigPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002368 RID: 9064
		private WebApplicationLevel pathLevel;

		// Token: 0x04002369 RID: 9065
		private string site;

		// Token: 0x0400236A RID: 9066
		private string applicationPath;

		// Token: 0x0400236B RID: 9067
		private string path;

		// Token: 0x0400236C RID: 9068
		private string locationSubPath;
	}
}
