using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportImplementation;
using PlayListExporter;

namespace PlaylistExport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please give auth key - ");
            youTubeJson.AuthKey = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(youTubeJson.AuthKey))
            {
                Console.WriteLine("please grab one from https://console.developers.google.com/");
                Console.ReadKey();
                return;
                
            }
            Console.WriteLine("playlist id - is from playlist URL");
            var plID = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(plID))
            {
                //https://www.youtube.com/playlist?list=PL4aSKgR4yk4OnmJW6PlBuDOXdYk6zTGps
                plID = "PL4aSKgR4yk4OnmJW6PlBuDOXdYk6zTGps";

            }
            var p = new Playlist();
            p.PlayListId = plID;
            p.Load();
            Console.WriteLine("Number Videos:"+p.VideoIds.Length);            
            p.ExportVideos(ExportToFormat.HTML, "pl.html");
            Process.Start("pl.html");

        }
    }
}
