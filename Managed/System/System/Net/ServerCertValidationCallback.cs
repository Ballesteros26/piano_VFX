using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004FB RID: 1275
	internal class ServerCertValidationCallback
	{
		// Token: 0x06002631 RID: 9777 RVA: 0x00093838 File Offset: 0x00091A38
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00093852 File Offset: 0x00091A52
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0009385C File Offset: 0x00091A5C
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0009389C File Offset: 0x00091A9C
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x040020EF RID: 8431
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x040020F0 RID: 8432
		private readonly ExecutionContext m_Context;

		// Token: 0x020004FC RID: 1276
		private class CallbackContext
		{
			// Token: 0x06002635 RID: 9781 RVA: 0x000938F0 File Offset: 0x00091AF0
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x040020F1 RID: 8433
			internal readonly object request;

			// Token: 0x040020F2 RID: 8434
			internal readonly X509Certificate certificate;

			// Token: 0x040020F3 RID: 8435
			internal readonly X509Chain chain;

			// Token: 0x040020F4 RID: 8436
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x040020F5 RID: 8437
			internal bool result;
		}
	}
}
