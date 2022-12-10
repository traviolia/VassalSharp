/*
*
* Copyright (c) 2000-2013 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'java.beans.PropertyChangeListenerProxy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PropertyChangeListenerProxy = java.beans.PropertyChangeListenerProxy;
using ChatControlsInitializer = VassalSharp.chat.ui.ChatControlsInitializer;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat
{
	
	/// <summary> Delegates calls to another SvrConnection instance, which can be changed programmatically
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class HybridClient : ChatServerConnection, PlayerEncoder, ChatControlsInitializer
	{
		private void  InitBlock()
		{
			defaultRoom = Resources.getString("Chat.main_room");
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public Player UserInfo
		{
			get
			{
				return delegate_Renamed.UserInfo;
			}
			
			set
			{
				delegate_Renamed.UserInfo = value;
			}
			
		}
		virtual public ChatServerConnection Delegate
		{
			get
			{
				return delegate_Renamed;
			}
			
			set
			{
				if (delegate_Renamed != null && delegate_Renamed.isConnected())
				{
					throw new System.SystemException(Resources.getString("Server.error1")); //$NON-NLS-1$
				}
				ChatServerConnection oldDelegate = delegate_Renamed;
				if (oldDelegate != null)
				{
					value.UserInfo = oldDelegate.UserInfo;
				}
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener[] listeners = propSupport.getPropertyChangeListeners();
				for (int i = 0; i < listeners.Length; i++)
				{
					value.addPropertyChangeListener(((PropertyChangeListenerProxy) listeners[i]).getPropertyName(), listeners[i]);
				}
				if (controls != null)
				{
					if (delegate_Renamed is ChatControlsInitializer)
					{
						((ChatControlsInitializer) delegate_Renamed).uninitializeControls(controls);
					}
					if (value is ChatControlsInitializer)
					{
						((ChatControlsInitializer) value).initializeControls(controls);
					}
				}
				delegate_Renamed = value;
			}
			
		}
		protected internal ChatServerConnection delegate_Renamed;
		//UPGRADE_NOTE: The initialization of  'defaultRoom' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.String defaultRoom; //$NON-NLS-1$
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PropertyChangeSupport propSupport;
		protected internal ChatServerControls controls;
		protected internal System.Drawing.Image currentIcon;
		protected internal System.String currentText;
		
		public HybridClient()
		{
			InitBlock();
			Delegate = new DummyClient();
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(System.String propertyName, PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(propertyName, l);
			if (delegate_Renamed != null)
			{
				delegate_Renamed.addPropertyChangeListener(propertyName, l);
			}
		}
		
		public virtual Room[] getAvailableRooms()
		{
			return delegate_Renamed.getAvailableRooms();
		}
		
		public virtual Room getRoom()
		{
			return delegate_Renamed.getRoom();
		}
		
		public virtual bool isConnected()
		{
			return delegate_Renamed.isConnected();
		}
		
		public virtual void  sendTo(Player recipient, Command c)
		{
			delegate_Renamed.sendTo(recipient, c);
		}
		
		public virtual void  sendToOthers(Command c)
		{
			delegate_Renamed.sendToOthers(c);
		}
		
		public virtual void  setConnected(bool connect)
		{
			delegate_Renamed.setConnected(connect);
		}
		
		public virtual void  setRoom(Room r)
		{
			delegate_Renamed.setRoom(r);
		}
		
		public virtual Player stringToPlayer(System.String s)
		{
			if (delegate_Renamed is PlayerEncoder)
			{
				return ((PlayerEncoder) delegate_Renamed).stringToPlayer(s);
			}
			return null;
		}
		
		public virtual System.String playerToString(Player p)
		{
			if (delegate_Renamed is PlayerEncoder)
			{
				return ((PlayerEncoder) delegate_Renamed).playerToString(p);
			}
			return null;
		}
		
		protected internal virtual void  fireStatus(System.String msg)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.firePropertyChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeEvent.PropertyChangeEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeEventPropertyChangeEvent_javalangObject_javalangString_javalangObject_javalangObject'"
			propSupport.firePropertyChange(new PropertyChangeEvent(this, VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, msg));
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			this.controls = controls;
			if (delegate_Renamed is ChatControlsInitializer)
			{
				((ChatControlsInitializer) delegate_Renamed).initializeControls(controls);
				controls.RoomControlsVisible = true;
			}
			controls.updateClientDisplay(currentIcon, currentText);
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			if (delegate_Renamed is ChatControlsInitializer)
			{
				((ChatControlsInitializer) delegate_Renamed).uninitializeControls(controls);
			}
		}
		
		public virtual void  updateDisplayControls(System.Drawing.Image icon, System.String text)
		{
			if (controls != null)
			{
				controls.updateClientDisplay(icon, text);
			}
			currentIcon = icon;
			currentText = text;
		}
	}
}