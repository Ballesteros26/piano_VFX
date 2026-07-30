using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown when a name conflict occurs while generating a strongly typed <see cref="T:System.Data.DataSet" />. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011B RID: 283
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x0004B9D8 File Offset: 0x00049BD8
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			int num = (int)info.GetValue(this.KEY_ARRAYCOUNT, typeof(int));
			if (num > 0)
			{
				this.errorList = new ArrayList();
				for (int i = 0; i < num; i++)
				{
					this.errorList.Add(info.GetValue(this.KEY_ARRAYVALUES + i, typeof(string)));
				}
				return;
			}
			this.errorList = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		// Token: 0x06000E5C RID: 3676 RVA: 0x0004BA6F File Offset: 0x00049C6F
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x0004BA9F File Offset: 0x00049C9F
		public TypedDataSetGeneratorException(string message)
			: base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string and inner exception. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x0004BAC9 File Offset: 0x00049CC9
		public TypedDataSetGeneratorException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		/// <param name="list">
		///   <see cref="T:System.Collections.ArrayList" /> object containing a dynamic list of exceptions. </param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		public TypedDataSetGeneratorException(ArrayList list)
			: this()
		{
			this.errorList = list;
			base.HResult = -2146232021;
		}

		/// <summary>Gets a dynamic list of generated errors.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0004BB0E File Offset: 0x00049D0E
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the <see cref="T:System.Data.TypedDataSetGeneratorException" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000E61 RID: 3681 RVA: 0x0004BB18 File Offset: 0x00049D18
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.errorList != null)
			{
				info.AddValue(this.KEY_ARRAYCOUNT, this.errorList.Count);
				for (int i = 0; i < this.errorList.Count; i++)
				{
					info.AddValue(this.KEY_ARRAYVALUES + i, this.errorList[i].ToString());
				}
				return;
			}
			info.AddValue(this.KEY_ARRAYCOUNT, 0);
		}

		// Token: 0x040009F1 RID: 2545
		private ArrayList errorList;

		// Token: 0x040009F2 RID: 2546
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x040009F3 RID: 2547
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
