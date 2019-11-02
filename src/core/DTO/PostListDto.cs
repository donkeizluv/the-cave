using System;

namespace CaveCore.DTO
{
    public class PostListDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public string CateName { get; set; }

        public long UpVotes { get; set; }

        public long DownVotes { get; set; }

        public DateTime Created { get; set; }

        public string CreatorName { get; set; }

    }
}