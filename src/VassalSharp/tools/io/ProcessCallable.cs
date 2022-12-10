/*
* $Id$
*
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
//UPGRADE_TODO: The type 'java.util.concurrent.Callable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callable = java.util.concurrent.Callable;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeoutException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeoutException = java.util.concurrent.TimeoutException;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
namespace VassalSharp.tools.io
{
	
	/// <summary> A {@link Callable} which wraps a {@link java.lang.Process}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	class ProcessCallable : Callable
	{
		private void  InitBlock()
		{
			try
			{
				f.get_Renamed(1000L, TimeUnit.MILLISECONDS);
			}
			catch (ExecutionException e)
			{
				logger.error("", e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				logger.error("", e);
			}
			catch (TimeoutException e)
			{
				logger.error("", e);
				f.cancel(true);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Integer >
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.io.ProcessCallable'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'proc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Diagnostics.Process proc;
		//UPGRADE_NOTE: Final was removed from the declaration of 'stdoutPump '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal InputStreamPump stdoutPump;
		//UPGRADE_NOTE: Final was removed from the declaration of 'stderrPump '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal InputStreamPump stderrPump;
		//UPGRADE_NOTE: Final was removed from the declaration of 'exec '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ExecutorService exec;
		
		/// <summary> Creates a <code>ProcessCallable</code>.
		/// 
		/// </summary>
		/// <param name="proc">the process
		/// </param>
		/// <param name="stdout">the stream where the process' STDOUT is redirected
		/// </param>
		/// <param name="stderr">the stream where the process' STDERR is redirected
		/// </param>
		/// <param name="exec">the executor which runs the stream pumps
		/// </param>
		public ProcessCallable(System.Diagnostics.Process proc, InputStreamPump stdoutPump, InputStreamPump stderrPump, ExecutorService exec)
		{
			InitBlock();
			if (proc == null)
				throw new System.ArgumentException("proc == null");
			
			if (stdoutPump == null)
			{
				throw new System.ArgumentException("stdoutPump == null");
			}
			
			if (stderrPump == null)
			{
				throw new System.ArgumentException("stderrPump == null");
			}
			
			if (exec == null)
				throw new System.ArgumentException("exec == null");
			
			this.proc = proc;
			this.stdoutPump = stdoutPump;
			this.stderrPump = stderrPump;
			this.exec = exec;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <returns> the return value of the process
		/// </returns>
		public virtual System.Int32 call()
		{
			
			stdoutPump.InputStream = proc.StandardInput.BaseStream;
			stderrPump.InputStream = proc.StandardError.BaseStream;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Future < ? > out_f = exec.submit(stdoutPump);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Future < ? > err_f = exec.submit(stderrPump);
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				proc.WaitForExit();
				int result = proc.ExitCode;
				
				// stop the stream pumps
				stopPump(out_f);
				stopPump(err_f);
				
				// close stdout, stderr, stdin
				closeStreams();
				
				return result;
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				// We don't log this because it's not an error, it just
				// means that the process is being cancelled.
				
				// cancel the futures
				out_f.cancel(true);
				err_f.cancel(true);
				
				// close stdout, stderr, stdin
				closeStreams();
				
				// kill the process
				proc.Kill();
			}
			
			// Pardon the interruption.
			return - 1;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void stopPump(Future < ? > f)
		
		protected internal virtual void  closeStreams()
		{
			IOUtils.closeQuietly(proc.StandardOutput.BaseStream);
			IOUtils.closeQuietly(proc.StandardError.BaseStream);
			IOUtils.closeQuietly(proc.StandardInput.BaseStream);
		}
		static ProcessCallable()
		{
			logger = LoggerFactory.getLogger(typeof(ProcessCallable));
		}
	}
}