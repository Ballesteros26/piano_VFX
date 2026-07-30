using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Resources
{
	/// <summary>Represents a link to an external resource.</summary>
	// Token: 0x0200000B RID: 11
	[TypeConverter(typeof(ResXFileRef.Converter))]
	[Serializable]
	public class ResXFileRef
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file.</summary>
		/// <param name="fileName">The file to reference. </param>
		/// <param name="typeName">The type of the resource that is referenced. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> or <paramref name="typeName " />is null.</exception>
		// Token: 0x0600001B RID: 27 RVA: 0x00002310 File Offset: 0x00000510
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file. </summary>
		/// <param name="fileName">The file to reference. </param>
		/// <param name="typeName">The type name of the resource that is referenced. </param>
		/// <param name="textFileEncoding">The encoding used in the referenced file.</param>
		// Token: 0x0600001C RID: 28 RVA: 0x00002354 File Offset: 0x00000554
		public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding)
			: this(fileName, typeName)
		{
			this.textFileEncoding = textFileEncoding;
		}

		/// <summary>Gets the file name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
		/// <returns>The name of the referenced file.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002368 File Offset: 0x00000568
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the encoding specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
		/// <returns>The encoding used in the referenced file.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002370 File Offset: 0x00000570
		public Encoding TextFileEncoding
		{
			get
			{
				return this.textFileEncoding;
			}
		}

		/// <summary>Gets the type name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor. </summary>
		/// <returns>The type name of the resource that is referenced. </returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002378 File Offset: 0x00000578
		public string TypeName
		{
			get
			{
				return this.typename;
			}
		}

		/// <summary>Gets the text representation of the current <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
		/// <returns>A string that consists of the concatenated text representations of the parameters specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</returns>
		// Token: 0x06000020 RID: 32 RVA: 0x00002380 File Offset: 0x00000580
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

		// Token: 0x06000021 RID: 33 RVA: 0x000023F8 File Offset: 0x000005F8
		internal static string[] Parse(string fileRef)
		{
			if (fileRef == null)
			{
				throw new ArgumentNullException("fileRef");
			}
			return fileRef.Split(new char[] { ';' });
		}

		// Token: 0x04000025 RID: 37
		private string filename;

		// Token: 0x04000026 RID: 38
		private string typename;

		// Token: 0x04000027 RID: 39
		private Encoding textFileEncoding;

		/// <summary>Provides a type converter to convert data for a <see cref="T:System.Resources.ResXFileRef" /> to and from a string.</summary>
		// Token: 0x0200000C RID: 12
		public class Converter : TypeConverter
		{
			/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
			/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
			// Token: 0x06000023 RID: 35 RVA: 0x00002430 File Offset: 0x00000630
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}

			/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
			/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
			// Token: 0x06000024 RID: 36 RVA: 0x00002440 File Offset: 0x00000640
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string);
			}

			/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
			// Token: 0x06000025 RID: 37 RVA: 0x00002450 File Offset: 0x00000650
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
					using (TextReader textReader = new StreamReader(array[0], encoding))
					{
						return textReader.ReadToEnd();
					}
				}
				byte[] array2;
				using (FileStream fileStream = new FileStream(array[0], 3, 1, 1))
				{
					array2 = new byte[fileStream.Length];
					fileStream.Read(array2, 0, (int)fileStream.Length);
				}
				if (type == typeof(byte[]))
				{
					return array2;
				}
				if (type == typeof(Bitmap) && Path.GetExtension(array[0]) == ".ico")
				{
					MemoryStream memoryStream = new MemoryStream(array2);
					return new Icon(memoryStream).ToBitmap();
				}
				if (type == typeof(MemoryStream))
				{
					return new MemoryStream(array2);
				}
				return Activator.CreateInstance(type, 532, null, new object[]
				{
					new MemoryStream(array2)
				}, culture);
			}

			/// <summary>Provides a type converter to convert data for an resource reference to and from a string.</summary>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
			/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
			// Token: 0x06000026 RID: 38 RVA: 0x000025E8 File Offset: 0x000007E8
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
