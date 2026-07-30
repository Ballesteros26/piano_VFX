using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only means of generating streams or files containing XML data.</summary>
	// Token: 0x020001DA RID: 474
	public abstract class XmlWriter : IDisposable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create" /> method, this property returns null.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010AC RID: 4268
		public abstract void WriteStartDocument();

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010AD RID: 4269
		public abstract void WriteStartDocument(bool standalone);

		/// <summary>When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010AE RID: 4270
		public abstract void WriteEndDocument();

		/// <summary>When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="name" /> would result in invalid XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010AF RID: 4271
		public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix, the writer automatically writes that prefix also.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B0 RID: 4272 RVA: 0x000633A2 File Offset: 0x000615A2
		public void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B1 RID: 4273
		public abstract void WriteStartElement(string prefix, string localName, string ns);

		/// <summary>When overridden in a derived class, writes out a start tag with the specified local name.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B2 RID: 4274 RVA: 0x000633AD File Offset: 0x000615AD
		public void WriteStartElement(string localName)
		{
			this.WriteStartElement(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">This results in an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B3 RID: 4275
		public abstract void WriteEndElement();

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B4 RID: 4276
		public abstract void WriteFullEndElement();

		/// <summary>When overridden in a derived class, writes an attribute with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI to associate with the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B5 RID: 4277 RVA: 0x000633B8 File Offset: 0x000615B8
		public void WriteAttributeString(string localName, string ns, string value)
		{
			this.WriteStartAttribute(null, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified local name and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B6 RID: 4278 RVA: 0x000633D0 File Offset: 0x000615D0
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteStartAttribute(null, localName, null);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.Xml.XmlException">The <paramref name="localName" /> or <paramref name="ns" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B7 RID: 4279 RVA: 0x000633E8 File Offset: 0x000615E8
		public void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>Writes the start of an attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00063401 File Offset: 0x00061601
		public void WriteStartAttribute(string localName, string ns)
		{
			this.WriteStartAttribute(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010B9 RID: 4281
		public abstract void WriteStartAttribute(string prefix, string localName, string ns);

		/// <summary>Writes the start of an attribute with the specified local name.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BA RID: 4282 RVA: 0x0006340C File Offset: 0x0006160C
		public void WriteStartAttribute(string localName)
		{
			this.WriteStartAttribute(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BB RID: 4283
		public abstract void WriteEndAttribute();

		/// <summary>When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BC RID: 4284
		public abstract void WriteCData(string text);

		/// <summary>When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well-formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BD RID: 4285
		public abstract void WriteComment(string text);

		/// <summary>When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument" /> has already been called. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BE RID: 4286
		public abstract void WriteProcessingInstruction(string name, string text);

		/// <summary>When overridden in a derived class, writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010BF RID: 4287
		public abstract void WriteEntityRef(string name);

		/// <summary>When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C0 RID: 4288
		public abstract void WriteCharEntity(char ch);

		/// <summary>When overridden in a derived class, writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C1 RID: 4289
		public abstract void WriteWhitespace(string ws);

		/// <summary>When overridden in a derived class, writes the given text content.</summary>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C2 RID: 4290
		public abstract void WriteString(string text);

		/// <summary>When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.ArgumentException">An invalid surrogate character pair was passed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C3 RID: 4291
		public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

		/// <summary>When overridden in a derived class, writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="buffer" /> parameter value is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C4 RID: 4292
		public abstract void WriteChars(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C5 RID: 4293
		public abstract void WriteRaw(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C6 RID: 4294
		public abstract void WriteRaw(string data);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C7 RID: 4295
		public abstract void WriteBase64(byte[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as BinHex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010C8 RID: 4296 RVA: 0x00063417 File Offset: 0x00061617
		public virtual void WriteBinHex(byte[] buffer, int index, int count)
		{
			BinHexEncoder.Encode(buffer, index, count, this);
		}

		/// <summary>When overridden in a derived class, gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060010C9 RID: 4297
		public abstract WriteState WriteState { get; }

		/// <summary>When overridden in a derived class, closes this stream and the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">A call is made to write more output after Close has been called or the result of this call is an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010CA RID: 4298 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010CB RID: 4299
		public abstract void Flush();

		/// <summary>When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">The namespace URI whose prefix you want to find.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010CC RID: 4300
		public abstract string LookupPrefix(string ns);

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning NoneThis is the default if no xml:space scope exists.DefaultThe current scope is xml:space="default".PreserveThe current scope is xml:space="preserve".</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00003242 File Offset: 0x00001442
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.Default;
			}
		}

		/// <summary>When overridden in a derived class, gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00003065 File Offset: 0x00001265
		public virtual string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010CF RID: 4303 RVA: 0x00063422 File Offset: 0x00061622
		public virtual void WriteNmToken(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			this.WriteString(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D0 RID: 4304 RVA: 0x0006344C File Offset: 0x0006164C
		public virtual void WriteName(string name)
		{
			this.WriteString(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write.</param>
		/// <param name="ns">The namespace URI for the name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D1 RID: 4305 RVA: 0x0006345C File Offset: 0x0006165C
		public virtual void WriteQualifiedName(string localName, string ns)
		{
			if (ns != null && ns.Length > 0)
			{
				string text = this.LookupPrefix(ns);
				if (text == null)
				{
					throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
				}
				this.WriteString(text);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		/// <summary>Writes the object value.</summary>
		/// <param name="value">The object value to write.Note   With the release of the .NET Framework 3.5, this method accepts <see cref="T:System.DateTimeOffset" /> as a parameter.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D2 RID: 4306 RVA: 0x000634B3 File Offset: 0x000616B3
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, null));
		}

		/// <summary>Writes a <see cref="T:System.String" /> value.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D3 RID: 4307 RVA: 0x000634D5 File Offset: 0x000616D5
		public virtual void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.WriteString(value);
		}

		/// <summary>Writes a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D4 RID: 4308 RVA: 0x000634E2 File Offset: 0x000616E2
		public virtual void WriteValue(bool value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D5 RID: 4309 RVA: 0x000634F0 File Offset: 0x000616F0
		public virtual void WriteValue(DateTime value)
		{
			this.WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
		}

		/// <summary>Writes a <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D6 RID: 4310 RVA: 0x000634FF File Offset: 0x000616FF
		public virtual void WriteValue(DateTimeOffset value)
		{
			if (value.Offset != TimeSpan.Zero)
			{
				this.WriteValue(value.LocalDateTime);
				return;
			}
			this.WriteValue(value.UtcDateTime);
		}

		/// <summary>Writes a <see cref="T:System.Double" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D7 RID: 4311 RVA: 0x0006352F File Offset: 0x0006172F
		public virtual void WriteValue(double value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a single-precision floating-point number.</summary>
		/// <param name="value">The single-precision floating-point number to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D8 RID: 4312 RVA: 0x0006353D File Offset: 0x0006173D
		public virtual void WriteValue(float value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010D9 RID: 4313 RVA: 0x0006354B File Offset: 0x0006174B
		public virtual void WriteValue(decimal value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int32" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DA RID: 4314 RVA: 0x00063559 File Offset: 0x00061759
		public virtual void WriteValue(int value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int64" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DB RID: 4315 RVA: 0x00063567 File Offset: 0x00061767
		public virtual void WriteValue(long value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>When overridden in a derived class, writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader from which to copy the attributes.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <exception cref="T:System.Xml.XmlException">The reader is not positioned on an element, attribute or XmlDeclaration node. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DC RID: 4316 RVA: 0x00063578 File Offset: 0x00061778
		public virtual void WriteAttributes(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				if (reader.MoveToFirstAttribute())
				{
					this.WriteAttributes(reader, defattr);
					reader.MoveToElement();
					return;
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("The current position on the Reader is neither an element nor an attribute.", string.Empty);
				}
				do
				{
					if (defattr || !reader.IsDefaultInternal)
					{
						this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						while (reader.ReadAttributeValue())
						{
							if (reader.NodeType == XmlNodeType.EntityReference)
							{
								this.WriteEntityRef(reader.Name);
							}
							else
							{
								this.WriteString(reader.Value);
							}
						}
						this.WriteEndAttribute();
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		/// <summary>When overridden in a derived class, copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="reader" /> contains invalid characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DD RID: 4317 RVA: 0x00063638 File Offset: 0x00061838
		public virtual void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool canReadValueChunk = reader.CanReadValueChunk;
			int num = ((reader.NodeType == XmlNodeType.None) ? (-1) : reader.Depth);
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					this.WriteAttributes(reader, defattr);
					if (reader.IsEmptyElement)
					{
						this.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					if (canReadValueChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int num2;
						while ((num2 = reader.ReadValueChunk(this.writeNodeBuffer, 0, 1024)) > 0)
						{
							this.WriteChars(this.writeNodeBuffer, 0, num2);
						}
					}
					else
					{
						this.WriteString(reader.Value);
					}
					break;
				case XmlNodeType.CDATA:
					this.WriteCData(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					this.WriteProcessingInstruction(reader.Name, reader.Value);
					break;
				case XmlNodeType.Comment:
					this.WriteComment(reader.Value);
					break;
				case XmlNodeType.DocumentType:
					this.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.WriteWhitespace(reader.Value);
					break;
				case XmlNodeType.EndElement:
					this.WriteFullEndElement();
					break;
				}
			}
			while (reader.Read() && (num < reader.Depth || (num == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		/// <summary>Copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">true to copy the default attributes; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="navigator" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DE RID: 4318 RVA: 0x000637F8 File Offset: 0x000619F8
		public virtual void WriteNode(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			int num = 0;
			navigator = navigator.Clone();
			for (;;)
			{
				IL_0018:
				bool flag = false;
				switch (navigator.NodeType)
				{
				case XPathNodeType.Root:
					flag = true;
					break;
				case XPathNodeType.Element:
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							IXmlSchemaInfo schemaInfo = navigator.SchemaInfo;
							if (defattr || schemaInfo == null || !schemaInfo.IsDefault)
							{
								this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
								this.WriteString(navigator.Value);
								this.WriteEndAttribute();
							}
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						this.WriteLocalNamespaces(navigator);
						navigator.MoveToParent();
					}
					flag = true;
					break;
				case XPathNodeType.Text:
					this.WriteString(navigator.Value);
					break;
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					this.WriteWhitespace(navigator.Value);
					break;
				case XPathNodeType.ProcessingInstruction:
					this.WriteProcessingInstruction(navigator.LocalName, navigator.Value);
					break;
				case XPathNodeType.Comment:
					this.WriteComment(navigator.Value);
					break;
				}
				if (flag)
				{
					if (navigator.MoveToFirstChild())
					{
						num++;
						continue;
					}
					if (navigator.NodeType == XPathNodeType.Element)
					{
						if (navigator.IsEmptyElement)
						{
							this.WriteEndElement();
						}
						else
						{
							this.WriteFullEndElement();
						}
					}
				}
				while (num != 0)
				{
					if (navigator.MoveToNext())
					{
						goto IL_0018;
					}
					num--;
					navigator.MoveToParent();
					if (navigator.NodeType == XPathNodeType.Element)
					{
						this.WriteFullEndElement();
					}
				}
				break;
			}
		}

		/// <summary>Writes an element with the specified local name and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010DF RID: 4319 RVA: 0x0006397B File Offset: 0x00061B7B
		public void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value);
		}

		/// <summary>Writes an element with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010E0 RID: 4320 RVA: 0x00063986 File Offset: 0x00061B86
		public void WriteElementString(string localName, string ns, string value)
		{
			this.WriteStartElement(localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Writes an element with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010E1 RID: 4321 RVA: 0x000639A8 File Offset: 0x00061BA8
		public void WriteElementString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartElement(prefix, localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlWriter" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010E2 RID: 4322 RVA: 0x000639CE File Offset: 0x00061BCE
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060010E3 RID: 4323 RVA: 0x000639D7 File Offset: 0x00061BD7
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000639EC File Offset: 0x00061BEC
		private void WriteLocalNamespaces(XPathNavigator nsNav)
		{
			string localName = nsNav.LocalName;
			string value = nsNav.Value;
			if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				this.WriteLocalNamespaces(nsNav);
			}
			if (localName.Length == 0)
			{
				this.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", value);
				return;
			}
			this.WriteAttributeString("xmlns", localName, "http://www.w3.org/2000/xmlns/", value);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified filename.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is null.</exception>
		// Token: 0x060010E5 RID: 4325 RVA: 0x00063A48 File Offset: 0x00061C48
		public static XmlWriter Create(string outputFileName)
		{
			return XmlWriter.Create(outputFileName, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the filename and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is null.</exception>
		// Token: 0x060010E6 RID: 4326 RVA: 0x00063A51 File Offset: 0x00061C51
		public static XmlWriter Create(string outputFileName, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(outputFileName);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified stream.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is null.</exception>
		// Token: 0x060010E7 RID: 4327 RVA: 0x00063A64 File Offset: 0x00061C64
		public static XmlWriter Create(Stream output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is null.</exception>
		// Token: 0x060010E8 RID: 4328 RVA: 0x00063A6D File Offset: 0x00061C6D
		public static XmlWriter Create(Stream output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is null.</exception>
		// Token: 0x060010E9 RID: 4329 RVA: 0x00063A80 File Offset: 0x00061C80
		public static XmlWriter Create(TextWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is null.</exception>
		// Token: 0x060010EA RID: 4330 RVA: 0x00063A89 File Offset: 0x00061C89
		public static XmlWriter Create(TextWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is null.</exception>
		// Token: 0x060010EB RID: 4331 RVA: 0x00063A9C File Offset: 0x00061C9C
		public static XmlWriter Create(StringBuilder output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.Text.StringBuilder" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is null.</exception>
		// Token: 0x060010EC RID: 4332 RVA: 0x00063AA5 File Offset: 0x00061CA5
		public static XmlWriter Create(StringBuilder output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return settings.CreateWriter(new StringWriter(output, CultureInfo.InvariantCulture));
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is null.</exception>
		// Token: 0x060010ED RID: 4333 RVA: 0x00063AD0 File Offset: 0x00061CD0
		public static XmlWriter Create(XmlWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is null.</exception>
		// Token: 0x060010EE RID: 4334 RVA: 0x00063AD9 File Offset: 0x00061CD9
		public static XmlWriter Create(XmlWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Asynchronously writes the XML declaration with the version "1.0".</summary>
		/// <returns>The task that represents the asynchronous WriteStartDocument operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010EF RID: 4335 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteStartDocumentAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <returns>The task that represents the asynchronous WriteStartDocument operation.</returns>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F0 RID: 4336 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteStartDocumentAsync(bool standalone)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <returns>The task that represents the asynchronous WriteEndDocument operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F1 RID: 4337 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteEndDocumentAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <returns>The task that represents the asynchronous WriteDocType operation.</returns>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F2 RID: 4338 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <returns>The task that represents the asynchronous WriteStartElement operation.</returns>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F3 RID: 4339 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes one element and pops the corresponding namespace scope.</summary>
		/// <returns>The task that represents the asynchronous WriteEndElement operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F4 RID: 4340 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteEndElementAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes one element and pops the corresponding namespace scope.</summary>
		/// <returns>The task that represents the asynchronous WriteFullEndElement operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F5 RID: 4341 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteFullEndElementAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <returns>The task that represents the asynchronous WriteAttributeString operation.</returns>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F6 RID: 4342 RVA: 0x00063AEC File Offset: 0x00061CEC
		public Task WriteAttributeStringAsync(string prefix, string localName, string ns, string value)
		{
			Task task = this.WriteStartAttributeAsync(prefix, localName, ns);
			if (task.IsSuccess())
			{
				return this.WriteStringAsync(value).CallTaskFuncWhenFinish(new Func<Task>(this.WriteEndAttributeAsync));
			}
			return this.WriteAttributeStringAsyncHelper(task, value);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00063B30 File Offset: 0x00061D30
		private async Task WriteAttributeStringAsyncHelper(Task task, string value)
		{
			await task.ConfigureAwait(false);
			await this.WriteStringAsync(value).ConfigureAwait(false);
			await this.WriteEndAttributeAsync().ConfigureAwait(false);
		}

		/// <summary>Asynchronously writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <returns>The task that represents the asynchronous WriteStartAttribute operation.</returns>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F8 RID: 4344 RVA: 0x0000A533 File Offset: 0x00008733
		protected internal virtual Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <returns>The task that represents the asynchronous WriteEndAttribute operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010F9 RID: 4345 RVA: 0x0000A533 File Offset: 0x00008733
		protected internal virtual Task WriteEndAttributeAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <returns>The task that represents the asynchronous WriteCData operation.</returns>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FA RID: 4346 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteCDataAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <returns>The task that represents the asynchronous WriteComment operation.</returns>
		/// <param name="text">Text to place inside the comment.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FB RID: 4347 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteCommentAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <returns>The task that represents the asynchronous WriteProcessingInstruction operation.</returns>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FC RID: 4348 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteProcessingInstructionAsync(string name, string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out an entity reference as &amp;name;.</summary>
		/// <returns>The task that represents the asynchronous WriteEntityRef operation.</returns>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FD RID: 4349 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteEntityRefAsync(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <returns>The task that represents the asynchronous WriteCharEntity operation.</returns>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FE RID: 4350 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteCharEntityAsync(char ch)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the given white space.</summary>
		/// <returns>The task that represents the asynchronous WriteWhitespace operation.</returns>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x060010FF RID: 4351 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteWhitespaceAsync(string ws)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the given text content.</summary>
		/// <returns>The task that represents the asynchronous WriteString operation.</returns>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001100 RID: 4352 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteStringAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <returns>The task that represents the asynchronous WriteSurrogateCharEntity operation.</returns>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001101 RID: 4353 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes text one buffer at a time.</summary>
		/// <returns>The task that represents the asynchronous WriteChars operation.</returns>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001102 RID: 4354 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a character buffer.</summary>
		/// <returns>The task that represents the asynchronous WriteRaw operation.</returns>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001103 RID: 4355 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteRawAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a string.</summary>
		/// <returns>The task that represents the asynchronous WriteRaw operation.</returns>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001104 RID: 4356 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteRawAsync(string data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <returns>The task that represents the asynchronous WriteBase64 operation.</returns>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001105 RID: 4357 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously encodes the specified binary bytes as BinHex and writes out the resulting text.</summary>
		/// <returns>The task that represents the asynchronous WriteBinHex operation.</returns>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001106 RID: 4358 RVA: 0x00063B85 File Offset: 0x00061D85
		public virtual Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			return BinHexEncoder.EncodeAsync(buffer, index, count, this);
		}

		/// <summary>Asynchronously flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		/// <returns>The task that represents the asynchronous Flush operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001107 RID: 4359 RVA: 0x0000A533 File Offset: 0x00008733
		public virtual Task FlushAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <returns>The task that represents the asynchronous WriteNmToken operation.</returns>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001108 RID: 4360 RVA: 0x00063B90 File Offset: 0x00061D90
		public virtual Task WriteNmTokenAsync(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			return this.WriteStringAsync(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		/// <summary>Asynchronously writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <returns>The task that represents the asynchronous WriteName operation.</returns>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001109 RID: 4361 RVA: 0x00063BBA File Offset: 0x00061DBA
		public virtual Task WriteNameAsync(string name)
		{
			return this.WriteStringAsync(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		/// <summary>Asynchronously writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <returns>The task that represents the asynchronous WriteQualifiedName operation.</returns>
		/// <param name="localName">The local name to write.</param>
		/// <param name="ns">The namespace URI for the name.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600110A RID: 4362 RVA: 0x00063BCC File Offset: 0x00061DCC
		public virtual async Task WriteQualifiedNameAsync(string localName, string ns)
		{
			if (ns != null && ns.Length > 0)
			{
				string text = this.LookupPrefix(ns);
				if (text == null)
				{
					throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
				}
				await this.WriteStringAsync(text).ConfigureAwait(false);
				await this.WriteStringAsync(":").ConfigureAwait(false);
			}
			await this.WriteStringAsync(localName).ConfigureAwait(false);
		}

		/// <summary>Asynchronously writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The task that represents the asynchronous WriteAttributes operation.</returns>
		/// <param name="reader">The XmlReader from which to copy the attributes.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600110B RID: 4363 RVA: 0x00063C24 File Offset: 0x00061E24
		public virtual async Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				if (reader.MoveToFirstAttribute())
				{
					await this.WriteAttributesAsync(reader, defattr).ConfigureAwait(false);
					reader.MoveToElement();
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("The current position on the Reader is neither an element nor an attribute.", string.Empty);
				}
				do
				{
					if (defattr || !reader.IsDefaultInternal)
					{
						await this.WriteStartAttributeAsync(reader.Prefix, reader.LocalName, reader.NamespaceURI).ConfigureAwait(false);
						while (reader.ReadAttributeValue())
						{
							if (reader.NodeType == XmlNodeType.EntityReference)
							{
								await this.WriteEntityRefAsync(reader.Name).ConfigureAwait(false);
							}
							else
							{
								await this.WriteStringAsync(reader.Value).ConfigureAwait(false);
							}
						}
						await this.WriteEndAttributeAsync().ConfigureAwait(false);
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		/// <summary>Asynchronously copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <returns>The task that represents the asynchronous WriteNode operation.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600110C RID: 4364 RVA: 0x00063C79 File Offset: 0x00061E79
		public virtual Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.Settings != null && reader.Settings.Async)
			{
				return this.WriteNodeAsync_CallAsyncReader(reader, defattr);
			}
			return this.WriteNodeAsync_CallSyncReader(reader, defattr);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00063CB0 File Offset: 0x00061EB0
		internal async Task WriteNodeAsync_CallSyncReader(XmlReader reader, bool defattr)
		{
			bool canReadChunk = reader.CanReadValueChunk;
			int d = ((reader.NodeType == XmlNodeType.None) ? (-1) : reader.Depth);
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					await this.WriteStartElementAsync(reader.Prefix, reader.LocalName, reader.NamespaceURI).ConfigureAwait(false);
					await this.WriteAttributesAsync(reader, defattr).ConfigureAwait(false);
					if (reader.IsEmptyElement)
					{
						await this.WriteEndElementAsync().ConfigureAwait(false);
					}
					break;
				case XmlNodeType.Text:
					if (canReadChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int read;
						while ((read = reader.ReadValueChunk(this.writeNodeBuffer, 0, 1024)) > 0)
						{
							await this.WriteCharsAsync(this.writeNodeBuffer, 0, read).ConfigureAwait(false);
						}
					}
					else
					{
						await this.WriteStringAsync(reader.Value).ConfigureAwait(false);
					}
					break;
				case XmlNodeType.CDATA:
					await this.WriteCDataAsync(reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.EntityReference:
					await this.WriteEntityRefAsync(reader.Name).ConfigureAwait(false);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					await this.WriteProcessingInstructionAsync(reader.Name, reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.Comment:
					await this.WriteCommentAsync(reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.DocumentType:
					await this.WriteDocTypeAsync(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					await this.WriteWhitespaceAsync(reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.EndElement:
					await this.WriteFullEndElementAsync().ConfigureAwait(false);
					break;
				}
			}
			while (reader.Read() && (d < reader.Depth || (d == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00063D08 File Offset: 0x00061F08
		internal async Task WriteNodeAsync_CallAsyncReader(XmlReader reader, bool defattr)
		{
			bool canReadChunk = reader.CanReadValueChunk;
			int d = ((reader.NodeType == XmlNodeType.None) ? (-1) : reader.Depth);
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					await this.WriteStartElementAsync(reader.Prefix, reader.LocalName, reader.NamespaceURI).ConfigureAwait(false);
					await this.WriteAttributesAsync(reader, defattr).ConfigureAwait(false);
					if (reader.IsEmptyElement)
					{
						await this.WriteEndElementAsync().ConfigureAwait(false);
					}
					break;
				case XmlNodeType.Text:
					if (canReadChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int read;
						while ((read = await reader.ReadValueChunkAsync(this.writeNodeBuffer, 0, 1024).ConfigureAwait(false)) > 0)
						{
							await this.WriteCharsAsync(this.writeNodeBuffer, 0, read).ConfigureAwait(false);
						}
					}
					else
					{
						await this.WriteStringAsync(await reader.GetValueAsync().ConfigureAwait(false)).ConfigureAwait(false);
					}
					break;
				case XmlNodeType.CDATA:
					await this.WriteCDataAsync(reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.EntityReference:
					await this.WriteEntityRefAsync(reader.Name).ConfigureAwait(false);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					await this.WriteProcessingInstructionAsync(reader.Name, reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.Comment:
					await this.WriteCommentAsync(reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.DocumentType:
					await this.WriteDocTypeAsync(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value).ConfigureAwait(false);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					await this.WriteWhitespaceAsync(await reader.GetValueAsync().ConfigureAwait(false)).ConfigureAwait(false);
					break;
				case XmlNodeType.EndElement:
					await this.WriteFullEndElementAsync().ConfigureAwait(false);
					break;
				}
				configuredTaskAwaiter = reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
			}
			while (configuredTaskAwaiter.GetResult() && (d < reader.Depth || (d == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		/// <summary>Asynchronously copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <returns>The task that represents the asynchronous WriteNode operation.</returns>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">true to copy the default attributes; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600110F RID: 4367 RVA: 0x00063D60 File Offset: 0x00061F60
		public virtual async Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			int iLevel = 0;
			navigator = navigator.Clone();
			for (;;)
			{
				IL_006F:
				bool mayHaveChildren = false;
				switch (navigator.NodeType)
				{
				case XPathNodeType.Root:
					mayHaveChildren = true;
					break;
				case XPathNodeType.Element:
					await this.WriteStartElementAsync(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI).ConfigureAwait(false);
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							IXmlSchemaInfo schemaInfo = navigator.SchemaInfo;
							if (defattr || schemaInfo == null || !schemaInfo.IsDefault)
							{
								await this.WriteStartAttributeAsync(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI).ConfigureAwait(false);
								await this.WriteStringAsync(navigator.Value).ConfigureAwait(false);
								await this.WriteEndAttributeAsync().ConfigureAwait(false);
							}
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						await this.WriteLocalNamespacesAsync(navigator).ConfigureAwait(false);
						navigator.MoveToParent();
					}
					mayHaveChildren = true;
					break;
				case XPathNodeType.Text:
					await this.WriteStringAsync(navigator.Value).ConfigureAwait(false);
					break;
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					await this.WriteWhitespaceAsync(navigator.Value).ConfigureAwait(false);
					break;
				case XPathNodeType.ProcessingInstruction:
					await this.WriteProcessingInstructionAsync(navigator.LocalName, navigator.Value).ConfigureAwait(false);
					break;
				case XPathNodeType.Comment:
					await this.WriteCommentAsync(navigator.Value).ConfigureAwait(false);
					break;
				}
				if (mayHaveChildren)
				{
					if (navigator.MoveToFirstChild())
					{
						iLevel++;
						continue;
					}
					if (navigator.NodeType == XPathNodeType.Element)
					{
						if (navigator.IsEmptyElement)
						{
							await this.WriteEndElementAsync().ConfigureAwait(false);
						}
						else
						{
							await this.WriteFullEndElementAsync().ConfigureAwait(false);
						}
					}
				}
				while (iLevel != 0)
				{
					if (navigator.MoveToNext())
					{
						goto IL_006F;
					}
					iLevel--;
					navigator.MoveToParent();
					if (navigator.NodeType == XPathNodeType.Element)
					{
						await this.WriteFullEndElementAsync().ConfigureAwait(false);
					}
				}
				break;
			}
		}

		/// <summary>Asynchronously writes an element with the specified prefix, local name, namespace URI, and value.</summary>
		/// <returns>The task that represents the asynchronous WriteElementString operation.</returns>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06001110 RID: 4368 RVA: 0x00063DB8 File Offset: 0x00061FB8
		public async Task WriteElementStringAsync(string prefix, string localName, string ns, string value)
		{
			await this.WriteStartElementAsync(prefix, localName, ns).ConfigureAwait(false);
			if (value != null && value.Length != 0)
			{
				await this.WriteStringAsync(value).ConfigureAwait(false);
			}
			await this.WriteEndElementAsync().ConfigureAwait(false);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00063E20 File Offset: 0x00062020
		private async Task WriteLocalNamespacesAsync(XPathNavigator nsNav)
		{
			string prefix = nsNav.LocalName;
			string ns = nsNav.Value;
			if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				await this.WriteLocalNamespacesAsync(nsNav).ConfigureAwait(false);
			}
			if (prefix.Length == 0)
			{
				await this.WriteAttributeStringAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", ns).ConfigureAwait(false);
			}
			else
			{
				await this.WriteAttributeStringAsync("xmlns", prefix, "http://www.w3.org/2000/xmlns/", ns).ConfigureAwait(false);
			}
		}

		// Token: 0x04000BEF RID: 3055
		private char[] writeNodeBuffer;

		// Token: 0x04000BF0 RID: 3056
		private const int WriteNodeBufferSize = 1024;
	}
}
