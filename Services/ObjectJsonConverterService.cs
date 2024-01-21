using JsonTree.Interfaces;
using JsonTree.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace JsonTree.Services
{
    public class ObjectJsonConverterService : JsonConverter<ObjectModel>
    {
        private readonly IObjectRepository _objectRepository;
        public ObjectJsonConverterService(IObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;
        }

        public override ObjectModel ReadJson(JsonReader reader, Type objectType, ObjectModel? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var objectChild = new ObjectChild();

            JObject obj = JObject.Load(reader);

            Dictionary<string, JToken> childrenDictionary = obj.Properties()
                .ToDictionary(property => property.Name, property => property.Value);

            GetChildren(childrenDictionary, null);

            return default!;
        }

        public Dictionary<string, JToken> GetChildren(Dictionary<string, JToken> childrenDictionary, string? parent_tokenkey)
        {
            Dictionary<string, JToken> result = new Dictionary<string, JToken>();

            for (int i = 0; i < childrenDictionary.Count(); i++)
            {
                var token = childrenDictionary.ElementAt(i);

                var objectModel = new ObjectModel()
                {
                    //Id = idObj++,
                    KeyName = token.Key
                };
                //objectModels.Add(objectModel);
                _objectRepository.AddObject(objectModel);

                if (parent_tokenkey != null)
                {
                    var child = new ObjectChild()
                    {
                        ChildId = objectModel.Id,
                        ParentId = _objectRepository.FindObjectIdByKeyNullable(parent_tokenkey)
                    };
                    //objectChilds.Add(child);
                    _objectRepository.AddChild(child);
                }
                else
                    objectModel.IsRoot = true;

                if (token.Value.Children().Count() == 0)
                {
                    objectModel.Value = token.Value.ToString();
                    //objectModel.IsRoot = false;

                    //_objectRepository.UpdateChildModel(objectModel);//післе рінейма
                    _objectRepository.UpdateObject(objectModel);
                }
                else
                {
                    Dictionary<string, JToken> newChildrenDictionary = token.Value.Children()
                        .ToDictionary(child => ((JProperty)child).Name, child => ((JProperty)child).Value);


                    var childrenResult = GetChildren(newChildrenDictionary, token.Key);

                    foreach (var kvp in childrenResult)
                    {
                        result.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return result;
        }


        public override void WriteJson(JsonWriter writer, ObjectModel? value, Newtonsoft.Json.JsonSerializer serializer)
        {
           throw new NotImplementedException();
        }

        
    }

   
}
