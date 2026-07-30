using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides base functionality for the common language runtime serialization formatters.</summary>
	// Token: 0x020006CB RID: 1739
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Formatter : IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatter" /> class.</summary>
		// Token: 0x0600499C RID: 18844 RVA: 0x00107FB5 File Offset: 0x001061B5
		protected Formatter()
		{
			this.m_objectQueue = new Queue();
			this.m_idGenerator = new ObjectIDGenerator();
		}

		/// <summary>When overridden in a derived class, deserializes the stream attached to the formatter when it was created, creating a graph of objects identical to the graph originally serialized into that stream.</summary>
		/// <returns>The top object of the deserialized graph of objects.</returns>
		/// <param name="serializationStream">The stream to deserialize. </param>
		// Token: 0x0600499D RID: 18845
		public abstract object Deserialize(Stream serializationStream);

		/// <summary>Returns the next object to serialize, from the formatter's internal work queue.</summary>
		/// <returns>The next object to serialize.</returns>
		/// <param name="objID">The ID assigned to the current object during serialization. </param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The next object retrieved from the work queue did not have an assigned ID. </exception>
		// Token: 0x0600499E RID: 18846 RVA: 0x00107FD4 File Offset: 0x001061D4
		protected virtual object GetNext(out long objID)
		{
			if (this.m_objectQueue.Count == 0)
			{
				objID = 0L;
				return null;
			}
			object obj = this.m_objectQueue.Dequeue();
			bool flag;
			objID = this.m_idGenerator.HasId(obj, out flag);
			if (flag)
			{
				throw new SerializationException(Environment.GetResourceString("Object has never been assigned an objectID."));
			}
			return obj;
		}

		/// <summary>Schedules an object for later serialization.</summary>
		/// <returns>The object ID assigned to the object.</returns>
		/// <param name="obj">The object to schedule for serialization. </param>
		// Token: 0x0600499F RID: 18847 RVA: 0x00108024 File Offset: 0x00106224
		protected virtual long Schedule(object obj)
		{
			if (obj == null)
			{
				return 0L;
			}
			bool flag;
			long id = this.m_idGenerator.GetId(obj, out flag);
			if (flag)
			{
				this.m_objectQueue.Enqueue(obj);
			}
			return id;
		}

		/// <summary>When overridden in a derived class, serializes the graph of objects with the specified root to the stream already attached to the formatter.</summary>
		/// <param name="serializationStream">The stream to which the objects are serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		// Token: 0x060049A0 RID: 18848
		public abstract void Serialize(Stream serializationStream, object graph);

		/// <summary>When overridden in a derived class, writes an array to the stream already attached to the formatter.</summary>
		/// <param name="obj">The array to write. </param>
		/// <param name="name">The name of the array. </param>
		/// <param name="memberType">The type of elements that the array holds. </param>
		// Token: 0x060049A1 RID: 18849
		protected abstract void WriteArray(object obj, string name, Type memberType);

		/// <summary>When overridden in a derived class, writes a Boolean value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A2 RID: 18850
		protected abstract void WriteBoolean(bool val, string name);

		/// <summary>When overridden in a derived class, writes an 8-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A3 RID: 18851
		protected abstract void WriteByte(byte val, string name);

		/// <summary>When overridden in a derived class, writes a Unicode character to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A4 RID: 18852
		protected abstract void WriteChar(char val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.DateTime" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A5 RID: 18853
		protected abstract void WriteDateTime(DateTime val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.Decimal" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A6 RID: 18854
		protected abstract void WriteDecimal(decimal val, string name);

		/// <summary>When overridden in a derived class, writes a double-precision floating-point number to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A7 RID: 18855
		protected abstract void WriteDouble(double val, string name);

		/// <summary>When overridden in a derived class, writes a 16-bit signed integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A8 RID: 18856
		protected abstract void WriteInt16(short val, string name);

		/// <summary>When overridden in a derived class, writes a 32-bit signed integer to the stream.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049A9 RID: 18857
		protected abstract void WriteInt32(int val, string name);

		/// <summary>When overridden in a derived class, writes a 64-bit signed integer to the stream.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049AA RID: 18858
		protected abstract void WriteInt64(long val, string name);

		/// <summary>When overridden in a derived class, writes an object reference to the stream already attached to the formatter.</summary>
		/// <param name="obj">The object reference to write. </param>
		/// <param name="name">The name of the member. </param>
		/// <param name="memberType">The type of object the reference points to. </param>
		// Token: 0x060049AB RID: 18859
		protected abstract void WriteObjectRef(object obj, string name, Type memberType);

		/// <summary>Inspects the type of data received, and calls the appropriate Write method to perform the write to the stream already attached to the formatter.</summary>
		/// <param name="memberName">The name of the member to serialize. </param>
		/// <param name="data">The object to write to the stream attached to the formatter. </param>
		// Token: 0x060049AC RID: 18860 RVA: 0x00108054 File Offset: 0x00106254
		protected virtual void WriteMember(string memberName, object data)
		{
			if (data == null)
			{
				this.WriteObjectRef(data, memberName, typeof(object));
				return;
			}
			Type type = data.GetType();
			if (type == typeof(bool))
			{
				this.WriteBoolean(Convert.ToBoolean(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(char))
			{
				this.WriteChar(Convert.ToChar(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(sbyte))
			{
				this.WriteSByte(Convert.ToSByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(byte))
			{
				this.WriteByte(Convert.ToByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(short))
			{
				this.WriteInt16(Convert.ToInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(int))
			{
				this.WriteInt32(Convert.ToInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(long))
			{
				this.WriteInt64(Convert.ToInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(float))
			{
				this.WriteSingle(Convert.ToSingle(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(double))
			{
				this.WriteDouble(Convert.ToDouble(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.WriteDateTime(Convert.ToDateTime(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(decimal))
			{
				this.WriteDecimal(Convert.ToDecimal(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ushort))
			{
				this.WriteUInt16(Convert.ToUInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(uint))
			{
				this.WriteUInt32(Convert.ToUInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ulong))
			{
				this.WriteUInt64(Convert.ToUInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type.IsArray)
			{
				this.WriteArray(data, memberName, type);
				return;
			}
			if (type.IsValueType)
			{
				this.WriteValueType(data, memberName, type);
				return;
			}
			this.WriteObjectRef(data, memberName, type);
		}

		/// <summary>When overridden in a derived class, writes an 8-bit signed integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049AD RID: 18861
		[CLSCompliant(false)]
		protected abstract void WriteSByte(sbyte val, string name);

		/// <summary>When overridden in a derived class, writes a single-precision floating-point number to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049AE RID: 18862
		protected abstract void WriteSingle(float val, string name);

		/// <summary>When overridden in a derived class, writes a <see cref="T:System.TimeSpan" /> value to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049AF RID: 18863
		protected abstract void WriteTimeSpan(TimeSpan val, string name);

		/// <summary>When overridden in a derived class, writes a 16-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049B0 RID: 18864
		[CLSCompliant(false)]
		protected abstract void WriteUInt16(ushort val, string name);

		/// <summary>When overridden in a derived class, writes a 32-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049B1 RID: 18865
		[CLSCompliant(false)]
		protected abstract void WriteUInt32(uint val, string name);

		/// <summary>When overridden in a derived class, writes a 64-bit unsigned integer to the stream already attached to the formatter.</summary>
		/// <param name="val">The value to write. </param>
		/// <param name="name">The name of the member. </param>
		// Token: 0x060049B2 RID: 18866
		[CLSCompliant(false)]
		protected abstract void WriteUInt64(ulong val, string name);

		/// <summary>When overridden in a derived class, writes a value of the given type to the stream already attached to the formatter.</summary>
		/// <param name="obj">The object representing the value type. </param>
		/// <param name="name">The name of the member. </param>
		/// <param name="memberType">The <see cref="T:System.Type" /> of the value type. </param>
		// Token: 0x060049B3 RID: 18867
		protected abstract void WriteValueType(object obj, string name, Type memberType);

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> used with the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> used with the current formatter.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060049B4 RID: 18868
		// (set) Token: 0x060049B5 RID: 18869
		public abstract ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060049B6 RID: 18870
		// (set) Token: 0x060049B7 RID: 18871
		public abstract SerializationBinder Binder { get; set; }

		/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for the current serialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for the current serialization.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060049B8 RID: 18872
		// (set) Token: 0x060049B9 RID: 18873
		public abstract StreamingContext Context { get; set; }

		/// <summary>Contains the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> used with the current formatter.</summary>
		// Token: 0x040026A8 RID: 9896
		protected ObjectIDGenerator m_idGenerator;

		/// <summary>Contains a <see cref="T:System.Collections.Queue" /> of the objects left to serialize.</summary>
		// Token: 0x040026A9 RID: 9897
		protected Queue m_objectQueue;
	}
}
