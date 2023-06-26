using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.10/docs/coded-automations.
namespace OpenAIWrapper
{
    public class GPTController
    {
        private GPTConfiguration config;
        
        public GPTController(){
            config = new GPTConfiguration();
            config.Init();
        }
        
      
        public string AskGPT(string prompt)
        {
             return  AskGPTAsync(prompt).GetAwaiter().GetResult();
        }
        
         private async Task<String> AskGPTAsync(string prompt)
        {

            string model = config.Model;
            int maxTokens = config.MaxTokens;
            string[] stop = { config.StopToken }; // Example stop sequences
            double temperature = config.Temperature;

            string apiKey = Environment.GetEnvironmentVariable(config.ApiKeyVariable);
            string apiUrl = config.BaseAPIUrl + "/"+ model +"/" + config.Capability;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new { prompt = prompt, max_tokens = maxTokens, stop = stop, temperature = temperature };
            var requestContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine(requestContent.ReadAsStringAsync().Result);

            var response = await client.PostAsync(apiUrl, requestContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            string text = jsonResponse.choices[0].text;

            return text; 
    }

        
        
    }
}