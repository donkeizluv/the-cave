using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class VoteResultDto
    {
        public string Id { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }
}