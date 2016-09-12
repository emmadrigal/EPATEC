using System;
using System.Xml;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

class piece{
    public string name;
    public int size;
}

class Product{
    public string Name;
    public int Price;
    public List<piece> pieces;
}

class principal {
      static void Main(){

        // Step 1 Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8000/EPATEC/");

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(typeof(Service), baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(IService), new WSHttpBinding(), "EPATEC_Service");

                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }

    }
}

class empleado
{
    public int id;  
}
