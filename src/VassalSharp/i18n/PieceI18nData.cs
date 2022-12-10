/*
 * Copyright (c) 2000-2006 by Rodney Kinney, Brent Easton
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

using GamePiece = VassalSharp.counters.GamePiece;

namespace VassalSharp.i18n
{
	
	/// <summary>
    /// Object encapsulating the internationalization information for a GamePiece.
	/// </summary>
	/// <author>
    /// Brent Easton
	/// </author>
	public class PieceI18nData
	{
        public class Property
        {
            virtual public string Description { get { return description; } }

            virtual public string Name { get { return name; } }

            private string name;
            private string description;

            public Property(string value_Renamed, string description) : base()
            {
                this.name = value_Renamed;
                this.description = description;
            }
        }

        protected internal GamePiece piece;
        protected List<Property> properties = new List<Property>();

        public PieceI18nData(GamePiece piece)
		{
			this.piece = piece;
		}
		
		public ReadOnlyCollection<Property> getProperties()
        {
            return properties.AsReadOnly();
        }
		
		public virtual void  add(string value_Renamed, string description)
		{
			if (value_Renamed != null && value_Renamed.Length > 0)
			{
				properties.Add(new Property(value_Renamed, description));
			}
		}

	}
}