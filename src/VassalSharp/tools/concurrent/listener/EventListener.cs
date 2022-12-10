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

namespace VassalSharp.tools.concurrent.listener
{
    /// <summary>
    /// An interface for handling events.
    /// </summary>
    /// <author>  Joel Uckelman
    /// </author>
    /// <since> 3.2.0
    /// </since>
    /// <seealso cref="EventListenerSupport">
    /// </seealso>
    public interface EventListener<in T>
    {
        /// <summary> Receive notification of an event.
        /// 
        /// </summary>
        /// <param name="src">the source of the event
        /// </param>
        /// <param name="event">the event
        /// </param>
        void receive(object src, T _event);
    }
}