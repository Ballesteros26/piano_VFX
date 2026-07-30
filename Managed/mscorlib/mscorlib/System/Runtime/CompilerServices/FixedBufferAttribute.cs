using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a field should be treated as containing a fixed number of elements of the specified primitive type. This class cannot be inherited. </summary>
	// Token: 0x02000877 RID: 2167
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> class. </summary>
		/// <param name="elementType">The type of the elements contained in the buffer.</param>
		/// <param name="length">The number of elements in the buffer.</param>
		// Token: 0x06005459 RID: 21593 RVA: 0x00127618 File Offset: 0x00125818
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		/// <summary>Gets the type of the elements contained in the fixed buffer. </summary>
		/// <returns>The type of the elements.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x0012762E File Offset: 0x0012582E
		public Type ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the number of elements in the fixed buffer. </summary>
		/// <returns>The number of elements in the fixed buffer.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600545B RID: 21595 RVA: 0x00127636 File Offset: 0x00125836
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04002BBF RID: 11199
		private Type elementType;

		// Token: 0x04002BC0 RID: 11200
		private int length;
	}
}
