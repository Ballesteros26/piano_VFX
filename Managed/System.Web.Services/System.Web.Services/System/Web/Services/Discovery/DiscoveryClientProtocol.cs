using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Provides support for programmatically invoking XML Web services discovery.</summary>
	// Token: 0x0200009E RID: 158
	public class DiscoveryClientProtocol : HttpWebClientProtocol
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> class.</summary>
		// Token: 0x06000409 RID: 1033 RVA: 0x00012B10 File Offset: 0x00010D10
		public DiscoveryClientProtocol()
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012B4F File Offset: 0x00010D4F
		internal DiscoveryClientProtocol(HttpWebClientProtocol protocol)
			: base(protocol)
		{
		}

		/// <summary>Gets information in addition to references found in the discovery document.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> containing additional information found in the discovery document.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00012B8F File Offset: 0x00010D8F
		public IList AdditionalInformation
		{
			get
			{
				return this.additionalInformation;
			}
		}

		/// <summary>Gets a collection of discovery documents.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> representing the collection of discovery documents found.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00012B97 File Offset: 0x00010D97
		public DiscoveryClientDocumentCollection Documents
		{
			get
			{
				return this.documents;
			}
		}

		/// <summary>Gets a collection of exceptions that occurred during invocation of method from this class.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> of exceptions.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00012B9F File Offset: 0x00010D9F
		public DiscoveryExceptionDictionary Errors
		{
			get
			{
				return this.errors;
			}
		}

		/// <summary>A collection of references founds in resolved discovery documents.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> of references discovered.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00012BA7 File Offset: 0x00010DA7
		public DiscoveryClientReferenceCollection References
		{
			get
			{
				return this.references;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00012BAF File Offset: 0x00010DAF
		internal Hashtable InlinedSchemas
		{
			get
			{
				return this.inlinedSchemas;
			}
		}

		/// <summary>Discovers the supplied URL to determine if it is a discovery document.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the results of XML Web services discovery at the supplied URL.</returns>
		/// <param name="url">The URL where XML Web services discovery begins. </param>
		/// <exception cref="T:System.Net.WebException">Accessing the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="url" /> parameteris a valid URL, but does not point to a valid discovery document. </exception>
		// Token: 0x06000410 RID: 1040 RVA: 0x00012BB8 File Offset: 0x00010DB8
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public DiscoveryDocument Discover(string url)
		{
			DiscoveryDocument discoveryDocument = this.Documents[url] as DiscoveryDocument;
			if (discoveryDocument != null)
			{
				return discoveryDocument;
			}
			DiscoveryDocumentReference discoveryDocumentReference = new DiscoveryDocumentReference(url);
			discoveryDocumentReference.ClientProtocol = this;
			this.References[url] = discoveryDocumentReference;
			this.Errors.Clear();
			return discoveryDocumentReference.Document;
		}

		/// <summary>Discovers the supplied URL to determine if it is a discovery document, service description or an XML Schema Definition (XSD) schema.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the results of XML Web services discovery at the supplied URL. If the <paramref name="url" /> parameter refers to a service description or an XSD Schema, a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is created in memory for it.</returns>
		/// <param name="url">The URL where XML Web services discovery begins. </param>
		/// <exception cref="T:System.Net.WebException">Accessing the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="url" /> parameteris a valid URL, but does not point to a valid discovery document, service description, or XSD schema. </exception>
		// Token: 0x06000411 RID: 1041 RVA: 0x00012C08 File Offset: 0x00010E08
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public DiscoveryDocument DiscoverAny(string url)
		{
			Type[] discoveryReferenceTypes = WebServicesSection.Current.DiscoveryReferenceTypes;
			DiscoveryReference discoveryReference = null;
			string text = null;
			Stream stream = this.Download(ref url, ref text);
			this.Errors.Clear();
			bool flag = true;
			Exception ex = null;
			ArrayList arrayList = new ArrayList();
			foreach (Type type in discoveryReferenceTypes)
			{
				if (typeof(DiscoveryReference).IsAssignableFrom(type))
				{
					discoveryReference = (DiscoveryReference)Activator.CreateInstance(type);
					discoveryReference.Url = url;
					discoveryReference.ClientProtocol = this;
					stream.Position = 0L;
					Exception ex2 = discoveryReference.AttemptResolve(text, stream);
					if (ex2 == null)
					{
						break;
					}
					this.Errors[type.FullName] = ex2;
					discoveryReference = null;
					InvalidContentTypeException ex3 = ex2 as InvalidContentTypeException;
					if (ex3 == null || !ContentType.MatchesBase(ex3.ContentType, "text/html"))
					{
						flag = false;
					}
					if (ex2 is InvalidDocumentContentsException)
					{
						ex = ex2;
						break;
					}
					if (ex2.InnerException != null && ex2.InnerException.InnerException == null)
					{
						arrayList.Add(ex2.InnerException.Message);
					}
				}
			}
			if (discoveryReference == null)
			{
				if (ex != null)
				{
					StringBuilder stringBuilder = new StringBuilder(Res.GetString("TheDocumentWasUnderstoodButContainsErrors"));
					while (ex != null)
					{
						stringBuilder.Append("\n  - ").Append(ex.Message);
						ex = ex.InnerException;
					}
					throw new InvalidOperationException(stringBuilder.ToString());
				}
				if (flag)
				{
					throw new InvalidOperationException(Res.GetString("TheHTMLDocumentDoesNotContainDiscoveryInformation"));
				}
				bool flag2 = arrayList.Count == this.Errors.Count && this.Errors.Count > 0;
				int num = 1;
				while (flag2 && num < arrayList.Count)
				{
					if ((string)arrayList[num - 1] != (string)arrayList[num])
					{
						flag2 = false;
					}
					num++;
				}
				if (flag2)
				{
					throw new InvalidOperationException(Res.GetString("TheDocumentWasNotRecognizedAsAKnownDocumentType", new object[] { arrayList[0] }));
				}
				StringBuilder stringBuilder2 = new StringBuilder(Res.GetString("WebMissingResource", new object[] { url }));
				foreach (object obj in this.Errors)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Exception ex4 = (Exception)dictionaryEntry.Value;
					string text2 = (string)dictionaryEntry.Key;
					if (string.Compare(text2, typeof(ContractReference).FullName, StringComparison.Ordinal) == 0)
					{
						text2 = Res.GetString("WebContractReferenceName");
					}
					else if (string.Compare(text2, typeof(SchemaReference).FullName, StringComparison.Ordinal) == 0)
					{
						text2 = Res.GetString("WebShemaReferenceName");
					}
					else if (string.Compare(text2, typeof(DiscoveryDocumentReference).FullName, StringComparison.Ordinal) == 0)
					{
						text2 = Res.GetString("WebDiscoveryDocumentReferenceName");
					}
					stringBuilder2.Append("\n- ").Append(Res.GetString("WebDiscoRefReport", new object[] { text2, ex4.Message }));
					while (ex4.InnerException != null)
					{
						stringBuilder2.Append("\n  - ").Append(ex4.InnerException.Message);
						ex4 = ex4.InnerException;
					}
				}
				throw new InvalidOperationException(stringBuilder2.ToString());
			}
			else
			{
				if (discoveryReference is DiscoveryDocumentReference)
				{
					return ((DiscoveryDocumentReference)discoveryReference).Document;
				}
				this.References[discoveryReference.Url] = discoveryReference;
				return new DiscoveryDocument
				{
					References = { discoveryReference }
				};
			}
		}

		/// <summary>Downloads the discovery document at the supplied URL into a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the document at the supplied URL.</returns>
		/// <param name="url">The URL of the discovery document to download. </param>
		/// <exception cref="T:System.Net.WebException">The download from the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
		// Token: 0x06000412 RID: 1042 RVA: 0x00012FC8 File Offset: 0x000111C8
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public Stream Download(ref string url)
		{
			string text = null;
			return this.Download(ref url, ref text);
		}

		/// <summary>Downloads the discovery document at the supplied URL into a <see cref="T:System.IO.Stream" /> object, setting the <paramref name="contentType" /> parameter to the MIME encoding of the discovery document.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the document at the supplied URL.</returns>
		/// <param name="url">The URL of the discovery document to download. </param>
		/// <param name="contentType">The MIME encoding of the downloaded discovery document. </param>
		/// <exception cref="T:System.Net.WebException">The download from the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
		// Token: 0x06000413 RID: 1043 RVA: 0x00012FE0 File Offset: 0x000111E0
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public Stream Download(ref string url, ref string contentType)
		{
			WebRequest webRequest = this.GetWebRequest(new Uri(url));
			webRequest.Method = "GET";
			WebResponse webResponse = null;
			try
			{
				webResponse = this.GetWebResponse(webRequest);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new WebException(Res.GetString("ThereWasAnErrorDownloading0", new object[] { url }), ex);
			}
			HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
			if (httpWebResponse != null && httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				string text = RequestResponseUtils.CreateResponseExceptionString(httpWebResponse);
				throw new WebException(Res.GetString("ThereWasAnErrorDownloading0", new object[] { url }), new WebException(text, null, WebExceptionStatus.ProtocolError, webResponse));
			}
			Stream responseStream = webResponse.GetResponseStream();
			Stream stream;
			try
			{
				url = webResponse.ResponseUri.ToString();
				contentType = webResponse.ContentType;
				if (webResponse.ResponseUri.Scheme == Uri.UriSchemeFtp || webResponse.ResponseUri.Scheme == Uri.UriSchemeFile)
				{
					int num = webResponse.ResponseUri.AbsolutePath.LastIndexOf('.');
					if (num != -1)
					{
						string text2 = webResponse.ResponseUri.AbsolutePath.Substring(num + 1).ToLower(CultureInfo.InvariantCulture);
						if (text2 == "xml" || text2 == "wsdl" || text2 == "xsd" || text2 == "disco")
						{
							contentType = "text/xml";
						}
					}
				}
				stream = RequestResponseUtils.StreamToMemoryStream(responseStream);
			}
			finally
			{
				responseStream.Close();
			}
			return stream;
		}

		/// <summary>Instructs the <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> object to load any external references.</summary>
		// Token: 0x06000414 RID: 1044 RVA: 0x0000210D File Offset: 0x0000030D
		[ComVisible(false)]
		[Obsolete("This method will be removed from a future version. The method call is no longer required for resource discovery", false)]
		public void LoadExternals()
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00013184 File Offset: 0x00011384
		internal void FixupReferences()
		{
			foreach (object obj in this.References.Values)
			{
				((DiscoveryReference)obj).LoadExternals(this.InlinedSchemas);
			}
			foreach (object obj2 in this.InlinedSchemas.Keys)
			{
				string text = (string)obj2;
				this.Documents.Remove(text);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00013238 File Offset: 0x00011438
		private static bool IsFilenameInUse(Hashtable filenames, string path)
		{
			return filenames[path.ToLower(CultureInfo.InvariantCulture)] != null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001324E File Offset: 0x0001144E
		private static void AddFilename(Hashtable filenames, string path)
		{
			filenames.Add(path.ToLower(CultureInfo.InvariantCulture), path);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00013264 File Offset: 0x00011464
		private static string GetUniqueFilename(Hashtable filenames, string path)
		{
			if (DiscoveryClientProtocol.IsFilenameInUse(filenames, path))
			{
				string extension = Path.GetExtension(path);
				string text = path.Substring(0, path.Length - extension.Length);
				int num = 0;
				do
				{
					path = text + num.ToString(CultureInfo.InvariantCulture) + extension;
					num++;
				}
				while (DiscoveryClientProtocol.IsFilenameInUse(filenames, path));
			}
			DiscoveryClientProtocol.AddFilename(filenames, path);
			return path;
		}

		/// <summary>Reads in a file containing a map of saved discovery documents populating the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> and <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" /> properties, with discovery documents, XML Schema Definition (XSD) schemas, and service descriptions referenced in the file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results found in the file with the map of saved discovery documents. The file format is a <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol.DiscoveryClientResultsFile" /> class serialized into XML; however, one would typically create the file using only the <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> method or Disco.exe.</returns>
		/// <param name="topLevelFilename">Name of file to read in, containing the map of saved discovery documents. </param>
		// Token: 0x06000419 RID: 1049 RVA: 0x000132C4 File Offset: 0x000114C4
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public DiscoveryClientResultCollection ReadAll(string topLevelFilename)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DiscoveryClientProtocol.DiscoveryClientResultsFile));
			Stream stream = File.OpenRead(topLevelFilename);
			string directoryName = Path.GetDirectoryName(topLevelFilename);
			DiscoveryClientProtocol.DiscoveryClientResultsFile discoveryClientResultsFile = null;
			try
			{
				discoveryClientResultsFile = (DiscoveryClientProtocol.DiscoveryClientResultsFile)xmlSerializer.Deserialize(stream);
				for (int i = 0; i < discoveryClientResultsFile.Results.Count; i++)
				{
					if (discoveryClientResultsFile.Results[i] == null)
					{
						throw new InvalidOperationException(Res.GetString("WebNullRef"));
					}
					string referenceTypeName = discoveryClientResultsFile.Results[i].ReferenceTypeName;
					if (referenceTypeName == null || referenceTypeName.Length == 0)
					{
						throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute", new object[] { "referenceType" }));
					}
					DiscoveryReference discoveryReference = (DiscoveryReference)Activator.CreateInstance(Type.GetType(referenceTypeName));
					discoveryReference.ClientProtocol = this;
					string url = discoveryClientResultsFile.Results[i].Url;
					if (url == null || url.Length == 0)
					{
						throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute2", new object[]
						{
							discoveryReference.GetType().FullName,
							"url"
						}));
					}
					discoveryReference.Url = url;
					string filename = discoveryClientResultsFile.Results[i].Filename;
					if (filename == null || filename.Length == 0)
					{
						throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute2", new object[]
						{
							discoveryReference.GetType().FullName,
							"filename"
						}));
					}
					Stream stream2 = File.OpenRead(Path.Combine(directoryName, discoveryClientResultsFile.Results[i].Filename));
					try
					{
						this.Documents[discoveryReference.Url] = discoveryReference.ReadDocument(stream2);
					}
					finally
					{
						stream2.Close();
					}
					this.References[discoveryReference.Url] = discoveryReference;
				}
				this.ResolveAll();
			}
			finally
			{
				stream.Close();
			}
			return discoveryClientResultsFile.Results;
		}

		/// <summary>Resolves all references to discovery documents, XML Schema Definition (XSD) schemas, and service descriptions in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" /> property, as well as references found in referenced discovery documents.</summary>
		// Token: 0x0600041A RID: 1050 RVA: 0x000134E0 File Offset: 0x000116E0
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void ResolveAll()
		{
			this.Errors.Clear();
			int num = this.InlinedSchemas.Keys.Count;
			while (num != this.References.Count)
			{
				num = this.References.Count;
				DiscoveryReference[] array = new DiscoveryReference[this.References.Count];
				this.References.Values.CopyTo(array, 0);
				int i = 0;
				while (i < array.Length)
				{
					DiscoveryReference discoveryReference = array[i];
					if (discoveryReference is DiscoveryDocumentReference)
					{
						try
						{
							((DiscoveryDocumentReference)discoveryReference).ResolveAll(true);
							goto IL_0111;
						}
						catch (Exception ex)
						{
							if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
							{
								throw;
							}
							this.Errors[discoveryReference.Url] = ex;
							if (Tracing.On)
							{
								Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex);
							}
							goto IL_0111;
						}
						goto IL_00BE;
					}
					goto IL_00BE;
					IL_0111:
					i++;
					continue;
					IL_00BE:
					try
					{
						discoveryReference.Resolve();
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						this.Errors[discoveryReference.Url] = ex2;
						if (Tracing.On)
						{
							Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex2);
						}
					}
					goto IL_0111;
				}
			}
			this.FixupReferences();
		}

		/// <summary>Resolves all references to discovery documents, XML Schema Definition (XSD) schemas and service descriptions in <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" />, as well as references found in those discovery documents.</summary>
		// Token: 0x0600041B RID: 1051 RVA: 0x00013640 File Offset: 0x00011840
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void ResolveOneLevel()
		{
			this.Errors.Clear();
			DiscoveryReference[] array = new DiscoveryReference[this.References.Count];
			this.References.Values.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					array[i].Resolve();
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					this.Errors[array[i].Url] = ex;
					if (Tracing.On)
					{
						Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveOneLevel", ex);
					}
				}
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000136EC File Offset: 0x000118EC
		private static string GetRelativePath(string fullPath, string relativeTo)
		{
			string text = Path.GetDirectoryName(Path.GetFullPath(relativeTo));
			string text2 = "";
			while (text.Length > 0)
			{
				if (text.Length <= fullPath.Length && string.Compare(text, fullPath.Substring(0, text.Length), StringComparison.OrdinalIgnoreCase) == 0)
				{
					text2 += fullPath.Substring(text.Length);
					if (text2.StartsWith(Path.DirectorySeparatorChar.ToString() ?? "", StringComparison.Ordinal))
					{
						text2 = text2.Substring(1);
					}
					return text2;
				}
				text2 = text2 + ".." + Path.DirectorySeparatorChar.ToString();
				if (text.Length < 2)
				{
					break;
				}
				int num = text.LastIndexOf(Path.DirectorySeparatorChar, text.Length - 2);
				text = text.Substring(0, num + 1);
			}
			return fullPath;
		}

		/// <summary>Writes all discovery documents, XML Schema Definition (XSD) schemas, and Service Descriptions in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property to the supplied directory and creates a file in that directory.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results of all files saved.</returns>
		/// <param name="directory">The directory in which to save all documents currently in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property. </param>
		/// <param name="topLevelFilename">The name of the file to create or overwrite containing a map of all documents saved. </param>
		// Token: 0x0600041D RID: 1053 RVA: 0x000137C0 File Offset: 0x000119C0
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public DiscoveryClientResultCollection WriteAll(string directory, string topLevelFilename)
		{
			DiscoveryClientProtocol.DiscoveryClientResultsFile discoveryClientResultsFile = new DiscoveryClientProtocol.DiscoveryClientResultsFile();
			Hashtable hashtable = new Hashtable();
			string text = Path.Combine(directory, topLevelFilename);
			DictionaryEntry[] array = new DictionaryEntry[this.Documents.Count + this.InlinedSchemas.Keys.Count];
			int num = 0;
			foreach (object obj in this.Documents)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = dictionaryEntry;
			}
			foreach (object obj2 in this.InlinedSchemas)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				array[num++] = dictionaryEntry2;
			}
			foreach (DictionaryEntry dictionaryEntry3 in array)
			{
				string text2 = (string)dictionaryEntry3.Key;
				object value = dictionaryEntry3.Value;
				if (value != null)
				{
					DiscoveryReference discoveryReference = this.References[text2];
					string text3 = ((discoveryReference == null) ? DiscoveryReference.FilenameFromUrl(base.Url) : discoveryReference.DefaultFilename);
					text3 = DiscoveryClientProtocol.GetUniqueFilename(hashtable, Path.GetFullPath(Path.Combine(directory, text3)));
					discoveryClientResultsFile.Results.Add(new DiscoveryClientResult((discoveryReference == null) ? null : discoveryReference.GetType(), text2, DiscoveryClientProtocol.GetRelativePath(text3, text)));
					Stream stream = File.Create(text3);
					try
					{
						discoveryReference.WriteDocument(value, stream);
					}
					finally
					{
						stream.Close();
					}
				}
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DiscoveryClientProtocol.DiscoveryClientResultsFile));
			Stream stream2 = File.Create(text);
			try
			{
				xmlSerializer.Serialize(new StreamWriter(stream2, new UTF8Encoding(false)), discoveryClientResultsFile);
			}
			finally
			{
				stream2.Close();
			}
			return discoveryClientResultsFile.Results;
		}

		// Token: 0x04000326 RID: 806
		private DiscoveryClientReferenceCollection references = new DiscoveryClientReferenceCollection();

		// Token: 0x04000327 RID: 807
		private DiscoveryClientDocumentCollection documents = new DiscoveryClientDocumentCollection();

		// Token: 0x04000328 RID: 808
		private Hashtable inlinedSchemas = new Hashtable();

		// Token: 0x04000329 RID: 809
		private ArrayList additionalInformation = new ArrayList();

		// Token: 0x0400032A RID: 810
		private DiscoveryExceptionDictionary errors = new DiscoveryExceptionDictionary();

		/// <summary>Represents the root element of an XML document containing the results of all files written when the <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> method is invoked.</summary>
		// Token: 0x0200009F RID: 159
		public sealed class DiscoveryClientResultsFile
		{
			/// <summary>Gets a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> objects.</summary>
			/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results from a <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.ReadAll(System.String)" /> or <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> invocation.</returns>
			// Token: 0x1700011E RID: 286
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x000139DC File Offset: 0x00011BDC
			public DiscoveryClientResultCollection Results
			{
				get
				{
					return this.results;
				}
			}

			// Token: 0x0400032B RID: 811
			private DiscoveryClientResultCollection results = new DiscoveryClientResultCollection();
		}
	}
}
