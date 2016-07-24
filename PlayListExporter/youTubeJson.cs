using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlayListExporter
{
    public class youTubeJson
    {
        public static string AuthKey = "";

        internal string[] VideoFromPlayListId(string playListId)
        {
            List<string> ret=new List<string>();
            string reqUrl =
                string.Format(
                    "https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails&maxResults=50&playlistId={0}&key={1}",
                    playListId, AuthKey);
            var req = (HttpWebRequest)WebRequest.Create(reqUrl);
            req.ContentType = "text/json";
            req.Method = "GET";
            
            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                dynamic x = JsonConvert.DeserializeObject(responseText);
                var videos = x.items;
                foreach (var album in videos)
                {
                    var videoId = album["contentDetails"]["videoId"];
                    ret.Add(videoId.Value);
                }
            }
            return ret.ToArray();
        }

        internal Video VideoData(string videoId)
        {
            
            string reqUrl =
                string.Format(
                    "https://www.googleapis.com/youtube/v3/videos?part=snippet&id={0}&key={1}",
                    videoId, AuthKey);
            var req = (HttpWebRequest)WebRequest.Create(reqUrl);
            req.ContentType = "text/json";
            req.Method = "GET";

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                
                var responseText = streamReader.ReadToEnd();
                var v = new Video();
                v.Id = videoId;

                dynamic x = JsonConvert.DeserializeObject(responseText);
                if (x.pageInfo.totalResults.Value != 0)
                {
                    var snippet = x.items[0].snippet;
                    
                    v.Title = snippet.title;
                    v.Description = snippet.description;

                }
                return v;
            }
            
        }
    }
}
