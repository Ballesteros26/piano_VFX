using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000129 RID: 297
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbError
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x00005C14 File Offset: 0x00003E14
		internal OleDbError()
		{
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A short description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the database-specific error information.</summary>
		/// <returns>The database-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00050D50 File Offset: 0x0004EF50
		public int NativeError
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the provider that generated the error.</summary>
		/// <returns>The name of the provider that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the five-character error code following the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string SQLState
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F82 RID: 3970 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
