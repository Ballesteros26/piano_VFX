using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by the <see cref="M:System.Reflection.Module.GetTypes" /> method if any of the classes in a module cannot be loaded. This class cannot be inherited.</summary>
	// Token: 0x020002FE RID: 766
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x0007EF0D File Offset: 0x0007D10D
		private ReflectionTypeLoadException()
			: base(Environment.GetResourceString("Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information."))
		{
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0007EF2A File Offset: 0x0007D12A
		private ReflectionTypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232830);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes and their associated exceptions.</summary>
		/// <param name="classes">An array of type Type containing the classes that were defined in the module and loaded. This array can contain null reference (Nothing in Visual Basic) values. </param>
		/// <param name="exceptions">An array of type Exception containing the exceptions that were thrown by the class loader. The null reference (Nothing in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array. </param>
		// Token: 0x0600210C RID: 8460 RVA: 0x0007EF3E File Offset: 0x0007D13E
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
			: base(null)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes, their associated exceptions, and exception descriptions.</summary>
		/// <param name="classes">An array of type Type containing the classes that were defined in the module and loaded. This array can contain null reference (Nothing in Visual Basic) values. </param>
		/// <param name="exceptions">An array of type Exception containing the exceptions that were thrown by the class loader. The null reference (Nothing in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array. </param>
		/// <param name="message">A String describing the reason the exception was thrown. </param>
		// Token: 0x0600210D RID: 8461 RVA: 0x0007EF60 File Offset: 0x0007D160
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
			: base(message)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0007EF84 File Offset: 0x0007D184
		internal ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._classes = (Type[])info.GetValue("Types", typeof(Type[]));
			this._exceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		/// <summary>Gets the array of classes that were defined in the module and loaded.</summary>
		/// <returns>An array of type Type containing the classes that were defined in the module and loaded. This array can contain some null values.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0007EFD9 File Offset: 0x0007D1D9
		public Type[] Types
		{
			get
			{
				return this._classes;
			}
		}

		/// <summary>Gets the array of exceptions thrown by the class loader.</summary>
		/// <returns>An array of type Exception containing the exceptions thrown by the class loader. The null values in the <paramref name="classes" /> array of this instance line up with the exceptions in this array.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0007EFE1 File Offset: 0x0007D1E1
		public Exception[] LoaderExceptions
		{
			get
			{
				return this._exceptions;
			}
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">info is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002111 RID: 8465 RVA: 0x0007EFEC File Offset: 0x0007D1EC
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Types", this._classes, typeof(Type[]));
			info.AddValue("Exceptions", this._exceptions, typeof(Exception[]));
		}

		// Token: 0x040012A9 RID: 4777
		private Type[] _classes;

		// Token: 0x040012AA RID: 4778
		private Exception[] _exceptions;
	}
}
