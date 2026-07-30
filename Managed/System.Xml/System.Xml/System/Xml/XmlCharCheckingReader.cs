using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000C9 RID: 201
	internal class XmlCharCheckingReader : XmlWrappingReader
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
		internal XmlCharCheckingReader(XmlReader reader, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing)
			: base(reader)
		{
			this.state = XmlCharCheckingReader.State.Initial;
			this.checkCharacters = checkCharacters;
			this.ignoreWhitespace = ignoreWhitespace;
			this.ignoreComments = ignoreComments;
			this.ignorePis = ignorePis;
			this.dtdProcessing = dtdProcessing;
			this.lastNodeType = XmlNodeType.None;
			if (checkCharacters)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001CE28 File Offset: 0x0001B028
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.reader.Settings;
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				else
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (this.checkCharacters)
				{
					xmlReaderSettings.CheckCharacters = true;
				}
				if (this.ignoreWhitespace)
				{
					xmlReaderSettings.IgnoreWhitespace = true;
				}
				if (this.ignoreComments)
				{
					xmlReaderSettings.IgnoreComments = true;
				}
				if (this.ignorePis)
				{
					xmlReaderSettings.IgnoreProcessingInstructions = true;
				}
				if (this.dtdProcessing != (DtdProcessing)(-1))
				{
					xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		public override bool MoveToAttribute(string name)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001CEE7 File Offset: 0x0001B0E7
		public override void MoveToAttribute(int i)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001CF04 File Offset: 0x0001B104
		public override bool MoveToFirstAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001CF20 File Offset: 0x0001B120
		public override bool MoveToNextAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public override bool MoveToElement()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001CF58 File Offset: 0x0001B158
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlCharCheckingReader.State.Initial:
				this.state = XmlCharCheckingReader.State.Interactive;
				if (this.reader.ReadState != ReadState.Initial)
				{
					goto IL_0055;
				}
				break;
			case XmlCharCheckingReader.State.InReadBinary:
				this.FinishReadBinary();
				this.state = XmlCharCheckingReader.State.Interactive;
				break;
			case XmlCharCheckingReader.State.Error:
				return false;
			case XmlCharCheckingReader.State.Interactive:
				break;
			default:
				return false;
			}
			if (!this.reader.Read())
			{
				return false;
			}
			IL_0055:
			XmlNodeType nodeType = this.reader.NodeType;
			if (!this.checkCharacters)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					if (this.ignorePis)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Comment:
					if (this.ignoreComments)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.DocumentType:
					if (this.dtdProcessing == DtdProcessing.Prohibit)
					{
						this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
					}
					else if (this.dtdProcessing == DtdProcessing.Ignore)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Whitespace:
					if (this.ignoreWhitespace)
					{
						return this.Read();
					}
					break;
				}
				return true;
			}
			switch (nodeType)
			{
			case XmlNodeType.Element:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
					if (this.reader.MoveToFirstAttribute())
					{
						do
						{
							this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
							this.CheckCharacters(this.reader.Value);
						}
						while (this.reader.MoveToNextAttribute());
						this.reader.MoveToElement();
					}
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.EntityReference:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
				}
				break;
			case XmlNodeType.ProcessingInstruction:
				if (this.ignorePis)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.Comment:
				if (this.ignoreComments)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.DocumentType:
				if (this.dtdProcessing == DtdProcessing.Prohibit)
				{
					this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
				}
				else if (this.dtdProcessing == DtdProcessing.Ignore)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
					string text = this.reader.GetAttribute("SYSTEM");
					if (text != null)
					{
						this.CheckCharacters(text);
					}
					text = this.reader.GetAttribute("PUBLIC");
					int num;
					if (text != null && (num = this.xmlCharType.IsPublicId(text)) >= 0)
					{
						this.Throw("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(text, num));
					}
				}
				break;
			case XmlNodeType.Whitespace:
				if (this.ignoreWhitespace)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.SignificantWhitespace:
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.EndElement:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
				}
				break;
			}
			this.lastNodeType = nodeType;
			return true;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XmlCharCheckingReader.State.Initial:
					if (this.reader.ReadState != ReadState.Closed)
					{
						return ReadState.Initial;
					}
					return ReadState.Closed;
				case XmlCharCheckingReader.State.Error:
					return ReadState.Error;
				}
				return this.reader.ReadState;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001D337 File Offset: 0x0001B537
		public override bool ReadAttributeValue()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001D354 File Offset: 0x0001B554
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001D46C File Offset: 0x0001B66C
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001D538 File Offset: 0x0001B738
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001D602 File Offset: 0x0001B802
		private void Throw(string res, string arg)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, arg, null);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001D613 File Offset: 0x0001B813
		private void Throw(string res, string[] args)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, args, null);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001D624 File Offset: 0x0001B824
		private void CheckWhitespace(string value)
		{
			int num;
			if ((num = this.xmlCharType.IsOnlyWhitespaceWithPos(value)) != -1)
			{
				this.Throw("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(value, num));
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001D654 File Offset: 0x0001B854
		private void ValidateQName(string name)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(name, out text, out text2);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001D66C File Offset: 0x0001B86C
		private void ValidateQName(string prefix, string localName)
		{
			try
			{
				if (prefix.Length > 0)
				{
					ValidateNames.ParseNCNameThrow(prefix);
				}
				ValidateNames.ParseNCNameThrow(localName);
			}
			catch
			{
				this.state = XmlCharCheckingReader.State.Error;
				throw;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		private void CheckCharacters(string value)
		{
			XmlConvert.VerifyCharData(value, ExceptionType.ArgumentException, ExceptionType.XmlException);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001D6B6 File Offset: 0x0001B8B6
		private void FinishReadBinary()
		{
			this.state = XmlCharCheckingReader.State.Interactive;
			if (this.readBinaryHelper != null)
			{
				this.readBinaryHelper.Finish();
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public override async Task<bool> ReadAsync()
		{
			switch (this.state)
			{
			case XmlCharCheckingReader.State.Initial:
				this.state = XmlCharCheckingReader.State.Interactive;
				if (this.reader.ReadState != ReadState.Initial)
				{
					goto IL_0176;
				}
				break;
			case XmlCharCheckingReader.State.InReadBinary:
				await this.FinishReadBinaryAsync().ConfigureAwait(false);
				this.state = XmlCharCheckingReader.State.Interactive;
				break;
			case XmlCharCheckingReader.State.Error:
				return false;
			case XmlCharCheckingReader.State.Interactive:
				break;
			default:
				return false;
			}
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				return false;
			}
			IL_0176:
			XmlNodeType nodeType = this.reader.NodeType;
			bool flag;
			if (!this.checkCharacters)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					if (this.ignorePis)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					break;
				case XmlNodeType.Comment:
					if (this.ignoreComments)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					break;
				case XmlNodeType.DocumentType:
					if (this.dtdProcessing == DtdProcessing.Prohibit)
					{
						this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
					}
					else if (this.dtdProcessing == DtdProcessing.Ignore)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					break;
				case XmlNodeType.Whitespace:
					if (this.ignoreWhitespace)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					break;
				}
				flag = true;
			}
			else
			{
				switch (nodeType)
				{
				case XmlNodeType.Element:
					if (this.checkCharacters)
					{
						this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
						if (this.reader.MoveToFirstAttribute())
						{
							do
							{
								this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
								this.CheckCharacters(this.reader.Value);
							}
							while (this.reader.MoveToNextAttribute());
							this.reader.MoveToElement();
						}
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					if (this.checkCharacters)
					{
						this.CheckCharacters(await this.reader.GetValueAsync().ConfigureAwait(false));
					}
					break;
				case XmlNodeType.EntityReference:
					if (this.checkCharacters)
					{
						this.ValidateQName(this.reader.Name);
					}
					break;
				case XmlNodeType.ProcessingInstruction:
					if (this.ignorePis)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					if (this.checkCharacters)
					{
						this.ValidateQName(this.reader.Name);
						this.CheckCharacters(this.reader.Value);
					}
					break;
				case XmlNodeType.Comment:
					if (this.ignoreComments)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					if (this.checkCharacters)
					{
						this.CheckCharacters(this.reader.Value);
					}
					break;
				case XmlNodeType.DocumentType:
					if (this.dtdProcessing == DtdProcessing.Prohibit)
					{
						this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
					}
					else if (this.dtdProcessing == DtdProcessing.Ignore)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					if (this.checkCharacters)
					{
						this.ValidateQName(this.reader.Name);
						this.CheckCharacters(this.reader.Value);
						string text = this.reader.GetAttribute("SYSTEM");
						if (text != null)
						{
							this.CheckCharacters(text);
						}
						text = this.reader.GetAttribute("PUBLIC");
						if (text != null)
						{
							int num = this.xmlCharType.IsPublicId(text);
							if (num >= 0)
							{
								this.Throw("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(text, num));
							}
						}
					}
					break;
				case XmlNodeType.Whitespace:
					if (this.ignoreWhitespace)
					{
						return await this.ReadAsync().ConfigureAwait(false);
					}
					if (this.checkCharacters)
					{
						this.CheckWhitespace(await this.reader.GetValueAsync().ConfigureAwait(false));
					}
					break;
				case XmlNodeType.SignificantWhitespace:
					if (this.checkCharacters)
					{
						this.CheckWhitespace(await this.reader.GetValueAsync().ConfigureAwait(false));
					}
					break;
				case XmlNodeType.EndElement:
					if (this.checkCharacters)
					{
						this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
					}
					break;
				}
				this.lastNodeType = nodeType;
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001D71C File Offset: 0x0001B91C
		public override async Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.state != XmlCharCheckingReader.State.InReadBinary)
				{
					if (base.CanReadBinaryContent && !this.checkCharacters)
					{
						this.readBinaryHelper = null;
						this.state = XmlCharCheckingReader.State.InReadBinary;
						return await base.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
					}
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				}
				else if (this.readBinaryHelper == null)
				{
					return await base.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				}
				this.state = XmlCharCheckingReader.State.Interactive;
				int num2 = await this.readBinaryHelper.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.state = XmlCharCheckingReader.State.InReadBinary;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001D77C File Offset: 0x0001B97C
		public override async Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.state != XmlCharCheckingReader.State.InReadBinary)
				{
					if (base.CanReadBinaryContent && !this.checkCharacters)
					{
						this.readBinaryHelper = null;
						this.state = XmlCharCheckingReader.State.InReadBinary;
						return await base.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
					}
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				}
				else if (this.readBinaryHelper == null)
				{
					return await base.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				}
				this.state = XmlCharCheckingReader.State.Interactive;
				int num2 = await this.readBinaryHelper.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.state = XmlCharCheckingReader.State.InReadBinary;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public override async Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.state != XmlCharCheckingReader.State.InReadBinary)
				{
					if (base.CanReadBinaryContent && !this.checkCharacters)
					{
						this.readBinaryHelper = null;
						this.state = XmlCharCheckingReader.State.InReadBinary;
						return await base.ReadElementContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
					}
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				}
				else if (this.readBinaryHelper == null)
				{
					return await base.ReadElementContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				}
				this.state = XmlCharCheckingReader.State.Interactive;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.state = XmlCharCheckingReader.State.InReadBinary;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001D83C File Offset: 0x0001BA3C
		public override async Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.state != XmlCharCheckingReader.State.InReadBinary)
				{
					if (base.CanReadBinaryContent && !this.checkCharacters)
					{
						this.readBinaryHelper = null;
						this.state = XmlCharCheckingReader.State.InReadBinary;
						return await base.ReadElementContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
					}
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				}
				else if (this.readBinaryHelper == null)
				{
					return await base.ReadElementContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				}
				this.state = XmlCharCheckingReader.State.Interactive;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.state = XmlCharCheckingReader.State.InReadBinary;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001D89C File Offset: 0x0001BA9C
		private async Task FinishReadBinaryAsync()
		{
			this.state = XmlCharCheckingReader.State.Interactive;
			if (this.readBinaryHelper != null)
			{
				await this.readBinaryHelper.FinishAsync().ConfigureAwait(false);
			}
		}

		// Token: 0x040003E9 RID: 1001
		private XmlCharCheckingReader.State state;

		// Token: 0x040003EA RID: 1002
		private bool checkCharacters;

		// Token: 0x040003EB RID: 1003
		private bool ignoreWhitespace;

		// Token: 0x040003EC RID: 1004
		private bool ignoreComments;

		// Token: 0x040003ED RID: 1005
		private bool ignorePis;

		// Token: 0x040003EE RID: 1006
		private DtdProcessing dtdProcessing;

		// Token: 0x040003EF RID: 1007
		private XmlNodeType lastNodeType;

		// Token: 0x040003F0 RID: 1008
		private XmlCharType xmlCharType;

		// Token: 0x040003F1 RID: 1009
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x020000CA RID: 202
		private enum State
		{
			// Token: 0x040003F3 RID: 1011
			Initial,
			// Token: 0x040003F4 RID: 1012
			InReadBinary,
			// Token: 0x040003F5 RID: 1013
			Error,
			// Token: 0x040003F6 RID: 1014
			Interactive
		}
	}
}
