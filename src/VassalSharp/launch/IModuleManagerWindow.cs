/*
* $Id$
*
* Copyright (c) 2000-2009 by Brent Easton, Rodney Kinney, Joel Uckelman
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using VassalSharp.UiInterfaces;

namespace VassalSharp.launch
{
	public interface IModuleManagerWindow : IUiWindow
	{
		public void setWaitCursor(bool wait);

		/// <summary> A File has been saved or created by the Player or the Editor. Update
		/// the display as necessary.
		/// </summary>
		/// <param name="f">The file
		/// </param>
		public void update(System.IO.FileInfo f);

		public System.IO.FileInfo getSelectedModule();

		public void addModule(System.IO.FileInfo f);

		public void removeModule(System.IO.FileInfo f);

		public System.IO.FileInfo getModuleByName(String name);

	}
}
