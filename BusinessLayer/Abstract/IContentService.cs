using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList();
        List<Content> GetListByContentValue(string value);
        List<Content> GetListByWriter(int id);
        List<Content> GetListByID(int id);
        void ContentAddBL(Content content);
        Content GetByID(int id);
        void ContentDelete(Content content);
        void ContentUpdate(Content content);
    }
}
