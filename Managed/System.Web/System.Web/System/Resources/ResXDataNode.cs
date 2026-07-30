using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal sealed class ResXDataNode : ISerializable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003C06 File Offset: 0x00001E06
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003C17 File Offset: 0x00001E17
		public string Comment
		{
			get
			{
				return this.comment ?? string.Empty;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003C20 File Offset: 0x00001E20
		public ResXFileRef FileRef
		{
			get
			{
				return this.fileRef;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003C28 File Offset: 0x00001E28
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003C39 File Offset: 0x00001E39
		public string Name
		{
			get
			{
				return this.name ?? string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("name");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("name");
				}
				this.name = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003C68 File Offset: 0x00001E68
		internal bool IsWritable
		{
			get
			{
				return this.handler is IWritableHandler;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C78 File Offset: 0x00001E78
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003C80 File Offset: 0x00001E80
		internal string MimeType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003C89 File Offset: 0x00001E89
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003C91 File Offset: 0x00001E91
		internal string Type { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003C9A File Offset: 0x00001E9A
		internal string DataString
		{
			get
			{
				if (this.IsWritable)
				{
					return ((IWritableHandler)this.handler).DataString;
				}
				throw new NotSupportedException("Node Not Writable");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003CBF File Offset: 0x00001EBF
		public ResXDataNode(string name, object value)
			: this(name, value, Point.Empty)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public ResXDataNode(string name, ResXFileRef fileRef)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileRef == null)
			{
				throw new ArgumentNullException("fileRef");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.name = name;
			this.fileRef = fileRef;
			this.pos = Point.Empty;
			this.handler = new FileRefHandler(fileRef);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003D38 File Offset: 0x00001F38
		internal ResXDataNode(string name, object value, Point position)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			Type type = ((value == null) ? typeof(object) : value.GetType());
			if (value != null && !type.IsSerializable)
			{
				throw new InvalidOperationException(string.Format("'{0}' of type '{1}' cannot be added because it is not serializable", name, type));
			}
			this.name = name;
			this.pos = position;
			this.handler = new InMemoryHandler(value);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DBC File Offset: 0x00001FBC
		internal ResXDataNode(string nameAtt, string mimeTypeAtt, string typeAtt, string dataString, string commentString, Point position, string basePath)
		{
			this.name = nameAtt;
			this.comment = commentString;
			this.pos = position;
			this.MimeType = mimeTypeAtt;
			this.Type = typeAtt;
			if (!string.IsNullOrEmpty(mimeTypeAtt))
			{
				if (!string.IsNullOrEmpty(typeAtt))
				{
					this.handler = new TypeConverterFromResXHandler(dataString, mimeTypeAtt, typeAtt);
				}
				else
				{
					this.handler = new SerializedFromResXHandler(dataString, mimeTypeAtt);
				}
			}
			else if (!string.IsNullOrEmpty(typeAtt))
			{
				if (typeAtt.StartsWith("System.Resources.ResXNullRef, System.Windows.Forms"))
				{
					this.handler = new NullRefHandler(typeAtt);
				}
				else if (typeAtt.StartsWith("System.Byte[], mscorlib"))
				{
					this.handler = new ByteArrayFromResXHandler(dataString);
				}
				else if (typeAtt.StartsWith("System.Resources.ResXFileRef, System.Windows.Forms"))
				{
					ResXFileRef resXFileRef = this.BuildFileRef(dataString, basePath);
					this.handler = new FileRefHandler(resXFileRef);
					this.fileRef = resXFileRef;
				}
				else
				{
					this.handler = new TypeConverterFromResXHandler(dataString, mimeTypeAtt, typeAtt);
				}
			}
			else
			{
				this.handler = new InMemoryHandler(dataString);
			}
			if (this.handler == null)
			{
				throw new Exception("handler is null");
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003EC8 File Offset: 0x000020C8
		public Point GetNodePosition()
		{
			return this.pos;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003ED0 File Offset: 0x000020D0
		public string GetValueTypeName(AssemblyName[] names)
		{
			return this.handler.GetValueTypeName(names);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003EDE File Offset: 0x000020DE
		public string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return this.handler.GetValueTypeName(typeResolver);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003EEC File Offset: 0x000020EC
		public object GetValue(AssemblyName[] names)
		{
			return this.handler.GetValue(names);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003EFA File Offset: 0x000020FA
		public object GetValue(ITypeResolutionService typeResolver)
		{
			return this.handler.GetValue(typeResolver);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F08 File Offset: 0x00002108
		private ResXFileRef BuildFileRef(string dataString, string basePath)
		{
			string[] array = ResXFileRef.Parse(dataString);
			if (array.Length < 2)
			{
				throw new ArgumentException("ResXFileRef cannot be generated");
			}
			string text = array[0];
			if (basePath != null)
			{
				text = Path.Combine(basePath, array[0]);
			}
			string text2 = array[1];
			ResXFileRef resXFileRef;
			if (array.Length == 3)
			{
				Encoding encoding = Encoding.GetEncoding(array[2]);
				resXFileRef = new ResXFileRef(text, text2, encoding);
			}
			else
			{
				resXFileRef = new ResXFileRef(text, text2);
			}
			return resXFileRef;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F67 File Offset: 0x00002167
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", this.Name);
			si.AddValue("Comment", this.Comment);
		}

		// Token: 0x04000D69 RID: 3433
		private string name;

		// Token: 0x04000D6A RID: 3434
		private ResXFileRef fileRef;

		// Token: 0x04000D6B RID: 3435
		private string comment;

		// Token: 0x04000D6C RID: 3436
		private Point pos;

		// Token: 0x04000D6D RID: 3437
		internal ResXDataNodeHandler handler;
	}
}
