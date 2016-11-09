using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Thoughts.AndroidApp.ViewModels.Adapters
{
    public abstract class CollectionAdapter<ViewModel> : BaseAdapter
    {
        protected Context _context { get; set; }

        protected List<ViewModel> _viewModels { get; set; }

        public CollectionAdapter(Context context)
        {
            _context = context;
            _viewModels = new List<ViewModel>();
        }

        public void Add(ViewModel viewModel)
        {
            _viewModels.Add(viewModel);
            this.NotifyDataSetChanged();
        }

        public override int Count
        {
            get
            {
                return _viewModels.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
    }
}