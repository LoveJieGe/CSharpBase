using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter13_AsyncLib
{
    public class SearchInfo:BinableBase
    {
        private ObservableCollection<SearchItemResult> list;
        private string searchTerm;
        public SearchInfo()
        {
            list = new ObservableCollection<SearchItemResult>();
            list.CollectionChanged += delegate { OnPropertyChange("List"); };
        }
        public ObservableCollection<SearchItemResult> List
        {
            get { return this.list; }
            set { SetProperty(ref list, value); }
        }
        public string SearchTerm
        {
            get { return this.searchTerm; }
            set { SetProperty(ref searchTerm, value); }
        }
    }
}
