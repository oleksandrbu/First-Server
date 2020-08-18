using System.Collections.Generic;

namespace First_Server{
    public interface IRequest{
        public List<(string, string)> Request();
    }
}