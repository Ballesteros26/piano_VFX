using System;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000252 RID: 594
	internal class DataSource
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00084CFB File Offset: 0x00082EFB
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00084D03 File Offset: 0x00082F03
		internal string ServerName { get; private set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00084D0C File Offset: 0x00082F0C
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x00084D14 File Offset: 0x00082F14
		internal int Port { get; private set; } = -1;

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00084D1D File Offset: 0x00082F1D
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00084D25 File Offset: 0x00082F25
		public string InstanceName { get; internal set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00084D2E File Offset: 0x00082F2E
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00084D36 File Offset: 0x00082F36
		public string PipeName { get; internal set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00084D3F File Offset: 0x00082F3F
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x00084D47 File Offset: 0x00082F47
		public string PipeHostName { get; internal set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x00084D50 File Offset: 0x00082F50
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00084D58 File Offset: 0x00082F58
		internal bool IsBadDataSource { get; private set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00084D61 File Offset: 0x00082F61
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00084D69 File Offset: 0x00082F69
		internal bool IsSsrpRequired { get; private set; }

		// Token: 0x06001A5E RID: 6750 RVA: 0x00084D74 File Offset: 0x00082F74
		private DataSource(string dataSource)
		{
			this._workingDataSource = dataSource.Trim().ToLowerInvariant();
			int num = this._workingDataSource.IndexOf(':');
			this.PopulateProtocol();
			this._dataSourceAfterTrimmingProtocol = ((num > -1 && this.ConnectionProtocol != DataSource.Protocol.None) ? this._workingDataSource.Substring(num + 1).Trim() : this._workingDataSource);
			if (this._dataSourceAfterTrimmingProtocol.Contains("/"))
			{
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.NP)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
				}
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00084E2C File Offset: 0x0008302C
		private void PopulateProtocol()
		{
			string[] array = this._workingDataSource.Split(new char[] { ':' });
			if (array.Length <= 1)
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			string text = array[0].Trim();
			if (text == "tcp")
			{
				this.ConnectionProtocol = DataSource.Protocol.TCP;
				return;
			}
			if (text == "np")
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
				return;
			}
			if (!(text == "admin"))
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			this.ConnectionProtocol = DataSource.Protocol.Admin;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00084EB0 File Offset: 0x000830B0
		public static string GetLocalDBInstance(string dataSource, out bool error)
		{
			string text = null;
			string[] array = dataSource.ToLowerInvariant().Split(new char[] { '\\' });
			error = false;
			if (array.Length == 2 && "(localdb)".Equals(array[0].TrimStart(Array.Empty<char>())))
			{
				if (string.IsNullOrWhiteSpace(array[1]))
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 51U, string.Empty);
					error = true;
					return null;
				}
				text = array[1].Trim();
			}
			return text;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00084F2C File Offset: 0x0008312C
		public static DataSource ParseServerName(string dataSource)
		{
			DataSource dataSource2 = new DataSource(dataSource);
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferNamedPipesInformation())
			{
				return dataSource2;
			}
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferConnectionDetails())
			{
				return dataSource2;
			}
			return null;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00084F69 File Offset: 0x00083169
		private void InferLocalServerName()
		{
			if (string.IsNullOrEmpty(this.ServerName) || DataSource.IsLocalHost(this.ServerName))
			{
				this.ServerName = ((this.ConnectionProtocol == DataSource.Protocol.Admin) ? Environment.MachineName : "localhost");
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00084FA0 File Offset: 0x000831A0
		private bool InferConnectionDetails()
		{
			string[] array = this._dataSourceAfterTrimmingProtocol.Split(new char[] { '\\', ',' });
			this.ServerName = array[0].Trim();
			int num = this._dataSourceAfterTrimmingProtocol.IndexOf(',');
			int num2 = this._dataSourceAfterTrimmingProtocol.IndexOf('\\');
			if (num > -1)
			{
				string text = ((num2 > -1) ? ((num > num2) ? array[2].Trim() : array[1].Trim()) : array[1].Trim());
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ConnectionProtocol = DataSource.Protocol.TCP;
				}
				else if (this.ConnectionProtocol != DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				int num3;
				if (!int.TryParse(text, out num3))
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				if (num3 < 1)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				this.Port = num3;
			}
			else if (num2 > -1)
			{
				this.InstanceName = array[1].Trim();
				if (string.IsNullOrWhiteSpace(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if ("mssqlserver".Equals(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				this.IsSsrpRequired = true;
			}
			this.InferLocalServerName();
			return true;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000850D3 File Offset: 0x000832D3
		private void ReportSNIError(SNIProviders provider)
		{
			SNILoadHandle.SingletonInstance.LastError = new SNIError(provider, 0U, 25U, string.Empty);
			this.IsBadDataSource = true;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000850F4 File Offset: 0x000832F4
		private bool InferNamedPipesInformation()
		{
			if (!this._dataSourceAfterTrimmingProtocol.StartsWith("\\\\") && this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				return false;
			}
			if (!this._dataSourceAfterTrimmingProtocol.Contains('\\'))
			{
				this.PipeHostName = (this.ServerName = this._dataSourceAfterTrimmingProtocol);
				this.InferLocalServerName();
				this.PipeName = "sql\\query";
				return true;
			}
			try
			{
				string[] array = this._dataSourceAfterTrimmingProtocol.Split(new char[] { '\\' });
				if (array.Length < 6)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				string text = array[2];
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (!"pipe".Equals(array[3]))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (array[4].StartsWith("mssql$"))
				{
					this.InstanceName = array[4].Substring("mssql$".Length);
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 4; i < array.Length - 1; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(array[array.Length - 1]);
				this.PipeName = stringBuilder.ToString();
				if (string.IsNullOrWhiteSpace(this.InstanceName) && !"sql\\query".Equals(this.PipeName))
				{
					this.InstanceName = "pipe" + this.PipeName;
				}
				this.ServerName = (DataSource.IsLocalHost(text) ? Environment.MachineName : text);
				this.PipeHostName = text;
			}
			catch (UriFormatException)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			if (this.ConnectionProtocol == DataSource.Protocol.None)
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
			}
			else if (this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			return true;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000852D8 File Offset: 0x000834D8
		private static bool IsLocalHost(string serverName)
		{
			return ".".Equals(serverName) || "(local)".Equals(serverName) || "localhost".Equals(serverName);
		}

		// Token: 0x040012D8 RID: 4824
		private const char CommaSeparator = ',';

		// Token: 0x040012D9 RID: 4825
		private const char BackSlashSeparator = '\\';

		// Token: 0x040012DA RID: 4826
		private const string DefaultHostName = "localhost";

		// Token: 0x040012DB RID: 4827
		private const string DefaultSqlServerInstanceName = "mssqlserver";

		// Token: 0x040012DC RID: 4828
		private const string PipeBeginning = "\\\\";

		// Token: 0x040012DD RID: 4829
		private const string PipeToken = "pipe";

		// Token: 0x040012DE RID: 4830
		private const string LocalDbHost = "(localdb)";

		// Token: 0x040012DF RID: 4831
		private const string NamedPipeInstanceNameHeader = "mssql$";

		// Token: 0x040012E0 RID: 4832
		private const string DefaultPipeName = "sql\\query";

		// Token: 0x040012E1 RID: 4833
		internal DataSource.Protocol ConnectionProtocol = DataSource.Protocol.None;

		// Token: 0x040012E7 RID: 4839
		private string _workingDataSource;

		// Token: 0x040012E8 RID: 4840
		private string _dataSourceAfterTrimmingProtocol;

		// Token: 0x02000253 RID: 595
		internal enum Protocol
		{
			// Token: 0x040012EC RID: 4844
			TCP,
			// Token: 0x040012ED RID: 4845
			NP,
			// Token: 0x040012EE RID: 4846
			None,
			// Token: 0x040012EF RID: 4847
			Admin
		}
	}
}
