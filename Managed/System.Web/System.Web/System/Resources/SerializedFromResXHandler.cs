using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace System.Resources
{
	// Token: 0x0200002B RID: 43
	internal class SerializedFromResXHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005C84 File Offset: 0x00003E84
		public SerializedFromResXHandler(string data, string _mime_type)
		{
			this.dataString = data;
			this.mime_type = _mime_type;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005C9A File Offset: 0x00003E9A
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return this.DeserializeObject(typeResolver);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005CA3 File Offset: 0x00003EA3
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return this.DeserializeObject(new AssemblyNamesTypeResolutionService(assemblyNames));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005CB1 File Offset: 0x00003EB1
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return this.InternalGetValueType(typeResolver);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005CBA File Offset: 0x00003EBA
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return this.InternalGetValueType(null);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005CC3 File Offset: 0x00003EC3
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005CCC File Offset: 0x00003ECC
		private string InternalGetValueType(ITypeResolutionService typeResolver)
		{
			object obj;
			try
			{
				obj = this.DeserializeObject(typeResolver);
			}
			catch
			{
				return typeof(object).AssemblyQualifiedName;
			}
			if (obj == null)
			{
				return null;
			}
			return obj.GetType().AssemblyQualifiedName;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005D18 File Offset: 0x00003F18
		private object DeserializeObject(ITypeResolutionService typeResolver)
		{
			object obj;
			try
			{
				if (this.mime_type == ResXResourceWriter.SoapSerializedObjectMimeType)
				{
					SoapFormatter soapFormatter = new SoapFormatter();
					if (this.binder == null)
					{
						this.binder = new SerializedFromResXHandler.CustomBinder(typeResolver);
					}
					soapFormatter.Binder = this.binder;
					using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(this.dataString)))
					{
						return soapFormatter.Deserialize(memoryStream);
					}
				}
				if (this.mime_type == ResXResourceWriter.BinSerializedObjectMimeType)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					if (this.binder == null)
					{
						this.binder = new SerializedFromResXHandler.CustomBinder(typeResolver);
					}
					binaryFormatter.Binder = this.binder;
					using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(this.dataString)))
					{
						return binaryFormatter.Deserialize(memoryStream2);
					}
				}
				obj = null;
			}
			catch (SerializationException ex)
			{
				if (ex.Message.StartsWith("Couldn't find assembly"))
				{
					throw new ArgumentException(ex.Message);
				}
				throw ex;
			}
			return obj;
		}

		// Token: 0x04000D8F RID: 3471
		private string dataString;

		// Token: 0x04000D90 RID: 3472
		private string mime_type;

		// Token: 0x04000D91 RID: 3473
		private SerializedFromResXHandler.CustomBinder binder;

		// Token: 0x0200002C RID: 44
		private sealed class CustomBinder : SerializationBinder
		{
			// Token: 0x0600010C RID: 268 RVA: 0x00005E38 File Offset: 0x00004038
			public CustomBinder(ITypeResolutionService _typeResolver)
			{
				this.typeResolver = _typeResolver;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00005E48 File Offset: 0x00004048
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type = null;
				string text = string.Format("{0}, {1}", typeName, assemblyName);
				if (this.typeResolver != null)
				{
					type = this.typeResolver.GetType(text);
				}
				if (type == null)
				{
					type = Type.GetType(text);
				}
				return type;
			}

			// Token: 0x04000D92 RID: 3474
			private ITypeResolutionService typeResolver;
		}
	}
}
