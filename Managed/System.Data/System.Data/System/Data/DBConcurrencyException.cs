using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown by the <see cref="T:System.Data.Common.DataAdapter" /> during an insert, update, or delete operation if the number of rows affected equals zero.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005E RID: 94
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		// Token: 0x06000305 RID: 773 RVA: 0x00010779 File Offset: 0x0000E979
		public DBConcurrencyException()
			: this("DB concurrency violation.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		// Token: 0x06000306 RID: 774 RVA: 0x00010787 File Offset: 0x0000E987
		public DBConcurrencyException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		/// <param name="inner">A reference to an inner exception. </param>
		// Token: 0x06000307 RID: 775 RVA: 0x00010791 File Offset: 0x0000E991
		public DBConcurrencyException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232011;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		/// <param name="dataRows">An array containing the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception.</param>
		// Token: 0x06000308 RID: 776 RVA: 0x000107A6 File Offset: 0x0000E9A6
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows)
			: base(message, inner)
		{
			base.HResult = -2146232011;
			this._dataRows = dataRows;
		}

		/// <summary>Populates the aprcified serialization information object with the data needed to serialize the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000309 RID: 777 RVA: 0x000107C2 File Offset: 0x0000E9C2
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Data.DataRow" /> that generated the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <returns>The value of the <see cref="T:System.Data.DataRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000107CC File Offset: 0x0000E9CC
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000107EC File Offset: 0x0000E9EC
		public DataRow Row
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null || dataRows.Length == 0)
				{
					return null;
				}
				return dataRows[0];
			}
			set
			{
				this._dataRows = new DataRow[] { value };
			}
		}

		/// <summary>Gets the number of rows whose update failed, generating this exception.</summary>
		/// <returns>An integer containing a count of the number of rows whose update failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00010800 File Offset: 0x0000EA00
		public int RowCount
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null)
				{
					return 0;
				}
				return dataRows.Length;
			}
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600030D RID: 781 RVA: 0x0001081C File Offset: 0x0000EA1C
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <param name="arrayIndex">The destination array index to start copying into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600030E RID: 782 RVA: 0x00010828 File Offset: 0x0000EA28
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x04000515 RID: 1301
		private DataRow[] _dataRows;
	}
}
