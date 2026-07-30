using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>The exception thrown when a method is invoked which attempts to construct a culture that is not available on the machine.</summary>
	// Token: 0x02000400 RID: 1024
	[ComVisible(true)]
	[Serializable]
	public class CultureNotFoundException : ArgumentException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x06003089 RID: 12425 RVA: 0x000AD766 File Offset: 0x000AB966
		public CultureNotFoundException()
			: base(CultureNotFoundException.DefaultMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600308A RID: 12426 RVA: 0x000AD773 File Offset: 0x000AB973
		public CultureNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600308B RID: 12427 RVA: 0x000AD77C File Offset: 0x000AB97C
		public CultureNotFoundException(string paramName, string message)
			: base(message, paramName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600308C RID: 12428 RVA: 0x000AD786 File Offset: 0x000AB986
		public CultureNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600308D RID: 12429 RVA: 0x000AD790 File Offset: 0x000AB990
		public CultureNotFoundException(string paramName, int invalidCultureId, string message)
			: base(message, paramName)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600308E RID: 12430 RVA: 0x000AD7A6 File Offset: 0x000AB9A6
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
			: base(message, innerException)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600308F RID: 12431 RVA: 0x000AD7BC File Offset: 0x000AB9BC
		public CultureNotFoundException(string paramName, string invalidCultureName, string message)
			: base(message, paramName)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06003090 RID: 12432 RVA: 0x000AD7CD File Offset: 0x000AB9CD
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
			: base(message, innerException)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06003091 RID: 12433 RVA: 0x000AD7E0 File Offset: 0x000AB9E0
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this.m_invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003092 RID: 12434 RVA: 0x000AD838 File Offset: 0x000ABA38
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			int? num = null;
			num = this.m_invalidCultureId;
			info.AddValue("InvalidCultureId", num, typeof(int?));
			info.AddValue("InvalidCultureName", this.m_invalidCultureName, typeof(string));
		}

		/// <summary>Gets the Culture ID that cannot be found.</summary>
		/// <returns>The invalid Culture ID.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000AD8A0 File Offset: 0x000ABAA0
		public virtual int? InvalidCultureId
		{
			get
			{
				return this.m_invalidCultureId;
			}
		}

		/// <summary>Gets the Culture Name that cannot be found.</summary>
		/// <returns>The invalid Culture Name.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000AD8A8 File Offset: 0x000ABAA8
		public virtual string InvalidCultureName
		{
			get
			{
				return this.m_invalidCultureName;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000AD8B0 File Offset: 0x000ABAB0
		private static string DefaultMessage
		{
			get
			{
				return Environment.GetResourceString("Culture is not supported.");
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000AD8BC File Offset: 0x000ABABC
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
				}
				return this.InvalidCultureName;
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A text string describing the details of the exception.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000AD904 File Offset: 0x000ABB04
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this.m_invalidCultureId == null && this.m_invalidCultureName == null)
				{
					return message;
				}
				string resourceString = Environment.GetResourceString("{0} is an invalid culture identifier.", new object[] { this.FormatedInvalidCultureId });
				if (message == null)
				{
					return resourceString;
				}
				return message + Environment.NewLine + resourceString;
			}
		}

		// Token: 0x0400193B RID: 6459
		private string m_invalidCultureName;

		// Token: 0x0400193C RID: 6460
		private int? m_invalidCultureId;
	}
}
