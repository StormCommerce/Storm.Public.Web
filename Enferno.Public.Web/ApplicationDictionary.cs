
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Applications;

namespace Enferno.Public.Web
{
    public interface IApplicationDictionary
    {
        CategoryItem CategoryItem(int id, string cultureCode);
        IEnumerable<CategoryItem> Categories(string cultureCode);
        ManufacturerItem ManufacturerItem(int id, string cultureCode);
        IEnumerable<ManufacturerItem> Manufacturers(string cultureCode);
        Flag ProductFlag(int id, string cultureCode);
        IEnumerable<Flag> ProductFlags(string cultureCode);
        ParametricInfo ParametricInfo(int id, string cultureCode);
        ParametricValue ParametricValue(ParametricType type, int id, string cultureCode);
        Store Store(int id, string cultureCode);
        Store Store(string code, string cultureCode);
        IEnumerable<Store> Stores(string cultureCode);
        IEnumerable<Pricelist> Pricelists(string cultureCode);
        void Refresh();
    }

    public class ApplicationDictionary : IApplicationDictionary
    {
        private enum DataSet
        {
            Stores,
            CategoryItems,
            ManufacturerItems,
            ParametricInfo,
            ParametricValues,
            ProductFlags,
            Pricelists
        }
       
        #region Singleton stuff

        private static readonly object SyncRoot = new object();        
        
        private static IApplicationDictionary instance;

        public static IApplicationDictionary Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    if (instance != null) return instance;
                }

