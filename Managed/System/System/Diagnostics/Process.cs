using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Provides access to local and remote processes and enables you to start and stop local system processes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D3 RID: 467
	[DefaultEvent("Exited")]
	[DefaultProperty("StartInfo")]
	[Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonitoringDescription("Provides access to local and remote processes, enabling starting and stopping of local processes.")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true, ExternalProcessMgmt = true, SelfAffectingProcessMgmt = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class Process : Component
	{
		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000E64 RID: 3684 RVA: 0x000443F4 File Offset: 0x000425F4
		// (remove) Token: 0x06000E65 RID: 3685 RVA: 0x0004442C File Offset: 0x0004262C
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[Browsable(true)]
		public event DataReceivedEventHandler OutputDataReceived;

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000E66 RID: 3686 RVA: 0x00044464 File Offset: 0x00042664
		// (remove) Token: 0x06000E67 RID: 3687 RVA: 0x0004449C File Offset: 0x0004269C
		[Browsable(true)]
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		public event DataReceivedEventHandler ErrorDataReceived;

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Process" /> class.</summary>
		// Token: 0x06000E68 RID: 3688 RVA: 0x000444D1 File Offset: 0x000426D1
		public Process()
		{
			this.machineName = ".";
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000444FD File Offset: 0x000426FD
		private Process(string machineName, bool isRemoteMachine, int processId, ProcessInfo processInfo)
		{
			this.machineName = machineName;
			this.isRemoteMachine = isRemoteMachine;
			this.processId = processId;
			this.haveProcessId = true;
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0004453A File Offset: 0x0004273A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[Browsable(false)]
		private bool Associated
		{
			get
			{
				return this.haveProcessId || this.haveProcessHandle;
			}
		}

		/// <summary>Gets the value that the associated process specified when it terminated.</summary>
		/// <returns>The code that the associated process specified when it terminated.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not exited.-or- The process <see cref="P:System.Diagnostics.Process.Handle" /> is not valid. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitCode" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0004454C File Offset: 0x0004274C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The value returned from the associated process when it terminated.")]
		[Browsable(false)]
		public int ExitCode
		{
			get
			{
				this.EnsureState(Process.State.Exited);
				if (this.exitCode == -1 && !Environment.IsRunningOnWindows)
				{
					throw new InvalidOperationException("Cannot get the exit code from a non-child process on Unix");
				}
				return this.exitCode;
			}
		}

		/// <summary>Gets a value indicating whether the associated process has been terminated.</summary>
		/// <returns>true if the operating system process referenced by the <see cref="T:System.Diagnostics.Process" /> component has terminated; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with the object. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The exit code for the process could not be retrieved. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.HasExited" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00044578 File Offset: 0x00042778
		[MonitoringDescription("Indicates if the associated process has been terminated.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool HasExited
		{
			get
			{
				if (!this.exited)
				{
					this.EnsureState(Process.State.Associated);
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1049600, false);
						int num;
						if (safeProcessHandle.IsInvalid)
						{
							this.exited = true;
						}
						else if (Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out num) && num != 259)
						{
							this.exited = true;
							this.exitCode = num;
						}
						else
						{
							if (!this.signaled)
							{
								ProcessWaitHandle processWaitHandle = null;
								try
								{
									processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
									this.signaled = processWaitHandle.WaitOne(0, false);
								}
								finally
								{
									if (processWaitHandle != null)
									{
										processWaitHandle.Close();
									}
								}
							}
							if (this.signaled)
							{
								if (!Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
								{
									throw new Win32Exception();
								}
								this.exited = true;
								this.exitCode = num;
							}
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					if (this.exited)
					{
						this.RaiseOnExited();
					}
				}
				return this.exited;
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00044668 File Offset: 0x00042868
		private ProcessThreadTimes GetProcessTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				int num = 1024;
				if (EnvironmentHelpers.IsWindowsVistaOrAbove())
				{
					num = 4096;
				}
				safeProcessHandle = this.GetProcessHandle(num, false);
				if (safeProcessHandle.IsInvalid)
				{
					throw new InvalidOperationException(global::SR.GetString("Cannot process request because the process ({0}) has exited.", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
				}
				if (!Microsoft.Win32.NativeMethods.GetProcessTimes(safeProcessHandle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return processThreadTimes;
		}

		/// <summary>Gets the time that the associated process exited.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates when the associated process was terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0004470C File Offset: 0x0004290C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The time that the associated process exited.")]
		public DateTime ExitTime
		{
			get
			{
				if (!this.haveExitTime)
				{
					this.EnsureState((Process.State)20);
					this.exitTime = this.GetProcessTimes().ExitTime;
					this.haveExitTime = true;
				}
				return this.exitTime;
			}
		}

		/// <summary>Gets the native handle of the associated process.</summary>
		/// <returns>The handle that the operating system assigned to the associated process when the process was started. The system uses this handle to keep track of process attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not been started or has exited. The <see cref="P:System.Diagnostics.Process.Handle" /> property cannot be read because there is no process associated with this <see cref="T:System.Diagnostics.Process" /> instance.-or- The <see cref="T:System.Diagnostics.Process" /> instance has been attached to a running process but you do not have the necessary permissions to get a handle with full access rights. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.Handle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0004473C File Offset: 0x0004293C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[MonitoringDescription("Returns the native handle for this process.   The handle is only available if the process was started using this component.")]
		public IntPtr Handle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess).DangerousGetHandle();
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00044757 File Offset: 0x00042957
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SafeProcessHandle SafeHandle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess);
			}
		}

		/// <summary>Gets the unique identifier for the associated process.</summary>
		/// <returns>The system-generated unique identifier of the process that is referenced by this <see cref="T:System.Diagnostics.Process" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process's <see cref="P:System.Diagnostics.Process.Id" /> property has not been set.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0004476D File Offset: 0x0004296D
		[MonitoringDescription("The unique identifier for the process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Id
		{
			get
			{
				this.EnsureState(Process.State.HaveId);
				return this.processId;
			}
		}

		/// <summary>Gets the name of the computer the associated process is running on.</summary>
		/// <returns>The name of the computer that the associated process is running on.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0004477C File Offset: 0x0004297C
		[MonitoringDescription("The name of the machine the running the process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string MachineName
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.machineName;
			}
		}

		/// <summary>Gets or sets the maximum allowable working set size for the associated process.</summary>
		/// <returns>The maximum working set size that is allowed in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum working set size is invalid. It must be greater than or equal to the minimum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.-or- The process identifier or process handle is zero because the process has not been started. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MaxWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0004478C File Offset: 0x0004298C
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0004479A File Offset: 0x0004299A
		[MonitoringDescription("The maximum amount of physical memory the process has required since it was started.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr MaxWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.maxWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(null, value);
			}
		}

		/// <summary>Gets or sets the minimum allowable working set size for the associated process.</summary>
		/// <returns>The minimum working set size that is required in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The minimum working set size is invalid. It must be less than or equal to the maximum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.-or- The process identifier or process handle is zero because the process has not been started. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MinWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000447A9 File Offset: 0x000429A9
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x000447B7 File Offset: 0x000429B7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The minimum amount of physical memory the process has required since it was started.")]
		public IntPtr MinWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.minWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(value, null);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000447C6 File Offset: 0x000429C6
		private OperatingSystem OperatingSystem
		{
			get
			{
				if (this.operatingSystem == null)
				{
					this.operatingSystem = Environment.OSVersion;
				}
				return this.operatingSystem;
			}
		}

		/// <summary>Gets or sets the overall priority category for the associated process.</summary>
		/// <returns>The priority category for the associated process, from which the <see cref="P:System.Diagnostics.Process.BasePriority" /> of the process is calculated.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Process priority information could not be set or retrieved from the associated process resource.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityClass" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">You have set the <see cref="P:System.Diagnostics.Process.PriorityClass" /> to AboveNormal or BelowNormal when using Windows 98 or Windows Millennium Edition (Windows Me). These platforms do not support those values for the priority class. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Priority class cannot be set because it does not use a valid value, as defined in the <see cref="T:System.Diagnostics.ProcessPriorityClass" /> enumeration.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000447E4 File Offset: 0x000429E4
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x00044848 File Offset: 0x00042A48
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The priority that the threads in the process run relative to.")]
		public ProcessPriorityClass PriorityClass
		{
			get
			{
				if (!this.havePriorityClass)
				{
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						int num = Microsoft.Win32.NativeMethods.GetPriorityClass(safeProcessHandle);
						if (num == 0)
						{
							throw new Win32Exception();
						}
						this.priorityClass = (ProcessPriorityClass)num;
						this.havePriorityClass = true;
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.priorityClass;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessPriorityClass), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessPriorityClass));
				}
				SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(512);
					if (!Microsoft.Win32.NativeMethods.SetPriorityClass(safeProcessHandle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityClass = value;
					this.havePriorityClass = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Gets the privileged processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the process has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000448C8 File Offset: 0x00042AC8
		[MonitoringDescription("The amount of CPU time the process spent inside the operating system core.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().PrivilegedProcessorTime;
			}
		}

		/// <summary>Gets or sets the properties to pass to the <see cref="M:System.Diagnostics.Process.Start" /> method of the <see cref="T:System.Diagnostics.Process" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that represents the data with which to start the process. These arguments include the name of the executable file or document used to start the process.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value that specifies the <see cref="P:System.Diagnostics.Process.StartInfo" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000448DC File Offset: 0x00042ADC
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000448F8 File Offset: 0x00042AF8
		[MonitoringDescription("Specifies information used to start a process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.startInfo == null)
				{
					this.startInfo = new ProcessStartInfo(this);
				}
				return this.startInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.startInfo = value;
			}
		}

		/// <summary>Gets the time that the associated process was started.</summary>
		/// <returns>An object  that indicates when the process started. An exception is thrown if the process is not running.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.StartTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.-or-The process has not been started.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred in the call to the Windows function.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0004490F File Offset: 0x00042B0F
		[MonitoringDescription("The time at which the process was started.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().StartTime;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls that are issued as a result of a process exit event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event handler calls that are issued as a result of an <see cref="E:System.Diagnostics.Process.Exited" /> event on the process.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00044924 File Offset: 0x00042B24
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0004497E File Offset: 0x00042B7E
		[Browsable(false)]
		[MonitoringDescription("The object used to marshal the event handler calls issued as a result of a Process exit.")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the total processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent utilizing the CPU. This value is the sum of the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> and the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.TotalProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00044987 File Offset: 0x00042B87
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of CPU time the process has used.")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().TotalProcessorTime;
			}
		}

		/// <summary>Gets the user processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent running code inside the application portion of the process (not inside the operating system core).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0004499B File Offset: 0x00042B9B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of CPU time the process spent outside the operating system core.")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().UserProcessorTime;
			}
		}

		/// <summary>Gets or sets whether the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the process terminates.</summary>
		/// <returns>true if the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the associated process is terminated (through either an exit or a call to <see cref="M:System.Diagnostics.Process.Kill" />); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x000449AF File Offset: 0x00042BAF
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x000449B7 File Offset: 0x00042BB7
		[Browsable(false)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether the process component should watch for the associated process to exit, and raise the Exited event.")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.watchForExit;
			}
			set
			{
				if (value != this.watchForExit)
				{
					if (this.Associated)
					{
						if (value)
						{
							this.OpenProcessHandle();
							this.EnsureWatchingForExit();
						}
						else
						{
							this.StopWatchingForExit();
						}
					}
					this.watchForExit = value;
				}
			}
		}

		/// <summary>Gets a stream used to write the input of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that can be used to write the standard input stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardInput" /> stream has not been defined because <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" /> is set to false. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x000449E9 File Offset: 0x00042BE9
		[Browsable(false)]
		[MonitoringDescription("Standard input stream of the process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StreamWriter StandardInput
		{
			get
			{
				if (this.standardInput == null)
				{
					throw new InvalidOperationException(global::SR.GetString("StandardIn has not been redirected."));
				}
				this.inputStreamReadMode = Process.StreamReadMode.syncMode;
				return this.standardInput;
			}
		}

		/// <summary>Gets a stream used to read the output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard output stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> is set to true and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to false.- or - The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginOutputReadLine" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00044A10 File Offset: 0x00042C10
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Standard output stream of the process.")]
		[Browsable(false)]
		public StreamReader StandardOutput
		{
			get
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(global::SR.GetString("StandardOut has not been redirected or the process hasn't started yet."));
				}
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.outputStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.outputStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(global::SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
				}
				return this.standardOutput;
			}
		}

		/// <summary>Gets a stream used to read the error output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard error stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is set to true and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to false.- or - The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginErrorReadLine" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00044A68 File Offset: 0x00042C68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Standard error stream of the process.")]
		[Browsable(false)]
		public StreamReader StandardError
		{
			get
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(global::SR.GetString("StandardError has not been redirected."));
				}
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.errorStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.errorStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(global::SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
				}
				return this.standardError;
			}
		}

		/// <summary>Occurs when a process exits.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000E87 RID: 3719 RVA: 0x00044ABD File Offset: 0x00042CBD
		// (remove) Token: 0x06000E88 RID: 3720 RVA: 0x00044AD6 File Offset: 0x00042CD6
		[MonitoringDescription("If the WatchForExit property is set to true, then this event is raised when the associated process exits.")]
		[Category("Behavior")]
		public event EventHandler Exited
		{
			add
			{
				this.onExited = (EventHandler)Delegate.Combine(this.onExited, value);
			}
			remove
			{
				this.onExited = (EventHandler)Delegate.Remove(this.onExited, value);
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00044AEF File Offset: 0x00042CEF
		private void ReleaseProcessHandle(SafeProcessHandle handle)
		{
			if (handle == null)
			{
				return;
			}
			if (this.haveProcessHandle && handle == this.m_processHandle)
			{
				return;
			}
			handle.Close();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00044B0D File Offset: 0x00042D0D
		private void CompletionCallback(object context, bool wasSignaled)
		{
			this.StopWatchingForExit();
			this.RaiseOnExited();
		}

		/// <summary>Release all resources used by this process.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000E8B RID: 3723 RVA: 0x00044B1B File Offset: 0x00042D1B
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>Frees all the resources that are associated with this component.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E8C RID: 3724 RVA: 0x00044B3C File Offset: 0x00042D3C
		public void Close()
		{
			if (this.Associated)
			{
				if (this.haveProcessHandle)
				{
					this.StopWatchingForExit();
					this.m_processHandle.Close();
					this.m_processHandle = null;
					this.haveProcessHandle = false;
				}
				this.haveProcessId = false;
				this.isRemoteMachine = false;
				this.machineName = ".";
				this.raisedOnExited = false;
				StreamWriter streamWriter = this.standardInput;
				this.standardInput = null;
				if (this.inputStreamReadMode == Process.StreamReadMode.undefined && streamWriter != null)
				{
					streamWriter.Close();
				}
				StreamReader streamReader = this.standardOutput;
				this.standardOutput = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				streamReader = this.standardError;
				this.standardError = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				AsyncStreamReader asyncStreamReader = this.output;
				this.output = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				asyncStreamReader = this.error;
				this.error = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				this.Refresh();
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00044C4C File Offset: 0x00042E4C
		private void EnsureState(Process.State state)
		{
			if ((state & Process.State.Associated) != (Process.State)0 && !this.Associated)
			{
				throw new InvalidOperationException(global::SR.GetString("No process is associated with this object."));
			}
			if ((state & Process.State.HaveId) != (Process.State)0 && !this.haveProcessId)
			{
				this.EnsureState(Process.State.Associated);
				throw new InvalidOperationException(global::SR.GetString("Feature requires a process identifier."));
			}
			if ((state & Process.State.IsLocal) != (Process.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(global::SR.GetString("Feature is not supported for remote machines."));
			}
			if ((state & Process.State.HaveProcessInfo) != (Process.State)0)
			{
				throw new InvalidOperationException(global::SR.GetString("Process has exited, so the requested information is not available."));
			}
			if ((state & Process.State.Exited) != (Process.State)0)
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(global::SR.GetString("Process must exit before requested information can be determined."));
				}
				if (!this.haveProcessHandle)
				{
					throw new InvalidOperationException(global::SR.GetString("Process was not started by this object, so requested information cannot be determined."));
				}
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00044D04 File Offset: 0x00042F04
		private void EnsureWatchingForExit()
		{
			if (!this.watchingForExit)
			{
				lock (this)
				{
					if (!this.watchingForExit)
					{
						this.watchingForExit = true;
						try
						{
							this.waitHandle = new ProcessWaitHandle(this.m_processHandle);
							this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.waitHandle, new WaitOrTimerCallback(this.CompletionCallback), null, -1, true);
						}
						catch
						{
							this.watchingForExit = false;
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00044D9C File Offset: 0x00042F9C
		private void EnsureWorkingSetLimits()
		{
			this.EnsureState(Process.State.IsNt);
			if (!this.haveWorkingSetLimits)
			{
				SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(1024);
					IntPtr intPtr;
					IntPtr intPtr2;
					if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Puts a <see cref="T:System.Diagnostics.Process" /> component in state to interact with operating system processes that run in a special mode by enabling the native property SeDebugPrivilege on the current thread.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E90 RID: 3728 RVA: 0x000027E8 File Offset: 0x000009E8
		public static void EnterDebugMode()
		{
		}

		/// <summary>Takes a <see cref="T:System.Diagnostics.Process" /> component out of the state that lets it interact with operating system processes that run in a special mode.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E91 RID: 3729 RVA: 0x000027E8 File Offset: 0x000009E8
		public static void LeaveDebugMode()
		{
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given the identifier of a process on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with the local process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <param name="processId">The system-unique identifier of a process resource. </param>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E92 RID: 3730 RVA: 0x00044E08 File Offset: 0x00043008
		public static Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId, ".");
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on the local computer that share the specified process name.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <param name="processName">The friendly name of the process. </param>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E93 RID: 3731 RVA: 0x00044E15 File Offset: 0x00043015
		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName, ".");
		}

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the local computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the local computer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E94 RID: 3732 RVA: 0x00044E22 File Offset: 0x00043022
		public static Process[] GetProcesses()
		{
			return Process.GetProcesses(".");
		}

		/// <summary>Gets a new <see cref="T:System.Diagnostics.Process" /> component and associates it with the currently active process.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component associated with the process resource that is running the calling application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E95 RID: 3733 RVA: 0x00044E2E File Offset: 0x0004302E
		public static Process GetCurrentProcess()
		{
			return new Process(".", false, Microsoft.Win32.NativeMethods.GetCurrentProcessId(), null);
		}

		/// <summary>Raises the <see cref="E:System.Diagnostics.Process.Exited" /> event.</summary>
		// Token: 0x06000E96 RID: 3734 RVA: 0x00044E44 File Offset: 0x00043044
		protected void OnExited()
		{
			EventHandler eventHandler = this.onExited;
			if (eventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(eventHandler, new object[]
					{
						this,
						EventArgs.Empty
					});
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00044E9C File Offset: 0x0004309C
		private SafeProcessHandle GetProcessHandle(int access, bool throwIfExited)
		{
			if (this.haveProcessHandle)
			{
				if (throwIfExited)
				{
					ProcessWaitHandle processWaitHandle = null;
					try
					{
						processWaitHandle = new ProcessWaitHandle(this.m_processHandle);
						if (processWaitHandle.WaitOne(0, false))
						{
							if (this.haveProcessId)
							{
								throw new InvalidOperationException(global::SR.GetString("Cannot process request because the process ({0}) has exited.", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
							}
							throw new InvalidOperationException(global::SR.GetString("Cannot process request because the process has exited."));
						}
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
				}
				return this.m_processHandle;
			}
			this.EnsureState((Process.State)3);
			SafeProcessHandle invalidHandle = SafeProcessHandle.InvalidHandle;
			IntPtr currentProcess = Microsoft.Win32.NativeMethods.GetCurrentProcess();
			if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), out invalidHandle, 0, false, 3))
			{
				throw new Win32Exception();
			}
			if (throwIfExited && (access & 1024) != 0 && Microsoft.Win32.NativeMethods.GetExitCodeProcess(invalidHandle, out this.exitCode) && this.exitCode != 259)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot process request because the process ({0}) has exited.", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
			}
			return invalidHandle;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00044FBC File Offset: 0x000431BC
		private SafeProcessHandle GetProcessHandle(int access)
		{
			return this.GetProcessHandle(access, true);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00044FC6 File Offset: 0x000431C6
		private SafeProcessHandle OpenProcessHandle()
		{
			return this.OpenProcessHandle(2035711);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00044FD3 File Offset: 0x000431D3
		private SafeProcessHandle OpenProcessHandle(int access)
		{
			if (!this.haveProcessHandle)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.SetProcessHandle(this.GetProcessHandle(access));
			}
			return this.m_processHandle;
		}

		/// <summary>Discards any information about the associated process that has been cached inside the process component.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E9B RID: 3739 RVA: 0x00045009 File Offset: 0x00043209
		public void Refresh()
		{
			this.threads = null;
			this.modules = null;
			this.exited = false;
			this.signaled = false;
			this.haveWorkingSetLimits = false;
			this.havePriorityClass = false;
			this.haveExitTime = false;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0004503C File Offset: 0x0004323C
		private void SetProcessHandle(SafeProcessHandle processHandle)
		{
			this.m_processHandle = processHandle;
			this.haveProcessHandle = true;
			if (this.watchForExit)
			{
				this.EnsureWatchingForExit();
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0004505A File Offset: 0x0004325A
		private void SetProcessId(int processId)
		{
			this.processId = processId;
			this.haveProcessId = true;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004506C File Offset: 0x0004326C
		private void SetWorkingSetLimits(object newMin, object newMax)
		{
			this.EnsureState(Process.State.IsNt);
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1280);
				IntPtr intPtr;
				IntPtr intPtr2;
				if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
				{
					throw new Win32Exception();
				}
				if (newMin != null)
				{
					intPtr = (IntPtr)newMin;
				}
				if (newMax != null)
				{
					intPtr2 = (IntPtr)newMax;
				}
				if ((long)intPtr > (long)intPtr2)
				{
					if (newMin != null)
					{
						throw new ArgumentException(global::SR.GetString("Minimum working set size is invalid. It must be less than or equal to the maximum working set size."));
					}
					throw new ArgumentException(global::SR.GetString("Maximum working set size is invalid. It must be greater than or equal to the minimum working set size."));
				}
				else
				{
					if (!Microsoft.Win32.NativeMethods.SetProcessWorkingSetSize(safeProcessHandle, intPtr, intPtr2))
					{
						throw new Win32Exception();
					}
					if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		/// <summary>Starts (or reuses) the process resource that is specified by the <see cref="P:System.Diagnostics.Process.StartInfo" /> property of this <see cref="T:System.Diagnostics.Process" /> component and associates it with the component.</summary>
		/// <returns>true if a process resource is started; false if no new process resource is started (for example, if an existing process is reused).</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the <see cref="P:System.Diagnostics.Process.StartInfo" /> property is true while <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is true. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E9F RID: 3743 RVA: 0x00045138 File Offset: 0x00043338
		public bool Start()
		{
			this.Close();
			ProcessStartInfo processStartInfo = this.StartInfo;
			if (processStartInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot start process because a file name has not been provided."));
			}
			if (processStartInfo.UseShellExecute)
			{
				return this.StartWithShellExecuteEx(processStartInfo);
			}
			return this.StartWithCreateProcess(processStartInfo);
		}

		/// <summary>Starts a process resource by specifying the name of an application, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="fileName" /> is not an executable (.exe) file.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00045186 File Offset: 0x00043386
		public static Process Start(string fileName, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of an application, a set of command-line arguments, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process. </param>
		/// <param name="arguments">Command-line arguments to pass when starting the process. </param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="fileName" /> is not an executable (.exe) file.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the associated file exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000451AF File Offset: 0x000433AF
		public static Process Start(string fileName, string arguments, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of a document or application file and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null, if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of a document or application file to run in the process. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x000451DA File Offset: 0x000433DA
		public static Process Start(string fileName)
		{
			return Process.Start(new ProcessStartInfo(fileName));
		}

		/// <summary>Starts a process resource by specifying the name of an application and a set of command-line arguments, and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process, or null, if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process. </param>
		/// <param name="arguments">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="fileName" /> or <paramref name="arguments" /> parameter is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x000451E7 File Offset: 0x000433E7
		public static Process Start(string fileName, string arguments)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments));
		}

		/// <summary>Starts the process resource that is specified by the parameter containing process start information (for example, the file name of the process to start) and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="startInfo">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments. </param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is also true.-or-The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property is not null or empty or the <see cref="P:System.Diagnostics.ProcessStartInfo.Password" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="startInfo" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property could not be found.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000451F8 File Offset: 0x000433F8
		public static Process Start(ProcessStartInfo startInfo)
		{
			Process process = new Process();
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			process.StartInfo = startInfo;
			if (process.Start())
			{
				return process;
			}
			return null;
		}

		/// <summary>Immediately stops the associated process.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The associated process could not be terminated. -or-The process is terminating.-or- The associated process is a Win16 executable.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to call <see cref="M:System.Diagnostics.Process.Kill" /> for a process that is running on a remote computer. The method is available only for processes running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited. -or-There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0004522C File Offset: 0x0004342C
		public void Kill()
		{
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				if (!Microsoft.Win32.NativeMethods.TerminateProcess(safeProcessHandle, -1))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004526C File Offset: 0x0004346C
		private void StopWatchingForExit()
		{
			if (this.watchingForExit)
			{
				lock (this)
				{
					if (this.watchingForExit)
					{
						this.watchingForExit = false;
						this.registeredWaitHandle.Unregister(null);
						this.waitHandle.Close();
						this.waitHandle = null;
						this.registeredWaitHandle = null;
					}
				}
			}
		}

		/// <summary>Formats the process's name as a string, combined with the parent component type, if applicable.</summary>
		/// <returns>The <see cref="P:System.Diagnostics.Process.ProcessName" />, combined with the base component's <see cref="M:System.Object.ToString" /> return value.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="M:System.Diagnostics.Process.ToString" /> is not supported on Windows 98.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x000452E0 File Offset: 0x000434E0
		public override string ToString()
		{
			if (!this.Associated)
			{
				return base.ToString();
			}
			string text = string.Empty;
			try
			{
				text = this.ProcessName;
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (text.Length != 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", base.ToString(), text);
			}
			return base.ToString();
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to exit.</summary>
		/// <returns>true if the associated process has exited; otherwise, false.</returns>
		/// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit(System.Int32)" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00045344 File Offset: 0x00043544
		public bool WaitForExit(int milliseconds)
		{
			SafeProcessHandle safeProcessHandle = null;
			ProcessWaitHandle processWaitHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1048576, false);
				if (safeProcessHandle.IsInvalid)
				{
					flag = true;
				}
				else
				{
					processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
					if (processWaitHandle.WaitOne(milliseconds, false))
					{
						flag = true;
						this.signaled = true;
					}
					else
					{
						flag = false;
						this.signaled = false;
					}
				}
				if (this.output != null && milliseconds == -1)
				{
					this.output.WaitUtilEOF();
				}
				if (this.error != null && milliseconds == -1)
				{
					this.error.WaitUtilEOF();
				}
			}
			finally
			{
				if (processWaitHandle != null)
				{
					processWaitHandle.Close();
				}
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			if (flag && this.watchForExit)
			{
				this.RaiseOnExited();
			}
			return flag;
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to exit.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x000453F8 File Offset: 0x000435F8
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>true if the associated process has reached an idle state; otherwise, false.</returns>
		/// <param name="milliseconds">A value of 1 to <see cref="F:System.Int32.MaxValue" /> that specifies the amount of time, in milliseconds, to wait for the associated process to become idle. A value of 0 specifies an immediate return, and a value of -1 specifies an infinite wait. </param>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.-or-An unknown error occurred. The process failed to enter an idle state.-or-The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAA RID: 3754 RVA: 0x00045404 File Offset: 0x00043604
		public bool WaitForInputIdle(int milliseconds)
		{
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1049600);
				int num = Microsoft.Win32.NativeMethods.WaitForInputIdle(safeProcessHandle, milliseconds);
				if (num != -1)
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 258)
					{
						return false;
					}
				}
				throw new InvalidOperationException(global::SR.GetString("WaitForInputIdle failed.  This could be because the process does not have a graphical interface."));
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			bool flag;
			return flag;
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>true if the associated process has reached an idle state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.-or-An unknown error occurred. The process failed to enter an idle state.-or-The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAB RID: 3755 RVA: 0x0004546C File Offset: 0x0004366C
		public bool WaitForInputIdle()
		{
			return this.WaitForInputIdle(int.MaxValue);
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EAC RID: 3756 RVA: 0x0004547C File Offset: 0x0004367C
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.outputStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.outputStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingOutputRead)
			{
				throw new InvalidOperationException(global::SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingOutputRead = true;
			if (this.output == null)
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(global::SR.GetString("StandardOut has not been redirected or the process hasn't started yet."));
				}
				Stream baseStream = this.standardOutput.BaseStream;
				this.output = new AsyncStreamReader(this, baseStream, new UserCallBack(this.OutputReadNotifyUser), this.standardOutput.CurrentEncoding);
			}
			this.output.BeginReadLine();
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EAD RID: 3757 RVA: 0x00045530 File Offset: 0x00043730
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.errorStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.errorStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingErrorRead)
			{
				throw new InvalidOperationException(global::SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingErrorRead = true;
			if (this.error == null)
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(global::SR.GetString("StandardError has not been redirected."));
				}
				Stream baseStream = this.standardError.BaseStream;
				this.error = new AsyncStreamReader(this, baseStream, new UserCallBack(this.ErrorReadNotifyUser), this.standardError.CurrentEncoding);
			}
			this.error.BeginReadLine();
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream is not enabled for asynchronous read operations. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EAE RID: 3758 RVA: 0x000455E1 File Offset: 0x000437E1
		[ComVisible(false)]
		public void CancelOutputRead()
		{
			if (this.output != null)
			{
				this.output.CancelOperation();
				this.pendingOutputRead = false;
				return;
			}
			throw new InvalidOperationException(global::SR.GetString("No async read operation is in progress on the stream."));
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream is not enabled for asynchronous read operations. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EAF RID: 3759 RVA: 0x0004560F File Offset: 0x0004380F
		[ComVisible(false)]
		public void CancelErrorRead()
		{
			if (this.error != null)
			{
				this.error.CancelOperation();
				this.pendingErrorRead = false;
				return;
			}
			throw new InvalidOperationException(global::SR.GetString("No async read operation is in progress on the stream."));
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00045640 File Offset: 0x00043840
		internal void OutputReadNotifyUser(string data)
		{
			DataReceivedEventHandler outputDataReceived = this.OutputDataReceived;
			if (outputDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(outputDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				outputDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00045698 File Offset: 0x00043898
		internal void ErrorReadNotifyUser(string data)
		{
			DataReceivedEventHandler errorDataReceived = this.ErrorDataReceived;
			if (errorDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(errorDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				errorDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000456EF File Offset: 0x000438EF
		private Process(SafeProcessHandle handle, int id)
		{
			this.SetProcessHandle(handle);
			this.SetProcessId(id);
		}

		/// <summary>Gets the base priority of the associated process.</summary>
		/// <returns>The base priority, which is computed from the <see cref="P:System.Diagnostics.Process.PriorityClass" /> of the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.-or- The process has not started, so there is no process ID. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Base process priority.")]
		public int BasePriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the number of handles opened by the process.</summary>
		/// <returns>The number of operating system handles the process has opened.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Handles for this process.")]
		public int HandleCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the main module for the associated process.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessModule" /> that was used to start the process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainModule" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A 32-bit process is trying to access the modules of a 64-bit process.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00045705 File Offset: 0x00043905
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[MonitoringDescription("The main module of the process.")]
		public ProcessModule MainModule
		{
			get
			{
				if (this.processId == Microsoft.Win32.NativeMethods.GetCurrentProcessId())
				{
					if (Process.current_main_module == null)
					{
						Process.current_main_module = this.Modules[0];
					}
					return Process.current_main_module;
				}
				return this.Modules[0];
			}
		}

		/// <summary>Gets the window handle of the main window of the associated process.</summary>
		/// <returns>The system-generated window handle of the main window of the associated process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> is not defined because the process has exited. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0004573E File Offset: 0x0004393E
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The handle of the main window of the process.")]
		public IntPtr MainWindowHandle
		{
			get
			{
				return (IntPtr)0;
			}
		}

		/// <summary>Gets the caption of the main window of the process.</summary>
		/// <returns>The main window title of the process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property is not defined because the process has exited. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00013BE5 File Offset: 0x00011DE5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		[MonitoringDescription("The title of the main window of the process.")]
		public string MainWindowTitle
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x06000EB8 RID: 3768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ProcessModule[] GetModules_internal(IntPtr handle);

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00045748 File Offset: 0x00043948
		private ProcessModule[] GetModules_internal(SafeProcessHandle handle)
		{
			bool flag = false;
			ProcessModule[] modules_internal;
			try
			{
				handle.DangerousAddRef(ref flag);
				modules_internal = this.GetModules_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return modules_internal;
		}

		/// <summary>Gets the modules that have been loaded by the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessModule" /> that represents the modules that have been loaded by the associated process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for either the system process or the idle process. These processes do not have modules.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0004578C File Offset: 0x0004398C
		[Browsable(false)]
		[MonitoringDescription("The modules that are loaded as part of this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProcessModuleCollection Modules
		{
			get
			{
				if (this.modules == null)
				{
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						this.modules = new ProcessModuleCollection(this.GetModules_internal(safeProcessHandle));
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.modules;
			}
		}

		// Token: 0x06000EBB RID: 3771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetProcessData(int pid, int data_type, out int error);

		/// <summary>Gets the nonpaged system memory size allocated to this process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO]
		[Obsolete("Use NonpagedSystemMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of bytes that are not pageable.")]
		public int NonpagedSystemMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the paged memory size.</summary>
		/// <returns>The amount of memory, in bytes, allocated by the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000457E4 File Offset: 0x000439E4
		[MonitoringDescription("The number of bytes that are paged.")]
		[Obsolete("Use PagedMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PagedMemorySize
		{
			get
			{
				return (int)this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the paged system memory size.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000457E4 File Offset: 0x000439E4
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use PagedSystemMemorySize64")]
		public int PagedSystemMemorySize
		{
			get
			{
				return (int)this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the peak paged memory size.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated by the associated process that could be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO]
		[Obsolete("Use PeakPagedMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		public int PeakPagedMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the peak virtual memory size.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000457F0 File Offset: 0x000439F0
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		[Obsolete("Use PeakVirtualMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PeakVirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 8, out num);
			}
		}

		/// <summary>Gets the peak working set size for the associated process.</summary>
		/// <returns>The maximum amount of physical memory that the associated process has required all at once, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0004580C File Offset: 0x00043A0C
		[Obsolete("Use PeakWorkingSet64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		public int PeakWorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 5, out num);
			}
		}

		/// <summary>Gets the amount of nonpaged system memory allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00045828 File Offset: 0x00043A28
		[MonoTODO]
		[ComVisible(false)]
		[MonitoringDescription("The number of bytes that are not pageable.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long NonpagedSystemMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the amount of paged memory allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated in the virtual memory paging file for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0004582C File Offset: 0x00043A2C
		[ComVisible(false)]
		[MonitoringDescription("The number of bytes that are paged.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long PagedMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 12, out num);
			}
		}

		/// <summary>Gets the amount of pageable system memory allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00045848 File Offset: 0x00043A48
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		public long PagedSystemMemorySize64
		{
			get
			{
				return this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated in the virtual memory paging file for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00045828 File Offset: 0x00043A28
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		public long PeakPagedMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00045850 File Offset: 0x00043A50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		[ComVisible(false)]
		public long PeakVirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 8, out num);
			}
		}

		/// <summary>Gets the maximum amount of physical memory used by the associated process.</summary>
		/// <returns>The maximum amount of physical memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0004586C File Offset: 0x00043A6C
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		public long PeakWorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 5, out num);
			}
		}

		/// <summary>Gets or sets a value indicating whether the associated process priority should temporarily be boosted by the operating system when the main window has the focus.</summary>
		/// <returns>true if dynamic boosting of the process priority should take place for a process when it is taken out of the wait state; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Priority boost information could not be retrieved from the associated process resource. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityBoostEnabled" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x000027E8 File Offset: 0x000009E8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		[MonitoringDescription("Process will be of higher priority while it is actively used.")]
		public bool PriorityBoostEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets the private memory size.</summary>
		/// <returns>The number of bytes allocated by the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00045888 File Offset: 0x00043A88
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use PrivateMemorySize64")]
		public int PrivateMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 6, out num);
			}
		}

		/// <summary>Gets the Terminal Services session identifier for the associated process.</summary>
		/// <returns>The Terminal Services session identifier for the associated process.</returns>
		/// <exception cref="T:System.NullReferenceException">There is no session associated with this process.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this session identifier.-or-The associated process is not on this machine. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Diagnostics.Process.SessionId" /> property is not supported on Windows 98.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00004240 File Offset: 0x00002440
		[MonoNotSupported("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The session ID for this process.")]
		public int SessionId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000ECC RID: 3788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ProcessName_internal(IntPtr handle);

		// Token: 0x06000ECD RID: 3789 RVA: 0x000458A4 File Offset: 0x00043AA4
		private static string ProcessName_internal(SafeProcessHandle handle)
		{
			bool flag = false;
			string text;
			try
			{
				handle.DangerousAddRef(ref flag);
				text = Process.ProcessName_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return text;
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name that the system uses to identify the process to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have an identifier, or no process is associated with the <see cref="T:System.Diagnostics.Process" />.-or- The associated process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is not on this computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000458E4 File Offset: 0x00043AE4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of this process.")]
		public string ProcessName
		{
			get
			{
				if (this.process_name == null)
				{
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						this.process_name = Process.ProcessName_internal(safeProcessHandle);
						if (this.process_name == null)
						{
							throw new InvalidOperationException("Process has exited or is inaccessible, so the requested information is not available.");
						}
						if (this.process_name.EndsWith(".exe") || this.process_name.EndsWith(".bat") || this.process_name.EndsWith(".com"))
						{
							this.process_name = this.process_name.Substring(0, this.process_name.Length - 4);
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.process_name;
			}
		}

		/// <summary>Gets or sets the processors on which the threads in this process can be scheduled to run.</summary>
		/// <returns>A bitmask representing the processors that the threads in the associated process can run on. The default depends on the number of processors on the computer. The default value is 2 n -1, where n is the number of processors.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> information could not be set or retrieved from the associated process resource.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> was not available.-or- The process has exited. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0004573E File Offset: 0x0004393E
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Allowed processor that can be used by this process.")]
		public IntPtr ProcessorAffinity
		{
			get
			{
				return (IntPtr)0;
			}
			set
			{
			}
		}

		/// <summary>Gets a value indicating whether the user interface of the process is responding.</summary>
		/// <returns>true if the user interface of the associated process is responding to the system; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Responding" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00004240 File Offset: 0x00002440
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Is this process responsive.")]
		[MonoTODO]
		public bool Responding
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the set of threads that are running in the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessThread" /> representing the operating system threads currently running in the associated process.</returns>
		/// <exception cref="T:System.SystemException">The process does not have an <see cref="P:System.Diagnostics.Process.Id" />, or no process is associated with the <see cref="T:System.Diagnostics.Process" /> instance.-or- The associated process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000459A0 File Offset: 0x00043BA0
		[MonitoringDescription("The number of threads of this process.")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProcessThreadCollection Threads
		{
			get
			{
				if (this.threads == null)
				{
					int num;
					this.threads = new ProcessThreadCollection(new ProcessThread[Process.GetProcessData(this.processId, 0, out num)]);
				}
				return this.threads;
			}
		}

		/// <summary>Gets the size of the process's virtual memory.</summary>
		/// <returns>The amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x000459DC File Offset: 0x00043BDC
		[Obsolete("Use VirtualMemorySize64")]
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 7, out num);
			}
		}

		/// <summary>Gets the associated process's physical memory usage.</summary>
		/// <returns>The total amount of physical memory the associated process is using, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000459F8 File Offset: 0x00043BF8
		[Obsolete("Use WorkingSet64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		public int WorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 4, out num);
			}
		}

		/// <summary>Gets the amount of private memory allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated for the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00045A14 File Offset: 0x00043C14
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		public long PrivateMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 6, out num);
			}
		}

		/// <summary>Gets the amount of the virtual memory allocated for the associated process.</summary>
		/// <returns>The amount of virtual memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00045A30 File Offset: 0x00043C30
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		public long VirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 7, out num);
			}
		}

		/// <summary>Gets the amount of physical memory allocated for the associated process.</summary>
		/// <returns>The amount of physical memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00045A4C File Offset: 0x00043C4C
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long WorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 4, out num);
			}
		}

		/// <summary>Closes a process that has a user interface by sending a close message to its main window.</summary>
		/// <returns>true if the close message was successfully sent; false if the associated process does not have a main window or if the main window is disabled (for example if a modal dialog is being shown).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00045A68 File Offset: 0x00043C68
		public bool CloseMainWindow()
		{
			SafeProcessHandle safeProcessHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				flag = Microsoft.Win32.NativeMethods.TerminateProcess(safeProcessHandle, -2);
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return flag;
		}

		// Token: 0x06000ED9 RID: 3801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetProcess_internal(int pid);

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given a process identifier and the name of a computer on the network.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with a remote process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <param name="processId">The system-unique identifier of a process resource. </param>
		/// <param name="machineName">The name of a computer on the network. </param>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.-or- The <paramref name="machineName" /> parameter syntax is invalid. The name might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDA RID: 3802 RVA: 0x00045AA4 File Offset: 0x00043CA4
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process GetProcessById(int processId, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			IntPtr process_internal = Process.GetProcess_internal(processId);
			if (process_internal == IntPtr.Zero)
			{
				throw new ArgumentException("Can't find process with ID " + processId.ToString());
			}
			return new Process(new SafeProcessHandle(process_internal, true), processId);
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on a remote computer that share the specified process name.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <param name="processName">The friendly name of the process. </param>
		/// <param name="machineName">The name of a computer on the network. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers. </exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDB RID: 3803 RVA: 0x00045B04 File Offset: 0x00043D04
		public static Process[] GetProcessesByName(string processName, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			Process[] processes = Process.GetProcesses();
			if (processes.Length == 0)
			{
				return processes;
			}
			int num = 0;
			for (int i = 0; i < processes.Length; i++)
			{
				try
				{
					if (string.Compare(processName, processes[i].ProcessName, true) == 0)
					{
						processes[num++] = processes[i];
					}
				}
				catch (SystemException)
				{
				}
			}
			Array.Resize<Process>(ref processes, num);
			return processes;
		}

		// Token: 0x06000EDC RID: 3804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] GetProcesses_internal();

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the specified computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the specified computer.</returns>
		/// <param name="machineName">The computer from which to read the list of processes. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers. </exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDD RID: 3805 RVA: 0x00045B84 File Offset: 0x00043D84
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process[] GetProcesses(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			int[] processes_internal = Process.GetProcesses_internal();
			if (processes_internal == null)
			{
				return new Process[0];
			}
			List<Process> list = new List<Process>(processes_internal.Length);
			for (int i = 0; i < processes_internal.Length; i++)
			{
				try
				{
					list.Add(Process.GetProcessById(processes_internal[i]));
				}
				catch (SystemException)
				{
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00045C00 File Offset: 0x00043E00
		private static bool IsLocalMachine(string machineName)
		{
			return machineName == "." || machineName.Length == 0 || string.Compare(machineName, Environment.MachineName, true) == 0;
		}

		// Token: 0x06000EDF RID: 3807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ShellExecuteEx_internal(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo);

		// Token: 0x06000EE0 RID: 3808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateProcess_internal(ProcessStartInfo startInfo, IntPtr stdin, IntPtr stdout, IntPtr stderr, ref Process.ProcInfo procInfo);

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00045C28 File Offset: 0x00043E28
		private bool StartWithShellExecuteEx(ProcessStartInfo startInfo)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName) || startInfo.Password != null)
			{
				throw new InvalidOperationException(global::SR.GetString("The Process object must have the UseShellExecute property set to false in order to start a process as a user."));
			}
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(global::SR.GetString("The Process object must have the UseShellExecute property set to false in order to redirect IO streams."));
			}
			if (startInfo.StandardErrorEncoding != null)
			{
				throw new InvalidOperationException(global::SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (startInfo.StandardOutputEncoding != null)
			{
				throw new InvalidOperationException(global::SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.environmentVariables != null)
			{
				throw new InvalidOperationException(global::SR.GetString("The Process object must have the UseShellExecute property set to false in order to use environment variables."));
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			Process.FillUserInfo(startInfo, ref procInfo);
			bool flag;
			try
			{
				flag = Process.ShellExecuteEx_internal(startInfo, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				throw new Win32Exception(-procInfo.pid);
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			return flag;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00045D68 File Offset: 0x00043F68
		private static void CreatePipe(out IntPtr read, out IntPtr write, bool writeDirection)
		{
			MonoIOError monoIOError;
			if (!MonoIO.CreatePipe(out read, out write, out monoIOError))
			{
				throw MonoIO.GetException(monoIOError);
			}
			if (Process.IsWindows)
			{
				IntPtr intPtr = (writeDirection ? write : read);
				if (!MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr, Process.GetCurrentProcess().Handle, out intPtr, 0, 0, 2, out monoIOError))
				{
					throw MonoIO.GetException(monoIOError);
				}
				if (writeDirection)
				{
					if (!MonoIO.Close(write, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					write = intPtr;
					return;
				}
				else
				{
					if (!MonoIO.Close(read, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					read = intPtr;
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00045DF0 File Offset: 0x00043FF0
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00045E1C File Offset: 0x0004401C
		private bool StartWithCreateProcess(ProcessStartInfo startInfo)
		{
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException(global::SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(global::SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			if (startInfo.HaveEnvVars)
			{
				List<string> list = null;
				StringBuilder stringBuilder = null;
				foreach (object obj in startInfo.EnvironmentVariables)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null)
					{
						if (list == null)
						{
							list = new List<string>();
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						else
						{
							stringBuilder.Clear();
						}
						stringBuilder.Append((string)dictionaryEntry.Key);
						stringBuilder.Append('=');
						stringBuilder.Append((string)dictionaryEntry.Value);
						list.Add(stringBuilder.ToString());
					}
				}
				procInfo.envVariables = ((list != null) ? list.ToArray() : null);
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr intPtr5 = IntPtr.Zero;
			IntPtr intPtr6 = IntPtr.Zero;
			try
			{
				if (startInfo.RedirectStandardInput)
				{
					Process.CreatePipe(out intPtr, out intPtr2, true);
				}
				else
				{
					intPtr = MonoIO.ConsoleInput;
					intPtr2 = IntPtr.Zero;
				}
				if (startInfo.RedirectStandardOutput)
				{
					Process.CreatePipe(out intPtr3, out intPtr4, false);
				}
				else
				{
					intPtr3 = IntPtr.Zero;
					intPtr4 = MonoIO.ConsoleOutput;
				}
				if (startInfo.RedirectStandardError)
				{
					Process.CreatePipe(out intPtr5, out intPtr6, false);
				}
				else
				{
					intPtr5 = IntPtr.Zero;
					intPtr6 = MonoIO.ConsoleError;
				}
				Process.FillUserInfo(startInfo, ref procInfo);
				if (!Process.CreateProcess_internal(startInfo, intPtr, intPtr4, intPtr6, ref procInfo))
				{
					throw new Win32Exception(-procInfo.pid, string.Concat(new string[]
					{
						"ApplicationName='",
						startInfo.FileName,
						"', CommandLine='",
						startInfo.Arguments,
						"', CurrentDirectory='",
						startInfo.WorkingDirectory,
						"', Native error= ",
						Win32Exception.GetErrorMessage(-procInfo.pid)
					}));
				}
			}
			catch
			{
				if (startInfo.RedirectStandardInput)
				{
					if (intPtr != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
					}
					if (intPtr2 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr2, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardOutput)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr3, out monoIOError);
					}
					if (intPtr4 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr4, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardError)
				{
					if (intPtr5 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr5, out monoIOError);
					}
					if (intPtr6 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr6, out monoIOError);
					}
				}
				throw;
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
					procInfo.Password = IntPtr.Zero;
				}
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			if (startInfo.RedirectStandardInput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr, out monoIOError);
				Encoding inputEncoding = Console.InputEncoding;
				this.standardInput = new StreamWriter(new FileStream(intPtr2, FileAccess.Write, true, 8192), inputEncoding)
				{
					AutoFlush = true
				};
			}
			if (startInfo.RedirectStandardOutput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr4, out monoIOError);
				Encoding encoding = startInfo.StandardOutputEncoding ?? Console.Out.Encoding;
				this.standardOutput = new StreamReader(new FileStream(intPtr3, FileAccess.Read, true, 8192), encoding, true);
			}
			if (startInfo.RedirectStandardError)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr6, out monoIOError);
				Encoding encoding2 = startInfo.StandardErrorEncoding ?? Console.Out.Encoding;
				this.standardError = new StreamReader(new FileStream(intPtr5, FileAccess.Read, true, 8192), encoding2, true);
			}
			return true;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00046260 File Offset: 0x00044460
		private static void FillUserInfo(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo)
		{
			if (startInfo.UserName.Length != 0)
			{
				procInfo.UserName = startInfo.UserName;
				procInfo.Domain = startInfo.Domain;
				if (startInfo.Password != null)
				{
					procInfo.Password = Marshal.SecureStringToBSTR(startInfo.Password);
				}
				else
				{
					procInfo.Password = IntPtr.Zero;
				}
				procInfo.LoadUserProfile = startInfo.LoadUserProfile;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000462C4 File Offset: 0x000444C4
		private void RaiseOnExited()
		{
			if (!this.watchForExit)
			{
				return;
			}
			if (!this.raisedOnExited)
			{
				lock (this)
				{
					if (!this.raisedOnExited)
					{
						this.raisedOnExited = true;
						this.OnExited();
					}
				}
			}
		}

		// Token: 0x040010A9 RID: 4265
		private bool haveProcessId;

		// Token: 0x040010AA RID: 4266
		private int processId;

		// Token: 0x040010AB RID: 4267
		private bool haveProcessHandle;

		// Token: 0x040010AC RID: 4268
		private SafeProcessHandle m_processHandle;

		// Token: 0x040010AD RID: 4269
		private bool isRemoteMachine;

		// Token: 0x040010AE RID: 4270
		private string machineName;

		// Token: 0x040010AF RID: 4271
		private int m_processAccess;

		// Token: 0x040010B0 RID: 4272
		private ProcessThreadCollection threads;

		// Token: 0x040010B1 RID: 4273
		private ProcessModuleCollection modules;

		// Token: 0x040010B2 RID: 4274
		private bool haveWorkingSetLimits;

		// Token: 0x040010B3 RID: 4275
		private IntPtr minWorkingSet;

		// Token: 0x040010B4 RID: 4276
		private IntPtr maxWorkingSet;

		// Token: 0x040010B5 RID: 4277
		private bool havePriorityClass;

		// Token: 0x040010B6 RID: 4278
		private ProcessPriorityClass priorityClass;

		// Token: 0x040010B7 RID: 4279
		private ProcessStartInfo startInfo;

		// Token: 0x040010B8 RID: 4280
		private bool watchForExit;

		// Token: 0x040010B9 RID: 4281
		private bool watchingForExit;

		// Token: 0x040010BA RID: 4282
		private EventHandler onExited;

		// Token: 0x040010BB RID: 4283
		private bool exited;

		// Token: 0x040010BC RID: 4284
		private int exitCode;

		// Token: 0x040010BD RID: 4285
		private bool signaled;

		// Token: 0x040010BE RID: 4286
		private DateTime exitTime;

		// Token: 0x040010BF RID: 4287
		private bool haveExitTime;

		// Token: 0x040010C0 RID: 4288
		private bool raisedOnExited;

		// Token: 0x040010C1 RID: 4289
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x040010C2 RID: 4290
		private WaitHandle waitHandle;

		// Token: 0x040010C3 RID: 4291
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x040010C4 RID: 4292
		private StreamReader standardOutput;

		// Token: 0x040010C5 RID: 4293
		private StreamWriter standardInput;

		// Token: 0x040010C6 RID: 4294
		private StreamReader standardError;

		// Token: 0x040010C7 RID: 4295
		private OperatingSystem operatingSystem;

		// Token: 0x040010C8 RID: 4296
		private bool disposed;

		// Token: 0x040010C9 RID: 4297
		private Process.StreamReadMode outputStreamReadMode;

		// Token: 0x040010CA RID: 4298
		private Process.StreamReadMode errorStreamReadMode;

		// Token: 0x040010CB RID: 4299
		private Process.StreamReadMode inputStreamReadMode;

		// Token: 0x040010CE RID: 4302
		internal AsyncStreamReader output;

		// Token: 0x040010CF RID: 4303
		internal AsyncStreamReader error;

		// Token: 0x040010D0 RID: 4304
		internal bool pendingOutputRead;

		// Token: 0x040010D1 RID: 4305
		internal bool pendingErrorRead;

		// Token: 0x040010D2 RID: 4306
		internal static TraceSwitch processTracing;

		// Token: 0x040010D3 RID: 4307
		private string process_name;

		// Token: 0x040010D4 RID: 4308
		private static ProcessModule current_main_module;

		// Token: 0x020001D4 RID: 468
		private enum StreamReadMode
		{
			// Token: 0x040010D6 RID: 4310
			undefined,
			// Token: 0x040010D7 RID: 4311
			syncMode,
			// Token: 0x040010D8 RID: 4312
			asyncMode
		}

		// Token: 0x020001D5 RID: 469
		private enum State
		{
			// Token: 0x040010DA RID: 4314
			HaveId = 1,
			// Token: 0x040010DB RID: 4315
			IsLocal,
			// Token: 0x040010DC RID: 4316
			IsNt = 4,
			// Token: 0x040010DD RID: 4317
			HaveProcessInfo = 8,
			// Token: 0x040010DE RID: 4318
			Exited = 16,
			// Token: 0x040010DF RID: 4319
			Associated = 32,
			// Token: 0x040010E0 RID: 4320
			IsWin2k = 64,
			// Token: 0x040010E1 RID: 4321
			HaveNtProcessInfo = 12
		}

		// Token: 0x020001D6 RID: 470
		private struct ProcInfo
		{
			// Token: 0x040010E2 RID: 4322
			public IntPtr process_handle;

			// Token: 0x040010E3 RID: 4323
			public int pid;

			// Token: 0x040010E4 RID: 4324
			public string[] envVariables;

			// Token: 0x040010E5 RID: 4325
			public string UserName;

			// Token: 0x040010E6 RID: 4326
			public string Domain;

			// Token: 0x040010E7 RID: 4327
			public IntPtr Password;

			// Token: 0x040010E8 RID: 4328
			public bool LoadUserProfile;
		}
	}
}
