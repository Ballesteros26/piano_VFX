using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>Provides a class to be used by <see cref="T:System.Web.Security.PassportAuthenticationModule" />. It provides a way for an application to access the <see cref="M:System.Web.Security.PassportIdentity.Ticket(System.String)" /> method. This class cannot be inherited. This class is deprecated.</summary>
	// Token: 0x020004C8 RID: 1224
	[global::System.MonoTODO("Not implemented")]
	[global::System.MonoNotSupported("")]
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PassportIdentity : IIdentity, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportIdentity" /> class. This class is deprecated.</summary>
		// Token: 0x0600371D RID: 14109 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PassportIdentity()
		{
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0009056C File Offset: 0x0008E76C
		~PassportIdentity()
		{
		}

		/// <summary>Returns a string containing the Login server URL for a member, as well as with optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as optional information sent to the Login server in the query string.</returns>
		// Token: 0x0600371F RID: 14111 RVA: 0x00090594 File Offset: 0x0008E794
		public string AuthUrl()
		{
			return this.AuthUrl(null, -1, -1, null, -1, null, -1, -1);
		}

		/// <summary>Returns a string containing the Login server URL for a member, along with optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">The URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003720 RID: 14112 RVA: 0x000905B0 File Offset: 0x0008E7B0
		public string AuthUrl(string strReturnUrl)
		{
			return this.AuthUrl(strReturnUrl, -1, -1, null, -1, null, -1, -1);
		}

		/// <summary>Returns the authentication server URL for a member. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003721 RID: 14113 RVA: 0x000905CC File Offset: 0x0008E7CC
		public string AuthUrl(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.AuthUrl(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
		}

		/// <summary>Returns a string containing the Login server URL for a member, along with the optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">Declares whether the actual Login UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003722 RID: 14114 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string AuthUrl(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string containing the Login server URL for a member, as well as optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as optional information sent to the Login server in the query string.</returns>
		// Token: 0x06003723 RID: 14115 RVA: 0x000905F8 File Offset: 0x0008E7F8
		public string AuthUrl2()
		{
			return this.AuthUrl2(null, -1, -1, null, -1, null, -1, -1);
		}

		/// <summary>Returns a string containing the Login server URL for a member, as well as optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">The URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003724 RID: 14116 RVA: 0x00090614 File Offset: 0x0008E814
		public string AuthUrl2(string strReturnUrl)
		{
			return this.AuthUrl2(strReturnUrl, -1, -1, null, -1, null, -1, -1);
		}

		/// <summary>Returns a string containing the Login server URL for a member, as well as the optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003725 RID: 14117 RVA: 0x00090630 File Offset: 0x0008E830
		public string AuthUrl2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.AuthUrl2(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
		}

		/// <summary>Retrieves a string containing the Login server URL for a member, as well as the optional information sent to the Login server in the query string. This class is deprecated.</summary>
		/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003726 RID: 14118 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string AuthUrl2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			throw new NotImplementedException();
		}

		/// <summary>Compresses data. This class is deprecated.</summary>
		/// <returns>The compressed data.</returns>
		/// <param name="strData">The data to be compressed. </param>
		// Token: 0x06003727 RID: 14119 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static string Compress(string strData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the state of a flag indicating if the Passport Manager is in a valid state for encryption. This class is deprecated.</summary>
		/// <returns>true if the key used for encryption and decryption is valid and if the Passport Manager is in a valid state for encryption; otherwise, false.</returns>
		// Token: 0x06003728 RID: 14120 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static bool CryptIsValid()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the key being used for Passport encryption and decryption. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		/// <param name="strHost">The host name or IP address. </param>
		// Token: 0x06003729 RID: 14121 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static int CryptPutHost(string strHost)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the key being used for Passport encryption and decryption by referring to the site-name label assigned to that key when the key was first installed. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		/// <param name="strSite">The site label. </param>
		// Token: 0x0600372A RID: 14122 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static int CryptPutSite(string strSite)
		{
			throw new NotImplementedException();
		}

		/// <summary>Decompresses data that has been compressed by the <see cref="M:System.Web.Security.PassportIdentity.Compress(System.String)" /> method. This class is deprecated.</summary>
		/// <returns>The decompressed data.</returns>
		/// <param name="strData">The data to be decompressed. </param>
		// Token: 0x0600372B RID: 14123 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static string Decompress(string strData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Decrypts data using the Passport participant key for the current site. This class is deprecated.</summary>
		/// <returns>Data decrypted using the Passport participant key for the current site.</returns>
		/// <param name="strData">The data to be decrypted. </param>
		// Token: 0x0600372C RID: 14124 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static string Decrypt(string strData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Encrypts data using the Passport participant key for the current site. This class is deprecated.</summary>
		/// <returns>Data encrypted using the Passport participant key for the current site.</returns>
		/// <param name="strData">The data to be encrypted. </param>
		// Token: 0x0600372D RID: 14125 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static string Encrypt(string strData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the contents of a registry key under the HKLM\SW\Microsoft\Passport hive. This class is deprecated.</summary>
		/// <returns>The contents of the registry key.</returns>
		/// <param name="strAttribute">The name of the registry key. </param>
		// Token: 0x0600372E RID: 14126 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object GetCurrentConfig(string strAttribute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides information for a Passport domain by querying the Passport manager for the requested domain attribute. This class is deprecated.</summary>
		/// <returns>A string representing the requested attribute.</returns>
		/// <param name="strAttribute">The name of the attribute value to retrieve. </param>
		/// <param name="iLCID">The language in which various Passport network pages should be displayed to the member. </param>
		/// <param name="strDomain">The domain authority name to query for an attribute. </param>
		// Token: 0x0600372F RID: 14127 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string GetDomainAttribute(string strAttribute, int iLCID, string strDomain)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the Passport domain from the member name string. This class is deprecated.</summary>
		/// <returns>The Passport domain for the specified member.</returns>
		/// <param name="strMemberName">The name of the Passport member </param>
		// Token: 0x06003730 RID: 14128 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string GetDomainFromMemberName(string strMemberName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether the user is authenticated by a central site responsible for Passport authentication. This class is deprecated.</summary>
		/// <returns>true if the user is authenticated by a Passport authority; otherwise, false.</returns>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on to the calling domain. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="bForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter is used. </param>
		/// <param name="bCheckSecure">Enables checking for an encrypted logon. SSL sign-in is not available as an option in the current version Login servers, so the value passed in is ignored at the server. </param>
		// Token: 0x06003731 RID: 14129 RVA: 0x0009065C File Offset: 0x0008E85C
		public bool GetIsAuthenticated(int iTimeWindow, bool bForceLogin, bool bCheckSecure)
		{
			return this.GetIsAuthenticated(iTimeWindow, bForceLogin ? 1 : 0, bCheckSecure ? 1 : 0);
		}

		/// <summary>Indicates whether the user is authenticated by a Passport authority. This class is deprecated.</summary>
		/// <returns>true if the user is authenticated by a central site responsible for Passport authentication; otherwise, false.</returns>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on to the calling domain. A value of -1 indicates that Passport should use the default value, 0 represents false, and 1 represents true. </param>
		/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter is used. A value of -1 indicates that Passport should use the default value, 0 represents false, and 1 represents true. </param>
		/// <param name="iCheckSecure">Enables checking for an encrypted logon. A value of -1 indicates that Passport should use the default value, 0 represents false, and 1 represents true.A value of 10 or 100 for Passport version 2.1 Login servers specify SecureLevel 10 or 100 for the Passport Manager IsAuthenticated method. See the Passport version 2.1 SDK documentation for more information.SSL sign-in is not available as an option for Passport version 1.4 Login servers. The value of <paramref name="iCheckSecure" /> is ignored at the server. </param>
		// Token: 0x06003732 RID: 14130 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool GetIsAuthenticated(int iTimeWindow, int iForceLogin, int iCheckSecure)
		{
			throw new NotImplementedException();
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>A string representing the Passport Login Challenge.</returns>
		// Token: 0x06003733 RID: 14131 RVA: 0x00090674 File Offset: 0x0008E874
		public string GetLoginChallenge()
		{
			return this.GetLoginChallenge(null, -1, -1, null, -1, null, -1, -1, null);
		}

		/// <summary>Logs the user on by outputting the appropriate headers to either a 302 redirect URL or the initiation of a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>A string representing the Passport Login Challenge.</returns>
		/// <param name="strReturnUrl">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003734 RID: 14132 RVA: 0x00090690 File Offset: 0x0008E890
		public string GetLoginChallenge(string strReturnUrl)
		{
			return this.GetLoginChallenge(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>A string representing the Passport Login Challenge.</returns>
		/// <param name="szRetURL">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="szCOBrandArgs">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="oExtraParams">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003735 RID: 14133 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string GetLoginChallenge(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a specific Passport logon option. This class is deprecated.</summary>
		/// <returns>The Passport logon option <paramref name="strOpt" />.</returns>
		/// <param name="strOpt">Logon option to query. </param>
		// Token: 0x06003736 RID: 14134 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object GetOption(string strOpt)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns Passport profile information for the specified profile attribute. This class is deprecated.</summary>
		/// <returns>The value of the Passport profile attribute specified by the <paramref name="strProfileName" /> parameter.</returns>
		/// <param name="strProfileName">The Passport profile attribute to return. </param>
		// Token: 0x06003737 RID: 14135 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object GetProfileObject(string strProfileName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a given flag is set in this user's profile. This class is deprecated.</summary>
		/// <returns>true if the Passport profile flag <paramref name="iFlagMask" /> is set in this user's profile; otherwise, false.</returns>
		/// <param name="iFlagMask">The Passport profile flag to query. </param>
		// Token: 0x06003738 RID: 14136 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool HasFlag(int iFlagMask)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a given profile attribute exists in this user's profile. This class is deprecated.</summary>
		/// <returns>true if the profile attribute <paramref name="strProfile" /> exists in this user's profile; otherwise, false.</returns>
		/// <param name="strProfile">The Passport profile attribute to query. </param>
		// Token: 0x06003739 RID: 14137 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool HasProfile(string strProfile)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether full consent is granted in this user's profile. This class is deprecated.</summary>
		/// <returns>true if full consent is granted in this user's profile.</returns>
		/// <param name="bNeedFullConsent">true to indicate full consent is required for Passport Authentication; otherwise, false. </param>
		/// <param name="bNeedBirthdate">true to indicate the user's birthdate is required for Passport Authentication; otherwise, false. </param>
		// Token: 0x0600373A RID: 14138 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool HaveConsent(bool bNeedFullConsent, bool bNeedBirthdate)
		{
			throw new NotImplementedException();
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		// Token: 0x0600373B RID: 14139 RVA: 0x000906AC File Offset: 0x0008E8AC
		public int LoginUser()
		{
			return this.LoginUser(null, -1, -1, null, -1, null, -1, -1, null);
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		/// <param name="strReturnUrl">The URL to which the Login server should redirect users after sign in is complete. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x0600373C RID: 14140 RVA: 0x000906C8 File Offset: 0x0008E8C8
		public int LoginUser(string strReturnUrl)
		{
			return this.LoginUser(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or by initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		/// <param name="szRetURL">The URL to which the Login server should redirect users after sign in is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">The time value, in seconds. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">true to have the Login server compare the <paramref name="iTimeWindow" /> parameter against the time since the user last signed in; false to have the Login server compare <paramref name="iTimeWindow" /> against the last time the Ticket was refreshed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="szCOBrandArgs">A string specifying variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">A locale identifier (LCID) specifying the language in which the Login page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">A domain name space to which you want to direct users without Passports to register. The specified name space must appear as a "domain name" entry in the Partner.xml Component Configuration Document (CCD). The typical default name space is "passport.com". Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Pass -1 to indicate that Passport should use the default value. This parameter is only relevant when implementing Kids Passport service; however, Kids Passport service cannot currently support use of this method. </param>
		/// <param name="fUseSecureAuth">SSL sign-in is not available as an option in the current version Login servers. Passport Manager methods include SSL sign-in parameters and they may be required for syntax, but they are currently ignored at the server. Check the Passport Web site for updates on the status of SSL sign-in. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="oExtraParams">Name-value pairs to be inserted directly into the challenge authentication header, specifically for Passport-aware authentication interaction. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x0600373D RID: 14141 RVA: 0x000906E4 File Offset: 0x0008E8E4
		public int LoginUser(string szRetURL, int iTimeWindow, bool fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, bool fUseSecureAuth, object oExtraParams)
		{
			return this.LoginUser(szRetURL, iTimeWindow, fForceLogin ? 1 : 0, szCOBrandArgs, iLangID, strNameSpace, iKPP, fUseSecureAuth ? 1 : 0, null);
		}

		/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
		/// <returns>An integer result code.</returns>
		/// <param name="szRetURL">The URL to which the Login server should redirect users after sign in is complete. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">The time value, in seconds. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">Indicates whether the Login server should compare the <paramref name="iTimeWindow" /> parameter against the time since the user last signed in or against the last time the Ticket was refreshed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="szCOBrandArgs">A string specifying variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">A locale identifier (LCID) specifying the language in which the Login page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">A domain name space to which you want to direct users without Passports to register. The specified name space must appear as a "domain name" entry in the Partner.xml Component Configuration Document (CCD). The typical default name space is "passport.com". Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Pass -1 to indicate that Passport should use the default value. This parameter is only relevant when implementing Kids Passport service; however, Kids Passport service cannot currently support use of this method. </param>
		/// <param name="iUseSecureAuth">SSL sign-in is not available as an option in the current version Login servers. Passport Manager methods include SSL sign-in parameters and they may be required for syntax, but they are currently ignored at the server. Check the Passport Web site for updates on the status of SSL sign-in. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="oExtraParams">Name-value pairs to be inserted directly into the challenge authentication header, specifically for Passport-aware authentication interaction. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x0600373E RID: 14142 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int LoginUser(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an HTML fragment containing an image tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		// Token: 0x0600373F RID: 14143 RVA: 0x00090714 File Offset: 0x0008E914
		public string LogoTag()
		{
			return this.LogoTag(null, -1, -1, null, -1, -1, null, -1, -1);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003740 RID: 14144 RVA: 0x00090730 File Offset: 0x0008E930
		public string LogoTag(string strReturnUrl)
		{
			return this.LogoTag(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003741 RID: 14145 RVA: 0x0009074C File Offset: 0x0008E94C
		public string LogoTag(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.LogoTag(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003742 RID: 14146 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string LogoTag(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an HTML fragment containing an image tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		// Token: 0x06003743 RID: 14147 RVA: 0x00090780 File Offset: 0x0008E980
		public string LogoTag2()
		{
			return this.LogoTag2(null, -1, -1, null, -1, -1, null, -1, -1);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		// Token: 0x06003744 RID: 14148 RVA: 0x0009079C File Offset: 0x0008E99C
		public string LogoTag2(string strReturnUrl)
		{
			return this.LogoTag2(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="fSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003745 RID: 14149 RVA: 0x000907B8 File Offset: 0x0008E9B8
		public string LogoTag2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.LogoTag2(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
		}

		/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
		/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
		/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003746 RID: 14150 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string LogoTag2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the Passport logout URL string. This class is deprecated.</summary>
		/// <returns>The Passport logout URL string.</returns>
		// Token: 0x06003747 RID: 14151 RVA: 0x000907EC File Offset: 0x0008E9EC
		public string LogoutURL()
		{
			return this.LogoutURL(null, null, -1, null, -1);
		}

		/// <summary>Returns the Passport logout URL string using the specified parameters. This class is deprecated.</summary>
		/// <returns>The Passport logout URL string.</returns>
		/// <param name="szReturnURL">See IPassportManager3.LogoutUrl for more details. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="szCOBrandArgs">See IPassportManager3.LogoutUrl for more details. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iLangID">See IPassportManager3.LogoutUrl for more details. Pass -1 to indicate that Passport should use the default value. </param>
		/// <param name="strDomain">See IPassportManager3.LogoutUrl for more details. Pass null to indicate that Passport should use the default value. </param>
		/// <param name="iUseSecureAuth">See IPassportManager3.LogoutUrl for more details. Pass -1 to indicate that Passport should use the default value. </param>
		// Token: 0x06003748 RID: 14152 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string LogoutURL(string szReturnURL, string szCOBrandArgs, int iLangID, string strDomain, int iUseSecureAuth)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a specific Passport logon option. This class is deprecated.</summary>
		/// <param name="strOpt">The option to set. </param>
		/// <param name="vOpt">The value to set. </param>
		// Token: 0x06003749 RID: 14153 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void SetOption(string strOpt, object vOpt)
		{
			throw new NotImplementedException();
		}

		/// <summary>Logs off the given Passport member from the current session. This class is deprecated.</summary>
		/// <param name="strSignOutDotGifFileName">An HTML fragment containing an image for the user to click on to sign out. </param>
		// Token: 0x0600374A RID: 14154 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static void SignOut(string strSignOutDotGifFileName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets information on a specific attribute of the Passport authentication ticket. This class is deprecated.</summary>
		/// <returns>An object representing an attribute of the Passport authentication ticket.</returns>
		/// <param name="strAttribute">A string identifying the Passport authentication ticket to return. </param>
		// Token: 0x0600374B RID: 14155 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object Ticket(string strAttribute)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of authentication used to identify the user. This class is deprecated.</summary>
		/// <returns>The string "Passport".</returns>
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string AuthenticationType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the error state associated with the current Passport ticket. This class is deprecated.</summary>
		/// <returns>A 32-bit signed integer indicating the current error state.</returns>
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int Error
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets information on a Passport server connection and query string. This class is deprecated.</summary>
		/// <returns>true if a connection is coming back from the Passport server (logon, update, or registration) and if the Passport data contained on the query string is valid; otherwise, false.</returns>
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool GetFromNetworkServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets information on whether the Passport member's password was saved. This class is deprecated.</summary>
		/// <returns>true if the Passport member's ticket indicates that the password was saved on the Passport logon page the last time the ticket was refreshed; otherwise, false.</returns>
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool HasSavedPassword
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the query string includes a Passport ticket as a cookie. This class is deprecated.</summary>
		/// <returns>true if the query string includes a Passport ticket as a cookie; otherwise, false.</returns>
		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool HasTicket
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the Passport Unique Identifier (PUID) for the currently authenticated user, in hexadecimal form. This class is deprecated.</summary>
		/// <returns>The PUID for the currently authenticated user, in hexadecimal form.</returns>
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string HexPUID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the user is authenticated against a Passport authority. This class is deprecated.</summary>
		/// <returns>true if the user is authenticated against a central site responsible for Passport authentication; otherwise, false.</returns>
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets Passport profile attributes. This class is deprecated.</summary>
		/// <returns>The Passport profile attribute.</returns>
		/// <param name="strProfileName">The Passport profile attribute to return. </param>
		// Token: 0x17001152 RID: 4434
		[global::System.MonoTODO("Not implemented")]
		public string this[string strProfileName]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the current user. This class is deprecated.</summary>
		/// <returns>The name of the current user, which is the Passport Unique Identifier (PUID).</returns>
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time, in seconds, since the last ticket was issued or refreshed. This class is deprecated.</summary>
		/// <returns>The time, in seconds, since the last ticket was issued or refreshed.</returns>
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int TicketAge
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time, in seconds, since a member's logon to the Passport logon server. This class is deprecated.</summary>
		/// <returns>The time, in seconds, since a member's logon to the Passport logon server.</returns>
		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int TimeSinceSignIn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Web.Security.PassportIdentity" /> class. This class is deprecated.</summary>
		// Token: 0x06003757 RID: 14167 RVA: 0x0000393A File Offset: 0x00001B3A
		void IDisposable.Dispose()
		{
		}
	}
}
