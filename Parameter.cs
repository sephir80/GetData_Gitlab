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
        public string pathToSave;
        private string fileName;

        //Init paramater and load token
        public Get_Parameter(string fileNameParameter) 
        {
            string[] buffer = new string[2];
            this.fileName = fileNameParameter;
            buffer = System.IO.File.ReadAllLines(fileNameParameter);
            token = buffer[0];
            pathToSave = buffer[1];
        }

    }
}
