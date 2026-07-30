using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000173 RID: 371
	internal class MWFVFS
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
		public MWFVFS()
		{
			if (XplatUI.RunningOnUnix)
			{
				this.fileSystem = new UnixFileSystem();
			}
			else
			{
				this.fileSystem = new WinFileSystem();
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005D174 File Offset: 0x0005B374
		public FSEntry ChangeDirectory(string folder)
		{
			return this.fileSystem.ChangeDirectory(folder);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0005D184 File Offset: 0x0005B384
		public void GetFolderContent()
		{
			this.GetFolderContent(null);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005D190 File Offset: 0x0005B390
		public void GetFolderContent(StringCollection filters)
		{
			this.the_filters = filters;
			if (this.workerThread != null)
			{
				this.workerThread.Stop();
				this.workerThread = null;
			}
			this.calling_control.CreateControl();
			this.workerThread = new MWFVFS.WorkerThread(this.fileSystem, this.the_filters, this.updateDelegate, this.calling_control);
			this.get_folder_content_thread_start = new ThreadStart(this.workerThread.GetFolderContentThread);
			this.worker = new Thread(this.get_folder_content_thread_start);
			this.worker.IsBackground = true;
			this.worker.Start();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005D230 File Offset: 0x0005B430
		public ArrayList GetFoldersOnly()
		{
			return this.fileSystem.GetFoldersOnly();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0005D240 File Offset: 0x0005B440
		public void WriteRecentlyUsedFiles(string filename)
		{
			this.fileSystem.WriteRecentlyUsedFiles(filename);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0005D250 File Offset: 0x0005B450
		public ArrayList GetRecentlyUsedFiles()
		{
			return this.fileSystem.GetRecentlyUsedFiles();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0005D260 File Offset: 0x0005B460
		public ArrayList GetMyComputerContent()
		{
			return this.fileSystem.GetMyComputerContent();
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005D270 File Offset: 0x0005B470
		public ArrayList GetMyNetworkContent()
		{
			return this.fileSystem.GetMyNetworkContent();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005D280 File Offset: 0x0005B480
		public bool CreateFolder(string new_folder)
		{
			try
			{
				if (Directory.Exists(new_folder))
				{
					string text = "Folder \"" + new_folder + "\" already exists.";
					MessageBox.Show(text, new_folder, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				Directory.CreateDirectory(new_folder);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, new_folder, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0005D304 File Offset: 0x0005B504
		public bool MoveFolder(string sourceDirName, string destDirName)
		{
			try
			{
				if (Directory.Exists(destDirName))
				{
					string text = "Cannot rename " + Path.GetFileName(sourceDirName) + ": A folder with the name you specified already exists. Specify a different folder name.";
					MessageBox.Show(text, "Error Renaming Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				Directory.Move(sourceDirName, destDirName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Renaming Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005D394 File Offset: 0x0005B594
		public bool MoveFile(string sourceFileName, string destFileName)
		{
			try
			{
				if (File.Exists(destFileName))
				{
					string text = "Cannot rename " + Path.GetFileName(sourceFileName) + ": A file with the name you specified already exists. Specify a different file name.";
					MessageBox.Show(text, "Error Renaming File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				File.Move(sourceFileName, destFileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Renaming File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005D424 File Offset: 0x0005B624
		public string GetParent()
		{
			return this.fileSystem.GetParent();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0005D434 File Offset: 0x0005B634
		public void RegisterUpdateDelegate(MWFVFS.UpdateDelegate updateDelegate, Control control)
		{
			this.updateDelegate = updateDelegate;
			this.calling_control = control;
		}

		// Token: 0x04000DCA RID: 3530
		private FileSystem fileSystem;

		// Token: 0x04000DCB RID: 3531
		public static readonly string DesktopPrefix = "Desktop://";

		// Token: 0x04000DCC RID: 3532
		public static readonly string PersonalPrefix = "Personal://";

		// Token: 0x04000DCD RID: 3533
		public static readonly string MyComputerPrefix = "MyComputer://";

		// Token: 0x04000DCE RID: 3534
		public static readonly string RecentlyUsedPrefix = "RecentlyUsed://";

		// Token: 0x04000DCF RID: 3535
		public static readonly string MyNetworkPrefix = "MyNetwork://";

		// Token: 0x04000DD0 RID: 3536
		public static readonly string MyComputerPersonalPrefix = "MyComputerPersonal://";

		// Token: 0x04000DD1 RID: 3537
		public static Hashtable MyComputerDevicesPrefix = new Hashtable();

		// Token: 0x04000DD2 RID: 3538
		private MWFVFS.UpdateDelegate updateDelegate;

		// Token: 0x04000DD3 RID: 3539
		private Control calling_control;

		// Token: 0x04000DD4 RID: 3540
		private ThreadStart get_folder_content_thread_start;

		// Token: 0x04000DD5 RID: 3541
		private Thread worker;

		// Token: 0x04000DD6 RID: 3542
		private MWFVFS.WorkerThread workerThread;

		// Token: 0x04000DD7 RID: 3543
		private StringCollection the_filters;

		// Token: 0x02000174 RID: 372
		internal class WorkerThread
		{
			// Token: 0x060018C0 RID: 6336 RVA: 0x0005D444 File Offset: 0x0005B644
			public WorkerThread(FileSystem fileSystem, StringCollection the_filters, MWFVFS.UpdateDelegate updateDelegate, Control calling_control)
			{
				this.fileSystem = fileSystem;
				this.the_filters = the_filters;
				this.updateDelegate = updateDelegate;
				this.calling_control = calling_control;
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x0005D480 File Offset: 0x0005B680
			public void GetFolderContentThread()
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				this.fileSystem.GetFolderContent(this.the_filters, out arrayList, out arrayList2);
				if (this.stopped)
				{
					return;
				}
				if (this.updateDelegate != null)
				{
					lock (this)
					{
						object[] array = new object[] { arrayList, arrayList2 };
						this.calling_control.BeginInvoke(this.updateDelegate, array);
					}
				}
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x0005D50C File Offset: 0x0005B70C
			public void Stop()
			{
				object obj = this.lockobject;
				lock (obj)
				{
					this.stopped = true;
				}
			}

			// Token: 0x04000DD8 RID: 3544
			private FileSystem fileSystem;

			// Token: 0x04000DD9 RID: 3545
			private StringCollection the_filters;

			// Token: 0x04000DDA RID: 3546
			private MWFVFS.UpdateDelegate updateDelegate;

			// Token: 0x04000DDB RID: 3547
			private Control calling_control;

			// Token: 0x04000DDC RID: 3548
			private readonly object lockobject = new object();

			// Token: 0x04000DDD RID: 3549
			private bool stopped;
		}

		// Token: 0x02000638 RID: 1592
		// (Invoke) Token: 0x06005092 RID: 20626
		public delegate void UpdateDelegate(ArrayList folders, ArrayList files);
	}
}
