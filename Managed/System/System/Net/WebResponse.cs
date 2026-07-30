using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a response from a Uniform Resource Identifier (URI). This is an abstract class.</summary>
	// Token: 0x0200047A RID: 1146
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class.</summary>
		// Token: 0x060021EE RID: 8686 RVA: 0x0004E8BB File Offset: 0x0004CABB
		protected WebResponse()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that indicates the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class. </exception>
		// Token: 0x060021EF RID: 8687 RVA: 0x0004E8BB File Offset: 0x0004CABB
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize <see cref="T:System.Net.WebResponse" />.  </summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.Net.WebResponse" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.WebResponse" />. </param>
		// Token: 0x060021F0 RID: 8688 RVA: 0x000843F0 File Offset: 0x000825F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization. </param>
		// Token: 0x060021F1 RID: 8689 RVA: 0x000027E8 File Offset: 0x000009E8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>When overridden by a descendant class, closes the response stream.</summary>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021F2 RID: 8690 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Close()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object.</summary>
		// Token: 0x060021F3 RID: 8691 RVA: 0x000843FA File Offset: 0x000825FA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x060021F4 RID: 8692 RVA: 0x0008440C File Offset: 0x0008260C
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			try
			{
				this.Close();
			}
			catch
			{
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this response was obtained from the cache.</summary>
		/// <returns>true if the response was taken from the cache; otherwise, false.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0008443C File Offset: 0x0008263C
		public virtual bool IsFromCache
		{
			get
			{
				return this.m_IsFromCache;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x00084444 File Offset: 0x00082644
		internal bool InternalSetFromCache
		{
			set
			{
				this.m_IsFromCache = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0008444D File Offset: 0x0008264D
		internal virtual bool IsCacheFresh
		{
			get
			{
				return this.m_IsCacheFresh;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x00084455 File Offset: 0x00082655
		internal bool InternalSetIsCacheFresh
		{
			set
			{
				this.m_IsCacheFresh = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of data being received.</summary>
		/// <returns>The number of bytes returned from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x0007D719 File Offset: 0x0007B919
		// (set) Token: 0x060021FB RID: 8699 RVA: 0x0007D719 File Offset: 0x0007B919
		public virtual long ContentLength
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the content type of the data being received.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0007D719 File Offset: 0x0007B919
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x0007D719 File Offset: 0x0007B919
		public virtual string ContentType
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, returns the data stream from the Internet resource.</summary>
		/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class for reading data from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060021FE RID: 8702 RVA: 0x0007D720 File Offset: 0x0007B920
		public virtual Stream GetResponseStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a derived class, gets the URI of the Internet resource that actually responded to the request.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet resource that actually responded to the request.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0007D719 File Offset: 0x0007B919
		public virtual Uri ResponseUri
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of header name-value pairs associated with this request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.WebHeaderCollection" /> class that contains header values associated with this response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0007D719 File Offset: 0x0007B919
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Gets a value that indicates if headers are supported.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if headers are supported; otherwise, false.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool SupportsHeaders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001E87 RID: 7815
		private bool m_IsCacheFresh;

		// Token: 0x04001E88 RID: 7816
		private bool m_IsFromCache;
	}
}
