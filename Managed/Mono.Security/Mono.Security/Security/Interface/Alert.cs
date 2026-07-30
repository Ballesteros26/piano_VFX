using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000077 RID: 119
	public class Alert
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00016E58 File Offset: 0x00015058
		public AlertLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00016E60 File Offset: 0x00015060
		public AlertDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00016E68 File Offset: 0x00015068
		public string Message
		{
			get
			{
				return Alert.GetAlertMessage(this.description);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00016E75 File Offset: 0x00015075
		public bool IsWarning
		{
			get
			{
				return this.level == AlertLevel.Warning;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00016E83 File Offset: 0x00015083
		public bool IsCloseNotify
		{
			get
			{
				return this.IsWarning && this.description == AlertDescription.CloseNotify;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016E98 File Offset: 0x00015098
		public Alert(AlertDescription description)
		{
			this.description = description;
			this.inferAlertLevel();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016EAD File Offset: 0x000150AD
		public Alert(AlertLevel level, AlertDescription description)
		{
			this.level = level;
			this.description = description;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016EC4 File Offset: 0x000150C4
		private void inferAlertLevel()
		{
			AlertDescription alertDescription = this.description;
			if (alertDescription <= AlertDescription.ExportRestriction)
			{
				if (alertDescription <= AlertDescription.UnexpectedMessage)
				{
					if (alertDescription != AlertDescription.CloseNotify)
					{
						if (alertDescription != AlertDescription.UnexpectedMessage)
						{
							goto IL_00C5;
						}
						goto IL_00C5;
					}
				}
				else
				{
					if (alertDescription - AlertDescription.BadRecordMAC <= 2)
					{
						goto IL_00C5;
					}
					switch (alertDescription)
					{
					case AlertDescription.DecompressionFailure:
					case (AlertDescription)31:
					case (AlertDescription)32:
					case (AlertDescription)33:
					case (AlertDescription)34:
					case (AlertDescription)35:
					case (AlertDescription)36:
					case (AlertDescription)37:
					case (AlertDescription)38:
					case (AlertDescription)39:
					case AlertDescription.HandshakeFailure:
					case AlertDescription.NoCertificate_RESERVED:
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
						goto IL_00C5;
					default:
						if (alertDescription != AlertDescription.ExportRestriction)
						{
							goto IL_00C5;
						}
						goto IL_00C5;
					}
				}
			}
			else if (alertDescription <= AlertDescription.InternalError)
			{
				if (alertDescription - AlertDescription.ProtocolVersion > 1 && alertDescription != AlertDescription.InternalError)
				{
					goto IL_00C5;
				}
				goto IL_00C5;
			}
			else if (alertDescription != AlertDescription.UserCancelled && alertDescription != AlertDescription.NoRenegotiation)
			{
				if (alertDescription != AlertDescription.UnsupportedExtension)
				{
					goto IL_00C5;
				}
				goto IL_00C5;
			}
			this.level = AlertLevel.Warning;
			return;
			IL_00C5:
			this.level = AlertLevel.Fatal;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016F9D File Offset: 0x0001519D
		public override string ToString()
		{
			return string.Format("[Alert: {0}:{1}]", this.Level, this.Description);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016FBF File Offset: 0x000151BF
		public static string GetAlertMessage(AlertDescription description)
		{
			return "The authentication or decryption has failed.";
		}

		// Token: 0x04000239 RID: 569
		private AlertLevel level;

		// Token: 0x0400023A RID: 570
		private AlertDescription description;
	}
}
