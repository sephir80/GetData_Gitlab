using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_Gitlab
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string web_url { get; set; }
        public Booth @namespace { get; set; }
        public Merge rev_info { get; set; } = new Merge();
        override public string ToString()
        {
            return id.ToString()+","+name+","+ web_url + ","+@namespace.ToString()+","+rev_info.ToString();
        }
    }

    public class Booth
    {
        public int id { get; set; }
        public string name { get; set; }
        override public string ToString()
        {
            return id.ToString() + "," + name;
        }


    }

    public class Merge
    {
        public int project_id { get; set; }
        public string state { get; set; } = "";
        public User merged_by { get; set; } = new User();
        override public string ToString()
        {
            return project_id.ToString() + "," + state + ","+ merged_by.ToString();
        }

    }

    public class User
    {
        public string name { get; set; } = "";
        override public string ToString()
        {
            return name;
        }

    }

}
