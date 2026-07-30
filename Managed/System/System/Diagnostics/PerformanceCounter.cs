using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics
{
	/// <summary>Represents a Windows NT performance counter component.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000207 RID: 519
	[InstallerType(typeof(PerformanceCounterInstaller))]
	public sealed class PerformanceCounter : Component, ISupportInitialize
	{
		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class, without associating the instance with any system or custom performance counter.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		// Token: 0x060010E0 RID: 4320 RVA: 0x0004A9E0 File Offset: 0x00048BE0
		public PerformanceCounter()
		{
			this.categoryName = (this.counterName = (this.instanceName = ""));
			this.machineName = ".";
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category have a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").-or- <paramref name="counterName" /> is an empty string ("").-or- The category specified does not exist. -or-The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.-or-<paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060010E1 RID: 4321 RVA: 0x0004AA1B File Offset: 0x00048C1B
		public PerformanceCounter(string categoryName, string counterName)
			: this(categoryName, counterName, false)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category contain a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <param name="readOnly">true to access the counter in read-only mode (although the counter itself could be read/write); false to access the counter in read/write mode. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="categoryName" /> is an empty string ("").-or- The <paramref name="counterName" /> is an empty string ("").-or- The category specified does not exist. (if <paramref name="readOnly" /> is true). -or- The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is false). -or-The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.-or-<paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060010E2 RID: 4322 RVA: 0x0004AA26 File Offset: 0x00048C26
		public PerformanceCounter(string categoryName, string counterName, bool readOnly)
			: this(categoryName, counterName, "", readOnly)
		{
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").-or- <paramref name="counterName" /> is an empty string ("").-or- The category specified is not valid. -or-The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.-or-<paramref name="instanceName" /> is longer than 127 characters.-or-<paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060010E3 RID: 4323 RVA: 0x0004AA36 File Offset: 0x00048C36
		public PerformanceCounter(string categoryName, string counterName, string instanceName)
			: this(categoryName, counterName, instanceName, false)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance. </param>
		/// <param name="readOnly">true to access a counter in read-only mode; false to access a counter in read/write mode. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").-or- <paramref name="counterName" /> is an empty string ("").-or- The read/write permission setting requested is invalid for this counter.-or- The category specified does not exist (if <paramref name="readOnly" /> is true). -or- The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is false). -or-The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.-or-<paramref name="instanceName" /> is longer than 127 characters.-or-<paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060010E4 RID: 4324 RVA: 0x0004AA44 File Offset: 0x00048C44
		public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			if (categoryName == "" || counterName == "")
			{
				throw new InvalidOperationException();
			}
			this.InstanceName = instanceName;
			this.instanceName = instanceName;
			this.machineName = ".";
			this.readOnly = readOnly;
			this.changed = true;
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance, on the specified computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance. </param>
		/// <param name="machineName">The computer on which the performance counter and its associated category exist. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").-or- <paramref name="counterName" /> is an empty string ("").-or- The read/write permission setting requested is invalid for this counter.-or- The counter does not exist on the specified computer. -or-The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.-or-<paramref name="instanceName" /> is longer than 127 characters.-or-<paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060010E5 RID: 4325 RVA: 0x0004AAD7 File Offset: 0x00048CD7
		public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName)
			: this(categoryName, counterName, instanceName, false)
		{
			this.machineName = machineName;
		}

		// Token: 0x060010E6 RID: 4326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetImpl(string category, string counter, string instance, string machine, out PerformanceCounterType ctype, out bool custom);

		// Token: 0x060010E7 RID: 4327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSample(IntPtr impl, bool only_value, out CounterSample sample);

		// Token: 0x060010E8 RID: 4328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long UpdateValue(IntPtr impl, bool do_incr, long value);

		// Token: 0x060010E9 RID: 4329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeData(IntPtr impl);

		// Token: 0x060010EA RID: 4330 RVA: 0x0004AAEC File Offset: 0x00048CEC
		private void UpdateInfo()
		{
			if (this.impl != IntPtr.Zero)
			{
				this.Close();
			}
			this.impl = PerformanceCounter.GetImpl(this.categoryName, this.counterName, this.instanceName, this.machineName, out this.type, out this.is_custom);
			if (!this.is_custom)
			{
				this.readOnly = true;
			}
			this.changed = false;
		}

		/// <summary>Gets or sets the name of the performance counter category for this performance counter.</summary>
		/// <returns>The name of the performance counter category (performance object) with which this performance counter is associated.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CategoryName" /> is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0004AB56 File Offset: 0x00048D56
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0004AB5E File Offset: 0x00048D5E
		[SRDescription("The category name for this performance counter.")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CategoryValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[ReadOnly(true)]
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("categoryName");
				}
				this.categoryName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets the description for this performance counter.</summary>
		/// <returns>A description of the item or quantity that this performance counter measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is not associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0004AB7C File Offset: 0x00048D7C
		[ReadOnly(true)]
		[MonitoringDescription("A description describing the counter.")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CounterHelp
		{
			get
			{
				return "";
			}
		}

		/// <summary>Gets or sets the name of the performance counter that is associated with this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
		/// <returns>The name of the counter, which generally describes the quantity being counted. This name is displayed in the list of counters of the Performance Counter Manager MMC snap in's Add Counters dialog box.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CounterName" /> is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0004AB83 File Offset: 0x00048D83
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x0004AB8B File Offset: 0x00048D8B
		[DefaultValue("")]
		[ReadOnly(true)]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.CounterNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("The name of this performance counter.")]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("counterName");
				}
				this.counterName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets the counter type of the associated performance counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that describes both how the counter interacts with a monitoring application and the nature of the values it contains (for example, calculated or uncalculated).</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0004ABA9 File Offset: 0x00048DA9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The type of the counter.")]
		public PerformanceCounterType CounterType
		{
			get
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				return this.type;
			}
		}

		/// <summary>Gets or sets the lifetime of a process.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> values. The default is <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Global" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value set is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> enumeration. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> is set after the <see cref="T:System.Diagnostics.PerformanceCounter" /> has been initialized.</exception>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004ABBF File Offset: 0x00048DBF
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x0004ABC7 File Offset: 0x00048DC7
		[MonoTODO]
		[DefaultValue(PerformanceCounterInstanceLifetime.Global)]
		public PerformanceCounterInstanceLifetime InstanceLifetime
		{
			get
			{
				return this.lifetime;
			}
			set
			{
				this.lifetime = value;
			}
		}

		/// <summary>Gets or sets an instance name for this performance counter.</summary>
		/// <returns>The name of the performance counter category instance, or an empty string (""), if the counter is a single-instance counter.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004ABD0 File Offset: 0x00048DD0
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0004ABD8 File Offset: 0x00048DD8
		[DefaultValue("")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.InstanceNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("The instance name for this performance counter.")]
		[ReadOnly(true)]
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.instanceName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets or sets the computer name for this performance counter </summary>
		/// <returns>The server on which the performance counter and its associated category reside.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounter.MachineName" /> format is invalid. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004ABF6 File Offset: 0x00048DF6
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0004AC00 File Offset: 0x00048E00
		[MonoTODO("What's the machine name format?")]
		[Browsable(false)]
		[SettingsBindable(true)]
		[SRDescription("The machine where this performance counter resides.")]
		[DefaultValue(".")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "" || value == ".")
				{
					this.machineName = ".";
					this.changed = true;
					return;
				}
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Gets or sets the raw, or uncalculated, value of this counter.</summary>
		/// <returns>The raw value of the counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">You are trying to set the counter's raw value, but the counter is read-only.-or- The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0004AC50 File Offset: 0x00048E50
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0004AC81 File Offset: 0x00048E81
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The raw value of the counter.")]
		public long RawValue
		{
			get
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				CounterSample counterSample;
				PerformanceCounter.GetSample(this.impl, true, out counterSample);
				return counterSample.RawValue;
			}
			set
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				if (this.readOnly)
				{
					throw new InvalidOperationException();
				}
				PerformanceCounter.UpdateValue(this.impl, false, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode.</summary>
		/// <returns>true, if the <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode (even if the counter itself is a custom .NET Framework counter); false if it is in read/write mode. The default is the value set by the constructor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0004ACAD File Offset: 0x00048EAD
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x0004ACB5 File Offset: 0x00048EB5
		[Browsable(false)]
		[DefaultValue(true)]
		[MonitoringDescription("The accessability level of the counter.")]
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance used on a form or by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010FB RID: 4347 RVA: 0x000027E8 File Offset: 0x000009E8
		public void BeginInit()
		{
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance that is used on a form or by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060010FC RID: 4348 RVA: 0x000027E8 File Offset: 0x000009E8
		public void EndInit()
		{
		}

		/// <summary>Closes the performance counter and frees all the resources allocated by this performance counter instance.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010FD RID: 4349 RVA: 0x0004ACC0 File Offset: 0x00048EC0
		public void Close()
		{
			IntPtr intPtr = this.impl;
			this.impl = IntPtr.Zero;
			if (intPtr != IntPtr.Zero)
			{
				PerformanceCounter.FreeData(intPtr);
			}
		}

		/// <summary>Frees the performance counter library shared state allocated by the counters.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1">
		///     <Machine name=".">
		///       <Category name="*" access="Browse" />
		///     </Machine>
		///   </IPermission>
		/// </PermissionSet>
		// Token: 0x060010FE RID: 4350 RVA: 0x000027E8 File Offset: 0x000009E8
		public static void CloseSharedResources()
		{
		}

		/// <summary>Decrements the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The decremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot decrement it.-or- The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060010FF RID: 4351 RVA: 0x0004ACF2 File Offset: 0x00048EF2
		public long Decrement()
		{
			return this.IncrementBy(-1L);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0004ACFC File Offset: 0x00048EFC
		protected override void Dispose(bool disposing)
		{
			this.Close();
		}

		/// <summary>Increments the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The incremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.-or- The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001101 RID: 4353 RVA: 0x0004AD04 File Offset: 0x00048F04
		public long Increment()
		{
			return this.IncrementBy(1L);
		}

		/// <summary>Increments or decrements the value of the associated performance counter by a specified amount through an efficient atomic operation.</summary>
		/// <returns>The new counter value.</returns>
		/// <param name="value">The value to increment by. (A negative value decrements the counter.) </param>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.-or- The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001102 RID: 4354 RVA: 0x0004AD0E File Offset: 0x00048F0E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public long IncrementBy(long value)
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			if (this.readOnly)
			{
				return 0L;
			}
			return PerformanceCounter.UpdateValue(this.impl, true, value);
		}

		/// <summary>Obtains a counter sample, and returns the raw, or uncalculated, value for it.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> that represents the next raw value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001103 RID: 4355 RVA: 0x0004AD38 File Offset: 0x00048F38
		public CounterSample NextSample()
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			CounterSample counterSample;
			PerformanceCounter.GetSample(this.impl, false, out counterSample);
			this.valid_old = true;
			this.old_sample = counterSample;
			return counterSample;
		}

		/// <summary>Obtains a counter sample and returns the calculated value for it.</summary>
		/// <returns>The next calculated value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001104 RID: 4356 RVA: 0x0004AD74 File Offset: 0x00048F74
		public float NextValue()
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			CounterSample counterSample;
			PerformanceCounter.GetSample(this.impl, false, out counterSample);
			float num;
			if (this.valid_old)
			{
				num = CounterSampleCalculator.ComputeCounterValue(this.old_sample, counterSample);
			}
			else
			{
				num = CounterSampleCalculator.ComputeCounterValue(counterSample);
			}
			this.valid_old = true;
			this.old_sample = counterSample;
			return num;
		}

		/// <summary>Deletes the category instance specified by the <see cref="T:System.Diagnostics.PerformanceCounter" /> object <see cref="P:System.Diagnostics.PerformanceCounter.InstanceName" /> property.</summary>
		/// <exception cref="T:System.InvalidOperationException">This counter is read-only, so any instance that is associated with the category cannot be removed.-or- The instance is not correctly associated with a performance counter. -or-The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" />  when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001105 RID: 4357 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void RemoveInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001186 RID: 4486
		private string categoryName;

		// Token: 0x04001187 RID: 4487
		private string counterName;

		// Token: 0x04001188 RID: 4488
		private string instanceName;

		// Token: 0x04001189 RID: 4489
		private string machineName;

		// Token: 0x0400118A RID: 4490
		private IntPtr impl;

		// Token: 0x0400118B RID: 4491
		private PerformanceCounterType type;

		// Token: 0x0400118C RID: 4492
		private CounterSample old_sample;

		// Token: 0x0400118D RID: 4493
		private bool readOnly;

		// Token: 0x0400118E RID: 4494
		private bool valid_old;

		// Token: 0x0400118F RID: 4495
		private bool changed;

		// Token: 0x04001190 RID: 4496
		private bool is_custom;

		// Token: 0x04001191 RID: 4497
		private PerformanceCounterInstanceLifetime lifetime;

		/// <summary>Specifies the size, in bytes, of the global memory shared by performance counters. The default size is 524,288 bytes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001192 RID: 4498
		[Obsolete]
		public static int DefaultFileMappingSize = 524288;
	}
}
