using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using Feel2Scale.Data; // Assuming ScaleData is in this namespace
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Feel2Scale.Configuration;

namespace OpenAIApi
{
    public class OpenAIService
    {
    

        private ChatClient _client;

        public OpenAIService(OpenAISettings settings)
        {
            
            // Deserialize the OpenAI settings from the configuration section
            
            if (settings == null)
            {
                throw new ArgumentException("OpenAI API key is not configured. Please set the 'OpenAI' key in your configuration file.");
            }
            // Initialize the OpenAI client with the API key from configuration
            _client = new ChatClient(model: settings.Model, apiKey: settings.ApiKey);
        }


        public ScaleData GetScaleAttributes(string userMessage)
        {
            ChatCompletion completion = _client.CompleteChat(new ChatMessage[]
            {
                new SystemChatMessage("You are a helpful assistant that provides information about musical scales and chords. You only provide only Json format with few variables that match the environment given in the request: ScaleName, Scale- array of greek numbers that are related to the scale like 'I','IIIm' etc, Chords- array of all the chords given by a certain random key, Instruments: array of instruments names that will fit, Effects: array of effects that are worth playing with, Message: suggestion from the AI and explanation why does it works, should be a string only. Note that it should be only Json without any other message before or after. This is used to parse later in C#."),
                new UserChatMessage(userMessage)
            });
            
            string jsonResponse = completion.Content[0].Text;
            int start = jsonResponse.IndexOf('{');
            int end = jsonResponse.LastIndexOf('}') + 1;
            if (start < 0 || end < 0 || start >= end)
            {
                Console.WriteLine("Invalid JSON response format.");
                return null;
            }
            jsonResponse = jsonResponse.Substring(start, end - start);

            return System.Text.Json.JsonSerializer.Deserialize<ScaleData>(jsonResponse); 
        }

        
    }
}
