using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the connection between an instance of <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the formatter-provided class best suited to parse the data inside the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
	// Token: 0x020006D2 RID: 1746
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is to be converted. </param>
		// Token: 0x060049F3 RID: 18931
		object Convert(object value, Type type);

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is to be converted. </param>
		// Token: 0x060049F4 RID: 18932
		object Convert(object value, TypeCode typeCode);

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049F5 RID: 18933
		bool ToBoolean(object value);

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049F6 RID: 18934
		char ToChar(object value);

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049F7 RID: 18935
		sbyte ToSByte(object value);

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049F8 RID: 18936
		byte ToByte(object value);

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049F9 RID: 18937
		short ToInt16(object value);

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FA RID: 18938
		ushort ToUInt16(object value);

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FB RID: 18939
		int ToInt32(object value);

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FC RID: 18940
		uint ToUInt32(object value);

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FD RID: 18941
		long ToInt64(object value);

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FE RID: 18942
		ulong ToUInt64(object value);

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x060049FF RID: 18943
		float ToSingle(object value);

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06004A00 RID: 18944
		double ToDouble(object value);

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06004A01 RID: 18945
		decimal ToDecimal(object value);

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06004A02 RID: 18946
		DateTime ToDateTime(object value);

		/// <summary>Converts a value to a <see cref="T:System.String" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06004A03 RID: 18947
		string ToString(object value);
	}
}
