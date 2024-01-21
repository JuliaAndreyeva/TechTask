using JsonTree.Data;
using JsonTree.Interfaces;
using JsonTree.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JsonTree.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ObjectRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public void AddObject(ObjectModel objectModel)
        {
            _context.Add(objectModel);
            _context.SaveChanges();
        }
        public void AddChild(ObjectChild childModel)
        {
            _context.Add(childModel);
            _context.SaveChanges();
        }
        public void UpdateChild(ObjectChild childModel)
        {
            _context.Update(childModel);
            _context.SaveChanges();
        }
        public void UpdateObject(ObjectModel objectModel)
        {
            _context.Update(objectModel);
            _context.SaveChanges();
        }
        public int FindObjectIdByKeyNullable(string key)
        {
            return _context.ObjectModels.AsNoTracking().FirstOrDefault(i => i.KeyName == key && i.Value == null)?.Id ?? 0;
        }
        public List<ObjectModel> FindAllObjects()
        {
            return _context.ObjectModels.AsNoTracking().ToList();
        }
        public List<ObjectChild> FindAllChildrens()
        {
            return _context.ObjectChilds.AsNoTracking().ToList();
        }
        public ObjectModel FindByKeyObjectNullable(string key)
        {
            return _context.ObjectModels.AsNoTracking().FirstOrDefault(i => i.KeyName == key && i.Value == null);
        }
        public ObjectModel FindByKeyObject(string key)
        {
            return _context.ObjectModels.AsNoTracking().FirstOrDefault(i => i.KeyName == key);
        }
        public List<int> FindChildrensId(int ParentId)
        {
            var childrenIds = _context.ObjectChilds
                .Where(child => child.ParentId == ParentId)
                .Select(child => child.ChildId)
                .ToList();

            return childrenIds.Any() ? childrenIds : null;
        }
        public List<ObjectModel> FindObjectsByChildId(int parentId)
        {
            var childIds = _context.ObjectChilds
                .Where(child => child.ParentId == parentId)
                .Select(child => child.ChildId)
                .ToList();


            var models = _context.ObjectModels
              .Where(model => childIds.Contains(model.Id))
              .ToList();

            return models;
        }

    }
}
