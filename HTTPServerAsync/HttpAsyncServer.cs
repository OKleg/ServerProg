using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HTTPServerAsync
{
    class HttpAsyncServer
    {
        static bool KeepGoing = true;
        static List<Task> OngoingTasks = new List<Task>();
        static string pathDir = Path.GetFullPath("..\\..\\..\\");
        static string logFilePath = pathDir + "log.csv";


        static async Task ProcessAsync(HttpListener listener)
        {
            while (KeepGoing)
            {
                if (OngoingTasks.Count < 10)
                {
                    Task? t = listener.GetContextAsync().ContinueWith(async task =>
                    {
                        if(task.IsCompleted)
                        {
                            Console.WriteLine("Request comleted");
                            //TODO
                            await HandleRequest(task.Result);
                        }
                        else
                        {
                            Console.WriteLine("Request canceled");
                        }
                    });
                    OngoingTasks.Add(t);
                }
                else
                {
                    await Task.WhenAny(OngoingTasks);
                }
                OngoingTasks.RemoveAll(task => task.IsCompleted);
            }
            await Task.WhenAll(OngoingTasks);
        }
        static async Task HandleRequest(HttpListenerContext ctx)
        {
            HttpListenerRequest req = ctx.Request;
            using HttpListenerResponse resp = ctx.Response;
            string? localPath = req.Url?.LocalPath.Replace("/", "\\");
            string path = pathDir + localPath;
            byte[] buf;
            if (File.Exists(path))
            {
                resp.Headers.Set("Content-Type", resp.ContentType);
                resp.StatusCode = (int)HttpStatusCode.OK;
                string msg = $"{resp.StatusCode}: {resp.StatusDescription}";
                Console.WriteLine(msg);
                buf = File.ReadAllBytes(path);
            }
            else
            {
                resp.Headers.Set("Content-Type", "text/plain");
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = $"{localPath} not found";
                string msg = $"{resp.StatusCode}: {resp.StatusDescription}";
                Console.WriteLine(msg);
                buf = Encoding.UTF8.GetBytes(msg);
            }
            resp.ContentLength64 = buf.Length;
            Stream ros = resp.OutputStream;
            ros.Write(buf, 0, buf.Length);
            log(ctx.Request, resp);
        }

        static void log(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string logLine = "";

            logLine = $"{DateTime.Now};" +
                $"{req.RemoteEndPoint};" +
                $"{req.Url};" +
                $"{resp.StatusCode};";

            File.AppendAllText(logFilePath, logLine + Environment.NewLine);
            Console.WriteLine(logLine);
        }

        static async Task Main(string[] args)
        {
            string ipAddress = "localhost";
            int port = 8001;
            using var listener = new HttpListener(); 
            listener.Prefixes.Add($"http://{ipAddress}:{port}/");
            listener.Start();
            var listenTask = ProcessAsync(listener);
            var cmd = Console.ReadLine();

            if (cmd != "")
            {
                KeepGoing = false;
                listener.Stop();
                await listenTask;
            }
            listener.Close();

        }
    }
}
