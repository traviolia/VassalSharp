// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Portable.Xml.Serialization
{
    using System.Reflection;
    using System.Collections;
    using System.IO;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class XmlSerializerNamespaces
    {
        private Dictionary<string, string> _namespaces = null;

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlSerializerNamespaces()
        {
        }


        /// <internalonly/>
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
        {
            _namespaces = new Dictionary<string, string>(namespaces.Namespaces);
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
        {
            for (int i = 0; i < namespaces.Length; i++)
            {
                XmlQualifiedName qname = namespaces[i];
                Add(qname.Name, qname.Namespace);
            }
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Add(string prefix, string ns)
        {
            // parameter value check
            if (prefix != null && prefix.Length > 0)
                XmlConvert.VerifyNCName(prefix);

            if (ns != null && ns.Length > 0)
                ToUri(ns);
            AddInternal(prefix, ns);
        }

        private static Uri ToUri(string s)
        {
            if (s != null && s.Length > 0)
            { //string.Empty is a valid uri but not "   "
                s = s.Trim(new char[] { ' ', '\t', '\n', '\r' });
                if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
                {
                    throw new FormatException(SR.Format(SR.XmlConvert_BadFormat, s, "Uri"));
                }
            }
            Uri uri;
            if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out uri))
            {
                throw new FormatException(SR.Format(SR.XmlConvert_BadFormat, s, "Uri"));
            }
            return uri;
        }

        internal void AddInternal(string prefix, string ns)
        {
            Namespaces[prefix] = ns;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlQualifiedName[] ToArray()
        {
            if (NamespaceList == null)
                return Array.Empty<XmlQualifiedName>();
            return (XmlQualifiedName[])NamespaceList.ToArray(typeof(XmlQualifiedName));
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public int Count
        {
            get { return Namespaces.Count; }
        }

        internal ArrayList NamespaceList
        {
            get
            {
                if (_namespaces == null || _namespaces.Count == 0)
                    return null;
                ArrayList namespaceList = new ArrayList();
                foreach (string key in Namespaces.Keys)
                {
                    namespaceList.Add(new XmlQualifiedName(key, (string)Namespaces[key]));
                }
                return namespaceList;
            }
        }

        internal Dictionary<string, string> Namespaces
        {
            get
            {
                if (_namespaces == null)
                    _namespaces = new Dictionary<string, string>();
                return _namespaces;
            }
            set { _namespaces = value; }
        }

        internal string LookupPrefix(string ns)
        {
            if (string.IsNullOrEmpty(ns))
                return null;
            if (_namespaces == null || _namespaces.Count == 0)
                return null;

            foreach (string prefix in _namespaces.Keys)
            {
                if (!string.IsNullOrEmpty(prefix) && _namespaces[prefix] == ns)
                {
                    return prefix;
                }
            }
            return null;
        }
    }
}

