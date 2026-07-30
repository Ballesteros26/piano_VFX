using System;
using System.Runtime.Serialization;
using System.Security;
using Unity;

namespace System.Runtime.CompilerServices
{
	/// <summary>Wraps an exception that does not derive from the <see cref="T:System.Exception" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000851 RID: 2129
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06005402 RID: 21506 RVA: 0x00126E34 File Offset: 0x00125034
		private RuntimeWrappedException(object thrownObject)
			: base(Environment.GetResourceString("An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException."))
		{
			base.SetErrorCode(-2146233026);
			this.m_wrappedException = thrownObject;
		}

		/// <summary>Gets the object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>The object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</returns>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x00126E58 File Offset: 0x00125058
		public object WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x06005404 RID: 21508 RVA: 0x00126E60 File Offset: 0x00125060
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.m_wrappedException, typeof(object));
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00126E93 File Offset: 0x00125093
		internal RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal RuntimeWrappedException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002BA2 RID: 11170
		private object m_wrappedException;
	}
}
