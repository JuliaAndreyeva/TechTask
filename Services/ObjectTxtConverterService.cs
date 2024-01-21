using JsonTree.Interfaces;
using Newtonsoft.Json;

namespace JsonTree.Services
{
    public class ObjectTxtConverterService
    {
        private readonly IObjectRepository _objectRepository;
        public ObjectTxtConverterService(IObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;
        }
        public string ConvertTxtToJson(string content, string filePath)
        {
            string[] lines = content.Split('\n');

            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');

                Dictionary<string, object> currentLevel = result;

                for (int i = 0; i < parts.Length - 2; i++)
                {
                    string key = parts[i];

                    if (!currentLevel.ContainsKey(key))
                    {
                        currentLevel[key] = new Dictionary<string, object>();
                    }

                    currentLevel = (Dictionary<string, object>)currentLevel[key];
                }

                string lastKey = parts[parts.Length - 2];
                string lastValue = parts[parts.Length - 1];

                lastValue = lastValue.Trim();

                currentLevel[lastKey] = lastValue;
            }

            string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

            File.WriteAllText(filePath, jsonResult);

            return jsonResult;
        }
    }

}




