using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportImplementation;

namespace PlayListExporter
{
    public class Playlist
    {
        public Video[] VideoIds { get; set; }
        public string PlayListId { get; set; }

        public void Load()
        {
            var data = new youTubeJson();
            VideoIds = data.VideoFromPlayListId(PlayListId)
                .Select(it => new Video() {Id = it}).ToArray();

            foreach (var videoId in VideoIds)
            {
                Console.WriteLine("Loading video details: "+ videoId.Id);
                videoId.Load();                
            }
        }

        public void ExportVideos(ExportToFormat exp, string fileName)
        {
            
            var data = ExportFactory.ExportData(VideoIds.ToList(), exp);
            File.WriteAllBytes(fileName, data);            
            


        }
    }
}
