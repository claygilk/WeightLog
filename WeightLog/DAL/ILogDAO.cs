using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightLog.Models;

namespace WeightLog.DAL
{
    public interface ILogDAO
    {
        public Set LogNewSet(Set set);

        public Set LookupSetById(int userId, int setId);
    }
}
