using System;
using System.Collections;

namespace System.Xml.Xsl
{
	/// <summary>Contains a variable number of arguments which are either XSLT parameters or extension objects.</summary>
	// Token: 0x020004DC RID: 1244
	public class XsltArgumentList
	{
		/// <summary>Gets the parameter associated with the namespace qualified name.</summary>
		/// <returns>The parameter object or null if one was not found.</returns>
		/// <param name="name">The name of the parameter. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be null. </param>
		/// <param name="namespaceUri">The namespace URI associated with the parameter. </param>
		// Token: 0x060032BA RID: 12986 RVA: 0x00124512 File Offset: 0x00122712
		public object GetParam(string name, string namespaceUri)
		{
			return this.parameters[new XmlQualifiedName(name, namespaceUri)];
		}

		/// <summary>Gets the object associated with the given namespace.</summary>
		/// <returns>The namespace URI object or null if one was not found.</returns>
		/// <param name="namespaceUri">The namespace URI of the object. </param>
		// Token: 0x060032BB RID: 12987 RVA: 0x00124526 File Offset: 0x00122726
		public object GetExtensionObject(string namespaceUri)
		{
			return this.extensions[namespaceUri];
		}

		/// <summary>Adds a parameter to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace qualified name.</summary>
		/// <param name="name">The name to associate with the parameter. </param>
		/// <param name="namespaceUri">The namespace URI to associate with the parameter. To use the default namespace, specify an empty string. </param>
		/// <param name="parameter">The parameter value or object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either null or http://www.w3.org/1999/XSL/Transform.The <paramref name="name" /> is not a valid name according to the W3C XML specification.The <paramref name="namespaceUri" /> already has a parameter associated with it. </exception>
		// Token: 0x060032BC RID: 12988 RVA: 0x00124534 File Offset: 0x00122734
		public void AddParam(string name, string namespaceUri, object parameter)
		{
			XsltArgumentList.CheckArgumentNull(name, "name");
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(parameter, "parameter");
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, namespaceUri);
			xmlQualifiedName.Verify();
			this.parameters.Add(xmlQualifiedName, parameter);
		}

		/// <summary>Adds a new object to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace URI.</summary>
		/// <param name="namespaceUri">The namespace URI to associate with the object. To use the default namespace, specify an empty string. </param>
		/// <param name="extension">The object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either null or http://www.w3.org/1999/XSL/Transform The <paramref name="namespaceUri" /> already has an extension object associated with it. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient permissions to call this method. </exception>
		// Token: 0x060032BD RID: 12989 RVA: 0x0012457D File Offset: 0x0012277D
		public void AddExtensionObject(string namespaceUri, object extension)
		{
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(extension, "extension");
			this.extensions.Add(namespaceUri, extension);
		}

		/// <summary>Removes the parameter from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <returns>The parameter object or null if one was not found.</returns>
		/// <param name="name">The name of the parameter to remove. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be null. </param>
		/// <param name="namespaceUri">The namespace URI of the parameter to remove. </param>
		// Token: 0x060032BE RID: 12990 RVA: 0x001245A4 File Offset: 0x001227A4
		public object RemoveParam(string name, string namespaceUri)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, namespaceUri);
			object obj = this.parameters[xmlQualifiedName];
			this.parameters.Remove(xmlQualifiedName);
			return obj;
		}

		/// <summary>Removes the object with the namespace URI from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <returns>The object with the namespace URI or null if one was not found.</returns>
		/// <param name="namespaceUri">The namespace URI associated with the object to remove. </param>
		// Token: 0x060032BF RID: 12991 RVA: 0x001245D1 File Offset: 0x001227D1
		public object RemoveExtensionObject(string namespaceUri)
		{
			object obj = this.extensions[namespaceUri];
			this.extensions.Remove(namespaceUri);
			return obj;
		}

		/// <summary>Occurs when a message is specified in the style sheet by the xsl:message element. </summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060032C0 RID: 12992 RVA: 0x001245EB File Offset: 0x001227EB
		// (remove) Token: 0x060032C1 RID: 12993 RVA: 0x00124604 File Offset: 0x00122804
		public event XsltMessageEncounteredEventHandler XsltMessageEncountered
		{
			add
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Combine(this.xsltMessageEncountered, value);
			}
			remove
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Remove(this.xsltMessageEncountered, value);
			}
		}

		/// <summary>Removes all parameters and extension objects from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		// Token: 0x060032C2 RID: 12994 RVA: 0x0012461D File Offset: 0x0012281D
		public void Clear()
		{
			this.parameters.Clear();
			this.extensions.Clear();
			this.xsltMessageEncountered = null;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0012463C File Offset: 0x0012283C
		private static void CheckArgumentNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x040020F4 RID: 8436
		private Hashtable parameters = new Hashtable();

		// Token: 0x040020F5 RID: 8437
		private Hashtable extensions = new Hashtable();

		// Token: 0x040020F6 RID: 8438
		internal XsltMessageEncounteredEventHandler xsltMessageEncountered;
	}
}
