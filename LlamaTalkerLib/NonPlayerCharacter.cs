using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaTalkerLib
{
    public class NonPlayerCharacter
    {
        public string? Personality { get; set; }
        public string? Scenario { get; set; }
        public string? Appearance { get; set; }
        public NPCSystemCommandDetails PossibleCommands { get; set; }
        public string? Name { get; set; }
        public string?[] Affiliations { get; set; }
        public int Id { get; set; }

        public StringBuilder AppendLLMString(StringBuilder? builder = null)
        {
            builder ??= new StringBuilder();
            builder.Append($"Name: {Name}");
            builder.Append($"Personality: {Personality}");
            builder.Append($"Scenario: {Scenario}");
            builder.Append($"Appearance: {Appearance}");

            builder.Append("Affiliations: " + (Affiliations != null && Affiliations.Length > 0
                ? string.Join(", ", Affiliations)
                : "None"));
            return builder;
        }
    }

    public class NPCSystemCommandDetails
    {
        public string Description { get; set; }
        public string Command { get; set; }
    }

    
}
