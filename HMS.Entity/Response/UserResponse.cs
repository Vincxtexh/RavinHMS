using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entity
{
    public class UserResponse
    {
        public int UserId { get; set; }  
        public string UserName { get; set; }    
        public int UserAge { get; set; }

        [Ignore]
        public bool IsCached { get; set; }

    }
}
