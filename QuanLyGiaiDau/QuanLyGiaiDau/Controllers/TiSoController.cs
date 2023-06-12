using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QuanLyGiaiDau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiaiDau.Controllers
{
    public class TiSoController : Controller
    {
        private readonly QuanLyGiaiDauContext _context;
        public TiSoController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/danh-sach-bang-dau")]
        public async Task<ActionResult<IEnumerable<TiSoModel>>> GetTiSo()
        {
            List<CT_TranDau> a = await _context.CT_TranDaus.ToListAsync();

            
            List<TiSoModel> TiSos = new List<TiSoModel>();
            TiSoModel tiSoModel = new TiSoModel();
            

            for (int i = 0; i < a.Count; i++)
            {
                for (int j = i+1; j < a.Count; j++)
                {
                    if (a[i] == a[j])
                    {
                        continue;
                    }
                    
                    if (a[i].IdGiaiDau == a[j].IdGiaiDau && a[i].SanDau == a[j].SanDau && a[i].ThoiGianBatDau == a[j].ThoiGianBatDau && a[i].VongDau == a[j].VongDau)
                    {
                        tiSoModel = new TiSoModel();
                        tiSoModel.Doi1 = _context.DoiDaus.Where(x => x.IdDoiDau == a[i].IdDoiDau).FirstOrDefault();
                        tiSoModel.Doi2 = _context.DoiDaus.Where(x => x.IdDoiDau == a[j].IdDoiDau).FirstOrDefault();
                        tiSoModel.TiSoDoi1 = a[i].TiSo;
                        tiSoModel.TiSoDoi2 = a[j].TiSo;
                        tiSoModel.ThoiGianBatDau = a[i].ThoiGianBatDau;
                        tiSoModel.GiaiDau = _context.GiaiDaus.Where(x=>x.IdGiaiDau == a[i].IdGiaiDau).FirstOrDefault();
                        tiSoModel.SanDau = a[i].SanDau;
                        tiSoModel.GiaiDau.MonTheThao = _context.MonTheThaos.Where(x => x.IdMonTheThao == a[i].GiaiDau.IdMonTheThao).FirstOrDefault();
                        //tiSoModel.GiaiDau.LoaiGiaiDau.MonTheThao = _context.MonTheThaos.Where(x => x.IdMonTheThao == a[i].GiaiDau.LoaiGiaiDau.IdMonTheThao).FirstOrDefault();
                        if (a[i].KetQua =="Win" && a[j].KetQua =="Lose")
                        {
                            tiSoModel.DoiThang = 1;
                        }
                        else if(a[j].KetQua =="Win" && a[i].KetQua =="Lose")
                        {
                            tiSoModel.DoiThang = 2;
                        }    
                        else if (a[i].KetQua == null && a[j].KetQua==null)
                        {
                            tiSoModel.DoiThang= 0;
                        }    
                        else if(a[i].KetQua =="Tie" && a[j].KetQua =="Tie")
                        {
                            tiSoModel.DoiThang = 3;
                        }    
                        TiSos.Add(tiSoModel);
                        
                        break;

                    }
                }
            }
            return TiSos;
        }
        [HttpPut]
        [Route("api/update-ti-so")]
        public async Task<IActionResult> UpdateTiSo([FromBody] TiSoModel TiSo)
        {
            var UpdateDoi1 = await _context.CT_TranDaus.Where(x => x.IdGiaiDau.Equals(TiSo.GiaiDau.IdGiaiDau) && x.IdDoiDau.Equals(TiSo.Doi1.IdDoiDau) && x.SanDau.Equals(TiSo.SanDau)
            && x.ThoiGianBatDau.Equals(TiSo.ThoiGianBatDau)).FirstAsync();
            var UpdateDoi2 = await _context.CT_TranDaus.Where(x => x.IdGiaiDau.Equals(TiSo.GiaiDau.IdGiaiDau) && x.IdDoiDau.Equals(TiSo.Doi2.IdDoiDau) && x.SanDau.Equals(TiSo.SanDau)
            && x.ThoiGianBatDau.Equals(TiSo.ThoiGianBatDau)).FirstAsync();
            //CT_TranDau updateD1 = new CT_TranDau();
            //CT_TranDau updateD2 = new CT_TranDau();

            //var UpdateDoi1 = await _context.CT_TranDaus.Where(x => x.IdDoiDau.Equals(TiSo.Doi1.IdDoiDau.Trim())).FirstAsync();
            //var UpdateDoi2 = await _context.CT_TranDaus.Where(x => x.IdDoiDau.Equals(TiSo.Doi2.IdDoiDau.Trim())).FirstAsync();
            UpdateDoi1.TiSo = TiSo.TiSoDoi1;
            UpdateDoi2.TiSo = TiSo.TiSoDoi2;
            //if (TiSo.DoiThang.Equals(1))
            //{
            //    UpdateDoi1.KetQua = "Win";
            //    UpdateDoi2.KetQua = "Lose";
            //}
            //if (TiSo.DoiThang.Equals(2))
            //{
            //    UpdateDoi1.KetQua = "Lose";
            //    UpdateDoi2.KetQua = "Win";
            //}
            //if (TiSo.DoiThang.Equals(0))
            //{
            //    UpdateDoi1.KetQua = null;
            //    UpdateDoi2.KetQua = null;
            //}

            _context.Entry(UpdateDoi1).State = EntityState.Modified;
            _context.Entry(UpdateDoi2).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            

            return NoContent();
        }

        [HttpPut]
        [Route("api/ket-thuc-tran-dau")]
        public async Task<IActionResult> KetThucTran([FromBody] TiSoModel TiSo)
        {
            var UpdateDoi1 = await _context.CT_TranDaus.Where(x => x.IdGiaiDau.Equals(TiSo.GiaiDau.IdGiaiDau) && x.IdDoiDau.Equals(TiSo.Doi1.IdDoiDau) && x.SanDau.Equals(TiSo.SanDau)
            && x.ThoiGianBatDau.Equals(TiSo.ThoiGianBatDau)).FirstAsync();
            var UpdateDoi2 = await _context.CT_TranDaus.Where(x => x.IdGiaiDau.Equals(TiSo.GiaiDau.IdGiaiDau) && x.IdDoiDau.Equals(TiSo.Doi2.IdDoiDau) && x.SanDau.Equals(TiSo.SanDau)
            && x.ThoiGianBatDau.Equals(TiSo.ThoiGianBatDau)).FirstAsync();
            //CT_TranDau updateD1 = new CT_TranDau();
            //CT_TranDau updateD2 = new CT_TranDau();

            //var UpdateDoi1 = await _context.CT_TranDaus.Where(x => x.IdDoiDau.Equals(TiSo.Doi1.IdDoiDau.Trim())).FirstAsync();
            //var UpdateDoi2 = await _context.CT_TranDaus.Where(x => x.IdDoiDau.Equals(TiSo.Doi2.IdDoiDau.Trim())).FirstAsync();
            UpdateDoi1.TiSo = TiSo.TiSoDoi1;
            UpdateDoi2.TiSo = TiSo.TiSoDoi2;
            if (TiSo.TiSoDoi1 > TiSo.TiSoDoi2)
            {
                UpdateDoi1.KetQua = "Win";
                UpdateDoi2.KetQua = "Lose";
            }
            if (TiSo.TiSoDoi1 < TiSo.TiSoDoi2)
            {
                UpdateDoi1.KetQua = "Lose";
                UpdateDoi2.KetQua = "Win";
            }
            if (TiSo.TiSoDoi1 == TiSo.TiSoDoi2)
            {
                UpdateDoi1.KetQua = "Tie";
                UpdateDoi2.KetQua = "Tie";
            }
            
            _context.Entry(UpdateDoi1).State = EntityState.Modified;
            _context.Entry(UpdateDoi2).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpGet]
        [Route("api/danh-sach-bang-dau/{id}")]
        public async Task<ActionResult<IEnumerable<TiSoModel>>> SearchTranDau(string id)
        {
            List<GiaiDau> giaiDaus = new List<GiaiDau>();
            giaiDaus = await _context.GiaiDaus.Where(x=>x.IdMonTheThao==id).ToListAsync();
            //List<CT_TranDau> a = new List<CT_TranDau>();
            List<CT_TranDau> a = await _context.CT_TranDaus
    .Join(
        _context.GiaiDaus,
        ct => ct.IdGiaiDau,
        gd => gd.IdGiaiDau,
        (ct, gd) => new { CT_TranDau = ct, GiaiDau = gd }
    )
    .Where(x => x.GiaiDau.IdMonTheThao == id)
    .Select(x => x.CT_TranDau)
    .ToListAsync();



            List<TiSoModel> TiSos = new List<TiSoModel>();
            TiSoModel tiSoModel = new TiSoModel();


            for (int i = 0; i < a.Count; i++)
            {
                for (int j = i + 1; j < a.Count; j++)
                {
                    if (a[i] == a[j])
                    {
                        continue;
                    }

                    if (a[i].IdGiaiDau == a[j].IdGiaiDau && a[i].SanDau == a[j].SanDau && a[i].ThoiGianBatDau == a[j].ThoiGianBatDau && a[i].VongDau == a[j].VongDau)
                    {
                        tiSoModel = new TiSoModel();
                        tiSoModel.Doi1 = _context.DoiDaus.Where(x => x.IdDoiDau == a[i].IdDoiDau).FirstOrDefault();
                        tiSoModel.Doi2 = _context.DoiDaus.Where(x => x.IdDoiDau == a[j].IdDoiDau).FirstOrDefault();
                        tiSoModel.TiSoDoi1 = a[i].TiSo;
                        tiSoModel.TiSoDoi2 = a[j].TiSo;
                        tiSoModel.ThoiGianBatDau = a[i].ThoiGianBatDau;
                        tiSoModel.GiaiDau = _context.GiaiDaus.Where(x => x.IdGiaiDau == a[i].IdGiaiDau).FirstOrDefault();
                        tiSoModel.SanDau = a[i].SanDau;
                        tiSoModel.GiaiDau.MonTheThao = _context.MonTheThaos.Where(x => x.IdMonTheThao == a[i].GiaiDau.IdMonTheThao).FirstOrDefault();
                        //tiSoModel.GiaiDau.LoaiGiaiDau.MonTheThao = _context.MonTheThaos.Where(x => x.IdMonTheThao == a[i].GiaiDau.LoaiGiaiDau.IdMonTheThao).FirstOrDefault();
                        if (a[i].KetQua == "Win" && a[j].KetQua == "Lose")
                        {
                            tiSoModel.DoiThang = 1;
                        }
                        else if (a[j].KetQua == "Win" && a[i].KetQua == "Lose")
                        {
                            tiSoModel.DoiThang = 2;
                        }
                        else if (a[i].KetQua == null && a[j].KetQua == null)
                        {
                            tiSoModel.DoiThang = 0;
                        }
                        else if (a[i].KetQua == "Tie" && a[j].KetQua == "Tie")
                        {
                            tiSoModel.DoiThang = 3;
                        }
                        TiSos.Add(tiSoModel);

                        break;

                    }
                }
            }
            return TiSos;
        }
    }

}
