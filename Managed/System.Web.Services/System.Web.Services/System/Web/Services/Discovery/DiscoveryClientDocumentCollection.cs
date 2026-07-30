using System;
using System.Collections;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a collection of documents discovered during XML Web services discovery that have been downloaded to the client. This class cannot be inherited.</summary>
	// Token: 0x0200009D RID: 157
	public sealed class DiscoveryClientDocumentCollection : DictionaryBase
	{
		/// <summary>Gets or sets a client discovery document object from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> with the specified URL.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the document discovered and downloaded to the client. The underlying type of the object can be a <see cref="T:System.Web.Services.Description.ServiceDescription" />, <see cref="T:System.Xml.Schema.XmlSchema" />, or <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />.</returns>
		/// <param name="url">The URL of the discovery document to get or set from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x17000116 RID: 278
		public object this[string url]
		{
			get
			{
				return base.Dictionary[url];
			}
			set
			{
				base.Dictionary[url] = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00012AC3 File Offset: 0x00010CC3
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00012AD0 File Offset: 0x00010CD0
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Adds an object with the specified URL to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
		/// <param name="url">The URL for the document to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
		/// <param name="value">A discovered document to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with a key of <paramref name="url" /> already exists in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </exception>
		// Token: 0x06000405 RID: 1029 RVA: 0x00012ADD File Offset: 0x00010CDD
		public void Add(string url, object value)
		{
			base.Dictionary.Add(url, value);
		}

		/// <summary>Determines if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> contains an object with the specified URL.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> contains an object with the specified URL; otherwise, false.</returns>
		/// <param name="url">The URL for the document to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x06000406 RID: 1030 RVA: 0x00012AEC File Offset: 0x00010CEC
		public bool Contains(string url)
		{
			return base.Dictionary.Contains(url);
		}

		/// <summary>Removes an object with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
		/// <param name="url">The URL for the discovered document to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x06000407 RID: 1031 RVA: 0x00012AFA File Offset: 0x00010CFA
		public void Remove(string url)
		{
			base.Dictionary.Remove(url);
		}
	}
}
