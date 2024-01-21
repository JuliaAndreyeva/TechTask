using JsonTree.Models;

namespace JsonTree.Interfaces
{
    public interface IObjectRepository
    {
        public void AddObject(ObjectModel objectModel);
        public void AddChild(ObjectChild objectModel);
        public void UpdateChild(ObjectChild objectModel);
        public void UpdateObject(ObjectModel objectModel);
        public int FindObjectIdByKeyNullable(string key);
        public ObjectModel FindByKeyObjectNullable(string key);
        public ObjectModel FindByKeyObject(string key);
        public List<int> FindChildrensId(int ParentId);
        public List<ObjectModel> FindAllObjects();
        public List<ObjectChild> FindAllChildrens();
        public List<ObjectModel> FindObjectsByChildId(int parentId);

    }
}
