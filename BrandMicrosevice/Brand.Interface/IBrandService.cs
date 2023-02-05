
using System;
using System.Collections.Generic;
using System.Text;
using BrandMicro.DTO;
using BrandMicro.Model;
using BrandMicro.Model.Models;

namespace BrandMicro.Interface
{
    public interface IBrandService
    {
        PageResult<Brand> QueryBrandByPageAndSort(int page, int rows, string sortBy, bool desc, string key);

        void SaveBrand(Brand brand, List<long> cids);

        void UpdateBrand(BrandDTO brandbo);

        void DeleteBrand(long bid);

        List<Brand> QueryBrandByCid(long cid);

        Brand QueryBrandByBid(long id);

        List<Brand> QueryBrandByIds(List<long> ids);

    }
}
