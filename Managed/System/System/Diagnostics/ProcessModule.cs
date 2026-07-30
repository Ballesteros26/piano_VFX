using System;
using System.ComponentModel;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Represents a.dll or .exe file that is loaded into a particular process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000213 RID: 531
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessModule : Component
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x0004B533 File Offset: 0x00049733
		internal ProcessModule(IntPtr baseaddr, IntPtr entryaddr, string filename, FileVersionInfo version_info, int memory_size, string modulename)
		{
			this.baseaddr = baseaddr;
			this.entryaddr = entryaddr;
			this.filename = filename;
			this.version_info = version_info;
			this.memory_size = memory_size;
			this.modulename = modulename;
		}

		/// <summary>Gets the memory address where the module was loaded.</summary>
		/// <returns>The load address of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004B568 File Offset: 0x00049768
		[MonitoringDescription("The base memory address of this module")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.baseaddr;
			}
		}

		/// <summary>Gets the memory address for the function that runs when the system loads and runs the module.</summary>
		/// <returns>The entry point of the module.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004B570 File Offset: 0x00049770
		[MonitoringDescription("The base memory address of the entry point of this module")]
		public IntPtr EntryPointAddress
		{
			get
			{
				return this.entryaddr;
			}
		}

		/// <summary>Gets the full path to the module.</summary>
		/// <returns>The fully qualified path that defines the location of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004B578 File Offset: 0x00049778
		[MonitoringDescription("The file name of this module")]
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets version information about the module.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> that contains the module's version information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004B580 File Offset: 0x00049780
		[Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				return this.version_info;
			}
		}

		/// <summary>Gets the amount of memory that is required to load the module.</summary>
		/// <returns>The size, in bytes, of the memory that the module occupies.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004B588 File Offset: 0x00049788
		[MonitoringDescription("The memory needed by this module")]
		public int ModuleMemorySize
		{
			get
			{
				return this.memory_size;
			}
		}

		/// <summary>Gets the name of the process module.</summary>
		/// <returns>The name of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004B590 File Offset: 0x00049790
		[MonitoringDescription("The name of this module")]
		public string ModuleName
		{
			get
			{
				return this.modulename;
			}
		}

		/// <summary>Converts the name of the module to a string.</summary>
		/// <returns>The value of the <see cref="P:System.Diagnostics.ProcessModule.ModuleName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600115F RID: 4447 RVA: 0x0004B598 File Offset: 0x00049798
		public override string ToString()
		{
			return this.ModuleName;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal ProcessModule()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040011CA RID: 4554
		private IntPtr baseaddr;

		// Token: 0x040011CB RID: 4555
		private IntPtr entryaddr;

		// Token: 0x040011CC RID: 4556
		private string filename;

		// Token: 0x040011CD RID: 4557
		private FileVersionInfo version_info;

		// Token: 0x040011CE RID: 4558
		private int memory_size;

		// Token: 0x040011CF RID: 4559
		private string modulename;
	}
}
