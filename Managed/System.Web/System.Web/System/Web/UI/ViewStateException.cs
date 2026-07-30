using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents the exception that is thrown when the view state cannot be loaded or validated. This class cannot be inherited.</summary>
	// Token: 0x0200024B RID: 587
	[Serializable]
	public sealed class ViewStateException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class. </summary>
		// Token: 0x0600180B RID: 6155 RVA: 0x00040E35 File Offset: 0x0003F035
		public ViewStateException()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the client is currently connected to the server.</summary>
		/// <returns>true if the client is still connected to the server; otherwise, false.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00003A1F File Offset: 0x00001C1F
		public bool IsConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets debugging information about the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the formatted message with information about the exception.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override string Message
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the path from the request.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the contents of the view-state string that, when read, caused the view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the view-state values that caused the view-state exception.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string PersistedState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URL of the page that linked to the page where the view-state exception occurred.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the HTTP referrer.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string Referer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the IP address of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the remote IP address of the client.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string RemoteAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the port number of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the remote port number.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string RemotePort
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the browser type of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the user agent, which is typically the browser type.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string UserAgent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001814 RID: 6164 RVA: 0x00003A1F File Offset: 0x00001C1F
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}
