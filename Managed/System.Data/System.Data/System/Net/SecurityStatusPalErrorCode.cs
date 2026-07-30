using System;

namespace System.Net
{
	// Token: 0x02000045 RID: 69
	internal enum SecurityStatusPalErrorCode
	{
		// Token: 0x04000490 RID: 1168
		NotSet,
		// Token: 0x04000491 RID: 1169
		OK,
		// Token: 0x04000492 RID: 1170
		ContinueNeeded,
		// Token: 0x04000493 RID: 1171
		CompleteNeeded,
		// Token: 0x04000494 RID: 1172
		CompAndContinue,
		// Token: 0x04000495 RID: 1173
		ContextExpired,
		// Token: 0x04000496 RID: 1174
		CredentialsNeeded,
		// Token: 0x04000497 RID: 1175
		Renegotiate,
		// Token: 0x04000498 RID: 1176
		OutOfMemory,
		// Token: 0x04000499 RID: 1177
		InvalidHandle,
		// Token: 0x0400049A RID: 1178
		Unsupported,
		// Token: 0x0400049B RID: 1179
		TargetUnknown,
		// Token: 0x0400049C RID: 1180
		InternalError,
		// Token: 0x0400049D RID: 1181
		PackageNotFound,
		// Token: 0x0400049E RID: 1182
		NotOwner,
		// Token: 0x0400049F RID: 1183
		CannotInstall,
		// Token: 0x040004A0 RID: 1184
		InvalidToken,
		// Token: 0x040004A1 RID: 1185
		CannotPack,
		// Token: 0x040004A2 RID: 1186
		QopNotSupported,
		// Token: 0x040004A3 RID: 1187
		NoImpersonation,
		// Token: 0x040004A4 RID: 1188
		LogonDenied,
		// Token: 0x040004A5 RID: 1189
		UnknownCredentials,
		// Token: 0x040004A6 RID: 1190
		NoCredentials,
		// Token: 0x040004A7 RID: 1191
		MessageAltered,
		// Token: 0x040004A8 RID: 1192
		OutOfSequence,
		// Token: 0x040004A9 RID: 1193
		NoAuthenticatingAuthority,
		// Token: 0x040004AA RID: 1194
		IncompleteMessage,
		// Token: 0x040004AB RID: 1195
		IncompleteCredentials,
		// Token: 0x040004AC RID: 1196
		BufferNotEnough,
		// Token: 0x040004AD RID: 1197
		WrongPrincipal,
		// Token: 0x040004AE RID: 1198
		TimeSkew,
		// Token: 0x040004AF RID: 1199
		UntrustedRoot,
		// Token: 0x040004B0 RID: 1200
		IllegalMessage,
		// Token: 0x040004B1 RID: 1201
		CertUnknown,
		// Token: 0x040004B2 RID: 1202
		CertExpired,
		// Token: 0x040004B3 RID: 1203
		AlgorithmMismatch,
		// Token: 0x040004B4 RID: 1204
		SecurityQosFailed,
		// Token: 0x040004B5 RID: 1205
		SmartcardLogonRequired,
		// Token: 0x040004B6 RID: 1206
		UnsupportedPreauth,
		// Token: 0x040004B7 RID: 1207
		BadBinding,
		// Token: 0x040004B8 RID: 1208
		DowngradeDetected
	}
}