                lock (SyncRoot)
                {
                    if (instance != null) return instance;
                    instance = new ApplicationDictionary();
                }
                return instance;
            }
        }

        #endregion

        private Dictionary<string, ItemLookup> myDictionary = new Dictionary<string, ItemLookup>();

        public CategoryItem CategoryItem(int id, string cultureCode)
        {
            return Lookup<int, CategoryItem, CategoryItemLookup>(DataSet.CategoryItems, id, cultureCode);
        }

        public IEnumerable<CategoryItem> Categories(string cultureCode)
        {
            var dictionary = GetDictionary<CategoryItemLookup>(DataSet.CategoryItems, cultureCode);
            return dictionary.Data.Values.AsEnumerable();
        }

        public ManufacturerItem ManufacturerItem(int id, string cultureCode)
        {
            return Lookup<int, ManufacturerItem, ManufacturerItemLookup>(DataSet.ManufacturerItems, id, cultureCode);
        }

        public IEnumerable<ManufacturerItem> Manufacturers(string cultureCode)
        {
            var dictionary = GetDictionary<ManufacturerItemLookup>(DataSet.ManufacturerItems, cultureCode);
            return dictionary.Data.Values.AsEnumerable();
        }

        public Flag ProductFlag(int id, string cultureCode)
        {
            return Lookup<int, Flag, ProductFlagLookup>(DataSet.ProductFlags, id, cultureCode);
        }

        public IEnumerable<Flag> ProductFlags(string cultureCode)
        {
            var dictionary = GetDictionary<ProductFlagLookup>(DataSet.ProductFlags, cultureCode);
            return dictionary.Data.Values.AsEnumerable();
        }

        public ParametricInfo ParametricInfo(int id, string cultureCode)
        {
            return Lookup<int, ParametricInfo, ParametricInfoLookup>(DataSet.ParametricInfo, id, cultureCode);
        }

        public ParametricValue ParametricValue(ParametricType type, int id, string cultureCode)
        {
            return Lookup<string, ParametricValue, ParametricValuesLookup>(DataSet.ParametricValues, Id(type, id), cultureCode);
        }

        public Store Store(int id, string cultureCode)
        {
            return Lookup<int, Store, StoreLookup>(DataSet.Stores, id, cultureCode);
        }

        /// <summary>
        /// Use Store(id) if possible since this one is not indexed.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cultureCode"></param>
        /// <returns></returns>
        public Store Store(string code, string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            var dictionary = GetDictionary<StoreLookup>(DataSet.Stores, cultureCode);
            return dictionary.Data.Values.FirstOrDefault(i => i.Code == code);
        }

        public IEnumerable<Store> Stores(string cultureCode)
        {
            var dictionary = GetDictionary<StoreLookup>(DataSet.Stores, cultureCode);
            return dictionary.Data.Values.AsEnumerable();
        }

        public IEnumerable<Pricelist> Pricelists(string cultureCode)
        {
            var dictionary = GetDictionary<PricelistLookup>(DataSet.Pricelists, cultureCode);
            return dictionary.Data.Values.AsEnumerable();
        }

        private static string Id(ParametricType type, int id)
        {
            return string.Concat(type == ParametricType.Value ? "V" : type == ParametricType.ListValue ? "L" : "M", id);
        }

        private readonly Stopwatch stopwatch = new Stopwatch();

        private T Lookup<TS, T, TU>(DataSet ds, TS id, string cultureCode) where TU : ItemLookup<TS, T>, new()
        {
            var dictionary = GetDictionary<TU>(ds, cultureCode);
            var data = dictionary.Lookup(id);

            if (data != null)
            {
                stopwatch.Stop();
                return data;
            }

            lock (SyncRoot)
            {
                // reload if null and not 5 min since last reload
                if (stopwatch.IsRunning && stopwatch.Elapsed.Minutes < 5) return default(T);

                stopwatch.Restart();

                data = dictionary.Lookup(id);
                if (data != null) return data;

                dictionary.Load(IoC.Resolve<IRepository>(), cultureCode);
                data = dictionary.Lookup(id);
            }

            return data;
        }

        private TU GetDictionary<TU>(DataSet ds, string cultureCode) where TU : ItemLookup, new()
        {
            var key = Key(ds, cultureCode);

            lock (SyncRoot)
            {
                if (myDictionary.ContainsKey(key))
                {
                    return myDictionary[key] as TU;
                }
            }

            lock (SyncRoot)
            {
                if (!myDictionary.ContainsKey(key))
                {
                    var dictionary = new TU();
                    var repository = IoC.Resolve<IRepository>();
                    dictionary.Load(repository, cultureCode);
                    myDictionary[key] = dictionary;
                }
            }
            return myDictionary[key] as TU;
        }

        private static string Key(DataSet dataset, string culture)
        {
            return string.Concat(dataset, ":", culture);
        }

        /// <summary>
        /// Refreshes the Lookup data. Gets called by the scheduler in N2.
        /// </summary>
        public void Refresh()
        {
            lock (SyncRoot)
            {
                myDictionary = new Dictionary<string, ItemLookup>();
            }
        }
    }

    public abstract class ItemLookup
    {
        public abstract void Load(IRepository repository, string cultureCode);
    }

    public abstract class ItemLookup<TS, T> : ItemLookup
    {
        protected string CultureCode;
        internal Dictionary<TS, T> Data;        

        public T Lookup(TS key)
        {
            return Data.ContainsKey(key) ? Data[key] : default(T);
        }
    }

    public class CategoryItemLookup : ItemLookup<int, CategoryItem>
    {   
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = new Dictionary<int, CategoryItem>();
            var tree = repository.Products.ListCategoryItems(cultureCode);
            Add(tree);
        }

        private void Add(IReadOnlyCollection<CategoryItem> tree)
        {
            if (tree == null || tree.Count == 0) return;
            foreach (var item in tree)
            {
                Add(item.Children);

                if (!Data.ContainsKey(item.CategoryId))
                {
                    Data.Add(item.CategoryId, new CategoryItem
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        CategoryId = item.CategoryId,
                        Name = item.Name,
                        Description = item.Description,
                        ImageKey = item.ImageKey,
                        Level = item.Level,
                        IsDisplayOnly = item.IsDisplayOnly,
                        Key = item.Key
                    });
                }
            }
        }
    }

    public class ManufacturerItemLookup : ItemLookup<int, ManufacturerItem>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Products.ListManufacturers(null, null, null, cultureCode).Items.ToDictionary(m => m.Id, m => m);
        }
    }

    public class ParametricInfoLookup : ItemLookup<int, ParametricInfo>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Products.ListParametricInfo(cultureCode).ToDictionary(p => p.Id, p => p);
        }
    }

    public class ParametricValuesLookup : ItemLookup<string, ParametricValue>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Products.ListParametricValues(cultureCode).ToDictionary(p => Id(p.Type, p.Id), p => p);
        }

        private static string Id(string type, int id)
        {
            return string.Concat(type == ParametricType.Value.ToString() ? "V" : type == ParametricType.ListValue.ToString() ? "L" : "M", id);
        }
    }

    public class StoreLookup : ItemLookup<int, Store>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Application.ListStores(cultureCode).ToDictionary(p => p.Id, p => p);
        }
    }

    public class ProductFlagLookup : ItemLookup<int, Flag>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Products.ListProductFlags(cultureCode).ToDictionary(p => p.Id, p => p);
        }
    }

    public class PricelistLookup : ItemLookup<int, Pricelist>
    {
        public override void Load(IRepository repository, string cultureCode)
        {
            CultureCode = cultureCode;
            Data = repository.Products.ListPricelists(cultureCode).ToDictionary(p => p.Id, p => p);
        }
    }
}

