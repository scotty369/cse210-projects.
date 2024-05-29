using System;

namespace YouTubeVideoComments
{
    public class Comment
    {
        public string CommenterName { get; set; }
        public string CommentText { get; set; }

        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }

        public override string ToString()
        {
            return $"Commenter: {CommenterName}, Comment: {CommentText}";
        }
    }

    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Length: {Length} seconds, Comments: {GetCommentCount()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create videos
            Video video1 = new Video("C# Tutorial", "John Doe", 600);
            Video video2 = new Video("Python for Beginners", "Jane Smith", 1200);
            Video video3 = new Video("JavaScript Basics", "Alice Johnson", 900);

            // Add comments to video1
            video1.AddComment(new Comment("User1", "Great tutorial!"));
            video1.AddComment(new Comment("User2", "Very helpful, thanks!"));
            video1.AddComment(new Comment("User3", "Clear and concise."));

            // Add comments to video2
            video2.AddComment(new Comment("User4", "Excellent explanation."));
            video2.AddComment(new Comment("User5", "I learned a lot."));
            video2.AddComment(new Comment("User6", "Perfect for beginners."));

            // Add comments to video3
            video3.AddComment(new Comment("User7", "Good intro to JavaScript."));
            video3.AddComment(new Comment("User8", "Well done!"));
            video3.AddComment(new Comment("User9", "Very informative."));

            // Put videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Iterate through the list of videos and display information
            foreach (var video in videos)
            {
                Console.WriteLine(video);
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"  {comment}");
                }
                Console.WriteLine();
            }
        }
    }
}
