namespace asp02di1_sk2.Services
{
    public class ValueStorage
    {
        private List<int> items = new List<int>();
        private ILogger<ValueStorage> _logger;

        public List<int> Items { get { return items; } }

        public ValueStorage(ILogger<ValueStorage> logger)
        {
            _logger = logger;
            _logger.LogInformation("Creating an instance of service");
            for (int i = 0; i < 10; i++)
            {
                items.Add(Random.Shared.Next(10));
            }
            _logger.LogDebug("Items are:" + String.Join(",", items));
        }

        public List<int> List()
        {
            return items;
        }

        public int GetItem(int id)
        {
            try
            {
                if (id < 0 || id >= items.Count)
                {
                    throw new IndexOutOfRangeException($"Index {id} is out of range");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return items[id];
        }

        public void AddItem(int item)
        {
            items.Add(item);
        }

        public void RemoveItem(int id)
        {
            try 
            {
                if (id < 0 || id >= items.Count)
                {
                    throw new IndexOutOfRangeException($"Index {id} is out of range");
                }
                items.RemoveAt(id);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void ReplaceItem(int id, int item)
        {
            try 
            {
                if (id < 0 || id >= items.Count)
                {
                    throw new IndexOutOfRangeException($"Index {id} is out of range");
                }
                items[id] = item;
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}
