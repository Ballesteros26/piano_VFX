using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000BB RID: 187
	[NativeHeader("Runtime/Export/CrashReport/CrashReport.bindings.h")]
	public sealed class CrashReport
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x00006658 File Offset: 0x00004858
		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			bool flag = ticks > ticks2;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				bool flag2 = ticks < ticks2;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000066A8 File Offset: 0x000048A8
		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				bool flag = CrashReport.internalReports != null;
				if (!flag)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					foreach (string text in reports)
					{
						double num;
						string reportData = CrashReport.GetReportData(text, out num);
						DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(num);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime, reportData));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00006774 File Offset: 0x00004974
		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] array;
				lock (obj)
				{
					array = CrashReport.internalReports.ToArray();
				}
				return array;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x000067BC File Offset: 0x000049BC
		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					bool flag = CrashReport.internalReports.Count > 0;
					if (flag)
					{
						return CrashReport.internalReports[CrashReport.internalReports.Count - 1];
					}
				}
				return null;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000682C File Offset: 0x00004A2C
		public static void RemoveAll()
		{
			foreach (CrashReport crashReport in CrashReport.reports)
			{
				crashReport.Remove();
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000685A File Offset: 0x00004A5A
		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000687C File Offset: 0x00004A7C
		public void Remove()
		{
			bool flag = CrashReport.RemoveReport(this.id);
			if (flag)
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		// Token: 0x06000464 RID: 1124
		[FreeFunction(Name = "CrashReport_Bindings::GetReports", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern string[] GetReports();

		// Token: 0x06000465 RID: 1125
		[FreeFunction(Name = "CrashReport_Bindings::GetReportData", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern string GetReportData(string id, out double secondsSinceUnixEpoch);

		// Token: 0x06000466 RID: 1126
		[FreeFunction(Name = "CrashReport_Bindings::RemoveReport", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool RemoveReport(string id);

		// Token: 0x04000223 RID: 547
		private static List<CrashReport> internalReports;

		// Token: 0x04000224 RID: 548
		private static object reportsLock = new object();

		// Token: 0x04000225 RID: 549
		private readonly string id;

		// Token: 0x04000226 RID: 550
		public readonly DateTime time;

		// Token: 0x04000227 RID: 551
		public readonly string text;
	}
}
