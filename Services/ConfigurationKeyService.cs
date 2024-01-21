using JsonTree.Interfaces;
using JsonTree.Models;

namespace JsonTree.Services
{
    public class ConfigurationKeyService
    {
        private readonly IObjectRepository _objectRepository;
        public ConfigurationKeyService(IObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;
        }
        public List<string> GetConfig(string[] array)
        {
            if (array.Length == 0)
                throw new ArgumentException("Array is empty");

            List<string> subTree = new List<string>();

            var rootKeyName = array[array.Length - 1];

            var rootObjectModel = _objectRepository.FindByKeyObjectNullable(rootKeyName);

            if(rootObjectModel is null)
            {
                rootObjectModel = _objectRepository.FindByKeyObject(rootKeyName);
                subTree.Add($"{rootObjectModel.Value}");
            }

            if (rootObjectModel.Value is not null && !rootObjectModel.IsRoot)
            {
                subTree.Add($"{rootObjectModel.Value}");
            }
            else
            {
                GetSubtreeValue(rootObjectModel, subTree);               
            }
            return subTree;
        }       
        private void GetSubtreeValue(ObjectModel parent, List<string> subTree)
        {
            var childObjectModels = _objectRepository.FindObjectsByChildId(parent.Id);

            foreach (var childObjectModel in childObjectModels)
            {
                if (childObjectModel.Value is not null)
                {
                    subTree.Add($"{childObjectModel.KeyName}:{childObjectModel.Value}");
                }
                else
                {
                    subTree.Add($"{childObjectModel.KeyName}{{");
                    GetSubtreeValue(childObjectModel, subTree);
                    subTree.Add("},");
                }
            }
        }

    }
}
