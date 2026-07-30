using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001A0 RID: 416
	internal class SqlConnectionTimeoutErrorInternal
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0005F90B File Offset: 0x0005DB0B
		internal SqlConnectionTimeoutErrorPhase CurrentPhase
		{
			get
			{
				return this._currentPhase;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0005F914 File Offset: 0x0005DB14
		public SqlConnectionTimeoutErrorInternal()
		{
			this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0005F950 File Offset: 0x0005DB50
		public void SetFailoverScenario(bool useFailoverServer)
		{
			this._isFailoverScenario = useFailoverServer;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0005F959 File Offset: 0x0005DB59
		public void SetInternalSourceType(SqlConnectionInternalSourceType sourceType)
		{
			this._currentSourceType = sourceType;
			if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
			{
				this._originalPhaseDurations = this._phaseDurations;
				this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
				this.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0005F98C File Offset: 0x0005DB8C
		internal void ResetAndRestartPhase()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.PreLoginBegin;
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0005F9BC File Offset: 0x0005DBBC
		internal void SetAndBeginPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._currentPhase = timeoutErrorPhase;
			if (this._phaseDurations[(int)timeoutErrorPhase] == null)
			{
				this._phaseDurations[(int)timeoutErrorPhase] = new SqlConnectionTimeoutPhaseDuration();
			}
			this._phaseDurations[(int)timeoutErrorPhase].StartCapture();
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0005F9E9 File Offset: 0x0005DBE9
		internal void EndPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._phaseDurations[(int)timeoutErrorPhase].StopCapture();
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0005F9F8 File Offset: 0x0005DBF8
		internal void SetAllCompleteMarker()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.Complete;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0005FA04 File Offset: 0x0005DC04
		internal string GetErrorMessage()
		{
			StringBuilder stringBuilder;
			string text;
			switch (this._currentPhase)
			{
			case SqlConnectionTimeoutErrorPhase.PreLoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_Begin());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.InitializeConnection:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_InitializeConnection());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_SendHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_ConsumeHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.LoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_Begin());
				text = SQLMessage.Duration_Login_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_ProcessConnectionAuth());
				text = SQLMessage.Duration_Login_ProcessConnectionAuth(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.PostLogin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PostLogin());
				text = SQLMessage.Duration_PostLogin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration(), this._phaseDurations[7].GetMilliSecondDuration());
				break;
			default:
				stringBuilder = new StringBuilder(SQLMessage.Timeout());
				text = null;
				break;
			}
			if (this._currentPhase != SqlConnectionTimeoutErrorPhase.Undefined && this._currentPhase != SqlConnectionTimeoutErrorPhase.Complete)
			{
				if (this._isFailoverScenario)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_FailoverInfo(), this._currentSourceType);
				}
				else if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_RoutingDestination(), new object[]
					{
						this._originalPhaseDurations[1].GetMilliSecondDuration() + this._originalPhaseDurations[2].GetMilliSecondDuration(),
						this._originalPhaseDurations[3].GetMilliSecondDuration() + this._originalPhaseDurations[4].GetMilliSecondDuration(),
						this._originalPhaseDurations[5].GetMilliSecondDuration(),
						this._originalPhaseDurations[6].GetMilliSecondDuration(),
						this._originalPhaseDurations[7].GetMilliSecondDuration()
					});
				}
			}
			if (text != null)
			{
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D12 RID: 3346
		private SqlConnectionTimeoutPhaseDuration[] _phaseDurations;

		// Token: 0x04000D13 RID: 3347
		private SqlConnectionTimeoutPhaseDuration[] _originalPhaseDurations;

		// Token: 0x04000D14 RID: 3348
		private SqlConnectionTimeoutErrorPhase _currentPhase;

		// Token: 0x04000D15 RID: 3349
		private SqlConnectionInternalSourceType _currentSourceType;

		// Token: 0x04000D16 RID: 3350
		private bool _isFailoverScenario;
	}
}
