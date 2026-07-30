using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.XPath
{
	/// <summary>Provides the exception thrown when an error occurs while processing an XPath expression. </summary>
	// Token: 0x020002B1 RID: 689
	[Serializable]
	public class XPathException : SystemException
	{
		/// <summary>Uses the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects to initialize a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains all the properties of an <see cref="T:System.Xml.XPath.XPathException" />. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06001943 RID: 6467 RVA: 0x0009095C File Offset: 0x0008EB5C
		protected XPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XPathException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see cref="T:System.Xml.XPath.XPathException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the specified <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06001944 RID: 6468 RVA: 0x00090A0D File Offset: 0x0008EC0D
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		// Token: 0x06001945 RID: 6469 RVA: 0x00090A49 File Offset: 0x0008EC49
		public XPathException()
			: this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class with the specified exception message.</summary>
		/// <param name="message">The description of the error condition.</param>
		// Token: 0x06001946 RID: 6470 RVA: 0x00090A57 File Offset: 0x0008EC57
		public XPathException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class using the specified exception message and <see cref="T:System.Exception" /> object.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the <see cref="T:System.Xml.XPath.XPathException" />, if any. This value can be null. </param>
		// Token: 0x06001947 RID: 6471 RVA: 0x00090A61 File Offset: 0x0008EC61
		public XPathException(string message, Exception innerException)
			: this("{0}", new string[] { message }, innerException)
		{
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00090A79 File Offset: 0x0008EC79
		internal static XPathException Create(string res)
		{
			return new XPathException(res, null);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00090A82 File Offset: 0x0008EC82
		internal static XPathException Create(string res, string arg)
		{
			return new XPathException(res, new string[] { arg });
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00090A94 File Offset: 0x0008EC94
		internal static XPathException Create(string res, string arg, string arg2)
		{
			return new XPathException(res, new string[] { arg, arg2 });
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00090AAA File Offset: 0x0008ECAA
		internal static XPathException Create(string res, string arg, Exception innerException)
		{
			return new XPathException(res, new string[] { arg }, innerException);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00090ABD File Offset: 0x0008ECBD
		private XPathException(string res, string[] args)
			: this(res, args, null)
		{
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00090AC8 File Offset: 0x0008ECC8
		private XPathException(string res, string[] args, Exception inner)
			: base(XPathException.CreateMessage(res, args), inner)
		{
			base.HResult = -2146231997;
			this.res = res;
			this.args = args;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00090AF4 File Offset: 0x0008ECF4
		private static string CreateMessage(string res, string[] args)
		{
			string text2;
			try
			{
				string text = Res.GetString(res, args);
				if (text == null)
				{
					text = "UNKNOWN(" + res + ")";
				}
				text2 = text;
			}
			catch (MissingManifestResourceException)
			{
				text2 = "UNKNOWN(" + res + ")";
			}
			return text2;
		}

		/// <summary>Gets the description of the error condition for this exception.</summary>
		/// <returns>The string description of the error condition for this exception.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00090B48 File Offset: 0x0008ED48
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

		// Token: 0x04001530 RID: 5424
		private string res;

		// Token: 0x04001531 RID: 5425
		private string[] args;

		// Token: 0x04001532 RID: 5426
		private string message;
	}
}
