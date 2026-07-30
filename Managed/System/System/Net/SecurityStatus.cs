using System;

namespace System.Net
{
	// Token: 0x0200043E RID: 1086
	internal enum SecurityStatus
	{
		// Token: 0x04001CCC RID: 7372
		OK,
		// Token: 0x04001CCD RID: 7373
		ContinueNeeded = 590610,
		// Token: 0x04001CCE RID: 7374
		CompleteNeeded,
		// Token: 0x04001CCF RID: 7375
		CompAndContinue,
		// Token: 0x04001CD0 RID: 7376
		ContextExpired = 590615,
		// Token: 0x04001CD1 RID: 7377
		CredentialsNeeded = 590624,
		// Token: 0x04001CD2 RID: 7378
		Renegotiate,
		// Token: 0x04001CD3 RID: 7379
		OutOfMemory = -2146893056,
		// Token: 0x04001CD4 RID: 7380
		InvalidHandle,
		// Token: 0x04001CD5 RID: 7381
		Unsupported,
		// Token: 0x04001CD6 RID: 7382
		TargetUnknown,
		// Token: 0x04001CD7 RID: 7383
		InternalError,
		// Token: 0x04001CD8 RID: 7384
		PackageNotFound,
		// Token: 0x04001CD9 RID: 7385
		NotOwner,
		// Token: 0x04001CDA RID: 7386
		CannotInstall,
		// Token: 0x04001CDB RID: 7387
		InvalidToken,
		// Token: 0x04001CDC RID: 7388
		CannotPack,
		// Token: 0x04001CDD RID: 7389
		QopNotSupported,
		// Token: 0x04001CDE RID: 7390
		NoImpersonation,
		// Token: 0x04001CDF RID: 7391
		LogonDenied,
		// Token: 0x04001CE0 RID: 7392
		UnknownCredentials,
		// Token: 0x04001CE1 RID: 7393
		NoCredentials,
		// Token: 0x04001CE2 RID: 7394
		MessageAltered,
		// Token: 0x04001CE3 RID: 7395
		OutOfSequence,
		// Token: 0x04001CE4 RID: 7396
		NoAuthenticatingAuthority,
		// Token: 0x04001CE5 RID: 7397
		IncompleteMessage = -2146893032,
		// Token: 0x04001CE6 RID: 7398
		IncompleteCredentials = -2146893024,
		// Token: 0x04001CE7 RID: 7399
		BufferNotEnough,
		// Token: 0x04001CE8 RID: 7400
		WrongPrincipal,
		// Token: 0x04001CE9 RID: 7401
		TimeSkew = -2146893020,
		// Token: 0x04001CEA RID: 7402
		UntrustedRoot,
		// Token: 0x04001CEB RID: 7403
		IllegalMessage,
		// Token: 0x04001CEC RID: 7404
		CertUnknown,
		// Token: 0x04001CED RID: 7405
		CertExpired,
		// Token: 0x04001CEE RID: 7406
		AlgorithmMismatch = -2146893007,
		// Token: 0x04001CEF RID: 7407
		SecurityQosFailed,
		// Token: 0x04001CF0 RID: 7408
		SmartcardLogonRequired = -2146892994,
		// Token: 0x04001CF1 RID: 7409
		UnsupportedPreauth = -2146892989,
		// Token: 0x04001CF2 RID: 7410
		BadBinding = -2146892986
	}
}
