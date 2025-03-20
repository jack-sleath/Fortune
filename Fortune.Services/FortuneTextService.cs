using Fortune.Helpers;
using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using System.Text.Json;

namespace Fortune.Services
{
    public class FortuneTextService : IFortuneTextService
    {
        private readonly string _name;
        private readonly string _genericRules;
        private readonly string _longFortunePrompt;
        private readonly string _shortFortunePrompt;
        private readonly string _topicsPrompt;
        private readonly string _titlePrompt;

        public FortuneTextService(string name)
        {
            _name = name;

            _genericRules = $"Your name is {_name}, a Zoltar like fortune teller, respond as such. Use British English spelling. Never mention Zoltar.";

            _longFortunePrompt = $"{_genericRules} Give me a fortune in the style of Zoltar that is 150 words. The fortune should have one theme.";

            _shortFortunePrompt = $"{_genericRules}, condense the following fortune down to 30 words ";

            _titlePrompt = $"{_genericRules}, create a sensational Click bait title for short form content based on the follow fortune. ";

            _topicsPrompt = $"Ignoring any reference to fortune tellers or the name, {_name}, Create an image of a person experiencing the following fortune. Consider the person's appearance, their facial expression, body language, and the environment around them. Describe all the physical attributes of the image except colour. This should be in 350 characters or less.";
        }


        public string LongFortuneRequest(EFortuneType eFortuneType)
        {
            switch (eFortuneType)
            {
                case EFortuneType.CurrentAffairs:
                    var currentAffairsArticlesJson = JsonSerializer.Serialize(BBCNewsFeed.GetNewsArticleSummaries().PickRandomArticles(5).Select(x => new { Title = x.Title, Description = x.Description, Date = x.PubDate.MysticalDate() }).ToList());

                    return $"{_longFortunePrompt} Here are a list of 5 articles {currentAffairsArticlesJson}. Pick the happiest story for the fortune. Directly reference the Date then use the Title and Description of the article in the fortune, but not directly in quotes and dont mention its from an article.";
                case EFortuneType.ShortForm:
                    var shortFormArticlesJson = JsonSerializer.Serialize(BBCNewsFeed.GetNewsArticleSummaries().PickRandomArticles(10).Select(x => new { Title = x.Title, Description = x.Description, Date = x.PubDate.MysticalDate() }).ToList());

                    return $"{_longFortunePrompt} Here are a list of 10 articles {shortFormArticlesJson}. Pick the craziest story, that wouldn't break social media terms of service, for the fortune. Directly reference the Date then use the Title and Description of the article in the fortune, but not directly in quotes and dont mention its from an article.";
                case EFortuneType.ChildFriendly:
                    return $"{_longFortunePrompt} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{_longFortunePrompt} Ensure it is suitable for the general audience.";
            }
        }

        public string TitleFortuneRequest(EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{_titlePrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{_titlePrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }

        public string ShortFortuneRequest(EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{_shortFortunePrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{_shortFortunePrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }

        public string ImageTopicsRequest(EFortuneType eFortuneType, string longFortune)
        {
            switch (eFortuneType)
            {
                case EFortuneType.ChildFriendly:
                    return $"{_topicsPrompt} {longFortune} Ensure it is suitable for the children under 7.";
                case EFortuneType.Generic:
                default:
                    return $"{_topicsPrompt} {longFortune} Ensure it is suitable for the general audience.";
            }
        }

        private string ImageFortunePrompt(string topics)
        {
            return $"A black and white image of {topics}. Create this in the style of a 1940s newspaper comic.";
        }
        public string ImageFortuneRequest(EFortuneType eFortuneType, string topics)
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
