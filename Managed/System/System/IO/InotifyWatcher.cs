using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.IO
{
	// Token: 0x020003D7 RID: 983
	internal class InotifyWatcher : IFileWatcher
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x000020EB File Offset: 0x000002EB
		private InotifyWatcher()
		{
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00077088 File Offset: 0x00075288
		public static bool GetInstance(out IFileWatcher watcher, bool gamin)
		{
			if (InotifyWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (InotifyWatcher.instance != null)
			{
				watcher = InotifyWatcher.instance;
				return true;
			}
			InotifyWatcher.FD = InotifyWatcher.GetInotifyInstance();
			if ((long)InotifyWatcher.FD == -1L)
			{
				InotifyWatcher.failed = true;
				watcher = null;
				return false;
			}
			InotifyWatcher.watches = Hashtable.Synchronized(new Hashtable());
			InotifyWatcher.requests = Hashtable.Synchronized(new Hashtable());
			InotifyWatcher.instance = new InotifyWatcher();
			watcher = InotifyWatcher.instance;
			return true;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00077104 File Offset: 0x00075304
		public void StartDispatching(FileSystemWatcher fsw)
		{
			InotifyWatcher inotifyWatcher = this;
			ParentInotifyData parentInotifyData;
			lock (inotifyWatcher)
			{
				if ((long)InotifyWatcher.FD == -1L)
				{
					InotifyWatcher.FD = InotifyWatcher.GetInotifyInstance();
				}
				if (InotifyWatcher.thread == null)
				{
					InotifyWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					InotifyWatcher.thread.IsBackground = true;
					InotifyWatcher.thread.Start();
				}
				parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
			}
			if (parentInotifyData == null)
			{
				InotifyData inotifyData = new InotifyData();
				inotifyData.FSW = fsw;
				inotifyData.Directory = fsw.FullPath;
				parentInotifyData = new ParentInotifyData();
				parentInotifyData.IncludeSubdirs = fsw.IncludeSubdirectories;
				parentInotifyData.Enabled = true;
				parentInotifyData.children = new ArrayList();
				parentInotifyData.data = inotifyData;
				InotifyWatcher.watches[fsw] = parentInotifyData;
				try
				{
					InotifyWatcher.StartMonitoringDirectory(inotifyData, false);
					inotifyWatcher = this;
					lock (inotifyWatcher)
					{
						InotifyWatcher.AppendRequestData(inotifyData);
						InotifyWatcher.stop = false;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00077234 File Offset: 0x00075434
		private static void AppendRequestData(InotifyData data)
		{
			int watch = data.Watch;
			object obj = InotifyWatcher.requests[watch];
			if (obj == null)
			{
				InotifyWatcher.requests[data.Watch] = data;
				return;
			}
			ArrayList arrayList;
			if (obj is InotifyData)
			{
				arrayList = new ArrayList();
				arrayList.Add(obj);
				arrayList.Add(data);
				InotifyWatcher.requests[data.Watch] = arrayList;
				return;
			}
			arrayList = (ArrayList)obj;
			arrayList.Add(data);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000772BC File Offset: 0x000754BC
		private static bool RemoveRequestData(InotifyData data)
		{
			int watch = data.Watch;
			object obj = InotifyWatcher.requests[watch];
			if (obj == null)
			{
				return true;
			}
			if (obj is InotifyData)
			{
				if (obj == data)
				{
					InotifyWatcher.requests.Remove(watch);
					return true;
				}
				return false;
			}
			else
			{
				ArrayList arrayList = (ArrayList)obj;
				arrayList.Remove(data);
				if (arrayList.Count == 0)
				{
					InotifyWatcher.requests.Remove(watch);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00077330 File Offset: 0x00075530
		private static InotifyMask GetMaskFromFilters(NotifyFilters filters)
		{
			InotifyMask inotifyMask = InotifyMask.Create | InotifyMask.Delete | InotifyMask.DeleteSelf | InotifyMask.AddMask;
			if ((filters & NotifyFilters.Attributes) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
			}
			if ((filters & NotifyFilters.Security) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
			}
			if ((filters & NotifyFilters.Size) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.LastAccess) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Access;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.LastWrite) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.FileName) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.MovedFrom;
				inotifyMask |= InotifyMask.MovedTo;
			}
			if ((filters & NotifyFilters.DirectoryName) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.MovedFrom;
				inotifyMask |= InotifyMask.MovedTo;
			}
			return inotifyMask;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000773AC File Offset: 0x000755AC
		private static void StartMonitoringDirectory(InotifyData data, bool justcreated)
		{
			InotifyMask maskFromFilters = InotifyWatcher.GetMaskFromFilters(data.FSW.NotifyFilter);
			int num = InotifyWatcher.AddDirectoryWatch(InotifyWatcher.FD, data.Directory, maskFromFilters);
			if (num != -1)
			{
				FileSystemWatcher fsw = data.FSW;
				data.Watch = num;
				ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
				if (parentInotifyData.IncludeSubdirs)
				{
					foreach (string text in Directory.GetDirectories(data.Directory))
					{
						InotifyData inotifyData = new InotifyData();
						inotifyData.FSW = fsw;
						inotifyData.Directory = text;
						if (justcreated)
						{
							FileSystemWatcher fileSystemWatcher = fsw;
							lock (fileSystemWatcher)
							{
								RenamedEventArgs renamedEventArgs = null;
								if (fsw.Pattern.IsMatch(text))
								{
									fsw.DispatchEvents(FileAction.Added, text, ref renamedEventArgs);
									if (fsw.Waiting)
									{
										fsw.Waiting = false;
										global::System.Threading.Monitor.PulseAll(fsw);
									}
								}
							}
						}
						try
						{
							InotifyWatcher.StartMonitoringDirectory(inotifyData, justcreated);
							InotifyWatcher.AppendRequestData(inotifyData);
							parentInotifyData.children.Add(inotifyData);
						}
						catch
						{
						}
					}
				}
				if (justcreated)
				{
					foreach (string text2 in Directory.GetFiles(data.Directory))
					{
						FileSystemWatcher fileSystemWatcher = fsw;
						lock (fileSystemWatcher)
						{
							RenamedEventArgs renamedEventArgs2 = null;
							if (fsw.Pattern.IsMatch(text2))
							{
								fsw.DispatchEvents(FileAction.Added, text2, ref renamedEventArgs2);
								fsw.DispatchEvents(FileAction.Modified, text2, ref renamedEventArgs2);
								if (fsw.Waiting)
								{
									fsw.Waiting = false;
									global::System.Threading.Monitor.PulseAll(fsw);
								}
							}
						}
					}
				}
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 4)
			{
				string text3 = "(unknown)";
				try
				{
					using (StreamReader streamReader = new StreamReader("/proc/sys/fs/inotify/max_user_watches"))
					{
						text3 = streamReader.ReadLine();
					}
				}
				catch
				{
				}
				string text4 = string.Format("The per-user inotify watches limit of {0} has been reached. If you're experiencing problems with your application, increase that limit in /proc/sys/fs/inotify/max_user_watches.", text3);
				throw new Win32Exception(lastWin32Error, text4);
			}
			throw new Win32Exception(lastWin32Error);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000775E8 File Offset: 0x000757E8
		public void StopDispatching(FileSystemWatcher fsw)
		{
			lock (this)
			{
				ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
				if (parentInotifyData != null)
				{
					if (InotifyWatcher.RemoveRequestData(parentInotifyData.data))
					{
						InotifyWatcher.StopMonitoringDirectory(parentInotifyData.data);
					}
					InotifyWatcher.watches.Remove(fsw);
					if (InotifyWatcher.watches.Count == 0)
					{
						InotifyWatcher.stop = true;
						IntPtr fd = InotifyWatcher.FD;
						InotifyWatcher.FD = (IntPtr)(-1);
						InotifyWatcher.Close(fd);
					}
					if (parentInotifyData.IncludeSubdirs)
					{
						foreach (object obj in parentInotifyData.children)
						{
							InotifyData inotifyData = (InotifyData)obj;
							if (InotifyWatcher.RemoveRequestData(inotifyData))
							{
								InotifyWatcher.StopMonitoringDirectory(inotifyData);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000776E0 File Offset: 0x000758E0
		private static void StopMonitoringDirectory(InotifyData data)
		{
			InotifyWatcher.RemoveWatch(InotifyWatcher.FD, data.Watch);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000776F4 File Offset: 0x000758F4
		private void Monitor()
		{
			byte[] array = new byte[4096];
			InotifyWatcher inotifyWatcher;
			while (!InotifyWatcher.stop)
			{
				int num = InotifyWatcher.ReadFromFD(InotifyWatcher.FD, array, (IntPtr)array.Length);
				if (num != -1)
				{
					inotifyWatcher = this;
					lock (inotifyWatcher)
					{
						this.ProcessEvents(array, num);
					}
				}
			}
			inotifyWatcher = this;
			lock (inotifyWatcher)
			{
				InotifyWatcher.thread = null;
				InotifyWatcher.stop = false;
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00077790 File Offset: 0x00075990
		private static int ReadEvent(byte[] source, int off, int size, out InotifyEvent evt)
		{
			evt = default(InotifyEvent);
			if (size <= 0 || off > size - 16)
			{
				return -1;
			}
			int num;
			if (BitConverter.IsLittleEndian)
			{
				evt.WatchDescriptor = (int)source[off] + ((int)source[off + 1] << 8) + ((int)source[off + 2] << 16) + ((int)source[off + 3] << 24);
				evt.Mask = (InotifyMask)((int)source[off + 4] + ((int)source[off + 5] << 8) + ((int)source[off + 6] << 16) + ((int)source[off + 7] << 24));
				num = (int)source[off + 12] + ((int)source[off + 13] << 8) + ((int)source[off + 14] << 16) + ((int)source[off + 15] << 24);
			}
			else
			{
				evt.WatchDescriptor = (int)source[off + 3] + ((int)source[off + 2] << 8) + ((int)source[off + 1] << 16) + ((int)source[off] << 24);
				evt.Mask = (InotifyMask)((int)source[off + 7] + ((int)source[off + 6] << 8) + ((int)source[off + 5] << 16) + ((int)source[off + 4] << 24));
				num = (int)source[off + 15] + ((int)source[off + 14] << 8) + ((int)source[off + 13] << 16) + ((int)source[off + 12] << 24);
			}
			if (num > 0)
			{
				if (off > size - 16 - num)
				{
					return -1;
				}
				string @string = Encoding.UTF8.GetString(source, off + 16, num);
				evt.Name = @string.Trim(new char[1]);
			}
			else
			{
				evt.Name = null;
			}
			return 16 + num;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000778D1 File Offset: 0x00075AD1
		private static IEnumerable GetEnumerator(object source)
		{
			if (source == null)
			{
				yield break;
			}
			if (source is InotifyData)
			{
				yield return source;
			}
			if (source is ArrayList)
			{
				ArrayList list = (ArrayList)source;
				int num;
				for (int i = 0; i < list.Count; i = num + 1)
				{
					yield return list[i];
					num = i;
				}
				list = null;
			}
			yield break;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000778E4 File Offset: 0x00075AE4
		private void ProcessEvents(byte[] buffer, int length)
		{
			ArrayList arrayList = null;
			int num = 0;
			RenamedEventArgs renamedEventArgs = null;
			while (length > num)
			{
				InotifyEvent inotifyEvent;
				int num2 = InotifyWatcher.ReadEvent(buffer, num, length, out inotifyEvent);
				if (num2 <= 0)
				{
					break;
				}
				num += num2;
				InotifyMask inotifyMask = inotifyEvent.Mask;
				bool flag = (inotifyMask & InotifyMask.Directory) > (InotifyMask)0U;
				inotifyMask &= InotifyWatcher.Interesting;
				if (inotifyMask != (InotifyMask)0U)
				{
					foreach (object obj in InotifyWatcher.GetEnumerator(InotifyWatcher.requests[inotifyEvent.WatchDescriptor]))
					{
						InotifyData inotifyData = (InotifyData)obj;
						ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[inotifyData.FSW];
						if (inotifyData != null && parentInotifyData.Enabled)
						{
							string directory = inotifyData.Directory;
							string text = inotifyEvent.Name;
							if (text == null)
							{
								text = directory;
							}
							FileSystemWatcher fsw = inotifyData.FSW;
							FileAction fileAction = (FileAction)0;
							if ((inotifyMask & (InotifyMask.Modify | InotifyMask.Attrib)) != (InotifyMask)0U)
							{
								fileAction = FileAction.Modified;
							}
							else if ((inotifyMask & InotifyMask.Create) != (InotifyMask)0U)
							{
								fileAction = FileAction.Added;
							}
							else if ((inotifyMask & InotifyMask.Delete) != (InotifyMask)0U)
							{
								fileAction = FileAction.Removed;
							}
							else if ((inotifyMask & InotifyMask.DeleteSelf) != (InotifyMask)0U)
							{
								if (inotifyData.Watch != parentInotifyData.data.Watch)
								{
									continue;
								}
								fileAction = FileAction.Removed;
							}
							else
							{
								if ((inotifyMask & InotifyMask.MoveSelf) != (InotifyMask)0U)
								{
									continue;
								}
								if ((inotifyMask & InotifyMask.MovedFrom) != (InotifyMask)0U)
								{
									InotifyEvent inotifyEvent2;
									int num3 = InotifyWatcher.ReadEvent(buffer, num, length, out inotifyEvent2);
									if (num3 == -1 || (inotifyEvent2.Mask & InotifyMask.MovedTo) == (InotifyMask)0U || inotifyEvent.WatchDescriptor != inotifyEvent2.WatchDescriptor)
									{
										fileAction = FileAction.Removed;
									}
									else
									{
										num += num3;
										fileAction = FileAction.RenamedNewName;
										renamedEventArgs = new RenamedEventArgs(WatcherChangeTypes.Renamed, inotifyData.Directory, inotifyEvent2.Name, inotifyEvent.Name);
										if (inotifyEvent.Name != inotifyData.Directory && !fsw.Pattern.IsMatch(inotifyEvent.Name))
										{
											text = inotifyEvent2.Name;
										}
									}
								}
								else if ((inotifyMask & InotifyMask.MovedTo) != (InotifyMask)0U)
								{
									fileAction = FileAction.Added;
								}
							}
							if (fsw.IncludeSubdirectories)
							{
								string fullPath = fsw.FullPath;
								string text2 = inotifyData.Directory;
								if (text2 != fullPath)
								{
									int length2 = fullPath.Length;
									int num4 = 1;
									if (length2 > 1 && fullPath[length2 - 1] == Path.DirectorySeparatorChar)
									{
										num4 = 0;
									}
									string text3 = text2.Substring(fullPath.Length + num4);
									text2 = Path.Combine(text2, text);
									text = Path.Combine(text3, text);
								}
								else
								{
									text2 = Path.Combine(fullPath, text);
								}
								if (fileAction == FileAction.Added && flag)
								{
									if (arrayList == null)
									{
										arrayList = new ArrayList(2);
									}
									arrayList.Add(new InotifyData
									{
										FSW = fsw,
										Directory = text2
									});
								}
								if (fileAction == FileAction.RenamedNewName && flag)
								{
									string oldFullPath = renamedEventArgs.OldFullPath;
									string fullPath2 = renamedEventArgs.FullPath;
									int length3 = oldFullPath.Length;
									foreach (object obj2 in parentInotifyData.children)
									{
										InotifyData inotifyData2 = (InotifyData)obj2;
										if (inotifyData2.Directory.StartsWith(oldFullPath, StringComparison.Ordinal))
										{
											inotifyData2.Directory = fullPath2 + inotifyData2.Directory.Substring(length3);
										}
									}
								}
							}
							if (fileAction == FileAction.Removed && text == inotifyData.Directory)
							{
								int num5 = parentInotifyData.children.IndexOf(inotifyData);
								if (num5 != -1)
								{
									parentInotifyData.children.RemoveAt(num5);
									if (!fsw.Pattern.IsMatch(Path.GetFileName(text)))
									{
										continue;
									}
								}
							}
							if (!(text != inotifyData.Directory) || fsw.Pattern.IsMatch(Path.GetFileName(text)))
							{
								FileSystemWatcher fileSystemWatcher = fsw;
								lock (fileSystemWatcher)
								{
									fsw.DispatchEvents(fileAction, text, ref renamedEventArgs);
									if (fileAction == FileAction.RenamedNewName)
									{
										renamedEventArgs = null;
									}
									if (fsw.Waiting)
									{
										fsw.Waiting = false;
										global::System.Threading.Monitor.PulseAll(fsw);
									}
								}
							}
						}
					}
				}
			}
			if (arrayList != null)
			{
				foreach (object obj3 in arrayList)
				{
					InotifyData inotifyData3 = (InotifyData)obj3;
					try
					{
						InotifyWatcher.StartMonitoringDirectory(inotifyData3, true);
						InotifyWatcher.AppendRequestData(inotifyData3);
						((ParentInotifyData)InotifyWatcher.watches[inotifyData3.FSW]).children.Add(inotifyData3);
					}
					catch
					{
					}
				}
				arrayList.Clear();
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00077DEC File Offset: 0x00075FEC
		private static int AddDirectoryWatch(IntPtr fd, string directory, InotifyMask mask)
		{
			mask |= InotifyMask.Directory;
			return InotifyWatcher.AddWatch(fd, directory, mask);
		}

		// Token: 0x06001E19 RID: 7705
		[DllImport("libc", EntryPoint = "close")]
		internal static extern int Close(IntPtr fd);

		// Token: 0x06001E1A RID: 7706
		[DllImport("libc", EntryPoint = "read")]
		private static extern int ReadFromFD(IntPtr fd, byte[] buffer, IntPtr length);

		// Token: 0x06001E1B RID: 7707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInotifyInstance();

		// Token: 0x06001E1C RID: 7708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWatch(IntPtr fd, string name, InotifyMask mask);

		// Token: 0x06001E1D RID: 7709
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RemoveWatch(IntPtr fd, int wd);

		// Token: 0x04001A4D RID: 6733
		private static bool failed;

		// Token: 0x04001A4E RID: 6734
		private static InotifyWatcher instance;

		// Token: 0x04001A4F RID: 6735
		private static Hashtable watches;

		// Token: 0x04001A50 RID: 6736
		private static Hashtable requests;

		// Token: 0x04001A51 RID: 6737
		private static IntPtr FD;

		// Token: 0x04001A52 RID: 6738
		private static Thread thread;

		// Token: 0x04001A53 RID: 6739
		private static bool stop;

		// Token: 0x04001A54 RID: 6740
		private static InotifyMask Interesting = InotifyMask.Modify | InotifyMask.Attrib | InotifyMask.MovedFrom | InotifyMask.MovedTo | InotifyMask.Create | InotifyMask.Delete | InotifyMask.DeleteSelf;
	}
}
