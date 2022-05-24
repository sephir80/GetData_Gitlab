using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace GetData_Gitlab
{
    public class SaveOnFile
    {
        List<string> buffer;
        public SaveOnFile(List<Project> bufferToSave, string nomeFile)
        {
            buffer = new List<string> { };
            foreach (Project p in bufferToSave)
                buffer.Add(p.ToString());
            if (File.Exists(nomeFile))
                File.Delete(nomeFile);
            
            File.WriteAllLines(nomeFile, buffer);

        }

        public SaveOnFile(List<Merge> bufferToSave, string nomeFile)
        {
            buffer = new List<string> { };
            foreach (Merge p in bufferToSave)
                buffer.Add(p.ToString());
            if (File.Exists(nomeFile))
                File.Delete(nomeFile);

            File.WriteAllLines(nomeFile, buffer);

        }

    }
}
