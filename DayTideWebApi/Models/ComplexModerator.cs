using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class ComplexModerator
    {
        public Moderator moderator { get; set; }
        public User user { get; set; }
     
    }
}