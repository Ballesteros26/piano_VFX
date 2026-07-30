using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200007B RID: 123
	internal class MonoTlsStream
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000087FF File Offset: 0x000069FF
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00008807 File Offset: 0x00006A07
		internal IMonoSslStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000880F File Offset: 0x00006A0F
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00008817 File Offset: 0x00006A17
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000881F File Offset: 0x00006A1F
		internal bool CertificateValidationFailed { get; set; }

		// Token: 0x060002B1 RID: 689 RVA: 0x00008828 File Offset: 0x00006A28
		public MonoTlsStream(HttpWebRequest request, NetworkStream networkStream)
		{
			this.request = request;
			this.networkStream = networkStream;
			this.settings = request.TlsSettings;
			this.provider = request.TlsProvider ?? MonoTlsProviderFactory.GetProviderInternal();
			this.status = WebExceptionStatus.SecureChannelFailure;
			ChainValidationHelper.Create(this.provider, ref this.settings, this);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008888 File Offset: 0x00006A88
		internal async Task<Stream> CreateStream(WebConnectionTunnel tunnel, CancellationToken cancellationToken)
		{
			Socket socket = this.networkStream.InternalSocket;
			this.sslStream = this.provider.CreateSslStream(this.networkStream, false, this.settings);
			try
			{
				string text = this.request.Host;
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.IndexOf(':');
					if (num > 0)
					{
						text = text.Substring(0, num);
					}
				}
				await this.sslStream.AuthenticateAsClientAsync(text, this.request.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList).ConfigureAwait(false);
				this.status = WebExceptionStatus.Success;
			}
			catch (Exception)
			{
				if (socket.CleanedUp)
				{
					this.status = WebExceptionStatus.RequestCanceled;
				}
				else
				{
					this.status = WebExceptionStatus.SecureChannelFailure;
				}
				throw;
			}
			finally
			{
				if (this.CertificateValidationFailed)
				{
					this.status = WebExceptionStatus.TrustFailure;
				}
				if (this.status == WebExceptionStatus.Success)
				{
					this.request.ServicePoint.UpdateClientCertificate(this.sslStream.InternalLocalCertificate);
				}
				else
				{
					this.request.ServicePoint.UpdateClientCertificate(null);
					this.sslStream = null;
				}
			}
			try
			{
				if (((tunnel != null) ? tunnel.Data : null) != null)
				{
					await this.sslStream.WriteAsync(tunnel.Data, 0, tunnel.Data.Length, cancellationToken).ConfigureAwait(false);
				}
			}
			catch
			{
				this.status = WebExceptionStatus.SendFailure;
				this.sslStream = null;
				throw;
			}
			return this.sslStream.AuthenticatedStream;
		}

		// Token: 0x040007F9 RID: 2041
		private readonly MonoTlsProvider provider;

		// Token: 0x040007FA RID: 2042
		private readonly NetworkStream networkStream;

		// Token: 0x040007FB RID: 2043
		private readonly HttpWebRequest request;

		// Token: 0x040007FC RID: 2044
		private readonly MonoTlsSettings settings;

		// Token: 0x040007FD RID: 2045
		private IMonoSslStream sslStream;

		// Token: 0x040007FE RID: 2046
		private WebExceptionStatus status;
	}
}
