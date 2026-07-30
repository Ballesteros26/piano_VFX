using System;

namespace System.Net
{
	// Token: 0x02000539 RID: 1337
	internal sealed class ListenerPrefix
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x0009FEB3 File Offset: 0x0009E0B3
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0009FEC9 File Offset: 0x0009E0C9
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0009FED1 File Offset: 0x0009E0D1
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x0009FED9 File Offset: 0x0009E0D9
		public IPAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0009FEE2 File Offset: 0x0009E0E2
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0009FEEA File Offset: 0x0009E0EA
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x0009FEF2 File Offset: 0x0009E0F2
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0009FEFA File Offset: 0x0009E0FA
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0009FF04 File Offset: 0x0009E104
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0009FF2E File Offset: 0x0009E12E
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0009FF3C File Offset: 0x0009E13C
		private void Parse(string uri)
		{
			ushort num = 80;
			if (uri.StartsWith("https://"))
			{
				num = 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (uri[num2] == '[')
			{
				num3 = uri.IndexOf("]:") + 1;
			}
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf('/', num2, length - num2);
			if (num4 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num3 > 0)
			{
				this.host = uri.Substring(num2, num3 - num2).Trim(new char[] { '[', ']' });
				this.port = ushort.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				this.host = uri.Substring(num2, num4 - num2).Trim(new char[] { '[', ']' });
				this.port = num;
			}
			this.path = uri.Substring(num4);
			if (this.path.Length != 1)
			{
				this.path = this.path.Substring(0, this.path.Length - 1);
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000A0088 File Offset: 0x0009E288
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (!uri.StartsWith("http://") && !uri.StartsWith("https://"))
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num = uri.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num2 = uri.IndexOf(':', num, length - num);
			if (uri[num] == '[')
			{
				num2 = uri.IndexOf("]:") + 1;
			}
			if (num == num2)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf('/', num, length - num);
			if (num3 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num2 > 0)
			{
				try
				{
					int num4 = int.Parse(uri.Substring(num2 + 1, num3 - num2 - 1));
					if (num4 <= 0 || num4 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x0400227F RID: 8831
		private string original;

		// Token: 0x04002280 RID: 8832
		private string host;

		// Token: 0x04002281 RID: 8833
		private ushort port;

		// Token: 0x04002282 RID: 8834
		private string path;

		// Token: 0x04002283 RID: 8835
		private bool secure;

		// Token: 0x04002284 RID: 8836
		private IPAddress[] addresses;

		// Token: 0x04002285 RID: 8837
		public HttpListener Listener;
	}
}
