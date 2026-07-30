using System;
using System.Collections;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200017C RID: 380
	internal class MasterMount
	{
		// Token: 0x0600190A RID: 6410 RVA: 0x0005F3A8 File Offset: 0x0005D5A8
		public MasterMount()
		{
			if (XplatUI.RunningOnUnix && File.Exists("/proc/mounts"))
			{
				this.proc_mount_available = true;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0005F408 File Offset: 0x0005D608
		public ArrayList Block_devices
		{
			get
			{
				return this.block_devices;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0005F410 File Offset: 0x0005D610
		public ArrayList Network_devices
		{
			get
			{
				return this.network_devices;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0005F418 File Offset: 0x0005D618
		public ArrayList Removable_devices
		{
			get
			{
				return this.removable_devices;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0005F420 File Offset: 0x0005D620
		public bool ProcMountAvailable
		{
			get
			{
				return this.proc_mount_available;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0005F428 File Offset: 0x0005D628
		public void GetMounts()
		{
			if (!this.proc_mount_available)
			{
				return;
			}
			this.block_devices.Clear();
			this.network_devices.Clear();
			this.removable_devices.Clear();
			try
			{
				StreamReader streamReader = new StreamReader("/proc/mounts");
				string text = streamReader.ReadLine();
				ArrayList arrayList = new ArrayList();
				while (text != null)
				{
					if (arrayList.IndexOf(text) == -1)
					{
						this.ProcessProcMountLine(text);
						arrayList.Add(text);
					}
					text = streamReader.ReadLine();
				}
				streamReader.Close();
				this.block_devices.Sort(this.mountComparer);
				this.network_devices.Sort(this.mountComparer);
				this.removable_devices.Sort(this.mountComparer);
			}
			catch
			{
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005F508 File Offset: 0x0005D708
		private void ProcessProcMountLine(string line)
		{
			string[] array = line.Split(new char[] { ' ' });
			if (array != null && array.Length > 0)
			{
				MasterMount.Mount mount = default(MasterMount.Mount);
				if (array[0].StartsWith("/dev/"))
				{
					mount.device_short = array[0].Replace("/dev/", string.Empty);
				}
				else
				{
					mount.device_short = array[0];
				}
				mount.device_or_filesystem = array[0];
				mount.mount_point = array[1];
				if (array[2] == "nfs")
				{
					mount.fsType = MasterMount.FsTypes.nfs;
					this.network_devices.Add(mount);
				}
				else if (array[2] == "smbfs")
				{
					mount.fsType = MasterMount.FsTypes.smbfs;
					this.network_devices.Add(mount);
				}
				else if (array[2] == "cifs")
				{
					mount.fsType = MasterMount.FsTypes.cifs;
					this.network_devices.Add(mount);
				}
				else if (array[2] == "ncpfs")
				{
					mount.fsType = MasterMount.FsTypes.ncpfs;
					this.network_devices.Add(mount);
				}
				else if (array[2] == "iso9660")
				{
					mount.fsType = MasterMount.FsTypes.iso9660;
					this.removable_devices.Add(mount);
				}
				else if (array[2] == "usbfs")
				{
					mount.fsType = MasterMount.FsTypes.usbfs;
					this.removable_devices.Add(mount);
				}
				else if (array[0].StartsWith("/"))
				{
					if (array[1].StartsWith("/dev/"))
					{
						return;
					}
					if (array[2] == "ext2")
					{
						mount.fsType = MasterMount.FsTypes.ext2;
					}
					else if (array[2] == "ext3")
					{
						mount.fsType = MasterMount.FsTypes.ext3;
					}
					else if (array[2] == "reiserfs")
					{
						mount.fsType = MasterMount.FsTypes.reiserfs;
					}
					else if (array[2] == "xfs")
					{
						mount.fsType = MasterMount.FsTypes.xfs;
					}
					else if (array[2] == "vfat")
					{
						mount.fsType = MasterMount.FsTypes.vfat;
					}
					else if (array[2] == "ntfs")
					{
						mount.fsType = MasterMount.FsTypes.ntfs;
					}
					else if (array[2] == "msdos")
					{
						mount.fsType = MasterMount.FsTypes.msdos;
					}
					else if (array[2] == "umsdos")
					{
						mount.fsType = MasterMount.FsTypes.umsdos;
					}
					else if (array[2] == "hpfs")
					{
						mount.fsType = MasterMount.FsTypes.hpfs;
					}
					else if (array[2] == "minix")
					{
						mount.fsType = MasterMount.FsTypes.minix;
					}
					else if (array[2] == "jfs")
					{
						mount.fsType = MasterMount.FsTypes.jfs;
					}
					this.block_devices.Add(mount);
				}
			}
		}

		// Token: 0x04000E08 RID: 3592
		private bool proc_mount_available;

		// Token: 0x04000E09 RID: 3593
		private ArrayList block_devices = new ArrayList();

		// Token: 0x04000E0A RID: 3594
		private ArrayList network_devices = new ArrayList();

		// Token: 0x04000E0B RID: 3595
		private ArrayList removable_devices = new ArrayList();

		// Token: 0x04000E0C RID: 3596
		private MasterMount.MountComparer mountComparer = new MasterMount.MountComparer();

		// Token: 0x0200017D RID: 381
		internal enum FsTypes
		{
			// Token: 0x04000E0E RID: 3598
			none,
			// Token: 0x04000E0F RID: 3599
			ext2,
			// Token: 0x04000E10 RID: 3600
			ext3,
			// Token: 0x04000E11 RID: 3601
			hpfs,
			// Token: 0x04000E12 RID: 3602
			iso9660,
			// Token: 0x04000E13 RID: 3603
			jfs,
			// Token: 0x04000E14 RID: 3604
			minix,
			// Token: 0x04000E15 RID: 3605
			msdos,
			// Token: 0x04000E16 RID: 3606
			ntfs,
			// Token: 0x04000E17 RID: 3607
			reiserfs,
			// Token: 0x04000E18 RID: 3608
			ufs,
			// Token: 0x04000E19 RID: 3609
			umsdos,
			// Token: 0x04000E1A RID: 3610
			vfat,
			// Token: 0x04000E1B RID: 3611
			sysv,
			// Token: 0x04000E1C RID: 3612
			xfs,
			// Token: 0x04000E1D RID: 3613
			ncpfs,
			// Token: 0x04000E1E RID: 3614
			nfs,
			// Token: 0x04000E1F RID: 3615
			smbfs,
			// Token: 0x04000E20 RID: 3616
			usbfs,
			// Token: 0x04000E21 RID: 3617
			cifs
		}

		// Token: 0x0200017E RID: 382
		internal struct Mount
		{
			// Token: 0x04000E22 RID: 3618
			public string device_or_filesystem;

			// Token: 0x04000E23 RID: 3619
			public string device_short;

			// Token: 0x04000E24 RID: 3620
			public string mount_point;

			// Token: 0x04000E25 RID: 3621
			public MasterMount.FsTypes fsType;
		}

		// Token: 0x0200017F RID: 383
		public class MountComparer : IComparer
		{
			// Token: 0x06001912 RID: 6418 RVA: 0x0005F84C File Offset: 0x0005DA4C
			public int Compare(object mount1, object mount2)
			{
				return string.Compare(((MasterMount.Mount)mount1).device_short, ((MasterMount.Mount)mount2).device_short);
			}
		}
	}
}
