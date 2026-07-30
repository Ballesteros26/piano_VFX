using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies security actions to control <see cref="T:System.Net.Sockets.Socket" /> connections. This class cannot be inherited.</summary>
	// Token: 0x0200054C RID: 1356
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value. </exception>
		// Token: 0x06002A52 RID: 10834 RVA: 0x0008208A File Offset: 0x0008028A
		public SocketPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the network access method that is allowed by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the network access method that is allowed by this instance of <see cref="T:System.Net.SocketPermissionAttribute" />. Valid values are "Accept" and "Connect." </returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Access" /> property is not null when you attempt to set the value. To specify more than one Access method, use an additional attribute declaration statement. </exception>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000A3393 File Offset: 0x000A1593
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x000A339B File Offset: 0x000A159B
		public string Access
		{
			get
			{
				return this.m_access;
			}
			set
			{
				if (this.m_access != null)
				{
					this.AlreadySet("Access");
				}
				this.m_access = value;
			}
		}

		/// <summary>Gets or sets the DNS host name or IP address that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the DNS host name or IP address that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Host" /> is not null when you attempt to set the value. To specify more than one host, use an additional attribute declaration statement. </exception>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000A33B7 File Offset: 0x000A15B7
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x000A33BF File Offset: 0x000A15BF
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (this.m_host != null)
				{
					this.AlreadySet("Host");
				}
				this.m_host = value;
			}
		}

		/// <summary>Gets or sets the port number that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the port number that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Port" /> property is null when you attempt to set the value. To specify more than one port, use an additional attribute declaration statement. </exception>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000A33DB File Offset: 0x000A15DB
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x000A33E3 File Offset: 0x000A15E3
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (this.m_port != null)
				{
					this.AlreadySet("Port");
				}
				this.m_port = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.TransportType" /> that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the <see cref="T:System.Net.TransportType" /> that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Transport" /> is not null when you attempt to set the value. To specify more than one transport type, use an additional attribute declaration statement. </exception>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000A33FF File Offset: 0x000A15FF
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x000A3407 File Offset: 0x000A1607
		public string Transport
		{
			get
			{
				return this.m_transport;
			}
			set
			{
				if (this.m_transport != null)
				{
					this.AlreadySet("Transport");
				}
				this.m_transport = value;
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.SocketPermission" /> class.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.SocketPermission" /> class that corresponds to the security declaration.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the current instance's <see cref="P:System.Net.SocketPermissionAttribute.Access" />, <see cref="P:System.Net.SocketPermissionAttribute.Host" />, <see cref="P:System.Net.SocketPermissionAttribute.Transport" />, or <see cref="P:System.Net.SocketPermissionAttribute.Port" /> properties is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A5B RID: 10843 RVA: 0x000A3424 File Offset: 0x000A1624
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			string text = string.Empty;
			if (this.m_access == null)
			{
				text += "Access, ";
			}
			if (this.m_host == null)
			{
				text += "Host, ";
			}
			if (this.m_port == null)
			{
				text += "Port, ";
			}
			if (this.m_transport == null)
			{
				text += "Transport, ";
			}
			if (text.Length > 0)
			{
				string text2 = global::Locale.GetText("The value(s) for {0} must be specified.");
				text = text.Substring(0, text.Length - 2);
				throw new ArgumentException(string.Format(text2, text));
			}
			int num = -1;
			NetworkAccess networkAccess;
			if (string.Compare(this.m_access, "Connect", true) == 0)
			{
				networkAccess = NetworkAccess.Connect;
			}
			else
			{
				if (string.Compare(this.m_access, "Accept", true) != 0)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Access', '{1}, is invalid."), this.m_access));
				}
				networkAccess = NetworkAccess.Accept;
			}
			if (string.Compare(this.m_port, "All", true) != 0)
			{
				try
				{
					num = int.Parse(this.m_port);
				}
				catch
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Port', '{1}, is invalid."), this.m_port));
				}
				new IPEndPoint(1L, num);
			}
			TransportType transportType;
			try
			{
				transportType = (TransportType)Enum.Parse(typeof(TransportType), this.m_transport, true);
			}
			catch
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Transport', '{1}, is invalid."), this.m_transport));
			}
			SocketPermission socketPermission = new SocketPermission(PermissionState.None);
			socketPermission.AddPermission(networkAccess, transportType, this.m_host, num);
			return socketPermission;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000A35C8 File Offset: 0x000A17C8
		internal void AlreadySet(string property)
		{
			throw new ArgumentException(string.Format(global::Locale.GetText("The parameter '{0}' can be set only once."), property), property);
		}

		// Token: 0x040022F8 RID: 8952
		private string m_access;

		// Token: 0x040022F9 RID: 8953
		private string m_host;

		// Token: 0x040022FA RID: 8954
		private string m_port;

		// Token: 0x040022FB RID: 8955
		private string m_transport;
	}
}
