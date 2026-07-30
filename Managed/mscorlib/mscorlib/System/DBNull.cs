using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a nonexistent value. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014B RID: 331
	[ComVisible(true)]
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x06000DC2 RID: 3522 RVA: 0x00002111 File Offset: 0x00000311
		private DBNull()
		{
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003A4F2 File Offset: 0x000386F2
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed."));
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.DBNull" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing information required to serialize the <see cref="T:System.DBNull" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.DBNull" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003A509 File Offset: 0x00038709
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2, null, null);
		}

		/// <summary>Returns an empty string (<see cref="F:System.String.Empty" />).</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003A514 File Offset: 0x00038714
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Returns an empty string using the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> to be used to format the return value.-or- null to obtain the format information from the current locale setting of the operating system. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003A514 File Offset: 0x00038714
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		/// <summary>Gets the <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />, which is <see cref="F:System.TypeCode.DBNull" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003A51B File Offset: 0x0003871B
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003A51B File Offset: 0x0003871B
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCA RID: 3530 RVA: 0x0003A51B File Offset: 0x0003871B
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCB RID: 3531 RVA: 0x0003A51B File Offset: 0x0003871B
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCC RID: 3532 RVA: 0x0003A51B File Offset: 0x0003871B
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCD RID: 3533 RVA: 0x0003A51B File Offset: 0x0003871B
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCE RID: 3534 RVA: 0x0003A51B File Offset: 0x0003871B
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DCF RID: 3535 RVA: 0x0003A51B File Offset: 0x0003871B
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003A51B File Offset: 0x0003871B
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003A51B File Offset: 0x0003871B
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003A51B File Offset: 0x0003871B
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003A51B File Offset: 0x0003871B
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003A51B File Offset: 0x0003871B
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003A51B File Offset: 0x0003871B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast from DBNull to other types."));
		}

		/// <summary>Converts the current <see cref="T:System.DBNull" /> object to the specified type.</summary>
		/// <returns>The boxed equivalent of the current <see cref="T:System.DBNull" /> object, if that conversion is supported; otherwise, an exception is thrown and no value is returned. </returns>
		/// <param name="type">The type to convert the current <see cref="T:System.DBNull" /> object to. </param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface and is used to augment the conversion. If null is specified, format information is obtained from the current culture. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003A52C File Offset: 0x0003872C
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.DBNull" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008CD RID: 2253
		public static readonly DBNull Value = new DBNull();
	}
}
