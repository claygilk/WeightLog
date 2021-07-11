using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightLog.Models;

namespace WeightLog.DAL
{
    public interface IMaxDAO
    {
        public List<Max> GetMaxes(int userId);

        public Max GetLiftMax(int userId, string lift);
    }
}
