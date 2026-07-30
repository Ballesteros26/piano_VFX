using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Lets you control the physical layout of the data fields of a class or structure.</summary>
	// Token: 0x020008C8 RID: 2248
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x0600551C RID: 21788 RVA: 0x001287F4 File Offset: 0x001269F4
		[SecurityCritical]
		internal static StructLayoutAttribute GetCustomAttribute(RuntimeType type)
		{
			if (!StructLayoutAttribute.IsDefined(type))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			typeAttributes = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			type.GetPacking(out num, out num2);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind, num, num2, charSet);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0012887F File Offset: 0x00126A7F
		internal static bool IsDefined(RuntimeType type)
		{
			return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0012889C File Offset: 0x00126A9C
		internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
		{
			this._val = layoutKind;
			this.Pack = pack;
			this.Size = size;
			this.CharSet = charSet;
		}

		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">One of the enumeration values that specifes how the class or structure should be arranged. </param>
		// Token: 0x0600551F RID: 21791 RVA: 0x001288C1 File Offset: 0x00126AC1
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this._val = layoutKind;
		}

		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">A 16-bit integer that represents one of the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> values that specifes how the class or structure should be arranged. </param>
		// Token: 0x06005520 RID: 21792 RVA: 0x001288C1 File Offset: 0x00126AC1
		public StructLayoutAttribute(short layoutKind)
		{
			this._val = (LayoutKind)layoutKind;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> value that specifies how the class or structure is arranged.</summary>
		/// <returns>One of the enumeration values that specifies how the class or structure is arranged.</returns>
		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x001288D0 File Offset: 0x00126AD0
		public LayoutKind Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002CA2 RID: 11426
		private const int DEFAULT_PACKING_SIZE = 8;

		// Token: 0x04002CA3 RID: 11427
		internal LayoutKind _val;

		/// <summary>Controls the alignment of data fields of a class or structure in memory.</summary>
		// Token: 0x04002CA4 RID: 11428
		public int Pack;

		/// <summary>Indicates the absolute size of the class or structure.</summary>
		// Token: 0x04002CA5 RID: 11429
		public int Size;

		/// <summary>Indicates whether string data fields within the class should be marshaled as LPWSTR or LPSTR by default.</summary>
		// Token: 0x04002CA6 RID: 11430
		public CharSet CharSet;
	}
}
