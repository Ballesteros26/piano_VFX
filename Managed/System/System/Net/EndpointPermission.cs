using System;
using System.Net.Sockets;
using Unity;

namespace System.Net
{
	/// <summary>Defines an endpoint that is authorized by a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
	// Token: 0x02000512 RID: 1298
	[Serializable]
	public class EndpointPermission
	{
		// Token: 0x060026E5 RID: 9957 RVA: 0x000963D9 File Offset: 0x000945D9
		internal EndpointPermission(string hostname, int port, TransportType transport)
		{
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			this.hostname = hostname;
			this.port = port;
			this.transport = transport;
			this.resolved = false;
			this.hasWildcard = false;
			this.addresses = null;
		}

		/// <summary>Gets the DNS host name or IP address of the server that is associated with this endpoint.</summary>
		/// <returns>A string that contains the DNS host name or IP address of the server.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x00096419 File Offset: 0x00094619
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
		}

		/// <summary>Gets the network port number that is associated with this endpoint.</summary>
		/// <returns>The network port number that is associated with this request, or <see cref="F:System.Net.SocketPermission.AllPorts" />.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x00096421 File Offset: 0x00094621
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		/// <summary>Gets the transport type that is associated with this endpoint.</summary>
		/// <returns>One of the <see cref="T:System.Net.TransportType" /> values.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00096429 File Offset: 0x00094629
		public TransportType Transport
		{
			get
			{
				return this.transport;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The specified <see cref="T:System.Object" /></param>
		// Token: 0x060026E9 RID: 9961 RVA: 0x00096434 File Offset: 0x00094634
		public override bool Equals(object obj)
		{
			EndpointPermission endpointPermission = obj as EndpointPermission;
			return endpointPermission != null && this.port == endpointPermission.port && this.transport == endpointPermission.transport && string.Compare(this.hostname, endpointPermission.hostname, true) == 0;
		}

		/// <summary>Serves as a hash function for a particular <see cref="T:System.Net.SocketPermission" /> instance. </summary>
		/// <returns>A hash code for the current object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060026EA RID: 9962 RVA: 0x0009647E File Offset: 0x0009467E
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060026EB RID: 9963 RVA: 0x0009648C File Offset: 0x0009468C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.hostname,
				"#",
				this.port,
				"#",
				(int)this.transport
			});
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000964DC File Offset: 0x000946DC
		internal bool IsSubsetOf(EndpointPermission perm)
		{
			if (perm == null)
			{
				return false;
			}
			if (perm.port != -1 && this.port != perm.port)
			{
				return false;
			}
			if (perm.transport != TransportType.All && this.transport != perm.transport)
			{
				return false;
			}
			this.Resolve();
			perm.Resolve();
			if (this.hasWildcard)
			{
				return perm.hasWildcard && this.IsSubsetOf(this.hostname, perm.hostname);
			}
			if (this.addresses == null)
			{
				return false;
			}
			if (perm.hasWildcard)
			{
				foreach (IPAddress ipaddress in this.addresses)
				{
					if (this.IsSubsetOf(ipaddress.ToString(), perm.hostname))
					{
						return true;
					}
				}
			}
			if (perm.addresses == null)
			{
				return false;
			}
			foreach (IPAddress ipaddress2 in perm.addresses)
			{
				if (this.IsSubsetOf(this.hostname, ipaddress2.ToString()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000965CC File Offset: 0x000947CC
		private bool IsSubsetOf(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return false;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return false;
				}
				if (num != 256 && num != num2 && num2 != 256)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00096638 File Offset: 0x00094838
		internal EndpointPermission Intersect(EndpointPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			int num;
			if (this.port == perm.port)
			{
				num = this.port;
			}
			else if (this.port == -1)
			{
				num = perm.port;
			}
			else
			{
				if (perm.port != -1)
				{
					return null;
				}
				num = this.port;
			}
			TransportType transportType;
			if (this.transport == perm.transport)
			{
				transportType = this.transport;
			}
			else if (this.transport == TransportType.All)
			{
				transportType = perm.transport;
			}
			else
			{
				if (perm.transport != TransportType.All)
				{
					return null;
				}
				transportType = this.transport;
			}
			string text = this.IntersectHostname(perm);
			if (text == null)
			{
				return null;
			}
			if (!this.hasWildcard)
			{
				return this;
			}
			if (!perm.hasWildcard)
			{
				return perm;
			}
			return new EndpointPermission(text, num, transportType)
			{
				hasWildcard = true,
				resolved = true
			};
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000966FC File Offset: 0x000948FC
		private string IntersectHostname(EndpointPermission perm)
		{
			if (this.hostname == perm.hostname)
			{
				return this.hostname;
			}
			this.Resolve();
			perm.Resolve();
			string text = null;
			if (this.hasWildcard)
			{
				if (perm.hasWildcard)
				{
					text = this.Intersect(this.hostname, perm.hostname);
				}
				else if (perm.addresses != null)
				{
					for (int i = 0; i < perm.addresses.Length; i++)
					{
						text = this.Intersect(this.hostname, perm.addresses[i].ToString());
						if (text != null)
						{
							break;
						}
					}
				}
			}
			else if (this.addresses != null)
			{
				for (int j = 0; j < this.addresses.Length; j++)
				{
					string text2 = this.addresses[j].ToString();
					if (perm.hasWildcard)
					{
						text = this.Intersect(text2, perm.hostname);
					}
					else if (perm.addresses != null)
					{
						for (int k = 0; k < perm.addresses.Length; k++)
						{
							text = this.Intersect(text2, perm.addresses[k].ToString());
							if (text != null)
							{
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00096814 File Offset: 0x00094A14
		private string Intersect(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			string[] array3 = new string[7];
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return null;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return null;
				}
				if (num == 256)
				{
					array3[i << 1] = ((num2 == 256) ? "*" : (string.Empty + num2));
				}
				else if (num2 == 256)
				{
					array3[i << 1] = ((num == 256) ? "*" : (string.Empty + num));
				}
				else
				{
					if (num != num2)
					{
						return null;
					}
					array3[i << 1] = string.Empty + num;
				}
			}
			array3[1] = (array3[3] = (array3[5] = "."));
			return string.Concat(array3);
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00096918 File Offset: 0x00094B18
		private int ToNumber(string value)
		{
			if (value == "*")
			{
				return 256;
			}
			int length = value.Length;
			if (length < 1 || length > 3)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if ('0' > c || c > '9')
				{
					return -1;
				}
				num = checked(num * 10 + (int)(c - '0'));
			}
			if (num > 255)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00096984 File Offset: 0x00094B84
		internal void Resolve()
		{
			if (this.resolved)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			this.addresses = null;
			string[] array = this.hostname.Split(EndpointPermission.dot_char);
			if (array.Length != 4)
			{
				flag = true;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					int num = this.ToNumber(array[i]);
					if (num == -1)
					{
						flag = true;
						break;
					}
					if (num == 256)
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.hasWildcard = false;
				try
				{
					this.addresses = Dns.GetHostAddresses(this.hostname);
					goto IL_00A3;
				}
				catch (SocketException)
				{
					goto IL_00A3;
				}
			}
			this.hasWildcard = flag2;
			if (!flag2)
			{
				this.addresses = new IPAddress[1];
				this.addresses[0] = IPAddress.Parse(this.hostname);
			}
			IL_00A3:
			this.resolved = true;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00096A4C File Offset: 0x00094C4C
		internal void UndoResolve()
		{
			this.resolved = false;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal EndpointPermission()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002128 RID: 8488
		private static char[] dot_char = new char[] { '.' };

		// Token: 0x04002129 RID: 8489
		private string hostname;

		// Token: 0x0400212A RID: 8490
		private int port;

		// Token: 0x0400212B RID: 8491
		private TransportType transport;

		// Token: 0x0400212C RID: 8492
		private bool resolved;

		// Token: 0x0400212D RID: 8493
		private bool hasWildcard;

		// Token: 0x0400212E RID: 8494
		private IPAddress[] addresses;
	}
}
