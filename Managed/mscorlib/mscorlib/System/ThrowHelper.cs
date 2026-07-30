using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000D5 RID: 213
	internal static class ThrowHelper
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x0002500E File Offset: 0x0002320E
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00025016 File Offset: 0x00023216
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0002502A File Offset: 0x0002322A
		internal static void ThrowArrayTypeMismatchException_ArrayTypeMustBeExactMatch(Type type)
		{
			throw ThrowHelper.CreateArrayTypeMismatchException_ArrayTypeMustBeExactMatch(type);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00025032 File Offset: 0x00023232
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException_ArrayTypeMustBeExactMatch(Type type)
		{
			return new ArrayTypeMismatchException(SR.Format("The array type must be exactly {0}.", type));
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00025044 File Offset: 0x00023244
		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0002504C File Offset: 0x0002324C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", type));
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0002505E File Offset: 0x0002325E
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00025065 File Offset: 0x00023265
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException("Destination is too short.");
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00025071 File Offset: 0x00023271
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00025078 File Offset: 0x00023278
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0002507F File Offset: 0x0002327F
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00025087 File Offset: 0x00023287
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0002509B File Offset: 0x0002329B
		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000250A2 File Offset: 0x000232A2
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException("Release all references before disposing this instance.");
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000250AE File Offset: 0x000232AE
		internal static void ThrowObjectDisposedException_MemoryDisposed(string objectName)
		{
			throw ThrowHelper.CreateObjectDisposedException_MemoryDisposed(objectName);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000250B6 File Offset: 0x000232B6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_MemoryDisposed(string objectName)
		{
			return new ObjectDisposedException(objectName, "Memory<T> has been disposed.");
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000250C3 File Offset: 0x000232C3
		internal static void ThrowArgumentOutOfRangeException()
		{
			ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000250CE File Offset: 0x000232CE
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[] { key, targetType }), "key");
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000250F2 File Offset: 0x000232F2
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[] { value, targetType }), "value");
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00025116 File Offset: 0x00023316
		internal static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0002511D File Offset: 0x0002331D
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002512F File Offset: 0x0002332F
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00025147 File Offset: 0x00023347
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), string.Empty);
			}
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00025177 File Offset: 0x00023377
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw new InvalidOperationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00025189 File Offset: 0x00023389
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002519B File Offset: 0x0002339B
		internal static void ThrowSecurityException(ExceptionResource resource)
		{
			throw new SecurityException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000251AD File Offset: 0x000233AD
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000251BF File Offset: 0x000233BF
		internal static void ThrowUnauthorizedAccessException(ExceptionResource resource)
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000251D1 File Offset: 0x000233D1
		internal static void ThrowObjectDisposedException(string objectName, ExceptionResource resource)
		{
			throw new ObjectDisposedException(objectName, Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000251E4 File Offset: 0x000233E4
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0002520C File Offset: 0x0002340C
		internal static string GetArgumentName(ExceptionArgument argument)
		{
			string text;
			switch (argument)
			{
			case ExceptionArgument.obj:
				text = "obj";
				break;
			case ExceptionArgument.dictionary:
				text = "dictionary";
				break;
			case ExceptionArgument.dictionaryCreationThreshold:
				text = "dictionaryCreationThreshold";
				break;
			case ExceptionArgument.array:
				text = "array";
				break;
			case ExceptionArgument.info:
				text = "info";
				break;
			case ExceptionArgument.key:
				text = "key";
				break;
			case ExceptionArgument.collection:
				text = "collection";
				break;
			case ExceptionArgument.list:
				text = "list";
				break;
			case ExceptionArgument.match:
				text = "match";
				break;
			case ExceptionArgument.converter:
				text = "converter";
				break;
			case ExceptionArgument.queue:
				text = "queue";
				break;
			case ExceptionArgument.stack:
				text = "stack";
				break;
			case ExceptionArgument.capacity:
				text = "capacity";
				break;
			case ExceptionArgument.index:
				text = "index";
				break;
			case ExceptionArgument.startIndex:
				text = "startIndex";
				break;
			case ExceptionArgument.value:
				text = "value";
				break;
			case ExceptionArgument.count:
				text = "count";
				break;
			case ExceptionArgument.arrayIndex:
				text = "arrayIndex";
				break;
			case ExceptionArgument.name:
				text = "name";
				break;
			case ExceptionArgument.mode:
				text = "mode";
				break;
			case ExceptionArgument.item:
				text = "item";
				break;
			case ExceptionArgument.options:
				text = "options";
				break;
			case ExceptionArgument.view:
				text = "view";
				break;
			case ExceptionArgument.sourceBytesToCopy:
				text = "sourceBytesToCopy";
				break;
			default:
				return string.Empty;
			}
			return text;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00025368 File Offset: 0x00023568
		internal static string GetResourceName(ExceptionResource resource)
		{
			string text;
			switch (resource)
			{
			case ExceptionResource.Argument_ImplementIComparable:
				text = "At least one object must implement IComparable.";
				break;
			case ExceptionResource.Argument_InvalidType:
				text = "The type of arguments passed into generic comparer methods is invalid.";
				break;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				text = "Type of argument is not compatible with the generic comparer.";
				break;
			case ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck:
				text = "The specified RegistryKeyPermissionCheck value is invalid.";
				break;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				text = "Non-negative number required.";
				break;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				text = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";
				break;
			case ExceptionResource.Arg_NonZeroLowerBound:
				text = "The lower bound of target array must be zero.";
				break;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				text = "Only single dimensional arrays are supported for the requested action.";
				break;
			case ExceptionResource.Arg_RegKeyDelHive:
				text = "Cannot delete a registry hive's subtree.";
				break;
			case ExceptionResource.Arg_RegKeyStrLenBug:
				text = "Registry key names should not be greater than 255 characters.";
				break;
			case ExceptionResource.Arg_RegSetStrArrNull:
				text = "RegistryKey.SetValue does not allow a String[] that contains a null String reference.";
				break;
			case ExceptionResource.Arg_RegSetMismatchedKind:
				text = "The type of the value object did not match the specified RegistryValueKind or the object could not be properly converted.";
				break;
			case ExceptionResource.Arg_RegSubKeyAbsent:
				text = "Cannot delete a subkey tree because the subkey does not exist.";
				break;
			case ExceptionResource.Arg_RegSubKeyValueAbsent:
				text = "No value exists with that name.";
				break;
			case ExceptionResource.Argument_AddingDuplicate:
				text = "An item with the same key has already been added.";
				break;
			case ExceptionResource.Serialization_InvalidOnDeser:
				text = "OnDeserialization method was called while the object was not being deserialized.";
				break;
			case ExceptionResource.Serialization_MissingKeys:
				text = "The Keys for this Hashtable are missing.";
				break;
			case ExceptionResource.Serialization_NullKey:
				text = "One of the serialized keys is null.";
				break;
			case ExceptionResource.Argument_InvalidArrayType:
				text = "Target array type is not compatible with the type of items in the collection.";
				break;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				text = "Mutating a key collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				text = "Mutating a value collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				text = "capacity was less than the current size.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Index:
				text = "Index was out of range. Must be non-negative and less than the size of the collection.";
				break;
			case ExceptionResource.Argument_InvalidOffLen:
				text = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";
				break;
			case ExceptionResource.Argument_ItemNotExist:
				text = "The specified item does not exist in this KeyedCollection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Count:
				text = "Count must be positive and count must refer to a location within the string/array/collection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_InvalidThreshold:
				text = "The specified threshold for creating dictionary is out of range.";
				break;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				text = "Index must be within the bounds of the List.";
				break;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				text = "Collection is read-only.";
				break;
			case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				text = "Removal is an invalid operation for Stack or Queue.";
				break;
			case ExceptionResource.InvalidOperation_EmptyQueue:
				text = "Queue empty.";
				break;
			case ExceptionResource.InvalidOperation_EnumOpCantHappen:
				text = "Enumeration has either not started or has already finished.";
				break;
			case ExceptionResource.InvalidOperation_EnumFailedVersion:
				text = "Collection was modified; enumeration operation may not execute.";
				break;
			case ExceptionResource.InvalidOperation_EmptyStack:
				text = "Stack empty.";
				break;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				text = "Larger than collection size.";
				break;
			case ExceptionResource.InvalidOperation_EnumNotStarted:
				text = "Enumeration has not started. Call MoveNext.";
				break;
			case ExceptionResource.InvalidOperation_EnumEnded:
				text = "Enumeration already finished.";
				break;
			case ExceptionResource.NotSupported_SortedListNestedWrite:
				text = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";
				break;
			case ExceptionResource.InvalidOperation_NoValue:
				text = "Nullable object must have a value.";
				break;
			case ExceptionResource.InvalidOperation_RegRemoveSubKey:
				text = "Registry key has subkeys and recursive removes are not supported by this method.";
				break;
			case ExceptionResource.Security_RegistryPermission:
				text = "Requested registry access is not allowed.";
				break;
			case ExceptionResource.UnauthorizedAccess_RegistryNoWrite:
				text = "Cannot write to the registry key.";
				break;
			case ExceptionResource.ObjectDisposed_RegKeyClosed:
				text = "Cannot access a closed registry key.";
				break;
			case ExceptionResource.NotSupported_InComparableType:
				text = "A type must implement IComparable<T> or IComparable to support comparison.";
				break;
			case ExceptionResource.Argument_InvalidRegistryOptionsCheck:
				text = "The specified RegistryOptions value is invalid.";
				break;
			case ExceptionResource.Argument_InvalidRegistryViewCheck:
				text = "The specified RegistryView value is invalid.";
				break;
			default:
				return string.Empty;
			}
			return text;
		}
	}
}
