using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Serializes and deserializes object graphs that represent the state of an object. This class cannot be inherited.</summary>
	// Token: 0x020001EE RID: 494
	public sealed class ObjectStateFormatter : IFormatter, IStateFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectStateFormatter" /> class. </summary>
		// Token: 0x060013DE RID: 5086 RVA: 0x00002050 File Offset: 0x00000250
		public ObjectStateFormatter()
		{
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00035C38 File Offset: 0x00033E38
		internal ObjectStateFormatter(Page page)
		{
			this.page = page;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00035C47 File Offset: 0x00033E47
		private bool EnableMac
		{
			get
			{
				if (this.page != null)
				{
					return this.page.EnableViewStateMac;
				}
				return this.section != null;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00035C66 File Offset: 0x00033E66
		private bool NeedViewStateEncryption
		{
			get
			{
				return this.page != null && this.page.NeedViewStateEncryption;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00035C7D File Offset: 0x00033E7D
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00035CA2 File Offset: 0x00033EA2
		internal MachineKeySection Section
		{
			get
			{
				if (this.section == null)
				{
					this.section = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection("system.web/machineKey");
				}
				return this.section;
			}
			set
			{
				this.section = value;
			}
		}

		/// <summary>Deserializes an object state graph from its binary-serialized form that is contained in the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>An object that represents a deserialized object state graph.</returns>
		/// <param name="inputStream">A <see cref="T:System.IO.Stream" /> that the <see cref="T:System.Web.UI.ObjectStateFormatter" /> deserializes into an initialized object. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="inputStream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An exception occurs during deserialization of the <see cref="T:System.IO.Stream" />. The exception message is appended to the message of the <see cref="T:System.ArgumentException" />.</exception>
		// Token: 0x060013E4 RID: 5092 RVA: 0x00035CAC File Offset: 0x00033EAC
		public object Deserialize(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			BinaryReader binaryReader = new BinaryReader(inputStream);
			if (binaryReader.ReadInt16() != 511)
			{
				throw new ArgumentException("The serialized data is invalid");
			}
			return this.DeserializeObject(binaryReader);
		}

		/// <summary>Deserializes an object state graph from its serialized base64-encoded string form.</summary>
		/// <returns>An object that represents a deserialized object state graph.</returns>
		/// <param name="inputString">A string that the <see cref="T:System.Web.UI.ObjectStateFormatter" /> deserializes into an initialized object.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="inputString" /> is null or has a <see cref="P:System.String.Length" /> of 0.</exception>
		/// <exception cref="T:System.ArgumentException">The serialized data is invalid.</exception>
		/// <exception cref="T:System.Web.HttpException">The machine authentication code (MAC) validation check that is performed when deserializing view state fails.</exception>
		// Token: 0x060013E5 RID: 5093 RVA: 0x00035CF0 File Offset: 0x00033EF0
		public object Deserialize(string inputString)
		{
			if (inputString == null)
			{
				throw new ArgumentNullException("inputString");
			}
			if (inputString.Length == 0)
			{
				throw new ArgumentNullException("inputString");
			}
			byte[] array = Convert.FromBase64String(inputString);
			if (array == null || array.Length == 0)
			{
				throw new ArgumentNullException("inputString");
			}
			if (this.NeedViewStateEncryption)
			{
				if (this.EnableMac)
				{
					array = MachineKeySectionUtils.VerifyDecrypt(this.Section, array);
				}
				else
				{
					array = MachineKeySectionUtils.Decrypt(this.Section, array);
				}
			}
			else if (this.EnableMac)
			{
				array = MachineKeySectionUtils.Verify(this.Section, array);
			}
			if (array == null)
			{
				throw new HttpException("Unable to validate data.");
			}
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				obj = this.Deserialize(memoryStream);
			}
			return obj;
		}

		/// <summary>Serializes an object state graph to a base64-encoded string.</summary>
		/// <returns>A base-64 encoded string that represents the serialized object state of the <paramref name="stateGraph" /> parameter.</returns>
		/// <param name="stateGraph">The object to serialize.</param>
		// Token: 0x060013E6 RID: 5094 RVA: 0x00035DB4 File Offset: 0x00033FB4
		public string Serialize(object stateGraph)
		{
			if (stateGraph == null)
			{
				return string.Empty;
			}
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.Serialize(memoryStream, stateGraph);
				array = memoryStream.GetBuffer();
			}
			if (this.NeedViewStateEncryption)
			{
				if (this.EnableMac)
				{
					array = MachineKeySectionUtils.EncryptSign(this.Section, array);
				}
				else
				{
					array = MachineKeySectionUtils.Encrypt(this.Section, array);
				}
			}
			else if (this.EnableMac)
			{
				array = MachineKeySectionUtils.Sign(this.Section, array);
			}
			return Convert.ToBase64String(array, 0, array.Length);
		}

		/// <summary>Serializes an object state graph to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="outputStream">A <see cref="T:System.IO.Stream" /> to which the <see cref="T:System.Web.UI.ObjectStateFormatter" /> serializes the state of the specified object.</param>
		/// <param name="stateGraph">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="inputStream" /> is null.</exception>
		// Token: 0x060013E7 RID: 5095 RVA: 0x00035E4C File Offset: 0x0003404C
		public void Serialize(Stream outputStream, object stateGraph)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (stateGraph == null)
			{
				throw new ArgumentNullException("stateGraph");
			}
			BinaryWriter binaryWriter = new BinaryWriter(outputStream);
			binaryWriter.Write(511);
			this.SerializeValue(binaryWriter, stateGraph);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00035E8F File Offset: 0x0003408F
		private void SerializeValue(BinaryWriter w, object o)
		{
			ObjectStateFormatter.ObjectFormatter.WriteObject(w, o, new ObjectStateFormatter.WriterContext());
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00035E9D File Offset: 0x0003409D
		private object DeserializeObject(BinaryReader r)
		{
			return ObjectStateFormatter.ObjectFormatter.ReadObject(r, new ObjectStateFormatter.ReaderContext());
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.ObjectStateFormatter.Deserialize(System.IO.Stream)" />.</summary>
		/// <returns>The top object of the deserialized graph.</returns>
		/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
		// Token: 0x060013EA RID: 5098 RVA: 0x00035EAA File Offset: 0x000340AA
		object IFormatter.Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.ObjectStateFormatter.Serialize(System.IO.Stream,System.Object)" />.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on). </param>
		/// <param name="stateGraph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized. </param>
		// Token: 0x060013EB RID: 5099 RVA: 0x00035EB3 File Offset: 0x000340B3
		void IFormatter.Serialize(Stream serializationStream, object stateGraph)
		{
			this.Serialize(serializationStream, stateGraph);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.Serialization.Formatter.Binder" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x0000393A File Offset: 0x00001B3A
		SerializationBinder IFormatter.Binder
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.Serialization.IFormatter.Context" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00035EBD File Offset: 0x000340BD
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x0000393A File Offset: 0x00001B3A
		StreamingContext IFormatter.Context
		{
			get
			{
				return new StreamingContext(StreamingContextStates.All);
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="T:System.Runtime.Serialization.SurrogateSelector" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by this formatter.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0000393A File Offset: 0x00001B3A
		ISurrogateSelector IFormatter.SurrogateSelector
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x04001482 RID: 5250
		private const ushort SERIALIZED_STREAM_MAGIC = 511;

		// Token: 0x04001483 RID: 5251
		private Page page;

		// Token: 0x04001484 RID: 5252
		private MachineKeySection section;

		// Token: 0x020001EF RID: 495
		private sealed class WriterContext
		{
			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00035EC9 File Offset: 0x000340C9
			public short Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x060013F3 RID: 5107 RVA: 0x00035ED4 File Offset: 0x000340D4
			public bool RegisterCache(object o)
			{
				if (this.nextKey == 32767)
				{
					return false;
				}
				if (this.cache == null)
				{
					this.cache = new Hashtable();
					Hashtable hashtable = this.cache;
					short num = this.nextKey;
					this.nextKey = num + 1;
					hashtable.Add(o, this.key = num);
					return false;
				}
				object obj = this.cache[o];
				if (obj == null)
				{
					Hashtable hashtable2 = this.cache;
					short num = this.nextKey;
					this.nextKey = num + 1;
					hashtable2.Add(o, this.key = num);
					return false;
				}
				this.key = (short)obj;
				return true;
			}

			// Token: 0x04001485 RID: 5253
			private Hashtable cache;

			// Token: 0x04001486 RID: 5254
			private short nextKey;

			// Token: 0x04001487 RID: 5255
			private short key;
		}

		// Token: 0x020001F0 RID: 496
		private sealed class ReaderContext
		{
			// Token: 0x060013F5 RID: 5109 RVA: 0x00035F7C File Offset: 0x0003417C
			public void CacheItem(object o)
			{
				if (this.cache == null)
				{
					this.cache = new ArrayList();
				}
				this.cache.Add(o);
			}

			// Token: 0x060013F6 RID: 5110 RVA: 0x00035F9E File Offset: 0x0003419E
			public object GetCache(short key)
			{
				return this.cache[(int)key];
			}

			// Token: 0x04001488 RID: 5256
			private ArrayList cache;
		}

		// Token: 0x020001F1 RID: 497
		private abstract class ObjectFormatter
		{
			// Token: 0x060013F8 RID: 5112 RVA: 0x00035FAC File Offset: 0x000341AC
			static ObjectFormatter()
			{
				new ObjectStateFormatter.StringFormatter().Register();
				new ObjectStateFormatter.Int64Formatter().Register();
				new ObjectStateFormatter.Int32Formatter().Register();
				new ObjectStateFormatter.Int16Formatter().Register();
				new ObjectStateFormatter.ByteFormatter().Register();
				new ObjectStateFormatter.BooleanFormatter().Register();
				new ObjectStateFormatter.CharFormatter().Register();
				new ObjectStateFormatter.DateTimeFormatter().Register();
				new ObjectStateFormatter.PairFormatter().Register();
				new ObjectStateFormatter.TripletFormatter().Register();
				new ObjectStateFormatter.ArrayListFormatter().Register();
				new ObjectStateFormatter.HashtableFormatter().Register();
				new ObjectStateFormatter.ObjectArrayFormatter().Register();
				new ObjectStateFormatter.UnitFormatter().Register();
				new ObjectStateFormatter.FontUnitFormatter().Register();
				new ObjectStateFormatter.IndexedStringFormatter().Register();
				new ObjectStateFormatter.ColorFormatter().Register();
				ObjectStateFormatter.ObjectFormatter.enumFormatter = new ObjectStateFormatter.EnumFormatter();
				ObjectStateFormatter.ObjectFormatter.enumFormatter.Register();
				ObjectStateFormatter.ObjectFormatter.typeFormatter = new ObjectStateFormatter.TypeFormatter();
				ObjectStateFormatter.ObjectFormatter.typeFormatter.Register();
				ObjectStateFormatter.ObjectFormatter.singleRankArrayFormatter = new ObjectStateFormatter.SingleRankArrayFormatter();
				ObjectStateFormatter.ObjectFormatter.singleRankArrayFormatter.Register();
				ObjectStateFormatter.ObjectFormatter.typeConverterFormatter = new ObjectStateFormatter.TypeConverterFormatter();
				ObjectStateFormatter.ObjectFormatter.typeConverterFormatter.Register();
				ObjectStateFormatter.ObjectFormatter.binaryObjectFormatter = new ObjectStateFormatter.BinaryObjectFormatter();
				ObjectStateFormatter.ObjectFormatter.binaryObjectFormatter.Register();
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x000360E8 File Offset: 0x000342E8
			public ObjectFormatter()
			{
				byte b = ObjectStateFormatter.ObjectFormatter.nextId;
				ObjectStateFormatter.ObjectFormatter.nextId = b + 1;
				this.PrimaryId = b;
				if (this.NumberOfIds == 1)
				{
					return;
				}
				byte b2 = ObjectStateFormatter.ObjectFormatter.nextId;
				ObjectStateFormatter.ObjectFormatter.nextId = b2 + 1;
				this.SecondaryId = b2;
				if (this.NumberOfIds == 2)
				{
					return;
				}
				byte b3 = ObjectStateFormatter.ObjectFormatter.nextId;
				ObjectStateFormatter.ObjectFormatter.nextId = b3 + 1;
				this.TertiaryId = b3;
				if (this.NumberOfIds == 3)
				{
					return;
				}
				throw new Exception();
			}

			// Token: 0x060013FA RID: 5114
			protected abstract void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx);

			// Token: 0x060013FB RID: 5115
			protected abstract object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx);

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x060013FC RID: 5116
			protected abstract Type Type { get; }

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x060013FD RID: 5117 RVA: 0x00008B66 File Offset: 0x00006D66
			protected virtual int NumberOfIds
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x060013FE RID: 5118 RVA: 0x00036170 File Offset: 0x00034370
			public virtual void Register()
			{
				ObjectStateFormatter.ObjectFormatter.writeMap[this.Type] = this;
				ObjectStateFormatter.ObjectFormatter.readMap[(int)this.PrimaryId] = this;
				if (this.SecondaryId != 255)
				{
					ObjectStateFormatter.ObjectFormatter.readMap[(int)this.SecondaryId] = this;
					if (this.TertiaryId != 255)
					{
						ObjectStateFormatter.ObjectFormatter.readMap[(int)this.TertiaryId] = this;
					}
				}
			}

			// Token: 0x060013FF RID: 5119 RVA: 0x000361D0 File Offset: 0x000343D0
			public static void WriteObject(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				if (o == null)
				{
					w.Write(0);
					return;
				}
				Type type = o.GetType();
				ObjectStateFormatter.ObjectFormatter objectFormatter = ObjectStateFormatter.ObjectFormatter.writeMap[type] as ObjectStateFormatter.ObjectFormatter;
				if (objectFormatter == null)
				{
					if (o is Type)
					{
						objectFormatter = ObjectStateFormatter.ObjectFormatter.typeFormatter;
					}
					else if (type.IsEnum)
					{
						objectFormatter = ObjectStateFormatter.ObjectFormatter.enumFormatter;
					}
					else if (type.IsArray && ((Array)o).Rank == 1)
					{
						objectFormatter = ObjectStateFormatter.ObjectFormatter.singleRankArrayFormatter;
					}
					else
					{
						TypeConverter converter = TypeDescriptor.GetConverter(o);
						if (converter == null || converter.GetType() == typeof(TypeConverter) || !converter.CanConvertTo(typeof(string)) || !converter.CanConvertFrom(typeof(string)))
						{
							objectFormatter = ObjectStateFormatter.ObjectFormatter.binaryObjectFormatter;
						}
						else
						{
							ObjectStateFormatter.ObjectFormatter.typeConverterFormatter.Converter = converter;
							objectFormatter = ObjectStateFormatter.ObjectFormatter.typeConverterFormatter;
						}
					}
				}
				objectFormatter.Write(w, o, ctx);
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x000362B0 File Offset: 0x000344B0
			public static object ReadObject(BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				byte b = r.ReadByte();
				if (b == 0)
				{
					return null;
				}
				return ObjectStateFormatter.ObjectFormatter.readMap[(int)b].Read(b, r, ctx);
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x000362D8 File Offset: 0x000344D8
			protected void Write7BitEncodedInt(BinaryWriter w, int value)
			{
				do
				{
					int num = (value >> 7) & 33554431;
					byte b = (byte)(value & 127);
					if (num != 0)
					{
						b |= 128;
					}
					w.Write(b);
					value = num;
				}
				while (value != 0);
			}

			// Token: 0x06001402 RID: 5122 RVA: 0x0003630C File Offset: 0x0003450C
			protected int Read7BitEncodedInt(BinaryReader r)
			{
				int num = 0;
				int num2 = 0;
				byte b;
				do
				{
					b = r.ReadByte();
					num |= (int)(b & 127) << num2;
					num2 += 7;
				}
				while ((b & 128) == 128);
				return num;
			}

			// Token: 0x04001489 RID: 5257
			private static readonly Hashtable writeMap = new Hashtable();

			// Token: 0x0400148A RID: 5258
			private static ObjectStateFormatter.ObjectFormatter[] readMap = new ObjectStateFormatter.ObjectFormatter[256];

			// Token: 0x0400148B RID: 5259
			private static ObjectStateFormatter.BinaryObjectFormatter binaryObjectFormatter;

			// Token: 0x0400148C RID: 5260
			private static ObjectStateFormatter.TypeFormatter typeFormatter;

			// Token: 0x0400148D RID: 5261
			private static ObjectStateFormatter.EnumFormatter enumFormatter;

			// Token: 0x0400148E RID: 5262
			private static ObjectStateFormatter.SingleRankArrayFormatter singleRankArrayFormatter;

			// Token: 0x0400148F RID: 5263
			private static ObjectStateFormatter.TypeConverterFormatter typeConverterFormatter;

			// Token: 0x04001490 RID: 5264
			private static byte nextId = 1;

			// Token: 0x04001491 RID: 5265
			protected readonly byte PrimaryId;

			// Token: 0x04001492 RID: 5266
			protected readonly byte SecondaryId = byte.MaxValue;

			// Token: 0x04001493 RID: 5267
			protected readonly byte TertiaryId = byte.MaxValue;
		}

		// Token: 0x020001F2 RID: 498
		private class StringFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001403 RID: 5123 RVA: 0x00036343 File Offset: 0x00034543
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				if (ctx.RegisterCache(o))
				{
					w.Write(this.SecondaryId);
					w.Write(ctx.Key);
					return;
				}
				w.Write(this.PrimaryId);
				w.Write((string)o);
			}

			// Token: 0x06001404 RID: 5124 RVA: 0x00036380 File Offset: 0x00034580
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				if (token == this.PrimaryId)
				{
					string text = r.ReadString();
					ctx.CacheItem(text);
					return text;
				}
				return ctx.GetCache(r.ReadInt16());
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001405 RID: 5125 RVA: 0x000363B2 File Offset: 0x000345B2
			protected override Type Type
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06001406 RID: 5126 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x020001F3 RID: 499
		private class IndexedStringFormatter : ObjectStateFormatter.StringFormatter
		{
			// Token: 0x06001408 RID: 5128 RVA: 0x000363CC File Offset: 0x000345CC
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				IndexedString indexedString = o as IndexedString;
				if (indexedString == null)
				{
					throw new InvalidOperationException("object is not of the IndexedString type");
				}
				base.Write(w, indexedString.Value, ctx);
			}

			// Token: 0x06001409 RID: 5129 RVA: 0x000363FC File Offset: 0x000345FC
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				string text = base.Read(token, r, ctx) as string;
				if (string.IsNullOrEmpty(text))
				{
					throw new InvalidOperationException("string must not be null or empty.");
				}
				return new IndexedString(text);
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x00036424 File Offset: 0x00034624
			protected override Type Type
			{
				get
				{
					return typeof(IndexedString);
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x0600140B RID: 5131 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x020001F4 RID: 500
		private class Int64Formatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600140D RID: 5133 RVA: 0x00036438 File Offset: 0x00034638
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				w.Write((long)o);
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x00036452 File Offset: 0x00034652
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return r.ReadInt64();
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x0600140F RID: 5135 RVA: 0x0003645F File Offset: 0x0003465F
			protected override Type Type
			{
				get
				{
					return typeof(long);
				}
			}
		}

		// Token: 0x020001F5 RID: 501
		private class Int32Formatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001411 RID: 5137 RVA: 0x0003646C File Offset: 0x0003466C
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				int num = (int)o;
				if ((int)((byte)num) == num)
				{
					w.Write(this.SecondaryId);
					w.Write((byte)num);
					return;
				}
				w.Write(this.PrimaryId);
				w.Write(num);
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x000364AD File Offset: 0x000346AD
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				if (token == this.PrimaryId)
				{
					return r.ReadInt32();
				}
				return (int)r.ReadByte();
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001413 RID: 5139 RVA: 0x000364CF File Offset: 0x000346CF
			protected override Type Type
			{
				get
				{
					return typeof(int);
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001414 RID: 5140 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x020001F6 RID: 502
		private class Int16Formatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001416 RID: 5142 RVA: 0x000364DB File Offset: 0x000346DB
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				w.Write((short)o);
			}

			// Token: 0x06001417 RID: 5143 RVA: 0x000364F5 File Offset: 0x000346F5
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return r.ReadInt16();
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001418 RID: 5144 RVA: 0x00036502 File Offset: 0x00034702
			protected override Type Type
			{
				get
				{
					return typeof(short);
				}
			}
		}

		// Token: 0x020001F7 RID: 503
		private class ByteFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600141A RID: 5146 RVA: 0x0003650E File Offset: 0x0003470E
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				w.Write((byte)o);
			}

			// Token: 0x0600141B RID: 5147 RVA: 0x00036528 File Offset: 0x00034728
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return r.ReadByte();
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x00036535 File Offset: 0x00034735
			protected override Type Type
			{
				get
				{
					return typeof(byte);
				}
			}
		}

		// Token: 0x020001F8 RID: 504
		private class BooleanFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600141E RID: 5150 RVA: 0x00036541 File Offset: 0x00034741
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				if ((bool)o)
				{
					w.Write(this.PrimaryId);
					return;
				}
				w.Write(this.SecondaryId);
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x00036564 File Offset: 0x00034764
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return token == this.PrimaryId;
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x00036574 File Offset: 0x00034774
			protected override Type Type
			{
				get
				{
					return typeof(bool);
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001421 RID: 5153 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x020001F9 RID: 505
		private class CharFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001423 RID: 5155 RVA: 0x00036580 File Offset: 0x00034780
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				w.Write((char)o);
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x0003659A File Offset: 0x0003479A
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return r.ReadChar();
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001425 RID: 5157 RVA: 0x000365A7 File Offset: 0x000347A7
			protected override Type Type
			{
				get
				{
					return typeof(char);
				}
			}
		}

		// Token: 0x020001FA RID: 506
		private class DateTimeFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001427 RID: 5159 RVA: 0x000365B4 File Offset: 0x000347B4
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				w.Write(((DateTime)o).Ticks);
			}

			// Token: 0x06001428 RID: 5160 RVA: 0x000365E1 File Offset: 0x000347E1
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return new DateTime(r.ReadInt64());
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001429 RID: 5161 RVA: 0x000365F3 File Offset: 0x000347F3
			protected override Type Type
			{
				get
				{
					return typeof(DateTime);
				}
			}
		}

		// Token: 0x020001FB RID: 507
		private class PairFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600142B RID: 5163 RVA: 0x00036600 File Offset: 0x00034800
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				Pair pair = (Pair)o;
				w.Write(this.PrimaryId);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, pair.First, ctx);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, pair.Second, ctx);
			}

			// Token: 0x0600142C RID: 5164 RVA: 0x0003663A File Offset: 0x0003483A
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return new Pair
				{
					First = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx),
					Second = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx)
				};
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003665B File Offset: 0x0003485B
			protected override Type Type
			{
				get
				{
					return typeof(Pair);
				}
			}
		}

		// Token: 0x020001FC RID: 508
		private class TripletFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600142F RID: 5167 RVA: 0x00036668 File Offset: 0x00034868
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				Triplet triplet = (Triplet)o;
				w.Write(this.PrimaryId);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, triplet.First, ctx);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, triplet.Second, ctx);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, triplet.Third, ctx);
			}

			// Token: 0x06001430 RID: 5168 RVA: 0x000366AF File Offset: 0x000348AF
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return new Triplet
				{
					First = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx),
					Second = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx),
					Third = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx)
				};
			}

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06001431 RID: 5169 RVA: 0x000366DD File Offset: 0x000348DD
			protected override Type Type
			{
				get
				{
					return typeof(Triplet);
				}
			}
		}

		// Token: 0x020001FD RID: 509
		private class ArrayListFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001433 RID: 5171 RVA: 0x000366EC File Offset: 0x000348EC
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				ArrayList arrayList = (ArrayList)o;
				w.Write(this.PrimaryId);
				base.Write7BitEncodedInt(w, arrayList.Count);
				for (int i = 0; i < arrayList.Count; i++)
				{
					ObjectStateFormatter.ObjectFormatter.WriteObject(w, arrayList[i], ctx);
				}
			}

			// Token: 0x06001434 RID: 5172 RVA: 0x00036738 File Offset: 0x00034938
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				int num = base.Read7BitEncodedInt(r);
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < num; i++)
				{
					arrayList.Add(ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx));
				}
				return arrayList;
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06001435 RID: 5173 RVA: 0x0003676F File Offset: 0x0003496F
			protected override Type Type
			{
				get
				{
					return typeof(ArrayList);
				}
			}
		}

		// Token: 0x020001FE RID: 510
		private class HashtableFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001437 RID: 5175 RVA: 0x0003677C File Offset: 0x0003497C
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				Hashtable hashtable = (Hashtable)o;
				w.Write(this.PrimaryId);
				base.Write7BitEncodedInt(w, hashtable.Count);
				foreach (object obj in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ObjectStateFormatter.ObjectFormatter.WriteObject(w, dictionaryEntry.Key, ctx);
					ObjectStateFormatter.ObjectFormatter.WriteObject(w, dictionaryEntry.Value, ctx);
				}
			}

			// Token: 0x06001438 RID: 5176 RVA: 0x00036808 File Offset: 0x00034A08
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				int num = base.Read7BitEncodedInt(r);
				Hashtable hashtable = new Hashtable(num);
				for (int i = 0; i < num; i++)
				{
					object obj = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
					object obj2 = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
					hashtable.Add(obj, obj2);
				}
				return hashtable;
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003684B File Offset: 0x00034A4B
			protected override Type Type
			{
				get
				{
					return typeof(Hashtable);
				}
			}
		}

		// Token: 0x020001FF RID: 511
		private class ObjectArrayFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600143B RID: 5179 RVA: 0x00036858 File Offset: 0x00034A58
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				object[] array = (object[])o;
				w.Write(this.PrimaryId);
				base.Write7BitEncodedInt(w, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ObjectStateFormatter.ObjectFormatter.WriteObject(w, array[i], ctx);
				}
			}

			// Token: 0x0600143C RID: 5180 RVA: 0x0003689C File Offset: 0x00034A9C
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				int num = base.Read7BitEncodedInt(r);
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
				}
				return array;
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x0600143D RID: 5181 RVA: 0x000368CF File Offset: 0x00034ACF
			protected override Type Type
			{
				get
				{
					return typeof(object[]);
				}
			}
		}

		// Token: 0x02000200 RID: 512
		private class ColorFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600143F RID: 5183 RVA: 0x000368DC File Offset: 0x00034ADC
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				Color color = (Color)o;
				if (!color.IsEmpty && !color.IsKnownColor)
				{
					w.Write(this.PrimaryId);
					w.Write(color.ToArgb());
					return;
				}
				w.Write(this.SecondaryId);
				if (color.IsEmpty)
				{
					w.Write(-1);
					return;
				}
				w.Write((int)color.ToKnownColor());
			}

			// Token: 0x06001440 RID: 5184 RVA: 0x00036948 File Offset: 0x00034B48
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				int num = r.ReadInt32();
				if (token == this.PrimaryId)
				{
					return Color.FromArgb(num);
				}
				if (num == -1)
				{
					return Color.Empty;
				}
				return Color.FromKnownColor((KnownColor)num);
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003698B File Offset: 0x00034B8B
			protected override Type Type
			{
				get
				{
					return typeof(Color);
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001442 RID: 5186 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x02000201 RID: 513
		private class EnumFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001444 RID: 5188 RVA: 0x00036998 File Offset: 0x00034B98
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				object obj = Convert.ChangeType(o, ((Enum)o).GetTypeCode());
				w.Write(this.PrimaryId);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, o.GetType(), ctx);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, obj, ctx);
			}

			// Token: 0x06001445 RID: 5189 RVA: 0x000369D8 File Offset: 0x00034BD8
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				Type type = (Type)ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
				object obj = ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
				return Enum.ToObject(type, obj);
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06001446 RID: 5190 RVA: 0x000369FF File Offset: 0x00034BFF
			protected override Type Type
			{
				get
				{
					return typeof(Enum);
				}
			}
		}

		// Token: 0x02000202 RID: 514
		private class TypeFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x06001448 RID: 5192 RVA: 0x00036A0C File Offset: 0x00034C0C
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				if (ctx.RegisterCache(o))
				{
					w.Write(this.SecondaryId);
					w.Write(ctx.Key);
					return;
				}
				w.Write(this.PrimaryId);
				w.Write(((Type)o).FullName);
				w.Write(((Type)o).Assembly.FullName);
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x00036A70 File Offset: 0x00034C70
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				if (token == this.PrimaryId)
				{
					string text = r.ReadString();
					Type type = Assembly.Load(r.ReadString()).GetType(text);
					ctx.CacheItem(type);
					return type;
				}
				return ctx.GetCache(r.ReadInt16());
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x0600144A RID: 5194 RVA: 0x00036AB4 File Offset: 0x00034CB4
			protected override Type Type
			{
				get
				{
					return typeof(Type);
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x0600144B RID: 5195 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}
		}

		// Token: 0x02000203 RID: 515
		private class SingleRankArrayFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600144D RID: 5197 RVA: 0x00036AC0 File Offset: 0x00034CC0
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				Array array = (Array)o;
				if (array.GetType().GetElementType().IsPrimitive)
				{
					w.Write(this.SecondaryId);
					this._binaryFormatter.Serialize(w.BaseStream, o);
					return;
				}
				w.Write(this.PrimaryId);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, array.GetType().GetElementType(), ctx);
				base.Write7BitEncodedInt(w, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ObjectStateFormatter.ObjectFormatter.WriteObject(w, array.GetValue(i), ctx);
				}
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x00036B50 File Offset: 0x00034D50
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				if (token == this.SecondaryId)
				{
					return this._binaryFormatter.Deserialize(r.BaseStream);
				}
				Type type = (Type)ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
				int num = base.Read7BitEncodedInt(r);
				Array array = Array.CreateInstance(type, num);
				for (int i = 0; i < num; i++)
				{
					array.SetValue(ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx), i);
				}
				return array;
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x0600144F RID: 5199 RVA: 0x00036BAE File Offset: 0x00034DAE
			protected override Type Type
			{
				get
				{
					return typeof(Array);
				}
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06001450 RID: 5200 RVA: 0x000363BE File Offset: 0x000345BE
			protected override int NumberOfIds
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x04001494 RID: 5268
			private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
		}

		// Token: 0x02000204 RID: 516
		private class FontUnitFormatter : ObjectStateFormatter.StringFormatter
		{
			// Token: 0x06001452 RID: 5202 RVA: 0x00036BCD File Offset: 0x00034DCD
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				base.Write(w, o.ToString(), ctx);
			}

			// Token: 0x06001453 RID: 5203 RVA: 0x00036BDD File Offset: 0x00034DDD
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return FontUnit.Parse((string)base.Read(token, r, ctx));
			}

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x06001454 RID: 5204 RVA: 0x00036BF7 File Offset: 0x00034DF7
			protected override Type Type
			{
				get
				{
					return typeof(FontUnit);
				}
			}
		}

		// Token: 0x02000205 RID: 517
		private class UnitFormatter : ObjectStateFormatter.StringFormatter
		{
			// Token: 0x06001456 RID: 5206 RVA: 0x00036BCD File Offset: 0x00034DCD
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				base.Write(w, o.ToString(), ctx);
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x00036C03 File Offset: 0x00034E03
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				return Unit.Parse((string)base.Read(token, r, ctx));
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06001458 RID: 5208 RVA: 0x00036C1D File Offset: 0x00034E1D
			protected override Type Type
			{
				get
				{
					return typeof(Unit);
				}
			}
		}

		// Token: 0x02000206 RID: 518
		private class TypeConverterFormatter : ObjectStateFormatter.StringFormatter
		{
			// Token: 0x0600145A RID: 5210 RVA: 0x00036C2C File Offset: 0x00034E2C
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				ObjectStateFormatter.ObjectFormatter.WriteObject(w, o.GetType(), ctx);
				string text = (string)this.converter.ConvertTo(null, Helpers.InvariantCulture, o, typeof(string));
				base.Write(w, text, ctx);
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x00036C80 File Offset: 0x00034E80
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				Type type = (Type)ObjectStateFormatter.ObjectFormatter.ReadObject(r, ctx);
				this.converter = TypeDescriptor.GetConverter(type);
				token = r.ReadByte();
				string text = (string)base.Read(token, r, ctx);
				return this.converter.ConvertFrom(null, Helpers.InvariantCulture, text);
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x0600145C RID: 5212 RVA: 0x00036CCF File Offset: 0x00034ECF
			protected override Type Type
			{
				get
				{
					return typeof(TypeConverter);
				}
			}

			// Token: 0x17000657 RID: 1623
			// (set) Token: 0x0600145D RID: 5213 RVA: 0x00036CDB File Offset: 0x00034EDB
			public TypeConverter Converter
			{
				set
				{
					this.converter = value;
				}
			}

			// Token: 0x04001495 RID: 5269
			private TypeConverter converter;
		}

		// Token: 0x02000207 RID: 519
		private class BinaryObjectFormatter : ObjectStateFormatter.ObjectFormatter
		{
			// Token: 0x0600145F RID: 5215 RVA: 0x00036CE4 File Offset: 0x00034EE4
			protected override void Write(BinaryWriter w, object o, ObjectStateFormatter.WriterContext ctx)
			{
				w.Write(this.PrimaryId);
				MemoryStream memoryStream = new MemoryStream(128);
				new BinaryFormatter().Serialize(memoryStream, o);
				byte[] buffer = memoryStream.GetBuffer();
				base.Write7BitEncodedInt(w, buffer.Length);
				w.Write(buffer, 0, buffer.Length);
			}

			// Token: 0x06001460 RID: 5216 RVA: 0x00036D30 File Offset: 0x00034F30
			protected override object Read(byte token, BinaryReader r, ObjectStateFormatter.ReaderContext ctx)
			{
				int num = base.Read7BitEncodedInt(r);
				byte[] array = r.ReadBytes(num);
				if (array.Length != num)
				{
					throw new Exception();
				}
				return new BinaryFormatter().Deserialize(new MemoryStream(array));
			}

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06001461 RID: 5217 RVA: 0x00036D69 File Offset: 0x00034F69
			protected override Type Type
			{
				get
				{
					return typeof(object);
				}
			}
		}
	}
}
