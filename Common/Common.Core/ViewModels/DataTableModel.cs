using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.ViewModels
{
    public class DataTableModel
    {
        public string? draw { get; set; }
        public string? start { get; set; }
        public string? lenght { get; set; }
        public string? sortColumn { get; set; }
        public string? sortColumnDirection { get; set; }
        public string? searchValue { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class DataTableResultModel
    {
        public string? draw { get; set; }
        public int? recordsFiltered { get; set; }
        public int? recordsTotal { get; set; }
        public dynamic? data { get; set; }
    }
}

