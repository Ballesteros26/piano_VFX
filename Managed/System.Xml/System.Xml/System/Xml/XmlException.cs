using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Xml
{
	/// <summary>Returns detailed information about the last exception.</summary>
	// Token: 0x0200029C RID: 668
	[Serializable]
	public class XmlException : SystemException
	{
		/// <summary>Initializes a new instance of the XmlException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XmlException. </param>
		/// <param name="context">The StreamingContext object containing the context information. </param>
		// Token: 0x060018B3 RID: 6323 RVA: 0x0008F000 File Offset: 0x0008D200
		protected XmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			this.sourceUri = string.Empty;
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (!(name == "sourceUri"))
				{
					if (name == "version")
					{
						text = (string)serializationEntry.Value;
					}
				}
				else
				{
					this.sourceUri = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlException.CreateMessage(this.res, this.args, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the XmlException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060018B4 RID: 6324 RVA: 0x0008F130 File Offset: 0x0008D330
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the XmlException class.</summary>
		// Token: 0x060018B5 RID: 6325 RVA: 0x0008F1AA File Offset: 0x0008D3AA
		public XmlException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class with a specified error message.</summary>
		/// <param name="message">The error description. </param>
		// Token: 0x060018B6 RID: 6326 RVA: 0x0008F1B3 File Offset: 0x0008D3B3
		public XmlException(string message)
			: this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the XmlException, if any. This value can be null. </param>
		// Token: 0x060018B7 RID: 6327 RVA: 0x0008F1BF File Offset: 0x0008D3BF
		public XmlException(string message, Exception innerException)
			: this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class with the specified message, inner exception, line number, and line position.</summary>
		/// <param name="message">The error description. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. This value can be null. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred. </param>
		/// <param name="linePosition">The line position indicating where the error occurred. </param>
		// Token: 0x060018B8 RID: 6328 RVA: 0x0008F1CB File Offset: 0x0008D3CB
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition)
			: this(message, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0008F1DC File Offset: 0x0008D3DC
		internal XmlException(string message, Exception innerException, int lineNumber, int linePosition, string sourceUri)
			: base(XmlException.FormatUserMessage(message, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = ((message == null) ? "An XML error has occurred." : "{0}");
			this.args = new string[] { message };
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0008F240 File Offset: 0x0008D440
		internal XmlException(string res, string[] args)
			: this(res, args, null, 0, 0, null)
		{
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0008F24E File Offset: 0x0008D44E
		internal XmlException(string res, string[] args, string sourceUri)
			: this(res, args, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0008F25C File Offset: 0x0008D45C
		internal XmlException(string res, string arg)
			: this(res, new string[] { arg }, null, 0, 0, null)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0008F273 File Offset: 0x0008D473
		internal XmlException(string res, string arg, string sourceUri)
			: this(res, new string[] { arg }, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0008F28A File Offset: 0x0008D48A
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo)
			: this(res, new string[] { arg }, lineInfo, null)
		{
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0008F29F File Offset: 0x0008D49F
		internal XmlException(string res, string arg, Exception innerException, IXmlLineInfo lineInfo)
			: this(res, new string[] { arg }, innerException, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, null)
		{
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0008F2D0 File Offset: 0x0008D4D0
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo, string sourceUri)
			: this(res, new string[] { arg }, lineInfo, sourceUri)
		{
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008F2E6 File Offset: 0x0008D4E6
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo)
			: this(res, args, lineInfo, null)
		{
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0008F2F2 File Offset: 0x0008D4F2
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo, string sourceUri)
			: this(res, args, null, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, sourceUri)
		{
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008F317 File Offset: 0x0008D517
		internal XmlException(string res, int lineNumber, int linePosition)
			: this(res, null, null, lineNumber, linePosition)
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008F324 File Offset: 0x0008D524
		internal XmlException(string res, string arg, int lineNumber, int linePosition)
			: this(res, new string[] { arg }, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0008F33C File Offset: 0x0008D53C
		internal XmlException(string res, string arg, int lineNumber, int linePosition, string sourceUri)
			: this(res, new string[] { arg }, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008F355 File Offset: 0x0008D555
		internal XmlException(string res, string[] args, int lineNumber, int linePosition)
			: this(res, args, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0008F364 File Offset: 0x0008D564
		internal XmlException(string res, string[] args, int lineNumber, int linePosition, string sourceUri)
			: this(res, args, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0008F374 File Offset: 0x0008D574
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition)
			: this(res, args, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0008F384 File Offset: 0x0008D584
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition, string sourceUri)
			: base(XmlException.CreateMessage(res, args, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0008F3D4 File Offset: 0x0008D5D4
		private static string FormatUserMessage(string message, int lineNumber, int linePosition)
		{
			if (message == null)
			{
				return XmlException.CreateMessage("An XML error has occurred.", null, lineNumber, linePosition);
			}
			if (lineNumber == 0 && linePosition == 0)
			{
				return message;
			}
			return XmlException.CreateMessage("{0}", new string[] { message }, lineNumber, linePosition);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0008F408 File Offset: 0x0008D608
		private static string CreateMessage(string res, string[] args, int lineNumber, int linePosition)
		{
			string text4;
			try
			{
				string text;
				if (lineNumber == 0)
				{
					text = Res.GetString(res, args);
				}
				else
				{
					string text2 = lineNumber.ToString(CultureInfo.InvariantCulture);
					string text3 = linePosition.ToString(CultureInfo.InvariantCulture);
					text = Res.GetString(res, args);
					text = Res.GetString("{0} Line {1}, position {2}.", new string[] { text, text2, text3 });
				}
				text4 = text;
			}
			catch (MissingManifestResourceException)
			{
				text4 = "UNKNOWN(" + res + ")";
			}
			return text4;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0008F48C File Offset: 0x0008D68C
		internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < data.Length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0008F4B1 File Offset: 0x0008D6B1
		internal static string[] BuildCharExceptionArgs(char[] data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data, data.Length, invCharIndex);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0008F4BD File Offset: 0x0008D6BD
		internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0008F4D8 File Offset: 0x0008D6D8
		internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
		{
			string[] array = new string[2];
			if (XmlCharType.IsHighSurrogate((int)invChar) && nextChar != '\0')
			{
				int num = XmlCharType.CombineSurrogateChar((int)nextChar, (int)invChar);
				array[0] = new string(new char[] { invChar, nextChar });
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", num);
			}
			else
			{
				if (invChar == '\0')
				{
					array[0] = ".";
				}
				else
				{
					array[0] = invChar.ToString(CultureInfo.InvariantCulture);
				}
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", (int)invChar);
			}
			return array;
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0008F564 File Offset: 0x0008D764
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0008F56C File Offset: 0x0008D76C
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the location of the XML file.</summary>
		/// <returns>The source URI for the XML data. If there is no source URI, this property returns null.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0008F574 File Offset: 0x0008D774
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets a message describing the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0008F57C File Offset: 0x0008D77C
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008F593 File Offset: 0x0008D793
		internal string ResString
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0008F59B File Offset: 0x0008D79B
		internal static bool IsCatchableException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is ThreadInterruptedException) && !(e is NullReferenceException) && !(e is AccessViolationException);
		}

		// Token: 0x0400101D RID: 4125
		private string res;

		// Token: 0x0400101E RID: 4126
		private string[] args;

		// Token: 0x0400101F RID: 4127
		private int lineNumber;

		// Token: 0x04001020 RID: 4128
		private int linePosition;

		// Token: 0x04001021 RID: 4129
		[OptionalField]
		private string sourceUri;

		// Token: 0x04001022 RID: 4130
		private string message;
	}
}
