# LlamaTalker NPC Chat Prototype

This project demonstrates a prototype for chatting with NPCs (Non-Player Characters) in a game using the `LLamaTalkerLib` library. The project integrates with the LLamA model for natural language processing and allows you to interact with various NPCs in different scenarios.

## Table of Contents

- [Project Structure](#project-structure)
- [Installation](#installation)
- [NPC Definitions](#npc-definitions)
- [Scenario Definitions](#scenario-definitions)
- [Running the Program](#running-the-program)
- [Contributing](#contributing)
- [License](#license)

## Project Structure

- `Program.cs`: The main entry point of the project, where NPCs and scenarios are defined and interaction sessions are managed.
- `LlamaTalkerLib`: A library that manages NPC interactions and scenarios.

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/bitninja/LlamaTalkerPrototype.git
    ```
2. Navigate to the project directory:
    ```bash
     cd llamatalker-npc-chat
     ```
3. You will need to download a model library in gguf format and place in a directory that you will reference at the top of the program file.  Currently it is hard coded to use the file openhermes-2.5-mistral-7b.Q3_K_L.gguf.

## NPC Definitions
The program defines three NPCs:

   * Gorim Stonefist: The butcher shop owner, gruff but kind-hearted.
   * Marla Weaver: A cheerful townsfolk, always ready to gossip.
   * Guardian-12: A stoic defense droid, ensuring town safety.

## Scenario Definitions
Three scenarios are defined where the player can interact with these NPCs:

  * Butcher Shop: Engage Gorim Stonefist about the day's special meat cuts.
  * Market Square: Chat with Marla Weaver about local events and news.
  * Town Gate: Report a potential security breach to Guardian-12.

## Running the Program
Open the project in your favorite C# IDE (e.g., Visual Studio).
Run the program. The program will start by initiating a chat session with the first NPC and scenario.
Input your text and interact with the NPCs. Type exit to end the session.

## Example Interaction
When the program starts, it will initiate a chat with Gorim Stonefist:

```bash
User: System: Hello, how can I help you today?
User: What kind of meats do you have fresh today?
System: Our fresh meat selection includes chicken, pork, and beef. Would you like to know more about any specific cuts or recipes?
User: Chicken sounds good, what cuts do you have?
System: We have a variety of chicken cuts available, including whole chickens, chicken breasts, chicken thighs, drumsticks, and wings. Which cut would you like to order?
```

## Contributing
If you'd like to contribute to this project, please fork the repository and use a feature branch. Pull requests are warmly welcome.  Please keep in mind though that this is a basic prototype and nothing more.  Please only use for learning purposes.

## License
This project is licensed under the MIT License - see the LICENSE file for details.