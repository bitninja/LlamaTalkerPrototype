using LLama;
using LLama.Common;
using Microsoft.Extensions.Logging;
using System.Text;

namespace LlamaTalkerLib
{
    public class LlamaTalker : IDisposable
    {
        public ChatHistory History { get; set; }
        public InferenceParams InferenceParameters { get; set; }

        public ChatSession Session { get; private set; }

        public InteractiveExecutor Executor { get; private set; }
        public LLamaWeights Weights { get; private set; }

        public LLamaContext Context { get; private set; }

        public ModelParams ModelParams { get; set; }

        public ILogger? Logger { private get;  set; }

        private string _modelPath;

        private Action _chatInitializer;

        private IDictionary<int, NonPlayerCharacter> _idToNPC;
        private IDictionary<int, Scenario> _idToScenario;

        public LlamaTalker(string modelPath)
        {
            _modelPath = modelPath;

            _idToNPC = new Dictionary<int, NonPlayerCharacter>();
            _idToScenario = new Dictionary<int, Scenario>();


            // Initialize chat.
            ModelParams ??= CreateDefaultModelParams();

            Weights = LLamaWeights.LoadFromFile(ModelParams);
            Logger?.LogTrace("Loaded model weights.");

            Context = Weights.CreateContext(ModelParams);
            Logger?.LogTrace("Created weigths context.");

            Executor = new InteractiveExecutor(Context);
            Logger?.LogTrace("Completed creating executor.");

            InferenceParameters ??= CreateDefaultInferenceParams();

            LoadAndFirstUseModel();
        }

        private void LoadAndFirstUseModel()
        {
            History = new ChatHistory();
            History.AddMessage(AuthorRole.System, "Initializing.");

            Session = new ChatSession(Executor, History);

            ChatAsync(AuthorRole.User, "Just say the word 'OK'", (a,b)=>Console.Write(a)).Wait();
        }

        public void ConfigChatHistory(Action<ChatHistory> onHistory)
        {
            History = new ChatHistory();
            onHistory?.Invoke(History);
        }

        protected ChatSession CreateDefaultSession() => new(Executor, History);
        protected InferenceParams CreateDefaultInferenceParams() => new()
        {
            MaxTokens = 256, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
            AntiPrompts = new List<string> { "User:" } // Stop generation once antiprompts appear.
        };

        protected ModelParams CreateDefaultModelParams() => new(_modelPath)
        {
            ContextSize = 4096, // The longest length of chat as memory.
            GpuLayerCount = 100 // How many layers to offload to GPU. Please adjust it according to your GPU memory.
        };

        public async Task StartNewNPCChatSessionAsync(int npcId, int scenarioId, string initalText, Action<string, string>? liveTextUpdate = null, int responseSizeLimit = 0)
        {
            var npc = _idToNPC[npcId];
            var scenario = _idToScenario[scenarioId];

            var instructions = new StringBuilder();
            instructions.Append("You will now roleplay as the following character:");
            npc.AppendLLMString(instructions);
            instructions.Append("Here are the scenario details for your character:");
            scenario.AppendLLMString(instructions);
            instructions.Append("Please keep your responses always less than 30 words.  I will play as the player, please only respond once you have been talked to by me. ");
            instructions.AppendLine("Also your responses should only include the words from the player you are playing, do not respond with scenario information.");

            History = new ChatHistory();
            History.AddMessage(AuthorRole.System, instructions.ToString());

            Session = new ChatSession(Executor, History);

            await ChatAsync(AuthorRole.User, initalText, liveTextUpdate, responseSizeLimit);
        }
        public async Task ChatWithNPCAsync(string chatText, Action<string, string>? liveTextUpdate = null, int responseSizeLimit = 0)
        {
            await ChatAsync(AuthorRole.User, chatText, liveTextUpdate, responseSizeLimit);
        }

        public async Task ChatAsync(AuthorRole role, string chatText, Action<string,string>? liveTextUpdate = null, int responseSizeLimit = 0)
        {
          //  _chatInitializer?.Invoke();

            var response = "";

            var chatEnumerator = Session.ChatAsync(new ChatHistory.Message(role, chatText), inferenceParams: InferenceParameters);
            
            await foreach(var text in chatEnumerator)
            {
                response += text;
                liveTextUpdate?.Invoke(text, response);

                if (responseSizeLimit > 0)
                {
                    if (response.Length > responseSizeLimit)
                    {
                        break;
                    }
                }
            }
        }

        public void AddNPC(NonPlayerCharacter npc)
        {
            _idToNPC[npc.Id] = npc;
        }

        public void AddScenario(Scenario scenario)
        {
            _idToScenario[scenario.Id] = scenario;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
