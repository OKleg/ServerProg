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
    /*try
    {

        if (path == "/")
        {
            sendHTML(ctx);
        }
        if (path != null && path.Split('.').Length > 1) {
            if (path.Split('.')[1] == "png")
            {
                sendImage(ctx, path);
            }
            else
            {
                sendUnknown(ctx, path);
            }
        } 
        else
        {
            notFound(ctx);
        }
    }
    catch(DirectoryNotFoundException e)
    {
        notFound(ctx);
    }
    catch (FileNotFoundException e)
    {
        notFound(ctx);
    }
    */
}
listener.Stop();

void log(HttpListenerRequest req, HttpListenerResponse resp)
{
    string logLine = "";
    using (FileStream fs = File.OpenWrite(logFilePath))
        using (StreamWriter sw = new StreamWriter(fs,Encoding.UTF8))
        {
        logLine = $"{DateTime.UtcNow};" +
            $"{req.RemoteEndPoint};" +
            $"{req.Url};" +
            $"{resp.StatusCode};";
        sw.WriteLine(logLine);
        }
    Console.WriteLine(logLine);
}

/*
void notFound(HttpListenerContext ctx)
{
    using HttpListenerResponse resp = ctx.Response;
    resp.Headers.Set("Content-Type", "text/plain");
    Console.WriteLine("");
    resp.StatusCode = (int)HttpStatusCode.NotFound;
    string err = "404 - not found";

    byte[] ebuf = Encoding.UTF8.GetBytes(err);
    resp.ContentLength64 = ebuf.Length;
    Stream ros = resp.OutputStream;
    ros.Write(ebuf, 0, ebuf.Length);
    log(ctx.Request, resp);
}

void sendImage(HttpListenerContext ctx, string? path)
{
    using HttpListenerResponse resp = ctx.Response;
    resp.Headers.Set("Content-Type", "image/png");

    byte[] buf = File.ReadAllBytes($"C:\\AOLEG\\MAG2\\ServerProg\\SP_HTTPListener_lab1\\{path}");
    resp.ContentLength64 = buf.Length;

    using Stream ros = resp.OutputStream;
    ros.Write(buf, 0, buf.Length);
    log(ctx.Request, resp);
}
void sendHTML(HttpListenerContext ctx)
{
    using HttpListenerResponse resp = ctx.Response;
    resp.Headers.Set("Content-Type", "text/html");
    resp.StatusCode = 200;
    byte[] buf = File.ReadAllBytes("C:\\AOLEG\\MAG2\\ServerProg\\SP_HTTPListener_lab1\\CSS Zen Garden_ The Beauty of CSS Design.html");
    resp.ContentLength64 = buf.Length;

    using Stream ros = resp.OutputStream;
    ros.Write(buf, 0, buf.Length);
    log(ctx.Request, resp);
}

void sendUnknown(HttpListenerContext ctx, string? path)
{
    using HttpListenerResponse resp = ctx.Response;
    resp.Headers.Set("Content-Type", "multipart/form-data");
    resp.StatusCode = 200;
    byte[] buf = File.ReadAllBytes($"C:\\AOLEG\\MAG2\\ServerProg\\SP_HTTPListener_lab1\\{path}");
    resp.ContentLength64 = buf.Length;

    using Stream ros = resp.OutputStream;
    ros.Write(buf, 0, buf.Length);
    log(ctx.Request, resp);
}*/