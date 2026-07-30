using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Allows you to override property, field, and class attributes when you use the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize or deserialize an object.</summary>
	// Token: 0x0200032A RID: 810
	public class XmlAttributeOverrides
	{
		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden by the <see cref="T:System.Xml.Serialization.XmlAttributes" /> object.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that is overridden. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001E8B RID: 7819 RVA: 0x000A6D17 File Offset: 0x000A4F17
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden. The <paramref name="member" /> parameter specifies the name of a member that is overridden.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to override. </param>
		/// <param name="member">The name of the member to override. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001E8C RID: 7820 RVA: 0x000A6D28 File Offset: 0x000A4F28
		public void Add(Type type, string member, XmlAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException(Res.GetString("'{0}.{1}' already has attributes.", new object[] { type.FullName, member }));
			}
			hashtable.Add(member, attributes);
		}

		/// <summary>Gets the object associated with the specified, base-class, type.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to retrieve. </param>
		// Token: 0x17000627 RID: 1575
		public XmlAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		/// <summary>Gets the object associated with the specified (base-class) type. The member parameter specifies the base-class member that is overridden.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want. </param>
		/// <param name="member">The name of the overridden member that specifies the <see cref="T:System.Xml.Serialization.XmlAttributes" /> to return. </param>
		// Token: 0x17000628 RID: 1576
		public XmlAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (XmlAttributes)hashtable[member];
			}
		}

		// Token: 0x04001712 RID: 5906
		private Hashtable types = new Hashtable();
	}
}
