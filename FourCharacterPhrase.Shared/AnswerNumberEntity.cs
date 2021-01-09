using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FourCharacterPhrase.Shared
{
    public class AnswerNumberEntity
    {
        [Required(ErrorMessage = "名前を入力してください")]
        public string Name { get; set; }

        public int? Count { get; set; }

        public int? ElapsedTime { get; set; }
    }
}
