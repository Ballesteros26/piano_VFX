using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000024 RID: 36
	[TypeConverter(typeof(ResXFileRef.Converter))]
	[Serializable]
	internal class ResXFileRef
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004018 File Offset: 0x00002218
		public ResXFileRef(string fileName, string typeName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.filename = fileName;
			this.typename = typeName;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000404A File Offset: 0x0000224A
		public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding)
			: this(fileName, typeName)
		{
			this.textFileEncoding = textFileEncoding;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000405B File Offset: 0x0000225B
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004063 File Offset: 0x00002263
		public Encoding TextFileEncoding
		{
			get
			{
				return this.textFileEncoding;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000406B File Offset: 0x0000226B
		public string TypeName
		{
			get
			{
				return this.typename;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004074 File Offset: 0x00002274
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.filename != null)
			{
				stringBuilder.Append(this.filename);
			}
			stringBuilder.Append(';');
			if (this.typename != null)
			{
				stringBuilder.Append(this.typename);
			}
			if (this.textFileEncoding != null)
			{
				stringBuilder.Append(';');
				stringBuilder.Append(this.textFileEncoding.WebName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000040E3 File Offset: 0x000022E3
		internal static string[] Parse(string fileRef)
		{
			if (fileRef == null)
			{
				throw new ArgumentNullException("fileRef");
			}
			return fileRef.Split(new char[] { ';' });
		}

		// Token: 0x04000D70 RID: 3440
		private string filename;

		// Token: 0x04000D71 RID: 3441
		private string typename;

		// Token: 0x04000D72 RID: 3442
		private Encoding textFileEncoding;

		// Token: 0x02000025 RID: 37
		internal class Converter : TypeConverter
		{
			// Token: 0x060000B1 RID: 177 RVA: 0x0000410C File Offset: 0x0000230C
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x0000410C File Offset: 0x0000230C
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string);
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00004120 File Offset: 0x00002320
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (!(value is string))
				{
					return null;
				}
				string[] array = ResXFileRef.Parse((string)value);
				if (array.Length == 1)
				{
					throw new ArgumentException("value");
				}
				string text = array[0];
				if (Path.DirectorySeparatorChar == '/')
				{
					text = text.Replace("\\", "/");
				}
				Type type = Type.GetType(array[1]);
				if (type == typeof(string))
				{
					Encoding encoding;
					if (array.Length > 2)
					{
						encoding = Encoding.GetEncoding(array[2]);
					}
					else
					{
						encoding = Encoding.Default;
					}
					using (TextReader textReader = new StreamReader(text, encoding))
					{
						return textReader.ReadToEnd();
					}
				}
				byte[] array2;
				using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					array2 = new byte[fileStream.Length];
					fileStream.Read(array2, 0, (int)fileStream.Length);
				}
				if (type == typeof(byte[]))
				{
					return array2;
				}
				if (type == typeof(Bitmap) && Path.GetExtension(text) == ".ico")
				{
					return new Icon(new MemoryStream(array2)).ToBitmap();
				}
				if (type == typeof(MemoryStream))
				{
					return new MemoryStream(array2);
				}
				return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[]
				{
					new MemoryStream(array2)
				}, culture);
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000042A0 File Offset: 0x000024A0
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType != typeof(string))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				return ((ResXFileRef)value).ToString();
			}
		}
	}
}
