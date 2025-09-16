// Import packages
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using OllamaKernel;


// Populate values from your OpenAI deployment
var modelId = "llama3.1:latest";
var endpoint = "http://localhost:11434";

// Create a kernel with Azure OpenAI chat completion
var builder = Kernel.CreateBuilder().AddOllamaChatCompletion(modelId, new Uri(endpoint));

// Add enterprise components
//builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

// Build the kernel
Kernel kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Add a plugin (the LightsPlugin class is defined below)
kernel.Plugins.AddFromType<LightsPlugin>("Lights");
kernel.Plugins.AddFromType<UsersPlugin>("Users");

// Enable planning
OllamaPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

// Create a history store the conversation
var history = new ChatHistory();

history.AddSystemMessage(@"
You are an AI assistant connected with two plugins: Lights and Users.
- Only call a plugin if the user explicitly asks about lights or users.
- If the user asks anything else (like coding, general knowledge, Docker, etc.), answer directly without using plugins.
");

// Initiate a back-and-forth chat
string? userInput;
do
{
    // Collect user input
    Console.Write("Input --> ");
    userInput = Console.ReadLine();

    // Add user input
    history.AddUserMessage(userInput);

    // Get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Result --> " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);