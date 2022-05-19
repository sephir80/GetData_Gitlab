using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_Gitlab
{
    public class Get_Parameter
    {
        public string token;
        private string fileName;

        //Init paramater and load token
        public Get_Parameter(string fileNameParameter) 
        {
            this.fileName = fileNameParameter;
            this.token = System.IO.File.ReadAllText(fileNameParameter);
        }

    }
}
