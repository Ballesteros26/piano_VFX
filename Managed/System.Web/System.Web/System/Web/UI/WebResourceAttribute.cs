using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that enables an embedded resource in an assembly. This class cannot be inherited. </summary>
	// Token: 0x0200024F RID: 591
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class WebResourceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebResourceAttribute" /> class with the specified Web resource and resource content type.</summary>
		/// <param name="webResource">The name of the of Web resource.</param>
		/// <param name="contentType">The type of resource, such as "image/gif" or "text/javascript".</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webResource" /> is null or an empty string ("").- or -<paramref name="contentType" /> is null or an empty string ("").</exception>
		// Token: 0x0600181B RID: 6171 RVA: 0x00040EA1 File Offset: 0x0003F0A1
		public WebResourceAttribute(string webResource, string contentType)
		{
			this.webResource = webResource;
			this.contentType = contentType;
		}

		/// <summary>Gets a string containing the MIME type of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
		/// <returns>The content type of the resource.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x00040EB7 File Offset: 0x0003F0B7
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether, during processing of the embedded resource referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class, other Web resource URLs are parsed and replaced with the full path to the resource.</summary>
		/// <returns>true if embedded resources are resolved during processing of the resource; otherwise, false. The default is false.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00040EBF File Offset: 0x0003F0BF
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00040EC7 File Offset: 0x0003F0C7
		public bool PerformSubstitution
		{
			get
			{
				return this.performSubstitution;
			}
			set
			{
				this.performSubstitution = value;
			}
		}

		/// <summary>Gets a string containing the name of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
		/// <returns>The name of the resource.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00040ED0 File Offset: 0x0003F0D0
		public string WebResource
		{
			get
			{
				return this.webResource;
			}
		}

		/// <summary>Gets or set the path of a Content Delivery Network (CDN) that contains Web resources.</summary>
		/// <returns>The path of a Content Delivery Network (CDN).</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string CdnPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set a value that indicates to the <see cref="T:System.Web.UI.ScriptManager" /> whether a script resource should be accessed using a secure connection to the content delivery network (CDN) path when the page is accessed using HTTPS.</summary>
		/// <returns>true if the CDN should be accessed using HTTPS; otherwise, false.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool CdnSupportsSecureConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an expression that is used when a Web resource has successfully loaded.</summary>
		/// <returns>An expression that is used when a Web resource has successfully loaded.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string LoadSuccessExpression
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001613 RID: 5651
		private bool performSubstitution;

		// Token: 0x04001614 RID: 5652
		private string webResource;

		// Token: 0x04001615 RID: 5653
		private string contentType;
	}
}
