using HMS.Entity.Masters.titles.Request;
using HMS.Entity.Masters.titles.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal.Interfaces.Masters
{
    public interface ITitleDL
    {
        public Task<List<TitleResponse>> GetTitleDetails();

        public Task<TitleResponse> SaveTitle(TitleSaveRequest createTitleRequest);
      
        public Task<List<TitleResponse>> GetTitleDetailsByID(TitleRequest titleRequest);
    }
}
