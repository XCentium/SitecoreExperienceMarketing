using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEM.Constants
{
    public class RegexConstants
    {
        /// <summary>
        /// regex check only allow aplhabet
        /// </summary>
        public const string REGEX_ONLY_APLHABET = "^[a-zA-Z]+$";

        /// <summary>
        /// regex check only allow aplhabet and/or space
        /// </summary>
        public const string REGEX_ONLY_APLHABETSPACE = @"^[a-zA-Z\s]+$";

        /// <summary>
        /// regex check only allow aplhabet, number and space
        /// </summary>
        public const string REGEX_SENTENCE = @"^[a-zA-Z0-9\s]+$";

        /// <summary>
        /// find character before hyphen
        /// </summary>
        public const string REGEX_FINDCHAR_BEFORE_HYPHEN = @"^.*?(?=-)";

        /// <summary>
        /// number regex
        /// </summary>
        public const string REGEX_NUMBER = @"\d+";

        /// <summary>
        /// not number
        /// </summary>
        public const string REGEX_NOT_NUMBER = @"[^0-9]+";
    }
}
