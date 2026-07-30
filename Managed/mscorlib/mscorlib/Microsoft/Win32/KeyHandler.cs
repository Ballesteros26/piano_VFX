using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace Microsoft.Win32
{
	// Token: 0x020000B7 RID: 183
	internal class KeyHandler
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		static KeyHandler()
		{
			KeyHandler.CleanVolatileKeys();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001FC76 File Offset: 0x0001DE76
		private KeyHandler(RegistryKey rkey, string basedir)
			: this(rkey, basedir, false)
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001FC84 File Offset: 0x0001DE84
		private KeyHandler(RegistryKey rkey, string basedir, bool is_volatile)
		{
			string volatileDir = KeyHandler.GetVolatileDir(basedir);
			string text = basedir;
			if (Directory.Exists(basedir))
			{
				is_volatile = false;
			}
			else if (Directory.Exists(volatileDir))
			{
				text = volatileDir;
				is_volatile = true;
			}
			else if (is_volatile)
			{
				text = volatileDir;
			}
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (UnauthorizedAccessException ex)
				{
					throw new SecurityException("No access to the given key", ex);
				}
			}
			this.Dir = basedir;
			this.ActualDir = text;
			this.IsVolatile = is_volatile;
			this.file = Path.Combine(this.ActualDir, "values.xml");
			this.Load();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public void Load()
		{
			this.values = new Hashtable();
			if (!File.Exists(this.file))
			{
				return;
			}
			try
			{
				using (FileStream fileStream = File.OpenRead(this.file))
				{
					string text = new StreamReader(fileStream).ReadToEnd();
					if (text.Length != 0)
					{
						SecurityElement securityElement = SecurityElement.FromString(text);
						if (securityElement.Tag == "values" && securityElement.Children != null)
						{
							foreach (object obj in securityElement.Children)
							{
								SecurityElement securityElement2 = (SecurityElement)obj;
								if (securityElement2.Tag == "value")
								{
									this.LoadKey(securityElement2);
								}
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				this.values.Clear();
				throw new SecurityException("No access to the given key");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While loading registry key at {0}: {1}", this.file, ex);
				this.values.Clear();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001FE60 File Offset: 0x0001E060
		private void LoadKey(SecurityElement se)
		{
			Hashtable attributes = se.Attributes;
			try
			{
				string text = (string)attributes["name"];
				if (text != null)
				{
					string text2 = (string)attributes["type"];
					if (text2 != null)
					{
						if (!(text2 == "int"))
						{
							if (!(text2 == "bytearray"))
							{
								if (!(text2 == "string"))
								{
									if (!(text2 == "expand"))
									{
										if (!(text2 == "qword"))
										{
											if (text2 == "string-array")
											{
												List<string> list = new List<string>();
												if (se.Children != null)
												{
													foreach (object obj in se.Children)
													{
														SecurityElement securityElement = (SecurityElement)obj;
														list.Add(securityElement.Text);
													}
												}
												this.values[text] = list.ToArray();
											}
										}
										else
										{
											this.values[text] = long.Parse(se.Text);
										}
									}
									else
									{
										this.values[text] = new ExpandString(se.Text);
									}
								}
								else
								{
									this.values[text] = ((se.Text == null) ? string.Empty : se.Text);
								}
							}
							else
							{
								this.values[text] = Convert.FromBase64String(se.Text);
							}
						}
						else
						{
							this.values[text] = int.Parse(se.Text);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00020044 File Offset: 0x0001E244
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable)
		{
			return this.Ensure(rkey, extra, writable, false);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00020050 File Offset: 0x0001E250
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable, bool is_volatile)
		{
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey registryKey2;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler == null)
				{
					keyHandler = new KeyHandler(rkey, text, is_volatile);
				}
				RegistryKey registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
				KeyHandler.key_to_handler[registryKey] = keyHandler;
				KeyHandler.dir_to_handler[text] = keyHandler;
				registryKey2 = registryKey;
			}
			return registryKey2;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000200E8 File Offset: 0x0001E2E8
		public RegistryKey Probe(RegistryKey rkey, string extra, bool writable)
		{
			RegistryKey registryKey = null;
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey registryKey2;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler != null)
				{
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				else if (Directory.Exists(text) || KeyHandler.VolatileKeyExists(text))
				{
					keyHandler = new KeyHandler(rkey, text);
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.dir_to_handler[text] = keyHandler;
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				registryKey2 = registryKey;
			}
			return registryKey2;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000201B4 File Offset: 0x0001E3B4
		private static string CombineName(RegistryKey rkey, string extra)
		{
			if (extra.IndexOf('/') != -1)
			{
				extra = extra.Replace('/', '\\');
			}
			return rkey.Name + "\\" + extra;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000201E0 File Offset: 0x0001E3E0
		private static long GetSystemBootTime()
		{
			if (!File.Exists("/proc/stat"))
			{
				return -1L;
			}
			string text = null;
			try
			{
				using (StreamReader streamReader = new StreamReader("/proc/stat", Encoding.ASCII))
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						if (text2.StartsWith("btime"))
						{
							text = text2;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While reading system info {0}", ex);
			}
			if (text == null)
			{
				return -1L;
			}
			int num = text.IndexOf(' ');
			long num2;
			if (!long.TryParse(text.Substring(num, text.Length - num), out num2))
			{
				return -1L;
			}
			return num2;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002029C File Offset: 0x0001E49C
		private static long GetRegisteredBootTime(string path)
		{
			if (!File.Exists(path))
			{
				return -1L;
			}
			string text = null;
			try
			{
				using (StreamReader streamReader = new StreamReader(path, Encoding.ASCII))
				{
					text = streamReader.ReadLine();
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While reading registry data at {0}: {1}", path, ex);
			}
			if (text == null)
			{
				return -1L;
			}
			long num;
			if (!long.TryParse(text, out num))
			{
				return -1L;
			}
			return num;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002031C File Offset: 0x0001E51C
		private static void SaveRegisteredBootTime(string path, long btime)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.ASCII))
				{
					streamWriter.WriteLine(btime.ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00020370 File Offset: 0x0001E570
		private static void CleanVolatileKeys()
		{
			long systemBootTime = KeyHandler.GetSystemBootTime();
			foreach (string text in new string[]
			{
				KeyHandler.UserStore,
				KeyHandler.MachineStore
			})
			{
				if (Directory.Exists(text))
				{
					string text2 = Path.Combine(text, "last-btime");
					string text3 = Path.Combine(text, "volatile-keys");
					if (Directory.Exists(text3))
					{
						long registeredBootTime = KeyHandler.GetRegisteredBootTime(text2);
						if (systemBootTime < 0L || registeredBootTime < 0L || registeredBootTime != systemBootTime)
						{
							Directory.Delete(text3, true);
						}
					}
					KeyHandler.SaveRegisteredBootTime(text2, systemBootTime);
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00020400 File Offset: 0x0001E600
		public static bool VolatileKeyExists(string dir)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[dir];
				if (keyHandler != null)
				{
					return keyHandler.IsVolatile;
				}
			}
			return !Directory.Exists(dir) && Directory.Exists(KeyHandler.GetVolatileDir(dir));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00020474 File Offset: 0x0001E674
		public static string GetVolatileDir(string dir)
		{
			string rootFromDir = KeyHandler.GetRootFromDir(dir);
			return dir.Replace(rootFromDir, Path.Combine(rootFromDir, "volatile-keys"));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002049C File Offset: 0x0001E69C
		public static KeyHandler Lookup(RegistryKey rkey, bool createNonExisting)
		{
			Type typeFromHandle = typeof(KeyHandler);
			KeyHandler keyHandler2;
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					keyHandler2 = keyHandler;
				}
				else if (!rkey.IsRoot || !createNonExisting)
				{
					keyHandler2 = null;
				}
				else
				{
					RegistryHive hive = rkey.Hive;
					switch (hive)
					{
					case RegistryHive.ClassesRoot:
					case RegistryHive.LocalMachine:
					case RegistryHive.Users:
					case RegistryHive.PerformanceData:
					case RegistryHive.CurrentConfig:
					case RegistryHive.DynData:
					{
						string text = Path.Combine(KeyHandler.MachineStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text);
						KeyHandler.dir_to_handler[text] = keyHandler;
						break;
					}
					case RegistryHive.CurrentUser:
					{
						string text2 = Path.Combine(KeyHandler.UserStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text2);
						KeyHandler.dir_to_handler[text2] = keyHandler;
						break;
					}
					default:
						throw new Exception("Unknown RegistryHive");
					}
					KeyHandler.key_to_handler[rkey] = keyHandler;
					keyHandler2 = keyHandler;
				}
			}
			return keyHandler2;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000205B8 File Offset: 0x0001E7B8
		private static string GetRootFromDir(string dir)
		{
			if (dir.IndexOf(KeyHandler.UserStore) > -1)
			{
				return KeyHandler.UserStore;
			}
			if (dir.IndexOf(KeyHandler.MachineStore) > -1)
			{
				return KeyHandler.MachineStore;
			}
			throw new Exception("Could not get root for dir " + dir);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000205F4 File Offset: 0x0001E7F4
		public static void Drop(RegistryKey rkey)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					KeyHandler.key_to_handler.Remove(rkey);
					int num = 0;
					foreach (object obj in KeyHandler.key_to_handler)
					{
						if (((DictionaryEntry)obj).Value == keyHandler)
						{
							num++;
						}
					}
					if (num == 0)
					{
						KeyHandler.dir_to_handler.Remove(keyHandler.Dir);
					}
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000206C0 File Offset: 0x0001E8C0
		public static void Drop(string dir)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[dir];
				if (keyHandler != null)
				{
					KeyHandler.dir_to_handler.Remove(dir);
					ArrayList arrayList = new ArrayList();
					foreach (object obj in KeyHandler.key_to_handler)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value == keyHandler)
						{
							arrayList.Add(dictionaryEntry.Key);
						}
					}
					foreach (object obj2 in arrayList)
					{
						KeyHandler.key_to_handler.Remove(obj2);
					}
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000207D4 File Offset: 0x0001E9D4
		public static bool Delete(string dir)
		{
			if (!Directory.Exists(dir))
			{
				string volatileDir = KeyHandler.GetVolatileDir(dir);
				if (!Directory.Exists(volatileDir))
				{
					return false;
				}
				dir = volatileDir;
			}
			Directory.Delete(dir, true);
			KeyHandler.Drop(dir);
			return true;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002080C File Offset: 0x0001EA0C
		public RegistryValueKind GetValueKind(string name)
		{
			if (name == null)
			{
				return RegistryValueKind.Unknown;
			}
			Hashtable hashtable = this.values;
			object obj;
			lock (hashtable)
			{
				obj = this.values[name];
			}
			if (obj == null)
			{
				return RegistryValueKind.Unknown;
			}
			if (obj is int)
			{
				return RegistryValueKind.DWord;
			}
			if (obj is string[])
			{
				return RegistryValueKind.MultiString;
			}
			if (obj is long)
			{
				return RegistryValueKind.QWord;
			}
			if (obj is byte[])
			{
				return RegistryValueKind.Binary;
			}
			if (obj is string)
			{
				return RegistryValueKind.String;
			}
			if (obj is ExpandString)
			{
				return RegistryValueKind.ExpandString;
			}
			return RegistryValueKind.Unknown;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002089C File Offset: 0x0001EA9C
		public object GetValue(string name, RegistryValueOptions options)
		{
			if (this.IsMarkedForDeletion)
			{
				return null;
			}
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			object obj;
			lock (hashtable)
			{
				obj = this.values[name];
			}
			ExpandString expandString = obj as ExpandString;
			if (expandString == null)
			{
				return obj;
			}
			if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
			{
				return expandString.Expand();
			}
			return expandString.ToString();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00020918 File Offset: 0x0001EB18
		public void SetValue(string name, object value)
		{
			this.AssertNotMarkedForDeletion();
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				if (value is int || value is string || value is byte[] || value is string[])
				{
					this.values[name] = value;
				}
				else
				{
					this.values[name] = value.ToString();
				}
			}
			this.SetDirty();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000209AC File Offset: 0x0001EBAC
		public string[] GetValueNames()
		{
			this.AssertNotMarkedForDeletion();
			Hashtable hashtable = this.values;
			string[] array2;
			lock (hashtable)
			{
				ICollection keys = this.values.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00020A10 File Offset: 0x0001EC10
		public int GetSubKeyCount()
		{
			return this.GetSubKeyNames().Length;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00020A1C File Offset: 0x0001EC1C
		public string[] GetSubKeyNames()
		{
			DirectoryInfo[] directories = new DirectoryInfo(this.ActualDir).GetDirectories();
			string[] array;
			if (this.IsVolatile || !Directory.Exists(KeyHandler.GetVolatileDir(this.Dir)))
			{
				array = new string[directories.Length];
				for (int i = 0; i < directories.Length; i++)
				{
					DirectoryInfo directoryInfo = directories[i];
					array[i] = directoryInfo.Name;
				}
				return array;
			}
			DirectoryInfo[] directories2 = new DirectoryInfo(KeyHandler.GetVolatileDir(this.Dir)).GetDirectories();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				dictionary[directoryInfo2.Name] = directoryInfo2.Name;
			}
			foreach (DirectoryInfo directoryInfo3 in directories2)
			{
				dictionary[directoryInfo3.Name] = directoryInfo3.Name;
			}
			array = new string[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				array[num++] = keyValuePair.Value;
			}
			return array;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00020B5C File Offset: 0x0001ED5C
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			this.SetDirty();
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				switch (valueKind)
				{
				case RegistryValueKind.String:
					if (value is string)
					{
						this.values[name] = value;
						return;
					}
					goto IL_0116;
				case RegistryValueKind.ExpandString:
					if (value is string)
					{
						this.values[name] = new ExpandString((string)value);
						return;
					}
					goto IL_0116;
				case RegistryValueKind.Binary:
					if (value is byte[])
					{
						this.values[name] = value;
						return;
					}
					goto IL_0116;
				case RegistryValueKind.DWord:
					try
					{
						this.values[name] = Convert.ToInt32(value);
						return;
					}
					catch (OverflowException)
					{
						goto IL_0122;
					}
					break;
				case (RegistryValueKind)5:
				case (RegistryValueKind)6:
				case (RegistryValueKind)8:
				case (RegistryValueKind)9:
				case (RegistryValueKind)10:
					goto IL_0106;
				case RegistryValueKind.MultiString:
					break;
				case RegistryValueKind.QWord:
					try
					{
						this.values[name] = Convert.ToInt64(value);
						return;
					}
					catch (OverflowException)
					{
						goto IL_0122;
					}
					goto IL_0106;
				default:
					goto IL_0106;
				}
				if (value is string[])
				{
					this.values[name] = value;
					return;
				}
				goto IL_0116;
				IL_0106:
				throw new ArgumentException("unknown value", "valueKind");
				IL_0116:;
			}
			IL_0122:
			throw new ArgumentException("Value could not be converted to specified type", "valueKind");
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		private void SetDirty()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (!this.dirty)
				{
					this.dirty = true;
					new Timer(new TimerCallback(this.DirtyTimeout), null, 3000, -1);
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00020D2C File Offset: 0x0001EF2C
		public void DirtyTimeout(object state)
		{
			this.Flush();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00020D34 File Offset: 0x0001EF34
		public void Flush()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (this.dirty)
				{
					this.Save();
					this.dirty = false;
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00020D88 File Offset: 0x0001EF88
		public bool ValueExists(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			bool flag2;
			lock (hashtable)
			{
				flag2 = this.values.Contains(name);
			}
			return flag2;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00020DDC File Offset: 0x0001EFDC
		public int ValueCount
		{
			get
			{
				Hashtable hashtable = this.values;
				int count;
				lock (hashtable)
				{
					count = this.values.Keys.Count;
				}
				return count;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00020E28 File Offset: 0x0001F028
		public bool IsMarkedForDeletion
		{
			get
			{
				return !KeyHandler.dir_to_handler.Contains(this.Dir);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00020E40 File Offset: 0x0001F040
		public void RemoveValue(string name)
		{
			this.AssertNotMarkedForDeletion();
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				this.values.Remove(name);
			}
			this.SetDirty();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00020E94 File Offset: 0x0001F094
		~KeyHandler()
		{
			this.Flush();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		private void Save()
		{
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			SecurityElement securityElement = new SecurityElement("values");
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				if (!File.Exists(this.file) && this.values.Count == 0)
				{
					return;
				}
				foreach (object obj in this.values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					SecurityElement securityElement2 = new SecurityElement("value");
					securityElement2.AddAttribute("name", SecurityElement.Escape((string)dictionaryEntry.Key));
					if (value is string)
					{
						securityElement2.AddAttribute("type", "string");
						securityElement2.Text = SecurityElement.Escape((string)value);
					}
					else if (value is int)
					{
						securityElement2.AddAttribute("type", "int");
						securityElement2.Text = value.ToString();
					}
					else if (value is long)
					{
						securityElement2.AddAttribute("type", "qword");
						securityElement2.Text = value.ToString();
					}
					else if (value is byte[])
					{
						securityElement2.AddAttribute("type", "bytearray");
						securityElement2.Text = Convert.ToBase64String((byte[])value);
					}
					else if (value is ExpandString)
					{
						securityElement2.AddAttribute("type", "expand");
						securityElement2.Text = SecurityElement.Escape(value.ToString());
					}
					else if (value is string[])
					{
						securityElement2.AddAttribute("type", "string-array");
						foreach (string text in (string[])value)
						{
							securityElement2.AddChild(new SecurityElement("string")
							{
								Text = SecurityElement.Escape(text)
							});
						}
					}
					securityElement.AddChild(securityElement2);
				}
			}
			using (FileStream fileStream = File.Create(this.file))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(securityElement.ToString());
				streamWriter.Flush();
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00021160 File Offset: 0x0001F360
		private void AssertNotMarkedForDeletion()
		{
			if (this.IsMarkedForDeletion)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00021170 File Offset: 0x0001F370
		private static string UserStore
		{
			get
			{
				if (KeyHandler.user_store == null)
				{
					KeyHandler.user_store = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".mono/registry");
				}
				return KeyHandler.user_store;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00021194 File Offset: 0x0001F394
		private static string MachineStore
		{
			get
			{
				if (KeyHandler.machine_store == null)
				{
					KeyHandler.machine_store = Environment.GetEnvironmentVariable("MONO_REGISTRY_PATH");
					if (KeyHandler.machine_store == null)
					{
						string machineConfigPath = Environment.GetMachineConfigPath();
						int num = machineConfigPath.IndexOf("machine.config");
						KeyHandler.machine_store = Path.Combine(Path.Combine(machineConfigPath.Substring(0, num - 1), ".."), "registry");
					}
				}
				return KeyHandler.machine_store;
			}
		}

		// Token: 0x0400063C RID: 1596
		private static Hashtable key_to_handler = new Hashtable(new RegistryKeyComparer());

		// Token: 0x0400063D RID: 1597
		private static Hashtable dir_to_handler = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());

		// Token: 0x0400063E RID: 1598
		private const string VolatileDirectoryName = "volatile-keys";

		// Token: 0x0400063F RID: 1599
		public string Dir;

		// Token: 0x04000640 RID: 1600
		private string ActualDir;

		// Token: 0x04000641 RID: 1601
		public bool IsVolatile;

		// Token: 0x04000642 RID: 1602
		private Hashtable values;

		// Token: 0x04000643 RID: 1603
		private string file;

		// Token: 0x04000644 RID: 1604
		private bool dirty;

		// Token: 0x04000645 RID: 1605
		private static string user_store;

		// Token: 0x04000646 RID: 1606
		private static string machine_store;
	}
}
