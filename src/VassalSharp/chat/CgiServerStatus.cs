/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.math.LongRange' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LongRange = org.apache.commons.lang.math.LongRange;
using Resources = VassalSharp.i18n.Resources;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat
{
	
	/// <summary> Queries a known URL to get historical status of the chat room server.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public class CgiServerStatus : ServerStatus
	{
		private void  InitBlock()
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			VassalSharp.chat.ModuleSummary[] e = entries.values().toArray(new VassalSharp.chat.ModuleSummary[entries.size()]);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Arrays.sort(e, new Comparator < ServerStatus.ModuleSummary >()
			{
			}
		}
		virtual public VassalSharp.chat.ModuleSummary[] Status
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final HashMap < String, ServerStatus.ModuleSummary > entries = 
				new HashMap < String, ServerStatus.ModuleSummary >();
				try
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(String s: request.doGet(getCurrentConnections, new Properties()))
					{
						//$NON-NLS-1$
						//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, '\t');
						try
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String moduleName = st.nextToken();
							//UPGRADE_NOTE: Final was removed from the declaration of 'roomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String roomName = st.nextToken();
							//UPGRADE_NOTE: Final was removed from the declaration of 'playerName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String playerName = st.nextToken();
							//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							VassalSharp.chat.ModuleSummary entry = entries.get_Renamed(moduleName);
							if (entry == null)
							{
								entries.put(moduleName, createEntry(moduleName, roomName, playerName));
							}
							else
							{
								updateEntry(entry, roomName, playerName);
							}
						}
						// FIXME: review error message
						catch (System.ArgumentOutOfRangeException e1)
						{
						}
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				
				return sortEntriesByModuleName(entries);
			}
			
		}
		virtual public System.String[] SupportedTimeRanges
		{
			get
			{
				return times;
			}
			
		}
		private const long DAY = 24L * 3600L * 1000L;
		
		public const System.String LAST_DAY = "Server.last_24_hours"; //$NON-NLS-1$
		public const System.String LAST_WEEK = "Server.last_week"; //$NON-NLS-1$
		public const System.String LAST_MONTH = "Server.last_month"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final Map < String, Long > timeRanges = new HashMap < String, Long >();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'times '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[] times = new System.String[]{Resources.getString(LAST_DAY), Resources.getString(LAST_WEEK), Resources.getString(LAST_MONTH)};
		
		private HttpRequestWrapper request;
		
		public CgiServerStatus()
		{
			InitBlock();
			request = new HttpRequestWrapper("http://www.vassalengine.org/util/"); //$NON-NLS-1$
			timeRanges.put(Resources.getString(LAST_DAY), DAY);
			timeRanges.put(Resources.getString(LAST_WEEK), DAY * 7);
			timeRanges.put(Resources.getString(LAST_MONTH), DAY * 30);
		}
		
		public virtual ModuleSummary[] getHistory(System.String timeRange)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int64 l = timeRanges.get_Renamed(timeRange);
			//UPGRADE_TODO: The 'System.Int64' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			return l != null?getHistory((long) l):new ModuleSummary[0];
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private SortedMap < Long, List < String [] >> records = 
		new TreeMap < Long, List < String [] >>();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < LongRange > requests = new ArrayList < LongRange >();
		
		private VassalSharp.chat.ModuleSummary[] getHistory(long time)
		{
			if (time <= 0)
				return Status;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'now '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long now = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			// start with new interval
			//UPGRADE_NOTE: Final was removed from the declaration of 'req '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			LongRange req = new LongRange(now - time, now);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < LongRange > toRequest = new ArrayList < LongRange >();
			toRequest.add(req);
			
			// subtract each old interval from new interval
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(LongRange y: requests)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			}
			
			// now toRequest contains the intervals we are missing; request those
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(LongRange i: toRequest)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String s: getInterval(i))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, '\t');
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String moduleName = st.nextToken();
						//UPGRADE_NOTE: Final was removed from the declaration of 'roomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String roomName = st.nextToken();
						//UPGRADE_NOTE: Final was removed from the declaration of 'playerName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String playerName = st.nextToken();
						//UPGRADE_NOTE: Final was removed from the declaration of 'when '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Int64 when = System.Int64.Parse(st.nextToken());
						
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						List < String [] > l = records.get(when);
						if (l == null)
						{
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							l = new ArrayList < String [] >();
							records.put(when, l);
						}
						
						l.add(new System.String[]{moduleName, roomName, playerName});
					}
					// FIXME: review error message
					catch (System.ArgumentOutOfRangeException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
					// FIXME: review error message
					catch (System.FormatException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
				
				requests.add(i);
			}
			
			// Join intervals to minimize the number we store.
			// Note: This is simple, but quadratic in the number of intervals.
			// For large numbers of intervals, use an interval tree instead.
			for (int i = 0; i < requests.size(); i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LongRange a = requests.get_Renamed(i);
				for (int j = i + 1; j < requests.size(); j++)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					LongRange b = requests.get_Renamed(j);
					if (a.overlapsRange(b))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'al '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						long al = a.getMinimumLong();
						//UPGRADE_NOTE: Final was removed from the declaration of 'ar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						long ar = a.getMaximumLong();
						//UPGRADE_NOTE: Final was removed from the declaration of 'bl '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						long bl = b.getMinimumLong();
						//UPGRADE_NOTE: Final was removed from the declaration of 'br '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						long br = b.getMaximumLong();
						
						requests.set_Renamed(i, new LongRange(System.Math.Min(al, bl), System.Math.Max(ar, br)));
						requests.remove(j--);
					}
				}
			}
			
			// pull what we need from the records
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final HashMap < String, ServerStatus.ModuleSummary > entries = 
			new HashMap < String, ServerStatus.ModuleSummary >();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(List < String [] > l: records.subMap(req.getMinimumLong(), 
			req.getMaximumLong()).values())
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String [] r: l)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String moduleName = r[0];
					//UPGRADE_NOTE: Final was removed from the declaration of 'roomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String roomName = r[1];
					//UPGRADE_NOTE: Final was removed from the declaration of 'playerName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String playerName = r[2];
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					VassalSharp.chat.ModuleSummary entry = entries.get_Renamed(moduleName);
					if (entry == null)
					{
						entries.put(moduleName, createEntry(moduleName, roomName, playerName));
					}
					else
					{
						updateEntry(entry, roomName, playerName);
					}
				}
			}
			
			return sortEntriesByModuleName(entries);
		}
		
		private VassalSharp.chat.ModuleSummary[] sortEntriesByModuleName;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(
		Map < String, ServerStatus.ModuleSummary > entries)
		public virtual int compare(VassalSharp.chat.ModuleSummary a, VassalSharp.chat.ModuleSummary b)
		{
			return String.CompareOrdinal(a.ModuleName, b.ModuleName);
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return e;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private List < String > getInterval(LongRange i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Properties p = new Properties();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	p.setProperty(start, Long.toString(i.getMinimumLong())); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	p.setProperty(end, Long.toString(i.getMaximumLong())); //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return request.doGet(getConnectionHistory, p); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// FIXME: review error message
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(IOException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		e.printStackTrace();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private ServerStatus.ModuleSummary updateEntry(
	ServerStatus.ModuleSummary entry, String roomName, String playerName)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		SimpleRoom existingRoom = entry.getRoom(roomName);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(existingRoom == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		existingRoom = new SimpleRoom(roomName);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	existingRoom.setPlayers(new Player []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ new SimplePlayer(playerName)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	entry.addRoom(existingRoom);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		existingRoom.addPlayer(new SimplePlayer(playerName));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return entry;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private ServerStatus.ModuleSummary createEntry(String moduleName, 
	String roomName, 
	String playerName)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final SimpleRoom r = new SimpleRoom(roomName);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	r.setPlayers(new Player []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ new SimplePlayer(playerName)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return new ServerStatus.ModuleSummary(moduleName, new Room []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ r
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}