using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;
using Unity;

namespace System.Web.Security
{
	/// <summary>Manages forms-authentication services for Web applications. This class cannot be inherited.</summary>
	// Token: 0x020004BE RID: 1214
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FormsAuthentication
	{
		/// <summary>Gets the amount of time before an authentication ticket expires.</summary>
		/// <returns>The amount of time before an authentication ticket expires.</returns>
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x0008EADC File Offset: 0x0008CCDC
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x0008EAE3 File Offset: 0x0008CCE3
		public static TimeSpan Timeout { get; private set; }

		/// <summary>Gets a value that indicates whether forms authentication is enabled.</summary>
		/// <returns>true if forms authentication is enabled; otherwise, false.</returns>
		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x0008EAEB File Offset: 0x0008CCEB
		public static bool IsEnabled
		{
			get
			{
				return FormsAuthentication.initialized;
			}
		}

		/// <summary>Enables forms authentication.</summary>
		/// <param name="configurationData">A name-value collection that contains values for "defaultUrl" and/or "loginUrl". The parameter can be null if there are no values for the default URL or the login URL. </param>
		/// <exception cref="T:System.InvalidOperationException">The application is not in the pre-start initialization phase.</exception>
		// Token: 0x0600368C RID: 13964 RVA: 0x0008EAF4 File Offset: 0x0008CCF4
		public static void EnableFormsAuthentication(NameValueCollection configurationData)
		{
			BuildManager.AssertPreStartMethodsRunning();
			if (configurationData == null || configurationData.Count == 0)
			{
				return;
			}
			string text = configurationData["loginUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				FormsAuthentication.login_url = text;
			}
			text = configurationData["defaultUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				FormsAuthentication.default_url = text;
			}
		}

		/// <summary>Validates a user name and password against credentials stored in the configuration file for an application.</summary>
		/// <returns>true if the user name and password are valid; otherwise, false.</returns>
		/// <param name="name">The user name.</param>
		/// <param name="password">The password for the user. </param>
		// Token: 0x0600368E RID: 13966 RVA: 0x0008EB48 File Offset: 0x0008CD48
		public static bool Authenticate(string name, string password)
		{
			if (name == null || password == null)
			{
				return false;
			}
			FormsAuthentication.Initialize();
			if (HttpContext.Current == null)
			{
				throw new HttpException("Context is null!");
			}
			name = name.ToLower(Helpers.InvariantCulture);
			FormsAuthenticationCredentials credentials = ((AuthenticationSection)WebConfigurationManager.GetSection(FormsAuthentication.authConfigPath)).Forms.Credentials;
			FormsAuthenticationUser formsAuthenticationUser = credentials.Users[name];
			string text = null;
			if (formsAuthenticationUser != null)
			{
				text = formsAuthenticationUser.Password;
			}
			if (text == null)
			{
				return false;
			}
			bool flag = true;
			switch (credentials.PasswordFormat)
			{
			case FormsAuthPasswordFormat.Clear:
				flag = false;
				break;
			case FormsAuthPasswordFormat.SHA1:
				password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
				break;
			case FormsAuthPasswordFormat.MD5:
				password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
				break;
			}
			return string.Compare(password, text, flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x0008EC04 File Offset: 0x0008CE04
		private static FormsAuthenticationTicket Decrypt2(byte[] bytes)
		{
			if (FormsAuthentication.protection == FormsProtectionEnum.None)
			{
				return FormsAuthenticationTicket.FromByteArray(bytes);
			}
			MachineKeySection machineKeySection = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(FormsAuthentication.machineKeyConfigPath);
			byte[] array = null;
			if (FormsAuthentication.protection == FormsProtectionEnum.All)
			{
				array = MachineKeySectionUtils.VerifyDecrypt(machineKeySection, bytes);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Encryption)
			{
				array = MachineKeySectionUtils.Decrypt(machineKeySection, bytes);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Validation)
			{
				array = MachineKeySectionUtils.Verify(machineKeySection, bytes);
			}
			return FormsAuthenticationTicket.FromByteArray(array);
		}

		/// <summary>Creates a <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object based on the encrypted forms-authentication ticket passed to the method.</summary>
		/// <returns>A <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object. If the <paramref name="encryptedTicket" /> parameter is not a valid ticket, null is returned.</returns>
		/// <param name="encryptedTicket">The encrypted authentication ticket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="encryptedTicket" /> is null.- or -<paramref name="encryptedTicket" /> is an empty string ("").- or -The length of <paramref name="encryptedTicket" /> is greater than 4096 characters.- or -<paramref name="encryptedTicket" /> is of an invalid format. </exception>
		// Token: 0x06003690 RID: 13968 RVA: 0x0008EC6C File Offset: 0x0008CE6C
		public static FormsAuthenticationTicket Decrypt(string encryptedTicket)
		{
			if (string.IsNullOrEmpty(encryptedTicket))
			{
				throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
			}
			FormsAuthentication.Initialize();
			byte[] array = Convert.FromBase64String(encryptedTicket);
			FormsAuthenticationTicket formsAuthenticationTicket;
			try
			{
				formsAuthenticationTicket = FormsAuthentication.Decrypt2(array);
			}
			catch (Exception)
			{
				formsAuthenticationTicket = null;
			}
			return formsAuthenticationTicket;
		}

		/// <summary>Creates a string containing an encrypted forms-authentication ticket suitable for use in an HTTP cookie.</summary>
		/// <returns>A string containing an encrypted forms-authentication ticket.</returns>
		/// <param name="ticket">The <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object with which to create the encrypted forms-authentication ticket. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ticket" /> is null.</exception>
		// Token: 0x06003691 RID: 13969 RVA: 0x0008ECBC File Offset: 0x0008CEBC
		public static string Encrypt(FormsAuthenticationTicket ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			FormsAuthentication.Initialize();
			byte[] array = ticket.ToByteArray();
			if (FormsAuthentication.protection == FormsProtectionEnum.None)
			{
				return Convert.ToBase64String(array);
			}
			byte[] array2 = null;
			MachineKeySection machineKeySection = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(FormsAuthentication.machineKeyConfigPath);
			if (FormsAuthentication.protection == FormsProtectionEnum.All)
			{
				array2 = MachineKeySectionUtils.EncryptSign(machineKeySection, array);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Encryption)
			{
				array2 = MachineKeySectionUtils.Encrypt(machineKeySection, array);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Validation)
			{
				array2 = MachineKeySectionUtils.Sign(machineKeySection, array);
			}
			return Convert.ToBase64String(array2);
		}

		/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response, so that an application can have more control over how the cookie is issued.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information. The default value for the <see cref="P:System.Web.Security.FormsAuthentication.FormsCookiePath" /> property is used.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		// Token: 0x06003692 RID: 13970 RVA: 0x0008ED3D File Offset: 0x0008CF3D
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie)
		{
			return FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, null);
		}

		/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The <see cref="P:System.Web.HttpCookie.Path" /> of the authentication cookie. </param>
		// Token: 0x06003693 RID: 13971 RVA: 0x0008ED48 File Offset: 0x0008CF48
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			FormsAuthentication.Initialize();
			if (userName == null)
			{
				userName = string.Empty;
			}
			if (strCookiePath == null || strCookiePath.Length == 0)
			{
				strCookiePath = FormsAuthentication.cookiePath;
			}
			DateTime now = DateTime.Now;
			DateTime dateTime = now.AddMinutes((double)FormsAuthentication.timeout);
			DateTime dateTime2 = (createPersistentCookie ? dateTime : DateTime.MinValue);
			FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, userName, now, dateTime, createPersistentCookie, string.Empty, FormsAuthentication.cookiePath);
			HttpCookie httpCookie = new HttpCookie(FormsAuthentication.cookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket), strCookiePath, dateTime2);
			if (FormsAuthentication.requireSSL)
			{
				httpCookie.Secure = true;
			}
			if (!string.IsNullOrEmpty(FormsAuthentication.cookie_domain))
			{
				httpCookie.Domain = FormsAuthentication.cookie_domain;
			}
			return httpCookie;
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x0008EDE8 File Offset: 0x0008CFE8
		internal static string ReturnUrl
		{
			get
			{
				return HttpContext.Current.Request["RETURNURL"];
			}
		}

		/// <summary>Returns the redirect URL for the original request that caused the redirect to the login page.</summary>
		/// <returns>A string that contains the redirect URL.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">This parameter is ignored.</param>
		// Token: 0x06003695 RID: 13973 RVA: 0x0008EE00 File Offset: 0x0008D000
		public static string GetRedirectUrl(string userName, bool createPersistentCookie)
		{
			if (userName == null)
			{
				return null;
			}
			FormsAuthentication.Initialize();
			HttpRequest request = HttpContext.Current.Request;
			string text = FormsAuthentication.ReturnUrl;
			if (text != null)
			{
				return text;
			}
			text = request.ApplicationPath;
			string physicalApplicationPath = request.PhysicalApplicationPath;
			bool flag = false;
			foreach (string text2 in FormsAuthentication.indexFiles)
			{
				if (File.Exists(Path.Combine(physicalApplicationPath, text2)))
				{
					text = UrlUtils.Combine(text, text2);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				text = UrlUtils.Combine(text, "index.aspx");
			}
			return text;
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x0008EE8C File Offset: 0x0008D08C
		private static string HashPasswordForStoringInConfigFile(string password, FormsAuthPasswordFormat passwordFormat)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			byte[] array;
			if (passwordFormat != FormsAuthPasswordFormat.SHA1)
			{
				if (passwordFormat != FormsAuthPasswordFormat.MD5)
				{
					throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat");
				}
				array = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
			}
			else
			{
				array = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
			}
			return MachineKeySectionUtils.GetHexString(array);
		}

		/// <summary>Produces a hash password suitable for storing in a configuration file based on the specified password and hash algorithm.</summary>
		/// <returns>The hashed password.</returns>
		/// <param name="password">The password to hash. </param>
		/// <param name="passwordFormat">The hash algorithm to use. <paramref name="passwordFormat" /> is a String that represents one of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="password" /> is null-or-<paramref name="passwordFormat" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="passwordFormat" /> is not a valid <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> value.</exception>
		// Token: 0x06003697 RID: 13975 RVA: 0x0008EEF8 File Offset: 0x0008D0F8
		public static string HashPasswordForStoringInConfigFile(string password, string passwordFormat)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (passwordFormat == null)
			{
				throw new ArgumentNullException("passwordFormat");
			}
			if (string.Compare(passwordFormat, "MD5", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
			}
			if (string.Compare(passwordFormat, "SHA1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
			}
			throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat");
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthentication" /> object based on the configuration settings for the application.</summary>
		// Token: 0x06003698 RID: 13976 RVA: 0x0008EF5C File Offset: 0x0008D15C
		public static void Initialize()
		{
			if (FormsAuthentication.initialized)
			{
				return;
			}
			object obj = FormsAuthentication.locker;
			lock (obj)
			{
				if (!FormsAuthentication.initialized)
				{
					FormsAuthenticationConfiguration forms = ((AuthenticationSection)WebConfigurationManager.GetSection(FormsAuthentication.authConfigPath)).Forms;
					FormsAuthentication.cookieName = forms.Name;
					FormsAuthentication.Timeout = forms.Timeout;
					FormsAuthentication.timeout = (int)forms.Timeout.TotalMinutes;
					FormsAuthentication.cookiePath = forms.Path;
					FormsAuthentication.protection = forms.Protection;
					FormsAuthentication.requireSSL = forms.RequireSSL;
					FormsAuthentication.slidingExpiration = forms.SlidingExpiration;
					FormsAuthentication.cookie_domain = forms.Domain;
					FormsAuthentication.cookie_mode = forms.Cookieless;
					FormsAuthentication.cookies_supported = true;
					if (!string.IsNullOrEmpty(FormsAuthentication.default_url))
					{
						FormsAuthentication.default_url = FormsAuthentication.MapUrl(FormsAuthentication.default_url);
					}
					else
					{
						FormsAuthentication.default_url = FormsAuthentication.MapUrl(forms.DefaultUrl);
					}
					FormsAuthentication.enable_crossapp_redirects = forms.EnableCrossAppRedirects;
					if (!string.IsNullOrEmpty(FormsAuthentication.login_url))
					{
						FormsAuthentication.login_url = FormsAuthentication.MapUrl(FormsAuthentication.login_url);
					}
					else
					{
						FormsAuthentication.login_url = FormsAuthentication.MapUrl(forms.LoginUrl);
					}
					FormsAuthentication.initialized = true;
				}
			}
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0008F0A8 File Offset: 0x0008D2A8
		private static string MapUrl(string url)
		{
			if (UrlUtils.IsRelativeUrl(url))
			{
				return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
			}
			return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
		}

		/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL.</summary>
		/// <param name="userName">The authenticated user name. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
		// Token: 0x0600369A RID: 13978 RVA: 0x0008F0C4 File Offset: 0x0008D2C4
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.RedirectFromLoginPage(userName, createPersistentCookie, null);
		}

		/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL using the specified cookie path for the forms-authentication cookie.</summary>
		/// <param name="userName">The authenticated user name. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The cookie path for the forms-authentication ticket. </param>
		/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
		// Token: 0x0600369B RID: 13979 RVA: 0x0008F0CE File Offset: 0x0008D2CE
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie, string strCookiePath)
		{
			if (userName == null)
			{
				return;
			}
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, strCookiePath);
			FormsAuthentication.Redirect(FormsAuthentication.GetRedirectUrl(userName, createPersistentCookie), false);
		}

		/// <summary>Conditionally updates the issue date and time and expiration date and time for a <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</summary>
		/// <returns>The updated <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</returns>
		/// <param name="tOld">The forms-authentication ticket to update.</param>
		// Token: 0x0600369C RID: 13980 RVA: 0x0008F0F0 File Offset: 0x0008D2F0
		public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
		{
			if (tOld == null)
			{
				return null;
			}
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - tOld.IssueDate;
			if (tOld.Expiration - now > timeSpan)
			{
				return tOld;
			}
			FormsAuthenticationTicket formsAuthenticationTicket = tOld.Clone();
			formsAuthenticationTicket.SetDates(now, now + (tOld.Expiration - tOld.IssueDate));
			return formsAuthenticationTicket;
		}

		/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, or to the URL if you are using cookieless authentication.</summary>
		/// <param name="userName">The name of an authenticated user. This does not have to map to a Windows account. </param>
		/// <param name="createPersistentCookie">true to create a persistent cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is true and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is false.</exception>
		// Token: 0x0600369D RID: 13981 RVA: 0x0008F14F File Offset: 0x0008D34F
		public static void SetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, FormsAuthentication.cookiePath);
		}

		/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, using the supplied cookie path, or using the URL if you are using cookieless authentication.</summary>
		/// <param name="userName">The name of an authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The cookie path for the forms-authentication ticket.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is true and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is false.</exception>
		// Token: 0x0600369E RID: 13982 RVA: 0x0008F162 File Offset: 0x0008D362
		public static void SetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new HttpException("Context is null!");
			}
			HttpResponse response = httpContext.Response;
			if (response == null)
			{
				throw new HttpException("Response is null!");
			}
			response.Cookies.Add(FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, strCookiePath));
		}

		/// <summary>Removes the forms-authentication ticket from the browser.</summary>
		// Token: 0x0600369F RID: 13983 RVA: 0x0008F19C File Offset: 0x0008D39C
		public static void SignOut()
		{
			FormsAuthentication.Initialize();
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new HttpException("Context is null!");
			}
			HttpResponse response = httpContext.Response;
			if (response == null)
			{
				throw new HttpException("Response is null!");
			}
			HttpCookieCollection cookies = response.Cookies;
			cookies.Remove(FormsAuthentication.cookieName);
			HttpCookie httpCookie = new HttpCookie(FormsAuthentication.cookieName, string.Empty);
			httpCookie.Expires = new DateTime(1999, 10, 12);
			httpCookie.Path = FormsAuthentication.cookiePath;
			if (!string.IsNullOrEmpty(FormsAuthentication.cookie_domain))
			{
				httpCookie.Domain = FormsAuthentication.cookie_domain;
			}
			cookies.Add(httpCookie);
			Roles.DeleteCookie();
		}

		/// <summary>Gets the name of the cookie used to store the forms-authentication ticket.</summary>
		/// <returns>The name of the cookie used to store the forms-authentication ticket. The default is ".ASPXAUTH".</returns>
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x0008F235 File Offset: 0x0008D435
		public static string FormsCookieName
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookieName;
			}
		}

		/// <summary>Gets the path for the forms-authentication cookie.</summary>
		/// <returns>The path of the cookie where the forms-authentication ticket information is stored. The default is "/".</returns>
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x0008F241 File Offset: 0x0008D441
		public static string FormsCookiePath
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookiePath;
			}
		}

		/// <summary>Gets a value indicating whether the forms-authentication cookie requires SSL in order to be returned to the server.</summary>
		/// <returns>true if SSL is required to return the forms-authentication cookie to the server; otherwise, false. The default is false.</returns>
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x0008F24D File Offset: 0x0008D44D
		public static bool RequireSSL
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.requireSSL;
			}
		}

		/// <summary>Gets a value indicating whether sliding expiration is enabled.</summary>
		/// <returns>true if sliding expiration is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x0008F259 File Offset: 0x0008D459
		public static bool SlidingExpiration
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.slidingExpiration;
			}
		}

		/// <summary>Gets the value of the domain of the forms-authentication cookie.</summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the forms-authentication cookie. The default is an empty string ("").</returns>
		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x0008F265 File Offset: 0x0008D465
		public static string CookieDomain
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookie_domain;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless forms authentication.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless forms authentication. The default is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x0008F271 File Offset: 0x0008D471
		public static HttpCookieMode CookieMode
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookie_mode;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured to support cookieless forms authentication.</summary>
		/// <returns>false if the application is configured to support cookieless forms authentication; otherwise, true.</returns>
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x0008F27D File Offset: 0x0008D47D
		public static bool CookiesSupported
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookies_supported;
			}
		}

		/// <summary>Gets the URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified.</summary>
		/// <returns>The URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified. The default is "default.aspx."</returns>
		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x0008F289 File Offset: 0x0008D489
		public static string DefaultUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.default_url;
			}
		}

		/// <summary>Gets a value indicating whether authenticated users can be redirected to URLs in other Web applications.</summary>
		/// <returns>true if authenticated users can be redirected to URLs in other Web applications; otherwise, false. The default is false.</returns>
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x0008F295 File Offset: 0x0008D495
		public static bool EnableCrossAppRedirects
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.enable_crossapp_redirects;
			}
		}

		/// <summary>Gets the URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to.</summary>
		/// <returns>The URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to. The default is "login.aspx."</returns>
		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x0008F2A1 File Offset: 0x0008D4A1
		public static string LoginUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.login_url;
			}
		}

		/// <summary>Redirects the browser to the login URL.</summary>
		// Token: 0x060036AA RID: 13994 RVA: 0x0008F2AD File Offset: 0x0008D4AD
		public static void RedirectToLoginPage()
		{
			FormsAuthentication.Redirect(FormsAuthentication.LoginUrl);
		}

		/// <summary>Redirects the browser to the login URL with the specified query string.</summary>
		/// <param name="extraQueryString">The query string to include with the redirect URL.</param>
		// Token: 0x060036AB RID: 13995 RVA: 0x0008F2B9 File Offset: 0x0008D4B9
		[global::System.MonoTODO("needs more tests")]
		public static void RedirectToLoginPage(string extraQueryString)
		{
			FormsAuthentication.Redirect(FormsAuthentication.LoginUrl + "?" + extraQueryString);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x0008F2D0 File Offset: 0x0008D4D0
		private static void Redirect(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x0008F2E2 File Offset: 0x0008D4E2
		private static void Redirect(string url, bool end)
		{
			HttpContext.Current.Response.Redirect(url, end);
		}

		/// <summary>Gets a value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</summary>
		/// <returns>A value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</returns>
		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x0008F358 File Offset: 0x0008D558
		public static TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TicketCompatibilityMode.Framework20;
			}
		}

		// Token: 0x04001DBC RID: 7612
		private static string authConfigPath = "system.web/authentication";

		// Token: 0x04001DBD RID: 7613
		private static string machineKeyConfigPath = "system.web/machineKey";

		// Token: 0x04001DBE RID: 7614
		private static object locker = new object();

		// Token: 0x04001DBF RID: 7615
		private static bool initialized;

		// Token: 0x04001DC0 RID: 7616
		private static string cookieName;

		// Token: 0x04001DC1 RID: 7617
		private static string cookiePath;

		// Token: 0x04001DC2 RID: 7618
		private static int timeout;

		// Token: 0x04001DC3 RID: 7619
		private static FormsProtectionEnum protection;

		// Token: 0x04001DC4 RID: 7620
		private static bool requireSSL;

		// Token: 0x04001DC5 RID: 7621
		private static bool slidingExpiration;

		// Token: 0x04001DC6 RID: 7622
		private static string cookie_domain;

		// Token: 0x04001DC7 RID: 7623
		private static HttpCookieMode cookie_mode;

		// Token: 0x04001DC8 RID: 7624
		private static bool cookies_supported;

		// Token: 0x04001DC9 RID: 7625
		private static string default_url;

		// Token: 0x04001DCA RID: 7626
		private static bool enable_crossapp_redirects;

		// Token: 0x04001DCB RID: 7627
		private static string login_url;

		// Token: 0x04001DCC RID: 7628
		private static string[] indexFiles = new string[] { "index.aspx", "Default.aspx", "default.aspx", "index.html", "index.htm" };
	}
}
