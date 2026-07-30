using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200002D RID: 45
	internal class TypeConverterFromResXHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00005E8A File Offset: 0x0000408A
		public TypeConverterFromResXHandler(string data, string _mime_type, string _typeString)
		{
			this.dataString = data;
			this.mime_type = _mime_type;
			this.typeString = _typeString;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005EA8 File Offset: 0x000040A8
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			if (!string.IsNullOrEmpty(this.mime_type) && this.mime_type != ResXResourceWriter.ByteArraySerializedObjectMimeType)
			{
				return null;
			}
			Type type = base.ResolveType(this.typeString, typeResolver);
			if (type == null)
			{
				throw new TypeLoadException();
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter == null)
			{
				throw new TypeLoadException();
			}
			return this.ConvertData(converter);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005F08 File Offset: 0x00004108
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			if (!string.IsNullOrEmpty(this.mime_type) && this.mime_type != ResXResourceWriter.ByteArraySerializedObjectMimeType)
			{
				return null;
			}
			Type type = base.ResolveType(this.typeString, assemblyNames);
			if (type == null)
			{
				throw new TypeLoadException();
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter == null)
			{
				throw new TypeLoadException();
			}
			return this.ConvertData(converter);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F68 File Offset: 0x00004168
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			Type type = base.ResolveType(this.typeString, typeResolver);
			if (type == null)
			{
				return this.typeString;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005F9C File Offset: 0x0000419C
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			Type type = base.ResolveType(this.typeString, assemblyNames);
			if (type == null)
			{
				return this.typeString;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005FCD File Offset: 0x000041CD
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005FD8 File Offset: 0x000041D8
		private object ConvertData(TypeConverter c)
		{
			if (this.mime_type == ResXResourceWriter.ByteArraySerializedObjectMimeType)
			{
				if (c.CanConvertFrom(typeof(byte[])))
				{
					return c.ConvertFrom(Convert.FromBase64String(this.dataString));
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(this.mime_type))
				{
					throw new Exception("shouldnt get here, invalid mime type");
				}
				if (c.CanConvertFrom(typeof(string)))
				{
					return c.ConvertFromInvariantString(this.dataString);
				}
			}
			throw new TypeLoadException("No converter for this type found");
		}

		// Token: 0x04000D93 RID: 3475
		private string dataString;

		// Token: 0x04000D94 RID: 3476
		private string mime_type;

		// Token: 0x04000D95 RID: 3477
		private string typeString;
	}
}
