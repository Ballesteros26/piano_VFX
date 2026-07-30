using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A7 RID: 1191
	internal class FlavorHandler
	{
		// Token: 0x06004BCB RID: 19403 RVA: 0x0012D4C8 File Offset: 0x0012B6C8
		internal FlavorHandler(IntPtr dragref, IntPtr itemref, uint counter)
		{
			FlavorHandler.GetFlavorType(dragref, itemref, counter, ref this.flavorref);
			FlavorHandler.GetFlavorFlags(dragref, itemref, this.flavorref, ref this.flags);
			byte[] bytes = BitConverter.GetBytes((int)this.flavorref);
			this.fourcc = string.Format("{0}{1}{2}{3}", new object[]
			{
				(char)bytes[3],
				(char)bytes[2],
				(char)bytes[1],
				(char)bytes[0]
			});
			this.dragref = dragref;
			this.itemref = itemref;
			this.GetData();
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0012D56C File Offset: 0x0012B76C
		internal void GetData()
		{
			FlavorHandler.GetFlavorDataSize(this.dragref, this.itemref, this.flavorref, ref this.size);
			this.data = new byte[this.size];
			FlavorHandler.GetFlavorData(this.dragref, this.itemref, this.flavorref, this.data, ref this.size, 0U);
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0012D5D0 File Offset: 0x0012B7D0
		internal string DataString
		{
			get
			{
				return Encoding.Default.GetString(this.data);
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x0012D5E4 File Offset: 0x0012B7E4
		internal byte[] DataArray
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x0012D5EC File Offset: 0x0012B7EC
		internal IntPtr DataPtr
		{
			get
			{
				return (IntPtr)BitConverter.ToInt32(this.data, 0);
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x0012D600 File Offset: 0x0012B800
		internal bool Supported
		{
			get
			{
				string text = this.fourcc;
				if (text != null)
				{
					if (FlavorHandler.<>f__switch$mapF == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("furl", 0);
						dictionary.Add("mono", 1);
						dictionary.Add("mser", 2);
						FlavorHandler.<>f__switch$mapF = dictionary;
					}
					int num;
					if (FlavorHandler.<>f__switch$mapF.TryGetValue(text, ref num))
					{
						switch (num)
						{
						case 0:
							return true;
						case 1:
							return true;
						case 2:
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0012D688 File Offset: 0x0012B888
		internal DataObject Convert(ArrayList flavorlist)
		{
			string text = this.fourcc;
			if (text != null)
			{
				if (FlavorHandler.<>f__switch$map10 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("furl", 0);
					dictionary.Add("mono", 1);
					dictionary.Add("mser", 2);
					FlavorHandler.<>f__switch$map10 = dictionary;
				}
				int num;
				if (FlavorHandler.<>f__switch$map10.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						return this.ConvertToFileDrop(flavorlist);
					case 1:
						return this.ConvertToObject(flavorlist);
					case 2:
						return this.DeserializeObject(flavorlist);
					}
				}
			}
			return new DataObject();
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0012D724 File Offset: 0x0012B924
		internal DataObject DeserializeObject(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			MemoryStream memoryStream = new MemoryStream(this.DataArray);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			if (memoryStream.Length == 0L)
			{
				return dataObject;
			}
			memoryStream.Seek(0L, 0);
			dataObject.SetData(binaryFormatter.Deserialize(memoryStream));
			return dataObject;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0012D770 File Offset: 0x0012B970
		internal DataObject ConvertToObject(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			foreach (object obj in flavorlist)
			{
				FlavorHandler flavorHandler = (FlavorHandler)obj;
				dataObject.SetData(((GCHandle)flavorHandler.DataPtr).Target);
			}
			return dataObject;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0012D7F8 File Offset: 0x0012B9F8
		internal DataObject ConvertToFileDrop(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in flavorlist)
			{
				FlavorHandler flavorHandler = (FlavorHandler)obj;
				try
				{
					arrayList.Add(new Uri(flavorHandler.DataString).LocalPath);
				}
				catch
				{
				}
			}
			string[] array = (string[])arrayList.ToArray(typeof(string));
			if (array.Length < 1)
			{
				return dataObject;
			}
			dataObject.SetData(DataFormats.FileDrop, array);
			dataObject.SetData("FileName", array[0]);
			dataObject.SetData("FileNameW", array[0]);
			return dataObject;
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0012D8F4 File Offset: 0x0012BAF4
		public override string ToString()
		{
			return this.fourcc;
		}

		// Token: 0x06004BD6 RID: 19414
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorDataSize(IntPtr dragref, IntPtr itemref, IntPtr flavorref, ref int size);

		// Token: 0x06004BD7 RID: 19415
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorData(IntPtr dragref, IntPtr itemref, IntPtr flavorref, [In] [Out] byte[] data, ref int size, uint offset);

		// Token: 0x06004BD8 RID: 19416
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorFlags(IntPtr dragref, IntPtr itemref, IntPtr flavorref, ref uint flags);

		// Token: 0x06004BD9 RID: 19417
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorType(IntPtr dragref, IntPtr itemref, uint index, ref IntPtr flavor);

		// Token: 0x040028C2 RID: 10434
		internal IntPtr flavorref;

		// Token: 0x040028C3 RID: 10435
		internal IntPtr dragref;

		// Token: 0x040028C4 RID: 10436
		internal IntPtr itemref;

		// Token: 0x040028C5 RID: 10437
		internal int size;

		// Token: 0x040028C6 RID: 10438
		internal uint flags;

		// Token: 0x040028C7 RID: 10439
		internal byte[] data;

		// Token: 0x040028C8 RID: 10440
		internal string fourcc;
	}
}
