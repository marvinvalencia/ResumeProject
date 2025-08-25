namespace ResumeProject.Blazor.Services.GroqApiLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    /// <summary>
    /// The GroqApiClient class provides methods to interact with the Groq API.
    /// </summary>
    public class GroqApiClient : IDisposable
    {
        private const string BaseUrl = "https://api.groq.com/openai/v1";
        private const string ChatCompletionsEndpoint = "/chat/completions";
        private const string TranscriptionsEndpoint = "/audio/transcriptions";
        private const string TranslationsEndpoint = "/audio/translations";

        private const string VisionModels = "llama-3.2-90b-vision-preview,llama-3.2-11b-vision-preview";
        private const int MaxImageSizeMB = 20;
        private const int MaxBase64SizeMB = 4;

        private readonly HttpClient httpClient;

        /// <summary>
        /// The GroqApiClient constructor initializes a new instance of the GroqApiClient class.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        public GroqApiClient(string apiKey)
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        /// <summary>
        /// The GroqApiClient constructor initializes a new instance of the GroqApiClient class with a custom HttpClient.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        /// <param name="httpClient">The http client.</param>
        public GroqApiClient(string apiKey, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        /// <summary>
        /// Converts an image file to a Base64 string.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>String</returns>
        /// <exception cref="FileNotFoundException">The exception.</exception>
        private async Task<string> ConvertImageToBase64(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Image file not found: {imagePath}");
            }

            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        /// <summary>
        /// The CreateChatCompletionAsync method sends a chat completion request to the Groq API and returns the response.
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>Json.</returns>
        /// <exception cref="HttpRequestException">The exception.</exception>
        public async Task<JsonObject?> CreateChatCompletionAsync(JsonObject request)
        {
            var response = await this.httpClient.PostAsJsonAsync(BaseUrl + ChatCompletionsEndpoint, request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}. Response content: {errorContent}");
            }

            return await response.Content.ReadFromJsonAsync<JsonObject>();
        }


        /// <summary>
        /// The CreateChatCompletionStreamAsync method sends a chat completion request to the Groq API and returns a stream of responses.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Json.</returns>
        public async IAsyncEnumerable<JsonObject?> CreateChatCompletionStreamAsync(JsonObject request)
        {
            request["stream"] = true;
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, BaseUrl + ChatCompletionsEndpoint) { Content = content };
            using var response = await this.httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new System.IO.StreamReader(stream);
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("data: "))
                {
                    var data = line["data: ".Length..];
                    if (data != "[DONE]")
                    {
                        yield return JsonSerializer.Deserialize<JsonObject>(data);
                    }
                }
            }
        }

        /// <summary>
        /// The CreateTranscriptionAsync method sends an audio file to the Groq API for transcription and returns the response.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="model">The model.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="responseFormat">The response format.</param>
        /// <param name="language">The language.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Json.</returns>
        public async Task<JsonObject?> CreateTranscriptionAsync(
            Stream audioFile,
            string fileName,
            string model,
            string? prompt = null,
            string responseFormat = "json",
            string? language = null,
            float? temperature = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(audioFile), "file", fileName);
            content.Add(new StringContent(model), "model");

            if (!string.IsNullOrEmpty(prompt))
            {
                content.Add(new StringContent(prompt), "prompt");
            }

            content.Add(new StringContent(responseFormat), "response_format");

            if (!string.IsNullOrEmpty(language))
            {
                content.Add(new StringContent(language), "language");
            }

            if (temperature.HasValue)
            {
                content.Add(new StringContent(temperature.Value.ToString()), "temperature");
            }

            var response = await this.httpClient.PostAsync(BaseUrl + TranscriptionsEndpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonObject>();
        }

        /// <summary>
        /// The CreateTranslationAsync method sends an audio file to the Groq API for translation and returns the response.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="model">The model.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="responseFormat">The response format.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>The Json object.</returns>
        public async Task<JsonObject?> CreateTranslationAsync(
            Stream audioFile,
            string fileName,
            string model,
            string? prompt = null,
            string responseFormat = "json",
            float? temperature = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(audioFile), "file", fileName);
            content.Add(new StringContent(model), "model");

            if (!string.IsNullOrEmpty(prompt))
            {
                content.Add(new StringContent(prompt), "prompt");
            }

            content.Add(new StringContent(responseFormat), "response_format");

            if (temperature.HasValue)
                content.Add(new StringContent(temperature.Value.ToString()), "temperature");

            var response = await this.httpClient.PostAsync(BaseUrl + TranslationsEndpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonObject>();
        }

        /// <summary>
        /// The CreateVisionCompletionAsync method sends a vision model request to the Groq API and returns the response.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Json Object.</returns>
        public async Task<JsonObject?> CreateVisionCompletionAsync(JsonObject request)
        {
            this.ValidateVisionModel(request);
            return await this.CreateChatCompletionAsync(request);
        }

        /// <summary>
        /// The CreateVisionCompletionWithImageUrlAsync method sends a vision model request with an image URL to the Groq API and returns the response.
        /// </summary>
        /// <param name="imageUrl">The Image URL.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="model">The model.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>The Json Object.</returns>
        public async Task<JsonObject?> CreateVisionCompletionWithImageUrlAsync(
            string imageUrl,
            string prompt,
            string model = "llama-3.2-90b-vision-preview",
            float? temperature = null)
        {
            this.ValidateImageUrl(imageUrl);

            var request = new JsonObject
            {
                ["model"] = model,
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = new JsonArray
                        {
                            new JsonObject
                            {
                                ["type"] = "text",
                                ["text"] = prompt,
                            },
                            new JsonObject
                            {
                                ["type"] = "image_url",
                                ["image_url"] = new JsonObject
                                {
                                    ["url"] = imageUrl,
                                },
                            },
                        },
                    },
                },
            };

            if (temperature.HasValue)
            {
                request["temperature"] = temperature.Value;
            }

            return await this.CreateVisionCompletionAsync(request);
        }

        /// <summary>
        /// The CreateVisionCompletionWithBase64ImageAsync method sends a vision model request with a Base64 encoded image to the Groq API and returns the response.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="model">The model.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>The Json Object.</returns>
        public async Task<JsonObject?> CreateVisionCompletionWithBase64ImageAsync(
            string imagePath,
            string prompt,
            string model = "llama-3.2-90b-vision-preview",
            float? temperature = null)
        {
            var base64Image = await this.ConvertImageToBase64(imagePath);
            this.ValidateBase64Size(base64Image);

            var request = new JsonObject
            {
                ["model"] = model,
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = new JsonArray
                        {
                            new JsonObject
                            {
                                ["type"] = "text",
                                ["text"] = prompt,
                            },
                            new JsonObject
                            {
                                ["type"] = "image_url",
                                ["image_url"] = new JsonObject
                                {
                                    ["url"] = $"data:image/jpeg;base64,{base64Image}",
                                },
                            },
                        },
                    },
                },
            };

            if (temperature.HasValue)
            {
                request["temperature"] = temperature.Value;
            }

            return await this.CreateVisionCompletionAsync(request);
        }

        /// <summary>
        /// The CreateVisionCompletionWithToolsAsync method sends a vision model request with an image URL and tools to the Groq API and returns the response.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="tools">The tools.</param>
        /// <param name="model">The model.</param>
        /// <returns>The Json Object.</returns>
        public async Task<JsonObject?> CreateVisionCompletionWithToolsAsync(
            string imageUrl,
            string prompt,
            List<Tool> tools,
            string model = "llama-3.2-90b-vision-preview")
        {
            this.ValidateImageUrl(imageUrl);

            var request = new JsonObject
            {
                ["model"] = model,
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = new JsonArray
                        {
                            new JsonObject
                            {
                                ["type"] = "text",
                                ["text"] = prompt,
                            },
                            new JsonObject
                            {
                                ["type"] = "image_url",
                                ["image_url"] = new JsonObject
                                {
                                    ["url"] = imageUrl,
                                },
                            },
                        },
                    },
                },
                ["tools"] = JsonSerializer.SerializeToNode(tools.Select(t => new
                {
                    type = t.Type,
                    function = new
                    {
                        name = t.Function.Name,
                        description = t.Function.Description,
                        parameters = t.Function.Parameters,
                    },
                })),
                ["tool_choice"] = "auto",
            };

            return await this.CreateVisionCompletionAsync(request);
        }

        /// <summary>
        /// The CreateVisionCompletionWithJsonModeAsync method sends a vision model request with an image URL and expects a JSON object response from the Groq API.
        /// </summary>
        /// <param name="imageUrl">The Image URL.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="model">The model.</param>
        /// <returns>The JSON Object.</returns>
        public async Task<JsonObject?> CreateVisionCompletionWithJsonModeAsync(
            string imageUrl,
            string prompt,
            string model = "llama-3.2-90b-vision-preview")
        {
            this.ValidateImageUrl(imageUrl);

            var request = new JsonObject
            {
                ["model"] = model,
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = new JsonArray
                        {
                            new JsonObject
                            {
                                ["type"] = "text",
                                ["text"] = prompt,
                            },
                            new JsonObject
                            {
                                ["type"] = "image_url",
                                ["image_url"] = new JsonObject
                                {
                                    ["url"] = imageUrl,
                                },
                            },
                        },
                    },
                },
                ["response_format"] = new JsonObject { ["type"] = "json_object" },
            };

            return await this.CreateVisionCompletionAsync(request);
        }

        /// <summary>
        /// The ListModelsAsync method retrieves the list of available models from the Groq API.
        /// </summary>
        /// <returns>The Json Object.</returns>
        public async Task<JsonObject?> ListModelsAsync()
        {
            HttpResponseMessage response = await this.httpClient.GetAsync($"{BaseUrl}/models");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            JsonObject? responseJson = JsonSerializer.Deserialize<JsonObject>(responseString);

            return responseJson;
        }

        /// <summary>
        /// The RunConversationWithToolsAsync method manages a conversation with tool usage, handling tool calls and responses.
        /// </summary>
        /// <param name="userPrompt">The User Prompt.</param>
        /// <param name="tools">The tools.</param>
        /// <param name="model">The model.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <returns>The String.</returns>
        public async Task<string> RunConversationWithToolsAsync(string userPrompt, List<Tool> tools, string model, string systemMessage)
        {
            try
            {
                var messages = new List<JsonObject>
                {
                    new JsonObject
                    {
                        ["role"] = "system",
                        ["content"] = systemMessage,
                    },
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = userPrompt,
                    },
                };

                var request = new JsonObject
                {
                    ["model"] = model,
                    ["messages"] = JsonSerializer.SerializeToNode(messages),
                    ["tools"] = JsonSerializer.SerializeToNode(tools.Select(t => new
                    {
                        type = t.Type,
                        function = new
                        {
                            name = t.Function.Name,
                            description = t.Function.Description,
                            parameters = t.Function.Parameters,
                        },
                    })),
                    ["tool_choice"] = "auto",
                };

                Console.WriteLine($"Sending request to API: {JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true })}");

                var response = await this.CreateChatCompletionAsync(request);
                var responseMessage = response?["choices"]?[0]?["message"]?.AsObject();
                var toolCalls = responseMessage?["tool_calls"]?.AsArray();

                if (toolCalls != null && toolCalls.Count > 0)
                {
                    messages.Add(responseMessage);
                    foreach (var toolCall in toolCalls)
                    {
                        var functionName = toolCall?["function"]?["name"]?.GetValue<string>();
                        var functionArgs = toolCall?["function"]?["arguments"]?.GetValue<string>();
                        var toolCallId = toolCall?["id"]?.GetValue<string>();

                        if (!string.IsNullOrEmpty(functionName) && !string.IsNullOrEmpty(functionArgs))
                        {
                            var tool = tools.Find(t => t.Function.Name == functionName);
                            if (tool != null)
                            {
                                var functionResponse = await tool.Function.ExecuteAsync(functionArgs);
                                messages.Add(new JsonObject
                                {
                                    ["tool_call_id"] = toolCallId,
                                    ["role"] = "tool",
                                    ["name"] = functionName,
                                    ["content"] = functionResponse,
                                });
                            }
                        }
                    }

                    request["messages"] = JsonSerializer.SerializeToNode(messages);
                    var secondResponse = await this.CreateChatCompletionAsync(request);
                    return secondResponse?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;
                }

                return responseMessage?["content"]?.GetValue<string>() ?? string.Empty;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// The ValidateVisionModel method checks if the provided vision model is valid.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="ArgumentException">The exception.</exception>
        private void ValidateVisionModel(JsonObject request)
        {
            var model = request["model"]?.GetValue<string>();
            if (string.IsNullOrEmpty(model) || !VisionModels.Contains(model))
            {
                throw new ArgumentException($"Invalid vision model. Must be one of: {VisionModels}");
            }
        }

        /// <summary>
        /// The ValidateBase64Size method checks if the Base64 encoded image exceeds the maximum allowed size.
        /// </summary>
        /// <param name="base64String">The base 64 string.</param>
        /// <exception cref="ArgumentException">The exception.</exception>
        private void ValidateBase64Size(string base64String)
        {
            double sizeInMB = (base64String.Length * 3.0 / 4.0) / (1024 * 1024);
            if (sizeInMB > MaxBase64SizeMB)
            {
                throw new ArgumentException($"Base64 encoded image exceeds maximum size of {MaxBase64SizeMB}MB");
            }
        }

        /// <summary>
        /// The ValidateImageUrl method checks if the provided image URL is valid.
        /// </summary>
        /// <param name="url">The Url.</param>
        /// <exception cref="ArgumentException">The exception.</exception>
        private void ValidateImageUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Image URL cannot be null or empty");
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
            {
                throw new ArgumentException("Invalid image URL format");
            }
        }


        /// <summary>
        /// The Dispose method.
        /// </summary>
        public void Dispose()
        {
            this.httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// The Tool class represents a tool that can be used in conjunction with the Groq API.
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// Gets or sets the Type property, which specifies the type of the tool. Default is "function".
        /// </summary>
        public string Type { get; set; } = "function";

        /// <summary>
        /// Gets or sets the Function property, which is an instance of the Function class representing the function to be executed.
        /// </summary>
        public Function Function { get; set; }
    }

    /// <summary>
    /// The Function class represents a function that can be executed as part of a tool in the Groq API.
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Gets or sets the Name property, which specifies the name of the function.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description property, which provides a brief description of the function.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Parameters property, which is a JsonObject representing the parameters of the function.
        /// </summary>
        public JsonObject Parameters { get; set; }

        /// <summary>
        /// Gets or sets the ExecuteAsync property, which is a function that takes a string argument and returns a Task.<string>.
        /// </summary>
        public Func<string, Task<string>> ExecuteAsync { get; set; }
    }
}
