using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
