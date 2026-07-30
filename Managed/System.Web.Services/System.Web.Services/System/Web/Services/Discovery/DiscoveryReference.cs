using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>The base class for discoverable references using XML Web services discovery.</summary>
	// Token: 0x020000AB RID: 171
	public abstract class DiscoveryReference
	{
		/// <summary>Gets or sets the instance of <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> used in a discovery process.</summary>
		/// <returns>An instance of <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> used in a discovery process </returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000151D0 File Offset: 0x000133D0
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000151D8 File Offset: 0x000133D8
		[XmlIgnore]
		public DiscoveryClientProtocol ClientProtocol
		{
			get
			{
				return this.clientProtocol;
			}
			set
			{
				this.clientProtocol = value;
			}
		}

		/// <summary>Gets the name of the default file to use when saving the referenced discovery document, XSD schema, or Service Description.</summary>
		/// <returns>Name of the default file to use when saving the referenced document.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000151E1 File Offset: 0x000133E1
		[XmlIgnore]
		public virtual string DefaultFilename
		{
			get
			{
				return DiscoveryReference.FilenameFromUrl(this.Url);
			}
		}

		/// <summary>When overridden in a derived class, writes the document to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="document">The document to write into a <see cref="T:System.IO.Stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the <paramref name="document" /> is written. </param>
		// Token: 0x06000476 RID: 1142
		public abstract void WriteDocument(object document, Stream stream);

		/// <summary>Reads the passed <see cref="T:System.IO.Stream" /> and returns an instance of the class representing the type of referenced document.</summary>
		/// <returns>An <see cref="T:System.Object" /> with an underlying type matching the type of referenced document.</returns>
		/// <param name="stream">
		///   <see cref="T:System.IO.Stream" /> containing the reference document. </param>
		// Token: 0x06000477 RID: 1143
		public abstract object ReadDocument(Stream stream);

		/// <summary>Gets or sets the URL of the referenced document.</summary>
		/// <returns>The URL of the referenced document.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000478 RID: 1144
		// (set) Token: 0x06000479 RID: 1145
		[XmlIgnore]
		public abstract string Url { get; set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x0000210D File Offset: 0x0000030D
		internal virtual void LoadExternals(Hashtable loadedExternals)
		{
		}

		/// <summary>Returns a file name based on the passed URL.</summary>
		/// <returns>Name of the file based on the passed URL.</returns>
		/// <param name="url">The URL on which the name of the file is based. </param>
		// Token: 0x0600047B RID: 1147 RVA: 0x000151F0 File Offset: 0x000133F0
		public static string FilenameFromUrl(string url)
		{
			int num = url.LastIndexOf('/', url.Length - 1);
			if (num >= 0)
			{
				url = url.Substring(num + 1);
			}
			int num2 = url.IndexOf('.');
			if (num2 >= 0)
			{
				url = url.Substring(0, num2);
			}
			int num3 = url.IndexOf('?');
			if (num3 >= 0)
			{
				url = url.Substring(0, num3);
			}
			if (url == null || url.Length == 0)
			{
				return "item";
			}
			return DiscoveryReference.MakeValidFilename(url);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015264 File Offset: 0x00013464
		private static bool FindChar(char ch, char[] chars)
		{
			for (int i = 0; i < chars.Length; i++)
			{
				if (ch == chars[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015288 File Offset: 0x00013488
		internal static string MakeValidFilename(string filename)
		{
			if (filename == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(filename.Length);
			foreach (char c in filename)
			{
				if (!DiscoveryReference.FindChar(c, Path.InvalidPathChars))
				{
					stringBuilder.Append(c);
				}
			}
			string text = stringBuilder.ToString();
			if (text.Length == 0)
			{
				text = "item";
			}
			return Path.GetFileName(text);
		}

		/// <summary>Downloads the referenced document at <see cref="P:System.Web.Services.Discovery.DiscoveryReference.Url" /> to resolve whether the referenced document is valid.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600047E RID: 1150 RVA: 0x000152F0 File Offset: 0x000134F0
		public void Resolve()
		{
			if (this.ClientProtocol == null)
			{
				throw new InvalidOperationException(Res.GetString("WebResolveMissingClientProtocol"));
			}
			if (this.ClientProtocol.Documents[this.Url] != null)
			{
				return;
			}
			if (this.ClientProtocol.InlinedSchemas[this.Url] != null)
			{
				return;
			}
			string url = this.Url;
			string url2 = this.Url;
			string text = null;
			Stream stream = this.ClientProtocol.Download(ref url, ref text);
			if (this.ClientProtocol.Documents[url] != null)
			{
				this.Url = url;
				return;
			}
			try
			{
				this.Url = url;
				this.Resolve(text, stream);
			}
			catch
			{
				this.Url = url2;
				throw;
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000153C4 File Offset: 0x000135C4
		internal Exception AttemptResolve(string contentType, Stream stream)
		{
			Exception ex;
			try
			{
				this.Resolve(contentType, stream);
				ex = null;
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "AttemptResolve", ex2);
				}
				ex = ex2;
			}
			return ex;
		}

		/// <summary>Resolves whether the referenced document is valid.</summary>
		/// <param name="contentType">The MIME type of <paramref name="stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> containing the referenced document. </param>
		// Token: 0x06000480 RID: 1152
		protected internal abstract void Resolve(string contentType, Stream stream);

		// Token: 0x06000481 RID: 1153 RVA: 0x00015424 File Offset: 0x00013624
		internal static string UriToString(string baseUrl, string relUrl)
		{
			return new Uri(new Uri(baseUrl), relUrl).GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
		}

		// Token: 0x04000346 RID: 838
		private DiscoveryClientProtocol clientProtocol;
	}
}
