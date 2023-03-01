using HMS.Entity.Masters.titles.Request;
using HMS.Entity.Masters.titles.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BL.Interfaces.Masters
{
    public interface ITitleBL
    {
        public Task<List<TitleResponse>> GetTitleDetails();

        public Task<List<TitleResponse>> GetTitleDetailsByID(TitleRequest titleRequest);
        public Task<TitleResponse> SaveTitle(TitleSaveRequest titleSaveRequest);
    }
}
