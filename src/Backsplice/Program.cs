using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Backsplice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Backsplice.BackspliceMain.UseDefaultTemplateDirectory)
            {
                if (!System.IO.Directory.Exists(Backsplice.BackspliceMain.DefaultTemplateDirectory))
                {
                    System.IO.Directory.CreateDirectory(Backsplice.BackspliceMain.DefaultTemplateDirectory);
                }

                if (!System.IO.File.Exists(Backsplice.BackspliceMain.DefaultTemplateDirectory + @"/Default Blank.xlsx"))
                {
                    string application = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string applicationPath = System.IO.Path.GetDirectoryName(application);
                    System.IO.File.Copy(applicationPath + @"/templates/Default Blank.xlsx", Backsplice.BackspliceMain.DefaultTemplateDirectory + @"/Default Blank.xlsx");
                }
            }

            Application.Run(new BackspliceMain());
        }
    }
}
