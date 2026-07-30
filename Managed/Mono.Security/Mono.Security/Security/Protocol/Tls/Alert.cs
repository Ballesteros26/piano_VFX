using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200002D RID: 45
	internal class Alert
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D396 File Offset: 0x0000B596
		public AlertLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000D39E File Offset: 0x0000B59E
		public AlertDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		public string Message
		{
			get
			{
				return Alert.GetAlertMessage(this.description);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000D3B3 File Offset: 0x0000B5B3
		public bool IsWarning
		{
			get
			{
				return this.level == AlertLevel.Warning;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public bool IsCloseNotify
		{
			get
			{
				return this.IsWarning && this.description == AlertDescription.CloseNotify;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D3D6 File Offset: 0x0000B5D6
		public Alert(AlertDescription description)
		{
			this.description = description;
			this.level = Alert.inferAlertLevel(description);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
		public Alert(AlertLevel level, AlertDescription description)
		{
			this.level = level;
			this.description = description;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D408 File Offset: 0x0000B608
		private static AlertLevel inferAlertLevel(AlertDescription description)
		{
			if (description <= AlertDescription.DecryptError)
			{
				if (description <= AlertDescription.UnexpectedMessage)
				{
					if (description != AlertDescription.CloseNotify)
					{
						if (description != AlertDescription.UnexpectedMessage)
						{
							return AlertLevel.Fatal;
						}
						return AlertLevel.Fatal;
					}
				}
				else
				{
					if (description - AlertDescription.BadRecordMAC <= 2)
					{
						return AlertLevel.Fatal;
					}
					switch (description)
					{
					case AlertDescription.DecompressionFailiure:
					case (AlertDescription)31:
					case (AlertDescription)32:
					case (AlertDescription)33:
					case (AlertDescription)34:
					case (AlertDescription)35:
					case (AlertDescription)36:
					case (AlertDescription)37:
					case (AlertDescription)38:
					case (AlertDescription)39:
					case AlertDescription.HandshakeFailiure:
					case AlertDescription.NoCertificate:
					case AlertDescription.BadCertificate:
					case AlertDescription.UnsupportedCertificate:
					case AlertDescription.CertificateRevoked:
					case AlertDescription.CertificateExpired:
					case AlertDescription.CertificateUnknown:
					case AlertDescription.IlegalParameter:
					case AlertDescription.UnknownCA:
					case AlertDescription.AccessDenied:
					case AlertDescription.DecodeError:
					case AlertDescription.DecryptError:
						return AlertLevel.Fatal;
					default:
						return AlertLevel.Fatal;
					}
				}
			}
			else if (description <= AlertDescription.InsuficientSecurity)
			{
				if (description != AlertDescription.ExportRestriction && description - AlertDescription.ProtocolVersion > 1)
				{
					return AlertLevel.Fatal;
				}
				return AlertLevel.Fatal;
			}
			else if (description == AlertDescription.InternalError || (description != AlertDescription.UserCancelled && description != AlertDescription.NoRenegotiation))
			{
				return AlertLevel.Fatal;
			}
			return AlertLevel.Warning;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		public static string GetAlertMessage(AlertDescription description)
		{
			return "The authentication or decryption has failed.";
		}

		// Token: 0x04000113 RID: 275
		private AlertLevel level;

		// Token: 0x04000114 RID: 276
		private AlertDescription description;
	}
}
