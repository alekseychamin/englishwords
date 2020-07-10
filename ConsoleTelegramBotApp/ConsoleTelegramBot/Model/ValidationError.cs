using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTelegramBot.Model
{
    public class ValidationError
    {
        public string[] Id { get; set; }
        public string[] WordPhrase { get; set; }
    }
}
