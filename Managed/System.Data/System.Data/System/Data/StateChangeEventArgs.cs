using System;

namespace System.Data
{
	/// <summary>Provides data for the state change event of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F7 RID: 247
	public sealed class StateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StateChangeEventArgs" /> class, when given the original state and the current state of the object.</summary>
		/// <param name="originalState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		/// <param name="currentState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		// Token: 0x06000CEC RID: 3308 RVA: 0x0003C202 File Offset: 0x0003A402
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this._originalState = originalState;
			this._currentState = currentState;
		}

		/// <summary>Gets the new state of the connection. The connection object will be in the new state already when the event is fired.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0003C218 File Offset: 0x0003A418
		public ConnectionState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		/// <summary>Gets the original state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0003C220 File Offset: 0x0003A420
		public ConnectionState OriginalState
		{
			get
			{
				return this._originalState;
			}
		}

		// Token: 0x0400089F RID: 2207
		private ConnectionState _originalState;

		// Token: 0x040008A0 RID: 2208
		private ConnectionState _currentState;
	}
}
