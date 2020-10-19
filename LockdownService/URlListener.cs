using System;
using System.Net;

namespace LockdownService
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.net.httplistenerrequest?view=netcore-3.1

    internal static class URlListener
    {

        public static void SimpleListener()
        {

            var prefixes = new[] { "https://localhost:44300/", "https://www.stuff.co.nz/" };


            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes missing");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }


            listener.Start();
            Console.WriteLine("Listening for lockdown ...");

            //When a client makes a request to a Uniform Resource Identifier (URI) handled by an HttpListener object, the HttpListener provides a HttpListenerContext object that contains information about the sender, the request, and the response that is sent to the client. The HttpListenerContext.Request property returns the HttpListenerRequest object that describes the request.

            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext(); //contains information about the sender, the request, and the response that is sent to the client.

            //https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
            // HttpListenerContext context = await listener.GetContextAsync();

            //The HttpListenerRequest object contains information about the request, such as the request HttpMethod string, UserAgent string, and request body data (see the InputStream property).




            HttpListenerRequest request = context.Request;

            var URL = request.Url.ToString();

            Console.WriteLine("Whoot! Detected a lockdown ..." + URL);

            if (URL.Contains("Hamilton"))
            {
                //LOCKDOWN 
                LockdownEvents();
            }
            if (URL.Contains("Auckland"))
            {
                //LOCKDOWN 
                LockdownEvents();
            }

            if (URL.Contains("Christchurch"))
            {
                //LOCKDOWN 
                LockdownEvents();
            }





            // To reply to the request, you must get the associated response using the Response property.

            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            //  listener.Stop();
        }

        private static void LockdownEvents()
        {
            Console.WriteLine("Whoot! Detected a lockdown ...");
            // run the WooWoo sound
            //Load the webpage with the SignalR in it

            //todo  need to differentuate betwenn student and staff machines as well




        }


    }
}
