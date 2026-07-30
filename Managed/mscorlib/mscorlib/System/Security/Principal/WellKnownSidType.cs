using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines a set of commonly used security identifiers (SIDs).</summary>
	// Token: 0x0200062B RID: 1579
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		/// <summary>Indicates a null SID.</summary>
		// Token: 0x0400228F RID: 8847
		NullSid,
		/// <summary>Indicates a SID that matches everyone.</summary>
		// Token: 0x04002290 RID: 8848
		WorldSid,
		/// <summary>Indicates a local SID.</summary>
		// Token: 0x04002291 RID: 8849
		LocalSid,
		/// <summary>Indicates a SID that matches the owner or creator of an object.</summary>
		// Token: 0x04002292 RID: 8850
		CreatorOwnerSid,
		/// <summary>Indicates a SID that matches the creator group of an object.</summary>
		// Token: 0x04002293 RID: 8851
		CreatorGroupSid,
		/// <summary>Indicates a creator owner server SID.</summary>
		// Token: 0x04002294 RID: 8852
		CreatorOwnerServerSid,
		/// <summary>Indicates a creator group server SID.</summary>
		// Token: 0x04002295 RID: 8853
		CreatorGroupServerSid,
		/// <summary>Indicates a SID for the Windows NT authority.</summary>
		// Token: 0x04002296 RID: 8854
		NTAuthoritySid,
		/// <summary>Indicates a SID for a dial-up account.</summary>
		// Token: 0x04002297 RID: 8855
		DialupSid,
		/// <summary>Indicates a SID for a network account. This SID is added to the process of a token when it logs on across a network.</summary>
		// Token: 0x04002298 RID: 8856
		NetworkSid,
		/// <summary>Indicates a SID for a batch process. This SID is added to the process of a token when it logs on as a batch job.</summary>
		// Token: 0x04002299 RID: 8857
		BatchSid,
		/// <summary>Indicates a SID for an interactive account. This SID is added to the process of a token when it logs on interactively.</summary>
		// Token: 0x0400229A RID: 8858
		InteractiveSid,
		/// <summary>Indicates a SID for a service. This SID is added to the process of a token when it logs on as a service.</summary>
		// Token: 0x0400229B RID: 8859
		ServiceSid,
		/// <summary>Indicates a SID for the anonymous account.</summary>
		// Token: 0x0400229C RID: 8860
		AnonymousSid,
		/// <summary>Indicates a proxy SID.</summary>
		// Token: 0x0400229D RID: 8861
		ProxySid,
		/// <summary>Indicates a SID for an enterprise controller.</summary>
		// Token: 0x0400229E RID: 8862
		EnterpriseControllersSid,
		/// <summary>Indicates a SID for self.</summary>
		// Token: 0x0400229F RID: 8863
		SelfSid,
		/// <summary>Indicates a SID for an authenticated user.</summary>
		// Token: 0x040022A0 RID: 8864
		AuthenticatedUserSid,
		/// <summary>Indicates a SID for restricted code.</summary>
		// Token: 0x040022A1 RID: 8865
		RestrictedCodeSid,
		/// <summary>Indicates a SID that matches a terminal server account.</summary>
		// Token: 0x040022A2 RID: 8866
		TerminalServerSid,
		/// <summary>Indicates a SID that matches remote logons.</summary>
		// Token: 0x040022A3 RID: 8867
		RemoteLogonIdSid,
		/// <summary>Indicates a SID that matches logon IDs.</summary>
		// Token: 0x040022A4 RID: 8868
		LogonIdsSid,
		/// <summary>Indicates a SID that matches the local system.</summary>
		// Token: 0x040022A5 RID: 8869
		LocalSystemSid,
		/// <summary>Indicates a SID that matches a local service.</summary>
		// Token: 0x040022A6 RID: 8870
		LocalServiceSid,
		/// <summary>Indicates a SID that matches a network service.</summary>
		// Token: 0x040022A7 RID: 8871
		NetworkServiceSid,
		/// <summary>Indicates a SID that matches the domain account.</summary>
		// Token: 0x040022A8 RID: 8872
		BuiltinDomainSid,
		/// <summary>Indicates a SID that matches the administrator account.</summary>
		// Token: 0x040022A9 RID: 8873
		BuiltinAdministratorsSid,
		/// <summary>Indicates a SID that matches built-in user accounts.</summary>
		// Token: 0x040022AA RID: 8874
		BuiltinUsersSid,
		/// <summary>Indicates a SID that matches the guest account.</summary>
		// Token: 0x040022AB RID: 8875
		BuiltinGuestsSid,
		/// <summary>Indicates a SID that matches the power users group.</summary>
		// Token: 0x040022AC RID: 8876
		BuiltinPowerUsersSid,
		/// <summary>Indicates a SID that matches the account operators account.</summary>
		// Token: 0x040022AD RID: 8877
		BuiltinAccountOperatorsSid,
		/// <summary>Indicates a SID that matches the system operators group.</summary>
		// Token: 0x040022AE RID: 8878
		BuiltinSystemOperatorsSid,
		/// <summary>Indicates a SID that matches the print operators group.</summary>
		// Token: 0x040022AF RID: 8879
		BuiltinPrintOperatorsSid,
		/// <summary>Indicates a SID that matches the backup operators group.</summary>
		// Token: 0x040022B0 RID: 8880
		BuiltinBackupOperatorsSid,
		/// <summary>Indicates a SID that matches the replicator account.</summary>
		// Token: 0x040022B1 RID: 8881
		BuiltinReplicatorSid,
		/// <summary>Indicates a SID that matches pre-Windows 2000 compatible accounts.</summary>
		// Token: 0x040022B2 RID: 8882
		BuiltinPreWindows2000CompatibleAccessSid,
		/// <summary>Indicates a SID that matches remote desktop users.</summary>
		// Token: 0x040022B3 RID: 8883
		BuiltinRemoteDesktopUsersSid,
		/// <summary>Indicates a SID that matches the network operators group.</summary>
		// Token: 0x040022B4 RID: 8884
		BuiltinNetworkConfigurationOperatorsSid,
		/// <summary>Indicates a SID that matches the account administrators group.</summary>
		// Token: 0x040022B5 RID: 8885
		AccountAdministratorSid,
		/// <summary>Indicates a SID that matches the account guest group.</summary>
		// Token: 0x040022B6 RID: 8886
		AccountGuestSid,
		/// <summary>Indicates a SID that matches the account Kerberos target group.</summary>
		// Token: 0x040022B7 RID: 8887
		AccountKrbtgtSid,
		/// <summary>Indicates a SID that matches the account domain administrator group.</summary>
		// Token: 0x040022B8 RID: 8888
		AccountDomainAdminsSid,
		/// <summary>Indicates a SID that matches the account domain users group.</summary>
		// Token: 0x040022B9 RID: 8889
		AccountDomainUsersSid,
		/// <summary>Indicates a SID that matches the account domain guests group.</summary>
		// Token: 0x040022BA RID: 8890
		AccountDomainGuestsSid,
		/// <summary>Indicates a SID that matches the account computer group.</summary>
		// Token: 0x040022BB RID: 8891
		AccountComputersSid,
		/// <summary>Indicates a SID that matches the account controller group.</summary>
		// Token: 0x040022BC RID: 8892
		AccountControllersSid,
		/// <summary>Indicates a SID that matches the certificate administrators group.</summary>
		// Token: 0x040022BD RID: 8893
		AccountCertAdminsSid,
		/// <summary>Indicates a SID that matches the schema administrators group.</summary>
		// Token: 0x040022BE RID: 8894
		AccountSchemaAdminsSid,
		/// <summary>Indicates a SID that matches the enterprise administrators group.</summary>
		// Token: 0x040022BF RID: 8895
		AccountEnterpriseAdminsSid,
		/// <summary>Indicates a SID that matches the policy administrators group.</summary>
		// Token: 0x040022C0 RID: 8896
		AccountPolicyAdminsSid,
		/// <summary>Indicates a SID that matches the RAS and IAS server account.</summary>
		// Token: 0x040022C1 RID: 8897
		AccountRasAndIasServersSid,
		/// <summary>Indicates a SID present when the Microsoft NTLM authentication package authenticated the client.</summary>
		// Token: 0x040022C2 RID: 8898
		NtlmAuthenticationSid,
		/// <summary>Indicates a SID present when the Microsoft Digest authentication package authenticated the client.</summary>
		// Token: 0x040022C3 RID: 8899
		DigestAuthenticationSid,
		/// <summary>Indicates a SID present when the Secure Channel (SSL/TLS) authentication package authenticated the client.</summary>
		// Token: 0x040022C4 RID: 8900
		SChannelAuthenticationSid,
		/// <summary>Indicates a SID present when the user authenticated from within the forest or across a trust that does not have the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.OtherOrganizationSid" /> cannot be present.</summary>
		// Token: 0x040022C5 RID: 8901
		ThisOrganizationSid,
		/// <summary>Indicates a SID present when the user authenticated across a forest with the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.ThisOrganizationSid" /> cannot be present.</summary>
		// Token: 0x040022C6 RID: 8902
		OtherOrganizationSid,
		/// <summary>Indicates a SID that allows a user to create incoming forest trusts. It is added to the token of users who are a member of the Incoming Forest Trust Builders built-in group in the root domain of the forest.</summary>
		// Token: 0x040022C7 RID: 8903
		BuiltinIncomingForestTrustBuildersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to schedule logging of performance counters on this computer.</summary>
		// Token: 0x040022C8 RID: 8904
		BuiltinPerformanceMonitoringUsersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to monitor the computer.</summary>
		// Token: 0x040022C9 RID: 8905
		BuiltinPerformanceLoggingUsersSid,
		/// <summary>Indicates a SID that matches the Windows Authorization Access group.</summary>
		// Token: 0x040022CA RID: 8906
		BuiltinAuthorizationAccessSid,
		/// <summary>Indicates a SID is present in a server that can issue Terminal Server licenses.</summary>
		// Token: 0x040022CB RID: 8907
		WinBuiltinTerminalServerLicenseServersSid,
		/// <summary>Indicates the maximum defined SID in the <see cref="T:System.Security.Principal.WellKnownSidType" /> enumeration.</summary>
		// Token: 0x040022CC RID: 8908
		MaxDefined = 60,
		// Token: 0x040022CD RID: 8909
		WinBuiltinDCOMUsersSid,
		// Token: 0x040022CE RID: 8910
		WinBuiltinIUsersSid,
		// Token: 0x040022CF RID: 8911
		WinIUserSid,
		// Token: 0x040022D0 RID: 8912
		WinBuiltinCryptoOperatorsSid,
		// Token: 0x040022D1 RID: 8913
		WinUntrustedLabelSid,
		// Token: 0x040022D2 RID: 8914
		WinLowLabelSid,
		// Token: 0x040022D3 RID: 8915
		WinMediumLabelSid,
		// Token: 0x040022D4 RID: 8916
		WinHighLabelSid,
		// Token: 0x040022D5 RID: 8917
		WinSystemLabelSid,
		// Token: 0x040022D6 RID: 8918
		WinWriteRestrictedCodeSid,
		// Token: 0x040022D7 RID: 8919
		WinCreatorOwnerRightsSid,
		// Token: 0x040022D8 RID: 8920
		WinCacheablePrincipalsGroupSid,
		// Token: 0x040022D9 RID: 8921
		WinNonCacheablePrincipalsGroupSid,
		// Token: 0x040022DA RID: 8922
		WinEnterpriseReadonlyControllersSid,
		// Token: 0x040022DB RID: 8923
		WinAccountReadonlyControllersSid,
		// Token: 0x040022DC RID: 8924
		WinBuiltinEventLogReadersGroup,
		// Token: 0x040022DD RID: 8925
		WinNewEnterpriseReadonlyControllersSid,
		// Token: 0x040022DE RID: 8926
		WinBuiltinCertSvcDComAccessGroup,
		// Token: 0x040022DF RID: 8927
		WinMediumPlusLabelSid,
		// Token: 0x040022E0 RID: 8928
		WinLocalLogonSid,
		// Token: 0x040022E1 RID: 8929
		WinConsoleLogonSid,
		// Token: 0x040022E2 RID: 8930
		WinThisOrganizationCertificateSid,
		// Token: 0x040022E3 RID: 8931
		WinApplicationPackageAuthoritySid,
		// Token: 0x040022E4 RID: 8932
		WinBuiltinAnyPackageSid,
		// Token: 0x040022E5 RID: 8933
		WinCapabilityInternetClientSid,
		// Token: 0x040022E6 RID: 8934
		WinCapabilityInternetClientServerSid,
		// Token: 0x040022E7 RID: 8935
		WinCapabilityPrivateNetworkClientServerSid,
		// Token: 0x040022E8 RID: 8936
		WinCapabilityPicturesLibrarySid,
		// Token: 0x040022E9 RID: 8937
		WinCapabilityVideosLibrarySid,
		// Token: 0x040022EA RID: 8938
		WinCapabilityMusicLibrarySid,
		// Token: 0x040022EB RID: 8939
		WinCapabilityDocumentsLibrarySid,
		// Token: 0x040022EC RID: 8940
		WinCapabilitySharedUserCertificatesSid,
		// Token: 0x040022ED RID: 8941
		WinCapabilityEnterpriseAuthenticationSid,
		// Token: 0x040022EE RID: 8942
		WinCapabilityRemovableStorageSid
	}
}
