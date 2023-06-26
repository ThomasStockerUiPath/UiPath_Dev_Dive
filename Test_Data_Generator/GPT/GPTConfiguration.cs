using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class GPTConfiguration
    {
        public GPTConfiguration()
        {
            
        }

        public void Init()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "GPT\\Config.json");
            string jsonContent = File.ReadAllText(jsonFilePath);
            GPTConfiguration parsedConfig = JsonConvert.DeserializeObject<GPTConfiguration>(jsonContent);

            Model =parsedConfig.Model;
            MaxTokens =parsedConfig.MaxTokens;
            StopToken =parsedConfig.StopToken;
            Temperature = parsedConfig.Temperature;
            ApiKeyVariable = parsedConfig.ApiKeyVariable;
            BaseAPIUrl = parsedConfig.BaseAPIUrl;
            Capability = parsedConfig.Capability;
        }


        public string Model { get; set; }
        public int MaxTokens { get; set; }
        public string StopToken { get; set; }
        public double Temperature { get; set; }
        public string ApiKeyVariable { get; set; }
        public string BaseAPIUrl { get; set; }
        public string Capability { get; set; }
    }
}