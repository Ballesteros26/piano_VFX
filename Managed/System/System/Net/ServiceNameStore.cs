using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200049C RID: 1180
	internal class ServiceNameStore
	{
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x00086DF3 File Offset: 0x00084FF3
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00086E14 File Offset: 0x00085014
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00086E2E File Offset: 0x0008502E
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x00086E50 File Offset: 0x00085050
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string text in array)
			{
				if (this.AddSingleServiceName(text))
				{
					flag = true;
					bool on = Logging.On;
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else
			{
				bool on2 = Logging.On;
			}
			return flag;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00086EA0 File Offset: 0x000850A0
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
			}
			return flag;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00086EE5 File Offset: 0x000850E5
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00086EF8 File Offset: 0x000850F8
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00086F0C File Offset: 0x0008510C
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				return new Uri(uriPrefix).Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00086FA0 File Offset: 0x000851A0
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00086FC8 File Offset: 0x000851C8
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[] { "HTTP/" + hostName };
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[] { "HTTP/" + text };
				}
				catch (SecurityException)
				{
					return new string[] { "HTTP/" + text };
				}
			}
			return new string[] { "HTTP/" + text };
		}

		// Token: 0x04001F31 RID: 7985
		private List<string> serviceNames;

		// Token: 0x04001F32 RID: 7986
		private ServiceNameCollection serviceNameCollection;
	}
}
