using System;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'java.util.concurrent.LinkedBlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedBlockingQueue = java.util.concurrent.LinkedBlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.ThreadPoolExecutor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ThreadPoolExecutor = java.util.concurrent.ThreadPoolExecutor;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
namespace VassalSharp.tools.concurrent
{
	
	public class Exec
	{
		protected internal Exec()
		{
		}
		
		public static ExecutorService ex;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		new ThreadPoolExecutor(
		Runtime.getRuntime().availableProcessors(), 
		Runtime.getRuntime().availableProcessors() + 1, 
		60, TimeUnit.SECONDS, 
		new LinkedBlockingQueue < Runnable >()
		);
		
		/*
		public static <T> T submitAndWait(Callable<T> c) {
		try {
		return ex.submit(c).get();
		}
		catch (CancellationException e) {
		ErrorDialog.bug(e);
		}
		catch (ExecutionException e) {
		ErrorDialog.bug(e);
		}
		catch (InterruptedException e) {
		ErrorDialog.bug(e);
		}
		
		}*/
	}
}