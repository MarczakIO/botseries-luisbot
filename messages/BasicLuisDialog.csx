using System;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

// For more information about this template visit http://aka.ms/azurebots-csharp-luis
[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public const string FacebookUrl = "https://www.facebook.com/MarczakIO/";
    public const string TwitterUrl = "https://twitter.com/MarczakIO";

    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
    }

    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
        context.Wait(MessageReceived);
    }

    [LuisIntent("Series.Info")]
    public async Task SeriesInfoIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"This Azure Bot Series - Smarter Bots.");
        context.Wait(MessageReceived);
    }
    
    [LuisIntent("Series.Author")]
    public async Task SeriesAuthorIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"Azure Bot Series are made by Adam Marczak. Visit Marczak.IO for more information.");
        context.Wait(MessageReceived);
    }
    
    [LuisIntent("Social.Location")]
    public async Task SocialLocationIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"Hey social media is my thing!");
        
        EntityRecommendation socialName;
        if (result.TryFindEntity("SocialName", out socialName)) {
            
            await context.PostAsync($"I know this social media! Let me check the URL for you!");
            
            switch(socialName.Entity) {
                case "facebook":
                    await context.PostAsync($"Here's the link {FacebookUrl}");
                    break;
                case "twitter":
                    await context.PostAsync($"Here's the link {TwitterUrl}");
                    break;
                default:
                    await context.PostAsync($"Sadly I don't have link for this on me :(");
                    break;
            }
            
        } else {
            await context.PostAsync($"Sadly I don't recognize that social media name :(");
        }
        
        context.Wait(MessageReceived);
    }
}