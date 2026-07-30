using System;

namespace System.Transactions
{
	/// <summary>Provides additional information regarding a transaction.</summary>
	// Token: 0x02000023 RID: 35
	public class TransactionInformation
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00002B64 File Offset: 0x00000D64
		internal TransactionInformation()
		{
			this.status = TransactionStatus.Active;
			this.creation_time = DateTime.Now.ToUniversalTime();
			this.local_id = Guid.NewGuid().ToString() + ":1";
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private TransactionInformation(TransactionInformation other)
		{
			this.local_id = other.local_id;
			this.dtcId = other.dtcId;
			this.creation_time = other.creation_time;
			this.status = other.status;
		}

		/// <summary>Gets the creation time of the transaction.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the creation time of the transaction.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002C0E File Offset: 0x00000E0E
		public DateTime CreationTime
		{
			get
			{
				return this.creation_time;
			}
		}

		/// <summary>Gets a unique identifier of the escalated transaction.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the unique identifier of the escalated transaction.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002C1E File Offset: 0x00000E1E
		public Guid DistributedIdentifier
		{
			get
			{
				return this.dtcId;
			}
			internal set
			{
				this.dtcId = value;
			}
		}

		/// <summary>Gets a unique identifier of the transaction.</summary>
		/// <returns>A unique identifier of the transaction.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002C27 File Offset: 0x00000E27
		public string LocalIdentifier
		{
			get
			{
				return this.local_id;
			}
		}

		/// <summary>Gets the status of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionStatus" /> that contains the status of the transaction.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002C2F File Offset: 0x00000E2F
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002C37 File Offset: 0x00000E37
		public TransactionStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002C40 File Offset: 0x00000E40
		internal TransactionInformation Clone(TransactionInformation other)
		{
			return new TransactionInformation(other);
		}

		// Token: 0x0400005A RID: 90
		private string local_id;

		// Token: 0x0400005B RID: 91
		private Guid dtcId = Guid.Empty;

		// Token: 0x0400005C RID: 92
		private DateTime creation_time;

		// Token: 0x0400005D RID: 93
		private TransactionStatus status;
	}
}
