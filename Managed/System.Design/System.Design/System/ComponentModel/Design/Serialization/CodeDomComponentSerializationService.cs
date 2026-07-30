using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Serializes a set of components into a serialization store.</summary>
	// Token: 0x02000141 RID: 321
	public sealed class CodeDomComponentSerializationService : ComponentSerializationService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService" /> class. </summary>
		// Token: 0x06000996 RID: 2454 RVA: 0x00011588 File Offset: 0x0000F788
		public CodeDomComponentSerializationService()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService" /> class using the given service provider to resolve services.</summary>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> to use for resolving services.</param>
		// Token: 0x06000997 RID: 2455 RVA: 0x00011591 File Offset: 0x0000F791
		public CodeDomComponentSerializationService(IServiceProvider provider)
		{
			this._provider = provider;
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <returns>A new serialization store.</returns>
		// Token: 0x06000998 RID: 2456 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public override SerializationStore CreateStore()
		{
			return new CodeDomComponentSerializationService.CodeDomSerializationStore(this._provider);
		}

		/// <summary>Loads a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from the given stream.</summary>
		/// <returns>The loaded <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</returns>
		/// <param name="stream">The stream from which to load the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="stream" /> supports seeking, but its length is 0. </exception>
		// Token: 0x06000999 RID: 2457 RVA: 0x000115AD File Offset: 0x0000F7AD
		public override SerializationStore LoadStore(Stream stream)
		{
			return CodeDomComponentSerializationService.CodeDomSerializationStore.Load(stream);
		}

		/// <summary>Deserializes the given store to produce a collection of objects.</summary>
		/// <returns>A collection of deserialized components.</returns>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from which objects will be deserialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x0600099A RID: 2458 RVA: 0x000115B5 File Offset: 0x0000F7B5
		public override ICollection Deserialize(SerializationStore store)
		{
			return this.Deserialize(store, null);
		}

		/// <summary>Deserializes the given store and populates the given <see cref="T:System.ComponentModel.IContainer" /> with deserialized <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
		/// <returns>A collection of deserialized components.</returns>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from which objects will be deserialized.</param>
		/// <param name="container">A container to which <see cref="T:System.ComponentModel.IComponent" />  objects will be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x0600099B RID: 2459 RVA: 0x000115BF File Offset: 0x0000F7BF
		public override ICollection Deserialize(SerializationStore store, IContainer container)
		{
			return this.DeserializeCore(store, container, true, true);
		}

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally applying default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />  from which the objects will be deserialized.</param>
		/// <param name="container">A container of objects to which data will be applied.</param>
		/// <param name="validateRecycledTypes">true to validate the recycled type; otherwise, false.</param>
		/// <param name="applyDefaults">true to apply default property values; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x0600099C RID: 2460 RVA: 0x000115CB File Offset: 0x0000F7CB
		public override void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults)
		{
			this.DeserializeCore(store, container, validateRecycledTypes, applyDefaults);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000115D9 File Offset: 0x0000F7D9
		private ICollection DeserializeCore(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults)
		{
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException("store type unsupported");
			}
			return codeDomSerializationStore.Deserialize(this._provider, container, validateRecycledTypes, applyDefaults);
		}

		/// <summary>Serializes the given object to the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />  to which <paramref name="value" /> will be serialized. </param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x0600099E RID: 2462 RVA: 0x000115FE File Offset: 0x0000F7FE
		public override void Serialize(SerializationStore store, object value)
		{
			this.SerializeCore(store, value, false);
		}

		/// <summary>Serializes the given object, accounting for default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />  to which <paramref name="value" /> will be serialized. </param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x0600099F RID: 2463 RVA: 0x00011609 File Offset: 0x0000F809
		public override void SerializeAbsolute(SerializationStore store, object value)
		{
			this.SerializeCore(store, value, true);
		}

		/// <summary>Serializes the given member on the given object.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />  to which <paramref name="member" /> will be serialized. </param>
		/// <param name="owningObject">The object that owns the <paramref name="member" />.</param>
		/// <param name="member">The given member.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" />, <paramref name="owningObject" />, or <paramref name="member" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060009A0 RID: 2464 RVA: 0x00011614 File Offset: 0x0000F814
		public override void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member)
		{
			this.SerializeMemberCore(store, owningObject, member, false);
		}

		/// <summary>Serializes the given member on the given object, but also serializes the member if it contains the default property value.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />  to which <paramref name="member" /> will be serialized. </param>
		/// <param name="owningObject">The object that owns the <paramref name="member" />.</param>
		/// <param name="member">The given member.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" />, <paramref name="owningObject" />, or <paramref name="member" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060009A1 RID: 2465 RVA: 0x00011620 File Offset: 0x0000F820
		public override void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member)
		{
			this.SerializeMemberCore(store, owningObject, member, true);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001162C File Offset: 0x0000F82C
		private void SerializeCore(SerializationStore store, object value, bool absolute)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (store == null)
			{
				throw new InvalidOperationException("store type unsupported");
			}
			codeDomSerializationStore.AddObject(value, absolute);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00011668 File Offset: 0x0000F868
		private void SerializeMemberCore(SerializationStore store, object owningObject, MemberDescriptor member, bool absolute)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException("store type unsupported");
			}
			codeDomSerializationStore.AddMember(owningObject, member, absolute);
		}

		// Token: 0x04000235 RID: 565
		private IServiceProvider _provider;

		// Token: 0x02000142 RID: 322
		[Serializable]
		private class CodeDomSerializationStore : SerializationStore, ISerializable
		{
			// Token: 0x060009A4 RID: 2468 RVA: 0x000116BC File Offset: 0x0000F8BC
			internal CodeDomSerializationStore()
				: this(null)
			{
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x000116C5 File Offset: 0x0000F8C5
			internal CodeDomSerializationStore(IServiceProvider provider)
			{
				this._provider = provider;
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x000116D4 File Offset: 0x0000F8D4
			private CodeDomSerializationStore(SerializationInfo info, StreamingContext context)
			{
				this._objects = (Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry>)info.GetValue("objects", typeof(Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry>));
				this._closed = (bool)info.GetValue("closed", typeof(bool));
			}

			// Token: 0x060009A7 RID: 2471 RVA: 0x00011727 File Offset: 0x0000F927
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("objects", this._objects);
				info.AddValue("closed", this._closed);
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x0001174B File Offset: 0x0000F94B
			public override void Close()
			{
				if (!this._closed)
				{
					this.Serialize(this._provider);
					this._closed = true;
				}
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x00011768 File Offset: 0x0000F968
			internal static CodeDomComponentSerializationService.CodeDomSerializationStore Load(Stream stream)
			{
				return new BinaryFormatter().Deserialize(stream) as CodeDomComponentSerializationService.CodeDomSerializationStore;
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x0001177A File Offset: 0x0000F97A
			public override void Save(Stream stream)
			{
				this.Close();
				new BinaryFormatter().Serialize(stream, this);
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x00011790 File Offset: 0x0000F990
			private void Serialize(IServiceProvider provider)
			{
				if (provider == null || this._objects == null)
				{
					return;
				}
				CodeDomComponentSerializationService.CodeDomSerializationStore.InstanceRedirectorDesignerSerializationManager instanceRedirectorDesignerSerializationManager = new CodeDomComponentSerializationService.CodeDomSerializationStore.InstanceRedirectorDesignerSerializationManager(provider, null, false);
				((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).AddSerializationProvider(CodeDomSerializationProvider.Instance);
				IDisposable disposable = instanceRedirectorDesignerSerializationManager.CreateSession();
				foreach (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry objectEntry in this._objects.Values)
				{
					if (objectEntry.IsEntireObject)
					{
						CodeDomSerializer codeDomSerializer = (CodeDomSerializer)((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).GetSerializer(objectEntry.Type, typeof(CodeDomSerializer));
						if (codeDomSerializer != null)
						{
							object obj;
							if (objectEntry.Absolute)
							{
								obj = codeDomSerializer.SerializeAbsolute(instanceRedirectorDesignerSerializationManager, objectEntry.Instance);
							}
							else
							{
								obj = codeDomSerializer.Serialize(instanceRedirectorDesignerSerializationManager, objectEntry.Instance);
							}
							objectEntry.Serialized = obj;
						}
					}
					else
					{
						foreach (CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry memberEntry in objectEntry.Members.Values)
						{
							CodeDomSerializer codeDomSerializer2 = (CodeDomSerializer)((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).GetSerializer(objectEntry.Type, typeof(CodeDomSerializer));
							if (codeDomSerializer2 != null)
							{
								object obj2;
								if (memberEntry.Absolute)
								{
									obj2 = codeDomSerializer2.SerializeMemberAbsolute(instanceRedirectorDesignerSerializationManager, objectEntry.Instance, memberEntry.Descriptor);
								}
								else
								{
									obj2 = codeDomSerializer2.SerializeMember(instanceRedirectorDesignerSerializationManager, objectEntry.Instance, memberEntry.Descriptor);
								}
								memberEntry.Serialized = obj2;
							}
						}
					}
				}
				this._errors = instanceRedirectorDesignerSerializationManager.Errors;
				disposable.Dispose();
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x00011948 File Offset: 0x0000FB48
			internal void AddObject(object instance, bool absolute)
			{
				if (this._closed)
				{
					throw new InvalidOperationException("store is closed");
				}
				if (this._objects == null)
				{
					this._objects = new Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry>();
				}
				string name = this.GetName(instance);
				if (!this._objects.ContainsKey(name))
				{
					CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry objectEntry = new CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry(instance, name);
					objectEntry.Absolute = absolute;
					objectEntry.IsEntireObject = true;
					this._objects[name] = objectEntry;
				}
			}

			// Token: 0x060009AD RID: 2477 RVA: 0x000119B4 File Offset: 0x0000FBB4
			internal void AddMember(object owner, MemberDescriptor member, bool absolute)
			{
				if (this._closed)
				{
					throw new InvalidOperationException("store is closed");
				}
				if (member == null)
				{
					throw new ArgumentNullException("member");
				}
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				if (this._objects == null)
				{
					this._objects = new Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry>();
				}
				string name = this.GetName(owner);
				if (!this._objects.ContainsKey(name))
				{
					this._objects.Add(name, new CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry(owner, name));
				}
				CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry memberEntry = new CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry(member);
				memberEntry.Absolute = absolute;
				this._objects[name].Members[member.Name] = memberEntry;
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x00011A58 File Offset: 0x0000FC58
			private string GetName(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				IComponent component = value as IComponent;
				string text;
				if (component != null && component.Site != null)
				{
					if (component.Site is INestedSite)
					{
						text = ((INestedSite)component.Site).FullName;
					}
					else
					{
						text = ((component.Site != null) ? component.Site.Name : null);
					}
				}
				else if (value is MemberDescriptor)
				{
					text = ((MemberDescriptor)value).Name;
				}
				else
				{
					text = value.GetHashCode().ToString();
				}
				return text;
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x00011AE8 File Offset: 0x0000FCE8
			internal ICollection Deserialize(IServiceProvider provider, IContainer container, bool validateRecycledTypes, bool applyDefaults)
			{
				List<object> list = new List<object>();
				if (provider == null || this._objects == null)
				{
					return list;
				}
				this._provider = provider;
				CodeDomComponentSerializationService.CodeDomSerializationStore.InstanceRedirectorDesignerSerializationManager instanceRedirectorDesignerSerializationManager = new CodeDomComponentSerializationService.CodeDomSerializationStore.InstanceRedirectorDesignerSerializationManager(provider, container, validateRecycledTypes);
				((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).AddSerializationProvider(CodeDomSerializationProvider.Instance);
				IDisposable disposable = instanceRedirectorDesignerSerializationManager.CreateSession();
				foreach (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry objectEntry in this._objects.Values)
				{
					list.Add(this.DeserializeEntry(instanceRedirectorDesignerSerializationManager, objectEntry));
				}
				this._errors = instanceRedirectorDesignerSerializationManager.Errors;
				disposable.Dispose();
				return list;
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x00011B94 File Offset: 0x0000FD94
			private object DeserializeEntry(IDesignerSerializationManager manager, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry objectEntry)
			{
				object obj = null;
				if (objectEntry.IsEntireObject)
				{
					CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(objectEntry.Type, typeof(CodeDomSerializer));
					if (codeDomSerializer != null)
					{
						obj = codeDomSerializer.Deserialize(manager, objectEntry.Serialized);
						string name = manager.GetName(obj);
						if (name != objectEntry.Name)
						{
							objectEntry.Name = name;
						}
					}
				}
				else
				{
					foreach (CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry memberEntry in objectEntry.Members.Values)
					{
						CodeDomSerializer codeDomSerializer2 = (CodeDomSerializer)manager.GetSerializer(objectEntry.Type, typeof(CodeDomSerializer));
						if (codeDomSerializer2 != null)
						{
							codeDomSerializer2.Deserialize(manager, memberEntry.Serialized);
						}
					}
				}
				return obj;
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00011C74 File Offset: 0x0000FE74
			public override ICollection Errors
			{
				get
				{
					if (this._errors == null)
					{
						this._errors = new object[0];
					}
					return this._errors;
				}
			}

			// Token: 0x04000236 RID: 566
			private bool _closed;

			// Token: 0x04000237 RID: 567
			private Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectEntry> _objects;

			// Token: 0x04000238 RID: 568
			private IServiceProvider _provider;

			// Token: 0x04000239 RID: 569
			private ICollection _errors;

			// Token: 0x02000143 RID: 323
			[Serializable]
			private class Entry
			{
				// Token: 0x060009B2 RID: 2482 RVA: 0x00002352 File Offset: 0x00000552
				protected Entry()
				{
				}

				// Token: 0x060009B3 RID: 2483 RVA: 0x00011C90 File Offset: 0x0000FE90
				public Entry(string name)
				{
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					this._name = name;
					this._isSerialized = false;
					this._absolute = false;
				}

				// Token: 0x1700020A RID: 522
				// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00011CBB File Offset: 0x0000FEBB
				// (set) Token: 0x060009B5 RID: 2485 RVA: 0x00011CC3 File Offset: 0x0000FEC3
				public bool IsSerialized
				{
					get
					{
						return this._isSerialized;
					}
					set
					{
						this._isSerialized = value;
					}
				}

				// Token: 0x1700020B RID: 523
				// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00011CCC File Offset: 0x0000FECC
				// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00011CD4 File Offset: 0x0000FED4
				public object Serialized
				{
					get
					{
						return this._serialized;
					}
					set
					{
						this._serialized = value;
						this._isSerialized = true;
					}
				}

				// Token: 0x1700020C RID: 524
				// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00011CE4 File Offset: 0x0000FEE4
				// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00011CEC File Offset: 0x0000FEEC
				public bool Absolute
				{
					get
					{
						return this._absolute;
					}
					set
					{
						this._absolute = value;
					}
				}

				// Token: 0x1700020D RID: 525
				// (get) Token: 0x060009BA RID: 2490 RVA: 0x00011CF5 File Offset: 0x0000FEF5
				// (set) Token: 0x060009BB RID: 2491 RVA: 0x00011CFD File Offset: 0x0000FEFD
				public string Name
				{
					get
					{
						return this._name;
					}
					set
					{
						this._name = value;
					}
				}

				// Token: 0x0400023A RID: 570
				private bool _isSerialized;

				// Token: 0x0400023B RID: 571
				private object _serialized;

				// Token: 0x0400023C RID: 572
				private bool _absolute;

				// Token: 0x0400023D RID: 573
				private string _name;
			}

			// Token: 0x02000144 RID: 324
			[Serializable]
			private class MemberEntry : CodeDomComponentSerializationService.CodeDomSerializationStore.Entry
			{
				// Token: 0x060009BC RID: 2492 RVA: 0x00011D06 File Offset: 0x0000FF06
				protected MemberEntry()
				{
				}

				// Token: 0x060009BD RID: 2493 RVA: 0x00011D0E File Offset: 0x0000FF0E
				public MemberEntry(MemberDescriptor descriptor)
				{
					if (descriptor == null)
					{
						throw new ArgumentNullException("descriptor");
					}
					this._descriptor = descriptor;
					base.Name = descriptor.Name;
				}

				// Token: 0x1700020E RID: 526
				// (get) Token: 0x060009BE RID: 2494 RVA: 0x00011D37 File Offset: 0x0000FF37
				// (set) Token: 0x060009BF RID: 2495 RVA: 0x00011D3F File Offset: 0x0000FF3F
				public MemberDescriptor Descriptor
				{
					get
					{
						return this._descriptor;
					}
					set
					{
						this._descriptor = value;
					}
				}

				// Token: 0x0400023E RID: 574
				private MemberDescriptor _descriptor;
			}

			// Token: 0x02000145 RID: 325
			[Serializable]
			private class ObjectEntry : CodeDomComponentSerializationService.CodeDomSerializationStore.Entry
			{
				// Token: 0x060009C0 RID: 2496 RVA: 0x00011D06 File Offset: 0x0000FF06
				protected ObjectEntry()
				{
				}

				// Token: 0x060009C1 RID: 2497 RVA: 0x00011D48 File Offset: 0x0000FF48
				public ObjectEntry(object instance, string name)
					: base(name)
				{
					if (instance == null)
					{
						throw new ArgumentNullException("instance");
					}
					this._instance = instance;
					this._type = instance.GetType();
					this._entireObject = false;
				}

				// Token: 0x1700020F RID: 527
				// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00011D79 File Offset: 0x0000FF79
				public Type Type
				{
					get
					{
						return this._type;
					}
				}

				// Token: 0x17000210 RID: 528
				// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00011D81 File Offset: 0x0000FF81
				// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00011D89 File Offset: 0x0000FF89
				public object Instance
				{
					get
					{
						return this._instance;
					}
					set
					{
						this._instance = value;
						if (value != null)
						{
							this._type = value.GetType();
						}
					}
				}

				// Token: 0x17000211 RID: 529
				// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00011DA1 File Offset: 0x0000FFA1
				// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00011DBC File Offset: 0x0000FFBC
				public Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry> Members
				{
					get
					{
						if (this._members == null)
						{
							this._members = new Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry>();
						}
						return this._members;
					}
					set
					{
						this._members = value;
					}
				}

				// Token: 0x17000212 RID: 530
				// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00011DC5 File Offset: 0x0000FFC5
				// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00011DCD File Offset: 0x0000FFCD
				public bool IsEntireObject
				{
					get
					{
						return this._entireObject;
					}
					set
					{
						this._entireObject = value;
					}
				}

				// Token: 0x0400023F RID: 575
				private Type _type;

				// Token: 0x04000240 RID: 576
				[NonSerialized]
				private object _instance;

				// Token: 0x04000241 RID: 577
				private Dictionary<string, CodeDomComponentSerializationService.CodeDomSerializationStore.MemberEntry> _members;

				// Token: 0x04000242 RID: 578
				private bool _entireObject;
			}

			// Token: 0x02000146 RID: 326
			private class InstanceRedirectorDesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
			{
				// Token: 0x060009C9 RID: 2505 RVA: 0x00011DD8 File Offset: 0x0000FFD8
				public InstanceRedirectorDesignerSerializationManager(IServiceProvider provider, IContainer container, bool validateRecycledTypes)
				{
					if (provider == null)
					{
						throw new ArgumentNullException("provider");
					}
					DesignerSerializationManager designerSerializationManager = new DesignerSerializationManager(provider);
					designerSerializationManager.PreserveNames = false;
					if (container != null)
					{
						designerSerializationManager.Container = container;
					}
					designerSerializationManager.ValidateRecycledTypes = validateRecycledTypes;
					this._manager = designerSerializationManager;
				}

				// Token: 0x060009CA RID: 2506 RVA: 0x00011E1F File Offset: 0x0001001F
				public IDisposable CreateSession()
				{
					return this._manager.CreateSession();
				}

				// Token: 0x17000213 RID: 531
				// (get) Token: 0x060009CB RID: 2507 RVA: 0x00011E2C File Offset: 0x0001002C
				public IList Errors
				{
					get
					{
						return this._manager.Errors;
					}
				}

				// Token: 0x060009CC RID: 2508 RVA: 0x00011E39 File Offset: 0x00010039
				object IServiceProvider.GetService(Type service)
				{
					return ((IServiceProvider)this._manager).GetService(service);
				}

				// Token: 0x060009CD RID: 2509 RVA: 0x00011E47 File Offset: 0x00010047
				void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this._manager).AddSerializationProvider(provider);
				}

				// Token: 0x060009CE RID: 2510 RVA: 0x00011E55 File Offset: 0x00010055
				void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this._manager).RemoveSerializationProvider(provider);
				}

				// Token: 0x060009CF RID: 2511 RVA: 0x00011E64 File Offset: 0x00010064
				object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
				{
					object obj = ((IDesignerSerializationManager)this._manager).CreateInstance(type, arguments, name, addToContainer);
					string name2 = ((IDesignerSerializationManager)this._manager).GetName(obj);
					if (name2 != name)
					{
						if (this._nameMap == null)
						{
							this._nameMap = new Dictionary<string, string>();
						}
						this._nameMap[name] = name2;
					}
					return obj;
				}

				// Token: 0x060009D0 RID: 2512 RVA: 0x00011EB9 File Offset: 0x000100B9
				object IDesignerSerializationManager.GetInstance(string name)
				{
					if (this._nameMap != null && this._nameMap.ContainsKey(name))
					{
						return ((IDesignerSerializationManager)this._manager).GetInstance(this._nameMap[name]);
					}
					return ((IDesignerSerializationManager)this._manager).GetInstance(name);
				}

				// Token: 0x060009D1 RID: 2513 RVA: 0x00011EF5 File Offset: 0x000100F5
				Type IDesignerSerializationManager.GetType(string name)
				{
					return ((IDesignerSerializationManager)this._manager).GetType(name);
				}

				// Token: 0x060009D2 RID: 2514 RVA: 0x00011F03 File Offset: 0x00010103
				object IDesignerSerializationManager.GetSerializer(Type type, Type serializerType)
				{
					return ((IDesignerSerializationManager)this._manager).GetSerializer(type, serializerType);
				}

				// Token: 0x060009D3 RID: 2515 RVA: 0x00011F12 File Offset: 0x00010112
				string IDesignerSerializationManager.GetName(object instance)
				{
					return ((IDesignerSerializationManager)this._manager).GetName(instance);
				}

				// Token: 0x060009D4 RID: 2516 RVA: 0x00011F20 File Offset: 0x00010120
				void IDesignerSerializationManager.SetName(object instance, string name)
				{
					((IDesignerSerializationManager)this._manager).SetName(instance, name);
				}

				// Token: 0x060009D5 RID: 2517 RVA: 0x00011F2F File Offset: 0x0001012F
				void IDesignerSerializationManager.ReportError(object error)
				{
					((IDesignerSerializationManager)this._manager).ReportError(error);
				}

				// Token: 0x17000214 RID: 532
				// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00011F3D File Offset: 0x0001013D
				ContextStack IDesignerSerializationManager.Context
				{
					get
					{
						return ((IDesignerSerializationManager)this._manager).Context;
					}
				}

				// Token: 0x17000215 RID: 533
				// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00011F4A File Offset: 0x0001014A
				PropertyDescriptorCollection IDesignerSerializationManager.Properties
				{
					get
					{
						return ((IDesignerSerializationManager)this._manager).Properties;
					}
				}

				// Token: 0x14000038 RID: 56
				// (add) Token: 0x060009D8 RID: 2520 RVA: 0x00011F57 File Offset: 0x00010157
				// (remove) Token: 0x060009D9 RID: 2521 RVA: 0x00011F65 File Offset: 0x00010165
				event EventHandler IDesignerSerializationManager.SerializationComplete
				{
					add
					{
						((IDesignerSerializationManager)this._manager).SerializationComplete += value;
					}
					remove
					{
						((IDesignerSerializationManager)this._manager).SerializationComplete -= value;
					}
				}

				// Token: 0x14000039 RID: 57
				// (add) Token: 0x060009DA RID: 2522 RVA: 0x00011F73 File Offset: 0x00010173
				// (remove) Token: 0x060009DB RID: 2523 RVA: 0x00011F81 File Offset: 0x00010181
				event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
				{
					add
					{
						((IDesignerSerializationManager)this._manager).ResolveName += value;
					}
					remove
					{
						((IDesignerSerializationManager)this._manager).ResolveName -= value;
					}
				}

				// Token: 0x04000243 RID: 579
				private DesignerSerializationManager _manager;

				// Token: 0x04000244 RID: 580
				private Dictionary<string, string> _nameMap;
			}
		}
	}
}
