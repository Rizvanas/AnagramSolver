﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class UserLogEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SearchPhraseId { get; set; }
        public PhraseEntity SearchPhrase { get; set; }

        public int UserId { get; set; }
        public UserEntity UserIp { get; set; }

        public int SearchTime { get; set; }
    }
}
