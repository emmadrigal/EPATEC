using System.Text;

namespace ErrorHandler
{
    //Class responsible for handling error messages
    public class ErrorHandler
    {
        static StringBuilder errMessage = new StringBuilder();

        //Make class immutable
        static ErrorHandler()
        {
        }
        /// <summary>
        /// Prperty - holds exception messages encountered 
        /// at code execution
        /// </summary>
        public string ErrorMessage
        {
            get { return errMessage.ToString(); }
            set
            {
                string path = System.Windows.Forms.Application.StartupPath + "error.txt";
                System.IO.File.WriteAllText(@path, value);
                errMessage.AppendLine(value);
            }
        }
    }
}
