using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can manage design-time serialization.</summary>
	// Token: 0x02000350 RID: 848
	public interface IDesignerSerializationManager : IServiceProvider
	{
		/// <summary>Gets a stack-based, user-defined storage area that is useful for communication between serializers.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> that stores data.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A66 RID: 6758
		ContextStack Context { get; }

		/// <summary>Indicates custom properties that can be serializable with available serializers.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties to be serialized.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A67 RID: 6759
		PropertyDescriptorCollection Properties { get; }

		/// <summary>Occurs when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetName(System.Object)" /> cannot locate the specified name in the serialization manager's name table.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001A68 RID: 6760
		// (remove) Token: 0x06001A69 RID: 6761
		event ResolveNameEventHandler ResolveName;

		/// <summary>Occurs when serialization is complete.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001A6A RID: 6762
		// (remove) Token: 0x06001A6B RID: 6763
		event EventHandler SerializationComplete;

		/// <summary>Adds the specified serialization provider to the serialization manager.</summary>
		/// <param name="provider">The serialization provider to add. </param>
		// Token: 0x06001A6C RID: 6764
		void AddSerializationProvider(IDesignerSerializationProvider provider);

		/// <summary>Creates an instance of the specified type and adds it to a collection of named instances.</summary>
		/// <returns>The newly created object instance.</returns>
		/// <param name="type">The data type to create. </param>
		/// <param name="arguments">The arguments to pass to the constructor for this type. </param>
		/// <param name="name">The name of the object. This name can be used to access the object later through <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetInstance(System.String)" />. If null is passed, the object is still created but cannot be accessed by name. </param>
		/// <param name="addToContainer">If true, this object is added to the design container. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect. </param>
		// Token: 0x06001A6D RID: 6765
		object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer);

		/// <summary>Gets an instance of a created object of the specified name, or null if that object does not exist.</summary>
		/// <returns>An instance of the object with the given name, or null if no object by that name can be found.</returns>
		/// <param name="name">The name of the object to retrieve. </param>
		// Token: 0x06001A6E RID: 6766
		object GetInstance(string name);

		/// <summary>Gets the name of the specified object, or null if the object has no name.</summary>
		/// <returns>The name of the object, or null if the object is unnamed.</returns>
		/// <param name="value">The object to retrieve the name for. </param>
		// Token: 0x06001A6F RID: 6767
		string GetName(object value);

		/// <summary>Gets a serializer of the requested type for the specified object type.</summary>
		/// <returns>An instance of the requested serializer, or null if no appropriate serializer can be located.</returns>
		/// <param name="objectType">The type of the object to get the serializer for. </param>
		/// <param name="serializerType">The type of the serializer to retrieve. </param>
		// Token: 0x06001A70 RID: 6768
		object GetSerializer(Type objectType, Type serializerType);

		/// <summary>Gets a type of the specified name.</summary>
		/// <returns>An instance of the type, or null if the type cannot be loaded.</returns>
		/// <param name="typeName">The fully qualified name of the type to load. </param>
		// Token: 0x06001A71 RID: 6769
		Type GetType(string typeName);

		/// <summary>Removes a custom serialization provider from the serialization manager.</summary>
		/// <param name="provider">The provider to remove. This object must have been added using <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.AddSerializationProvider(System.ComponentModel.Design.Serialization.IDesignerSerializationProvider)" />. </param>
		// Token: 0x06001A72 RID: 6770
		void RemoveSerializationProvider(IDesignerSerializationProvider provider);

		/// <summary>Reports an error in serialization.</summary>
		/// <param name="errorInformation">The error to report. This information object can be of any object type. If it is an exception, the message of the exception is extracted and reported to the user. If it is any other type, <see cref="M:System.Object.ToString" /> is called to display the information to the user. </param>
		// Token: 0x06001A73 RID: 6771
		void ReportError(object errorInformation);

		/// <summary>Sets the name of the specified existing object.</summary>
		/// <param name="instance">The object instance to name. </param>
		/// <param name="name">The name to give the instance. </param>
		// Token: 0x06001A74 RID: 6772
		void SetName(object instance, string name);
	}
}
