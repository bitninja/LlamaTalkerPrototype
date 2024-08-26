using LLama.Common;
using LLama;
using Serilog;
using LlamaTalkerLib;

string file = "openhermes-2.5-mistral-7b.Q3_K_L.gguf";
string modelPath = @$"C:\Projects\GameSmarts\LLM\{file}";


var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();


var talker = new LlamaTalker(modelPath);


talker.AddNPC(new NonPlayerCharacter
{
    Id = 1,
    Personality = "Gruff but kind-hearted, always eager to chat about meat cuts and recipes.",
    Scenario = "Owner of the town's butcher shop, known for the best cuts of meat in the region.",
    Appearance = "A burly man with a stained apron, a thick beard, and a kind smile hidden under a rough exterior.",
    Name = "Gorim Stonefist",
    Affiliations = new string?[] { "Town Guild", "Merchant's Association" },
    PossibleCommands = new NPCSystemCommandDetails
    {
        Description = "Talk to the butcher about today's special cut.",
        Command = "Discuss meat selection"
    }
});

talker.AddNPC(new NonPlayerCharacter
{ 
    Id = 2,
    Personality = "Cheerful and friendly, always ready to lend a hand or share local gossip.",
    Scenario = "A regular townsperson who knows everyone and everything happening in town.",
    Appearance = "A middle-aged woman with a warm smile, wearing simple, practical clothing suitable for daily errands.",
    Name = "Marla Weaver",
    Affiliations = new string?[] { "Local Church", "Town Watch" },
    PossibleCommands = new NPCSystemCommandDetails
    {
        Description = "Engage in conversation to learn about local events and news.",
        Command = "Chat about town news"
    }
});

talker.AddNPC(new NonPlayerCharacter
{
    Id = 3,
    Personality = "Stoic and efficient, strictly follows its programming to ensure town safety.",
    Scenario = "A defense droid assigned to patrol the town, ensuring peace and order.",
    Appearance = "A tall, humanoid robot with a sleek metal chassis, glowing blue eyes, and a series of sensors across its body.",
    Name = "Guardian-12",
    Affiliations = new string?[] { "Town Security Force" },
    PossibleCommands = new NPCSystemCommandDetails
    {
        Description = "Inquire about security protocols and any recent threats.",
        Command = "Request security status"
    }
});

talker.AddScenario(new Scenario
{
    Id = 1,
    Description = "The player enters the butcher shop, the aroma of fresh cuts filling the air.",
    Location = "Gorim Stonefist's Butcher Shop",
    IntendedEmotionalInfluenceOnNPC = "Engage Gorim's passion for his craft by asking about today's special cuts.",
    MoreStoryContext = "You've been hearing rumors of a rare meat cut available only this season, and Gorim is the go-to expert.",
    InformationOnlyAvailableToNPC = "Gorim knows the exact details about the special cut, including its origin and how to prepare it."
});

talker.AddScenario(new Scenario
{
    Id = 2,
    Description = "The player strolls through the bustling market and spot Marla Weaver at her usual spot, chatting with other townsfolk.",
    Location = "Market Square",
    IntendedEmotionalInfluenceOnNPC = "Appeal to Marla's curiosity by mentioning a recent strange event in town.",
    MoreStoryContext = "The town has been abuzz with talk of an upcoming festival, and Marla is always in the know.",
    InformationOnlyAvailableToNPC = "Marla has insider information about the festival that could be crucial for planning your next move."
});

talker.AddScenario(new Scenario
{
    Id = 3,
    Description = "The player approachs the town's main gate, Guardian-12 is on patrol, scanning for any signs of trouble.",
    Location = "Town Gate",
    IntendedEmotionalInfluenceOnNPC = "Instill a sense of urgency by reporting a potential security breach near the gate.",
    MoreStoryContext = "You've noticed unusual activity in the outskirts of town and suspect it might be related to recent rumors of bandit attacks.",
    InformationOnlyAvailableToNPC = "Guardian-12 has access to recent security logs and can confirm whether there have been any unusual activities."
});



Task.Run(()=> talker.StartNewNPCChatSessionAsync(1, 1, "Hello!", (a,b)=> Console.Write(a))).Wait();

string userInput = "";

while (userInput != "exit")
{
    if (userInput.Trim().Length > 0)
    {

        Task.Run(()=>talker.ChatWithNPCAsync(userInput, (partial, complete) =>
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(partial);
        })).Wait();
    }

    Console.ForegroundColor = ConsoleColor.Green;
    userInput = Console.ReadLine() ?? "";
}

return;
