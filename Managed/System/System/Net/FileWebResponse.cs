using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class FileWebResponse : WebResponse, ISerializable, ICloseEx
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x0008CC44 File Offset: 0x0008AE44
		internal FileWebResponse(FileWebRequest request, Uri uri, FileAccess access, bool asyncHint)
		{
			try
			{
				this.m_fileAccess = access;
				if (access == FileAccess.Write)
				{
					this.m_stream = Stream.Null;
				}
				else
				{
					this.m_stream = new FileWebStream(request, uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, asyncHint);
					this.m_contentLength = this.m_stream.Length;
				}
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebResponse);
				this.m_headers.AddInternal("Content-Length", this.m_contentLength.ToString(NumberFormatInfo.InvariantInfo));
				this.m_headers.AddInternal("Content-Type", "application/octet-stream");
				this.m_uri = uri;
			}
			catch (Exception ex)
			{
				throw new WebException(ex.Message, ex, WebExceptionStatus.ConnectFailure, null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains the source of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		// Token: 0x06002411 RID: 9233 RVA: 0x0008CD08 File Offset: 0x0008AF08
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.FileWebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> , which will hold the serialized data for the <see cref="T:System.Net.FileWebResponse" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the destination of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" />. </param>
		// Token: 0x06002412 RID: 9234 RVA: 0x000843F0 File Offset: 0x000825F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002413 RID: 9235 RVA: 0x0008CD80 File Offset: 0x0008AF80
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the length of the content in the file system resource.</summary>
		/// <returns>The number of bytes returned from the file system resource.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0008CDF2 File Offset: 0x0008AFF2
		public override long ContentLength
		{
			get
			{
				this.CheckDisposed();
				return this.m_contentLength;
			}
		}

		/// <summary>Gets the content type of the file system resource.</summary>
		/// <returns>The value "binary/octet-stream".</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0008CE00 File Offset: 0x0008B000
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				return "application/octet-stream";
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header name/value pairs associated with the response.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0008CE0D File Offset: 0x0008B00D
		public override WebHeaderCollection Headers
		{
			get
			{
				this.CheckDisposed();
				return this.m_headers;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance; otherwise, false.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the URI of the file system resource that provided the response.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the file system resource that provided the response.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x0008CE1B File Offset: 0x0008B01B
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.m_uri;
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0008CE29 File Offset: 0x0008B029
		private void CheckDisposed()
		{
			if (this.m_closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		/// <summary>Closes the response stream.</summary>
		// Token: 0x0600241A RID: 9242 RVA: 0x0008CE44 File Offset: 0x0008B044
		public override void Close()
		{
			((ICloseEx)this).CloseEx(CloseExState.Normal);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x0008CE50 File Offset: 0x0008B050
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			try
			{
				if (!this.m_closed)
				{
					this.m_closed = true;
					Stream stream = this.m_stream;
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(closeState);
						}
						else
						{
							stream.Close();
						}
						this.m_stream = null;
					}
				}
			}
			finally
			{
			}
		}

		/// <summary>Returns the data stream from the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for reading data from the file system resource.</returns>
		// Token: 0x0600241C RID: 9244 RVA: 0x0008CEAC File Offset: 0x0008B0AC
		public override Stream GetResponseStream()
		{
			try
			{
				this.CheckDisposed();
			}
			finally
			{
			}
			return this.m_stream;
		}

		// Token: 0x04002004 RID: 8196
		private const int DefaultFileStreamBufferSize = 8192;

		// Token: 0x04002005 RID: 8197
		private const string DefaultFileContentType = "application/octet-stream";

		// Token: 0x04002006 RID: 8198
		private bool m_closed;

		// Token: 0x04002007 RID: 8199
		private long m_contentLength;

		// Token: 0x04002008 RID: 8200
		private FileAccess m_fileAccess;

		// Token: 0x04002009 RID: 8201
		private WebHeaderCollection m_headers;

		// Token: 0x0400200A RID: 8202
		private Stream m_stream;

		// Token: 0x0400200B RID: 8203
		private Uri m_uri;
	}
}
