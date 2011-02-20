﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tik4Net
{
    /// <summary>
    /// Collection of Mikrotik entity properties (attributes).
    /// Supports <see cref="IsModified"/>.
    /// </summary>
    public class TikPropertyList
    {
        private Dictionary<string, TikPropertyItem> items = new Dictionary<string, TikPropertyItem>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets a value indicating whether this instance is modified (any property (attribute) has been modified).
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance (any property=attribute) is modified; otherwise, <c>false</c>.
        /// </value>
        public bool IsModified
        {
            get
            {
                foreach (KeyValuePair<string, TikPropertyItem> pair in items)
                {
                    if (pair.Value.IsModified)
                        return true;
                }
                return false;
            }
        }

        private TikPropertyItem GetOrCreateItem(string propertyName)
        {
            TikPropertyItem result;
            if (!items.TryGetValue(propertyName, out result))
            {
                result = new TikPropertyItem();
                items.Add(propertyName, result);
            }

            return result;
        }

        /// <summary>
        /// Sets the property (attribute) value (<see cref="IsModified"/> becomes true).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void SetAttribute(string attributeName, string value)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            item.SetValue(value);
        }

        /// <summary>
        /// Sets the attribute value (<see cref="IsModified"/> becomes true).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void SetAttribute(string attributeName, long? value)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            item.SetValue(value);
        }

        /// <summary>
        /// Sets the attribute value (<see cref="IsModified"/> becomes true).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void SetAttribute(string attributeName, bool? value)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            item.SetValue(value);
        }

        /// <summary>
        /// Creates the new (attribute could not exists before) attribute value 
        /// (<see cref="IsModified"/> of attribute is false).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void CreateAttribute(string attributeName, string value)
        {
            TikPropertyItem item = new TikPropertyItem(value);
            items.Add(attributeName, item);
        }

        /// <summary>
        /// Creates the new (attribute could not exists before) attribute value 
        /// (<see cref="IsModified"/> of attribute is false).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void CreateAttribute(string attributeName, bool? value)
        {
            TikPropertyItem item = new TikPropertyItem(value);
            items.Add(attributeName, item);
        }

        /// <summary>
        /// Creates the new (attribute could not exists before) attribute value 
        /// (<see cref="IsModified"/> of attribute is false).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void CreateAttribute(string attributeName, long? value)
        {
            TikPropertyItem item = new TikPropertyItem(value);
            items.Add(attributeName, item);
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as string (or returns 
        /// default value if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or default value.</returns>
        public string GetAsString(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            return item.GetAsString();
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as bool (or returns 
        /// default value if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or default value.</returns>
        public bool GetAsBoolean(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            return item.GetAsBool();
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as Int64 (or returns 
        /// default value if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or default value.</returns>
        public long GetAsInt64(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            return item.GetAsInt64();
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as string (or returns 
        /// null if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or null.</returns>
        public string GetAsStringOrNull(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            if (item.HasValue)
                return item.GetAsString();
            else
                return null;
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as bool (or returns 
        /// null if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or null.</returns>
        public bool? GetAsBooleanOrNull(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            if (item.HasValue)
                return item.GetAsBool();
            else
                return null;
        }

        /// <summary>
        /// Gets attribute <paramref name="attributeName"/> as Int64 (or returns 
        /// null if does not exists).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value or null.</returns>
        public long? GetAsInt64OrNull(string attributeName)
        {
            TikPropertyItem item = GetOrCreateItem(attributeName);
            if (item.HasValue)
                return item.GetAsInt64();
            else
                return null;
        }

        /// <summary>
        /// Determines whether contains attribute with the specified attribute name and attribute has value.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>
        /// 	<c>true</c> if contains attribute the specified attribute name and attribute has value; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsAttributeWithValue(string attributeName)
        {
            TikPropertyItem item;
            if (!items.TryGetValue(attributeName, out item))
                return false;
            else
                return item.HasValue;
        }

        public void GetAttributeState(string attributeName, out bool found, out bool modified, out bool hasValue)
        {
            TikPropertyItem item;
            if (!items.TryGetValue(attributeName, out item))
            {
                found = false;
                modified = false;
                hasValue = false;
            }
            else
            {
                found = true;
                hasValue = item.HasValue;
                modified = item.IsModified;
            }
        }

        public bool IsDataEqual(TikPropertyList tikPropertyList)
        {
            foreach (KeyValuePair<string, TikPropertyItem> pair in items)
            {
                TikPropertyItem item2;
                if (!tikPropertyList.items.TryGetValue(pair.Key, out item2))
                    return false;
                else
                {
                    if (!pair.Value.IsDataEqual(item2))
                        return false;
                }
            }
            return true;
        }
    }
}