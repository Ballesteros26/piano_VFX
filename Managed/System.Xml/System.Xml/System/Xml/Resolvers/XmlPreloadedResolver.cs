using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml.Resolvers
{
	/// <summary>Represents a class that is used to prepopulate the cache with DTDs or XML streams.</summary>
	// Token: 0x020004B2 RID: 1202
	public class XmlPreloadedResolver : XmlResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class.</summary>
		// Token: 0x060030E3 RID: 12515 RVA: 0x0011BCC1 File Offset: 0x00119EC1
		public XmlPreloadedResolver()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified preloaded well-known DTDs.</summary>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into the cache.</param>
		// Token: 0x060030E4 RID: 12516 RVA: 0x0011BCCA File Offset: 0x00119ECA
		public XmlPreloadedResolver(XmlKnownDtds preloadedDtds)
			: this(null, preloadedDtds, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver.</summary>
		/// <param name="fallbackResolver">The XmlResolver, XmlXapResolver, or your own resolver.</param>
		// Token: 0x060030E5 RID: 12517 RVA: 0x0011BCD5 File Offset: 0x00119ED5
		public XmlPreloadedResolver(XmlResolver fallbackResolver)
			: this(fallbackResolver, XmlKnownDtds.All, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver and preloaded well-known DTDs.</summary>
		/// <param name="fallbackResolver">The XmlResolver, XmlXapResolver, or your own resolver.</param>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into the cache.</param>
		// Token: 0x060030E6 RID: 12518 RVA: 0x0011BCE4 File Offset: 0x00119EE4
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds)
			: this(fallbackResolver, preloadedDtds, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver, preloaded well-known DTDs, and URI equality comparer.</summary>
		/// <param name="fallbackResolver">The XmlResolver, XmlXapResolver, or your own resolver.</param>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into cache.</param>
		/// <param name="uriComparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when you compare URIs.</param>
		// Token: 0x060030E7 RID: 12519 RVA: 0x0011BCF0 File Offset: 0x00119EF0
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds, IEqualityComparer<Uri> uriComparer)
		{
			this.fallbackResolver = fallbackResolver;
			this.mappings = new Dictionary<Uri, XmlPreloadedResolver.PreloadedData>(16, uriComparer);
			this.preloadedDtds = preloadedDtds;
			if (preloadedDtds != XmlKnownDtds.None)
			{
				if ((preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.Xhtml10_Dtd);
				}
				if ((preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.Rss091_Dtd);
				}
			}
		}

		/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
		/// <returns>The <see cref="T:System.Uri" /> representing the absolute URI or null if the relative URI cannot be resolved.</returns>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null.</exception>
		// Token: 0x060030E8 RID: 12520 RVA: 0x0011BD44 File Offset: 0x00119F44
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			if (relativeUri != null && relativeUri.StartsWith("-//", StringComparison.CurrentCulture))
			{
				if ((this.preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None && relativeUri.StartsWith("-//W3C//", StringComparison.CurrentCulture))
				{
					for (int i = 0; i < XmlPreloadedResolver.Xhtml10_Dtd.Length; i++)
					{
						if (relativeUri == XmlPreloadedResolver.Xhtml10_Dtd[i].publicId)
						{
							return new Uri(relativeUri, UriKind.Relative);
						}
					}
				}
				if ((this.preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None && relativeUri == XmlPreloadedResolver.Rss091_Dtd[0].publicId)
				{
					return new Uri(relativeUri, UriKind.Relative);
				}
			}
			return base.ResolveUri(baseUri, relativeUri);
		}

		/// <summary>Maps a URI to an object that contains the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> or <see cref="T:System.IO.TextReader" /> object that corresponds to the actual source.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current version of the .NET Framework for Silverlight does not use this parameter when resolving URIs. This parameter is provided for future extensibility purposes. For example, this parameter can be mapped to the xlink:role and used as an implementation-specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> supports <see cref="T:System.IO.Stream" /> objects and <see cref="T:System.IO.TextReader" /> objects for URIs that were added as String. If the requested type is not supported by the resolver, an exception will be thrown. Use the <see cref="M:System.Xml.Resolvers.XmlPreloadedResolver.SupportsType(System.Uri,System.Type)" /> method to determine whether a certain Type is supported by this resolver.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="absoluteUri" /> is null.</exception>
		/// <exception cref="T:System.Xml.XmlException">Cannot resolve URI passed in <paramref name="absoluteUri" />.-or-<paramref name="ofObjectToReturn" /> is not of a supported type.</exception>
		// Token: 0x060030E9 RID: 12521 RVA: 0x0011BDD8 File Offset: 0x00119FD8
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this.fallbackResolver != null)
				{
					return this.fallbackResolver.GetEntity(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(Res.GetString("Cannot resolve '{0}'.", new object[] { absoluteUri.ToString() }));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return preloadedData.AsStream();
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return preloadedData.AsTextReader();
				}
				throw new XmlException(Res.GetString("Object type is not supported."));
			}
		}

		/// <summary>Sets the credentials that are used to authenticate the underlying <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>The credentials that are used to authenticate the underlying web request.</returns>
		// Token: 0x17000A46 RID: 2630
		// (set) Token: 0x060030EA RID: 12522 RVA: 0x0011BE9B File Offset: 0x0011A09B
		public override ICredentials Credentials
		{
			set
			{
				if (this.fallbackResolver != null)
				{
					this.fallbackResolver.Credentials = value;
				}
			}
		}

		/// <summary>Determines whether the resolver supports other <see cref="T:System.Type" />s than just <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is supported; otherwise, false.</returns>
		/// <param name="absoluteUri">The absolute URI to check.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to return.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null.</exception>
		// Token: 0x060030EB RID: 12523 RVA: 0x0011BEB4 File Offset: 0x0011A0B4
		public override bool SupportsType(Uri absoluteUri, Type type)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				return preloadedData.SupportsType(type);
			}
			if (this.fallbackResolver != null)
			{
				return this.fallbackResolver.SupportsType(absoluteUri, type);
			}
			return base.SupportsType(absoluteUri, type);
		}

		/// <summary>Adds a byte array to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A byte array with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060030EC RID: 12524 RVA: 0x0011BF0B File Offset: 0x0011A10B
		public void Add(Uri uri, byte[] value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, 0, value.Length));
		}

		/// <summary>Adds a byte array to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A byte array with the data that corresponds to the provided URI.</param>
		/// <param name="offset">The offset in the provided byte array where the data starts.</param>
		/// <param name="count">The number of bytes to read from the byte array, starting at the provided offset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than 0.-or-The length of the <paramref name="value" /> minus <paramref name="offset" /> is less than <paramref name="count." /></exception>
		// Token: 0x060030ED RID: 12525 RVA: 0x0011BF40 File Offset: 0x0011A140
		public void Add(Uri uri, byte[] value, int offset, int count)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (value.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, offset, count));
		}

		/// <summary>Adds a <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A <see cref="T:System.IO.Stream" /> with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060030EE RID: 12526 RVA: 0x0011BFB4 File Offset: 0x0011A1B4
		public void Add(Uri uri, Stream value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			checked
			{
				if (value.CanSeek)
				{
					int num = (int)value.Length;
					byte[] array = new byte[num];
					value.Read(array, 0, num);
					this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array));
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				byte[] array2 = new byte[4096];
				int num2;
				while ((num2 = value.Read(array2, 0, array2.Length)) > 0)
				{
					memoryStream.Write(array2, 0, num2);
				}
				int num3 = (int)memoryStream.Position;
				byte[] array3 = new byte[num3];
				Array.Copy(memoryStream.GetBuffer(), array3, num3);
				this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array3));
			}
		}

		/// <summary>Adds a string with preloaded data to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A String with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060030EF RID: 12527 RVA: 0x0011C06F File Offset: 0x0011A26F
		public void Add(Uri uri, string value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.StringData(value));
		}

		/// <summary>Gets a collection of preloaded URIs.</summary>
		/// <returns>The collection of preloaded URIs.</returns>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x0011C0A0 File Offset: 0x0011A2A0
		public IEnumerable<Uri> PreloadedUris
		{
			get
			{
				return this.mappings.Keys;
			}
		}

		/// <summary>Removes the data that corresponds to the URI from the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" />.</summary>
		/// <param name="uri">The URI of the data that should be removed from the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null.</exception>
		// Token: 0x060030F1 RID: 12529 RVA: 0x0011C0AD File Offset: 0x0011A2AD
		public void Remove(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.mappings.Remove(uri);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0011C0D0 File Offset: 0x0011A2D0
		private void Add(Uri uri, XmlPreloadedResolver.PreloadedData data)
		{
			if (this.mappings.ContainsKey(uri))
			{
				this.mappings[uri] = data;
				return;
			}
			this.mappings.Add(uri, data);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0011C0FC File Offset: 0x0011A2FC
		private void AddKnownDtd(XmlPreloadedResolver.XmlKnownDtdData[] dtdSet)
		{
			foreach (XmlPreloadedResolver.XmlKnownDtdData xmlKnownDtdData in dtdSet)
			{
				this.mappings.Add(new Uri(xmlKnownDtdData.publicId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
				this.mappings.Add(new Uri(xmlKnownDtdData.systemId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
			}
		}

		/// <summary>Asynchronously maps a URI to an object that contains the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> or <see cref="T:System.IO.TextReader" /> object that corresponds to the actual source.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current version of the .NET Framework for Silverlight does not use this parameter when resolving URIs. This parameter is provided for future extensibility purposes. For example, this parameter can be mapped to the xlink:role and used as an implementation-specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> supports <see cref="T:System.IO.Stream" /> objects and <see cref="T:System.IO.TextReader" /> objects for URIs that were added as String. If the requested type is not supported by the resolver, an exception will be thrown. Use the <see cref="M:System.Xml.Resolvers.XmlPreloadedResolver.SupportsType(System.Uri,System.Type)" /> method to determine whether a certain Type is supported by this resolver.</param>
		// Token: 0x060030F4 RID: 12532 RVA: 0x0011C14C File Offset: 0x0011A34C
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this.mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this.fallbackResolver != null)
				{
					return this.fallbackResolver.GetEntityAsync(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(Res.GetString("Cannot resolve '{0}'.", new object[] { absoluteUri.ToString() }));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return Task.FromResult<object>(preloadedData.AsStream());
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return Task.FromResult<object>(preloadedData.AsTextReader());
				}
				throw new XmlException(Res.GetString("Object type is not supported."));
			}
		}

		// Token: 0x04002018 RID: 8216
		private XmlResolver fallbackResolver;

		// Token: 0x04002019 RID: 8217
		private Dictionary<Uri, XmlPreloadedResolver.PreloadedData> mappings;

		// Token: 0x0400201A RID: 8218
		private XmlKnownDtds preloadedDtds;

		// Token: 0x0400201B RID: 8219
		private static XmlPreloadedResolver.XmlKnownDtdData[] Xhtml10_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", "xhtml1-strict.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", "xhtml1-transitional.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Frameset//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd", "xhtml1-frameset.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Latin 1 for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-lat1.ent", "xhtml-lat1.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Symbols for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-symbol.ent", "xhtml-symbol.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Special for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-special.ent", "xhtml-special.ent")
		};

		// Token: 0x0400201C RID: 8220
		private static XmlPreloadedResolver.XmlKnownDtdData[] Rss091_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//Netscape Communications//DTD RSS 0.91//EN", "http://my.netscape.com/publish/formats/rss-0.91.dtd", "rss-0.91.dtd")
		};

		// Token: 0x020004B3 RID: 1203
		private abstract class PreloadedData
		{
			// Token: 0x060030F6 RID: 12534
			internal abstract Stream AsStream();

			// Token: 0x060030F7 RID: 12535 RVA: 0x0011C2E0 File Offset: 0x0011A4E0
			internal virtual TextReader AsTextReader()
			{
				throw new XmlException(Res.GetString("Object type is not supported."));
			}

			// Token: 0x060030F8 RID: 12536 RVA: 0x0011C2F1 File Offset: 0x0011A4F1
			internal virtual bool SupportsType(Type type)
			{
				return type == null || type == typeof(Stream);
			}
		}

		// Token: 0x020004B4 RID: 1204
		private class XmlKnownDtdData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060030FA RID: 12538 RVA: 0x0011C311 File Offset: 0x0011A511
			internal XmlKnownDtdData(string publicId, string systemId, string resourceName)
			{
				this.publicId = publicId;
				this.systemId = systemId;
				this.resourceName = resourceName;
			}

			// Token: 0x060030FB RID: 12539 RVA: 0x0011C32E File Offset: 0x0011A52E
			internal override Stream AsStream()
			{
				return Assembly.GetExecutingAssembly().GetManifestResourceStream(this.resourceName);
			}

			// Token: 0x0400201D RID: 8221
			internal string publicId;

			// Token: 0x0400201E RID: 8222
			internal string systemId;

			// Token: 0x0400201F RID: 8223
			private string resourceName;
		}

		// Token: 0x020004B5 RID: 1205
		private class ByteArrayChunk : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060030FC RID: 12540 RVA: 0x0011C340 File Offset: 0x0011A540
			internal ByteArrayChunk(byte[] array)
				: this(array, 0, array.Length)
			{
			}

			// Token: 0x060030FD RID: 12541 RVA: 0x0011C34D File Offset: 0x0011A54D
			internal ByteArrayChunk(byte[] array, int offset, int length)
			{
				this.array = array;
				this.offset = offset;
				this.length = length;
			}

			// Token: 0x060030FE RID: 12542 RVA: 0x0011C36A File Offset: 0x0011A56A
			internal override Stream AsStream()
			{
				return new MemoryStream(this.array, this.offset, this.length);
			}

			// Token: 0x04002020 RID: 8224
			private byte[] array;

			// Token: 0x04002021 RID: 8225
			private int offset;

			// Token: 0x04002022 RID: 8226
			private int length;
		}

		// Token: 0x020004B6 RID: 1206
		private class StringData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x060030FF RID: 12543 RVA: 0x0011C383 File Offset: 0x0011A583
			internal StringData(string str)
			{
				this.str = str;
			}

			// Token: 0x06003100 RID: 12544 RVA: 0x0011C392 File Offset: 0x0011A592
			internal override Stream AsStream()
			{
				return new MemoryStream(Encoding.Unicode.GetBytes(this.str));
			}

			// Token: 0x06003101 RID: 12545 RVA: 0x0011C3A9 File Offset: 0x0011A5A9
			internal override TextReader AsTextReader()
			{
				return new StringReader(this.str);
			}

			// Token: 0x06003102 RID: 12546 RVA: 0x0011C3B6 File Offset: 0x0011A5B6
			internal override bool SupportsType(Type type)
			{
				return type == typeof(TextReader) || base.SupportsType(type);
			}

			// Token: 0x04002023 RID: 8227
			private string str;
		}
	}
}
