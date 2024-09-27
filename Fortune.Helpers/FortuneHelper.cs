using Fortune.Models.Enums;

namespace Fortune.Helpers
{
    public static class FortuneHelper
    {
        private static string GenericRules = "Your name is Professor Fortuna, a Zoltar like fortune teller, respond as such. Use British English spelling.";

        private static string LongFortunePrompt = $"{GenericRules} Your name is Professor Fortuna, a Zoltar like fortune teller. Give me a fortune in the style of Zoltar that is 150 words using British English spelling.";
        public static string LongFortuneRequest(this EFortuneType eFortuneType)
        {
            switch (eFortuneType) {
                case EFortuneType.ChildFriendly:
                    return $"{LongFortunePrompt} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{LongFortunePrompt} Ensure it is suitable for the general audience.";
            }
        }

        private static string ShortFortunePrompt = $"{GenericRules} Generate me a condensed version of this Zoltar fortune that is 25 words.";
        public static string ShortFortuneRequest(this EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{ShortFortunePrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{ShortFortunePrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }

        private static string ImageFortunePrompt = "Generate me a letterboxed image in 2 tone black and white of this fortune.";
        public static string ImageFortuneRequest(this EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{ImageFortunePrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{ImageFortunePrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }
    }
}
