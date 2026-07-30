using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;

namespace System.Windows.Forms
{
	// Token: 0x02000180 RID: 384
	internal class MWFConfig
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x0005F89C File Offset: 0x0005DA9C
		public static object GetValue(string class_name, string value_name)
		{
			object obj = MWFConfig.lock_object;
			object value;
			lock (obj)
			{
				value = MWFConfig.Instance.GetValue(class_name, value_name);
			}
			return value;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0005F8F0 File Offset: 0x0005DAF0
		public static void SetValue(string class_name, string value_name, object value)
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.SetValue(class_name, value_name, value);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005F940 File Offset: 0x0005DB40
		public static void Flush()
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.Flush();
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005F98C File Offset: 0x0005DB8C
		public static void RemoveClass(string class_name)
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.RemoveClass(class_name);
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005F9D8 File Offset: 0x0005DBD8
		public static void RemoveClassValue(string class_name, string value_name)
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.RemoveClassValue(class_name, value_name);
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public static void RemoveAllClassValues(string class_name)
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.RemoveAllClassValues(class_name);
			}
		}

		// Token: 0x04000E26 RID: 3622
		private static MWFConfig.MWFConfigInstance Instance = new MWFConfig.MWFConfigInstance();

		// Token: 0x04000E27 RID: 3623
		private static object lock_object = new object();

		// Token: 0x02000181 RID: 385
		internal class MWFConfigInstance
		{
			// Token: 0x0600191B RID: 6427 RVA: 0x0005FA74 File Offset: 0x0005DC74
			public MWFConfigInstance()
			{
				this.Open(MWFConfig.MWFConfigInstance.default_file_name);
			}

			// Token: 0x0600191C RID: 6428 RVA: 0x0005FAA0 File Offset: 0x0005DCA0
			public MWFConfigInstance(string filename)
			{
				string text = Path.GetDirectoryName(filename);
				if (text == null || text == string.Empty)
				{
					text = Environment.GetFolderPath(5);
					MWFConfig.MWFConfigInstance.full_file_name = Path.Combine(text, filename);
				}
				else
				{
					MWFConfig.MWFConfigInstance.full_file_name = filename;
				}
				this.Open(MWFConfig.MWFConfigInstance.full_file_name);
			}

			// Token: 0x0600191D RID: 6429 RVA: 0x0005FB10 File Offset: 0x0005DD10
			static MWFConfigInstance()
			{
				string text = "mwf_config";
				string text2 = Environment.GetFolderPath(5);
				if (XplatUI.RunningOnUnix)
				{
					text2 = Path.Combine(text2, ".mono");
					try
					{
						Directory.CreateDirectory(text2);
					}
					catch
					{
					}
				}
				MWFConfig.MWFConfigInstance.default_file_name = Path.Combine(text2, text);
				MWFConfig.MWFConfigInstance.full_file_name = MWFConfig.MWFConfigInstance.default_file_name;
			}

			// Token: 0x0600191E RID: 6430 RVA: 0x0005FB84 File Offset: 0x0005DD84
			~MWFConfigInstance()
			{
				this.Flush();
			}

			// Token: 0x0600191F RID: 6431 RVA: 0x0005FBC0 File Offset: 0x0005DDC0
			public object GetValue(string class_name, string value_name)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry != null)
				{
					return classEntry.GetValue(value_name);
				}
				return null;
			}

			// Token: 0x06001920 RID: 6432 RVA: 0x0005FBF0 File Offset: 0x0005DDF0
			public void SetValue(string class_name, string value_name, object value)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry == null)
				{
					classEntry = new MWFConfig.MWFConfigInstance.ClassEntry();
					classEntry.ClassName = class_name;
					this.classes_hashtable[class_name] = classEntry;
				}
				classEntry.SetValue(value_name, value);
			}

			// Token: 0x06001921 RID: 6433 RVA: 0x0005FC38 File Offset: 0x0005DE38
			private void Open(string filename)
			{
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(filename);
					this.ReadConfig(xmlTextReader);
					xmlTextReader.Close();
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06001922 RID: 6434 RVA: 0x0005FC80 File Offset: 0x0005DE80
			public void Flush()
			{
				try
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(MWFConfig.MWFConfigInstance.full_file_name, null);
					xmlTextWriter.Formatting = 1;
					this.WriteConfig(xmlTextWriter);
					xmlTextWriter.Close();
					if (!XplatUI.RunningOnUnix)
					{
						File.SetAttributes(MWFConfig.MWFConfigInstance.full_file_name, 2);
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06001923 RID: 6435 RVA: 0x0005FCEC File Offset: 0x0005DEEC
			public void RemoveClass(string class_name)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry != null)
				{
					classEntry.RemoveAllClassValues();
					this.classes_hashtable.Remove(class_name);
				}
			}

			// Token: 0x06001924 RID: 6436 RVA: 0x0005FD24 File Offset: 0x0005DF24
			public void RemoveClassValue(string class_name, string value_name)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry != null)
				{
					classEntry.RemoveClassValue(value_name);
				}
			}

			// Token: 0x06001925 RID: 6437 RVA: 0x0005FD50 File Offset: 0x0005DF50
			public void RemoveAllClassValues(string class_name)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry != null)
				{
					classEntry.RemoveAllClassValues();
				}
			}

			// Token: 0x06001926 RID: 6438 RVA: 0x0005FD7C File Offset: 0x0005DF7C
			private void ReadConfig(XmlTextReader xtr)
			{
				if (!this.CheckForMWFConfig(xtr))
				{
					return;
				}
				while (xtr.Read())
				{
					XmlNodeType nodeType = xtr.NodeType;
					if (nodeType == 1)
					{
						MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[xtr.Name] as MWFConfig.MWFConfigInstance.ClassEntry;
						if (classEntry == null)
						{
							classEntry = new MWFConfig.MWFConfigInstance.ClassEntry();
							classEntry.ClassName = xtr.Name;
							this.classes_hashtable[xtr.Name] = classEntry;
						}
						classEntry.ReadXml(xtr);
					}
				}
			}

			// Token: 0x06001927 RID: 6439 RVA: 0x0005FE08 File Offset: 0x0005E008
			private bool CheckForMWFConfig(XmlTextReader xtr)
			{
				return xtr.Read() && xtr.NodeType == 1 && xtr.Name == this.configName;
			}

			// Token: 0x06001928 RID: 6440 RVA: 0x0005FE48 File Offset: 0x0005E048
			private void WriteConfig(XmlTextWriter xtw)
			{
				if (this.classes_hashtable.Count == 0)
				{
					return;
				}
				xtw.WriteStartElement(this.configName);
				foreach (object obj in this.classes_hashtable)
				{
					MWFConfig.MWFConfigInstance.ClassEntry classEntry = ((DictionaryEntry)obj).Value as MWFConfig.MWFConfigInstance.ClassEntry;
					classEntry.WriteXml(xtw);
				}
				xtw.WriteEndElement();
			}

			// Token: 0x04000E28 RID: 3624
			private Hashtable classes_hashtable = new Hashtable();

			// Token: 0x04000E29 RID: 3625
			private static string full_file_name;

			// Token: 0x04000E2A RID: 3626
			private static string default_file_name;

			// Token: 0x04000E2B RID: 3627
			private readonly string configName = "MWFConfig";

			// Token: 0x02000182 RID: 386
			internal class ClassEntry
			{
				// Token: 0x170005FE RID: 1534
				// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005FF08 File Offset: 0x0005E108
				// (set) Token: 0x0600192A RID: 6442 RVA: 0x0005FEFC File Offset: 0x0005E0FC
				public string ClassName
				{
					get
					{
						return this.className;
					}
					set
					{
						this.className = value;
					}
				}

				// Token: 0x0600192C RID: 6444 RVA: 0x0005FF10 File Offset: 0x0005E110
				public void SetValue(string value_name, object value)
				{
					MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[value_name] as MWFConfig.MWFConfigInstance.ClassValue;
					if (classValue == null)
					{
						classValue = new MWFConfig.MWFConfigInstance.ClassValue();
						classValue.Name = value_name;
						this.classvalues_hashtable[value_name] = classValue;
					}
					classValue.SetValue(value);
				}

				// Token: 0x0600192D RID: 6445 RVA: 0x0005FF58 File Offset: 0x0005E158
				public object GetValue(string value_name)
				{
					MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[value_name] as MWFConfig.MWFConfigInstance.ClassValue;
					if (classValue == null)
					{
						return null;
					}
					return classValue.GetValue();
				}

				// Token: 0x0600192E RID: 6446 RVA: 0x0005FF88 File Offset: 0x0005E188
				public void RemoveAllClassValues()
				{
					this.classvalues_hashtable.Clear();
				}

				// Token: 0x0600192F RID: 6447 RVA: 0x0005FF98 File Offset: 0x0005E198
				public void RemoveClassValue(string value_name)
				{
					MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[value_name] as MWFConfig.MWFConfigInstance.ClassValue;
					if (classValue != null)
					{
						this.classvalues_hashtable.Remove(value_name);
					}
				}

				// Token: 0x06001930 RID: 6448 RVA: 0x0005FFCC File Offset: 0x0005E1CC
				public void ReadXml(XmlTextReader xtr)
				{
					while (xtr.Read())
					{
						XmlNodeType nodeType = xtr.NodeType;
						if (nodeType != 1)
						{
							if (nodeType == 15)
							{
								return;
							}
						}
						else
						{
							string attribute = xtr.GetAttribute("name");
							MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[attribute] as MWFConfig.MWFConfigInstance.ClassValue;
							if (classValue == null)
							{
								classValue = new MWFConfig.MWFConfigInstance.ClassValue();
								classValue.Name = attribute;
								this.classvalues_hashtable[attribute] = classValue;
							}
							classValue.ReadXml(xtr);
						}
					}
				}

				// Token: 0x06001931 RID: 6449 RVA: 0x00060050 File Offset: 0x0005E250
				public void WriteXml(XmlTextWriter xtw)
				{
					if (this.classvalues_hashtable.Count == 0)
					{
						return;
					}
					xtw.WriteStartElement(this.className);
					foreach (object obj in this.classvalues_hashtable)
					{
						MWFConfig.MWFConfigInstance.ClassValue classValue = ((DictionaryEntry)obj).Value as MWFConfig.MWFConfigInstance.ClassValue;
						classValue.WriteXml(xtw);
					}
					xtw.WriteEndElement();
				}

				// Token: 0x04000E2C RID: 3628
				private Hashtable classvalues_hashtable = new Hashtable();

				// Token: 0x04000E2D RID: 3629
				private string className;
			}

			// Token: 0x02000183 RID: 387
			internal class ClassValue
			{
				// Token: 0x170005FF RID: 1535
				// (get) Token: 0x06001934 RID: 6452 RVA: 0x00060104 File Offset: 0x0005E304
				// (set) Token: 0x06001933 RID: 6451 RVA: 0x000600F8 File Offset: 0x0005E2F8
				public string Name
				{
					get
					{
						return this.name;
					}
					set
					{
						this.name = value;
					}
				}

				// Token: 0x06001935 RID: 6453 RVA: 0x0006010C File Offset: 0x0005E30C
				public void SetValue(object value)
				{
					this.value = value;
				}

				// Token: 0x06001936 RID: 6454 RVA: 0x00060118 File Offset: 0x0005E318
				public object GetValue()
				{
					return this.value;
				}

				// Token: 0x06001937 RID: 6455 RVA: 0x00060120 File Offset: 0x0005E320
				public void ReadXml(XmlTextReader xtr)
				{
					string attribute = xtr.GetAttribute("type");
					if (attribute == "byte_array" || attribute.IndexOf("-array") == -1)
					{
						string text = xtr.ReadString();
						if (attribute == "string")
						{
							this.value = text;
						}
						else if (attribute == "int")
						{
							this.value = int.Parse(text);
						}
						else if (attribute == "byte")
						{
							this.value = byte.Parse(text);
						}
						else if (attribute == "color")
						{
							int num = int.Parse(text);
							this.value = Color.FromArgb(num);
						}
						else if (attribute == "byte-array")
						{
							byte[] array = Convert.FromBase64String(text);
							this.value = array;
						}
					}
					else
					{
						this.ReadXmlArrayValues(xtr, attribute);
					}
				}

				// Token: 0x06001938 RID: 6456 RVA: 0x00060224 File Offset: 0x0005E424
				private void ReadXmlArrayValues(XmlTextReader xtr, string type)
				{
					ArrayList arrayList = new ArrayList();
					while (xtr.Read())
					{
						XmlNodeType nodeType = xtr.NodeType;
						if (nodeType != 1)
						{
							if (nodeType == 15)
							{
								if (xtr.Name == "value")
								{
									if (type == "int-array")
									{
										this.value = arrayList.ToArray(typeof(int));
									}
									else if (type == "string-array")
									{
										this.value = arrayList.ToArray(typeof(string));
									}
									return;
								}
							}
						}
						else
						{
							string text = xtr.ReadString();
							if (type == "int-array")
							{
								int num = int.Parse(text);
								arrayList.Add(num);
							}
							else if (type == "string-array")
							{
								string text2 = text;
								arrayList.Add(text2);
							}
						}
					}
				}

				// Token: 0x06001939 RID: 6457 RVA: 0x00060320 File Offset: 0x0005E520
				public void WriteXml(XmlTextWriter xtw)
				{
					xtw.WriteStartElement("value");
					xtw.WriteAttributeString("name", this.name);
					if (this.value is Array)
					{
						this.WriteArrayContent(xtw);
					}
					else
					{
						this.WriteSingleContent(xtw);
					}
					xtw.WriteEndElement();
				}

				// Token: 0x0600193A RID: 6458 RVA: 0x00060374 File Offset: 0x0005E574
				private void WriteSingleContent(XmlTextWriter xtw)
				{
					string text = string.Empty;
					if (this.value is string)
					{
						text = "string";
					}
					else if (this.value is int)
					{
						text = "int";
					}
					else if (this.value is byte)
					{
						text = "byte";
					}
					else if (this.value is Color)
					{
						text = "color";
					}
					xtw.WriteAttributeString("type", text);
					if (this.value is Color)
					{
						xtw.WriteString(((Color)this.value).ToArgb().ToString());
					}
					else
					{
						xtw.WriteString(this.value.ToString());
					}
				}

				// Token: 0x0600193B RID: 6459 RVA: 0x00060444 File Offset: 0x0005E644
				private void WriteArrayContent(XmlTextWriter xtw)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					if (this.value is string[])
					{
						text = "string-array";
						text2 = "string";
					}
					else if (this.value is int[])
					{
						text = "int-array";
						text2 = "int";
					}
					else if (this.value is byte[])
					{
						text = "byte-array";
						text2 = "byte";
					}
					xtw.WriteAttributeString("type", text);
					if (text != "byte-array")
					{
						Array array = this.value as Array;
						foreach (object obj in array)
						{
							xtw.WriteStartElement(text2);
							xtw.WriteString(obj.ToString());
							xtw.WriteEndElement();
						}
					}
					else
					{
						byte[] array2 = this.value as byte[];
						xtw.WriteString(Convert.ToBase64String(array2, 0, array2.Length));
					}
				}

				// Token: 0x04000E2E RID: 3630
				private object value;

				// Token: 0x04000E2F RID: 3631
				private string name;
			}
		}
	}
}
