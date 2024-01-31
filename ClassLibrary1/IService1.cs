using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClassLibrary1
{
   [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Movie> ReadMovies();

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        string Register(string username, string password);
    }
}
