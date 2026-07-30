using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x020004F2 RID: 1266
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x06002608 RID: 9736 RVA: 0x000020EB File Offset: 0x000002EB
		public AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000930B3 File Offset: 0x000912B3
		// (set) Token: 0x0600260A RID: 9738 RVA: 0x000930BB File Offset: 0x000912BB
		public Uri AutomaticConfigurationScript { get; set; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000930C4 File Offset: 0x000912C4
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x000930CC File Offset: 0x000912CC
		public bool AutomaticallyDetectSettings { get; set; }

		// Token: 0x0600260D RID: 9741 RVA: 0x000930D8 File Offset: 0x000912D8
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000930F1 File Offset: 0x000912F1
		public bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			return false;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Close()
		{
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Abort(ref int syncStatus)
		{
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000027E8 File Offset: 0x000009E8
		public void CheckForChanges()
		{
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000930F8 File Offset: 0x000912F8
		public WebProxyData GetWebProxyData()
		{
			try
			{
				WebProxyData webProxyData;
				if (AutoWebProxyScriptEngine.IsWindows())
				{
					webProxyData = this.InitializeRegistryGlobalProxy();
					if (webProxyData != null)
					{
						return webProxyData;
					}
				}
				webProxyData = this.ReadEnvVariables();
				if (webProxyData != null)
				{
					return webProxyData;
				}
			}
			catch (DllNotFoundException)
			{
			}
			return new WebProxyData();
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x00093144 File Offset: 0x00091344
		private WebProxyData ReadEnvVariables()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy") ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					Uri uri = new Uri(text);
					IPAddress ipaddress;
					if (IPAddress.TryParse(uri.Host, out ipaddress))
					{
						if (IPAddress.Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					bool flag = false;
					ArrayList arrayList = new ArrayList();
					string text2 = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
					if (text2 != null)
					{
						foreach (string text3 in text2.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text3 != "*.local")
							{
								arrayList.Add(text3);
							}
							else
							{
								flag = true;
							}
						}
					}
					return new WebProxyData
					{
						proxyAddress = uri,
						bypassOnLocal = flag,
						bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
					};
				}
				catch (UriFormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000932A4 File Offset: 0x000914A4
		private static bool IsWindows()
		{
			return Environment.OSVersion.Platform < PlatformID.Unix;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000932B4 File Offset: 0x000914B4
		private WebProxyData InitializeRegistryGlobalProxy()
		{
			if ((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable", 0) > 0)
			{
				string text = "";
				bool flag = false;
				ArrayList arrayList = new ArrayList();
				string text2 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer", null);
				string text3 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyOverride", null);
				if (text2.Contains("="))
				{
					foreach (string text4 in text2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
					{
						if (text4.StartsWith("http="))
						{
							text = text4.Substring(5);
							break;
						}
					}
				}
				else
				{
					text = text2;
				}
				if (text3 != null)
				{
					foreach (string text5 in text3.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
					{
						if (text5 != "<local>")
						{
							arrayList.Add(text5);
						}
						else
						{
							flag = true;
						}
					}
				}
				return new WebProxyData
				{
					proxyAddress = AutoWebProxyScriptEngine.ToUri(text),
					bypassOnLocal = flag,
					bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
				};
			}
			return null;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000933E8 File Offset: 0x000915E8
		private static Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://", StringComparison.Ordinal) == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00093414 File Offset: 0x00091614
		private static ArrayList CreateBypassList(ArrayList al)
		{
			string[] array = al.ToArray(typeof(string)) as string[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "^" + Regex.Escape(array[i]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}
			return new ArrayList(array);
		}
	}
}
