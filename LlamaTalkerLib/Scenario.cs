using System.Text;

namespace LlamaTalkerLib
{
    public class Scenario
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string IntendedEmotionalInfluenceOnNPC { get; set; }
        public string MoreStoryContext { get; set; }
        public string InformationOnlyAvailableToNPC { get; set; }

        public StringBuilder AppendLLMString(StringBuilder? builder = null)
        {
            builder ??= new StringBuilder();
            builder.Append($"PlayerDescription: {Description}");
            builder.Append($"Location: {Location}");
            builder.Append($"IntendedEmotionalInfluenceOnNPC: {IntendedEmotionalInfluenceOnNPC}");
            builder.Append($"MoreStoryContext: {MoreStoryContext}");
            builder.Append($"InformationOnlyAvailableToNPC: {InformationOnlyAvailableToNPC}");
            return builder;
        }
    }
}
