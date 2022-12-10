/*
 * Copyright (c) 2008 by Joel Uckelman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

using Microsoft.Extensions.Logging;

using System;


namespace VassalSharp.tools
{
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ThrowableUtils
	{
        private static readonly ILogger logger;

        static ThrowableUtils()
        {
            logger = (new LoggerFactory()).CreateLogger(nameof(ThrowableUtils));
        }

 		private ThrowableUtils()
		{
		}
		
		/// <summary> Returns the most recent {@link Throwable} of class <code>T</code> in
		/// the proper causal history of the given <code>Throwable</code>, if one
		/// exists.
		/// 
		/// </summary>
		/// <param name="cl">the {@link Class} to search for
		/// </param>
		/// <param name="t"> the <code>Throwable</code> to check
		/// </param>
		/// <returns> the proper ancestor of class <code>T</code>, or <code>null</code>
		/// if none exists
		/// </returns>
		public static Exception getAncestor(Type cl, Exception t)
        {
            for (Exception e = t.InnerException; e != null; e = e.InnerException)
            {
                if (cl.IsAssignableFrom(e.GetType())) return e;
            }
            return null;
        }

        /// <summary> Returns the most recent {@link Throwable} of class <code>T</code> in
        /// the (not necessarily proper) causal history of the given
        /// <code>Throwable</code>, if one exists. If the given
        /// <code>Throwable</code> is of class <code>T</code>, it will be returned.
        /// 
        /// </summary>
        /// <param name="cl">the {@link Class} to search for
        /// </param>
        /// <param name="t"> the <code>Throwable</code> to check
        /// </param>
        /// <returns> the ancestor of class <code>T</code>, or <code>null</code>
        /// if none exists
        /// </returns>
        public static Exception getRecent(Type cl, Exception t)
        {
            if (cl.IsAssignableFrom(t.GetType())) return t;
            return getAncestor(cl, t);
        }

        /// <summary>
        /// Throws the most recent {@link Throwable} of class <code>T</code> in
        /// the proper causal history of the given <code>Throwable</code>, if one
        /// exists.
        /// </summary>
        /// <param name="cl">the <code>Class</code> to search for
        /// </param>
        /// <param name="t">the <code>Throwable</code> to check
        /// </param>
        /// <throws>  T if an ancestor of that class is found </throws>
        public static void throwAncestor(Type cl, Exception t)
        {
            var ancestor = getAncestor(cl, t);
            if (ancestor != null)
                throwMe(cl, t);
        }

        /// <summary>
        /// Throws the most recent {@link Throwable} of class <code>T</code> in
        /// the (not necessarily proper) causal history of the given
        /// <code>Throwable</code>, if one exists.
        /// </summary>
        /// <param name="cl">the <code>Class</code> to search for
        /// </param>
        /// <param name="t">the <code>Throwable</code> to check
        /// </param>
        /// <throws>  T if an ancestor of that class is found </throws>
        public static void throwRecent(Type cl, Exception t)
        {
            var other = t.GetType();
            if (cl.IsAssignableFrom(other)) 
                throwMe(cl, t);
            else 
                throwAncestor(cl, t);
        }

        private static void throwMe(Type cl, Exception  t) 
        {
            Exception toThrow = null;
            try
            {
                toThrow = (Exception) Activator.CreateInstance(cl, t);
            }
            catch (Exception ignore)
            {
                // If anything happens here, we're screwed anyway, as we're already
                // calling this during error handling. Just log it and soldier on.
                logger.warn("ignored", ignore);
            }

            if (toThrow != null) throw toThrow;
        }
	}
}