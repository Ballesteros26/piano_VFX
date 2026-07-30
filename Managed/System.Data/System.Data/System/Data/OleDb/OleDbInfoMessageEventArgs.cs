using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012D RID: 301
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00050F4A File Offset: 0x0004F14A
		internal OleDbInfoMessageEventArgs()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the HRESULT following the ANSI SQL standard for the database.</summary>
		/// <returns>The HRESULT, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00050D50 File Offset: 0x0004EF50
		public int ErrorCode
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbErrorCollection Errors
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the full text of the error sent from the data source.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F9C RID: 3996 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
