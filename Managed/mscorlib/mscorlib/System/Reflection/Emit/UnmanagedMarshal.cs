using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents the class that describes how to marshal a field from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x02000383 RID: 899
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x060029AD RID: 10669 RVA: 0x00093ECE File Offset: 0x000920CE
		private UnmanagedMarshal(UnmanagedType maint, int cnt)
		{
			this.count = cnt;
			this.t = maint;
			this.tbase = maint;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00093EEB File Offset: 0x000920EB
		private UnmanagedMarshal(UnmanagedType maint, UnmanagedType elemt)
		{
			this.count = 0;
			this.t = maint;
			this.tbase = elemt;
		}

		/// <summary>Gets an unmanaged base type. This property is read-only.</summary>
		/// <returns>An UnmanagedType object.</returns>
		/// <exception cref="T:System.ArgumentException">The unmanaged type is not an LPArray or a SafeArray. </exception>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x00093F08 File Offset: 0x00092108
		public UnmanagedType BaseType
		{
			get
			{
				if (this.t == UnmanagedType.LPArray)
				{
					throw new ArgumentException();
				}
				if (this.t == UnmanagedType.SafeArray)
				{
					throw new ArgumentException();
				}
				return this.tbase;
			}
		}

		/// <summary>Gets a number element. This property is read-only.</summary>
		/// <returns>An integer indicating the element count.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not an unmanaged element count. </exception>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x00093F30 File Offset: 0x00092130
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Indicates an unmanaged type. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> object.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x00093F38 File Offset: 0x00092138
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.t;
			}
		}

		/// <summary>Gets a GUID. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a custom marshaler. </exception>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x00093F40 File Offset: 0x00092140
		public Guid IIDGuid
		{
			get
			{
				return new Guid(this.guid);
			}
		}

		/// <summary>Specifies a fixed-length array (ByValArray) to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemCount">The number of elements in the fixed-length array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060029B3 RID: 10675 RVA: 0x00093F4D File Offset: 0x0009214D
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, elemCount);
		}

		/// <summary>Specifies a string in a fixed array buffer (ByValTStr) to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemCount">The number of elements in the fixed array buffer. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060029B4 RID: 10676 RVA: 0x00093F57 File Offset: 0x00092157
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, elemCount);
		}

		/// <summary>Specifies an LPArray to marshal to unmanaged code. The length of an LPArray is determined at runtime by the size of the actual marshaled array.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemType">The unmanaged type to which to marshal the array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060029B5 RID: 10677 RVA: 0x00093F61 File Offset: 0x00092161
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType);
		}

		/// <summary>Specifies a SafeArray to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemType">The base type or the UnmanagedType of each element of the array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060029B6 RID: 10678 RVA: 0x00093F6B File Offset: 0x0009216B
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, elemType);
		}

		/// <summary>Specifies a given type that is to be marshaled to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="unmanagedType">The unmanaged type to which the type is to be marshaled. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060029B7 RID: 10679 RVA: 0x00093F75 File Offset: 0x00092175
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			return new UnmanagedMarshal(unmanagedType, unmanagedType);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00093F80 File Offset: 0x00092180
		internal static UnmanagedMarshal DefineCustom(Type typeref, string cookie, string mtype, Guid id)
		{
			UnmanagedMarshal unmanagedMarshal = new UnmanagedMarshal(UnmanagedType.CustomMarshaler, UnmanagedType.CustomMarshaler);
			unmanagedMarshal.mcookie = cookie;
			unmanagedMarshal.marshaltype = mtype;
			unmanagedMarshal.marshaltyperef = typeref;
			if (id == Guid.Empty)
			{
				unmanagedMarshal.guid = string.Empty;
			}
			else
			{
				unmanagedMarshal.guid = id.ToString();
			}
			return unmanagedMarshal;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00093FDA File Offset: 0x000921DA
		internal static UnmanagedMarshal DefineLPArrayInternal(UnmanagedType elemType, int sizeConst, int sizeParamIndex)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType)
			{
				count = sizeConst,
				param_num = sizeParamIndex,
				has_size = true
			};
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal UnmanagedMarshal()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001625 RID: 5669
		private int count;

		// Token: 0x04001626 RID: 5670
		private UnmanagedType t;

		// Token: 0x04001627 RID: 5671
		private UnmanagedType tbase;

		// Token: 0x04001628 RID: 5672
		private string guid;

		// Token: 0x04001629 RID: 5673
		private string mcookie;

		// Token: 0x0400162A RID: 5674
		private string marshaltype;

		// Token: 0x0400162B RID: 5675
		internal Type marshaltyperef;

		// Token: 0x0400162C RID: 5676
		private int param_num;

		// Token: 0x0400162D RID: 5677
		private bool has_size;
	}
}
