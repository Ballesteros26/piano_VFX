using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B0D RID: 2829
	internal class EtwSession
	{
		// Token: 0x060065B6 RID: 26038 RVA: 0x0014DF04 File Offset: 0x0014C104
		public static EtwSession GetEtwSession(int etwSessionId, bool bCreateIfNeeded = false)
		{
			if (etwSessionId < 0)
			{
				return null;
			}
			EtwSession etwSession;
			using (List<WeakReference<EtwSession>>.Enumerator enumerator = EtwSession.s_etwSessions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryGetTarget(out etwSession) && etwSession.m_etwSessionId == etwSessionId)
					{
						return etwSession;
					}
				}
			}
			if (!bCreateIfNeeded)
			{
				return null;
			}
			if (EtwSession.s_etwSessions == null)
			{
				EtwSession.s_etwSessions = new List<WeakReference<EtwSession>>();
			}
			etwSession = new EtwSession(etwSessionId);
			EtwSession.s_etwSessions.Add(new WeakReference<EtwSession>(etwSession));
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
			return etwSession;
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x0014DFB0 File Offset: 0x0014C1B0
		public static void RemoveEtwSession(EtwSession etwSession)
		{
			if (EtwSession.s_etwSessions == null || etwSession == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession2;
				return wrEtwSession.TryGetTarget(out etwSession2) && etwSession2.m_etwSessionId == etwSession.m_etwSessionId;
			});
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x0014E004 File Offset: 0x0014C204
		private static void TrimGlobalList()
		{
			if (EtwSession.s_etwSessions == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return !wrEtwSession.TryGetTarget(out etwSession);
			});
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x0014E038 File Offset: 0x0014C238
		private EtwSession(int etwSessionId)
		{
			this.m_etwSessionId = etwSessionId;
		}

		// Token: 0x040032A6 RID: 12966
		public readonly int m_etwSessionId;

		// Token: 0x040032A7 RID: 12967
		public ActivityFilter m_activityFilter;

		// Token: 0x040032A8 RID: 12968
		private static List<WeakReference<EtwSession>> s_etwSessions = new List<WeakReference<EtwSession>>();

		// Token: 0x040032A9 RID: 12969
		private const int s_thrSessionCount = 16;
	}
}
