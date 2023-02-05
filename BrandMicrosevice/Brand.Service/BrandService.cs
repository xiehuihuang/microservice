using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrandMicro.Model;
using BrandMicro.Model.Models;
using BrandMicro.Interface;
using BrandMicro.DTO;

namespace BrandMicro.Service
{
    public class BrandService : IBrandService
    {
        private DbEntitys _db;
        public BrandService(DbEntitys dbEntitys)
        {
            _db = dbEntitys;
        }

        public void DeleteBrand(long bid)
        {
            throw new NotImplementedException();
        }

        public Brand QueryBrandByBid(long id)
        {
            Brand b1 = _db.Brand.Where(m => m.Id == id).FirstOrDefault();
            if (b1 == null)
            {
                throw new Exception("查询品牌不存在");
            }
            return b1;
        }

        public List<Brand> QueryBrandByCid(long cid)
        {
            var brandList = _db.Brand.FromSqlRaw($"select * from tb_brand where id in (select brand_id from tb_category_brand where category_id = {cid})").ToList();
            if (brandList.Count <= 0)
            {
                throw new Exception("没有找到分类下的品牌");
            }
            return brandList;
        }

        public List<Brand> QueryBrandByIds(List<long> ids)
        {
            List<Brand> brands = _db.Brand.Where(m => ids.Contains(m.Id)).ToList();
            if (brands.Count <= 0)
            {
                throw new Exception("查询品牌不存在");
            }
            return brands;
        }

        public PageResult<Brand> QueryBrandByPageAndSort(int page, int rows, string sortBy, bool desc, string key)
        {
            var list = _db.Brand.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                list = list.Where(m => m.Name.Contains(key) || m.Letter == key);
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                string sortByClause = sortBy + (desc ? " DESC" : " ASC");
                if (desc)
                {
                    list.OrderByDescending(m => m.Letter);
                }
            }

            var total = list.Count();
            var Brands = list.Take(10).ToList();
            if (Brands.Count() <= 0)
            {
                throw new Exception("查询的品牌列表为空");
            }
            var data = new PageResult<Brand>(total, Brands);
            return data;
        }

        public void SaveBrand(Brand brand, List<long> cids)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrand(BrandDTO brandbo)
        {
            throw new NotImplementedException();
        }
    }
}
