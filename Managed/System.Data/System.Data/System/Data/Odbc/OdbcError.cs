using System;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029D RID: 669
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x06001C7F RID: 7295 RVA: 0x0008DAC0 File Offset: 0x0008BCC0
		internal OdbcError(string source, string message, string state, int nativeerror)
		{
			this._source = source;
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0008DAE5 File Offset: 0x0008BCE5
		public string Message
		{
			get
			{
				if (this._message == null)
				{
					return string.Empty;
				}
				return this._message;
			}
		}

		/// <summary>Gets the five-character error code that follows the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0008DAFB File Offset: 0x0008BCFB
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		/// <summary>Gets the data source-specific error information.</summary>
		/// <returns>The data source-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0008DB03 File Offset: 0x0008BD03
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		/// <summary>Gets the name of the driver that generated the error.</summary>
		/// <returns>The name of the driver that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0008DB0B File Offset: 0x0008BD0B
		public string Source
		{
			get
			{
				if (this._source == null)
				{
					return string.Empty;
				}
				return this._source;
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0008DB21 File Offset: 0x0008BD21
		internal void SetSource(string Source)
		{
			this._source = Source;
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C85 RID: 7301 RVA: 0x0008DB2A File Offset: 0x0008BD2A
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00010468 File Offset: 0x0000E668
		internal OdbcError()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400153C RID: 5436
		internal string _message;

		// Token: 0x0400153D RID: 5437
		internal string _state;

		// Token: 0x0400153E RID: 5438
		internal int _nativeerror;

		// Token: 0x0400153F RID: 5439
		internal string _source;
	}
}
