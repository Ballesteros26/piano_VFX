using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;
using Unity;

namespace System.Web.WebSockets
{
	/// <summary>Provides a base class that represents contextual details about an individual <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</summary>
	// Token: 0x020006A1 RID: 1697
	public abstract class AspNetWebSocketContext : WebSocketContext
	{
		/// <summary>When implemented in a derived class, initializes a new instance of the <see cref="T:System.Web.WebSockets.AspNetWebSocketContext" /> class.</summary>
		// Token: 0x060047CD RID: 18381 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected AspNetWebSocketContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the anonymous-user identifier for the current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The identifier for the anonymous user.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string AnonymousID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the host ASP.NET application.</summary>
		/// <returns>A reference to the application state.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual HttpApplicationStateBase Application
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the root virtual path of the host ASP.NET application.</summary>
		/// <returns>The path of the application.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string ApplicationPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
		/// <returns>The cache object.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual Cache Cache
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the certificate that a remote client issues in response to the server's request for the client's identity.</summary>
		/// <returns>A string that represents the binary stream of the certificate's content in ASN.1 format.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual HttpClientCertificate ClientCertificate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of active WebSocket connections.</summary>
		/// <returns>The number of active WebSocket connections.</returns>
		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x000C9E08 File Offset: 0x000C8008
		public static int ConnectionCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Returns the <see cref="P:System.Web.WebSockets.AspNetWebSocketContext.Cookies" /> collection typed as a <see cref="T:System.Net.CookieCollection" /> for Windows applications that use cookies based on the <see cref="T:System.Net.Cookie" /> class (such as WCF server applications).</summary>
		/// <returns>The collection of cookies.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override CookieCollection CookieCollection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of cookies that was sent by a remote client in an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message.</summary>
		/// <returns>The collection of cookies.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the virtual path of the requested file.</summary>
		/// <returns>The virtual path.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string FilePath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of headers that was sent by a remote client.</summary>
		/// <returns>The collection of message headers.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override NameValueCollection Headers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether a message from a remote client has been authenticated.</summary>
		/// <returns>true if the message has been authenticated; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x000C9E24 File Offset: 0x000C8024
		public override bool IsAuthenticated
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the client is connected to the server.</summary>
		/// <returns>true if the client is connected; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x060047D9 RID: 18393 RVA: 0x000C9E40 File Offset: 0x000C8040
		public virtual bool IsClientConnected
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the application that hosts the current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection is running in ASP.NET debug mode.</summary>
		/// <returns>true if the application is in debug mode; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x060047DA RID: 18394 RVA: 0x000C9E5C File Offset: 0x000C805C
		public virtual bool IsDebuggingEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message was sent from the local computer.</summary>
		/// <returns>true if the message is from the local computer; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x060047DB RID: 18395 RVA: 0x000C9E78 File Offset: 0x000C8078
		public override bool IsLocal
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates  whether the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection uses the WebSocket Secure protocol (WSS).</summary>
		/// <returns>true if the connection uses WSS; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x000C9E94 File Offset: 0x000C8094
		public override bool IsSecureConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When overridden in a derived class, gets a key/value collection that can be used to share data between a module and a handler during an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> request.</summary>
		/// <returns>The key/value collection.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual IDictionary Items
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the security token for the current user.</summary>
		/// <returns>An object that provides identity information to IIS about the current user.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WindowsIdentity LogonUserIdentity
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the origin of the WebSocket connection.</summary>
		/// <returns>The origin of the WebSocket connection.</returns>
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string Origin
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the virtual path of the requested resource.</summary>
		/// <returns>The virtual path.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string Path
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets additional path information for a resource that has a URL extension.</summary>
		/// <returns>A string that contains additional path information for a resource.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string PathInfo
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an object that contains user profile data.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileBase" /> object if the application configuration file contains a definition for the profile's properties; otherwise, null.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ProfileBase Profile
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of query string variables from an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message that was sent by the client.</summary>
		/// <returns>The collection of query string variables that was sent by the client. For example, if the request URL is http://www.contoso.com/default.aspx?id=44, this property returns a collection that contains a single item whose value is id=44.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual NameValueCollection QueryString
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the portion of a URL that follows the website name in an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message that was sent by the client.</summary>
		/// <returns>The part of the URL that follows the website name. For example, if the complete request URL is http://www.contoso.com/default.aspx?id=44, the value of this property is /default.aspx?id=44.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string RawUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the original Uniform Resource Identifier (URI) of an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message that was sent by the client.</summary>
		/// <returns>The original URI.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Uri RequestUri
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the encrypted key that is sent in the handshake request to establish an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The encrypted key.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string SecWebSocketKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a list of application-level protocols (subprotocols) that a client can use to send messages using an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The list of subprotocols, separated by spaces.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the version of the WebSocket protocol that an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection must use.</summary>
		/// <returns>The specified version of the WebSocket protocol.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string SecWebSocketVersion
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object that provides methods that are used in processing requests.</summary>
		/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object that provides methods that are used in processing requests.</returns>
		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual HttpServerUtilityBase Server
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a name/value collection of variables that provide information about the web server and about the current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The collection of server variables.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual NameValueCollection ServerVariables
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the timestamp of an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message that was sent by the client.</summary>
		/// <returns>The timestamp.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x000C9EB0 File Offset: 0x000C80B0
		public virtual DateTime Timestamp
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets unvalidated versions of one or more field values that are submitted in an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> message.</summary>
		/// <returns>The unvalidated values.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of URI data about the message that was sent by the client prior to the current message.</summary>
		/// <returns>The URI data.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual Uri UrlReferrer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an object that represents the security context of the user for the current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The security context of the user.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IPrincipal User
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the remote client.</summary>
		/// <returns>The browser name and version.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string UserAgent
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the IP address of the remote client.</summary>
		/// <returns>The IP address.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string UserHostAddress
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the DNS name of the remote client.</summary>
		/// <returns>The DNS name.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string UserHostName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of language preferences for the remote client.</summary>
		/// <returns>The client language preferences.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string[] UserLanguages
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> instance.</summary>
		/// <returns>The current <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> instance.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override WebSocket WebSocket
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
