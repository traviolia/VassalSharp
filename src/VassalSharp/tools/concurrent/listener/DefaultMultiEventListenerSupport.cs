/*
 * Copyright (c) 2010 by Joel Uckelman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace VassalSharp.tools.concurrent.listener
{
    /// <summary
    /// > A class to provide support for {@link EventListener}s.
    /// </summary>
    /// <author>  Joel Uckelman
    /// </author>
    /// <since> 3.2.0
    /// </since>
    public class DefaultMultiEventListenerSupport : MultiEventListenerSupport
    {
        protected internal object _source;
        protected ConcurrentDictionary<Type, SynchronizedCollection<object>> _listeners = new ConcurrentDictionary<Type, SynchronizedCollection<object>>();

        /// <summary>
        /// Creates a <code>DefaultMultiEventListenerSupport</code>.
        /// </summary>
        /// <param name="source">the source of events
        /// </param>
        public DefaultMultiEventListenerSupport(object source)
        {
            this._source = source;
        }

        /// <summary>{@inheritDoc} </summary>
        public void addEventListener<T>(Type c, EventListener<T> listener)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (listener == null) throw new ArgumentNullException(nameof(listener));
            if (!c.IsAssignableFrom(typeof(T))) throw new ArgumentException(nameof(listener));

            // ensure that a listener list exists for c
            if (!_listeners.TryGetValue(c, out _))
                registerType(c);

            // add the listener to the list for every subtype of c, including c
            foreach (KeyValuePair<Type, SynchronizedCollection<object>> e in _listeners)
            {
                Type other = e.Key;
                if (other.IsAssignableFrom(c))
                {
                    e.Value.Add(listener);
                }
            }
        }

        /// <summary>{@inheritDoc} </summary>
        public void removeEventListener<T>(Type c, EventListener<T> listener)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (listener == null) throw new ArgumentNullException(nameof(listener));
            if (!c.IsAssignableFrom(typeof(T))) throw new ArgumentException(nameof(listener));

            // remove the listener to the list for every subtype of c, including c
            foreach (KeyValuePair<Type, SynchronizedCollection<object>> e in _listeners)
            {
                Type other = e.Key;
                if (other.IsAssignableFrom(c))
                {
                    e.Value.Remove(listener);
                }
            }
        }

        /// <summary>{@inheritDoc} </summary>
        public bool hasEventListeners<T>()
        {
            // check for listeners for every supertype of T, include T
            foreach (KeyValuePair<Type, SynchronizedCollection<object>> e in _listeners)
            {
                Type other = e.Key;
                if (typeof(T).IsAssignableFrom(other))
                {
                    if (e.Value.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>{@inheritDoc} </summary>
        public IList<EventListener<T>> getEventListeners<T>()
        {
            List<EventListener<T>> list = (List<EventListener<T>>)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(EventListener<>).MakeGenericType(typeof(T))));

            // make a list of all listeners for every supertype of T, include T
            foreach (KeyValuePair<Type, SynchronizedCollection<object>> e in _listeners)
            {
                Type other = e.Key;
                if (typeof(T).IsAssignableFrom(other))
                {
                    foreach (object o in e.Value)
                    {
                        list.Add((EventListener<T>)o);
                    }
                }
            }
            return list;
        }

        /// <summary>{@inheritDoc} </summary>
		public virtual void notify<T>(T _event)
        {
            if (! _listeners.TryGetValue(typeof(T), out SynchronizedCollection<object> list))
                list = registerType(typeof(T));

            foreach (object l in list)
            {
                ((EventListener<T>)l).receive(_source, _event);
            }
        }

        protected SynchronizedCollection<object> registerType(Type t)
        {
            // ensure that a listener list exists for Type t and if there is one then return it
            SynchronizedCollection<object> list = new SynchronizedCollection<object>();
            if (!_listeners.TryAdd(t, list))
            {
                _listeners.TryGetValue(t, out list);
                return list;
            }

            // make a set of all listeners for every supertype of t
            HashSet<object> listenerSet = new HashSet<object>();
            foreach (KeyValuePair<Type, SynchronizedCollection<object>> e in _listeners)
            {
                Type other = e.Key;
                if (other != t && t.IsAssignableFrom(other))
                {
                    foreach (object o in e.Value)
                    {
                        listenerSet.Add(o);
                    }
                }
            }

            // initialize the listener list for Type t
            foreach(object o in listenerSet)
            {
                list.Add(o);
            }

            return list;
        }
    }
}