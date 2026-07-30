using System;
using System.Security.Principal;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000D1 RID: 209
	internal sealed class ServerVariablesCollection : BaseParamsCollection
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001D384 File Offset: 0x0001B584
		private string QueryString
		{
			get
			{
				string queryStringRaw = this._request.QueryStringRaw;
				if (string.IsNullOrEmpty(queryStringRaw))
				{
					return queryStringRaw;
				}
				if (queryStringRaw[0] == '?')
				{
					return queryStringRaw.Substring(1);
				}
				return queryStringRaw;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		private IIdentity UserIdentity
		{
			get
			{
				HttpContext httpContext = ((this._request != null) ? this._request.Context : null);
				IPrincipal principal = ((httpContext != null) ? httpContext.User : null);
				if (principal == null)
				{
					return null;
				}
				return principal.Identity;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		public ServerVariablesCollection(HttpRequest request)
			: base(request)
		{
			base.IsReadOnly = true;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001D408 File Offset: 0x0001B608
		private void AppendKeyValue(StringBuilder sb, string key, string value, bool standard)
		{
			if (standard)
			{
				sb.Append("HTTP_");
				sb.Append(key.ToUpper(Helpers.InvariantCulture).Replace('-', '_'));
				sb.Append(":");
			}
			else
			{
				sb.Append(key);
				sb.Append(": ");
			}
			sb.Append(value);
			sb.Append("\r\n");
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001D478 File Offset: 0x0001B678
		private string Fill(HttpWorkerRequest wr, bool standard)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 40; i++)
			{
				string knownRequestHeader = wr.GetKnownRequestHeader(i);
				if (!string.IsNullOrEmpty(knownRequestHeader))
				{
					string knownRequestHeaderName = HttpWorkerRequest.GetKnownRequestHeaderName(i);
					this.AppendKeyValue(stringBuilder, knownRequestHeaderName, knownRequestHeader, standard);
				}
			}
			string[][] unknownRequestHeaders = wr.GetUnknownRequestHeaders();
			if (unknownRequestHeaders == null)
			{
				return stringBuilder.ToString();
			}
			int j = unknownRequestHeaders.Length;
			while (j > 0)
			{
				j--;
				this.AppendKeyValue(stringBuilder, unknownRequestHeaders[j][0], unknownRequestHeaders[j][1], standard);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		private void AddHeaderVariables(HttpWorkerRequest wr)
		{
			for (int i = 0; i < 40; i++)
			{
				string text = wr.GetKnownRequestHeader(i);
				if (text != null && text.Length > 0)
				{
					string text2 = HttpWorkerRequest.GetKnownRequestHeaderName(i);
					if (text2 != null && text2.Length > 0)
					{
						this.Add("HTTP_" + text2.ToUpper(Helpers.InvariantCulture).Replace('-', '_'), text);
					}
				}
			}
			string[][] unknownRequestHeaders = wr.GetUnknownRequestHeaders();
			if (unknownRequestHeaders != null)
			{
				for (int j = 0; j < unknownRequestHeaders.Length; j++)
				{
					string text2 = unknownRequestHeaders[j][0];
					if (text2 != null)
					{
						string text = unknownRequestHeaders[j][1];
						this.Add("HTTP_" + text2.ToUpper(Helpers.InvariantCulture).Replace('-', '_'), text);
					}
				}
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		private void loadServerVariablesCollection()
		{
			HttpWorkerRequest workerRequest = this._request.WorkerRequest;
			if (this.loaded || workerRequest == null)
			{
				return;
			}
			base.IsReadOnly = false;
			this.Add("ALL_HTTP", this.Fill(workerRequest, true));
			this.Add("ALL_RAW", this.Fill(workerRequest, false));
			this.Add("APPL_MD_PATH", workerRequest.GetServerVariable("APPL_MD_PATH"));
			this.Add("APPL_PHYSICAL_PATH", workerRequest.GetServerVariable("APPL_PHYSICAL_PATH"));
			IIdentity userIdentity = this.UserIdentity;
			if (userIdentity != null && userIdentity.IsAuthenticated)
			{
				this.Add("AUTH_TYPE", userIdentity.AuthenticationType);
				this.Add("AUTH_USER", userIdentity.Name);
			}
			else
			{
				this.Add("AUTH_TYPE", string.Empty);
				this.Add("AUTH_USER", string.Empty);
			}
			this.Add("AUTH_PASSWORD", workerRequest.GetServerVariable("AUTH_PASSWORD"));
			this.Add("LOGON_USER", workerRequest.GetServerVariable("LOGON_USER"));
			this.Add("REMOTE_USER", workerRequest.GetServerVariable("REMOTE_USER"));
			this.Add("CERT_COOKIE", workerRequest.GetServerVariable("CERT_COOKIE"));
			this.Add("CERT_FLAGS", workerRequest.GetServerVariable("CERT_FLAGS"));
			this.Add("CERT_ISSUER", workerRequest.GetServerVariable("CERT_ISSUER"));
			this.Add("CERT_KEYSIZE", workerRequest.GetServerVariable("CERT_KEYSIZE"));
			this.Add("CERT_SECRETKEYSIZE", workerRequest.GetServerVariable("CERT_SECRETKEYSIZE"));
			this.Add("CERT_SERIALNUMBER", workerRequest.GetServerVariable("CERT_SERIALNUMBER"));
			this.Add("CERT_SERVER_ISSUER", workerRequest.GetServerVariable("CERT_SERVER_ISSUER"));
			this.Add("CERT_SERVER_SUBJECT", workerRequest.GetServerVariable("CERT_SERVER_SUBJECT"));
			this.Add("CERT_SUBJECT", workerRequest.GetServerVariable("CERT_SUBJECT"));
			string knownRequestHeader = workerRequest.GetKnownRequestHeader(11);
			if (knownRequestHeader != null)
			{
				this.Add("CONTENT_LENGTH", knownRequestHeader);
			}
			this.Add("CONTENT_TYPE", this._request.ContentType);
			this.Add("GATEWAY_INTERFACE", workerRequest.GetServerVariable("GATEWAY_INTERFACE"));
			this.Add("HTTPS", workerRequest.GetServerVariable("HTTPS"));
			this.Add("HTTPS_KEYSIZE", workerRequest.GetServerVariable("HTTPS_KEYSIZE"));
			this.Add("HTTPS_SECRETKEYSIZE", workerRequest.GetServerVariable("HTTPS_SECRETKEYSIZE"));
			this.Add("HTTPS_SERVER_ISSUER", workerRequest.GetServerVariable("HTTPS_SERVER_ISSUER"));
			this.Add("HTTPS_SERVER_SUBJECT", workerRequest.GetServerVariable("HTTPS_SERVER_SUBJECT"));
			this.Add("INSTANCE_ID", workerRequest.GetServerVariable("INSTANCE_ID"));
			this.Add("INSTANCE_META_PATH", workerRequest.GetServerVariable("INSTANCE_META_PATH"));
			this.Add("LOCAL_ADDR", workerRequest.GetLocalAddress());
			this.Add("PATH_INFO", this._request.PathInfo);
			this.Add("PATH_TRANSLATED", this._request.PhysicalPath);
			this.Add("QUERY_STRING", this.QueryString);
			this.Add("REMOTE_ADDR", this._request.UserHostAddress);
			this.Add("REMOTE_HOST", this._request.UserHostName);
			this.Add("REMOTE_PORT", workerRequest.GetRemotePort().ToString());
			this.Add("REQUEST_METHOD", this._request.HttpMethod);
			this.Add("SCRIPT_NAME", this._request.FilePath);
			this.Add("SERVER_NAME", workerRequest.GetServerName());
			this.Add("SERVER_PORT", workerRequest.GetLocalPort().ToString());
			if (workerRequest.IsSecure())
			{
				this.Add("SERVER_PORT_SECURE", "1");
			}
			else
			{
				this.Add("SERVER_PORT_SECURE", "0");
			}
			this.Add("SERVER_PROTOCOL", workerRequest.GetHttpVersion());
			this.Add("SERVER_SOFTWARE", workerRequest.GetServerVariable("SERVER_SOFTWARE"));
			this.Add("URL", this._request.FilePath);
			this.AddHeaderVariables(workerRequest);
			base.IsReadOnly = true;
			this.loaded = true;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001D9CF File Offset: 0x0001BBCF
		protected override void InsertInfo()
		{
			this.loadServerVariablesCollection();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		protected override string InternalGet(string name)
		{
			if (name == null || this._request == null)
			{
				return null;
			}
			name = name.ToUpper(Helpers.InvariantCulture);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2668667095U)
			{
				if (num <= 1848583145U)
				{
					if (num <= 1026267064U)
					{
						if (num <= 605221635U)
						{
							if (num != 334791987U)
							{
								if (num != 370945231U)
								{
									if (num != 605221635U)
									{
										goto IL_08C6;
									}
									if (!(name == "HTTP_SOAPACTION"))
									{
										goto IL_08C6;
									}
								}
								else
								{
									if (!(name == "PATH_INFO"))
									{
										goto IL_08C6;
									}
									return this._request.PathInfo;
								}
							}
							else if (!(name == "HTTPS_SERVER_SUBJECT"))
							{
								goto IL_08C6;
							}
						}
						else if (num != 732448210U)
						{
							if (num != 861989970U)
							{
								if (num != 1026267064U)
								{
									goto IL_08C6;
								}
								if (!(name == "INSTANCE_META_PATH"))
								{
									goto IL_08C6;
								}
							}
							else if (!(name == "APPL_MD_PATH"))
							{
								goto IL_08C6;
							}
						}
						else if (!(name == "CERT_KEYSIZE"))
						{
							goto IL_08C6;
						}
					}
					else if (num <= 1422767864U)
					{
						if (num != 1319203176U)
						{
							if (num != 1396424551U)
							{
								if (num != 1422767864U)
								{
									goto IL_08C6;
								}
								if (!(name == "HTTP_HOST"))
								{
									goto IL_08C6;
								}
							}
							else if (!(name == "REMOTE_USER"))
							{
								goto IL_08C6;
							}
						}
						else
						{
							if (!(name == "SERVER_PORT_SECURE"))
							{
								goto IL_08C6;
							}
							if (!this._request.WorkerRequest.IsSecure())
							{
								return "0";
							}
							return "1";
						}
					}
					else if (num != 1668027490U)
					{
						if (num != 1700625763U)
						{
							if (num != 1848583145U)
							{
								goto IL_08C6;
							}
							if (!(name == "CERT_SERIALNUMBER"))
							{
								goto IL_08C6;
							}
						}
						else if (!(name == "HTTP_REFERER"))
						{
							goto IL_08C6;
						}
					}
					else
					{
						if (!(name == "LOCAL_ADDR"))
						{
							goto IL_08C6;
						}
						return this._request.WorkerRequest.GetLocalAddress();
					}
				}
				else if (num <= 2139611876U)
				{
					if (num <= 1896462291U)
					{
						if (num != 1878781097U)
						{
							if (num != 1881196522U)
							{
								if (num != 1896462291U)
								{
									goto IL_08C6;
								}
								if (!(name == "AUTH_USER"))
								{
									goto IL_08C6;
								}
								IIdentity identity = this.UserIdentity;
								if (identity != null && identity.IsAuthenticated)
								{
									return identity.Name;
								}
								return string.Empty;
							}
							else if (!(name == "HTTP_CONNECTION"))
							{
								goto IL_08C6;
							}
						}
						else if (!(name == "GATEWAY_INTERFACE"))
						{
							goto IL_08C6;
						}
					}
					else if (num != 1969840958U)
					{
						if (num != 2067459782U)
						{
							if (num != 2139611876U)
							{
								goto IL_08C6;
							}
							if (!(name == "REMOTE_ADDRESS"))
							{
								goto IL_08C6;
							}
							return this._request.UserHostAddress;
						}
						else
						{
							if (!(name == "APPL_PHYSICAL_PATH"))
							{
								goto IL_08C6;
							}
							return this._request.WorkerRequest.GetAppPathTranslated();
						}
					}
					else
					{
						if (!(name == "URL"))
						{
							goto IL_08C6;
						}
						return this._request.FilePath;
					}
				}
				else if (num <= 2214154838U)
				{
					if (num != 2142861984U)
					{
						if (num != 2200705565U)
						{
							if (num != 2214154838U)
							{
								goto IL_08C6;
							}
							if (!(name == "SERVER_SOFTWARE"))
							{
								goto IL_08C6;
							}
						}
						else
						{
							if (!(name == "ALL_RAW"))
							{
								goto IL_08C6;
							}
							return this.Fill(this._request.WorkerRequest, false);
						}
					}
					else if (!(name == "CERT_SERVER_SUBJECT"))
					{
						goto IL_08C6;
					}
				}
				else if (num <= 2312497215U)
				{
					if (num != 2269661762U)
					{
						if (num != 2312497215U)
						{
							goto IL_08C6;
						}
						if (!(name == "HTTPS_KEYSIZE"))
						{
							goto IL_08C6;
						}
					}
					else if (!(name == "HTTPS"))
					{
						goto IL_08C6;
					}
				}
				else if (num != 2635623166U)
				{
					if (num != 2668667095U)
					{
						goto IL_08C6;
					}
					if (!(name == "REMOTE_ADDR"))
					{
						goto IL_08C6;
					}
					return this._request.UserHostAddress;
				}
				else if (!(name == "CERT_SECRETKEYSIZE"))
				{
					goto IL_08C6;
				}
			}
			else if (num <= 3551305322U)
			{
				if (num <= 3094011469U)
				{
					if (num <= 3001680838U)
					{
						if (num != 2809629090U)
						{
							if (num != 2952808373U)
							{
								if (num != 3001680838U)
								{
									goto IL_08C6;
								}
								if (!(name == "LOGON_USER"))
								{
									goto IL_08C6;
								}
							}
							else
							{
								if (!(name == "QUERY_STRING"))
								{
									goto IL_08C6;
								}
								return this.QueryString;
							}
						}
						else if (!(name == "HTTP_ACCEPT_ENCODING"))
						{
							goto IL_08C6;
						}
					}
					else if (num != 3022226023U)
					{
						if (num != 3060917343U)
						{
							if (num != 3094011469U)
							{
								goto IL_08C6;
							}
							if (!(name == "PATH_TRANSLATED"))
							{
								goto IL_08C6;
							}
							return this._request.PhysicalPath;
						}
						else
						{
							if (!(name == "CONTENT_TYPE"))
							{
								goto IL_08C6;
							}
							return this._request.ContentType;
						}
					}
					else if (!(name == "CERT_FLAGS"))
					{
						goto IL_08C6;
					}
				}
				else if (num <= 3316192912U)
				{
					if (num != 3200750643U)
					{
						if (num != 3233110119U)
						{
							if (num != 3316192912U)
							{
								goto IL_08C6;
							}
							if (!(name == "SERVER_PORT"))
							{
								goto IL_08C6;
							}
							return this._request.WorkerRequest.GetLocalPort().ToString();
						}
						else if (!(name == "HTTPS_SECRETKEYSIZE"))
						{
							goto IL_08C6;
						}
					}
					else
					{
						if (!(name == "SERVER_PROTOCOL"))
						{
							goto IL_08C6;
						}
						return this._request.WorkerRequest.GetHttpVersion();
					}
				}
				else if (num != 3499569683U)
				{
					if (num != 3502402942U)
					{
						if (num != 3551305322U)
						{
							goto IL_08C6;
						}
						if (!(name == "HTTPS_SERVER_ISSUER"))
						{
							goto IL_08C6;
						}
					}
					else
					{
						if (!(name == "REQUEST_METHOD"))
						{
							goto IL_08C6;
						}
						return this._request.HttpMethod;
					}
				}
				else if (!(name == "CERT_SERVER_ISSUER"))
				{
					goto IL_08C6;
				}
			}
			else if (num <= 3779608376U)
			{
				if (num <= 3702711791U)
				{
					if (num != 3641393186U)
					{
						if (num != 3681228662U)
						{
							if (num != 3702711791U)
							{
								goto IL_08C6;
							}
							if (!(name == "REMOTE_PORT"))
							{
								goto IL_08C6;
							}
							return this._request.WorkerRequest.GetRemotePort().ToString();
						}
						else
						{
							if (!(name == "REMOTE_HOST"))
							{
								goto IL_08C6;
							}
							return this._request.UserHostName;
						}
					}
					else if (!(name == "CERT_COOKIE"))
					{
						goto IL_08C6;
					}
				}
				else if (num != 3708580239U)
				{
					if (num != 3761613420U)
					{
						if (num != 3779608376U)
						{
							goto IL_08C6;
						}
						if (!(name == "AUTH_TYPE"))
						{
							goto IL_08C6;
						}
						IIdentity identity = this.UserIdentity;
						if (identity != null && identity.IsAuthenticated)
						{
							return identity.AuthenticationType;
						}
						return string.Empty;
					}
					else
					{
						if (!(name == "SCRIPT_NAME"))
						{
							goto IL_08C6;
						}
						return this._request.FilePath;
					}
				}
				else if (!(name == "HTTP_USER_AGENT"))
				{
					goto IL_08C6;
				}
			}
			else if (num <= 3975639333U)
			{
				if (num != 3794028986U)
				{
					if (num != 3871112111U)
					{
						if (num != 3975639333U)
						{
							goto IL_08C6;
						}
						if (!(name == "HTTP_ACCEPT_LANGUAGE"))
						{
							goto IL_08C6;
						}
					}
					else if (!(name == "CERT_ISSUER"))
					{
						goto IL_08C6;
					}
				}
				else if (!(name == "INSTANCE_ID"))
				{
					goto IL_08C6;
				}
			}
			else if (num <= 4246061236U)
			{
				if (num != 4067030777U)
				{
					if (num != 4246061236U)
					{
						goto IL_08C6;
					}
					if (!(name == "SERVER_NAME"))
					{
						goto IL_08C6;
					}
					return this._request.WorkerRequest.GetServerName();
				}
				else if (!(name == "AUTH_PASSWORD"))
				{
					goto IL_08C6;
				}
			}
			else if (num != 4252582225U)
			{
				if (num != 4282377502U)
				{
					goto IL_08C6;
				}
				if (!(name == "HTTP_ACCEPT"))
				{
					goto IL_08C6;
				}
			}
			else
			{
				if (!(name == "ALL_HTTP"))
				{
					goto IL_08C6;
				}
				return this.Fill(this._request.WorkerRequest, true);
			}
			return this._request.WorkerRequest.GetServerVariable(name);
			IL_08C6:
			return null;
		}

		// Token: 0x04001092 RID: 4242
		private bool loaded;
	}
}
