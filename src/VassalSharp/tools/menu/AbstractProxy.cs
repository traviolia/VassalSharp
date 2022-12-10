using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace VassalSharp.tools.menu
{

	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public abstract class AbstractProxy<T> : ChildProxy<T> where T : System.ComponentModel.Component
	{
		protected internal ParentProxy<System.Windows.Forms.Control> _parent;

		virtual public ParentProxy<System.Windows.Forms.Control> Parent
		{
			get
			{
				return _parent;
			}

			set
			{
				_parent = value;
				if (value == null)
					forEachPeer((peer) => { peer.Parent?.Controls.Remove(peer); });
			}
		}

		protected List<WeakReference<T>> peers = new List<WeakReference<T>>();

		public AbstractProxy()
		{
		}

		protected internal virtual void CleanupWeakReferences()
		{
			var list = new List<WeakReference<T>>();
			foreach (WeakReference<T> wref in peers)
				if (wref.TryGetTarget(out T _))
					list.Add(wref);
			peers = list;
		}

		protected void forEachPeer(Action<T> functor)
		{
			CleanupWeakReferences();

			foreach (WeakReference < T > wref in peers)
			{
				if (wref.TryGetTarget(out T peer))
				{
					functor.Invoke(peer);
				}
			}
		}

		public abstract T createPeer();

		System.ComponentModel.Component IChildProxy.createPeer()
		{
			return this.createPeer();
		}
	}
}