using Fortune.Models.Enums;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Fortune.Helpers
{
    public static class FortuneHelper
    {
        private static string Name = "Professor Fortuna";

        private static string GenericRules = $"Your name is {Name}, a Zoltar like fortune teller, respond as such. Use British English spelling. Never mention Zoltar.";

        private static string LongFortunePrompt = $"{GenericRules} Give me a fortune in the style of Zoltar that is 150 words. The fortune should have one theme.";
        public static string LongFortuneRequest(this EFortuneType eFortuneType)
        {
            switch (eFortuneType)
            {
                case EFortuneType.CurrentAffairs:
                    var articlesJson = JsonSerializer.Serialize(BBCNewsFeed.GetNewsArticleSummaries().PickRandomArticles(5).Select(x=> new { Title = x.Title, Description = x.Description, Date = x.PubDate.MysticalDate()}).ToList());

                    return $"{LongFortunePrompt}Here are a list of 5 articles {articlesJson}. Pick the happiest story for the fortune. Directly reference the Date then use the Title and Description of the article in the fortune, but not directly in quotes and dont mention its from an article.";
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

        private static string TopicsPrompt = $"Ignoring any reference to fortune tellers or the name, {Name}, Create an image of a person experiencing the following fortune. Consider the person's appearance, their facial expression, body language, and the environment around them. Describe all the physical attributes of the image except colour. This should be in 350 characters or less.";

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
            return $"A black and white image of {topics}. Create this in the style of a 1940s newspaper comic.";
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
