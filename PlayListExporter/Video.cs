namespace PlayListExporter
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Url
        {
            get
            {
                return "https://www.youtube.com/watch?v=" + Id;
            }
        }

        public void Load()
        {
            var v = Load(Id);
            Description = v.Description;
            Title = v.Title;
            
        }
        static Video Load(string videoId)
        {
            var data = new youTubeJson();
            return data.VideoData(videoId);
        }
    }
}