using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Text;


using var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8001/");

listener.Start();

Console.WriteLine("Listening on port 8001...");
string pathDir = Path.GetFullPath("..\\..\\..\\") ;
Console.WriteLine(pathDir);
string logFilePath = pathDir + "log.csv";

while (listener.IsListening)
{
    HttpListenerContext ctx = listener.GetContext();
    HttpListenerRequest req = ctx.Request;
    using HttpListenerResponse resp = ctx.Response;
    string? localPath = ctx.Request.Url?.LocalPath.Replace("/", "\\");
    string path = pathDir + localPath;
    Console.WriteLine(path);
    Console.WriteLine(File.Exists(path));
    Directory.GetCurrentDirectory();
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
listener.Stop();

void log(HttpListenerRequest req, HttpListenerResponse resp)
{
    string logLine = "";
   
    logLine = $"{DateTime.UtcNow};" +
        $"{req.RemoteEndPoint};" +
        $"{req.Url};" +
        $"{resp.StatusCode};";
   
    File.AppendAllText(logFilePath, logLine);
    Console.WriteLine(logLine);
}
