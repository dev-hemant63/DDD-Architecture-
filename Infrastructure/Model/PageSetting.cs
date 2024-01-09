using System.Collections.Generic;

namespace Infrastructure.Model
{
    public class JDataTable
    {
        public int CurrentPage { get; set; }
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
    public class JDataTable<T>: JDataTable
    {
        public List<T> Data { get; set; }
    }
    public class JSONAOData
    {
        public int draw { get; set; }
        public int start { get; set; } = 0;
        public int length { get; set; } = 100;
        public jsonAODataSearch search { get; set; }
        public object param { get; set; }
    }
    public class jsonAODataSearch
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
}
