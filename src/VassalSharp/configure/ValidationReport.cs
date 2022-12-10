/*
 * Copyright (c) 2004 by Rodney Kinney
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VassalSharp.configure
{
    /// <summary>
    /// Summarizes error/warning messages about invalid module configuration.
    /// </summary>
    public class ValidationReport
    {
        private List<string> messages = new List<string>();

        public virtual void addWarning(string msg)
        {
            messages.Add(msg);
        }

        public ReadOnlyCollection<string> getWarnings()
        {
            return messages.AsReadOnly();
        }
    }
}