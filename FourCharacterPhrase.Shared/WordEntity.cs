using System;
using System.Collections.Generic;
using System.Text;

namespace FourCharacterPhrase.Shared
{
    public class WordEntity
    {
        public string Value { get; set; }

        public List<Char> GetOneCharacter()
        {
            var returnValue = new List<char>();
            returnValue.AddRange(Value.ToCharArray());
            return returnValue;
        }

        public string GetCharSortValue()
        {
            var returnValue = new List<char>();
            returnValue.AddRange(Value.ToCharArray());
            returnValue.Sort();
            return new string(returnValue.ToArray());
        }
    }
}
