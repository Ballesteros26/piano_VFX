using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>Represents an element in a resource file.</summary>
	// Token: 0x0200000A RID: 10
	[Serializable]
	public sealed class ResXDataNode : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class. </summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The resource to store. </param>
		/// <exception cref="T:System.InvalidOperationException">The resource named in <paramref name="value" /> does not support serialization. </exception>
		// Token: 0x0600000C RID: 12 RVA: 0x00002164 File Offset: 0x00000364
		public ResXDataNode(string name, object value)
			: this(name, value, Point.Empty)
		{
		}

		/// <summary>This overload of the <see cref="T:System.Resources.ResXDataNode" /> constructor allows you to create a reference to a file, and store that file as a resource for your application.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="fileRef">The file reference to use as the resource.</param>
		// Token: 0x0600000D RID: 13 RVA: 0x00002174 File Offset: 0x00000374
		public ResXDataNode(string name, ResXFileRef fileRef)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileRef == null)
			{
				throw new ArgumentNullException("fileRef");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.name = name;
			this.fileRef = fileRef;
			this.pos = Point.Empty;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D8 File Offset: 0x000003D8
		internal ResXDataNode(string name, object value, Point position)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			Type type = ((value != null) ? value.GetType() : typeof(object));
			if (value != null && !type.IsSerializable)
			{
				throw new InvalidOperationException(string.Format("'{0}' of type '{1}' cannot be added because it is not serializable", name, type));
			}
			this.type = type;
			this.name = name;
			this.value = value;
			this.pos = position;
		}

		/// <summary>Retrieves the object's data.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600000F RID: 15 RVA: 0x00002270 File Offset: 0x00000470
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", this.Name);
			si.AddValue("Comment", this.Comment);
		}

		/// <summary>Gets or sets an arbitrary comment regarding this resource. </summary>
		/// <returns>A <see cref="T:System.String" /> representing the comment.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022A0 File Offset: 0x000004A0
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022A8 File Offset: 0x000004A8
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		/// <summary>Gets the file reference for this resource.</summary>
		/// <returns>The <see cref="T:System.Resources.ResXFileRef" /> corresponding to the file reference, if this resource uses one. If this resource stores its value as an <see cref="T:System.Object" />, this property will return null.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022B4 File Offset: 0x000004B4
		public ResXFileRef FileRef
		{
			get
			{
				return this.fileRef;
			}
		}

		/// <summary>Gets or sets the name of this resource.</summary>
		/// <returns>A <see cref="T:System.String" /> corresponding to the resource name. If no name is assigned, returns a zero-length string.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022C4 File Offset: 0x000004C4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022D0 File Offset: 0x000004D0
		internal object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets the position of the resource in the resource file. </summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure specifying the location of this resource in the resource file as a line position (<see cref="P:System.Drawing.Point.X" />) and a column position (<see cref="P:System.Drawing.Point.Y" />). If this resource is not part of a resource file, returns a <see cref="T:System.Drawing.Point" /> structure with an <see cref="P:System.Drawing.Point.X" /> of 0 and a <see cref="P:System.Drawing.Point.Y" /> of 0. </returns>
		// Token: 0x06000016 RID: 22 RVA: 0x000022D8 File Offset: 0x000004D8
		public Point GetNodePosition()
		{
			return this.pos;
		}

		/// <summary>Gets the name of the type of the value.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the fully qualified name of the type.</returns>
		/// <param name="names">The assemblies to examine for the type. </param>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found. </exception>
		// Token: 0x06000017 RID: 23 RVA: 0x000022E0 File Offset: 0x000004E0
		[MonoInternalNote("Move the type parsing process from ResxResourceReader")]
		public string GetValueTypeName(AssemblyName[] names)
		{
			return this.type.AssemblyQualifiedName;
		}

		/// <summary>A <see cref="T:System.String" /> representing the fully qualified name of the type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the fully qualified name of the type.</returns>
		/// <param name="typeResolver">The type resolution service to use to locate a converter for this type. </param>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found. </exception>
		// Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
		[MonoInternalNote("Move the type parsing process from ResxResourceReader")]
		public string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return this.type.AssemblyQualifiedName;
		}

		/// <summary>Gets the object stored by this node.</summary>
		/// <returns>The <see cref="T:System.Object" /> corresponding to the stored value. </returns>
		/// <param name="names">The list of assemblies in which to look for the type of the object.</param>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
		// Token: 0x06000019 RID: 25 RVA: 0x00002300 File Offset: 0x00000500
		[MonoInternalNote("Move the value parsing process from ResxResourceReader")]
		public object GetValue(AssemblyName[] names)
		{
			return this.value;
		}

		/// <summary>Gets the object stored by this node.</summary>
		/// <returns>The <see cref="T:System.Object" /> corresponding to the stored value. </returns>
		/// <param name="typeResolver">The type resolution service to use when looking for a type converter.</param>
		/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
		// Token: 0x0600001A RID: 26 RVA: 0x00002308 File Offset: 0x00000508
		[MonoInternalNote("Move the value parsing process from ResxResourceReader")]
		public object GetValue(ITypeResolutionService typeResolver)
		{
			return this.value;
		}

		// Token: 0x0400001F RID: 31
		private string name;

		// Token: 0x04000020 RID: 32
		private object value;

		// Token: 0x04000021 RID: 33
		private Type type;

		// Token: 0x04000022 RID: 34
		private ResXFileRef fileRef;

		// Token: 0x04000023 RID: 35
		private string comment;

		// Token: 0x04000024 RID: 36
		private Point pos;
	}
}
