using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to make or accept connections on a transport address.</summary>
	// Token: 0x0200054B RID: 1355
	[Serializable]
	public sealed class SocketPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class that allows unrestricted access to the <see cref="T:System.Net.Sockets.Socket" /> or disallows access to the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06002A41 RID: 10817 RVA: 0x000A2BCE File Offset: 0x000A0DCE
		public SocketPermission(PermissionState state)
		{
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class for the given transport address with the specified permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values. </param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values. </param>
		/// <param name="hostName">The host name for the transport address. </param>
		/// <param name="portNumber">The port number for the transport address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		// Token: 0x06002A42 RID: 10818 RVA: 0x000A2BF6 File Offset: 0x000A0DF6
		public SocketPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			this.m_noRestriction = false;
			this.AddPermission(access, transport, hostName, portNumber);
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be accepted under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000A2C26 File Offset: 0x000A0E26
		public IEnumerator AcceptList
		{
			get
			{
				return this.m_acceptList.GetEnumerator();
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be connected to under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x000A2C33 File Offset: 0x000A0E33
		public IEnumerator ConnectList
		{
			get
			{
				return this.m_connectList.GetEnumerator();
			}
		}

		/// <summary>Adds a permission to the set of permissions for a transport address.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values. </param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values. </param>
		/// <param name="hostName">The host name for the transport address. </param>
		/// <param name="portNumber">The port number for the transport address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		// Token: 0x06002A45 RID: 10821 RVA: 0x000A2C40 File Offset: 0x000A0E40
		public void AddPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			if (this.m_noRestriction)
			{
				return;
			}
			EndpointPermission endpointPermission = new EndpointPermission(hostName, portNumber, transport);
			if (access == NetworkAccess.Accept)
			{
				this.m_acceptList.Add(endpointPermission);
				return;
			}
			this.m_connectList.Add(endpointPermission);
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.SocketPermission" /> class that is a copy of the current instance.</returns>
		// Token: 0x06002A46 RID: 10822 RVA: 0x000A2C83 File Offset: 0x000A0E83
		public override IPermission Copy()
		{
			return new SocketPermission(this.m_noRestriction ? PermissionState.Unrestricted : PermissionState.None)
			{
				m_connectList = (ArrayList)this.m_connectList.Clone(),
				m_acceptList = (ArrayList)this.m_acceptList.Clone()
			};
		}

		/// <summary>Returns the logical intersection between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the intersection of two <see cref="T:System.Net.SocketPermission" /> instances. If the intersection is empty, the method returns null. If the <paramref name="target" /> parameter is a null reference, the method returns null.</returns>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to intersect with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a <see cref="T:System.Net.SocketPermission" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A47 RID: 10823 RVA: 0x000A2CC4 File Offset: 0x000A0EC4
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction)
			{
				if (!this.IntersectEmpty(socketPermission))
				{
					return socketPermission.Copy();
				}
				return null;
			}
			else if (socketPermission.m_noRestriction)
			{
				if (!this.IntersectEmpty(this))
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				SocketPermission socketPermission2 = new SocketPermission(PermissionState.None);
				this.Intersect(this.m_connectList, socketPermission.m_connectList, socketPermission2.m_connectList);
				this.Intersect(this.m_acceptList, socketPermission.m_acceptList, socketPermission2.m_acceptList);
				if (!this.IntersectEmpty(socketPermission2))
				{
					return socketPermission2;
				}
				return null;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000A2D62 File Offset: 0x000A0F62
		private bool IntersectEmpty(SocketPermission permission)
		{
			return !permission.m_noRestriction && permission.m_connectList.Count == 0 && permission.m_acceptList.Count == 0;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000A2D8C File Offset: 0x000A0F8C
		private void Intersect(ArrayList list1, ArrayList list2, ArrayList result)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				foreach (object obj2 in list2)
				{
					EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
					EndpointPermission endpointPermission3 = endpointPermission.Intersect(endpointPermission2);
					if (endpointPermission3 != null)
					{
						bool flag = false;
						for (int i = 0; i < result.Count; i++)
						{
							EndpointPermission endpointPermission4 = (EndpointPermission)result[i];
							EndpointPermission endpointPermission5 = endpointPermission3.Intersect(endpointPermission4);
							if (endpointPermission5 != null)
							{
								result[i] = endpointPermission5;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							result.Add(endpointPermission3);
						}
					}
				}
			}
		}

		/// <summary>Determines if the current permission is a subset of the specified permission.</summary>
		/// <returns>If <paramref name="target" /> is null, this method returns true if the current instance defines no permissions; otherwise, false. If <paramref name="target" /> is not null, this method returns true if the current instance defines a subset of <paramref name="target" /> permissions; otherwise, false.</returns>
		/// <param name="target">A <see cref="T:System.Net.SocketPermission" /> that is to be tested for the subset relationship. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.Sockets.SocketException" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A4A RID: 10826 RVA: 0x000A2E80 File Offset: 0x000A1080
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Parameter target must be of type SocketPermission");
			}
			return socketPermission.m_noRestriction || (!this.m_noRestriction && ((this.m_acceptList.Count == 0 && this.m_connectList.Count == 0) || ((socketPermission.m_acceptList.Count != 0 || socketPermission.m_connectList.Count != 0) && this.IsSubsetOf(this.m_connectList, socketPermission.m_connectList) && this.IsSubsetOf(this.m_acceptList, socketPermission.m_acceptList))));
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000A2F40 File Offset: 0x000A1140
		private bool IsSubsetOf(ArrayList list1, ArrayList list2)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				bool flag = false;
				foreach (object obj2 in list2)
				{
					EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
					if (endpointPermission.IsSubsetOf(endpointPermission2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>true if the <see cref="T:System.Net.SocketPermission" /> instance is created with the Unrestricted value from <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, false.</returns>
		// Token: 0x06002A4C RID: 10828 RVA: 0x000A2FEC File Offset: 0x000A11EC
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.SocketPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the <see cref="T:System.Net.SocketPermission" /> instance, including state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002A4D RID: 10829 RVA: 0x000A2FF4 File Offset: 0x000A11F4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if (this.m_connectList.Count > 0)
			{
				this.ToXml(securityElement, "ConnectAccess", this.m_connectList.GetEnumerator());
			}
			if (this.m_acceptList.Count > 0)
			{
				this.ToXml(securityElement, "AcceptAccess", this.m_acceptList.GetEnumerator());
			}
			return securityElement;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A3098 File Offset: 0x000A1298
		private void ToXml(SecurityElement root, string childName, IEnumerator enumerator)
		{
			SecurityElement securityElement = new SecurityElement(childName);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				EndpointPermission endpointPermission = obj as EndpointPermission;
				SecurityElement securityElement2 = new SecurityElement("ENDPOINT");
				securityElement2.AddAttribute("host", endpointPermission.Hostname);
				securityElement2.AddAttribute("transport", endpointPermission.Transport.ToString());
				securityElement2.AddAttribute("port", (endpointPermission.Port == -1) ? "All" : endpointPermission.Port.ToString());
				securityElement.AddChild(securityElement2);
			}
			root.AddChild(securityElement);
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.SocketPermission" /> instance for an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the <see cref="T:System.Net.SocketPermission" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> is not a permission element for this type. </exception>
		// Token: 0x06002A4F RID: 10831 RVA: 0x000A3138 File Offset: 0x000A1338
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.m_noRestriction = string.Compare(text, "true", true) == 0;
				if (this.m_noRestriction)
				{
					return;
				}
			}
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				if (securityElement2.Tag == "ConnectAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Connect);
				}
				else if (securityElement2.Tag == "AcceptAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Accept);
				}
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A3244 File Offset: 0x000A1444
		private void FromXml(ArrayList endpoints, NetworkAccess access)
		{
			foreach (object obj in endpoints)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (!(securityElement.Tag != "ENDPOINT"))
				{
					string text = securityElement.Attribute("host");
					TransportType transportType = (TransportType)Enum.Parse(typeof(TransportType), securityElement.Attribute("transport"), true);
					string text2 = securityElement.Attribute("port");
					int num;
					if (text2 == "All")
					{
						num = -1;
					}
					else
					{
						num = int.Parse(text2);
					}
					this.AddPermission(access, transportType, text, num);
				}
			}
		}

		/// <summary>Returns the logical union between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the union of two <see cref="T:System.Net.SocketPermission" /> instances. If <paramref name="target" /> parameter is null, it returns a copy of the current instance.</returns>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to combine with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.SocketPermission" />. </exception>
		// Token: 0x06002A51 RID: 10833 RVA: 0x000A3310 File Offset: 0x000A1510
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction || socketPermission.m_noRestriction)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			SocketPermission socketPermission2 = (SocketPermission)socketPermission.Copy();
			socketPermission2.m_acceptList.InsertRange(socketPermission2.m_acceptList.Count, this.m_acceptList);
			socketPermission2.m_connectList.InsertRange(socketPermission2.m_connectList.Count, this.m_connectList);
			return socketPermission2;
		}

		// Token: 0x040022F4 RID: 8948
		private ArrayList m_acceptList = new ArrayList();

		// Token: 0x040022F5 RID: 8949
		private ArrayList m_connectList = new ArrayList();

		// Token: 0x040022F6 RID: 8950
		private bool m_noRestriction;

		/// <summary>Defines a constant that represents all ports.</summary>
		// Token: 0x040022F7 RID: 8951
		public const int AllPorts = -1;
	}
}
