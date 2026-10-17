using System;
using System.Collections.Generic;
using System.Text;

namespace Geometry.Core
{
    public interface IResultStorage
    {
        void Save(string result);
    }
    public class FileResultStorage : IResultStorage
    {
        private readonly string _path;
        public FileResultStorage(string path) => _path = path;
        public void Save(string result) => File.AppendAllText(_path, result + Environment.NewLine);
    }
    public class InMemoryResultStorage : IResultStorage
    {
        private readonly List<string> _saved = new();
        public IReadOnlyList<string> Saved => _saved;
        public void Save(string result) => _saved.Add(result);
    }
    public class ShapeManager
    {
        private readonly ShapeCollection _collection;
        private readonly IResultStorage _storage;

        public ShapeManager(ShapeCollection collection, IResultStorage storage)
        {
            _collection = collection;
            _storage = storage;
        }
        public void AddAndSave(Shape shape)
        {
            _collection.Add(shape);
            _storage.Save($"Добавлена: {shape}");
        }
        public void ReportAndSave()
        {
            _storage.Save($"Фигур: {_collection.Shapes.Count}, S={_collection.TotalArea():F2}");
        }
    }

}
