using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Security;
using System.Text;

namespace System.Resources
{
	/// <summary>Writes resources in the system-default format to an output file or an output stream. This class cannot be inherited.</summary>
	// Token: 0x020002AE RID: 686
	[ComVisible(true)]
	public sealed class ResourceWriter : IResourceWriter, IDisposable
	{
		/// <summary>Gets or sets a delegate that enables resource assemblies to be written that target versions of the .NET Framework prior to the .NET Framework 4 by using qualified assembly names.</summary>
		/// <returns>The type that is encapsulated by the delegate.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x0007B956 File Offset: 0x00079B56
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0007B95E File Offset: 0x00079B5E
		public Func<Type, string> TypeNameConverter
		{
			get
			{
				return this.typeConverter;
			}
			set
			{
				this.typeConverter = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the specified file.</summary>
		/// <param name="fileName">The output file name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is null. </exception>
		// Token: 0x06001F8D RID: 8077 RVA: 0x0007B968 File Offset: 0x00079B68
		public ResourceWriter(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this._output = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the provided stream.</summary>
		/// <param name="stream">The output stream. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not writable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is null. </exception>
		// Token: 0x06001F8E RID: 8078 RVA: 0x0007B9C0 File Offset: 0x00079BC0
		public ResourceWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this._output = stream;
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Adds a string resource to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">The value of the resource. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this ResourceWriter. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable. </exception>
		// Token: 0x06001F8F RID: 8079 RVA: 0x0007BA28 File Offset: 0x00079C28
		public void AddResource(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a named resource specified as an object to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">The value of the resource. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable. </exception>
		// Token: 0x06001F90 RID: 8080 RVA: 0x0007BA78 File Offset: 0x00079C78
		public void AddResource(string name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			if (value != null && value is Stream)
			{
				this.AddResourceInternal(name, (Stream)value, false);
				return;
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a named resource specified as a stream to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource to add. </param>
		/// <param name="value">The value of the resource to add. The resource must support the <see cref="P:System.IO.Stream.Length" /> property. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.-or-The stream does not support the <see cref="P:System.IO.Stream.Length" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed.</exception>
		// Token: 0x06001F91 RID: 8081 RVA: 0x0007BADF File Offset: 0x00079CDF
		public void AddResource(string name, Stream value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this.AddResourceInternal(name, value, false);
		}

		/// <summary>Adds a named resource specified as a stream to the list of resources to be written, and specifies whether the stream should be closed after the <see cref="M:System.Resources.ResourceWriter.Generate" /> method is called. </summary>
		/// <param name="name">The name of the resource to add. </param>
		/// <param name="value">The value of the resource to add. The resource must support the <see cref="P:System.IO.Stream.Length" /> property. </param>
		/// <param name="closeAfterWrite">true to close the stream after the <see cref="M:System.Resources.ResourceWriter.Generate" /> method is called; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.-or-The stream does not support the <see cref="P:System.IO.Stream.Length" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed.</exception>
		// Token: 0x06001F92 RID: 8082 RVA: 0x0007BB10 File Offset: 0x00079D10
		public void AddResource(string name, Stream value, bool closeAfterWrite)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this.AddResourceInternal(name, value, closeAfterWrite);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0007BB44 File Offset: 0x00079D44
		private void AddResourceInternal(string name, Stream value, bool closeAfterWrite)
		{
			if (value == null)
			{
				this._caseInsensitiveDups.Add(name, null);
				this._resourceList.Add(name, value);
				return;
			}
			if (!value.CanSeek)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream does not support seeking."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, new ResourceWriter.StreamWrapper(value, closeAfterWrite));
		}

		/// <summary>Adds a named resource specified as a byte array to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">Value of the resource as an 8-bit unsigned integer array. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable. </exception>
		// Token: 0x06001F94 RID: 8084 RVA: 0x0007BBA8 File Offset: 0x00079DA8
		public void AddResource(string name, byte[] value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a unit of data as a resource to the list of resources to be written. </summary>
		/// <param name="name">A name that identifies the resource that contains the added data.</param>
		/// <param name="typeName">The type name of the added data. For more information, see the Remarks section.</param>
		/// <param name="serializedData">A byte array that contains the binary representation of the added data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="typeName" />, or <paramref name="serializedData" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" /> object. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceWriter" /> object is not initialized. The probable cause is that the <see cref="T:System.Resources.ResourceWriter" /> object is closed.</exception>
		// Token: 0x06001F95 RID: 8085 RVA: 0x0007BBF8 File Offset: 0x00079DF8
		public void AddResourceData(string name, string typeName, byte[] serializedData)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (serializedData == null)
			{
				throw new ArgumentNullException("serializedData");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			if (this._preserializedData == null)
			{
				this._preserializedData = new Dictionary<string, ResourceWriter.PrecannedResource>(FastResourceComparer.Default);
			}
			this._preserializedData.Add(name, new ResourceWriter.PrecannedResource(typeName, serializedData));
		}

		/// <summary>Saves the resources to the output stream and then closes it.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F96 RID: 8086 RVA: 0x0007BC7F File Offset: 0x00079E7F
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0007BC88 File Offset: 0x00079E88
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._resourceList != null)
				{
					this.Generate();
				}
				if (this._output != null)
				{
					this._output.Close();
				}
			}
			this._output = null;
			this._caseInsensitiveDups = null;
		}

		/// <summary>Allows users to close the resource file or stream, explicitly releasing resources.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F98 RID: 8088 RVA: 0x0007BC7F File Offset: 0x00079E7F
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Saves all resources to the output stream in the system default format.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error occurred during serialization of the object. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F99 RID: 8089 RVA: 0x0007BCBC File Offset: 0x00079EBC
		[SecuritySafeCritical]
		public void Generate()
		{
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			BinaryWriter binaryWriter = new BinaryWriter(this._output, Encoding.UTF8);
			List<string> list = new List<string>();
			binaryWriter.Write(ResourceManager.MagicNumber);
			binaryWriter.Write(ResourceManager.HeaderVersionNumber);
			MemoryStream memoryStream = new MemoryStream(240);
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
			binaryWriter2.Write(MultitargetingHelpers.GetAssemblyQualifiedName(typeof(ResourceReader), this.typeConverter));
			binaryWriter2.Write(ResourceManager.ResSetTypeName);
			binaryWriter2.Flush();
			binaryWriter.Write((int)memoryStream.Length);
			binaryWriter.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			binaryWriter.Write(2);
			int num = this._resourceList.Count;
			if (this._preserializedData != null)
			{
				num += this._preserializedData.Count;
			}
			binaryWriter.Write(num);
			int[] array = new int[num];
			int[] array2 = new int[num];
			int num2 = 0;
			MemoryStream memoryStream2 = new MemoryStream(num * 40);
			BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2, Encoding.Unicode);
			Stream stream = null;
			try
			{
				string tempFileName = Path.GetTempFileName();
				File.SetAttributes(tempFileName, FileAttributes.NotContentIndexed | FileAttributes.Temporary);
				stream = new FileStream(tempFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose | FileOptions.SequentialScan);
			}
			catch (UnauthorizedAccessException)
			{
				stream = new MemoryStream();
			}
			catch (IOException)
			{
				stream = new MemoryStream();
			}
			using (stream)
			{
				BinaryWriter binaryWriter4 = new BinaryWriter(stream, Encoding.UTF8);
				IFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
				SortedList sortedList = new SortedList(this._resourceList, FastResourceComparer.Default);
				if (this._preserializedData != null)
				{
					foreach (KeyValuePair<string, ResourceWriter.PrecannedResource> keyValuePair in this._preserializedData)
					{
						sortedList.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IDictionaryEnumerator enumerator2 = sortedList.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					array[num2] = FastResourceComparer.HashFunction((string)enumerator2.Key);
					array2[num2++] = (int)binaryWriter3.Seek(0, SeekOrigin.Current);
					binaryWriter3.Write((string)enumerator2.Key);
					binaryWriter3.Write((int)binaryWriter4.Seek(0, SeekOrigin.Current));
					object value = enumerator2.Value;
					ResourceTypeCode resourceTypeCode = this.FindTypeCode(value, list);
					ResourceWriter.Write7BitEncodedInt(binaryWriter4, (int)resourceTypeCode);
					ResourceWriter.PrecannedResource precannedResource = value as ResourceWriter.PrecannedResource;
					if (precannedResource != null)
					{
						binaryWriter4.Write(precannedResource.Data);
					}
					else
					{
						this.WriteValue(resourceTypeCode, value, binaryWriter4, formatter);
					}
				}
				binaryWriter.Write(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					binaryWriter.Write(list[i]);
				}
				Array.Sort<int, int>(array, array2);
				binaryWriter.Flush();
				int num3 = (int)binaryWriter.BaseStream.Position & 7;
				if (num3 > 0)
				{
					for (int j = 0; j < 8 - num3; j++)
					{
						binaryWriter.Write("PAD"[j % 3]);
					}
				}
				foreach (int num4 in array)
				{
					binaryWriter.Write(num4);
				}
				foreach (int num5 in array2)
				{
					binaryWriter.Write(num5);
				}
				binaryWriter.Flush();
				binaryWriter3.Flush();
				binaryWriter4.Flush();
				int num6 = (int)(binaryWriter.Seek(0, SeekOrigin.Current) + memoryStream2.Length);
				num6 += 4;
				binaryWriter.Write(num6);
				binaryWriter.Write(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length);
				binaryWriter3.Close();
				stream.Position = 0L;
				stream.CopyTo(binaryWriter.BaseStream);
				binaryWriter4.Close();
			}
			binaryWriter.Flush();
			this._resourceList = null;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0007C0EC File Offset: 0x0007A2EC
		private ResourceTypeCode FindTypeCode(object value, List<string> types)
		{
			if (value == null)
			{
				return ResourceTypeCode.Null;
			}
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return ResourceTypeCode.String;
			}
			if (type == typeof(int))
			{
				return ResourceTypeCode.Int32;
			}
			if (type == typeof(bool))
			{
				return ResourceTypeCode.Boolean;
			}
			if (type == typeof(char))
			{
				return ResourceTypeCode.Char;
			}
			if (type == typeof(byte))
			{
				return ResourceTypeCode.Byte;
			}
			if (type == typeof(sbyte))
			{
				return ResourceTypeCode.SByte;
			}
			if (type == typeof(short))
			{
				return ResourceTypeCode.Int16;
			}
			if (type == typeof(long))
			{
				return ResourceTypeCode.Int64;
			}
			if (type == typeof(ushort))
			{
				return ResourceTypeCode.UInt16;
			}
			if (type == typeof(uint))
			{
				return ResourceTypeCode.UInt32;
			}
			if (type == typeof(ulong))
			{
				return ResourceTypeCode.UInt64;
			}
			if (type == typeof(float))
			{
				return ResourceTypeCode.Single;
			}
			if (type == typeof(double))
			{
				return ResourceTypeCode.Double;
			}
			if (type == typeof(decimal))
			{
				return ResourceTypeCode.Decimal;
			}
			if (type == typeof(DateTime))
			{
				return ResourceTypeCode.DateTime;
			}
			if (type == typeof(TimeSpan))
			{
				return ResourceTypeCode.TimeSpan;
			}
			if (type == typeof(byte[]))
			{
				return ResourceTypeCode.ByteArray;
			}
			if (type == typeof(ResourceWriter.StreamWrapper))
			{
				return ResourceTypeCode.Stream;
			}
			string text;
			if (type == typeof(ResourceWriter.PrecannedResource))
			{
				text = ((ResourceWriter.PrecannedResource)value).TypeName;
				if (text.StartsWith("ResourceTypeCode.", StringComparison.Ordinal))
				{
					text = text.Substring(17);
					return (ResourceTypeCode)Enum.Parse(typeof(ResourceTypeCode), text);
				}
			}
			else
			{
				text = MultitargetingHelpers.GetAssemblyQualifiedName(type, this.typeConverter);
			}
			int num = types.IndexOf(text);
			if (num == -1)
			{
				num = types.Count;
				types.Add(text);
			}
			return num + ResourceTypeCode.StartOfUserTypes;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0007C2F0 File Offset: 0x0007A4F0
		private void WriteValue(ResourceTypeCode typeCode, object value, BinaryWriter writer, IFormatter objFormatter)
		{
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return;
			case ResourceTypeCode.String:
				writer.Write((string)value);
				return;
			case ResourceTypeCode.Boolean:
				writer.Write((bool)value);
				return;
			case ResourceTypeCode.Char:
				writer.Write((ushort)((char)value));
				return;
			case ResourceTypeCode.Byte:
				writer.Write((byte)value);
				return;
			case ResourceTypeCode.SByte:
				writer.Write((sbyte)value);
				return;
			case ResourceTypeCode.Int16:
				writer.Write((short)value);
				return;
			case ResourceTypeCode.UInt16:
				writer.Write((ushort)value);
				return;
			case ResourceTypeCode.Int32:
				writer.Write((int)value);
				return;
			case ResourceTypeCode.UInt32:
				writer.Write((uint)value);
				return;
			case ResourceTypeCode.Int64:
				writer.Write((long)value);
				return;
			case ResourceTypeCode.UInt64:
				writer.Write((ulong)value);
				return;
			case ResourceTypeCode.Single:
				writer.Write((float)value);
				return;
			case ResourceTypeCode.Double:
				writer.Write((double)value);
				return;
			case ResourceTypeCode.Decimal:
				writer.Write((decimal)value);
				return;
			case ResourceTypeCode.DateTime:
			{
				long num = ((DateTime)value).ToBinary();
				writer.Write(num);
				return;
			}
			case ResourceTypeCode.TimeSpan:
				writer.Write(((TimeSpan)value).Ticks);
				return;
			case ResourceTypeCode.ByteArray:
			{
				byte[] array = (byte[])value;
				writer.Write(array.Length);
				writer.Write(array, 0, array.Length);
				return;
			}
			case ResourceTypeCode.Stream:
			{
				ResourceWriter.StreamWrapper streamWrapper = (ResourceWriter.StreamWrapper)value;
				if (streamWrapper.m_stream.GetType() == typeof(MemoryStream))
				{
					MemoryStream memoryStream = (MemoryStream)streamWrapper.m_stream;
					if (memoryStream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					int num2;
					int num3;
					memoryStream.InternalGetOriginAndLength(out num2, out num3);
					byte[] array2 = memoryStream.InternalGetBuffer();
					writer.Write(num3);
					writer.Write(array2, num2, num3);
					return;
				}
				else
				{
					Stream stream = streamWrapper.m_stream;
					if (stream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					stream.Position = 0L;
					writer.Write((int)stream.Length);
					byte[] array3 = new byte[4096];
					int num4;
					while ((num4 = stream.Read(array3, 0, array3.Length)) != 0)
					{
						writer.Write(array3, 0, num4);
					}
					if (streamWrapper.m_closeAfterWrite)
					{
						stream.Close();
						return;
					}
					return;
				}
				break;
			}
			}
			objFormatter.Serialize(writer.BaseStream, value);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0007C590 File Offset: 0x0007A790
		private static void Write7BitEncodedInt(BinaryWriter store, int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				store.Write((byte)(num | 128U));
			}
			store.Write((byte)num);
		}

		// Token: 0x04001114 RID: 4372
		private Func<Type, string> typeConverter;

		// Token: 0x04001115 RID: 4373
		private const int _ExpectedNumberOfResources = 1000;

		// Token: 0x04001116 RID: 4374
		private const int AverageNameSize = 40;

		// Token: 0x04001117 RID: 4375
		private const int AverageValueSize = 40;

		// Token: 0x04001118 RID: 4376
		private Dictionary<string, object> _resourceList;

		// Token: 0x04001119 RID: 4377
		internal Stream _output;

		// Token: 0x0400111A RID: 4378
		private Dictionary<string, object> _caseInsensitiveDups;

		// Token: 0x0400111B RID: 4379
		private Dictionary<string, ResourceWriter.PrecannedResource> _preserializedData;

		// Token: 0x0400111C RID: 4380
		private const int _DefaultBufferSize = 4096;

		// Token: 0x020002AF RID: 687
		private class PrecannedResource
		{
			// Token: 0x06001F9D RID: 8093 RVA: 0x0007C5C3 File Offset: 0x0007A7C3
			internal PrecannedResource(string typeName, byte[] data)
			{
				this.TypeName = typeName;
				this.Data = data;
			}

			// Token: 0x0400111D RID: 4381
			internal string TypeName;

			// Token: 0x0400111E RID: 4382
			internal byte[] Data;
		}

		// Token: 0x020002B0 RID: 688
		private class StreamWrapper
		{
			// Token: 0x06001F9E RID: 8094 RVA: 0x0007C5D9 File Offset: 0x0007A7D9
			internal StreamWrapper(Stream s, bool closeAfterWrite)
			{
				this.m_stream = s;
				this.m_closeAfterWrite = closeAfterWrite;
			}

			// Token: 0x0400111F RID: 4383
			internal Stream m_stream;

			// Token: 0x04001120 RID: 4384
			internal bool m_closeAfterWrite;
		}
	}
}
