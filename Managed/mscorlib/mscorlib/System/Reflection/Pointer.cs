using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>Provides a wrapper class for pointers.</summary>
	// Token: 0x020002FB RID: 763
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x00002111 File Offset: 0x00000311
		private Pointer()
		{
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0007EDA8 File Offset: 0x0007CFA8
		[SecurityCritical]
		private Pointer(SerializationInfo info, StreamingContext context)
		{
			this._ptr = ((IntPtr)info.GetValue("_ptr", typeof(IntPtr))).ToPointer();
			this._ptrType = (RuntimeType)info.GetValue("_ptrType", typeof(RuntimeType));
		}

		/// <summary>Boxes the supplied unmanaged memory pointer and the type associated with that pointer into a managed <see cref="T:System.Reflection.Pointer" /> wrapper object. The value and the type are saved so they can be accessed from the native code during an invocation.</summary>
		/// <returns>A pointer object.</returns>
		/// <param name="ptr">The supplied unmanaged memory pointer. </param>
		/// <param name="type">The type associated with the <paramref name="ptr" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a pointer. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x06002101 RID: 8449 RVA: 0x0007EE04 File Offset: 0x0007D004
		[SecurityCritical]
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a Pointer."), "ptr");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a Pointer."), "ptr");
			}
			return new Pointer
			{
				_ptr = ptr,
				_ptrType = runtimeType
			};
		}

		/// <summary>Returns the stored pointer.</summary>
		/// <returns>This method returns void.</returns>
		/// <param name="ptr">The stored pointer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is not a pointer. </exception>
		// Token: 0x06002102 RID: 8450 RVA: 0x0007EE7A File Offset: 0x0007D07A
		[SecurityCritical]
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a Pointer."), "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
		internal RuntimeType GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0007EEAC File Offset: 0x0007D0AC
		[SecurityCritical]
		internal object GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, fusion log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002105 RID: 8453 RVA: 0x0007EEBE File Offset: 0x0007D0BE
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_ptr", new IntPtr(this._ptr));
			info.AddValue("_ptrType", this._ptrType);
		}

		// Token: 0x0400129E RID: 4766
		[SecurityCritical]
		private unsafe void* _ptr;

		// Token: 0x0400129F RID: 4767
		private RuntimeType _ptrType;
	}
}
