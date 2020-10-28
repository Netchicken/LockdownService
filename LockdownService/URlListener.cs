﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace LockdownService
{
    //Todo create a service for each branch
    //todo crate a service for student /staff

    //todo create a folder to hold the service, a page with a sound - Forms? 


    //https://docs.microsoft.com/en-us/dotnet/api/system.net.httplistenerrequest?view=netcore-3.1

    internal  class URlListener
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(URlListener));
        public  void SimpleListener()
        {
            //does it work?
            if (!HttpListener.IsSupported)
            {
                _log.Info("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }


            var prefixes = new[] { "https://localhost:44300/", "https://www.stuff.co.nz/", "https://www.reddit.com/" };


            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes missing");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
                _log.Info("loading: " + s);
            }


            listener.Start();
            _log.Info("Listening for lockdown ...");

            //When a client makes a request to a Uniform Resource Identifier (URI) handled by an HttpListener object, the HttpListener provides a HttpListenerContext object that contains information about the sender, the request, and the response that is sent to the client. The HttpListenerContext.Request property returns the HttpListenerRequest object that describes the request.

            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext(); //contains information about the sender, the request, and the response that is sent to the client.

            //https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
            // HttpListenerContext context = await listener.GetContextAsync();

            //The HttpListenerRequest object contains information about the request, such as the request HttpMethod string, UserAgent string, and request body data (see the InputStream property).




            HttpListenerRequest request = context.Request;

            var URL = request.Url.ToString();

            _log.Info("Whoot! I Detected a lockdown ..." + URL);

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

        private  void LockdownEvents()
        {
            _log.Info("Whoot! Detected a lockdown ...");

            //  string path = @"C:\Dropbox\Enform\Lockdown_service_form\lockdown.html";



            string basePath = Environment.CurrentDirectory;
            string fullPath = Path.GetFullPath("lockdown.html", basePath);
            OpenUrl(fullPath);

            playSimpleSound();



            // run the WooWoo sound
            //Load the webpage with the SignalR in it

            //todo  need to differentuate betwenn student and staff machines as well






        }
        //open the webpage while fixing core bug
        private  void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Need to load sound outside of webpage becuase browser doesn't let sound start without interaction first
        /// </summary>

        private  void playSimpleSound()
        {
            string basePath = Environment.CurrentDirectory;
            string fullPath = Path.GetFullPath("Alarm.mp3", basePath);

            WindowsMediaPlayer myplayer = new WindowsMediaPlayer();
            myplayer.URL = fullPath;
            myplayer.controls.play();


        }





    }
}
