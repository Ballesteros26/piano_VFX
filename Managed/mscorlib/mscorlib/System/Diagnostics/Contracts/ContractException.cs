using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A8C RID: 2700
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600624B RID: 25163 RVA: 0x0014102B File Offset: 0x0013F22B
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x0600624C RID: 25164 RVA: 0x00141033 File Offset: 0x0013F233
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600624D RID: 25165 RVA: 0x0014103B File Offset: 0x0013F23B
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x00141043 File Offset: 0x0013F243
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0014104B File Offset: 0x0013F24B
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0014105E File Offset: 0x0013F25E
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
			: base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0014108A File Offset: 0x0013F28A
		private ContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x001410C8 File Offset: 0x0013F2C8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x04003101 RID: 12545
		private readonly ContractFailureKind _Kind;

		// Token: 0x04003102 RID: 12546
		private readonly string _UserMessage;

		// Token: 0x04003103 RID: 12547
		private readonly string _Condition;
	}
}
