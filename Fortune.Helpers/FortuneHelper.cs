using Fortune.Models.Enums;
using System.Runtime.CompilerServices;

namespace Fortune.Helpers
{
    public static class FortuneHelper
    {
        private static string Name = "Professor Fortuna";

        private static string GenericRules = $"Your name is {Name}, a Zoltar like fortune teller, respond as such. Use British English spelling.";

        private static string LongFortunePrompt = $"{GenericRules} Give me a fortune in the style of Zoltar that is 150 words. The fortune should have one theme.";
        public static string LongFortuneRequest(this EFortuneType eFortuneType)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{LongFortunePrompt} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{LongFortunePrompt} Ensure it is suitable for the general audience.";
            }
        }

        private static string ShortFortunePrompt = $"{GenericRules}, condense the following fortune down to 30 words ";
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

        private static string TopicsPrompt = $"Ignoring any reference to fortune tellers or the name, {Name}, Create an image of a person experiencing the following fortune. Consider the person's appearance, their facial expression, body language, and the environment around them. Describe all the physical attributes of the image.This should be in 350 characters or less.";

        public static string ImageTopicsRequest(this EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{TopicsPrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{TopicsPrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }
        
        private static string ImageFortunePrompt(string topics)
        {
            return $"Generate a greyscale image of {topics}.";
        }
        public static string ImageFortuneRequest(this EFortuneType eFortuneType, string topics)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{ImageFortunePrompt} {ImageFortunePrompt(topics)} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{ImageFortunePrompt} {ImageFortunePrompt(topics)} Ensure it is suitable for the general audience.";
            }
        }
    }
}
