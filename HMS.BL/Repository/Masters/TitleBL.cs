using HMS.BL.Interfaces.Masters;
using HMS.Dal.Interfaces.Masters;
using HMS.Entity.Masters.titles.Request;
using HMS.Entity.Masters.titles.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BL.Repository.Masters
{
    public class TitleBL: ITitleBL
    {
        public ITitleDL TitleDL { get; set; }
        private readonly ITitleDL _titleDetailsDL;

        public TitleBL(ITitleDL titleDetailsDL)
        {
            _titleDetailsDL = titleDetailsDL;

        }

      
        public async Task<List<TitleResponse>> GetTitleDetails()
        {
            return await _titleDetailsDL.GetTitleDetails();
        }

        public async Task<TitleResponse> SaveTitle(TitleSaveRequest titleSaveRequest)
        {
            return await _titleDetailsDL.SaveTitle(titleSaveRequest);
        }

        

        public async Task<List<TitleResponse>> GetTitleDetailsByID(TitleRequest titleRequest)
            {
             return await _titleDetailsDL.GetTitleDetailsByID(titleRequest);
            }
        }
}
