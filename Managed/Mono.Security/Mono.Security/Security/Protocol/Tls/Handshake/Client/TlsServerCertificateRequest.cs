using System;
using System.Text;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000067 RID: 103
	internal class TlsServerCertificateRequest : HandshakeMessage
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00015152 File Offset: 0x00013352
		public TlsServerCertificateRequest(Context context, byte[] buffer)
			: base(context, HandshakeType.CertificateRequest, buffer)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00015160 File Offset: 0x00013360
		public override void Update()
		{
			base.Update();
			base.Context.ServerSettings.CertificateTypes = this.certificateTypes;
			base.Context.ServerSettings.DistinguisedNames = this.distinguisedNames;
			base.Context.ServerSettings.CertificateRequest = true;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000151B0 File Offset: 0x000133B0
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000151B8 File Offset: 0x000133B8
		protected override void ProcessAsTls1()
		{
			int num = (int)base.ReadByte();
			this.certificateTypes = new ClientCertificateType[num];
			for (int i = 0; i < num; i++)
			{
				this.certificateTypes[i] = (ClientCertificateType)base.ReadByte();
			}
			if (base.ReadInt16() != 0)
			{
				ASN1 asn = new ASN1(base.ReadBytes((int)base.ReadInt16()));
				this.distinguisedNames = new string[asn.Count];
				for (int j = 0; j < asn.Count; j++)
				{
					ASN1 asn2 = new ASN1(asn[j].Value);
					this.distinguisedNames[j] = Encoding.UTF8.GetString(asn2[1].Value);
				}
			}
		}

		// Token: 0x040001E8 RID: 488
		private ClientCertificateType[] certificateTypes;

		// Token: 0x040001E9 RID: 489
		private string[] distinguisedNames;
	}
}
