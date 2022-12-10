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
    /// An interface to provide support for {@link EventListener}s.
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public interface MultiEventListenerSupport
	{
        /// <summary>
        /// Adds an {@link EventListener}.
        /// </summary>
        /// <param name="c">the class to listen for
        /// </param>
        /// <param name="l">the listener to add
        /// </param>
        void addEventListener<T>(Type c, EventListener<T> l);

        /// <summary>
        /// Removes an {@link EventListener}.
        /// </summary>
        /// <param name="c">the class to check
        /// </param>
        /// <param name="l">the listener to remove
        /// </param>
        void removeEventListener<T>(Type c, EventListener<T> l);

        /// <summary>
        /// Checks whether there are any {@link EventListener}s.
        /// </summary>
        /// <param name="c">the class to check
        /// </param>
        /// <returns> <code>true</code> if there are any listeners for the given class
        /// </returns>
        bool hasEventListeners<T>();

        /// <summary>
        /// Gets the list of listerners.
        /// </summary>
        /// <param name="c">the class to check
        /// </param>
        /// <returns> the list of listeners for the given class
        /// </returns>
        IList<EventListener<T>> getEventListeners<T>();

        /// <summary> Notify the listeners of an event.
        /// 
        /// </summary>
        /// <param name="event">the event to send
        /// </param>
        void notify<T>(T _event);
	}
}