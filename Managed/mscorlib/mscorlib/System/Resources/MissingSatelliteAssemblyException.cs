using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown when the satellite assembly for the resources of the default culture is missing.</summary>
	// Token: 0x020002A2 RID: 674
	[ComVisible(true)]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with default properties.</summary>
		// Token: 0x06001F00 RID: 7936 RVA: 0x000788AD File Offset: 0x00076AAD
		public MissingSatelliteAssemblyException()
			: base(Environment.GetResourceString("Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application."))
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with the specified error message. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001F01 RID: 7937 RVA: 0x000788CA File Offset: 0x00076ACA
		public MissingSatelliteAssemblyException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and the name of a neutral culture. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="cultureName">The name of the neutral culture.</param>
		// Token: 0x06001F02 RID: 7938 RVA: 0x000788DE File Offset: 0x00076ADE
		public MissingSatelliteAssemblyException(string message, string cultureName)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
			this._cultureName = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06001F03 RID: 7939 RVA: 0x000788F9 File Offset: 0x00076AF9
		public MissingSatelliteAssemblyException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class from serialized data. </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x06001F04 RID: 7940 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the name of the default culture. </summary>
		/// <returns>The name of the default culture.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0007890E File Offset: 0x00076B0E
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x040010BB RID: 4283
		private string _cultureName;
	}
}
