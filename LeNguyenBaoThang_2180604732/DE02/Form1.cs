using DE02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace De02
{
    public partial class frmSanPham : Form
    {
        Model1 model = new Model1();
        public frmSanPham()
        {

            InitializeComponent();
        }
        List<SanPham> sanPhams = new List<SanPham>();
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            var find = model.SanPhams.FirstOrDefault(s => s.MaSP == txtMaSP.Text);
            if (find == null)
            {
                SanPham sp = new SanPham()
                {
                    MaSP = txtMaSP.Text,
                    TenSP = txtTenSP.Text,
                    NgayNhap = dateTimePicker1.Value,
                    MaLoai = ((LoaiSP)cbLoai.SelectedItem)?.MaLoai
                };
                model.SanPhams.Add(sp);
                model.SaveChanges();
            }

            List<SanPham> ls = model.SanPhams.ToList();
            BindList(ls);

        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {

            List<SanPham> listSP = model.SanPhams.ToList();
            List<LoaiSP> listLoaiSP = model.LoaiSPs.ToList();
            FillTenLoaiCombobox(listLoaiSP);
            BindList(listSP);
        }
        private void BindList(List<SanPham> sanPhams)
        {
            listView1.Items.Clear();
            foreach (SanPham items in sanPhams)
            {
                var list = new ListViewItem(items.MaSP);
                list.SubItems.Add(items.TenSP);
                list.SubItems.Add(items.NgayNhap.ToString("dd/MM/yyyy"));
                string tenLoai = items.LoaiSP != null ? items.LoaiSP.TenLoai : "";
                list.SubItems.Add(tenLoai);
                listView1.Items.Add(list);
            }
        }
        private void FillTenLoaiCombobox(List<LoaiSP> loaiSPs)
        {
            this.cbLoai.DataSource = loaiSPs;
            this.cbLoai.DisplayMember = "TenLoai";
            this.cbLoai.ValueMember = "MaLoai";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text;
            string tenSP = txtTenSP.Text;
            DateTime ngayNhap = dateTimePicker1.Value;
            string maLoai = ((LoaiSP)cbLoai.SelectedItem)?.MaLoai;


            SanPham existingSanPham = model.SanPhams.FirstOrDefault(sp => sp.MaSP == maSP);

            if (existingSanPham != null)
            {

                existingSanPham.TenSP = tenSP;
                existingSanPham.NgayNhap = ngayNhap;
                existingSanPham.MaLoai = maLoai;
                model.SaveChanges();
                List<SanPham> ls = model.SanPhams.ToList();
                BindList(ls);

                txtMaSP.Text = "";
                txtTenSP.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                cbLoai.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Khong tim thay ma san pham trong danh sach.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
