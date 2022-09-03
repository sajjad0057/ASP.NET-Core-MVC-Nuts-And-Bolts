using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstDemo.Infrastructure.UnitOfWorks
{
    public class TrainingUnitOfWork
    {
        private List<object> _itemsToAdd;
        private List<object> _itemsToDelete;
        private List<object> _itemsToModify;

        public void Add(object item)
        {
            _itemsToAdd.Add(item);
        }







        public void Save()
        {
            using SqlTransaction transaction;
            try
            {
                foreach (var item in _itemsToAdd)
                {
                    ///// code for adding database for find Repository
                    ///// Add to repository 
                }

                foreach(var i in _itemsToDelete)
                {
                    /////
                }

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
        }
    }
}
 