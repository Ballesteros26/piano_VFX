using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020006DB RID: 1755
	internal sealed class ObjectHolder
	{
		// Token: 0x06004A3C RID: 19004 RVA: 0x0010A39E File Offset: 0x0010859E
		internal ObjectHolder(long objID)
			: this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0010A3B0 File Offset: 0x001085B0
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0010A46C File Offset: 0x0010866C
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0010A4F5 File Offset: 0x001086F5
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0010A505 File Offset: 0x00108705
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0010A525 File Offset: 0x00108725
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0010A534 File Offset: 0x00108734
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0010A574 File Offset: 0x00108774
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0010A59F File Offset: 0x0010879F
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0010A5C0 File Offset: 0x001087C0
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0010A64B File Offset: 0x0010884B
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0010A654 File Offset: 0x00108854
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0010A6D4 File Offset: 0x001088D4
		// (set) Token: 0x06004A49 RID: 19017 RVA: 0x0010A6E1 File Offset: 0x001088E1
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x0010A704 File Offset: 0x00108904
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x0010A711 File Offset: 0x00108911
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0010A71E File Offset: 0x0010891E
		// (set) Token: 0x06004A4D RID: 19021 RVA: 0x0010A752 File Offset: 0x00108952
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0010A769 File Offset: 0x00108969
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0010A776 File Offset: 0x00108976
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0010A783 File Offset: 0x00108983
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x0010A7A9 File Offset: 0x001089A9
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0010A7C5 File Offset: 0x001089C5
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x0010A7CD File Offset: 0x001089CD
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0010A7DC File Offset: 0x001089DC
		// (set) Token: 0x06004A55 RID: 19029 RVA: 0x0010A7E4 File Offset: 0x001089E4
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0010A7ED File Offset: 0x001089ED
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0010A7F8 File Offset: 0x001089F8
		// (set) Token: 0x06004A58 RID: 19032 RVA: 0x0010A800 File Offset: 0x00108A00
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x0010A809 File Offset: 0x00108A09
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0010A811 File Offset: 0x00108A11
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x0010A84E File Offset: 0x00108A4E
		// (set) Token: 0x06004A5C RID: 19036 RVA: 0x0010A856 File Offset: 0x00108A56
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0010A85F File Offset: 0x00108A5F
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0010A867 File Offset: 0x00108A67
		// (set) Token: 0x06004A5F RID: 19039 RVA: 0x0010A86F File Offset: 0x00108A6F
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x0010A878 File Offset: 0x00108A78
		// (set) Token: 0x06004A61 RID: 19041 RVA: 0x0010A89F File Offset: 0x00108A9F
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0010A8C9 File Offset: 0x00108AC9
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x0010A8D1 File Offset: 0x00108AD1
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x0010A8E6 File Offset: 0x00108AE6
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x040026CA RID: 9930
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x040026CB RID: 9931
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x040026CC RID: 9932
		internal const int HAS_SURROGATE = 4;

		// Token: 0x040026CD RID: 9933
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x040026CE RID: 9934
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x040026CF RID: 9935
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x040026D0 RID: 9936
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x040026D1 RID: 9937
		private object m_object;

		// Token: 0x040026D2 RID: 9938
		internal long m_id;

		// Token: 0x040026D3 RID: 9939
		private int m_missingElementsRemaining;

		// Token: 0x040026D4 RID: 9940
		private int m_missingDecendents;

		// Token: 0x040026D5 RID: 9941
		internal SerializationInfo m_serInfo;

		// Token: 0x040026D6 RID: 9942
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x040026D7 RID: 9943
		internal FixupHolderList m_missingElements;

		// Token: 0x040026D8 RID: 9944
		internal LongList m_dependentObjects;

		// Token: 0x040026D9 RID: 9945
		internal ObjectHolder m_next;

		// Token: 0x040026DA RID: 9946
		internal int m_flags;

		// Token: 0x040026DB RID: 9947
		private bool m_markForFixupWhenAvailable;

		// Token: 0x040026DC RID: 9948
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x040026DD RID: 9949
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x040026DE RID: 9950
		private bool m_reachable;
	}
}
