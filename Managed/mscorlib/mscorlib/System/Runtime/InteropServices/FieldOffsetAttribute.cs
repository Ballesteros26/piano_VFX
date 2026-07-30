using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the physical position of fields within the unmanaged representation of a class or structure.</summary>
	// Token: 0x020008C9 RID: 2249
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x06005522 RID: 21794 RVA: 0x001288D8 File Offset: 0x00126AD8
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int fieldOffset;
			if (field.DeclaringType != null && (fieldOffset = field.GetFieldOffset()) >= 0)
			{
				return new FieldOffsetAttribute(fieldOffset);
			}
			return null;
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00128906 File Offset: 0x00126B06
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> class with the offset in the structure to the beginning of the field.</summary>
		/// <param name="offset">The offset in bytes from the beginning of the structure to the beginning of the field. </param>
		// Token: 0x06005524 RID: 21796 RVA: 0x00128911 File Offset: 0x00126B11
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		/// <summary>Gets the offset from the beginning of the structure to the beginning of the field.</summary>
		/// <returns>The offset from the beginning of the structure to the beginning of the field.</returns>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00128920 File Offset: 0x00126B20
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002CA7 RID: 11431
		internal int _val;
	}
}
