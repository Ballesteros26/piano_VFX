using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FD RID: 509
	public class EventSourceCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSourceCreationData" /> class with a specified event source and event log name.</summary>
		/// <param name="source">The name to register with the event log as a source of entries. </param>
		/// <param name="logName">The name of the log to which entries from the source are written. </param>
		// Token: 0x0600105D RID: 4189 RVA: 0x000499F9 File Offset: 0x00047BF9
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = ".";
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00049A1A File Offset: 0x00047C1A
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			if (logName == null || logName.Length == 0)
			{
				this._logName = "Application";
			}
			else
			{
				this._logName = logName;
			}
			this._machineName = machineName;
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00049A4F File Offset: 0x00047C4F
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x00049A57 File Offset: 0x00047C57
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00049A6F File Offset: 0x00047C6F
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x00049A77 File Offset: 0x00047C77
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00049A80 File Offset: 0x00047C80
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x00049A88 File Offset: 0x00047C88
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00049A91 File Offset: 0x00047C91
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00049A99 File Offset: 0x00047C99
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00049AA2 File Offset: 0x00047CA2
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00049AAA File Offset: 0x00047CAA
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00049AB3 File Offset: 0x00047CB3
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00049ABB File Offset: 0x00047CBB
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00049AC4 File Offset: 0x00047CC4
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00049ACC File Offset: 0x00047CCC
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x04001158 RID: 4440
		private string _source;

		// Token: 0x04001159 RID: 4441
		private string _logName;

		// Token: 0x0400115A RID: 4442
		private string _machineName;

		// Token: 0x0400115B RID: 4443
		private string _messageResourceFile;

		// Token: 0x0400115C RID: 4444
		private string _parameterResourceFile;

		// Token: 0x0400115D RID: 4445
		private string _categoryResourceFile;

		// Token: 0x0400115E RID: 4446
		private int _categoryCount;
	}
}
