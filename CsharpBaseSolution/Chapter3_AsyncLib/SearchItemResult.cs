using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter13_AsyncLib
{
    /// <summary>
    /// 标识结果集合中的一项
    /// </summary>
    public class SearchItemResult:BinableBase
    {
        private string title;
        private string url;
        private string thumbnailUrl;
        private string source;
        /// <summary>
        /// 描述图片的文本
        /// </summary>
        public string Title
        {
            get{ return this.title;}
            set{ SetProperty(ref title, value); }
        }
        /// <summary>
        /// 包含大尺寸图片的链接
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { SetProperty(ref url, value); }
        }
        /// <summary>
        /// 缩略图图片
        /// </summary>
        public string ThumbnailUrl
        {
            get { return this.thumbnailUrl; }
            set { SetProperty(ref thumbnailUrl, value); }
        }
        public string Source
        {
            get { return this.source; }
            set { SetProperty(ref source, value); }
        }
    }
}
