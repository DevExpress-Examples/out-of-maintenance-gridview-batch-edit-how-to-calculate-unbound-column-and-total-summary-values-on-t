using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Models {
    public class BatchEditRepository {
        public static List<GridDataItem> GridData {
            get {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
                var Session = HttpContext.Current.Session;
                if (Session[key] == null)
                    Session[key] = Enumerable.Range(0, 3).Select(i => new GridDataItem
                    {
                        ID = i,
                        Mon = i * 10 % 3,
                        Tue = i * 5 % 3,
                        Wen = i % 2
                    }).ToList();
                return (List<GridDataItem>)Session[key];
            }
        }
        public static GridDataItem InsertNewItem(GridDataItem postedItem) {
            var newItem = new GridDataItem() { ID = GridData.Count };
            LoadNewValues(newItem, postedItem);
            GridData.Add(newItem);
            return newItem;
        }
        public static GridDataItem UpdateItem(GridDataItem postedItem) {
            var editedItem = GridData.First(i => i.ID == postedItem.ID);
            LoadNewValues(editedItem, postedItem);
            return editedItem;
        }
        public static GridDataItem DeleteItem(int itemKey) {
            var item = GridData.First(i => i.ID == itemKey);
            GridData.Remove(item);
            return item;
        }
        protected static void LoadNewValues(GridDataItem newItem, GridDataItem postedItem) {
            newItem.Mon = postedItem.Mon;
            newItem.Tue = postedItem.Tue;
            newItem.Wen = postedItem.Wen;
        }
    }

    public class GridDataItem {
        public int ID { get; set; }
        public int Mon { get; set; }
        public int Tue { get; set; }
        public int Wen { get; set; }
    }
}