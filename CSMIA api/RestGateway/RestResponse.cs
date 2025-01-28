using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSMIA_api
{
    public class RestResponse<T> : IDisposable
    {
        private Boolean issuccessful;
        private T resultdata;
        private HttpStatusCode statuscode;
        private string errormessage;

        public Boolean IsSuccessful { get { return issuccessful; } set { issuccessful = value; } }
        public T ResultData { get { return resultdata; } set { resultdata = value; } }
        public HttpStatusCode StatusCode { get { return statuscode; } set { statuscode = value; } }
        public string ErrorMessage { get { return errormessage; } set { errormessage = value; } }
        public Exception Error { get; set; }
        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}
