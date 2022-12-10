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
using System.Collections.Generic;

namespace VassalSharp.tools.concurrent.listener
{
    /// <summary> 
    /// A class to provide support for {@link EventListener}s.
    /// </summary>
    /// <author>  Joel Uckelman
    /// </author>
    /// <since> 3.2.0
    /// </since>
    public class DefaultEventListenerSupport<T> : EventListenerSupport<T>
    {
        protected internal object source;
        protected SynchronizedCollection<EventListener<T>> _listeners = new SynchronizedCollection<EventListener<T>>();

        /// <summary>
        /// Creates a <code>DefaultEventListenerSupport</code>.
        /// </summary>
        /// <param name="src">the source of events
        /// </param>
        public DefaultEventListenerSupport(object src)
        {
            this.source = src;
        }

        /// <summary>{@inheritDoc} </summary>
        public void addEventListener(EventListener<T> l)
        {
            _listeners.Add(l);
        }

        /// <summary>{@inheritDoc} </summary>
        public void removeEventListener(EventListener<T> l)
        {
            _listeners.Remove(l);
        }

        /// <summary>{@inheritDoc} </summary>
        public virtual bool hasEventListeners()
        {
            return _listeners.Count > 0;
        }

        /// <summary>{@inheritDoc} </summary>
        public IList<EventListener<T>> getEventListeners()
        {
            return _listeners;
        }

        /// <summary>{@inheritDoc} </summary>
        public virtual void notify(T _event)
        {
            foreach (EventListener<T> l in _listeners)
                l.receive(source, _event);
        }
    }
}