using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only way of generating streams or files containing XML data that conforms to the W3C Extensible Markup Language (XML) 1.0 and the Namespaces in XML recommendations. </summary>
	// Token: 0x02000178 RID: 376
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlTextWriter : XmlWriter
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0004E234 File Offset: 0x0004C434
		internal XmlTextWriter()
		{
			this.namespaces = true;
			this.formatting = Formatting.None;
			this.indentation = 2;
			this.indentChar = ' ';
			this.nsStack = new XmlTextWriter.Namespace[8];
			this.nsTop = -1;
			this.stack = new XmlTextWriter.TagInfo[10];
			this.top = 0;
			this.stack[this.top].Init(-1);
			this.quoteChar = '"';
			this.stateTable = XmlTextWriter.stateTableDefault;
			this.currentState = XmlTextWriter.State.Start;
			this.lastToken = XmlTextWriter.Token.Empty;
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified stream and encoding.</summary>
		/// <param name="w">The stream to which you want to write. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes out the stream as UTF-8 and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported or the stream cannot be written to. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="w" /> is null. </exception>
		// Token: 0x06000D92 RID: 3474 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
		public XmlTextWriter(Stream w, Encoding encoding)
			: this()
		{
			this.encoding = encoding;
			if (encoding != null)
			{
				this.textWriter = new StreamWriter(w, encoding);
			}
			else
			{
				this.textWriter = new StreamWriter(w);
			}
			this.xmlEncoder = new XmlTextEncoder(this.textWriter);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Xml.XmlTextWriter" /> class using the specified file.</summary>
		/// <param name="filename">The filename to write to. If the file exists, it truncates it and overwrites it with the new content. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes the file out as UTF-8, and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported; the filename is empty, contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentNullException">The filename is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to write to is not found. </exception>
		/// <exception cref="T:System.IO.IOException">The filename includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000D93 RID: 3475 RVA: 0x0004E32A File Offset: 0x0004C52A
		public XmlTextWriter(string filename, Encoding encoding)
			: this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), encoding)
		{
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="w">The TextWriter to write to. It is assumed that the TextWriter is already set to the correct encoding. </param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x0004E33C File Offset: 0x0004C53C
		public XmlTextWriter(TextWriter w)
			: this()
		{
			this.textWriter = w;
			this.encoding = w.Encoding;
			this.xmlEncoder = new XmlTextEncoder(w);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Gets the underlying stream object.</summary>
		/// <returns>The stream to which the XmlTextWriter is writing or null if the XmlTextWriter was constructed using a <see cref="T:System.IO.TextWriter" /> that does not inherit from the <see cref="T:System.IO.StreamWriter" /> class.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0004E374 File Offset: 0x0004C574
		public Stream BaseStream
		{
			get
			{
				StreamWriter streamWriter = this.textWriter as StreamWriter;
				if (streamWriter != null)
				{
					return streamWriter.BaseStream;
				}
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to support namespaces; otherwise, false.The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">You can only change this property when in the WriteState.Start state. </exception>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0004E398 File Offset: 0x0004C598
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0004E3A0 File Offset: 0x0004C5A0
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("NotInWriteState."));
				}
				this.namespaces = value;
			}
		}

		/// <summary>Indicates how the output is formatted.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Formatting" /> values. The default is Formatting.None (no special formatting).</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0004E3C9 File Offset: 0x0004C5C9
		public Formatting Formatting
		{
			get
			{
				return this.formatting;
			}
			set
			{
				this.formatting = value;
				this.indented = value == Formatting.Indented;
			}
		}

		/// <summary>Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to Formatting.Indented.</summary>
		/// <returns>Number of IndentChars for each level. The default is 2.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to a negative value. </exception>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0004E3E4 File Offset: 0x0004C5E4
		public int Indentation
		{
			get
			{
				return this.indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Indentation value must be greater than 0."));
				}
				this.indentation = value;
			}
		}

		/// <summary>Gets or sets which character to use for indenting when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to Formatting.Indented.</summary>
		/// <returns>The character to use for indenting. The default is space.NoteThe XmlTextWriter allows you to set this property to any character. To ensure valid XML, you must specify a valid white space character, 0x9, 0x10, 0x13 or 0x20.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0004E401 File Offset: 0x0004C601
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0004E409 File Offset: 0x0004C609
		public char IndentChar
		{
			get
			{
				return this.indentChar;
			}
			set
			{
				this.indentChar = value;
			}
		}

		/// <summary>Gets or sets which character to use to quote attribute values.</summary>
		/// <returns>The character to use to quote attribute values. This must be a single quote (&amp;#39;) or a double quote (&amp;#34;). The default is a double quote.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to something other than either a single or double quote. </exception>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0004E412 File Offset: 0x0004C612
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0004E41A File Offset: 0x0004C61A
		public char QuoteChar
		{
			get
			{
				return this.quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException(Res.GetString("Invalid XML attribute quote character. Valid attribute quote characters are ' and \"."));
				}
				this.quoteChar = value;
				this.xmlEncoder.QuoteChar = value;
			}
		}

		/// <summary>Writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0004E449 File Offset: 0x0004C649
		public override void WriteStartDocument()
		{
			this.StartDocument(-1);
		}

		/// <summary>Writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0004E452 File Offset: 0x0004C652
		public override void WriteStartDocument(bool standalone)
		{
			this.StartDocument(standalone ? 1 : 0);
		}

		/// <summary>Closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid. </exception>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0004E464 File Offset: 0x0004C664
		public override void WriteEndDocument()
		{
			try
			{
				this.AutoCompleteAll();
				if (this.currentState != XmlTextWriter.State.Epilog)
				{
					if (this.currentState == XmlTextWriter.State.Closed)
					{
						throw new ArgumentException(Res.GetString("The Writer is closed or in error state."));
					}
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				else
				{
					this.stateTable = XmlTextWriter.stateTableDefault;
					this.currentState = XmlTextWriter.State.Start;
					this.lastToken = XmlTextWriter.Token.Empty;
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty. </param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments. </param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument. </param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument. </param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or String.Empty-or- the value for <paramref name="name" /> would result in invalid XML. </exception>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0004E4E0 File Offset: 0x0004C6E0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Doctype);
				this.textWriter.Write("<!DOCTYPE ");
				this.textWriter.Write(name);
				if (pubid != null)
				{
					this.textWriter.Write(" PUBLIC " + this.quoteChar.ToString());
					this.textWriter.Write(pubid);
					this.textWriter.Write(this.quoteChar.ToString() + " " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				else if (sysid != null)
				{
					this.textWriter.Write(" SYSTEM " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				if (subset != null)
				{
					this.textWriter.Write("[");
					this.textWriter.Write(subset);
					this.textWriter.Write("]");
				}
				this.textWriter.Write('>');
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element. </param>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix then the writer automatically writes that prefix also. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0004E638 File Offset: 0x0004C838
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartElement);
				this.PushStack();
				this.textWriter.Write('<');
				if (this.namespaces)
				{
					this.stack[this.top].defaultNs = this.stack[this.top - 1].defaultNs;
					if (this.stack[this.top - 1].defaultNsState != XmlTextWriter.NamespaceState.Uninitialized)
					{
						this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.NotDeclaredButInScope;
					}
					this.stack[this.top].mixed = this.stack[this.top - 1].mixed;
					if (ns == null)
					{
						if (prefix != null && prefix.Length != 0 && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (prefix == null)
					{
						string text = this.FindPrefix(ns);
						if (text != null)
						{
							prefix = text;
						}
						else
						{
							this.PushNamespace(null, ns, false);
						}
					}
					else if (prefix.Length == 0)
					{
						this.PushNamespace(null, ns, false);
					}
					else
					{
						if (ns.Length == 0)
						{
							prefix = null;
						}
						this.VerifyPrefixXml(prefix, ns);
						this.PushNamespace(prefix, ns, false);
					}
					this.stack[this.top].prefix = null;
					if (prefix != null && prefix.Length != 0)
					{
						this.stack[this.top].prefix = prefix;
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.stack[this.top].name = localName;
				this.textWriter.Write(localName);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x0004E834 File Offset: 0x0004CA34
		public override void WriteEndElement()
		{
			this.InternalWriteEndElement(false);
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x0004E83D File Offset: 0x0004CA3D
		public override void WriteFullEndElement()
		{
			this.InternalWriteEndElement(true);
		}

		/// <summary>Writes the start of an attribute.</summary>
		/// <param name="prefix">Namespace prefix of the attribute. </param>
		/// <param name="localName">LocalName of the attribute. </param>
		/// <param name="ns">NamespaceURI of the attribute </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty. </exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0004E848 File Offset: 0x0004CA48
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartAttribute);
				this.specialAttr = XmlTextWriter.SpecialAttr.None;
				if (this.namespaces)
				{
					if (prefix != null && prefix.Length == 0)
					{
						prefix = null;
					}
					if (ns == "http://www.w3.org/2000/xmlns/" && prefix == null && localName != "xmlns")
					{
						prefix = "xmlns";
					}
					if (prefix == "xml")
					{
						if (localName == "lang")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
						}
						else if (localName == "space")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
						}
					}
					else if (prefix == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						if (localName == null || localName.Length == 0)
						{
							localName = prefix;
							prefix = null;
							this.prefixForXmlNs = null;
						}
						else
						{
							this.prefixForXmlNs = localName;
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
					}
					else if (prefix == null && localName == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
						this.prefixForXmlNs = null;
					}
					else if (ns == null)
					{
						if (prefix != null && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (ns.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						this.VerifyPrefixXml(prefix, ns);
						if (prefix != null && this.LookupNamespaceInCurrentScope(prefix) != -1)
						{
							prefix = null;
						}
						string text = this.FindPrefix(ns);
						if (text != null && (prefix == null || prefix == text))
						{
							prefix = text;
						}
						else
						{
							if (prefix == null)
							{
								prefix = this.GeneratePrefix();
							}
							this.PushNamespace(prefix, ns, false);
						}
					}
					if (prefix != null && prefix.Length != 0)
					{
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else
				{
					if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
					{
						throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
					}
					if (localName == "xml:lang")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
					}
					else if (localName == "xml:space")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
					}
				}
				this.xmlEncoder.StartAttribute(this.specialAttr > XmlTextWriter.SpecialAttr.None);
				this.textWriter.Write(localName);
				this.textWriter.Write('=');
				if (this.curQuoteChar != this.quoteChar)
				{
					this.curQuoteChar = this.quoteChar;
					this.xmlEncoder.QuoteChar = this.quoteChar;
				}
				this.textWriter.Write(this.curQuoteChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes the previous <see cref="M:System.Xml.XmlTextWriter.WriteStartAttribute(System.String,System.String,System.String)" /> call.</summary>
		// Token: 0x06000DA8 RID: 3496 RVA: 0x0004EAFC File Offset: 0x0004CCFC
		public override void WriteEndAttribute()
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.EndAttribute);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">Text to place inside the CDATA block. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x0004EB2C File Offset: 0x0004CD2C
		public override void WriteCData(string text)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.CData);
				if (text != null && text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have ']]>' inside an XML CDATA block."));
				}
				this.textWriter.Write("<![CDATA[");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("]]>");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DAA RID: 3498 RVA: 0x0004EBB0 File Offset: 0x0004CDB0
		public override void WriteComment(string text)
		{
			try
			{
				if (text != null && (text.IndexOf("--", StringComparison.Ordinal) >= 0 || (text.Length != 0 && text[text.Length - 1] == '-')))
				{
					throw new ArgumentException(Res.GetString("An XML comment cannot contain '--', and '-' cannot be the last character."));
				}
				this.AutoComplete(XmlTextWriter.Token.Comment);
				this.textWriter.Write("<!--");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("-->");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">Name of the processing instruction. </param>
		/// <param name="text">Text to include in the processing instruction. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlTextWriter.WriteStartDocument" /> has already been called. </exception>
		// Token: 0x06000DAB RID: 3499 RVA: 0x0004EC4C File Offset: 0x0004CE4C
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (text != null && text.IndexOf("?>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have '?>' inside an XML processing instruction."));
				}
				if (string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0 && this.stateTable == XmlTextWriter.stateTableDocument)
				{
					throw new ArgumentException(Res.GetString("Cannot write XML declaration. WriteStartDocument method has already written it."));
				}
				this.AutoComplete(XmlTextWriter.Token.PI);
				this.InternalWriteProcessingInstruction(name, text);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">Name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DAC RID: 3500 RVA: 0x0004ECD4 File Offset: 0x0004CED4
		public override void WriteEntityRef(string name)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteEntityRef(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">Unicode character for which to generate a character entity. </param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff; or the text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DAD RID: 3501 RVA: 0x0004ED1C File Offset: 0x0004CF1C
		public override void WriteCharEntity(char ch)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteCharEntity(ch);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters. </param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters. </exception>
		// Token: 0x06000DAE RID: 3502 RVA: 0x0004ED5C File Offset: 0x0004CF5C
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!this.xmlCharType.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AutoComplete(XmlTextWriter.Token.Whitespace);
				this.xmlEncoder.Write(ws);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="text">Text to write. </param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair. </exception>
		// Token: 0x06000DAF RID: 3503 RVA: 0x0004EDC4 File Offset: 0x0004CFC4
		public override void WriteString(string text)
		{
			try
			{
				if (text != null && text.Length != 0)
				{
					this.AutoComplete(XmlTextWriter.Token.Content);
					this.xmlEncoder.Write(text);
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF. </param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF. </param>
		/// <exception cref="T:System.Exception">An invalid surrogate character pair was passed. </exception>
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0004EE0C File Offset: 0x0004D00C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteSurrogateCharEntity(lowChar, highChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position in the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0004EE4C File Offset: 0x0004D04C
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.Write(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position within the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0004EE8C File Offset: 0x0004D08C
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRaw(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write. </param>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0004EECC File Offset: 0x0004D0CC
		public override void WriteRaw(string data)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRawWithSurrogateChecking(data);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position within the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0004EF0C File Offset: 0x0004D10C
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (!this.flush)
				{
					this.AutoComplete(XmlTextWriter.Token.Base64);
				}
				this.flush = true;
				if (this.base64Encoder == null)
				{
					this.base64Encoder = new XmlTextWriterBase64Encoder(this.xmlEncoder);
				}
				this.base64Encoder.Encode(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as binhex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0004EF74 File Offset: 0x0004D174
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				BinHexEncoder.Encode(buffer, index, count, this);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		public override WriteState WriteState
		{
			get
			{
				switch (this.currentState)
				{
				case XmlTextWriter.State.Start:
					return WriteState.Start;
				case XmlTextWriter.State.Prolog:
				case XmlTextWriter.State.PostDTD:
					return WriteState.Prolog;
				case XmlTextWriter.State.Element:
					return WriteState.Element;
				case XmlTextWriter.State.Attribute:
				case XmlTextWriter.State.AttrOnly:
					return WriteState.Attribute;
				case XmlTextWriter.State.Content:
				case XmlTextWriter.State.Epilog:
					return WriteState.Content;
				case XmlTextWriter.State.Error:
					return WriteState.Error;
				case XmlTextWriter.State.Closed:
					return WriteState.Closed;
				default:
					return WriteState.Error;
				}
			}
		}

		/// <summary>Closes this stream and the underlying stream.</summary>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0004F004 File Offset: 0x0004D204
		public override void Close()
		{
			try
			{
				this.AutoCompleteAll();
			}
			catch
			{
			}
			finally
			{
				this.currentState = XmlTextWriter.State.Closed;
				this.textWriter.Close();
			}
		}

		/// <summary>Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0004F04C File Offset: 0x0004D24C
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		/// <summary>Writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0004F05C File Offset: 0x0004D25C
		public override void WriteName(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.InternalWriteName(name, false);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write. </param>
		/// <param name="ns">The namespace URI to associate with the name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name according to the W3C Namespaces spec. </exception>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0004F098 File Offset: 0x0004D298
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (this.namespaces)
				{
					if (ns != null && ns.Length != 0 && ns != this.stack[this.top].defaultNs)
					{
						string text = this.FindPrefix(ns);
						if (text == null)
						{
							if (this.currentState != XmlTextWriter.State.Attribute)
							{
								throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
							}
							text = this.GeneratePrefix();
							this.PushNamespace(text, ns, false);
						}
						if (text.Length != 0)
						{
							this.InternalWriteName(text, true);
							this.textWriter.Write(':');
						}
					}
				}
				else if (ns != null && ns.Length != 0)
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.InternalWriteName(localName, true);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix. Or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">Namespace URI whose prefix you want to find. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty. </exception>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0004F184 File Offset: 0x0004D384
		public override string LookupPrefix(string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			string text = this.FindPrefix(ns);
			if (text == null && ns == this.stack[this.top].defaultNs)
			{
				text = string.Empty;
			}
			return text;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning None This is the default if no xml:space scope exists. Default The current scope is xml:space="default". Preserve The current scope is xml:space="preserve". </returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		public override XmlSpace XmlSpace
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					XmlSpace xmlSpace = this.stack[i].xmlSpace;
					if (xmlSpace != XmlSpace.None)
					{
						return xmlSpace;
					}
				}
				return XmlSpace.None;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang or null if there is no xml:lang in the current scope.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0004F214 File Offset: 0x0004D414
		public override string XmlLang
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					string xmlLang = this.stack[i].xmlLang;
					if (xmlLang != null)
					{
						return xmlLang;
					}
				}
				return null;
			}
		}

		/// <summary>Writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0004F24C File Offset: 0x0004D44C
		public override void WriteNmToken(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				if (!ValidateNames.IsNmtokenNoNamespaces(name))
				{
					throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }));
				}
				this.textWriter.Write(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0004F2C8 File Offset: 0x0004D4C8
		private void StartDocument(int standalone)
		{
			try
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument needs to be the first call."));
				}
				this.stateTable = XmlTextWriter.stateTableDocument;
				this.currentState = XmlTextWriter.State.Prolog;
				StringBuilder stringBuilder = new StringBuilder(128);
				stringBuilder.Append("version=" + this.quoteChar.ToString() + "1.0" + this.quoteChar.ToString());
				if (this.encoding != null)
				{
					stringBuilder.Append(" encoding=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append(this.encoding.WebName);
					stringBuilder.Append(this.quoteChar);
				}
				if (standalone >= 0)
				{
					stringBuilder.Append(" standalone=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append((standalone == 0) ? "no" : "yes");
					stringBuilder.Append(this.quoteChar);
				}
				this.InternalWriteProcessingInstruction("xml", stringBuilder.ToString());
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0004F3E4 File Offset: 0x0004D5E4
		private void AutoComplete(XmlTextWriter.Token token)
		{
			if (this.currentState == XmlTextWriter.State.Closed)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed."));
			}
			if (this.currentState == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[8]
				}));
			}
			XmlTextWriter.State state = this.stateTable[(int)(token * XmlTextWriter.Token.EndAttribute + (int)this.currentState)];
			if (state == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[(int)this.currentState]
				}));
			}
			switch (token)
			{
			case XmlTextWriter.Token.PI:
			case XmlTextWriter.Token.Comment:
			case XmlTextWriter.Token.CData:
			case XmlTextWriter.Token.StartElement:
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.WriteEndStartTag(false);
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.WriteEndStartTag(false);
				}
				if (token == XmlTextWriter.Token.CData)
				{
					this.stack[this.top].mixed = true;
				}
				else if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.Doctype:
				if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.EndElement:
			case XmlTextWriter.Token.LongEndElement:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
				}
				if (this.currentState == XmlTextWriter.State.Content)
				{
					token = XmlTextWriter.Token.LongEndElement;
				}
				else
				{
					this.WriteEndStartTag(token == XmlTextWriter.Token.EndElement);
				}
				if (XmlTextWriter.stateTableDocument == this.stateTable && this.top == 1)
				{
					state = XmlTextWriter.State.Epilog;
				}
				break;
			case XmlTextWriter.Token.StartAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.textWriter.Write(' ');
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.textWriter.Write(' ');
				}
				break;
			case XmlTextWriter.Token.EndAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				this.WriteEndAttributeQuote();
				break;
			case XmlTextWriter.Token.Content:
			case XmlTextWriter.Token.Base64:
			case XmlTextWriter.Token.RawData:
			case XmlTextWriter.Token.Whitespace:
				if (token != XmlTextWriter.Token.Base64 && this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Element && this.lastToken != XmlTextWriter.Token.Content)
				{
					this.WriteEndStartTag(false);
				}
				if (state == XmlTextWriter.State.Content)
				{
					this.stack[this.top].mixed = true;
				}
				break;
			default:
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			this.currentState = state;
			this.lastToken = token;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0004F66B File Offset: 0x0004D86B
		private void AutoCompleteAll()
		{
			if (this.flush)
			{
				this.FlushEncoders();
			}
			while (this.top > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0004F68C File Offset: 0x0004D88C
		private void InternalWriteEndElement(bool longFormat)
		{
			try
			{
				if (this.top <= 0)
				{
					throw new InvalidOperationException(Res.GetString("There was no XML start tag open."));
				}
				this.AutoComplete(longFormat ? XmlTextWriter.Token.LongEndElement : XmlTextWriter.Token.EndElement);
				if (this.lastToken == XmlTextWriter.Token.LongEndElement)
				{
					if (this.indented)
					{
						this.Indent(true);
					}
					this.textWriter.Write('<');
					this.textWriter.Write('/');
					if (this.namespaces && this.stack[this.top].prefix != null)
					{
						this.textWriter.Write(this.stack[this.top].prefix);
						this.textWriter.Write(':');
					}
					this.textWriter.Write(this.stack[this.top].name);
					this.textWriter.Write('>');
				}
				int prevNsTop = this.stack[this.top].prevNsTop;
				if (this.useNsHashtable && prevNsTop < this.nsTop)
				{
					this.PopNamespaces(prevNsTop + 1, this.nsTop);
				}
				this.nsTop = prevNsTop;
				this.top--;
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0004F7E4 File Offset: 0x0004D9E4
		private void WriteEndStartTag(bool empty)
		{
			this.xmlEncoder.StartAttribute(false);
			for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
			{
				if (!this.nsStack[i].declared)
				{
					this.textWriter.Write(" xmlns");
					this.textWriter.Write(':');
					this.textWriter.Write(this.nsStack[i].prefix);
					this.textWriter.Write('=');
					this.textWriter.Write(this.quoteChar);
					this.xmlEncoder.Write(this.nsStack[i].ns);
					this.textWriter.Write(this.quoteChar);
				}
			}
			if (this.stack[this.top].defaultNs != this.stack[this.top - 1].defaultNs && this.stack[this.top].defaultNsState == XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
			{
				this.textWriter.Write(" xmlns");
				this.textWriter.Write('=');
				this.textWriter.Write(this.quoteChar);
				this.xmlEncoder.Write(this.stack[this.top].defaultNs);
				this.textWriter.Write(this.quoteChar);
				this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.DeclaredAndWrittenOut;
			}
			this.xmlEncoder.EndAttribute();
			if (empty)
			{
				this.textWriter.Write(" /");
			}
			this.textWriter.Write('>');
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0004F9B6 File Offset: 0x0004DBB6
		private void WriteEndAttributeQuote()
		{
			if (this.specialAttr != XmlTextWriter.SpecialAttr.None)
			{
				this.HandleSpecialAttribute();
			}
			this.xmlEncoder.EndAttribute();
			this.textWriter.Write(this.curQuoteChar);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
		private void Indent(bool beforeEndElement)
		{
			if (this.top == 0)
			{
				this.textWriter.WriteLine();
				return;
			}
			if (!this.stack[this.top].mixed)
			{
				this.textWriter.WriteLine();
				int i = (beforeEndElement ? (this.top - 1) : this.top);
				for (i *= this.indentation; i > 0; i--)
				{
					this.textWriter.Write(this.indentChar);
				}
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0004FA60 File Offset: 0x0004DC60
		private void PushNamespace(string prefix, string ns, bool declared)
		{
			if ("http://www.w3.org/2000/xmlns/" == ns)
			{
				throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
			}
			if (prefix == null)
			{
				XmlTextWriter.NamespaceState defaultNsState = this.stack[this.top].defaultNsState;
				if (defaultNsState > XmlTextWriter.NamespaceState.NotDeclaredButInScope)
				{
					if (defaultNsState != XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
					{
						return;
					}
				}
				else
				{
					this.stack[this.top].defaultNs = ns;
				}
				this.stack[this.top].defaultNsState = (declared ? XmlTextWriter.NamespaceState.DeclaredAndWrittenOut : XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut);
				return;
			}
			if (prefix.Length != 0 && ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
			}
			int num = this.LookupNamespace(prefix);
			if (num != -1 && this.nsStack[num].ns == ns)
			{
				if (declared)
				{
					this.nsStack[num].declared = true;
					return;
				}
			}
			else
			{
				if (declared && num != -1 && num > this.stack[this.top].prevNsTop)
				{
					this.nsStack[num].declared = true;
				}
				this.AddNamespace(prefix, ns, declared);
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0004FB78 File Offset: 0x0004DD78
		private void AddNamespace(string prefix, string ns, bool declared)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlTextWriter.Namespace[] array = new XmlTextWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, array, num2);
				this.nsStack = array;
			}
			this.nsStack[num2].Set(prefix, ns, declared);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(num2);
				return;
			}
			if (num2 == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(new SecureStringHasher());
				for (int i = 0; i <= num2; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0004FC14 File Offset: 0x0004DE14
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int num;
			if (this.nsHashtable.TryGetValue(prefix, out num))
			{
				this.nsStack[namespaceIndex].prevNsIndex = num;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0004FC64 File Offset: 0x0004DE64
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		private string GeneratePrefix()
		{
			XmlTextWriter.TagInfo[] array = this.stack;
			int num = this.top;
			int prefixCount = array[num].prefixCount;
			array[num].prefixCount = prefixCount + 1;
			int num2 = prefixCount + 1;
			return "d" + this.top.ToString("d", CultureInfo.InvariantCulture) + "p" + num2.ToString("d", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0004FD44 File Offset: 0x0004DF44
		private void InternalWriteProcessingInstruction(string name, string text)
		{
			this.textWriter.Write("<?");
			this.ValidateName(name, false);
			this.textWriter.Write(name);
			this.textWriter.Write(' ');
			if (text != null)
			{
				this.xmlEncoder.WriteRawWithSurrogateChecking(text);
			}
			this.textWriter.Write("?>");
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
		private int LookupNamespace(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num))
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		private int LookupNamespaceInCurrentScope(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num) && num > this.stack[this.top].prevNsTop)
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0004FE7C File Offset: 0x0004E07C
		private string FindPrefix(string ns)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].ns == ns && this.LookupNamespace(this.nsStack[i].prefix) == i)
				{
					return this.nsStack[i].prefix;
				}
			}
			return null;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0004FEE0 File Offset: 0x0004E0E0
		private void InternalWriteName(string name, bool isNCName)
		{
			this.ValidateName(name, isNCName);
			this.textWriter.Write(name);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0004FEF8 File Offset: 0x0004E0F8
		private void ValidateName(string name, bool isNCName)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			int length = name.Length;
			if (this.namespaces)
			{
				int num = -1;
				for (int num2 = ValidateNames.ParseNCName(name); num2 != length; num2 += ValidateNames.ParseNmtoken(name, num2))
				{
					if (name[num2] != ':' || isNCName || num != -1 || num2 <= 0 || num2 + 1 >= length)
					{
						goto IL_006F;
					}
					num = num2;
					num2++;
				}
				return;
			}
			if (ValidateNames.IsNameNoNamespaces(name))
			{
				return;
			}
			IL_006F:
			throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }));
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0004FF90 File Offset: 0x0004E190
		private void HandleSpecialAttribute()
		{
			string text = this.xmlEncoder.AttributeValue;
			switch (this.specialAttr)
			{
			case XmlTextWriter.SpecialAttr.XmlSpace:
				text = XmlConvert.TrimString(text);
				if (text == "default")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Default;
					return;
				}
				if (text == "preserve")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Preserve;
					return;
				}
				throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { text }));
			case XmlTextWriter.SpecialAttr.XmlLang:
				this.stack[this.top].xmlLang = text;
				return;
			case XmlTextWriter.SpecialAttr.XmlNs:
				this.VerifyPrefixXml(this.prefixForXmlNs, text);
				this.PushNamespace(this.prefixForXmlNs, text, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00050064 File Offset: 0x0004E264
		private void VerifyPrefixXml(string prefix, string ns)
		{
			if (prefix != null && prefix.Length == 3 && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L') && "http://www.w3.org/XML/1998/namespace" != ns)
			{
				throw new ArgumentException(Res.GetString("Prefixes beginning with \"xml\" (regardless of whether the characters are uppercase, lowercase, or some combination thereof) are reserved for use by XML."));
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000500DC File Offset: 0x0004E2DC
		private void PushStack()
		{
			if (this.top == this.stack.Length - 1)
			{
				XmlTextWriter.TagInfo[] array = new XmlTextWriter.TagInfo[this.stack.Length + 10];
				if (this.top > 0)
				{
					Array.Copy(this.stack, array, this.top + 1);
				}
				this.stack = array;
			}
			this.top++;
			this.stack[this.top].Init(this.nsTop);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0005015A File Offset: 0x0004E35A
		private void FlushEncoders()
		{
			if (this.base64Encoder != null)
			{
				this.base64Encoder.Flush();
			}
			this.flush = false;
		}

		// Token: 0x0400091D RID: 2333
		private TextWriter textWriter;

		// Token: 0x0400091E RID: 2334
		private XmlTextEncoder xmlEncoder;

		// Token: 0x0400091F RID: 2335
		private Encoding encoding;

		// Token: 0x04000920 RID: 2336
		private Formatting formatting;

		// Token: 0x04000921 RID: 2337
		private bool indented;

		// Token: 0x04000922 RID: 2338
		private int indentation;

		// Token: 0x04000923 RID: 2339
		private char indentChar;

		// Token: 0x04000924 RID: 2340
		private XmlTextWriter.TagInfo[] stack;

		// Token: 0x04000925 RID: 2341
		private int top;

		// Token: 0x04000926 RID: 2342
		private XmlTextWriter.State[] stateTable;

		// Token: 0x04000927 RID: 2343
		private XmlTextWriter.State currentState;

		// Token: 0x04000928 RID: 2344
		private XmlTextWriter.Token lastToken;

		// Token: 0x04000929 RID: 2345
		private XmlTextWriterBase64Encoder base64Encoder;

		// Token: 0x0400092A RID: 2346
		private char quoteChar;

		// Token: 0x0400092B RID: 2347
		private char curQuoteChar;

		// Token: 0x0400092C RID: 2348
		private bool namespaces;

		// Token: 0x0400092D RID: 2349
		private XmlTextWriter.SpecialAttr specialAttr;

		// Token: 0x0400092E RID: 2350
		private string prefixForXmlNs;

		// Token: 0x0400092F RID: 2351
		private bool flush;

		// Token: 0x04000930 RID: 2352
		private XmlTextWriter.Namespace[] nsStack;

		// Token: 0x04000931 RID: 2353
		private int nsTop;

		// Token: 0x04000932 RID: 2354
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000933 RID: 2355
		private bool useNsHashtable;

		// Token: 0x04000934 RID: 2356
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000935 RID: 2357
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x04000936 RID: 2358
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000937 RID: 2359
		private static string[] stateName = new string[] { "Start", "Prolog", "PostDTD", "Element", "Attribute", "Content", "AttrOnly", "Epilog", "Error", "Closed" };

		// Token: 0x04000938 RID: 2360
		private static string[] tokenName = new string[]
		{
			"PI", "Doctype", "Comment", "CData", "StartElement", "EndElement", "LongEndElement", "StartAttribute", "EndAttribute", "Content",
			"Base64", "RawData", "Whitespace", "Empty"
		};

		// Token: 0x04000939 RID: 2361
		private static readonly XmlTextWriter.State[] stateTableDefault = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.AttrOnly,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x0400093A RID: 2362
		private static readonly XmlTextWriter.State[] stateTableDocument = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x02000179 RID: 377
		private enum NamespaceState
		{
			// Token: 0x0400093C RID: 2364
			Uninitialized,
			// Token: 0x0400093D RID: 2365
			NotDeclaredButInScope,
			// Token: 0x0400093E RID: 2366
			DeclaredButNotWrittenOut,
			// Token: 0x0400093F RID: 2367
			DeclaredAndWrittenOut
		}

		// Token: 0x0200017A RID: 378
		private struct TagInfo
		{
			// Token: 0x06000DD6 RID: 3542 RVA: 0x00050291 File Offset: 0x0004E491
			internal void Init(int nsTop)
			{
				this.name = null;
				this.defaultNs = string.Empty;
				this.defaultNsState = XmlTextWriter.NamespaceState.Uninitialized;
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = null;
				this.prevNsTop = nsTop;
				this.prefixCount = 0;
				this.mixed = false;
			}

			// Token: 0x04000940 RID: 2368
			internal string name;

			// Token: 0x04000941 RID: 2369
			internal string prefix;

			// Token: 0x04000942 RID: 2370
			internal string defaultNs;

			// Token: 0x04000943 RID: 2371
			internal XmlTextWriter.NamespaceState defaultNsState;

			// Token: 0x04000944 RID: 2372
			internal XmlSpace xmlSpace;

			// Token: 0x04000945 RID: 2373
			internal string xmlLang;

			// Token: 0x04000946 RID: 2374
			internal int prevNsTop;

			// Token: 0x04000947 RID: 2375
			internal int prefixCount;

			// Token: 0x04000948 RID: 2376
			internal bool mixed;
		}

		// Token: 0x0200017B RID: 379
		private struct Namespace
		{
			// Token: 0x06000DD7 RID: 3543 RVA: 0x000502CF File Offset: 0x0004E4CF
			internal void Set(string prefix, string ns, bool declared)
			{
				this.prefix = prefix;
				this.ns = ns;
				this.declared = declared;
				this.prevNsIndex = -1;
			}

			// Token: 0x04000949 RID: 2377
			internal string prefix;

			// Token: 0x0400094A RID: 2378
			internal string ns;

			// Token: 0x0400094B RID: 2379
			internal bool declared;

			// Token: 0x0400094C RID: 2380
			internal int prevNsIndex;
		}

		// Token: 0x0200017C RID: 380
		private enum SpecialAttr
		{
			// Token: 0x0400094E RID: 2382
			None,
			// Token: 0x0400094F RID: 2383
			XmlSpace,
			// Token: 0x04000950 RID: 2384
			XmlLang,
			// Token: 0x04000951 RID: 2385
			XmlNs
		}

		// Token: 0x0200017D RID: 381
		private enum State
		{
			// Token: 0x04000953 RID: 2387
			Start,
			// Token: 0x04000954 RID: 2388
			Prolog,
			// Token: 0x04000955 RID: 2389
			PostDTD,
			// Token: 0x04000956 RID: 2390
			Element,
			// Token: 0x04000957 RID: 2391
			Attribute,
			// Token: 0x04000958 RID: 2392
			Content,
			// Token: 0x04000959 RID: 2393
			AttrOnly,
			// Token: 0x0400095A RID: 2394
			Epilog,
			// Token: 0x0400095B RID: 2395
			Error,
			// Token: 0x0400095C RID: 2396
			Closed
		}

		// Token: 0x0200017E RID: 382
		private enum Token
		{
			// Token: 0x0400095E RID: 2398
			PI,
			// Token: 0x0400095F RID: 2399
			Doctype,
			// Token: 0x04000960 RID: 2400
			Comment,
			// Token: 0x04000961 RID: 2401
			CData,
			// Token: 0x04000962 RID: 2402
			StartElement,
			// Token: 0x04000963 RID: 2403
			EndElement,
			// Token: 0x04000964 RID: 2404
			LongEndElement,
			// Token: 0x04000965 RID: 2405
			StartAttribute,
			// Token: 0x04000966 RID: 2406
			EndAttribute,
			// Token: 0x04000967 RID: 2407
			Content,
			// Token: 0x04000968 RID: 2408
			Base64,
			// Token: 0x04000969 RID: 2409
			RawData,
			// Token: 0x0400096A RID: 2410
			Whitespace,
			// Token: 0x0400096B RID: 2411
			Empty
		}
	}
}
