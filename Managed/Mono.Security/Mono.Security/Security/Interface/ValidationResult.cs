using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000078 RID: 120
	public class ValidationResult
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00016FC6 File Offset: 0x000151C6
		public ValidationResult(bool trusted, bool user_denied, int error_code, MonoSslPolicyErrors? policy_errors)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
			this.policy_errors = policy_errors;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00016FEB File Offset: 0x000151EB
		internal ValidationResult(bool trusted, bool user_denied, int error_code)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00017008 File Offset: 0x00015208
		public bool Trusted
		{
			get
			{
				return this.trusted;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00017010 File Offset: 0x00015210
		public bool UserDenied
		{
			get
			{
				return this.user_denied;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00017018 File Offset: 0x00015218
		public int ErrorCode
		{
			get
			{
				return this.error_code;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00017020 File Offset: 0x00015220
		public MonoSslPolicyErrors? PolicyErrors
		{
			get
			{
				return this.policy_errors;
			}
		}

		// Token: 0x0400023B RID: 571
		private bool trusted;

		// Token: 0x0400023C RID: 572
		private bool user_denied;

		// Token: 0x0400023D RID: 573
		private int error_code;

		// Token: 0x0400023E RID: 574
		private MonoSslPolicyErrors? policy_errors;
	}
}
