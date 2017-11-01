using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCF.ErrorHandling.Service.Infrastructure.ErrorHandler
{
    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            var directory = ConfigurationManager.AppSettings["WCFLogFileDirectory"];
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var appName = ConfigurationManager.AppSettings["AppName"];
            var fileName = appName + "_WCF_Error_" + DateTime.Now.ToShortDateString() + ".txt";
            var filePath = Path.Combine(directory, fileName);

            using (TextWriter tw = File.AppendText(filePath))
            {
                if (error != null)
                {
                    tw.WriteLine("Exception: " + error.GetType().Name + "-" + error.Message + "-" + error.StackTrace);
                }
                tw.Close();
            }
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            ////If you want to pass message to client
            //var newEx = new FaultException(string.Format("Exception caught at Service Application GlobalErrorHandler{0}Method: { 1}{ 2}Message: { 3}", Environment.NewLine,
            //    error.TargetSite.Name, Environment.NewLine,
            //    error.Message));

            //MessageFault msgFault = newEx.CreateMessageFault();
            //fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }
    }
}
