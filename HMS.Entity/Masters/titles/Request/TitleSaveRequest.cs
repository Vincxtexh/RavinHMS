using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entity.Masters.titles.Request
    {
    public class TitleSaveRequest
        {
        public int TitleID { get; set; }
        public string TitleName { get; set; }
        public string Gender { get; set; }
        public string AgeUnit { get; set; }
        public string CommandType { get; set; }
        }
    }
